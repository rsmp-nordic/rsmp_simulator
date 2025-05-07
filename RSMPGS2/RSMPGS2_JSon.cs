using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics.Eventing.Reader;
using static System.Net.WebRequestMethods;
using System.Security.Claims;
using RSMP_Messages;
using static nsRSMPGS.cValue;

namespace nsRSMPGS
{

  //
  // JSon decoding and encoding
  //
  // All decoding is running delegated, hence it is safe to use all listviews and stuff
  // here...
  //
  // CreateAndSendVersionMessage is however called by the socket thread
  //

  public class cJSonGS2 : cJSon
  {

    public override bool DecodeAndParseGSSpecificJSonPacket(RSMP_Messages.Header Header, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
    {

      bool bSuccess = false;

      lock (this)
      {
        try
        {

          switch (Header.type.ToLower())
          {

            case "alarm":
              bSuccess = DecodeAndParseAlarmMessage(Header, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
              break;

            case "commandresponse":
              bSuccess = DecodeAndParseCommandMessage(Header, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
              break;

            case "statusresponse":
            case "statusupdate":
              bSuccess = DecodeAndParseStatusMessage(Header, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
              break;

            case "aggregatedstatus":
              bSuccess = DecodeAndParseAggregatedStatusMessage(Header, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
              break;

            default:
              sError = "Illegal packet type: '" + Header.type + "', MsgId: '" + Header.mId + "'";
              break;
          }
        }
        catch (Exception e)
        {
          sError = "Failed to deserialize packet type: '" + Header.type + "', MsgId: '" + Header.mId + "': " + e.Message;
        }
      }

      return bSuccess;

    }

    private bool DecodeAndParseAlarmMessage(RSMP_Messages.Header packetHeader, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
    {
      StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

      try
      {
        RSMP_Messages.AlarmHeaderAndBody AlarmHeader = JSonSerializer.Deserialize<RSMP_Messages.AlarmHeaderAndBody>(sJSon);

        if (AlarmHeader.cat == null || AlarmHeader.cat == "")
        {
          sError = "Failed to handle Alarm message. Category (cat) missing or empty in alarm message";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
          return false;
        }

        if (AlarmHeader.pri == null || AlarmHeader.pri == "")
        {
          sError = "Failed to handle Alarm message. Priority (pri) missing or empty in alarm message";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
          return false;
        }

        cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(AlarmHeader.ntsOId, AlarmHeader.cId, bUseCaseSensitiveIds);
        if (RoadSideObject == null)
        {
          sError = "Failed to handle Alarm message, could not find object, ntsOId: `" + AlarmHeader.ntsOId + "´, cId: `" + AlarmHeader.cId + "´";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
          return false;
        }

        cAlarmObject AlarmObject = RoadSideObject.AlarmObjects.Find(x => x.sAlarmCodeId.Equals(AlarmHeader.aCId, sc));
        if (AlarmObject == null)
        {
          sError = "Failed to handle Alarm message, could not find alarm code id, ntsOId: `" + AlarmHeader.ntsOId + "´, cId: `" + AlarmHeader.cId + "´, aCId: `" + AlarmHeader.aCId + "´";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
          return false;
        }

        // Previosly, RSMPGS2 considered alarm messages which didn't contain 'cat' to orignate from
        // SCADA. This resulted in not performing any validation, writing to syslog and returning true.
        // RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got alarm message from SCADA, aSp: {0} (corresponding MsgId {1}) ", AlarmHeader.aSp, AlarmHeader.mId);

        if (AlarmObject.sPriority != AlarmHeader.pri)
        {
          sError = "Failed to handle Alarm message. Priority (pri) mismatch, pri: `" + AlarmHeader.pri + "´";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
          return false;
        }

        if (AlarmObject.sCategory != AlarmHeader.cat)
        {
          sError = "Failed to handle Alarm message. Category (cat) mismatch, cat: `" + AlarmHeader.cat + "´";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
          return false;
        }

        cAlarmEvent AlarmEvent = new cAlarmEvent();
        AlarmEvent.AlarmObject = AlarmObject;
        AlarmEvent.sDirection = "Received";
        AlarmEvent.sTimeStamp = UnpackISO8601UTCTimeStamp(AlarmHeader.aTs);
        AlarmEvent.sMessageId = AlarmHeader.mId;
        AlarmEvent.sAlarmCodeId = AlarmHeader.aCId;

        if (AlarmHeader.rvs != null)
        {
          foreach (RSMP_Messages.AlarmReturnValue Reply in AlarmHeader.rvs)
          {
            cAlarmReturnValue AlarmReturnValue = AlarmObject.AlarmReturnValues.Find(x => x.sName.Equals(Reply.n, sc));
            if (AlarmReturnValue == null)
            {
              sError = "Failed to handle Alarm message. Failed to find name, n: `" + Reply.n + "´";
              RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
              return false;
            }

            if (AlarmReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
            {
              if (RSMPGS.MainForm.ToolStripMenuItem_StoreBase64Updates.Checked)
              {
                AlarmReturnValue.Value.SetValue(RSMPGS.SysLog.StoreBase64DebugData((string)Reply.v));
              }
              else
              {
                AlarmReturnValue.Value.SetValue("base64");
              }
            }
            else
            {
              AlarmReturnValue.Value.SetValue(Reply.v);
            }

            if (Reply.v == null)
            {
              sError = "Value and/or type is out of range or invalid for this RSMP protocol version, type: `" + AlarmReturnValue.Value.GetValueType() + "´, returnvalue: `(null)´";
              RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
              return false;
            }

            if (AlarmReturnValue.Value.GetValueType().Equals("array", StringComparison.OrdinalIgnoreCase))
            {
              string arrayResult = ValidateArrayObject(AlarmReturnValue.Value.ValueTypeObject.Items, Reply.v);
              Reply.v = stringifyObject(Reply.v);

              List<Dictionary<string, object>> dictionaries = JSonSerializer.Deserialize<List<Dictionary<string, object>>>((string)Reply.v);

              AlarmReturnValue.Value.SetArray(dictionaries);

              Reply.v = "(array)";

              if (arrayResult != "success")
              {
                sError = "Failed to handle Alarm message. Failed to handle array , array: `" + Reply.v + "´";
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, arrayResult);
                return false;
              }
            }

            if (ValidateTypeAndRange(AlarmReturnValue.Value.GetValueType(),
                Reply.v.ToString(), AlarmReturnValue.Value.GetSelectableValues(),
                AlarmReturnValue.Value.GetValueMin(), AlarmReturnValue.Value.GetValueMax()))
            {
              if (AlarmReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
              {
                AlarmEvent.AlarmEventReturnValues.Add(new cAlarmEventReturnValue(Reply.n, "base64"));
              }
              else
              {
                AlarmEvent.AlarmEventReturnValues.Add(new cAlarmEventReturnValue(Reply.n, Reply.v));
              }
            }
            else
            {
              string status = Reply.v.ToString();
              string sReturnValue = (status.Length < 10) ? status : status.Substring(0, 9) + "...";
              sError = "Value and/or type is out of range or invalid for this RSMP protocol version, type: `" + AlarmReturnValue.Value.GetValueType() + "´, returnvalue: `" + sReturnValue + "´";
              RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
              return false;
            }
          }
        }

        switch (AlarmHeader.aSp)
        {
          case var name when string.Equals(name, "Issue", sc):
            AlarmEvent.sEvent = AlarmHeader.aSp + " / " + AlarmHeader.aS;
            if (AlarmHeader.aS.Equals("active", StringComparison.OrdinalIgnoreCase))
            {
              AlarmObject.AlarmCount++;
            }
            break;
          case var name when string.Equals(name, "Acknowledge", sc):
            AlarmEvent.sEvent = AlarmHeader.aSp + " / " + AlarmHeader.ack;
            break;
          case var name when string.Equals(name, "Suspend", sc):
            AlarmEvent.sEvent = AlarmHeader.aSp + " / " + AlarmHeader.sS;
            break;
          default:
            AlarmEvent.sEvent = "(unknown: " + AlarmHeader.aSp + ")";
            sError = "Could not parse correct alarm state `" + AlarmHeader.aSp + "´ (corresponding MsgId " + AlarmHeader.mId + ")";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
        }

        switch (AlarmHeader.aS)
        {
          case var name when string.Equals(name, "Active", sc):
            AlarmObject.bActive = true;
            break;
          case var name when string.Equals(name, "inActive", sc):
            AlarmObject.bActive = false;
            break;
          default:
            AlarmEvent.sEvent = "(unknown: " + AlarmHeader.aS + ")";
            sError = "Could not parse correct alarm state `" + AlarmHeader.aS + "´ (corresponding MsgId " + AlarmHeader.mId + ")";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
        }

        switch (AlarmHeader.ack)
        {
          case var name when string.Equals(name, "Acknowledged", sc):
            AlarmObject.bAcknowledged = true;
            break;
          case var name when string.Equals(name, "notAcknowledged", sc):
            AlarmObject.bAcknowledged = false;
            break;
          default:
            AlarmEvent.sEvent = "(unknown: " + AlarmHeader.ack + ")";
            sError = "Could not parse correct alarm state `" + AlarmHeader.ack + "´ (corresponding MsgId " + AlarmHeader.mId + ")";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
        }

        switch (AlarmHeader.sS)
        {
          case var name when string.Equals(name, "Suspended", sc):
            AlarmObject.bSuspended = true;
            break;
          case var name when string.Equals(name, "notSuspended", sc):
            AlarmObject.bSuspended = false;
            break;
          default:
            AlarmEvent.sEvent = "(unknown: " + AlarmHeader.sS + ")";
            sError = "Could not parse correct alarm state `" + AlarmHeader.sS + "´ (corresponding MsgId " + AlarmHeader.mId + ")";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
        }

        if (AlarmObject.bActive == false && AlarmObject.bAcknowledged)
        {
          AlarmObject.AlarmCount = 0;
        }
        
        // Update the column "External Alarm Code Id" based on xACId
        AlarmObject.sExternalAlarmCodeId = AlarmHeader.xACId;

        // Don't update the alarm unless the timestamp is equal or newer
        if(DateTime.Compare(AlarmObject.sTimestamp, DateTime.Parse(AlarmHeader.aTs)) <= 0)
        {
          AlarmObject.sTimestamp = DateTime.Parse(AlarmHeader.aTs);
          RSMPGS.MainForm.AddAlarmEventToAlarmObjectAndToList(AlarmObject, AlarmEvent);
          RSMPGS.MainForm.UpdateAlarmListView(AlarmObject);
        }

      }
      catch (Exception e)
      {
        sError = "Failed to deserialize packet: " + e.Message;
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
        return false;
      }

      return true;
    }

    private bool DecodeAndParseAggregatedStatusMessage(RSMP_Messages.Header packetHeader, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
    {

      StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

      bool bSuccess = false;

      try
      {
        RSMP_Messages.AggregatedStatus AggregatedStatus = JSonSerializer.Deserialize<RSMP_Messages.AggregatedStatus>(sJSon);

        cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(AggregatedStatus.ntsOId, AggregatedStatus.cId, bUseCaseSensitiveIds);

        if (RoadSideObject != null)
        {
          RoadSideObject.bBitStatus = AggregatedStatus.se;
          RoadSideObject.sFunctionalPosition = AggregatedStatus.fP;
          RoadSideObject.sFunctionalState = AggregatedStatus.fS;
          cAggregatedStatusEvent AggregatedStatusEvent = new cAggregatedStatusEvent();
          AggregatedStatusEvent.sTimeStamp = UnpackISO8601UTCTimeStamp(AggregatedStatus.aSTS);
          AggregatedStatusEvent.sMessageId = AggregatedStatus.mId;
          AggregatedStatusEvent.sFunctionalPosition = AggregatedStatus.fP;
          AggregatedStatusEvent.sFunctionalState = AggregatedStatus.fS;

          for (int i = 1; i < AggregatedStatus.se.Length + 1; i++)
          {
            AggregatedStatusEvent.sBitStatus += "B" + i + ": " + AggregatedStatus.se[i - 1] + " | ";
          }
          AggregatedStatusEvent.sBitStatus.Trim();

          if (RSMPGS_Main.bWriteEventsContinous)
          {
            RSMPGS.SysLog.EventLog("AggregatedStatus;{0}\tMId: {1}\tComponentId: {2}\tBitStatus: {3}\tFuncPos: {4}\tFunkState: {5}",
            AggregatedStatusEvent.sTimeStamp, AggregatedStatusEvent.sMessageId, AggregatedStatus.cId, AggregatedStatusEvent.sBitStatus,
            AggregatedStatusEvent.sFunctionalPosition, AggregatedStatusEvent.sFunctionalState);
          }

          RoadSideObject.AggregatedStatusEvents.Add(AggregatedStatusEvent);
          RSMPGS.MainForm.HandleAggregatedStatusListUpdate(RoadSideObject, AggregatedStatusEvent);
          bSuccess = true;
        }
        if (bSuccess == false)
        {
          sError = "Failed to update AggregatedStatus, could not find object, ntsOId: '" + AggregatedStatus.ntsOId + "', cId: '" + AggregatedStatus.cId + "'";
        }
      }
      catch (Exception e)
      {
        sError = "Failed to deserialize packet: " + e.Message;
        bSuccess = false;
      }

      return bSuccess;

    }

    private bool DecodeAndParseCommandMessage(RSMP_Messages.Header packetHeader, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
    {

      StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

      try
      {
        RSMP_Messages.CommandResponse CommandResponse = JSonSerializer.Deserialize<RSMP_Messages.CommandResponse>(sJSon);

        if (!CommandResponse.type.Equals("commandresponse", StringComparison.OrdinalIgnoreCase))
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got commandrequest message from SCADA, (corresponding MsgId {0}) ", CommandResponse.mId);
          return true;
        }

        cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(CommandResponse.ntsOId, CommandResponse.cId, bUseCaseSensitiveIds);
        if (RoadSideObject == null)
        {
          sError = "Failed to handle Command message, could not find object, ntsOId: `" + CommandResponse.ntsOId + "´, cId: `" + CommandResponse.cId + "´";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
          return false;
        }

        foreach (RSMP_Messages.CommandResponse_Value Reply in CommandResponse.rvs)
        {
          cCommandObject CommandObject = RoadSideObject.CommandObjects.Find(x => x.sCommandCodeId.Equals(Reply.cCI, sc));
          if (CommandObject == null)
          {
            sError = "Failed to handle Command message, could not find command code id, ntsOId: `" + CommandResponse.ntsOId + "´, cId: `" + CommandResponse.cId + "´, sCI: `" + Reply.cCI + "´";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
          }

          cCommandReturnValue CommandReturnValue = CommandObject.CommandReturnValues.Find(x => x.sName.Equals(Reply.n, sc));
          if (CommandReturnValue == null)
          {
            sError = "Failed to handle Command message. Failed to find name, n: `" + Reply.n + "´";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
          }
          CommandReturnValue.sAge = Reply.age;

          // Here we're treating 'age' (commands) as a 'quality' (status), but they are 100% equal in the spec.
          if (!Enum.GetNames(typeof(cValue.eQuality)).Any(x => x.Equals(CommandReturnValue.sAge, sc)))
          {
            sError = "Failed to handle Command message. Failed to find age, age: `" + Reply.age + "´";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
          }

          if (!ValidateTypeAndRange(CommandReturnValue.Value.GetValueType(), Reply.v, CommandReturnValue.Value.GetSelectableValues(), CommandReturnValue.Value.GetValueMin(), CommandReturnValue.Value.GetValueMax()))
          {
            string sStatusValue;
            if (Reply.v == null)
            {
              sStatusValue = "(null)";
            }
            else
            {
              sStatusValue = (Reply.v.ToString().Length < 10) ? Reply.v.ToString() : Reply.v.ToString().Substring(0, 9) + "...";
            }
            sError = "Value and/or type is out of range or invalid for this RSMP protocol version, type: " + CommandReturnValue.Value.GetValueType() + ", value: " + sStatusValue;
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
          }

          cCommandEvent CommandEvent = new cCommandEvent();
          CommandEvent.sTimeStamp = UnpackISO8601UTCTimeStamp(CommandResponse.cTS);
          CommandEvent.sMessageId = CommandResponse.mId;
          CommandEvent.sEvent = "Received command";
          CommandEvent.sCommandCodeId = Reply.cCI;
          CommandEvent.sName = Reply.n;
          if (CommandReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
          {
            if (RSMPGS.MainForm.ToolStripMenuItem_StoreBase64Updates.Checked)
            {
              RSMPGS.SysLog.StoreBase64DebugData(Reply.v.ToString());
            }
            CommandEvent.oValue = "base64";
          }
          else
          {
            CommandEvent.oValue = Reply.v;
          }
          CommandEvent.sAge = Reply.age;
          CommandReturnValue.sLastRecValue = Reply.v.ToString();
          CommandReturnValue.sLastRecAge = Reply.age;

          if (RSMPGS_Main.bWriteEventsContinous)
          {
            RSMPGS.SysLog.EventLog("Command;{0}\tMId: {1}\tComponentId: {2}\tCommandCodeId: {3}\tName: {4}\tCommand: {5}\tValue: {6}\t Age: {7}\tEvent: {8}",
                    CommandEvent.sTimeStamp, CommandEvent.sMessageId, CommandResponse.cId, CommandEvent.sCommandCodeId,
                    CommandEvent.sName, CommandEvent.sCommand, CommandEvent.oValue, CommandEvent.sAge, CommandEvent.sEvent);
          }
          RoadSideObject.CommandEvents.Add(CommandEvent);
          RSMPGS.MainForm.HandleCommandListUpdate(RoadSideObject, CommandResponse.ntsOId, CommandResponse.cId, CommandEvent, false, bUseCaseSensitiveIds);
        }
      }
      catch (Exception e)
      {
        sError = "Failed to deserialize packet: " + e.Message;
        return false;
      }
      return true;
    }

    private bool DecodeAndParseStatusMessage(RSMP_Messages.Header packetHeader, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
    {
      StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

      try
      {
        RSMP_Messages.StatusResponse StatusResponse = JSonSerializer.Deserialize<RSMP_Messages.StatusResponse>(sJSon);

        cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(StatusResponse.ntsOId, StatusResponse.cId, bUseCaseSensitiveIds);
        if (RoadSideObject == null)
        {
          sError = "Failed to handle Status message, could not find object, ntsOId: `" + StatusResponse.ntsOId + "´, cId: `" + StatusResponse.cId + "´";
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
          return false;
        }

        foreach (RSMP_Messages.Status_VTQ Reply in StatusResponse.sS)
        {
          cStatusObject StatusObject = RoadSideObject.StatusObjects.Find(x => x.sStatusCodeId.Equals(Reply.sCI, sc));
          if (StatusObject == null)
          {
            sError = "Failed to handle Status message, could not find status code id, ntsOId: `" + StatusResponse.ntsOId + "´, cId: `" + StatusResponse.cId + "´, sCI: `" + Reply.sCI + "´";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
          }

          cStatusReturnValue StatusReturnValue = StatusObject.StatusReturnValues.Find(x => x.sName.Equals(Reply.n, sc));
          if (StatusReturnValue == null)
          {
            sError = "Failed to handle Status message. Failed to find name, n: `" + Reply.n + "´";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
          }

          if (StatusReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
          {
            if (RSMPGS.MainForm.ToolStripMenuItem_StoreBase64Updates.Checked)
            {
              StatusReturnValue.Value.SetValue(RSMPGS.SysLog.StoreBase64DebugData(Reply.s.ToString()));
            }
            else
            {
              StatusReturnValue.Value.SetValue("base64");
            }
          }
          else
          {
            StatusReturnValue.Value.SetValue(Reply.s.ToString());
          }

          if (StatusReturnValue.Value.ValueTypeObject.ValueType.ToString() == "_array")
          {
            string arrayResult = ValidateArrayObject(StatusReturnValue.Value.ValueTypeObject.Items, Reply.s);

            Reply.s = stringifyObject(Reply.s);

            List<Dictionary<string, object>> dictionaries = JSonSerializer.Deserialize<List<Dictionary<string, object>>>((string)Reply.s);
            StatusReturnValue.Value.SetArray(dictionaries);

            Reply.s = "(array)";

            if (arrayResult != "success")
            {
              sError = "Failed to handle Status message. Failed to handle array , array: `" + Reply.s + "´";
              RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, arrayResult);
              return false;
            }
          }

          StatusReturnValue.sQuality = Reply.q;
          if (!Enum.GetNames(typeof(cValue.eQuality)).Any(x => x.Equals(StatusReturnValue.sQuality, sc)))
          {
            sError = "Failed to handle Status message. Failed to find quality, q: `" + Reply.q + "´";
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
          }

          if (!ValidateTypeAndRange(StatusReturnValue.Value.GetValueType(), Reply.s.ToString(), StatusReturnValue.Value.GetSelectableValues(), StatusReturnValue.Value.GetValueMin(), StatusReturnValue.Value.GetValueMax()))
          {
            string sStatusValue;
            if (Reply.s == null)
            {
              sStatusValue = "(null)";
            }
            else
            {
              string status = Reply.s.ToString();
              sStatusValue = (status.Length < 10) ? status : status.Substring(0, 9) + "...";
            }
            sError = "Value and/or type is out of range or invalid for this RSMP protocol version, type: " + StatusReturnValue.Value.GetValueType() + ", quality: " + StatusReturnValue.sQuality + ", statusvalue: " + sStatusValue;
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
            return false;
          }

          cStatusEvent StatusEvent = new cStatusEvent();
          StatusEvent.sTimeStamp = UnpackISO8601UTCTimeStamp(StatusResponse.sTs);
          StatusEvent.sMessageId = StatusResponse.mId;
          StatusEvent.sEvent = "Received status";
          StatusEvent.sStatusCodeId = Reply.sCI;
          StatusEvent.sName = Reply.n;
          if (StatusReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
          {
            StatusEvent.sStatus = "base64";
          }
          else
          {
            StatusEvent.sStatus = Reply.s.ToString();
          }
          StatusEvent.sQuality = Reply.q;
          if (RSMPGS_Main.bWriteEventsContinous)
          {
            RSMPGS.SysLog.EventLog("Status;{0}\tMId: {1}\tComponentId: {2}\tStatusCommandId: {3}\tName: {4}\tStatus: {5}\tQuality: {6}\tUpdateRate: {7}\tUpdateOnChange: {8}\tEvent: {9}",
                StatusEvent.sTimeStamp, StatusEvent.sMessageId, StatusResponse.cId, StatusEvent.sStatusCodeId,
                StatusEvent.sName, StatusEvent.sStatus, StatusEvent.sQuality, StatusEvent.sUpdateRate, StatusEvent.bUpdateOnChange, StatusEvent.sEvent);
          }
          RoadSideObject.StatusEvents.Add(StatusEvent);
          RSMPGS.MainForm.HandleStatusListUpdate(RoadSideObject, StatusEvent, false);
        }
      }
      catch (Exception e)
      {
        sError = "Failed to deserialize packet: " + e.Message;
        return false;
      }
      return true;
    }

    public void CreateAndSendAggregatedStatusRequestMessage(cRoadSideObject RoadSideObject)
    {

      RSMP_Messages.AggregatedStatusRequest AggregatedStatusRequest;

      string sSendBuffer;

      try
      {

        AggregatedStatusRequest = new RSMP_Messages.AggregatedStatusRequest();

        AggregatedStatusRequest.mType = "rSMsg";
        AggregatedStatusRequest.type = "AggregatedStatusRequest";
        AggregatedStatusRequest.mId = System.Guid.NewGuid().ToString();

        AggregatedStatusRequest.ntsOId = RoadSideObject.sNTSObjectId;
        AggregatedStatusRequest.xNId = RoadSideObject.sExternalNTSId;
        AggregatedStatusRequest.cId = RoadSideObject.sComponentId;

        sSendBuffer = JSonSerializer.SerializeObject(AggregatedStatusRequest);

        RSMPGS.JSon.SendJSonPacket(AggregatedStatusRequest.type, AggregatedStatusRequest.mId, sSendBuffer, true);

        if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent AggregatedStatusRequest message, ntsOId: " + RoadSideObject.sNTSObjectId + ", cId: " + RoadSideObject.sComponentId + ", MsgId: " + AggregatedStatusRequest.mId);
        }
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create AggregatedStatusRequest message: {0}", e.Message);
      }

    }

    public void CreateAndSendAlarmMessage(cAlarmObject AlarmObject, AlarmSpecialisation alarmSpecialisation)
    {
      RSMP_Messages.AlarmHeader AlarmHeader;
      cAlarmEvent AlarmEvent = null;
      string sSendBuffer;

      try
      {

        AlarmHeader = new RSMP_Messages.AlarmHeader();

        AlarmHeader.mType = "rSMsg";
        AlarmHeader.type = "Alarm";
        AlarmHeader.mId = System.Guid.NewGuid().ToString();

        AlarmHeader.ntsOId = AlarmObject.RoadSideObject.sNTSObjectId;
        AlarmHeader.xNId = AlarmObject.RoadSideObject.sExternalNTSId;
        AlarmHeader.cId = AlarmObject.RoadSideObject.sComponentId;
        AlarmHeader.aCId = AlarmObject.sAlarmCodeId;
        AlarmHeader.xACId = AlarmObject.sExternalAlarmCodeId;
        AlarmHeader.xNACId = AlarmObject.sExternalNTSAlarmCodeId;

        AlarmHeader.aSp = alarmSpecialisation.ToString();

        sSendBuffer = JSonSerializer.SerializeObject(AlarmHeader);

        AlarmEvent = new cAlarmEvent();
        AlarmEvent.AlarmObject = AlarmObject;
        AlarmEvent.sDirection = "Sent";
        AlarmEvent.sTimeStamp = CreateLocalTimeStamp();
        AlarmEvent.sMessageId = AlarmHeader.mId;
        AlarmEvent.sAlarmCodeId = AlarmHeader.aCId;
        AlarmEvent.sEvent = AlarmHeader.aSp;

        RSMPGS.MainForm.AddAlarmEventToAlarmObjectAndToList(AlarmObject, AlarmEvent);
        RSMPGS.MainForm.UpdateAlarmListView(AlarmObject);
        /*
        if (RSMPGS_Main.bWriteEventsContinous)
        {
          RSMPGS.SysLog.EventLog("Alarm;{0}\tMId: {1}\tComponentId: {2}\tAlarmCodeId: {3}\tEvent: {4}",
              AlarmEvent.sTimeStamp, AlarmEvent.sMessageId, AlarmHeader.cId, AlarmEvent.sAlarmCodeId, AlarmEvent.sEvent);
        }
        */
        RSMPGS.JSon.SendJSonPacket(AlarmHeader.type, AlarmHeader.mId, sSendBuffer, true);
        if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent alarm message, AlarmCode: " + AlarmObject.sAlarmCodeId + ", Type: " + AlarmHeader.type + "/" + AlarmHeader.aSp + ", MsgId: " + AlarmHeader.mId);
        }
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create alarm message: {0}", e.Message);
      }

    }

    public void CreateAndSendCommandMessage(cRoadSideObject RoadSideObject, List<cCommandReturnValue> ReturnValues, bool bUseCaseSensitiveIds)
    {

      StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

      RSMP_Messages.CommandRequest CommandRequest;
      RSMP_Messages.CommandRequest_Value CommandRequest_Value;
      string sSendBuffer;

      try
      {
        CommandRequest = new RSMP_Messages.CommandRequest();

        CommandRequest.mType = "rSMsg";
        CommandRequest.type = "CommandRequest";
        CommandRequest.mId = System.Guid.NewGuid().ToString();

        CommandRequest.ntsOId = RoadSideObject.sNTSObjectId;
        CommandRequest.xNId = RoadSideObject.sExternalNTSId;
        CommandRequest.cId = RoadSideObject.sComponentId;
        CommandRequest.arg = new List<RSMP_Messages.CommandRequest_Value>();
        foreach (cCommandReturnValue CommandReturnValue in ReturnValues)
        {
          CommandRequest_Value = new RSMP_Messages.CommandRequest_Value();
          CommandRequest_Value.cCI = CommandReturnValue.CommandObject.sCommandCodeId;
          CommandRequest_Value.n = CommandReturnValue.sName;
          CommandRequest_Value.cO = CommandReturnValue.sCommand;

          if (CommandReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
          {
            // Path?
            if (CommandReturnValue.Value.GetValue().ToString().Contains("\\"))
            {
              try
              {
                byte[] Base64Bytes = null;
                // Open file for reading 
                System.IO.FileStream fsBase64 = new System.IO.FileStream(CommandReturnValue.Value.GetValue().ToString(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader brBase64 = new System.IO.BinaryReader(fsBase64);
                long lBytes = new System.IO.FileInfo(CommandReturnValue.Value.GetValue().ToString()).Length;
                Base64Bytes = brBase64.ReadBytes((Int32)lBytes);
                fsBase64.Close();
                fsBase64.Dispose();
                brBase64.Close();
                CommandRequest_Value.v = Convert.ToBase64String(Base64Bytes);
                if (CommandRequest_Value.v.ToString().Length > (cTcpSocketClientThread.BUFLENGTH - 100))
                {
                  RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Base64 encoded packet is too big (" + Base64Bytes.GetLength(0).ToString() + " bytes), max buffer length is " + cTcpSocketClientThread.BUFLENGTH.ToString() + " bytes");
                  CommandRequest_Value.v = null;
                }
              }
              catch (Exception e)
              {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not base64-encode and send file '{0}', error {1}", CommandReturnValue.Value.GetValue(), e.Message);
                CommandRequest_Value.v = null;
              }
            }
          }
          else CommandRequest_Value.v = CommandReturnValue.Value.GetValue();
          CommandRequest.arg.Add(CommandRequest_Value);

          cCommandEvent CommandEvent = new cCommandEvent();
          CommandEvent.sTimeStamp = CreateLocalTimeStamp();
          CommandEvent.sMessageId = CommandRequest.mId;
          CommandEvent.sEvent = "Sent Command";
          CommandEvent.sCommandCodeId = CommandReturnValue.CommandObject.sCommandCodeId;
          CommandEvent.sName = CommandReturnValue.sName;
          CommandEvent.sCommand = CommandReturnValue.sCommand;
          CommandEvent.oValue = CommandReturnValue.Value.GetValue();
          RoadSideObject.CommandEvents.Add(CommandEvent);
          RSMPGS.MainForm.HandleCommandListUpdate(RoadSideObject, CommandRequest.ntsOId, CommandRequest.cId, CommandEvent, true, bUseCaseSensitiveIds);

          if (RSMPGS_Main.bWriteEventsContinous)
          {
            RSMPGS.SysLog.EventLog("Command;{0}\tMId: {1}\tComponentId: {2}\tCommandCodeId: {3}\tName: {4}\tCommand: {5}\tValue: {6}\t Age: {7}\tEvent: {8}",
            CommandEvent.sTimeStamp, CommandEvent.sMessageId, CommandRequest.cId, CommandEvent.sCommandCodeId,
            CommandEvent.sName, CommandEvent.sCommand, CommandEvent.oValue, CommandEvent.sAge, CommandEvent.sEvent);
          }
        }

        sSendBuffer = JSonSerializer.SerializeObject(CommandRequest);

        if (RSMPGS.JSon.SendJSonPacket(CommandRequest.type, CommandRequest.mId, sSendBuffer, true))
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent command message, MsgId: " + CommandRequest.mId);
        }
        else
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to send command message, MsgId: " + CommandRequest.mId);
        }

      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create command message: {0}", e.Message);
      }
    }

    public void CreateAndSendStatusMessage(cRoadSideObject RoadSideObject, List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4> StatusSubscribeValues, string statusType)
    {
      RSMP_Messages.StatusRequest StatusRequest;
      RSMP_Messages.StatusRequest_Status StatusRequest_Status;
      cStatusEvent StatusEvent = null;
      string sSendBuffer;

      string messageTypeDisplayed = "status request";
      if (statusType.ToLower() == "statusunsubscribe")
        messageTypeDisplayed = "unsubscription";

      try
      {
        StatusRequest = new RSMP_Messages.StatusRequest();

        StatusRequest.mType = "rSMsg";
        StatusRequest.type = statusType;
        StatusRequest.mId = System.Guid.NewGuid().ToString();

        StatusRequest.ntsOId = RoadSideObject.sNTSObjectId;
        StatusRequest.xNId = RoadSideObject.sExternalNTSId;
        StatusRequest.cId = RoadSideObject.sComponentId;
        StatusRequest.sS = new List<RSMP_Messages.StatusRequest_Status>();

        foreach (RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscribeValue in StatusSubscribeValues)
        {
          StatusRequest_Status = new RSMP_Messages.StatusRequest_Status();
          StatusRequest_Status.sCI = StatusSubscribeValue.sCI;
          StatusRequest_Status.n = StatusSubscribeValue.n;
          StatusRequest.sS.Add(StatusRequest_Status);

          StatusEvent = new cStatusEvent();
          StatusEvent.sTimeStamp = CreateLocalTimeStamp();
          StatusEvent.sMessageId = StatusRequest.mId;
          StatusEvent.sStatusCodeId = StatusRequest_Status.sCI;
          StatusEvent.sName = StatusRequest_Status.n;
          StatusEvent.sEvent = "Sent "+messageTypeDisplayed;

          if (RSMPGS_Main.bWriteEventsContinous)
          {
            RSMPGS.SysLog.EventLog("Status;{0}\tMId: {1}\tComponentId: {2}\tStatusCommandId: {3}\tName: {4}\tStatus: {5}\tQuality: {6}\tUpdateRate: {7}\tEvent: {8}",
                StatusEvent.sTimeStamp, StatusEvent.sMessageId, StatusRequest.cId, StatusEvent.sStatusCodeId,
                StatusEvent.sName, StatusEvent.sStatus, StatusEvent.sQuality, StatusEvent.sUpdateRate, StatusEvent.sEvent);
          }

          RoadSideObject.StatusEvents.Add(StatusEvent);
          RSMPGS.MainForm.HandleStatusListUpdate(RoadSideObject, StatusEvent, true);
        }

        sSendBuffer = JSonSerializer.SerializeObject(StatusRequest);

        RSMPGS.JSon.SendJSonPacket(StatusRequest.type, StatusRequest.mId, sSendBuffer, true);
        if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent "+ messageTypeDisplayed + " message, ComponentId: " + StatusRequest.cId + " , MsgId: " + StatusRequest.mId);
        }
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create "+ messageTypeDisplayed + " message: {0}", e.Message);
      }
    }

    public void CreateAndSendSubscriptionMessage(cRoadSideObject RoadSideObject, List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4> StatusSubscribeValues)
    {
      if (NegotiatedRSMPVersion > RSMPVersion.RSMP_3_1_4 )
      {
        CreateAndSendSubscriptionMessage_over_3_1_4(RoadSideObject, StatusSubscribeValues);
      }
      else
      {
        CreateAndSendSubscriptionMessage_upto_3_1_4(RoadSideObject, StatusSubscribeValues);
      }
    }

    public void CreateAndSendSubscriptionMessage_upto_3_1_4(cRoadSideObject RoadSideObject, List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4> StatusSubscribeValues)
    {


      RSMP_Messages.StatusSubscribe_UpTo_3_1_4 StatusSubscribe;

      string sSendBuffer;

      try
      {
        StatusSubscribe = new RSMP_Messages.StatusSubscribe_UpTo_3_1_4();

        StatusSubscribe.mType = "rSMsg";
        StatusSubscribe.type = "StatusSubscribe";
        StatusSubscribe.mId = System.Guid.NewGuid().ToString();

        StatusSubscribe.ntsOId = RoadSideObject.sNTSObjectId;
        StatusSubscribe.xNId = RoadSideObject.sExternalNTSId;
        StatusSubscribe.cId = RoadSideObject.sComponentId;
        StatusSubscribe.sS = new List<RSMP_Messages.StatusSubscribe_Status_UpTo_3_1_4>();

        foreach (RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscriptionValue in StatusSubscribeValues)
        {
          RSMP_Messages.StatusSubscribe_Status_UpTo_3_1_4 StatusSubscribeValue = new RSMP_Messages.StatusSubscribe_Status_UpTo_3_1_4();

          StatusSubscribeValue.n = StatusSubscriptionValue.n;
          StatusSubscribeValue.sCI = StatusSubscriptionValue.sCI;
          StatusSubscribeValue.uRt = StatusSubscriptionValue.uRt;

          StatusSubscribe.sS.Add(StatusSubscribeValue);

          AddSubscriptionMessageEvent(RoadSideObject, StatusSubscribe.mId, StatusSubscribe.cId, StatusSubscriptionValue);

        }

        sSendBuffer = JSonSerializer.SerializeObject(StatusSubscribe);
        if (RSMPGS.JSon.SendJSonPacket(StatusSubscribe.type, StatusSubscribe.mId, sSendBuffer, true))
        {
          if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent subscription message, ComponentId: " + StatusSubscribe.cId + " , MsgId: " + StatusSubscribe.mId);
          }
        }
        else
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to send subscription message, ComponentId: " + StatusSubscribe.cId + " , MsgId: " + StatusSubscribe.mId);
        }
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create subscription message: {0}", e.Message);
      }

    }

    public void CreateAndSendSubscriptionMessage_over_3_1_4(cRoadSideObject RoadSideObject, List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4> StatusSubscribeValues)
    {


      RSMP_Messages.StatusSubscribe_Over_3_1_4 StatusSubscribe;

      string sSendBuffer;

      try
      {
        StatusSubscribe = new RSMP_Messages.StatusSubscribe_Over_3_1_4();

        StatusSubscribe.mType = "rSMsg";
        StatusSubscribe.type = "StatusSubscribe";
        StatusSubscribe.mId = System.Guid.NewGuid().ToString();

        StatusSubscribe.ntsOId = RoadSideObject.sNTSObjectId;
        StatusSubscribe.xNId = RoadSideObject.sExternalNTSId;
        StatusSubscribe.cId = RoadSideObject.sComponentId;
        StatusSubscribe.sS = StatusSubscribeValues;

        foreach (RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscriptionValue in StatusSubscribeValues)
        {
          AddSubscriptionMessageEvent(RoadSideObject, StatusSubscribe.mId, StatusSubscribe.cId, StatusSubscriptionValue);
        }

        sSendBuffer = JSonSerializer.SerializeObject(StatusSubscribe);
        if (RSMPGS.JSon.SendJSonPacket(StatusSubscribe.type, StatusSubscribe.mId, sSendBuffer, true))
        {
          if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent subscription message, ComponentId: " + StatusSubscribe.cId + " , MsgId: " + StatusSubscribe.mId);
          }
        }
        else
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to send subscription message, ComponentId: " + StatusSubscribe.cId + " , MsgId: " + StatusSubscribe.mId);
        }
      }
      catch (Exception e)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create subscription message: {0}", e.Message);
      }

    }

    private void AddSubscriptionMessageEvent(cRoadSideObject RoadSideObject, string sMessageId, string sComponentId, RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscribe_Status)
    {

      cStatusEvent StatusEvent = null;

      StatusEvent = new cStatusEvent();
      StatusEvent.sTimeStamp = CreateLocalTimeStamp();
      StatusEvent.sMessageId = sMessageId;
      StatusEvent.sEvent = "Sent subscription";
      StatusEvent.sStatusCodeId = StatusSubscribe_Status.sCI;
      StatusEvent.sName = StatusSubscribe_Status.n;
      StatusEvent.sUpdateRate = StatusSubscribe_Status.uRt;
      StatusEvent.bUpdateOnChange = StatusSubscribe_Status.sOc;

      if (RSMPGS_Main.bWriteEventsContinous)
      {
        RSMPGS.SysLog.EventLog("Status;{0}\tMId: {1}\tComponentId: {2}\tStatusCommandId: {3}\tName: {4}\tStatus: {5}\tQuality: {6}\tUpdateRate: {7}\tEvent: {8}",
            StatusEvent.sTimeStamp, StatusEvent.sMessageId, sComponentId, StatusEvent.sStatusCodeId,
            StatusEvent.sName, StatusEvent.sStatus, StatusEvent.sQuality, StatusEvent.sUpdateRate, StatusEvent.sEvent);
      }

      RoadSideObject.StatusEvents.Add(StatusEvent);
      RSMPGS.MainForm.HandleStatusListUpdate(RoadSideObject, StatusEvent, true);

    }

    private string CreateLocalTimeStamp()
    {
      string sTimeStamp = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");
      return sTimeStamp;
    }

    public override void SocketWasConnected()
    {
      base.SocketWasConnected();
    }

    public override void SocketWasClosed()
    {

      RSMPGS.MainForm.button_AggregatedStatus_Request.Enabled = false;

      base.SocketWasClosed();
    }

    public override void SocketIsConnectedAndInitSequenceIsNegotiated()
    {

      base.SocketIsConnectedAndInitSequenceIsNegotiated();

      string sAutoSubscribeAtConnectInterval = cPrivateProfile.GetIniFileString("RSMP", "AutoSubscribeAtConnectInterval", "");

      int iAutoSubscribeValues = 0;

      RSMPGS.MainForm.button_AggregatedStatus_Request.Enabled = RSMPGS.JSon.NegotiatedRSMPVersion >= cJSon.RSMPVersion.RSMP_3_1_5;

      // Auto subscribe for all status' (2019-04-14/TR for some performance testing Sthlm Stad)
      if (sAutoSubscribeAtConnectInterval != "")
      {

        foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
        {

          List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4> StatusSubscribe_Values = new List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4>();

          foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
          {
            foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
            {

              RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscribe_Value = new RSMP_Messages.StatusSubscribe_Status_Over_3_1_4();
              StatusSubscribe_Value.sCI = StatusReturnValue.StatusObject.sStatusCodeId;
              StatusSubscribe_Value.n = StatusReturnValue.sName;
              StatusSubscribe_Value.uRt = sAutoSubscribeAtConnectInterval;
              StatusSubscribe_Value.sOc = false;
              StatusSubscribe_Values.Add(StatusSubscribe_Value);
              iAutoSubscribeValues++;
            }
          }
          RSMPGS.JSon.CreateAndSendSubscriptionMessage(RoadSideObject, StatusSubscribe_Values);
        }
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Initially subscribed to {0} status updates using UpdateRate {1}", iAutoSubscribeValues, sAutoSubscribeAtConnectInterval);
      }

    }

  }

}
