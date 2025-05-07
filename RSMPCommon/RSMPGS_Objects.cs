

using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Windows.Forms;
using static nsRSMPGS.cJSon;
using System.Diagnostics;
using static nsRSMPGS.cValueTypeObject;
using System.CodeDom;

namespace nsRSMPGS
{

  //
  // Site root object, only for viewing
  //
  public class cSiteIdObject
  {
    public string sSiteId;      // 37101
    public string sDescription;

    public List<cRoadSideObject> RoadSideObjects = new List<cRoadSideObject>();

    public System.Windows.Forms.TreeNode RootNode;

  }

  // Actually both grouped and single objects
  public class cRoadSideObject
  {
    public bool bIsComponentGroup = false;
    public string sObjectType;
    public string sObject;
    public string sComponentId;
    public string sNTSObjectId; // AB+37101=881CG001
    public string sExternalNTSId;
    public string sDescription;
    public bool[] bBitStatus = new bool[8];
    public string sFunctionalPosition;
    public string sFunctionalState;

    public System.Windows.Forms.TreeNode Node;

    public List<cAlarmObject> AlarmObjects = new List<cAlarmObject>();
    public List<cCommandObject> CommandObjects = new List<cCommandObject>();
    public List<cStatusObject> StatusObjects = new List<cStatusObject>();

    public cSiteIdObject SiteIdObject;

    public ListViewGroup StatusGroup;
    public ListViewGroup AlarmsGroup;
    public ListViewGroup CommandsGroup;


#if _RSMPGS1

    public DateTime dtLastChangedAggregatedStatus;
    public List<cSubscription> Subscriptions = new List<cSubscription>();

#endif

#if _RSMPGS2

    public List<cCommandEvent> CommandEvents = new List<cCommandEvent>(); 
    public List<cStatusEvent> StatusEvents = new List<cStatusEvent>();

    public List<cAggregatedStatusEvent> AggregatedStatusEvents = new List<cAggregatedStatusEvent>();

#endif

    public string UniqueId()
    {
      return SiteIdObject.sSiteId + "." + sNTSObjectId + "." + sObjectType + "." + sObject + "." + sComponentId;
    }


    public cCommandObject getCommandObject(String commandID)
    {
      foreach (cCommandObject CommandoObject in CommandObjects)
      {
        //        if (CommandoObject.CommandReturnValues != null && CommandoObject.CommandReturnValues.Count > 0
        //            && CommandoObject.CommandReturnValues[0].sCommandCodeId.Equals(commandID, StringComparison.OrdinalIgnoreCase))

        if (CommandoObject.CommandReturnValues != null && CommandoObject.CommandReturnValues.Count > 0
          && CommandoObject.sCommandCodeId.Equals(commandID, StringComparison.OrdinalIgnoreCase))
        {
          return CommandoObject;
        }
      }
      return null;
    }
    public cAlarmObject getAlarmObject(String sAlarmCodeId)
    {
      foreach (cAlarmObject AlarmObject in AlarmObjects)
      {
        if (AlarmObject.sAlarmCodeId.Equals(sAlarmCodeId, StringComparison.OrdinalIgnoreCase))
        {
          return AlarmObject;
        }
      }
      return null;
    }
    public cStatusObject getStatusObject(String statusID)
    {
      foreach (cStatusObject StatusObject in StatusObjects)
      {
        if (StatusObject.StatusReturnValues != null && StatusObject.StatusReturnValues.Count > 0
            && StatusObject.sStatusCodeId.Equals(statusID, StringComparison.OrdinalIgnoreCase))
        {
          return StatusObject;
        }
      }
      return null;
    }
  }

  public class cAggregatedStatusObject
  {
    public string sObjectType;
    public List<string> sFunctionalPositions;
    public List<string> sFunctionalStates;
    public string sDescription;
  }


  public class cAlarmObject
  {
    public cRoadSideObject RoadSideObject;

    public bool bActive = false;
    public bool bSuspended = false;
    public bool bAcknowledged = false;
    public string sObjectType;
    public string sSpecificObject; // Optional, if set must match object in SXL
    public string sAlarmCodeId;
    public DateTime sTimestamp;
    public string sDescription;
    public string sExternalAlarmCodeId;
    public string sExternalNTSAlarmCodeId;
    public string sPriority;
    public string sCategory;
    public int AlarmCount;

#if _RSMPGS1

