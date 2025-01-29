using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Linq;
using System.Collections;
using System.Diagnostics;
using static System.Windows.Forms.ListViewItem;
using System.Runtime.CompilerServices;
using RSMP_Messages;
using System.Drawing.Printing;

namespace nsRSMPGS
{
  public class cSetting
  {

    public string sKey;
    public string sDescription;

    public int RowIndex;

    public bool IsAffectedByRSMPVersion;

    private bool bActualValue;

    private bool bActualValue_RSMP_3_1_1;
    private bool bActualValue_RSMP_3_1_2;
    private bool bActualValue_RSMP_3_1_3;
    private bool bActualValue_RSMP_3_1_4;
    private bool bActualValue_RSMP_3_1_5;
    private bool bActualValue_RSMP_3_2_0;
    private bool bActualValue_RSMP_3_2_1;
    private bool bActualValue_RSMP_3_2_2;

    private bool bDefaultValue;

    private bool bDefaultValue_RSMP_3_1_1;
    private bool bDefaultValue_RSMP_3_1_2;
    private bool bDefaultValue_RSMP_3_1_3;
    private bool bDefaultValue_RSMP_3_1_4;
    private bool bDefaultValue_RSMP_3_1_5;
    private bool bDefaultValue_RSMP_3_2_0;
    private bool bDefaultValue_RSMP_3_2_1;
    private bool bDefaultValue_RSMP_3_2_2;

    public cSetting(string sKey, string sDescription, int iRowIndex, bool IsAffectedByRSMPVersion, bool bDefaultValue, bool bDefaultValue_RSMP_3_1_1, bool bDefaultValue_RSMP_3_1_2, bool bDefaultValue_RSMP_3_1_3, bool bDefaultValue_RSMP_3_1_4, bool bDefaultValue_RSMP_3_1_5, bool bDefaultValue_RSMP_3_2_0, bool bDefaultValue_RSMP_3_2_1, bool bDefaultValue_RSMP_3_2_2)
    {

      this.sKey = sKey;
      this.sDescription = sDescription;

      this.RowIndex = iRowIndex;

      this.IsAffectedByRSMPVersion = IsAffectedByRSMPVersion;

      this.bDefaultValue = bDefaultValue;

      this.bDefaultValue_RSMP_3_1_1 = bDefaultValue_RSMP_3_1_1;
      this.bDefaultValue_RSMP_3_1_2 = bDefaultValue_RSMP_3_1_2;
      this.bDefaultValue_RSMP_3_1_3 = bDefaultValue_RSMP_3_1_3;
      this.bDefaultValue_RSMP_3_1_4 = bDefaultValue_RSMP_3_1_4;
      this.bDefaultValue_RSMP_3_1_5 = bDefaultValue_RSMP_3_1_5;
      this.bDefaultValue_RSMP_3_2_0 = bDefaultValue_RSMP_3_2_0;
      this.bDefaultValue_RSMP_3_2_1 = bDefaultValue_RSMP_3_2_1;
      this.bDefaultValue_RSMP_3_2_2 = bDefaultValue_RSMP_3_2_2;
    }

    public int GetColumnIndex(cJSon.RSMPVersion rsmpVersion)
    {
      return (int)(rsmpVersion + 1);
    }

    public cJSon.RSMPVersion GetRSMPVersion(int iColumnIndex)
    {
      return (cJSon.RSMPVersion)(iColumnIndex - 1);
    }

    public bool GetActualValue(int iColumnIndex)
    {
      return GetActualValue(GetRSMPVersion(iColumnIndex));
    }

    public bool GetActualValue(cJSon.RSMPVersion rsmpVersion)
    {

      switch (rsmpVersion)
      {
        case cJSon.RSMPVersion.NotSupported:
          return bActualValue;

        case cJSon.RSMPVersion.RSMP_3_1_1:
          return bActualValue_RSMP_3_1_1;

        case cJSon.RSMPVersion.RSMP_3_1_2:
          return bActualValue_RSMP_3_1_2;

        case cJSon.RSMPVersion.RSMP_3_1_3:
          return bActualValue_RSMP_3_1_3;

        case cJSon.RSMPVersion.RSMP_3_1_4:
          return bActualValue_RSMP_3_1_4;

        case cJSon.RSMPVersion.RSMP_3_1_5:
          return bActualValue_RSMP_3_1_5;

        case cJSon.RSMPVersion.RSMP_3_2_0:
          return bActualValue_RSMP_3_2_0;

        case cJSon.RSMPVersion.RSMP_3_2_1:
          return bActualValue_RSMP_3_2_1;

        case cJSon.RSMPVersion.RSMP_3_2_2:
          return bActualValue_RSMP_3_2_2;
      }
      return false;
    }

    public bool GetDefaultValue(int iColumnIndex)
    {
      return GetDefaultValue(GetRSMPVersion(iColumnIndex));
    }

    public bool GetDefaultValue(cJSon.RSMPVersion rsmpVersion)
    {

      switch (rsmpVersion)
      {

        case cJSon.RSMPVersion.NotSupported:
          return bDefaultValue;

        case cJSon.RSMPVersion.RSMP_3_1_1:
          return bDefaultValue_RSMP_3_1_1;

        case cJSon.RSMPVersion.RSMP_3_1_2:
          return bDefaultValue_RSMP_3_1_2;

        case cJSon.RSMPVersion.RSMP_3_1_3:
          return bDefaultValue_RSMP_3_1_3;

        case cJSon.RSMPVersion.RSMP_3_1_4:
          return bDefaultValue_RSMP_3_1_4;

        case cJSon.RSMPVersion.RSMP_3_1_5:
          return bDefaultValue_RSMP_3_1_5;

        case cJSon.RSMPVersion.RSMP_3_2_0:
          return bDefaultValue_RSMP_3_2_0;

        case cJSon.RSMPVersion.RSMP_3_2_1:
          return bDefaultValue_RSMP_3_2_1;

        case cJSon.RSMPVersion.RSMP_3_2_2:
          return bDefaultValue_RSMP_3_2_2;

        default:
          return false;
      }
    }

    public void SetActualValue(int iColumnIndex, bool bValue)
    {
      SetActualValue(GetRSMPVersion(iColumnIndex), bValue);
    }

    public void SetActualValue(cJSon.RSMPVersion rsmpVersion, bool bValue)
    {
      switch (rsmpVersion)
      {

        case cJSon.RSMPVersion.NotSupported:
          bActualValue = bValue;
          break;

        case cJSon.RSMPVersion.RSMP_3_1_1:
          bActualValue_RSMP_3_1_1 = bValue;
          break;

        case cJSon.RSMPVersion.RSMP_3_1_2:
          bActualValue_RSMP_3_1_2 = bValue;
          break;

        case cJSon.RSMPVersion.RSMP_3_1_3:
          bActualValue_RSMP_3_1_3 = bValue;
          break;

        case cJSon.RSMPVersion.RSMP_3_1_4:
          bActualValue_RSMP_3_1_4 = bValue;
          break;

        case cJSon.RSMPVersion.RSMP_3_1_5:
          bActualValue_RSMP_3_1_5 = bValue;
          break;

        case cJSon.RSMPVersion.RSMP_3_2_0:
          bActualValue_RSMP_3_2_0 = bValue;
          break;

        case cJSon.RSMPVersion.RSMP_3_2_1:
          bActualValue_RSMP_3_2_1 = bValue;
          break;

        case cJSon.RSMPVersion.RSMP_3_2_2:
          bActualValue_RSMP_3_2_2 = bValue;
          break;
      }
    }
  }

  public class cPrivateProfile
  {

    [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
      SetLastError = true,
      CharSet = CharSet.Unicode, ExactSpelling = true,
      CallingConvention = CallingConvention.StdCall)]
    private static extern int GetPrivateProfileString(
    string lpAppName,
    string lpKeyName,
    string lpDefault,
    string lpReturnString,
    int nSize,
    string lpFilename);

