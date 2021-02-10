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

namespace nsRSMPGS
{

    public class cSysLogAndDebug
    {

        public enum Severity
        {
            Info = 0,
            Warning = 1,
            Error = 2
        }

        private int DaysToKeepLogFiles = cPrivateProfile.GetIniFileInt("RSMP", "DaysToKeepLogFiles", 365);

        public const int Direction_In = 0;
        public const int Direction_Out = 1;

        public string sSysLogFilePath;

        public string sEventFilePath;

        private int LastCleanupDay;

        public bool bEnableSysLog = true;

        private StreamWriter swSysLogFile = null;

        public cSysLogAndDebug()
        {
            sSysLogFilePath = cPrivateProfile.SysLogFilesPath();

            sEventFilePath = cPrivateProfile.EventFilesPath();

            RSMPGS.MainForm.listView_SysLog.StopSorting();

        }

        public void Shutdown()
        {
            CloseSysLogFile();
        }

        private void CloseSysLogFile()
        {
            if (swSysLogFile != null)
            {
                lock (this)
                {
                    try
                    {
                        swSysLogFile.Close();
                        swSysLogFile = null;
                    }
                    catch
                    {
                    }
                }
            }
        }

        // Delete old logfiles
        public void CyclicCleanup(int iElapsedMillisecs)
        {
            if (LastCleanupDay != DateTime.Now.Day)
            {
                DeleteLogFiles(sSysLogFilePath, "SysLog_????????.Log");
#if _RSMPGS2
                DeleteLogFiles(sEventFilePath, "AlarmEvents_????????.txt");
                DeleteLogFiles(sEventFilePath, "CommandEvents_????????.txt");
                DeleteLogFiles(sEventFilePath, "StatusEvents_????????.txt");
                DeleteLogFiles(sEventFilePath, "AggregatedStatusEvents_????????.txt");
#endif
                LastCleanupDay = DateTime.Now.Day;

            }

            lock (RSMPGS.SysLogItems)
            {
                if (RSMPGS.SysLogItems.Count > 0)
                {

                    RSMPGS.MainForm.listView_SysLog.BeginUpdate();

                    bool bShowLastItem;

                    // Show last only if it was selected or none was selected
                    if (RSMPGS.MainForm.listView_SysLog.SelectedItems.Count == 0)
                    {
                        bShowLastItem = true;
                    }
                    else
                    {
                        if (RSMPGS.MainForm.listView_SysLog.SelectedItems[0].Index == RSMPGS.MainForm.listView_SysLog.Items.Count - 1)
                        {
                            RSMPGS.MainForm.listView_SysLog.SelectedItems[0].Selected = false;
                            bShowLastItem = true;
                        }
                        else
                        {
                            bShowLastItem = false;
                        }
                    }

                    for (int iIndex = 0; iIndex < RSMPGS.SysLogItems.Count; iIndex++)
                    {
                        RSMPGS.MainForm.listView_SysLog.Items.Add(RSMPGS.SysLogItems[iIndex]);
                    }

                    while (RSMPGS.MainForm.listView_SysLog.Items.Count > 2000)
                    {
                        RSMPGS.MainForm.listView_SysLog.Items.RemoveAt(0);
                    }

                    RSMPGS.SysLogItems.Clear();

                    if (bShowLastItem)
                    {
                        RSMPGS.MainForm.listView_SysLog.EnsureVisible(RSMPGS.MainForm.listView_SysLog.Items.Count - 1);
                        RSMPGS.MainForm.listView_SysLog.Items[RSMPGS.MainForm.listView_SysLog.Items.Count - 1].Selected = true;
                    }

                    RSMPGS.MainForm.listView_SysLog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                    RSMPGS.MainForm.listView_SysLog.EndUpdate();

                    RSMPGS.MainForm.Refresh();

                }

            }

            CloseSysLogFile();

        }