    public DateTime dtLastChangedAlarmStatus;

#endif

    public List<cAlarmReturnValue> AlarmReturnValues = new List<cAlarmReturnValue>();
    public List<cAlarmEvent> AlarmEvents = new List<cAlarmEvent>();

    public string StatusAsText()
    {
      string sStatus = "";
      if (bActive)
      {
        sStatus += "Active, ";
      }
      if (bSuspended)
      {
        sStatus += "Suspended, ";
      }
      if (bAcknowledged)
      {
        sStatus += "Acked, ";
      }
      if (bActive == false && bSuspended == false && bAcknowledged == true)
      {
        sStatus = "";
      }
      if (sStatus.Length > 0)
      {
        sStatus = sStatus.Remove(sStatus.Length - 2);
      }

      return sStatus;

    }

    public void CloneFromAlarmObject(cAlarmObject AlarmObjectToCopy)
    {
      RoadSideObject = AlarmObjectToCopy.RoadSideObject;
      bActive = AlarmObjectToCopy.bActive;
      bSuspended = AlarmObjectToCopy.bSuspended;
      bAcknowledged = AlarmObjectToCopy.bAcknowledged;
      sObjectType = AlarmObjectToCopy.sObjectType;
      sSpecificObject = AlarmObjectToCopy.sSpecificObject;
      sAlarmCodeId = AlarmObjectToCopy.sAlarmCodeId;
      sDescription = AlarmObjectToCopy.sDescription;
      sExternalAlarmCodeId = AlarmObjectToCopy.sExternalAlarmCodeId;
      sExternalNTSAlarmCodeId = AlarmObjectToCopy.sExternalNTSAlarmCodeId;
      sPriority = AlarmObjectToCopy.sPriority;
      sCategory = AlarmObjectToCopy.sCategory;
      AlarmReturnValues.Clear();
      foreach (cAlarmReturnValue AlarmReturnValue in AlarmObjectToCopy.AlarmReturnValues)
      {
        cAlarmReturnValue NewAlarmReturnValue = new cAlarmReturnValue();
        NewAlarmReturnValue.sName = AlarmReturnValue.sName;
        NewAlarmReturnValue.sComment = AlarmReturnValue.sComment;
        NewAlarmReturnValue.Value = new cValue(AlarmReturnValue.Value.ValueTypeObject, true);
        AlarmReturnValues.Add(NewAlarmReturnValue);
      }
      // We don't copy alarm events
    }
  }

  public class cAlarmReturnValue
  {
    public string sName;
    public string sComment;

    public cValue Value;

    // Create from YAML-file
    public cAlarmReturnValue()
    {
    }

    // Create from CSV-file
    public cAlarmReturnValue(cAlarmObject AlarmObject, string sCompleteRow, ref int iIndex)
    {

      sName = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();

      if (IsValid())
      {

        string sType = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
        string sValueRange = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
        sComment = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();

        string sValueTypeKey = AlarmObject.sObjectType + "\t" + "alarms" + "\t" + AlarmObject.sAlarmCodeId + "\t" + AlarmObject.sSpecificObject + "\t" + sName;

        RSMPGS.ProcessImage.CreateAndAddValueTypeObject(sValueTypeKey, sName, sType, sValueRange, sComment);

        Value = new cValue(sValueTypeKey, true);

      }

    }

    public bool IsValid()
    {
      return sName.Length > 0;
    }

  }

  public class cAlarmEvent
  {
    public cAlarmObject AlarmObject;
    public string sTimeStamp;
    public string sMessageId;
    public string sAlarmCodeId;
    public string sEvent;
    public string sDirection;
    public List<cAlarmEventReturnValue> AlarmEventReturnValues = new List<cAlarmEventReturnValue>();
  }

  public class cAlarmEventReturnValue
  {
    public string sName;
    public object oValue;

    public cAlarmEventReturnValue(string sName, object oValue)
    {
      this.sName = sName;
      this.oValue = oValue;
    }
  }


  public class cCommandObject
  {
    // Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;

