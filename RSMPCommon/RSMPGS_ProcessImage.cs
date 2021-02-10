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
using System.Linq;
using System.Diagnostics;

namespace nsRSMPGS
{

    public partial class cProcessImage
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

        // These ones are flat
        public Dictionary<string, cRoadSideObject> RoadSideObjects = new Dictionary<string, cRoadSideObject>(StringComparer.InvariantCultureIgnoreCase);
        public Dictionary<string, cValueTypeObject> ValueTypeObjects = new Dictionary<string, cValueTypeObject>(StringComparer.InvariantCultureIgnoreCase);

        public List<cAlarmObject> AlarmObjects = new List<cAlarmObject>();
        public List<cCommandObject> CommandObjects = new List<cCommandObject>();
        public List<cStatusObject> StatusObjects = new List<cStatusObject>();
        public List<cAggregatedStatusObject> AggregatedStatusObjects = new List<cAggregatedStatusObject>();

        public string[] sAggregatedStatusBitTexts = new string[8];

        public int MaxAlarmReturnValues = 0;

        public string sSULRevision = "";
        public int ObjectFilesTimeStamp = 0;

#if _RSMPGS1

        public List<cBufferedMessage> BufferedMessages = new List<cBufferedMessage>();

        /*
        public List<cBufferedPacket> BufferedAlarms = new List<cBufferedPacket>();
        public List<cBufferedPacket> BufferedAggregatedStatus = new List<cBufferedPacket>();
        public List<cBufferedPacket> BufferedStatusUpdates = new List<cBufferedPacket>();
        */
#endif

        public enum ObjectFileType
        {
            CSVfiles = 0,
            YAMLfile = 1
        }

        public void Clear()
        {

            SiteIdObjects.Clear();
            RoadSideObjects.Clear();
            AlarmObjects.Clear();
            CommandObjects.Clear();
            StatusObjects.Clear();
            AggregatedStatusObjects.Clear();
            MaxAlarmReturnValues = 0;
            sSULRevision = "";
            ObjectFilesTimeStamp = 0;
            ValueTypeObjects.Clear();

#if _RSMPGS1

            BufferedMessages.Clear();
            RSMPGS.MainForm.ListView_BufferedMessages.Items.Clear();
            //BufferedAlarms.Clear();
            //BufferedAggregatedStatus.Clear();
            //BufferedStatusUpdates.Clear();

#endif

        }

