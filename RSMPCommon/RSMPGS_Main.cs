using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Authentication;

namespace nsRSMPGS
{

  public partial class RSMPGS_Main : Form
  {

    private cRoadSideObject SelectedRoadSideObject = null;

    public delegate void AddSysLogListItem(cSysLogAndDebug.Severity severity, string sDateTime, string sLogText);
    public AddSysLogListItem DelegateAddSysLogListItem;

    public delegate void DecodeJSonPacket(string sLogText);
    public DecodeJSonPacket DelegateDecodeJSonPacket;

    public delegate void SocketWasClosed();
    public SocketWasClosed DelegateSocketWasClosed;

    public delegate void SocketWasConnected();
    public SocketWasConnected DelegateSocketWasConnected;

    static public bool bIsCurrentlyChangingSelection = false;

    static int LastConnectionStatus = cTcpSocket.ConnectionStatus_Unknown;

    static System.Drawing.Color rgbDefaultBackColor;
    static System.Drawing.Color rgbDefaultForeColor;

    static int WatchdogInterval;
    static int WatchdogTimeout;

    static int iMaxEventsPerObject;

    static int iDefaultAlarmColumns;
    static int iDefaultAlarmEventsColumns;

    static public bool bActiveWatchdogAlarm = false;
    static public bool bWriteEventsContinous = false;

    static bool bLoadFailed = false;

    static string sCSVObjectFilesPath;
    static string sYAMLFileName;

    static string sProcessImageDefaultName = "";

    static cProcessImage.ObjectFileType SelectedObjectFileType;

    static List<string> MostRecentObjectFilesAndPaths = new List<string>();
    static List<ToolStripMenuItem> MostRecentObjectFilesAndPathsMenuItem = new List<ToolStripMenuItem>();

    public bool bIsLoading;

    public RSMPGS_Main()
    {
      InitializeComponent();
    }