    public string sObjectType;
    public string sSpecificObject; // Optional, if set must match object in SXL
    public string sCommandCodeId;
    public string sDescription;
    public cRoadSideObject RoadSideObject;
    public List<cCommandReturnValue> CommandReturnValues = new List<cCommandReturnValue>();

    public void CloneFromCommandObject(cCommandObject CommandObjectToCopy)
    {
      RoadSideObject = CommandObjectToCopy.RoadSideObject;
      sObjectType = CommandObjectToCopy.sObjectType;
      sSpecificObject = CommandObjectToCopy.sSpecificObject;
      sCommandCodeId = CommandObjectToCopy.sCommandCodeId;
      sDescription = CommandObjectToCopy.sDescription;
      CommandReturnValues.Clear();
      foreach (cCommandReturnValue CommandReturnValue in CommandObjectToCopy.CommandReturnValues)
      {
        cCommandReturnValue NewCommandReturnValue = new cCommandReturnValue(this);
        NewCommandReturnValue.CommandObject = CommandReturnValue.CommandObject;
        NewCommandReturnValue.sName = CommandReturnValue.sName;
        NewCommandReturnValue.sCommand = CommandReturnValue.sCommand;
        NewCommandReturnValue.sComment = CommandReturnValue.sComment;
        NewCommandReturnValue.bOptional = CommandReturnValue.bOptional;
        NewCommandReturnValue.Value = new cValue(CommandReturnValue.Value.ValueTypeObject, true);

#if _RSMPGS2

        NewCommandReturnValue.sAge = CommandReturnValue.sAge;
        NewCommandReturnValue.sLastRecValue = CommandReturnValue.sLastRecValue;
        NewCommandReturnValue.sLastRecAge = CommandReturnValue.sLastRecAge;
        NewCommandReturnValue.CommandObject = this;
#endif

        CommandReturnValues.Add(NewCommandReturnValue);
      }
    }

    public cCommandReturnValue getCommandReturnValueByName(String name)
    {

      foreach (cCommandReturnValue v in CommandReturnValues)
      {
        if (v.sName.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
          return v;
        }
      }
      return null;
    }
  }

  public class cCommandReturnValue
  {

    public cCommandObject CommandObject;

    // Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;
    public string sName;
    public string sCommand;
    public string sComment;

    public cValue Value;
    public Boolean bOptional;

#if _RSMPGS2

    public string sAge;
    public string sLastRecValue;
    public string sLastRecAge;

#endif

    public cCommandReturnValue(cCommandObject CommandObject, string sCompleteRow, ref int iIndex)
    {

      this.CommandObject = CommandObject;

      sName = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();

      if (IsValid())
      {
        sCommand = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
        string sType = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
        string sValueRange = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();

        sComment = cHelper.Item(sCompleteRow, iIndex++, ';').Replace("\"", "").Trim();

        string sValueTypeKey = CommandObject.sObjectType + "\t" + "alarms" + "\t" + CommandObject.sCommandCodeId + "\t" + CommandObject.sSpecificObject + "\t" + sName;

        RSMPGS.ProcessImage.CreateAndAddValueTypeObject(sValueTypeKey, sName, sType, sValueRange, sComment);

        Value = new cValue(sValueTypeKey, true);

      }
    }

    public cCommandReturnValue(cCommandObject CommandObject)
    {
      this.CommandObject = CommandObject;
    }

    public bool IsValid()
    {
      return sName.Length > 0;
    }
  }



  public class cStatusObject
  {
    // Object type;Object (optional);statusCodeId;description;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment

    public string sObjectType;
    public string sSpecificObject; // Optional, if set must match object in SXL
    public string sStatusCodeId;
    public string sDescription;

    public List<cStatusReturnValue> StatusReturnValues = new List<cStatusReturnValue>();

    public cRoadSideObject RoadSideObject;

#if _RSMPGS2

    public List<cStatusSubscription> StatusSubscriptions = new List<cStatusSubscription>();

#endif

    public cStatusReturnValue getStatusReturnValueByName(String name)
    {
      foreach (cStatusReturnValue v in StatusReturnValues)
      {
        if (v.sName.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
          return v;
        }
      }
      return null;
    }

