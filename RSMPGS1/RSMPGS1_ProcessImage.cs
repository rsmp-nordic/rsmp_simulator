using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.Diagnostics.Eventing.Reader;

namespace nsRSMPGS
{
  
  public class cSubscription
  {

    public cStatusObject StatusObject;
    public cStatusReturnValue StatusReturnValue;
    
    public enum SubscribeMethod
    {
      Nothing,
      Interval,
      OnChange,
      IntervalAndOnChange
    }

    public SubscribeMethod SubscribeStatus;
    public int UpdateRate;
    public DateTime LastUpdate;

    public cSubscription(cStatusObject sO, cStatusReturnValue sS, float fUpdateRate, bool bAlwaysSendOnChange)
    {
      if (fUpdateRate <= 0)
      {
        SubscribeStatus = SubscribeMethod.OnChange;
        UpdateRate = 0;
      }
      else
      {
        try
        {
          UpdateRate = (int)(fUpdateRate * 1000);
        }
        catch
        {
          UpdateRate = 0;
        }

        // Interval 0 -> Always on change
        // > 0 could be both interval and on change
        if (UpdateRate == 0)
        {
          SubscribeStatus = SubscribeMethod.OnChange;
        }
        else
        {
          if (bAlwaysSendOnChange)
          {
            SubscribeStatus = SubscribeMethod.IntervalAndOnChange;
          }
          else
          {
            SubscribeStatus = SubscribeMethod.Interval;
          }
        }
      }
      LastUpdate = DateTime.Now;
      StatusObject = sO;
      StatusReturnValue = sS;
    }

  }

  public class cBufferedMessage
  {

    public enum eMessageType
    {
      Alarm,
      Status,
      AggregatedStatus
    }

    public ListViewItem lvItem;

    public eMessageType MessageType;

    public string sPacketType;
    public string sMessageId;
    public string sSendString;

    public cBufferedMessage(eMessageType MessageType, string PacketType, string MessageId, string SendString)
    {
      this.MessageType = MessageType;
      sPacketType = PacketType;
      sMessageId = MessageId;
      sSendString = SendString;
    }
  }

  public partial class cProcessImage
  {