    //
    // Load main form
    //
    private void RSMPGS_Main_Load(object sender, EventArgs e)
    {

      ListViewItem lvItem;

      if (File.Exists(RSMPGS.IniFileFullname) == false)
      {
        MessageBox.Show("Configuration file '" + Path.GetFullPath(RSMPGS.IniFileFullname) + "' is missing!", "RSMP Protocol Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
        bLoadFailed = true;
        this.Close();
        return;
      }

      bIsLoading = true;

      DelegateAddSysLogListItem = new AddSysLogListItem(AddSysLogListItemMethod);
      DelegateDecodeJSonPacket = new DecodeJSonPacket(DecodeJSonPacketMethod);
      DelegateSocketWasClosed = new SocketWasClosed(SocketWasClosedMethod);
      DelegateSocketWasConnected = new SocketWasConnected(SocketWasConnectedMethod);

      this.Text = this.Text + " " + Application.ProductVersion;

      if (RSMPGS.DebugName.Length > 0)
      {
        this.Text += " (" + RSMPGS.DebugName + ")";
      }

      //this.Text += " (1.0.1.6 B1)";

      cHelper.CreateDirectories();

      cHelper.AddStatistics();

      RSMPGS.DebugConnection = new cDebugConnection(RSMPGS.DebugName, RSMPGS.DebugServer);

      // We actually begin executing stuff here...
      RSMPGS.SysLog = new cSysLogAndDebug();

      RSMPGS.SysLog.bEnableSysLog = cPrivateProfile.GetIniFileInt("Main", "EnableSysLog", 1) != 0;

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, RSMPGS.SimulatorType.ToString() + ", version " + Application.ProductVersion + " is starting...");

      iDefaultAlarmColumns = listView_Alarms.Columns.Count;
      iDefaultAlarmEventsColumns = listView_AlarmEvents.Columns.Count;

#if _RSMPGS1

      RSMPGS.JSon = new cJSonGS1();

#endif

#if _RSMPGS2

      RSMPGS.JSon = new cJSonGS2();

#endif

      RSMPGS.ProcessImage = new cProcessImage();
      RSMPGS.DebugForms = new List<RSMPGS_Debug>();

      cHelper.LoadRSMPSettings();

      //
      // Get my locations
      //
      try
      {
        this.Left = cPrivateProfile.GetIniFileInt("Main", "Left", this.Left);
        this.Top = cPrivateProfile.GetIniFileInt("Main", "Top", this.Top);
        this.Width = cPrivateProfile.GetIniFileInt("Main", "Width", this.Width);
        this.Height = cPrivateProfile.GetIniFileInt("Main", "Height", this.Height);
        if (this.Left < 0 || this.Top < 0)
        {
          this.Left = 0;
          this.Top = 0;
        }
      }
      catch
      {
      }

      ToolStripMenuItem_View_AlwaysShowGroupHeaders.Checked = cPrivateProfile.GetIniFileInt("Main", "AlwaysShowGroupHeaders", 0) != 0;

      iMaxEventsPerObject = cPrivateProfile.GetIniFileInt("Main", "MaxEventsPerObject", 500);

      rgbDefaultForeColor = ToolStripMenuItem_ConnectionStatus.ForeColor;
      rgbDefaultBackColor = ToolStripMenuItem_ConnectionStatus.BackColor;

      cHelper.RestoreDebugForms();

      // Load aggregated status texts from INI-file
      for (int iIndex = 0; iIndex < RSMPGS.ProcessImage.sAggregatedStatusBitTexts.GetLength(0); iIndex++)
      {
        RSMPGS.ProcessImage.sAggregatedStatusBitTexts[iIndex] = cPrivateProfile.GetIniFileString("AggregatedStatus", "BitText_" + (iIndex + 1).ToString(), "");
        lvItem = listView_AggregatedStatus_StatusBits.Items.Add((iIndex + 1).ToString());
        lvItem.UseItemStyleForSubItems = false;
        lvItem.SubItems.Add("");
        lvItem.SubItems.Add(RSMPGS.ProcessImage.sAggregatedStatusBitTexts[iIndex]);
        SetStatusBitColor(lvItem, false);
      }

      LoadColumnWidths(this);

      listView_AlarmEvents.ColumnSorter.SortColumn = 0;
      listView_AlarmEvents.ColumnSorter.Order = SortOrder.Ascending;
    
      checkBox_ShowTooltip.Checked = cPrivateProfile.GetIniFileInt("Main", "ShowTooltip", 0) != 0;

      ToolStripMenuItem_DisableNagleAlgorithm.Checked = cPrivateProfile.GetIniFileInt("Main", "DisableNagleAlgorithm", 0) != 0;
      ToolStripMenuItem_SplitPackets.Checked = cPrivateProfile.GetIniFileInt("Main", "SplitPackets", 0) != 0;
      ToolStripMenuItem_StoreBase64Updates.Checked = cPrivateProfile.GetIniFileInt("Main", "StoreBase64Updates", 0) != 0;

      ToolStripMenuItem_ConnectAutomatically.Checked = cPrivateProfile.GetIniFileInt("Main", "ConnectAutomatically", 0) != 0;

      tabControl_Object.SelectedIndex = cPrivateProfile.GetIniFileInt("Main", "TabControl_Object", 1);

      checkBox_AlwaysUseSXLFromFile.Checked = cPrivateProfile.GetIniFileInt("Main", "AlwaysUseSXLFromFile", 0) != 0;
      checkBox_ViewOnlyFailedPackets.Checked = cPrivateProfile.GetIniFileInt("Main", "ViewOnlyFailedPackets", 0) != 0;

      RSMPGS.EncryptionSettings.AuthenticateAsClientUsingCertificate = cPrivateProfile.GetIniFileInt("Encryption", "AuthenticateAsClientUsingCertificate", 0) != 0;
      RSMPGS.EncryptionSettings.CheckCertificateRevocationList = cPrivateProfile.GetIniFileInt("Encryption", "CheckCertificateRevocationList", 0) != 0;
      RSMPGS.EncryptionSettings.ClientCertificateFile = cPrivateProfile.GetIniFileString("Encryption", "ClientCertificateFile", "");
      RSMPGS.EncryptionSettings.IgnoreCertificateErrors = cPrivateProfile.GetIniFileInt("Encryption", "IgnoreCertificateErrors", 0) != 0;
      RSMPGS.EncryptionSettings.RequireClientCertificate = cPrivateProfile.GetIniFileInt("Encryption", "RequireClientCertificate", 0) != 0;
      RSMPGS.EncryptionSettings.ServerCertificateFile = cPrivateProfile.GetIniFileString("Encryption", "ServerCertificateFile", "");
      RSMPGS.EncryptionSettings.ServerName = cPrivateProfile.GetIniFileString("Encryption", "ServerName", "");

      checkBox_Encryption_Protocol_Default.Checked = cPrivateProfile.GetIniFileInt("Encryption", "Protocols.None", 0) != 0;
      checkBox_Encryption_Protocol_TLS10.Checked = cPrivateProfile.GetIniFileInt("Encryption", "Protocols.TLS10", 0) != 0;
      checkBox_Encryption_Protocol_TLS11.Checked = cPrivateProfile.GetIniFileInt("Encryption", "Protocols.TLS11", 0) != 0;
      checkBox_Encryption_Protocol_TLS12.Checked = cPrivateProfile.GetIniFileInt("Encryption", "Protocols.TLS12", 0) != 0;
      //checkBox_Encryption_Protocol_TLS13.Checked = cPrivateProfile.GetIniFileInt("Encryption", "Protocols.TLS13", 0) != 0;

      // Do some calc
      checkBox_Encryption_Protocol_CheckedChanged();

      button_Encryption_CheckRevocation.Checked = RSMPGS.EncryptionSettings.CheckCertificateRevocationList;
      button_Encryption_IgnoreCertErrors.Checked = RSMPGS.EncryptionSettings.IgnoreCertificateErrors;

      RSMPGS.EncryptionSettings.ServerCertificateFilePassword = cPrivateProfile.Base64Decode(cPrivateProfile.GetIniFileString("Encryption", "ServerCertificateFilePassword", ""));
      RSMPGS.EncryptionSettings.ClientCertificateFilePassword = cPrivateProfile.Base64Decode(cPrivateProfile.GetIniFileString("Encryption", "ClientCertificateFilePassword", ""));

      for (int iIndex = 0; iIndex < 10; iIndex++)
      {
        string sFileOrPath = cPrivateProfile.GetIniFileString("MostRecentObjectFiles", iIndex.ToString(), "");
        if (sFileOrPath != "")
        {
          MostRecentObjectFilesAndPaths.Add(sFileOrPath);
        }
        ToolStripMenuItem tsItem = new ToolStripMenuItem();
        tsItem.Click += new System.EventHandler(toolStripMenuItem_LoadObjects_MRU_Click);
        ToolStripMenuItem_File_LoadObjects.DropDownItems.Add(tsItem);
        MostRecentObjectFilesAndPathsMenuItem.Add(tsItem);
      }

      RefillMostRecentObjectFilesAndPathsMenu();

      // Defaults to load at startup and will point out the old objects folder
      checkBox_AutomaticallyLoadObjects.Checked = cPrivateProfile.GetIniFileInt("Main", "AutomaticallyLoadObjects", 1) != 0;

      sCSVObjectFilesPath = cPrivateProfile.GetIniFileString("Main", "CSVObjectFilesPath", cPrivateProfile.DefaultObjectFilesPath() + "\\");
      sYAMLFileName = cPrivateProfile.GetIniFileString("Main", "YAMLFileName", cPrivateProfile.DefaultObjectFilesPath());

      try
      {
        SelectedObjectFileType = (cProcessImage.ObjectFileType)cPrivateProfile.GetIniFileInt("Main", "SelectedObjectFileType", (int)cProcessImage.ObjectFileType.CSVfiles);
      }
      catch
      {
      }

      WatchdogInterval = cPrivateProfile.GetIniFileInt("RSMP", "WatchdogInterval", 0);
      WatchdogTimeout = cPrivateProfile.GetIniFileInt("RSMP", "WatchdogTimeout", 0);

      Main_Load();

      if (checkBox_AutomaticallyLoadObjects.Checked)
      {

        LoadProcessImageObjects();

#if _RSMPGS1

        if (checkbox_AutomaticallyLoadProcessData.Checked)
        {
          RSMPGS.ProcessImage.LoadProcessImageValues(this, sProcessImageDefaultName);
          bProcessDataWasLoadedAtStartup = true;
        }

#endif

#if _RSMPGS2

        if (ToolStripMenuItem_ProcessImage_LoadAtStartUp.Checked)
        {
          RSMPGS.ProcessImage.LoadProcessImageValues(sProcessImageDefaultName);
        }

#endif

      }

      timer_System.Enabled = true;

      bIsLoading = false;

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, RSMPGS.SimulatorType.ToString() + " has started");

    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {

      if (bLoadFailed)
      {
        return;
      }

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, RSMPGS.SimulatorType.ToString() + " is shutting down...");