    public void CloneFromStatusObject(cStatusObject StatusObjectToCopy)
    {
      RoadSideObject = StatusObjectToCopy.RoadSideObject;
      sObjectType = StatusObjectToCopy.sObjectType;
      sSpecificObject = StatusObjectToCopy.sSpecificObject;
      sDescription = StatusObjectToCopy.sDescription;
      sStatusCodeId = StatusObjectToCopy.sStatusCodeId;

      StatusReturnValues.Clear();

      foreach (cStatusReturnValue StatusReturnValue in StatusObjectToCopy.StatusReturnValues)
      {
        cStatusReturnValue NewStatusReturnValue = new cStatusReturnValue(this);
        NewStatusReturnValue.StatusObject = StatusReturnValue.StatusObject;
        NewStatusReturnValue.sName = StatusReturnValue.sName;
        NewStatusReturnValue.sComment = StatusReturnValue.sComment;
        NewStatusReturnValue.Value = new cValue(StatusReturnValue.Value.ValueTypeObject, true);

#if _RSMPGS2

        NewStatusReturnValue.sLastUpdateRate = StatusReturnValue.sLastUpdateRate;
        NewStatusReturnValue.bLastUpdateOnChange = StatusReturnValue.bLastUpdateOnChange;
        NewStatusReturnValue.sQuality = StatusReturnValue.sQuality;

#endif
        StatusReturnValues.Add(NewStatusReturnValue);
      }
    }
  }

  public class cStatusReturnValue
  {

    public cStatusObject StatusObject;

    // Object type;Object (optional);statusCodeId;description;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment
    public string sName;
    public string sComment;

    public cValue Value;// Value column

#if _RSMPGS1

    public bool bRecentlyChanged = false;

#endif

#if _RSMPGS2

    public string sQuality = "";
    public string sLastUpdateRate = "";
    public bool bLastUpdateOnChange = false;

#endif

    public cStatusReturnValue(cStatusObject StatusObject, string sCompleteRow, ref int iIndex)
    {

      this.StatusObject = StatusObject;

      sName = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();

      if (IsValid())
      {
        string sType = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
        string sValueRange = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
        string sCommentFromFile = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
        sComment = sCommentFromFile.Replace("\"", "").Trim();

        string sValueTypeKey = StatusObject.sObjectType + "\t" + "alarms" + "\t" + StatusObject.sStatusCodeId + "\t" + StatusObject.sSpecificObject + "\t" + sName;

        RSMPGS.ProcessImage.CreateAndAddValueTypeObject(sValueTypeKey, sName, sType, sValueRange, sComment);

        Value = new cValue(sValueTypeKey, true);
      }
    }

    public cStatusReturnValue(cStatusObject ParentStatusObject)
    {
      StatusObject = ParentStatusObject;
    }

    public bool IsValid()
    {
      return sName.Length > 0;
    }
  }

  public class cValue
  {

    private object oValue = null;

    private List<Dictionary<string, object>> items = new List<Dictionary<string, object>>(); // array

    private eQuality quality = eQuality.unknown;

    public cValueTypeObject ValueTypeObject = null;

    public enum eQuality
    {
      unknown,
      recent,
      old
    }

    public Dictionary<string, string> GetSelectableValues()
    {
      return ValueTypeObject.GetSelectableValues();
    }

    public double GetValueMin()
    {
      return ValueTypeObject.GetValueMin();
    }

    public double GetValueMax()
    {
      return ValueTypeObject.GetValueMax();
    }

    public string GetValueType()
    {
      return ValueTypeObject.GetValueTypeAsString();
    }