    public void LoadProcessImageValues(RSMPGS_Main MainForm, string FileName)
    {

      bool bWeAreConnected = false;

      try
      {

        if (RSMPGS.RSMPConnection != null)
        {
          bWeAreConnected = RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected;
        }

        List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();

        //StreamReader swReferenceFile = new StreamReader((System.IO.Stream)File.OpenRead(FileName), encoding);
        //swReferenceFile.Close();

        /*
        [26108.AB+26108=881CG001.Passagedetektor.Passagedetektor DP1.AB+26108=881DP001.Alarms]
        A904.Active=True

        [26108.AB+26108=881CG001.Passagedetektor.Passagedetektor DP1.AB+26108=881DP001.AlarmEvents]
        A904.AlarmEvent_0.MessageId=f56ad0f6-7c4c-4b51-a602-8ba9fadf6ea8
        A904.AlarmEvent_0.TimeStamp=2011-11-16T12:52:39.437
        [26108.AB+26108=881CG001.Passagedetektor.Passagedetektor DP1.AB+26108=881DP001.Status]
        S0001.status 1.Status=10
        */

        foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
        {

          bool bSomeThingWasChangedInAggregatedStatus = false;

          if (MainForm.checkBox_ProcessImageLoad_AlarmStatus.Checked)
          {
            foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
            {

              string sSection = RoadSideObject.UniqueId() + ".Alarms";
              string sParameter = AlarmObject.sAlarmCodeId + ".Active";

              cAlarmEvent AlarmEvent = null;
              cAlarmObject NewAlarmObject = new cAlarmObject();

              NewAlarmObject.bActive = cPrivateProfile.GetIniFileInt(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".Active", 0) != 0;
              NewAlarmObject.bSuspended = cPrivateProfile.GetIniFileInt(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".Suspended", 0) != 0;
              NewAlarmObject.bAcknowledged = cPrivateProfile.GetIniFileInt(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".Acknowledged", 0) != 0;

              AlarmObject.AlarmEvents.Clear();

              foreach (cAlarmReturnValue AlarmReturnValue in AlarmObject.AlarmReturnValues)
              {
                string sValue = cPrivateProfile.GetIniFileString(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + "." + AlarmReturnValue.sName + ".Value", "?");

                if (sValue != "?")
                {
                  AlarmReturnValue.Value.SetValue(sValue);
                }
              }

              if (AlarmObject.bSuspended != NewAlarmObject.bSuspended)
              {
                AlarmObject.bSuspended = NewAlarmObject.bSuspended;
                if (bWeAreConnected)
                {
                  RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Suspend);
                }
              }

              if (AlarmObject.bActive != NewAlarmObject.bActive)
              {
                AlarmObject.bActive = NewAlarmObject.bActive;
                if (bWeAreConnected)
                {
                  if (NewAlarmObject.bActive)
                  {
                    RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Issue, out AlarmEvent);
                    if (AlarmEvent != null)
                    {
                      AlarmObject.AlarmEvents.Add(AlarmEvent);
                    }
                  }
                  else
                  {
                    RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Issue);
                    AlarmObject.AlarmEvents.Clear();
                  }
                }
              }

              if (AlarmObject.bAcknowledged != NewAlarmObject.bAcknowledged)
              {
                AlarmObject.bAcknowledged = NewAlarmObject.bAcknowledged;
                if (bWeAreConnected)
                {
                  RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Acknowledge);
                }
                AlarmObject.AlarmEvents.Clear();
              }

              AlarmObject.AlarmCount = 0;


              /*
                        sSection = sObjectUniqueId + ".AlarmEvents";

                        foreach (cAlarmReturnValue AlarmReturnValue in AlarmObject.AlarmReturnValues)
                        {
                          AlarmReturnValue.sValue = cPrivateProfile.GetIniFileString(FileName, sSection, AlarmObject.sAlarmCodeId + ".ReturnValue_" + AlarmReturnValue.sName + ".Value", "");
                        }
                        for (int iIndex = 0; ; iIndex++)
                        {
                          string sMsgId = cPrivateProfile.GetIniFileString(FileName, sSection, AlarmObject.sAlarmCodeId + ".AlarmEvent_" + iIndex.ToString() + ".MessageId", "");
                          string sTS = cPrivateProfile.GetIniFileString(FileName, sSection, AlarmObject.sAlarmCodeId + ".AlarmEvent_" + iIndex.ToString() + ".TimeStamp", "");
                          if (sMsgId.Length > 0)
                          {
                            cAlarmEvent AlarmEvent = new cAlarmEvent();
                            AlarmEvent.sMessageId = sMsgId;
                            AlarmEvent.sTimeStamp = sTS;
                            AlarmObject.AlarmEvents.Add(AlarmEvent);
                          }
                          else
                          {
                            break;
                          }

                        }
               */
            }
          }