    [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileIntW",
      SetLastError = true,
      CharSet = CharSet.Unicode, ExactSpelling = true,
      CallingConvention = CallingConvention.StdCall)]
    private static extern int GetPrivateProfileInt(
    string lpAppName,
    string lpKeyName,
    int iDefault,
    string lpFilename);

    [DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringW",
      SetLastError = true,
      CharSet = CharSet.Unicode, ExactSpelling = true,
      CallingConvention = CallingConvention.StdCall)]
    private static extern int WritePrivateProfileString(
    string lpAppName,
    string lpKeyName,
    string lpString,
    string lpFilename);

    public static string ApplicationPath()
    {

      if (RSMPGS.SpecifiedPath.Length == 0)
      {
#if DEBUG
        return "..\\..\\..";
#else
                string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                System.IO.FileInfo file = new System.IO.FileInfo(AssemblyName);
                return file.Directory.FullName;
#endif
      }
      else
      {
        return RSMPGS.SpecifiedPath;
      }


    }

    public static string SettingsPath()
    {
      return ApplicationPath() + "\\Settings";
    }

    public static string DefaultObjectFilesPath()
    {
      return ApplicationPath() + "\\Objects";
    }

    public static string LogFilesPath()
    {
      return ApplicationPath() + "\\LogFiles";
    }

    public static string SysLogFilesPath()
    {
      return LogFilesPath() + "\\SysLogFiles";
    }

    public static string DebugFilesPath()
    {
      return LogFilesPath() + "\\DebugFiles";
    }

    public static string EventFilesPath()
    {
      return LogFilesPath() + "\\EventFiles";
    }
    /*
    public static string ProcessImageFileFullName()
    {
      return cPrivateProfile.ObjectFilesPath() + "\\" + "ProcessImage.dat";
    }
    */
    public static string GetIniFileString(string category, string key, string defaultValue)
    {
      return GetIniFileString(RSMPGS.IniFileFullname, category, key, defaultValue);
    }

    public static string GetIniFileString(string iniFile, string category, string key, string defaultValue)
    {
      string returnString = new string(' ', 1024);
      GetPrivateProfileString(category, key, defaultValue, returnString, 1024, iniFile);
      return returnString.Split('\0')[0];
    }

    public static int GetIniFileInt(string category, string key, int defaultValue)
    {
      return GetIniFileInt(RSMPGS.IniFileFullname, category, key, defaultValue);
    }

    public static int GetIniFileInt(string iniFile, string category, string key, int defaultValue)
    {
      return GetPrivateProfileInt(category, key, defaultValue, iniFile);
    }

    public static void WriteIniFileString(string category, string key, string value)
    {
      WriteIniFileString(RSMPGS.IniFileFullname, category, key, value);
    }

    public static void WriteIniFileString(string iniFile, string category, string key, string value)
    {
      WritePrivateProfileString(category, key, value, iniFile);
    }

    public static void WriteIniFileInt(string category, string key, int value)
    {
      WriteIniFileInt(RSMPGS.IniFileFullname, category, key, value);
    }

    public static void WriteIniFileInt(string iniFile, string category, string key, int value)
    {
      WritePrivateProfileString(category, key, value.ToString(), iniFile);
    }

    public static List<string> GetCategories()
    {
      return GetCategories(RSMPGS.IniFileFullname);
    }

    public static List<string> GetCategories(string iniFile)
    {
      string returnString = new string(' ', 65536);
      GetPrivateProfileString(null, null, null, returnString, 65536, iniFile);
      List<string> result = new List<string>(returnString.Split('\0'));
      result.RemoveRange(result.Count - 2, 2);
      return result;
    }

    public static List<string> GetKeys(string category)
    {
      return GetKeys(RSMPGS.IniFileFullname, category);
    }

    public static List<string> GetKeys(string iniFile, string category)
    {
      string returnString = new string(' ', 32768);
      GetPrivateProfileString(category, null, null, returnString, 32768, iniFile);
      List<string> result = new List<string>(returnString.Split('\0'));
      result.RemoveRange(result.Count - 2, 2);
      return result;
    }

    public static string Base64Encode(string plainText)
    {
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
      return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
      var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
      return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

  }

  public class cHelper
  {
    public static void CreateDirectories()
    {

      try
      {
        Directory.CreateDirectory(cPrivateProfile.SettingsPath());
      }
      catch { }

      /*
      try
      {
        Directory.CreateDirectory(cPrivateProfile.ObjectFilesPath());
      }
      catch { }
      */

      try
      {
        Directory.CreateDirectory(cPrivateProfile.LogFilesPath());
      }
      catch { }

      try
      {
        Directory.CreateDirectory(cPrivateProfile.SysLogFilesPath());
      }
      catch { }

      try
      {
        Directory.CreateDirectory(cPrivateProfile.DebugFilesPath());
      }
      catch { }
#if _RSMPGS2

      try
      {
        Directory.CreateDirectory(cPrivateProfile.EventFilesPath());
      }
      catch { }

#endif
    }

    public static void RestoreDebugForms()
    {

      int iDebugFormIndex;
      RSMPGS_Debug DebugForm;

      //
      // Create debug windows (if any)
      //
      for (iDebugFormIndex = 0; ; iDebugFormIndex++)
      {
        string sPrefix = "DebugForm_" + iDebugFormIndex.ToString() + "_";
        if (cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Width", -1) > 0)
        {
          DebugForm = new RSMPGS_Debug();
          //          DebugForm.MainForm = this;
          DebugForm.Left = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Left", iDebugFormIndex * 50);
          DebugForm.Top = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Top", iDebugFormIndex * 50);
          DebugForm.Width = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Width", 500);
          DebugForm.Height = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Height", 500);
          DebugForm.ToolStripMenuItem_PacketTypes_Raw.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Raw", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_All.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "All", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_Version.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Version", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_Alarm.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Alarm", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_AggStatus.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "AggStatus", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_Status.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Status", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_Command.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Command", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_Watchdog.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Watchdog", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_PacketAck.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "PacketAck", 0) != 0;
          DebugForm.ToolStripMenuItem_PacketTypes_Unknown.Checked = cPrivateProfile.GetIniFileInt("Debug", sPrefix + "Unknown", 0) != 0;
          DebugForm.SetDebugSaveFile(cPrivateProfile.GetIniFileString("Debug", sPrefix + "SaveFile", ""));
          DebugForm.toolStripMenuItem_SaveContinousToFile.Checked = DebugForm.GetDebugSaveFile() != "";

          DebugForm.CalcNewCaption();
          // Forms will be shown at show event
          RSMPGS.DebugForms.Add(DebugForm);
        }
        else
        {
          break;
        }
      }
    }

    public static void StoreDebugForms()
    {

      int iDebugFormIndex = 0;

      // Clear section
      cPrivateProfile.WriteIniFileString("Debug", null, null);

      //
      // Store debug window stuff
      //
      foreach (RSMPGS_Debug DebugForm in RSMPGS.DebugForms)
      {
        string sPrefix = "DebugForm_" + iDebugFormIndex.ToString() + "_";
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Left", DebugForm.Left);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Top", DebugForm.Top);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Width", DebugForm.Width);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Height", DebugForm.Height);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "All", DebugForm.ToolStripMenuItem_PacketTypes_All.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Raw", DebugForm.ToolStripMenuItem_PacketTypes_Raw.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Version", DebugForm.ToolStripMenuItem_PacketTypes_Version.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Alarm", DebugForm.ToolStripMenuItem_PacketTypes_Alarm.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "AggStatus", DebugForm.ToolStripMenuItem_PacketTypes_AggStatus.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Status", DebugForm.ToolStripMenuItem_PacketTypes_Status.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Command", DebugForm.ToolStripMenuItem_PacketTypes_Command.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Watchdog", DebugForm.ToolStripMenuItem_PacketTypes_Watchdog.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "PacketAck", DebugForm.ToolStripMenuItem_PacketTypes_PacketAck.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileInt("Debug", sPrefix + "Unknown", DebugForm.ToolStripMenuItem_PacketTypes_Unknown.Checked == true ? 1 : 0);
        cPrivateProfile.WriteIniFileString("Debug", sPrefix + "SaveFile", DebugForm.GetDebugSaveFile());

        iDebugFormIndex++;
      }

      //
      // Close the debug forms
      //
      while (RSMPGS.DebugForms.Count > 0)
      {
        // Will remove itself from the list when closed
        RSMPGS.DebugForms[0].Close();
      }

    }

    public static void LoadRSMPSettings()
    {

      AddSetting("AllowUseRSMPVersion", "Allow/use RSMP version in protocol negotiation", true, true, true, true, true, true, true, true);

      AddSetting("SendVersionInfoAtConnect", "Send and expect version info when connecting", true);
      AddSetting("SXL_VersionIgnore", "Ignore client RSMP and SXL version incompability", false);
      AddSetting("SendWatchdogPacketAtStartup", "Send and expect Watchdog packet when connecting", true);

      /*
#if _RSMPGS2

      AddSetting("SendInitialRequestsOfAlarms", "Send initial alarms Request messages when connected", false, false, false, false, true);
      AddSetting("SendInitialRequestsOfAggStatus", "Send initial aggregated status Request messages when connected", false, false, false, false, true);

#endif
*/

#if _RSMPGS1

      AddSetting("ClearSubscriptionsAtDisconnect", "Clear subscriptions when disconnecting", true, true, false, false, false, false, false, false);
      AddSetting("AllowRequestsOfAlarmsAndAggStatus", "Allow alarms and aggregated status Request messages", false, false, false, false, true, true, true, true);
      AddSetting("Buffer10000Messages", "Buffer up to 10000 messages (instead of 1000)", false, false, false, true, true, true, true, true);
      AddSetting("SendAggregatedStatusAtConnect", "Send aggregated status when connecting", false, true, true, true, true, true, true, true);
      AddSetting("SendAllAlarmsWhenConnect", "Send all alarms when connecting", false, false, true, true, true, true, true, true);
      AddSetting("BufferAndSendAlarmsWhenConnect", "Buffer alarm events when disconnected and send them when connecting", false, false, true, true, true, true, true, true);
      AddSetting("BufferAndSendAggregatedStatusWhenConnect", "Buffer aggregated status when disconnected and send them when connecting", false, false, true, true, true, true, true, true);
      AddSetting("BufferAndSendStatusUpdatesWhenConnect", "Buffer status updates when disconnected and send them when connecting", false, false, true, true, true, true, true, true);

#endif
      AddSetting("UseStrictProtocolAnalysis", "Use strict and unforgiving protocol parsing", false, true, true, true, true, true, true, true);
      AddSetting("UseCaseSensitiveIds", "Use case sensitive lookup for object id's and references", false, true, true, true, true, true, true, true);
      AddSetting("UseCaseSensitiveValue", "Use case sensitive value", false, false, false, false, false, true, true, true);
      AddSetting("DontAckPackets", "Never Ack or NAck packets", false);
      AddSetting("ResendUnackedPackets", "Resend unacked packets", false);
      AddSetting("WaitInfiniteForUnackedPackets", "Wait infinite for packet Ack / NAcks", false);
      AddSetting("JSonPropertyCaseChange10", "Change JSon property name characters randomly to ucase/lcase (10% change rate)", false);
      AddSetting("DropBytesInNegotiationPackets10", "Drop random bytes in negotiation packets (10% dropped)", false);
      AddSetting("CloseConnectionIfNegotiationIsOutOfSequence", "Close connection if negotiation is out of sequence", true);

      AddSetting("SendWatchdogPacketCyclically", "Send Watchdog packets cyclically", true);
      AddSetting("ExpectWatchdogPackets", "Expect Watchdog packets", true);

      AddSetting("UseEncryption", "Use encryption (TLS)", false);

      RSMPGS.MainForm.dataGridView_Behaviour.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

    }

    private static void AddSetting(string sKey, string sDescription, bool bDefaultValue)
    {
      AddSetting(sKey, sDescription, false, bDefaultValue, false, false, false, false, false, false, false, false);
    }

    private static void AddSetting(string sKey, string sDescription, bool bRSMP_3_1_1, bool bRSMP_3_1_2, bool bRSMP_3_1_3, bool bRSMP_3_1_4, bool bRSMP_3_1_5, bool bRSMP_3_2_0, bool bRSMP_3_2_1, bool bRSMP_3_2_2)
    {
      AddSetting(sKey, sDescription, true, false, bRSMP_3_1_1, bRSMP_3_1_2, bRSMP_3_1_3, bRSMP_3_1_4, bRSMP_3_1_5, bRSMP_3_2_0, bRSMP_3_2_1, bRSMP_3_2_2);
    }

    private static void AddSetting(string sKey, string sDescription, bool IsAffectedByRSMPVersion, bool bDefaultValue, bool bRSMP_3_1_1, bool bRSMP_3_1_2, bool bRSMP_3_1_3, bool bRSMP_3_1_4, bool bRSMP_3_1_5, bool bRSMP_3_2_0, bool bRSMP_3_2_1, bool bRSMP_3_2_2)
   {

      int iRowIndex = RSMPGS.MainForm.dataGridView_Behaviour.Rows.Add(sDescription, bDefaultValue, bRSMP_3_1_1, bRSMP_3_1_2, bRSMP_3_1_3, bRSMP_3_1_4, bRSMP_3_1_5, bRSMP_3_2_0, bRSMP_3_2_1, bRSMP_3_2_2);

      //RSMPGS.MainForm.dataGridView_Behaviour.Rows.Add(
      //RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[2]

      cSetting Setting = new cSetting(sKey, sDescription, iRowIndex, IsAffectedByRSMPVersion, bDefaultValue, bRSMP_3_1_1, bRSMP_3_1_2, bRSMP_3_1_3, bRSMP_3_1_4, bRSMP_3_1_5, bRSMP_3_2_0, bRSMP_3_2_1, bRSMP_3_2_2);

      RSMPGS.Settings.Add(sKey, Setting);

      RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Tag = Setting;

      // Prevent blue for text
      RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Cells[0].Style.SelectionBackColor = RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Cells[0].Style.BackColor;

      if (IsAffectedByRSMPVersion)
      {
        int iColumnIndex;

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_1);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_RSMP_3_1_1", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_2);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_RSMP_3_1_2", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_3);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_RSMP_3_1_3", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_4);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_RSMP_3_1_4", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_5);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_RSMP_3_1_5", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_0);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_RSMP_3_2_0", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_1);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_RSMP_3_2_1", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_2);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_RSMP_3_2_2", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.NotSupported));

      }
      else
      {

        int iColumnIndex;

        iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.NotSupported);
        Setting.SetActualValue(iColumnIndex, cPrivateProfile.GetIniFileInt("Behaviour_Other", sKey, Setting.GetDefaultValue(iColumnIndex) ? 1 : 0) != 0);
        RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetActualValue(iColumnIndex);

        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_1));
        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_2));
        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_3));
        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_4));
        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_5));
        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_0));
        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_1));
        HideSettingCell(iRowIndex, Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_2));
      }

      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.NotSupported);

      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.RSMP_3_1_1);
      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.RSMP_3_1_2);
      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.RSMP_3_1_3);
      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.RSMP_3_1_4);
      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.RSMP_3_1_5);
      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.RSMP_3_2_0);
      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.RSMP_3_2_1);
      ApplySettingBackColor(iRowIndex, cJSon.RSMPVersion.RSMP_3_2_2);

      //RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Cells[1].Style.BackColor = Color.Red;

      //RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Cells[1].Style.SelectionBackColor = Color.;

    }

    public static void HideSettingCell(int iRowIndex, int iColumnIndex)
    {
      RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Cells[iColumnIndex].Value = false;
      RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Cells[iColumnIndex] = new DataGridViewTextBoxCell();
      RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Cells[iColumnIndex].Value = "";
    }

    public static void ApplySettingBackColor(int iRowIndex, cJSon.RSMPVersion rsmpVersion)
    {

      if (iRowIndex == -1)
      {
        return;
      }

      cSetting setting = (cSetting)RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Tag;

      ApplySettingBackColor(setting, setting.GetColumnIndex(rsmpVersion), setting.GetActualValue(rsmpVersion), setting.GetDefaultValue(rsmpVersion));

    }

    public static void ApplySettingBackColor(int iRowIndex, int iColumnIndex)
    {

      if (iRowIndex == -1)
      {
        return;
      }

      cSetting setting = (cSetting)RSMPGS.MainForm.dataGridView_Behaviour.Rows[iRowIndex].Tag;

      ApplySettingBackColor(setting, iColumnIndex, setting.GetActualValue(iColumnIndex), setting.GetDefaultValue(iColumnIndex));

    }

    public static void ApplySettingBackColor(cSetting setting, int iColumnIndex, bool bValue, bool bDefaultValue)
    {

      Color backColor = SystemColors.Window;

      if (setting.sKey.Equals("AllowUseRSMPVersion", StringComparison.OrdinalIgnoreCase) == false)
      {
        backColor = bValue != bDefaultValue ? Color.Red : SystemColors.Window;
      }

      RSMPGS.MainForm.dataGridView_Behaviour.Rows[setting.RowIndex].Cells[iColumnIndex].Style.BackColor = backColor;
      RSMPGS.MainForm.dataGridView_Behaviour.Rows[setting.RowIndex].Cells[iColumnIndex].Style.SelectionBackColor = backColor;
    }

    public static bool IsSettingChecked(string sKey)
    {

      cJSon.RSMPVersion rsmpVersion = RSMPGS.JSon.NegotiatedRSMPVersion;

      cSetting setting = RSMPGS.Settings[sKey];

      if (setting.IsAffectedByRSMPVersion)
      {
        if (rsmpVersion == cJSon.RSMPVersion.NotSupported)
        {
          rsmpVersion = RSMPGS.JSon.FindOutHighestCheckedRSMPVersion();
          bool bValue;
          if (rsmpVersion == cJSon.RSMPVersion.NotSupported)
          {
            bValue = false;
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Setting '{0}' could not be fetched as we have not negotiated any version, we will use '{1}'", setting.sDescription, bValue);
          }
          else
          {
            bValue = setting.GetActualValue(rsmpVersion);
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Setting '{0}' could not be fetched as we have not negotiated any version, we will use '{1}' from {2}", setting.sDescription, bValue, RSMPGS.JSon.sRSMPVersions[(int)rsmpVersion]);
          }
          return bValue;
        }
        else
        {
          return setting.GetActualValue(rsmpVersion);
        }
      }
      else
      {
        return setting.GetActualValue(cJSon.RSMPVersion.NotSupported);
      }

    }

    public static void SettingCheckChanged(DataGridViewCellEventArgs e)
    {

      if (e.RowIndex < 0)
      {
        return;
      }

      if (RSMPGS.MainForm.bIsLoading)
      {
        return;
      }

      try
      {
        cSetting setting = (cSetting)RSMPGS.MainForm.dataGridView_Behaviour.Rows[e.RowIndex].Tag;
        if (RSMPGS.MainForm.dataGridView_Behaviour.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(bool))
        {
          bool bValue = (bool)RSMPGS.MainForm.dataGridView_Behaviour.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
          //bool pValue = setting.GetActualValue(e.ColumnIndex);
          setting.SetActualValue(e.ColumnIndex, bValue);
          //Debug.WriteLine(setting.sKey + "." + setting.GetRSMPVersion(e.ColumnIndex) + ", " + pValue + " -> " + bValue);
          cHelper.ApplySettingBackColor(e.RowIndex, e.ColumnIndex);
        }
      }
      catch
      {
      }

    }

    public static void SaveRSMPSettings()
    {

      foreach (string sKey in RSMPGS.Settings.Keys)
      {
        cSetting Setting = RSMPGS.Settings[sKey];

        if (Setting.IsAffectedByRSMPVersion)
        {
          cPrivateProfile.WriteIniFileInt("Behaviour_RSMP_3_1_1", sKey, Setting.GetActualValue(cJSon.RSMPVersion.RSMP_3_1_1) ? 1 : 0);
          cPrivateProfile.WriteIniFileInt("Behaviour_RSMP_3_1_2", sKey, Setting.GetActualValue(cJSon.RSMPVersion.RSMP_3_1_2) ? 1 : 0);
          cPrivateProfile.WriteIniFileInt("Behaviour_RSMP_3_1_3", sKey, Setting.GetActualValue(cJSon.RSMPVersion.RSMP_3_1_3) ? 1 : 0);
          cPrivateProfile.WriteIniFileInt("Behaviour_RSMP_3_1_4", sKey, Setting.GetActualValue(cJSon.RSMPVersion.RSMP_3_1_4) ? 1 : 0);
          cPrivateProfile.WriteIniFileInt("Behaviour_RSMP_3_1_5", sKey, Setting.GetActualValue(cJSon.RSMPVersion.RSMP_3_1_5) ? 1 : 0);
          cPrivateProfile.WriteIniFileInt("Behaviour_RSMP_3_2_0", sKey, Setting.GetActualValue(cJSon.RSMPVersion.RSMP_3_2_0) ? 1 : 0);
          cPrivateProfile.WriteIniFileInt("Behaviour_RSMP_3_2_1", sKey, Setting.GetActualValue(cJSon.RSMPVersion.RSMP_3_2_1) ? 1 : 0);
          cPrivateProfile.WriteIniFileInt("Behaviour_RSMP_3_2_2", sKey, Setting.GetActualValue(cJSon.RSMPVersion.RSMP_3_2_2) ? 1 : 0);
        }
        else
        {
          cPrivateProfile.WriteIniFileInt("Behaviour_Other", sKey, Setting.GetActualValue(cJSon.RSMPVersion.NotSupported) ? 1 : 0);
        }

      }

    }

    public static void ResetRSMPSettingToDefault()
    {
      int iColumnIndex;

      foreach (cSetting Setting in RSMPGS.Settings.Values)
      {
        if (Setting.IsAffectedByRSMPVersion)
        {
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_1);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_2);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_3);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_4);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_1_5);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_0);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_1);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.RSMP_3_2_2);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
        }
        else
        {
          iColumnIndex = Setting.GetColumnIndex(cJSon.RSMPVersion.NotSupported);
          RSMPGS.MainForm.dataGridView_Behaviour.Rows[Setting.RowIndex].Cells[iColumnIndex].Value = Setting.GetDefaultValue(iColumnIndex);
        }

      }
    }

    public static void AddStatistics()
    {
      AddStatistic("TxPackets", "Sent data", "packets");
      AddStatistic("RxPackets", "Received data", "packets");
      AddStatistic("TxBytes", "Sent data", "bytes");
      AddStatistic("RxBytes", "Received data", "bytes");
      AddStatistic("TxAvLength", "Average sent packet length", "bytes");
      AddStatistic("RxAvLength", "Average received packet length", "bytes");
      AddStatistic("TxRTTimeInMsec", "Roundtrip time", "msec");
      AddStatistic("TxAvRTTimeInMsec", "Average Roundtrip time", "msec");

      // Used as memory variable for calc
      RSMPGS.Statistics.Add("TxRTTimeNoOfPackets", 0);
      RSMPGS.Statistics.Add("TxRTTimeTotalTimeInMsec", 0);


    }

    public static void AddStatistic(string sKey, string sDescription, string sUnit)
    {

      ListViewItem lvItem = RSMPGS.MainForm.listView_Statistics.Items.Add(sKey, sDescription, -1);

      lvItem.SubItems.Add("");
      lvItem.SubItems.Add(sUnit);

      RSMPGS.Statistics.Add(sKey, 0);

    }

    public static void ClearStatistics()
    {
      List<string> sKeys = new List<string>(RSMPGS.Statistics.Keys);
      foreach (string sKey in sKeys)
      {
        RSMPGS.Statistics[sKey] = 0;
        UpdateStatisticsRow(sKey, "");
      }

    }

    public static void UpdateStatistics(int iInterval)
    {

      int iLastConnectedStatus = -1;

      UpdateStatisticsRow("TxPackets", RSMPGS.Statistics["TxPackets"].ToString());
      UpdateStatisticsRow("RxPackets", RSMPGS.Statistics["RxPackets"].ToString());
      UpdateStatisticsRow("TxBytes", RSMPGS.Statistics["TxBytes"].ToString());
      UpdateStatisticsRow("RxBytes", RSMPGS.Statistics["RxBytes"].ToString());

      UpdateStatisticsRow("TxRTTimeInMsec", RSMPGS.Statistics["TxRTTimeInMsec"].ToString("n1"));

      if (RSMPGS.Statistics["TxPackets"] > 0)
      {
        UpdateStatisticsRow("TxAvLength", (RSMPGS.Statistics["TxBytes"] / RSMPGS.Statistics["TxPackets"]).ToString("n0"));
      }
      if (RSMPGS.Statistics["RxPackets"] > 0)
      {
        UpdateStatisticsRow("RxAvLength", (RSMPGS.Statistics["RxBytes"] / RSMPGS.Statistics["RxPackets"]).ToString("n0"));
      }

      if (RSMPGS.Statistics["TxRTTimeNoOfPackets"] > 0)
      {
        UpdateStatisticsRow("TxAvRTTimeInMsec", (RSMPGS.Statistics["TxRTTimeTotalTimeInMsec"] / RSMPGS.Statistics["TxRTTimeNoOfPackets"]).ToString("n1"));
      }

      RSMPGS.DebugConnection.DebugConnectionStatisticsTimer += iInterval;

      if (RSMPGS.DebugConnection.DebugConnectionStatisticsTimer >= 1000 || iLastConnectedStatus != RSMPGS.RSMPConnection.ConnectionStatus())
      {

        iLastConnectedStatus = RSMPGS.RSMPConnection.ConnectionStatus();

        string sStatisticPacket = "S" + "\t" + ((iLastConnectedStatus == cTcpSocket.ConnectionStatus_Connected) ? "connected" : "");

        foreach (ListViewItem lvItem in RSMPGS.MainForm.listView_Statistics.Items)
        {
          sStatisticPacket += "\t" + lvItem.SubItems[1].Text;
        }

        RSMPGS.DebugConnection.SendPacket(sStatisticPacket);
        RSMPGS.DebugConnection.DebugConnectionStatisticsTimer = 0;

      }

    }

    public static void UpdateStatisticsRow(string sColumnKey, string sNewValue)
    {
      try
      {
        // Prevent flicker
        ListViewItem lvItem = RSMPGS.MainForm.listView_Statistics.Items[sColumnKey];
        if (lvItem.SubItems[1].Text != sNewValue)
        {
          RSMPGS.MainForm.listView_Statistics.Items[sColumnKey].SubItems[1].Text = sNewValue;
        }
      }
      catch
      {
      }
    }

    public static void ChangeJSONPropertiesCasing(object obj, ref string sJSon, int iErrorPercentage)
    {

      Random rnd = new Random();

      Type T = obj.GetType();

      FieldInfo[] fields = T.GetFields();

      char[] chArray = sJSon.ToCharArray();

      foreach (FieldInfo field in fields)
      {
        int iStartIndex = sJSon.IndexOf("\"" + field.Name + "\":", StringComparison.OrdinalIgnoreCase);

        if (iStartIndex >= 0)
        {
          for (int iCharIndex = 0; iCharIndex < field.Name.Length; iCharIndex++)
          {
            if (iErrorPercentage > rnd.Next(0, 100))
            {
              char c = chArray[iStartIndex + iCharIndex + 1];
              c = Char.IsLower(c) ? char.ToUpper(c) : char.ToLower(c);
              chArray[iStartIndex + iCharIndex + 1] = c;
            }
          }
        }
      }
      sJSon = new string(chArray);
    }

    public static string Item(string sString, int iItem, char chDelimiter)
    {

      string[] sItems = sString.Split(chDelimiter);

      if (sItems.GetLength(0) > iItem)
      {
        return sItems[iItem];
      }
      else
      {
        return "";
      }
    }

    public static string DOSAscii2ISOLatin1(string InString)
    {

      string OutString = InString;

      OutString = OutString.Replace("\x8e", "Ä");
      OutString = OutString.Replace("\x8f", "Å");
      OutString = OutString.Replace("\x99", "Ö");

      OutString = OutString.Replace("\x84", "ä");
      OutString = OutString.Replace("\x86", "å");
      OutString = OutString.Replace("\x94", "ö");

      OutString = OutString.Replace("\xf8", "°");

      return OutString;

    }

    public static bool IsGuid(string guidString)
    {
      // Length of a proper GUID, without any surrounding braces.
      const int len_without_braces = 36;

      // Delimiter for GUID data parts.
      const char delim = '-';

      // Delimiter positions.
      const int d_0 = 8;
      const int d_1 = 13;
      const int d_2 = 18;
      const int d_3 = 23;

      // Before Delimiter positions.
      const int bd_0 = 7;
      const int bd_1 = 12;
      const int bd_2 = 17;
      const int bd_3 = 22;

      if (guidString == null)
        return false;

      if (guidString.Length != len_without_braces)
        return false;

      if (guidString[d_0] != delim ||
          guidString[d_1] != delim ||
          guidString[d_2] != delim ||
          guidString[d_3] != delim)
        return false;

      for (int i = 0;
          i < guidString.Length;
          i = i + (i == bd_0 ||
                  i == bd_1 ||
                  i == bd_2 ||
                  i == bd_3
                  ? 2 : 1))
      {
        if (!IsHex(guidString[i])) return false;
      }

      return true;
    }

    private static bool IsHex(char c)
    {
      return ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));
    }

    public static cRoadSideObject FindRoadSideObject(string ntsOId, string cId, bool bUseCaseSensitiveIds)
    {

      StringComparison sc = bUseCaseSensitiveIds ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

      cRoadSideObject RoadSideObject = null;

      if (RSMPGS.ProcessImage.RoadSideObjects.TryGetValue(ntsOId + "\t" + cId, out RoadSideObject))
      {
        // The collection is case insensitive, ensure it has correct case if that is what we want
        if (bUseCaseSensitiveIds)
        {
          if (RoadSideObject.sNTSObjectId != ntsOId || RoadSideObject.sComponentId != cId)
          {
            RoadSideObject = null;
          }
        }
      }
      /*

      foreach (cSiteIdObject siteIdObject in RSMPGS.ProcessImage.SiteIdObjects)
      {
        cRoadSideObject RoadSideObject = siteIdObject.RoadSideObjects.Find(x => x.sNTSObjectId.Equals(ntsOId, sc) && x.sComponentId.Equals(cId, sc));
        if (RoadSideObject != null)
        {
          break;
        }
      }
      */

      return RoadSideObject;

    }


    public static string[] ConvertStringArrayFromCommaSeparatedToSemicolonSeparated(string[] sLines)
    {

      char cDelimiter = default(char);

      string[] sResultLines = new string[sLines.GetLength(0)];
      //
      // This function removes any quotes and commas and replaces them with semicolon. If semicolon is used somewhere it is replaced with comma
      // It is also right trimmed from semicolons
      //
      //
      // Rev. Datum:;2012-10-17;;;;;;;"Obs! ""-"" ska ej finnas med i fält";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
      // Rev. Datum:;2012-10-17;;;;;;;Obs! "-" ska ej finnas med i fält
      //
      // Rev. Datum:;1900-1-0;"Obs! ""-"" ska ej finnas med i fält";
      // Rev. Datum:;1900-1-0;Obs! "-" ska ej finnas med i fält
      //
      // Styrskåp,Styrskåp,O+20431=881CG001,O+20431=881CG001,,"Styrskåp, 111",,,
      // Styrskåp;Styrskåp;O+20431=881CG001;O+20431=881CG001;;Styrskåp, 111
      //
      // Kombinerad Sändare och Mottagare,Kombinerad Sändare och Mottagare DH111,O+20431=881DH111,O+20431=881CG003,,,se02x111mr011,,
      // Kombinerad Sändare och Mottagare;Kombinerad Sändare och Mottagare DH111;O+20431=881DH111;O+20431=881CG003;;;se02x111mr011
      //
      // Anläggning,Utfart,O+13133=881CG002,O+13133=881CG002,,"Utfart, CPID 082",se02x082mlc010,,
      // Anläggning;Utfart;O+13133=881CG002;O+13133=881CG002;;Utfart, CPID 082;se02x082mlc010
      //
      // col1;col2;"col3a;col3b;col3c";col4
      // col1;col2;col3a,col3b,col3c;col4
      //
      // \ncol1;col2;"col3a;col3b;col3c";col4;"col5rad1\ncol5rad2\ncol5rad3\n\n\n";col6
      // col1;col2;col3a,col3b,col3c;col4;col5rad1\ncol5rad2\ncol5rad3\n\n\n;col6
      //
      // "Obs! ""-"" ska ej finnas med i fält"
      // Obs! "-" ska ej finnas med i fält
      //
      // "test med "" bara"
      // test med " bara

      for (int iLineIndex = 0; iLineIndex < sLines.GetLength(0); iLineIndex++)
      {

        string sLine = sLines[iLineIndex].Trim('\n').Trim();

        string sResult = "";

        bool bIsInQuoteSection = false;

        for (int iIndex = 0; iIndex < sLine.Length; iIndex++)
        {

          char cCharacter = sLine[iIndex];

          if (cCharacter == '\"')
          {
            if (bIsInQuoteSection)
            {
              // Double quotes is one only (in quote section anyway)
              if (iIndex < sLine.Length - 1)
              {
                if (sLine[iIndex + 1] == '\"')
                {
                  sResult += '\"';
                  iIndex++;
                }
                else
                {
                  bIsInQuoteSection = false;
                }
              }
              else
              {
                bIsInQuoteSection = false;
              }
            }
            else
            {
              bIsInQuoteSection = true;
            }
          }
          // In quote section, just add characters as long as it is not a semicolon, we use comma instead
          // Split stuff in the processimage parsing does not manage if there are semicolons as other character than split char
          else if (bIsInQuoteSection)
          {
            if (cCharacter == ';')
            {
              sResult += ',';
            }
            else
            {
              sResult += cCharacter;
            }
          }
          else if (cDelimiter == default(char))
          {
            if (cCharacter == ';')
            {
              cDelimiter = ';';
              sResult += ';';
            }
            else if (cCharacter == ',')
            {
              cDelimiter = ',';
              sResult += ';';
            }
            else
            {
              sResult += cCharacter;
            }
          }
          else if (cCharacter == cDelimiter)
          {
            sResult += ";";
          }
          else
          {
            sResult += cCharacter;
          }
        }

        sResult = sResult.TrimEnd(';');
        sResultLines[iLineIndex] = sResult;

        //Debug.WriteLine(sLine.Replace("\n", "\\n"));
        //Debug.WriteLine(sResult.Replace("\n", "\\n"));

      }

      return sResultLines;

    }

    public static string[] SplitFields(string sInString, char cSplitter)
    {

      // "Utebel Tänd",,58e0e01d-950b-4c57-ba46-0f26b0a5eff0,AlarmEvent,Default_AlarmItem0,AlarmItem0,Normal,1,,"2016-12-05 08:11:17","2016-12-05 08:11:17","2016-12-04 15:40:41"

      bool bLastFieldWasEmpty = false;

      string sItem;
      List<string> sItems = new List<string>();

      while (sInString != "")
      {
        sItem = "";
        if (sInString.Substring(0, 1) == "\"")
        {
          int iNextQuote = -1;
          try
          {
            iNextQuote = sInString.IndexOf('\"', 1);
            if (iNextQuote >= 1)
            {
              sItem = sInString.Substring(1, iNextQuote - 1);
              sInString = sInString.Substring(iNextQuote + 1);
            }
          }
          catch
          {
            sItem = sInString;
            sInString = "";
          };
          // Cut next comma
          if (sInString != "")
          {
            sInString = sInString.Substring(1);
            bLastFieldWasEmpty = sInString == "";
          }
        }
        else
        {
          int iNextComma = sInString.IndexOf(cSplitter);
          if (iNextComma == -1)
          {
            sItem = sInString;
            sInString = "";
          }
          else
          {
            sItem = sInString.Substring(0, iNextComma);
            sInString = sInString.Substring(iNextComma + 1);
            bLastFieldWasEmpty = sInString == "";
          }
        }
        sItems.Add(sItem.Trim());
      }

      if (bLastFieldWasEmpty)
      {
        sItems.Add("");
      }

      return sItems.ToArray();

    }

  }

  public class cDebugConnection
  {

    public TcpClient DebugConnectionTcpClient = null;
    public NetworkStream DebugConnectionNetworkStream = null;
    public int DebugConnectionStatisticsTimer = 0;

    private IPAddress ipDebugServerAddress = null;
    private int iDebugServerPort = 0;

    private bool ThreadIsRunning = false;
    private bool ThreadShouldRun = false;

    public cDebugConnection(string sDebugName, string sDebugServer)
    {

      if (sDebugName == "" || sDebugServer == "")
      {
        return;
      }

      try
      {
        ipDebugServerAddress = IPAddress.Parse(sDebugServer.Split(':')[0]);
      }
      catch
      {
        try
        {

          IPHostEntry host = Dns.GetHostEntry(sDebugServer.Split(':')[0]);

          // Lookup some IPv4 address (fails in Windows 7 otherwise)
          foreach (IPAddress ip in host.AddressList)
          {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
              ipDebugServerAddress = ip;
            }
          }
        }
        catch
        {
        }
      }

      try
      {
        iDebugServerPort = Int32.Parse(sDebugServer.Split(':')[1]);
      }
      catch
      {
      }

      if (iDebugServerPort > 0 && iDebugServerPort < 65536 && ipDebugServerAddress != null)
      {
        ThreadShouldRun = true;
        ThreadIsRunning = true;
        new Thread(new ThreadStart(RunThread)).Start();
      }

    }

    public void Shutdown()
    {

      ThreadShouldRun = false;

      while (ThreadIsRunning)
      {
        cTcpHelper.CloseAndDeleteStreamAndSocket(ref DebugConnectionNetworkStream, ref DebugConnectionTcpClient);
        Thread.Sleep(100);
      }

    }

    public void SendPacket(string sPacket)
    {

      if (DebugConnectionTcpClient == null || DebugConnectionNetworkStream == null)
      {
        return;
      }

      Encoding encoding = Encoding.GetEncoding("iso-8859-1");

      byte[] SendBytes = encoding.GetBytes(sPacket + "\n");

      try
      {
        DebugConnectionNetworkStream.Write(SendBytes, 0, SendBytes.GetLength(0));
      }
      catch (Exception exc)
      {
        cTcpHelper.CloseAndDeleteStreamAndSocket(ref DebugConnectionNetworkStream, ref DebugConnectionTcpClient);
      }

    }

    public void RunThread()
    {

      byte[] bBuffer = new byte[10240];
      Encoding encoding = Encoding.GetEncoding("iso-8859-1");

      while (ThreadShouldRun)
      {
        try
        {

          string sBuffer = "";

          DebugConnectionTcpClient = new TcpClient();
          DebugConnectionTcpClient.Connect(ipDebugServerAddress, iDebugServerPort);

          DebugConnectionNetworkStream = DebugConnectionTcpClient.GetStream();

          SendPacket("I" + "\t" + RSMPGS.DebugName);

          while (ThreadShouldRun == true && DebugConnectionNetworkStream != null)
          {

            int iReadBytes = DebugConnectionNetworkStream.Read(bBuffer, 0, bBuffer.GetLength(0));

            if (iReadBytes <= 0)
            {
              break;
            }

            sBuffer += encoding.GetString(bBuffer, 0, iReadBytes);

            while (sBuffer.Contains('\n'))
            {

              string sPacket = sBuffer.Substring(0, sBuffer.IndexOf('\n'));
              string[] sPacketElements = sPacket.Split('\t');

              switch (sPacketElements[0].ToUpper())
              {

                case "C":
                  if (RSMPGS.RSMPConnection.ConnectionStatus() != cTcpSocket.ConnectionStatus_Connected)
                  {
                    RSMPGS.RSMPConnection.Connect();
                  }
                  break;

                case "D":
                  RSMPGS.RSMPConnection.Disconnect();
                  break;

                default:
                  break;

              }

              sBuffer = sBuffer.Substring(sBuffer.IndexOf('\n') + 1);

            }


          }

        }
        catch (Exception exc)
        {
          cTcpHelper.CloseAndDeleteStreamAndSocket(ref DebugConnectionNetworkStream, ref DebugConnectionTcpClient);
        }
        for (int iDelay = 0; iDelay < 10000 && ThreadShouldRun == true; iDelay += 100)
        {
          Thread.Sleep(100);
        }
      }

      cTcpHelper.CloseAndDeleteStreamAndSocket(ref DebugConnectionNetworkStream, ref DebugConnectionTcpClient);
      ThreadIsRunning = false;

    }

  }

  public class cTcpHelper
  {

    public const int WrapMethod_None = 0;
    public const int WrapMethod_FormFeed = 1;
    public const int WrapMethod_LengthPrefix = 2;

    public static bool CloseAndDeleteStreamAndSocket(ref cSocketStream socketStream, ref TcpClient tcpClient)
    {

      bool bDidCloseSomething = false;

      if (socketStream != null)
      {
        try
        {
          socketStream.Close();
          socketStream = null;
        }
        catch
        {
        }
        bDidCloseSomething = true;
      }
      if (tcpClient != null)
      {
        try
        {
          tcpClient.Close();
          tcpClient = null;
        }
        catch
        {
        }
        bDidCloseSomething = true;
      }

      return bDidCloseSomething;

    }
    public static bool CloseAndDeleteStreamAndSocket(ref NetworkStream networkStream, ref TcpClient tcpClient)
    {

      bool bDidCloseSomething = false;

      if (networkStream != null)
      {
        try
        {
          networkStream.Close();
          networkStream = null;
        }
        catch
        {
        }
        bDidCloseSomething = true;
      }
      if (tcpClient != null)
      {
        try
        {
          tcpClient.Close();
          tcpClient = null;
        }
        catch
        {
        }
        bDidCloseSomething = true;
      }

      return bDidCloseSomething;

    }

  }

  public class cJSonMessageIdAndTimeStamp
  {
    public string PacketType;
    public string MessageId;
    public string SendString;
    public DateTime TimeStamp;
    public double TimeToWaitForAck;
    public bool ResendPacketIfWeGetNoAck;

    public cJSonMessageIdAndTimeStamp(string sPacketType, string sMessageId, string sSendString, double dTimeToWaitForAck, bool bResendPacketIfWeGetNoAck)
    {
      PacketType = sPacketType;
      MessageId = sMessageId;
      SendString = sSendString;
      TimeToWaitForAck = dTimeToWaitForAck;
      TimeStamp = DateTime.Now;
      ResendPacketIfWeGetNoAck = bResendPacketIfWeGetNoAck;
    }

    public bool IsPacketToOld()
    {
      if (cHelper.IsSettingChecked("WaitInfiniteForUnackedPackets"))
      {
        return false;
      }
      else
      {
        return (TimeStamp.AddMilliseconds(TimeToWaitForAck) < DateTime.Now) ? true : false;
      }
    }

  }


  public class ListViewColumnSorter : IComparer
  {
    /// <summary>
    /// Specifies the column to be sorted
    /// </summary>
    private int ColumnToSort;
    /// <summary>
    /// Specifies the order in which to sort (i.e. 'Ascending').
    /// </summary>
    private SortOrder OrderOfSort;
    /// <summary>
    /// Case insensitive comparer object
    /// </summary>
    private CaseInsensitiveComparer ObjectCompare;

    /// <summary>
    /// Class constructor.  Initializes various elements
    /// </summary>
    public ListViewColumnSorter()
    {
      // Initialize the column to '0'
      ColumnToSort = 0;

      // Initialize the sort order to 'none'
      OrderOfSort = SortOrder.None;

      // Initialize the CaseInsensitiveComparer object
      ObjectCompare = new CaseInsensitiveComparer();
    }

    /// <summary>
    /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
    /// </summary>
    /// <param name="x">First object to be compared</param>
    /// <param name="y">Second object to be compared</param>
    /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
    public int Compare(object x, object y)
    {
      int compareResult;
      ListViewItem listviewX, listviewY;

      try
      {
        // Cast the objects to be compared to ListViewItem objects
        listviewX = (ListViewItem)x;
        listviewY = (ListViewItem)y;

        // Compare the two items
        compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

        // Calculate correct return value based on object comparison
        if (OrderOfSort == SortOrder.Ascending)
        {
          // Ascending sort is selected, return normal result of compare operation
          return compareResult;
        }
        else if (OrderOfSort == SortOrder.Descending)
        {
          // Descending sort is selected, return negative result of compare operation
          return (-compareResult);
        }
        else
        {
          // Return '0' to indicate they are equal
          return 0;
        }
      }
      catch(Exception exc)
      {
        return 0;
      }
    }

    /// <summary>
    /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
    /// </summary>
    public int SortColumn
    {
      set
      {
        ColumnToSort = value;
      }
      get
      {
        return ColumnToSort;
      }
    }

    /// <summary>
    /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
    /// </summary>
    public SortOrder Order
    {
      set
      {
        OrderOfSort = value;
      }
      get
      {
        return OrderOfSort;
      }
    }

  }

  internal static class ListViewExtensions
  {
    [StructLayout(LayoutKind.Sequential)]
    public struct LVCOLUMN
    {
      public Int32 mask;
      public Int32 cx;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string pszText;
      public IntPtr hbm;
      public Int32 cchTextMax;
      public Int32 fmt;
      public Int32 iSubItem;
      public Int32 iImage;
      public Int32 iOrder;
    }

    const Int32 HDI_WIDTH = 0x0001;
    const Int32 HDI_HEIGHT = HDI_WIDTH;
    const Int32 HDI_TEXT = 0x0002;
    const Int32 HDI_FORMAT = 0x0004;
    const Int32 HDI_LPARAM = 0x0008;
    const Int32 HDI_BITMAP = 0x0010;
    const Int32 HDI_IMAGE = 0x0020;
    const Int32 HDI_DI_SETITEM = 0x0040;
    const Int32 HDI_ORDER = 0x0080;
    const Int32 HDI_FILTER = 0x0100;

    const Int32 HDF_LEFT = 0x0000;
    const Int32 HDF_RIGHT = 0x0001;
    const Int32 HDF_CENTER = 0x0002;
    const Int32 HDF_JUSTIFYMASK = 0x0003;
    const Int32 HDF_RTLREADING = 0x0004;
    const Int32 HDF_OWNERDRAW = 0x8000;
    const Int32 HDF_STRING = 0x4000;
    const Int32 HDF_BITMAP = 0x2000;
    const Int32 HDF_BITMAP_ON_RIGHT = 0x1000;
    const Int32 HDF_IMAGE = 0x0800;
    const Int32 HDF_SORTUP = 0x0400;
    const Int32 HDF_SORTDOWN = 0x0200;

    const Int32 LVM_FIRST = 0x1000;         // List messages
    const Int32 LVM_GETHEADER = LVM_FIRST + 31;
    const Int32 HDM_FIRST = 0x1200;         // Header messages
    const Int32 HDM_SETIMAGELIST = HDM_FIRST + 8;
    const Int32 HDM_GETIMAGELIST = HDM_FIRST + 9;
    const Int32 HDM_GETITEM = HDM_FIRST + 11;
    const Int32 HDM_SETITEM = HDM_FIRST + 12;

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", EntryPoint = "SendMessage")]
    private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref LVCOLUMN lPLVCOLUMN);


    //This method used to set arrow icon
    public static void SetSortIcon(this ListView listView, int columnIndex, SortOrder order)
    {
      IntPtr columnHeader = SendMessage(listView.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);

      for (int columnNumber = 0; columnNumber <= listView.Columns.Count - 1; columnNumber++)
      {
        IntPtr columnPtr = new IntPtr(columnNumber);
        LVCOLUMN lvColumn = new LVCOLUMN();
        lvColumn.mask = HDI_FORMAT;

        SendMessageLVCOLUMN(columnHeader, HDM_GETITEM, columnPtr, ref lvColumn);

        if (!(order == SortOrder.None) && columnNumber == columnIndex)
        {
          switch (order)
          {
            case System.Windows.Forms.SortOrder.Ascending:
              lvColumn.fmt &= ~HDF_SORTDOWN;
              lvColumn.fmt |= HDF_SORTUP;
              break;
            case System.Windows.Forms.SortOrder.Descending:
              lvColumn.fmt &= ~HDF_SORTUP;
              lvColumn.fmt |= HDF_SORTDOWN;
              break;
          }
          lvColumn.fmt |= (HDF_LEFT | HDF_BITMAP_ON_RIGHT);
        }
        else
        {
          lvColumn.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP & ~HDF_BITMAP_ON_RIGHT;
        }

        SendMessageLVCOLUMN(columnHeader, HDM_SETITEM, columnPtr, ref lvColumn);
      }
    }
  }

  public class ListViewDoubleBuffered : ListView
  {

    public ListViewColumnSorter ColumnSorter = new nsRSMPGS.ListViewColumnSorter();

    public void StopSorting()
    {
      ListViewItemSorter = null;
    }

    public void ResumeSorting()
    {
      ListViewItemSorter = ColumnSorter;
      Sort();
    }

    public ListViewDoubleBuffered()
    {
      ListViewItemSorter = ColumnSorter;
      DoubleBuffered = true;
    }
  }


  public class cFormsHelper
  {

    public static string sFileName = "";

    static cValue currentArrayValue;
    static ListView arrayListView = new ListView();
    static int arrayListViewIndex;
    static Form arrayForm = new Form();

    public static void ColumnClick(object sender, ColumnClickEventArgs e)
    {

      ListViewDoubleBuffered listView = (ListViewDoubleBuffered)sender;

      // Determine if clicked column is already the column that is being sorted.
      if (e.Column == listView.ColumnSorter.SortColumn)
      {

        // Reverse the current sort direction for this column.
        if (listView.ColumnSorter.Order == SortOrder.Ascending)
        {
          listView.ColumnSorter.Order = SortOrder.Descending;
        }
        else
        {
          listView.ColumnSorter.Order = SortOrder.Ascending;
        }
      }
      else
      {
        // Set the column number that is to be sorted; default to ascending.
        listView.ColumnSorter.SortColumn = e.Column;
        listView.ColumnSorter.Order = SortOrder.Ascending;
      }

      // Perform the sort with these new sort options.
      listView.Sort();
      listView.SetSortIcon(listView.ColumnSorter.SortColumn, listView.ColumnSorter.Order);

    }

    public static DialogResult InputStatusBox(string title, string promptText, ref string value, string sType, string sValues, string sComment, bool bReturnCancelIfValueHasNotChanged)
    {

      Form form = new Form();
      Label label = new Label();
      ComboBox comboBox = new ComboBox();
      Button buttonOk = new Button();
      Button buttonCancel = new Button();
      Button buttonBrowse = new Button();

      form.Text = title + " (" + sType + ")";
      label.Text = promptText;
      comboBox.Text = value;
      comboBox.DropDownStyle = ComboBoxStyle.DropDown;

      if (sType.ToLower() == "boolean")
      {
        comboBox.Items.Add("true");
        comboBox.Items.Add("false");
      }
      else
      {
        if (sValues.StartsWith("["))
        {
          form.Text += " " + sValues;
        }
        else
        {
          foreach (string sValue in sValues.Split('\n'))
          {
            comboBox.Items.Add(sValue.TrimStart('-'));
          }
        }
      }

      buttonBrowse.Text = "Browse...";
      buttonCancel.Text = "Cancel";
      buttonOk.Text = "OK";

      buttonOk.DialogResult = DialogResult.OK;
      buttonCancel.DialogResult = DialogResult.Cancel;

      if (sType.ToLower() == "base64")
      {
        comboBox.SetBounds(131, 17, 260, 22);
        buttonBrowse.SetBounds(408, 13, 93, 31);
        buttonCancel.SetBounds(309, 59, 93, 31);
        buttonOk.SetBounds(408, 59, 93, 31);
        form.ClientSize = new Size(530, 100);
      }
      else
      {
        comboBox.SetBounds(131, 17, 270, 22);
        //textBox.SetBounds(12, 36, 372, 20);
        buttonBrowse.Visible = false;
        buttonCancel.SetBounds(209, 59, 93, 31);
        buttonOk.SetBounds(308, 59, 93, 31);
        form.ClientSize = new Size(430, 100);
      }

      label.SetBounds(12, 17, 88, 17);

      label.AutoSize = true;
      label.TextAlign = ContentAlignment.MiddleRight;

      comboBox.Anchor = comboBox.Anchor | AnchorStyles.Right;
      buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

      form.Controls.AddRange(new Control[] { label, comboBox, buttonOk, buttonCancel, buttonBrowse });
      form.FormBorderStyle = FormBorderStyle.FixedDialog;
      form.StartPosition = FormStartPosition.CenterScreen;
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.AcceptButton = buttonOk;
      form.CancelButton = buttonCancel;

      buttonBrowse.Click += new System.EventHandler(buttonBrowse_Click);

      comboBox.SelectedIndexChanged += new System.EventHandler(InputStatusBoxComboBox_SelectionChanged);
      comboBox.TextChanged += new System.EventHandler(InputStatusBoxComboBox_TextChanged);

      comboBox.Tag = new nsRSMPGS.cInputBoxValue(sType, sValues);

      DialogResult dialogResult = form.ShowDialog();

      if (bReturnCancelIfValueHasNotChanged)
      {
        // Changed value ?
        if (comboBox.Text.Equals(value))
        {
          return DialogResult.Cancel;
        }
        else
        {
          value = comboBox.Text;
          return dialogResult;
        }
      }
      else
      {
        value = comboBox.Text;
        return dialogResult;
      }

    }

    public static DialogResult InputStatusBoxValueType(string title, ref string value, ref List<Dictionary<string, object>> list, cValue Value, string sComment, bool bReturnCancelIfValueHasNotChanged, bool bReadOnly)
    {

      Form form = new Form();
      //Label label = new Label();
      ComboBox comboBox = new ComboBox();
      Button buttonOk = new Button();
      Button buttonCancel = new Button();
      Button buttonBrowse = new Button();
      TextBox textBox = new TextBox();
      CheckBox checkBoxUpdatedEvenIfValueNotChanged = new CheckBox( );
      checkBoxUpdatedEvenIfValueNotChanged.Checked = !bReturnCancelIfValueHasNotChanged;

      form.Text = title + " (" + Value.GetValueType() + ")";

      if (Value.ValueTypeObject.GetValueMax() > 0)
      {
        form.Text += " / [" + Value.ValueTypeObject.GetValueMin().ToString() + "-" + Value.ValueTypeObject.GetValueMax().ToString() + "]";
      }

      //label.Text = promptText;
      textBox.Text = sComment.Replace("\r", "").Replace("\n", "\r\n");

      if(value.GetType() == typeof(string))
      {
        comboBox.Text = (string)value;
      }
      comboBox.Name = "comboBox";
      comboBox.DropDownStyle = ComboBoxStyle.DropDown;
      

      textBox.Multiline = true;
      textBox.ReadOnly = true;

      if (Value.ValueTypeObject.ValueType == cValueTypeObject.eValueType._boolean)
      {
        comboBox.Items.Add("true");
        comboBox.Items.Add("false");
      }
      else if (Value.ValueTypeObject.ValueType == cValueTypeObject.eValueType._array)
      {
        arrayListView = new ListView();
        arrayListView.MultiSelect = false;
        arrayListView.FullRowSelect = true;
        arrayListView.ItemActivate += new EventHandler(updateArrayRow);
        arrayListView.SelectedIndexChanged += new EventHandler(selectArrayRow);
        currentArrayValue = Value;

        // Add "new item"
        Button buttonNewRow = new Button();
        buttonNewRow.Click += new EventHandler(newArrayRow);
        buttonNewRow.Text = "New item";

        // Add "delete item"
        Button buttonDeleteRow = new Button();
        buttonDeleteRow.Click += new EventHandler(deleteArrayRow);
        buttonDeleteRow.Name = "buttonDeleteRow";
        buttonDeleteRow.Text = "Delete item";
        buttonDeleteRow.Enabled = false;

        // Add "refresh"
        Button buttonRefresh = new Button();
        buttonRefresh.Click += new EventHandler(refeshArrayDisplay);
        buttonRefresh.Text = "Refresh";

        if (bReadOnly)
        {
          buttonNewRow.Visible = false;
          buttonDeleteRow.Visible = false;
          buttonRefresh.SetBounds(165, 5, 75, 23);
        }
        else
        {
          buttonRefresh.Visible = false;
        }
        arrayListView.View = View.Details;
        buttonNewRow.SetBounds(5, 5, 75, 23);
        buttonDeleteRow.SetBounds(85, 5, 75, 23);
        buttonCancel.SetBounds(165, 5, 75, 23);
        buttonOk.SetBounds(245, 5, 70, 23);
        // arrayListView.Bounds = new Rectangle(new Point(5, 33), new Size(310, 160));
        arrayListView.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        arrayListView.Location = new Point( 5, 33 );
        arrayListView.Size = new Size(275, 210);

        initColumnsArray(Value.ValueTypeObject.Items);
        loadArray(Value.GetArray());
        form.Controls.AddRange(new Control[] { buttonOk, buttonNewRow, buttonDeleteRow, buttonRefresh, buttonCancel, arrayListView });
      }
      else
      {
        if (Value.ValueTypeObject.SelectableValues != null)
        {
          foreach (KeyValuePair<string, string> kvp in Value.ValueTypeObject.SelectableValues)
          {
            string sItem = kvp.Key;
            if (kvp.Value != "")
            {
              //sItem += "\t" + kvp.Value;
            }
            comboBox.Items.Add(sItem);
          }
        }
      }

      buttonBrowse.Text = "Browse...";
      buttonCancel.Text = "Cancel";

      buttonOk.DialogResult = DialogResult.OK;
      buttonCancel.DialogResult = DialogResult.Cancel;

      if (bReadOnly)
      {
        buttonOk.Text = "Close";
        buttonCancel.Visible = false;
        checkBoxUpdatedEvenIfValueNotChanged.Enabled = false;
      }
      else
      {
        buttonOk.Text = "OK";
      }

      //      comboBox.SetBounds(12, 112, 183, 21);
      comboBox.SetBounds(12, 142, 183, 21);
      if (Value.ValueTypeObject.ValueType == cValueTypeObject.eValueType._array)
      {
        form.ClientSize = new Size(320, 200);
      }
      else
      {
        form.ClientSize = new Size(400, 170);
        checkBoxUpdatedEvenIfValueNotChanged.Text = "Considered updated even if value not changed";
        checkBoxUpdatedEvenIfValueNotChanged.SetBounds(12, 110, 345, 20);
        buttonCancel.SetBounds(202, 140, 75, 23);
        buttonOk.SetBounds(283, 140, 75, 23);
      }
      buttonBrowse.SetBounds(283, 73, 93, 31);

      if (Value.ValueTypeObject.ValueType == cValueTypeObject.eValueType._base64)
      {
        textBox.SetBounds(12, 10, 265, 86);
      }
      else
      {
        textBox.SetBounds(12, 10, 345, 86);
        buttonBrowse.Visible = false;
      }

      //comboBox.Anchor = comboBox.Anchor | AnchorStyles.Right;
      //buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      //buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      //buttonBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

      if (Value.ValueTypeObject.ValueType != cValueTypeObject.eValueType._array)
      {
        form.Controls.AddRange(new Control[] { comboBox, buttonOk, buttonCancel, buttonBrowse, textBox, checkBoxUpdatedEvenIfValueNotChanged });
      }

      form.FormBorderStyle = (Value.ValueTypeObject.ValueType == cValueTypeObject.eValueType._array) ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog;
      form.StartPosition = FormStartPosition.CenterScreen;
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.AcceptButton = buttonOk;
      form.CancelButton = buttonCancel;

      buttonBrowse.Click += new System.EventHandler(buttonBrowse_Click);

      comboBox.SelectedIndexChanged += new System.EventHandler(InputStatusBoxComboBoxValueType_SelectionChanged);
      comboBox.TextChanged += new System.EventHandler(InputStatusBoxComboBoxValueType_TextChanged);

      comboBox.Tag = Value.ValueTypeObject;

      DialogResult dialogResult = form.ShowDialog();

      if (Value.ValueTypeObject.ValueType == cValueTypeObject.eValueType._array)
      {
        list = getItems();
        value = "(array)";
        return dialogResult;
      }

      if( !checkBoxUpdatedEvenIfValueNotChanged.Checked )
      {
        // Changed value ?
        if (comboBox.Text.Equals(value))
        {
          return DialogResult.Cancel;
        }
        else
        { 
          value = comboBox.Text;
        }
      }
      else
      {
        value = comboBox.Text;
      }

      if (Value.ValueTypeObject.ValueType == cValueTypeObject.eValueType._boolean)
      {
        if (value == "1")
        {
          value = "true";
        }
        else if (value == "0")
        {
          value = "false";

        }
      }

      return dialogResult;
    }

    // splitted columns init and load items (to not loose columns resized on 'refresh')
    private static void initColumnsArray(Dictionary<string, cYAMLMapping> items)
    {
      // Add column headers
      foreach (var item in items)
      {
        ColumnHeader col = new ColumnHeader();
        col.Text = item.Key;
        arrayListView.Columns.Add(col);
      }
    }
    private static void loadArray(List<Dictionary<string, object>> array)
    {
      // Exit if no values entered
      if (array == null)
      {
        return;
      }

      ListViewItem listViewItem;
      ListViewSubItem listViewSubItem;

      // Add each item to list
      foreach (Dictionary<string, object> item in array)
      {
        listViewItem = new ListViewItem();

        // Find matching column
        foreach (ColumnHeader col in arrayListView.Columns)
        {
          listViewSubItem = new ListViewSubItem();

          object value = null;
          item.TryGetValue(col.Text, out value);
          if (value != null)
          {
            if (col.Index > 0)
            {
              listViewSubItem.Text = value.ToString();
              listViewSubItem.Tag = "True";
            }
            else
            {
              listViewItem.Text = value.ToString();
              listViewItem.Tag = "True";
            }
          }

          if (col.Index > 0)
          {
            listViewItem.SubItems.Add(listViewSubItem);
          }
        }
        arrayListView.Items.Add(listViewItem);
      }
    }

    private static List<Dictionary<string, object>> getItems()
    {
      List<Dictionary<string, object>> array = new List<Dictionary<string, object>>();

      int col;
      Dictionary<string, object> item;
      foreach (ListViewItem listItem in arrayListView.Items)
      {
        col = 0;
        item = new Dictionary<string, object>();
        foreach (ListViewSubItem subitem in listItem.SubItems)
        {
          string sName = arrayListView.Columns[col].Text;
          string sValue = listItem.SubItems[col].Text;
          object oValue;
          string sType = "";

          Dictionary<string, cYAMLMapping> items = currentArrayValue.ValueTypeObject.Items;
          foreach (string i in items.Keys)
            if (i == sName)
              sType = items[i].YAMLScalars["type"];

          switch (sType.ToLower())
          {
            case "integer":
              int iValue;
              if (int.TryParse(sValue, out iValue))
                oValue = iValue;
              else
                oValue = null;
              break;
            case "number":
            case "long":
              int lValue;
              if (int.TryParse(sValue, out lValue))
                oValue = lValue;
              else
                oValue = null;
              break;
            case "boolean":
              if (sValue.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                oValue = true;
              else
                oValue = false;
              break;
            default:
              oValue = sValue;
              break;
          }

          if((col == 0) && (listItem.Tag != null) && (listItem.Tag.ToString() == "True"))
          {
            item.Add(sName, oValue);
          }
          
          if((col > 0) && (listItem.SubItems[col].Tag != null) && (listItem.SubItems[col].Tag.ToString() == "True"))
          { 
            item.Add(sName, oValue);
          }
          col++;
        }
        array.Add(item);
      }
      
      return array;
    }

    private static void newArrayRow(object sender, EventArgs e)
    {
      inputArrayRow(-1);
    }

    private static void updateArrayRow(object sender, EventArgs e)
    {
      int i = arrayListView.SelectedIndices[0];
      inputArrayRow(i);
    }

    private static void refeshArrayDisplay(object sender, EventArgs e)
    {
      arrayListView.Items.Clear();
      loadArray(currentArrayValue.GetArray());
    }

    private static void selectArrayRow(object sender, EventArgs e)
    {
      ListView send = (ListView)sender;
      Control btn = (Control)send.Parent.Controls.Find("buttonDeleteRow", false).First();
      if (arrayListView.SelectedItems.Count > 0)
      { 
        btn.Enabled = true;
      }
      else
      {
        btn.Enabled = false;
      }
    }

    // index at -1 to add a new item in array
    private static void inputArrayRow(int index)
    {
      arrayListViewIndex = index;
      arrayForm.Controls.Clear();

      // Create the ToolTip
      ToolTip toolTipInputArray = new ToolTip();

      Button buttonOk = new Button();
      Button buttonCancel = new Button();

      buttonCancel.Text = "Cancel";
      buttonOk.Text = "OK";
      buttonOk.Click += new EventHandler(saveArrayRow);

      arrayForm.Text = "Item";
      arrayForm.FormBorderStyle = FormBorderStyle.FixedDialog;
      arrayForm.StartPosition = FormStartPosition.CenterScreen;
      arrayForm.MinimizeBox = false;
      arrayForm.MaximizeBox = false;
      arrayForm.AcceptButton = buttonOk;
      arrayForm.CancelButton = buttonCancel;
      arrayForm.Controls.AddRange(new Control[] { buttonOk, buttonCancel });

      Label label;
      CheckBox sendCheckBox;
      TextBox textBox;
      NumericUpDown numericUpDown;
      ComboBox comboBox;
      bool isCombo;

      int y = 10;

      Dictionary<string, cYAMLMapping> items = currentArrayValue.ValueTypeObject.Items;

      // YAMLMapping
      KeyValuePair<string, cYAMLMapping> item;
      string schemaKey;
      cYAMLMapping schemaValue;
      string schemaDescription = "";

      // YAMLScalar
      Dictionary<string, string> schemaScalars;
      KeyValuePair<string, string> schemaScalar;
      string schemaScalarType;
      Boolean schemaScalarOptional;
      string schemaScalarMin;
      string schemaScalarMax;

      Dictionary<string, cYAMLMapping> schemaMappings;

      for (int iKeyIndex = 0; iKeyIndex < items.Count; iKeyIndex++)
      {

        // get YAMLMapping
        item = items.ElementAt(iKeyIndex);
        schemaKey = item.Key;
        schemaValue = item.Value;
        schemaScalars = schemaValue.YAMLScalars;
        schemaMappings = schemaValue.YAMLMappings;
        schemaScalarOptional = false;
        schemaScalarType = "";
        schemaScalarMin = "";
        schemaScalarMax = "";

        // loop YAMLScalars
        for (int j = 0; j < schemaScalars.Count; j++)
        {
          schemaScalar = schemaScalars.ElementAt(j);
          if (schemaScalar.Key == "type")
          {
            schemaScalarType = schemaScalar.Value;
          }
          if (schemaScalar.Key == "description")
          {
            schemaDescription = schemaScalar.Value;
          }
          if (schemaScalar.Key == "optional")
          {
            schemaScalarOptional = true;
          }
          if (schemaScalar.Key == "min")
          {
            schemaScalarMin = schemaScalar.Value;
          }
          if (schemaScalar.Key == "max")
          {
            schemaScalarMax = schemaScalar.Value;
          }
          if (schemaScalar.Key == "values")
          {
            schemaScalarMax = schemaScalar.Value;
          }
        }

        textBox = null;
        comboBox = null;

        isCombo = false;
        if (schemaMappings.Count > 0)
        {
          comboBox = new ComboBox();
          isCombo = true;

          foreach (KeyValuePair<string, string> scalar in schemaMappings.FirstOrDefault().Value.YAMLScalars)
          {
            schemaScalar = scalar;
            comboBox.Items.Add(schemaScalar.Key);
          }
        }

        if(schemaScalarType.ToLower() == "boolean")
        { 
          comboBox = new ComboBox();
          isCombo = true;

          comboBox.Items.Add("true");
          comboBox.Items.Add("false");
        }


        label = new Label();
        label.Text = item.Key;
        toolTipInputArray.SetToolTip(label, schemaDescription);
        label.SetBounds(10, y, 75, 23);

        sendCheckBox = new CheckBox();
        sendCheckBox.Name = "OptChk" + iKeyIndex.ToString();
        toolTipInputArray.SetToolTip(sendCheckBox,"Include optional key");
        sendCheckBox.SetBounds(270, y, 23, 23);

        if (!schemaScalarOptional)
        {
          label.Text = label.Text + " *";
          sendCheckBox.Visible = false;
          sendCheckBox.Checked = true;
        }
        else
        {
          sendCheckBox.Visible = true;
          sendCheckBox.Checked = false;
        }

        switch (schemaScalarType.ToLower())
        {
          case "base64":
          case "boolean":
          case "timestamp":
          case "string":
            if (isCombo)
            {
              comboBox.SetBounds(110, y, 150, 23);
              comboBox.Tag = schemaScalarType + "#" + schemaScalarOptional + "#" + iKeyIndex;
              comboBox.TextChanged += new System.EventHandler(arrayValue_TextChanged);
              arrayForm.Controls.AddRange(new Control[] { label, comboBox, sendCheckBox });
            }
            else
            {
              textBox = new TextBox();
              textBox.SetBounds(110, y, 150, 23);
              textBox.Tag = schemaScalarType + "#" + schemaScalarOptional + "#" + iKeyIndex;
              textBox.TextChanged += new System.EventHandler(arrayValue_TextChanged);
              arrayForm.Controls.AddRange(new Control[] { label, textBox, sendCheckBox });
            }

            if (arrayListViewIndex != -1)
            {
              if (iKeyIndex == 0)
              {
                if (arrayListView.Items[arrayListViewIndex].Tag!=null && arrayListView.Items[arrayListViewIndex].Tag.ToString() == "True")
                {
                  if (isCombo)
                  {
                    comboBox.Text = arrayListView.Items[arrayListViewIndex].Text;
                  }
                  else
                  {
                    textBox.Text = arrayListView.Items[arrayListViewIndex].Text;
                  }
                  sendCheckBox.Checked = true;
                }
              }
              else
              {
                if (arrayListView.Items[arrayListViewIndex].SubItems[iKeyIndex].Tag!=null && arrayListView.Items[arrayListViewIndex].SubItems[iKeyIndex].Tag.ToString() == "True")
                {
                  if (isCombo)
                  {
                    comboBox.Text = arrayListView.Items[arrayListViewIndex].SubItems[iKeyIndex].Text;
                  }
                  else
                  {
                    textBox.Text = arrayListView.Items[arrayListViewIndex].SubItems[iKeyIndex].Text;
                  }
                  sendCheckBox.Checked = true;
                }
              }
            }
            break;
          case "number":
          case "integer":
            numericUpDown = new NumericUpDown();
            numericUpDown.Tag = schemaScalarType + "#" + schemaScalarOptional + "#" + iKeyIndex;
            numericUpDown.TextChanged += new System.EventHandler(arrayValue_TextChanged);
            numericUpDown.SetBounds(110, y, 150, 23);
            arrayForm.Controls.AddRange(new Control[] { label, numericUpDown, sendCheckBox });

            if (schemaScalarMin != "")
            {
              numericUpDown.Minimum = Int32.Parse(schemaScalarMin);
            }
            if (schemaScalarMax != "")
            {
              numericUpDown.Maximum = Int32.Parse(schemaScalarMax);
            }


            if (arrayListViewIndex != -1)
            {
              if (iKeyIndex == 0)
              {
                if(arrayListView.Items[arrayListViewIndex].Text == "")
                {
                  sendCheckBox.Checked = false;
                }
                else
                {
                  numericUpDown.Value = Int32.Parse(arrayListView.Items[arrayListViewIndex].Text);
                  sendCheckBox.Checked = true;
                }
              }
              else
              {
                if (arrayListView.Items[arrayListViewIndex].SubItems[iKeyIndex].Text == "")
                {
                  sendCheckBox.Checked = false;
                }
                else
                {
                  numericUpDown.Value = Int32.Parse(arrayListView.Items[arrayListViewIndex].SubItems[iKeyIndex].Text);
                  sendCheckBox.Checked = true;
                }
              }
            }
            break;
          default:
            break;
        }
        y = y + 23;
      }

      buttonCancel.SetBounds(105, y + 10, 75, 23);
      buttonOk.SetBounds(185, y + 10, 75, 23);

      arrayForm.ClientSize = new Size(300, y + 45);

      DialogResult dialogResult = arrayForm.ShowDialog();
    }

    // Auto check optional checkbox if corresponding TextBox/ComboBox/NumericUpDown value modified
    private static void arrayValue_TextChanged(object sender, EventArgs e)
    {
      Control valueControl = (Control)sender;
      string tag = valueControl.Tag.ToString();
      string[] subs = tag.Split('#'); // to get key index
      if (subs.Length >= 3)
      {
        CheckBox chkControl = (CheckBox)valueControl.Parent.Controls.Find("OptChk" + subs[2], false).First();
        if (chkControl != null)
        {
          if (chkControl.Visible)
            chkControl.Checked = true;
        }
      }
    }

    private static void saveArrayRow(object sender, EventArgs e)
    {
      string value = "";
      string tag = "";
      string schemaScalarOptional;
      string schemaScalarType;
      CheckBox sendCheckBox;

      int controlIndex = 3;
      value = arrayForm.Controls[controlIndex].Text;

      sendCheckBox = (CheckBox)arrayForm.Controls[controlIndex + 1];
      tag = sendCheckBox.Checked.ToString();

      schemaScalarType = arrayForm.Controls[3].Tag.ToString().Split('#')[0];
      schemaScalarOptional = arrayForm.Controls[3].Tag.ToString().Split('#')[1];

      if (schemaScalarOptional == "False" && value == "")
      {
        MessageBox.Show(arrayForm.Controls[3].Text + " have to be filled in", "Error");
        return;
      }
      ListViewItem newItem = null;
      ListViewItem currentItem = null;

      if (arrayListViewIndex == -1)
      {
        newItem = new ListViewItem();
      }
      else
      {
        currentItem = arrayListView.Items[arrayListViewIndex];
      }

      Dictionary<string, cYAMLMapping> items = currentArrayValue.ValueTypeObject.Items;

      if (arrayListViewIndex == -1)
      {
        newItem.Text = value;
        newItem.Tag = tag;
      }
      else
      {
        currentItem.Text = value;
        currentItem.Tag = tag;
      }

      var controls = arrayForm.Controls;
      controlIndex = controlIndex + 3;

      ListViewSubItem listViewSubItem;

      for (int i = 0; i < items.Count - 1; i++)
      {
        value = controls[controlIndex].Text;
        sendCheckBox = (CheckBox)controls[controlIndex + 1];
        tag = sendCheckBox.Checked.ToString();

        schemaScalarType = controls[controlIndex].Tag.ToString().Split('#')[0];
        schemaScalarOptional = controls[controlIndex].Tag.ToString().Split('#')[1];

        if (schemaScalarOptional == "False" && value == "")
        {
          MessageBox.Show(arrayForm.Controls[controlIndex - 1].Text + " have to be filled in", "Error");
          return;
        }

        if (arrayListViewIndex == -1)
        {
          listViewSubItem = new ListViewSubItem();
          listViewSubItem.Tag = tag;
          if (tag == "True")
          {
            listViewSubItem.Text = value;
          }
          else
          {
            listViewSubItem.Text = "";
          }
          newItem.SubItems.Add(listViewSubItem);
          controlIndex = controlIndex + 3;
        }
        else
        {
          currentItem.SubItems[i + 1].Tag = tag;
          if (tag == "True")
          {
            currentItem.SubItems[i + 1].Text = value;
          }
          else
          {
            currentItem.SubItems[i + 1].Text = "";
          }
          controlIndex = controlIndex + 3;
        }
      }

      if (arrayListViewIndex == -1)
      {
        arrayListView.Items.Add(newItem);
        newItem.SubItems[0].Tag = newItem.Tag;
      }

      arrayForm.Close();
    }

    private static void deleteArrayRow(object sender, EventArgs e)
    {
      int i = arrayListView.SelectedIndices[0];
      arrayListView.Items.RemoveAt(i);
    }

    private static void InputStatusBoxComboBoxValueType_SelectionChanged(object sender, EventArgs e)
    {
      InputStatusBoxComboBoxValueType_ValidateValue((ComboBox)sender);
    }

    private static void InputStatusBoxComboBoxValueType_TextChanged(object sender, EventArgs e)
    {
      InputStatusBoxComboBoxValueType_ValidateValue((ComboBox)sender);
    }

    private static void InputStatusBoxComboBoxValueType_ValidateValue(ComboBox comboBox)
    {

      cValueTypeObject ValueTypeObject = (cValueTypeObject)comboBox.Tag;

      if (ValueTypeObject.ValidateValue(comboBox.Text))
      {
        comboBox.ForeColor = default(Color);
        comboBox.BackColor = default(Color);
      }
      else
      {
        comboBox.ForeColor = Color.White;
        comboBox.BackColor = Color.Red;
      }

    }


    private static void InputStatusBoxComboBox_SelectionChanged(object sender, EventArgs e)
    {
      InputStatusBoxComboBox_ValidateValue((ComboBox)sender);
    }

    private static void InputStatusBoxComboBox_TextChanged(object sender, EventArgs e)
    {
      InputStatusBoxComboBox_ValidateValue((ComboBox)sender);
    }

    private static void InputStatusBoxComboBox_ValidateValue(ComboBox comboBox)
    {

      cInputBoxValue InputBoxValue = (cInputBoxValue)comboBox.Tag;
      Dictionary<string, string> eNums = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

      foreach(string sValueItem in InputBoxValue.sValues.Split('\n'))
      {
        eNums.Add(sValueItem, "");
      }

      if (RSMPGS.JSon.ValidateTypeAndRange(InputBoxValue.sType, comboBox.Text, eNums))
      {
        comboBox.ForeColor = default(Color);
        comboBox.BackColor = default(Color);
      }
      else
      {
        comboBox.ForeColor = Color.White;
        comboBox.BackColor = Color.Red;
      }

    }

    public static DialogResult InputBox(string title, string promptText, ref string value, bool bAllowFileBrowse, bool bReturnCancelIfValueHasNotChanged)
    {
      return InputBox(title, promptText, ref value, bAllowFileBrowse, bReturnCancelIfValueHasNotChanged, false);
    }

    public static DialogResult InputBox(string title, string promptText, ref string value, bool bAllowFileBrowse, bool bReturnCancelIfValueHasNotChanged, bool bIsPassword)
    {

      Form form = new Form();
      Label label = new Label();
      TextBox textBox = new TextBox();
      Button buttonOk = new Button();
      Button buttonCancel = new Button();
      Button buttonBrowse = new Button();

      form.Text = title;
      label.Text = promptText;
      textBox.Text = value;
      textBox.TextAlign = HorizontalAlignment.Center;

      if (bIsPassword)
      {
        textBox.PasswordChar = '*';
      }

      buttonBrowse.Text = "Browse...";
      buttonCancel.Text = "Cancel";
      buttonOk.Text = "OK";

      buttonOk.DialogResult = DialogResult.OK;
      buttonCancel.DialogResult = DialogResult.Cancel;

      if (bAllowFileBrowse)
      {
        textBox.SetBounds(131, 17, 260, 22);
        buttonBrowse.SetBounds(408, 13, 93, 31);
        buttonCancel.SetBounds(309, 59, 93, 31);
        buttonOk.SetBounds(408, 59, 93, 31);
        form.ClientSize = new Size(530, 100);
      }
      else
      {
        textBox.SetBounds(131, 17, 270, 22);
        //textBox.SetBounds(12, 36, 372, 20);
        buttonBrowse.Visible = false;
        buttonCancel.SetBounds(209, 59, 93, 31);
        buttonOk.SetBounds(308, 59, 93, 31);
        form.ClientSize = new Size(430, 100);
      }

      label.SetBounds(12, 17, 88, 17);

      label.AutoSize = true;
      label.TextAlign = ContentAlignment.MiddleRight;

      textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
      buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

      form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel, buttonBrowse });
      form.FormBorderStyle = FormBorderStyle.FixedDialog;
      form.StartPosition = FormStartPosition.CenterScreen;
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.AcceptButton = buttonOk;
      form.CancelButton = buttonCancel;

      buttonBrowse.Click += new System.EventHandler(buttonBrowse_Click);

      DialogResult dialogResult = form.ShowDialog();

      if (bReturnCancelIfValueHasNotChanged)
      {
        // Changed value ?
        if (textBox.Text.Equals(value))
        {
          return DialogResult.Cancel;
        }
        else
        {
          value = textBox.Text;
          return dialogResult;
        }
      }
      else
      {
        value = textBox.Text;
        return dialogResult;
      }

    }

    private static void buttonBrowse_Click(object sender, EventArgs e)
    {
      Button btn = (Button)sender;

      Form frm = (Form)btn.FindForm();
      ComboBox combobox = (ComboBox)frm.Controls.Find("comboBox", false).FirstOrDefault();

      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.InitialDirectory = sFileName;
      openFileDialog.Filter = "All files|*.*";
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        sFileName = openFileDialog.FileName;
        string sBase64;

        try
        {
          byte[] Base64Bytes = null;
          // Open file for reading 
          System.IO.FileStream fsBase64 = new System.IO.FileStream(sFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
          System.IO.BinaryReader brBase64 = new System.IO.BinaryReader(fsBase64);
          long lBytes = new System.IO.FileInfo(sFileName).Length;
          Base64Bytes = brBase64.ReadBytes((Int32)lBytes);
          fsBase64.Close();
          fsBase64.Dispose();
          brBase64.Close();
          sBase64 = Convert.ToBase64String(Base64Bytes);
          if (sBase64.Length > (cTcpSocketClientThread.BUFLENGTH - 100))
          {
            RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Base64 encoded packet is too big (" + Base64Bytes.GetLength(0).ToString() + " bytes), max buffer length is " + cTcpSocketClientThread.BUFLENGTH.ToString() + " bytes");
            sBase64 = null;
          }
        }
        catch (Exception ex)
        {
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Could not base64-encode '{0}', error {1}", sFileName, ex.Message);
          sBase64 = null;
        }
        combobox.Text = sBase64;
      }
    }
  }
  public class cInputBoxValue
  {
    public string sType;
    public string sValues;
    public cInputBoxValue(string sType, string sValues)
    {
      this.sType = sType;
      this.sValues = sValues;
    }
  }


  public class UseFul
  {

    public static int Val(string sInString)
    {
      int iValue;

      int.TryParse(sInString, out iValue);

      return iValue;

    }

    public static string StringLeft(string sInString, int iLength)
    {

      if (iLength < 0)
      {
        return "";
      }

      if (iLength > sInString.Length)
      {
        return sInString;
      }

      return sInString.Substring(0, iLength);

    }

    public static string StringMid(string sInString, int iStartPos)
    {

      return StringMid(sInString, iStartPos, sInString.Length - iStartPos);

    }

    public static string StringMid(string sInString, int iStartPos, int iLength)
    {
      if (iStartPos < 0 || iLength < 0)
      {
        return "";
      }

      if (iStartPos >= sInString.Length)
      {
        return "";
      }

      if (iStartPos + iLength > sInString.Length)
      {
        return sInString.Substring(iStartPos);
      }
      return sInString.Substring(iStartPos, iLength);

    }

    public static string StringRight(string sInString, int iLength)
    {

      if (iLength <= 0)
      {
        return "";
      }

      if (iLength >= sInString.Length)
      {
        return sInString;
      }

      return sInString.Substring(sInString.Length - iLength);

    }

    public static string ItemWithQuote(string InString, int iIndex, char cSplitter)
    {

      string[] sStringArray = cHelper.SplitFields(InString, cSplitter);

      if (iIndex < 0 || iIndex >= sStringArray.GetLength(0))
      {
        return "";
      }
      else
      {
        return sStringArray[iIndex];
      }
    }

  }


}