    public void SetInitialUnknownValue()
    {

      quality = eQuality.unknown;

      if (ValueTypeObject != null)
      {

        switch (ValueTypeObject.ValueType)
        {
          case cValueTypeObject.eValueType._unknown:
          case cValueTypeObject.eValueType._string:
          case cValueTypeObject.eValueType._raw:
          case cValueTypeObject.eValueType._scale:
          case cValueTypeObject.eValueType._number:
          case cValueTypeObject.eValueType._unit:
            oValue = "?";
            break;
          case cValueTypeObject.eValueType._base64:
            oValue = "";
            break;
          case cValueTypeObject.eValueType._timestamp:
            oValue = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture);
            break; 
          case cValueTypeObject.eValueType._array:
            oValue = "(array)";
            break;
          case cValueTypeObject.eValueType._integer:
          case cValueTypeObject.eValueType._long:
          case cValueTypeObject.eValueType._real:
          case cValueTypeObject.eValueType._ordinal:
            oValue = GetValueMin();
            break;
          case cValueTypeObject.eValueType._boolean:
            oValue = false;
            break;
          default:
            oValue = "(invalid type)";
            break;

        }

        // Check selectable values
        Dictionary<string, string> sEnums = ValueTypeObject.GetSelectableValues();
        if (sEnums != null && sEnums.Count > 0)
        {
          foreach (string sEnum in sEnums.Keys)
          {
            // Select the first one in the list
            oValue = sEnum;
            break;
          }
        }
      }
      else
      {
        oValue = "(type not found)";
      }
    }

    public cValue(cValueTypeObject ValueTypeObject, bool bSetInitialUnknownValue)
    {
      this.ValueTypeObject = ValueTypeObject;

      if (bSetInitialUnknownValue)
      {
        SetInitialUnknownValue();
      }

    }

    public cValue(string sValueTypeKey, bool bSetInitialUnknownValue)
    {

      RSMPGS.ProcessImage.ValueTypeObjects.TryGetValue(sValueTypeKey, out ValueTypeObject);

      if (bSetInitialUnknownValue)
      {
        SetInitialUnknownValue();
      }

    }

    public eQuality Quality
    {
      get
      {
        return quality;
      }
      set
      {
        quality = eQuality.recent;
      }
    }

    public bool SetValue(object oValue)
    {
      this.oValue = oValue;
      quality = eQuality.recent;
      return true;
    }
    public bool SetValue(string sValue)
    {
      switch (ValueTypeObject.ValueType)
      {
        case eValueType._integer:
          int iValue;
          if (int.TryParse(sValue, out iValue))
            this.oValue = iValue;
          else
            this.oValue = null;
          break;
        case eValueType._number:
        case eValueType._long:
          long lValue;
          if (long.TryParse(sValue, out lValue))
            this.oValue = lValue;
          else
            this.oValue = null;
          break;
        case eValueType._boolean:
          if (sValue.Equals("True", StringComparison.InvariantCultureIgnoreCase))
            this.oValue = true;
          else
            this.oValue = false;
          break;
        default:
          this.oValue = sValue == null ? null : sValue.ToString();
          break;
      }

      quality = eQuality.recent;
      return true;
    }
    public object GetValue()
    {
      return oValue;
    }

