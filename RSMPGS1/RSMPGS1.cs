using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;

//
//
// RSMPGS1 - TroSoft AB (c) 2011-2021
//
// Date     / Sign / Vers    / Comment
// ---------------------------------------------------------------------------------------------------
// 11.09.xx / TR / 0.0.0.5   / Main
// 11.11.17 / BS / 1.0.0.0   / Converted to RSMPGS2
// 11.11.30 / TR / 1.0.0.1   / Some updates according to FAT wishes
// 11.11.31 / TR / 1.0.0.2   / RSMPGS 3.0.1 updates (new date/time format, SiteId array in version info)
// 12.01.24 / TR / 1.0.0.3   / Updated SUL/SXL format
// 12.01.25 / TR / 1.0.0.4   / RSMP version changed to 3.1.1
// 12.01.31 / TR / 1.0.0.5   / Removed 'type' from Alarm Returnvalues, updated CreateAndSendAlarmMessage,
//                             there was a bug in Returnvalues
// 12.02.01 / TR / 1.0.0.6   / ReturnValues fixup in Alarms tab
// 12.0x.xx / BS / 1.0.0.x   / Some Benny updates
// 12.08.15 / TR / 1.0.0.8   / Updated quality to 'undefined' if objects does not exist
// 12.10.19 / TR / 1.0.1.0   / Added base64 debug file
// 13.11.22 / TR / 1.0.1.1   / Added socket server stuff (ConnectionType and PortNumber parameters)
// 14.09.11 / TR / 1.0.1.2   / Save/Load status/aggregated status implemented, changed to platform target x86
//                             to be able to debug in runtime on Win 7 64-bit. Accepts also '\' as separator in
//                             FunctionalPosition / FunctionalState (Aggregated CSV file)
// 15.05.29 / TR / 1.0.1.3   / Value upper/lowercase issues addressed. Some minor updates on Alarm handling (send when connecting)
// 18.10.03 / TR / 1.0.1.4   / No changes yet, don't forget to change assembly version number
// 19.02.05 / TR / 1.0.1.4 B1/ Major updates
// 19.04.14 / TR / 1.0.1.4 B2/ SysLog speed improved, AggregatedStatus changed without selecting object caused error, increased debug performance,
//                           / /Path: did not work (IniFileFullname was initialized too early) (NOT RELEASED)
// 19.05.22 / TR / 1.0.1.4 B3/ Changed initial sequence expectations to not wait for watchdog packet MessageAck 
// 19.06.27 / TR / 1.0.1.4 B4/ AutoScroll in SysLog is disabled if some other than last item is selected
// 19.08.28 / TR / 1.0.1.4 B5/ Socket implemented for performance testing, IgnoreFileTimeStamps parameter added
// 20.03.09 / TR / 1.0.1.4 B6/ Lifted to VS2015 and .NET 4.6.1. Did not show all objects if SXL had empty rows. Major updates in treeview/grouping
// 20.03.17 / TR / 1.0.1.4 B7/ Cosmetic changes to form
// 20.03.24 / TR / 1.0.1.4 B8/ TLS (SSL) encryption implemented
// 20.03.25 / TR / 1.0.1.4 B9/ TLS (SSL) encryption updated
// 20.03.25 / TR / 1.0.1.5   / Release only
// 20.10.16 / TR / 1.0.1.6 B1/ Updates begin - release 3 updates in RSMPInterreg,Simulatorspec_v0.39.xlsx. CSV-files with " and commas etc are accepted,
//                           / significally improvement in SysLog (file handling). Group view update bugs fixed.
// 20.10.16 / TR / 1.0.1.6   / Significally improvement in Debugging. Minor updates.
// 21.09.27 / TR / 1.0.1.7   / Lockup bug in debug fixed, command lookup bug fixed, invariantculture in ISO8601 (pull request #22 / jakobht),
//                           / and full path of ini file when error occur at startup (pull request #23 / jakobht)
// 23.09.12 / DO / 1.0.2     / Add support for RSMP 3.2. Set quality "old" when status is buffered. Add option for checking case sensitive in values,
//                           / Add support array data type (mikaelbroms). Use semantic versioning.
// 23.10.01 / DO / 1.0.3     / Add support for RSMP 3.2.1. Add "number" and "timestamp" as data types
// 23.10.25 / DO / 1.0.4     / Fix data types in array window #69
//                           / Move "delete item" to array window #68
//                           / Select all command arguments by default when sending command #64
//                           / Don't send alarm if it is blocked #31
//                           / Don't update the alarm list when recieving alarm with old timestamp #19
// 23.12.01 / DO / 1.0.5     / Enable option for TLS 1.3 #71
//                           / Use correct SXL metadata for the YAML examples #73
//                           / Set valid initial values and valid random values #74
//                           / Don't resend unacked packets by default #76
//                           / Create a new GUID when resending a packet #77
//                           / Include SXL 1.0.7 #78
// 24.06.01 / DO / 1.0.6     / Use "Send aggregated status when connecting" by default in 3.1.2
//                           / Fix issues with client/server configuration options #81
//                           / Replace "SUL" with "SXL" #85
//                           / Reset uRt interval when the value changes on RSMPGS 3.2.2 #86
//                           / Fix support for floating point UpdateRates #88
//                           / All arguments needs to be present #89
// 24.08.19 / DO / 1.0.7     / For simple value modif, option to considerate updated even if same value. #91 (pull request #91 / marcgarba)
//                           / Modifications on array window and item edit #96 (pull request #96 / marcgarba)
//                           / When sending status, only using selected items for current object #95 (pull request #95 / marcgarba)
// 25.01.15 / DO / 1.0.8     / Added version in debug file + fixed 'unsubscription' in SystemLog (pull request #98 / marcgarba)    
//                           / Use RSMP version string "3.2.0" instead of "3.2" #101
//                           / Use strings for the state bits in aggregated status in RSMP 3.1.1 and RSMP 3.1.2 #106 
//                           / Add ability to edit ExternalAlarmCodeId #104
// 25.05.16 / DO / 1.0.9     / Treat values in SXL as case sensitive #110
//                           / For beginners, easier config per default in .ini (pull request #112 / marcgarba)
//                           / In debug window menu, new option to decode times or not + renamed some items (pull request #113 / marcgarba)
//                           / Revised main menu for debug windows (show in front / cascade / tile)  (pull request #114 / marcgarba)
//                           / Ctrl+c to copy selection in debug window + option copy data only (pull request #119 / marcgarba)
//                           / Handle undefined and unknown quality #121
// 25.05.16 / DO / 1.0.10    / Revert PR 114 since it causes issues in main window
//
//
//
// ---------------------------------------------------------------------------------------------------

