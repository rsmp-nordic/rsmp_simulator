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

    private void Main_Load()
    {

      RSMPGS.bConnectAsSocketClient = cPrivateProfile.GetIniFileInt("Main", "ConnectAsSocketClient", 0) != 0;

      ToolStripMenuItem_ProcessImage_LoadAtStartUp.Checked = cPrivateProfile.GetIniFileInt("Main", "LoadProcessImageAtStartUp", 0) != 0;
      ToolStripMenuItem_EventFiles_SaveCont.Checked = cPrivateProfile.GetIniFileInt("Main", "SaveEventsContinousToFile", 0) != 0;
      bWriteEventsContinous = ToolStripMenuItem_EventFiles_SaveCont.Checked;

      if (ToolStripMenuItem_ProcessImage_LoadAtStartUp.Checked)
      {
        RSMPGS.ProcessImage.LoadProcessImageValues(cPrivateProfile.ProcessImageFileFullName());
      }

      try
      {
        saveFileDialog_SXL.InitialDirectory = cPrivateProfile.GetIniFileString("Main", "SaveAsInitialDirectory", cPrivateProfile.ApplicationPath());
        saveFileDialog_SXL.FileName = cPrivateProfile.GetIniFileString("Main", "SaveAsFileName", "");
      }
      catch
      {
      }

      checkBox_AlwaysUseSXLFromFile.Checked = cPrivateProfile.GetIniFileInt("Main", "AlwaysUseSXLFromFile", 0) != 0;

      checkBox_ViewOnlyFailedPackets.Checked = cPrivateProfile.GetIniFileInt("Main", "ViewOnlyFailedPackets", 0) != 0;

      checkBox_ShowTooltip.Checked = cPrivateProfile.GetIniFileInt("Main", "ShowTooltip", 0) != 0;

      ToolStripMenuItem_ConnectAutomatically.Checked = cPrivateProfile.GetIniFileInt("Main", "ConnectAutomatically", 0) != 0;
      ToolStripMenuItem_ConnectAutomatically.Checked = cPrivateProfile.GetIniFileInt("Main", "ConnectAutomatically", 0) != 0;
      ToolStripMenuItem_DisableNagleAlgorithm.Checked = cPrivateProfile.GetIniFileInt("Main", "DisableNagleAlgorithm", 0) != 0;
      ToolStripMenuItem_SplitPackets.Checked = cPrivateProfile.GetIniFileInt("Main", "SplitPackets", 0) != 0;
      ToolStripMenuItem_StoreBase64Updates.Checked = cPrivateProfile.GetIniFileInt("Main", "StoreBase64Updates", 0) != 0;

      tabControl_Object.SelectedIndex = cPrivateProfile.GetIniFileInt("Main", "TabControl_Object", 1);

      string sObjectUniqueId = cPrivateProfile.GetIniFileString("Main", "SelectedObject", "");
      if (sObjectUniqueId.Length > 0)
      {
        foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
        {
          if (sObjectUniqueId.Equals(RoadSideObject.UniqueId(), StringComparison.OrdinalIgnoreCase))
          {
            try
            {
              treeView_SitesAndObjects.SelectedNode = RoadSideObject.Node;
              RoadSideObject.Node.EnsureVisible();
              treeView_SitesAndObjects.Select();
            }
            catch
            {
            }
            break;
          }
        }
      }

      foreach (string sUpdateRate in cPrivateProfile.GetIniFileString("RSMP", "UpdateRates", "0, 5, 300").Split(','))
      {
        ToolStripMenuItem tsUpdateRate = new ToolStripMenuItem();
        //tsUpdateRate.Name = "toolStripMenuItem_EnterUpdateRate";
        tsUpdateRate.Text = "Update Rate " + sUpdateRate.Trim() + " seconds";
        if (sUpdateRate.Trim() == "0")
        {
          tsUpdateRate.Text += " (update when value is changed)";
        }
        tsUpdateRate.Tag = "StatusSubscribe_" + sUpdateRate.Trim();
        tsUpdateRate.Click += new System.EventHandler(ToolStripMenuItem_Status_Click);
        ToolStripMenuItem_StatusSubscribe.DropDownItems.Add(tsUpdateRate);
      }

      textBox_SignalExchangeListVersionFromFile.Text = RSMPGS.ProcessImage.sSULRevision;

      if (checkBox_AlwaysUseSXLFromFile.Checked == true && textBox_SignalExchangeListVersionFromFile.Text.Length > 0)
      {
        textBox_SignalExchangeListVersion.Text = textBox_SignalExchangeListVersionFromFile.Text;
      }


      //
      // Create socket connection
      //

      ToolStripMenuItem_ConnectAutomatically.Visible = RSMPGS.bConnectAsSocketClient;
      ToolStripMenuItem_Delimiter_Connect.Visible = RSMPGS.bConnectAsSocketClient;
      ToolStripMenuItem_ConnectNow.Visible = RSMPGS.bConnectAsSocketClient;

      WatchdogInterval = cPrivateProfile.GetIniFileInt("RSMP", "WatchdogInterval", 0);
      WatchdogTimeout = cPrivateProfile.GetIniFileInt("RSMP", "WatchdogTimeout", 0);

      checkBox_Encryption_RequireClientCertificate.Checked = RSMPGS.EncryptionSettings.RequireClientCertificate;
      textBox_EncryptionFile.Text = RSMPGS.EncryptionSettings.ServerCertificateFile;

      RSMPGS.RSMPConnection = new cTcpSocket(RSMPGS.bConnectAsSocketClient ? cTcpSocket.ConnectionMethod_SocketClient : cTcpSocket.ConnectionMethod_SocketServer,
      cPrivateProfile.GetIniFileString("RSMP", "IPAddress", ""),
      cPrivateProfile.GetIniFileInt("RSMP", "ReconnectInterval", 10000),
      ToolStripMenuItem_ConnectAutomatically.Checked,
      cPrivateProfile.GetIniFileInt("RSMP", "PortNumber", 0),
      cPrivateProfile.GetIniFileInt("RSMP", "PacketTimeout", 5000),
      cTcpHelper.WrapMethod_FormFeed);

      timer_System.Enabled = true;

      bIsLoading = false;

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "RSMPGS2 has started");

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

			if (bLoadFailed)
			{
				return;
			}

			RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "RSMPGS2 is shutting down...");

			//
			// Disconnect
			//
			RSMPGS.RSMPConnection.Shutdown();

			cHelper.SaveRSMPSettings();

			cHelper.StoreDebugForms();

			//
			// Store my locations
			//
			cPrivateProfile.WriteIniFileInt("Main", "Left", this.Left);
			cPrivateProfile.WriteIniFileInt("Main", "Top", this.Top);
			cPrivateProfile.WriteIniFileInt("Main", "Width", this.Width);
			cPrivateProfile.WriteIniFileInt("Main", "Height", this.Height);

			cPrivateProfile.WriteIniFileInt("Main", "ConnectAutomatically", ToolStripMenuItem_ConnectAutomatically.Checked == true ? 1 : 0);
			cPrivateProfile.WriteIniFileInt("Main", "ShowTooltip", checkBox_ShowTooltip.Checked == true ? 1 : 0);

			cPrivateProfile.WriteIniFileInt("Main", "TabControl_Object", tabControl_Object.SelectedIndex);

			cPrivateProfile.WriteIniFileString("Main", "SaveAsInitialDirectory", saveFileDialog_SXL.InitialDirectory);
			cPrivateProfile.WriteIniFileString("Main", "SaveAsFileName", saveFileDialog_SXL.FileName);

			cPrivateProfile.WriteIniFileString("Main", "SignalExchangeListVersion", textBox_SignalExchangeListVersion.Text);
			cPrivateProfile.WriteIniFileInt("Main", "AlwaysUseSXLFromFile", checkBox_AlwaysUseSXLFromFile.Checked == true ? 1 : 0);

			cPrivateProfile.WriteIniFileInt("Main", "ViewOnlyFailedPackets", checkBox_ViewOnlyFailedPackets.Checked == true ? 1 : 0);

			cPrivateProfile.WriteIniFileInt("Main", "DisableNagleAlgorithm", ToolStripMenuItem_DisableNagleAlgorithm.Checked == true ? 1 : 0);
			cPrivateProfile.WriteIniFileInt("Main", "SplitPackets", ToolStripMenuItem_SplitPackets.Checked == true ? 1 : 0);
			cPrivateProfile.WriteIniFileInt("Main", "StoreBase64Updates", ToolStripMenuItem_StoreBase64Updates.Checked == true ? 1 : 0);

			cPrivateProfile.WriteIniFileInt("Main", "LoadProcessImageAtStartUp", ToolStripMenuItem_ProcessImage_LoadAtStartUp.Checked == true ? 1 : 0);

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

			RSMPGS.ProcessImage.SaveProcessImageValues();

      RSMPGS.DebugConnection.Shutdown();

			RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "RSMPGS2 was shut down");
			RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "");

		}

		//
		// Do some cyclic cleanup
		//
		private void timer_System_Tick(object sender, EventArgs e)
		{

			RSMPGS.SysLog.CyclicCleanup(timer_System.Interval);
			RSMPGS.ProcessImage.CyclicCleanup(timer_System.Interval);

			RSMPGS.JSon.CyclicCleanup(timer_System.Interval, WatchdogInterval, WatchdogTimeout);

			cHelper.UpdateStatistics(timer_System.Interval);

			if (RSMPGS.RSMPConnection.ConnectionStatus() != LastConnectionStatus)
			{
				ToolStripMenuItem_ConnectionStatus.ForeColor = rgbDefaultForeColor;
				ToolStripMenuItem_ConnectionStatus.BackColor = rgbDefaultBackColor;
				LastConnectionStatus = RSMPGS.RSMPConnection.ConnectionStatus();
				if (RSMPGS.bConnectAsSocketClient)
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

    private void treeView_SitesAndObjects_AfterSelect(object sender, TreeViewEventArgs e)
    {
      treeView_SitesAndObjects_AfterSelect();
    }

    private void treeView_SitesAndObjects_AfterSelect()
		{

			cRoadSideObject RoadSideObject;
			cSiteIdObject SiteIdObject;

			bIsCurrentlyChangingSelection = true;

			// Clear Alarm tab
			listView_Alarms.Items.Clear();
      listView_Alarms.StopSorting();

      // Clear Command tab
      listView_Commands.Items.Clear();
      listView_Commands.StopSorting();

      // Clear Status tab
      listView_Status.Items.Clear();
      listView_Status.StopSorting();

      // Clear Aggregated status tab
      groupBox_AggregatedStatus_FunctionalPosition.Enabled = false;
			groupBox_AggregatedStatus_FunctionalState.Enabled = false;
			groupBox_AggregatedStatus_StatusBits.Enabled = false;
			listBox_AggregatedStatus_FunctionalPosition.Items.Clear();
			listBox_AggregatedStatus_FunctionalState.Items.Clear();
			listView_AggregatedStatusEvents.Items.Clear();
			foreach (ListViewItem lvItem in listView_AggregatedStatus_StatusBits.Items)
			{
				SetStatusBitColor(lvItem, false);
			}

			if (treeView_SitesAndObjects.SelectedNode.Tag == null)
			{
				bIsCurrentlyChangingSelection = false;
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
				//bIsCurrentlyChangingSelection = false;
				//return;
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
				RSMPGS.ProcessImage.LoadProcessImageValues(cPrivateProfile.ProcessImageFileFullName());
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


  }
}
