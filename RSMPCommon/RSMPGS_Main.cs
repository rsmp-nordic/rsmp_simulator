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

    static public bool bActiveWatchdogAlarm = false;
		static public bool bWriteEventsContinous = false;

		static bool bLoadFailed = false;

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
        MessageBox.Show("Configuration file '" + RSMPGS.IniFileFullname + "' is missing!", "RSMP Protocol Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

      cHelper.CreateDirectories();

      cHelper.AddStatistics();

      RSMPGS.DebugConnection = new cDebugConnection(RSMPGS.DebugName, RSMPGS.DebugServer);

      // We actually begin executing stuff here...
      RSMPGS.SysLog = new cSysLogAndDebug();

      RSMPGS.SysLog.bEnableSysLog = cPrivateProfile.GetIniFileInt("Main", "EnableSysLog", 1) != 0;

#if _RSMPGS1
      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "RSMPGS1 is starting...");
      RSMPGS.JSon = new cJSonGS1();
#endif

#if _RSMPGS2
      RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "RSMPGS2 is starting...");
      RSMPGS.JSon = new cJSonGS2();
#endif

      RSMPGS.ProcessImage = new cProcessImage();
      RSMPGS.DebugForms = new List<RSMPGS_Debug>();

      cHelper.LoadRSMPSettings();

      RSMPGS.ProcessImage.LoadReferenceFiles(treeView_SitesAndObjects);

      listView_Status.Groups.Clear();
      listView_Alarms.Groups.Clear();
      listView_Commands.Groups.Clear();

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

      foreach (cRoadSideObject RoadSideObject in RSMPGS.ProcessImage.RoadSideObjects.Values)
      {
        if (RoadSideObject.AlarmObjects.Count == 0 && RoadSideObject.CommandObjects.Count == 0 && RoadSideObject.StatusObjects.Count == 0)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Warning, "Nothing was loaded for component: {0}", RoadSideObject.sComponentId);
        }
      }

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

      textBox_SignalExchangeListPath.Text = cPrivateProfile.ObjectFilesPath();

      if (RSMPGS.ProcessImage.sSULRevision.Length > 0)
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Signal Exchange List revision number from file is '{0}'", RSMPGS.ProcessImage.sSULRevision);
      }
      else
      {
        RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Signal Exchange List revision number could not be found in any object file");
      }

      textBox_SignalExchangeListVersion.Text = cPrivateProfile.GetIniFileString("Main", "SignalExchangeListVersion", "").Trim();

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

      LoadColumnWidths(this);

      listView_AlarmEvents.ColumnSorter.SortColumn = 0;
      listView_AlarmEvents.ColumnSorter.Order = SortOrder.Ascending;

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

      Main_Load();

    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {

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

      SaveColumnWidths(this);

      Main_Closing();

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

  }
}
