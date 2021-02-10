using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace nsRSMPGS
{

    //
    // JSon decoding and encoding
    //
    // All decoding is running delegated, hence it is safe to use all listviews and stuff
    // here...
    //

    public class cJSonGS1 : cJSon
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

                        case "commandrequest":
                            bSuccess = DecodeAndParseCommandMessage(Header, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
                            break;

                        case "statusrequest":
                            bSuccess = DecodeAndParseStatusMessage(Header, StatusMsgType.Request, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
                            break;

                        case "statussubscribe":
                            bSuccess = DecodeAndParseStatusMessage(Header, StatusMsgType.Subscribe, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
                            break;

                        case "statusunsubscribe":
                            bSuccess = DecodeAndParseStatusMessage(Header, StatusMsgType.UnSubscribe, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
                            break;

                        case "aggregatedstatusrequest":
                            bSuccess = DecodeAndParseAggregatedStatusRequestMessage(Header, sJSon, bUseStrictProtocolAnalysis, bUseCaseSensitiveIds, ref bHasSentAckOrNack, ref sError);
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

        public RSMP_Messages.AlarmHeaderAndBody CreateAndSendAlarmMessage(cAlarmObject AlarmObject, AlarmSpecialisation alarmSpecialisation)
        {

            cAlarmEvent AlarmEvent;

            return CreateAndSendAlarmMessage(AlarmObject, alarmSpecialisation, out AlarmEvent);

        }

        public RSMP_Messages.AlarmHeaderAndBody CreateAndSendAlarmMessage(cAlarmObject AlarmObject, AlarmSpecialisation alarmSpecialisation, bool bCreateMessageOnly, out string sSendBuffer)
        {

            cAlarmEvent AlarmEvent;

            return CreateAndSendAlarmMessage(AlarmObject, alarmSpecialisation, bCreateMessageOnly, out sSendBuffer, out AlarmEvent);

        }

        public RSMP_Messages.AlarmHeaderAndBody CreateAndSendAlarmMessage(cAlarmObject AlarmObject, AlarmSpecialisation alarmSpecialisation, out cAlarmEvent AlarmEvent)
        {
            string sSendBuffer;

            return CreateAndSendAlarmMessage(AlarmObject, alarmSpecialisation, false, out sSendBuffer, out AlarmEvent);
        }

        public RSMP_Messages.AlarmHeaderAndBody CreateAndSendAlarmMessage(cAlarmObject AlarmObject, AlarmSpecialisation alarmSpecialisation, bool bCreateMessageOnly, out string sSendBuffer, out cAlarmEvent AlarmEvent)
        {

            sSendBuffer = "";

            RSMP_Messages.AlarmHeaderAndBody AlarmHeaderAndBody;
            //      RSMP_Messages.AlarmHeader AlarmHeader;


            AlarmHeaderAndBody = new RSMP_Messages.AlarmHeaderAndBody();

            AlarmEvent = new cAlarmEvent();

            try
            {

                AlarmHeaderAndBody.mType = "rSMsg";
                AlarmHeaderAndBody.type = "Alarm";
                AlarmHeaderAndBody.mId = System.Guid.NewGuid().ToString();
                AlarmHeaderAndBody.ntsOId = AlarmObject.RoadSideObject.sNTSObjectId;
                AlarmHeaderAndBody.xNId = AlarmObject.RoadSideObject.sExternalNTSId;
                AlarmHeaderAndBody.cId = AlarmObject.RoadSideObject.sComponentId;
                AlarmHeaderAndBody.aCId = AlarmObject.sAlarmCodeId;
                AlarmHeaderAndBody.xACId = AlarmObject.sExternalAlarmCodeId;
                AlarmHeaderAndBody.xNACId = AlarmObject.sExternalNTSAlarmCodeId;
                AlarmHeaderAndBody.rvs = new List<RSMP_Messages.AlarmReturnValue>();


                AlarmEvent.AlarmObject = AlarmObject;

                AlarmHeaderAndBody.ack = AlarmObject.bAcknowledged ? "Acknowledged" : "notAcknowledged";
                AlarmHeaderAndBody.aS = AlarmObject.bActive ? "Active" : "inActive";
                AlarmHeaderAndBody.sS = AlarmObject.bSuspended ? "Suspended" : "notSuspended";

                switch (alarmSpecialisation)
                {
                    case AlarmSpecialisation.Issue:
                        AlarmHeaderAndBody.aSp = "Issue";
                        AlarmEvent.sEvent = AlarmHeaderAndBody.aSp + " / " + AlarmHeaderAndBody.aS;
                        break;
                    case AlarmSpecialisation.Acknowledge:
                        AlarmHeaderAndBody.aSp = "Acknowledge";
                        AlarmEvent.sEvent = AlarmHeaderAndBody.aSp + " / " + AlarmHeaderAndBody.ack;
                        break;
                    case AlarmSpecialisation.Suspend:
                        AlarmHeaderAndBody.aSp = "Suspend";
                        AlarmEvent.sEvent = AlarmHeaderAndBody.aSp + " / " + AlarmHeaderAndBody.sS;
                        break;
                }

                if (AlarmObject.bActive == false && AlarmObject.bAcknowledged)
                {
                    AlarmObject.AlarmCount = 0;
                }

                if (AlarmObject.dtLastChangedAlarmStatus == DateTime.MinValue)
                {
                    AlarmObject.dtLastChangedAlarmStatus = DateTime.Now;
                }

                AlarmHeaderAndBody.aTs = CreateISO8601UTCTimeStamp(AlarmObject.dtLastChangedAlarmStatus);
                AlarmHeaderAndBody.cat = AlarmObject.sCategory;
                AlarmHeaderAndBody.pri = AlarmObject.sPriority;

                AlarmEvent.sAlarmCodeId = AlarmObject.sAlarmCodeId;
                AlarmEvent.sDirection = "Sent";
                AlarmEvent.sTimeStamp = UnpackISO8601UTCTimeStamp(AlarmHeaderAndBody.aTs); // String.Format("{0:yyyy-MM-dd}T{0:HH:mm:ss.fff}", UnpackISO8601UTCTimeStamp(AlarmHeaderAndBody.aTs));
                AlarmEvent.sMessageId = AlarmHeaderAndBody.mId;

                foreach (cAlarmReturnValue AlarmReturnValue in AlarmObject.AlarmReturnValues)
                {
                    RSMP_Messages.AlarmReturnValue rv = new RSMP_Messages.AlarmReturnValue();
                    rv.n = AlarmReturnValue.sName;
                    rv.v = AlarmReturnValue.Value.GetValue();
                    AlarmHeaderAndBody.rvs.Add(rv);
                    AlarmEvent.AlarmEventReturnValues.Add(new nsRSMPGS.cAlarmEventReturnValue(rv.n, rv.v));
                }

                sSendBuffer = JSonSerializer.SerializeObject(AlarmHeaderAndBody);

                if (bCreateMessageOnly)
                {
                    return AlarmHeaderAndBody;
                }

                if (RSMPGS.JSon.SendJSonPacket(AlarmHeaderAndBody.type, AlarmHeaderAndBody.mId, sSendBuffer, true))
                {
                    if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                    {
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent alarm message, AlarmCode: " + AlarmObject.sAlarmCodeId + ", Type: " + AlarmHeaderAndBody.type + "/" + AlarmHeaderAndBody.aSp + "/" + AlarmHeaderAndBody.aS + ", MsgId: " + AlarmHeaderAndBody.mId);//, SequenceNumber: " + AlarmHeaderAndBody.sNr);
                    }
                }
                else
                {
                    if (cHelper.IsSettingChecked("BufferAndSendAlarmsWhenConnect"))
                    {
                        cBufferedMessage BufferedMessage = new cBufferedMessage(cBufferedMessage.eMessageType.Alarm, AlarmHeaderAndBody.type, AlarmHeaderAndBody.mId, sSendBuffer);
                        RSMPGS.MainForm.AddBufferedMessageToListAndListView(BufferedMessage);
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Buffered alarm message, AlarmCode: " + AlarmObject.sAlarmCodeId + ", Type: " + AlarmHeaderAndBody.type + "/" + AlarmHeaderAndBody.aSp + "/" + AlarmHeaderAndBody.aS + ", MsgId: " + AlarmHeaderAndBody.mId);// + ", SequenceNumber: " + AlarmHeaderAndBody.sNr);
                    }
                }

                //        RSMPGS.ProcessImage.SequenceNumber_Alarm++;

            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create alarm message: {0}", e.Message);
                AlarmEvent = null;
            }

            return AlarmHeaderAndBody;

        }


        public RSMP_Messages.AggregatedStatus CreateAndSendAggregatedStatusMessage(cRoadSideObject RoadSideObject)
        {
            string sSendBuffer;

            return CreateAndSendAggregatedStatusMessage(RoadSideObject, false, out sSendBuffer);
        }

        public RSMP_Messages.AggregatedStatus CreateAndSendAggregatedStatusMessage(cRoadSideObject RoadSideObject, bool bCreateMessageOnly, out string sSendBuffer)
        {

            sSendBuffer = "";

            RSMP_Messages.AggregatedStatus AggregatedStatusMessage = new RSMP_Messages.AggregatedStatus();

            AggregatedStatusMessage.mType = "rSMsg";
            AggregatedStatusMessage.type = "AggregatedStatus";
            AggregatedStatusMessage.mId = System.Guid.NewGuid().ToString();
            AggregatedStatusMessage.ntsOId = RoadSideObject.sNTSObjectId;
            AggregatedStatusMessage.xNId = RoadSideObject.sExternalNTSId;
            AggregatedStatusMessage.cId = RoadSideObject.sComponentId;

            if (RoadSideObject.dtLastChangedAggregatedStatus == DateTime.MinValue)
            {
                RoadSideObject.dtLastChangedAggregatedStatus = DateTime.Now;
            }

            AggregatedStatusMessage.aSTS = CreateISO8601UTCTimeStamp(RoadSideObject.dtLastChangedAggregatedStatus);

            AggregatedStatusMessage.fP = RoadSideObject.sFunctionalPosition.Length > 0 ? RoadSideObject.sFunctionalPosition : null;
            AggregatedStatusMessage.fS = RoadSideObject.sFunctionalState.Length > 0 ? RoadSideObject.sFunctionalState : null;

            AggregatedStatusMessage.se = (bool[])RoadSideObject.bBitStatus.Clone();

            for (int iIndex = 0; iIndex < RoadSideObject.bBitStatus.GetLength(0); iIndex++)
            {
                AggregatedStatusMessage.se[iIndex] = RoadSideObject.bBitStatus[iIndex];
            }

            sSendBuffer = JSonSerializer.SerializeObject(AggregatedStatusMessage);

            if (bCreateMessageOnly)
            {
                return AggregatedStatusMessage;
            }

            if (RSMPGS.JSon.SendJSonPacket(AggregatedStatusMessage.type, AggregatedStatusMessage.mId, sSendBuffer, true))
            {
                if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent aggregated status message, ntsOId: " + AggregatedStatusMessage.ntsOId + ", Type: " + AggregatedStatusMessage.type + ", MsgId: " + AggregatedStatusMessage.mId);
                }
            }
            else
            {
                if (cHelper.IsSettingChecked("BufferAndSendAggregatedStatusWhenConnect"))
                {

                    cBufferedMessage BufferedMessage = new cBufferedMessage(cBufferedMessage.eMessageType.AggregatedStatus, AggregatedStatusMessage.type, AggregatedStatusMessage.mId, sSendBuffer);
                    RSMPGS.MainForm.AddBufferedMessageToListAndListView(BufferedMessage);
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Buffered aggregated status message, ntsOId: " + AggregatedStatusMessage.ntsOId + ", Type: " + AggregatedStatusMessage.type + ", MsgId: " + AggregatedStatusMessage.mId);
                }
            }

            return AggregatedStatusMessage;

        }

        private bool DecodeAndParseAggregatedStatusRequestMessage(RSMP_Messages.Header packetHeader, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
        {

            bool bPacketWasProperlyHandled = false;

            StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            try
            {

                RSMP_Messages.AggregatedStatusRequest AggregatedStatusRequest = JSonSerializer.Deserialize<RSMP_Messages.AggregatedStatusRequest>(sJSon);

                cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(AggregatedStatusRequest.ntsOId, AggregatedStatusRequest.cId, bUseStrictProtocolAnalysis);

                if (RoadSideObject != null)
                {
                    if (cHelper.IsSettingChecked("AllowRequestsOfAlarmsAndAggStatus"))
                    {
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Request of aggregated status, ntsOId: {0}, cId: {1}", AggregatedStatusRequest.ntsOId, AggregatedStatusRequest.cId);
                        bHasSentAckOrNack = SendPacketAck(true, packetHeader.mId, "");
                        CreateAndSendAggregatedStatusMessage(RoadSideObject);
                    }
                    else
                    {
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Request of aggregated status, ntsOId: {0}, cId: {1}. RSMP settings prevent us from answering.", AggregatedStatusRequest.ntsOId, AggregatedStatusRequest.cId);
                        bHasSentAckOrNack = SendPacketAck(false, packetHeader.mId, "Invalid packet type: " + packetHeader.type);
                    }
                    bPacketWasProperlyHandled = true;
                }
                else
                {
                    sError = "Failed to handle AggregatedStatusRequest message, ntsOId: " + AggregatedStatusRequest.ntsOId + "/ cId: " + AggregatedStatusRequest.cId + " could not be found)";
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "{0}", sError);
                }
            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to deserialize packet: {0}", e.Message);
            }

            return bPacketWasProperlyHandled;

        }

        private bool DecodeAndParseAlarmMessage(RSMP_Messages.Header packetHeader, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
        {

            StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            bool bPacketWasProperlyHandled = false;

            try
            {
                RSMP_Messages.AlarmHeader AlarmHeader = JSonSerializer.Deserialize<RSMP_Messages.AlarmHeader>(sJSon);

                cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(AlarmHeader.ntsOId, AlarmHeader.cId, bUseStrictProtocolAnalysis);

                if (RoadSideObject != null)
                {

                    foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
                    {

                        if (AlarmObject.sAlarmCodeId.Equals(AlarmHeader.aCId, sc))
                        {

                            cAlarmEvent AlarmEvent = new cAlarmEvent();
                            AlarmEvent.AlarmObject = AlarmObject;
                            AlarmEvent.sDirection = "Received";
                            AlarmEvent.sTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            AlarmEvent.sMessageId = AlarmHeader.mId;
                            AlarmEvent.sAlarmCodeId = AlarmHeader.aCId;
                            AlarmEvent.sEvent = AlarmHeader.aSp;

                            AlarmSpecialisation alarmSpecialisation = cJSon.AlarmSpecialisation.Unknown;

                            switch (AlarmHeader.aSp.ToLower())
                            {
                                case "acknowledge":
                                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Ack of alarm, AlarmCodeId: {0}", AlarmHeader.aCId);
                                    AlarmObject.bAcknowledged = true;
                                    alarmSpecialisation = cJSon.AlarmSpecialisation.Acknowledge;
                                    break;
                                case "suspend":
                                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Suspend of alarm, AlarmCodeId: {0}", AlarmHeader.aCId);
                                    AlarmObject.bSuspended = true;
                                    alarmSpecialisation = cJSon.AlarmSpecialisation.Suspend;
                                    break;
                                case "resume":
                                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Resume of alarm, AlarmCodeId: {0}", AlarmHeader.aCId);
                                    AlarmObject.bSuspended = false;
                                    alarmSpecialisation = cJSon.AlarmSpecialisation.Suspend;
                                    break;
                                case "request":
                                    if (cHelper.IsSettingChecked("AllowRequestsOfAlarmsAndAggStatus"))
                                    {
                                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Request of alarm status, AlarmCodeId: {0}", AlarmHeader.aCId);
                                        alarmSpecialisation = cJSon.AlarmSpecialisation.Issue;
                                    }
                                    else
                                    {
                                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Request of alarm status, AlarmCodeId: {0}. RSMP settings prevent us from answering.", AlarmHeader.aCId);
                                    }
                                    break;

                            }

                            if (bHasSentAckOrNack == false)
                            {
                                if (alarmSpecialisation == AlarmSpecialisation.Unknown)
                                {
                                    bHasSentAckOrNack = SendPacketAck(false, packetHeader.mId, "Unknown Alarm Specialisation: " + AlarmHeader.aSp);
                                }
                                else
                                {
                                    bHasSentAckOrNack = SendPacketAck(true, packetHeader.mId, "");
                                }
                            }

                            bPacketWasProperlyHandled = true;

                            if (alarmSpecialisation != AlarmSpecialisation.Unknown)
                            {

                                if (AlarmObject.bActive == false && AlarmObject.bAcknowledged && alarmSpecialisation != AlarmSpecialisation.Unknown)
                                {
                                    AlarmObject.AlarmCount = 0;
                                }

                                if (AlarmHeader.aSp.ToLower() != "request")
                                {
                                    AlarmObject.dtLastChangedAlarmStatus = DateTime.Now;
                                }

                                CreateAndSendAlarmMessage(AlarmObject, alarmSpecialisation);

                                RSMPGS.MainForm.AddAlarmEventToAlarmObjectAndToList(AlarmObject, AlarmEvent);

                                RSMPGS.MainForm.UpdateAlarmListView(AlarmObject);
                            }

                        }

                    }
                }
                if (bPacketWasProperlyHandled == false)
                {
                    sError = "Failed to handle alarm message, AlarmCodeId " + AlarmHeader.aCId + " could not be found)";
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "{0}", sError);
                }
            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to deserialize packet: {0}", e.Message);
            }

            return bPacketWasProperlyHandled;

        }

        private bool DecodeAndParseCommandMessage(RSMP_Messages.Header packetHeader, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
        {

            StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            bool bSuccess = false;

            // Values to return
            List<RSMP_Messages.CommandResponse_Value> rvs = new List<RSMP_Messages.CommandResponse_Value>();

            //Dictionary<cRoadSideObject, cRoadSideObject> UpdatedRoadSideObjects = new Dictionary<cRoadSideObject, cRoadSideObject>();

            try
            {
                RSMP_Messages.CommandRequest CommandRequest = JSonSerializer.Deserialize<RSMP_Messages.CommandRequest>(sJSon);

                // Response message
                RSMP_Messages.CommandResponse CommandResponse = new RSMP_Messages.CommandResponse();

                bool bSomeValueWasBad = false;

                // Scan through each value to set
                foreach (RSMP_Messages.CommandRequest_Value CommandRequest_Value in CommandRequest.arg)
                {
                    // Create return value for each value to be set
                    RSMP_Messages.CommandResponse_Value rv = new RSMP_Messages.CommandResponse_Value();
                    rv.v = null;
                    rv.n = CommandRequest_Value.n;
                    rv.age = "undefined";

                    bool bFoundCommand = false;

                    cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(CommandRequest.ntsOId, CommandRequest.cId, bUseStrictProtocolAnalysis);

                    if (RoadSideObject != null)
                    {

                        // Find command in object
                        foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
                        {
                            bool bDone = false;
                            // Find command name in command
                            foreach (cCommandReturnValue CommandReturnValue in CommandObject.CommandReturnValues)
                            {
                                if (CommandReturnValue.sName.Equals(CommandRequest_Value.n, sc) &&
                                  CommandReturnValue.sCommand.Equals(CommandRequest_Value.cO, sc))
                                {
                                    // Do some validation
                                    if (ValidateTypeAndRange(CommandReturnValue.Value.GetValueType(), CommandRequest_Value.v))
                                    {
                                        if (CommandReturnValue.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (RSMPGS.MainForm.ToolStripMenuItem_StoreBase64Updates.Checked)
                                            {
                                                CommandReturnValue.Value.SetValue(RSMPGS.SysLog.StoreBase64DebugData(CommandRequest_Value.v));
                                            }
                                        }
                                        else
                                        {
                                            CommandReturnValue.Value.SetValue(CommandRequest_Value.v);
                                        }
                                        rv.v = CommandRequest_Value.v;
                                        rv.cCI = CommandRequest_Value.cCI;
                                        rv.age = "recent";
                                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got Command, updated NTSObjectId: {0}, ComponentId: {1}, CommandCodeId: {2}, Name: {3}, Command: {4}, Value: {5}", CommandRequest.ntsOId, CommandRequest.cId, CommandRequest_Value.cCI, CommandRequest_Value.n, CommandRequest_Value.cO, CommandRequest_Value.v);
                                        RSMPGS.MainForm.HandleCommandListUpdate(RoadSideObject, CommandObject, CommandReturnValue);
                                    }
                                    else
                                    {
                                        rv.v = null;
                                        rv.cCI = CommandRequest_Value.cCI;
                                        rv.age = "unknown";
                                        sError = "Value and/or type is out of range or invalid for this RSMP protocol version, type: " + CommandReturnValue.Value.GetValueType() + ", value: " + ((CommandRequest_Value.v.Length < 10) ? CommandRequest_Value.v : CommandRequest_Value.v.Substring(0, 9) + "...");
                                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "{0}", sError);
                                        bSomeValueWasBad = true;
                                    }
                                    /*
                                    // Found at least one value
                                    if (UpdatedRoadSideObjects.ContainsKey(RoadSideObject) == false)
                                    {
                                      UpdatedRoadSideObjects.Add(RoadSideObject, RoadSideObject);
                                    }
                                    */
                                    bDone = true;
                                    bFoundCommand = true;
                                    break;
                                }
                            }
                            if (bDone) break;

                        }

                    }
                    rvs.Add(rv);
                    if (bFoundCommand == false)
                    {
                        sError = "Got Command, failed to find object/command/name (NTSObjectId: " + CommandRequest.ntsOId + ", ComponentId: " + CommandRequest.cId + ", CommandCodeId: " + CommandRequest_Value.cCI + ", Name: " + CommandRequest_Value.n + ", Command: " + CommandRequest_Value.cO + ", Value: " + CommandRequest_Value.v + ")";
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "{0}", sError);
                        bSomeValueWasBad = true;
                    }
                }

                //cRoadSideObject UpdatedRoadSideObject = null;
                /*
                foreach (cRoadSideObject UpdatedRoadSideObject in UpdatedRoadSideObjects.Values)
                {
                  RSMPGS.MainForm.HandleCommandListUpdate(UpdatedRoadSideObject);
                }
                */

                bSuccess = bSomeValueWasBad == false ? true : false;

                // Send response to client
                CommandResponse.mType = "rSMsg";
                CommandResponse.type = "CommandResponse";
                CommandResponse.mId = System.Guid.NewGuid().ToString();
                CommandResponse.ntsOId = CommandRequest.ntsOId;
                CommandResponse.xNId = CommandRequest.xNId;
                CommandResponse.cId = CommandRequest.cId;
                //CommandResponse.cCI = CommandRequest.cCI;
                CommandResponse.cTS = CreateISO8601UTCTimeStamp();
                CommandResponse.rvs = rvs;

                if (bHasSentAckOrNack == false)
                {
                    bHasSentAckOrNack = SendPacketAck(bSuccess, packetHeader.mId, "");
                }

                string sSendBuffer = JSonSerializer.SerializeObject(CommandResponse);
                RSMPGS.JSon.SendJSonPacket(CommandResponse.type, CommandResponse.mId, sSendBuffer, true);

                if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent CommandResponse message, Type: " + CommandResponse.type + ", MsgId: " + CommandResponse.mId);
                }
            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to deserialize packet: {0}", e.Message);
            }

            return bSuccess;

        }

        private bool DecodeAndParseStatusMessage(RSMP_Messages.Header packetHeader, StatusMsgType statusMsgType, string sJSon, bool bUseStrictProtocolAnalysis, bool bUseCaseSensitiveIds, ref bool bHasSentAckOrNack, ref string sError)
        {

            StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            bool bSuccess = true;

            // Values to return
            List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();

            try
            {

                // StatusSubscribe, StatusUnsubscribe and StatusRequest are very much alike, differns by the uRt property only
                RSMP_Messages.StatusSubscribe_Over_3_1_4 StatusSubscribe = JSonSerializer.Deserialize<RSMP_Messages.StatusSubscribe_Over_3_1_4>(sJSon);

                foreach (RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscribe_Status in StatusSubscribe.sS)
                {
                    if (StatusSubscribe_Status.sCI == null)
                    {
                        sError = "StatusCode Id (sCI) in " + packetHeader.type + " is missing";
                        return false;
                    }
                }

                cRoadSideObject RoadSideObject = cHelper.FindRoadSideObject(StatusSubscribe.ntsOId, StatusSubscribe.cId, bUseCaseSensitiveIds);

                if (RoadSideObject != null)
                {
                    foreach (RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscribe_Status in StatusSubscribe.sS)
                    {
                        RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                        s.sCI = StatusSubscribe_Status.sCI;
                        s.n = StatusSubscribe_Status.n;
                        s.s = null;
                        // 3.1.1 = unknown
                        //s.q = "unknown";
                        // 3.1.2 = undefined ??
                        s.q = "undefined";
                        // Find status in object
                        cStatusObject StatusObject = RoadSideObject.StatusObjects.Find(x => x.sStatusCodeId.Equals(StatusSubscribe_Status.sCI, sc));
                        cStatusReturnValue StatusReturnValue = null;
                        if (StatusObject != null)
                        {
                            StatusReturnValue = StatusObject.StatusReturnValues.Find(x => x.sName.Equals(StatusSubscribe_Status.n, sc));
                        }
                        if (StatusReturnValue != null)
                        {
                            RSMPGS.ProcessImage.UpdateStatusValue(ref s, StatusReturnValue.Value.GetValueType(), StatusReturnValue.Value.GetValue());
                            switch (statusMsgType)
                            {
                                case StatusMsgType.Request:
                                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got status request (NTSObjectId: {0}, ComponentId: {1}, StatusCodeId: {2}, Name: {3}, Status: {4})", StatusSubscribe.ntsOId, StatusSubscribe.cId, StatusObject.sStatusCodeId, StatusReturnValue.sName, StatusReturnValue.Value.GetValue());
                                    break;
                                case StatusMsgType.UnSubscribe:
                                case StatusMsgType.Subscribe:
                                    // Delete subscription if it already exists
                                    foreach (cSubscription Subscription in RoadSideObject.Subscriptions)
                                    {
                                        if (Subscription.StatusReturnValue == StatusReturnValue)
                                        {
                                            RoadSideObject.Subscriptions.Remove(Subscription);
                                            break;
                                        }
                                    }
                                    if (statusMsgType == StatusMsgType.Subscribe)
                                    {
                                        string sUpdateRate = StatusSubscribe_Status.uRt;
                                        float fUpdateRate = 0;
                                        float.TryParse(StatusSubscribe_Status.uRt, out fUpdateRate);
                                        if (fUpdateRate == 0)
                                        {
                                            float.TryParse(StatusSubscribe_Status.uRt.Replace('.', ','), out fUpdateRate);
                                        }
                                        bool bAlwaysSendOnChange = StatusSubscribe_Status.sOc;
                                        RoadSideObject.Subscriptions.Add(new cSubscription(StatusObject, StatusReturnValue, fUpdateRate, bAlwaysSendOnChange));
                                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got status subscribe (NTSObjectId: {0}, ComponentId: {1}. StatusCodeId: {2}, Name: {3}, Status: {4})", StatusSubscribe.ntsOId, StatusSubscribe.cId, StatusObject.sStatusCodeId, StatusReturnValue.sName, StatusReturnValue.Value.GetValue());
                                    }
                                    else
                                    {
                                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Got status unsubscribe, removed subscription (NTSObjectId: {0}, ComponentId: {1}. StatusCodeId: {2}, Name: {3}, Status: {4})", StatusSubscribe.ntsOId, StatusSubscribe.cId, StatusObject.sStatusCodeId, StatusReturnValue.sName, StatusReturnValue.Value.GetValue());
                                    }
                                    break;
                            }
                        }
                        if (s.s == null)
                        {
                            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Got status request/subscribe, failed to update StatusCodeId or Object (could be unknown value) (NTSObjectId: {0}, ComponentId: {1}, StatusCodeId: {2}))", StatusSubscribe.ntsOId, StatusSubscribe.cId, StatusSubscribe_Status.sCI);
                        }

                        sS.Add(s);
                    }
                }
                else
                {
                    // Failed, fill return list with 'unknown'
                    foreach (RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 StatusSubscribe_Status in StatusSubscribe.sS)
                    {
                        RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                        s.sCI = StatusSubscribe_Status.sCI;
                        s.n = StatusSubscribe_Status.n;
                        s.s = null;
                        // 3.1.1 = unknown
                        //s.q = "unknown";
                        // 3.1.2 = undefined ??
                        s.q = "undefined";
                        sS.Add(s);
                    }
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Got status message, failed to find object (NTSObjectId: {0}, ComponentId: {1})", StatusSubscribe.ntsOId, StatusSubscribe.cId);
                }

                if (statusMsgType != StatusMsgType.UnSubscribe)
                {
                    // Response message
                    RSMP_Messages.StatusResponse StatusResponse = new RSMP_Messages.StatusResponse();
                    // Send response to client
                    StatusResponse.mType = "rSMsg";
                    StatusResponse.type = (statusMsgType == StatusMsgType.Subscribe) ? "StatusUpdate" : "StatusResponse";
                    StatusResponse.mId = System.Guid.NewGuid().ToString();
                    StatusResponse.ntsOId = StatusSubscribe.ntsOId;
                    StatusResponse.xNId = StatusSubscribe.xNId;
                    StatusResponse.cId = StatusSubscribe.cId;
                    StatusResponse.sTs = CreateISO8601UTCTimeStamp();
                    StatusResponse.sS = sS;
                    string sSendBuffer = JSonSerializer.SerializeObject(StatusResponse);
                    if (bHasSentAckOrNack == false)
                    {
                        bHasSentAckOrNack = SendPacketAck(true, packetHeader.mId, "");
                    }
                    RSMPGS.JSon.SendJSonPacket(StatusResponse.type, StatusResponse.mId, sSendBuffer, true);
                    if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                    {
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent StatusResponse message, Type: " + StatusResponse.type + ", MsgId: " + StatusResponse.mId);
                    }
                }
            }
            catch (Exception e)
            {
                sError = "Failed to deserialize packet: " + e.Message;
                bSuccess = false;
            }

            return bSuccess;
        }


        public RSMP_Messages.StatusUpdate CreateAndSendStatusUpdateMessage(cRoadSideObject RoadSideObject, List<RSMP_Messages.Status_VTQ> sS)
        {
            string sSendBuffer;

            return CreateAndSendStatusUpdateMessage(RoadSideObject, sS, false, out sSendBuffer);
        }


        public RSMP_Messages.StatusUpdate CreateAndSendStatusUpdateMessage(cRoadSideObject RoadSideObject, List<RSMP_Messages.Status_VTQ> sS, bool bCreateMessageOnly, out string sSendBuffer)
        {

            sSendBuffer = "";

            RSMP_Messages.StatusUpdate StatusUpdateMessage = new RSMP_Messages.StatusUpdate();

            StatusUpdateMessage.mType = "rSMsg";
            StatusUpdateMessage.type = "StatusUpdate";
            StatusUpdateMessage.mId = System.Guid.NewGuid().ToString();

            StatusUpdateMessage.ntsOId = RoadSideObject.sNTSObjectId;
            StatusUpdateMessage.xNId = RoadSideObject.sExternalNTSId;
            StatusUpdateMessage.cId = RoadSideObject.sComponentId;

            StatusUpdateMessage.sTs = CreateISO8601UTCTimeStamp();

            StatusUpdateMessage.sS = sS;

            sSendBuffer = JSonSerializer.SerializeObject(StatusUpdateMessage);

            if (bCreateMessageOnly)
            {
                return StatusUpdateMessage;
            }

            if (RSMPGS.JSon.SendJSonPacket(StatusUpdateMessage.type, StatusUpdateMessage.mId, sSendBuffer, true))
            {
                if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent Status Update message, Type: " + StatusUpdateMessage.type + ", MsgId: " + StatusUpdateMessage.mId);
                }
            }
            else
            {
                if (cHelper.IsSettingChecked("BufferAndSendStatusUpdatesWhenConnect"))
                {

                    cBufferedMessage BufferedMessage = new cBufferedMessage(cBufferedMessage.eMessageType.Status, StatusUpdateMessage.type, StatusUpdateMessage.mId, sSendBuffer);
                    RSMPGS.MainForm.AddBufferedMessageToListAndListView(BufferedMessage);
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Buffered Status Update message, Type: " + StatusUpdateMessage.type + ", MsgId: " + StatusUpdateMessage.mId);
                }
            }

            return StatusUpdateMessage;

        }

        public override void SocketWasConnected()
        {

            base.SocketWasConnected();

        }

        public override void SocketWasClosed()
        {

            if (cHelper.IsSettingChecked("ClearSubscriptionsAtDisconnect"))
            {

                foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
                {
                    RoadSideObject.Subscriptions.Clear();
                }

            }

            // Call this last as it will cause NegotiatedRSMPVersion = RSMPVersion.NotSupported, hence
            // we will not find last protocol to locate above settings
            base.SocketWasClosed();

        }

        public override void SocketIsConnectedAndInitSequenceIsNegotiated()
        {

            base.SocketIsConnectedAndInitSequenceIsNegotiated();

            // Should probably be delgated to main thread or we should have made some lock
            // for ProcessImage
            if (cHelper.IsSettingChecked("SendAggregatedStatusAtConnect"))
            {
                foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
                {
                    if (RoadSideObject.bIsComponentGroup)
                    {
                        RSMPGS.JSon.CreateAndSendAggregatedStatusMessage(RoadSideObject);
                    }
                }
            }

            // May only send active and suspended stuff if we don't have any buffered things to send (or we
            // could end up sending double info)
            if (cHelper.IsSettingChecked("SendAllAlarmsWhenConnect"))
            {
                foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
                {
                    foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
                    {
                        //if (AlarmObject.bActive == true || AlarmObject.bSuspended == true)
                        //{
                        RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Issue);
                        //}
                    }
                }
            }

            if (cHelper.IsSettingChecked("BufferAndSendAlarmsWhenConnect"))
            {
                SendBufferedEvents(cBufferedMessage.eMessageType.Alarm);
            }

            if (cHelper.IsSettingChecked("BufferAndSendAggregatedStatusWhenConnect"))
            {
                SendBufferedEvents(cBufferedMessage.eMessageType.AggregatedStatus);
            }

            if (cHelper.IsSettingChecked("BufferAndSendStatusUpdatesWhenConnect"))
            {
                SendBufferedEvents(cBufferedMessage.eMessageType.Status);
            }

            RSMPGS.ProcessImage.BufferedMessages.Clear();
            RSMPGS.MainForm.ListView_BufferedMessages.Items.Clear();
            RSMPGS.MainForm.textBox_BufferedMessages.Text = RSMPGS.ProcessImage.BufferedMessages.Count.ToString();

        }

        private void SendBufferedEvents(cBufferedMessage.eMessageType MessageType)
        {

            List<cBufferedMessage> BufferedMessagesToSend = RSMPGS.ProcessImage.BufferedMessages.FindAll(bbuf => bbuf.MessageType == MessageType);

            foreach (cBufferedMessage BufferedPacket in BufferedMessagesToSend)
            {
                if (RSMPGS.MainForm.checkBox_ViewOnlyFailedPackets.Checked == false)
                {
                    if (RSMPGS.MainForm.checkBox_ShowMax10BufferedMessagesInSysLog.Checked == false || BufferedMessagesToSend.Count <= 10)
                    {
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent buffered " + MessageType.ToString() + " message, Type: " + BufferedPacket.sPacketType + ", MsgId: " + BufferedPacket.sMessageId);
                    }
                }
                SendJSonPacket(BufferedPacket.sPacketType, BufferedPacket.sMessageId, BufferedPacket.sSendString, true);
            }
            if (BufferedMessagesToSend.Count > 10 && RSMPGS.MainForm.checkBox_ShowMax10BufferedMessagesInSysLog.Checked == true)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Sent {0} buffered " + MessageType.ToString() + " messages", BufferedMessagesToSend.Count);
            }

        }

    }

}