      cPrivateProfile.WriteIniFileInt("Main", "AlwaysShowGroupHeaders", ToolStripMenuItem_View_AlwaysShowGroupHeaders.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Encryption", "AuthenticateAsClientUsingCertificate", RSMPGS.EncryptionSettings.AuthenticateAsClientUsingCertificate == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Encryption", "CheckCertificateRevocationList", RSMPGS.EncryptionSettings.CheckCertificateRevocationList == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Encryption", "IgnoreCertificateErrors", RSMPGS.EncryptionSettings.IgnoreCertificateErrors == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Encryption", "RequireClientCertificate", RSMPGS.EncryptionSettings.RequireClientCertificate == true ? 1 : 0);

      cPrivateProfile.WriteIniFileString("Encryption", "ClientCertificateFile", RSMPGS.EncryptionSettings.ClientCertificateFile);
      cPrivateProfile.WriteIniFileString("Encryption", "ServerCertificateFile", RSMPGS.EncryptionSettings.ServerCertificateFile);
      cPrivateProfile.WriteIniFileString("Encryption", "ServerName", RSMPGS.EncryptionSettings.ServerName);

      cPrivateProfile.WriteIniFileInt("Encryption", "Protocols.None", checkBox_Encryption_Protocol_Default.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Encryption", "Protocols.TLS10", checkBox_Encryption_Protocol_TLS10.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Encryption", "Protocols.TLS11", checkBox_Encryption_Protocol_TLS11.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Encryption", "Protocols.TLS12", checkBox_Encryption_Protocol_TLS12.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Encryption", "Protocols.TLS13", checkBox_Encryption_Protocol_TLS13.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileString("Encryption", "ServerCertificateFilePassword", cPrivateProfile.Base64Encode(RSMPGS.EncryptionSettings.ServerCertificateFilePassword));
      cPrivateProfile.WriteIniFileString("Encryption", "ClientCertificateFilePassword", cPrivateProfile.Base64Encode(RSMPGS.EncryptionSettings.ClientCertificateFilePassword));


      //
      // Disconnect
      //
      RSMPGS.RSMPConnection.Shutdown();

      cHelper.StoreDebugForms();

      cHelper.SaveRSMPSettings();

      //
      // Store my locations
      //
      cPrivateProfile.WriteIniFileInt("Main", "Left", this.Left);
      cPrivateProfile.WriteIniFileInt("Main", "Top", this.Top);
      cPrivateProfile.WriteIniFileInt("Main", "Width", this.Width);
      cPrivateProfile.WriteIniFileInt("Main", "Height", this.Height);

      cPrivateProfile.WriteIniFileInt("Main", "DisableNagleAlgorithm", ToolStripMenuItem_DisableNagleAlgorithm.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "SplitPackets", ToolStripMenuItem_SplitPackets.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Main", "StoreBase64Updates", ToolStripMenuItem_StoreBase64Updates.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Main", "ConnectAutomatically", ToolStripMenuItem_ConnectAutomatically.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "ShowTooltip", checkBox_ShowTooltip.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "TabControl_Object", tabControl_Object.SelectedIndex);

      cPrivateProfile.WriteIniFileString("Main", "SignalExchangeListVersion", textBox_SignalExchangeListVersion.Text);
      cPrivateProfile.WriteIniFileInt("Main", "AlwaysUseSXLFromFile", checkBox_AlwaysUseSXLFromFile.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Main", "AutomaticallyLoadObjects", checkBox_AutomaticallyLoadObjects.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Main", "ViewOnlyFailedPackets", checkBox_ViewOnlyFailedPackets.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileString("Main", "CSVObjectFilesPath", sCSVObjectFilesPath);
      cPrivateProfile.WriteIniFileString("Main", "YAMLFileName", sYAMLFileName);

      cPrivateProfile.WriteIniFileInt("Main", "SelectedObjectFileType", (int)SelectedObjectFileType);

      for (int iIndex = 0; iIndex < MostRecentObjectFilesAndPaths.Count; iIndex++)
      {
        cPrivateProfile.WriteIniFileString("MostRecentObjectFiles", iIndex.ToString(), MostRecentObjectFilesAndPaths[iIndex]);
      }
      

      try
      {
        cRoadSideObject RoadSideObject = (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag;
        if (RoadSideObject == null)
        {
          cPrivateProfile.WriteIniFileString("Main", "SelectedObject", "");
        }
        else
        {
          cPrivateProfile.WriteIniFileString("Main", "SelectedObject", RoadSideObject.UniqueId());
        }
      }
      catch
      {
      }

      SaveColumnWidths(this);

      Main_Closing();

      RSMPGS.DebugConnection.Shutdown();

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, RSMPGS.SimulatorType.ToString() + " was shut down");
      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "");

      RSMPGS.SysLog.Shutdown();

    }

