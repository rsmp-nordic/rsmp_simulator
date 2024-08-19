﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;

//
//
// RSMPGS1 - AcobiaFLUX AB / TroSoft AB (c) 2011-2021
//
// Date     / Sign / Vers    / Comment
// ---------------------------------------------------------------------------------------------------
// 11.09.xx / TR / 0.0.0.5   / Main
// 11.10.xx / BS / 0.0.0.6   / Converted to RSMPGS2
// 11.11.29 / TR / 0.0.0.7   / Added socket server and other stuff for direct comms with RSMPGS1
// xx.xx.xx / BS / 1.0.0.x   / Some versions
// 12.10.19 / TR / 1.0.1.0   / Added base64 debug file
// 12.10.25 / TR / 1.0.1.1   / Watchdog.wTs = CreateLocalTimeStamp() changed to CreateISO8601UTCTimeStamp()
//                           / StatusEvent.sTimeStamp = CreateISO8601UTCTimeStamp() changed to CreateLocalTimeStamp() 
// 14.09.11 / TR / 1.0.1.2   / Changed to platform target x86 to be able to debug in runtime on Win 7 64-bit
// 15.05.29 / TR / 1.0.1.3   / value upper/lowercase issues addressed
// 18.10.03 / TR / 1.0.1.4   / Changed point 16, needs to be fixed and added to RSMPGS1
// 19.02.05 / TR / 1.0.1.4 B1/ Major updates
// 19.04.14 / TR / 1.0.1.4 B2/ SysLog speed improved, some changes to JSon test forms, timestamp decoding in debug fixed, 
//                           / /Path: did not work (IniFileFullname was initialized too early and '/' was missing) (NOT RELEASED)
// 19.05.22 / TR / 1.0.1.4 B3/ Proper info when alarm object is not found, changed initial sequence expectations to not wait for watchdog packet MessageAck 
// 19.06.27 / TR / 1.0.1.4 B4/ AutoScroll in SysLog is disabled if some other than last item is selected
// 19.08.28 / TR / 1.0.1.4 B5/ Socket implemented for performance testing, IgnoreFileTimeStamps parameter added
// 20.03.09 / TR / 1.0.1.4 B6/ Lifted to VS2015 and .NET 4.6.1. Did not show all objects if SXL had empty rows. Major updates in treeview/grouping
// 20.03.17 / TR / 1.0.1.4 B7/ Cosmetic changes to form, Save As fixups, Status update fixup
// 20.03.24 / TR / 1.0.1.4 B8/ TLS (SSL) encryption implemented
// 20.03.25 / TR / 1.0.1.4 B9/ TLS (SSL) encryption updated
// 20.03.25 / TR / 1.0.1.5   / Release only
// 20.10.16 / TR / 1.0.1.6 B1/ Updates begin - release 3 updates in RSMPInterreg,Simulatorspec_v0.39.xlsx. CSV-files with " and commas etc are accepted,
//                           / significally improvement in SysLog (file handling). Group view update bugs fixed.
// 20.10.16 / TR / 1.0.1.6   / Significally improvement in Debugging. Minor updates.
// 21.09.27 / TR / 1.0.1.7   / Lockup bug in debug fixed, invariantculture in ISO8601 (pull request #22 / jakobht),
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
// 24.06.01 / DO / 1.0.6     / Use "Send aggregated status when connecting" by default in 3.1.2 #83
//                           / Fix issues with client/server configuration options #81
//                           / Replace "SUL" with "SXL" #85
//                           / Reset uRt interval when the value changes on RSMPGS 3.2.2 #86
//                           / Fix support for floating point UpdateRates #88
//                           / All arguments needs to be present #89
// xx.xx.xx / DO / 1.0.7     / 
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
    static public bool bConnectAsSocketClient = false;

    static public RSMPGSType SimulatorType = RSMPGSType.RSMPGS2;

    static public Dictionary<string, double> Statistics = new Dictionary<string, double>();
    static public Dictionary<string, cSetting> Settings = new Dictionary<string, cSetting>();   

    static public string IniFileFullname;

    static public cJSonGS2 JSon;

    static public RSMPGS_Main MainForm;

    static public List<ListViewItem> SysLogItems = new List<ListViewItem>();

    static public cEncryptionSettings EncryptionSettings = new cEncryptionSettings();

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
            if (SpecifiedPath.EndsWith("\\") == false)
            {
              SpecifiedPath += "\\";
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
      IniFileFullname = cPrivateProfile.SettingsPath() + "\\" + "RSMPGS2.ini";

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      MainForm = new RSMPGS_Main();
      Application.Run(MainForm);
    }
  }


}
