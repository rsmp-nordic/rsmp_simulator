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

      /*
      // Fill out status listview
      for (int iIndex = 0; iIndex < RSMPGS.ProcessImage.MaxStatusReturnValues; iIndex++)
      {
        listView_Status.Columns.Add("Name", 100, HorizontalAlignment.Left);
        listView_Status.Columns.Add("Type", 100, HorizontalAlignment.Center);
        ColumnHeader columnHeader = listView_Status.Columns.Add("Status", 100, HorizontalAlignment.Center);
        columnHeader.Tag = "Status" + "_" + iIndex.ToString();
        listView_Status.Columns.Add("Comment", 200, HorizontalAlignment.Left);
      }
      */

      checkBox_AlwaysUseSXLFromFile.Checked = cPrivateProfile.GetIniFileInt("Main", "AlwaysUseSXLFromFile", 0) != 0;

      checkBox_ViewOnlyFailedPackets.Checked = cPrivateProfile.GetIniFileInt("Main", "ViewOnlyFailedPackets", 0) != 0;

      checkBox_ShowTooltip.Checked = cPrivateProfile.GetIniFileInt("Main", "ShowTooltip", 0) != 0;

      checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Checked = cPrivateProfile.GetIniFileInt("Main", "AggregatedStatus_SendAutomaticallyWhenChanged", 0) != 0;
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
            treeView_SitesAndObjects.SelectedNode = RoadSideObject.Node;
            RoadSideObject.Node.EnsureVisible();
            treeView_SitesAndObjects.Select();
            break;
          }
        }
      }

      checkBox_AutomaticallySaveProcessData.Checked = cPrivateProfile.GetIniFileInt("Main", "AutomaticallySaveProcessData", 0) != 0;
      checkbox_AutomaticallyLoadProcessData.Checked = cPrivateProfile.GetIniFileInt("Main", "AutomaticallyLoadProcessData", 0) != 0;
      checkBox_ProcessImageLoad_AlarmStatus.Checked = cPrivateProfile.GetIniFileInt("Main", "ProcessImageLoad_AlarmStatus", 0) != 0;
      checkBox_ProcessImageLoad_AggregatedStatus.Checked = cPrivateProfile.GetIniFileInt("Main", "ProcessImageLoad_AggregatedStatus", 0) != 0;
      checkBox_ProcessImageLoad_Status.Checked = cPrivateProfile.GetIniFileInt("Main", "ProcessImageLoad_Status", 0) != 0;

      saveFileDialog_ProcessImage.Title = "Save Process Data as";
      openFileDialog_ProcessImage.Title = "Load Process Data from";
      saveFileDialog_ProcessImage.InitialDirectory = cPrivateProfile.GetIniFileString("Main", "ProcessImageLoadSave_DefaultPath", cPrivateProfile.ObjectFilesPath());
      openFileDialog_ProcessImage.InitialDirectory = saveFileDialog_ProcessImage.InitialDirectory;

      textBox_SignalExchangeListVersionFromFile.Text = RSMPGS.ProcessImage.sSULRevision;

      if (checkBox_AlwaysUseSXLFromFile.Checked == true && textBox_SignalExchangeListVersionFromFile.Text.Length > 0)
      {
        textBox_SignalExchangeListVersion.Text = textBox_SignalExchangeListVersionFromFile.Text;
      }

      WatchdogInterval = cPrivateProfile.GetIniFileInt("RSMP", "WatchdogInterval", 0);
      WatchdogTimeout = cPrivateProfile.GetIniFileInt("RSMP", "WatchdogTimeout", 0);

      RSMPGS.ConnectionType = cPrivateProfile.GetIniFileInt("RSMP", "ConnectionType", cTcpSocket.ConnectionMethod_SocketClient);

      ToolStripMenuItem_ConnectAutomatically.Enabled = (RSMPGS.ConnectionType == cTcpSocket.ConnectionMethod_SocketClient) ? true : false;
      ToolStripMenuItem_ConnectNow.Enabled = (RSMPGS.ConnectionType == cTcpSocket.ConnectionMethod_SocketClient) ? true : false;

      checkBox_Encryption_AuthenticateAsClientUsingCertificate.Checked = RSMPGS.EncryptionSettings.AuthenticateAsClientUsingCertificate;
      textBox_EncryptionFile.Text = RSMPGS.EncryptionSettings.ClientCertificateFile;
      textBox_Encryption_ServerName.Text = RSMPGS.EncryptionSettings.ServerName;

      checkBox_Encryption_AuthenticateClient_CheckedChanged();

      if (checkbox_AutomaticallyLoadProcessData.Checked)
      {
        RSMPGS.ProcessImage.LoadProcessImageValues(this, cPrivateProfile.ProcessImageFileFullName());
        bProcessDataWasLoadedAtStartup = true;
      }

      RSMPGS.RSMPConnection = new cTcpSocket(RSMPGS.ConnectionType,
        cPrivateProfile.GetIniFileString("RSMP", "IPAddress", ""),
      cPrivateProfile.GetIniFileInt("RSMP", "ReconnectInterval", 10000),
      ToolStripMenuItem_ConnectAutomatically.Checked,
      cPrivateProfile.GetIniFileInt("RSMP", "PortNumber", 0),
      cPrivateProfile.GetIniFileInt("RSMP", "PacketTimeout", 5000),
      cTcpHelper.WrapMethod_FormFeed);

      timer_System.Enabled = true;

      bIsLoading = false;

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "RSMPGS1 has started");

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

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "RSMPGS1 is shutting down...");

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

      cPrivateProfile.WriteIniFileInt("Main", "ConnectAutomatically", ToolStripMenuItem_ConnectAutomatically.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "ShowTooltip", checkBox_ShowTooltip.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "AggregatedStatus_SendAutomaticallyWhenChanged", checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Main", "DisableNagleAlgorithm", ToolStripMenuItem_DisableNagleAlgorithm.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "SplitPackets", ToolStripMenuItem_SplitPackets.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "StoreBase64Updates", ToolStripMenuItem_StoreBase64Updates.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Main", "TabControl_Object", tabControl_Object.SelectedIndex);

      cPrivateProfile.WriteIniFileString("Main", "SignalExchangeListVersion", textBox_SignalExchangeListVersion.Text);
      cPrivateProfile.WriteIniFileInt("Main", "AlwaysUseSXLFromFile", checkBox_AlwaysUseSXLFromFile.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Main", "ViewOnlyFailedPackets", checkBox_ViewOnlyFailedPackets.Checked == true ? 1 : 0);

      cPrivateProfile.WriteIniFileInt("Main", "AutomaticallySaveProcessData", checkBox_AutomaticallySaveProcessData.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "AutomaticallyLoadProcessData", checkbox_AutomaticallyLoadProcessData.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "ProcessImageLoad_AlarmStatus", checkBox_ProcessImageLoad_AlarmStatus.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "ProcessImageLoad_AggregatedStatus", checkBox_ProcessImageLoad_AggregatedStatus.Checked == true ? 1 : 0);
      cPrivateProfile.WriteIniFileInt("Main", "ProcessImageLoad_Status", checkBox_ProcessImageLoad_Status.Checked == true ? 1 : 0);

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

      if (checkBox_AutomaticallySaveProcessData.Checked)
      {
        if (bProcessDataWasLoadedAtStartup == false)
        {
          if (MessageBox.Show("Process data was not loaded at startup but you have selected to save process data when exiting RSMPGS1\r\n\r\nThis will owerwrite the old ProcessImage.dat file. Is this ok?", "Save Process Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          {
            RSMPGS.ProcessImage.SaveProcessImageValues(cPrivateProfile.ProcessImageFileFullName());
          }
        }
        else
        {
          RSMPGS.ProcessImage.SaveProcessImageValues(cPrivateProfile.ProcessImageFileFullName());
        }
      }
      else
      {

      }

      RSMPGS.DebugConnection.Shutdown();

      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "RSMPGS1 was shut down");
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
        if (RSMPGS.ConnectionType == cTcpSocket.ConnectionMethod_SocketClient)
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
      RSMPGS.JSon.SocketWasClosed();
    }

    public void SocketWasConnectedMethod()
    {
      ToolStripMenuItem_SendWatchdog.Enabled = true;
      RSMPGS.JSon.SocketWasConnected();
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

      listView_AlarmEvents.Items.Clear();

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
      button_AggregatedStatus_Send.Enabled = false;
      checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Enabled = false;
      listBox_AggregatedStatus_FunctionalPosition.Items.Clear();
      listBox_AggregatedStatus_FunctionalState.Items.Clear();
      listBox_AggregatedStatus_FunctionalPosition.Items.Add("");
      listBox_AggregatedStatus_FunctionalState.Items.Add("");
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
        /*
				foreach (cRoadSideObject ScanRoadSideObject in RSMPGS.ProcessImage.RoadSideObjects)
				{
					UpdateStatusListView(ScanRoadSideObject, false);
				}
				*/
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
          switch (Subscription.StatusReturnValue.sType.ToLower())
          {
            case "boolean":
              Subscription.StatusReturnValue.sStatus = Rnd.Next(0, 2) >= 1 ? "true" : "false";
              break;
            case "string":
              Subscription.StatusReturnValue.sStatus = Rnd.Next(0, 1).ToString();
              break;
            case "real":
              Subscription.StatusReturnValue.sStatus = (Rnd.Next(-10000, 10000) / 10).ToString();
              break;
            default:
              Subscription.StatusReturnValue.sStatus = Rnd.Next(-1000, 1000).ToString();
              break;
          }
          if (Subscription.SubscribeStatus == cSubscription.Subscribe_OnChange)
          {
            RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
            s.sCI = Subscription.StatusObject.sStatusCodeId;
            s.n = Subscription.StatusReturnValue.sName;
            RSMPGS.ProcessImage.UpdateStatusValue(ref s, Subscription.StatusReturnValue.sType, Subscription.StatusReturnValue.sStatus);
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

      RSMPGS.ProcessImage.BufferedAlarms.Clear();

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
            StatusReturnValue.sStatus = "?";
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
            switch (StatusReturnValue.sType.ToLower())
            {
              case "boolean":
                StatusReturnValue.sStatus = Rnd.Next(0, 2) >= 1 ? "true" : "false";
                break;
              case "string":
                StatusReturnValue.sStatus = Rnd.Next(0, 1).ToString();
                break;
              case "real":
                StatusReturnValue.sStatus = (Rnd.Next(-10000, 10000) / 10).ToString();
                break;
              default:
                StatusReturnValue.sStatus = Rnd.Next(-1000, 1000).ToString();
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
      ToolStripMenuItem_ProcessImage_Clear.Enabled = File.Exists(cPrivateProfile.ProcessImageFileFullName());
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
      if (File.Exists(cPrivateProfile.ProcessImageFileFullName()))
      {
        if (MessageBox.Show("Do you wish to remove automatically saved process image data file '" + cPrivateProfile.ProcessImageFileFullName() + "'?", "", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.Yes)
        {
          try
          {
            File.Delete(cPrivateProfile.ProcessImageFileFullName());
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

  }

  /*
  public class HelperForms
  {

    private static TextBox textBox;

    public static string InputBox(string title, string promptText, string value, bool bAllowFileBrowse)
    {

      Form form = new Form();
      Label label = new Label();
      textBox = new TextBox();
      Button buttonOk = new Button();
      Button buttonCancel = new Button();
      Button buttonBrowse = new Button();

      form.Text = title;
      label.Text = promptText;
      textBox.Text = value;

      buttonOk.Text = "OK";
      buttonCancel.Text = "Cancel";
      buttonBrowse.Text = "Browse...";

      buttonOk.DialogResult = DialogResult.OK;
      buttonCancel.DialogResult = DialogResult.Cancel;

      if (bAllowFileBrowse)
      {
        textBox.SetBounds(12, 36, 290, 20);
        buttonBrowse.SetBounds(310, 34, 75, 23);
      }
      else
      {
        textBox.SetBounds(12, 36, 372, 20);
        buttonBrowse.Visible = false;
      }

      label.SetBounds(9, 20, 372, 13);
      buttonOk.SetBounds(228, 72, 75, 23);
      buttonCancel.SetBounds(309, 72, 75, 23);

      label.AutoSize = true;
      textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
      buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

      form.ClientSize = new Size(396, 107);
      form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel, buttonBrowse });
      form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
      form.FormBorderStyle = FormBorderStyle.FixedDialog;
      form.StartPosition = FormStartPosition.CenterScreen;
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.AcceptButton = buttonOk;
      form.CancelButton = buttonCancel;

      buttonBrowse.Click += new System.EventHandler(buttonBrowse_Click);

      DialogResult dialogResult = form.ShowDialog();

      if (dialogResult == DialogResult.OK)
      {
        return textBox.Text;
      }
      else
      {
        return value;
      }

    }

    private static void buttonBrowse_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.InitialDirectory = textBox.Text;
      openFileDialog.Filter = "All files|*.*";
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        textBox.Text = openFileDialog.FileName;
      }
    }
  }
  */

}
