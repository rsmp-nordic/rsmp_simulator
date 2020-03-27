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
    public string sNTSObjectId; // AB+37101=881CG001
    public string sExternalNTSId;
    public string sDescription;
    public bool[] bBitStatus = new bool[8];
    public string sFunctionalPosition;
    public string sFunctionalState;

    public List<cAlarmObject> AlarmObjects = new List<cAlarmObject>();
    public List<cCommandObject> CommandObjects = new List<cCommandObject>();
    public List<cStatusObject> StatusObjects = new List<cStatusObject>();

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public cSiteIdObject SiteIdObject;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public System.Windows.Forms.TreeNode Node;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public List<cCommandEvent> CommandEvents = new List<cCommandEvent>();

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public List<cStatusEvent> StatusEvents = new List<cStatusEvent>();

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public ListViewGroup StatusGroup;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public ListViewGroup AlarmsGroup;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public ListViewGroup CommandsGroup;

    public List<cAggregatedStatusEvent> AggregatedStatusEvents = new List<cAggregatedStatusEvent>();

		public string UniqueId()
		{
			return SiteIdObject.sSiteId + "." + sNTSObjectId + "." + sObjectType + "." + sObject + "." + sComponentId;
		}

		public cCommandObject getCommandObject(String commandID)
		{
			foreach (cCommandObject CommandoObject in CommandObjects)
			{
				if (CommandoObject.CommandReturnValues != null && CommandoObject.CommandReturnValues.Count > 0
						&& CommandoObject.CommandReturnValues[0].sCommandCodeId.Equals(commandID, StringComparison.OrdinalIgnoreCase))
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

	public class cAggregatedStatusEvent
	{
		public string sTimeStamp;
		public string sMessageId;
		public string sBitStatus;
		public string sFunctionalPosition;
		public string sFunctionalState;
	}

	public class cAlarmObject
	{

    [XmlIgnoreAttribute]
    [ScriptIgnore]
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

    [XmlIgnoreAttribute]
		[ScriptIgnore]
		public List<cAlarmReturnValue> AlarmReturnValues = new List<cAlarmReturnValue>();

    public List<cAlarmEvent> AlarmEvents = new List<cAlarmEvent>();

    public string StatusAsText()
		{
			string sStatus = "";
			if (bActive) sStatus += "Active, ";
			if (bSuspended) sStatus += "Suspended, ";
			if (bAcknowledged) sStatus += "Acked, ";
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
    public string sDirection;
    public string sEvent;
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
		//Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;

		public string sObjectType;
		public string sSpecificObject; // Optional, if set must match object in SUL
		public string sDescription;
		public List<cCommandReturnValue> CommandReturnValues = new List<cCommandReturnValue>();
		[XmlIgnoreAttribute]
		[ScriptIgnore]
		public cRoadSideObject RoadSideObject;

    /*

		public cRoadSideObject getRoadSideObject()
		{
			return RoadSideObject;
		}
		public void setRoadSideObject(cRoadSideObject rs)
		{
			this.RoadSideObject = rs;
		}
    */
		public void CloneFromCommandObject(cCommandObject CommandObjectToCopy)
		{
			RoadSideObject = CommandObjectToCopy.RoadSideObject;
			sObjectType = CommandObjectToCopy.sObjectType;
			sSpecificObject = CommandObjectToCopy.sSpecificObject;
			sDescription = CommandObjectToCopy.sDescription;
			CommandReturnValues.Clear();
			foreach (cCommandReturnValue CommandReturnValue in CommandObjectToCopy.CommandReturnValues)
			{
				cCommandReturnValue NewCommandReturnValue = new cCommandReturnValue();
				NewCommandReturnValue.sCommandCodeId = CommandReturnValue.sCommandCodeId;
				NewCommandReturnValue.sName = CommandReturnValue.sName;
				NewCommandReturnValue.sCommand = CommandReturnValue.sCommand;
				NewCommandReturnValue.sType = CommandReturnValue.sType;
				NewCommandReturnValue.sValue = CommandReturnValue.sValue;
				NewCommandReturnValue.sAge = CommandReturnValue.sAge;
				NewCommandReturnValue.sLastRecValue = CommandReturnValue.sLastRecValue;
				NewCommandReturnValue.sLastRecAge = CommandReturnValue.sLastRecAge;
				NewCommandReturnValue.sComment = CommandReturnValue.sComment;
        NewCommandReturnValue.CommandObject = this;
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
		// Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;
		public string sCommandCodeId;
		public string sName;
		public string sCommand;
		public string sType;
		public string sValue;
		public string sComment;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public cCommandObject CommandObject;

		[XmlIgnoreAttribute]
		[ScriptIgnore]
		public string sAge;

		[XmlIgnoreAttribute]
		[ScriptIgnore]
		public string sLastRecValue;

		[XmlIgnoreAttribute]
		[ScriptIgnore]
		public string sLastRecAge;

		public cCommandReturnValue(string sCommandCodeId, string sCompleteRow, ref int iIndex)
		{
			this.sCommandCodeId = sCommandCodeId;
			sName = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			sCommand = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			sType = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			sValue = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			sComment = cHelper.Item(sCompleteRow, iIndex++, ';').Replace("\"", "").Replace("\n", " ").Trim();
		}
		public cCommandReturnValue()
		{
		}
		public bool IsValid()
		{
			return sName.Length > 0;
		}
	}

	public class cCommandEvent
	{
		public string sTimeStamp;
		public string sMessageId;
		public string sEvent;
		public string sCommandCodeId;
		public string sName;
		public string sCommand;

		[XmlIgnoreAttribute]
		[ScriptIgnore]
		public string sValue;

		[XmlIgnoreAttribute]
		[ScriptIgnore]
		public string sAge;
	}

	public class cStatusObject
	{
    //Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;

    public string sObjectType;
		public string sSpecificObject; // Optional, if set must match object in SUL
    public string sStatusCodeId;
    public string sDescription;

		public List<cStatusReturnValue> StatusReturnValues = new List<cStatusReturnValue>();
		public List<cStatusSubscription> StatusSubscriptions = new List<cStatusSubscription>();

		[XmlIgnoreAttribute]
		[ScriptIgnore]
    public cRoadSideObject RoadSideObject;

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
				NewStatusReturnValue.sName = StatusReturnValue.sName;
				NewStatusReturnValue.sType = StatusReturnValue.sType;
				NewStatusReturnValue.sStatus = StatusReturnValue.sStatus;
				NewStatusReturnValue.sComment = StatusReturnValue.sComment;
				NewStatusReturnValue.sLastUpdateRate = StatusReturnValue.sLastUpdateRate;
				NewStatusReturnValue.sQuality = StatusReturnValue.sQuality;
				StatusReturnValues.Add(NewStatusReturnValue);
			}
		}
	}


	public class cStatusReturnValue
	{
		// Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;
		public string sName;
		public string sType;
		public string sStatus;
		public string sComment;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public string sValueRange;

    [XmlIgnoreAttribute]
		[ScriptIgnore]
		public string sQuality;

		[XmlIgnoreAttribute]
		[ScriptIgnore]
		public string sLastUpdateRate;

    [XmlIgnoreAttribute]
    [ScriptIgnore]
    public cStatusObject StatusObject;

    public cStatusReturnValue(cStatusObject ParentStatusObject, string sCompleteRow, ref int iIndex)
		{
      StatusObject = ParentStatusObject;
			sName = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
			sType = cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      sStatus = ""; // cHelper.Item(sCompleteRow, iIndex++, ';').Trim();
      sValueRange = cHelper.Item(sCompleteRow, iIndex++, ';').Trim(); // Usually [] wrapped possible values
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

	public class cStatusEvent
	{
		public string sTimeStamp;
		public string sMessageId;
		public string sEvent;
		public string sStatusCommandId;
		public string sName;
		public string sStatus;
		public string sUpdateRate;
		public string sQuality;
	}

	public class cStatusSubscription
	{
		public string sStatusCommandId;
		public string sName;
		public string sUpdateRate;
	}


	public class cProcessImage
	{

		static object LockObject = new Object();

		public const int SectionType_None = 0;
		public const int SectionType_RootObject = 1;
		public const int SectionType_GroupObject = 2;
		public const int SectionType_SingleObject = 3;
		public const int SectionType_Alarm = 4;
		public const int SectionType_Event = 5;
		public const int SectionType_Command = 6;
		public const int SectionType_Status = 7;
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

		public string[] sAggregatedStatusBitTexts = new string[8];

		public int MaxAlarmReturnValues = 0;

		public cProcessImage()
		{

		}

		public string sSULRevision = "";
		public int ObjectFilesTimeStamp = 0;

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
						bool bCopyThisAlarmObject = false;
						if (CommandObject.sSpecificObject.Length > 0)
						{
							if (CommandObject.sSpecificObject.Equals(RoadSideObject.sObject, StringComparison.OrdinalIgnoreCase))
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
        foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
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

			string sSiteId = "";
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
                  //LastSiteIdObject = FindOrCreateParentAndSiteId(treeView, LastSiteIdObject, RoadSideObject.sComponentId, RoadSideObject.sDescription, sSiteIdDescription);
                  RoadSideObject.SiteIdObject = LastSiteIdObject;
                  treeNode = new TreeNode(RoadSideObject.sComponentId + " / " + RoadSideObject.sObject);
                  LastSiteIdObject.RoadSideObjects.Add(RoadSideObject);
                  treeNode.Tag = RoadSideObject;
                  treeNode.SelectedImageIndex = RoadSideObject.bIsComponentGroup ? 2 : 1;
                  treeNode.ImageIndex = treeNode.SelectedImageIndex;
                  //Objekttyp;Objekt;componentId;siteId;externalNtsId;Beskrivning
                  treeNode.ToolTipText = "Objekttyp: " + RoadSideObject.sObjectType + "\r\nObjekt: " + RoadSideObject.sObject + "\r\ncomponentId: " + RoadSideObject.sComponentId +
                    "\r\nNTSObjectId: " + RoadSideObject.sNTSObjectId + "\r\nsiteId: " + RoadSideObject.SiteIdObject.sSiteId + "\r\nexternalNtsId: " + RoadSideObject.sExternalNTSId + "\r\nBeskrivning: " + RoadSideObject.sDescription;
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
                //for (iIndex = 8, AlarmReturnValue = new cAlarmReturnValue(sLine, ref iIndex, false); AlarmReturnValue.IsValid(); AlarmObject.AlarmReturnValues.Add(AlarmReturnValue), AlarmReturnValue = new cAlarmReturnValue(sLine, ref iIndex, false)) ;
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
							CommandObject.sDescription = cHelper.Item(sLine, 3, ';').Trim();
							for (iIndex = 4, CommandReturnValue = new cCommandReturnValue(sCommandCodeId, sLine, ref iIndex); CommandReturnValue.IsValid(); CommandObject.CommandReturnValues.Add(CommandReturnValue), CommandReturnValue = new cCommandReturnValue(sCommandCodeId, sLine, ref iIndex)) ;
							CommandObjects.Add(CommandObject);
							iLoadedCommands++;
							break;

						case SectionType_Status:
							// Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;;;;;;;;;;;;;;;;;;;;
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
						case "siteid":
							sSiteId = cHelper.Item(sLine, 1, ';').Trim();
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
						case "sxl revision":
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

		public bool SaveReferenceFile(string FileName, string sFileExtension)
		{

			try
			{

				string sSerialized = "";

				switch (sFileExtension)
				{
					case ".json":
						sSerialized = RSMPGS.JSon.JSonSerializer.SerializeObject(SiteIdObjects);
						break;

					case ".xml":
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<cSiteIdObject>));
						StringWriter textWriter = new StringWriter();
						xmlSerializer.Serialize(textWriter, SiteIdObjects);
						sSerialized = textWriter.ToString();
						break;

				}
				File.WriteAllText(FileName, sSerialized);

				return true;

			}

			catch (Exception e)
			{

				RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not save file '{0}', reason: {1}", FileName, e.Message);

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

			//Could be ex F+40100=416CG001, cut out 40100
			//if (sSiteId.IndexOf('+') > 0)
			//{
			//    sSiteId = sSiteId.Substring(sSiteId.IndexOf('+') + 1);
			//    if (sSiteId.IndexOf('=') > 0)
			//    {
			//        sSiteId = sSiteId.Substring(0, sSiteId.IndexOf('='));
			//    }
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


		public void LoadProcessImageValues(string FileName)
		{

			try
			{
				if (!File.Exists(FileName))
				{
					var hFile = File.Create(FileName);
					hFile.Close();
				}
			}
			catch (Exception e)
			{
				RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to create file {0}", e.Message, FileName);
			}

			try
			{
				StreamReader swReferenceFile = new StreamReader((System.IO.Stream)File.OpenRead(FileName), Encoding.UTF8);


				string sLine;

				while ((sLine = swReferenceFile.ReadLine()) != null)
				{
					cRoadSideObject RoadSideObject;
					sLine = sLine.Trim();
					if (sLine.StartsWith(";") || sLine.Length == 0 || sLine.StartsWith("["))
					{
						continue;
					}
					try
					{
						RoadSideObject = RSMPGS.JSon.JSonSerializer.Deserialize<cRoadSideObject>(sLine);
					}
					catch
					{
						continue;
					}

          cRoadSideObject gRoadSide = cHelper.FindRoadSideObject(RoadSideObject.sNTSObjectId, RoadSideObject.sComponentId, false);

					if (gRoadSide != null)
					{
						gRoadSide.bBitStatus = RoadSideObject.bBitStatus;

						//foreach (cAlarmEvent AlarmEvent in RoadSideObject.AlarmEvents)
						//{
						//	gRoadSide.AlarmEvents.Add(AlarmEvent);
						//}
						foreach (cCommandEvent CommandEvent in RoadSideObject.CommandEvents)
						{
							gRoadSide.CommandEvents.Add(CommandEvent);
						}
						foreach (cStatusEvent StatusEvent in RoadSideObject.StatusEvents)
						{
							gRoadSide.StatusEvents.Add(StatusEvent);
						}
						foreach (cAggregatedStatusEvent AggregatedStatusEvent in RoadSideObject.AggregatedStatusEvents)
						{
							gRoadSide.AggregatedStatusEvents.Add(AggregatedStatusEvent);
						}
						foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
						{
							if (StatusObject.StatusReturnValues != null && StatusObject.StatusReturnValues.Count > 0)
							{
                cStatusObject StatusObject2 = gRoadSide.getStatusObject(StatusObject.sStatusCodeId); // .StatusReturnValues[0].sStatusCommandId);
								if (StatusObject2 != null)
								{
									foreach (cStatusReturnValue StatusReturnValues in StatusObject.StatusReturnValues)
									{
										cStatusReturnValue StatusReturnValues2 = StatusObject2.getStatusReturnValueByName(StatusReturnValues.sName);
										if (StatusReturnValues2 != null)
										{
											StatusReturnValues2.sLastUpdateRate = StatusReturnValues.sLastUpdateRate;
											StatusReturnValues2.sStatus = StatusReturnValues.sStatus;
											StatusReturnValues2.sQuality = StatusReturnValues.sQuality;
										}
									}
								}
							}

						}
						//foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
						//{
						//    cAlarmObject AlarmObject2 = gRoadSide.getAlarmObject(AlarmObject.sAlarmCodeId);
						//    if (AlarmObject2 != null)
						//    {
						//        AlarmObject2.bAcknowledged = AlarmObject.bAcknowledged;
						//        AlarmObject2.bActive = AlarmObject.bActive;
						//        AlarmObject2.bSuspended = AlarmObject.bSuspended;
						//    }
						//}

						foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
						{
							if (CommandObject.CommandReturnValues != null && CommandObject.CommandReturnValues.Count > 0)
							{
								cCommandObject CommandObject2 = gRoadSide.getCommandObject(CommandObject.CommandReturnValues[0].sCommandCodeId);
								if (CommandObject2 != null)
								{
									foreach (cCommandReturnValue CommandReturnValue in CommandObject.CommandReturnValues)
									{
										cCommandReturnValue CommandReturnValue2 = CommandObject2.getCommandReturnValueByName(CommandReturnValue.sName);
										if (CommandReturnValue2 != null)
										{
											CommandReturnValue2.sLastRecAge = CommandReturnValue.sLastRecAge;
											CommandReturnValue2.sLastRecValue = CommandReturnValue.sLastRecValue;
											CommandReturnValue2.sAge = CommandReturnValue.sAge;
											CommandReturnValue2.sValue = CommandReturnValue.sValue;
										}
									}
								}
							}

						}
					}
					else
					{
						//System.Windows.Forms.MessageBox.Show("Did not find roadside object");
					}
				}

        swReferenceFile.Close();
			}
			catch (Exception e)
			{
				RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to open file {0}", e.Message, FileName);
			}
		}


		public void SaveProcessImageValues()
		{

			string FileName = cPrivateProfile.SettingsPath() + "\\" + "ProcessImage.dat";

			try
			{

				StreamWriter swProcessImageFile = new StreamWriter((System.IO.Stream)File.OpenWrite(FileName), Encoding.UTF8);

				swProcessImageFile.WriteLine(";");
				swProcessImageFile.WriteLine("; ProcessImage stored " + DateTime.Now.ToString());
				swProcessImageFile.WriteLine(";");

				swProcessImageFile.WriteLine("[General]");
				swProcessImageFile.WriteLine("");
				foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
				{
					swProcessImageFile.WriteLine(RSMPGS.JSon.JSonSerializer.SerializeObject(RoadSideObject));
				}
				RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Stored Process Image to '{0}'", FileName);
				swProcessImageFile.Close();
			}
			catch (Exception e)
			{
				RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Process Image could not be stored to '{0}'", FileName, e.Message);
			}

		}

		public void ResetProcessImage()
		{
			foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
			{

        RoadSideObject.StatusEvents.Clear();
				RoadSideObject.CommandEvents.Clear();
				RoadSideObject.AggregatedStatusEvents.Clear();
				RoadSideObject.bBitStatus = null;
				RoadSideObject.sFunctionalPosition = null;
				RoadSideObject.sFunctionalState = null;

				//foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
				//{
				//    AlarmObject.bAcknowledged = false;
				//    AlarmObject.bActive = false;
				//    AlarmObject.bSuspended = false;
				//}

				foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
				{
					foreach (cStatusReturnValue StatusReturnValues in StatusObject.StatusReturnValues)
					{
						StatusReturnValues.sQuality = null;
						StatusReturnValues.sStatus = null;
						StatusReturnValues.sLastUpdateRate = null;
					}
				}
				foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
				{
					foreach (cCommandReturnValue CommandReturnValue in CommandObject.CommandReturnValues)
					{
						CommandReturnValue.sLastRecAge = null;
						CommandReturnValue.sLastRecValue = null;
					}
				}
				foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
				{
          AlarmObject.AlarmEvents.Clear();
          AlarmObject.bAcknowledged = false;
					AlarmObject.bActive = false;
					AlarmObject.bSuspended = false;
        }
			}
		}

		public void ReSendSubscriptions(bool bSubscribe)
		{
			foreach (cRoadSideObject RoadSideObject in RoadSideObjects.Values)
			{
				List<cStatusReturnValue> Status = new List<cStatusReturnValue>();

				foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
				{
					foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
					{

						if (StatusReturnValue.sLastUpdateRate != null)
						{
							Status.Add(StatusReturnValue);
						}
					}
				}
				if (Status.Count > 0)
				{
					if (bSubscribe) RSMPGS.JSon.CreateAndSendSubscriptionMessage(RoadSideObject, Status);
					else RSMPGS.JSon.CreateAndSendStatusMessage(RoadSideObject, Status, "StatusUnsubscribe");
				}
			}
		}

		public void CyclicCleanup(int iElapsedMillisecs)
		{

		}

	}

}