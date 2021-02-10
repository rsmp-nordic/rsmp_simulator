using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace nsRSMPGS
{

    [System.ComponentModel.DesignerCategory("form")]

    public partial class RSMPGS_Main : Form
    {

        public bool bIsUpdatingAlarmEventList = false;
        public bool bIsUpdatingStatusEventList = false;
        public bool bIsUpdatingAggregatedStatusEventList = false;

        private void Main_Load()
        {

            RSMPGS.bConnectAsSocketClient = cPrivateProfile.GetIniFileInt("Main", "ConnectAsSocketClient", 0) != 0;

            ToolStripMenuItem_ProcessImage_LoadAtStartUp.Checked = cPrivateProfile.GetIniFileInt("Main", "LoadProcessImageAtStartUp", 0) != 0;
            ToolStripMenuItem_EventFiles_SaveCont.Checked = cPrivateProfile.GetIniFileInt("Main", "SaveEventsContinousToFile", 0) != 0;
            bWriteEventsContinous = ToolStripMenuItem_EventFiles_SaveCont.Checked;

            try
            {
                saveFileDialog_SXL.InitialDirectory = cPrivateProfile.GetIniFileString("Main", "SaveAsInitialDirectory", cPrivateProfile.ApplicationPath());
                saveFileDialog_SXL.FileName = cPrivateProfile.GetIniFileString("Main", "SaveAsFileName", "");
            }
            catch
            {
            }

            foreach (string sUpdateRate in cPrivateProfile.GetIniFileString("RSMP", "UpdateRates", "0,5,10,30,60,120").Split(','))
            {
                ToolStripMenuItem tsUpdateRate1 = new ToolStripMenuItem();
                ToolStripMenuItem tsUpdateRate2 = new ToolStripMenuItem();
                tsUpdateRate1.Text = "Update Rate " + sUpdateRate.Trim() + " seconds";
                if (sUpdateRate.Trim() == "0")
                {
                    tsUpdateRate1.Text += " (update when value is changed only)";
                }
                tsUpdateRate2.Text = tsUpdateRate1.Text;
                tsUpdateRate1.Tag = sUpdateRate.Trim();
                tsUpdateRate2.Tag = sUpdateRate.Trim();
                tsUpdateRate1.Click += new System.EventHandler(ToolStripMenuItem_Status_Click);
                tsUpdateRate2.Click += new System.EventHandler(ToolStripMenuItem_Status_Click);
                ToolStripMenuItem_StatusSubscribe_UpdateOnInterval.DropDownItems.Add(tsUpdateRate1);
                ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval.DropDownItems.Add(tsUpdateRate2);
            }

            //
            // Create socket connection
            //

            ToolStripMenuItem_ConnectAutomatically.Visible = RSMPGS.bConnectAsSocketClient;
            ToolStripMenuItem_Delimiter_Connect.Visible = RSMPGS.bConnectAsSocketClient;
            ToolStripMenuItem_ConnectNow.Visible = RSMPGS.bConnectAsSocketClient;

            checkBox_Encryption_RequireClientCertificate.Checked = RSMPGS.EncryptionSettings.RequireClientCertificate;
            textBox_EncryptionFile.Text = RSMPGS.EncryptionSettings.ServerCertificateFile;

            RSMPGS.RSMPConnection = new cTcpSocket(RSMPGS.bConnectAsSocketClient ? cTcpSocket.ConnectionMethod_SocketClient : cTcpSocket.ConnectionMethod_SocketServer,
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


            cPrivateProfile.WriteIniFileString("Main", "SaveAsInitialDirectory", saveFileDialog_SXL.InitialDirectory);
            cPrivateProfile.WriteIniFileString("Main", "SaveAsFileName", saveFileDialog_SXL.FileName);

            cPrivateProfile.WriteIniFileInt("Main", "LoadProcessImageAtStartUp", ToolStripMenuItem_ProcessImage_LoadAtStartUp.Checked == true ? 1 : 0);

            RSMPGS.ProcessImage.SaveProcessImageValues();

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
            //DebugForm.MainForm = this;
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

        private void ToolStripMenuItem_ProcessImage_RestoreAtStartUp_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_ProcessImage_LoadAtStartUp.Checked = !ToolStripMenuItem_ProcessImage_LoadAtStartUp.Checked;
        }

        private void ToolStripMenuItem_ProcessImage_Load_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("This will replace the current ProcessImage with the last saved one.\r\nAre you sure?", "RSMPGS2", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RSMPGS.ProcessImage.ResetProcessImage();
                RSMPGS.ProcessImage.LoadProcessImageValues(sProcessImageDefaultName);
            }
        }

        private void ToolStripMenuItem_ProcessImage_Reset_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("This will reset all eventlists, subscriptions and statuses to default.\r\nAre you sure?", "RSMPGS2", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RSMPGS.ProcessImage.ResetProcessImage();
            }
        }


        private void ToolStripMenuItem_File_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void DecodeJSonPacketMethod(string sJSon)
        {
            RSMPGS.JSon.DecodeAndParseJSonPacket(sJSon);
        }

        public void SocketWasClosedMethod()
        {
            ToolStripMenuItem_SendWatchdog.Enabled = false;
            RSMPGS.JSon.SocketWasClosed();
        }

        public void SocketWasConnectedMethod()
        {
            ToolStripMenuItem_SendWatchdog.Enabled = true;
            RSMPGS.JSon.SocketWasConnected();
        }

        private void ToolStripMenuItem_Connection_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem_ConnectNow.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() != cTcpSocket.ConnectionStatus_Connected && RSMPGS.bConnectAsSocketClient == true;
            ToolStripMenuItem_Disconnect.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected;

            ToolStripMenuItem_SendSomeRandomCrap.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected;
            ToolStripMenuItem_SendWatchdog.Enabled = RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected;
        }

        private void ToolStripMenuItem_ConnectNow_Click(object sender, EventArgs e)
        {
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

        private void ToolStripMenuItem_DisableNagleAlgorithm_Click(object sender, EventArgs e)
        {
            RSMPGS.RSMPConnection.NagleAlgorithm(ToolStripMenuItem_DisableNagleAlgorithm.Checked);
        }

        private void ToolStripMenuItem_Subscriptions_ResendAll_Click(object sender, EventArgs e)
        {
            RSMPGS.ProcessImage.ReSendSubscriptions(true);
        }

        private void ToolStripMenuItem_Subscriptions_UnsubscribeAll_Click(object sender, EventArgs e)
        {
            RSMPGS.ProcessImage.ReSendSubscriptions(false);
        }

        private void ToolStripMenuItem_EventFiles_SaveCont_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_EventFiles_SaveCont.Checked = !ToolStripMenuItem_EventFiles_SaveCont.Checked;
            bWriteEventsContinous = ToolStripMenuItem_EventFiles_SaveCont.Checked;
        }

        private void ToolStripMenuItem_SendSomeRandomCrap_Click(object sender, EventArgs e)
        {
            Random Rnd = new Random();
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");

            byte[] RandomArray = new byte[Rnd.Next(1, 2048)];
            Rnd.NextBytes(RandomArray);
            RSMPGS.JSon.SendJSonPacket("crap", null, encoding.GetString(RandomArray), false);

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
        /*
            private void ToolStripMenuItem_File_SaveAs_Click(object sender, EventArgs e)
            {

                if (saveFileDialog_SXL.ShowDialog() == DialogResult.OK)
                {

                    saveFileDialog_SXL.AddExtension = true;

                    string sFileExtension = Path.GetExtension(saveFileDialog_SXL.FileName).ToLower();

                    switch (sFileExtension)
                    {
                        case ".xml":
                        case ".json":

                            RSMPGS.ProcessImage.SaveReferenceFile(saveFileDialog_SXL.FileName, sFileExtension);

                            break;

                        default:

                            MessageBox.Show("Unknown file format", "Saving file as", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            break;
                    }
                }
            }
        */

        private void button_ClearSystemLog_Click(object sender, EventArgs e)
        {
            listView_SysLog.Items.Clear();

        }

        private void ToolStripMenuItem_SendWatchdog_Click(object sender, EventArgs e)
        {
            RSMPGS.JSon.CreateAndSendWatchdogMessage(true);
        }

        private void button_ResetRSMPSettingToDefault_Click(object sender, EventArgs e)
        {
            cHelper.ResetRSMPSettingToDefault();
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

        private void ToolStripMenuItem_View_Clear_AggregatedStatusEvents_Click(object sender, EventArgs e)
        {
            foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
            {
                RoadSideObject.AggregatedStatusEvents.Clear();
            }

            listView_AggregatedStatusEvents.Items.Clear();

        }

        private void ToolStripMenuItem_View_Clear_StatusEvents_Click(object sender, EventArgs e)
        {

            foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
            {
                RoadSideObject.StatusEvents.Clear();
            }

            listView_StatusEvents.Items.Clear();

        }

        private void ToolStripMenuItem_View_Clear_CommandEvents_Click(object sender, EventArgs e)
        {
            foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
            {
                RoadSideObject.CommandEvents.Clear();
            }

            listView_CommandEvents.Items.Clear();

        }

        private void textBox_EncryptionFile_TextChanged(object sender, EventArgs e)
        {
            RSMPGS.EncryptionSettings.ServerCertificateFile = textBox_EncryptionFile.Text;
        }

        private void checkBox_Encryption_RequireClientCertificate_CheckedChanged(object sender, EventArgs e)
        {
            RSMPGS.EncryptionSettings.RequireClientCertificate = checkBox_Encryption_RequireClientCertificate.Checked;
        }

        private void button_AggregatedStatus_Request_Click(object sender, EventArgs e)
        {
            if (SelectedRoadSideObject != null)
            {
                RSMPGS.JSon.CreateAndSendAggregatedStatusRequestMessage(SelectedRoadSideObject);
            }
        }

    }
}
