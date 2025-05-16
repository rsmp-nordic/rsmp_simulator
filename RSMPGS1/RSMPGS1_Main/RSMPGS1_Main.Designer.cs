namespace nsRSMPGS
{
  partial class RSMPGS_Main
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RSMPGS_Main));
      this.timer_System = new System.Windows.Forms.Timer(this.components);
      this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
      this.ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_File_LoadObjects = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_File_LoadObjects_CSV = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_File_LoadObjects_YAML = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_File_LoadObjects_Delimiter = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_Delimiter_0 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_File_Debug = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_File_Debug_CreateNew = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_File_Debug_Show = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_File_Debug_Cascade = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_File_Debug_TileH = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_File_Debug_TileV = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_File_Debug_CloseAll = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_StoreBase64Updates = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Delimiter_5 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_File_Close = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_ProcessImage = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_ProcessImage_RandomUpdates = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Delimiter_1 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_ProcessImage_Reset = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Delimiter_2 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_ProcessImage_SaveToFile = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_ProcessImage_LoadFromFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_ProcessImage_Clear = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Connection = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_ConnectAutomatically = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Delimiter_3 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_ConnectNow = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Disconnect = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Delimiter_4 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_SendOptions = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_SendWatchdog = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_SendSomeRandomCrap = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_DisableNagleAlgorithm = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_SplitPackets = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_View = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_View_AlwaysShowGroupHeaders = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItem_View_Clear_AlarmEvents = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_ConnectionStatus = new System.Windows.Forms.ToolStripTextBox();
      this.splitContainer_Main = new System.Windows.Forms.SplitContainer();
      this.splitContainer_ObjectsAndSyslog = new System.Windows.Forms.SplitContainer();
      this.groupBox_SitesAndObjects = new System.Windows.Forms.GroupBox();
      this.checkBox_ShowTooltip = new System.Windows.Forms.CheckBox();
      this.treeView_SitesAndObjects = new System.Windows.Forms.TreeView();
      this.imageList_ListView = new System.Windows.Forms.ImageList(this.components);
      this.groupBox_SystemLog = new System.Windows.Forms.GroupBox();
      this.button_ClearSystemLog = new System.Windows.Forms.Button();
      this.checkBox_ViewOnlyFailedPackets = new System.Windows.Forms.CheckBox();
      this.listView_SysLog = new nsRSMPGS.ListViewDoubleBuffered();
      this.columnHeader_SysLog_Severity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_SysLog_TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_SysLog_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.imageList_Severity = new System.Windows.Forms.ImageList(this.components);
      this.tabControl_Object = new System.Windows.Forms.TabControl();
      this.tabPage_Generic = new System.Windows.Forms.TabPage();
      this.groupBox_Encryption = new System.Windows.Forms.GroupBox();
      this.button_Encryption_CheckRevocation = new System.Windows.Forms.CheckBox();
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate = new System.Windows.Forms.CheckBox();
      this.label_Encryption_ServerName = new System.Windows.Forms.Label();
      this.textBox_Encryption_ServerName = new System.Windows.Forms.TextBox();
      this.button_Encryption_IgnoreCertErrors = new System.Windows.Forms.CheckBox();
      this.button_EncryptionFile_Browse = new System.Windows.Forms.Button();
      this.textBox_EncryptionFile = new System.Windows.Forms.TextBox();
      this.groupBox_EncryptionProtocols = new System.Windows.Forms.GroupBox();
      this.checkBox_Encryption_Protocol_TLS13 = new System.Windows.Forms.CheckBox();
      this.checkBox_Encryption_Protocol_TLS12 = new System.Windows.Forms.CheckBox();
      this.checkBox_Encryption_Protocol_TLS11 = new System.Windows.Forms.CheckBox();
      this.checkBox_Encryption_Protocol_TLS10 = new System.Windows.Forms.CheckBox();
      this.checkBox_Encryption_Protocol_Default = new System.Windows.Forms.CheckBox();
      this.groupBox_ProcessImage = new System.Windows.Forms.GroupBox();
      this.label_ProcessImage_Info_1 = new System.Windows.Forms.Label();
      this.groupBox_ProcessImage_Load = new System.Windows.Forms.GroupBox();
      this.label_ProcessImage_Info_2 = new System.Windows.Forms.Label();
      this.checkBox_ProcessImageLoad_Status = new System.Windows.Forms.CheckBox();
      this.checkBox_ProcessImageLoad_AggregatedStatus = new System.Windows.Forms.CheckBox();
      this.checkBox_ProcessImageLoad_AlarmStatus = new System.Windows.Forms.CheckBox();
      this.checkbox_AutomaticallyLoadProcessData = new System.Windows.Forms.CheckBox();
      this.checkBox_AutomaticallySaveProcessData = new System.Windows.Forms.CheckBox();
      this.groupBox_SXL_Version = new System.Windows.Forms.GroupBox();
      this.checkBox_AutomaticallyLoadObjects = new System.Windows.Forms.CheckBox();
      this.label_SXL_FilePath = new System.Windows.Forms.Label();
      this.textBox_SignalExchangeListPath = new System.Windows.Forms.TextBox();
      this.checkBox_AlwaysUseSXLFromFile = new System.Windows.Forms.CheckBox();
      this.label_SXL_VersionFromFile = new System.Windows.Forms.Label();
      this.textBox_SignalExchangeListVersionFromFile = new System.Windows.Forms.TextBox();
      this.textBox_SignalExchangeListVersion = new System.Windows.Forms.TextBox();
      this.label_SXL_VersionManually = new System.Windows.Forms.Label();
      this.tabPage_RSMP = new System.Windows.Forms.TabPage();
      this.splitContainer_RSMP = new System.Windows.Forms.SplitContainer();
      this.groupBox_ProtocolSettings = new System.Windows.Forms.GroupBox();
      this.button_ResetRSMPSettingToDefault = new System.Windows.Forms.Button();
      this.dataGridView_Behaviour = new System.Windows.Forms.DataGridView();
      this.Column_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Common = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.RSMP_3_1_1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.RSMP_3_1_2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.RSMP_3_1_3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.RSMP_3_1_4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.RSMP_3_1_5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.RSMP_3_2_0 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.RSMP_3_2_1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.RSMP_3_2_2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.groupBox_Statistics = new System.Windows.Forms.GroupBox();
      this.button_ClearStatistics = new System.Windows.Forms.Button();
      this.listView_Statistics = new nsRSMPGS.ListViewDoubleBuffered();
      this.columnHeader_Statistics_Desription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Statistics_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Statistics_Unit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPage_Alarms = new System.Windows.Forms.TabPage();
      this.splitContainer_Alarms = new System.Windows.Forms.SplitContainer();
      this.listView_Alarms = new nsRSMPGS.ListViewDoubleBuffered();
      this.columnHeader_Alarms_Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Alarms_AlarmEvents = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Alarms_AlarmCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Alarms_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Alarms_ExternalAlarmCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Alarms_ExternalNtSAlarmCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Alarms_Priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Alarms_Category = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.contextMenuStrip_Alarm = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ToolStripMenuItem_Active = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Acknowledge = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItem_Suspend = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox_AlarmEvents = new System.Windows.Forms.GroupBox();
      this.listView_AlarmEvents = new nsRSMPGS.ListViewDoubleBuffered();
      this.columnHeader_AlarmEvent_TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_AlarmEvent_RoadSideObject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_AlarmEvent_MsgId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_AlarmEvent_AlarmCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_AlarmEvent_Direction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_AlarmEvent_Event = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPage_AggregatedStatus = new System.Windows.Forms.TabPage();
      this.groupBox_AggregatedStatus_FunctionalState = new System.Windows.Forms.GroupBox();
      this.listBox_AggregatedStatus_FunctionalState = new System.Windows.Forms.ListBox();
      this.groupBox_AggregatedStatus_StatusBits = new System.Windows.Forms.GroupBox();
      this.listView_AggregatedStatus_StatusBits = new nsRSMPGS.ListViewDoubleBuffered();
      this.columnHeader_StatusBits_BitNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_StatusBits_NTSColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_StatusBits_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupBox_AggregatedStatus_FunctionalPosition = new System.Windows.Forms.GroupBox();
      this.listBox_AggregatedStatus_FunctionalPosition = new System.Windows.Forms.ListBox();
      this.button_AggregatedStatus_Send = new System.Windows.Forms.Button();
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged = new System.Windows.Forms.CheckBox();
      this.tabPage_Status = new System.Windows.Forms.TabPage();
      this.groupBox_Status = new System.Windows.Forms.GroupBox();
      this.listView_Status = new nsRSMPGS.ListViewDoubleBuffered();
      this.columnHeader_Status_StatusCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Status_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Status_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Status_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Status_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Status_Comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPage_Commands = new System.Windows.Forms.TabPage();
      this.groupBox_Commands = new System.Windows.Forms.GroupBox();
      this.listView_Commands = new nsRSMPGS.ListViewDoubleBuffered();
      this.columnHeader_Commands_CommandCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Commands_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Commands_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Commands_Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Commands_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Commands_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Commands_Age = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_Commands_Comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPage_TestSend = new System.Windows.Forms.TabPage();
      this.groupBox_TestSend = new System.Windows.Forms.GroupBox();
      this.button_TestPackage_2_Browse = new System.Windows.Forms.Button();
      this.button_TestPackage_1_Browse = new System.Windows.Forms.Button();
      this.button_SendTestPackage_2 = new System.Windows.Forms.Button();
      this.textBox_TestPackage_2 = new System.Windows.Forms.TextBox();
      this.button_SendTestPackage_1 = new System.Windows.Forms.Button();
      this.textBox_TestPackage_1 = new System.Windows.Forms.TextBox();
      this.tabPage_BufferedMessages = new System.Windows.Forms.TabPage();
      this.groupBox_BufferedMessages = new System.Windows.Forms.GroupBox();
      this.textBox_BufferedMessages = new System.Windows.Forms.TextBox();
      this.label_CreateRandomMessages_Total = new System.Windows.Forms.Label();
      this.groupBox_BufferedMessages_CreateRandom = new System.Windows.Forms.GroupBox();
      this.textBox_CreateRandomMessages_Count = new System.Windows.Forms.TextBox();
      this.label_CreateRandomMessages_Number = new System.Windows.Forms.Label();
      this.label_CreateRandomMessages_SelectType = new System.Windows.Forms.Label();
      this.button_BufferedMessages_CreateRandom = new System.Windows.Forms.Button();
      this.comboBox_BufferedMessages_CreateRandom_Type = new System.Windows.Forms.ComboBox();
      this.groupBox_BufferedMessages_Clear = new System.Windows.Forms.GroupBox();
      this.button_ClearAlarmMessages = new System.Windows.Forms.Button();
      this.button_ClearStatusMessages = new System.Windows.Forms.Button();
      this.button_ClearAggStatusMessages = new System.Windows.Forms.Button();
      this.checkBox_ShowMax10BufferedMessagesInSysLog = new System.Windows.Forms.CheckBox();
      this.ListView_BufferedMessages = new nsRSMPGS.ListViewDoubleBuffered();
      this.columnHeader_BufferedMessages_MessageType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_BufferedMessages_MessageId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader_BufferedMessages_SendString = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.openFileDialog_TestPackage = new System.Windows.Forms.OpenFileDialog();
      this.openFileDialog_ProcessImage = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog_ProcessImage = new System.Windows.Forms.SaveFileDialog();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.menuStrip_Main.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).BeginInit();
      this.splitContainer_Main.Panel1.SuspendLayout();
      this.splitContainer_Main.Panel2.SuspendLayout();
      this.splitContainer_Main.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer_ObjectsAndSyslog)).BeginInit();
      this.splitContainer_ObjectsAndSyslog.Panel1.SuspendLayout();
      this.splitContainer_ObjectsAndSyslog.Panel2.SuspendLayout();
      this.splitContainer_ObjectsAndSyslog.SuspendLayout();
      this.groupBox_SitesAndObjects.SuspendLayout();
      this.groupBox_SystemLog.SuspendLayout();
      this.tabControl_Object.SuspendLayout();
      this.tabPage_Generic.SuspendLayout();
      this.groupBox_Encryption.SuspendLayout();
      this.groupBox_EncryptionProtocols.SuspendLayout();
      this.groupBox_ProcessImage.SuspendLayout();
      this.groupBox_ProcessImage_Load.SuspendLayout();
      this.groupBox_SXL_Version.SuspendLayout();
      this.tabPage_RSMP.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer_RSMP)).BeginInit();
      this.splitContainer_RSMP.Panel1.SuspendLayout();
      this.splitContainer_RSMP.Panel2.SuspendLayout();
      this.splitContainer_RSMP.SuspendLayout();
      this.groupBox_ProtocolSettings.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Behaviour)).BeginInit();
      this.groupBox_Statistics.SuspendLayout();
      this.tabPage_Alarms.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Alarms)).BeginInit();
      this.splitContainer_Alarms.Panel1.SuspendLayout();
      this.splitContainer_Alarms.Panel2.SuspendLayout();
      this.splitContainer_Alarms.SuspendLayout();
      this.contextMenuStrip_Alarm.SuspendLayout();
      this.groupBox_AlarmEvents.SuspendLayout();
      this.tabPage_AggregatedStatus.SuspendLayout();
      this.groupBox_AggregatedStatus_FunctionalState.SuspendLayout();
      this.groupBox_AggregatedStatus_StatusBits.SuspendLayout();
      this.groupBox_AggregatedStatus_FunctionalPosition.SuspendLayout();
      this.tabPage_Status.SuspendLayout();
      this.groupBox_Status.SuspendLayout();
      this.tabPage_Commands.SuspendLayout();
      this.groupBox_Commands.SuspendLayout();
      this.tabPage_TestSend.SuspendLayout();
      this.groupBox_TestSend.SuspendLayout();
      this.tabPage_BufferedMessages.SuspendLayout();
      this.groupBox_BufferedMessages.SuspendLayout();
      this.groupBox_BufferedMessages_CreateRandom.SuspendLayout();
      this.groupBox_BufferedMessages_Clear.SuspendLayout();
      this.SuspendLayout();
      // 
      // timer_System
      // 
      this.timer_System.Tick += new System.EventHandler(this.timer_System_Tick);
      // 
      // menuStrip_Main
      // 
      this.menuStrip_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File,
            this.ToolStripMenuItem_ProcessImage,
            this.ToolStripMenuItem_Connection,
            this.ToolStripMenuItem_View,
            this.ToolStripMenuItem_ConnectionStatus});
      this.menuStrip_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
      this.menuStrip_Main.Name = "menuStrip_Main";
      this.menuStrip_Main.Size = new System.Drawing.Size(1427, 31);
      this.menuStrip_Main.TabIndex = 2;
      this.menuStrip_Main.Text = "menuStrip_Main";
      // 
      // ToolStripMenuItem_File
      // 
      this.ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File_LoadObjects,
            this.ToolStripMenuItem_Delimiter_0,
            this.ToolStripMenuItem_File_Debug,
            this.ToolStripMenuItem_Delimiter_5,
            this.ToolStripMenuItem_File_Close});
      this.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File";
      this.ToolStripMenuItem_File.Size = new System.Drawing.Size(46, 27);
      this.ToolStripMenuItem_File.Text = "&File";
      this.ToolStripMenuItem_File.DropDownOpening += new System.EventHandler(this.ToolStripMenuItem_File_DropDownOpening);
      // 
      // ToolStripMenuItem_File_LoadObjects
      // 
      this.ToolStripMenuItem_File_LoadObjects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File_LoadObjects_CSV,
            this.ToolStripMenuItem_File_LoadObjects_YAML,
            this.ToolStripMenuItem_File_LoadObjects_Delimiter});
      this.ToolStripMenuItem_File_LoadObjects.Name = "ToolStripMenuItem_File_LoadObjects";
      this.ToolStripMenuItem_File_LoadObjects.Size = new System.Drawing.Size(224, 26);
      this.ToolStripMenuItem_File_LoadObjects.Text = "Load objects from";
      // 
      // ToolStripMenuItem_File_LoadObjects_CSV
      // 
      this.ToolStripMenuItem_File_LoadObjects_CSV.Name = "ToolStripMenuItem_File_LoadObjects_CSV";
      this.ToolStripMenuItem_File_LoadObjects_CSV.Size = new System.Drawing.Size(165, 26);
      this.ToolStripMenuItem_File_LoadObjects_CSV.Text = "CSV-files...";
      this.ToolStripMenuItem_File_LoadObjects_CSV.Click += new System.EventHandler(this.ToolStripMenuItem_File_LoadObjects_CSV_Click);
      // 
      // ToolStripMenuItem_File_LoadObjects_YAML
      // 
      this.ToolStripMenuItem_File_LoadObjects_YAML.Name = "ToolStripMenuItem_File_LoadObjects_YAML";
      this.ToolStripMenuItem_File_LoadObjects_YAML.Size = new System.Drawing.Size(165, 26);
      this.ToolStripMenuItem_File_LoadObjects_YAML.Text = "YAML-file...";
      this.ToolStripMenuItem_File_LoadObjects_YAML.Click += new System.EventHandler(this.ToolStripMenuItem_File_LoadObjects_YAML_Click);
      // 
      // ToolStripMenuItem_File_LoadObjects_Delimiter
      // 
      this.ToolStripMenuItem_File_LoadObjects_Delimiter.Name = "ToolStripMenuItem_File_LoadObjects_Delimiter";
      this.ToolStripMenuItem_File_LoadObjects_Delimiter.Size = new System.Drawing.Size(162, 6);
      // 
      // ToolStripMenuItem_Delimiter_0
      // 
      this.ToolStripMenuItem_Delimiter_0.Name = "ToolStripMenuItem_Delimiter_0";
      this.ToolStripMenuItem_Delimiter_0.Size = new System.Drawing.Size(221, 6);
      // 
      // ToolStripMenuItem_File_Debug
      // 
      this.ToolStripMenuItem_File_Debug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File_Debug_CreateNew,
            this.toolStripSeparator2,
            this.ToolStripMenuItem_File_Debug_Show,
            this.ToolStripMenuItem_File_Debug_Cascade,
            this.ToolStripMenuItem_File_Debug_TileH,
            this.ToolStripMenuItem_File_Debug_TileV,
            this.toolStripSeparator3,
            this.ToolStripMenuItem_File_Debug_CloseAll,
            this.toolStripMenuItem1,
            this.ToolStripMenuItem_StoreBase64Updates});
      this.ToolStripMenuItem_File_Debug.Name = "ToolStripMenuItem_File_Debug";
      this.ToolStripMenuItem_File_Debug.Size = new System.Drawing.Size(224, 26);
      this.ToolStripMenuItem_File_Debug.Text = "&Debug";
      // 
      // ToolStripMenuItem_File_Debug_CreateNew
      // 
      this.ToolStripMenuItem_File_Debug_CreateNew.Name = "ToolStripMenuItem_File_Debug_CreateNew";
      this.ToolStripMenuItem_File_Debug_CreateNew.Size = new System.Drawing.Size(327, 26);
      this.ToolStripMenuItem_File_Debug_CreateNew.Text = "Create &new debug window";
      this.ToolStripMenuItem_File_Debug_CreateNew.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_CreateNew_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(324, 6);
      // 
      // ToolStripMenuItem_File_Debug_Show
      // 
      this.ToolStripMenuItem_File_Debug_Show.Name = "ToolStripMenuItem_File_Debug_Show";
      this.ToolStripMenuItem_File_Debug_Show.Size = new System.Drawing.Size(327, 26);
      this.ToolStripMenuItem_File_Debug_Show.Text = "Show all debug windows in &front";
      this.ToolStripMenuItem_File_Debug_Show.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_Show_Click);
      // 
      // ToolStripMenuItem_File_Debug_Cascade
      // 
      this.ToolStripMenuItem_File_Debug_Cascade.Name = "ToolStripMenuItem_File_Debug_Cascade";
      this.ToolStripMenuItem_File_Debug_Cascade.Size = new System.Drawing.Size(327, 26);
      this.ToolStripMenuItem_File_Debug_Cascade.Text = "&Cascade all debug windows";
      this.ToolStripMenuItem_File_Debug_Cascade.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_Cascade_Click);
      // 
      // ToolStripMenuItem_File_Debug_TileH
      // 
      this.ToolStripMenuItem_File_Debug_TileH.Name = "ToolStripMenuItem_File_Debug_TileH";
      this.ToolStripMenuItem_File_Debug_TileH.Size = new System.Drawing.Size(327, 26);
      this.ToolStripMenuItem_File_Debug_TileH.Text = "Tile &horizontally all debug windows";
      this.ToolStripMenuItem_File_Debug_TileH.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_TileH_Click);
      // 
      // ToolStripMenuItem_File_Debug_TileV
      // 
      this.ToolStripMenuItem_File_Debug_TileV.Name = "ToolStripMenuItem_File_Debug_TileV";
      this.ToolStripMenuItem_File_Debug_TileV.Size = new System.Drawing.Size(327, 26);
      this.ToolStripMenuItem_File_Debug_TileV.Text = "Tile &vertically all debug windows";
      this.ToolStripMenuItem_File_Debug_TileV.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_Tile_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(324, 6);
      // 
      // ToolStripMenuItem_File_Debug_CloseAll
      // 
      this.ToolStripMenuItem_File_Debug_CloseAll.Name = "ToolStripMenuItem_File_Debug_CloseAll";
      this.ToolStripMenuItem_File_Debug_CloseAll.Size = new System.Drawing.Size(327, 26);
      this.ToolStripMenuItem_File_Debug_CloseAll.Text = "Close all debug &windows";
      this.ToolStripMenuItem_File_Debug_CloseAll.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_CloseAll_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(324, 6);
      // 
      // ToolStripMenuItem_StoreBase64Updates
      // 
      this.ToolStripMenuItem_StoreBase64Updates.CheckOnClick = true;
      this.ToolStripMenuItem_StoreBase64Updates.Name = "ToolStripMenuItem_StoreBase64Updates";
      this.ToolStripMenuItem_StoreBase64Updates.Size = new System.Drawing.Size(327, 26);
      this.ToolStripMenuItem_StoreBase64Updates.Text = "&Store base64 updates";
      // 
      // ToolStripMenuItem_Delimiter_5
      // 
      this.ToolStripMenuItem_Delimiter_5.Name = "ToolStripMenuItem_Delimiter_5";
      this.ToolStripMenuItem_Delimiter_5.Size = new System.Drawing.Size(221, 6);
      // 
      // ToolStripMenuItem_File_Close
      // 
      this.ToolStripMenuItem_File_Close.Name = "ToolStripMenuItem_File_Close";
      this.ToolStripMenuItem_File_Close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
      this.ToolStripMenuItem_File_Close.Size = new System.Drawing.Size(224, 26);
      this.ToolStripMenuItem_File_Close.Text = "&Exit";
      this.ToolStripMenuItem_File_Close.Click += new System.EventHandler(this.ToolStripMenuItem_File_Close_Click);
      // 
      // ToolStripMenuItem_ProcessImage
      // 
      this.ToolStripMenuItem_ProcessImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ProcessImage_RandomUpdates,
            this.ToolStripMenuItem_Delimiter_1,
            this.ToolStripMenuItem_ProcessImage_Reset,
            this.ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues,
            this.ToolStripMenuItem_Delimiter_2,
            this.ToolStripMenuItem_ProcessImage_SaveToFile,
            this.ToolStripMenuItem_ProcessImage_LoadFromFile,
            this.toolStripMenuItem2,
            this.ToolStripMenuItem_ProcessImage_Clear});
      this.ToolStripMenuItem_ProcessImage.Name = "ToolStripMenuItem_ProcessImage";
      this.ToolStripMenuItem_ProcessImage.Size = new System.Drawing.Size(118, 27);
      this.ToolStripMenuItem_ProcessImage.Text = "&Process Image";
      this.ToolStripMenuItem_ProcessImage.DropDownOpening += new System.EventHandler(this.ToolStripMenuItem_ProcessImage_DropDownOpening);
      // 
      // ToolStripMenuItem_ProcessImage_RandomUpdates
      // 
      this.ToolStripMenuItem_ProcessImage_RandomUpdates.Name = "ToolStripMenuItem_ProcessImage_RandomUpdates";
      this.ToolStripMenuItem_ProcessImage_RandomUpdates.Size = new System.Drawing.Size(460, 26);
      this.ToolStripMenuItem_ProcessImage_RandomUpdates.Text = "&Random update all subscriptions";
      this.ToolStripMenuItem_ProcessImage_RandomUpdates.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessImage_RandomUpdates_Click);
      // 
      // ToolStripMenuItem_Delimiter_1
      // 
      this.ToolStripMenuItem_Delimiter_1.Name = "ToolStripMenuItem_Delimiter_1";
      this.ToolStripMenuItem_Delimiter_1.Size = new System.Drawing.Size(457, 6);
      // 
      // ToolStripMenuItem_ProcessImage_Reset
      // 
      this.ToolStripMenuItem_ProcessImage_Reset.Name = "ToolStripMenuItem_ProcessImage_Reset";
      this.ToolStripMenuItem_ProcessImage_Reset.Size = new System.Drawing.Size(460, 26);
      this.ToolStripMenuItem_ProcessImage_Reset.Text = "Reset &Alarm, Status, Aggregated and Command objects";
      this.ToolStripMenuItem_ProcessImage_Reset.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessImage_Reset_Click);
      // 
      // ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues
      // 
      this.ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues.Name = "ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues";
      this.ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues.Size = new System.Drawing.Size(460, 26);
      this.ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues.Text = "Random &update all Status values";
      this.ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues_Click);
      // 
      // ToolStripMenuItem_Delimiter_2
      // 
      this.ToolStripMenuItem_Delimiter_2.Name = "ToolStripMenuItem_Delimiter_2";
      this.ToolStripMenuItem_Delimiter_2.Size = new System.Drawing.Size(457, 6);
      // 
      // ToolStripMenuItem_ProcessImage_SaveToFile
      // 
      this.ToolStripMenuItem_ProcessImage_SaveToFile.Name = "ToolStripMenuItem_ProcessImage_SaveToFile";
      this.ToolStripMenuItem_ProcessImage_SaveToFile.Size = new System.Drawing.Size(460, 26);
      this.ToolStripMenuItem_ProcessImage_SaveToFile.Text = "&Save Process data to file...";
      this.ToolStripMenuItem_ProcessImage_SaveToFile.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessData_SaveToFile_Click);
      // 
      // ToolStripMenuItem_ProcessImage_LoadFromFile
      // 
      this.ToolStripMenuItem_ProcessImage_LoadFromFile.Name = "ToolStripMenuItem_ProcessImage_LoadFromFile";
      this.ToolStripMenuItem_ProcessImage_LoadFromFile.Size = new System.Drawing.Size(460, 26);
      this.ToolStripMenuItem_ProcessImage_LoadFromFile.Text = "&Load Process data from file...";
      this.ToolStripMenuItem_ProcessImage_LoadFromFile.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessData_LoadFromFile_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(457, 6);
      // 
      // ToolStripMenuItem_ProcessImage_Clear
      // 
      this.ToolStripMenuItem_ProcessImage_Clear.Name = "ToolStripMenuItem_ProcessImage_Clear";
      this.ToolStripMenuItem_ProcessImage_Clear.Size = new System.Drawing.Size(460, 26);
      this.ToolStripMenuItem_ProcessImage_Clear.Text = "&Clear Automatically saved Process data";
      this.ToolStripMenuItem_ProcessImage_Clear.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessImage_Clear_Click);
      // 
      // ToolStripMenuItem_Connection
      // 
      this.ToolStripMenuItem_Connection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ConnectAutomatically,
            this.ToolStripMenuItem_Delimiter_3,
            this.ToolStripMenuItem_ConnectNow,
            this.ToolStripMenuItem_Disconnect,
            this.ToolStripMenuItem_Delimiter_4,
            this.ToolStripMenuItem_SendOptions});
      this.ToolStripMenuItem_Connection.Name = "ToolStripMenuItem_Connection";
      this.ToolStripMenuItem_Connection.Size = new System.Drawing.Size(98, 27);
      this.ToolStripMenuItem_Connection.Text = "Connection";
      this.ToolStripMenuItem_Connection.DropDownOpening += new System.EventHandler(this.ToolStripMenuItem_Connection_DropDownOpening);
      // 
      // ToolStripMenuItem_ConnectAutomatically
      // 
      this.ToolStripMenuItem_ConnectAutomatically.CheckOnClick = true;
      this.ToolStripMenuItem_ConnectAutomatically.Name = "ToolStripMenuItem_ConnectAutomatically";
      this.ToolStripMenuItem_ConnectAutomatically.Size = new System.Drawing.Size(240, 26);
      this.ToolStripMenuItem_ConnectAutomatically.Text = "Connect &automatically";
      this.ToolStripMenuItem_ConnectAutomatically.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_ConnectAutomatically_CheckedChanged);
      // 
      // ToolStripMenuItem_Delimiter_3
      // 
      this.ToolStripMenuItem_Delimiter_3.Name = "ToolStripMenuItem_Delimiter_3";
      this.ToolStripMenuItem_Delimiter_3.Size = new System.Drawing.Size(237, 6);
      // 
      // ToolStripMenuItem_ConnectNow
      // 
      this.ToolStripMenuItem_ConnectNow.Name = "ToolStripMenuItem_ConnectNow";
      this.ToolStripMenuItem_ConnectNow.Size = new System.Drawing.Size(240, 26);
      this.ToolStripMenuItem_ConnectNow.Text = "Connect &now";
      this.ToolStripMenuItem_ConnectNow.Click += new System.EventHandler(this.ToolStripMenuItem_ConnectNow_Click);
      // 
      // ToolStripMenuItem_Disconnect
      // 
      this.ToolStripMenuItem_Disconnect.Name = "ToolStripMenuItem_Disconnect";
      this.ToolStripMenuItem_Disconnect.Size = new System.Drawing.Size(240, 26);
      this.ToolStripMenuItem_Disconnect.Text = "&Disconnect";
      this.ToolStripMenuItem_Disconnect.Click += new System.EventHandler(this.ToolStripMenuItem_Disconnect_Click);
      // 
      // ToolStripMenuItem_Delimiter_4
      // 
      this.ToolStripMenuItem_Delimiter_4.Name = "ToolStripMenuItem_Delimiter_4";
      this.ToolStripMenuItem_Delimiter_4.Size = new System.Drawing.Size(237, 6);
      // 
      // ToolStripMenuItem_SendOptions
      // 
      this.ToolStripMenuItem_SendOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_SendWatchdog,
            this.ToolStripMenuItem_SendSomeRandomCrap,
            this.ToolStripMenuItem_DisableNagleAlgorithm,
            this.ToolStripMenuItem_SplitPackets});
      this.ToolStripMenuItem_SendOptions.Name = "ToolStripMenuItem_SendOptions";
      this.ToolStripMenuItem_SendOptions.Size = new System.Drawing.Size(240, 26);
      this.ToolStripMenuItem_SendOptions.Text = "&Send options";
      // 
      // ToolStripMenuItem_SendWatchdog
      // 
      this.ToolStripMenuItem_SendWatchdog.Name = "ToolStripMenuItem_SendWatchdog";
      this.ToolStripMenuItem_SendWatchdog.Size = new System.Drawing.Size(374, 26);
      this.ToolStripMenuItem_SendWatchdog.Text = "Send Watchdog packet now";
      this.ToolStripMenuItem_SendWatchdog.Click += new System.EventHandler(this.ToolStripMenuItem_SendWatchdog_Click);
      // 
      // ToolStripMenuItem_SendSomeRandomCrap
      // 
      this.ToolStripMenuItem_SendSomeRandomCrap.Name = "ToolStripMenuItem_SendSomeRandomCrap";
      this.ToolStripMenuItem_SendSomeRandomCrap.Size = new System.Drawing.Size(374, 26);
      this.ToolStripMenuItem_SendSomeRandomCrap.Text = "Send some &random crap";
      this.ToolStripMenuItem_SendSomeRandomCrap.Click += new System.EventHandler(this.ToolStripMenuItem_SendSomeRandomCrap_Click);
      // 
      // ToolStripMenuItem_DisableNagleAlgorithm
      // 
      this.ToolStripMenuItem_DisableNagleAlgorithm.CheckOnClick = true;
      this.ToolStripMenuItem_DisableNagleAlgorithm.Name = "ToolStripMenuItem_DisableNagleAlgorithm";
      this.ToolStripMenuItem_DisableNagleAlgorithm.Size = new System.Drawing.Size(374, 26);
      this.ToolStripMenuItem_DisableNagleAlgorithm.Text = "Disable &Nagle algorithm (send coalescing)";
      this.ToolStripMenuItem_DisableNagleAlgorithm.Click += new System.EventHandler(this.ToolStripMenuItem_DisableNagleAlgorithm_Click);
      // 
      // ToolStripMenuItem_SplitPackets
      // 
      this.ToolStripMenuItem_SplitPackets.CheckOnClick = true;
      this.ToolStripMenuItem_SplitPackets.Name = "ToolStripMenuItem_SplitPackets";
      this.ToolStripMenuItem_SplitPackets.Size = new System.Drawing.Size(374, 26);
      this.ToolStripMenuItem_SplitPackets.Text = "&Split packets";
      // 
      // ToolStripMenuItem_View
      // 
      this.ToolStripMenuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_View_AlwaysShowGroupHeaders,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_View_Clear_AlarmEvents});
      this.ToolStripMenuItem_View.Name = "ToolStripMenuItem_View";
      this.ToolStripMenuItem_View.Size = new System.Drawing.Size(55, 27);
      this.ToolStripMenuItem_View.Text = "View";
      // 
      // ToolStripMenuItem_View_AlwaysShowGroupHeaders
      // 
      this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.CheckOnClick = true;
      this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.Name = "ToolStripMenuItem_View_AlwaysShowGroupHeaders";
      this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.Size = new System.Drawing.Size(276, 26);
      this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.Text = "&Always show group headers";
      this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_View_AlwaysShowGroupHeaders_CheckedChanged);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(273, 6);
      // 
      // ToolStripMenuItem_View_Clear_AlarmEvents
      // 
      this.ToolStripMenuItem_View_Clear_AlarmEvents.Name = "ToolStripMenuItem_View_Clear_AlarmEvents";
      this.ToolStripMenuItem_View_Clear_AlarmEvents.Size = new System.Drawing.Size(276, 26);
      this.ToolStripMenuItem_View_Clear_AlarmEvents.Text = "Clear &Alarm Events list";
      this.ToolStripMenuItem_View_Clear_AlarmEvents.Click += new System.EventHandler(this.ToolStripMenuItem_View_Clear_AlarmEvents_Click);
      // 
      // ToolStripMenuItem_ConnectionStatus
      // 
      this.ToolStripMenuItem_ConnectionStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.ToolStripMenuItem_ConnectionStatus.AutoSize = false;
      this.ToolStripMenuItem_ConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.ToolStripMenuItem_ConnectionStatus.Name = "ToolStripMenuItem_ConnectionStatus";
      this.ToolStripMenuItem_ConnectionStatus.ReadOnly = true;
      this.ToolStripMenuItem_ConnectionStatus.Size = new System.Drawing.Size(300, 27);
      this.ToolStripMenuItem_ConnectionStatus.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // splitContainer_Main
      // 
      this.splitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer_Main.Location = new System.Drawing.Point(0, 31);
      this.splitContainer_Main.Margin = new System.Windows.Forms.Padding(4);
      this.splitContainer_Main.Name = "splitContainer_Main";
      // 
      // splitContainer_Main.Panel1
      // 
      this.splitContainer_Main.Panel1.Controls.Add(this.splitContainer_ObjectsAndSyslog);
      // 
      // splitContainer_Main.Panel2
      // 
      this.splitContainer_Main.Panel2.Controls.Add(this.tabControl_Object);
      this.splitContainer_Main.Size = new System.Drawing.Size(1427, 682);
      this.splitContainer_Main.SplitterDistance = 468;
      this.splitContainer_Main.SplitterWidth = 5;
      this.splitContainer_Main.TabIndex = 4;
      // 
      // splitContainer_ObjectsAndSyslog
      // 
      this.splitContainer_ObjectsAndSyslog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer_ObjectsAndSyslog.Location = new System.Drawing.Point(0, 0);
      this.splitContainer_ObjectsAndSyslog.Margin = new System.Windows.Forms.Padding(4);
      this.splitContainer_ObjectsAndSyslog.Name = "splitContainer_ObjectsAndSyslog";
      this.splitContainer_ObjectsAndSyslog.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer_ObjectsAndSyslog.Panel1
      // 
      this.splitContainer_ObjectsAndSyslog.Panel1.Controls.Add(this.groupBox_SitesAndObjects);
      // 
      // splitContainer_ObjectsAndSyslog.Panel2
      // 
      this.splitContainer_ObjectsAndSyslog.Panel2.Controls.Add(this.groupBox_SystemLog);
      this.splitContainer_ObjectsAndSyslog.Size = new System.Drawing.Size(468, 682);
      this.splitContainer_ObjectsAndSyslog.SplitterDistance = 404;
      this.splitContainer_ObjectsAndSyslog.SplitterWidth = 5;
      this.splitContainer_ObjectsAndSyslog.TabIndex = 0;
      // 
      // groupBox_SitesAndObjects
      // 
      this.groupBox_SitesAndObjects.Controls.Add(this.checkBox_ShowTooltip);
      this.groupBox_SitesAndObjects.Controls.Add(this.treeView_SitesAndObjects);
      this.groupBox_SitesAndObjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_SitesAndObjects.Location = new System.Drawing.Point(0, 0);
      this.groupBox_SitesAndObjects.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_SitesAndObjects.Name = "groupBox_SitesAndObjects";
      this.groupBox_SitesAndObjects.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_SitesAndObjects.Size = new System.Drawing.Size(468, 404);
      this.groupBox_SitesAndObjects.TabIndex = 1;
      this.groupBox_SitesAndObjects.TabStop = false;
      this.groupBox_SitesAndObjects.Text = "&Sites and Objects";
      // 
      // checkBox_ShowTooltip
      // 
      this.checkBox_ShowTooltip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBox_ShowTooltip.AutoSize = true;
      this.checkBox_ShowTooltip.Location = new System.Drawing.Point(12, 379);
      this.checkBox_ShowTooltip.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_ShowTooltip.Name = "checkBox_ShowTooltip";
      this.checkBox_ShowTooltip.Size = new System.Drawing.Size(195, 20);
      this.checkBox_ShowTooltip.TabIndex = 2;
      this.checkBox_ShowTooltip.Text = "Show all node info in Tooltip";
      this.checkBox_ShowTooltip.UseVisualStyleBackColor = true;
      this.checkBox_ShowTooltip.CheckedChanged += new System.EventHandler(this.checkBox_ShowTooltip_CheckedChanged);
      // 
      // treeView_SitesAndObjects
      // 
      this.treeView_SitesAndObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.treeView_SitesAndObjects.HideSelection = false;
      this.treeView_SitesAndObjects.ImageIndex = 0;
      this.treeView_SitesAndObjects.ImageList = this.imageList_ListView;
      this.treeView_SitesAndObjects.Location = new System.Drawing.Point(8, 23);
      this.treeView_SitesAndObjects.Margin = new System.Windows.Forms.Padding(4);
      this.treeView_SitesAndObjects.Name = "treeView_SitesAndObjects";
      this.treeView_SitesAndObjects.SelectedImageIndex = 0;
      this.treeView_SitesAndObjects.Size = new System.Drawing.Size(451, 344);
      this.treeView_SitesAndObjects.TabIndex = 0;
      this.treeView_SitesAndObjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_SitesAndObjects_AfterSelect);
      // 
      // imageList_ListView
      // 
      this.imageList_ListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_ListView.ImageStream")));
      this.imageList_ListView.TransparentColor = System.Drawing.Color.Magenta;
      this.imageList_ListView.Images.SetKeyName(0, "ListView_Root.bmp");
      this.imageList_ListView.Images.SetKeyName(1, "ListView_Component.bmp");
      this.imageList_ListView.Images.SetKeyName(2, "ListView_ComponentGroup.bmp");
      this.imageList_ListView.Images.SetKeyName(3, "ListView_UnChecked.bmp");
      this.imageList_ListView.Images.SetKeyName(4, "ListView_Checked.bmp");
      // 
      // groupBox_SystemLog
      // 
      this.groupBox_SystemLog.Controls.Add(this.button_ClearSystemLog);
      this.groupBox_SystemLog.Controls.Add(this.checkBox_ViewOnlyFailedPackets);
      this.groupBox_SystemLog.Controls.Add(this.listView_SysLog);
      this.groupBox_SystemLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_SystemLog.Location = new System.Drawing.Point(0, 0);
      this.groupBox_SystemLog.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_SystemLog.Name = "groupBox_SystemLog";
      this.groupBox_SystemLog.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_SystemLog.Size = new System.Drawing.Size(468, 273);
      this.groupBox_SystemLog.TabIndex = 1;
      this.groupBox_SystemLog.TabStop = false;
      this.groupBox_SystemLog.Text = "System &Log";
      // 
      // button_ClearSystemLog
      // 
      this.button_ClearSystemLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button_ClearSystemLog.Location = new System.Drawing.Point(377, 243);
      this.button_ClearSystemLog.Margin = new System.Windows.Forms.Padding(4);
      this.button_ClearSystemLog.Name = "button_ClearSystemLog";
      this.button_ClearSystemLog.Size = new System.Drawing.Size(83, 25);
      this.button_ClearSystemLog.TabIndex = 4;
      this.button_ClearSystemLog.Text = "Clear";
      this.button_ClearSystemLog.UseVisualStyleBackColor = true;
      this.button_ClearSystemLog.Click += new System.EventHandler(this.button_ClearSystemLog_Click);
      // 
      // checkBox_ViewOnlyFailedPackets
      // 
      this.checkBox_ViewOnlyFailedPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBox_ViewOnlyFailedPackets.AutoSize = true;
      this.checkBox_ViewOnlyFailedPackets.Location = new System.Drawing.Point(12, 248);
      this.checkBox_ViewOnlyFailedPackets.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_ViewOnlyFailedPackets.Name = "checkBox_ViewOnlyFailedPackets";
      this.checkBox_ViewOnlyFailedPackets.Size = new System.Drawing.Size(173, 20);
      this.checkBox_ViewOnlyFailedPackets.TabIndex = 3;
      this.checkBox_ViewOnlyFailedPackets.Text = "View only failed packets";
      this.checkBox_ViewOnlyFailedPackets.UseVisualStyleBackColor = true;
      // 
      // listView_SysLog
      // 
      this.listView_SysLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listView_SysLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_SysLog_Severity,
            this.columnHeader_SysLog_TimeStamp,
            this.columnHeader_SysLog_Description});
      this.listView_SysLog.FullRowSelect = true;
      this.listView_SysLog.GridLines = true;
      this.listView_SysLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.listView_SysLog.HideSelection = false;
      this.listView_SysLog.Location = new System.Drawing.Point(7, 21);
      this.listView_SysLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.listView_SysLog.MultiSelect = false;
      this.listView_SysLog.Name = "listView_SysLog";
      this.listView_SysLog.ShowItemToolTips = true;
      this.listView_SysLog.Size = new System.Drawing.Size(451, 215);
      this.listView_SysLog.SmallImageList = this.imageList_Severity;
      this.listView_SysLog.TabIndex = 1;
      this.listView_SysLog.UseCompatibleStateImageBehavior = false;
      this.listView_SysLog.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader_SysLog_Severity
      // 
      this.columnHeader_SysLog_Severity.Text = "Severity";
      this.columnHeader_SysLog_Severity.Width = 10;
      // 
      // columnHeader_SysLog_TimeStamp
      // 
      this.columnHeader_SysLog_TimeStamp.Text = "Time";
      // 
      // columnHeader_SysLog_Description
      // 
      this.columnHeader_SysLog_Description.Text = "Description";
      this.columnHeader_SysLog_Description.Width = 200;
      // 
      // imageList_Severity
      // 
      this.imageList_Severity.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Severity.ImageStream")));
      this.imageList_Severity.TransparentColor = System.Drawing.Color.White;
      this.imageList_Severity.Images.SetKeyName(0, "SysLog_Info.png");
      this.imageList_Severity.Images.SetKeyName(1, "SysLog_Warning.png");
      this.imageList_Severity.Images.SetKeyName(2, "SysLog_Error.png");
      // 
      // tabControl_Object
      // 
      this.tabControl_Object.Controls.Add(this.tabPage_Generic);
      this.tabControl_Object.Controls.Add(this.tabPage_RSMP);
      this.tabControl_Object.Controls.Add(this.tabPage_Alarms);
      this.tabControl_Object.Controls.Add(this.tabPage_AggregatedStatus);
      this.tabControl_Object.Controls.Add(this.tabPage_Status);
      this.tabControl_Object.Controls.Add(this.tabPage_Commands);
      this.tabControl_Object.Controls.Add(this.tabPage_TestSend);
      this.tabControl_Object.Controls.Add(this.tabPage_BufferedMessages);
      this.tabControl_Object.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl_Object.Location = new System.Drawing.Point(0, 0);
      this.tabControl_Object.Margin = new System.Windows.Forms.Padding(4);
      this.tabControl_Object.Multiline = true;
      this.tabControl_Object.Name = "tabControl_Object";
      this.tabControl_Object.SelectedIndex = 0;
      this.tabControl_Object.Size = new System.Drawing.Size(954, 682);
      this.tabControl_Object.TabIndex = 1;
      // 
      // tabPage_Generic
      // 
      this.tabPage_Generic.Controls.Add(this.groupBox_Encryption);
      this.tabPage_Generic.Controls.Add(this.groupBox_ProcessImage);
      this.tabPage_Generic.Controls.Add(this.groupBox_SXL_Version);
      this.tabPage_Generic.Location = new System.Drawing.Point(4, 25);
      this.tabPage_Generic.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage_Generic.Name = "tabPage_Generic";
      this.tabPage_Generic.Padding = new System.Windows.Forms.Padding(4);
      this.tabPage_Generic.Size = new System.Drawing.Size(946, 653);
      this.tabPage_Generic.TabIndex = 0;
      this.tabPage_Generic.Text = "Generic";
      this.tabPage_Generic.UseVisualStyleBackColor = true;
      // 
      // groupBox_Encryption
      // 
      this.groupBox_Encryption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox_Encryption.Controls.Add(this.button_Encryption_CheckRevocation);
      this.groupBox_Encryption.Controls.Add(this.checkBox_Encryption_AuthenticateAsClientUsingCertificate);
      this.groupBox_Encryption.Controls.Add(this.label_Encryption_ServerName);
      this.groupBox_Encryption.Controls.Add(this.textBox_Encryption_ServerName);
      this.groupBox_Encryption.Controls.Add(this.button_Encryption_IgnoreCertErrors);
      this.groupBox_Encryption.Controls.Add(this.button_EncryptionFile_Browse);
      this.groupBox_Encryption.Controls.Add(this.textBox_EncryptionFile);
      this.groupBox_Encryption.Controls.Add(this.groupBox_EncryptionProtocols);
      this.groupBox_Encryption.Location = new System.Drawing.Point(4, 402);
      this.groupBox_Encryption.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBox_Encryption.Name = "groupBox_Encryption";
      this.groupBox_Encryption.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBox_Encryption.Size = new System.Drawing.Size(934, 222);
      this.groupBox_Encryption.TabIndex = 3;
      this.groupBox_Encryption.TabStop = false;
      this.groupBox_Encryption.Text = "&Encryption settings";
      // 
      // button_Encryption_CheckRevocation
      // 
      this.button_Encryption_CheckRevocation.AutoSize = true;
      this.button_Encryption_CheckRevocation.Checked = true;
      this.button_Encryption_CheckRevocation.CheckState = System.Windows.Forms.CheckState.Checked;
      this.button_Encryption_CheckRevocation.Location = new System.Drawing.Point(429, 114);
      this.button_Encryption_CheckRevocation.Margin = new System.Windows.Forms.Padding(4);
      this.button_Encryption_CheckRevocation.Name = "button_Encryption_CheckRevocation";
      this.button_Encryption_CheckRevocation.Size = new System.Drawing.Size(319, 20);
      this.button_Encryption_CheckRevocation.TabIndex = 18;
      this.button_Encryption_CheckRevocation.Text = "Check certificate against certificate revocation list";
      this.button_Encryption_CheckRevocation.UseVisualStyleBackColor = true;
      this.button_Encryption_CheckRevocation.CheckedChanged += new System.EventHandler(this.button_Encryption_CheckRevocation_CheckedChanged);
      // 
      // checkBox_Encryption_AuthenticateAsClientUsingCertificate
      // 
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.AutoSize = true;
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.Checked = true;
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.Location = new System.Drawing.Point(21, 156);
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.Name = "checkBox_Encryption_AuthenticateAsClientUsingCertificate";
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.Size = new System.Drawing.Size(295, 20);
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.TabIndex = 15;
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.Text = "Authenticate as client using this certificate file:";
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.UseVisualStyleBackColor = true;
      this.checkBox_Encryption_AuthenticateAsClientUsingCertificate.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_AuthenticateClient_CheckedChanged);
      // 
      // label_Encryption_ServerName
      // 
      this.label_Encryption_ServerName.AutoSize = true;
      this.label_Encryption_ServerName.Location = new System.Drawing.Point(427, 57);
      this.label_Encryption_ServerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_Encryption_ServerName.Name = "label_Encryption_ServerName";
      this.label_Encryption_ServerName.Size = new System.Drawing.Size(84, 16);
      this.label_Encryption_ServerName.TabIndex = 14;
      this.label_Encryption_ServerName.Text = "Server name";
      this.label_Encryption_ServerName.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // textBox_Encryption_ServerName
      // 
      this.textBox_Encryption_ServerName.Location = new System.Drawing.Point(523, 55);
      this.textBox_Encryption_ServerName.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_Encryption_ServerName.Name = "textBox_Encryption_ServerName";
      this.textBox_Encryption_ServerName.Size = new System.Drawing.Size(256, 22);
      this.textBox_Encryption_ServerName.TabIndex = 13;
      this.textBox_Encryption_ServerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.textBox_Encryption_ServerName.TextChanged += new System.EventHandler(this.textBox_Encryption_ServerName_TextChanged);
      // 
      // button_Encryption_IgnoreCertErrors
      // 
      this.button_Encryption_IgnoreCertErrors.AutoSize = true;
      this.button_Encryption_IgnoreCertErrors.Checked = true;
      this.button_Encryption_IgnoreCertErrors.CheckState = System.Windows.Forms.CheckState.Checked;
      this.button_Encryption_IgnoreCertErrors.Location = new System.Drawing.Point(429, 85);
      this.button_Encryption_IgnoreCertErrors.Margin = new System.Windows.Forms.Padding(4);
      this.button_Encryption_IgnoreCertErrors.Name = "button_Encryption_IgnoreCertErrors";
      this.button_Encryption_IgnoreCertErrors.Size = new System.Drawing.Size(165, 20);
      this.button_Encryption_IgnoreCertErrors.TabIndex = 12;
      this.button_Encryption_IgnoreCertErrors.Text = "Ignore certificate errors";
      this.button_Encryption_IgnoreCertErrors.UseVisualStyleBackColor = true;
      this.button_Encryption_IgnoreCertErrors.CheckedChanged += new System.EventHandler(this.button_Encryption_IgnoreCertErrors_CheckedChanged);
      // 
      // button_EncryptionFile_Browse
      // 
      this.button_EncryptionFile_Browse.Location = new System.Drawing.Point(819, 182);
      this.button_EncryptionFile_Browse.Margin = new System.Windows.Forms.Padding(4);
      this.button_EncryptionFile_Browse.Name = "button_EncryptionFile_Browse";
      this.button_EncryptionFile_Browse.Size = new System.Drawing.Size(83, 25);
      this.button_EncryptionFile_Browse.TabIndex = 11;
      this.button_EncryptionFile_Browse.Text = "Browse";
      this.button_EncryptionFile_Browse.UseVisualStyleBackColor = true;
      this.button_EncryptionFile_Browse.Click += new System.EventHandler(this.button_EncryptionFile_Browse_Click);
      // 
      // textBox_EncryptionFile
      // 
      this.textBox_EncryptionFile.Enabled = false;
      this.textBox_EncryptionFile.Location = new System.Drawing.Point(39, 185);
      this.textBox_EncryptionFile.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_EncryptionFile.Name = "textBox_EncryptionFile";
      this.textBox_EncryptionFile.Size = new System.Drawing.Size(772, 22);
      this.textBox_EncryptionFile.TabIndex = 7;
      this.textBox_EncryptionFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.textBox_EncryptionFile.TextChanged += new System.EventHandler(this.textBox_EncryptionFile_TextChanged);
      // 
      // groupBox_EncryptionProtocols
      // 
      this.groupBox_EncryptionProtocols.Controls.Add(this.checkBox_Encryption_Protocol_TLS13);
      this.groupBox_EncryptionProtocols.Controls.Add(this.checkBox_Encryption_Protocol_TLS12);
      this.groupBox_EncryptionProtocols.Controls.Add(this.checkBox_Encryption_Protocol_TLS11);
      this.groupBox_EncryptionProtocols.Controls.Add(this.checkBox_Encryption_Protocol_TLS10);
      this.groupBox_EncryptionProtocols.Controls.Add(this.checkBox_Encryption_Protocol_Default);
      this.groupBox_EncryptionProtocols.Location = new System.Drawing.Point(21, 34);
      this.groupBox_EncryptionProtocols.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBox_EncryptionProtocols.Name = "groupBox_EncryptionProtocols";
      this.groupBox_EncryptionProtocols.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBox_EncryptionProtocols.Size = new System.Drawing.Size(380, 114);
      this.groupBox_EncryptionProtocols.TabIndex = 6;
      this.groupBox_EncryptionProtocols.TabStop = false;
      this.groupBox_EncryptionProtocols.Text = "&Encryption protocols";
      // 
      // checkBox_Encryption_Protocol_TLS13
      // 
      this.checkBox_Encryption_Protocol_TLS13.AutoSize = true;
      this.checkBox_Encryption_Protocol_TLS13.Checked = true;
      this.checkBox_Encryption_Protocol_TLS13.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_Encryption_Protocol_TLS13.Location = new System.Drawing.Point(125, 80);
      this.checkBox_Encryption_Protocol_TLS13.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_Encryption_Protocol_TLS13.Name = "checkBox_Encryption_Protocol_TLS13";
      this.checkBox_Encryption_Protocol_TLS13.Size = new System.Drawing.Size(74, 20);
      this.checkBox_Encryption_Protocol_TLS13.TabIndex = 12;
      this.checkBox_Encryption_Protocol_TLS13.Text = "TLS 1.3";
      this.checkBox_Encryption_Protocol_TLS13.UseVisualStyleBackColor = true;
      this.checkBox_Encryption_Protocol_TLS13.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_Protocol_CheckedChanged);
      // 
      // checkBox_Encryption_Protocol_TLS12
      // 
      this.checkBox_Encryption_Protocol_TLS12.AutoSize = true;
      this.checkBox_Encryption_Protocol_TLS12.Checked = true;
      this.checkBox_Encryption_Protocol_TLS12.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_Encryption_Protocol_TLS12.Location = new System.Drawing.Point(125, 50);
      this.checkBox_Encryption_Protocol_TLS12.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_Encryption_Protocol_TLS12.Name = "checkBox_Encryption_Protocol_TLS12";
      this.checkBox_Encryption_Protocol_TLS12.Size = new System.Drawing.Size(74, 20);
      this.checkBox_Encryption_Protocol_TLS12.TabIndex = 11;
      this.checkBox_Encryption_Protocol_TLS12.Text = "TLS 1.2";
      this.checkBox_Encryption_Protocol_TLS12.UseVisualStyleBackColor = true;
      this.checkBox_Encryption_Protocol_TLS12.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_Protocol_CheckedChanged);
      // 
      // checkBox_Encryption_Protocol_TLS11
      // 
      this.checkBox_Encryption_Protocol_TLS11.AutoSize = true;
      this.checkBox_Encryption_Protocol_TLS11.Checked = true;
      this.checkBox_Encryption_Protocol_TLS11.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_Encryption_Protocol_TLS11.Location = new System.Drawing.Point(21, 80);
      this.checkBox_Encryption_Protocol_TLS11.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_Encryption_Protocol_TLS11.Name = "checkBox_Encryption_Protocol_TLS11";
      this.checkBox_Encryption_Protocol_TLS11.Size = new System.Drawing.Size(74, 20);
      this.checkBox_Encryption_Protocol_TLS11.TabIndex = 10;
      this.checkBox_Encryption_Protocol_TLS11.Text = "TLS 1.1";
      this.checkBox_Encryption_Protocol_TLS11.UseVisualStyleBackColor = true;
      this.checkBox_Encryption_Protocol_TLS11.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_Protocol_CheckedChanged);
      // 
      // checkBox_Encryption_Protocol_TLS10
      // 
      this.checkBox_Encryption_Protocol_TLS10.AutoSize = true;
      this.checkBox_Encryption_Protocol_TLS10.Checked = true;
      this.checkBox_Encryption_Protocol_TLS10.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_Encryption_Protocol_TLS10.Location = new System.Drawing.Point(21, 50);
      this.checkBox_Encryption_Protocol_TLS10.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_Encryption_Protocol_TLS10.Name = "checkBox_Encryption_Protocol_TLS10";
      this.checkBox_Encryption_Protocol_TLS10.Size = new System.Drawing.Size(74, 20);
      this.checkBox_Encryption_Protocol_TLS10.TabIndex = 9;
      this.checkBox_Encryption_Protocol_TLS10.Text = "TLS 1.0";
      this.checkBox_Encryption_Protocol_TLS10.UseVisualStyleBackColor = true;
      this.checkBox_Encryption_Protocol_TLS10.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_Protocol_CheckedChanged);
      // 
      // checkBox_Encryption_Protocol_Default
      // 
      this.checkBox_Encryption_Protocol_Default.AutoSize = true;
      this.checkBox_Encryption_Protocol_Default.Checked = true;
      this.checkBox_Encryption_Protocol_Default.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_Encryption_Protocol_Default.Location = new System.Drawing.Point(21, 22);
      this.checkBox_Encryption_Protocol_Default.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_Encryption_Protocol_Default.Name = "checkBox_Encryption_Protocol_Default";
      this.checkBox_Encryption_Protocol_Default.Size = new System.Drawing.Size(312, 20);
      this.checkBox_Encryption_Protocol_Default.TabIndex = 7;
      this.checkBox_Encryption_Protocol_Default.Text = "Default (let OS automatically select the protocol)";
      this.checkBox_Encryption_Protocol_Default.UseVisualStyleBackColor = true;
      this.checkBox_Encryption_Protocol_Default.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_Protocol_CheckedChanged);
      // 
      // groupBox_ProcessImage
      // 
      this.groupBox_ProcessImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox_ProcessImage.Controls.Add(this.label_ProcessImage_Info_1);
      this.groupBox_ProcessImage.Controls.Add(this.groupBox_ProcessImage_Load);
      this.groupBox_ProcessImage.Controls.Add(this.checkbox_AutomaticallyLoadProcessData);
      this.groupBox_ProcessImage.Controls.Add(this.checkBox_AutomaticallySaveProcessData);
      this.groupBox_ProcessImage.Location = new System.Drawing.Point(4, 198);
      this.groupBox_ProcessImage.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_ProcessImage.Name = "groupBox_ProcessImage";
      this.groupBox_ProcessImage.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_ProcessImage.Size = new System.Drawing.Size(934, 198);
      this.groupBox_ProcessImage.TabIndex = 2;
      this.groupBox_ProcessImage.TabStop = false;
      this.groupBox_ProcessImage.Text = "Process Image";
      // 
      // label_ProcessImage_Info_1
      // 
      this.label_ProcessImage_Info_1.AutoEllipsis = true;
      this.label_ProcessImage_Info_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label_ProcessImage_Info_1.ForeColor = System.Drawing.SystemColors.Desktop;
      this.label_ProcessImage_Info_1.Location = new System.Drawing.Point(8, 94);
      this.label_ProcessImage_Info_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_ProcessImage_Info_1.Name = "label_ProcessImage_Info_1";
      this.label_ProcessImage_Info_1.Size = new System.Drawing.Size(399, 76);
      this.label_ProcessImage_Info_1.TabIndex = 12;
      this.label_ProcessImage_Info_1.Text = "Data will be saved/loaded from the file \'ProcessImage.dat\' located in the SXL (SU" +
    "L) path. To save/Load from some other file use the \'Process Image\' menu.";
      this.label_ProcessImage_Info_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // groupBox_ProcessImage_Load
      // 
      this.groupBox_ProcessImage_Load.Controls.Add(this.label_ProcessImage_Info_2);
      this.groupBox_ProcessImage_Load.Controls.Add(this.checkBox_ProcessImageLoad_Status);
      this.groupBox_ProcessImage_Load.Controls.Add(this.checkBox_ProcessImageLoad_AggregatedStatus);
      this.groupBox_ProcessImage_Load.Controls.Add(this.checkBox_ProcessImageLoad_AlarmStatus);
      this.groupBox_ProcessImage_Load.Location = new System.Drawing.Point(453, 23);
      this.groupBox_ProcessImage_Load.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_ProcessImage_Load.Name = "groupBox_ProcessImage_Load";
      this.groupBox_ProcessImage_Load.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_ProcessImage_Load.Size = new System.Drawing.Size(445, 158);
      this.groupBox_ProcessImage_Load.TabIndex = 11;
      this.groupBox_ProcessImage_Load.TabStop = false;
      this.groupBox_ProcessImage_Load.Text = "When loading data we should load following:";
      // 
      // label_ProcessImage_Info_2
      // 
      this.label_ProcessImage_Info_2.AutoEllipsis = true;
      this.label_ProcessImage_Info_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label_ProcessImage_Info_2.ForeColor = System.Drawing.SystemColors.Desktop;
      this.label_ProcessImage_Info_2.Location = new System.Drawing.Point(8, 105);
      this.label_ProcessImage_Info_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_ProcessImage_Info_2.Name = "label_ProcessImage_Info_2";
      this.label_ProcessImage_Info_2.Size = new System.Drawing.Size(429, 44);
      this.label_ProcessImage_Info_2.TabIndex = 13;
      this.label_ProcessImage_Info_2.Text = "These settings apply to both automatically load at startup and manually load usin" +
    "g the Process Image menu";
      this.label_ProcessImage_Info_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // checkBox_ProcessImageLoad_Status
      // 
      this.checkBox_ProcessImageLoad_Status.AutoSize = true;
      this.checkBox_ProcessImageLoad_Status.Checked = true;
      this.checkBox_ProcessImageLoad_Status.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_ProcessImageLoad_Status.Location = new System.Drawing.Point(23, 80);
      this.checkBox_ProcessImageLoad_Status.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_ProcessImageLoad_Status.Name = "checkBox_ProcessImageLoad_Status";
      this.checkBox_ProcessImageLoad_Status.Size = new System.Drawing.Size(109, 20);
      this.checkBox_ProcessImageLoad_Status.TabIndex = 12;
      this.checkBox_ProcessImageLoad_Status.Text = "Status values";
      this.checkBox_ProcessImageLoad_Status.UseVisualStyleBackColor = true;
      // 
      // checkBox_ProcessImageLoad_AggregatedStatus
      // 
      this.checkBox_ProcessImageLoad_AggregatedStatus.AutoSize = true;
      this.checkBox_ProcessImageLoad_AggregatedStatus.Checked = true;
      this.checkBox_ProcessImageLoad_AggregatedStatus.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_ProcessImageLoad_AggregatedStatus.Location = new System.Drawing.Point(23, 52);
      this.checkBox_ProcessImageLoad_AggregatedStatus.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_ProcessImageLoad_AggregatedStatus.Name = "checkBox_ProcessImageLoad_AggregatedStatus";
      this.checkBox_ProcessImageLoad_AggregatedStatus.Size = new System.Drawing.Size(184, 20);
      this.checkBox_ProcessImageLoad_AggregatedStatus.TabIndex = 11;
      this.checkBox_ProcessImageLoad_AggregatedStatus.Text = "Aggregated Status values";
      this.checkBox_ProcessImageLoad_AggregatedStatus.UseVisualStyleBackColor = true;
      // 
      // checkBox_ProcessImageLoad_AlarmStatus
      // 
      this.checkBox_ProcessImageLoad_AlarmStatus.AutoSize = true;
      this.checkBox_ProcessImageLoad_AlarmStatus.Checked = true;
      this.checkBox_ProcessImageLoad_AlarmStatus.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_ProcessImageLoad_AlarmStatus.Location = new System.Drawing.Point(23, 23);
      this.checkBox_ProcessImageLoad_AlarmStatus.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_ProcessImageLoad_AlarmStatus.Name = "checkBox_ProcessImageLoad_AlarmStatus";
      this.checkBox_ProcessImageLoad_AlarmStatus.Size = new System.Drawing.Size(102, 20);
      this.checkBox_ProcessImageLoad_AlarmStatus.TabIndex = 10;
      this.checkBox_ProcessImageLoad_AlarmStatus.Text = "Alarm status";
      this.checkBox_ProcessImageLoad_AlarmStatus.UseVisualStyleBackColor = true;
      // 
      // checkbox_AutomaticallyLoadProcessData
      // 
      this.checkbox_AutomaticallyLoadProcessData.AutoSize = true;
      this.checkbox_AutomaticallyLoadProcessData.Checked = true;
      this.checkbox_AutomaticallyLoadProcessData.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkbox_AutomaticallyLoadProcessData.Location = new System.Drawing.Point(36, 36);
      this.checkbox_AutomaticallyLoadProcessData.Margin = new System.Windows.Forms.Padding(4);
      this.checkbox_AutomaticallyLoadProcessData.Name = "checkbox_AutomaticallyLoadProcessData";
      this.checkbox_AutomaticallyLoadProcessData.Size = new System.Drawing.Size(278, 20);
      this.checkbox_AutomaticallyLoadProcessData.TabIndex = 10;
      this.checkbox_AutomaticallyLoadProcessData.Text = "Automatically load process data at startup";
      this.checkbox_AutomaticallyLoadProcessData.UseVisualStyleBackColor = true;
      // 
      // checkBox_AutomaticallySaveProcessData
      // 
      this.checkBox_AutomaticallySaveProcessData.AutoSize = true;
      this.checkBox_AutomaticallySaveProcessData.Checked = true;
      this.checkBox_AutomaticallySaveProcessData.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_AutomaticallySaveProcessData.Location = new System.Drawing.Point(36, 64);
      this.checkBox_AutomaticallySaveProcessData.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_AutomaticallySaveProcessData.Name = "checkBox_AutomaticallySaveProcessData";
      this.checkBox_AutomaticallySaveProcessData.Size = new System.Drawing.Size(265, 20);
      this.checkBox_AutomaticallySaveProcessData.TabIndex = 4;
      this.checkBox_AutomaticallySaveProcessData.Text = "Automatically save process data on exit";
      this.checkBox_AutomaticallySaveProcessData.UseVisualStyleBackColor = true;
      // 
      // groupBox_SXL_Version
      // 
      this.groupBox_SXL_Version.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox_SXL_Version.Controls.Add(this.checkBox_AutomaticallyLoadObjects);
      this.groupBox_SXL_Version.Controls.Add(this.label_SXL_FilePath);
      this.groupBox_SXL_Version.Controls.Add(this.textBox_SignalExchangeListPath);
      this.groupBox_SXL_Version.Controls.Add(this.checkBox_AlwaysUseSXLFromFile);
      this.groupBox_SXL_Version.Controls.Add(this.label_SXL_VersionFromFile);
      this.groupBox_SXL_Version.Controls.Add(this.textBox_SignalExchangeListVersionFromFile);
      this.groupBox_SXL_Version.Controls.Add(this.textBox_SignalExchangeListVersion);
      this.groupBox_SXL_Version.Controls.Add(this.label_SXL_VersionManually);
      this.groupBox_SXL_Version.Location = new System.Drawing.Point(4, 4);
      this.groupBox_SXL_Version.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_SXL_Version.Name = "groupBox_SXL_Version";
      this.groupBox_SXL_Version.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_SXL_Version.Size = new System.Drawing.Size(934, 187);
      this.groupBox_SXL_Version.TabIndex = 0;
      this.groupBox_SXL_Version.TabStop = false;
      this.groupBox_SXL_Version.Text = "Signal Exchange List (SXL) info";
      // 
      // checkBox_AutomaticallyLoadObjects
      // 
      this.checkBox_AutomaticallyLoadObjects.AutoSize = true;
      this.checkBox_AutomaticallyLoadObjects.Checked = true;
      this.checkBox_AutomaticallyLoadObjects.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox_AutomaticallyLoadObjects.Location = new System.Drawing.Point(169, 154);
      this.checkBox_AutomaticallyLoadObjects.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_AutomaticallyLoadObjects.Name = "checkBox_AutomaticallyLoadObjects";
      this.checkBox_AutomaticallyLoadObjects.Size = new System.Drawing.Size(267, 20);
      this.checkBox_AutomaticallyLoadObjects.TabIndex = 11;
      this.checkBox_AutomaticallyLoadObjects.Text = "Automatically load last objects at startup";
      this.checkBox_AutomaticallyLoadObjects.UseVisualStyleBackColor = true;
      // 
      // label_SXL_FilePath
      // 
      this.label_SXL_FilePath.AutoSize = true;
      this.label_SXL_FilePath.Location = new System.Drawing.Point(36, 126);
      this.label_SXL_FilePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_SXL_FilePath.Name = "label_SXL_FilePath";
      this.label_SXL_FilePath.Size = new System.Drawing.Size(60, 16);
      this.label_SXL_FilePath.TabIndex = 9;
      this.label_SXL_FilePath.Text = "SXL path";
      this.label_SXL_FilePath.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // textBox_SignalExchangeListPath
      // 
      this.textBox_SignalExchangeListPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox_SignalExchangeListPath.Location = new System.Drawing.Point(169, 122);
      this.textBox_SignalExchangeListPath.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_SignalExchangeListPath.Name = "textBox_SignalExchangeListPath";
      this.textBox_SignalExchangeListPath.ReadOnly = true;
      this.textBox_SignalExchangeListPath.Size = new System.Drawing.Size(736, 22);
      this.textBox_SignalExchangeListPath.TabIndex = 8;
      // 
      // checkBox_AlwaysUseSXLFromFile
      // 
      this.checkBox_AlwaysUseSXLFromFile.Location = new System.Drawing.Point(39, 94);
      this.checkBox_AlwaysUseSXLFromFile.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_AlwaysUseSXLFromFile.Name = "checkBox_AlwaysUseSXLFromFile";
      this.checkBox_AlwaysUseSXLFromFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.checkBox_AlwaysUseSXLFromFile.Size = new System.Drawing.Size(385, 21);
      this.checkBox_AlwaysUseSXLFromFile.TabIndex = 7;
      this.checkBox_AlwaysUseSXLFromFile.Text = "(Always use the SXL version from file (if found";
      this.checkBox_AlwaysUseSXLFromFile.UseVisualStyleBackColor = true;
      this.checkBox_AlwaysUseSXLFromFile.CheckedChanged += new System.EventHandler(this.checkBox_AlwaysUseSXLFromFile_CheckedChanged);
      // 
      // label_SXL_VersionFromFile
      // 
      this.label_SXL_VersionFromFile.AutoSize = true;
      this.label_SXL_VersionFromFile.Location = new System.Drawing.Point(184, 65);
      this.label_SXL_VersionFromFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_SXL_VersionFromFile.Name = "label_SXL_VersionFromFile";
      this.label_SXL_VersionFromFile.Size = new System.Drawing.Size(147, 16);
      this.label_SXL_VersionFromFile.TabIndex = 6;
      this.label_SXL_VersionFromFile.Text = "SXL version found in file";
      this.label_SXL_VersionFromFile.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // textBox_SignalExchangeListVersionFromFile
      // 
      this.textBox_SignalExchangeListVersionFromFile.Enabled = false;
      this.textBox_SignalExchangeListVersionFromFile.Location = new System.Drawing.Point(405, 62);
      this.textBox_SignalExchangeListVersionFromFile.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_SignalExchangeListVersionFromFile.Name = "textBox_SignalExchangeListVersionFromFile";
      this.textBox_SignalExchangeListVersionFromFile.Size = new System.Drawing.Size(139, 22);
      this.textBox_SignalExchangeListVersionFromFile.TabIndex = 5;
      this.textBox_SignalExchangeListVersionFromFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // textBox_SignalExchangeListVersion
      // 
      this.textBox_SignalExchangeListVersion.Location = new System.Drawing.Point(405, 30);
      this.textBox_SignalExchangeListVersion.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_SignalExchangeListVersion.Name = "textBox_SignalExchangeListVersion";
      this.textBox_SignalExchangeListVersion.Size = new System.Drawing.Size(139, 22);
      this.textBox_SignalExchangeListVersion.TabIndex = 1;
      this.textBox_SignalExchangeListVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label_SXL_VersionManually
      // 
      this.label_SXL_VersionManually.AutoSize = true;
      this.label_SXL_VersionManually.Location = new System.Drawing.Point(32, 33);
      this.label_SXL_VersionManually.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_SXL_VersionManually.Name = "label_SXL_VersionManually";
      this.label_SXL_VersionManually.Size = new System.Drawing.Size(286, 16);
      this.label_SXL_VersionManually.TabIndex = 0;
      this.label_SXL_VersionManually.Text = "Active SXL version to be used when connecting";
      this.label_SXL_VersionManually.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // tabPage_RSMP
      // 
      this.tabPage_RSMP.Controls.Add(this.splitContainer_RSMP);
      this.tabPage_RSMP.Location = new System.Drawing.Point(4, 25);
      this.tabPage_RSMP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.tabPage_RSMP.Name = "tabPage_RSMP";
      this.tabPage_RSMP.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.tabPage_RSMP.Size = new System.Drawing.Size(946, 653);
      this.tabPage_RSMP.TabIndex = 7;
      this.tabPage_RSMP.Text = "RSMP";
      this.tabPage_RSMP.UseVisualStyleBackColor = true;
      // 
      // splitContainer_RSMP
      // 
      this.splitContainer_RSMP.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer_RSMP.Location = new System.Drawing.Point(3, 2);
      this.splitContainer_RSMP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.splitContainer_RSMP.Name = "splitContainer_RSMP";
      this.splitContainer_RSMP.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer_RSMP.Panel1
      // 
      this.splitContainer_RSMP.Panel1.Controls.Add(this.groupBox_ProtocolSettings);
      // 
      // splitContainer_RSMP.Panel2
      // 
      this.splitContainer_RSMP.Panel2.Controls.Add(this.groupBox_Statistics);
      this.splitContainer_RSMP.Size = new System.Drawing.Size(940, 649);
      this.splitContainer_RSMP.SplitterDistance = 387;
      this.splitContainer_RSMP.TabIndex = 10;
      // 
      // groupBox_ProtocolSettings
      // 
      this.groupBox_ProtocolSettings.Controls.Add(this.button_ResetRSMPSettingToDefault);
      this.groupBox_ProtocolSettings.Controls.Add(this.dataGridView_Behaviour);
      this.groupBox_ProtocolSettings.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_ProtocolSettings.Location = new System.Drawing.Point(0, 0);
      this.groupBox_ProtocolSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBox_ProtocolSettings.Name = "groupBox_ProtocolSettings";
      this.groupBox_ProtocolSettings.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBox_ProtocolSettings.Size = new System.Drawing.Size(940, 387);
      this.groupBox_ProtocolSettings.TabIndex = 9;
      this.groupBox_ProtocolSettings.TabStop = false;
      this.groupBox_ProtocolSettings.Text = "&Behaviour";
      // 
      // button_ResetRSMPSettingToDefault
      // 
      this.button_ResetRSMPSettingToDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button_ResetRSMPSettingToDefault.Location = new System.Drawing.Point(631, 355);
      this.button_ResetRSMPSettingToDefault.Margin = new System.Windows.Forms.Padding(4);
      this.button_ResetRSMPSettingToDefault.Name = "button_ResetRSMPSettingToDefault";
      this.button_ResetRSMPSettingToDefault.Size = new System.Drawing.Size(301, 25);
      this.button_ResetRSMPSettingToDefault.TabIndex = 9;
      this.button_ResetRSMPSettingToDefault.Text = "Reset behaviour to default";
      this.button_ResetRSMPSettingToDefault.UseVisualStyleBackColor = true;
      this.button_ResetRSMPSettingToDefault.Click += new System.EventHandler(this.button_ResetRSMPSettingToDefault_Click);
      // 
      // dataGridView_Behaviour
      // 
      this.dataGridView_Behaviour.AllowUserToAddRows = false;
      this.dataGridView_Behaviour.AllowUserToDeleteRows = false;
      this.dataGridView_Behaviour.AllowUserToResizeRows = false;
      this.dataGridView_Behaviour.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridView_Behaviour.BackgroundColor = System.Drawing.SystemColors.Window;
      this.dataGridView_Behaviour.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dataGridView_Behaviour.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView_Behaviour.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Description,
            this.Common,
            this.RSMP_3_1_1,
            this.RSMP_3_1_2,
            this.RSMP_3_1_3,
            this.RSMP_3_1_4,
            this.RSMP_3_1_5,
            this.RSMP_3_2_0,
            this.RSMP_3_2_1,
            this.RSMP_3_2_2});
      this.dataGridView_Behaviour.Location = new System.Drawing.Point(3, 18);
      this.dataGridView_Behaviour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.dataGridView_Behaviour.MultiSelect = false;
      this.dataGridView_Behaviour.Name = "dataGridView_Behaviour";
      this.dataGridView_Behaviour.RowHeadersVisible = false;
      this.dataGridView_Behaviour.RowHeadersWidth = 51;
      this.dataGridView_Behaviour.RowTemplate.Height = 24;
      this.dataGridView_Behaviour.Size = new System.Drawing.Size(928, 332);
      this.dataGridView_Behaviour.TabIndex = 17;
      this.dataGridView_Behaviour.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Behaviour_CellContentClick);
      this.dataGridView_Behaviour.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Behaviour_CellValueChanged);
      // 
      // Column_Description
      // 
      this.Column_Description.HeaderText = "Parameter";
      this.Column_Description.MinimumWidth = 6;
      this.Column_Description.Name = "Column_Description";
      this.Column_Description.ReadOnly = true;
      this.Column_Description.Width = 500;
      // 
      // Common
      // 
      this.Common.HeaderText = "Common";
      this.Common.MinimumWidth = 6;
      this.Common.Name = "Common";
      this.Common.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.Common.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.Common.Width = 80;
      // 
      // RSMP_3_1_1
      // 
      this.RSMP_3_1_1.HeaderText = "3.1.1";
      this.RSMP_3_1_1.MinimumWidth = 6;
      this.RSMP_3_1_1.Name = "RSMP_3_1_1";
      this.RSMP_3_1_1.Width = 50;
      // 
      // RSMP_3_1_2
      // 
      this.RSMP_3_1_2.HeaderText = "3.1.2";
      this.RSMP_3_1_2.MinimumWidth = 6;
      this.RSMP_3_1_2.Name = "RSMP_3_1_2";
      this.RSMP_3_1_2.Width = 50;
      // 
      // RSMP_3_1_3
      // 
      this.RSMP_3_1_3.HeaderText = "3.1.3";
      this.RSMP_3_1_3.MinimumWidth = 6;
      this.RSMP_3_1_3.Name = "RSMP_3_1_3";
      this.RSMP_3_1_3.Width = 50;
      // 
      // RSMP_3_1_4
      // 
      this.RSMP_3_1_4.HeaderText = "3.1.4";
      this.RSMP_3_1_4.MinimumWidth = 6;
      this.RSMP_3_1_4.Name = "RSMP_3_1_4";
      this.RSMP_3_1_4.Width = 50;
      // 
      // RSMP_3_1_5
      // 
      this.RSMP_3_1_5.HeaderText = "3.1.5";
      this.RSMP_3_1_5.MinimumWidth = 6;
      this.RSMP_3_1_5.Name = "RSMP_3_1_5";
      this.RSMP_3_1_5.Width = 50;
      // 
      // RSMP_3_2_0
      // 
      this.RSMP_3_2_0.HeaderText = "3.2.0";
      this.RSMP_3_2_0.MinimumWidth = 6;
      this.RSMP_3_2_0.Name = "RSMP_3_2_0";
      this.RSMP_3_2_0.Width = 50;
      // 
      // RSMP_3_2_1
      // 
      this.RSMP_3_2_1.HeaderText = "3.2.1";
      this.RSMP_3_2_1.MinimumWidth = 6;
      this.RSMP_3_2_1.Name = "RSMP_3_2_1";
      this.RSMP_3_2_1.Width = 50;
      // 
      // RSMP_3_2_2
      // 
      this.RSMP_3_2_2.HeaderText = "3.2.2";
      this.RSMP_3_2_2.MinimumWidth = 6;
      this.RSMP_3_2_2.Name = "RSMP_3_2_2";
      this.RSMP_3_2_2.Width = 50;
      // 
      // groupBox_Statistics
      // 
      this.groupBox_Statistics.Controls.Add(this.button_ClearStatistics);
      this.groupBox_Statistics.Controls.Add(this.listView_Statistics);
      this.groupBox_Statistics.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_Statistics.Location = new System.Drawing.Point(0, 0);
      this.groupBox_Statistics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBox_Statistics.Name = "groupBox_Statistics";
      this.groupBox_Statistics.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.groupBox_Statistics.Size = new System.Drawing.Size(940, 258);
      this.groupBox_Statistics.TabIndex = 7;
      this.groupBox_Statistics.TabStop = false;
      this.groupBox_Statistics.Text = "&Connection statistics";
      // 
      // button_ClearStatistics
      // 
      this.button_ClearStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button_ClearStatistics.Location = new System.Drawing.Point(848, 226);
      this.button_ClearStatistics.Margin = new System.Windows.Forms.Padding(4);
      this.button_ClearStatistics.Name = "button_ClearStatistics";
      this.button_ClearStatistics.Size = new System.Drawing.Size(83, 25);
      this.button_ClearStatistics.TabIndex = 10;
      this.button_ClearStatistics.Text = "Clear";
      this.button_ClearStatistics.UseVisualStyleBackColor = true;
      this.button_ClearStatistics.Click += new System.EventHandler(this.button_ClearStatistics_Click);
      // 
      // listView_Statistics
      // 
      this.listView_Statistics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listView_Statistics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Statistics_Desription,
            this.columnHeader_Statistics_Value,
            this.columnHeader_Statistics_Unit});
      this.listView_Statistics.FullRowSelect = true;
      this.listView_Statistics.GridLines = true;
      this.listView_Statistics.HideSelection = false;
      this.listView_Statistics.Location = new System.Drawing.Point(3, 18);
      this.listView_Statistics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.listView_Statistics.Name = "listView_Statistics";
      this.listView_Statistics.Size = new System.Drawing.Size(931, 201);
      this.listView_Statistics.TabIndex = 0;
      this.listView_Statistics.UseCompatibleStateImageBehavior = false;
      this.listView_Statistics.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader_Statistics_Desription
      // 
      this.columnHeader_Statistics_Desription.Text = "Description";
      this.columnHeader_Statistics_Desription.Width = 200;
      // 
      // columnHeader_Statistics_Value
      // 
      this.columnHeader_Statistics_Value.Text = "Value";
      this.columnHeader_Statistics_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_Statistics_Value.Width = 100;
      // 
      // columnHeader_Statistics_Unit
      // 
      this.columnHeader_Statistics_Unit.Text = "Unit";
      this.columnHeader_Statistics_Unit.Width = 100;
      // 
      // tabPage_Alarms
      // 
      this.tabPage_Alarms.Controls.Add(this.splitContainer_Alarms);
      this.tabPage_Alarms.Location = new System.Drawing.Point(4, 25);
      this.tabPage_Alarms.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage_Alarms.Name = "tabPage_Alarms";
      this.tabPage_Alarms.Padding = new System.Windows.Forms.Padding(4);
      this.tabPage_Alarms.Size = new System.Drawing.Size(946, 653);
      this.tabPage_Alarms.TabIndex = 1;
      this.tabPage_Alarms.Text = "Alarms";
      this.tabPage_Alarms.UseVisualStyleBackColor = true;
      // 
      // splitContainer_Alarms
      // 
      this.splitContainer_Alarms.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer_Alarms.Location = new System.Drawing.Point(4, 4);
      this.splitContainer_Alarms.Margin = new System.Windows.Forms.Padding(4);
      this.splitContainer_Alarms.Name = "splitContainer_Alarms";
      this.splitContainer_Alarms.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer_Alarms.Panel1
      // 
      this.splitContainer_Alarms.Panel1.Controls.Add(this.listView_Alarms);
      // 
      // splitContainer_Alarms.Panel2
      // 
      this.splitContainer_Alarms.Panel2.Controls.Add(this.groupBox_AlarmEvents);
      this.splitContainer_Alarms.Size = new System.Drawing.Size(938, 645);
      this.splitContainer_Alarms.SplitterDistance = 428;
      this.splitContainer_Alarms.SplitterWidth = 5;
      this.splitContainer_Alarms.TabIndex = 0;
      // 
      // listView_Alarms
      // 
      this.listView_Alarms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Alarms_Status,
            this.columnHeader_Alarms_AlarmEvents,
            this.columnHeader_Alarms_AlarmCodeId,
            this.columnHeader_Alarms_Description,
            this.columnHeader_Alarms_ExternalAlarmCodeId,
            this.columnHeader_Alarms_ExternalNtSAlarmCodeId,
            this.columnHeader_Alarms_Priority,
            this.columnHeader_Alarms_Category});
      this.listView_Alarms.ContextMenuStrip = this.contextMenuStrip_Alarm;
      this.listView_Alarms.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView_Alarms.FullRowSelect = true;
      this.listView_Alarms.GridLines = true;
      this.listView_Alarms.HideSelection = false;
      this.listView_Alarms.Location = new System.Drawing.Point(0, 0);
      this.listView_Alarms.Margin = new System.Windows.Forms.Padding(4);
      this.listView_Alarms.MultiSelect = false;
      this.listView_Alarms.Name = "listView_Alarms";
      this.listView_Alarms.Size = new System.Drawing.Size(938, 428);
      this.listView_Alarms.TabIndex = 1;
      this.listView_Alarms.UseCompatibleStateImageBehavior = false;
      this.listView_Alarms.View = System.Windows.Forms.View.Details;
      this.listView_Alarms.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
      this.listView_Alarms.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_Alarms_MouseClick);
      this.listView_Alarms.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Alarms_MouseDoubleClick);
      // 
      // columnHeader_Alarms_Status
      // 
      this.columnHeader_Alarms_Status.Text = "Status";
      this.columnHeader_Alarms_Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_Alarms_Status.Width = 104;
      // 
      // columnHeader_Alarms_AlarmEvents
      // 
      this.columnHeader_Alarms_AlarmEvents.Text = "Active Events";
      this.columnHeader_Alarms_AlarmEvents.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // columnHeader_Alarms_AlarmCodeId
      // 
      this.columnHeader_Alarms_AlarmCodeId.Tag = "AlarmCodeId";
      this.columnHeader_Alarms_AlarmCodeId.Text = "AlarmCodeId";
      this.columnHeader_Alarms_AlarmCodeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_Alarms_AlarmCodeId.Width = 83;
      // 
      // columnHeader_Alarms_Description
      // 
      this.columnHeader_Alarms_Description.Tag = "Description";
      this.columnHeader_Alarms_Description.Text = "Description";
      this.columnHeader_Alarms_Description.Width = 154;
      // 
      // columnHeader_Alarms_ExternalAlarmCodeId
      // 
      this.columnHeader_Alarms_ExternalAlarmCodeId.Tag = "ExternalAlarmCodeId";
      this.columnHeader_Alarms_ExternalAlarmCodeId.Text = "ExternalAlarmCodeId";
      this.columnHeader_Alarms_ExternalAlarmCodeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_Alarms_ExternalAlarmCodeId.Width = 127;
      // 
      // columnHeader_Alarms_ExternalNtSAlarmCodeId
      // 
      this.columnHeader_Alarms_ExternalNtSAlarmCodeId.Tag = "ExternalNtSAlarmCodeId";
      this.columnHeader_Alarms_ExternalNtSAlarmCodeId.Text = "ExternalNTSAlarmCodeId";
      this.columnHeader_Alarms_ExternalNtSAlarmCodeId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_Alarms_ExternalNtSAlarmCodeId.Width = 143;
      // 
      // columnHeader_Alarms_Priority
      // 
      this.columnHeader_Alarms_Priority.Tag = "Priority";
      this.columnHeader_Alarms_Priority.Text = "Priority";
      this.columnHeader_Alarms_Priority.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_Alarms_Priority.Width = 63;
      // 
      // columnHeader_Alarms_Category
      // 
      this.columnHeader_Alarms_Category.Tag = "Category";
      this.columnHeader_Alarms_Category.Text = "Category";
      this.columnHeader_Alarms_Category.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // contextMenuStrip_Alarm
      // 
      this.contextMenuStrip_Alarm.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.contextMenuStrip_Alarm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Active,
            this.ToolStripMenuItem_Acknowledge,
            this.ToolStripMenuItem_Suspend});
      this.contextMenuStrip_Alarm.Name = "contextMenuStrip_Alarm";
      this.contextMenuStrip_Alarm.Size = new System.Drawing.Size(169, 76);
      this.contextMenuStrip_Alarm.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Alarm_Opening);
      // 
      // ToolStripMenuItem_Active
      // 
      this.ToolStripMenuItem_Active.Name = "ToolStripMenuItem_Active";
      this.ToolStripMenuItem_Active.Size = new System.Drawing.Size(168, 24);
      this.ToolStripMenuItem_Active.Tag = "ActiveAndSend";
      this.ToolStripMenuItem_Active.Text = "&Activate";
      this.ToolStripMenuItem_Active.Click += new System.EventHandler(this.ToolStripMenuItem_SendAlarmEvent_Click);
      // 
      // ToolStripMenuItem_Acknowledge
      // 
      this.ToolStripMenuItem_Acknowledge.Name = "ToolStripMenuItem_Acknowledge";
      this.ToolStripMenuItem_Acknowledge.Size = new System.Drawing.Size(168, 24);
      this.ToolStripMenuItem_Acknowledge.Tag = "AcknowledgeAndSend";
      this.ToolStripMenuItem_Acknowledge.Text = "&Acknowledge";
      this.ToolStripMenuItem_Acknowledge.Click += new System.EventHandler(this.ToolStripMenuItem_SendAlarmEvent_Click);
      // 
      // ToolStripMenuItem_Suspend
      // 
      this.ToolStripMenuItem_Suspend.Name = "ToolStripMenuItem_Suspend";
      this.ToolStripMenuItem_Suspend.Size = new System.Drawing.Size(168, 24);
      this.ToolStripMenuItem_Suspend.Tag = "SuspendAndSend";
      this.ToolStripMenuItem_Suspend.Text = "&Suspend";
      this.ToolStripMenuItem_Suspend.Click += new System.EventHandler(this.ToolStripMenuItem_SendAlarmEvent_Click);
      // 
      // groupBox_AlarmEvents
      // 
      this.groupBox_AlarmEvents.Controls.Add(this.listView_AlarmEvents);
      this.groupBox_AlarmEvents.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_AlarmEvents.Location = new System.Drawing.Point(0, 0);
      this.groupBox_AlarmEvents.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_AlarmEvents.Name = "groupBox_AlarmEvents";
      this.groupBox_AlarmEvents.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_AlarmEvents.Size = new System.Drawing.Size(938, 212);
      this.groupBox_AlarmEvents.TabIndex = 0;
      this.groupBox_AlarmEvents.TabStop = false;
      this.groupBox_AlarmEvents.Text = "Alarm &Events";
      // 
      // listView_AlarmEvents
      // 
      this.listView_AlarmEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_AlarmEvent_TimeStamp,
            this.columnHeader_AlarmEvent_RoadSideObject,
            this.columnHeader_AlarmEvent_MsgId,
            this.columnHeader_AlarmEvent_AlarmCodeId,
            this.columnHeader_AlarmEvent_Direction,
            this.columnHeader_AlarmEvent_Event});
      this.listView_AlarmEvents.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView_AlarmEvents.FullRowSelect = true;
      this.listView_AlarmEvents.GridLines = true;
      this.listView_AlarmEvents.HideSelection = false;
      this.listView_AlarmEvents.Location = new System.Drawing.Point(4, 19);
      this.listView_AlarmEvents.Margin = new System.Windows.Forms.Padding(4);
      this.listView_AlarmEvents.MultiSelect = false;
      this.listView_AlarmEvents.Name = "listView_AlarmEvents";
      this.listView_AlarmEvents.Size = new System.Drawing.Size(930, 189);
      this.listView_AlarmEvents.TabIndex = 3;
      this.listView_AlarmEvents.UseCompatibleStateImageBehavior = false;
      this.listView_AlarmEvents.View = System.Windows.Forms.View.Details;
      this.listView_AlarmEvents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
      // 
      // columnHeader_AlarmEvent_TimeStamp
      // 
      this.columnHeader_AlarmEvent_TimeStamp.Tag = "AlarmEvent_Timestamp";
      this.columnHeader_AlarmEvent_TimeStamp.Text = "Timestamp";
      this.columnHeader_AlarmEvent_TimeStamp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_AlarmEvent_TimeStamp.Width = 181;
      // 
      // columnHeader_AlarmEvent_RoadSideObject
      // 
      this.columnHeader_AlarmEvent_RoadSideObject.Tag = "AlarmEvent_RoadSideObject";
      this.columnHeader_AlarmEvent_RoadSideObject.Text = "Object";
      this.columnHeader_AlarmEvent_RoadSideObject.Width = 100;
      // 
      // columnHeader_AlarmEvent_MsgId
      // 
      this.columnHeader_AlarmEvent_MsgId.Tag = "AlarmEvent_MsgId";
      this.columnHeader_AlarmEvent_MsgId.Text = "MessageId";
      this.columnHeader_AlarmEvent_MsgId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_AlarmEvent_MsgId.Width = 260;
      // 
      // columnHeader_AlarmEvent_AlarmCodeId
      // 
      this.columnHeader_AlarmEvent_AlarmCodeId.Tag = "AlarmEvent_AlarmCodeId";
      this.columnHeader_AlarmEvent_AlarmCodeId.Text = "AlarmCodeId";
      // 
      // columnHeader_AlarmEvent_Direction
      // 
      this.columnHeader_AlarmEvent_Direction.Tag = "AlarmEvent_Direction";
      this.columnHeader_AlarmEvent_Direction.Text = "Direction";
      this.columnHeader_AlarmEvent_Direction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // columnHeader_AlarmEvent_Event
      // 
      this.columnHeader_AlarmEvent_Event.Tag = "AlarmEvent_Event";
      this.columnHeader_AlarmEvent_Event.Text = "Event";
      this.columnHeader_AlarmEvent_Event.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // tabPage_AggregatedStatus
      // 
      this.tabPage_AggregatedStatus.Controls.Add(this.groupBox_AggregatedStatus_FunctionalState);
      this.tabPage_AggregatedStatus.Controls.Add(this.groupBox_AggregatedStatus_StatusBits);
      this.tabPage_AggregatedStatus.Controls.Add(this.groupBox_AggregatedStatus_FunctionalPosition);
      this.tabPage_AggregatedStatus.Controls.Add(this.button_AggregatedStatus_Send);
      this.tabPage_AggregatedStatus.Controls.Add(this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged);
      this.tabPage_AggregatedStatus.Location = new System.Drawing.Point(4, 25);
      this.tabPage_AggregatedStatus.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage_AggregatedStatus.Name = "tabPage_AggregatedStatus";
      this.tabPage_AggregatedStatus.Padding = new System.Windows.Forms.Padding(4);
      this.tabPage_AggregatedStatus.Size = new System.Drawing.Size(946, 653);
      this.tabPage_AggregatedStatus.TabIndex = 3;
      this.tabPage_AggregatedStatus.Text = "Aggregated Status";
      this.tabPage_AggregatedStatus.UseVisualStyleBackColor = true;
      // 
      // groupBox_AggregatedStatus_FunctionalState
      // 
      this.groupBox_AggregatedStatus_FunctionalState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox_AggregatedStatus_FunctionalState.Controls.Add(this.listBox_AggregatedStatus_FunctionalState);
      this.groupBox_AggregatedStatus_FunctionalState.Location = new System.Drawing.Point(404, 356);
      this.groupBox_AggregatedStatus_FunctionalState.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_AggregatedStatus_FunctionalState.Name = "groupBox_AggregatedStatus_FunctionalState";
      this.groupBox_AggregatedStatus_FunctionalState.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_AggregatedStatus_FunctionalState.Size = new System.Drawing.Size(525, 247);
      this.groupBox_AggregatedStatus_FunctionalState.TabIndex = 11;
      this.groupBox_AggregatedStatus_FunctionalState.TabStop = false;
      this.groupBox_AggregatedStatus_FunctionalState.Text = "&FunctionalState";
      // 
      // listBox_AggregatedStatus_FunctionalState
      // 
      this.listBox_AggregatedStatus_FunctionalState.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBox_AggregatedStatus_FunctionalState.FormattingEnabled = true;
      this.listBox_AggregatedStatus_FunctionalState.ItemHeight = 16;
      this.listBox_AggregatedStatus_FunctionalState.Location = new System.Drawing.Point(4, 19);
      this.listBox_AggregatedStatus_FunctionalState.Margin = new System.Windows.Forms.Padding(4);
      this.listBox_AggregatedStatus_FunctionalState.Name = "listBox_AggregatedStatus_FunctionalState";
      this.listBox_AggregatedStatus_FunctionalState.Size = new System.Drawing.Size(517, 224);
      this.listBox_AggregatedStatus_FunctionalState.TabIndex = 0;
      this.listBox_AggregatedStatus_FunctionalState.SelectedIndexChanged += new System.EventHandler(this.listBox_AggregatedStatus_FunctionalState_SelectedIndexChanged);
      // 
      // groupBox_AggregatedStatus_StatusBits
      // 
      this.groupBox_AggregatedStatus_StatusBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox_AggregatedStatus_StatusBits.Controls.Add(this.listView_AggregatedStatus_StatusBits);
      this.groupBox_AggregatedStatus_StatusBits.Location = new System.Drawing.Point(4, 4);
      this.groupBox_AggregatedStatus_StatusBits.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_AggregatedStatus_StatusBits.Name = "groupBox_AggregatedStatus_StatusBits";
      this.groupBox_AggregatedStatus_StatusBits.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_AggregatedStatus_StatusBits.Size = new System.Drawing.Size(925, 348);
      this.groupBox_AggregatedStatus_StatusBits.TabIndex = 10;
      this.groupBox_AggregatedStatus_StatusBits.TabStop = false;
      this.groupBox_AggregatedStatus_StatusBits.Text = "StatusBits";
      // 
      // listView_AggregatedStatus_StatusBits
      // 
      this.listView_AggregatedStatus_StatusBits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_StatusBits_BitNumber,
            this.columnHeader_StatusBits_NTSColor,
            this.columnHeader_StatusBits_Description});
      this.listView_AggregatedStatus_StatusBits.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView_AggregatedStatus_StatusBits.FullRowSelect = true;
      this.listView_AggregatedStatus_StatusBits.HideSelection = false;
      this.listView_AggregatedStatus_StatusBits.Location = new System.Drawing.Point(4, 19);
      this.listView_AggregatedStatus_StatusBits.Margin = new System.Windows.Forms.Padding(4);
      this.listView_AggregatedStatus_StatusBits.MultiSelect = false;
      this.listView_AggregatedStatus_StatusBits.Name = "listView_AggregatedStatus_StatusBits";
      this.listView_AggregatedStatus_StatusBits.Size = new System.Drawing.Size(917, 325);
      this.listView_AggregatedStatus_StatusBits.SmallImageList = this.imageList_ListView;
      this.listView_AggregatedStatus_StatusBits.TabIndex = 7;
      this.listView_AggregatedStatus_StatusBits.UseCompatibleStateImageBehavior = false;
      this.listView_AggregatedStatus_StatusBits.View = System.Windows.Forms.View.Details;
      this.listView_AggregatedStatus_StatusBits.DoubleClick += new System.EventHandler(this.listView_AggregatedStatus_StatusBits_DoubleClick);
      // 
      // columnHeader_StatusBits_BitNumber
      // 
      this.columnHeader_StatusBits_BitNumber.Text = "Status bit";
      this.columnHeader_StatusBits_BitNumber.Width = 63;
      // 
      // columnHeader_StatusBits_NTSColor
      // 
      this.columnHeader_StatusBits_NTSColor.Text = "NTS Color";
      this.columnHeader_StatusBits_NTSColor.Width = 70;
      // 
      // columnHeader_StatusBits_Description
      // 
      this.columnHeader_StatusBits_Description.Text = "Description";
      this.columnHeader_StatusBits_Description.Width = 530;
      // 
      // groupBox_AggregatedStatus_FunctionalPosition
      // 
      this.groupBox_AggregatedStatus_FunctionalPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox_AggregatedStatus_FunctionalPosition.Controls.Add(this.listBox_AggregatedStatus_FunctionalPosition);
      this.groupBox_AggregatedStatus_FunctionalPosition.Location = new System.Drawing.Point(8, 356);
      this.groupBox_AggregatedStatus_FunctionalPosition.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_AggregatedStatus_FunctionalPosition.Name = "groupBox_AggregatedStatus_FunctionalPosition";
      this.groupBox_AggregatedStatus_FunctionalPosition.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_AggregatedStatus_FunctionalPosition.Size = new System.Drawing.Size(388, 247);
      this.groupBox_AggregatedStatus_FunctionalPosition.TabIndex = 9;
      this.groupBox_AggregatedStatus_FunctionalPosition.TabStop = false;
      this.groupBox_AggregatedStatus_FunctionalPosition.Text = "&FunctionalPosition";
      // 
      // listBox_AggregatedStatus_FunctionalPosition
      // 
      this.listBox_AggregatedStatus_FunctionalPosition.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBox_AggregatedStatus_FunctionalPosition.FormattingEnabled = true;
      this.listBox_AggregatedStatus_FunctionalPosition.ItemHeight = 16;
      this.listBox_AggregatedStatus_FunctionalPosition.Location = new System.Drawing.Point(4, 19);
      this.listBox_AggregatedStatus_FunctionalPosition.Margin = new System.Windows.Forms.Padding(4);
      this.listBox_AggregatedStatus_FunctionalPosition.Name = "listBox_AggregatedStatus_FunctionalPosition";
      this.listBox_AggregatedStatus_FunctionalPosition.Size = new System.Drawing.Size(380, 224);
      this.listBox_AggregatedStatus_FunctionalPosition.TabIndex = 0;
      this.listBox_AggregatedStatus_FunctionalPosition.SelectedIndexChanged += new System.EventHandler(this.listBox_AggregatedStatus_FunctionalPosition_SelectedIndexChanged);
      // 
      // button_AggregatedStatus_Send
      // 
      this.button_AggregatedStatus_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_AggregatedStatus_Send.Location = new System.Drawing.Point(41, 610);
      this.button_AggregatedStatus_Send.Margin = new System.Windows.Forms.Padding(4);
      this.button_AggregatedStatus_Send.Name = "button_AggregatedStatus_Send";
      this.button_AggregatedStatus_Send.Size = new System.Drawing.Size(301, 28);
      this.button_AggregatedStatus_Send.TabIndex = 8;
      this.button_AggregatedStatus_Send.Text = "Send Aggregated Status update";
      this.button_AggregatedStatus_Send.UseVisualStyleBackColor = true;
      this.button_AggregatedStatus_Send.Click += new System.EventHandler(this.button_AggregatedStatus_Send_Click);
      // 
      // checkBox_AggregatedStatus_SendAutomaticallyWhenChanged
      // 
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.AutoSize = true;
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Location = new System.Drawing.Point(392, 618);
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Name = "checkBox_AggregatedStatus_SendAutomaticallyWhenChanged";
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Size = new System.Drawing.Size(343, 20);
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.TabIndex = 7;
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Text = "Automatically send update when anything is changed";
      this.checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.UseVisualStyleBackColor = true;
      // 
      // tabPage_Status
      // 
      this.tabPage_Status.Controls.Add(this.groupBox_Status);
      this.tabPage_Status.Location = new System.Drawing.Point(4, 25);
      this.tabPage_Status.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage_Status.Name = "tabPage_Status";
      this.tabPage_Status.Padding = new System.Windows.Forms.Padding(4);
      this.tabPage_Status.Size = new System.Drawing.Size(946, 653);
      this.tabPage_Status.TabIndex = 4;
      this.tabPage_Status.Text = "Status";
      this.tabPage_Status.UseVisualStyleBackColor = true;
      // 
      // groupBox_Status
      // 
      this.groupBox_Status.Controls.Add(this.listView_Status);
      this.groupBox_Status.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_Status.Location = new System.Drawing.Point(4, 4);
      this.groupBox_Status.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_Status.Name = "groupBox_Status";
      this.groupBox_Status.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_Status.Size = new System.Drawing.Size(938, 645);
      this.groupBox_Status.TabIndex = 1;
      this.groupBox_Status.TabStop = false;
      this.groupBox_Status.Text = "Status";
      // 
      // listView_Status
      // 
      this.listView_Status.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Status_StatusCodeId,
            this.columnHeader_Status_Description,
            this.columnHeader_Status_Name,
            this.columnHeader_Status_Type,
            this.columnHeader_Status_Value,
            this.columnHeader_Status_Comment});
      this.listView_Status.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView_Status.FullRowSelect = true;
      this.listView_Status.GridLines = true;
      this.listView_Status.HideSelection = false;
      this.listView_Status.Location = new System.Drawing.Point(4, 19);
      this.listView_Status.Margin = new System.Windows.Forms.Padding(4);
      this.listView_Status.MultiSelect = false;
      this.listView_Status.Name = "listView_Status";
      this.listView_Status.Size = new System.Drawing.Size(930, 622);
      this.listView_Status.TabIndex = 8;
      this.listView_Status.UseCompatibleStateImageBehavior = false;
      this.listView_Status.View = System.Windows.Forms.View.Details;
      this.listView_Status.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
      this.listView_Status.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_Status_MouseClick);
      this.listView_Status.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Status_MouseDoubleClick);
      // 
      // columnHeader_Status_StatusCodeId
      // 
      this.columnHeader_Status_StatusCodeId.Tag = "StatusCodeId";
      this.columnHeader_Status_StatusCodeId.Text = "StatusCodeId";
      this.columnHeader_Status_StatusCodeId.Width = 85;
      // 
      // columnHeader_Status_Description
      // 
      this.columnHeader_Status_Description.Tag = "Description";
      this.columnHeader_Status_Description.Text = "Description";
      this.columnHeader_Status_Description.Width = 181;
      // 
      // columnHeader_Status_Name
      // 
      this.columnHeader_Status_Name.Text = "Name";
      this.columnHeader_Status_Name.Width = 100;
      // 
      // columnHeader_Status_Type
      // 
      this.columnHeader_Status_Type.Text = "Type";
      this.columnHeader_Status_Type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_Status_Type.Width = 100;
      // 
      // columnHeader_Status_Value
      // 
      this.columnHeader_Status_Value.Text = "Value";
      this.columnHeader_Status_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader_Status_Value.Width = 100;
      // 
      // columnHeader_Status_Comment
      // 
      this.columnHeader_Status_Comment.Text = "Comment";
      this.columnHeader_Status_Comment.Width = 200;
      // 
      // tabPage_Commands
      // 
      this.tabPage_Commands.Controls.Add(this.groupBox_Commands);
      this.tabPage_Commands.Location = new System.Drawing.Point(4, 25);
      this.tabPage_Commands.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage_Commands.Name = "tabPage_Commands";
      this.tabPage_Commands.Padding = new System.Windows.Forms.Padding(4);
      this.tabPage_Commands.Size = new System.Drawing.Size(946, 653);
      this.tabPage_Commands.TabIndex = 5;
      this.tabPage_Commands.Text = "Commands";
      this.tabPage_Commands.UseVisualStyleBackColor = true;
      // 
      // groupBox_Commands
      // 
      this.groupBox_Commands.Controls.Add(this.listView_Commands);
      this.groupBox_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_Commands.Location = new System.Drawing.Point(4, 4);
      this.groupBox_Commands.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_Commands.Name = "groupBox_Commands";
      this.groupBox_Commands.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_Commands.Size = new System.Drawing.Size(938, 645);
      this.groupBox_Commands.TabIndex = 0;
      this.groupBox_Commands.TabStop = false;
      this.groupBox_Commands.Text = "Commands (received updates)";
      // 
      // listView_Commands
      // 
      this.listView_Commands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Commands_CommandCodeId,
            this.columnHeader_Commands_Description,
            this.columnHeader_Commands_Name,
            this.columnHeader_Commands_Command,
            this.columnHeader_Commands_Type,
            this.columnHeader_Commands_Value,
            this.columnHeader_Commands_Age,
            this.columnHeader_Commands_Comment});
      this.listView_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView_Commands.FullRowSelect = true;
      this.listView_Commands.GridLines = true;
      this.listView_Commands.HideSelection = false;
      this.listView_Commands.Location = new System.Drawing.Point(4, 19);
      this.listView_Commands.Margin = new System.Windows.Forms.Padding(4);
      this.listView_Commands.MultiSelect = false;
      this.listView_Commands.Name = "listView_Commands";
      this.listView_Commands.Size = new System.Drawing.Size(930, 622);
      this.listView_Commands.TabIndex = 8;
      this.listView_Commands.UseCompatibleStateImageBehavior = false;
      this.listView_Commands.View = System.Windows.Forms.View.Details;
      this.listView_Commands.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
      // 
      // columnHeader_Commands_CommandCodeId
      // 
      this.columnHeader_Commands_CommandCodeId.Text = "CommandCodeId";
      this.columnHeader_Commands_CommandCodeId.Width = 63;
      // 
      // columnHeader_Commands_Description
      // 
      this.columnHeader_Commands_Description.Text = "Description";
      this.columnHeader_Commands_Description.Width = 181;
      // 
      // columnHeader_Commands_Name
      // 
      this.columnHeader_Commands_Name.Text = "Name";
      this.columnHeader_Commands_Name.Width = 68;
      // 
      // columnHeader_Commands_Command
      // 
      this.columnHeader_Commands_Command.Text = "Command";
      this.columnHeader_Commands_Command.Width = 81;
      // 
      // columnHeader_Commands_Type
      // 
      this.columnHeader_Commands_Type.Text = "Type";
      this.columnHeader_Commands_Type.Width = 89;
      // 
      // columnHeader_Commands_Value
      // 
      this.columnHeader_Commands_Value.Text = "Value";
      this.columnHeader_Commands_Value.Width = 103;
      // 
      // columnHeader_Commands_Age
      // 
      this.columnHeader_Commands_Age.Text = "Age";
      // 
      // columnHeader_Commands_Comment
      // 
      this.columnHeader_Commands_Comment.Text = "Comment";
      this.columnHeader_Commands_Comment.Width = 96;
      // 
      // tabPage_TestSend
      // 
      this.tabPage_TestSend.Controls.Add(this.groupBox_TestSend);
      this.tabPage_TestSend.Location = new System.Drawing.Point(4, 25);
      this.tabPage_TestSend.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage_TestSend.Name = "tabPage_TestSend";
      this.tabPage_TestSend.Size = new System.Drawing.Size(946, 653);
      this.tabPage_TestSend.TabIndex = 6;
      this.tabPage_TestSend.Text = "Test send";
      this.tabPage_TestSend.UseVisualStyleBackColor = true;
      // 
      // groupBox_TestSend
      // 
      this.groupBox_TestSend.Controls.Add(this.button_TestPackage_2_Browse);
      this.groupBox_TestSend.Controls.Add(this.button_TestPackage_1_Browse);
      this.groupBox_TestSend.Controls.Add(this.button_SendTestPackage_2);
      this.groupBox_TestSend.Controls.Add(this.textBox_TestPackage_2);
      this.groupBox_TestSend.Controls.Add(this.button_SendTestPackage_1);
      this.groupBox_TestSend.Controls.Add(this.textBox_TestPackage_1);
      this.groupBox_TestSend.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_TestSend.Location = new System.Drawing.Point(0, 0);
      this.groupBox_TestSend.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_TestSend.Name = "groupBox_TestSend";
      this.groupBox_TestSend.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_TestSend.Size = new System.Drawing.Size(946, 653);
      this.groupBox_TestSend.TabIndex = 0;
      this.groupBox_TestSend.TabStop = false;
      this.groupBox_TestSend.Text = "JSon package";
      // 
      // button_TestPackage_2_Browse
      // 
      this.button_TestPackage_2_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_TestPackage_2_Browse.Location = new System.Drawing.Point(613, 619);
      this.button_TestPackage_2_Browse.Margin = new System.Windows.Forms.Padding(4);
      this.button_TestPackage_2_Browse.Name = "button_TestPackage_2_Browse";
      this.button_TestPackage_2_Browse.Size = new System.Drawing.Size(173, 28);
      this.button_TestPackage_2_Browse.TabIndex = 6;
      this.button_TestPackage_2_Browse.Text = "Browse...";
      this.button_TestPackage_2_Browse.UseVisualStyleBackColor = true;
      this.button_TestPackage_2_Browse.Click += new System.EventHandler(this.button_TestPackage_2_Browse_Click);
      // 
      // button_TestPackage_1_Browse
      // 
      this.button_TestPackage_1_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_TestPackage_1_Browse.Location = new System.Drawing.Point(217, 619);
      this.button_TestPackage_1_Browse.Margin = new System.Windows.Forms.Padding(4);
      this.button_TestPackage_1_Browse.Name = "button_TestPackage_1_Browse";
      this.button_TestPackage_1_Browse.Size = new System.Drawing.Size(173, 28);
      this.button_TestPackage_1_Browse.TabIndex = 5;
      this.button_TestPackage_1_Browse.Text = "Browse...";
      this.button_TestPackage_1_Browse.UseVisualStyleBackColor = true;
      this.button_TestPackage_1_Browse.Click += new System.EventHandler(this.button_TestPackage_1_Browse_Click);
      // 
      // button_SendTestPackage_2
      // 
      this.button_SendTestPackage_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_SendTestPackage_2.Location = new System.Drawing.Point(432, 619);
      this.button_SendTestPackage_2.Margin = new System.Windows.Forms.Padding(4);
      this.button_SendTestPackage_2.Name = "button_SendTestPackage_2";
      this.button_SendTestPackage_2.Size = new System.Drawing.Size(173, 28);
      this.button_SendTestPackage_2.TabIndex = 4;
      this.button_SendTestPackage_2.Text = "Send above package";
      this.button_SendTestPackage_2.UseVisualStyleBackColor = true;
      this.button_SendTestPackage_2.Click += new System.EventHandler(this.button_SendTestPackage_2_Click);
      // 
      // textBox_TestPackage_2
      // 
      this.textBox_TestPackage_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.textBox_TestPackage_2.Location = new System.Drawing.Point(413, 23);
      this.textBox_TestPackage_2.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_TestPackage_2.Multiline = true;
      this.textBox_TestPackage_2.Name = "textBox_TestPackage_2";
      this.textBox_TestPackage_2.Size = new System.Drawing.Size(387, 586);
      this.textBox_TestPackage_2.TabIndex = 3;
      // 
      // button_SendTestPackage_1
      // 
      this.button_SendTestPackage_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_SendTestPackage_1.Location = new System.Drawing.Point(36, 619);
      this.button_SendTestPackage_1.Margin = new System.Windows.Forms.Padding(4);
      this.button_SendTestPackage_1.Name = "button_SendTestPackage_1";
      this.button_SendTestPackage_1.Size = new System.Drawing.Size(173, 28);
      this.button_SendTestPackage_1.TabIndex = 2;
      this.button_SendTestPackage_1.Text = "Send above package";
      this.button_SendTestPackage_1.UseVisualStyleBackColor = true;
      this.button_SendTestPackage_1.Click += new System.EventHandler(this.button_SendTestPackage_1_Click);
      // 
      // textBox_TestPackage_1
      // 
      this.textBox_TestPackage_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.textBox_TestPackage_1.Location = new System.Drawing.Point(17, 23);
      this.textBox_TestPackage_1.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_TestPackage_1.Multiline = true;
      this.textBox_TestPackage_1.Name = "textBox_TestPackage_1";
      this.textBox_TestPackage_1.Size = new System.Drawing.Size(387, 586);
      this.textBox_TestPackage_1.TabIndex = 0;
      // 
      // tabPage_BufferedMessages
      // 
      this.tabPage_BufferedMessages.Controls.Add(this.groupBox_BufferedMessages);
      this.tabPage_BufferedMessages.Location = new System.Drawing.Point(4, 25);
      this.tabPage_BufferedMessages.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage_BufferedMessages.Name = "tabPage_BufferedMessages";
      this.tabPage_BufferedMessages.Padding = new System.Windows.Forms.Padding(4);
      this.tabPage_BufferedMessages.Size = new System.Drawing.Size(946, 653);
      this.tabPage_BufferedMessages.TabIndex = 8;
      this.tabPage_BufferedMessages.Text = "Buffered messages";
      this.tabPage_BufferedMessages.UseVisualStyleBackColor = true;
      // 
      // groupBox_BufferedMessages
      // 
      this.groupBox_BufferedMessages.Controls.Add(this.textBox_BufferedMessages);
      this.groupBox_BufferedMessages.Controls.Add(this.label_CreateRandomMessages_Total);
      this.groupBox_BufferedMessages.Controls.Add(this.groupBox_BufferedMessages_CreateRandom);
      this.groupBox_BufferedMessages.Controls.Add(this.groupBox_BufferedMessages_Clear);
      this.groupBox_BufferedMessages.Controls.Add(this.checkBox_ShowMax10BufferedMessagesInSysLog);
      this.groupBox_BufferedMessages.Controls.Add(this.ListView_BufferedMessages);
      this.groupBox_BufferedMessages.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox_BufferedMessages.Location = new System.Drawing.Point(4, 4);
      this.groupBox_BufferedMessages.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_BufferedMessages.Name = "groupBox_BufferedMessages";
      this.groupBox_BufferedMessages.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_BufferedMessages.Size = new System.Drawing.Size(938, 645);
      this.groupBox_BufferedMessages.TabIndex = 0;
      this.groupBox_BufferedMessages.TabStop = false;
      this.groupBox_BufferedMessages.Text = "Buffered messages";
      // 
      // textBox_BufferedMessages
      // 
      this.textBox_BufferedMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.textBox_BufferedMessages.Location = new System.Drawing.Point(605, 614);
      this.textBox_BufferedMessages.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_BufferedMessages.Name = "textBox_BufferedMessages";
      this.textBox_BufferedMessages.Size = new System.Drawing.Size(111, 22);
      this.textBox_BufferedMessages.TabIndex = 13;
      this.textBox_BufferedMessages.Text = "0";
      this.textBox_BufferedMessages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label_CreateRandomMessages_Total
      // 
      this.label_CreateRandomMessages_Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label_CreateRandomMessages_Total.AutoSize = true;
      this.label_CreateRandomMessages_Total.Location = new System.Drawing.Point(491, 618);
      this.label_CreateRandomMessages_Total.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_CreateRandomMessages_Total.Name = "label_CreateRandomMessages_Total";
      this.label_CreateRandomMessages_Total.Size = new System.Drawing.Size(95, 16);
      this.label_CreateRandomMessages_Total.TabIndex = 12;
      this.label_CreateRandomMessages_Total.Text = "Buffered count:";
      // 
      // groupBox_BufferedMessages_CreateRandom
      // 
      this.groupBox_BufferedMessages_CreateRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.groupBox_BufferedMessages_CreateRandom.Controls.Add(this.textBox_CreateRandomMessages_Count);
      this.groupBox_BufferedMessages_CreateRandom.Controls.Add(this.label_CreateRandomMessages_Number);
      this.groupBox_BufferedMessages_CreateRandom.Controls.Add(this.label_CreateRandomMessages_SelectType);
      this.groupBox_BufferedMessages_CreateRandom.Controls.Add(this.button_BufferedMessages_CreateRandom);
      this.groupBox_BufferedMessages_CreateRandom.Controls.Add(this.comboBox_BufferedMessages_CreateRandom_Type);
      this.groupBox_BufferedMessages_CreateRandom.Location = new System.Drawing.Point(9, 496);
      this.groupBox_BufferedMessages_CreateRandom.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_BufferedMessages_CreateRandom.Name = "groupBox_BufferedMessages_CreateRandom";
      this.groupBox_BufferedMessages_CreateRandom.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_BufferedMessages_CreateRandom.Size = new System.Drawing.Size(716, 113);
      this.groupBox_BufferedMessages_CreateRandom.TabIndex = 11;
      this.groupBox_BufferedMessages_CreateRandom.TabStop = false;
      this.groupBox_BufferedMessages_CreateRandom.Text = "Create random messages";
      // 
      // textBox_CreateRandomMessages_Count
      // 
      this.textBox_CreateRandomMessages_Count.Location = new System.Drawing.Point(189, 63);
      this.textBox_CreateRandomMessages_Count.Margin = new System.Windows.Forms.Padding(4);
      this.textBox_CreateRandomMessages_Count.Name = "textBox_CreateRandomMessages_Count";
      this.textBox_CreateRandomMessages_Count.Size = new System.Drawing.Size(219, 22);
      this.textBox_CreateRandomMessages_Count.TabIndex = 11;
      this.textBox_CreateRandomMessages_Count.Text = "1";
      this.textBox_CreateRandomMessages_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label_CreateRandomMessages_Number
      // 
      this.label_CreateRandomMessages_Number.AutoSize = true;
      this.label_CreateRandomMessages_Number.Location = new System.Drawing.Point(71, 66);
      this.label_CreateRandomMessages_Number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_CreateRandomMessages_Number.Name = "label_CreateRandomMessages_Number";
      this.label_CreateRandomMessages_Number.Size = new System.Drawing.Size(99, 16);
      this.label_CreateRandomMessages_Number.TabIndex = 10;
      this.label_CreateRandomMessages_Number.Text = "Count to create:";
      // 
      // label_CreateRandomMessages_SelectType
      // 
      this.label_CreateRandomMessages_SelectType.AutoSize = true;
      this.label_CreateRandomMessages_SelectType.Location = new System.Drawing.Point(8, 32);
      this.label_CreateRandomMessages_SelectType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label_CreateRandomMessages_SelectType.Name = "label_CreateRandomMessages_SelectType";
      this.label_CreateRandomMessages_SelectType.Size = new System.Drawing.Size(163, 16);
      this.label_CreateRandomMessages_SelectType.TabIndex = 9;
      this.label_CreateRandomMessages_SelectType.Text = "Select message to create:";
      // 
      // button_BufferedMessages_CreateRandom
      // 
      this.button_BufferedMessages_CreateRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_BufferedMessages_CreateRandom.Location = new System.Drawing.Point(471, 60);
      this.button_BufferedMessages_CreateRandom.Margin = new System.Windows.Forms.Padding(4);
      this.button_BufferedMessages_CreateRandom.Name = "button_BufferedMessages_CreateRandom";
      this.button_BufferedMessages_CreateRandom.Size = new System.Drawing.Size(173, 28);
      this.button_BufferedMessages_CreateRandom.TabIndex = 8;
      this.button_BufferedMessages_CreateRandom.Text = "Create";
      this.button_BufferedMessages_CreateRandom.UseVisualStyleBackColor = true;
      this.button_BufferedMessages_CreateRandom.Click += new System.EventHandler(this.button_BufferedMessages_CreateRandom_Click);
      // 
      // comboBox_BufferedMessages_CreateRandom_Type
      // 
      this.comboBox_BufferedMessages_CreateRandom_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox_BufferedMessages_CreateRandom_Type.FormattingEnabled = true;
      this.comboBox_BufferedMessages_CreateRandom_Type.Items.AddRange(new object[] {
            "Alarms",
            "Aggregated status",
            "Status",
            "Status (only valid values)"});
      this.comboBox_BufferedMessages_CreateRandom_Type.Location = new System.Drawing.Point(189, 26);
      this.comboBox_BufferedMessages_CreateRandom_Type.Margin = new System.Windows.Forms.Padding(4);
      this.comboBox_BufferedMessages_CreateRandom_Type.Name = "comboBox_BufferedMessages_CreateRandom_Type";
      this.comboBox_BufferedMessages_CreateRandom_Type.Size = new System.Drawing.Size(219, 24);
      this.comboBox_BufferedMessages_CreateRandom_Type.TabIndex = 0;
      // 
      // groupBox_BufferedMessages_Clear
      // 
      this.groupBox_BufferedMessages_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.groupBox_BufferedMessages_Clear.Controls.Add(this.button_ClearAlarmMessages);
      this.groupBox_BufferedMessages_Clear.Controls.Add(this.button_ClearStatusMessages);
      this.groupBox_BufferedMessages_Clear.Controls.Add(this.button_ClearAggStatusMessages);
      this.groupBox_BufferedMessages_Clear.Location = new System.Drawing.Point(732, 497);
      this.groupBox_BufferedMessages_Clear.Margin = new System.Windows.Forms.Padding(4);
      this.groupBox_BufferedMessages_Clear.Name = "groupBox_BufferedMessages_Clear";
      this.groupBox_BufferedMessages_Clear.Padding = new System.Windows.Forms.Padding(4);
      this.groupBox_BufferedMessages_Clear.Size = new System.Drawing.Size(195, 142);
      this.groupBox_BufferedMessages_Clear.TabIndex = 10;
      this.groupBox_BufferedMessages_Clear.TabStop = false;
      this.groupBox_BufferedMessages_Clear.Text = "Delete messages";
      // 
      // button_ClearAlarmMessages
      // 
      this.button_ClearAlarmMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_ClearAlarmMessages.Location = new System.Drawing.Point(8, 23);
      this.button_ClearAlarmMessages.Margin = new System.Windows.Forms.Padding(4);
      this.button_ClearAlarmMessages.Name = "button_ClearAlarmMessages";
      this.button_ClearAlarmMessages.Size = new System.Drawing.Size(173, 28);
      this.button_ClearAlarmMessages.TabIndex = 7;
      this.button_ClearAlarmMessages.Text = "Alarms";
      this.button_ClearAlarmMessages.UseVisualStyleBackColor = true;
      this.button_ClearAlarmMessages.Click += new System.EventHandler(this.button_ClearAlarmMessages_Click);
      // 
      // button_ClearStatusMessages
      // 
      this.button_ClearStatusMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_ClearStatusMessages.Location = new System.Drawing.Point(8, 95);
      this.button_ClearStatusMessages.Margin = new System.Windows.Forms.Padding(4);
      this.button_ClearStatusMessages.Name = "button_ClearStatusMessages";
      this.button_ClearStatusMessages.Size = new System.Drawing.Size(173, 28);
      this.button_ClearStatusMessages.TabIndex = 9;
      this.button_ClearStatusMessages.Text = "Status";
      this.button_ClearStatusMessages.UseVisualStyleBackColor = true;
      this.button_ClearStatusMessages.Click += new System.EventHandler(this.button_ClearStatusMessages_Click);
      // 
      // button_ClearAggStatusMessages
      // 
      this.button_ClearAggStatusMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button_ClearAggStatusMessages.Location = new System.Drawing.Point(8, 59);
      this.button_ClearAggStatusMessages.Margin = new System.Windows.Forms.Padding(4);
      this.button_ClearAggStatusMessages.Name = "button_ClearAggStatusMessages";
      this.button_ClearAggStatusMessages.Size = new System.Drawing.Size(173, 28);
      this.button_ClearAggStatusMessages.TabIndex = 8;
      this.button_ClearAggStatusMessages.Text = "Aggr. status";
      this.button_ClearAggStatusMessages.UseVisualStyleBackColor = true;
      this.button_ClearAggStatusMessages.Click += new System.EventHandler(this.button_ClearAggStatusMessages_Click);
      // 
      // checkBox_ShowMax10BufferedMessagesInSysLog
      // 
      this.checkBox_ShowMax10BufferedMessagesInSysLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.checkBox_ShowMax10BufferedMessagesInSysLog.AutoSize = true;
      this.checkBox_ShowMax10BufferedMessagesInSysLog.Location = new System.Drawing.Point(9, 618);
      this.checkBox_ShowMax10BufferedMessagesInSysLog.Margin = new System.Windows.Forms.Padding(4);
      this.checkBox_ShowMax10BufferedMessagesInSysLog.Name = "checkBox_ShowMax10BufferedMessagesInSysLog";
      this.checkBox_ShowMax10BufferedMessagesInSysLog.Size = new System.Drawing.Size(401, 20);
      this.checkBox_ShowMax10BufferedMessagesInSysLog.TabIndex = 1;
      this.checkBox_ShowMax10BufferedMessagesInSysLog.Text = "Don\'t show these packets in system log if they are more than 10";
      this.checkBox_ShowMax10BufferedMessagesInSysLog.UseVisualStyleBackColor = true;
      // 
      // ListView_BufferedMessages
      // 
      this.ListView_BufferedMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ListView_BufferedMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_BufferedMessages_MessageType,
            this.columnHeader_BufferedMessages_MessageId,
            this.columnHeader_BufferedMessages_SendString});
      this.ListView_BufferedMessages.FullRowSelect = true;
      this.ListView_BufferedMessages.GridLines = true;
      this.ListView_BufferedMessages.HideSelection = false;
      this.ListView_BufferedMessages.Location = new System.Drawing.Point(8, 23);
      this.ListView_BufferedMessages.Margin = new System.Windows.Forms.Padding(4);
      this.ListView_BufferedMessages.Name = "ListView_BufferedMessages";
      this.ListView_BufferedMessages.Size = new System.Drawing.Size(920, 464);
      this.ListView_BufferedMessages.TabIndex = 0;
      this.ListView_BufferedMessages.UseCompatibleStateImageBehavior = false;
      this.ListView_BufferedMessages.View = System.Windows.Forms.View.Details;
      this.ListView_BufferedMessages.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
      // 
      // columnHeader_BufferedMessages_MessageType
      // 
      this.columnHeader_BufferedMessages_MessageType.Text = "Type";
      this.columnHeader_BufferedMessages_MessageType.Width = 80;
      // 
      // columnHeader_BufferedMessages_MessageId
      // 
      this.columnHeader_BufferedMessages_MessageId.Text = "Message id";
      this.columnHeader_BufferedMessages_MessageId.Width = 150;
      // 
      // columnHeader_BufferedMessages_SendString
      // 
      this.columnHeader_BufferedMessages_SendString.Text = "JSon Packet data";
      this.columnHeader_BufferedMessages_SendString.Width = 300;
      // 
      // openFileDialog_TestPackage
      // 
      this.openFileDialog_TestPackage.Filter = "Debug (text) files|*.txt|All files|*.*";
      this.openFileDialog_TestPackage.RestoreDirectory = true;
      // 
      // openFileDialog_ProcessImage
      // 
      this.openFileDialog_ProcessImage.Filter = "Process data files|*.dat|All files|*.*";
      this.openFileDialog_ProcessImage.RestoreDirectory = true;
      // 
      // saveFileDialog_ProcessImage
      // 
      this.saveFileDialog_ProcessImage.DefaultExt = "Process data files|*.dat|All files|*.*";
      this.saveFileDialog_ProcessImage.Filter = "Process data files|*.dat|All files|*.*";
      this.saveFileDialog_ProcessImage.RestoreDirectory = true;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // RSMPGS_Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1427, 713);
      this.Controls.Add(this.splitContainer_Main);
      this.Controls.Add(this.menuStrip_Main);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "RSMPGS_Main";
      this.Text = "RSMPGS1 - RoadSide Protocol Simulator - version";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this.Load += new System.EventHandler(this.RSMPGS_Main_Load);
      this.Shown += new System.EventHandler(this.RSMPGS_Main_Shown);
      this.menuStrip_Main.ResumeLayout(false);
      this.menuStrip_Main.PerformLayout();
      this.splitContainer_Main.Panel1.ResumeLayout(false);
      this.splitContainer_Main.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).EndInit();
      this.splitContainer_Main.ResumeLayout(false);
      this.splitContainer_ObjectsAndSyslog.Panel1.ResumeLayout(false);
      this.splitContainer_ObjectsAndSyslog.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer_ObjectsAndSyslog)).EndInit();
      this.splitContainer_ObjectsAndSyslog.ResumeLayout(false);
      this.groupBox_SitesAndObjects.ResumeLayout(false);
      this.groupBox_SitesAndObjects.PerformLayout();
      this.groupBox_SystemLog.ResumeLayout(false);
      this.groupBox_SystemLog.PerformLayout();
      this.tabControl_Object.ResumeLayout(false);
      this.tabPage_Generic.ResumeLayout(false);
      this.groupBox_Encryption.ResumeLayout(false);
      this.groupBox_Encryption.PerformLayout();
      this.groupBox_EncryptionProtocols.ResumeLayout(false);
      this.groupBox_EncryptionProtocols.PerformLayout();
      this.groupBox_ProcessImage.ResumeLayout(false);
      this.groupBox_ProcessImage.PerformLayout();
      this.groupBox_ProcessImage_Load.ResumeLayout(false);
      this.groupBox_ProcessImage_Load.PerformLayout();
      this.groupBox_SXL_Version.ResumeLayout(false);
      this.groupBox_SXL_Version.PerformLayout();
      this.tabPage_RSMP.ResumeLayout(false);
      this.splitContainer_RSMP.Panel1.ResumeLayout(false);
      this.splitContainer_RSMP.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer_RSMP)).EndInit();
      this.splitContainer_RSMP.ResumeLayout(false);
      this.groupBox_ProtocolSettings.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Behaviour)).EndInit();
      this.groupBox_Statistics.ResumeLayout(false);
      this.tabPage_Alarms.ResumeLayout(false);
      this.splitContainer_Alarms.Panel1.ResumeLayout(false);
      this.splitContainer_Alarms.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Alarms)).EndInit();
      this.splitContainer_Alarms.ResumeLayout(false);
      this.contextMenuStrip_Alarm.ResumeLayout(false);
      this.groupBox_AlarmEvents.ResumeLayout(false);
      this.tabPage_AggregatedStatus.ResumeLayout(false);
      this.tabPage_AggregatedStatus.PerformLayout();
      this.groupBox_AggregatedStatus_FunctionalState.ResumeLayout(false);
      this.groupBox_AggregatedStatus_StatusBits.ResumeLayout(false);
      this.groupBox_AggregatedStatus_FunctionalPosition.ResumeLayout(false);
      this.tabPage_Status.ResumeLayout(false);
      this.groupBox_Status.ResumeLayout(false);
      this.tabPage_Commands.ResumeLayout(false);
      this.groupBox_Commands.ResumeLayout(false);
      this.tabPage_TestSend.ResumeLayout(false);
      this.groupBox_TestSend.ResumeLayout(false);
      this.groupBox_TestSend.PerformLayout();
      this.tabPage_BufferedMessages.ResumeLayout(false);
      this.groupBox_BufferedMessages.ResumeLayout(false);
      this.groupBox_BufferedMessages.PerformLayout();
      this.groupBox_BufferedMessages_CreateRandom.ResumeLayout(false);
      this.groupBox_BufferedMessages_CreateRandom.PerformLayout();
      this.groupBox_BufferedMessages_Clear.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Timer timer_System;
    private System.Windows.Forms.MenuStrip menuStrip_Main;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_CreateNew;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_TileV;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_CloseAll;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_0;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Close;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_RandomUpdates;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Connection;
    public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ConnectAutomatically;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_3;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ConnectNow;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Disconnect;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_4;
    private System.Windows.Forms.SplitContainer splitContainer_Main;
    public System.Windows.Forms.ToolStrip toolStrip_Main;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SendOptions;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SendSomeRandomCrap;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Alarm;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Active;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Acknowledge;
    private System.Windows.Forms.SplitContainer splitContainer_ObjectsAndSyslog;
    private System.Windows.Forms.GroupBox groupBox_SitesAndObjects;
    private System.Windows.Forms.CheckBox checkBox_ShowTooltip;
    private System.Windows.Forms.GroupBox groupBox_SystemLog;
    private System.Windows.Forms.TabControl tabControl_Object;
    private System.Windows.Forms.TabPage tabPage_Generic;
    private System.Windows.Forms.TabPage tabPage_Alarms;
    private System.Windows.Forms.SplitContainer splitContainer_Alarms;
    private ListViewDoubleBuffered listView_Alarms;
    private System.Windows.Forms.ColumnHeader columnHeader_Alarms_AlarmCodeId;
    private System.Windows.Forms.ColumnHeader columnHeader_Alarms_Description;
    private System.Windows.Forms.ColumnHeader columnHeader_Alarms_ExternalAlarmCodeId;
    private System.Windows.Forms.ColumnHeader columnHeader_Alarms_ExternalNtSAlarmCodeId;
    private System.Windows.Forms.ColumnHeader columnHeader_Alarms_Priority;
    private System.Windows.Forms.ColumnHeader columnHeader_Alarms_Category;
    private System.Windows.Forms.TabPage tabPage_AggregatedStatus;
    private System.Windows.Forms.TabPage tabPage_Status;
    private System.Windows.Forms.TabPage tabPage_Commands;
    private System.Windows.Forms.GroupBox groupBox_AlarmEvents;
    private ListViewDoubleBuffered listView_AlarmEvents;
    private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_TimeStamp;
    private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_MsgId;
    private System.Windows.Forms.ColumnHeader columnHeader_Alarms_Status;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_1;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_Reset;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Suspend;
    private System.Windows.Forms.ColumnHeader columnHeader_Alarms_AlarmEvents;
    public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SplitPackets;
    private System.Windows.Forms.TabPage tabPage_TestSend;
    private System.Windows.Forms.GroupBox groupBox_TestSend;
    private System.Windows.Forms.Button button_SendTestPackage_2;
    private System.Windows.Forms.TextBox textBox_TestPackage_2;
    private System.Windows.Forms.Button button_SendTestPackage_1;
    private System.Windows.Forms.TextBox textBox_TestPackage_1;
    private System.Windows.Forms.OpenFileDialog openFileDialog_TestPackage;
    private System.Windows.Forms.Button button_TestPackage_1_Browse;
    private System.Windows.Forms.Button button_TestPackage_2_Browse;
    private System.Windows.Forms.ImageList imageList_ListView;
    private System.Windows.Forms.GroupBox groupBox_AggregatedStatus_FunctionalPosition;
    private System.Windows.Forms.Button button_AggregatedStatus_Send;
    private System.Windows.Forms.GroupBox groupBox_AggregatedStatus_FunctionalState;
    private System.Windows.Forms.GroupBox groupBox_AggregatedStatus_StatusBits;
    private System.Windows.Forms.ColumnHeader columnHeader_StatusBits_BitNumber;
    private System.Windows.Forms.ColumnHeader columnHeader_StatusBits_NTSColor;
    private System.Windows.Forms.ColumnHeader columnHeader_StatusBits_Description;
    private System.Windows.Forms.GroupBox groupBox_Commands;
    private System.Windows.Forms.ColumnHeader columnHeader_Commands_CommandCodeId;
    private System.Windows.Forms.ColumnHeader columnHeader_Commands_Description;
    private System.Windows.Forms.ColumnHeader columnHeader_Commands_Name;
    private System.Windows.Forms.ColumnHeader columnHeader_Commands_Command;
    private System.Windows.Forms.ColumnHeader columnHeader_Commands_Type;
    private System.Windows.Forms.ColumnHeader columnHeader_Commands_Value;
    private System.Windows.Forms.ColumnHeader columnHeader_Commands_Age;
    private System.Windows.Forms.ColumnHeader columnHeader_Commands_Comment;
    public System.Windows.Forms.TreeView treeView_SitesAndObjects;
    public ListViewDoubleBuffered listView_Commands;
    private System.Windows.Forms.GroupBox groupBox_SXL_Version;
    public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_DisableNagleAlgorithm;
    public System.Windows.Forms.TextBox textBox_SignalExchangeListVersion;
    private System.Windows.Forms.Label label_SXL_VersionManually;
    private System.Windows.Forms.GroupBox groupBox_Status;
    public ListViewDoubleBuffered listView_Status;
    private System.Windows.Forms.ColumnHeader columnHeader_Status_StatusCodeId;
    private System.Windows.Forms.ColumnHeader columnHeader_Status_Description;
    public System.Windows.Forms.TextBox textBox_SignalExchangeListVersionFromFile;
    private System.Windows.Forms.Label label_SXL_VersionFromFile;
    public System.Windows.Forms.CheckBox checkBox_AlwaysUseSXLFromFile;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StoreBase64Updates;
    private System.Windows.Forms.GroupBox groupBox_ProcessImage;
    public System.Windows.Forms.CheckBox checkBox_AutomaticallySaveProcessData;
    private System.Windows.Forms.Label label_SXL_FilePath;
    public System.Windows.Forms.TextBox textBox_SignalExchangeListPath;
    public System.Windows.Forms.CheckBox checkbox_AutomaticallyLoadProcessData;
    private System.Windows.Forms.Label label_ProcessImage_Info_1;
    private System.Windows.Forms.GroupBox groupBox_ProcessImage_Load;
    public System.Windows.Forms.CheckBox checkBox_ProcessImageLoad_Status;
    public System.Windows.Forms.CheckBox checkBox_ProcessImageLoad_AggregatedStatus;
    public System.Windows.Forms.CheckBox checkBox_ProcessImageLoad_AlarmStatus;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_2;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_SaveToFile;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_LoadFromFile;
    private System.Windows.Forms.Label label_ProcessImage_Info_2;
    private System.Windows.Forms.OpenFileDialog openFileDialog_ProcessImage;
    private System.Windows.Forms.SaveFileDialog saveFileDialog_ProcessImage;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_Clear;
    public System.Windows.Forms.CheckBox checkBox_AggregatedStatus_SendAutomaticallyWhenChanged;
    public System.Windows.Forms.ListBox listBox_AggregatedStatus_FunctionalPosition;
    public System.Windows.Forms.ListBox listBox_AggregatedStatus_FunctionalState;
    public ListViewDoubleBuffered listView_AggregatedStatus_StatusBits;
    public ListViewDoubleBuffered listView_SysLog;
    private System.Windows.Forms.ColumnHeader columnHeader_SysLog_Severity;
    private System.Windows.Forms.ColumnHeader columnHeader_SysLog_TimeStamp;
    private System.Windows.Forms.ColumnHeader columnHeader_SysLog_Description;
    private System.Windows.Forms.ImageList imageList_Severity;
    private System.Windows.Forms.Button button_ClearSystemLog;
    public System.Windows.Forms.CheckBox checkBox_ViewOnlyFailedPackets;
    private System.Windows.Forms.TabPage tabPage_RSMP;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SendWatchdog;
    private System.Windows.Forms.SplitContainer splitContainer_RSMP;
    private System.Windows.Forms.GroupBox groupBox_ProtocolSettings;
    private System.Windows.Forms.Button button_ResetRSMPSettingToDefault;
    public System.Windows.Forms.DataGridView dataGridView_Behaviour;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column_Description;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Common;
    private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_1;
    private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_2;
    private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_3;
    private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_4;
    private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_5;
    private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_2_0;
    private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_2_1;
    private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_2_2;
    private System.Windows.Forms.GroupBox groupBox_Statistics;
    public ListViewDoubleBuffered listView_Statistics;
    private System.Windows.Forms.ColumnHeader columnHeader_Statistics_Desription;
    private System.Windows.Forms.ColumnHeader columnHeader_Statistics_Value;
    private System.Windows.Forms.ColumnHeader columnHeader_Statistics_Unit;
    private System.Windows.Forms.Button button_ClearStatistics;
    private System.Windows.Forms.ColumnHeader columnHeader_Status_Name;
    private System.Windows.Forms.ColumnHeader columnHeader_Status_Type;
    private System.Windows.Forms.ColumnHeader columnHeader_Status_Value;
    private System.Windows.Forms.ColumnHeader columnHeader_Status_Comment;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View_AlwaysShowGroupHeaders;
    private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_AlarmCodeId;
    private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_Direction;
    private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_Event;
    private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_RoadSideObject;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View_Clear_AlarmEvents;
    private System.Windows.Forms.ToolStripTextBox ToolStripMenuItem_ConnectionStatus;
    private System.Windows.Forms.GroupBox groupBox_Encryption;
    private System.Windows.Forms.Button button_EncryptionFile_Browse;
    private System.Windows.Forms.GroupBox groupBox_EncryptionProtocols;
    private System.Windows.Forms.Label label_Encryption_ServerName;
    private System.Windows.Forms.TextBox textBox_EncryptionFile;
    private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_TLS13;
    private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_TLS12;
    private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_TLS11;
    private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_TLS10;
    private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_Default;
    private System.Windows.Forms.TextBox textBox_Encryption_ServerName;
    private System.Windows.Forms.CheckBox button_Encryption_IgnoreCertErrors;
    private System.Windows.Forms.CheckBox checkBox_Encryption_AuthenticateAsClientUsingCertificate;
    private System.Windows.Forms.CheckBox button_Encryption_CheckRevocation;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_LoadObjects;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_LoadObjects_CSV;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_LoadObjects_YAML;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_5;
    public System.Windows.Forms.CheckBox checkBox_AutomaticallyLoadObjects;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_File_LoadObjects_Delimiter;
    private System.Windows.Forms.TabPage tabPage_BufferedMessages;
    private System.Windows.Forms.GroupBox groupBox_BufferedMessages;
    public ListViewDoubleBuffered ListView_BufferedMessages;
    private System.Windows.Forms.Button button_ClearAlarmMessages;
    public System.Windows.Forms.CheckBox checkBox_ShowMax10BufferedMessagesInSysLog;
    private System.Windows.Forms.Button button_ClearStatusMessages;
    private System.Windows.Forms.Button button_ClearAggStatusMessages;
    private System.Windows.Forms.ColumnHeader columnHeader_BufferedMessages_MessageType;
    private System.Windows.Forms.ColumnHeader columnHeader_BufferedMessages_MessageId;
    private System.Windows.Forms.ColumnHeader columnHeader_BufferedMessages_SendString;
    private System.Windows.Forms.GroupBox groupBox_BufferedMessages_Clear;
    private System.Windows.Forms.GroupBox groupBox_BufferedMessages_CreateRandom;
    private System.Windows.Forms.ComboBox comboBox_BufferedMessages_CreateRandom_Type;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.TextBox textBox_CreateRandomMessages_Count;
    private System.Windows.Forms.Label label_CreateRandomMessages_Number;
    private System.Windows.Forms.Label label_CreateRandomMessages_SelectType;
    private System.Windows.Forms.Button button_BufferedMessages_CreateRandom;
    public System.Windows.Forms.TextBox textBox_BufferedMessages;
    private System.Windows.Forms.Label label_CreateRandomMessages_Total;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_Show;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_Cascade;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_TileH;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
  }
}

