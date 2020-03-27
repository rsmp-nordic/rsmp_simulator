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

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public System.Windows.Forms.TreeNode RootNode;

	}

  // Actually both grouped and single objects
	public class cRoadSideObject
	{
    public bool bIsComponentGroup = false;
		public string sObjectType;
		public string sObject;
		public string sComponentId;
    public cSiteIdObject SiteIdObject; 
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
    public List<cSubscription> Subscriptions = new List<cSubscription>();

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public ListViewGroup StatusGroup;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public ListViewGroup AlarmsGroup;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public ListViewGroup CommandsGroup;


    public string UniqueId()
		{
      return SiteIdObject.sSiteId + "." + sNTSObjectId + "." + sObjectType + "." + sObject + "." + sComponentId;
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
    public string sSpecificObject; // Optional, if set must match object in SUL
    public string sAlarmCodeId;
		public string sDescription;
		public string sExternalAlarmCodeId;
		public string sExternalNTSAlarmCodeId;
		public string sPriority;
		public string sCategory;
    public int AlarmCount;
    public List<cAlarmReturnValue> AlarmReturnValues = new List<cAlarmReturnValue>();
		public List<cAlarmEvent> AlarmEvents = new List<cAlarmEvent>();
		public string StatusAsText()
		{
			string sStatus = "";
			if (bActive) sStatus += "Active, ";
			if (bSuspended) sStatus += "Suspended, ";
			if (bAcknowledged) sStatus += "Acked, ";
			if (bActive == false && bSuspended == false && bAcknowledged == true) sStatus = "";
			if (sStatus.Length > 0) sStatus = sStatus.Remove(sStatus.Length - 2);
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
        NewAlarmReturnValue.sType = AlarmReturnValue.sType;
        NewAlarmReturnValue.sValue = AlarmReturnValue.sValue;
        NewAlarmReturnValue.sValues = AlarmReturnValue.sValues;
        NewAlarmReturnValue.sComment = AlarmReturnValue.sComment;
        AlarmReturnValues.Add(NewAlarmReturnValue);
      }
      // We don't copy alarm events
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
    public string sValue;
    public cAlarmEventReturnValue(string sName, string sValue)
    {
      this.sName = sName;
      this.sValue = sValue;
    }
  }

  public class cAlarmReturnValue
	{
		public string sName;
		public string sType;
		public string sUnit;
		public string sValue = "?";
    public string sValues;
    public string sComment;
		public cAlarmReturnValue(string sCompleteRow, ref int iIndex, bool bReadAlsoUnit)
		{
			sName = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			sType = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			if (bReadAlsoUnit)
			{
				sUnit = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			}
			else
			{
				sUnit = "";
			}
			sValues = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			sComment = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
		}
    public cAlarmReturnValue()
    {
    }
    public bool IsValid()
    {
      return sName.Length > 0;
    }
	}

  public class cCommandObject
  {
    // Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;

    public string sObjectType;
    public string sSpecificObject; // Optional, if set must match object in SUL
    public string sDescription;
    public cRoadSideObject RoadSideObject;
    //public string sCommandCodeId;
    public List<cCommandReturnValue> CommandReturnValues = new List<cCommandReturnValue>();

    public void CloneFromCommandObject(cCommandObject CommandObjectToCopy)
    {
      RoadSideObject = CommandObjectToCopy.RoadSideObject;
      sObjectType = CommandObjectToCopy.sObjectType;
      sSpecificObject = CommandObjectToCopy.sSpecificObject;
      sDescription = CommandObjectToCopy.sDescription;
      //sCommandCodeId = CommandObjectToCopy.sCommandCodeId;
      CommandReturnValues.Clear();
      foreach (cCommandReturnValue CommandReturnValue in CommandObjectToCopy.CommandReturnValues)
      {
        cCommandReturnValue NewCommandReturnValue = new cCommandReturnValue();
        NewCommandReturnValue.sCommandCodeId = CommandReturnValue.sCommandCodeId;
        NewCommandReturnValue.sName = CommandReturnValue.sName;
        NewCommandReturnValue.sCommand = CommandReturnValue.sCommand;
        NewCommandReturnValue.sType = CommandReturnValue.sType;
        NewCommandReturnValue.sValue = CommandReturnValue.sValue;
        NewCommandReturnValue.sComment = CommandReturnValue.sComment;
        CommandReturnValues.Add(NewCommandReturnValue);
      }
    }
  }

  public class cCommandReturnValue
  {
    // Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;
    public string sCommandCodeId;
    public string sName;
    public string sCommand;
    public string sType;
    public string sValue;
    public string sComment;
    public cCommandReturnValue(string sCommandCodeId, string sCompleteRow, ref int iIndex)
    {
      this.sCommandCodeId = sCommandCodeId;
      sName = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      sCommand = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      sType    = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      sValue   = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      sValue   = "0";
      sComment = cHelper.Item(sCompleteRow, iIndex++, ';').Replace("\"","").Replace("\n", " ").Trim();
    }
    public cCommandReturnValue()
    {
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
		public string sSpecificObject; // Optional, if set must match object in SUL
    public string sStatusCodeId;
    public string sDescription;

		public List<cStatusReturnValue> StatusReturnValues = new List<cStatusReturnValue>();

    public cRoadSideObject RoadSideObject;

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
				NewStatusReturnValue.sName = StatusReturnValue.sName;
				NewStatusReturnValue.sType = StatusReturnValue.sType;
        NewStatusReturnValue.sStatus = StatusReturnValue.sStatus;
        NewStatusReturnValue.sValues = StatusReturnValue.sValues;
        //				NewStatusReturnValue.sValue = StatusReturnValue.sValue;
        NewStatusReturnValue.sComment = StatusReturnValue.sComment;
				StatusReturnValues.Add(NewStatusReturnValue);
			}
		}
	}

	public class cStatusReturnValue
	{

    public cStatusObject StatusObject;
    // Object type;Object (optional);statusCodeId;description;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment
		public string sName;
    public string sType;
    public string sStatus;
		public string sComment;
    public string sValues; // Value column
    public bool bRecentlyChanged = false;
		public cStatusReturnValue(cStatusObject ParentStatusObject, string sCompleteRow, ref int iIndex)
		{
      StatusObject = ParentStatusObject;
      sName = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			sType = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      sValues = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      //sStatus = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      sStatus = "?"; // sStatus = "0";
			sComment = cHelper.Item(sCompleteRow, iIndex++, ';').Replace("\"", "").Replace("\n", " ").Trim();
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

  public class cSubscription
  {

    public cStatusObject StatusObject;
    public cStatusReturnValue StatusReturnValue;

    public const int Subscribe_Nothing = 0;
    public const int Subscribe_Interval = 1;
    public const int Subscribe_OnChange = 2;

    public int SubscribeStatus;
    public int UpdateRate;
		public DateTime LastUpdate;

    public cSubscription(cStatusObject sO, cStatusReturnValue sS, float fUpdateRate)
    {
      if (fUpdateRate <= 0)
			{
				SubscribeStatus = Subscribe_OnChange;
				UpdateRate = 0;
			}
			else
			{
        try
        {
          SubscribeStatus = Subscribe_Interval;
          UpdateRate = (int)fUpdateRate * 1000;
        }
        catch
        {
          UpdateRate = 0;
        }
			}
      LastUpdate = DateTime.Now;
      StatusObject = sO;
      StatusReturnValue = sS;
    }

  }

  public class cBufferedPacket
  {

    public string sPacketType;
    public string sMessageId;
    public string sSendString;

		public cBufferedPacket(string PacketType, string MessageId, string SendString)
    {
      sPacketType = PacketType;
      sMessageId = MessageId;
      sSendString = SendString;
    }
  }

	public class cProcessImage
	{

		static object LockObject = new Object();

		public const int SectionType_None         = 0;
		public const int SectionType_RootObject   = 1;
		public const int SectionType_GroupObject  = 2;
    public const int SectionType_SingleObject = 3;
		public const int SectionType_Alarm        = 4;
		public const int SectionType_Event        = 5;
		public const int SectionType_Command      = 6;
		public const int SectionType_Status       = 7;
    public const int SectionType_AggregatedStatus = 8;
    public const int SectionType_Revision = 9;

    // Root objects
		public List<cSiteIdObject> SiteIdObjects = new List<cSiteIdObject>();

    // This one is flat
    public Dictionary<string, cRoadSideObject> RoadSideObjects = new Dictionary<string, cRoadSideObject>(StringComparer.InvariantCultureIgnoreCase);

    public List<cAlarmObject> AlarmObjects = new List<cAlarmObject>();
    public List<cCommandObject> CommandObjects = new List<cCommandObject>();
		public List<cStatusObject> StatusObjects = new List<cStatusObject>();
		public List<cAggregatedStatusObject> AggregatedStatusObjects = new List<cAggregatedStatusObject>();
		public List<cBufferedPacket> BufferedAlarms = new List<cBufferedPacket>();
		public List<cBufferedPacket> BufferedAggregatedStatus = new List<cBufferedPacket>();
		public List<cBufferedPacket> BufferedStatusUpdates = new List<cBufferedPacket>();

    public string[] sAggregatedStatusBitTexts = new string[8];

		public int MaxAlarmReturnValues = 0;
		//public int MaxStatusReturnValues = 0;

    public string sSULRevision = "";
    public int ObjectFilesTimeStamp = 0;

		public cProcessImage()
		{

		}

		public int LoadReferenceFiles(System.Windows.Forms.TreeView treeView)
		{

			bool bLoadFailed = false;
			int iReadFiles = 0;
			string[] ReferenceFiles;

      DateTime dtStartTime = new DateTime(1970, 1, 1);
      UInt32 uObjectFilesTimeStamp = 0;

			ReferenceFiles = Directory.GetFiles(cPrivateProfile.ObjectFilesPath(), "*.skv");
			foreach (string ReferenceFile in ReferenceFiles)
			{
				if (LoadReferenceFile(treeView, ReferenceFile))
				{
          uObjectFilesTimeStamp += Convert.ToUInt32(Math.Abs((File.GetCreationTime(ReferenceFile) - dtStartTime).TotalSeconds));
          uObjectFilesTimeStamp += Convert.ToUInt32(Math.Abs((File.GetLastWriteTime(ReferenceFile) - dtStartTime).TotalSeconds));
          iReadFiles++;
				}
				else
				{
					bLoadFailed = true;
					break;
				}
			}

			if (bLoadFailed == false)
			{
				ReferenceFiles = Directory.GetFiles(cPrivateProfile.ObjectFilesPath(), "*.csv");
				foreach (string ReferenceFile in ReferenceFiles)
				{
					if (LoadReferenceFile(treeView, ReferenceFile))
					{
            uObjectFilesTimeStamp += Convert.ToUInt32(Math.Abs((File.GetCreationTime(ReferenceFile) - dtStartTime).TotalSeconds));
            uObjectFilesTimeStamp += Convert.ToUInt32(Math.Abs((File.GetLastWriteTime(ReferenceFile) - dtStartTime).TotalSeconds));
            iReadFiles++;
					}
					else
					{
						bLoadFailed = true;
						break;
					}
				}
			}

      ObjectFilesTimeStamp = (int)(uObjectFilesTimeStamp & 0xffffff);
			//
			// Loaded all objects, now find alarms for each object type
			//
			foreach (cAlarmObject AlarmObject in AlarmObjects) if (MaxAlarmReturnValues < AlarmObject.AlarmReturnValues.Count) MaxAlarmReturnValues = AlarmObject.AlarmReturnValues.Count;

			foreach (cRoadSideObject RoadSideObject in RoadSideObjects.Values)
			{
				foreach (cAlarmObject AlarmObject in AlarmObjects)
				{
					if (RoadSideObject.sObjectType.Equals(AlarmObject.sObjectType, StringComparison.OrdinalIgnoreCase))
					{
            bool bCopyThisAlarmObject = false;
            if (AlarmObject.sSpecificObject.Length > 0)
            {
              if (AlarmObject.sSpecificObject.Equals(RoadSideObject.sObject, StringComparison.OrdinalIgnoreCase))
              {
                bCopyThisAlarmObject = true;
              }
            }
            else
            {
              bCopyThisAlarmObject = true;
            }
            if (bCopyThisAlarmObject)
            {
              cAlarmObject NewAlarmObject = new cAlarmObject();
              NewAlarmObject.CloneFromAlarmObject(AlarmObject);
              NewAlarmObject.RoadSideObject = RoadSideObject;
              RoadSideObject.AlarmObjects.Add(NewAlarmObject);
              // Don't break here, object type may occur more than once
            }
					}
				}
			}
      //
      // Find command for each object type
      //

      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        foreach (cCommandObject CommandObject in CommandObjects)
        {
          if (RoadSideObject.sObjectType.Equals(CommandObject.sObjectType, StringComparison.OrdinalIgnoreCase))
          {
            bool bCopyThisCommandObject = false;
            if (CommandObject.sSpecificObject.Length > 0)
            {
              if (CommandObject.sSpecificObject.Equals(RoadSideObject.sObject, StringComparison.OrdinalIgnoreCase))
              {
                bCopyThisCommandObject = true;
              }
            }
            else
            {
              bCopyThisCommandObject = true;
            }
            if (bCopyThisCommandObject)
            {
              cCommandObject NewCommandObject = new cCommandObject();
              NewCommandObject.CloneFromCommandObject(CommandObject);
              NewCommandObject.RoadSideObject = RoadSideObject;
              RoadSideObject.CommandObjects.Add(NewCommandObject);
              // Don't break here, object type may occur more than once
            }
          }
        }
      }

			//
			// Find status for each object type
			//
			//foreach (cStatusObject StatusObject in StatusObjects) if (MaxStatusReturnValues < StatusObject.StatusReturnValues.Count) MaxStatusReturnValues = StatusObject.StatusReturnValues.Count;

			foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
			{
				foreach (cStatusObject StatusObject in StatusObjects)
				{
					if (RoadSideObject.sObjectType.Equals(StatusObject.sObjectType, StringComparison.OrdinalIgnoreCase))
					{
						bool bCopyThisStatusObject = false;
						if (StatusObject.sSpecificObject.Length > 0)
						{
							if (StatusObject.sSpecificObject.Equals(RoadSideObject.sObject, StringComparison.OrdinalIgnoreCase))
							{
                bCopyThisStatusObject = true;
							}
						}
						else
						{
              bCopyThisStatusObject = true;
						}
						if (bCopyThisStatusObject)
						{
							cStatusObject NewStatusObject = new cStatusObject();
							NewStatusObject.CloneFromStatusObject(StatusObject);
              NewStatusObject.RoadSideObject = RoadSideObject;
							RoadSideObject.StatusObjects.Add(NewStatusObject);
							// Don't break here, object type may occur more than once
						}
					}
				}
			}

      // Fixup some links
      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        foreach (cStatusObject StatusObject in StatusObjects)
        {
          foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
          {
            StatusReturnValue.StatusObject = StatusObject;
          }
          StatusObject.RoadSideObject = RoadSideObject;
        }
      }

      return iReadFiles;

		}

		public bool LoadReferenceFile(System.Windows.Forms.TreeView treeView, string FileName)
		{

			int iSectionType = SectionType_None;
			int iIndex;

      cAlarmReturnValue AlarmReturnValue;
      cCommandReturnValue CommandReturnValue;
      cStatusReturnValue StatusReturnValue;
      cSiteIdObject LastSiteIdObject = null;
      cRoadSideObject RoadSideObject;

      Encoding encoding;

			System.Windows.Forms.TreeNode treeNode;

			int iLoadedAlarms = 0;
			int iLoadedCommands = 0;
			int iLoadedStatus = 0;
      int iLoadedAggregatedStatus = 0;
			int iLoadedObjects = 0;

      string sSiteIdDescription = "";

    	try
			{
    
        if (FileName.EndsWith(".skv", StringComparison.OrdinalIgnoreCase))
        {
          encoding = Encoding.GetEncoding("ibm852");
        }
        else
        {
          encoding = Encoding.GetEncoding("iso-8859-1");
        }

        StreamReader swReferenceFile = new StreamReader((System.IO.Stream)File.OpenRead(FileName), encoding);

        string sCompleteFile = swReferenceFile.ReadToEnd();
        string[] sCompleteFileLines = sCompleteFile.Split('\r');

        foreach (string sUnformattedLine in sCompleteFileLines)
        {

          string sLine;

          if (sUnformattedLine.StartsWith("\n"))
          {
            sLine = sUnformattedLine.Substring(1).Trim();
          }
          else
          {
            sLine = sUnformattedLine.Trim();
          }

          string sFirstColumn = cHelper.Item(sLine, 0, ';').Trim();

          switch (iSectionType)
          {
            // Revision CSV have filled rows in column 2 (new spec), column 0 is always empty...
            case SectionType_Revision:
              if (cHelper.Item(sLine, 2, ';').Trim().Length == 0)
              {
                iSectionType = SectionType_None;
              }
              break;
            default:
              if (sFirstColumn.Length == 0)
	  				  {
                // 2020-03-09/TR May be empty lines in between
                //iSectionType = SectionType_None;
                continue;
              }
              break;
          }

          if (sFirstColumn.StartsWith("/"))
          {
						iSectionType = SectionType_None;
          }
          if (sFirstColumn.Equals("Rev. Datum:", StringComparison.OrdinalIgnoreCase))
					{
						iSectionType = SectionType_None;
            continue;
          }
          if (sFirstColumn.Equals("Revision date:", StringComparison.OrdinalIgnoreCase))
					{
						iSectionType = SectionType_None;
            continue;
          }
          // Aggregated status section determined from column headers (must be before 'continues' below)
          if (cHelper.Item(sLine, 2, ';').Trim().Equals("functionalPosition", StringComparison.OrdinalIgnoreCase) &&
            cHelper.Item(sLine, 3, ';').Trim().Equals("functionalState", StringComparison.OrdinalIgnoreCase))
          {
            LastSiteIdObject = null;
            iSectionType = SectionType_AggregatedStatus;
          }
          // Alarm section determined from column header
          if (cHelper.Item(sLine, 2, ';').Trim().Equals("alarmCodeId", StringComparison.OrdinalIgnoreCase))
          {
            LastSiteIdObject = null;
            iSectionType = SectionType_Alarm;
          }
          // Command section determined from column header
          if (cHelper.Item(sLine, 2, ';').Trim().Equals("commandCodeId", StringComparison.OrdinalIgnoreCase))
          {
            LastSiteIdObject = null;
            iSectionType = SectionType_Command;
          }
          // Status section determined from column header
          if (cHelper.Item(sLine, 2, ';').Trim().Equals("statusCodeId", StringComparison.OrdinalIgnoreCase))
          {
            LastSiteIdObject = null;
            iSectionType = SectionType_Status;
          }
          if (sFirstColumn.Equals("Objekttyp", StringComparison.OrdinalIgnoreCase))
          {
            continue;
          }
          if (sFirstColumn.Equals("Objekt typ", StringComparison.OrdinalIgnoreCase))
          {
            continue;
          }
          if (sFirstColumn.Equals("ObjectType", StringComparison.OrdinalIgnoreCase))
          {
            continue;
          }
          if (sFirstColumn.Equals("Object Type", StringComparison.OrdinalIgnoreCase))
          {
            continue;
          }
          switch (iSectionType)
          {
            case SectionType_RootObject:
              /*
              SiteIdObject.sSiteName = cHelper.Item(sLine, 0, ';').Trim();
              SiteIdObject.sSiteDescription = cHelper.Item(sLine, 1, ';').Trim();
              LastParent = treeView.Nodes.Add(SiteIdObject.sSiteName + " / " + SiteIdObject.sSiteDescription);
              LastParent.ToolTipText = SiteIdObject.sSiteName + "\r\n" + SiteIdObject.sSiteDescription;
               */
              break;

            case SectionType_GroupObject:
            case SectionType_SingleObject:
              RoadSideObject = new cRoadSideObject();
              RoadSideObject.bIsComponentGroup = (iSectionType == SectionType_GroupObject);
              RoadSideObject.sObjectType = cHelper.Item(sLine, 0, ';').Trim();
              RoadSideObject.sObject = cHelper.Item(sLine, 1, ';').Trim();
              RoadSideObject.sComponentId = cHelper.Item(sLine, 2, ';').Trim();
              RoadSideObject.sNTSObjectId = cHelper.Item(sLine, 3, ';').Trim();
              if (RoadSideObject.sComponentId.Length > 0 || RoadSideObject.sNTSObjectId.Length > 0)
              {
                // ! is not likely to be used and is stored in the INI-file to remember last selected node
                string sKey = RoadSideObject.sNTSObjectId + "\t" + RoadSideObject.sComponentId;
                if (RoadSideObjects.ContainsKey(sKey))
                {
                  RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "RoadSideObject ntsOId: {0}, cId: {1} in reference file '{2}' already exists",
                    RoadSideObject.sNTSObjectId, RoadSideObject.sComponentId, FileName);
                }
                else
                {


                  RoadSideObject.sExternalNTSId = cHelper.Item(sLine, 4, ';').Trim();
                  RoadSideObject.sDescription = cHelper.Item(sLine, 5, ';').Trim();
                  RoadSideObject.sFunctionalPosition = "";
                  RoadSideObject.sFunctionalState = "";
                  //if (LastSiteIdObject == null)
                  //{
                  //	LastSiteIdObject = FindOrCreateParentAndSiteId(treeView, LastSiteIdObject, RoadSideObject.sComponentId, RoadSideObject.sDescription, sSiteIdDescription);
                  //}
                  RoadSideObject.SiteIdObject = LastSiteIdObject;
                  treeNode = new TreeNode(RoadSideObject.sComponentId + " / " + RoadSideObject.sObject);
                  LastSiteIdObject.RoadSideObjects.Add(RoadSideObject);
                  treeNode.Tag = RoadSideObject;
                  treeNode.SelectedImageIndex = RoadSideObject.bIsComponentGroup ? 2 : 1;
                  treeNode.ImageIndex = treeNode.SelectedImageIndex;
                  //Objekttyp;Objekt;componentId;siteId;externalNtsId;Beskrivning
                  treeNode.ToolTipText = "Objekttyp: " + RoadSideObject.sObjectType + "\r\nObjekt: " + RoadSideObject.sObject + "\r\ncomponentId: " + RoadSideObject.sComponentId +
                    "\r\nNTSObjectId: " + RoadSideObject.sNTSObjectId + "\r\nsiteId: " + RoadSideObject.SiteIdObject.sSiteId + "\r\nexternalNtsId: " + RoadSideObject.sExternalNTSId + "\r\nDescription: " + RoadSideObject.sDescription;
                  RoadSideObject.Node = treeNode;
                  RoadSideObjects.Add(sKey, RoadSideObject);
                  LastSiteIdObject.RootNode.Nodes.Add(treeNode);
                  iLoadedObjects++;
                }
              }
              break;

            case SectionType_AggregatedStatus:
              // Aggregerad status;;;;;;;;
              // Rev. Datum:;2010-08-31;"Obs! ""-"" ska ej finnas med i fält";;;;
              // Objettyp;state;functionalPosition;functionalState;Kommentar;;
              cAggregatedStatusObject AggregatedStatusObject = new cAggregatedStatusObject();
              AggregatedStatusObject.sObjectType = cHelper.Item(sLine, 0, ';').Trim();
              AggregatedStatusObject.sDescription = cHelper.Item(sLine, 4, ';').Trim();
              AggregatedStatusObject.sFunctionalPositions = GetAllPositionsFromColumn(cHelper.Item(sLine, 2, ';'));
              AggregatedStatusObject.sFunctionalStates = GetAllPositionsFromColumn(cHelper.Item(sLine, 3, ';'));
              AggregatedStatusObjects.Add(AggregatedStatusObject);
              iLoadedAggregatedStatus++;
              break;

            case SectionType_Alarm:
              cAlarmObject AlarmObject = new cAlarmObject();
              AlarmObject.sObjectType = cHelper.Item(sLine, 0, ';').Trim();
              AlarmObject.sSpecificObject = cHelper.Item(sLine, 1, ';').Trim();
              AlarmObject.sAlarmCodeId = cHelper.Item(sLine, 2, ';').Trim();
              AlarmObject.sDescription = cHelper.Item(sLine, 3, ';').Trim();
              AlarmObject.sExternalAlarmCodeId = cHelper.Item(sLine, 4, ';').Trim();
              AlarmObject.sExternalNTSAlarmCodeId = cHelper.Item(sLine, 5, ';').Trim();
              AlarmObject.sPriority = cHelper.Item(sLine, 6, ';').Trim();
              AlarmObject.sCategory = cHelper.Item(sLine, 7, ';').Trim();
              if (AlarmObject.sCategory != "D" && AlarmObject.sCategory != "T")
              {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Alarm to Object Type {0}, Alarm Code Id {1} has an invalid Category '{2}', it will be ignored", AlarmObject.sObjectType, AlarmObject.sAlarmCodeId, AlarmObject.sCategory);
              }
              else
              {
                for (iIndex = 8, AlarmReturnValue = new cAlarmReturnValue(sLine, ref iIndex, false); AlarmReturnValue.IsValid();)
                {
                  AlarmObject.AlarmReturnValues.Add(AlarmReturnValue);
                  AlarmReturnValue = new cAlarmReturnValue(sLine, ref iIndex, false);
                }
                AlarmObjects.Add(AlarmObject);
                iLoadedAlarms++;
              }
              break;

            case SectionType_Command:
              // Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;;;;;;;;;;;;;;;;;;;;
              cCommandObject CommandObject = new cCommandObject();
              CommandObject.sObjectType = cHelper.Item(sLine, 0, ';').Trim();
              CommandObject.sSpecificObject = cHelper.Item(sLine, 1, ';').Trim();
              string sCommandCodeId = cHelper.Item(sLine, 2, ';').Trim();
              //CommandObject.sCommandCodeId = cHelper.Item(sLine, 2, ';').Trim();
              CommandObject.sDescription = cHelper.Item(sLine, 3, ';').Trim();
              for (iIndex = 4, CommandReturnValue = new cCommandReturnValue(sCommandCodeId, sLine, ref iIndex); CommandReturnValue.IsValid(); CommandObject.CommandReturnValues.Add(CommandReturnValue), CommandReturnValue = new cCommandReturnValue(sCommandCodeId, sLine, ref iIndex)) ;
              CommandObjects.Add(CommandObject);
              iLoadedCommands++;
              break;

            case SectionType_Status:
              // Object type;Object (optional);statusCodeId;description;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment;name;type;value;Comment
              cStatusObject StatusObject = new cStatusObject();
							StatusObject.sObjectType = cHelper.Item(sLine, 0, ';').Trim();
							StatusObject.sSpecificObject = cHelper.Item(sLine, 1, ';').Trim();
							StatusObject.sStatusCodeId = cHelper.Item(sLine, 2, ';').Trim();
              StatusObject.sDescription = cHelper.Item(sLine, 3, ';').Trim();
              for (iIndex = 4, StatusReturnValue = new cStatusReturnValue(StatusObject, sLine, ref iIndex); StatusReturnValue.IsValid(); StatusObject.StatusReturnValues.Add(StatusReturnValue), StatusReturnValue = new cStatusReturnValue(StatusObject, sLine, ref iIndex)) ;
							StatusObjects.Add(StatusObject);
							iLoadedStatus++;
              break;

            case SectionType_Revision:
              // ;;;;1.3;;;
              // Just take the last line...
              sSULRevision = cHelper.Item(sLine, 1, ';').Trim();
              break;

          }

          switch (sFirstColumn.ToLower())
          {
            case "siteid:":
              sSiteIdDescription = cHelper.Item(sLine, 2, ';').Trim();
              LastSiteIdObject = FindOrCreateParentAndSiteId(treeView, null, cHelper.Item(sLine, 1, ';').Trim(), cHelper.Item(sLine, 2, ';').Trim(), sSiteIdDescription);
              break;
            case "övervakade och styrda objekt i anläggningen":
            case "/rootobject":
              LastSiteIdObject = null;
              iSectionType = SectionType_RootObject;
              break;
            case "grupperade objekt":
            case "/groupobject":
            case "grouped objects":
              iSectionType = SectionType_GroupObject;
              break;
            case "enskilda objekt":
            case "/singleobject":
            case "single objects":
              iSectionType = SectionType_SingleObject;
              break;
            case "/alarm":
              LastSiteIdObject = null;
              iSectionType = SectionType_Alarm;
              break;
            case "/event":
              LastSiteIdObject = null;
              iSectionType = SectionType_Event;
              break;
            case "/command":
              LastSiteIdObject = null;
              iSectionType = SectionType_Command;
              break;
            case "/status":
              LastSiteIdObject = null;
              iSectionType = SectionType_Status;
              break;
            case "sxl revision:":
              LastSiteIdObject = null;
              iSectionType = SectionType_Revision;
              break;
            case "aggregerad status":
            case "/aggregatedstatus":
              LastSiteIdObject = null;
              iSectionType = SectionType_AggregatedStatus;
              break;
          }
          // Godkänd:;;Skapat datum:;;Revision:;Revideringsdatum:;;
          // ;;20XX-XX-XX;;1.1;20XX-XX-XX;;
          // ;;;;1.2;;;
          // ;;;;1.3;;;
          if (cHelper.Item(sLine, 4, ';').Trim().Equals("Revision", StringComparison.OrdinalIgnoreCase) ||
            cHelper.Item(sLine, 4, ';').Trim().Equals("Revision:", StringComparison.OrdinalIgnoreCase))
          {
            iSectionType = SectionType_Revision;
          }
				}

				swReferenceFile.Close();

				RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Loaded {0} objects, {1} alarms, {2} commands, {3} status and {4} agg.status from reference file '{5}'",
          iLoadedObjects, iLoadedAlarms, iLoadedCommands, iLoadedStatus, iLoadedAggregatedStatus, FileName);

				return true;

			}

			catch (Exception e)
			{
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not read reference file '{0}', reason: {1}", FileName, e.Message);
				return false;
			}

		}

    public List<string> GetAllPositionsFromColumn(string sColumn)
    {

      //
      // Rows are separated by LF and should really begin with '-'
      //
      if (sColumn.StartsWith("\""))
      {
        sColumn = sColumn.Substring(1);
      }

      if (sColumn.EndsWith("\""))
      {
        sColumn = sColumn.Substring(0, sColumn.Length - 1);
      }

      string[] sValues = sColumn.Split('\n');
			// Can also use \
			if (sValues.GetLength(0) == 1)
			{
				if (sValues[0].Contains("\\"))
				{
					sValues = sColumn.Split('\\');
				}
			}
      List<string> sReturnValues = new List<string>();

      foreach (string sValue in sValues)
      {
        string sNewValue = sValue;
        if (sValue.StartsWith("-"))
        {
          sNewValue = sValue.Substring(1);
        }
        sNewValue = sNewValue.Trim();
        if (sNewValue.Length > 0)
        {
          sReturnValues.Add(sNewValue);
        }
      }

      return sReturnValues;

    }

    public cSiteIdObject FindOrCreateParentAndSiteId(System.Windows.Forms.TreeView treeView, cSiteIdObject LastSiteIdObject, string sComponentId, string sDescription, string sDefaultSiteIdDescription)
    {
      
			System.Windows.Forms.TreeNode treeNode = null;
      cSiteIdObject SiteIdObject = LastSiteIdObject;

      string sSiteId = sComponentId;

      // Could be ex F+40100=416CG001, cut out 40100
      //if (sSiteId.IndexOf('+') > 0)
      //{
      //  sSiteId = sSiteId.Substring(sSiteId.IndexOf('+') + 1);
      //  if (sSiteId.IndexOf('=') > 0)
      //  {
      //    sSiteId = sSiteId.Substring(0, sSiteId.IndexOf('='));
      //  }
      //}

      // Was previous site id the same?
      if (LastSiteIdObject != null)
      {
        if (LastSiteIdObject.sSiteId.Equals(sSiteId, StringComparison.OrdinalIgnoreCase))
        {
          treeNode = LastSiteIdObject.RootNode;
          SiteIdObject = LastSiteIdObject;
        }
      }
      // Nope, find site id
      if (treeNode == null)
      {
        foreach (cSiteIdObject ScanSiteIdObject in SiteIdObjects)
        {
          if (ScanSiteIdObject.sSiteId.Equals(sSiteId, StringComparison.OrdinalIgnoreCase))
          {
            treeNode = ScanSiteIdObject.RootNode;
            SiteIdObject = ScanSiteIdObject;
            break;
          }
        }
      }
      // Did not exist, create a new root and site id object
      if (treeNode == null)
      {
        // Anläggning;37502_Anläggning;AB+37502=881CG001;AB+37502=881CG001;;Betalstation Klarastrandsleden infart;;;
        SiteIdObject = new cSiteIdObject();
        SiteIdObject.sSiteId = sSiteId;
        // First object may contain site id description, if we are lucky
        SiteIdObject.sDescription = sDescription;
        if (SiteIdObject.sDescription.Length == 0)
        {
          SiteIdObject.sDescription = sDefaultSiteIdDescription;
        }
        treeNode = treeView.Nodes.Add(sSiteId + " / " + SiteIdObject.sDescription);
        SiteIdObject.RootNode = treeNode;
        treeNode.Tag = SiteIdObject;
        SiteIdObjects.Add(SiteIdObject);
      }

      return SiteIdObject;

    }

    public void RemoveAllSubscriptions()
    {
      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        RoadSideObject.Subscriptions.Clear();
      }
    }

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

							if (AlarmObject.bSuspended != NewAlarmObject.bSuspended)
							{
								AlarmObject.bSuspended = NewAlarmObject.bSuspended;
								if (bWeAreConnected)
								{
									RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation_Suspend);
								}
							}

							if (AlarmObject.bActive != NewAlarmObject.bActive)
							{
								AlarmObject.bActive = NewAlarmObject.bActive;
								if (bWeAreConnected)
								{
									if (NewAlarmObject.bActive)
									{
										AlarmEvent = RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation_Alarm);
										if (AlarmEvent != null)
										{
											AlarmObject.AlarmEvents.Add(AlarmEvent);
                    }
                  }
									else
									{
										RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation_Alarm);
										AlarmObject.AlarmEvents.Clear();
									}
								}
							}

							if (AlarmObject.bAcknowledged != NewAlarmObject.bAcknowledged)
							{
								AlarmObject.bAcknowledged = NewAlarmObject.bAcknowledged;
								if (bWeAreConnected)
								{
									RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation_Acknowledge);
								}
								AlarmObject.AlarmEvents.Clear();
							}

              AlarmObject.AlarmCount = 0;

              foreach (cAlarmReturnValue AlarmReturnValue in AlarmObject.AlarmReturnValues)
              {
                AlarmReturnValue.sValue = cPrivateProfile.GetIniFileString(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".AlarmReturnValue", AlarmReturnValue.sValue);
              }

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
                string sStatus = cPrivateProfile.GetIniFileString(FileName, RoadSideObject.UniqueId() + ".Status", StatusObject.sStatusCodeId + "." + StatusReturnValue.sName + ".Status", "");
                StatusReturnValue.bRecentlyChanged = false;
                if (sStatus.Length > 0)
                {
                  StatusReturnValue.bRecentlyChanged = StatusReturnValue.sStatus.Equals(sStatus) ? false : true;
                  StatusReturnValue.sStatus = sStatus;
                }
              }
            }
            sS.Clear();
            foreach (cSubscription Subscription in RoadSideObject.Subscriptions)
            {
              if (Subscription.SubscribeStatus == cSubscription.Subscribe_OnChange)
              {
                if (Subscription.StatusReturnValue.bRecentlyChanged)
                {
                  RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                  s.sCI = Subscription.StatusObject.sStatusCodeId;
                  s.n = Subscription.StatusReturnValue.sName; // Subscription.StatusObject.StatusReturnValues .StatusReturnValues[iIndex].sName;
                  UpdateStatusValue(ref s, Subscription.StatusReturnValue.sType, Subscription.StatusReturnValue.sStatus);
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
            if (AlarmReturnValue.sValue != "?")
            {
              cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".Alarms", AlarmObject.sAlarmCodeId + ".AlarmReturnValue", AlarmReturnValue.sValue);
            }
          }
        }
				foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
				{
					foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
					{
						if (StatusReturnValue.sStatus != "?")
						{
							cPrivateProfile.WriteIniFileString(FileName, RoadSideObject.UniqueId() + ".Status", StatusObject.sStatusCodeId + "." + StatusReturnValue.sName + ".Status", StatusReturnValue.sStatus);
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

		public void UpdateStatusValue(ref RSMP_Messages.Status_VTQ s, string sType, string sStatus)
		{
			if (sStatus == null || sStatus == "?")
			{
				s.s = null;
				s.q = "unknown";
			}
			else
			{
        // Could be base64
        if (sType.Equals("base64", StringComparison.OrdinalIgnoreCase))
        {
          // Path?
          if (sStatus.Contains("\\"))
					{
            try
            {
              byte[] Base64Bytes = null;
              // Open file for reading 
              System.IO.FileStream fsBase64 = new System.IO.FileStream(sStatus, System.IO.FileMode.Open, System.IO.FileAccess.Read);
              System.IO.BinaryReader brBase64 = new System.IO.BinaryReader(fsBase64);
              long lBytes = new System.IO.FileInfo(sStatus).Length;
              Base64Bytes = brBase64.ReadBytes((Int32)lBytes);
              fsBase64.Close();
              fsBase64.Dispose();
              brBase64.Close();
              s.s = Convert.ToBase64String(Base64Bytes);
              if (s.s.Length > (cTcpSocketClientThread.BUFLENGTH - 100))
              {
								RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Base64 encoded packet is too big (" + Base64Bytes.GetLength(0).ToString() + " bytes), max buffer length is " + cTcpSocketClientThread.BUFLENGTH.ToString() + " bytes");
                s.s = null;
              }
              s.q = "recent";
            }
            catch (Exception e)
            {
							RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not base64-encode and send file '{0}', error {1}", sStatus, e.Message);
              s.q = "unknown";
            }
          }
          else
          {
            s.s = sStatus;
            s.q = "recent";
          }
        }
        else
        {
          s.s = sStatus;
          s.q = "recent";
        }
			}
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
          if (Subscription.SubscribeStatus == cSubscription.Subscribe_Interval)
          {
            if (DateTime.Compare(Subscription.LastUpdate.AddMilliseconds(Subscription.UpdateRate), DateTime.Now) <= 0)
            {
              RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
              s.sCI = Subscription.StatusObject.sStatusCodeId;
              s.n = Subscription.StatusReturnValue.sName; // Subscription.StatusObject.StatusReturnValues .StatusReturnValues[iIndex].sName;
              UpdateStatusValue(ref s, Subscription.StatusReturnValue.sType, Subscription.StatusReturnValue.sStatus);
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