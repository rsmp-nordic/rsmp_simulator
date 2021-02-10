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

    public partial class RSMPGS_Main
    {
        private void button_SendTestPackage_1_Click(object sender, EventArgs e)
        {
            RSMPGS.RSMPConnection.SendRawString(textBox_TestPackage_1.Text);
        }

        private void button_SendTestPackage_2_Click(object sender, EventArgs e)
        {
            RSMPGS.RSMPConnection.SendRawString(textBox_TestPackage_2.Text);
        }

        private void button_TestPackage_1_Browse_Click(object sender, EventArgs e)
        {
            if (openFileDialog_TestPackage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    textBox_TestPackage_1.Clear();
                    StreamReader swTestPackageFile = new StreamReader((System.IO.Stream)File.OpenRead(openFileDialog_TestPackage.FileName));
                    textBox_TestPackage_1.Text = swTestPackageFile.ReadToEnd();
                    swTestPackageFile.Close();
                }
                catch
                {
                }
            }
        }

        private void button_TestPackage_2_Browse_Click(object sender, EventArgs e)
        {
            if (openFileDialog_TestPackage.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    textBox_TestPackage_2.Clear();
                    StreamReader swTestPackageFile = new StreamReader((System.IO.Stream)File.OpenRead(openFileDialog_TestPackage.FileName));
                    textBox_TestPackage_2.Text = swTestPackageFile.ReadToEnd();
                    swTestPackageFile.Close();
                }
                catch
                {
                }
            }
        }
    }

}