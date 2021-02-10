namespace nsRSMPGS
{
    partial class RSMPGS_Debug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RSMPGS_Debug));
            this.menuStrip_Debug = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_Debug = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Raw = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Delimiter_0 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_PacketTypes_All = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Delimiter_1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_PacketTypes_Version = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Alarm = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_AggStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Status = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Command = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Watchdog = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Delimiter_2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_PacketTypes_PacketAck = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_PacketTypes_Delimiter_3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_PacketTypes_Unknown = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ShowLastRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Delimiter_1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_CopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Delimiter_2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_SaveContinousToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Delimiter_3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_CloseForm = new System.Windows.Forms.ToolStripMenuItem();
            this.listView_Debug = new nsRSMPGS.ListViewDoubleBuffered();
            this.columnHeader_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Direction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Text = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveFileDialog_Debug = new System.Windows.Forms.SaveFileDialog();
            this.timer_System = new System.Windows.Forms.Timer(this.components);
            this.menuStrip_Debug.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_Debug
            // 
            this.menuStrip_Debug.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_Debug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Debug});
            this.menuStrip_Debug.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Debug.Name = "menuStrip_Debug";
            this.menuStrip_Debug.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip_Debug.Size = new System.Drawing.Size(823, 28);
            this.menuStrip_Debug.TabIndex = 0;
            this.menuStrip_Debug.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_Debug
            // 
            this.ToolStripMenuItem_Debug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_PacketTypes,
            this.ToolStripMenuItem_ShowLastRow,
            this.toolStripMenuItem_Delimiter_1,
            this.toolStripMenuItem_CopyToClipboard,
            this.toolStripMenuItem_Clear,
            this.toolStripMenuItem_Delimiter_2,
            this.toolStripMenuItem_SaveContinousToFile,
            this.toolStripMenuItem_Delimiter_3,
            this.ToolStripMenuItem_CloseForm});
            this.ToolStripMenuItem_Debug.Name = "ToolStripMenuItem_Debug";
            this.ToolStripMenuItem_Debug.Size = new System.Drawing.Size(66, 24);
            this.ToolStripMenuItem_Debug.Text = "&Debug";
            this.ToolStripMenuItem_Debug.DropDownOpening += new System.EventHandler(this.ToolStripMenuItem_Debug_DropDownOpening);
            // 
            // ToolStripMenuItem_PacketTypes
            // 
            this.ToolStripMenuItem_PacketTypes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_PacketTypes_Raw,
            this.ToolStripMenuItem_PacketTypes_Delimiter_0,
            this.ToolStripMenuItem_PacketTypes_All,
            this.ToolStripMenuItem_PacketTypes_Delimiter_1,
            this.ToolStripMenuItem_PacketTypes_Version,
            this.ToolStripMenuItem_PacketTypes_Alarm,
            this.ToolStripMenuItem_PacketTypes_AggStatus,
            this.ToolStripMenuItem_PacketTypes_Status,
            this.ToolStripMenuItem_PacketTypes_Command,
            this.ToolStripMenuItem_PacketTypes_Watchdog,
            this.ToolStripMenuItem_PacketTypes_Delimiter_2,
            this.ToolStripMenuItem_PacketTypes_PacketAck,
            this.ToolStripMenuItem_PacketTypes_Delimiter_3,
            this.ToolStripMenuItem_PacketTypes_Unknown});
            this.ToolStripMenuItem_PacketTypes.Name = "ToolStripMenuItem_PacketTypes";
            this.ToolStripMenuItem_PacketTypes.Size = new System.Drawing.Size(292, 26);
            this.ToolStripMenuItem_PacketTypes.Text = "Select &what to debug...";
            this.ToolStripMenuItem_PacketTypes.DropDownOpening += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_DropDownOpening);
            // 
            // ToolStripMenuItem_PacketTypes_Raw
            // 
            this.ToolStripMenuItem_PacketTypes_Raw.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_Raw.Name = "ToolStripMenuItem_PacketTypes_Raw";
            this.ToolStripMenuItem_PacketTypes_Raw.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_Raw.Text = "Show all traffic in &raw format";
            this.ToolStripMenuItem_PacketTypes_Raw.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_Delimiter_0
            // 
            this.ToolStripMenuItem_PacketTypes_Delimiter_0.Name = "ToolStripMenuItem_PacketTypes_Delimiter_0";
            this.ToolStripMenuItem_PacketTypes_Delimiter_0.Size = new System.Drawing.Size(273, 6);
            // 
            // ToolStripMenuItem_PacketTypes_All
            // 
            this.ToolStripMenuItem_PacketTypes_All.Checked = true;
            this.ToolStripMenuItem_PacketTypes_All.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_All.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_All.Name = "ToolStripMenuItem_PacketTypes_All";
            this.ToolStripMenuItem_PacketTypes_All.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_All.Text = "&All packet types";
            this.ToolStripMenuItem_PacketTypes_All.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_Delimiter_1
            // 
            this.ToolStripMenuItem_PacketTypes_Delimiter_1.Name = "ToolStripMenuItem_PacketTypes_Delimiter_1";
            this.ToolStripMenuItem_PacketTypes_Delimiter_1.Size = new System.Drawing.Size(273, 6);
            // 
            // ToolStripMenuItem_PacketTypes_Version
            // 
            this.ToolStripMenuItem_PacketTypes_Version.Checked = true;
            this.ToolStripMenuItem_PacketTypes_Version.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_Version.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_Version.Name = "ToolStripMenuItem_PacketTypes_Version";
            this.ToolStripMenuItem_PacketTypes_Version.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_Version.Text = "&Version packets";
            this.ToolStripMenuItem_PacketTypes_Version.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_Alarm
            // 
            this.ToolStripMenuItem_PacketTypes_Alarm.Checked = true;
            this.ToolStripMenuItem_PacketTypes_Alarm.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_Alarm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_Alarm.Name = "ToolStripMenuItem_PacketTypes_Alarm";
            this.ToolStripMenuItem_PacketTypes_Alarm.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_Alarm.Text = "Alar&m packets";
            this.ToolStripMenuItem_PacketTypes_Alarm.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_AggStatus
            // 
            this.ToolStripMenuItem_PacketTypes_AggStatus.Checked = true;
            this.ToolStripMenuItem_PacketTypes_AggStatus.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_AggStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_AggStatus.Name = "ToolStripMenuItem_PacketTypes_AggStatus";
            this.ToolStripMenuItem_PacketTypes_AggStatus.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_AggStatus.Text = "A&ggregated status packets";
            this.ToolStripMenuItem_PacketTypes_AggStatus.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_Status
            // 
            this.ToolStripMenuItem_PacketTypes_Status.Checked = true;
            this.ToolStripMenuItem_PacketTypes_Status.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_Status.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_Status.Name = "ToolStripMenuItem_PacketTypes_Status";
            this.ToolStripMenuItem_PacketTypes_Status.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_Status.Text = "&Status packets";
            this.ToolStripMenuItem_PacketTypes_Status.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_Command
            // 
            this.ToolStripMenuItem_PacketTypes_Command.Checked = true;
            this.ToolStripMenuItem_PacketTypes_Command.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_Command.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_Command.Name = "ToolStripMenuItem_PacketTypes_Command";
            this.ToolStripMenuItem_PacketTypes_Command.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_Command.Text = "&Command packets";
            this.ToolStripMenuItem_PacketTypes_Command.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_Watchdog
            // 
            this.ToolStripMenuItem_PacketTypes_Watchdog.Checked = true;
            this.ToolStripMenuItem_PacketTypes_Watchdog.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_Watchdog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_Watchdog.Name = "ToolStripMenuItem_PacketTypes_Watchdog";
            this.ToolStripMenuItem_PacketTypes_Watchdog.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_Watchdog.Text = "Watchdog packets";
            this.ToolStripMenuItem_PacketTypes_Watchdog.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_Delimiter_2
            // 
            this.ToolStripMenuItem_PacketTypes_Delimiter_2.Name = "ToolStripMenuItem_PacketTypes_Delimiter_2";
            this.ToolStripMenuItem_PacketTypes_Delimiter_2.Size = new System.Drawing.Size(273, 6);
            // 
            // ToolStripMenuItem_PacketTypes_PacketAck
            // 
            this.ToolStripMenuItem_PacketTypes_PacketAck.Checked = true;
            this.ToolStripMenuItem_PacketTypes_PacketAck.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_PacketAck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_PacketAck.Name = "ToolStripMenuItem_PacketTypes_PacketAck";
            this.ToolStripMenuItem_PacketTypes_PacketAck.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_PacketAck.Text = "Packet acknowledge packets";
            this.ToolStripMenuItem_PacketTypes_PacketAck.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_PacketTypes_Delimiter_3
            // 
            this.ToolStripMenuItem_PacketTypes_Delimiter_3.Name = "ToolStripMenuItem_PacketTypes_Delimiter_3";
            this.ToolStripMenuItem_PacketTypes_Delimiter_3.Size = new System.Drawing.Size(273, 6);
            // 
            // ToolStripMenuItem_PacketTypes_Unknown
            // 
            this.ToolStripMenuItem_PacketTypes_Unknown.Checked = true;
            this.ToolStripMenuItem_PacketTypes_Unknown.CheckOnClick = true;
            this.ToolStripMenuItem_PacketTypes_Unknown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_PacketTypes_Unknown.Name = "ToolStripMenuItem_PacketTypes_Unknown";
            this.ToolStripMenuItem_PacketTypes_Unknown.Size = new System.Drawing.Size(276, 26);
            this.ToolStripMenuItem_PacketTypes_Unknown.Text = "Unknown packets";
            this.ToolStripMenuItem_PacketTypes_Unknown.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_PacketTypes_CheckChanged);
            // 
            // ToolStripMenuItem_ShowLastRow
            // 
            this.ToolStripMenuItem_ShowLastRow.Checked = true;
            this.ToolStripMenuItem_ShowLastRow.CheckOnClick = true;
            this.ToolStripMenuItem_ShowLastRow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItem_ShowLastRow.Name = "ToolStripMenuItem_ShowLastRow";
            this.ToolStripMenuItem_ShowLastRow.Size = new System.Drawing.Size(292, 26);
            this.ToolStripMenuItem_ShowLastRow.Text = "Always show last row";
            // 
            // toolStripMenuItem_Delimiter_1
            // 
            this.toolStripMenuItem_Delimiter_1.Name = "toolStripMenuItem_Delimiter_1";
            this.toolStripMenuItem_Delimiter_1.Size = new System.Drawing.Size(289, 6);
            // 
            // toolStripMenuItem_CopyToClipboard
            // 
            this.toolStripMenuItem_CopyToClipboard.Name = "toolStripMenuItem_CopyToClipboard";
            this.toolStripMenuItem_CopyToClipboard.Size = new System.Drawing.Size(292, 26);
            this.toolStripMenuItem_CopyToClipboard.Text = "Copy selection to Cli&pboard";
            this.toolStripMenuItem_CopyToClipboard.Click += new System.EventHandler(this.toolStripMenuItem_CopyToClipboard_Click);
            // 
            // toolStripMenuItem_Clear
            // 
            this.toolStripMenuItem_Clear.Name = "toolStripMenuItem_Clear";
            this.toolStripMenuItem_Clear.Size = new System.Drawing.Size(292, 26);
            this.toolStripMenuItem_Clear.Text = "&Clear debug list";
            this.toolStripMenuItem_Clear.Click += new System.EventHandler(this.toolStripMenuItem_Clear_Click);
            // 
            // toolStripMenuItem_Delimiter_2
            // 
            this.toolStripMenuItem_Delimiter_2.Name = "toolStripMenuItem_Delimiter_2";
            this.toolStripMenuItem_Delimiter_2.Size = new System.Drawing.Size(289, 6);
            // 
            // toolStripMenuItem_SaveContinousToFile
            // 
            this.toolStripMenuItem_SaveContinousToFile.Name = "toolStripMenuItem_SaveContinousToFile";
            this.toolStripMenuItem_SaveContinousToFile.Size = new System.Drawing.Size(292, 26);
            this.toolStripMenuItem_SaveContinousToFile.Text = "&Save continous to file (record)...";
            this.toolStripMenuItem_SaveContinousToFile.Click += new System.EventHandler(this.toolStripMenuItem_SaveContinousToFile_Click);
            // 
            // toolStripMenuItem_Delimiter_3
            // 
            this.toolStripMenuItem_Delimiter_3.Name = "toolStripMenuItem_Delimiter_3";
            this.toolStripMenuItem_Delimiter_3.Size = new System.Drawing.Size(289, 6);
            // 
            // ToolStripMenuItem_CloseForm
            // 
            this.ToolStripMenuItem_CloseForm.Name = "ToolStripMenuItem_CloseForm";
            this.ToolStripMenuItem_CloseForm.Size = new System.Drawing.Size(292, 26);
            this.ToolStripMenuItem_CloseForm.Text = "&Close debug form";
            this.ToolStripMenuItem_CloseForm.Click += new System.EventHandler(this.ToolStripMenuItem_CloseForm_Click);
            // 
            // listView_Debug
            // 
            this.listView_Debug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_Debug.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Time,
            this.columnHeader_Direction,
            this.columnHeader_Text});
            this.listView_Debug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Debug.FullRowSelect = true;
            this.listView_Debug.Location = new System.Drawing.Point(0, 28);
            this.listView_Debug.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView_Debug.Name = "listView_Debug";
            this.listView_Debug.Size = new System.Drawing.Size(823, 570);
            this.listView_Debug.TabIndex = 1;
            this.listView_Debug.UseCompatibleStateImageBehavior = false;
            this.listView_Debug.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_Time
            // 
            this.columnHeader_Time.Text = "Time";
            this.columnHeader_Time.Width = 157;
            // 
            // columnHeader_Direction
            // 
            this.columnHeader_Direction.Text = "Direction";
            this.columnHeader_Direction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader_Text
            // 
            this.columnHeader_Text.Text = "Data";
            this.columnHeader_Text.Width = 447;
            // 
            // saveFileDialog_Debug
            // 
            this.saveFileDialog_Debug.Filter = "Debug (text) files|*.txt|All files|*.*";
            this.saveFileDialog_Debug.OverwritePrompt = false;
            this.saveFileDialog_Debug.RestoreDirectory = true;
            this.saveFileDialog_Debug.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_Debug_FileOk);
            // 
            // timer_System
            // 
            this.timer_System.Tick += new System.EventHandler(this.timer_System_Tick);
            // 
            // RSMPGS_Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 598);
            this.Controls.Add(this.listView_Debug);
            this.Controls.Add(this.menuStrip_Debug);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_Debug;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RSMPGS_Debug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RSMPGS_Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RSMPGS_Debug_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RSMPGS_Debug_FormClosed);
            this.Load += new System.EventHandler(this.RSMPGS_Debug_Load);
            this.menuStrip_Debug.ResumeLayout(false);
            this.menuStrip_Debug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_Debug;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Debug;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_All;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_PacketTypes_Delimiter_1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShowLastRow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_Delimiter_1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_CloseForm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SaveContinousToFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_Delimiter_2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Clear;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CopyToClipboard;
        private ListViewDoubleBuffered listView_Debug;
        private System.Windows.Forms.ColumnHeader columnHeader_Time;
        private System.Windows.Forms.ColumnHeader columnHeader_Direction;
        private System.Windows.Forms.ColumnHeader columnHeader_Text;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_Alarm;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_Raw;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_PacketTypes_Delimiter_0;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_Delimiter_3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_Debug;
        private System.Windows.Forms.Timer timer_System;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_Status;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_Command;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_PacketTypes_Delimiter_2;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_Unknown;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_Watchdog;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_PacketAck;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_AggStatus;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_PacketTypes_Delimiter_3;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_PacketTypes_Version;
    }
}