    private void RefillMostRecentObjectFilesAndPathsMenu()
    {
      for (int iIndex = 0; iIndex < 10; iIndex++)
      {
        if (iIndex < MostRecentObjectFilesAndPaths.Count)
        {
          if (MostRecentObjectFilesAndPaths[iIndex].EndsWith("\\"))
          {
            MostRecentObjectFilesAndPathsMenuItem[iIndex].Text = MostRecentObjectFilesAndPaths[iIndex]; // + "\t" + "(CSV-files object folder)";
          }
          else
          {
            MostRecentObjectFilesAndPathsMenuItem[iIndex].Text = MostRecentObjectFilesAndPaths[iIndex]; // + "\t" + "(YAML-file)";
          }
          MostRecentObjectFilesAndPathsMenuItem[iIndex].Visible = true;
        }
        else
        {
          MostRecentObjectFilesAndPathsMenuItem[iIndex].Visible = false;
        }
      }
    }

    private void LoadProcessImageObjects()
    {

      int iReadFiles = 0;

      // int iReadFiles = RSMPGS.ProcessImage.LoadSXLCSVFiles();

      string sFileOrPath = "";

      try
      {
        cRoadSideObject RoadSideObject = (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag;
        if (RoadSideObject == null)
        {
          cPrivateProfile.WriteIniFileString("Main", "SelectedObject", "");
        }
        else
        {
          cPrivateProfile.WriteIniFileString("Main", "SelectedObject", RoadSideObject.UniqueId());
        }
      }
      catch
      {
      }

      RSMPGS.ProcessImage.Clear();

      listView_Status.Groups.Clear();
      listView_Alarms.Groups.Clear();
      listView_Commands.Groups.Clear();

      listView_Alarms.Items.Clear();
      listView_AlarmEvents.Items.Clear();

      listView_AlarmEvents.Items.Clear();
      listView_Commands.Items.Clear();
      listView_Status.Items.Clear();

      RemoveExtraColumns(listView_Alarms, iDefaultAlarmColumns);
      RemoveExtraColumns(listView_AlarmEvents, iDefaultAlarmEventsColumns);

#if _RSMPGS2

      listView_AggregatedStatusEvents.Items.Clear();

      listView_StatusEvents.Items.Clear();
      listView_CommandEvents.Items.Clear();

#endif

      treeView_SitesAndObjects.Nodes.Clear();

      switch (SelectedObjectFileType)
      {
        case cProcessImage.ObjectFileType.CSVfiles:
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Loading object files from: {0}", sCSVObjectFilesPath);
          iReadFiles = RSMPGS.ProcessImage.LoadSXLCSVFiles(sCSVObjectFilesPath);
          sFileOrPath = sCSVObjectFilesPath;
          sProcessImageDefaultName = sCSVObjectFilesPath + "ProcessImage.dat";
          textBox_SignalExchangeListPath.Text = sCSVObjectFilesPath;
          break;

        case cProcessImage.ObjectFileType.YAMLfile:
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Loading YAML file: {0}", sYAMLFileName);
          iReadFiles = RSMPGS.ProcessImage.LoadYAMLFile(sYAMLFileName);
          sFileOrPath = sYAMLFileName;
          sProcessImageDefaultName = Path.ChangeExtension(sYAMLFileName, ".dat");
          textBox_SignalExchangeListPath.Text = sYAMLFileName;
          break;
      }

      if (iReadFiles == 0)
      {
        return;
      }

      int iMenuIndex = MostRecentObjectFilesAndPaths.FindIndex(x => x.Equals(sFileOrPath, StringComparison.OrdinalIgnoreCase));

      if (iMenuIndex >= 0)
      {
        MostRecentObjectFilesAndPaths.RemoveAt(iMenuIndex);
      }
      MostRecentObjectFilesAndPaths.Insert(0, sFileOrPath);
      while (MostRecentObjectFilesAndPaths.Count > 10)
      {
        MostRecentObjectFilesAndPaths.RemoveAt(MostRecentObjectFilesAndPaths.Count - 1);
      }

      RefillMostRecentObjectFilesAndPathsMenu();

      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        string sGroupName = RoadSideObject.sComponentId + " / " + RoadSideObject.sObject + " (" + RoadSideObject.sDescription + ")";
        RoadSideObject.StatusGroup = new ListViewGroup(sGroupName);
        RoadSideObject.AlarmsGroup = new ListViewGroup(sGroupName);
        RoadSideObject.CommandsGroup = new ListViewGroup(sGroupName);
        listView_Status.Groups.Add(RoadSideObject.StatusGroup);
        listView_Alarms.Groups.Add(RoadSideObject.AlarmsGroup);
        listView_Commands.Groups.Add(RoadSideObject.CommandsGroup);
      }

      int iAlarmObjects = 0;
      int iCommandObjects = 0;
      int StatusObjects = 0;

      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        if (RoadSideObject.AlarmObjects.Count == 0 && RoadSideObject.CommandObjects.Count == 0 && RoadSideObject.StatusObjects.Count == 0)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Nothing was loaded for component: {0}", RoadSideObject.sComponentId);
        }
        else
        {
          iAlarmObjects += RoadSideObject.AlarmObjects.Count;
          iCommandObjects += RoadSideObject.CommandObjects.Count;
          StatusObjects += RoadSideObject.StatusObjects.Count;

          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Expanded {0} alarms, {1} commands {1} and {2} status for component: {3}",
            RoadSideObject.AlarmObjects.Count, RoadSideObject.CommandObjects.Count, RoadSideObject.StatusObjects.Count, RoadSideObject.sComponentId);
        }
      }

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Expanded totally {0} alarms, {1} commands, {2} status in {3} RoadSideObjects and {4} agg.status found in {5} files",
        iAlarmObjects, iCommandObjects, StatusObjects, RSMPGS.ProcessImage.RoadSideObjects.Count, RSMPGS.ProcessImage.AggregatedStatusObjects.Count, iReadFiles);

      // Fill out alarm listview
      for (int iIndex = 0; iIndex < RSMPGS.ProcessImage.MaxAlarmReturnValues; iIndex++)
      {
        listView_Alarms.Columns.Add("Name", 100, HorizontalAlignment.Center);
        listView_Alarms.Columns.Add("Type", 100, HorizontalAlignment.Center);
        //				listView_Alarms.Columns.Add("Value", 100, HorizontalAlignment.Center);
        ColumnHeader columnHeader = listView_Alarms.Columns.Add("Value", 100, HorizontalAlignment.Center);
        columnHeader.Tag = "Value" + "_" + iIndex.ToString();
        listView_Alarms.Columns.Add("Comment", 200, HorizontalAlignment.Left);
        listView_AlarmEvents.Columns.Add("Name", 100, HorizontalAlignment.Center);
        listView_AlarmEvents.Columns.Add("Value", 100, HorizontalAlignment.Center);
      }

      if (RSMPGS.ProcessImage.sSULRevision.Length > 0)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Signal Exchange List revision number from file is '{0}'", RSMPGS.ProcessImage.sSULRevision);
      }
      else
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Signal Exchange List revision number could not be found in any object file");
      }

      textBox_SignalExchangeListVersion.Text = cPrivateProfile.GetIniFileString("Main", "SignalExchangeListVersion", "").Trim();

      textBox_SignalExchangeListVersionFromFile.Text = RSMPGS.ProcessImage.sSULRevision;

      if (checkBox_AlwaysUseSXLFromFile.Checked == true && textBox_SignalExchangeListVersionFromFile.Text.Length > 0)
      {
        textBox_SignalExchangeListVersion.Text = textBox_SignalExchangeListVersionFromFile.Text;
      }

      if (cPrivateProfile.GetIniFileInt("Main", "IgnoreFileTimeStamps", 0) == 0)
      {
        if (RSMPGS.ProcessImage.ObjectFilesTimeStamp != cPrivateProfile.GetIniFileInt("Main", "ObjectFilesTimeStamp", 0) && RSMPGS.ProcessImage.ObjectFilesTimeStamp != 0)
        {
          string sLastSXLRevisionFromFile = cPrivateProfile.GetIniFileString("Main", "LastSXLRevisionFromFile", "");
          if (RSMPGS.ProcessImage.sSULRevision.Length > 0 && sLastSXLRevisionFromFile.Length > 0)
          {
            if (RSMPGS.ProcessImage.sSULRevision.Equals(sLastSXLRevisionFromFile, StringComparison.OrdinalIgnoreCase))
            {
              System.Windows.Forms.MessageBox.Show("Signal Exchange List files have been updated, however the version number found in file (" + RSMPGS.ProcessImage.sSULRevision + ") is still the same.", "RSMPGS2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          }
          cPrivateProfile.WriteIniFileInt("Main", "ObjectFilesTimeStamp", RSMPGS.ProcessImage.ObjectFilesTimeStamp);
          cPrivateProfile.WriteIniFileString("Main", "LastSXLRevisionFromFile", RSMPGS.ProcessImage.sSULRevision);
        }
      }

      string sObjectUniqueId = cPrivateProfile.GetIniFileString("Main", "SelectedObject", "");
      if (sObjectUniqueId.Length > 0)
      {
        foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
        {
          if (sObjectUniqueId.Equals(RoadSideObject.UniqueId(), StringComparison.OrdinalIgnoreCase))
          {
            treeView_SitesAndObjects.SelectedNode = RoadSideObject.Node;
            RoadSideObject.Node.EnsureVisible();
            treeView_SitesAndObjects.Select();
            break;
          }
        }
      }

    }

    private void RemoveExtraColumns(ListView listView, int iDefaultColumns)
    {
      while (listView.Columns.Count > iDefaultColumns)
      {
        listView.Columns.RemoveAt(listView.Columns.Count - 1);
      }
    }

    private void ToolStripMenuItem_View_AlwaysShowGroupHeaders_CheckedChanged(object sender, EventArgs e)
    {
      if (treeView_SitesAndObjects.SelectedNode == null)
      {
        return;
      }
      // If this is a node, change group heading
      if (treeView_SitesAndObjects.SelectedNode.Parent != null)
      {
        treeView_SitesAndObjects_AfterSelect();
      }
    }

    private void treeView_SitesAndObjects_AfterSelect(object sender, TreeViewEventArgs e)
    {
      treeView_SitesAndObjects_AfterSelect();
    }

    private void treeView_SitesAndObjects_AfterSelect()
    {
      cRoadSideObject RoadSideObject;
      cSiteIdObject SiteIdObject;

      bIsCurrentlyChangingSelection = true;

      Cursor.Current = Cursors.WaitCursor;
      Application.DoEvents();

      // Clear Alarm tab
      listView_Alarms.BeginUpdate();
      listView_Alarms.Items.Clear();
      listView_Alarms.StopSorting();
      listView_Alarms.EndUpdate();

      // Clear Command tab
      listView_Commands.BeginUpdate();
      listView_Commands.Items.Clear();
      listView_Commands.StopSorting();
      listView_Commands.EndUpdate();

      // Clear Status tab
      listView_Status.BeginUpdate();
      listView_Status.Items.Clear();
      listView_Status.StopSorting();
      listView_Status.EndUpdate();

      // Clear Aggregated status tab
      groupBox_AggregatedStatus_FunctionalPosition.Enabled = false;
      groupBox_AggregatedStatus_FunctionalState.Enabled = false;
      groupBox_AggregatedStatus_StatusBits.Enabled = false;
      listBox_AggregatedStatus_FunctionalPosition.Items.Clear();
      listBox_AggregatedStatus_FunctionalState.Items.Clear();

#if _RSMPGS1

      listView_AlarmEvents.Items.Clear();

      button_AggregatedStatus_Send.Enabled = false;
      checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Enabled = false;
      listBox_AggregatedStatus_FunctionalPosition.Items.Add("");
      listBox_AggregatedStatus_FunctionalState.Items.Add("");

#endif

#if _RSMPGS2

	  listView_AggregatedStatusEvents.Items.Clear();
      button_AggregatedStatus_Request.Enabled = false;

#endif

      foreach (ListViewItem lvItem in listView_AggregatedStatus_StatusBits.Items)
      {
        SetStatusBitColor(lvItem, false);
      }

      if (treeView_SitesAndObjects.SelectedNode.Tag == null)
      {
        bIsCurrentlyChangingSelection = false;
        Cursor.Current = Cursors.Default;
        return;
      }

      if (treeView_SitesAndObjects.SelectedNode.Parent == null)
      {
        SiteIdObject = (cSiteIdObject)treeView_SitesAndObjects.SelectedNode.Tag;
        RoadSideObject = null;
      }
      else
      {
        SiteIdObject = (cSiteIdObject)treeView_SitesAndObjects.SelectedNode.Parent.Tag;
        RoadSideObject = (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag;
      }

      // Root object (group/siteid)
      if (RoadSideObject == null)
      {

      }

      SelectedRoadSideObject = RoadSideObject;

      if (RoadSideObject != null)
      {
        // Objekttyp;Objekt;componentId;siteId;externalNtsId;Beskrivning;functionalPosition;functionalState;
        if (RoadSideObject.bIsComponentGroup)
        {
          groupBox_AggregatedStatus_FunctionalPosition.Enabled = true;
          groupBox_AggregatedStatus_FunctionalState.Enabled = true;
          groupBox_AggregatedStatus_StatusBits.Enabled = true;

#if _RSMPGS1

          button_AggregatedStatus_Send.Enabled = true;
          checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Enabled = true;

          // Fill the Aggregated Status list
          foreach (cAggregatedStatusObject AggregatedStatusObject in RSMPGS.ProcessImage.AggregatedStatusObjects)
          {
            if (RoadSideObject.sObjectType.Equals(AggregatedStatusObject.sObjectType, StringComparison.OrdinalIgnoreCase))
            {
              foreach (string sItem in AggregatedStatusObject.sFunctionalPositions)
              {
                listBox_AggregatedStatus_FunctionalPosition.Items.Add(sItem);
                if (sItem.Equals(RoadSideObject.sFunctionalPosition))
                {
                  listBox_AggregatedStatus_FunctionalPosition.SelectedIndex = listBox_AggregatedStatus_FunctionalPosition.Items.Count - 1;
                }
              }
              foreach (string sItem in AggregatedStatusObject.sFunctionalStates)
              {
                listBox_AggregatedStatus_FunctionalState.Items.Add(sItem);
                if (sItem.Equals(RoadSideObject.sFunctionalState))
                {
                  listBox_AggregatedStatus_FunctionalState.SelectedIndex = listBox_AggregatedStatus_FunctionalState.Items.Count - 1;
                }
              }
              break;
            }
          }
#endif

#if _RSMPGS2

          if (RSMPGS.RSMPConnection != null)
          {
            if (RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected && RSMPGS.JSon.NegotiatedRSMPVersion >= cJSon.RSMPVersion.RSMP_3_1_5)
            {
              button_AggregatedStatus_Request.Enabled = true;
            }
          }

          foreach (cAggregatedStatusEvent AggregatedStatusEvent in RoadSideObject.AggregatedStatusEvents)
          {
            ListViewItem lvItem = listView_AggregatedStatusEvents.Items.Add(AggregatedStatusEvent.sTimeStamp.ToString());
            lvItem.SubItems.Add(AggregatedStatusEvent.sMessageId);
            lvItem.SubItems.Add(AggregatedStatusEvent.sBitStatus);
            lvItem.SubItems.Add(AggregatedStatusEvent.sFunctionalPosition);
            lvItem.SubItems.Add(AggregatedStatusEvent.sFunctionalState);
          }

          // Fill the Aggregated Status list
          foreach (cAggregatedStatusObject AggregatedStatusObject in RSMPGS.ProcessImage.AggregatedStatusObjects)
          {
            if (RoadSideObject.sObjectType.Equals(AggregatedStatusObject.sObjectType, StringComparison.OrdinalIgnoreCase))
            {
              foreach (string sItem in AggregatedStatusObject.sFunctionalPositions)
              {
                if (listBox_AggregatedStatus_FunctionalPosition.Items != null && listBox_AggregatedStatus_FunctionalPosition.SelectedIndex != 0)
                {
                  listBox_AggregatedStatus_FunctionalPosition.Items.Add(sItem);
                  if (sItem.Equals(RoadSideObject.sFunctionalPosition))
                  {
                    try
                    {
                      listBox_AggregatedStatus_FunctionalPosition.SelectedIndex = listBox_AggregatedStatus_FunctionalPosition.Items.Count; //BUGG!!!
                    }
                    catch
                    {
                    }
                  }
                }
              }
              foreach (string sItem in AggregatedStatusObject.sFunctionalStates)
              {

                if (listBox_AggregatedStatus_FunctionalState.Items != null && listBox_AggregatedStatus_FunctionalState.SelectedIndex != 0)
                {
                  listBox_AggregatedStatus_FunctionalState.Items.Add(sItem);
                  if (sItem.Equals(RoadSideObject.sFunctionalState))
                  {
                    listBox_AggregatedStatus_FunctionalState.SelectedIndex = listBox_AggregatedStatus_FunctionalState.Items.Count;
                  }
                }
              }
              break;
            }
          }
		  
#endif

          if (RoadSideObject.bBitStatus != null)
          {
            for (int iIndex = 0; iIndex < RoadSideObject.bBitStatus.GetLength(0); iIndex++)
            {
              SetStatusBitColor(listView_AggregatedStatus_StatusBits.Items[iIndex], RoadSideObject.bBitStatus[iIndex]);
            }
          }
        }
      }

      UpdateAlarmListView(SiteIdObject, RoadSideObject);
      UpdateCommandListView(SiteIdObject, RoadSideObject);
      UpdateStatusListView(SiteIdObject, RoadSideObject);

      bIsCurrentlyChangingSelection = false;

      Cursor.Current = Cursors.Default;
    }

    private void SaveColumnWidths(Control parent)
    {
      foreach (Control control in parent.Controls)
      {
        if (control.GetType() == typeof(ListView) || control.GetType() == typeof(nsRSMPGS.ListViewDoubleBuffered))
        {
          ListView listView = (ListView)control;
          foreach (ColumnHeader columnHeader in listView.Columns)
          {
            try
            {
              cPrivateProfile.WriteIniFileInt(listView.Name, columnHeader.Text + ".Width", columnHeader.Width);
            }
            catch
            {
            }
          }
        }
        else
        {
          SaveColumnWidths(control);
        }
      }
    }
    private void LoadColumnWidths(Control parent)
    {
      foreach (Control control in parent.Controls)
      {
        if (control.GetType() == typeof(ListView) || control.GetType() == typeof(nsRSMPGS.ListViewDoubleBuffered))
        {
          ListView listView = (ListView)control;
          foreach (ColumnHeader columnHeader in listView.Columns)
          {
            try
            {
              columnHeader.Width = cPrivateProfile.GetIniFileInt(listView.Name, columnHeader.Text + ".Width", columnHeader.Width);
            }
            catch
            {
            }
          }
        }
        else
        {
          LoadColumnWidths(control);
        }
      }
    }

    private void ToolStripMenuItem_View_Clear_AlarmEvents_Click(object sender, EventArgs e)
    {

      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
        {
          AlarmObject.AlarmEvents.Clear();
        }
      }

      listView_AlarmEvents.Items.Clear();

    }

    private void button_EncryptionFile_Browse_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
#if _RSMPGS1
      openFileDialog.InitialDirectory = RSMPGS.EncryptionSettings.ClientCertificateFile;
      string sPassword = RSMPGS.EncryptionSettings.ClientCertificateFilePassword;
#endif
#if _RSMPGS2
      openFileDialog.InitialDirectory = RSMPGS.EncryptionSettings.ServerCertificateFile;
      string sPassword = RSMPGS.EncryptionSettings.ServerCertificateFilePassword;
#endif
      openFileDialog.Filter = "Certificate files (*.cer;*.cert;*.pfx)|*.cer;*.cert;*.pfx|All files (*.*)|*.*";
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        if (openFileDialog.FileName.EndsWith(".cer", StringComparison.OrdinalIgnoreCase) == false && openFileDialog.FileName.EndsWith(".cert", StringComparison.OrdinalIgnoreCase) == false)
        {
          if (cFormsHelper.InputBox("Enter certificate file password (if any)", "Password", ref sPassword, false, true, true) == DialogResult.OK)
          {
#if _RSMPGS1
            RSMPGS.EncryptionSettings.ClientCertificateFilePassword = sPassword;
#endif
#if _RSMPGS2
          RSMPGS.EncryptionSettings.ServerCertificateFilePassword = sPassword;
#endif
          }
        }
        textBox_EncryptionFile.Text = openFileDialog.FileName;
      }
    }

    private void button_Encryption_CheckRevocation_CheckedChanged(object sender, EventArgs e)
    {
      RSMPGS.EncryptionSettings.CheckCertificateRevocationList = button_Encryption_CheckRevocation.Checked;
    }

    private void button_Encryption_IgnoreCertErrors_CheckedChanged(object sender, EventArgs e)
    {
      RSMPGS.EncryptionSettings.IgnoreCertificateErrors = button_Encryption_IgnoreCertErrors.Checked;
    }

    private void checkBox_Encryption_Protocol_CheckedChanged()
    {

      checkBox_Encryption_Protocol_TLS10.Enabled = checkBox_Encryption_Protocol_Default.Checked == false;
      checkBox_Encryption_Protocol_TLS11.Enabled = checkBox_Encryption_Protocol_Default.Checked == false;
      checkBox_Encryption_Protocol_TLS12.Enabled = checkBox_Encryption_Protocol_Default.Checked == false;
      checkBox_Encryption_Protocol_TLS13.Enabled = checkBox_Encryption_Protocol_Default.Checked == false;

      //RSMPGS.EncryptionSettings.sslProtocols = SslProtocols.None;

      if (checkBox_Encryption_Protocol_Default.Checked)
      {
        RSMPGS.EncryptionSettings.sslProtocols = SslProtocols.Default;
      }
      else
      {
        RSMPGS.EncryptionSettings.sslProtocols = SslProtocols.None;
        if (checkBox_Encryption_Protocol_TLS10.Checked) RSMPGS.EncryptionSettings.sslProtocols |= SslProtocols.Tls;
        if (checkBox_Encryption_Protocol_TLS11.Checked) RSMPGS.EncryptionSettings.sslProtocols |= SslProtocols.Tls11;
        if (checkBox_Encryption_Protocol_TLS12.Checked) RSMPGS.EncryptionSettings.sslProtocols |= SslProtocols.Tls12;
        // Not in .NET 4.6...
        //if (checkBox_Encryption_Protocol_TLS13.Checked) RSMPGS.EncryptionSettings.sslProtocols |= SslProtocols.Tls13;
      }

    }

    private void checkBox_Encryption_Protocol_CheckedChanged(object sender, EventArgs e)
    {
      checkBox_Encryption_Protocol_CheckedChanged();
    }


    //
    // Do some cyclic cleanup
    //
    private void timer_System_Tick(object sender, EventArgs e)
    {

#if _RSMPGS1

      bool bIsSocketClient = RSMPGS.ConnectionType == cTcpSocket.ConnectionMethod_SocketClient;

#endif

#if _RSMPGS2

      bool bIsSocketClient = RSMPGS.bConnectAsSocketClient;

      if (bIsUpdatingAlarmEventList)
      {
        listView_AlarmEvents.EndUpdate();
        listView_AlarmEvents.ResumeSorting();
        bIsUpdatingAlarmEventList = false;
      }

      if (bIsUpdatingStatusEventList)
      {
        listView_StatusEvents.EndUpdate();
        listView_StatusEvents.ResumeSorting();
        bIsUpdatingStatusEventList = false;
      }

      if (bIsUpdatingAggregatedStatusEventList)
      {
        listView_AggregatedStatusEvents.EndUpdate();
        listView_AggregatedStatusEvents.ResumeSorting();
        bIsUpdatingAggregatedStatusEventList = false;
      }

#endif

    RSMPGS.SysLog.CyclicCleanup(timer_System.Interval);

      RSMPGS.ProcessImage.CyclicCleanup(timer_System.Interval);

      RSMPGS.JSon.CyclicCleanup(timer_System.Interval, WatchdogInterval, WatchdogTimeout);

      cHelper.UpdateStatistics(timer_System.Interval);

      if (RSMPGS.RSMPConnection.ConnectionStatus() != LastConnectionStatus)
      {

        ToolStripMenuItem_ConnectionStatus.ForeColor = rgbDefaultForeColor;
        ToolStripMenuItem_ConnectionStatus.BackColor = rgbDefaultBackColor;

        LastConnectionStatus = RSMPGS.RSMPConnection.ConnectionStatus();

        if (bIsSocketClient)
        {
          switch (LastConnectionStatus)
          {
            case cTcpSocket.ConnectionStatus_Unknown:
              ToolStripMenuItem_ConnectionStatus.Text = "Unknown state";
              break;
            case cTcpSocket.ConnectionStatus_Disconnected:
              ToolStripMenuItem_ConnectionStatus.Text = "Disconnected";
              ToolStripMenuItem_ConnectionStatus.ForeColor = Color.White;
              ToolStripMenuItem_ConnectionStatus.BackColor = Color.Red;
              break;
            case cTcpSocket.ConnectionStatus_Connecting:
              ToolStripMenuItem_ConnectionStatus.Text = "Connecting to " + RSMPGS.RSMPConnection.RemoteServerOrClientIP() + "...";
              break;
            case cTcpSocket.ConnectionStatus_Connected:
              ToolStripMenuItem_ConnectionStatus.Text = "Connected to " + RSMPGS.RSMPConnection.RemoteServerOrClientIP();
              ToolStripMenuItem_ConnectionStatus.ForeColor = Color.White;
              ToolStripMenuItem_ConnectionStatus.BackColor = Color.Green;
              break;
          }
        }
        else
        {
          switch (LastConnectionStatus)
          {
            case cTcpSocket.ConnectionStatus_Unknown:
              ToolStripMenuItem_ConnectionStatus.Text = "Unknown state";
              break;
            case cTcpSocket.ConnectionStatus_Disconnected:
              ToolStripMenuItem_ConnectionStatus.Text = "Waiting, serverport: " + RSMPGS.RSMPConnection.ListenPort();
              ToolStripMenuItem_ConnectionStatus.ForeColor = Color.White;
              ToolStripMenuItem_ConnectionStatus.BackColor = Color.Red;
              break;
            case cTcpSocket.ConnectionStatus_Connecting:
              ToolStripMenuItem_ConnectionStatus.Text = "(not valid)";
              break;
            case cTcpSocket.ConnectionStatus_Connected:
              ToolStripMenuItem_ConnectionStatus.Text = "Connected from " + RSMPGS.RSMPConnection.RemoteServerOrClientIP();
              ToolStripMenuItem_ConnectionStatus.ForeColor = Color.White;
              ToolStripMenuItem_ConnectionStatus.BackColor = Color.Green;
              break;
          }
        }
        this.Refresh();
      }

    }

    private void ToolStripMenuItem_File_LoadObjects_CSV_Click(object sender, EventArgs e)
    {

      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

      try
      {
        folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
        folderBrowserDialog.SelectedPath = sCSVObjectFilesPath;
      }
      catch
      {
      }

      folderBrowserDialog.Description = "Select SXL objects folder";

      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {

        sCSVObjectFilesPath = folderBrowserDialog.SelectedPath + "\\";

        SelectedObjectFileType = cProcessImage.ObjectFileType.CSVfiles;

        LoadProcessImageObjects();

      }
    }

    private void ToolStripMenuItem_File_LoadObjects_YAML_Click(object sender, EventArgs e)
    {

      OpenFileDialog openFileDialog = new OpenFileDialog();

      openFileDialog.Title = "Select YAML objects file";

      try
      {
        openFileDialog.InitialDirectory = sYAMLFileName;
      }
      catch
      {
      }

      openFileDialog.Filter = "YAML-files (*.yml;*.yaml)|*.yml;*.yaml|All files (*.*)|*.*";

      openFileDialog.RestoreDirectory = true;

      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {

        sYAMLFileName = openFileDialog.FileName;

        SelectedObjectFileType = cProcessImage.ObjectFileType.YAMLfile;

        LoadProcessImageObjects();

      }
    }

    private void toolStripMenuItem_LoadObjects_MRU_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem tsItem = (ToolStripMenuItem)sender;

      string sFileOrPath = cHelper.Item(tsItem.Text, 0, '\t');

      if (sFileOrPath.EndsWith("\\"))
      {
        sCSVObjectFilesPath = sFileOrPath;
        SelectedObjectFileType = cProcessImage.ObjectFileType.CSVfiles;
      }
      else
      {
        sYAMLFileName = sFileOrPath;
        SelectedObjectFileType = cProcessImage.ObjectFileType.YAMLfile;
      }

      LoadProcessImageObjects();

    }

    private void ToolStripMenuItem_File_DropDownOpening(object sender, EventArgs e)
    {

      ToolStripMenuItem_File_LoadObjects.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected ? false : true;
    }

  }
}
