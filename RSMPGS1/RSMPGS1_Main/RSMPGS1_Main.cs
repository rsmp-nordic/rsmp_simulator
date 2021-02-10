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

namespace nsRSMPGS
{

    [System.ComponentModel.DesignerCategory("form")]

    public partial class RSMPGS_Main : Form
    {

        static int LastMouseX = 0;
        static int LastMouseY = 0;

        static bool bProcessDataWasLoadedAtStartup = false;

        //
        // Load main form
        //
        private void Main_Load()
        {

            checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Checked = cPrivateProfile.GetIniFileInt("Main", "AggregatedStatus_SendAutomaticallyWhenChanged", 0) != 0;

            checkBox_AutomaticallySaveProcessData.Checked = cPrivateProfile.GetIniFileInt("Main", "AutomaticallySaveProcessData", 0) != 0;
            checkbox_AutomaticallyLoadProcessData.Checked = cPrivateProfile.GetIniFileInt("Main", "AutomaticallyLoadProcessData", 0) != 0;
            checkBox_ProcessImageLoad_AlarmStatus.Checked = cPrivateProfile.GetIniFileInt("Main", "ProcessImageLoad_AlarmStatus", 0) != 0;
            checkBox_ProcessImageLoad_AggregatedStatus.Checked = cPrivateProfile.GetIniFileInt("Main", "ProcessImageLoad_AggregatedStatus", 0) != 0;
            checkBox_ProcessImageLoad_Status.Checked = cPrivateProfile.GetIniFileInt("Main", "ProcessImageLoad_Status", 0) != 0;
            checkBox_ShowMax10BufferedMessagesInSysLog.Checked = cPrivateProfile.GetIniFileInt("Main", "ShowMax10BufferedMessagesInSysLog", 1) != 0;

            saveFileDialog_ProcessImage.Title = "Save Process Data as";
            openFileDialog_ProcessImage.Title = "Load Process Data from";

            saveFileDialog_ProcessImage.InitialDirectory = cPrivateProfile.GetIniFileString("Main", "ProcessImageLoadSave_DefaultPath", sCSVObjectFilesPath);
            openFileDialog_ProcessImage.InitialDirectory = saveFileDialog_ProcessImage.InitialDirectory;

            RSMPGS.ConnectionType = cPrivateProfile.GetIniFileInt("RSMP", "ConnectionType", cTcpSocket.ConnectionMethod_SocketClient);

            ToolStripMenuItem_ConnectAutomatically.Enabled = (RSMPGS.ConnectionType == cTcpSocket.ConnectionMethod_SocketClient) ? true : false;
            ToolStripMenuItem_ConnectNow.Enabled = (RSMPGS.ConnectionType == cTcpSocket.ConnectionMethod_SocketClient) ? true : false;

            checkBox_Encryption_AuthenticateAsClientUsingCertificate.Checked = RSMPGS.EncryptionSettings.AuthenticateAsClientUsingCertificate;
            textBox_EncryptionFile.Text = RSMPGS.EncryptionSettings.ClientCertificateFile;
            textBox_Encryption_ServerName.Text = RSMPGS.EncryptionSettings.ServerName;

            checkBox_Encryption_AuthenticateClient_CheckedChanged();

            comboBox_BufferedMessages_CreateRandom_Type.SelectedIndex = 0;

            RSMPGS.RSMPConnection = new cTcpSocket(RSMPGS.ConnectionType,
              cPrivateProfile.GetIniFileString("RSMP", "IPAddress", ""),
            cPrivateProfile.GetIniFileInt("RSMP", "ReconnectInterval", 10000),
            ToolStripMenuItem_ConnectAutomatically.Checked,
            cPrivateProfile.GetIniFileInt("RSMP", "PortNumber", 0),
            cPrivateProfile.GetIniFileInt("RSMP", "PacketTimeout", 5000),
            cTcpHelper.WrapMethod_FormFeed);

        }

        //
        // View all forms
        //
        private void RSMPGS_Main_Shown(object sender, EventArgs e)
        {

            if (bLoadFailed)
            {
                return;
            }

            foreach (RSMPGS_Debug DebugForm in RSMPGS.DebugForms)
            {
                DebugForm.Show();
                DebugForm.BringToFront();
            }
        }