        public void DeleteLogFiles(string LogFilePath, string LogFileName)
        {
            IFormatProvider culture = new CultureInfo("en-US");

            string[] LogFiles = Directory.GetFiles(LogFilePath, LogFileName);

            foreach (string LogFile in LogFiles)
            {
                if (LogFile.Length > 12)
                {
                    string sDateTime = LogFile.Substring(LogFile.Length - 12, 8);
                    try
                    {
                        DateTime LogFileDateTime = DateTime.ParseExact(sDateTime, "yyyyMMdd", culture);
                        if (LogFileDateTime.AddDays(DaysToKeepLogFiles).CompareTo(DateTime.Now) < 0)
                        {
                            File.Delete(LogFile);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        public void SysLog(Severity severity, string sFormat, params object[] pArg)
        {
            try
            {
                string sLogText = String.Format(sFormat, pArg);
                SysLog(severity, sLogText);
            }
            catch
            {
                SysLog(Severity.Error, "Formatting error occured (" + sFormat + ")");

            }
        }

        public void SysLog(Severity severity, string sLogText)
        {

            if (bEnableSysLog == false)
            {
                return;
            }

            string sDateTime = String.Format("{0:HH:mm:ss.fff}", DateTime.Now);

            string sFileName = sSysLogFilePath + "\\SysLog_" + String.Format("{0:yyyyMMdd}", DateTime.Now) + ".Log";

            RSMPGS.MainForm.BeginInvoke(RSMPGS.MainForm.DelegateAddSysLogListItem, new Object[] { severity, sDateTime, sLogText });

            lock (this)
            {
                try
                {
                    if (swSysLogFile == null)
                    {
                        swSysLogFile = File.AppendText(sFileName);
                    }
                    if (sLogText.Length == 0)
                    {
                        swSysLogFile.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                    }
                    else
                    {
                        swSysLogFile.WriteLine(severity + "\t" + sDateTime + "\t" + sLogText);
                    }
                }
                catch
                {
                }
            }
        }

        public void EventLog(string sFormat, params object[] pArg)
        {

            string sLogText = String.Format(sFormat, pArg);
            string sEventType = "";

            if (sLogText.ToLower().Contains("alarm")) sEventType = "Alarm";
            if (sLogText.ToLower().Contains("command")) sEventType = "Command";
            if (sLogText.ToLower().Contains("status")) sEventType = "Status";
            if (sLogText.ToLower().Contains("aggregatedstatus")) sEventType = "AggregatedStatus";

            sLogText = sLogText.Replace(sEventType + ";", "");
            string sFileName = sEventFilePath + "\\" + sEventType + "Event_" + String.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt";

            lock (this)
            {
                try
                {
                    StreamWriter swEventFile = File.AppendText(sFileName);
                    swEventFile.WriteLine(sLogText.Trim());
                    swEventFile.Close();
                }
                catch { }
            }
        }

        public void AddRawDebugData(bool bNewPacket, int iDirection, bool bForceHexCode, byte[] bBuffer, int iOffset, int iBufferLength)
        {

            lock (this)
            {
                foreach (RSMPGS_Debug DebugForm in RSMPGS.DebugForms)
                {
                    try
                    {
                        DebugForm.AddRawDebugDataMethod(DateTime.Now, bNewPacket, iDirection, bForceHexCode, bBuffer, iOffset, iBufferLength);
                        //DebugForm.BeginInvoke(DebugForm.DelegateAddRawDebugData, new Object[] { DateTime.Now, bNewPacket, iDirection, bForceHexCode, bBuffer, iOffset, iBufferLength });
                    }
                    catch { }
                }
            }
        }

        public void AddJSonDebugData(int iDirection, string sPacketType, string sDebugData)
        {
            lock (this)
            {
                foreach (RSMPGS_Debug DebugForm in RSMPGS.DebugForms)
                {
                    try
                    {
                        DebugForm.AddJSonDebugDataMethod(DateTime.Now, iDirection, sPacketType, sDebugData);
                        //DebugForm.BeginInvoke(DebugForm.DelegateAddJSonDebugData, new Object[] { DateTime.Now, iDirection, sPacketType, sDebugData });
                    }
                    catch { }
                }
            }
        }
        public string StoreBase64DebugData(string sValue)
        {

            string sBase64Info = "(failed to store)";

            try
            {
                Random Rnd = new Random();
                string sFileName = cPrivateProfile.DebugFilesPath() + "\\Base64_" + String.Format("{0:yyyyMMdd}_{0:HHmmss_fff}", DateTime.Now) + "_" + Rnd.Next(4095).ToString("x3") + ".Bin";
                Encoding encoding;
                encoding = Encoding.GetEncoding("IBM437");
                byte[] Base64Bytes = encoding.GetBytes(sValue);
                char[] Base64Chars = encoding.GetChars(Base64Bytes);
                byte[] Base8Bytes = System.Convert.FromBase64CharArray(Base64Chars, 0, Base64Chars.GetLength(0));

                System.IO.FileStream fsBase8 = new System.IO.FileStream(sFileName, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
                System.IO.BinaryWriter bwBase8 = new System.IO.BinaryWriter(fsBase8);
                bwBase8.Write(Base8Bytes);
                fsBase8.Close();
                fsBase8.Dispose();
                bwBase8.Close();
                sBase64Info = "base64 (" + Base8Bytes.GetLength(0).ToString() + " bytes) updated " + String.Format("{0:HH:mm:ss.fff}", DateTime.Now);
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Stored base64 decoded binary data to: {0}", sFileName);
            }
            catch (Exception e)
            {
                RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Error, "Failed to store base64 data: " + e.Message);
            }
            return sBase64Info;
        }
    }

}