          if (MainForm.checkBox_ProcessImageLoad_Status.Checked)
          {
            // Stored status
            foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
            {
              foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
              {
                string sStatus = cPrivateProfile.GetIniFileString(FileName, RoadSideObject.UniqueId() + ".Status", StatusObject.sStatusCodeId + "." + StatusReturnValue.sName + ".Status", "?");
                StatusReturnValue.bRecentlyChanged = false;
                if (sStatus != "?")
                {
                  StatusReturnValue.bRecentlyChanged = StatusReturnValue.Value.GetValue().Equals(sStatus) ? false : true;
                  StatusReturnValue.Value.SetValue(sStatus);
                }
              }
            }
            sS.Clear();
            foreach (cSubscription Subscription in RoadSideObject.Subscriptions)
            {
              if (Subscription.SubscribeStatus == cSubscription.SubscribeMethod.OnChange || Subscription.SubscribeStatus == cSubscription.SubscribeMethod.IntervalAndOnChange)
              {
                if (Subscription.StatusReturnValue.bRecentlyChanged)
                {
                  RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                  s.sCI = Subscription.StatusObject.sStatusCodeId;
                  s.n = Subscription.StatusReturnValue.sName; // Subscription.StatusObject.StatusReturnValues .StatusReturnValues[iIndex].sName;
                  UpdateStatusValue(ref s, Subscription.StatusReturnValue.Value.GetValueType(), Subscription.StatusReturnValue.Value.GetValue(), Subscription.StatusReturnValue.Value.GetArray());
                  sS.Add(s);
                  Subscription.LastUpdate = DateTime.Now;
                  Subscription.StatusReturnValue.bRecentlyChanged = false;
                }
              }
            }

            if (sS.Count > 0)
            {
              if (bWeAreConnected)
              {
                RSMPGS.JSon.CreateAndSendStatusUpdateMessage(RoadSideObject, sS);
              }
            }
          }
          if (MainForm.checkBox_ProcessImageLoad_AggregatedStatus.Checked)
          {
            if (RoadSideObject.bIsComponentGroup)
            {

              if (RoadSideObject.sFunctionalPosition.Equals(cPrivateProfile.GetIniFileString(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "FunctionalPosition", "")) == false)
              {
                RoadSideObject.sFunctionalPosition = cPrivateProfile.GetIniFileString(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "FunctionalPosition", "");
                bSomeThingWasChangedInAggregatedStatus = true;
              }
              if (RoadSideObject.sFunctionalState.Equals(cPrivateProfile.GetIniFileString(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "FunctionalState", "")) == false)
              {
                RoadSideObject.sFunctionalState = cPrivateProfile.GetIniFileString(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "FunctionalState", "");
                bSomeThingWasChangedInAggregatedStatus = true;
              }
              for (int iIndex = 0; iIndex < RoadSideObject.bBitStatus.GetLength(0); iIndex++)
              {
                if (RoadSideObject.bBitStatus[iIndex] != (cPrivateProfile.GetIniFileInt(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "BitStatus_" + iIndex.ToString(), 0) != 0))
                {
                  RoadSideObject.bBitStatus[iIndex] = cPrivateProfile.GetIniFileInt(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "BitStatus_" + iIndex.ToString(), 0) != 0;
                  bSomeThingWasChangedInAggregatedStatus = true;
                }
              }
              if (bSomeThingWasChangedInAggregatedStatus)
              {
                if (MainForm.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Checked)
                {
                  if (bWeAreConnected)
                  {
                    RSMPGS.JSon.CreateAndSendAggregatedStatusMessage(RoadSideObject);
                  }
                }
              }
            }
          }