    public bool SetArray(List<Dictionary<string, object>> items)
    {
      this.items = items;
      quality = eQuality.recent;
      return true;
    }
    public List<Dictionary<string, object>> GetArray()
    {
      return this.items;
    }
  }

  public class cValueTypeObject
  {

    public string sValueTypeKey; // Not really used here only for debugging


    public Int32 iMinValue = Int32.MinValue;
    public Int32 iMaxValue = Int32.MaxValue;

    public double dMinValue = double.MinValue;
    public double dMaxValue = double.MaxValue;

    public Dictionary<string, string> SelectableValues;
    public string sName;
    public Dictionary<string, cYAMLMapping> Items;

    public enum eValueType
    {
      _unknown,
      _timestamp,
      _string,
      _integer,
      _long,
      _real,
      _number,
      _boolean,
      _base64,
      _raw,
      _scale,
      _unit,
      _ordinal,
      _array
    }

    public string sComment;
    public eValueType ValueType;


    public cValueTypeObject(string sValueTypeKey, string sName, string sType, Dictionary<string, string> SelectableValues, double dMin, double dMax, string sComment, Dictionary<string, cYAMLMapping> items)
    {

      this.sValueTypeKey = sValueTypeKey;
      this.dMinValue = dMin;
      this.dMaxValue = dMax;
      this.sComment = sComment;
      this.SelectableValues = SelectableValues;
      this.sName = sName;
      this.Items = items;

      foreach (eValueType valueType in Enum.GetValues(typeof(eValueType)))
      {
        if (sType.Equals(valueType.ToString().Substring(1), StringComparison.OrdinalIgnoreCase))
        {
          ValueType = valueType;
          break;
        }
      }
    }

    public Dictionary<string, string> GetSelectableValues()
    {
      return this.SelectableValues;
    }

    public double GetValueMin()
    {
      return this.dMinValue;
    }

    public double GetValueMax()
    {
      return this.dMaxValue;
    }

    public string GetValueTypeAsString()
    {
      return ValueType.ToString().Substring(1);
    }

    public bool ValidateValue(string sValue)
    {

      switch (ValueType)
      {

        case eValueType._string:

          if (SelectableValues == null || SelectableValues.Count == 0)
          {
            return true;
          }
          else
          {
            foreach (string sScanValue in SelectableValues.Keys)
            {
              bool bUseCaseSensitiveValue = cHelper.IsSettingChecked("UseCaseSensitiveValue");
              var comparisonType = StringComparison.Ordinal;
              if (!bUseCaseSensitiveValue) {
                comparisonType = StringComparison.OrdinalIgnoreCase;
              }

              if (sScanValue.Equals(sValue, comparisonType))
              {
                return true;
              }
            }
          }
          return false;

        case eValueType._raw:
        case eValueType._scale:
        case eValueType._unit:

          if (RSMPGS.JSon.NegotiatedRSMPVersion != RSMPVersion.RSMP_3_1_1 && RSMPGS.JSon.NegotiatedRSMPVersion != RSMPVersion.RSMP_3_1_2)
          {
            return false;
          }
          else
          {
            return true;
          }

        case eValueType._base64:

          try
          {
            Encoding encoding;
            encoding = Encoding.GetEncoding("IBM437");
            byte[] Base64Bytes = encoding.GetBytes(sValue);
            char[] Base64Chars = encoding.GetChars(Base64Bytes);
            byte[] Base8Bytes = System.Convert.FromBase64CharArray(Base64Chars, 0, Base64Chars.GetLength(0));

            return true;
          }
          catch
          {
            return false;
          }

        case eValueType._boolean:

          // Boolean is treated as an enum in Excel/CSV, but not in YAML. To
          // preserve backwards compatibility we need to treat this as case
          // insensitive for now
          if (sValue.Equals("true", StringComparison.OrdinalIgnoreCase) ||
            sValue.Equals("false", StringComparison.OrdinalIgnoreCase) ||
            sValue.Equals("0", StringComparison.OrdinalIgnoreCase) ||
            sValue.Equals("1", StringComparison.OrdinalIgnoreCase))
          {
            return true;
          }
          else
          {
            return false;
          }

        case eValueType._integer:

          Int32 iValue;
          if (Int32.TryParse(sValue, out iValue))
          {
            if (iValue <= iMaxValue && iValue >= iMinValue)
            {
              return true;
            }
            else
            {
              return false;
            }
          }
          else
          {
            return false;
          }

        case eValueType._long:

          Int32 lValue;
          if (Int32.TryParse(sValue, out lValue))
          {
            if (lValue <= iMaxValue && lValue >= iMinValue)
            {
              return true;
            }
            else
            {
              return false;
            }
          }
          else
          {
            return false;
          }

        case eValueType._number:
        case eValueType._real:

          double dValue;
          if (double.TryParse(sValue, out dValue))
          {
            if (dValue <= dMaxValue && dValue >= dMinValue)
            {
              return true;
            }
            else
            {
              return false;
            }
          }
          else
          {
            return false;
          }

        case eValueType._ordinal:

          if (RSMPGS.JSon.NegotiatedRSMPVersion != RSMPVersion.RSMP_3_1_1 && RSMPGS.JSon.NegotiatedRSMPVersion != RSMPVersion.RSMP_3_1_2)
          {
            return false;
          }
          else
          {
            Int32 oValue;
            if (Int32.TryParse(sValue, out oValue))
            {
              if (oValue <= iMaxValue && oValue >= iMinValue)
              {
                return true;
              }
              else
              {
                return false;
              }
            }
            else
            {
              return false;
            }
          }

        case eValueType._timestamp:
          DateTime timestamp;
          if (DateTime.TryParse(sValue, out timestamp))
          {
            return true;
          }
          else
          {
            return false;
          }          

        default:

          return false;
      }
    }

  }
}