        public int LoadYAMLFile(string sFileName)
        {

            int iLoadedAlarms = 0;
            int iLoadedCommands = 0;
            int iLoadedStatus = 0;
            int iLoadedAggregatedStatus = 0;
            int iLoadedObjects = 0;

            //Dictionary<string, string> YAMLScalars = new Dictionary<string, string>();
            //Dictionary<string, cYAMLMapping> YAMLMappings = new Dictionary<string, cYAMLMapping>();

            List<string> sFileLines;

            try
            {
                sFileLines = File.ReadAllLines(sFileName).ToList<string>();
            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not read SXL (YAML) file '{0}', reason: {1}", sFileName, e.Message);
                return 0;
            }

            DateTime dtStartTime = new DateTime(1970, 1, 1);
            UInt32 uObjectFilesTimeStamp = 0;

            uObjectFilesTimeStamp += Convert.ToUInt32(Math.Abs((File.GetCreationTime(sFileName) - dtStartTime).TotalSeconds));
            uObjectFilesTimeStamp += Convert.ToUInt32(Math.Abs((File.GetLastWriteTime(sFileName) - dtStartTime).TotalSeconds));

            int iReadFiles = 1;

            cYAMLMapping YAML = cYAMLParser.GetYAMLMappings(sFileLines);

            // ProcessImage.sId = YAML.GetScalar("id");

            sSULRevision = YAML.GetScalar("version");

            //ProcessImage.sDescription = YAML.GetScalar("description");
            //ProcessImage.sConstructor = YAML.GetScalar("constructor");
            //ProcessImage.sCreatedDate = YAML.GetScalar("created-date");
            //ProcessImage.sDate = YAML.GetScalar("date");
            //ProcessImage.sRSMPVersion = YAML.GetScalar("rsmp-version");

            cYAMLMapping YAMLObjectTypes;
            cYAMLMapping YAMLSites;

            YAML.YAMLMappings.TryGetValue("objects", out YAMLObjectTypes);

            if (YAMLObjectTypes != null)
            {
                foreach (cYAMLMapping YAMLObjectType in YAMLObjectTypes.YAMLMappings.Values)
                {

                    cYAMLMapping AggregatedStatusType;
                    cYAMLMapping AlarmsType;

                    if (YAMLObjectType.YAMLMappings.TryGetValue("aggregated_status", out AggregatedStatusType))
                    {
                        cAggregatedStatusObject AggregatedStatusObject = new cAggregatedStatusObject();
                        AggregatedStatusObject.sFunctionalPositions = GetAllPositions(YAMLObjectType.GetScalar("functional_position"));
                        AggregatedStatusObject.sFunctionalStates = GetAllPositions(YAMLObjectType.GetScalar("functional_state"));
                        AggregatedStatusObject.sDescription = YAMLObjectType.GetScalar("description");
                        AggregatedStatusObject.sObjectType = YAMLObjectType.sMappingName;
                        AggregatedStatusObjects.Add(AggregatedStatusObject);
                    }

                    // Extract value types
                    foreach (cYAMLMapping ObjectTypeObject in YAMLObjectType.YAMLMappings.Values)
                    {
                        if (ObjectTypeObject.sMappingName.Equals("alarms", StringComparison.OrdinalIgnoreCase) ||
                          ObjectTypeObject.sMappingName.Equals("statuses", StringComparison.OrdinalIgnoreCase) ||
                          ObjectTypeObject.sMappingName.Equals("commands", StringComparison.OrdinalIgnoreCase))
                        {
                            foreach (cYAMLMapping ObjectTypeObjectItem in ObjectTypeObject.YAMLMappings.Values)
                            {
                                cYAMLMapping YAMLArguments;

                                if (ObjectTypeObjectItem.YAMLMappings.TryGetValue("arguments", out YAMLArguments))
                                {
                                    foreach (cYAMLMapping YAMLArgument in YAMLArguments.YAMLMappings.Values)
                                    {

                                        string sSpecificObject = ObjectTypeObjectItem.GetScalar("object");

                                        string sValueTypeKey = YAMLObjectType.sMappingName + "\t" + ObjectTypeObject.sMappingName + "\t" + ObjectTypeObjectItem.sMappingName + "\t" + sSpecificObject + "\t" + YAMLArgument.sMappingName;

                                        string sType = YAMLArgument.GetScalar("type");
                                        string sRange = YAMLArgument.GetScalar("range");

                                        string sDescription = YAMLArgument.GetScalar("description");

                                        string sDebug = "NAME: " + YAMLArgument.GetFullPath();
                                        if (YAMLArgument.GetFullPath().Contains("S0020"))
                                        {

                                        }

                                        foreach (KeyValuePair<string, string> kvp in YAMLArgument.YAMLScalars)
                                        {
                                            sDebug += "\r\nSCALAR: " + kvp.Key + " = " + kvp.Value.Replace("\n", "\n\t");
                                        }

                                        foreach (KeyValuePair<string, cYAMLMapping> kvp in YAMLArgument.YAMLMappings)
                                        {
                                            sDebug += "\r\nMAPPING: " + kvp.Key + " = " + kvp.Value.sMappingName;
                                        }

                                        Debug.WriteLine(sDebug + "\r\n");

                                        if (sType.Length > 0)
                                        {
                                            cValueTypeObject ValueTypeObject;
                                            cYAMLMapping Values;
                                            if (YAMLArgument.YAMLMappings.TryGetValue("values", out Values))
                                            {
                                                ValueTypeObject = new cValueTypeObject(sValueTypeKey, YAMLArgument.sMappingName, sType, sRange, Values.YAMLScalars, sDescription);
                                            }
                                            else
                                            {
                                                ValueTypeObject = new cValueTypeObject(sValueTypeKey, YAMLArgument.sMappingName, sType, sRange, null, sDescription);
                                            }
                                            if (ValueTypeObjects.ContainsKey(sValueTypeKey))
                                            {
                                                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Value already exists: " + sValueTypeKey.Replace("\t", "/"));
                                            }
                                            else
                                            {
                                                ValueTypeObjects.Add(sValueTypeKey, ValueTypeObject);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }


                    if (YAMLObjectType.YAMLMappings.TryGetValue("alarms", out AlarmsType))
                    {
                        foreach (cYAMLMapping AlarmTypeType in AlarmsType.YAMLMappings.Values)
                        {
                            cYAMLMapping Arguments;
                            if (AlarmTypeType.YAMLMappings.TryGetValue("arguments", out Arguments))
                            {
                                if (MaxAlarmReturnValues < Arguments.YAMLMappings.Count)
                                {
                                    MaxAlarmReturnValues = Arguments.YAMLMappings.Count;
                                }
                            }
                        }
                    }






                }
            }

            if (YAML.YAMLMappings.TryGetValue("sites", out YAMLSites) == false)
            {
                return 0;
            }

            foreach (cYAMLMapping YAMLSite in YAMLSites.YAMLMappings.Values)
            {

                cSiteIdObject SiteIdObject = new cSiteIdObject();

                SiteIdObject.sSiteId = YAMLSite.sMappingName;
                SiteIdObject.sDescription = YAMLSite.GetScalar("description");

                TreeNode treeNode = RSMPGS.MainForm.treeView_SitesAndObjects.Nodes.Add(SiteIdObject.sSiteId + " / " + SiteIdObject.sDescription);
                SiteIdObject.RootNode = treeNode;
                treeNode.Tag = SiteIdObject;

                SiteIdObjects.Add(SiteIdObject);

                cYAMLMapping YAMLSiteObjects;

                if (YAMLSite.YAMLMappings.TryGetValue("objects", out YAMLSiteObjects) == false)
                {
                    continue;
                }

                foreach (cYAMLMapping YAMLSiteObject in YAMLSiteObjects.YAMLMappings.Values)
                {
                    cYAMLMapping YAMLSiteObjectType;

                    if (YAMLObjectTypes == null || YAMLObjectTypes.YAMLMappings.TryGetValue(YAMLSiteObject.sMappingName, out YAMLSiteObjectType) == false)
                    {
                        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Could not find object type: " + YAMLSiteObject.sMappingName);
                        continue;
                    }

                    foreach (cYAMLMapping YAMLSiteObjectObject in YAMLSiteObject.YAMLMappings.Values)
                    {

                        cRoadSideObject RoadSideObject = new cRoadSideObject();

                        RoadSideObject.sComponentId = YAMLSiteObjectObject.GetScalar("componentId");
                        RoadSideObject.sNTSObjectId = YAMLSiteObjectObject.GetScalar("ntsObjectId");
                        RoadSideObject.sExternalNTSId = YAMLSiteObjectObject.GetScalar("externalNtsId");

                        RoadSideObject.sObject = YAMLSiteObjectObject.sMappingName;
                        RoadSideObject.sObjectType = YAMLSiteObject.sMappingName;

                        // Not existing ??? Yes, but what is the name ??
                        //RoadSideObject.sExternalNTSId = cHelper.Item(sLine, 4, ';').Trim();

                        // Not existing ?
                        RoadSideObject.sDescription = YAMLSiteObjectObject.GetScalar("description");

                        RoadSideObject.bIsComponentGroup = YAMLSiteObjectType.YAMLMappings.ContainsKey("aggregated_status");

                        if (RoadSideObject.sComponentId.Length == 0 && RoadSideObject.sNTSObjectId.Length == 0)
                        {
                            continue;
                        }

                        string sKey = RoadSideObject.sNTSObjectId + "\t" + RoadSideObject.sComponentId;

                        if (RoadSideObjects.ContainsKey(sKey))
                        {
                            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "RoadSideObject ntsOId: {0}, cId: {1} in SXL (YAML) file '{2}' already exists",
                              RoadSideObject.sNTSObjectId, RoadSideObject.sComponentId, sFileName);
                            continue;
                        }

                        RoadSideObject.sFunctionalPosition = "";
                        RoadSideObject.sFunctionalState = "";

                        RoadSideObject.SiteIdObject = SiteIdObject;
                        treeNode = new TreeNode(RoadSideObject.sComponentId + " / " + RoadSideObject.sObject);
                        SiteIdObject.RoadSideObjects.Add(RoadSideObject);
                        treeNode.Tag = RoadSideObject;
                        treeNode.SelectedImageIndex = RoadSideObject.bIsComponentGroup ? 2 : 1;
                        treeNode.ImageIndex = treeNode.SelectedImageIndex;

                        treeNode.ToolTipText = "Object type: " + RoadSideObject.sObjectType + "\r\nObject: " + RoadSideObject.sObject + "\r\ncomponentId: " + RoadSideObject.sComponentId +
                          "\r\nNTSObjectId: " + RoadSideObject.sNTSObjectId + "\r\nsiteId: " + RoadSideObject.SiteIdObject.sSiteId + "\r\nexternalNtsId: " + RoadSideObject.sExternalNTSId + "\r\nDescription: " + RoadSideObject.sDescription;
                        RoadSideObject.Node = treeNode;
                        RoadSideObjects.Add(sKey, RoadSideObject);
                        SiteIdObject.RootNode.Nodes.Add(treeNode);

                        iLoadedObjects++;

                        cYAMLMapping YAMLAlarms;
                        cYAMLMapping YAMLStatuses;
                        cYAMLMapping YAMLCommands;

                        if (YAMLSiteObjectType.YAMLMappings.TryGetValue("alarms", out YAMLAlarms))
                        {

                            foreach (cYAMLMapping YAMLAlarm in YAMLAlarms.YAMLMappings.Values)
                            {
                                cAlarmObject AlarmObject = new cAlarmObject();

                                AlarmObject.RoadSideObject = RoadSideObject;

                                AlarmObject.sObjectType = YAMLSiteObjectType.sMappingName;

                                // What is the exported name, check David's code ??
                                string sSpecificObject = YAMLAlarm.GetScalar("object");

                                if (sSpecificObject.Length > 0)
                                {
                                    if (sSpecificObject.Equals(RoadSideObject.sObject, StringComparison.OrdinalIgnoreCase) == false)
                                    {
                                        continue;
                                    }
                                }

                                AlarmObject.sAlarmCodeId = YAMLAlarm.sMappingName;
                                AlarmObject.sDescription = YAMLAlarm.GetScalar("description");
                                AlarmObject.sExternalAlarmCodeId = YAMLAlarm.GetScalar("externalAlarmCodeId");
                                AlarmObject.sExternalNTSAlarmCodeId = YAMLAlarm.GetScalar("externalNTSAlarmCodeId");
                                AlarmObject.sPriority = YAMLAlarm.GetScalar("priority");
                                AlarmObject.sCategory = YAMLAlarm.GetScalar("category");

                                cYAMLMapping YAMLArguments;

                                if (YAMLAlarm.YAMLMappings.TryGetValue("arguments", out YAMLArguments))
                                {
                                    foreach (cYAMLMapping YAMLArgument in YAMLArguments.YAMLMappings.Values)
                                    {
                                        cAlarmReturnValue AlarmReturnValue = new cAlarmReturnValue();

                                        AlarmReturnValue.sName = YAMLArgument.sMappingName;
                                        //AlarmReturnValue.sType = YAMLArgument.GetScalar("type");
                                        AlarmReturnValue.sComment = YAMLArgument.GetScalar("description");

                                        if (AlarmReturnValue.sComment.Contains("Type of detector"))
                                        {

                                        }
                                        string sValueTypeKey = YAMLSiteObjectType.sMappingName + "\t" + YAMLAlarms.sMappingName + "\t" + YAMLAlarm.sMappingName + "\t" + sSpecificObject + "\t" + YAMLArgument.sMappingName;

                                        AlarmReturnValue.Value = new cValue(sValueTypeKey, true);

                                        //AlarmReturnValue.sValues = YAMLArgument.GetScalar("range");

                                        //      AlarmObject.Value = CreateValueAndType("alarms", AlarmObject.sObjectType, sSpecificObject, AlarmObject.sAlarmCodeId,
                                        //        YAMLArgument.sMappingName, YAMLArgument.GetScalar("type"), YAMLArgument.GetScalar("description"));

                                        if (AlarmReturnValue.sName != "")
                                        {
                                            AlarmObject.AlarmReturnValues.Add(AlarmReturnValue);
                                        }
                                    }
                                }
                                // 2020-11-27/TR Load anyway warn only
                                if (AlarmObject.sCategory != "D" && AlarmObject.sCategory != "T")

                                {
                                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Alarm to Object Type {0}, Alarm Code Id {1} has an invalid Category '{2}', it will be ignored", AlarmObject.sObjectType, AlarmObject.sAlarmCodeId, AlarmObject.sCategory);
                                }
                                //else
                                //{
                                RoadSideObject.AlarmObjects.Add(AlarmObject);
                                iLoadedAlarms++;
                                //}

                            }
                        }

                        if (YAMLSiteObjectType.YAMLMappings.TryGetValue("statuses", out YAMLStatuses))
                        {

                            foreach (cYAMLMapping YAMLStatus in YAMLStatuses.YAMLMappings.Values)
                            {

                                cStatusObject StatusObject = new cStatusObject();

                                StatusObject.RoadSideObject = RoadSideObject;

                                StatusObject.sObjectType = YAMLSiteObjectType.sMappingName;

                                // What is the exported name, check David's code ??
                                string sSpecificObject = YAMLStatus.GetScalar("object");

                                if (sSpecificObject.Length > 0)
                                {
                                    if (sSpecificObject.Equals(RoadSideObject.sObject, StringComparison.OrdinalIgnoreCase) == false)
                                    {
                                        continue;
                                    }
                                }

                                StatusObject.sStatusCodeId = YAMLStatus.sMappingName;
                                StatusObject.sDescription = YAMLStatus.GetScalar("description");

                                cYAMLMapping YAMLArguments;

                                if (YAMLStatus.YAMLMappings.TryGetValue("arguments", out YAMLArguments))
                                {
                                    foreach (cYAMLMapping YAMLArgument in YAMLArguments.YAMLMappings.Values)
                                    {
                                        cStatusReturnValue StatusReturnValue = new cStatusReturnValue(StatusObject);

                                        StatusReturnValue.sName = YAMLArgument.sMappingName;
                                        //StatusReturnValue.sType = YAMLArgument.GetScalar("type");
                                        StatusReturnValue.sComment = YAMLArgument.GetScalar("description");

                                        string sValueTypeKey = YAMLSiteObjectType.sMappingName + "\t" + YAMLStatuses.sMappingName + "\t" + YAMLStatus.sMappingName + "\t" + sSpecificObject + "\t" + YAMLArgument.sMappingName;

                                        StatusReturnValue.Value = new cValue(sValueTypeKey, true);

                                        if (StatusReturnValue.sName != "")
                                        {
                                            StatusObject.StatusReturnValues.Add(StatusReturnValue);
                                        }
                                    }
                                }
                                RoadSideObject.StatusObjects.Add(StatusObject);
                                iLoadedStatus++;

                            }
                        }

                        if (YAMLSiteObjectType.YAMLMappings.TryGetValue("commands", out YAMLCommands))
                        {

                            foreach (cYAMLMapping YAMLCommand in YAMLCommands.YAMLMappings.Values)
                            {

                                cCommandObject CommandObject = new cCommandObject();

                                CommandObject.RoadSideObject = RoadSideObject;

                                CommandObject.sObjectType = YAMLSiteObjectType.sMappingName;

                                // What is the exported name, check David's code ??
                                string sSpecificObject = YAMLCommand.GetScalar("object");

                                if (sSpecificObject.Length > 0)
                                {
                                    if (sSpecificObject.Equals(RoadSideObject.sObject, StringComparison.OrdinalIgnoreCase) == false)
                                    {
                                        continue;
                                    }
                                }

                                CommandObject.sCommandCodeId = YAMLCommand.sMappingName;

                                CommandObject.sDescription = YAMLCommand.GetScalar("description");

                                cYAMLMapping YAMLArguments;

                                if (YAMLCommand.YAMLMappings.TryGetValue("arguments", out YAMLArguments))
                                {
                                    foreach (cYAMLMapping YAMLArgument in YAMLArguments.YAMLMappings.Values)
                                    {
                                        cCommandReturnValue CommandReturnValue = new cCommandReturnValue(CommandObject);

                                        CommandReturnValue.sName = YAMLArgument.sMappingName;
                                        // Actually in the level above...
                                        CommandReturnValue.sCommand = YAMLCommand.GetScalar("command");
                                        //CommandReturnValue.sType = YAMLArgument.GetScalar("type");
                                        //CommandReturnValue.sValue = YAMLArgument.GetScalar("values");
                                        CommandReturnValue.sComment = YAMLArgument.GetScalar("description");

                                        string sValueTypeKey = YAMLSiteObjectType.sMappingName + "\t" + YAMLCommands.sMappingName + "\t" + YAMLCommand.sMappingName + "\t" + sSpecificObject + "\t" + YAMLArgument.sMappingName;

                                        CommandReturnValue.Value = new cValue(sValueTypeKey, true);

                                        if (CommandReturnValue.sName != "")
                                        {
                                            CommandObject.CommandReturnValues.Add(CommandReturnValue);
                                        }
                                    }
                                }
                                RoadSideObject.CommandObjects.Add(CommandObject);
                                iLoadedCommands++;

                            }

                        }

                    }

                }
                /*
            public List<cAlarmObject> AlarmObjects = new List<cAlarmObject>();
            public List<cCommandObject> CommandObjects = new List<cCommandObject>();
            public List<cStatusObject> StatusObjects = new List<cStatusObject>();
            public List<cAggregatedStatusObject> AggregatedStatusObjects = new List<cAggregatedStatusObject>();
            */

            }

            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Loaded {0} objects, {1} alarms, {2} commands, {3} status and {4} agg.status from SXL (YAML) file '{5}'",
            iLoadedObjects, iLoadedAlarms, iLoadedCommands, iLoadedStatus, iLoadedAggregatedStatus, sFileName);

            return iReadFiles;

        }

        public int LoadSXLCSVFiles(string sCSVObjectFilesPath)
        {

            bool bLoadFailed = false;
            int iReadFiles = 0;
            string[] ReferenceFiles;

            DateTime dtStartTime = new DateTime(1970, 1, 1);
            UInt32 uObjectFilesTimeStamp = 0;

            try
            {
                ReferenceFiles = Directory.GetFiles(sCSVObjectFilesPath, "*.skv");
                foreach (string ReferenceFile in ReferenceFiles)
                {
                    if (LoadSXLCSVFile(ReferenceFile))
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
                    ReferenceFiles = Directory.GetFiles(sCSVObjectFilesPath, "*.csv");
                    foreach (string ReferenceFile in ReferenceFiles)
                    {
                        if (LoadSXLCSVFile(ReferenceFile))
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
            }
            catch (Exception exc)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to load object files: {0}", exc.Message);
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

        public bool LoadSXLCSVFile(string FileName)
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

                string[] sCompleteFileLines = cHelper.ConvertStringArrayFromCommaSeparatedToSemicolonSeparated(sCompleteFile.Split('\r'));

                foreach (string sLine in sCompleteFileLines)
                {

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

                                    RoadSideObject.SiteIdObject = LastSiteIdObject;
                                    treeNode = new TreeNode(RoadSideObject.sComponentId + " / " + RoadSideObject.sObject);
                                    LastSiteIdObject.RoadSideObjects.Add(RoadSideObject);
                                    treeNode.Tag = RoadSideObject;
                                    treeNode.SelectedImageIndex = RoadSideObject.bIsComponentGroup ? 2 : 1;
                                    treeNode.ImageIndex = treeNode.SelectedImageIndex;

                                    treeNode.ToolTipText = "Object type: " + RoadSideObject.sObjectType + "\r\nObject: " + RoadSideObject.sObject + "\r\ncomponentId: " + RoadSideObject.sComponentId +
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
                            // 2020-11-27/TR Load anyway warn only
                            //else
                            //{
                            for (iIndex = 8, AlarmReturnValue = new cAlarmReturnValue(AlarmObject, sLine, ref iIndex); AlarmReturnValue.IsValid();)
                            {
                                AlarmObject.AlarmReturnValues.Add(AlarmReturnValue);
                                AlarmReturnValue = new cAlarmReturnValue(AlarmObject, sLine, ref iIndex);
                            }
                            AlarmObjects.Add(AlarmObject);
                            iLoadedAlarms++;
                            //}
                            break;

                        case SectionType_Command:
                            // Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;;;;;;;;;;;;;;;;;;;;
                            cCommandObject CommandObject = new cCommandObject();
                            CommandObject.sObjectType = cHelper.Item(sLine, 0, ';').Trim();
                            CommandObject.sSpecificObject = cHelper.Item(sLine, 1, ';').Trim();
                            CommandObject.sCommandCodeId = cHelper.Item(sLine, 2, ';').Trim();
                            //string sCommandCodeId = cHelper.Item(sLine, 2, ';').Trim();
                            CommandObject.sDescription = cHelper.Item(sLine, 3, ';').Trim();
                            //for (iIndex = 4, CommandReturnValue = new cCommandReturnValue(sCommandCodeId, sLine, ref iIndex); CommandReturnValue.IsValid(); CommandObject.CommandReturnValues.Add(CommandReturnValue), CommandReturnValue = new cCommandReturnValue(sCommandCodeId, sLine, ref iIndex)) ;
                            for (iIndex = 4, CommandReturnValue = new cCommandReturnValue(CommandObject, sLine, ref iIndex); CommandReturnValue.IsValid(); CommandObject.CommandReturnValues.Add(CommandReturnValue), CommandReturnValue = new cCommandReturnValue(CommandObject, sLine, ref iIndex)) ;
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
                            LastSiteIdObject = FindOrCreateParentAndSiteId(null, cHelper.Item(sLine, 1, ';').Trim(), cHelper.Item(sLine, 2, ';').Trim(), sSiteIdDescription);
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

        public cSiteIdObject FindOrCreateParentAndSiteId(cSiteIdObject LastSiteIdObject, string sComponentId, string sDescription, string sDefaultSiteIdDescription)
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
                treeNode = RSMPGS.MainForm.treeView_SitesAndObjects.Nodes.Add(sSiteId + " / " + SiteIdObject.sDescription);
                SiteIdObject.RootNode = treeNode;
                treeNode.Tag = SiteIdObject;
                SiteIdObjects.Add(SiteIdObject);
            }

            return SiteIdObject;

        }

        public cValueTypeObject CreateAndAddValueTypeObject(string sValueTypeKey, string sName, string sType, string sRange, string sComment)
        {

            cValueTypeObject ValueTypeObject = null;

            if (ValueTypeObjects.ContainsKey(sValueTypeKey))
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Value already exists: " + sValueTypeKey.Replace("\t", "/"));
            }
            else
            {

                ValueTypeObject = new cValueTypeObject(sValueTypeKey, sName, sType, sRange, null, sComment);

                ValueTypeObjects.Add(sValueTypeKey, ValueTypeObject);
            }

            return ValueTypeObject;

        }


        public List<string> GetAllPositions(string sPositionString)
        {

            string[] sValues = sPositionString.Split('\n');

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

    }
}