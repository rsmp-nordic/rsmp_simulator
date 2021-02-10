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
            this.ToolStripMenuItem_File_Debug_Tile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_File_Debug_CloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_StoreBase64Updates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_Delimiter_1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_File_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ProcessImage = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Delimiter_1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_ProcessImage_LoadAtStartUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ProcessImage_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ProcessImage_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.subscriptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Subscriptions_ResendAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Subscriptions_UnsubscribeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_EventFiles_SaveCont = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Connection = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ConnectAutomatically = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Delimiter_Connect = new System.Windows.Forms.ToolStripSeparator();
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
            this.ToolStripMenuItem_View_Clear_AggregatedStatusEvents = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_View_Clear_StatusEvents = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_View_Clear_CommandEvents = new System.Windows.Forms.ToolStripMenuItem();
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
            this.imageList_Severity = new System.Windows.Forms.ImageList(this.components);
            this.tabControl_Object = new System.Windows.Forms.TabControl();
            this.tabPage_Generic = new System.Windows.Forms.TabPage();
            this.groupBox_Encryption = new System.Windows.Forms.GroupBox();
            this.button_Encryption_CheckRevocation = new System.Windows.Forms.CheckBox();
            this.label_Encryption_CertificateFile = new System.Windows.Forms.Label();
            this.checkBox_Encryption_RequireClientCertificate = new System.Windows.Forms.CheckBox();
            this.button_Encryption_IgnoreCertErrors = new System.Windows.Forms.CheckBox();
            this.button_EncryptionFile_Browse = new System.Windows.Forms.Button();
            this.textBox_EncryptionFile = new System.Windows.Forms.TextBox();
            this.groupBox_EncryptionProtocols = new System.Windows.Forms.GroupBox();
            this.checkBox_Encryption_Protocol_TLS13 = new System.Windows.Forms.CheckBox();
            this.checkBox_Encryption_Protocol_TLS12 = new System.Windows.Forms.CheckBox();
            this.checkBox_Encryption_Protocol_TLS11 = new System.Windows.Forms.CheckBox();
            this.checkBox_Encryption_Protocol_TLS10 = new System.Windows.Forms.CheckBox();
            this.checkBox_Encryption_Protocol_Default = new System.Windows.Forms.CheckBox();
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
            this.dataGridView_Behaviour = new System.Windows.Forms.DataGridView();
            this.Column_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Common = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RSMP_3_1_1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RSMP_3_1_2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RSMP_3_1_3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RSMP_3_1_4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RSMP_3_1_5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_ResetRSMPSettingToDefault = new System.Windows.Forms.Button();
            this.groupBox_Statistics = new System.Windows.Forms.GroupBox();
            this.button_ClearStatistics = new System.Windows.Forms.Button();
            this.tabPage_Alarms = new System.Windows.Forms.TabPage();
            this.splitContainer_Alarms = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip_Alarm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Acknowledge = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Suspend = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Alarm_RequestCurrentState = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_AlarmEvents = new System.Windows.Forms.GroupBox();
            this.tabPage_AggregatedStatus = new System.Windows.Forms.TabPage();
            this.splitContainer_AggregatedStatus = new System.Windows.Forms.SplitContainer();
            this.groupBox_AggregatedStatus_StatusBits = new System.Windows.Forms.GroupBox();
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events = new System.Windows.Forms.SplitContainer();
            this.splitContainer_FunctionalStatPos = new System.Windows.Forms.SplitContainer();
            this.groupBox_AggregatedStatus_FunctionalPosition = new System.Windows.Forms.GroupBox();
            this.listBox_AggregatedStatus_FunctionalPosition = new System.Windows.Forms.ListBox();
            this.groupBox_AggregatedStatus_FunctionalState = new System.Windows.Forms.GroupBox();
            this.listBox_AggregatedStatus_FunctionalState = new System.Windows.Forms.ListBox();
            this.button_AggregatedStatus_Request = new System.Windows.Forms.Button();
            this.groupBox_AggregatedStatusEvents = new System.Windows.Forms.GroupBox();
            this.tabPage_Status = new System.Windows.Forms.TabPage();
            this.splitContainer_Status = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip_Status = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_StatusRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StatusSubscribe = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_StatusUnsubscribe = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_StatusEvents = new System.Windows.Forms.GroupBox();
            this.tabPage_Commands = new System.Windows.Forms.TabPage();
            this.contextMenuStrip_Commands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Commands = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_TestSend = new System.Windows.Forms.TabPage();
            this.splitContainer_TestSend = new System.Windows.Forms.SplitContainer();
            this.button_TestPackage_1_Browse = new System.Windows.Forms.Button();
            this.button_SendTestPackage_1 = new System.Windows.Forms.Button();
            this.groupBox_TestSend1 = new System.Windows.Forms.GroupBox();
            this.textBox_TestPackage_1 = new System.Windows.Forms.TextBox();
            this.button_SendTestPackage_2 = new System.Windows.Forms.Button();
            this.button_TestPackage_2_Browse = new System.Windows.Forms.Button();
            this.groupBox_TestSend2 = new System.Windows.Forms.GroupBox();
            this.textBox_TestPackage_2 = new System.Windows.Forms.TextBox();
            this.openFileDialog_TestPackage = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog_SXL = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog_Sequence = new System.Windows.Forms.OpenFileDialog();
            this.listView_SysLog = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_SysLog_Severity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_SysLog_TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_SysLog_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_Statistics = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_Statistics_Desription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Statistics_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Statistics_Unit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_Alarms = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_Alarms_Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Alarms_AlarmEvents = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Alarms_AlarmCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Alarms_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Alarms_ExternalAlarmCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Alarms_ExternalNtSAlarmCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Alarms_Priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Alarms_Category = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_AlarmEvents = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_AlarmEvent_TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AlarmEvent_RoadSideObject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AlarmEvent_MsgId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AlarmEvent_AlarmCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AlarmEvent_Direction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AlarmEvent_Event = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_AggregatedStatus_StatusBits = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_StatusBits_BitNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusBits_NTSColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusBits_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_AggregatedStatusEvents = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_AggStatEvent_TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AggStatEvent_MsgId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AggStatEvent_BitStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AggStatEvent_FuncPos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_AggStatEvent_FuncState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_Status = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_Status_StatusCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Status_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Status_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Status_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Status_Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Status_Quality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Status_UpdateRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Status_UpdateOnChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Status_Comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_StatusEvents = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_StatusEvents_TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusEvents_MessageId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusEvents_Event = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusEvents_StatusCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusEvents_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusEvents_Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusEvents_Quality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_StatusEvents_UpdateRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer_Commands = new System.Windows.Forms.SplitContainer();
            this.listView_Commands = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_Commands_CommandCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Commands_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Commands_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Commands_Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Commands_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Commands_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Commands_Age = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Commands_Comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox_CommandEvents = new System.Windows.Forms.GroupBox();
            this.listView_CommandEvents = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_CommandEvent_TimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_CommandEvent_MsgId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_CommandEvent_Event = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_CommandEvent_CommandCodeId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_CommandEvent_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_CommandEvent_Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_CommandEvent_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_CommandEvent_Age = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_AggregatedStatus)).BeginInit();
            this.splitContainer_AggregatedStatus.Panel1.SuspendLayout();
            this.splitContainer_AggregatedStatus.Panel2.SuspendLayout();
            this.splitContainer_AggregatedStatus.SuspendLayout();
            this.groupBox_AggregatedStatus_StatusBits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_AggregatedStatus_FunctionalStatPos_Events)).BeginInit();
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel1.SuspendLayout();
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel2.SuspendLayout();
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_FunctionalStatPos)).BeginInit();
            this.splitContainer_FunctionalStatPos.Panel1.SuspendLayout();
            this.splitContainer_FunctionalStatPos.Panel2.SuspendLayout();
            this.splitContainer_FunctionalStatPos.SuspendLayout();
            this.groupBox_AggregatedStatus_FunctionalPosition.SuspendLayout();
            this.groupBox_AggregatedStatus_FunctionalState.SuspendLayout();
            this.groupBox_AggregatedStatusEvents.SuspendLayout();
            this.tabPage_Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Status)).BeginInit();
            this.splitContainer_Status.Panel1.SuspendLayout();
            this.splitContainer_Status.Panel2.SuspendLayout();
            this.splitContainer_Status.SuspendLayout();
            this.contextMenuStrip_Status.SuspendLayout();
            this.groupBox_StatusEvents.SuspendLayout();
            this.tabPage_Commands.SuspendLayout();
            this.contextMenuStrip_Commands.SuspendLayout();
            this.tabPage_TestSend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_TestSend)).BeginInit();
            this.splitContainer_TestSend.Panel1.SuspendLayout();
            this.splitContainer_TestSend.Panel2.SuspendLayout();
            this.splitContainer_TestSend.SuspendLayout();
            this.groupBox_TestSend1.SuspendLayout();
            this.groupBox_TestSend2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Commands)).BeginInit();
            this.splitContainer_Commands.Panel1.SuspendLayout();
            this.splitContainer_Commands.Panel2.SuspendLayout();
            this.splitContainer_Commands.SuspendLayout();
            this.groupBox_CommandEvents.SuspendLayout();
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
            this.subscriptionsToolStripMenuItem,
            this.eventsToolStripMenuItem,
            this.ToolStripMenuItem_Connection,
            this.ToolStripMenuItem_View,
            this.ToolStripMenuItem_ConnectionStatus});
            this.menuStrip_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Size = new System.Drawing.Size(997, 27);
            this.menuStrip_Main.TabIndex = 2;
            this.menuStrip_Main.Text = "menuStrip_Main";
            // 
            // ToolStripMenuItem_File
            // 
            this.ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File_LoadObjects,
            this.ToolStripMenuItem_Delimiter_0,
            this.ToolStripMenuItem_File_Debug,
            this.toolStripSeparator_Delimiter_1,
            this.ToolStripMenuItem_File_Close});
            this.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File";
            this.ToolStripMenuItem_File.Size = new System.Drawing.Size(37, 23);
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
            this.ToolStripMenuItem_File_LoadObjects.Size = new System.Drawing.Size(170, 22);
            this.ToolStripMenuItem_File_LoadObjects.Text = "Load objects from";
            // 
            // ToolStripMenuItem_File_LoadObjects_CSV
            // 
            this.ToolStripMenuItem_File_LoadObjects_CSV.Name = "ToolStripMenuItem_File_LoadObjects_CSV";
            this.ToolStripMenuItem_File_LoadObjects_CSV.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_File_LoadObjects_CSV.Text = "CSV-files...";
            this.ToolStripMenuItem_File_LoadObjects_CSV.Click += new System.EventHandler(this.ToolStripMenuItem_File_LoadObjects_CSV_Click);
            // 
            // ToolStripMenuItem_File_LoadObjects_YAML
            // 
            this.ToolStripMenuItem_File_LoadObjects_YAML.Name = "ToolStripMenuItem_File_LoadObjects_YAML";
            this.ToolStripMenuItem_File_LoadObjects_YAML.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_File_LoadObjects_YAML.Text = "YAML-file...";
            this.ToolStripMenuItem_File_LoadObjects_YAML.Click += new System.EventHandler(this.ToolStripMenuItem_File_LoadObjects_YAML_Click);
            // 
            // ToolStripMenuItem_File_LoadObjects_Delimiter
            // 
            this.ToolStripMenuItem_File_LoadObjects_Delimiter.Name = "ToolStripMenuItem_File_LoadObjects_Delimiter";
            this.ToolStripMenuItem_File_LoadObjects_Delimiter.Size = new System.Drawing.Size(149, 6);
            // 
            // ToolStripMenuItem_Delimiter_0
            // 
            this.ToolStripMenuItem_Delimiter_0.Name = "ToolStripMenuItem_Delimiter_0";
            this.ToolStripMenuItem_Delimiter_0.Size = new System.Drawing.Size(167, 6);
            // 
            // ToolStripMenuItem_File_Debug
            // 
            this.ToolStripMenuItem_File_Debug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File_Debug_CreateNew,
            this.ToolStripMenuItem_File_Debug_Tile,
            this.ToolStripMenuItem_File_Debug_CloseAll,
            this.toolStripMenuItem1,
            this.ToolStripMenuItem_StoreBase64Updates});
            this.ToolStripMenuItem_File_Debug.Name = "ToolStripMenuItem_File_Debug";
            this.ToolStripMenuItem_File_Debug.Size = new System.Drawing.Size(170, 22);
            this.ToolStripMenuItem_File_Debug.Text = "&Debug";
            // 
            // ToolStripMenuItem_File_Debug_CreateNew
            // 
            this.ToolStripMenuItem_File_Debug_CreateNew.Name = "ToolStripMenuItem_File_Debug_CreateNew";
            this.ToolStripMenuItem_File_Debug_CreateNew.Size = new System.Drawing.Size(215, 22);
            this.ToolStripMenuItem_File_Debug_CreateNew.Text = "&Create new debug window";
            this.ToolStripMenuItem_File_Debug_CreateNew.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_CreateNew_Click);
            // 
            // ToolStripMenuItem_File_Debug_Tile
            // 
            this.ToolStripMenuItem_File_Debug_Tile.Name = "ToolStripMenuItem_File_Debug_Tile";
            this.ToolStripMenuItem_File_Debug_Tile.Size = new System.Drawing.Size(215, 22);
            this.ToolStripMenuItem_File_Debug_Tile.Text = "&Tile all debug windows";
            this.ToolStripMenuItem_File_Debug_Tile.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_Tile_Click);
            // 
            // ToolStripMenuItem_File_Debug_CloseAll
            // 
            this.ToolStripMenuItem_File_Debug_CloseAll.Name = "ToolStripMenuItem_File_Debug_CloseAll";
            this.ToolStripMenuItem_File_Debug_CloseAll.Size = new System.Drawing.Size(215, 22);
            this.ToolStripMenuItem_File_Debug_CloseAll.Text = "Close all debug &windows";
            this.ToolStripMenuItem_File_Debug_CloseAll.Click += new System.EventHandler(this.ToolStripMenuItem_File_Debug_CloseAll_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(212, 6);
            // 
            // ToolStripMenuItem_StoreBase64Updates
            // 
            this.ToolStripMenuItem_StoreBase64Updates.CheckOnClick = true;
            this.ToolStripMenuItem_StoreBase64Updates.Name = "ToolStripMenuItem_StoreBase64Updates";
            this.ToolStripMenuItem_StoreBase64Updates.Size = new System.Drawing.Size(215, 22);
            this.ToolStripMenuItem_StoreBase64Updates.Text = "&Store base64 updates";
            // 
            // toolStripSeparator_Delimiter_1
            // 
            this.toolStripSeparator_Delimiter_1.Name = "toolStripSeparator_Delimiter_1";
            this.toolStripSeparator_Delimiter_1.Size = new System.Drawing.Size(167, 6);
            // 
            // ToolStripMenuItem_File_Close
            // 
            this.ToolStripMenuItem_File_Close.Name = "ToolStripMenuItem_File_Close";
            this.ToolStripMenuItem_File_Close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ToolStripMenuItem_File_Close.Size = new System.Drawing.Size(170, 22);
            this.ToolStripMenuItem_File_Close.Text = "&Exit";
            this.ToolStripMenuItem_File_Close.Click += new System.EventHandler(this.ToolStripMenuItem_File_Close_Click);
            // 
            // ToolStripMenuItem_ProcessImage
            // 
            this.ToolStripMenuItem_ProcessImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Delimiter_1,
            this.ToolStripMenuItem_ProcessImage_LoadAtStartUp,
            this.ToolStripMenuItem_ProcessImage_Load,
            this.ToolStripMenuItem_ProcessImage_Reset});
            this.ToolStripMenuItem_ProcessImage.Name = "ToolStripMenuItem_ProcessImage";
            this.ToolStripMenuItem_ProcessImage.Size = new System.Drawing.Size(95, 23);
            this.ToolStripMenuItem_ProcessImage.Text = "&Process Image";
            // 
            // ToolStripMenuItem_Delimiter_1
            // 
            this.ToolStripMenuItem_Delimiter_1.Name = "ToolStripMenuItem_Delimiter_1";
            this.ToolStripMenuItem_Delimiter_1.Size = new System.Drawing.Size(230, 6);
            // 
            // ToolStripMenuItem_ProcessImage_LoadAtStartUp
            // 
            this.ToolStripMenuItem_ProcessImage_LoadAtStartUp.Name = "ToolStripMenuItem_ProcessImage_LoadAtStartUp";
            this.ToolStripMenuItem_ProcessImage_LoadAtStartUp.Size = new System.Drawing.Size(233, 22);
            this.ToolStripMenuItem_ProcessImage_LoadAtStartUp.Text = "Load Process Image at Startup";
            this.ToolStripMenuItem_ProcessImage_LoadAtStartUp.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessImage_RestoreAtStartUp_Click);
            // 
            // ToolStripMenuItem_ProcessImage_Load
            // 
            this.ToolStripMenuItem_ProcessImage_Load.Name = "ToolStripMenuItem_ProcessImage_Load";
            this.ToolStripMenuItem_ProcessImage_Load.Size = new System.Drawing.Size(233, 22);
            this.ToolStripMenuItem_ProcessImage_Load.Text = "Load Process Image";
            this.ToolStripMenuItem_ProcessImage_Load.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessImage_Load_Click);
            // 
            // ToolStripMenuItem_ProcessImage_Reset
            // 
            this.ToolStripMenuItem_ProcessImage_Reset.Name = "ToolStripMenuItem_ProcessImage_Reset";
            this.ToolStripMenuItem_ProcessImage_Reset.Size = new System.Drawing.Size(233, 22);
            this.ToolStripMenuItem_ProcessImage_Reset.Text = "Reset Process &Image";
            this.ToolStripMenuItem_ProcessImage_Reset.Click += new System.EventHandler(this.ToolStripMenuItem_ProcessImage_Reset_Click);
            // 
            // subscriptionsToolStripMenuItem
            // 
            this.subscriptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Subscriptions_ResendAll,
            this.ToolStripMenuItem_Subscriptions_UnsubscribeAll});
            this.subscriptionsToolStripMenuItem.Name = "subscriptionsToolStripMenuItem";
            this.subscriptionsToolStripMenuItem.Size = new System.Drawing.Size(90, 23);
            this.subscriptionsToolStripMenuItem.Text = "Subscriptions";
            // 
            // ToolStripMenuItem_Subscriptions_ResendAll
            // 
            this.ToolStripMenuItem_Subscriptions_ResendAll.Name = "ToolStripMenuItem_Subscriptions_ResendAll";
            this.ToolStripMenuItem_Subscriptions_ResendAll.Size = new System.Drawing.Size(229, 22);
            this.ToolStripMenuItem_Subscriptions_ResendAll.Text = "Resend all Subscriptions";
            this.ToolStripMenuItem_Subscriptions_ResendAll.Click += new System.EventHandler(this.ToolStripMenuItem_Subscriptions_ResendAll_Click);
            // 
            // ToolStripMenuItem_Subscriptions_UnsubscribeAll
            // 
            this.ToolStripMenuItem_Subscriptions_UnsubscribeAll.Name = "ToolStripMenuItem_Subscriptions_UnsubscribeAll";
            this.ToolStripMenuItem_Subscriptions_UnsubscribeAll.Size = new System.Drawing.Size(229, 22);
            this.ToolStripMenuItem_Subscriptions_UnsubscribeAll.Text = "UnSubscribe all Subscriptions";
            this.ToolStripMenuItem_Subscriptions_UnsubscribeAll.Click += new System.EventHandler(this.ToolStripMenuItem_Subscriptions_UnsubscribeAll_Click);
            // 
            // eventsToolStripMenuItem
            // 
            this.eventsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_EventFiles_SaveCont});
            this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            this.eventsToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.eventsToolStripMenuItem.Text = "Events";
            // 
            // ToolStripMenuItem_EventFiles_SaveCont
            // 
            this.ToolStripMenuItem_EventFiles_SaveCont.Name = "ToolStripMenuItem_EventFiles_SaveCont";
            this.ToolStripMenuItem_EventFiles_SaveCont.Size = new System.Drawing.Size(241, 22);
            this.ToolStripMenuItem_EventFiles_SaveCont.Text = "&Save continous to file (record)...";
            this.ToolStripMenuItem_EventFiles_SaveCont.Click += new System.EventHandler(this.ToolStripMenuItem_EventFiles_SaveCont_Click);
            // 
            // ToolStripMenuItem_Connection
            // 
            this.ToolStripMenuItem_Connection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ConnectAutomatically,
            this.ToolStripMenuItem_Delimiter_Connect,
            this.ToolStripMenuItem_ConnectNow,
            this.ToolStripMenuItem_Disconnect,
            this.ToolStripMenuItem_Delimiter_4,
            this.ToolStripMenuItem_SendOptions});
            this.ToolStripMenuItem_Connection.Name = "ToolStripMenuItem_Connection";
            this.ToolStripMenuItem_Connection.Size = new System.Drawing.Size(81, 23);
            this.ToolStripMenuItem_Connection.Text = "Connection";
            this.ToolStripMenuItem_Connection.DropDownOpening += new System.EventHandler(this.ToolStripMenuItem_Connection_DropDownOpening);
            // 
            // ToolStripMenuItem_ConnectAutomatically
            // 
            this.ToolStripMenuItem_ConnectAutomatically.CheckOnClick = true;
            this.ToolStripMenuItem_ConnectAutomatically.Name = "ToolStripMenuItem_ConnectAutomatically";
            this.ToolStripMenuItem_ConnectAutomatically.Size = new System.Drawing.Size(194, 22);
            this.ToolStripMenuItem_ConnectAutomatically.Text = "Connect &automatically";
            this.ToolStripMenuItem_ConnectAutomatically.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_ConnectAutomatically_CheckedChanged);
            // 
            // ToolStripMenuItem_Delimiter_Connect
            // 
            this.ToolStripMenuItem_Delimiter_Connect.Name = "ToolStripMenuItem_Delimiter_Connect";
            this.ToolStripMenuItem_Delimiter_Connect.Size = new System.Drawing.Size(191, 6);
            // 
            // ToolStripMenuItem_ConnectNow
            // 
            this.ToolStripMenuItem_ConnectNow.Name = "ToolStripMenuItem_ConnectNow";
            this.ToolStripMenuItem_ConnectNow.Size = new System.Drawing.Size(194, 22);
            this.ToolStripMenuItem_ConnectNow.Text = "Connect &now";
            this.ToolStripMenuItem_ConnectNow.Click += new System.EventHandler(this.ToolStripMenuItem_ConnectNow_Click);
            // 
            // ToolStripMenuItem_Disconnect
            // 
            this.ToolStripMenuItem_Disconnect.Name = "ToolStripMenuItem_Disconnect";
            this.ToolStripMenuItem_Disconnect.Size = new System.Drawing.Size(194, 22);
            this.ToolStripMenuItem_Disconnect.Text = "&Disconnect";
            this.ToolStripMenuItem_Disconnect.Click += new System.EventHandler(this.ToolStripMenuItem_Disconnect_Click);
            // 
            // ToolStripMenuItem_Delimiter_4
            // 
            this.ToolStripMenuItem_Delimiter_4.Name = "ToolStripMenuItem_Delimiter_4";
            this.ToolStripMenuItem_Delimiter_4.Size = new System.Drawing.Size(191, 6);
            // 
            // ToolStripMenuItem_SendOptions
            // 
            this.ToolStripMenuItem_SendOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_SendWatchdog,
            this.ToolStripMenuItem_SendSomeRandomCrap,
            this.ToolStripMenuItem_DisableNagleAlgorithm,
            this.ToolStripMenuItem_SplitPackets});
            this.ToolStripMenuItem_SendOptions.Name = "ToolStripMenuItem_SendOptions";
            this.ToolStripMenuItem_SendOptions.Size = new System.Drawing.Size(194, 22);
            this.ToolStripMenuItem_SendOptions.Text = "&Send options";
            // 
            // ToolStripMenuItem_SendWatchdog
            // 
            this.ToolStripMenuItem_SendWatchdog.Name = "ToolStripMenuItem_SendWatchdog";
            this.ToolStripMenuItem_SendWatchdog.Size = new System.Drawing.Size(296, 22);
            this.ToolStripMenuItem_SendWatchdog.Text = "Send Watchdog packet now";
            this.ToolStripMenuItem_SendWatchdog.Click += new System.EventHandler(this.ToolStripMenuItem_SendWatchdog_Click);
            // 
            // ToolStripMenuItem_SendSomeRandomCrap
            // 
            this.ToolStripMenuItem_SendSomeRandomCrap.Name = "ToolStripMenuItem_SendSomeRandomCrap";
            this.ToolStripMenuItem_SendSomeRandomCrap.Size = new System.Drawing.Size(296, 22);
            this.ToolStripMenuItem_SendSomeRandomCrap.Text = "Send some &random crap";
            this.ToolStripMenuItem_SendSomeRandomCrap.Click += new System.EventHandler(this.ToolStripMenuItem_SendSomeRandomCrap_Click);
            // 
            // ToolStripMenuItem_DisableNagleAlgorithm
            // 
            this.ToolStripMenuItem_DisableNagleAlgorithm.CheckOnClick = true;
            this.ToolStripMenuItem_DisableNagleAlgorithm.Name = "ToolStripMenuItem_DisableNagleAlgorithm";
            this.ToolStripMenuItem_DisableNagleAlgorithm.Size = new System.Drawing.Size(296, 22);
            this.ToolStripMenuItem_DisableNagleAlgorithm.Text = "Disable &Nagle algorithm (send coalescing)";
            this.ToolStripMenuItem_DisableNagleAlgorithm.Click += new System.EventHandler(this.ToolStripMenuItem_DisableNagleAlgorithm_Click);
            // 
            // ToolStripMenuItem_SplitPackets
            // 
            this.ToolStripMenuItem_SplitPackets.CheckOnClick = true;
            this.ToolStripMenuItem_SplitPackets.Name = "ToolStripMenuItem_SplitPackets";
            this.ToolStripMenuItem_SplitPackets.Size = new System.Drawing.Size(296, 22);
            this.ToolStripMenuItem_SplitPackets.Text = "&Split packets";
            // 
            // ToolStripMenuItem_View
            // 
            this.ToolStripMenuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_View_AlwaysShowGroupHeaders,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_View_Clear_AlarmEvents,
            this.ToolStripMenuItem_View_Clear_AggregatedStatusEvents,
            this.ToolStripMenuItem_View_Clear_StatusEvents,
            this.ToolStripMenuItem_View_Clear_CommandEvents});
            this.ToolStripMenuItem_View.Name = "ToolStripMenuItem_View";
            this.ToolStripMenuItem_View.Size = new System.Drawing.Size(44, 23);
            this.ToolStripMenuItem_View.Text = "View";
            // 
            // ToolStripMenuItem_View_AlwaysShowGroupHeaders
            // 
            this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.CheckOnClick = true;
            this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.Name = "ToolStripMenuItem_View_AlwaysShowGroupHeaders";
            this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.Size = new System.Drawing.Size(256, 22);
            this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.Text = "Always show group &headers";
            this.ToolStripMenuItem_View_AlwaysShowGroupHeaders.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_View_AlwaysShowGroupHeaders_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(253, 6);
            // 
            // ToolStripMenuItem_View_Clear_AlarmEvents
            // 
            this.ToolStripMenuItem_View_Clear_AlarmEvents.Name = "ToolStripMenuItem_View_Clear_AlarmEvents";
            this.ToolStripMenuItem_View_Clear_AlarmEvents.Size = new System.Drawing.Size(256, 22);
            this.ToolStripMenuItem_View_Clear_AlarmEvents.Text = "Clear &Alarm Events list";
            this.ToolStripMenuItem_View_Clear_AlarmEvents.Click += new System.EventHandler(this.ToolStripMenuItem_View_Clear_AlarmEvents_Click);
            // 
            // ToolStripMenuItem_View_Clear_AggregatedStatusEvents
            // 
            this.ToolStripMenuItem_View_Clear_AggregatedStatusEvents.Name = "ToolStripMenuItem_View_Clear_AggregatedStatusEvents";
            this.ToolStripMenuItem_View_Clear_AggregatedStatusEvents.Size = new System.Drawing.Size(256, 22);
            this.ToolStripMenuItem_View_Clear_AggregatedStatusEvents.Text = "Clear A&ggregated Status Events list";
            this.ToolStripMenuItem_View_Clear_AggregatedStatusEvents.Click += new System.EventHandler(this.ToolStripMenuItem_View_Clear_AggregatedStatusEvents_Click);
            // 
            // ToolStripMenuItem_View_Clear_StatusEvents
            // 
            this.ToolStripMenuItem_View_Clear_StatusEvents.Name = "ToolStripMenuItem_View_Clear_StatusEvents";
            this.ToolStripMenuItem_View_Clear_StatusEvents.Size = new System.Drawing.Size(256, 22);
            this.ToolStripMenuItem_View_Clear_StatusEvents.Text = "Clear &Status Events list";
            this.ToolStripMenuItem_View_Clear_StatusEvents.Click += new System.EventHandler(this.ToolStripMenuItem_View_Clear_StatusEvents_Click);
            // 
            // ToolStripMenuItem_View_Clear_CommandEvents
            // 
            this.ToolStripMenuItem_View_Clear_CommandEvents.Name = "ToolStripMenuItem_View_Clear_CommandEvents";
            this.ToolStripMenuItem_View_Clear_CommandEvents.Size = new System.Drawing.Size(256, 22);
            this.ToolStripMenuItem_View_Clear_CommandEvents.Text = "Clear &Command Events list";
            this.ToolStripMenuItem_View_Clear_CommandEvents.Click += new System.EventHandler(this.ToolStripMenuItem_View_Clear_CommandEvents_Click);
            // 
            // ToolStripMenuItem_ConnectionStatus
            // 
            this.ToolStripMenuItem_ConnectionStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolStripMenuItem_ConnectionStatus.AutoSize = false;
            this.ToolStripMenuItem_ConnectionStatus.Name = "ToolStripMenuItem_ConnectionStatus";
            this.ToolStripMenuItem_ConnectionStatus.ReadOnly = true;
            this.ToolStripMenuItem_ConnectionStatus.Size = new System.Drawing.Size(226, 23);
            this.ToolStripMenuItem_ConnectionStatus.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // splitContainer_Main
            // 
            this.splitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Main.Location = new System.Drawing.Point(0, 27);
            this.splitContainer_Main.Name = "splitContainer_Main";
            // 
            // splitContainer_Main.Panel1
            // 
            this.splitContainer_Main.Panel1.Controls.Add(this.splitContainer_ObjectsAndSyslog);
            // 
            // splitContainer_Main.Panel2
            // 
            this.splitContainer_Main.Panel2.Controls.Add(this.tabControl_Object);
            this.splitContainer_Main.Size = new System.Drawing.Size(997, 751);
            this.splitContainer_Main.SplitterDistance = 313;
            this.splitContainer_Main.TabIndex = 4;
            // 
            // splitContainer_ObjectsAndSyslog
            // 
            this.splitContainer_ObjectsAndSyslog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_ObjectsAndSyslog.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer_ObjectsAndSyslog.Size = new System.Drawing.Size(313, 751);
            this.splitContainer_ObjectsAndSyslog.SplitterDistance = 440;
            this.splitContainer_ObjectsAndSyslog.TabIndex = 0;
            // 
            // groupBox_SitesAndObjects
            // 
            this.groupBox_SitesAndObjects.Controls.Add(this.checkBox_ShowTooltip);
            this.groupBox_SitesAndObjects.Controls.Add(this.treeView_SitesAndObjects);
            this.groupBox_SitesAndObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_SitesAndObjects.Location = new System.Drawing.Point(0, 0);
            this.groupBox_SitesAndObjects.Name = "groupBox_SitesAndObjects";
            this.groupBox_SitesAndObjects.Size = new System.Drawing.Size(313, 440);
            this.groupBox_SitesAndObjects.TabIndex = 1;
            this.groupBox_SitesAndObjects.TabStop = false;
            this.groupBox_SitesAndObjects.Text = "&Sites and Objects";
            // 
            // checkBox_ShowTooltip
            // 
            this.checkBox_ShowTooltip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_ShowTooltip.AutoSize = true;
            this.checkBox_ShowTooltip.Location = new System.Drawing.Point(9, 418);
            this.checkBox_ShowTooltip.Name = "checkBox_ShowTooltip";
            this.checkBox_ShowTooltip.Size = new System.Drawing.Size(159, 17);
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
            this.treeView_SitesAndObjects.Location = new System.Drawing.Point(6, 19);
            this.treeView_SitesAndObjects.Name = "treeView_SitesAndObjects";
            this.treeView_SitesAndObjects.SelectedImageIndex = 0;
            this.treeView_SitesAndObjects.Size = new System.Drawing.Size(301, 393);
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
            this.groupBox_SystemLog.Name = "groupBox_SystemLog";
            this.groupBox_SystemLog.Size = new System.Drawing.Size(313, 307);
            this.groupBox_SystemLog.TabIndex = 1;
            this.groupBox_SystemLog.TabStop = false;
            this.groupBox_SystemLog.Text = "System &Log";
            // 
            // button_ClearSystemLog
            // 
            this.button_ClearSystemLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ClearSystemLog.Location = new System.Drawing.Point(245, 281);
            this.button_ClearSystemLog.Name = "button_ClearSystemLog";
            this.button_ClearSystemLog.Size = new System.Drawing.Size(62, 20);
            this.button_ClearSystemLog.TabIndex = 5;
            this.button_ClearSystemLog.Text = "Clear";
            this.button_ClearSystemLog.UseVisualStyleBackColor = true;
            this.button_ClearSystemLog.Click += new System.EventHandler(this.button_ClearSystemLog_Click);
            // 
            // checkBox_ViewOnlyFailedPackets
            // 
            this.checkBox_ViewOnlyFailedPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_ViewOnlyFailedPackets.AutoSize = true;
            this.checkBox_ViewOnlyFailedPackets.Location = new System.Drawing.Point(9, 286);
            this.checkBox_ViewOnlyFailedPackets.Name = "checkBox_ViewOnlyFailedPackets";
            this.checkBox_ViewOnlyFailedPackets.Size = new System.Drawing.Size(140, 17);
            this.checkBox_ViewOnlyFailedPackets.TabIndex = 4;
            this.checkBox_ViewOnlyFailedPackets.Text = "View only failed packets";
            this.checkBox_ViewOnlyFailedPackets.UseVisualStyleBackColor = true;
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
            this.tabControl_Object.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Object.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Object.Multiline = true;
            this.tabControl_Object.Name = "tabControl_Object";
            this.tabControl_Object.SelectedIndex = 0;
            this.tabControl_Object.Size = new System.Drawing.Size(680, 751);
            this.tabControl_Object.TabIndex = 2;
            // 
            // tabPage_Generic
            // 
            this.tabPage_Generic.Controls.Add(this.groupBox_Encryption);
            this.tabPage_Generic.Controls.Add(this.groupBox_SXL_Version);
            this.tabPage_Generic.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Generic.Name = "tabPage_Generic";
            this.tabPage_Generic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Generic.Size = new System.Drawing.Size(672, 725);
            this.tabPage_Generic.TabIndex = 0;
            this.tabPage_Generic.Text = "Generic";
            this.tabPage_Generic.UseVisualStyleBackColor = true;
            // 
            // groupBox_Encryption
            // 
            this.groupBox_Encryption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Encryption.Controls.Add(this.button_Encryption_CheckRevocation);
            this.groupBox_Encryption.Controls.Add(this.label_Encryption_CertificateFile);
            this.groupBox_Encryption.Controls.Add(this.checkBox_Encryption_RequireClientCertificate);
            this.groupBox_Encryption.Controls.Add(this.button_Encryption_IgnoreCertErrors);
            this.groupBox_Encryption.Controls.Add(this.button_EncryptionFile_Browse);
            this.groupBox_Encryption.Controls.Add(this.textBox_EncryptionFile);
            this.groupBox_Encryption.Controls.Add(this.groupBox_EncryptionProtocols);
            this.groupBox_Encryption.Location = new System.Drawing.Point(3, 169);
            this.groupBox_Encryption.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Encryption.Name = "groupBox_Encryption";
            this.groupBox_Encryption.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Encryption.Size = new System.Drawing.Size(669, 162);
            this.groupBox_Encryption.TabIndex = 4;
            this.groupBox_Encryption.TabStop = false;
            this.groupBox_Encryption.Text = "&Encryption settings";
            // 
            // button_Encryption_CheckRevocation
            // 
            this.button_Encryption_CheckRevocation.AutoSize = true;
            this.button_Encryption_CheckRevocation.Checked = true;
            this.button_Encryption_CheckRevocation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.button_Encryption_CheckRevocation.Location = new System.Drawing.Point(328, 93);
            this.button_Encryption_CheckRevocation.Name = "button_Encryption_CheckRevocation";
            this.button_Encryption_CheckRevocation.Size = new System.Drawing.Size(260, 17);
            this.button_Encryption_CheckRevocation.TabIndex = 17;
            this.button_Encryption_CheckRevocation.Text = "Check certificate against certificate revocation list";
            this.button_Encryption_CheckRevocation.UseVisualStyleBackColor = true;
            this.button_Encryption_CheckRevocation.CheckedChanged += new System.EventHandler(this.button_Encryption_CheckRevocation_CheckedChanged);
            // 
            // label_Encryption_CertificateFile
            // 
            this.label_Encryption_CertificateFile.AutoSize = true;
            this.label_Encryption_CertificateFile.Location = new System.Drawing.Point(18, 135);
            this.label_Encryption_CertificateFile.Name = "label_Encryption_CertificateFile";
            this.label_Encryption_CertificateFile.Size = new System.Drawing.Size(106, 13);
            this.label_Encryption_CertificateFile.TabIndex = 16;
            this.label_Encryption_CertificateFile.Text = "Server certificate file:";
            this.label_Encryption_CertificateFile.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkBox_Encryption_RequireClientCertificate
            // 
            this.checkBox_Encryption_RequireClientCertificate.AutoSize = true;
            this.checkBox_Encryption_RequireClientCertificate.Checked = true;
            this.checkBox_Encryption_RequireClientCertificate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Encryption_RequireClientCertificate.Location = new System.Drawing.Point(328, 46);
            this.checkBox_Encryption_RequireClientCertificate.Name = "checkBox_Encryption_RequireClientCertificate";
            this.checkBox_Encryption_RequireClientCertificate.Size = new System.Drawing.Size(213, 17);
            this.checkBox_Encryption_RequireClientCertificate.TabIndex = 15;
            this.checkBox_Encryption_RequireClientCertificate.Text = "Require client certificate authentication ";
            this.checkBox_Encryption_RequireClientCertificate.UseVisualStyleBackColor = true;
            this.checkBox_Encryption_RequireClientCertificate.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_RequireClientCertificate_CheckedChanged);
            // 
            // button_Encryption_IgnoreCertErrors
            // 
            this.button_Encryption_IgnoreCertErrors.AutoSize = true;
            this.button_Encryption_IgnoreCertErrors.Checked = true;
            this.button_Encryption_IgnoreCertErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.button_Encryption_IgnoreCertErrors.Location = new System.Drawing.Point(328, 70);
            this.button_Encryption_IgnoreCertErrors.Name = "button_Encryption_IgnoreCertErrors";
            this.button_Encryption_IgnoreCertErrors.Size = new System.Drawing.Size(134, 17);
            this.button_Encryption_IgnoreCertErrors.TabIndex = 12;
            this.button_Encryption_IgnoreCertErrors.Text = "Ignore certificate errors";
            this.button_Encryption_IgnoreCertErrors.UseVisualStyleBackColor = true;
            this.button_Encryption_IgnoreCertErrors.CheckedChanged += new System.EventHandler(this.button_Encryption_IgnoreCertErrors_CheckedChanged);
            // 
            // button_EncryptionFile_Browse
            // 
            this.button_EncryptionFile_Browse.Location = new System.Drawing.Point(589, 132);
            this.button_EncryptionFile_Browse.Name = "button_EncryptionFile_Browse";
            this.button_EncryptionFile_Browse.Size = new System.Drawing.Size(62, 20);
            this.button_EncryptionFile_Browse.TabIndex = 11;
            this.button_EncryptionFile_Browse.Text = "Browse";
            this.button_EncryptionFile_Browse.UseVisualStyleBackColor = true;
            this.button_EncryptionFile_Browse.Click += new System.EventHandler(this.button_EncryptionFile_Browse_Click);
            // 
            // textBox_EncryptionFile
            // 
            this.textBox_EncryptionFile.Enabled = false;
            this.textBox_EncryptionFile.Location = new System.Drawing.Point(130, 132);
            this.textBox_EncryptionFile.Name = "textBox_EncryptionFile";
            this.textBox_EncryptionFile.Size = new System.Drawing.Size(454, 20);
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
            this.groupBox_EncryptionProtocols.Location = new System.Drawing.Point(16, 28);
            this.groupBox_EncryptionProtocols.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_EncryptionProtocols.Name = "groupBox_EncryptionProtocols";
            this.groupBox_EncryptionProtocols.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_EncryptionProtocols.Size = new System.Drawing.Size(285, 93);
            this.groupBox_EncryptionProtocols.TabIndex = 6;
            this.groupBox_EncryptionProtocols.TabStop = false;
            this.groupBox_EncryptionProtocols.Text = "&Encryption protocols";
            // 
            // checkBox_Encryption_Protocol_TLS13
            // 
            this.checkBox_Encryption_Protocol_TLS13.AutoSize = true;
            this.checkBox_Encryption_Protocol_TLS13.Checked = true;
            this.checkBox_Encryption_Protocol_TLS13.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Encryption_Protocol_TLS13.Location = new System.Drawing.Point(94, 65);
            this.checkBox_Encryption_Protocol_TLS13.Name = "checkBox_Encryption_Protocol_TLS13";
            this.checkBox_Encryption_Protocol_TLS13.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Encryption_Protocol_TLS13.TabIndex = 12;
            this.checkBox_Encryption_Protocol_TLS13.Text = "TLS 1.3";
            this.checkBox_Encryption_Protocol_TLS13.UseVisualStyleBackColor = true;
            this.checkBox_Encryption_Protocol_TLS13.Visible = false;
            this.checkBox_Encryption_Protocol_TLS13.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_Protocol_CheckedChanged);
            // 
            // checkBox_Encryption_Protocol_TLS12
            // 
            this.checkBox_Encryption_Protocol_TLS12.AutoSize = true;
            this.checkBox_Encryption_Protocol_TLS12.Checked = true;
            this.checkBox_Encryption_Protocol_TLS12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Encryption_Protocol_TLS12.Location = new System.Drawing.Point(94, 41);
            this.checkBox_Encryption_Protocol_TLS12.Name = "checkBox_Encryption_Protocol_TLS12";
            this.checkBox_Encryption_Protocol_TLS12.Size = new System.Drawing.Size(64, 17);
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
            this.checkBox_Encryption_Protocol_TLS11.Location = new System.Drawing.Point(16, 65);
            this.checkBox_Encryption_Protocol_TLS11.Name = "checkBox_Encryption_Protocol_TLS11";
            this.checkBox_Encryption_Protocol_TLS11.Size = new System.Drawing.Size(64, 17);
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
            this.checkBox_Encryption_Protocol_TLS10.Location = new System.Drawing.Point(16, 41);
            this.checkBox_Encryption_Protocol_TLS10.Name = "checkBox_Encryption_Protocol_TLS10";
            this.checkBox_Encryption_Protocol_TLS10.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Encryption_Protocol_TLS10.TabIndex = 9;
            this.checkBox_Encryption_Protocol_TLS10.Text = "TLS 1.0";
            this.checkBox_Encryption_Protocol_TLS10.UseVisualStyleBackColor = true;
            // 
            // checkBox_Encryption_Protocol_Default
            // 
            this.checkBox_Encryption_Protocol_Default.AutoSize = true;
            this.checkBox_Encryption_Protocol_Default.Checked = true;
            this.checkBox_Encryption_Protocol_Default.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Encryption_Protocol_Default.Location = new System.Drawing.Point(16, 18);
            this.checkBox_Encryption_Protocol_Default.Name = "checkBox_Encryption_Protocol_Default";
            this.checkBox_Encryption_Protocol_Default.Size = new System.Drawing.Size(252, 17);
            this.checkBox_Encryption_Protocol_Default.TabIndex = 7;
            this.checkBox_Encryption_Protocol_Default.Text = "Default (let OS automatically select the protocol)";
            this.checkBox_Encryption_Protocol_Default.UseVisualStyleBackColor = true;
            this.checkBox_Encryption_Protocol_Default.CheckedChanged += new System.EventHandler(this.checkBox_Encryption_Protocol_CheckedChanged);
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
            this.groupBox_SXL_Version.Location = new System.Drawing.Point(3, 3);
            this.groupBox_SXL_Version.Name = "groupBox_SXL_Version";
            this.groupBox_SXL_Version.Size = new System.Drawing.Size(669, 161);
            this.groupBox_SXL_Version.TabIndex = 2;
            this.groupBox_SXL_Version.TabStop = false;
            this.groupBox_SXL_Version.Text = "Signal Exchange List (SXL/SUL) info";
            // 
            // checkBox_AutomaticallyLoadObjects
            // 
            this.checkBox_AutomaticallyLoadObjects.AutoSize = true;
            this.checkBox_AutomaticallyLoadObjects.Checked = true;
            this.checkBox_AutomaticallyLoadObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_AutomaticallyLoadObjects.Location = new System.Drawing.Point(124, 127);
            this.checkBox_AutomaticallyLoadObjects.Name = "checkBox_AutomaticallyLoadObjects";
            this.checkBox_AutomaticallyLoadObjects.Size = new System.Drawing.Size(214, 17);
            this.checkBox_AutomaticallyLoadObjects.TabIndex = 12;
            this.checkBox_AutomaticallyLoadObjects.Text = "Automatically load last objects at startup";
            this.checkBox_AutomaticallyLoadObjects.UseVisualStyleBackColor = true;
            // 
            // label_SXL_FilePath
            // 
            this.label_SXL_FilePath.AutoSize = true;
            this.label_SXL_FilePath.Location = new System.Drawing.Point(24, 104);
            this.label_SXL_FilePath.Name = "label_SXL_FilePath";
            this.label_SXL_FilePath.Size = new System.Drawing.Size(81, 13);
            this.label_SXL_FilePath.TabIndex = 11;
            this.label_SXL_FilePath.Text = "SXL (SUL) path";
            this.label_SXL_FilePath.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_SignalExchangeListPath
            // 
            this.textBox_SignalExchangeListPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SignalExchangeListPath.Location = new System.Drawing.Point(124, 101);
            this.textBox_SignalExchangeListPath.Name = "textBox_SignalExchangeListPath";
            this.textBox_SignalExchangeListPath.ReadOnly = true;
            this.textBox_SignalExchangeListPath.Size = new System.Drawing.Size(517, 20);
            this.textBox_SignalExchangeListPath.TabIndex = 10;
            // 
            // checkBox_AlwaysUseSXLFromFile
            // 
            this.checkBox_AlwaysUseSXLFromFile.AutoSize = true;
            this.checkBox_AlwaysUseSXLFromFile.Location = new System.Drawing.Point(48, 76);
            this.checkBox_AlwaysUseSXLFromFile.Name = "checkBox_AlwaysUseSXLFromFile";
            this.checkBox_AlwaysUseSXLFromFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_AlwaysUseSXLFromFile.Size = new System.Drawing.Size(270, 17);
            this.checkBox_AlwaysUseSXLFromFile.TabIndex = 7;
            this.checkBox_AlwaysUseSXLFromFile.Text = "(Always use the SXL (SUL) version from file (if found";
            this.checkBox_AlwaysUseSXLFromFile.UseVisualStyleBackColor = true;
            this.checkBox_AlwaysUseSXLFromFile.CheckedChanged += new System.EventHandler(this.checkBox_AlwaysUseSXLFromFile_CheckedChanged);
            // 
            // label_SXL_VersionFromFile
            // 
            this.label_SXL_VersionFromFile.AutoSize = true;
            this.label_SXL_VersionFromFile.Location = new System.Drawing.Point(138, 53);
            this.label_SXL_VersionFromFile.Name = "label_SXL_VersionFromFile";
            this.label_SXL_VersionFromFile.Size = new System.Drawing.Size(151, 13);
            this.label_SXL_VersionFromFile.TabIndex = 6;
            this.label_SXL_VersionFromFile.Text = "SXL (SUL) version found in file";
            this.label_SXL_VersionFromFile.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_SignalExchangeListVersionFromFile
            // 
            this.textBox_SignalExchangeListVersionFromFile.Enabled = false;
            this.textBox_SignalExchangeListVersionFromFile.Location = new System.Drawing.Point(304, 50);
            this.textBox_SignalExchangeListVersionFromFile.Name = "textBox_SignalExchangeListVersionFromFile";
            this.textBox_SignalExchangeListVersionFromFile.Size = new System.Drawing.Size(105, 20);
            this.textBox_SignalExchangeListVersionFromFile.TabIndex = 5;
            this.textBox_SignalExchangeListVersionFromFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_SignalExchangeListVersion
            // 
            this.textBox_SignalExchangeListVersion.Location = new System.Drawing.Point(304, 24);
            this.textBox_SignalExchangeListVersion.Name = "textBox_SignalExchangeListVersion";
            this.textBox_SignalExchangeListVersion.Size = new System.Drawing.Size(105, 20);
            this.textBox_SignalExchangeListVersion.TabIndex = 1;
            this.textBox_SignalExchangeListVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_SXL_VersionManually
            // 
            this.label_SXL_VersionManually.AutoSize = true;
            this.label_SXL_VersionManually.Location = new System.Drawing.Point(24, 27);
            this.label_SXL_VersionManually.Name = "label_SXL_VersionManually";
            this.label_SXL_VersionManually.Size = new System.Drawing.Size(265, 13);
            this.label_SXL_VersionManually.TabIndex = 0;
            this.label_SXL_VersionManually.Text = "Active SXL (SUL) version to be used when connecting";
            this.label_SXL_VersionManually.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabPage_RSMP
            // 
            this.tabPage_RSMP.Controls.Add(this.splitContainer_RSMP);
            this.tabPage_RSMP.Location = new System.Drawing.Point(4, 22);
            this.tabPage_RSMP.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_RSMP.Name = "tabPage_RSMP";
            this.tabPage_RSMP.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_RSMP.Size = new System.Drawing.Size(672, 725);
            this.tabPage_RSMP.TabIndex = 7;
            this.tabPage_RSMP.Text = "RSMP";
            this.tabPage_RSMP.UseVisualStyleBackColor = true;
            // 
            // splitContainer_RSMP
            // 
            this.splitContainer_RSMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_RSMP.Location = new System.Drawing.Point(2, 2);
            this.splitContainer_RSMP.Margin = new System.Windows.Forms.Padding(2);
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
            this.splitContainer_RSMP.Size = new System.Drawing.Size(668, 721);
            this.splitContainer_RSMP.SplitterDistance = 364;
            this.splitContainer_RSMP.SplitterWidth = 3;
            this.splitContainer_RSMP.TabIndex = 11;
            // 
            // groupBox_ProtocolSettings
            // 
            this.groupBox_ProtocolSettings.Controls.Add(this.dataGridView_Behaviour);
            this.groupBox_ProtocolSettings.Controls.Add(this.button_ResetRSMPSettingToDefault);
            this.groupBox_ProtocolSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_ProtocolSettings.Location = new System.Drawing.Point(0, 0);
            this.groupBox_ProtocolSettings.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_ProtocolSettings.Name = "groupBox_ProtocolSettings";
            this.groupBox_ProtocolSettings.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_ProtocolSettings.Size = new System.Drawing.Size(668, 364);
            this.groupBox_ProtocolSettings.TabIndex = 10;
            this.groupBox_ProtocolSettings.TabStop = false;
            this.groupBox_ProtocolSettings.Text = "&Behaviour";
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
            this.RSMP_3_1_5});
            this.dataGridView_Behaviour.Location = new System.Drawing.Point(2, 14);
            this.dataGridView_Behaviour.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_Behaviour.MultiSelect = false;
            this.dataGridView_Behaviour.Name = "dataGridView_Behaviour";
            this.dataGridView_Behaviour.RowHeadersVisible = false;
            this.dataGridView_Behaviour.RowTemplate.Height = 24;
            this.dataGridView_Behaviour.Size = new System.Drawing.Size(663, 320);
            this.dataGridView_Behaviour.TabIndex = 18;
            this.dataGridView_Behaviour.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Behaviour_CellContentClick);
            this.dataGridView_Behaviour.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Behaviour_CellValueChanged);
            // 
            // Column_Description
            // 
            this.Column_Description.HeaderText = "Parameter";
            this.Column_Description.Name = "Column_Description";
            this.Column_Description.ReadOnly = true;
            this.Column_Description.Width = 500;
            // 
            // Common
            // 
            this.Common.HeaderText = "Common";
            this.Common.Name = "Common";
            this.Common.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Common.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Common.Width = 80;
            // 
            // RSMP_3_1_1
            // 
            this.RSMP_3_1_1.HeaderText = "3.1.1";
            this.RSMP_3_1_1.Name = "RSMP_3_1_1";
            this.RSMP_3_1_1.Width = 50;
            // 
            // RSMP_3_1_2
            // 
            this.RSMP_3_1_2.HeaderText = "3.1.2";
            this.RSMP_3_1_2.Name = "RSMP_3_1_2";
            this.RSMP_3_1_2.Width = 50;
            // 
            // RSMP_3_1_3
            // 
            this.RSMP_3_1_3.HeaderText = "3.1.3";
            this.RSMP_3_1_3.Name = "RSMP_3_1_3";
            this.RSMP_3_1_3.Width = 50;
            // 
            // RSMP_3_1_4
            // 
            this.RSMP_3_1_4.HeaderText = "3.1.4";
            this.RSMP_3_1_4.Name = "RSMP_3_1_4";
            this.RSMP_3_1_4.Width = 50;
            // 
            // RSMP_3_1_5
            // 
            this.RSMP_3_1_5.HeaderText = "3.1.5";
            this.RSMP_3_1_5.Name = "RSMP_3_1_5";
            this.RSMP_3_1_5.Width = 50;
            // 
            // button_ResetRSMPSettingToDefault
            // 
            this.button_ResetRSMPSettingToDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ResetRSMPSettingToDefault.Location = new System.Drawing.Point(439, 338);
            this.button_ResetRSMPSettingToDefault.Name = "button_ResetRSMPSettingToDefault";
            this.button_ResetRSMPSettingToDefault.Size = new System.Drawing.Size(226, 20);
            this.button_ResetRSMPSettingToDefault.TabIndex = 11;
            this.button_ResetRSMPSettingToDefault.Text = "Reset behaviour to default";
            this.button_ResetRSMPSettingToDefault.UseVisualStyleBackColor = true;
            this.button_ResetRSMPSettingToDefault.Click += new System.EventHandler(this.button_ResetRSMPSettingToDefault_Click);
            // 
            // groupBox_Statistics
            // 
            this.groupBox_Statistics.Controls.Add(this.button_ClearStatistics);
            this.groupBox_Statistics.Controls.Add(this.listView_Statistics);
            this.groupBox_Statistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Statistics.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Statistics.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Statistics.Name = "groupBox_Statistics";
            this.groupBox_Statistics.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Statistics.Size = new System.Drawing.Size(668, 354);
            this.groupBox_Statistics.TabIndex = 6;
            this.groupBox_Statistics.TabStop = false;
            this.groupBox_Statistics.Text = "&Connection statistics";
            // 
            // button_ClearStatistics
            // 
            this.button_ClearStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ClearStatistics.Location = new System.Drawing.Point(601, 329);
            this.button_ClearStatistics.Name = "button_ClearStatistics";
            this.button_ClearStatistics.Size = new System.Drawing.Size(62, 20);
            this.button_ClearStatistics.TabIndex = 11;
            this.button_ClearStatistics.Text = "Clear";
            this.button_ClearStatistics.UseVisualStyleBackColor = true;
            this.button_ClearStatistics.Click += new System.EventHandler(this.button_ClearStatistics_Click);
            // 
            // tabPage_Alarms
            // 
            this.tabPage_Alarms.Controls.Add(this.splitContainer_Alarms);
            this.tabPage_Alarms.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Alarms.Name = "tabPage_Alarms";
            this.tabPage_Alarms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Alarms.Size = new System.Drawing.Size(672, 725);
            this.tabPage_Alarms.TabIndex = 1;
            this.tabPage_Alarms.Text = "Alarms";
            this.tabPage_Alarms.UseVisualStyleBackColor = true;
            // 
            // splitContainer_Alarms
            // 
            this.splitContainer_Alarms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Alarms.Location = new System.Drawing.Point(3, 3);
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
            this.splitContainer_Alarms.Size = new System.Drawing.Size(666, 719);
            this.splitContainer_Alarms.SplitterDistance = 370;
            this.splitContainer_Alarms.TabIndex = 0;
            // 
            // contextMenuStrip_Alarm
            // 
            this.contextMenuStrip_Alarm.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_Alarm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Acknowledge,
            this.ToolStripMenuItem_Suspend,
            this.toolStripSeparator_2,
            this.toolStripMenuItem_Alarm_RequestCurrentState});
            this.contextMenuStrip_Alarm.Name = "contextMenuStrip_Alarm";
            this.contextMenuStrip_Alarm.Size = new System.Drawing.Size(186, 76);
            this.contextMenuStrip_Alarm.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Alarm_Opening);
            // 
            // ToolStripMenuItem_Acknowledge
            // 
            this.ToolStripMenuItem_Acknowledge.Name = "ToolStripMenuItem_Acknowledge";
            this.ToolStripMenuItem_Acknowledge.Size = new System.Drawing.Size(185, 22);
            this.ToolStripMenuItem_Acknowledge.Tag = "AcknowledgeAndSend";
            this.ToolStripMenuItem_Acknowledge.Text = "&Acknowledge";
            this.ToolStripMenuItem_Acknowledge.Click += new System.EventHandler(this.ToolStripMenuItem_SendAlarmEvent_Click);
            // 
            // ToolStripMenuItem_Suspend
            // 
            this.ToolStripMenuItem_Suspend.Name = "ToolStripMenuItem_Suspend";
            this.ToolStripMenuItem_Suspend.Size = new System.Drawing.Size(185, 22);
            this.ToolStripMenuItem_Suspend.Tag = "SuspendAndSend";
            this.ToolStripMenuItem_Suspend.Text = "&Suspend";
            this.ToolStripMenuItem_Suspend.Click += new System.EventHandler(this.ToolStripMenuItem_SendAlarmEvent_Click);
            // 
            // toolStripSeparator_2
            // 
            this.toolStripSeparator_2.Name = "toolStripSeparator_2";
            this.toolStripSeparator_2.Size = new System.Drawing.Size(182, 6);
            // 
            // toolStripMenuItem_Alarm_RequestCurrentState
            // 
            this.toolStripMenuItem_Alarm_RequestCurrentState.Name = "toolStripMenuItem_Alarm_RequestCurrentState";
            this.toolStripMenuItem_Alarm_RequestCurrentState.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItem_Alarm_RequestCurrentState.Tag = "RequestAndSend";
            this.toolStripMenuItem_Alarm_RequestCurrentState.Text = "&Request current state";
            this.toolStripMenuItem_Alarm_RequestCurrentState.Click += new System.EventHandler(this.ToolStripMenuItem_SendAlarmEvent_Click);
            // 
            // groupBox_AlarmEvents
            // 
            this.groupBox_AlarmEvents.Controls.Add(this.listView_AlarmEvents);
            this.groupBox_AlarmEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_AlarmEvents.Location = new System.Drawing.Point(0, 0);
            this.groupBox_AlarmEvents.Name = "groupBox_AlarmEvents";
            this.groupBox_AlarmEvents.Size = new System.Drawing.Size(666, 345);
            this.groupBox_AlarmEvents.TabIndex = 0;
            this.groupBox_AlarmEvents.TabStop = false;
            this.groupBox_AlarmEvents.Text = "Alarm &Events";
            // 
            // tabPage_AggregatedStatus
            // 
            this.tabPage_AggregatedStatus.Controls.Add(this.splitContainer_AggregatedStatus);
            this.tabPage_AggregatedStatus.Location = new System.Drawing.Point(4, 22);
            this.tabPage_AggregatedStatus.Name = "tabPage_AggregatedStatus";
            this.tabPage_AggregatedStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_AggregatedStatus.Size = new System.Drawing.Size(672, 725);
            this.tabPage_AggregatedStatus.TabIndex = 3;
            this.tabPage_AggregatedStatus.Text = "Aggregated Status";
            this.tabPage_AggregatedStatus.UseVisualStyleBackColor = true;
            // 
            // splitContainer_AggregatedStatus
            // 
            this.splitContainer_AggregatedStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_AggregatedStatus.Location = new System.Drawing.Point(3, 3);
            this.splitContainer_AggregatedStatus.Name = "splitContainer_AggregatedStatus";
            this.splitContainer_AggregatedStatus.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_AggregatedStatus.Panel1
            // 
            this.splitContainer_AggregatedStatus.Panel1.Controls.Add(this.groupBox_AggregatedStatus_StatusBits);
            // 
            // splitContainer_AggregatedStatus.Panel2
            // 
            this.splitContainer_AggregatedStatus.Panel2.Controls.Add(this.splitContainer_AggregatedStatus_FunctionalStatPos_Events);
            this.splitContainer_AggregatedStatus.Size = new System.Drawing.Size(666, 719);
            this.splitContainer_AggregatedStatus.SplitterDistance = 215;
            this.splitContainer_AggregatedStatus.TabIndex = 0;
            // 
            // groupBox_AggregatedStatus_StatusBits
            // 
            this.groupBox_AggregatedStatus_StatusBits.Controls.Add(this.listView_AggregatedStatus_StatusBits);
            this.groupBox_AggregatedStatus_StatusBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_AggregatedStatus_StatusBits.Location = new System.Drawing.Point(0, 0);
            this.groupBox_AggregatedStatus_StatusBits.Name = "groupBox_AggregatedStatus_StatusBits";
            this.groupBox_AggregatedStatus_StatusBits.Size = new System.Drawing.Size(666, 215);
            this.groupBox_AggregatedStatus_StatusBits.TabIndex = 12;
            this.groupBox_AggregatedStatus_StatusBits.TabStop = false;
            this.groupBox_AggregatedStatus_StatusBits.Text = "StatusBits";
            // 
            // splitContainer_AggregatedStatus_FunctionalStatPos_Events
            // 
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Name = "splitContainer_AggregatedStatus_FunctionalStatPos_Events";
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel1
            // 
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel1.Controls.Add(this.splitContainer_FunctionalStatPos);
            // 
            // splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel2
            // 
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel2.Controls.Add(this.button_AggregatedStatus_Request);
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel2.Controls.Add(this.groupBox_AggregatedStatusEvents);
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Size = new System.Drawing.Size(666, 500);
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.SplitterDistance = 135;
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.TabIndex = 0;
            // 
            // splitContainer_FunctionalStatPos
            // 
            this.splitContainer_FunctionalStatPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_FunctionalStatPos.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_FunctionalStatPos.Name = "splitContainer_FunctionalStatPos";
            // 
            // splitContainer_FunctionalStatPos.Panel1
            // 
            this.splitContainer_FunctionalStatPos.Panel1.Controls.Add(this.groupBox_AggregatedStatus_FunctionalPosition);
            // 
            // splitContainer_FunctionalStatPos.Panel2
            // 
            this.splitContainer_FunctionalStatPos.Panel2.Controls.Add(this.groupBox_AggregatedStatus_FunctionalState);
            this.splitContainer_FunctionalStatPos.Size = new System.Drawing.Size(666, 135);
            this.splitContainer_FunctionalStatPos.SplitterDistance = 306;
            this.splitContainer_FunctionalStatPos.TabIndex = 0;
            // 
            // groupBox_AggregatedStatus_FunctionalPosition
            // 
            this.groupBox_AggregatedStatus_FunctionalPosition.Controls.Add(this.listBox_AggregatedStatus_FunctionalPosition);
            this.groupBox_AggregatedStatus_FunctionalPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_AggregatedStatus_FunctionalPosition.Location = new System.Drawing.Point(0, 0);
            this.groupBox_AggregatedStatus_FunctionalPosition.Name = "groupBox_AggregatedStatus_FunctionalPosition";
            this.groupBox_AggregatedStatus_FunctionalPosition.Size = new System.Drawing.Size(306, 135);
            this.groupBox_AggregatedStatus_FunctionalPosition.TabIndex = 15;
            this.groupBox_AggregatedStatus_FunctionalPosition.TabStop = false;
            this.groupBox_AggregatedStatus_FunctionalPosition.Text = "&FunctionalPosition";
            // 
            // listBox_AggregatedStatus_FunctionalPosition
            // 
            this.listBox_AggregatedStatus_FunctionalPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_AggregatedStatus_FunctionalPosition.FormattingEnabled = true;
            this.listBox_AggregatedStatus_FunctionalPosition.Location = new System.Drawing.Point(3, 16);
            this.listBox_AggregatedStatus_FunctionalPosition.Name = "listBox_AggregatedStatus_FunctionalPosition";
            this.listBox_AggregatedStatus_FunctionalPosition.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox_AggregatedStatus_FunctionalPosition.Size = new System.Drawing.Size(300, 116);
            this.listBox_AggregatedStatus_FunctionalPosition.TabIndex = 0;
            // 
            // groupBox_AggregatedStatus_FunctionalState
            // 
            this.groupBox_AggregatedStatus_FunctionalState.Controls.Add(this.listBox_AggregatedStatus_FunctionalState);
            this.groupBox_AggregatedStatus_FunctionalState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_AggregatedStatus_FunctionalState.Location = new System.Drawing.Point(0, 0);
            this.groupBox_AggregatedStatus_FunctionalState.Name = "groupBox_AggregatedStatus_FunctionalState";
            this.groupBox_AggregatedStatus_FunctionalState.Size = new System.Drawing.Size(356, 135);
            this.groupBox_AggregatedStatus_FunctionalState.TabIndex = 16;
            this.groupBox_AggregatedStatus_FunctionalState.TabStop = false;
            this.groupBox_AggregatedStatus_FunctionalState.Text = "&FunctionalState";
            // 
            // listBox_AggregatedStatus_FunctionalState
            // 
            this.listBox_AggregatedStatus_FunctionalState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_AggregatedStatus_FunctionalState.FormattingEnabled = true;
            this.listBox_AggregatedStatus_FunctionalState.Location = new System.Drawing.Point(3, 16);
            this.listBox_AggregatedStatus_FunctionalState.Name = "listBox_AggregatedStatus_FunctionalState";
            this.listBox_AggregatedStatus_FunctionalState.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox_AggregatedStatus_FunctionalState.Size = new System.Drawing.Size(350, 116);
            this.listBox_AggregatedStatus_FunctionalState.TabIndex = 0;
            // 
            // button_AggregatedStatus_Request
            // 
            this.button_AggregatedStatus_Request.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_AggregatedStatus_Request.Location = new System.Drawing.Point(29, 333);
            this.button_AggregatedStatus_Request.Name = "button_AggregatedStatus_Request";
            this.button_AggregatedStatus_Request.Size = new System.Drawing.Size(226, 23);
            this.button_AggregatedStatus_Request.TabIndex = 9;
            this.button_AggregatedStatus_Request.Text = "Request Aggregated Status";
            this.button_AggregatedStatus_Request.UseVisualStyleBackColor = true;
            this.button_AggregatedStatus_Request.Click += new System.EventHandler(this.button_AggregatedStatus_Request_Click);
            // 
            // groupBox_AggregatedStatusEvents
            // 
            this.groupBox_AggregatedStatusEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_AggregatedStatusEvents.Controls.Add(this.listView_AggregatedStatusEvents);
            this.groupBox_AggregatedStatusEvents.Location = new System.Drawing.Point(0, 0);
            this.groupBox_AggregatedStatusEvents.Name = "groupBox_AggregatedStatusEvents";
            this.groupBox_AggregatedStatusEvents.Size = new System.Drawing.Size(666, 327);
            this.groupBox_AggregatedStatusEvents.TabIndex = 1;
            this.groupBox_AggregatedStatusEvents.TabStop = false;
            this.groupBox_AggregatedStatusEvents.Text = "Aggregated Status &Events";
            // 
            // tabPage_Status
            // 
            this.tabPage_Status.Controls.Add(this.splitContainer_Status);
            this.tabPage_Status.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Status.Name = "tabPage_Status";
            this.tabPage_Status.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Status.Size = new System.Drawing.Size(672, 725);
            this.tabPage_Status.TabIndex = 4;
            this.tabPage_Status.Text = "Status";
            this.tabPage_Status.UseVisualStyleBackColor = true;
            // 
            // splitContainer_Status
            // 
            this.splitContainer_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Status.Location = new System.Drawing.Point(3, 3);
            this.splitContainer_Status.Name = "splitContainer_Status";
            this.splitContainer_Status.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_Status.Panel1
            // 
            this.splitContainer_Status.Panel1.Controls.Add(this.listView_Status);
            // 
            // splitContainer_Status.Panel2
            // 
            this.splitContainer_Status.Panel2.Controls.Add(this.groupBox_StatusEvents);
            this.splitContainer_Status.Size = new System.Drawing.Size(666, 719);
            this.splitContainer_Status.SplitterDistance = 370;
            this.splitContainer_Status.TabIndex = 2;
            // 
            // contextMenuStrip_Status
            // 
            this.contextMenuStrip_Status.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_StatusRequest,
            this.ToolStripMenuItem_StatusSubscribe,
            this.ToolStripMenuItem_StatusUnsubscribe});
            this.contextMenuStrip_Status.Name = "contextMenuStrip_Status";
            this.contextMenuStrip_Status.Size = new System.Drawing.Size(175, 70);
            this.contextMenuStrip_Status.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Status_Opening);
            // 
            // ToolStripMenuItem_StatusRequest
            // 
            this.ToolStripMenuItem_StatusRequest.Name = "ToolStripMenuItem_StatusRequest";
            this.ToolStripMenuItem_StatusRequest.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuItem_StatusRequest.Tag = "StatusRequest";
            this.ToolStripMenuItem_StatusRequest.Text = "Status &Request";
            this.ToolStripMenuItem_StatusRequest.Click += new System.EventHandler(this.ToolStripMenuItem_Status_Click);
            // 
            // ToolStripMenuItem_StatusSubscribe
            // 
            this.ToolStripMenuItem_StatusSubscribe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly,
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval,
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval});
            this.ToolStripMenuItem_StatusSubscribe.Name = "ToolStripMenuItem_StatusSubscribe";
            this.ToolStripMenuItem_StatusSubscribe.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuItem_StatusSubscribe.Tag = "StatusSubscribe";
            this.ToolStripMenuItem_StatusSubscribe.Text = "Status &Subscribe";
            // 
            // ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly
            // 
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly.Name = "ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly";
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly.Size = new System.Drawing.Size(236, 22);
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly.Text = "Update on change";
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly.Click += new System.EventHandler(this.ToolStripMenuItem_Status_Click);
            // 
            // ToolStripMenuItem_StatusSubscribe_UpdateOnInterval
            // 
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually,
            this.toolStripSeparator2});
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval.Name = "ToolStripMenuItem_StatusSubscribe_UpdateOnInterval";
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval.Size = new System.Drawing.Size(236, 22);
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval.Text = "Update on interval";
            // 
            // ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually
            // 
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually.Name = "ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually";
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually.Text = "Enter Update Rate manually";
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually.Click += new System.EventHandler(this.ToolStripMenuItem_Status_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval
            // 
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually,
            this.toolStripSeparator3});
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval.Name = "ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval";
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval.Size = new System.Drawing.Size(236, 22);
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval.Text = "Update on change and interval";
            // 
            // ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually
            // 
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually.Name = "ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually";
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually.Text = "Enter Update Rate manually";
            this.ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually.Click += new System.EventHandler(this.ToolStripMenuItem_Status_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(217, 6);
            // 
            // ToolStripMenuItem_StatusUnsubscribe
            // 
            this.ToolStripMenuItem_StatusUnsubscribe.Name = "ToolStripMenuItem_StatusUnsubscribe";
            this.ToolStripMenuItem_StatusUnsubscribe.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuItem_StatusUnsubscribe.Tag = "StatusUnsubscribe";
            this.ToolStripMenuItem_StatusUnsubscribe.Text = "Status &Unsubscribe";
            this.ToolStripMenuItem_StatusUnsubscribe.Click += new System.EventHandler(this.ToolStripMenuItem_Status_Click);
            // 
            // groupBox_StatusEvents
            // 
            this.groupBox_StatusEvents.Controls.Add(this.listView_StatusEvents);
            this.groupBox_StatusEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_StatusEvents.Location = new System.Drawing.Point(0, 0);
            this.groupBox_StatusEvents.Name = "groupBox_StatusEvents";
            this.groupBox_StatusEvents.Size = new System.Drawing.Size(666, 345);
            this.groupBox_StatusEvents.TabIndex = 0;
            this.groupBox_StatusEvents.TabStop = false;
            this.groupBox_StatusEvents.Text = "Status &Events";
            // 
            // tabPage_Commands
            // 
            this.tabPage_Commands.Controls.Add(this.splitContainer_Commands);
            this.tabPage_Commands.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Commands.Name = "tabPage_Commands";
            this.tabPage_Commands.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Commands.Size = new System.Drawing.Size(672, 725);
            this.tabPage_Commands.TabIndex = 5;
            this.tabPage_Commands.Text = "Commands";
            this.tabPage_Commands.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip_Commands
            // 
            this.contextMenuStrip_Commands.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Commands});
            this.contextMenuStrip_Commands.Name = "contextMenuStrip_Commands";
            this.contextMenuStrip_Commands.Size = new System.Drawing.Size(137, 26);
            this.contextMenuStrip_Commands.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Commands_Opening);
            // 
            // ToolStripMenuItem_Commands
            // 
            this.ToolStripMenuItem_Commands.Name = "ToolStripMenuItem_Commands";
            this.ToolStripMenuItem_Commands.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItem_Commands.Tag = "SelectCommandsAndSend";
            this.ToolStripMenuItem_Commands.Text = "&Commands";
            this.ToolStripMenuItem_Commands.Click += new System.EventHandler(this.ToolStripMenuItem_Commands_Click);
            // 
            // tabPage_TestSend
            // 
            this.tabPage_TestSend.Controls.Add(this.splitContainer_TestSend);
            this.tabPage_TestSend.Location = new System.Drawing.Point(4, 22);
            this.tabPage_TestSend.Name = "tabPage_TestSend";
            this.tabPage_TestSend.Size = new System.Drawing.Size(672, 725);
            this.tabPage_TestSend.TabIndex = 6;
            this.tabPage_TestSend.Text = "Test send";
            this.tabPage_TestSend.UseVisualStyleBackColor = true;
            // 
            // splitContainer_TestSend
            // 
            this.splitContainer_TestSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_TestSend.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_TestSend.Name = "splitContainer_TestSend";
            // 
            // splitContainer_TestSend.Panel1
            // 
            this.splitContainer_TestSend.Panel1.Controls.Add(this.button_TestPackage_1_Browse);
            this.splitContainer_TestSend.Panel1.Controls.Add(this.button_SendTestPackage_1);
            this.splitContainer_TestSend.Panel1.Controls.Add(this.groupBox_TestSend1);
            // 
            // splitContainer_TestSend.Panel2
            // 
            this.splitContainer_TestSend.Panel2.Controls.Add(this.button_SendTestPackage_2);
            this.splitContainer_TestSend.Panel2.Controls.Add(this.button_TestPackage_2_Browse);
            this.splitContainer_TestSend.Panel2.Controls.Add(this.groupBox_TestSend2);
            this.splitContainer_TestSend.Size = new System.Drawing.Size(672, 725);
            this.splitContainer_TestSend.SplitterDistance = 318;
            this.splitContainer_TestSend.TabIndex = 0;
            // 
            // button_TestPackage_1_Browse
            // 
            this.button_TestPackage_1_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_TestPackage_1_Browse.Location = new System.Drawing.Point(160, 695);
            this.button_TestPackage_1_Browse.Name = "button_TestPackage_1_Browse";
            this.button_TestPackage_1_Browse.Size = new System.Drawing.Size(130, 23);
            this.button_TestPackage_1_Browse.TabIndex = 6;
            this.button_TestPackage_1_Browse.Text = "Browse...";
            this.button_TestPackage_1_Browse.UseVisualStyleBackColor = true;
            this.button_TestPackage_1_Browse.Click += new System.EventHandler(this.button_TestPackage_1_Browse_Click);
            // 
            // button_SendTestPackage_1
            // 
            this.button_SendTestPackage_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_SendTestPackage_1.Location = new System.Drawing.Point(16, 695);
            this.button_SendTestPackage_1.Name = "button_SendTestPackage_1";
            this.button_SendTestPackage_1.Size = new System.Drawing.Size(130, 23);
            this.button_SendTestPackage_1.TabIndex = 3;
            this.button_SendTestPackage_1.Text = "Send above package";
            this.button_SendTestPackage_1.UseVisualStyleBackColor = true;
            this.button_SendTestPackage_1.Click += new System.EventHandler(this.button_SendTestPackage_1_Click);
            // 
            // groupBox_TestSend1
            // 
            this.groupBox_TestSend1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_TestSend1.Controls.Add(this.textBox_TestPackage_1);
            this.groupBox_TestSend1.Location = new System.Drawing.Point(0, 0);
            this.groupBox_TestSend1.Name = "groupBox_TestSend1";
            this.groupBox_TestSend1.Size = new System.Drawing.Size(318, 687);
            this.groupBox_TestSend1.TabIndex = 0;
            this.groupBox_TestSend1.TabStop = false;
            this.groupBox_TestSend1.Text = "JSon package 1";
            // 
            // textBox_TestPackage_1
            // 
            this.textBox_TestPackage_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TestPackage_1.Location = new System.Drawing.Point(3, 19);
            this.textBox_TestPackage_1.Multiline = true;
            this.textBox_TestPackage_1.Name = "textBox_TestPackage_1";
            this.textBox_TestPackage_1.Size = new System.Drawing.Size(310, 664);
            this.textBox_TestPackage_1.TabIndex = 1;
            // 
            // button_SendTestPackage_2
            // 
            this.button_SendTestPackage_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_SendTestPackage_2.Location = new System.Drawing.Point(16, 695);
            this.button_SendTestPackage_2.Name = "button_SendTestPackage_2";
            this.button_SendTestPackage_2.Size = new System.Drawing.Size(130, 23);
            this.button_SendTestPackage_2.TabIndex = 8;
            this.button_SendTestPackage_2.Text = "Send above package";
            this.button_SendTestPackage_2.UseVisualStyleBackColor = true;
            this.button_SendTestPackage_2.Click += new System.EventHandler(this.button_SendTestPackage_2_Click);
            // 
            // button_TestPackage_2_Browse
            // 
            this.button_TestPackage_2_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_TestPackage_2_Browse.Location = new System.Drawing.Point(159, 695);
            this.button_TestPackage_2_Browse.Name = "button_TestPackage_2_Browse";
            this.button_TestPackage_2_Browse.Size = new System.Drawing.Size(130, 23);
            this.button_TestPackage_2_Browse.TabIndex = 7;
            this.button_TestPackage_2_Browse.Text = "Browse...";
            this.button_TestPackage_2_Browse.UseVisualStyleBackColor = true;
            this.button_TestPackage_2_Browse.Click += new System.EventHandler(this.button_TestPackage_2_Browse_Click);
            // 
            // groupBox_TestSend2
            // 
            this.groupBox_TestSend2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_TestSend2.Controls.Add(this.textBox_TestPackage_2);
            this.groupBox_TestSend2.Location = new System.Drawing.Point(0, 0);
            this.groupBox_TestSend2.Name = "groupBox_TestSend2";
            this.groupBox_TestSend2.Size = new System.Drawing.Size(318, 687);
            this.groupBox_TestSend2.TabIndex = 1;
            this.groupBox_TestSend2.TabStop = false;
            this.groupBox_TestSend2.Text = "JSon package 2";
            // 
            // textBox_TestPackage_2
            // 
            this.textBox_TestPackage_2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_TestPackage_2.Location = new System.Drawing.Point(6, 19);
            this.textBox_TestPackage_2.Multiline = true;
            this.textBox_TestPackage_2.Name = "textBox_TestPackage_2";
            this.textBox_TestPackage_2.Size = new System.Drawing.Size(306, 664);
            this.textBox_TestPackage_2.TabIndex = 4;
            // 
            // openFileDialog_TestPackage
            // 
            this.openFileDialog_TestPackage.Filter = "Debug (text) files|*.txt|All files|*.*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 0;
            // 
            // saveFileDialog_SXL
            // 
            this.saveFileDialog_SXL.Filter = "JSon|*.json|XML|*.xml|All files|*.*";
            // 
            // openFileDialog_Sequence
            // 
            this.openFileDialog_Sequence.Filter = "Sequence files|*.seq|All files|*.*";
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
            this.listView_SysLog.Location = new System.Drawing.Point(6, 18);
            this.listView_SysLog.Margin = new System.Windows.Forms.Padding(2);
            this.listView_SysLog.MultiSelect = false;
            this.listView_SysLog.Name = "listView_SysLog";
            this.listView_SysLog.ShowItemToolTips = true;
            this.listView_SysLog.Size = new System.Drawing.Size(301, 258);
            this.listView_SysLog.SmallImageList = this.imageList_Severity;
            this.listView_SysLog.TabIndex = 0;
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
            this.listView_Statistics.Location = new System.Drawing.Point(2, 15);
            this.listView_Statistics.Margin = new System.Windows.Forms.Padding(2);
            this.listView_Statistics.Name = "listView_Statistics";
            this.listView_Statistics.Size = new System.Drawing.Size(665, 310);
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
            this.listView_Alarms.Name = "listView_Alarms";
            this.listView_Alarms.Size = new System.Drawing.Size(666, 370);
            this.listView_Alarms.TabIndex = 1;
            this.listView_Alarms.UseCompatibleStateImageBehavior = false;
            this.listView_Alarms.View = System.Windows.Forms.View.Details;
            this.listView_Alarms.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // columnHeader_Alarms_Status
            // 
            this.columnHeader_Alarms_Status.Text = "Status";
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
            this.listView_AlarmEvents.Location = new System.Drawing.Point(3, 16);
            this.listView_AlarmEvents.MultiSelect = false;
            this.listView_AlarmEvents.Name = "listView_AlarmEvents";
            this.listView_AlarmEvents.Size = new System.Drawing.Size(660, 326);
            this.listView_AlarmEvents.TabIndex = 3;
            this.listView_AlarmEvents.UseCompatibleStateImageBehavior = false;
            this.listView_AlarmEvents.View = System.Windows.Forms.View.Details;
            this.listView_AlarmEvents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // columnHeader_AlarmEvent_TimeStamp
            // 
            this.columnHeader_AlarmEvent_TimeStamp.Tag = "AlarmEvent_Timestamp";
            this.columnHeader_AlarmEvent_TimeStamp.Text = "Timestamp";
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
            this.columnHeader_AlarmEvent_MsgId.Width = 260;
            // 
            // columnHeader_AlarmEvent_AlarmCodeId
            // 
            this.columnHeader_AlarmEvent_AlarmCodeId.Tag = "AlarmEvent_AlarmCodeId";
            this.columnHeader_AlarmEvent_AlarmCodeId.Text = "AlarmCodeId";
            this.columnHeader_AlarmEvent_AlarmCodeId.Width = 116;
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
            this.columnHeader_AlarmEvent_Event.Width = 127;
            // 
            // listView_AggregatedStatus_StatusBits
            // 
            this.listView_AggregatedStatus_StatusBits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_StatusBits_BitNumber,
            this.columnHeader_StatusBits_NTSColor,
            this.columnHeader_StatusBits_Description});
            this.listView_AggregatedStatus_StatusBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_AggregatedStatus_StatusBits.FullRowSelect = true;
            this.listView_AggregatedStatus_StatusBits.Location = new System.Drawing.Point(3, 16);
            this.listView_AggregatedStatus_StatusBits.MultiSelect = false;
            this.listView_AggregatedStatus_StatusBits.Name = "listView_AggregatedStatus_StatusBits";
            this.listView_AggregatedStatus_StatusBits.Size = new System.Drawing.Size(660, 196);
            this.listView_AggregatedStatus_StatusBits.SmallImageList = this.imageList_ListView;
            this.listView_AggregatedStatus_StatusBits.TabIndex = 7;
            this.listView_AggregatedStatus_StatusBits.UseCompatibleStateImageBehavior = false;
            this.listView_AggregatedStatus_StatusBits.View = System.Windows.Forms.View.Details;
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
            this.columnHeader_StatusBits_Description.Width = 434;
            // 
            // listView_AggregatedStatusEvents
            // 
            this.listView_AggregatedStatusEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_AggStatEvent_TimeStamp,
            this.columnHeader_AggStatEvent_MsgId,
            this.columnHeader_AggStatEvent_BitStatus,
            this.columnHeader_AggStatEvent_FuncPos,
            this.columnHeader_AggStatEvent_FuncState});
            this.listView_AggregatedStatusEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_AggregatedStatusEvents.FullRowSelect = true;
            this.listView_AggregatedStatusEvents.GridLines = true;
            this.listView_AggregatedStatusEvents.HideSelection = false;
            this.listView_AggregatedStatusEvents.Location = new System.Drawing.Point(3, 16);
            this.listView_AggregatedStatusEvents.MultiSelect = false;
            this.listView_AggregatedStatusEvents.Name = "listView_AggregatedStatusEvents";
            this.listView_AggregatedStatusEvents.Size = new System.Drawing.Size(660, 308);
            this.listView_AggregatedStatusEvents.TabIndex = 3;
            this.listView_AggregatedStatusEvents.UseCompatibleStateImageBehavior = false;
            this.listView_AggregatedStatusEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_AggStatEvent_TimeStamp
            // 
            this.columnHeader_AggStatEvent_TimeStamp.Tag = "AggStatEvent_Timestamp";
            this.columnHeader_AggStatEvent_TimeStamp.Text = "Timestamp";
            this.columnHeader_AggStatEvent_TimeStamp.Width = 181;
            // 
            // columnHeader_AggStatEvent_MsgId
            // 
            this.columnHeader_AggStatEvent_MsgId.Tag = "AggStatEvent_MsgId";
            this.columnHeader_AggStatEvent_MsgId.Text = "MessageId";
            this.columnHeader_AggStatEvent_MsgId.Width = 260;
            // 
            // columnHeader_AggStatEvent_BitStatus
            // 
            this.columnHeader_AggStatEvent_BitStatus.Tag = "AggStatEvent_BitStatus";
            this.columnHeader_AggStatEvent_BitStatus.Text = "BitStatus";
            this.columnHeader_AggStatEvent_BitStatus.Width = 116;
            // 
            // columnHeader_AggStatEvent_FuncPos
            // 
            this.columnHeader_AggStatEvent_FuncPos.Tag = "AggStatEvent_FuncPos";
            this.columnHeader_AggStatEvent_FuncPos.Text = "FunctionalPosition";
            this.columnHeader_AggStatEvent_FuncPos.Width = 127;
            // 
            // columnHeader_AggStatEvent_FuncState
            // 
            this.columnHeader_AggStatEvent_FuncState.Tag = "AggStatEvent_FuncState";
            this.columnHeader_AggStatEvent_FuncState.Text = "FunctionalState";
            this.columnHeader_AggStatEvent_FuncState.Width = 109;
            // 
            // listView_Status
            // 
            this.listView_Status.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Status_StatusCodeId,
            this.columnHeader_Status_Description,
            this.columnHeader_Status_Name,
            this.columnHeader_Status_Type,
            this.columnHeader_Status_Status,
            this.columnHeader_Status_Quality,
            this.columnHeader_Status_UpdateRate,
            this.columnHeader_Status_UpdateOnChange,
            this.columnHeader_Status_Comment});
            this.listView_Status.ContextMenuStrip = this.contextMenuStrip_Status;
            this.listView_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Status.FullRowSelect = true;
            this.listView_Status.GridLines = true;
            this.listView_Status.HideSelection = false;
            this.listView_Status.Location = new System.Drawing.Point(0, 0);
            this.listView_Status.Name = "listView_Status";
            this.listView_Status.Size = new System.Drawing.Size(666, 370);
            this.listView_Status.TabIndex = 1;
            this.listView_Status.UseCompatibleStateImageBehavior = false;
            this.listView_Status.View = System.Windows.Forms.View.Details;
            this.listView_Status.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // columnHeader_Status_StatusCodeId
            // 
            this.columnHeader_Status_StatusCodeId.Text = "StatusCodeId";
            this.columnHeader_Status_StatusCodeId.Width = 104;
            // 
            // columnHeader_Status_Description
            // 
            this.columnHeader_Status_Description.Text = "Description";
            this.columnHeader_Status_Description.Width = 90;
            // 
            // columnHeader_Status_Name
            // 
            this.columnHeader_Status_Name.Text = "Name";
            // 
            // columnHeader_Status_Type
            // 
            this.columnHeader_Status_Type.Text = "Type";
            // 
            // columnHeader_Status_Status
            // 
            this.columnHeader_Status_Status.Text = "Status";
            // 
            // columnHeader_Status_Quality
            // 
            this.columnHeader_Status_Quality.Text = "Quality";
            // 
            // columnHeader_Status_UpdateRate
            // 
            this.columnHeader_Status_UpdateRate.Text = "UpdateRate";
            this.columnHeader_Status_UpdateRate.Width = 97;
            // 
            // columnHeader_Status_UpdateOnChange
            // 
            this.columnHeader_Status_UpdateOnChange.Text = "UpdateOnChange";
            this.columnHeader_Status_UpdateOnChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader_Status_Comment
            // 
            this.columnHeader_Status_Comment.Text = "Comment";
            // 
            // listView_StatusEvents
            // 
            this.listView_StatusEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_StatusEvents_TimeStamp,
            this.columnHeader_StatusEvents_MessageId,
            this.columnHeader_StatusEvents_Event,
            this.columnHeader_StatusEvents_StatusCodeId,
            this.columnHeader_StatusEvents_Name,
            this.columnHeader_StatusEvents_Status,
            this.columnHeader_StatusEvents_Quality,
            this.columnHeader_StatusEvents_UpdateRate});
            this.listView_StatusEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_StatusEvents.FullRowSelect = true;
            this.listView_StatusEvents.GridLines = true;
            this.listView_StatusEvents.HideSelection = false;
            this.listView_StatusEvents.Location = new System.Drawing.Point(3, 16);
            this.listView_StatusEvents.MultiSelect = false;
            this.listView_StatusEvents.Name = "listView_StatusEvents";
            this.listView_StatusEvents.Size = new System.Drawing.Size(660, 326);
            this.listView_StatusEvents.TabIndex = 3;
            this.listView_StatusEvents.UseCompatibleStateImageBehavior = false;
            this.listView_StatusEvents.View = System.Windows.Forms.View.Details;
            this.listView_StatusEvents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // columnHeader_StatusEvents_TimeStamp
            // 
            this.columnHeader_StatusEvents_TimeStamp.Tag = "";
            this.columnHeader_StatusEvents_TimeStamp.Text = "Timestamp";
            this.columnHeader_StatusEvents_TimeStamp.Width = 91;
            // 
            // columnHeader_StatusEvents_MessageId
            // 
            this.columnHeader_StatusEvents_MessageId.Tag = "";
            this.columnHeader_StatusEvents_MessageId.Text = "MessageId";
            this.columnHeader_StatusEvents_MessageId.Width = 139;
            // 
            // columnHeader_StatusEvents_Event
            // 
            this.columnHeader_StatusEvents_Event.Tag = "";
            this.columnHeader_StatusEvents_Event.Text = "Event";
            this.columnHeader_StatusEvents_Event.Width = 127;
            // 
            // columnHeader_StatusEvents_StatusCodeId
            // 
            this.columnHeader_StatusEvents_StatusCodeId.Text = "StatusCodeId";
            this.columnHeader_StatusEvents_StatusCodeId.Width = 92;
            // 
            // columnHeader_StatusEvents_Name
            // 
            this.columnHeader_StatusEvents_Name.Text = "Name";
            // 
            // columnHeader_StatusEvents_Status
            // 
            this.columnHeader_StatusEvents_Status.Text = "Status";
            // 
            // columnHeader_StatusEvents_Quality
            // 
            this.columnHeader_StatusEvents_Quality.Text = "Quality";
            // 
            // columnHeader_StatusEvents_UpdateRate
            // 
            this.columnHeader_StatusEvents_UpdateRate.Text = "UpdateRate";
            this.columnHeader_StatusEvents_UpdateRate.Width = 99;
            // 
            // splitContainer_Commands
            // 
            this.splitContainer_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Commands.Location = new System.Drawing.Point(3, 3);
            this.splitContainer_Commands.Name = "splitContainer_Commands";
            this.splitContainer_Commands.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_Commands.Panel1
            // 
            this.splitContainer_Commands.Panel1.Controls.Add(this.listView_Commands);
            // 
            // splitContainer_Commands.Panel2
            // 
            this.splitContainer_Commands.Panel2.Controls.Add(this.groupBox_CommandEvents);
            this.splitContainer_Commands.Size = new System.Drawing.Size(666, 719);
            this.splitContainer_Commands.SplitterDistance = 370;
            this.splitContainer_Commands.TabIndex = 1;
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
            this.listView_Commands.ContextMenuStrip = this.contextMenuStrip_Commands;
            this.listView_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Commands.FullRowSelect = true;
            this.listView_Commands.GridLines = true;
            this.listView_Commands.HideSelection = false;
            this.listView_Commands.Location = new System.Drawing.Point(0, 0);
            this.listView_Commands.Name = "listView_Commands";
            this.listView_Commands.Size = new System.Drawing.Size(666, 370);
            this.listView_Commands.TabIndex = 1;
            this.listView_Commands.UseCompatibleStateImageBehavior = false;
            this.listView_Commands.View = System.Windows.Forms.View.Details;
            this.listView_Commands.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // columnHeader_Commands_CommandCodeId
            // 
            this.columnHeader_Commands_CommandCodeId.Text = "CommandCodeId";
            this.columnHeader_Commands_CommandCodeId.Width = 103;
            // 
            // columnHeader_Commands_Description
            // 
            this.columnHeader_Commands_Description.Text = "Description";
            this.columnHeader_Commands_Description.Width = 90;
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
            // groupBox_CommandEvents
            // 
            this.groupBox_CommandEvents.Controls.Add(this.listView_CommandEvents);
            this.groupBox_CommandEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_CommandEvents.Location = new System.Drawing.Point(0, 0);
            this.groupBox_CommandEvents.Name = "groupBox_CommandEvents";
            this.groupBox_CommandEvents.Size = new System.Drawing.Size(666, 345);
            this.groupBox_CommandEvents.TabIndex = 0;
            this.groupBox_CommandEvents.TabStop = false;
            this.groupBox_CommandEvents.Text = "Command &Events";
            // 
            // listView_CommandEvents
            // 
            this.listView_CommandEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_CommandEvent_TimeStamp,
            this.columnHeader_CommandEvent_MsgId,
            this.columnHeader_CommandEvent_Event,
            this.columnHeader_CommandEvent_CommandCodeId,
            this.columnHeader_CommandEvent_Name,
            this.columnHeader_CommandEvent_Command,
            this.columnHeader_CommandEvent_Value,
            this.columnHeader_CommandEvent_Age});
            this.listView_CommandEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_CommandEvents.FullRowSelect = true;
            this.listView_CommandEvents.GridLines = true;
            this.listView_CommandEvents.HideSelection = false;
            this.listView_CommandEvents.Location = new System.Drawing.Point(3, 16);
            this.listView_CommandEvents.MultiSelect = false;
            this.listView_CommandEvents.Name = "listView_CommandEvents";
            this.listView_CommandEvents.Size = new System.Drawing.Size(660, 326);
            this.listView_CommandEvents.TabIndex = 3;
            this.listView_CommandEvents.UseCompatibleStateImageBehavior = false;
            this.listView_CommandEvents.View = System.Windows.Forms.View.Details;
            this.listView_CommandEvents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // columnHeader_CommandEvent_TimeStamp
            // 
            this.columnHeader_CommandEvent_TimeStamp.Tag = "CommandEvent_Timestamp";
            this.columnHeader_CommandEvent_TimeStamp.Text = "Timestamp";
            this.columnHeader_CommandEvent_TimeStamp.Width = 181;
            // 
            // columnHeader_CommandEvent_MsgId
            // 
            this.columnHeader_CommandEvent_MsgId.Tag = "CommandEvent_MsgId";
            this.columnHeader_CommandEvent_MsgId.Text = "MessageId";
            this.columnHeader_CommandEvent_MsgId.Width = 260;
            // 
            // columnHeader_CommandEvent_Event
            // 
            this.columnHeader_CommandEvent_Event.Tag = "CommandEvent_Event";
            this.columnHeader_CommandEvent_Event.Text = "Event";
            this.columnHeader_CommandEvent_Event.Width = 127;
            // 
            // columnHeader_CommandEvent_CommandCodeId
            // 
            this.columnHeader_CommandEvent_CommandCodeId.Text = "CommandCodeId";
            // 
            // columnHeader_CommandEvent_Name
            // 
            this.columnHeader_CommandEvent_Name.Text = "Name";
            // 
            // columnHeader_CommandEvent_Command
            // 
            this.columnHeader_CommandEvent_Command.Text = "Command";
            this.columnHeader_CommandEvent_Command.Width = 99;
            // 
            // columnHeader_CommandEvent_Value
            // 
            this.columnHeader_CommandEvent_Value.Text = "Value";
            // 
            // columnHeader_CommandEvent_Age
            // 
            this.columnHeader_CommandEvent_Age.Text = "Age";
            // 
            // RSMPGS_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 778);
            this.Controls.Add(this.splitContainer_Main);
            this.Controls.Add(this.menuStrip_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RSMPGS_Main";
            this.Text = "RSMPGS2 - SCADA Interface Simulator - version";
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
            this.splitContainer_AggregatedStatus.Panel1.ResumeLayout(false);
            this.splitContainer_AggregatedStatus.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_AggregatedStatus)).EndInit();
            this.splitContainer_AggregatedStatus.ResumeLayout(false);
            this.groupBox_AggregatedStatus_StatusBits.ResumeLayout(false);
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel1.ResumeLayout(false);
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_AggregatedStatus_FunctionalStatPos_Events)).EndInit();
            this.splitContainer_AggregatedStatus_FunctionalStatPos_Events.ResumeLayout(false);
            this.splitContainer_FunctionalStatPos.Panel1.ResumeLayout(false);
            this.splitContainer_FunctionalStatPos.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_FunctionalStatPos)).EndInit();
            this.splitContainer_FunctionalStatPos.ResumeLayout(false);
            this.groupBox_AggregatedStatus_FunctionalPosition.ResumeLayout(false);
            this.groupBox_AggregatedStatus_FunctionalState.ResumeLayout(false);
            this.groupBox_AggregatedStatusEvents.ResumeLayout(false);
            this.tabPage_Status.ResumeLayout(false);
            this.splitContainer_Status.Panel1.ResumeLayout(false);
            this.splitContainer_Status.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Status)).EndInit();
            this.splitContainer_Status.ResumeLayout(false);
            this.contextMenuStrip_Status.ResumeLayout(false);
            this.groupBox_StatusEvents.ResumeLayout(false);
            this.tabPage_Commands.ResumeLayout(false);
            this.contextMenuStrip_Commands.ResumeLayout(false);
            this.tabPage_TestSend.ResumeLayout(false);
            this.splitContainer_TestSend.Panel1.ResumeLayout(false);
            this.splitContainer_TestSend.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_TestSend)).EndInit();
            this.splitContainer_TestSend.ResumeLayout(false);
            this.groupBox_TestSend1.ResumeLayout(false);
            this.groupBox_TestSend1.PerformLayout();
            this.groupBox_TestSend2.ResumeLayout(false);
            this.groupBox_TestSend2.PerformLayout();
            this.splitContainer_Commands.Panel1.ResumeLayout(false);
            this.splitContainer_Commands.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Commands)).EndInit();
            this.splitContainer_Commands.ResumeLayout(false);
            this.groupBox_CommandEvents.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer_System;
        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_CreateNew;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_Tile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Debug_CloseAll;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_0;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_Close;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Connection;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ConnectAutomatically;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_Connect;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ConnectNow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Disconnect;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_4;
        private System.Windows.Forms.SplitContainer splitContainer_Main;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SendOptions;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Alarm;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Acknowledge;
        private System.Windows.Forms.SplitContainer splitContainer_ObjectsAndSyslog;
        private System.Windows.Forms.GroupBox groupBox_SitesAndObjects;
        private System.Windows.Forms.CheckBox checkBox_ShowTooltip;
        private System.Windows.Forms.GroupBox groupBox_SystemLog;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Delimiter_1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_Reset;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Suspend;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SplitPackets;
        private System.Windows.Forms.OpenFileDialog openFileDialog_TestPackage;
        private System.Windows.Forms.ImageList imageList_ListView;
        public System.Windows.Forms.TreeView treeView_SitesAndObjects;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_DisableNagleAlgorithm;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Commands;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Commands;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Status;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StatusRequest;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StatusSubscribe;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StatusUnsubscribe;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_LoadAtStartUp;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ProcessImage_Load;
        private System.Windows.Forms.ToolStripMenuItem subscriptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Subscriptions_ResendAll;
        private System.Windows.Forms.ToolStripMenuItem eventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Subscriptions_UnsubscribeAll;
        private System.Windows.Forms.TabControl tabControl_Object;
        private System.Windows.Forms.TabPage tabPage_Generic;
        private System.Windows.Forms.TabPage tabPage_Alarms;
        private System.Windows.Forms.SplitContainer splitContainer_Alarms;
        private System.Windows.Forms.ColumnHeader columnHeader_Alarms_Status;
        private System.Windows.Forms.ColumnHeader columnHeader_Alarms_AlarmCodeId;
        private System.Windows.Forms.ColumnHeader columnHeader_Alarms_Description;
        private System.Windows.Forms.ColumnHeader columnHeader_Alarms_ExternalAlarmCodeId;
        private System.Windows.Forms.ColumnHeader columnHeader_Alarms_ExternalNtSAlarmCodeId;
        private System.Windows.Forms.ColumnHeader columnHeader_Alarms_Priority;
        private System.Windows.Forms.ColumnHeader columnHeader_Alarms_Category;
        private System.Windows.Forms.GroupBox groupBox_AlarmEvents;
        private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_TimeStamp;
        private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_MsgId;
        private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_AlarmCodeId;
        private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_Event;
        private System.Windows.Forms.TabPage tabPage_AggregatedStatus;
        private System.Windows.Forms.SplitContainer splitContainer_AggregatedStatus;
        private System.Windows.Forms.GroupBox groupBox_AggregatedStatus_StatusBits;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusBits_BitNumber;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusBits_NTSColor;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusBits_Description;
        private System.Windows.Forms.SplitContainer splitContainer_AggregatedStatus_FunctionalStatPos_Events;
        private System.Windows.Forms.SplitContainer splitContainer_FunctionalStatPos;
        private System.Windows.Forms.GroupBox groupBox_AggregatedStatus_FunctionalPosition;
        private System.Windows.Forms.ListBox listBox_AggregatedStatus_FunctionalPosition;
        private System.Windows.Forms.GroupBox groupBox_AggregatedStatus_FunctionalState;
        private System.Windows.Forms.ListBox listBox_AggregatedStatus_FunctionalState;
        private System.Windows.Forms.GroupBox groupBox_AggregatedStatusEvents;
        private System.Windows.Forms.ColumnHeader columnHeader_AggStatEvent_TimeStamp;
        private System.Windows.Forms.ColumnHeader columnHeader_AggStatEvent_MsgId;
        private System.Windows.Forms.ColumnHeader columnHeader_AggStatEvent_BitStatus;
        private System.Windows.Forms.ColumnHeader columnHeader_AggStatEvent_FuncPos;
        private System.Windows.Forms.ColumnHeader columnHeader_AggStatEvent_FuncState;
        private System.Windows.Forms.TabPage tabPage_Status;
        private System.Windows.Forms.SplitContainer splitContainer_Status;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_Description;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_StatusCodeId;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_Name;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_Type;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_Status;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_Quality;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_UpdateRate;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_Comment;
        private System.Windows.Forms.GroupBox groupBox_StatusEvents;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusEvents_TimeStamp;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusEvents_MessageId;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusEvents_Event;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusEvents_StatusCodeId;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusEvents_Name;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusEvents_Status;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusEvents_Quality;
        private System.Windows.Forms.ColumnHeader columnHeader_StatusEvents_UpdateRate;
        private System.Windows.Forms.TabPage tabPage_Commands;
        private System.Windows.Forms.SplitContainer splitContainer_Commands;
        private System.Windows.Forms.ColumnHeader columnHeader_Commands_Description;
        private System.Windows.Forms.ColumnHeader columnHeader_Commands_CommandCodeId;
        private System.Windows.Forms.ColumnHeader columnHeader_Commands_Name;
        private System.Windows.Forms.ColumnHeader columnHeader_Commands_Command;
        private System.Windows.Forms.ColumnHeader columnHeader_Commands_Type;
        private System.Windows.Forms.ColumnHeader columnHeader_Commands_Value;
        private System.Windows.Forms.ColumnHeader columnHeader_Commands_Age;
        private System.Windows.Forms.ColumnHeader columnHeader_Commands_Comment;
        private System.Windows.Forms.GroupBox groupBox_CommandEvents;
        private System.Windows.Forms.ColumnHeader columnHeader_CommandEvent_TimeStamp;
        private System.Windows.Forms.ColumnHeader columnHeader_CommandEvent_MsgId;
        private System.Windows.Forms.ColumnHeader columnHeader_CommandEvent_Event;
        private System.Windows.Forms.ColumnHeader columnHeader_CommandEvent_CommandCodeId;
        private System.Windows.Forms.ColumnHeader columnHeader_CommandEvent_Name;
        private System.Windows.Forms.ColumnHeader columnHeader_CommandEvent_Command;
        private System.Windows.Forms.ColumnHeader columnHeader_CommandEvent_Value;
        private System.Windows.Forms.ColumnHeader columnHeader_CommandEvent_Age;
        private System.Windows.Forms.TabPage tabPage_TestSend;
        private System.Windows.Forms.SplitContainer splitContainer_TestSend;
        private System.Windows.Forms.GroupBox groupBox_TestSend1;
        private System.Windows.Forms.GroupBox groupBox_TestSend2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_EventFiles_SaveCont;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SendSomeRandomCrap;
        private System.Windows.Forms.GroupBox groupBox_SXL_Version;
        public System.Windows.Forms.CheckBox checkBox_AlwaysUseSXLFromFile;
        private System.Windows.Forms.Label label_SXL_VersionFromFile;
        public System.Windows.Forms.TextBox textBox_SignalExchangeListVersionFromFile;
        public System.Windows.Forms.TextBox textBox_SignalExchangeListVersion;
        private System.Windows.Forms.Label label_SXL_VersionManually;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StoreBase64Updates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_Delimiter_1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_SXL;
        private System.Windows.Forms.ImageList imageList_Severity;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Sequence;
        private System.Windows.Forms.ColumnHeader columnHeader_SysLog_Severity;
        private System.Windows.Forms.ColumnHeader columnHeader_SysLog_TimeStamp;
        private System.Windows.Forms.ColumnHeader columnHeader_SysLog_Description;
        private System.Windows.Forms.Button button_ClearSystemLog;
        public System.Windows.Forms.CheckBox checkBox_ViewOnlyFailedPackets;
        private System.Windows.Forms.TabPage tabPage_RSMP;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SendWatchdog;
        private System.Windows.Forms.Button button_TestPackage_1_Browse;
        private System.Windows.Forms.Button button_SendTestPackage_1;
        private System.Windows.Forms.Button button_SendTestPackage_2;
        private System.Windows.Forms.Button button_TestPackage_2_Browse;
        private System.Windows.Forms.TextBox textBox_TestPackage_1;
        private System.Windows.Forms.TextBox textBox_TestPackage_2;
        private System.Windows.Forms.SplitContainer splitContainer_RSMP;
        private System.Windows.Forms.GroupBox groupBox_ProtocolSettings;
        public System.Windows.Forms.DataGridView dataGridView_Behaviour;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Common;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RSMP_3_1_5;
        private System.Windows.Forms.Button button_ResetRSMPSettingToDefault;
        private System.Windows.Forms.GroupBox groupBox_Statistics;
        private System.Windows.Forms.ColumnHeader columnHeader_Statistics_Desription;
        private System.Windows.Forms.ColumnHeader columnHeader_Statistics_Value;
        private System.Windows.Forms.ColumnHeader columnHeader_Statistics_Unit;
        private ListViewDoubleBuffered listView_Alarms;
        private ListViewDoubleBuffered listView_AlarmEvents;
        private ListViewDoubleBuffered listView_AggregatedStatus_StatusBits;
        private ListViewDoubleBuffered listView_AggregatedStatusEvents;
        private ListViewDoubleBuffered listView_Status;
        private ListViewDoubleBuffered listView_StatusEvents;
        private ListViewDoubleBuffered listView_Commands;
        private ListViewDoubleBuffered listView_CommandEvents;
        public ListViewDoubleBuffered listView_SysLog;
        public ListViewDoubleBuffered listView_Statistics;
        private System.Windows.Forms.Button button_ClearStatistics;
        private System.Windows.Forms.Label label_SXL_FilePath;
        public System.Windows.Forms.TextBox textBox_SignalExchangeListPath;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View_AlwaysShowGroupHeaders;
        private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_Direction;
        private System.Windows.Forms.ColumnHeader columnHeader_AlarmEvent_RoadSideObject;
        private System.Windows.Forms.ColumnHeader columnHeader_Alarms_AlarmEvents;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View_Clear_AlarmEvents;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View_Clear_AggregatedStatusEvents;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View_Clear_StatusEvents;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_View_Clear_CommandEvents;
        private System.Windows.Forms.ToolStripTextBox ToolStripMenuItem_ConnectionStatus;
        private System.Windows.Forms.GroupBox groupBox_Encryption;
        private System.Windows.Forms.Label label_Encryption_CertificateFile;
        private System.Windows.Forms.Button button_EncryptionFile_Browse;
        private System.Windows.Forms.GroupBox groupBox_EncryptionProtocols;
        private System.Windows.Forms.CheckBox button_Encryption_CheckRevocation;
        private System.Windows.Forms.CheckBox checkBox_Encryption_RequireClientCertificate;
        private System.Windows.Forms.CheckBox button_Encryption_IgnoreCertErrors;
        private System.Windows.Forms.TextBox textBox_EncryptionFile;
        private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_TLS13;
        private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_TLS12;
        private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_TLS11;
        private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_TLS10;
        private System.Windows.Forms.CheckBox checkBox_Encryption_Protocol_Default;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Alarm_RequestCurrentState;
        public System.Windows.Forms.Button button_AggregatedStatus_Request;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StatusSubscribe_UpdateOnInterval;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ColumnHeader columnHeader_Status_UpdateOnChange;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_LoadObjects;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_LoadObjects_CSV;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File_LoadObjects_YAML;
        public System.Windows.Forms.CheckBox checkBox_AutomaticallyLoadObjects;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_File_LoadObjects_Delimiter;
    }
}

