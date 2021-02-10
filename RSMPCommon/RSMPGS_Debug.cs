using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace nsRSMPGS
{
    public partial class RSMPGS_Debug : Form
    {

        //public delegate void AddRawDebugData(DateTime dtNow, bool bNewPacket, int iDirection, bool bForceHexCode, byte[] bBuffer, int iOffset, int iBufferLength);
        // public AddRawDebugData DelegateAddRawDebugData;

        //public delegate void AddJSonDebugData(DateTime dtNow, int iDirection, string sPacketType, string sBuffer);
        // public AddJSonDebugData DelegateAddJSonDebugData;

        private StreamWriter swDebugFile = null;
        private int MaxDebugLines;

        System.Drawing.Color rgbForeColor = Color.White;
        System.Drawing.Color rgbBackColor = Color.Black;

        private string[] sSubItems = new String[2];
        private ListViewItem lvItem;
        private System.Drawing.Font MonospacedCourier = new System.Drawing.Font("Courier New", 9, FontStyle.Bold);
        private string ExpandedBuffer = "";

        private List<ListViewItem> DebuglvItems = new List<ListViewItem>();

        public RSMPGS_Debug()
        {
            InitializeComponent();
        }

        private void RSMPGS_Debug_Load(object sender, EventArgs e)
        {

            MaxDebugLines = cPrivateProfile.GetIniFileInt("RSMP", "MaxDebugLines", 1000);

            //DelegateAddRawDebugData = new AddRawDebugData(AddRawDebugDataMethod);
            //DelegateAddJSonDebugData = new AddJSonDebugData(AddJSonDebugDataMethod);

            saveFileDialog_Debug.InitialDirectory = cPrivateProfile.DebugFilesPath();

            timer_System.Enabled = true;

            listView_Debug.StopSorting();

        }

        private void RSMPGS_Debug_FormClosed(object sender, FormClosedEventArgs e)
        {
            RSMPGS.DebugForms.Remove(this);
        }

        private void ToolStripMenuItem_PacketTypes_DropDownOpening(object sender, EventArgs e)
        {

            bool bDifferentPacketsEnabled = (ToolStripMenuItem_PacketTypes_Raw.Checked == false && ToolStripMenuItem_PacketTypes_All.Checked == false);

            ToolStripMenuItem_PacketTypes_All.Enabled = (ToolStripMenuItem_PacketTypes_Raw.Checked == false);

            ToolStripMenuItem_PacketTypes_Version.Enabled = bDifferentPacketsEnabled;
            ToolStripMenuItem_PacketTypes_Alarm.Enabled = bDifferentPacketsEnabled;
            ToolStripMenuItem_PacketTypes_AggStatus.Enabled = bDifferentPacketsEnabled;
            ToolStripMenuItem_PacketTypes_Status.Enabled = bDifferentPacketsEnabled;
            ToolStripMenuItem_PacketTypes_Command.Enabled = bDifferentPacketsEnabled;
            ToolStripMenuItem_PacketTypes_Watchdog.Enabled = bDifferentPacketsEnabled;
            ToolStripMenuItem_PacketTypes_PacketAck.Enabled = bDifferentPacketsEnabled;
            ToolStripMenuItem_PacketTypes_Unknown.Enabled = bDifferentPacketsEnabled;

        }

        private void ToolStripMenuItem_CloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripMenuItem_PacketTypes_CheckChanged(object sender, EventArgs e)
        {
            CalcNewCaption();
        }

        public void CalcNewCaption()
        {
            string sDebugItems = "";

            if (ToolStripMenuItem_PacketTypes_Raw.Checked)
            {
                this.Text = "Debugging all traffic (raw data)";
            }
            else
            {
                if (ToolStripMenuItem_PacketTypes_All.Checked)
                {
                    this.Text = "Debugging all packet types";
                }
                else
                {
                    if (ToolStripMenuItem_PacketTypes_Version.Checked) sDebugItems += "Version, ";
                    if (ToolStripMenuItem_PacketTypes_Alarm.Checked) sDebugItems += "Alarms, ";
                    if (ToolStripMenuItem_PacketTypes_AggStatus.Checked) sDebugItems += "AggStatus, ";
                    if (ToolStripMenuItem_PacketTypes_Status.Checked) sDebugItems += "Status, ";
                    if (ToolStripMenuItem_PacketTypes_Command.Checked) sDebugItems += "Command, ";
                    if (ToolStripMenuItem_PacketTypes_Watchdog.Checked) sDebugItems += "Watchdog, ";
                    if (ToolStripMenuItem_PacketTypes_PacketAck.Checked) sDebugItems += "PacketAck, ";
                    if (ToolStripMenuItem_PacketTypes_Unknown.Checked) sDebugItems += "Unknown, ";

                    if (sDebugItems.Length == 0)
                    {
                        this.Text = "(nothing to debug is selected)";
                    }
                    else
                    {
                        if (sDebugItems.EndsWith(", "))
                        {
                            sDebugItems = sDebugItems.Substring(0, sDebugItems.Length - 2);
                        }
                        this.Text = "Debugging these packet types: " + sDebugItems;
                    }
                }
            }

#if _RSMPGS1
            this.Text = "RSMPGS1 Debug - " + this.Text;
#endif

#if _RSMPGS2
            this.Text = "RSMPGS2 Debug - " + this.Text;
#endif

            if (swDebugFile != null)
            {
                this.Text += " (recording)";
            }

        }

        // If byte array, then force hex bytes
        public void AddRawDebugDataMethod(DateTime dtNow, bool bNewPacket, int iDirection, bool bForceHexCode, byte[] bBuffer, int iOffset, int iBufferLength)
        {

            if (ToolStripMenuItem_PacketTypes_Raw.Checked == false || iBufferLength == 0 || bBuffer == null)
            {
                return;
            }

            sSubItems[0] = String.Format("{0:yyyy-MM-dd}", dtNow) + " " + String.Format("{0:HH:mm:ss.fff}", dtNow);

            switch (iDirection)
            {
                case cSysLogAndDebug.Direction_Out:
                    sSubItems[1] = "Tx ->";
                    rgbForeColor = Color.Black;
                    rgbBackColor = Color.MediumAquamarine;
                    break;
                case cSysLogAndDebug.Direction_In:
                    sSubItems[1] = "Rx <-";
                    rgbForeColor = Color.Black;
                    rgbBackColor = Color.Salmon;
                    break;
            }

            if (bNewPacket == false)
            {
                sSubItems[0] = "";
                sSubItems[1] = "";
            }

            for (int iCharacterIndex = iOffset, iCharactersLeft = iBufferLength; iCharactersLeft > 0;)
            {
                int iCharactersToViewInNextLine = iCharactersLeft;

                //lvItem = listView_Debug.Items.Add(sSubItems[0]);

                lvItem = new ListViewItem(sSubItems[0]);

                lvItem.SubItems.Add(sSubItems[1]);

                ExpandedBuffer = "";

                if (iCharactersToViewInNextLine > 80)
                {
                    iCharactersToViewInNextLine = 70;
                }
                for (; iCharactersToViewInNextLine > 0; iCharactersToViewInNextLine--, iCharacterIndex++, iCharactersLeft--)
                {
                    if (bForceHexCode == true || bBuffer[iCharacterIndex] < ' ') // || bBuffer[iCharacter] > '\x7f')
                    {
                        ExpandedBuffer = ExpandedBuffer + "<0x" + bBuffer[iCharacterIndex].ToString("x2") + ">";
                    }
                    else
                    {
                        ExpandedBuffer += Encoding.Default.GetString(bBuffer, iCharacterIndex, 1);
                    }
                }
                lvItem.SubItems.Add(ExpandedBuffer);
                lvItem.UseItemStyleForSubItems = false;
                foreach (ListViewItem.ListViewSubItem lvSubItem in lvItem.SubItems)
                {
                    lvSubItem.ForeColor = rgbForeColor;
                    lvSubItem.BackColor = rgbBackColor;
                }
                lvItem.SubItems[2].Font = MonospacedCourier;

                lock (DebuglvItems)
                {
                    DebuglvItems.Add(lvItem);
                }

                sSubItems[0] = "";
                sSubItems[1] = "";

                lock (this)
                {
                    if (swDebugFile != null)
                    {
                        swDebugFile.WriteLine(lvItem.SubItems[0].Text + "\t" + lvItem.SubItems[1].Text + "\t" + lvItem.SubItems[2].Text);
                    }
                }
            }

        }

        public void AddJSonDebugDataMethod(DateTime dtNow, int iDirection, string sPacketType, string sDebugData)
        {

            string sPacketData = sDebugData;
            string sRowData = "";

            if (ToolStripMenuItem_PacketTypes_Raw.Checked == true || sDebugData.Length == 0)
            {
                return;
            }

            if (ToolStripMenuItem_PacketTypes_All.Checked == false)
            {
                switch (sPacketType.ToLower())
                {
                    case "version":
                        if (ToolStripMenuItem_PacketTypes_Version.Checked == false) return;
                        break;
                    case "alarm":
                        if (ToolStripMenuItem_PacketTypes_Alarm.Checked == false) return;
                        break;
                    case "aggregatedstatus":
                    case "aggregatedstatusrequest":
                        if (ToolStripMenuItem_PacketTypes_AggStatus.Checked == false) return;
                        break;
                    case "statussubscribe":
                    case "statusunsubscribe":
                    case "statusrequest":
                    case "statusresponse":
                    case "statusupdate":
                        if (ToolStripMenuItem_PacketTypes_Status.Checked == false) return;
                        break;
                    case "commandrequest":
                    case "commandresponse":
                        if (ToolStripMenuItem_PacketTypes_Command.Checked == false) return;
                        break;
                    case "messageack":
                    case "messagenotack":
                        if (ToolStripMenuItem_PacketTypes_PacketAck.Checked == false) return;
                        break;
                    case "watchdog":
                        if (ToolStripMenuItem_PacketTypes_Watchdog.Checked == false) return;
                        break;
                    default:
                        if (ToolStripMenuItem_PacketTypes_Unknown.Checked == false) return;
                        break;
                }
            }

            sSubItems[0] = String.Format("{0:yyyy-MM-dd}", dtNow) + " " + String.Format("{0:HH:mm:ss.fff}", dtNow);

            switch (iDirection)
            {
                case cSysLogAndDebug.Direction_Out:
                    sSubItems[1] = "Tx ->";
                    rgbForeColor = Color.Black;
                    rgbBackColor = Color.MediumAquamarine;
                    break;
                case cSysLogAndDebug.Direction_In:
                    sSubItems[1] = "Rx <-";
                    rgbForeColor = Color.Black;
                    rgbBackColor = Color.Salmon;
                    break;
            }

            sPacketData = sPacketData.Replace("\t", "  ");

            while (sPacketData.Length > 0)
            {

                //lvItem = listView_Debug.Items.Add(sSubItems[0]);

                lvItem = new ListViewItem(sSubItems[0]);

                lvItem.SubItems.Add(sSubItems[1]);

                if (sPacketData.IndexOf("\r\n") > 0)
                {
                    sRowData = sPacketData.Substring(0, sPacketData.IndexOf("\r\n"));
                    sPacketData = sPacketData.Substring(sPacketData.IndexOf("\r\n") + 2);
                }
                else
                {
                    if (sPacketData.IndexOf("\n") > 0)
                    {
                        sRowData = sPacketData.Substring(0, sPacketData.IndexOf("\n"));
                        sPacketData = sPacketData.Substring(sPacketData.IndexOf("\n") + 1);
                    }
                    else
                    {
                        if (sPacketData.IndexOf("\r") > 0)
                        {
                            sRowData = sPacketData.Substring(0, sPacketData.IndexOf("\r"));
                            sPacketData = sPacketData.Substring(sPacketData.IndexOf("\r") + 1);
                        }
                        else
                        {
                            if (sPacketData.IndexOf(",\"") > 0)
                            {
                                sRowData = sPacketData.Substring(0, sPacketData.IndexOf(",\"") + 1);
                                sPacketData = sPacketData.Substring(sPacketData.IndexOf(",\"") + 1);
                            }
                            else
                            {
                                if (sPacketData.Length > 80)
                                {
                                    sRowData = sPacketData.Substring(0, 70);
                                    sPacketData = sPacketData.Substring(70);
                                }
                                else
                                {
                                    sRowData = sPacketData;
                                    sPacketData = "";
                                }
                            }
                        }
                    }
                }
                // Make timestamps more readable...
                // String.Format("{0:yyyy-MM-dd}T{0:HH:mm:ss.fff}", AlarmHeaderAndBody.aTs.ToLocalTime());
                // "aTs":"\/Date(1320254751484)\/"
                //int iDatePosition = sRowData.IndexOf("\\/Date(", StringComparison.OrdinalIgnoreCase);
                int iDatePosition = sRowData.IndexOf("\":\"20", StringComparison.OrdinalIgnoreCase);
                if (iDatePosition >= 0)
                {
                    try
                    {
                        // "aSTS":"2019-02-26T15:36:17.588Z","
                        if (sRowData.Substring(iDatePosition + 13, 1).Equals("T", StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime dtTimeStamp = DateTime.ParseExact(sRowData.Substring(iDatePosition + 3, 24).ToUpper(), @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                            sRowData += " << debugger decoded UTC: " + String.Format("{0:yyyy-MM-dd} {0:HH:mm:ss.fff}", dtTimeStamp.ToUniversalTime()) + ", local: " + String.Format("{0:yyyy-MM-dd} {0:HH:mm:ss.fff}", dtTimeStamp.ToLocalTime()) + " >>";
                        }
                    }
                    catch
                    {
                    }
                }

                lvItem.SubItems.Add(sRowData);
                lvItem.UseItemStyleForSubItems = false;

                foreach (ListViewItem.ListViewSubItem lvSubItem in lvItem.SubItems)
                {
                    lvSubItem.ForeColor = rgbForeColor;
                    lvSubItem.BackColor = rgbBackColor;
                }
                lvItem.SubItems[2].Font = MonospacedCourier;
                sSubItems[0] = "";
                sSubItems[1] = "";

                lock (DebuglvItems)
                {
                    DebuglvItems.Add(lvItem);
                }

                lock (this)
                {
                    if (swDebugFile != null)
                    {
                        swDebugFile.WriteLine(lvItem.SubItems[0].Text + "\t" + lvItem.SubItems[1].Text + "\t" + lvItem.SubItems[2].Text);
                    }
                }
            }

            //lvItem = listView_Debug.Items.Add("");

            lvItem = new ListViewItem("");

            lock (DebuglvItems)
            {
                DebuglvItems.Add(lvItem);
            }

            lock (this)
            {
                if (swDebugFile != null)
                {
                    swDebugFile.WriteLine("");
                }
            }


        }

        private void ToolStripMenuItem_Debug_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItem_CopyToClipboard.Enabled = listView_Debug.SelectedItems.Count > 0;
            toolStripMenuItem_Clear.Enabled = listView_Debug.Items.Count > 0;
            toolStripMenuItem_SaveContinousToFile.Checked = swDebugFile != null;
        }

        private void toolStripMenuItem_CopyToClipboard_Click(object sender, EventArgs e)
        {
            string sDebugData = "";
            foreach (ListViewItem lvItem in listView_Debug.SelectedItems)
            {
                string sDebugLine = "";
                foreach (ListViewItem.ListViewSubItem lvSubItem in lvItem.SubItems)
                {
                    sDebugLine += lvSubItem.Text;
                    sDebugLine += "\t";
                }
                if (sDebugData.Length > 0)
                {
                    sDebugData += "\r\n";
                }
                sDebugData += sDebugLine.Substring(0, sDebugLine.Length - 1); ;
            }
            Clipboard.Clear();
            Clipboard.SetText(sDebugData);
        }

        private void toolStripMenuItem_Clear_Click(object sender, EventArgs e)
        {
            listView_Debug.Items.Clear();
        }

        private void toolStripMenuItem_SaveContinousToFile_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem_SaveContinousToFile.Checked)
            {
                lock (this)
                {
                    if (swDebugFile != null)
                    {
                        swDebugFile.Close();
                        swDebugFile = null;
                    }
                }
            }
            else
            {
                saveFileDialog_Debug.ShowDialog();
            }
            CalcNewCaption();
        }

        private void saveFileDialog_Debug_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                lock (this)
                {
                    swDebugFile = File.AppendText(saveFileDialog_Debug.FileName);
                }
            }
            catch { }
        }

        private void RSMPGS_Debug_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (swDebugFile != null)
            {
                lock (this)
                {
                    swDebugFile.Close();
                    swDebugFile = null;
                }
            }
        }

        private void timer_System_Tick(object sender, EventArgs e)
        {

            timer_System.Enabled = false;

            lock (this)
            {
                if (swDebugFile != null)
                {
                    swDebugFile.Flush();
                }

            }

            lock (DebuglvItems)
            {


                if (DebuglvItems.Count > 0)
                {
                    listView_Debug.BeginUpdate();

                    foreach (ListViewItem lvItem in DebuglvItems)
                    {

                        listView_Debug.Items.Add(lvItem);

                        while (listView_Debug.Items.Count > MaxDebugLines)
                        {
                            listView_Debug.Items.RemoveAt(0);
                        }

                    }

                    DebuglvItems.Clear();

                    if (ToolStripMenuItem_ShowLastRow.Checked && listView_Debug.Items.Count > 0)
                    {
                        listView_Debug.Items[listView_Debug.Items.Count - 1].EnsureVisible();
                    }

                    listView_Debug.EndUpdate();

                    listView_Debug.Update();

                    Application.DoEvents();

                }

            }

            timer_System.Enabled = true;

        }

    }

}
