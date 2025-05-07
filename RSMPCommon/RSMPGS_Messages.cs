using System;
using System.Collections.Generic;

namespace RSMP_Messages
{
  public class Header
  {
    public string mType;
    public string type;
    public string mId;
  }

  public class MessageAck
  {
    public string mType;
    public string type;
    public string oMId;
  }

  public class MessageNotAck
  {
    public string mType;
    public string type;
    public string oMId;
    public string rea;
  }

  public class AlarmHeader
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId; // ntsObjectId
    public string xNId; // externalNtsId
    public string cId; // componentId
    public string aCId; // alarmCodeId
    public string xACId; // externalAlarmCodeId
    public string xNACId; // externalNtsAlarmCodeId
    public string aSp; // alarmSpecialisation 
  }

  public class AlarmHeaderAndBody
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId; // ntsObjectId
    public string xNId; // externalNtsId
    public string cId; // componentId
    public string aCId; // alarmCodeId
    public string xACId; // externalAlarmCodeId
    public string xNACId; // externalNtsAlarmCodeId
    public string aSp; // alarmSpecialisation 

    public string ack; // acknowledgeState
    public string aS; // alarmState
    public string sS; // suspendState
    public string aTs; // alarmTimestamp
    public string cat; // category
    //      public string desc; // description 
    public string pri; // priority
    public List<AlarmReturnValue> rvs; // returnvalues
  }

  public class AlarmReturnValue
  {
    public string n; // name
    public object v; // value
  }

  public class AggregatedStatus
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId; // ntsObjectId
    public string xNId; // externalNtsId
    public string cId; // componentId

    public string aSTS; // AggregatedTimeStamp
    public string fP; // functionalPosition
    public string fS; // functionalState

    public bool[] se; // StatusBits
  }

  // Use string for status bits in RSMP 3.1.1 and RSMP 3.1.2
  public class AggregatedStatus3_1_2
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId; // ntsObjectId
    public string xNId; // externalNtsId
    public string cId; // componentId

    public string aSTS; // AggregatedTimeStamp
    public string fP; // functionalPosition
    public string fS; // functionalState

    public List<string> se; // StatusBits
  }

  public class AggregatedStatusRequest
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId; // ntsObjectId
    public string xNId; // externalNtsId
    public string cId; // componentId

  }

    public class CommandRequest
  {

    public string mType;
    public string type;
    public string mId;

    public string ntsOId;  // ntsObjectId
    public string xNId; // externalNtsId
    public string cId;  // componentId

    public List<CommandRequest_Value> arg; // Values

  }

  public class CommandRequest_Value
  {
    public string cCI;  // CommandCodeId
    public string n;  // Name
    public string cO;  // Command
    public object v;  // Value
  }

  public class CommandResponse
  {

    public string mType;
    public string type;
    public string mId;

    public string ntsOId;  // ntsObjectId
    public string xNId; // externalNtsId
    public string cId;  // componentId

    public string cTS;  // CommandTimeStamp

    public List<CommandResponse_Value> rvs; // Values

  }

  public class CommandResponse_Value
  {
    public string cCI;  // CommandCodeId
    public string n;  // Name
    public object v;  // Value
    public string age;  // Age
  }

  public class StatusRequest
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId;  // ntsObjectId
    public string xNId; // externalNtsId
    public string cId;  // componentId

    public List<StatusRequest_Status> sS; // Values
  }

  public class StatusRequest_Status
  {
    public string sCI; // StatusCodeId
    public string n;   // Name
  }

  public class StatusResponse
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId;  // ntsObjectId
    public string xNId; // externalNtsId
    public string cId;  // componentId
    public string sTs;  // TimeStamp

    public List<Status_VTQ> sS; // Values

  }

  public class Status_VTQ
  {
    public string sCI; // StatusCodeId
    public string n;   // Name
    public object s;   // Status
    public string q;   // Quality

  }


  public class StatusSubscribe_UpTo_3_1_4
  {

    public string mType;
    public string type;
    public string mId;

    public string ntsOId;  // ntsObjectId
    public string xNId; // externalNtsId
    public string cId;  // componentId

    public List<StatusSubscribe_Status_UpTo_3_1_4> sS; // Values

  }

  public class StatusSubscribe_Over_3_1_4
  {

    public string mType;
    public string type;
    public string mId;

    public string ntsOId;  // ntsObjectId
    public string xNId; // externalNtsId
    public string cId;  // componentId

    public List<StatusSubscribe_Status_Over_3_1_4> sS; // Values

  }

  public class StatusSubscribe_Status_Base
  {
    public string sCI;  // StatusCodeId
    public string n;    // Name
    public string uRt;  // UpdateRate
  }

  public class StatusSubscribe_Status_UpTo_3_1_4 : StatusSubscribe_Status_Base
  {
  }

  public class StatusSubscribe_Status_Over_3_1_4 : StatusSubscribe_Status_Base
  {
    public bool sOc;    // sendOnChange
  }

  
  public class StatusUpdate
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId;  // ntsObjectId
    public string xNId; // externalNtsId
    public string cId;  // componentId

    public string sTs; // StatusTimeStamp

    public List<Status_VTQ> sS; // Values

  }

  public class StatusUnsubscribe
  {
    public string mType;
    public string type;
    public string mId;

    public string ntsOId;  // ntsObjectId
    public string xNId; // externalNtsId
    public string cId;  // componentId

    public List<StatusUnsubscribe_Status> sS; // Values
  }

  public class StatusUnsubscribe_Status
  {
    public string sCI; // StatusCodeId
    public string n;   // Name
  }


  public class Watchdog
  {
    public string mType;
    public string type;
    public string mId;

    public string wTs;
  }

  public class rsVersion
  {
    public string mType;
    public string type;
    public string mId;

    public List<Version_RSMP> RSMP; // Versions
    public List<SiteId> siteId; // SiteId's

    public string SXL;  // Signal Exchange List
  }

  public class Version_RSMP
  {
    public string vers;
    public Version_RSMP()
    {
      vers = "";
    }
    public Version_RSMP(string sVersion)
    {
      vers = sVersion;
    }
  }

  public class SiteId
  {
    public string sId;
  }

}