          if (MainForm.treeView_SitesAndObjects.SelectedNode != null)
          {
            if (MainForm.treeView_SitesAndObjects.SelectedNode.Tag != null && MainForm.treeView_SitesAndObjects.SelectedNode.Parent != null)
            {
              if (RoadSideObject == (cRoadSideObject)MainForm.treeView_SitesAndObjects.SelectedNode.Tag)
              {
                MainForm.UpdateStatusListView(null, RoadSideObject);
                MainForm.UpdateAlarmListView(null, RoadSideObject);
                if (bSomeThingWasChangedInAggregatedStatus)
                {
                  for (int iIndex = 0; iIndex < RoadSideObject.bBitStatus.GetLength(0); iIndex++)
                  {
                    ListViewItem lvItem = MainForm.listView_AggregatedStatus_StatusBits.Items[iIndex];
                    MainForm.SetStatusBitColor(lvItem, RoadSideObject.bBitStatus[iIndex]);
                  }
                  RSMPGS_Main.bIsCurrentlyChangingSelection = true;
                  MainForm.listBox_AggregatedStatus_FunctionalPosition.ClearSelected();
                  for (int iIndex = 0; iIndex < MainForm.listBox_AggregatedStatus_FunctionalPosition.Items.Count; iIndex++)
                  {
                    if (MainForm.listBox_AggregatedStatus_FunctionalPosition.Items[iIndex].Equals(RoadSideObject.sFunctionalPosition))
                    {
                      MainForm.listBox_AggregatedStatus_FunctionalPosition.SelectedIndex = iIndex;
                    }
                  }
                  MainForm.listBox_AggregatedStatus_FunctionalState.ClearSelected();
                  for (int iIndex = 0; iIndex < MainForm.listBox_AggregatedStatus_FunctionalState.Items.Count; iIndex++)
                  {
                    if (MainForm.listBox_AggregatedStatus_FunctionalState.Items[iIndex].Equals(RoadSideObject.sFunctionalState))
                    {
                      MainForm.listBox_AggregatedStatus_FunctionalState.SelectedIndex = iIndex;
                    }
                  }
                  RSMPGS_Main.bIsCurrentlyChangingSelection = false;
                }
              }
            }
          }
        }


        /*
listBox_AggregatedStatus_FunctionalPosition_SelectedIndexChanged(object sender, EventArgs e)
listBox_AggregatedStatus_FunctionalState_SelectedIndexChanged(object sender, EventArgs e)

*/
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Loaded Process data from '{0}'", FileName);
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to load Process data from '{0}' - {1}", FileName, e.Message);
      }


    }

    public void SaveProcessImageValues(string FileName)
    {

      try
      {
        StreamWriter swProcessImageFile = new StreamWriter((System.IO.Stream)File.Create(FileName));

        swProcessImageFile.WriteLine(";");
        swProcessImageFile.WriteLine("; ProcessImage stored " + DateTime.Now.ToString());
        swProcessImageFile.WriteLine(";");

        swProcessImageFile.Close();
      }
      catch
      {
      }

      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
        {
          if (AlarmObject.bActive)
          {
            cPrivateProfile.WriteIniFileInt(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".Active", 1);
          }
          if (AlarmObject.bSuspended)
          {
            cPrivateProfile.WriteIniFileInt(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".Suspended", 1);
          }
          if (AlarmObject.bAcknowledged)
          {
            cPrivateProfile.WriteIniFileInt(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".Acknowledged", 1);
          }
          /*
          if (AlarmObject.AlarmEvents.Count > 0)
          {
            cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".AlarmEvents", AlarmObject.AlarmEvents.Count.ToString());
            int iIndex = 0;
            foreach (cAlarmEvent AlarmEvent in AlarmObject.AlarmEvents)
            {
              cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".AlarmEvent_" + iIndex.ToString() + ".MessageId", AlarmEvent.sMessageId);
              cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".AlarmEvent_" + iIndex.ToString() + ".TimeStamp", AlarmEvent.sTimeStamp);
              iIndex++;
            }
          }
          */
          foreach (cAlarmReturnValue AlarmReturnValue in AlarmObject.AlarmReturnValues)
          {
            if (AlarmReturnValue.Value.Quality == cValue.eQuality.recent)
            {
              cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + "." + AlarmReturnValue.sName + ".Value", AlarmReturnValue.Value.GetValue().ToString());
            }
          }
        }
        foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
        {
          foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
          {
            if (StatusReturnValue.Value.GetValue() != "?")
            {
              cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".Status", StatusObject.sStatusCodeId + "." + StatusReturnValue.sName + ".Status", StatusReturnValue.Value.GetValue().ToString());
            }
          }
        }

        if (RoadSideObject.bIsComponentGroup)
        {
          if (RoadSideObject.sFunctionalPosition.Length > 0)
          {
            cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "FunctionalPosition", RoadSideObject.sFunctionalPosition);
          }
          if (RoadSideObject.sFunctionalState.Length > 0)
          {
            cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "FunctionalState", RoadSideObject.sFunctionalState);
          }

          for (int iIndex = 0; iIndex < RoadSideObject.bBitStatus.GetLength(0); iIndex++)
          {
            if (RoadSideObject.bBitStatus[iIndex])
            {
              cPrivateProfile.WriteIniFileInt(FileName, RoadSideObject.UniqueId() + ".AggregatedStatus", "BitStatus_" + iIndex.ToString(), RoadSideObject.bBitStatus[iIndex] ? 1 : 0);
            }
          }
        }

      }

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Stored Process data to '{0}'", FileName);
    }

    public void UpdateStatusValue(ref RSMP_Messages.Status_VTQ s, string sType, object oValue, List<Dictionary<string, object>> items)
    {
      // Could be array, in which case "oValue" is not used
      if (sType.Equals("array", StringComparison.OrdinalIgnoreCase))
      {
        
        if (items == null)
        {
          s.s = new List<Dictionary<string, string>>();
          s.q = "unknown";
        }
        else
        {
          s.s = items;
          s.q = "recent";
        }
        return;
      }

      if (oValue == null)
      {
        s.s = null;
        s.q = "unknown";
        return;
      }

      // Could be base64
      if (sType.Equals("base64", StringComparison.OrdinalIgnoreCase))
      {
        // Path?
        if (oValue.ToString().Contains("\\"))
        {
          try
          {
            byte[] Base64Bytes = null;
            // Open file for reading 
            System.IO.FileStream fsBase64 = new System.IO.FileStream(oValue.ToString(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader brBase64 = new System.IO.BinaryReader(fsBase64);
            long lBytes = new System.IO.FileInfo(oValue.ToString()).Length;
            Base64Bytes = brBase64.ReadBytes((Int32)lBytes);
            fsBase64.Close();
            fsBase64.Dispose();
            brBase64.Close();
            s.s = Convert.ToBase64String(Base64Bytes);
            if (s.s.ToString().Length > (cTcpSocketClientThread.BUFLENGTH - 100))
            {
              RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Base64 encoded packet is too big (" + Base64Bytes.GetLength(0).ToString() + " bytes), max buffer length is " + cTcpSocketClientThread.BUFLENGTH.ToString() + " bytes");
              s.s = null;
            }
            s.q = "recent";
          }
          catch (Exception e)
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not base64-encode and send file '{0}', error {1}", oValue.ToString(), e.Message);
            s.q = "unknown";
          }
        }
        else
        {
          s.s = oValue;
          s.q = "recent";
        }
        return;
      }


      s.s = oValue;
      s.q = "recent";

    }

    public void CyclicCleanup(int iElapsedMillisecs)
    {
      List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();
      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        sS.Clear();
        // Delete subscription if it already exists
        foreach (cSubscription Subscription in RoadSideObject.Subscriptions)
        {
          if (Subscription.SubscribeStatus == cSubscription.SubscribeMethod.Interval || Subscription.SubscribeStatus == cSubscription.SubscribeMethod.IntervalAndOnChange)
          {
            if (DateTime.Compare(Subscription.LastUpdate.AddMilliseconds(Subscription.UpdateRate), DateTime.Now) <= 0)
            {
              RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
              s.sCI = Subscription.StatusObject.sStatusCodeId;
              s.n = Subscription.StatusReturnValue.sName; // Subscription.StatusObject.StatusReturnValues .StatusReturnValues[iIndex].sName;
              UpdateStatusValue(ref s, Subscription.StatusReturnValue.Value.GetValueType(), Subscription.StatusReturnValue.Value.GetValue(), Subscription.StatusReturnValue.Value.GetArray());
              sS.Add(s);
              Subscription.LastUpdate = DateTime.Now;
            }
          }
        }
        if (sS.Count > 0)
        {
          RSMPGS.JSon.CreateAndSendStatusUpdateMessage(RoadSideObject, sS);
        }
      }
    }
  }

}
