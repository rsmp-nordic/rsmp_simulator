using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;

namespace nsRSMPGS
{

  public class cJSonSerializer : JavaScriptSerializer
  {

    public string SerializeObject(Object obj)
    {
      string sJSon = base.Serialize(obj);
      if (cHelper.IsSettingChecked("JSonPropertyCaseChange10"))
      {
        cHelper.ChangeJSONPropertiesCasing(obj, ref sJSon, 10);
      }
      return sJSon;
    }
  }


  public class cJSon
  {

    public enum AlarmSpecialisation
    {
      Unknown,
      Issue,
      Request,
      Acknowledge,
      Suspend,
      Resume
    }

    /*
        public const int AlarmSpecialisation_Alarm = 1;
        public const int AlarmSpecialisation_Acknowledge = 2;
        public const int AlarmSpecialisation_Suspend = 3;
        public const int AlarmSpecialisation_Request = 4;
    */

    public enum StatusMsgType
    {
      Request,
      Subscribe,
      UnSubscribe
    }

    /*
    public const int StatusMsgType_Request = 1;
    public const int StatusMsgType_Subscribe = 2;
    public const int StatusMsgType_UnSubscribe = 3;
    */


    public cJSonSerializer JSonSerializer = new cJSonSerializer();

    public DateTime LastSentWatchdogTimeStamp = DateTime.Now;
    public DateTime LastReceivedWatchdogTimeStamp = DateTime.Now;

    public cJSonMessageIdAndTimeStamp VersionPacket = null;

    // 2019-05-22/TR Removed as watchdog packet ack it should not be expected ni the initial sequence
    //public cJSonMessageIdAndTimeStamp WatchdogPacket = null;

    private bool bHaveGotVersionPacket = false;
    private bool bHaveGotVersionPacketAck = false;
    private bool bHaveGotWatchdogPacket = false;
    // 2019-05-22/TR Removed as watchdog packet ack it should not be expected ni the initial sequence
    //private bool bHaveGotWatchdogPacketAck = false;

    public bool bInitialNegotiationIsFinished = false;

    public RSMPVersion NegotiatedRSMPVersion = RSMPVersion.NotSupported;

    public List<cJSonMessageIdAndTimeStamp> JSonMessageIdAndTimeStamps = new List<cJSonMessageIdAndTimeStamp>();

    public enum RSMPVersion
    {
      // Numbering used to determine highest protocol number
      NotSupported = 0,

      RSMP_3_1_1 = 1,
      RSMP_3_1_2 = 2,
      RSMP_3_1_3 = 3,
      RSMP_3_1_4 = 4,
      RSMP_3_1_5 = 5,
      RSMP_3_2_0 = 6,
      RSMP_3_2_1 = 7,
      RSMP_3_2_2 = 8,
    }

    public string[] sRSMPVersions = { "", "3.1.1", "3.1.2", "3.1.3", "3.1.4", "3.1.5", "3.2.0", "3.2.1", "3.2.2" };

    public bool DecodeAndParseJSonPacket(string sJSon)
    {

      RSMP_Messages.Header Header;

      bool bSuccess = true;

      string sError = "";

      bool bUseStrictProtocolAnalysis = cHelper.IsSettingChecked("UseStrictProtocolAnalysis");
      bool bUseCaseSensitiveIds = cHelper.IsSettingChecked("UseCaseSensitiveIds");

      StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

      if (sJSon.Length == 0)
      {
        return true;
      }

      try
      {

        Header = JSonSerializer.Deserialize<RSMP_Messages.Header>(sJSon);

        RSMPGS.SysLog.AddJSonDebugData(cSysLogAndDebug.Direction_In, Header.type, sJSon);

        if (Header.mType == null || Header.type == null)
        {
          string sReason = "Packet header is bad";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sReason);
          return false;
        }

        if (Header.mType.Equals("rSMsg", sc) == false)
        {
          string sReason = "Packet Message Type is not 'rSMsg'";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sReason);
          SendPacketAck(false, Header.mId, sReason);
          return false;
        }

        // We could have closed the port because of some version issue, throw any packets
        if (RSMPGS.RSMPConnection.ConnectionStatus() != cTcpSocket.ConnectionStatus_Connected)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Threw packet we got when we are closing the connection, Type: {0}", Header.type);
          return false;
        }

        if (bUseStrictProtocolAnalysis)
        {
          if (Header.type.ToLower() != "messageack" && Header.type.ToLower() != "messagenotack")
          {
            bSuccess = ValidateGUID(Header.mId, ref sError);
          }
        }

        if (bUseStrictProtocolAnalysis == true && bSuccess == true)
        {

          switch (Header.type.ToLower())
          {
            case "version":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.rsVersion), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "Version", ref sError);

              break;

            case "messageack":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.MessageAck), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "MessageAck", ref sError);

              break;

            case "messagenotack":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.MessageNotAck), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "MessageNotAck", ref sError);

              break;

            case "alarm":
#if _RSMPGS2
              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.AlarmHeaderAndBody), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "Alarm", ref sError);

              // Validate the return value, but only if defined
              if (bSuccess && !(sJSon.Replace(" ", "").IndexOf("\"rvs\":[]", StringComparison.Ordinal) > 0))
              {
                bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.AlarmReturnValue), sJSon, ref sError);
              }
#endif
#if _RSMPGS1
              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.AlarmHeader), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "Alarm", ref sError);