        //
        // Closing main form
        //
        private void Main_Closing()
        {

            cPrivateProfile.WriteIniFileInt("Main", "AggregatedStatus_SendAutomaticallyWhenChanged", checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Checked == true ? 1 : 0);

            cPrivateProfile.WriteIniFileInt("Main", "AutomaticallySaveProcessData", checkBox_AutomaticallySaveProcessData.Checked == true ? 1 : 0);
            cPrivateProfile.WriteIniFileInt("Main", "AutomaticallyLoadProcessData", checkbox_AutomaticallyLoadProcessData.Checked == true ? 1 : 0);
            cPrivateProfile.WriteIniFileInt("Main", "ProcessImageLoad_AlarmStatus", checkBox_ProcessImageLoad_AlarmStatus.Checked == true ? 1 : 0);
            cPrivateProfile.WriteIniFileInt("Main", "ProcessImageLoad_AggregatedStatus", checkBox_ProcessImageLoad_AggregatedStatus.Checked == true ? 1 : 0);
            cPrivateProfile.WriteIniFileInt("Main", "ProcessImageLoad_Status", checkBox_ProcessImageLoad_Status.Checked == true ? 1 : 0);
            cPrivateProfile.WriteIniFileInt("Main", "ShowMax10BufferedMessagesInSysLog", checkBox_ShowMax10BufferedMessagesInSysLog.Checked == true ? 1 : 0);

            if (checkBox_AutomaticallySaveProcessData.Checked)
            {
                if (bProcessDataWasLoadedAtStartup == false)
                {
                    if (MessageBox.Show("Process data was not loaded at startup but you have selected to save process data when exiting RSMPGS1\r\n\r\nThis will owerwrite the old process image .dat file. Is this ok?", "Save Process Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        RSMPGS.ProcessImage.SaveProcessImageValues(sProcessImageDefaultName);
                    }
                }
                else
                {
                    RSMPGS.ProcessImage.SaveProcessImageValues(sProcessImageDefaultName);
                }
            }
            else
            {

            }

        }

        // Tooltip on/off
        private void checkBox_ShowTooltip_CheckedChanged(object sender, EventArgs e)
        {
            treeView_SitesAndObjects.ShowNodeToolTips = checkBox_ShowTooltip.Checked;
        }

        // Create new debug form
        private void ToolStripMenuItem_File_Debug_CreateNew_Click(object sender, EventArgs e)
        {
            RSMPGS_Debug DebugForm = new RSMPGS_Debug();
            //      DebugForm.MainForm = this;
            DebugForm.CalcNewCaption();
            RSMPGS.DebugForms.Add(DebugForm);
            DebugForm.Show();
        }

        // Tile debug forms
        private void ToolStripMenuItem_File_Debug_Tile_Click(object sender, EventArgs e)
        {
            int iX = this.Left + this.Width;
            int iY = this.Top;

            if (iX > Screen.PrimaryScreen.Bounds.Width) iX = 0;
            if (iY > Screen.PrimaryScreen.Bounds.Height) iY = 0;

            foreach (RSMPGS_Debug DebugForm in RSMPGS.DebugForms)
            {
                DebugForm.WindowState = FormWindowState.Normal;
                DebugForm.Left = iX;
                DebugForm.Top = iY;
                DebugForm.Width = 500;
                DebugForm.Height = 500;
                DebugForm.BringToFront();
                iX += 50;
                iY += 50;
            }
        }

        // Close all debug forms
        private void ToolStripMenuItem_File_Debug_CloseAll_Click(object sender, EventArgs e)
        {
            while (RSMPGS.DebugForms.Count() > 0)
            {
                RSMPGS.DebugForms[0].Close();
            }
        }

        //
        // Delegated methods to prevent problems, all calls to ProcessImage and JSon is delegated
        // and called from socket thread using these methods, hence we don't need criticalsections
        // everywhere
        //
        public void AddSysLogListItemMethod(cSysLogAndDebug.Severity severity, string sDateTime, string sLogText)
        {

            ListViewItem lvItem = new ListViewItem();

            lvItem.ImageIndex = (int)severity;
            lvItem.SubItems.Add(sDateTime);
            lvItem.SubItems.Add(sLogText);

            lock (RSMPGS.SysLogItems)
            {
                RSMPGS.SysLogItems.Add(lvItem);
            }

        }

        public void DecodeJSonPacketMethod(string sJSon)
        {
            RSMPGS.JSon.DecodeAndParseJSonPacket(sJSon);
        }

        public void SocketWasClosedMethod()
        {
            ToolStripMenuItem_SendWatchdog.Enabled = false;
            button_BufferedMessages_CreateRandom.Enabled = true;
            RSMPGS.JSon.SocketWasClosed();
        }

        public void SocketWasConnectedMethod()
        {
            ToolStripMenuItem_SendWatchdog.Enabled = true;
            button_BufferedMessages_CreateRandom.Enabled = false;
            RSMPGS.JSon.SocketWasConnected();
        }


        private void ToolStripMenuItem_ProcessImage_RandomUpdates_Click(object sender, EventArgs e)
        {
            Random Rnd = new Random();

            List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();
            foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
            {
                sS.Clear();
                // Delete subscription if it already exists
                foreach (cSubscription Subscription in RoadSideObject.Subscriptions)
                {
                    switch (Subscription.StatusReturnValue.Value.GetValueType().ToLower())
                    {
                        case "boolean":
                            Subscription.StatusReturnValue.Value.SetValue(Rnd.Next(0, 2) >= 1 ? "true" : "false");
                            break;
                        case "string":
                            Subscription.StatusReturnValue.Value.SetValue(Rnd.Next(0, 1).ToString());
                            break;
                        case "real":
                            Subscription.StatusReturnValue.Value.SetValue((Rnd.Next(-10000, 10000) / 10).ToString());
                            break;
                        default:
                            Subscription.StatusReturnValue.Value.SetValue(Rnd.Next(-1000, 1000).ToString());
                            break;
                    }
                    if (Subscription.SubscribeStatus == cSubscription.SubscribeMethod.OnChange || Subscription.SubscribeStatus == cSubscription.SubscribeMethod.IntervalAndOnChange)
                    {
                        RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                        s.sCI = Subscription.StatusObject.sStatusCodeId;
                        s.n = Subscription.StatusReturnValue.sName;
                        RSMPGS.ProcessImage.UpdateStatusValue(ref s, Subscription.StatusReturnValue.Value.GetValueType(), Subscription.StatusReturnValue.Value.GetValue());
                        sS.Add(s);
                    }
                }
                if (sS.Count > 0)
                {
                    RSMPGS.JSon.CreateAndSendStatusUpdateMessage(RoadSideObject, sS);
                }
                // Update ListView if this RoadSide object is selected
                if (treeView_SitesAndObjects.SelectedNode != null)
                {
                    if (treeView_SitesAndObjects.SelectedNode.Tag != null)
                    {
                        if (treeView_SitesAndObjects.SelectedNode.Parent == null)
                        {
                            cSiteIdObject SiteIdObject = (cSiteIdObject)treeView_SitesAndObjects.SelectedNode.Tag;
                            UpdateStatusListView(SiteIdObject, null);
                        }
                        else
                        {
                            if (RoadSideObject == (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag)
                            {
                                UpdateStatusListView(null, RoadSideObject);
                            }
                        }
                    }
                }

            }
        }

        private void ToolStripMenuItem_ProcessImage_Reset_Click(object sender, EventArgs e)
        {
            List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();
            //			if (System.Windows.Forms.MessageBox.Show("This will reset all status' to default (unknown/null).\r\nAre you sure?", "RSMPGS1", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //			{

            RSMPGS.ProcessImage.BufferedMessages.Clear();
            ListView_BufferedMessages.Items.Clear();
            textBox_BufferedMessages.Text = RSMPGS.ProcessImage.BufferedMessages.Count.ToString();

            foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
            {
                foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
                {
                    AlarmObject.AlarmEvents.Clear();
                    AlarmObject.bAcknowledged = false;
                    AlarmObject.bActive = false;
                    AlarmObject.bSuspended = false;
                    AlarmObject.AlarmCount = 0;
                }
                if (RoadSideObject.bIsComponentGroup)
                {
                    RoadSideObject.sFunctionalState = "";
                    RoadSideObject.sFunctionalPosition = "";
                    for (int iIndex = 0; iIndex < RoadSideObject.bBitStatus.GetLength(0); iIndex++)
                    {
                        RoadSideObject.bBitStatus[iIndex] = false;
                    }
                }
                foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
                {
                    foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
                    {
                        StatusReturnValue.Value.SetInitialUnknownValue();
                    }
                }
                /*
                        sS.Clear();
                        // Delete subscription if it already exists
                        foreach (cSubscription Subscription in RoadSideObject.Subscriptions)
                        {
                            if (Subscription.SubscribeStatus == cSubscription.Subscribe_OnChange)
                            {
                                RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                                s.sCI = Subscription.StatusObject.sStatusCodeId;
                                s.n = Subscription.StatusReturnValue.sName;
                                RSMPGS.ProcessImage.UpdateStatusValue(ref s, Subscription.StatusReturnValue.sStatus);
                                sS.Add(s);
                            }
                        }
                        if (sS.Count > 0)
                        {
                            RSMPGS_Main.JSon.CreateAndSendStatusUpdateMessage(RoadSideObject, sS);
                        }
                         */
                // Update ListView if this RoadSide object is selected
                if (treeView_SitesAndObjects.SelectedNode != null)
                {
                    if (treeView_SitesAndObjects.SelectedNode.Tag != null)
                    {
                        if (treeView_SitesAndObjects.SelectedNode.Parent != null)
                        {
                            if (RoadSideObject == (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag)
                            {
                                UpdateStatusListView(null, RoadSideObject);
                                UpdateAlarmListView(null, RoadSideObject);
                            }
                        }
                    }
                }
            }

            if (treeView_SitesAndObjects.SelectedNode != null)
            {
                if (treeView_SitesAndObjects.SelectedNode.Parent == null)
                {
                    cSiteIdObject SiteIdObject = (cSiteIdObject)treeView_SitesAndObjects.SelectedNode.Tag;
                    UpdateStatusListView(SiteIdObject, null);
                    UpdateAlarmListView(SiteIdObject, null);
                }
            }
        }

        private void ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues_Click(object sender, EventArgs e)
        {

            Random Rnd = new Random();

            List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();

            foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
            {
                foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
                {
                    foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
                    {
                        switch (StatusReturnValue.Value.GetValueType().ToLower())
                        {
                            case "boolean":
                                StatusReturnValue.Value.SetValue(Rnd.Next(0, 2) >= 1 ? "true" : "false");
                                break;
                            case "string":
                                StatusReturnValue.Value.SetValue(Rnd.Next(0, 1).ToString());
                                break;
                            case "real":
                                StatusReturnValue.Value.SetValue((Rnd.Next(-10000, 10000) / 10).ToString());
                                break;
                            default:
                                StatusReturnValue.Value.SetValue(Rnd.Next(-1000, 1000).ToString());
                                break;
                        }
                        //StatusReturnValue.sStatus = "?";
                    }
                }
                // Update ListView if this RoadSide object is selected
                if (treeView_SitesAndObjects.SelectedNode != null)
                {
                    if (treeView_SitesAndObjects.SelectedNode.Tag != null)
                    {
                        if (treeView_SitesAndObjects.SelectedNode.Parent == null)
                        {
                            cSiteIdObject SiteIdObject = (cSiteIdObject)treeView_SitesAndObjects.SelectedNode.Tag;
                            UpdateStatusListView(SiteIdObject, null);
                        }
                        else
                        {
                            if (RoadSideObject == (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag)
                            {
                                UpdateStatusListView(null, RoadSideObject);
                            }
                        }
                    }
                }
            }
        }

        private void ToolStripMenuItem_File_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripMenuItem_Connection_DropDownOpening(object sender, EventArgs e)
        {

            ToolStripMenuItem_ConnectNow.Enabled = (RSMPGS.RSMPConnection.ConnectionStatus() != cTcpSocket.ConnectionStatus_Connected && RSMPGS.ConnectionType == cTcpSocket.ConnectionMethod_SocketClient);
            ToolStripMenuItem_Disconnect.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected;

            ToolStripMenuItem_SendSomeRandomCrap.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected;
            ToolStripMenuItem_SendWatchdog.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected;

        }

        private void ToolStripMenuItem_ConnectNow_Click(object sender, EventArgs e)
        {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Connecting to server...");
            RSMPGS.RSMPConnection.Connect();
        }

        private void ToolStripMenuItem_Disconnect_Click(object sender, EventArgs e)
        {
            RSMPGS.RSMPConnection.Disconnect();
        }

        private void ToolStripMenuItem_ConnectAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            // May not be created when we resore status from INI-file
            if (RSMPGS.RSMPConnection != null)
            {
                RSMPGS.RSMPConnection.SetConnectBehaviour(ToolStripMenuItem_ConnectAutomatically.Checked);
            }
        }

        private void ToolStripMenuItem_SendSomeRandomCrap_Click(object sender, EventArgs e)
        {

            Random Rnd = new Random();
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");

            byte[] RandomArray = new byte[Rnd.Next(1, 2048)];
            Rnd.NextBytes(RandomArray);
            RSMPGS.JSon.SendJSonPacket("crap", null, encoding.GetString(RandomArray), false);
        }

        private void ToolStripMenuItem_DisableNagleAlgorithm_Click(object sender, EventArgs e)
        {
            RSMPGS.RSMPConnection.NagleAlgorithm(ToolStripMenuItem_DisableNagleAlgorithm.Checked);
        }

        private void ToolStripMenuItem_ProcessImage_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem_ProcessImage_RandomUpdates.Enabled = ToolStripMenuItem_ProcessImage_Reset.Enabled != true;
            ToolStripMenuItem_ProcessImage_Reset.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() != cTcpSocket.ConnectionStatus_Connected;
            ToolStripMenuItem_ProcessImage_RandomUpdateAllStatusValues.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() != cTcpSocket.ConnectionStatus_Connected;
            try
            {
                ToolStripMenuItem_ProcessImage_Clear.Enabled = File.Exists(sProcessImageDefaultName);
            }
            catch
            {
                ToolStripMenuItem_ProcessImage_Clear.Enabled = false;
            }
        }

        private void checkBox_AlwaysUseSXLFromFile_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AlwaysUseSXLFromFile.Checked == true && RSMPGS.ProcessImage.sSULRevision.Length > 0)
            {
                if (textBox_SignalExchangeListVersion.Text.Equals(RSMPGS.ProcessImage.sSULRevision, StringComparison.OrdinalIgnoreCase) == false)
                {
                    RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Signal Exchange List revision number was set to the file version '{0}'", RSMPGS.ProcessImage.sSULRevision, textBox_SignalExchangeListVersion.Text);
                    textBox_SignalExchangeListVersion.Text = RSMPGS.ProcessImage.sSULRevision;
                }
                textBox_SignalExchangeListVersion.Enabled = false;
            }
            else
            {
                textBox_SignalExchangeListVersion.Enabled = true;
            }
        }

