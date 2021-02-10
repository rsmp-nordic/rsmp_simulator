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

            bool bPacketWasProperlyHandled = false;

            try
            {
                RSMP_Messages.AlarmHeaderAndBody AlarmHeader = JSonSerializer.Deserialize<RSMP_Messages.AlarmHeaderAndBody>(sJSon);

                if (AlarmHeader.cat != null && AlarmHeader.cat != "")
                {
                    cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(AlarmHeader.ntsOId, AlarmHeader.cId, bUseCaseSensitiveIds);
                    if (RoadSideObject != null)
                    {
                        foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
                        {
                            if (AlarmObject.sAlarmCodeId.Equals(AlarmHeader.aCId, sc))
                            {
                                cAlarmEvent AlarmEvent = new cAlarmEvent();
                                AlarmEvent.AlarmObject = AlarmObject;
                                AlarmEvent.sDirection = "Received";
                                AlarmEvent.sTimeStamp = UnpackISO8601UTCTimeStamp(AlarmHeader.aTs);
                                AlarmEvent.sMessageId = AlarmHeader.mId;
                                AlarmEvent.sAlarmCodeId = AlarmHeader.aCId;

                                //foreach (cAlarmReturnValue AlarmReturnValue in AlarmHeader.rvs)
                                if (AlarmHeader.rvs != null)
                                {
                                    foreach (RSMP_Messages.AlarmReturnValue AlarmReturnValue in AlarmHeader.rvs)
                                    {
                                        AlarmEvent.AlarmEventReturnValues.Add(new cAlarmEventReturnValue(AlarmReturnValue.n, AlarmReturnValue.v));
                                    }
                                }
                                switch (AlarmHeader.aSp.ToLower())
                                {
                                    case "issue":
                                        AlarmEvent.sEvent = AlarmHeader.aSp + " / " + AlarmHeader.aS;
                                        if (AlarmHeader.aS.Equals("active", StringComparison.OrdinalIgnoreCase))
                                        {
                                            AlarmObject.AlarmCount++;
                                        }
                                        bPacketWasProperlyHandled = true;
                                        break;
                                    case "acknowledge":
                                        AlarmEvent.sEvent = AlarmHeader.aSp + " / " + AlarmHeader.ack;
                                        bPacketWasProperlyHandled = true;
                                        break;
                                    case "suspend":
                                        AlarmEvent.sEvent = AlarmHeader.aSp + " / " + AlarmHeader.sS;
                                        bPacketWasProperlyHandled = true;
                                        break;
                                    default:
                                        AlarmEvent.sEvent = "(unknown: " + AlarmHeader.aSp + ")";
                                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Could not parse correct alarm state {0} (corresponding MsgId {1}) ", AlarmHeader.aSp, AlarmHeader.mId);
                                        break;
                                }

                                if (bPacketWasProperlyHandled)
                                {

                                    AlarmObject.bActive = AlarmHeader.aS.Equals("active", StringComparison.OrdinalIgnoreCase)
                                        ? true : false;
                                    AlarmObject.bAcknowledged = AlarmHeader.ack.Equals("acknowledged", StringComparison.OrdinalIgnoreCase)
                                        ? true : false;
                                    AlarmObject.bSuspended = AlarmHeader.sS.Equals("suspended", StringComparison.OrdinalIgnoreCase)
                                        ? true : false;
                                    if (AlarmObject.bActive == false && AlarmObject.bAcknowledged)
                                    {
                                        AlarmObject.AlarmCount = 0;
                                    }
                                }
                                RSMPGS.MainForm.AddAlarmEventToAlarmObjectAndToList(AlarmObject, AlarmEvent);
                                RSMPGS.MainForm.UpdateAlarmListView(AlarmObject);
                                break;
                            }
                        }
                    }
                    if (bPacketWasProperlyHandled == false)
                    {
                        sError = "Failed to handle Alarm message, could not find object, ntsOId: '" + AlarmHeader.ntsOId + "', cId: '" + AlarmHeader.cId + "', aCId: '" + AlarmHeader.aCId + "'";
                    }
                }
                else
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got alarm message from SCADA, aSp: {0} (corresponding MsgId {1}) ", AlarmHeader.aSp, AlarmHeader.mId);
                }

            }
            catch (Exception e)
            {
                sError = "Failed to deserialize packet: " + e.Message;
                bPacketWasProperlyHandled = false;
            }

            return bPacketWasProperlyHandled;

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

            bool bSuccess = false;

            try
            {
                RSMP_Messages.CommandResponse CommandResponse = JSonSerializer.Deserialize<RSMP_Messages.CommandResponse>(sJSon);

                if (CommandResponse.type.Equals("commandresponse", StringComparison.OrdinalIgnoreCase))
                {

                    cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(CommandResponse.ntsOId, CommandResponse.cId, bUseCaseSensitiveIds);

                    if (RoadSideObject != null)
                    {
                        foreach (RSMP_Messages.CommandResponse_Value Reply in CommandResponse.rvs)
                        {
                            foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
                            {
                                bool bDone = false;
                                foreach (cCommandReturnValue CommandReturnValue in CommandObject.CommandReturnValues)
                                {
                                    if (CommandReturnValue.sName.Equals(Reply.n, sc) &&
                                        CommandObject.sCommandCodeId.Equals(Reply.cCI, sc))
                                    {

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
                                                RSMPGS.SysLog.StoreBase64DebugData(Reply.v);
                                            }
                                            CommandEvent.sValue = "base64";
                                        }
                                        else
                                        {
                                            CommandEvent.sValue = Reply.v;
                                        }

                                        CommandEvent.sAge = Reply.age;

                                        CommandReturnValue.sLastRecValue = Reply.v;
                                        CommandReturnValue.sLastRecAge = Reply.age;

                                        if (ValidateTypeAndRange(CommandReturnValue.Value.GetValueType(), Reply.v))
                                        {
                                            bSuccess = true;
                                        }
                                        else
                                        {
                                            sError = "Value and/or type is out of range or invalid for this RSMP protocol version, type: " + CommandReturnValue.Value.GetValueType() + ", value: " + ((Reply.v.Length < 10) ? Reply.v : Reply.v.Substring(0, 9) + "...");
                                            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
                                        }

                                        if (RSMPGS_Main.bWriteEventsContinous)
                                        {
                                            RSMPGS.SysLog.EventLog("Command;{0}\tMId: {1}\tComponentId: {2}\tCommandCodeId: {3}\tName: {4}\tCommand: {5}\tValue: {6}\t Age: {7}\tEvent: {8}",
                                                    CommandEvent.sTimeStamp, CommandEvent.sMessageId, CommandResponse.cId, CommandEvent.sCommandCodeId,
                                                    CommandEvent.sName, CommandEvent.sCommand, CommandEvent.sValue, CommandEvent.sAge, CommandEvent.sEvent);
                                        }

                                        RoadSideObject.CommandEvents.Add(CommandEvent);
                                        RSMPGS.MainForm.HandleCommandListUpdate(RoadSideObject, CommandResponse.ntsOId, CommandResponse.cId, CommandEvent, false, bUseCaseSensitiveIds);
                                        bDone = true;
                                        break;
                                    }
                                }
                                if (bDone) break;
                            }
                        }
                    }
                }
                else
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got commandrequest message from SCADA, (corresponding MsgId {0}) ", CommandResponse.mId);
                }
            }
            catch (Exception e)
            {
                sError = "Failed to deserialize packet: " + e.Message;
                bSuccess = false;
            }

            return bSuccess;

        }

        private bool DecodeAndParseStatusMessage(RSMP_Messages.Header packetHeader, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
        {

            StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            bool bSuccess = false;

            try
            {
                RSMP_Messages.StatusResponse StatusResponse = JSonSerializer.Deserialize<RSMP_Messages.StatusResponse>(sJSon);

                cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(StatusResponse.ntsOId, StatusResponse.cId, bUseCaseSensitiveIds);

                foreach (RSMP_Messages.Status_VTQ Reply in StatusResponse.sS)
                {
                    cStatusObject StatusObject = RoadSideObject.StatusObjects.Find(x => x.sStatusCodeId.Equals(Reply.sCI, sc));

                    if (StatusObject == null)
                    {
                        continue;
                    }

                    cStatusReturnValue StatusReturnValue = StatusObject.StatusReturnValues.Find(x => x.sName.Equals(Reply.n, sc));

                    if (StatusReturnValue == null)
                    {
                        continue;
                    }

                    if (StatusReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
                    {
                        if (RSMPGS.MainForm.ToolStripMenuItem_StoreBase64Updates.Checked)
                        {
                            StatusReturnValue.Value.SetValue(RSMPGS.SysLog.StoreBase64DebugData(Reply.s));
                        }
                        else
                        {
                            StatusReturnValue.Value.SetValue("base64");
                        }
                    }
                    else
                    {
                        StatusReturnValue.Value.SetValue(Reply.s);
                    }
                    StatusReturnValue.sQuality = Reply.q;

                    if (ValidateTypeAndRange(StatusReturnValue.Value.GetValueType(), Reply.s))
                    {
                        bSuccess = true;
                    }
                    else
                    {
                        string sStatusValue;
                        if (Reply.s == null)
                        {
                            sStatusValue = "(null)";
                        }
                        else
                        {
                            sStatusValue = (Reply.s.Length < 10) ? Reply.s : Reply.s.Substring(0, 9) + "...";
                        }
                        sError = "Value and/or type is out of range or invalid for this RSMP protocol version, type: " + StatusReturnValue.Value.GetValueType() + ", quality: " + StatusReturnValue.sQuality + ", statusvalue: " + sStatusValue;
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, sError);
                    }

                    cStatusEvent StatusEvent = new cStatusEvent();
                    StatusEvent.sTimeStamp = UnpackISO8601UTCTimeStamp(StatusResponse.sTs);
                    StatusEvent.sMessageId = StatusResponse.mId;
                    StatusEvent.sEvent = "Received status";
                    StatusEvent.sStatusCommandId = Reply.sCI;
                    StatusEvent.sName = Reply.n;
                    if (StatusReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
                    {
                        StatusEvent.sStatus = "base64";
                    }
                    else
                    {
                        StatusEvent.sStatus = Reply.s;
                    }
                    StatusEvent.sQuality = Reply.q;
                    if (RSMPGS_Main.bWriteEventsContinous)
                    {
                        RSMPGS.SysLog.EventLog("Status;{0}\tMId: {1}\tComponentId: {2}\tStatusCommandId: {3}\tName: {4}\tStatus: {5}\tQuality: {6}\tUpdateRate: {7}\tUpdateOnChange: {8}\tEvent: {9}",
                            StatusEvent.sTimeStamp, StatusEvent.sMessageId, StatusResponse.cId, StatusEvent.sStatusCommandId,
                            StatusEvent.sName, StatusEvent.sStatus, StatusEvent.sQuality, StatusEvent.sUpdateRate, StatusEvent.bUpdateOnChange, StatusEvent.sEvent);
                    }
                    RoadSideObject.StatusEvents.Add(StatusEvent);
                    RSMPGS.MainForm.HandleStatusListUpdate(RoadSideObject, StatusEvent, false);
                }
            }
            catch (Exception e)
            {
                sError = "Failed to deserialize packet: " + e.Message;
                bSuccess = false;
            }
            return bSuccess;
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
                        if (CommandReturnValue.Value.GetValue().Contains("\\"))
                        {
                            try
                            {
                                byte[] Base64Bytes = null;
                                // Open file for reading 
                                System.IO.FileStream fsBase64 = new System.IO.FileStream(CommandReturnValue.Value.GetValue(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                System.IO.BinaryReader brBase64 = new System.IO.BinaryReader(fsBase64);
                                long lBytes = new System.IO.FileInfo(CommandReturnValue.Value.GetValue()).Length;
                                Base64Bytes = brBase64.ReadBytes((Int32)lBytes);
                                fsBase64.Close();
                                fsBase64.Dispose();
                                brBase64.Close();
                                CommandRequest_Value.v = Convert.ToBase64String(Base64Bytes);
                                if (CommandRequest_Value.v.Length > (cTcpSocketClientThread.BUFLENGTH - 100))
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
                    CommandEvent.sValue = CommandReturnValue.Value.GetValue();
                    RoadSideObject.CommandEvents.Add(CommandEvent);
                    RSMPGS.MainForm.HandleCommandListUpdate(RoadSideObject, CommandRequest.ntsOId, CommandRequest.cId, CommandEvent, true, bUseCaseSensitiveIds);

                    if (RSMPGS_Main.bWriteEventsContinous)
                    {
                        RSMPGS.SysLog.EventLog("Command;{0}\tMId: {1}\tComponentId: {2}\tCommandCodeId: {3}\tName: {4}\tCommand: {5}\tValue: {6}\t Age: {7}\tEvent: {8}",
                        CommandEvent.sTimeStamp, CommandEvent.sMessageId, CommandRequest.cId, CommandEvent.sCommandCodeId,
                        CommandEvent.sName, CommandEvent.sCommand, CommandEvent.sValue, CommandEvent.sAge, CommandEvent.sEvent);
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
                    StatusEvent.sStatusCommandId = StatusRequest_Status.sCI;
                    StatusEvent.sName = StatusRequest_Status.n;
                    if (statusType.ToLower() == "statusunsubscribe")
                    {
                        StatusEvent.sEvent = "Sent unsubscription";
                        //StatusReturnValue.sLastUpdateRate = null;
                    }
                    else
                    {
                        StatusEvent.sEvent = "Sent status request";
                    }

                    if (RSMPGS_Main.bWriteEventsContinous)
                    {
                        RSMPGS.SysLog.EventLog("Status;{0}\tMId: {1}\tComponentId: {2}\tStatusCommandId: {3}\tName: {4}\tStatus: {5}\tQuality: {6}\tUpdateRate: {7}\tEvent: {8}",
                            StatusEvent.sTimeStamp, StatusEvent.sMessageId, StatusRequest.cId, StatusEvent.sStatusCommandId,
                            StatusEvent.sName, StatusEvent.sStatus, StatusEvent.sQuality, StatusEvent.sUpdateRate, StatusEvent.sEvent);
                    }

                    RoadSideObject.StatusEvents.Add(StatusEvent);
                    RSMPGS.MainForm.HandleStatusListUpdate(RoadSideObject, StatusEvent, true);
                }

                sSendBuffer = JSonSerializer.SerializeObject(StatusRequest);

                RSMPGS.JSon.SendJSonPacket(StatusRequest.type, StatusRequest.mId, sSendBuffer, true);
                if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent status message, ComponentId: " + StatusRequest.cId + " , MsgId: " + StatusRequest.mId);
                }
            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create status message: {0}", e.Message);
            }
        }

        public void CreateAndSendSubscriptionMessage(cRoadSideObject RoadSideObject, List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4> StatusSubscribeValues)
        {
            if (NegotiatedRSMPVersion > RSMPVersion.RSMP_3_1_4)
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
            StatusEvent.sStatusCommandId = StatusSubscribe_Status.sCI;
            StatusEvent.sName = StatusSubscribe_Status.n;
            StatusEvent.sUpdateRate = StatusSubscribe_Status.uRt;
            StatusEvent.bUpdateOnChange = StatusSubscribe_Status.sOc;

            if (RSMPGS_Main.bWriteEventsContinous)
            {
                RSMPGS.SysLog.EventLog("Status;{0}\tMId: {1}\tComponentId: {2}\tStatusCommandId: {3}\tName: {4}\tStatus: {5}\tQuality: {6}\tUpdateRate: {7}\tEvent: {8}",
                    StatusEvent.sTimeStamp, StatusEvent.sMessageId, sComponentId, StatusEvent.sStatusCommandId,
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