#endif
              break;

            case "aggregatedstatus":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.AggregatedStatus), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "AggregatedStatus", ref sError);

              break;

            case "aggregatedstatusrequest":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.AggregatedStatusRequest), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "AggregatedStatusRequest", ref sError);

              break;

            case "commandrequest":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.CommandRequest), sJSon, ref sError) &&
                ValidateJSONProperties(typeof(RSMP_Messages.CommandRequest_Value), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "CommandRequest", ref sError);

              break;

            case "commandresponse":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.CommandResponse), sJSon, ref sError) &&
                ValidateJSONProperties(typeof(RSMP_Messages.CommandResponse_Value), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "CommandResponse", ref sError);

              break;

            case "statusrequest":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.StatusRequest), sJSon, ref sError) &&
                ValidateJSONProperties(typeof(RSMP_Messages.StatusRequest_Status), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "StatusRequest", ref sError);

              break;

            case "statussubscribe":
              if (NegotiatedRSMPVersion > RSMPVersion.RSMP_3_1_4)
              {
                bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.StatusSubscribe_Over_3_1_4), sJSon, ref sError) &&
                  ValidatePropertiesString(Header.type, "StatusSubscribe", ref sError);
              }
              else
              {
                bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.StatusSubscribe_UpTo_3_1_4), sJSon, ref sError) &&
                  ValidatePropertiesString(Header.type, "StatusSubscribe", ref sError);
              }

              break;

            case "statusunsubscribe":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.StatusUnsubscribe), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "StatusUnsubscribe", ref sError);

              break;

            case "statusresponse":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.StatusResponse), sJSon, ref sError) &&
                ValidateJSONProperties(typeof(RSMP_Messages.Status_VTQ), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "StatusResponse", ref sError);

              break;

            case "statusupdate":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.StatusUpdate), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "StatusUpdate", ref sError);
              break;

            case "watchdog":

              bSuccess = ValidateJSONProperties(typeof(RSMP_Messages.Watchdog), sJSon, ref sError) &&
                ValidatePropertiesString(Header.type, "Watchdog", ref sError);
              break;

            default:
              sError = "Unknown packet type: " + Header.type;
              bSuccess = false;
              break;

          }

          if (bSuccess == false)
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to deserialize packet: {0}", sError);
            if (Header.type.ToLower() != "messageack" && Header.type.ToLower() != "messagenotack")
            {
              SendPacketAck(false, Header.mId, sError);
            }
            return false;
          }

        }

        if (bInitialNegotiationIsFinished == false)
        {

          switch (Header.type.ToLower())
          {
            case "messageack":

              RSMP_Messages.MessageAck MessageAck = JSonSerializer.Deserialize<RSMP_Messages.MessageAck>(sJSon);

              if (bHaveGotVersionPacketAck == false && VersionPacket != null && MessageAck.oMId == VersionPacket.MessageId)
              {
                if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                {
                  RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Version packet was acked");
                }
                VersionPacket = null;
                bHaveGotVersionPacketAck = true;
                FindOutIfWeAreFinishedWithNegotiation();

                return true;
              }
              /*

              if (bHaveGotWatchdogPacketAck == false && WatchdogPacket != null && MessageAck.oMId == WatchdogPacket.MessageId)
              {
                if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                {
                  RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Initial watchdog packet was acked");
                  WatchdogPacket = null;
                  bHaveGotWatchdogPacketAck = true;
                  FindOutIfWeAreFinishedWithNegotiation();
                  return true;
                }
              }
              */

              break;

            case "messagenotack":

              RSMP_Messages.MessageNotAck MessageNotAck = JSonSerializer.Deserialize<RSMP_Messages.MessageNotAck>(sJSon);

              if (bHaveGotVersionPacketAck == false && VersionPacket != null && MessageNotAck.oMId == VersionPacket.MessageId)
              {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Version packet was rejected, reason: {0}", MessageNotAck.rea);
                VersionPacket = null;
                RSMPGS.RSMPConnection.Disconnect();
                return false;
              }
              /*
              if (bHaveGotWatchdogPacketAck == false && WatchdogPacket != null && MessageNotAck.oMId == WatchdogPacket.MessageId)
              {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Initial watchdog was rejected, reason: {0}", MessageNotAck.rea);
                WatchdogPacket = null;
                RSMPGS.RSMPConnection.Disconnect();
                bHaveGotWatchdogPacketAck = true;

                FindOutIfWeAreFinishedWithNegotiation();

                return true;
              }
              */

              break;

            case "version":

              if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
              {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got version packet, Type: {0}, mId: {1}", Header.type, Header.mId);
              }

              if (DecodeAndParseVersionMessage(sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref sError) == false)
              {
                SendPacketAck(false, Header.mId, "RSMP or SXL versions are incompatible");
                // Allow for some negotiation
                Thread.Sleep(100);
                RSMPGS.RSMPConnection.Disconnect();
                return false;
              }
              else
              {

                SendPacketAck(true, Header.mId, "");

                bHaveGotVersionPacket = true;

                if (RSMPGS.SimulatorType == RSMPGS.RSMPGSType.RSMPGS2)
                {
                  if (cHelper.IsSettingChecked("SendVersionInfoAtConnect"))
                  {
                    // We are supervision (SCADA), send version now when we got the roadside version
                    VersionPacket = RSMPGS.JSon.CreateAndSendVersionMessage();
                  }
                  else
                  {
                    // Don't expect any starting packets, just begin comms
                    NegotiatedRSMPVersion = FindOutHighestCheckedRSMPVersion();
                    bHaveGotVersionPacketAck = true;

                    if (cHelper.IsSettingChecked("SendWatchdogPacketAtStartup"))
                    {
                      //WatchdogPacket = RSMPGS.JSon.CreateAndSendWatchdogMessage(false);
                      RSMPGS.JSon.CreateAndSendWatchdogMessage(true);
                    }
                    else
                    {
                      //bHaveGotWatchdogPacketAck = true;
                      bHaveGotWatchdogPacket = true;
                    }

                  }
                }
                if (RSMPGS.SimulatorType == RSMPGS.RSMPGSType.RSMPGS1)
                {
                  if (cHelper.IsSettingChecked("SendWatchdogPacketAtStartup"))
                  {
                    //WatchdogPacket = RSMPGS.JSon.CreateAndSendWatchdogMessage(false);
                    RSMPGS.JSon.CreateAndSendWatchdogMessage(true);
                  }
                  else
                  {
                    //bHaveGotWatchdogPacketAck = true;
                    bHaveGotWatchdogPacket = true;
                  }
                }
              }
              FindOutIfWeAreFinishedWithNegotiation();
              return true;

            case "watchdog":

              if (bHaveGotWatchdogPacket == false)
              {

                if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                {
                  RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got initial Watchdog packet, Type: {0}, mId: {1}", Header.type, Header.mId);
                }

                SendPacketAck(true, Header.mId, "");

                bHaveGotWatchdogPacket = true;

                if (RSMPGS.SimulatorType == RSMPGS.RSMPGSType.RSMPGS2)
                {
                  if (cHelper.IsSettingChecked("SendWatchdogPacketAtStartup"))
                  {
                    //WatchdogPacket = RSMPGS.JSon.CreateAndSendWatchdogMessage(false);
                    RSMPGS.JSon.CreateAndSendWatchdogMessage(true);
                  }
                  else
                  {
                    //bHaveGotWatchdogPacketAck = true;
                    bHaveGotWatchdogPacket = true;
                  }
                }
                FindOutIfWeAreFinishedWithNegotiation();
                return true;
              }
              break;

          }

          // 2019-05-22/TR Don't care about these
          if (Header.type.ToLower() != "messageack" && Header.type.ToLower() != "messagenotack")
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Initial sequence is out of order, got a '{0}' packet?", Header.type);
            if (cHelper.IsSettingChecked("CloseConnectionIfNegotiationIsOutOfSequence"))
            {
              Thread.Sleep(100);
              RSMPGS.RSMPConnection.Disconnect();
              return false;
            }
          }
        }

        switch (Header.type.ToLower())
        {
          case "messageack":
            bSuccess = DecodeAndParseMessageAckMessage(sJSon, bUseStrictProtocolAnalysis, ref sError);
            break;

          case "messagenotack":
            bSuccess = DecodeAndParseMessageNotAckMessage(sJSon, bUseStrictProtocolAnalysis, ref sError);
            break;

          case "watchdog":
            bSuccess = DecodeAndParseWatchdogMessage(sJSon, bUseStrictProtocolAnalysis, ref sError);
            if (bSuccess)
            {
              if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
              {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got message, Type: {0}, mId: {1}", Header.type, Header.mId);
              }
              SendPacketAck(true, Header.mId, "");
            }
            break;

          default:
            if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
            {
              RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got message, Type: {0}, mId: {1}", Header.type, Header.mId);
            }
            bool bHasSentAckOrNack = false;
            bSuccess = DecodeAndParseGSSpecificJSonPacket(Header, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
            if (bHasSentAckOrNack == false)
            {
              SendPacketAck(bSuccess, Header.mId, sError);
            }

            break;
        }
      }
      catch (Exception e)
      {
        sError = "Failed to deserialize packet: " + e.Message;
        SendPacketAck(false, "00000000-0000-0000-0000-000000000000", sError);
        bSuccess = false;
      }

      if (bSuccess == false)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to parse packet: {0}", sError);
        return false;
      }
      else
      {
        return true;
      }

    }


    public virtual void SocketWasConnected()
    {

      LastSentWatchdogTimeStamp = DateTime.MinValue;

      //initSequence = InitSequence.None;

      //RSMPVersion HighestRSMPVersion = FindOutHighestCheckedRSMPVersion();

      NegotiatedRSMPVersion = RSMPVersion.NotSupported;

      VersionPacket = null;
      //WatchdogPacket = null;

      bHaveGotVersionPacket = false;
      bHaveGotVersionPacketAck = false;
      bHaveGotWatchdogPacket = false;
      //bHaveGotWatchdogPacketAck = false;

      bInitialNegotiationIsFinished = false;

      if (cHelper.IsSettingChecked("SendVersionInfoAtConnect"))
      {
        if (RSMPGS.SimulatorType == RSMPGS.RSMPGSType.RSMPGS1)
        {
          // We are roadside, send version first
          VersionPacket = RSMPGS.JSon.CreateAndSendVersionMessage();
        }
      }
      else
      {
        NegotiatedRSMPVersion = FindOutHighestCheckedRSMPVersion(); ;
        bHaveGotVersionPacket = true;
        bHaveGotVersionPacketAck = true;
        if (cHelper.IsSettingChecked("SendWatchdogPacketAtStartup"))
        {
          if (RSMPGS.SimulatorType == RSMPGS.RSMPGSType.RSMPGS1)
          {
            RSMPGS.JSon.CreateAndSendWatchdogMessage(true);
            //WatchdogPacket = RSMPGS.JSon.CreateAndSendWatchdogMessage(false);
          }
        }
      }

      // For both RSMP-simulators
      if (cHelper.IsSettingChecked("SendWatchdogPacketAtStartup") == false)
      {
        //bHaveGotWatchdogPacketAck = true;
        bHaveGotWatchdogPacket = true;
      }

      FindOutIfWeAreFinishedWithNegotiation();

    }

    private void FindOutIfWeAreFinishedWithNegotiation()
    {
      if (bInitialNegotiationIsFinished == false &&
        bHaveGotVersionPacket == true &&
        bHaveGotVersionPacketAck == true &&
        bHaveGotWatchdogPacket == true)
        //bHaveGotWatchdogPacketAck == true)
      {

        bInitialNegotiationIsFinished = true;
        SocketIsConnectedAndInitSequenceIsNegotiated();
      }
    }

    public virtual void SocketIsConnectedAndInitSequenceIsNegotiated()
    {

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Initial negotiation has finished, ready for communication");

      RSMPGS.JSon.LastReceivedWatchdogTimeStamp = DateTime.Now;
      LastSentWatchdogTimeStamp = DateTime.Now;

      bInitialNegotiationIsFinished = true;

    }

    public virtual void SocketWasClosed()
    {

      NegotiatedRSMPVersion = RSMPVersion.NotSupported;

      VersionPacket = null;
      //WatchdogPacket = null;

      bHaveGotVersionPacket = false;
      bHaveGotVersionPacketAck = false;
      bHaveGotWatchdogPacket = false;
      //bHaveGotWatchdogPacketAck = false;

      bInitialNegotiationIsFinished = false;

      JSonMessageIdAndTimeStamps.Clear();
    }

    public virtual bool DecodeAndParseGSSpecificJSonPacket(RSMP_Messages.Header Header, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
    {
      return false;
    }

    protected bool SendPacketAck(bool bSendPositiveAck, string oMId, string sReason)
    {

      bool bSuccess = false;

      if (cHelper.IsSettingChecked("DontAckPackets"))
      {
        return true;
      }

      if (bSendPositiveAck)
      {
        RSMP_Messages.MessageAck MsgAck = new RSMP_Messages.MessageAck();
        MsgAck.mType = "rSMsg";
        MsgAck.type = "MessageAck";
        MsgAck.oMId = oMId;
        string sSendBuffer = JSonSerializer.SerializeObject(MsgAck);
        bSuccess = RSMPGS.JSon.SendJSonPacket(MsgAck.type, null, sSendBuffer, false);
      }
      else
      {
        RSMP_Messages.MessageNotAck MsgNAck = new RSMP_Messages.MessageNotAck();
        MsgNAck.mType = "rSMsg";
        MsgNAck.type = "MessageNotAck";
        MsgNAck.rea = sReason;
        MsgNAck.oMId = oMId;
        string sSendBuffer = JSonSerializer.SerializeObject(MsgNAck);
        bSuccess = RSMPGS.JSon.SendJSonPacket(MsgNAck.type, null, sSendBuffer, false);
      }

      return bSuccess;

    }

    public cJSonMessageIdAndTimeStamp CreateAndSendVersionMessage()
    {

      RSMP_Messages.rsVersion rsVersion = new RSMP_Messages.rsVersion();

      int iIndex;

      rsVersion.mType = "rSMsg";
      rsVersion.type = "Version";
      rsVersion.mId = System.Guid.NewGuid().ToString();
      rsVersion.RSMP = new List<RSMP_Messages.Version_RSMP>();
      rsVersion.siteId = new List<RSMP_Messages.SiteId>();

      cSetting setting = RSMPGS.Settings["AllowUseRSMPVersion"];

      for (iIndex = 1; iIndex < sRSMPVersions.GetLength(0); iIndex++)
      {
        if (setting.GetActualValue((RSMPVersion)iIndex))
        {
          rsVersion.RSMP.Add(new RSMP_Messages.Version_RSMP(sRSMPVersions[iIndex]));
        }
      }
      
      rsVersion.SXL = RSMPGS.MainForm.textBox_SignalExchangeListVersion.Text;
      foreach (cSiteIdObject SiteIdObject in RSMPGS.ProcessImage.SiteIdObjects)
      {
        RSMP_Messages.SiteId sId = new RSMP_Messages.SiteId();
        sId.sId = SiteIdObject.sSiteId;
        rsVersion.siteId.Add(sId);
      }

      string sSendBuffer = JSonSerializer.SerializeObject(rsVersion);

      cJSonMessageIdAndTimeStamp JSonMessageIdAndTimeStamp = new cJSonMessageIdAndTimeStamp(rsVersion.type, rsVersion.mId, sSendBuffer, RSMPGS.RSMPConnection.PacketTimeout, false);

      // Don't pass through message queueing
      if (RSMPGS.RSMPConnection.SendJSonPacket(rsVersion.type, sSendBuffer))
      {
        if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent Version packet, MsgId: {0}", rsVersion.mId);
        }
        return JSonMessageIdAndTimeStamp;
      }
      else
      {
        return null;
      }

    }

    private bool DecodeAndParseVersionMessage(string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref string sError)
    {

      RSMPVersion HighestRSMPVersion = RSMPVersion.NotSupported;

      bool bSXLVersionIsOk = false;

      int iIndex;

      try
      {
        RSMP_Messages.rsVersion rsVersion = JSonSerializer.Deserialize<RSMP_Messages.rsVersion>(sJSon);

        cSetting setting = RSMPGS.Settings["AllowUseRSMPVersion"];

        foreach (RSMP_Messages.Version_RSMP Version_RSMP in rsVersion.RSMP)
        {
          // Validate this version is not unknown
          for (iIndex = 1; iIndex < sRSMPVersions.Count(); iIndex++)
          {
            if (Version_RSMP.vers.Trim() == sRSMPVersions[iIndex])
            {
              if (setting.GetActualValue((RSMPVersion)iIndex) == true && (int)HighestRSMPVersion < iIndex)
              {
                HighestRSMPVersion = (RSMPVersion)iIndex;
              }
              break;
            }
          }
          if (iIndex == sRSMPVersions.Count())
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Client RSMP version '{0}' is unknown", Version_RSMP.vers.Trim());
          }
        }
        bSXLVersionIsOk = (rsVersion.SXL.Trim() == RSMPGS.MainForm.textBox_SignalExchangeListVersion.Text.Trim());
      }
      catch (Exception e)
      {
        sError = "Failed to deserialize version packet: " + e.Message;
        return false;
      }

      if (HighestRSMPVersion == RSMPVersion.NotSupported)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Client RSMP version is not compatible");
      }
      else
      {

        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "We will use RSMP protocol version {0}", sRSMPVersions[(int)HighestRSMPVersion]);
      }

      if (bSXLVersionIsOk == false)
      {
        RSMPGS.SysLog.SysLog(cHelper.IsSettingChecked("SXL_VersionIgnore") ? cSysLogAndDebug.Severity.Warning : cSysLogAndDebug.Severity.Error,
          "Client SXL version is not compatible");
      }

      if (HighestRSMPVersion == RSMPVersion.NotSupported || bSXLVersionIsOk == false)
      {
        if (cHelper.IsSettingChecked("SXL_VersionIgnore") == true)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Ignoring client version incompability");
        }
        else
        {
          sError = "Client version(s) are incompatible, closing connection";
          return false;
        }
      }

      NegotiatedRSMPVersion = HighestRSMPVersion;

      return true;

    }

    private bool DecodeAndParseMessageAckMessage(string sJSon, bool bUseStrictProtocolAnalysis, ref string sError)
    {

      RSMP_Messages.MessageAck MessageAck = JSonSerializer.Deserialize<RSMP_Messages.MessageAck>(sJSon);

      if (MessageAck.oMId == null)
      {
        sError = "MessageAck.oMId is null";
        return bUseStrictProtocolAnalysis ? false : true;
      }

      if (MessageAck.oMId == "")
      {
        sError = "MessageAck.oMId does not contain any GUID";
        return bUseStrictProtocolAnalysis ? false : true;
      }

      RemovePacketFromJSonMessageIdAndTimeStamps(MessageAck.oMId);

      return true;

    }

    private bool DecodeAndParseMessageNotAckMessage(string sJSon, bool bUseStrictProtocolAnalysis, ref string sError)
    {

      RSMP_Messages.MessageNotAck MessageNotAck = JSonSerializer.Deserialize<RSMP_Messages.MessageNotAck>(sJSon);

      if (MessageNotAck.oMId == null)
      {
        sError = "MessageNotAck.oMId is null";
        return bUseStrictProtocolAnalysis ? false : true;
      }

      if (MessageNotAck.oMId == "")
      {
        sError = "MessageNotAck.oMId is null";
        return bUseStrictProtocolAnalysis ? false : true;
      }

      RemovePacketFromJSonMessageIdAndTimeStamps(MessageNotAck.oMId);

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Message mId: {0} was refused, reason: {1}", MessageNotAck.oMId, MessageNotAck.rea);

      return true;

    }

    public bool DecodeAndParseWatchdogMessage(string sJSon, bool bUseStrictProtocolAnalysis, ref string sError)
    {

      bool bSuccess = false;

      try
      {

        RSMP_Messages.Watchdog Watchdog = JSonSerializer.Deserialize<RSMP_Messages.Watchdog>(sJSon);

        LastReceivedWatchdogTimeStamp = DateTime.Now;

        bSuccess = true;

      }
      catch (Exception e)
      {
        sError = "Failed to deserialize packet: " + e.Message;
      }
      return bSuccess;

    }

    public cJSonMessageIdAndTimeStamp CreateAndSendWatchdogMessage(bool bExpectAckOrNack)
    {

      RSMP_Messages.Watchdog Watchdog = new RSMP_Messages.Watchdog();

      Watchdog.mType = "rSMsg";
      Watchdog.type = "Watchdog";
      Watchdog.mId = System.Guid.NewGuid().ToString();
      //Watchdog.wTs = DateTime.UtcNow;
      // 2012-10-25/TR CreateLocalTimeStamp() changed to CreateISO8601UTCTimeStamp()
      Watchdog.wTs = CreateISO8601UTCTimeStamp();

      cJSonMessageIdAndTimeStamp JSonMessageIdAndTimeStamp = null;

      LastSentWatchdogTimeStamp = DateTime.Now;

      string sSendBuffer = JSonSerializer.SerializeObject(Watchdog);

      if (bExpectAckOrNack == false)
      {
        if (RSMPGS.RSMPConnection.SendJSonPacket(Watchdog.type, sSendBuffer))
        {
          JSonMessageIdAndTimeStamp = new cJSonMessageIdAndTimeStamp(Watchdog.type, Watchdog.mId, sSendBuffer, RSMPGS.RSMPConnection.PacketTimeout, false);
          if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent initial Watchdog message, MsgId: {0}", Watchdog.mId);
          }
        }
        else
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to send initial Watchdog message, MsgId: {0}", Watchdog.mId);
        }
      }
      else
      {
        if (RSMPGS.JSon.SendJSonPacket(Watchdog.type, Watchdog.mId, sSendBuffer, false))
        {
          if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent Watchdog message, MsgId: {0}", Watchdog.mId);
          }
        }
        else
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to send Watchdog message, MsgId: {0}", Watchdog.mId);
        }
      }

      return JSonMessageIdAndTimeStamp;

    }

    public string CreateISO8601UTCTimeStamp()
    {
      return CreateISO8601UTCTimeStamp(DateTime.Now);
    }

    public string CreateISO8601UTCTimeStamp(DateTime dtLocalTimeStamp)
    {
      string sTimeStamp = String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}T{0:HH:mm:ss.fff}Z", dtLocalTimeStamp.ToUniversalTime());
      return sTimeStamp;
    }

    public string UnpackISO8601UTCTimeStamp(string sDateTime)
    {
      string dtReturnDateTime;
      try
      {
        dtReturnDateTime = DateTime.ParseExact(sDateTime.ToUpper(), @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not deserialize timestamp '{0}', error: {1}", sDateTime, e.Message);
        dtReturnDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
      }

      return dtReturnDateTime;
    }

    protected bool ValidateJSONProperties(Type T, string sJSon, ref string sError)
    {

      FieldInfo[] fields = T.GetFields();

      // Pack to ensure there is no space between "Tag" and ':'
      sJSon = sJSon.Replace(" ", "");


      // When this is used to test return values, only the first occurence is tested
      foreach (FieldInfo field in fields)
      {
        // Ugly stuff to find out if the field exists and has bad casing
        if (sJSon.IndexOf("\"" + field.Name + "\":", StringComparison.OrdinalIgnoreCase) >= 0)
        {
          if (sJSon.IndexOf("\"" + field.Name + "\":", StringComparison.Ordinal) == -1)
          {
            sError = "Upper/lower case error when deserializing object '" + T.Name + "', property/field '" + field.Name + "'";
            return false;
          }
        }
        else
        {
          sError = "Missing property field '" + field.Name + "' when deserializing object '" + T.Name + "'";
          return false;
        }
      }

      return true;

    }

    protected bool ValidateJSONProperties(object obj, string sJSon, ref string sError)
    {

      Type T = obj.GetType();

      return ValidateJSONProperties(T, sJSon, ref sError);

    }

    protected bool ValidatePropertiesString(string sFirstString, string sSecondString, ref string sError)
    {

      if (sFirstString != sSecondString)
      {
        sError = "Invalid property, '" + sFirstString + "' does not match '" + sSecondString + "'";
        return false;
      }
      else
      {
        return true;
      }
    }

    protected bool ValidateGUID(string sGUID, ref string sError)
    {
      if (cHelper.IsGuid(sGUID) == false)
      {
        sError = "Invalid message id (is not a valid GUID): '" + sGUID + "'";
        return false;
      }
      else
      {
        return true;
      }

    }

    public bool SendJSonPacket(string PacketType, string MessageId, string SendString, bool ResendPacketIfWeGetNoAck)
    {

      if (RSMPGS.RSMPConnection.SendJSonPacket(PacketType, SendString))
      {
        // Crap packets and Ack/NAck does not have any mId
        if (MessageId != null)
        {
          // Do some noncase compare if we are sending case faulty packets for protocol testing
          if (PacketType.Equals("Version", StringComparison.OrdinalIgnoreCase) == false)
          {
            // Store message id for ack determination
            JSonMessageIdAndTimeStamps.Add(new cJSonMessageIdAndTimeStamp(PacketType, MessageId, SendString, RSMPGS.RSMPConnection.PacketTimeout, ResendPacketIfWeGetNoAck));
          }
        }
        return true;
      }
      else
      {
        return false;
      }

    }

    public void CyclicCleanup(int iElapsedMillisecs, int iWatchdogInterval, int iWatchdogTimeout)
    {

      if (RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected)
      {
        if (bInitialNegotiationIsFinished)
        {
          bool bUseStrictProtocolAnalysis = cHelper.IsSettingChecked("UseStrictProtocolAnalysis");
          bool bResendUnackedPackets = cHelper.IsSettingChecked("ResendUnackedPackets");

          // Find out if we have received som msgack's
          for (int iIndex = 0; iIndex < JSonMessageIdAndTimeStamps.Count; )
          {
            cJSonMessageIdAndTimeStamp JSonMessageIdAndTimeStamp = JSonMessageIdAndTimeStamps[iIndex];

            if (JSonMessageIdAndTimeStamp.IsPacketToOld())
            {
              if (JSonMessageIdAndTimeStamp.ResendPacketIfWeGetNoAck && bResendUnackedPackets)
              {
                // Set new GUID
                JSonMessageIdAndTimeStamp.MessageId = System.Guid.NewGuid().ToString();
                string pattern = @"""mId"":""[a-z0-9-]*""";
                string replace = @"""mId"":""" + JSonMessageIdAndTimeStamp.MessageId + @"""";
                JSonMessageIdAndTimeStamp.SendString = Regex.Replace(JSonMessageIdAndTimeStamp.SendString, pattern, replace);
                
                // Don't pass queuing algorithm
                RSMPGS.RSMPConnection.SendJSonPacket(JSonMessageIdAndTimeStamp.PacketType, JSonMessageIdAndTimeStamp.SendString);
                JSonMessageIdAndTimeStamp.TimeStamp = DateTime.Now;
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "No message ack received for mId: {0} within {1} msecs, it has been retransmitted", JSonMessageIdAndTimeStamp.MessageId, JSonMessageIdAndTimeStamp.TimeToWaitForAck);
                iIndex++;
              }
              else
              {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "No message ack received for mId: {0} within {1} msecs, it will not be retransmitted", JSonMessageIdAndTimeStamp.MessageId, JSonMessageIdAndTimeStamp.TimeToWaitForAck);
                JSonMessageIdAndTimeStamps.RemoveAt(iIndex);
              }
            }
            else
            {
              iIndex++;
            }
          }
          if (iWatchdogInterval > 0 && cHelper.IsSettingChecked("SendWatchdogPacketCyclically") == true)
          {
            if (LastSentWatchdogTimeStamp.AddMilliseconds(iWatchdogInterval) <= DateTime.Now)
            {
              CreateAndSendWatchdogMessage(true);
            }
          }
          else
          {
            LastSentWatchdogTimeStamp = DateTime.Now;
          }
          if (cHelper.IsSettingChecked("ExpectWatchdogPackets"))
          {
            if (iWatchdogTimeout > 0)
            {
              if (LastReceivedWatchdogTimeStamp.AddMilliseconds(iWatchdogTimeout) <= DateTime.Now)
              {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "No Watchdog message received within {0} msecs", iWatchdogTimeout);
                if (bUseStrictProtocolAnalysis)
                {
                  RSMPGS.RSMPConnection.Disconnect();
                }
                LastReceivedWatchdogTimeStamp = DateTime.Now;
              }
            }
          }
        }
      }
    }

    public void RemovePacketFromJSonMessageIdAndTimeStamps(string sMsgId)
    {

      int iFoundPackets = 0;

      if (sMsgId == "")
      {
        return;
      }

      // Find out if we have received some msgack's
      for (int iIndex = 0; iIndex < JSonMessageIdAndTimeStamps.Count; )
      {
        cJSonMessageIdAndTimeStamp JSonMessageIdAndTimeStamp = JSonMessageIdAndTimeStamps[iIndex];
        if (JSonMessageIdAndTimeStamp.MessageId.Equals(sMsgId))
        {

          iFoundPackets++;

          JSonMessageIdAndTimeStamps.RemoveAt(iIndex);

          RSMPGS.Statistics["TxRTTimeInMsec"] = ((TimeSpan)(DateTime.Now - JSonMessageIdAndTimeStamp.TimeStamp)).TotalMilliseconds;
          RSMPGS.Statistics["TxRTTimeTotalTimeInMsec"] += RSMPGS.Statistics["TxRTTimeInMsec"];
          RSMPGS.Statistics["TxRTTimeNoOfPackets"]++;

          break;
        }
        else
        {
          iIndex++;
        }
      }
      if (iFoundPackets == 0)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Got ack for unknown packet, mId: {0}", sMsgId);
      }
      else
      {
        if (iFoundPackets > 1)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Found more than one packet to ack, mId: {0}", sMsgId);
        }
      }
    }

    public string ValidateArrayObject(Dictionary<string, cYAMLMapping> items, object status)
    {
      object[] statusObjects = (object[])status;

      // YAMLMapping
      KeyValuePair<string, cYAMLMapping> item;
      string schemaKey;
      cYAMLMapping schemaValue;

      // YAMLScalar
      Dictionary<string, string> schemaScalars;
      KeyValuePair<string, string> schemaScalar;
      string schemaScalarType;
      Boolean schemaScalarOptional;
      string schemaScalarMin;
      string schemaScalarMax;
      string cellName;

      Dictionary<string, cYAMLMapping> schemaMappings;
      KeyValuePair<string, cYAMLMapping> schemaMapping;
      List<string> keys;

      // incoming status
      //string[] fieldStrings;
      string statusKey;
      object statusValue;
      Boolean hit;

      // loop alla YAMLMappings
      for (int i = 0; i < items.Count; i++)
      {

        // get YAMLMapping
        item = items.ElementAt(i);
        schemaKey = item.Key;
        schemaValue = item.Value;
        schemaScalars = schemaValue.YAMLScalars;
        schemaScalarOptional = false;
        schemaScalarType = "";
        schemaScalarMin = "";
        schemaScalarMax = "";
        cellName = "";

        // loop YAMLScalars
        for (int j = 0; j < schemaScalars.Count; j++)
        {
          schemaScalar = schemaScalars.ElementAt(j);
          if (schemaScalar.Key == "type")
          {
            schemaScalarType = schemaScalar.Value;
          }
          if (schemaScalar.Key == "optional")
          {
            if (schemaScalar.Value == "true")
            {
              schemaScalarOptional = true;
            }
            else
            {
              schemaScalarOptional = false;
            }
          }
          if (schemaScalar.Key == "min")
          {
            schemaScalarMin = schemaScalar.Value;
          }
          if (schemaScalar.Key == "max")
          {
            schemaScalarMax = schemaScalar.Value;
          }
        }

        int rowIndex = 0;

        // validate incoming values to verify YAMLMapping
        foreach (object statusObject in statusObjects)
        {
          Dictionary<string, object> fields = (Dictionary<string, object>)statusObject;

          hit = false;
          foreach (object field in fields)
          {
            KeyValuePair<string, object> f = (KeyValuePair<string, object>)field;

            statusKey = f.Key;
            statusValue = f.Value;

            cellName = "row:" + (rowIndex + 1).ToString() + " col:" + schemaKey + " ";

            if (schemaKey == statusKey)
            {
              hit = true;
              switch (schemaScalarType.ToLower())
              {
                case "integer":
                  try
                  {
                    Int32 iValue = Int32.Parse(statusValue.ToString());
                    Int32 iMin = Int32.Parse(schemaScalarMin);
                    Int32 iMax = Int32.Parse(schemaScalarMax);
                    if (iValue < iMin) { return cellName + " to small"; }
                    if (iValue > iMax) { return cellName + " to big"; }
                  }
                  catch
		              {
                    return cellName + " wrong type";
                  }
                  break;
                case "long":
                  try
                  {
                    Int32 iValue = Int32.Parse(statusValue.ToString());
                    Int32 iMin = Int32.Parse(schemaScalarMin);
                    Int32 iMax = Int32.Parse(schemaScalarMax);
                    if (iValue < iMin) { return cellName + " to small"; }
                    if (iValue > iMax) { return cellName + " to big"; }
                  }
                  catch
                  {
                    return cellName + " wrong type";
                  }
                  break;
                case "number":
                case "real":
                  try
                  {
                    Double dValue = Double.Parse(statusValue.ToString());
                    Double dMin = Double.Parse(schemaScalarMin);
                    Double dMax = Double.Parse(schemaScalarMax);
                    if (dValue < dMin) { return cellName + " to small"; }
                    if (dValue > dMax) { return cellName + " to big"; }
                  }
                  catch
                  {
                    return cellName + " wrong type";
                  }
                  break;
                case "boolean":
                  // Boolean is treated as an enum in Excel/CSV, but not in YAML. To
                  // preserve backwards compability we need to treat this as case
                  // insensitive for now
                  if(!(statusValue.ToString().Equals("true", StringComparison.OrdinalIgnoreCase) ||
                    statusValue.ToString().Equals("false", StringComparison.OrdinalIgnoreCase) ||
                    statusValue.ToString().Equals("0", StringComparison.OrdinalIgnoreCase) ||
                    statusValue.ToString().Equals("1", StringComparison.OrdinalIgnoreCase)))
                  {
                    return cellName + "boolean can't be parsed";
                  }
                  break;
                case "timestamp":
                case "string":
                    break;
                default:
                  return cellName + "type:" + schemaScalarType + " not supported";
              }
            }
          }

          // error if key not found and not optional
          if (!hit && !schemaScalarOptional)
          {
            return cellName + " required";
          }

          rowIndex++;
        }
      }

      return "success";
    }
    
    public string stringifyObject(object status)
    {
      object[] statusObjects = (object[])status;
      string objectsString = "";
      string objectString;
      string fieldString;

      foreach (object statusObject in statusObjects)
      {
        Dictionary<string, object> fields = (Dictionary<string, object>)statusObject;
        objectString = "";
        foreach (object field in fields)
        {
          KeyValuePair<string, object> f = (KeyValuePair<string, object>)field;
          fieldString = "\"" + f.Key + "\":\"" + f.Value + "\"";
          if (objectString != "") { objectString = objectString + ","; }
          objectString = objectString + fieldString;
        }
        if (objectsString != "") { objectsString = objectsString + ","; }
        objectsString = objectsString + "{" + objectString + "}";
      }
      return "[" + objectsString + "]";
    }

    public bool ValidateTypeAndRange(string sType, object oValue, Dictionary<string, string> sEnums)
    {
      return ValidateTypeAndRange(sType, oValue, sEnums, 0, 0);
    }

    public bool ValidateTypeAndRange(string sType, object oValue, Dictionary<string, string> sEnums, double dMin, double dMax)
    {
      bool bUseCaseSensitiveValue = cHelper.IsSettingChecked("UseCaseSensitiveValue");
      var comparisonType = StringComparison.Ordinal;
      if (!bUseCaseSensitiveValue) { comparisonType = StringComparison.OrdinalIgnoreCase; }

      if (oValue == null)
      {
        return false;
      }

      bool bValueIsValid = false;

      switch (sType.ToLower())
      {
        case "timestamp":
        case "string":
        case "array":
          bValueIsValid = true;
          break;

        case "integer":
          try
          {
            Int32 iValue = Int32.Parse(oValue.ToString());
            bValueIsValid = true;
          }
          catch { }
          break;

        case "long":
          try
          {
            Int32 iValue = Int32.Parse(oValue.ToString());
            bValueIsValid = true;
          }
          catch { }
          break;

        case "number":
        case "real":
          try
          {
            Double dValue = Double.Parse(oValue.ToString());
            bValueIsValid = true;
          }
          catch { }
          break;

        case "boolean":
          // Boolean is treated as an enum in Excel/CSV, but not in YAML. To
          // preserve backwards compability we need to treat this as case
          // insensitive for now
          
          bValueIsValid = oValue.ToString().Equals("true", StringComparison.OrdinalIgnoreCase) ||
            oValue.ToString().Equals("false", StringComparison.OrdinalIgnoreCase) ||
            oValue.ToString().Equals("0", StringComparison.OrdinalIgnoreCase) ||
            oValue.ToString().Equals("1", StringComparison.OrdinalIgnoreCase);
          break;

        case "base64":
          try
          {
            Encoding encoding;
            encoding = Encoding.GetEncoding("IBM437");
            byte[] Base64Bytes = encoding.GetBytes(oValue.ToString());
            char[] Base64Chars = encoding.GetChars(Base64Bytes);
            byte[] Base8Bytes = System.Convert.FromBase64CharArray(Base64Chars, 0, Base64Chars.GetLength(0));
            bValueIsValid = true;
          }
          catch { }
          break;

        case "ordinal":

          if (NegotiatedRSMPVersion == RSMPVersion.RSMP_3_1_1 || NegotiatedRSMPVersion == RSMPVersion.RSMP_3_1_2)
          {
            try
            {
              UInt32 iValue = UInt32.Parse(oValue.ToString());
              bValueIsValid = true;
            }
            catch { }
          }
          break;

        // These are all valid
        case "raw":
        case "scale":
        case "unit":
          if (NegotiatedRSMPVersion == RSMPVersion.RSMP_3_1_1 || NegotiatedRSMPVersion == RSMPVersion.RSMP_3_1_2)
          {
            bValueIsValid = true;
          }
          break;
      }

      // Validate range
      if (bValueIsValid == true)
      {
        switch (sType.ToLower())
        {
          case "integer":
          case "long":
          case "number":
          case "real":

            try
            {
              Double dValue = Double.Parse(oValue.ToString());
              bValueIsValid = dValue <= dMax && dValue >= dMin;

              if (sEnums != null && sEnums.Count > 0)
              {
                bValueIsValid = false;
                foreach (string sEnum in sEnums.Keys)
                {
                  if (oValue.ToString().Equals(sEnum, comparisonType))
                  {
                    bValueIsValid = true;
                  }
                }
              }
            }
            catch { }
            break;
          case "timestamp":
          case "string":

            try
            {
              if (sEnums != null && sEnums.Count > 0)
              {
                bValueIsValid = false;
                foreach (string sEnum in sEnums.Keys)
                {
                  if (oValue.ToString().Equals(sEnum, comparisonType))
                  {
                    bValueIsValid = true;
                  }
                }
              }
            }
            catch { }
            break;
        }
      }
      
      return bValueIsValid;

    }

    public RSMPVersion FindOutHighestCheckedRSMPVersion()
    {

      RSMPVersion HighestRSMPVersion = RSMPVersion.NotSupported;

      cSetting setting = RSMPGS.Settings["AllowUseRSMPVersion"];

      if (setting.GetActualValue(RSMPVersion.RSMP_3_1_1))
      {
        HighestRSMPVersion = RSMPVersion.RSMP_3_1_1;
      }

      if (setting.GetActualValue(RSMPVersion.RSMP_3_1_2))
      {
        HighestRSMPVersion = RSMPVersion.RSMP_3_1_2;
      }

      if (setting.GetActualValue(RSMPVersion.RSMP_3_1_3))
      {
        HighestRSMPVersion = RSMPVersion.RSMP_3_1_3;
      }

      if (setting.GetActualValue(RSMPVersion.RSMP_3_1_4))
      {
        HighestRSMPVersion = RSMPVersion.RSMP_3_1_4;
      }

      if (setting.GetActualValue(RSMPVersion.RSMP_3_1_5))
      {
        HighestRSMPVersion = RSMPVersion.RSMP_3_1_5;
      }

      if (setting.GetActualValue(RSMPVersion.RSMP_3_2_0))
      {
        HighestRSMPVersion = RSMPVersion.RSMP_3_2_0;
      }

      if (setting.GetActualValue(RSMPVersion.RSMP_3_2_1))
      {
        HighestRSMPVersion = RSMPVersion.RSMP_3_2_1;
      }

      if (setting.GetActualValue(RSMPVersion.RSMP_3_2_2))
      {
        HighestRSMPVersion = RSMPVersion.RSMP_3_2_2;
      }

      return HighestRSMPVersion;

    }

  }

}