        private void ToolStripMenuItem_ProcessData_LoadFromFile_Click(object sender, EventArgs e)
        {

            if (openFileDialog_ProcessImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    /*
                              StreamReader swTestPackageFile = new StreamReader((System.IO.Stream)File.OpenRead(openFileDialog_TestPackage.FileName));
                              textBox_TestPackage_2.Text = swTestPackageFile.ReadToEnd();
                              swTestPackageFile.Close();
                               */
                    RSMPGS.ProcessImage.LoadProcessImageValues(this, openFileDialog_ProcessImage.FileName);
                    cPrivateProfile.WriteIniFileString("Main", "ProcessImageLoadSave_DefaultPath", Path.GetFullPath(openFileDialog_ProcessImage.FileName));
                }
                catch
                {
                }
            }

        }

        private void ToolStripMenuItem_ProcessData_SaveToFile_Click(object sender, EventArgs e)
        {

            //  string FileName = cPrivateProfile.ObjectFilesPath() + "\\" + "ProcessImage.dat";


            if (saveFileDialog_ProcessImage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    RSMPGS.ProcessImage.SaveProcessImageValues(saveFileDialog_ProcessImage.FileName);
                    /*
                              textBox_TestPackage_2.Clear();
                              StreamReader swTestPackageFile = new StreamReader((System.IO.Stream)File.OpenRead(openFileDialog_TestPackage.FileName));
                              textBox_TestPackage_2.Text = swTestPackageFile.ReadToEnd();
                              swTestPackageFile.Close();
                               */
                    cPrivateProfile.WriteIniFileString("Main", "ProcessImageLoadSave_DefaultPath", Path.GetFullPath(saveFileDialog_ProcessImage.FileName));
                }
                catch
                {
                }
            }

        }

        private void ToolStripMenuItem_ProcessImage_Clear_Click(object sender, EventArgs e)
        {
            if (File.Exists(sProcessImageDefaultName))
            {
                if (MessageBox.Show("Do you wish to remove automatically saved process image data file '" + sProcessImageDefaultName + "'?", "", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(sProcessImageDefaultName);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void button_ClearSystemLog_Click(object sender, EventArgs e)
        {
            listView_SysLog.Items.Clear();
        }

        /*
            private void checkBox_SendBufferedAlarmsWhenConnect_CheckedChanged(object sender, EventArgs e)
            {

                if (listView_RSMPSettings.Items["SendBufferedAlarmsWhenConnect"].Checked == false)
                {
                    if (RSMPGS.ProcessImage.BufferedAlarms.Count > 0)
                    {
                        if (System.Windows.Forms.MessageBox.Show("There are some buffered alarm, do you wish to remove them?", "RSMPGS1", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            RSMPGS.ProcessImage.BufferedAlarms.Clear();
                        }
                    }
                }
                //listView_RSMPSettings.Items["SendActiveOrSuspendedAlarmsWhenConnect"].Enabled = listView_RSMPSettings.Items["SendBufferedAlarmsWhenConnect"].Checked == false;

            }
            */

        private void button_ResetRSMPSettingToDefault_Click(object sender, EventArgs e)
        {
            cHelper.ResetRSMPSettingToDefault();
        }

        private void ToolStripMenuItem_SendWatchdog_Click(object sender, EventArgs e)
        {
            RSMPGS.JSon.CreateAndSendWatchdogMessage(true);
        }

        private void button_ClearStatistics_Click(object sender, EventArgs e)
        {
            cHelper.ClearStatistics();
        }

        private void dataGridView_Behaviour_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_Behaviour.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView_Behaviour_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            cHelper.SettingCheckChanged(e);
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            cFormsHelper.ColumnClick(sender, e);
        }

        private void checkBox_Encryption_AuthenticateClient_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_Encryption_AuthenticateClient_CheckedChanged();
        }
        private void checkBox_Encryption_AuthenticateClient_CheckedChanged()
        {
            button_EncryptionFile_Browse.Enabled = checkBox_Encryption_AuthenticateAsClientUsingCertificate.Checked;
            RSMPGS.EncryptionSettings.AuthenticateAsClientUsingCertificate = checkBox_Encryption_AuthenticateAsClientUsingCertificate.Checked;
        }

        private void textBox_EncryptionFile_TextChanged(object sender, EventArgs e)
        {
            RSMPGS.EncryptionSettings.ClientCertificateFile = textBox_EncryptionFile.Text;
        }

        private void textBox_Encryption_ServerName_TextChanged(object sender, EventArgs e)
        {
            RSMPGS.EncryptionSettings.ServerName = textBox_Encryption_ServerName.Text;
        }

        public void AddBufferedMessageToListAndListView(cBufferedMessage BufferedMessage)
        {
            ListViewItem lvItem = new ListViewItem(BufferedMessage.MessageType.ToString());
            lvItem.SubItems.Add(BufferedMessage.sMessageId);
            lvItem.SubItems.Add(BufferedMessage.sSendString);
            lvItem.Tag = BufferedMessage;
            ListView_BufferedMessages.Items.Add(lvItem);
            BufferedMessage.lvItem = lvItem;
            RSMPGS.ProcessImage.BufferedMessages.Add(BufferedMessage);
            textBox_BufferedMessages.Text = RSMPGS.ProcessImage.BufferedMessages.Count.ToString();
        }

        private void button_ClearAlarmMessages_Click(object sender, EventArgs e)
        {
            RemoveBufferedMessages(cBufferedMessage.eMessageType.Alarm);
        }

        private void button_ClearAggStatusMessages_Click(object sender, EventArgs e)
        {
            RemoveBufferedMessages(cBufferedMessage.eMessageType.AggregatedStatus);
        }

        private void button_ClearStatusMessages_Click(object sender, EventArgs e)
        {
            RemoveBufferedMessages(cBufferedMessage.eMessageType.Status);
        }

        private void RemoveBufferedMessages(cBufferedMessage.eMessageType MessageType)
        {

            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            ListView_BufferedMessages.StopSorting();
            ListView_BufferedMessages.BeginUpdate();
            for (int iIndex = ListView_BufferedMessages.Items.Count - 1; iIndex >= 0; iIndex--)
            {
                cBufferedMessage BufferedMessage = (cBufferedMessage)ListView_BufferedMessages.Items[iIndex].Tag;
                if (BufferedMessage.MessageType == MessageType)
                {
                    ListView_BufferedMessages.Items.RemoveAt(iIndex);
                }
            }
            RSMPGS.ProcessImage.BufferedMessages.RemoveAll(bbuf => bbuf.MessageType == MessageType);

            /*
             * 
            List<cBufferedMessage> BufferedMessagesToSend = 
            foreach (cBufferedMessage BufferedMessage in BufferedMessagesToSend)
            {
              ListView_BufferedMessages.Items.Remove(BufferedMessage.lvItem);
            }
            RSMPGS.ProcessImage.BufferedMessages.RemoveAll(bbuf => bbuf.MessageType == MessageType);
            */
            ListView_BufferedMessages.EndUpdate();
            ListView_BufferedMessages.ResumeSorting();

            textBox_BufferedMessages.Text = RSMPGS.ProcessImage.BufferedMessages.Count.ToString();

            Cursor.Current = Cursors.Default;
        }

        private void button_BufferedMessages_CreateRandom_Click(object sender, EventArgs e)
        {
            if (comboBox_BufferedMessages_CreateRandom_Type.SelectedIndex < 0)
            {
                return;
            }

            int iMessageCount;

            if (Int32.TryParse(textBox_CreateRandomMessages_Count.Text, out iMessageCount) == false || iMessageCount > 30000)
            {
                MessageBox.Show("Invalid number or too many messages (>30000)", "Create buffered messages", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (iMessageCount <= 0)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            List<cAlarmObject> AlarmObjects = new List<cAlarmObject>();
            List<cStatusReturnValue> StatusReturnValues = new List<cStatusReturnValue>();
            List<cStatusReturnValue> ValidStatusReturnValues = new List<cStatusReturnValue>();
            List<cRoadSideObject> AggregatedStatusRoadSideObjects = new List<nsRSMPGS.cRoadSideObject>();

            foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
            {
                AlarmObjects.AddRange(RoadSideObject.AlarmObjects);
                foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
                {
                    StatusReturnValues.AddRange(StatusObject.StatusReturnValues);
                    ValidStatusReturnValues.AddRange(StatusObject.StatusReturnValues.FindAll(srv => srv.Value.Quality == cValue.eQuality.recent));
                }
                if (RoadSideObject.bIsComponentGroup)
                {
                    AggregatedStatusRoadSideObjects.Add(RoadSideObject);
                }
            }

            Random rnd = new Random();

            ListView_BufferedMessages.StopSorting();
            ListView_BufferedMessages.BeginUpdate();

            //  RSMPGS.ProcessImage.AggregatedStatusObjects
            string sSendBuffer;

            for (int iIndex = 0; iIndex < iMessageCount; iIndex++)
            {
                switch (comboBox_BufferedMessages_CreateRandom_Type.SelectedIndex)
                {

                    case 0:

                        if (AlarmObjects.Count > 0)
                        {
                            cAlarmObject AlarmObject = AlarmObjects[rnd.Next(0, AlarmObjects.Count - 1)];
                            cJSon.AlarmSpecialisation alarmSpecialisation = new cJSon.AlarmSpecialisation[] { cJSon.AlarmSpecialisation.Acknowledge, cJSon.AlarmSpecialisation.Issue, cJSon.AlarmSpecialisation.Suspend }[rnd.Next(0, 2)];
                            RSMP_Messages.AlarmHeaderAndBody alarmHeaderAndBody = RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, alarmSpecialisation, true, out sSendBuffer);
                            cBufferedMessage BufferedMessage = new cBufferedMessage(cBufferedMessage.eMessageType.Alarm, alarmHeaderAndBody.type, alarmHeaderAndBody.mId, sSendBuffer);
                            AddBufferedMessageToListAndListView(BufferedMessage);
                        }
                        break;

                    case 1:

                        if (AggregatedStatusRoadSideObjects.Count > 0)
                        {
                            cRoadSideObject RoadSideObject = AggregatedStatusRoadSideObjects[rnd.Next(0, AggregatedStatusRoadSideObjects.Count - 1)];
                            RSMP_Messages.AggregatedStatus aggregatedStatus = RSMPGS.JSon.CreateAndSendAggregatedStatusMessage(RoadSideObject, true, out sSendBuffer);
                            cBufferedMessage BufferedMessage = new cBufferedMessage(cBufferedMessage.eMessageType.AggregatedStatus, aggregatedStatus.type, aggregatedStatus.mId, sSendBuffer);
                            AddBufferedMessageToListAndListView(BufferedMessage);
                        }
                        break;

                    case 2:

                        if (StatusReturnValues.Count > 0)
                        {
                            cStatusReturnValue StatusReturnValue = StatusReturnValues[rnd.Next(0, StatusReturnValues.Count - 1)];
                            List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();
                            RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                            s.sCI = StatusReturnValue.StatusObject.sStatusCodeId;
                            s.n = StatusReturnValue.sName;
                            s.q = StatusReturnValue.Value.Quality.ToString();
                            s.s = StatusReturnValue.Value.Quality == cValue.eQuality.unknown ? null : StatusReturnValue.Value.GetValue();
                            sS.Add(s);

                            RSMP_Messages.StatusUpdate statusUpdate = RSMPGS.JSon.CreateAndSendStatusUpdateMessage(StatusReturnValue.StatusObject.RoadSideObject, sS, true, out sSendBuffer);
                            cBufferedMessage BufferedMessage = new cBufferedMessage(cBufferedMessage.eMessageType.Status, statusUpdate.type, statusUpdate.mId, sSendBuffer);
                            AddBufferedMessageToListAndListView(BufferedMessage);
                        }
                        break;

                    case 3:

                        if (ValidStatusReturnValues.Count > 0)
                        {
                            cStatusReturnValue StatusReturnValue = ValidStatusReturnValues[rnd.Next(0, ValidStatusReturnValues.Count - 1)];
                            List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();
                            RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                            s.sCI = StatusReturnValue.StatusObject.sStatusCodeId;
                            s.n = StatusReturnValue.sName;
                            s.q = StatusReturnValue.Value.Quality.ToString();
                            s.s = StatusReturnValue.Value.Quality == cValue.eQuality.unknown ? null : StatusReturnValue.Value.GetValue();
                            sS.Add(s);

                            RSMP_Messages.StatusUpdate statusUpdate = RSMPGS.JSon.CreateAndSendStatusUpdateMessage(StatusReturnValue.StatusObject.RoadSideObject, sS, true, out sSendBuffer);
                            cBufferedMessage BufferedMessage = new cBufferedMessage(cBufferedMessage.eMessageType.Status, statusUpdate.type, statusUpdate.mId, sSendBuffer);
                            AddBufferedMessageToListAndListView(BufferedMessage);
                        }
                        break;
                }

                //cBufferedMessage
                //AddBufferedMessageToListAndListView(cBufferedMessage BufferedMessage)
            }

            ListView_BufferedMessages.EndUpdate();
            ListView_BufferedMessages.ResumeSorting();

            Cursor.Current = Cursors.Default;

        }

    }
}