namespace nsRSMPGS
{
  public static class RSMPGS
  {

    public enum RSMPGSType
    {
      RSMPGS1,
      RSMPGS2
    }

    static public cSysLogAndDebug SysLog;
    static public cDebugConnection DebugConnection;
    static public cProcessImage ProcessImage;
    static public List<RSMPGS_Debug> DebugForms;
    static public cTcpSocket RSMPConnection;
    static public string SpecifiedPath = "";
    static public string DebugName = "";
    static public string DebugServer = "";
    static public int ConnectionType;

    static public RSMPGSType SimulatorType = RSMPGSType.RSMPGS1;

    static public Dictionary<string, double> Statistics = new Dictionary<string, double>();
    static public Dictionary<string, cSetting> Settings = new Dictionary<string, cSetting>();   

    static public string IniFileFullname;

    static public cJSonGS1 JSon;

    static public cEncryptionSettings EncryptionSettings = new cEncryptionSettings();

    static public RSMPGS_Main MainForm;

    static public List<ListViewItem> SysLogItems = new List<ListViewItem>();

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {

      foreach (string sArgument in args)
      {
        //
        // Use /path:... argument to point to specific folder (settings/logfiles etc)
        //
        if (sArgument.StartsWith("/Path:", StringComparison.OrdinalIgnoreCase))
        {
          SpecifiedPath = sArgument.Substring(6).Trim();
          if (SpecifiedPath.Length > 0)
          {
            if (SpecifiedPath.EndsWith("\\"))
            {
              SpecifiedPath = SpecifiedPath.Substring(0, SpecifiedPath.Length - 1);
            }
          }
        }
        if (sArgument.StartsWith("/DebugName:", StringComparison.OrdinalIgnoreCase))
        {
          DebugName = sArgument.Substring(11).Trim();
        }
        if (sArgument.StartsWith("/DebugServer:", StringComparison.OrdinalIgnoreCase))
        {
          DebugServer = sArgument.Substring(13).Trim();
        }
      }

      // Must be initialized after Argument parsing...
      IniFileFullname = cPrivateProfile.SettingsPath() + "\\" + "RSMPGS1.ini";

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      MainForm = new RSMPGS_Main();
      Application.Run(MainForm);
    }
  }
}
