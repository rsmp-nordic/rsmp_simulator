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
        private void listView_AggregatedStatus_StatusBits_DoubleClick(object sender, EventArgs e)
        {

            if (treeView_SitesAndObjects.SelectedNode == null)
            {
                return;
            }

            if (treeView_SitesAndObjects.SelectedNode.Parent == null || bIsCurrentlyChangingSelection == true)
            {
                return;
            }

            cRoadSideObject RoadSideObject = (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag;
            ListViewItem lvItem = listView_AggregatedStatus_StatusBits.SelectedItems[0];

            RoadSideObject.bBitStatus[lvItem.Index] = !RoadSideObject.bBitStatus[lvItem.Index];

            SetStatusBitColor(lvItem, RoadSideObject.bBitStatus[lvItem.Index]);

            RoadSideObject.dtLastChangedAggregatedStatus = DateTime.Now;

            if (checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Checked)
            {
                RSMPGS.JSon.CreateAndSendAggregatedStatusMessage(RoadSideObject);
            }

            lvItem.Selected = false;

        }

        private void listBox_AggregatedStatus_FunctionalPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treeView_SitesAndObjects.SelectedNode.Parent == null || bIsCurrentlyChangingSelection == true || listBox_AggregatedStatus_FunctionalPosition.SelectedItem == null)
            {
                return;
            }
            cRoadSideObject RoadSideObject = (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag;
            RoadSideObject.sFunctionalPosition = listBox_AggregatedStatus_FunctionalPosition.SelectedItem.ToString();

            RoadSideObject.dtLastChangedAggregatedStatus = DateTime.Now;

            if (checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Checked)
            {
                RSMPGS.JSon.CreateAndSendAggregatedStatusMessage(RoadSideObject);
            }
        }

        private void listBox_AggregatedStatus_FunctionalState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (treeView_SitesAndObjects.SelectedNode.Parent == null || bIsCurrentlyChangingSelection == true || listBox_AggregatedStatus_FunctionalState.SelectedItem == null) //  || listView_AggregatedStatus_StatusBits.SelectedItems.Count == 0 - removed 2014-09-11/TR to enable lvItem.Selected = false above
            {
                return;
            }
            cRoadSideObject RoadSideObject = (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag;
            RoadSideObject.sFunctionalState = listBox_AggregatedStatus_FunctionalState.SelectedItem.ToString();

            RoadSideObject.dtLastChangedAggregatedStatus = DateTime.Now;

            if (checkBox_AggregatedStatus_SendAutomaticallyWhenChanged.Checked)
            {
                RSMPGS.JSon.CreateAndSendAggregatedStatusMessage(RoadSideObject);
            }
        }
        public void SetStatusBitColor(ListViewItem lvItem, bool bIsSet)
        {
            // If called when item was added, just return
            if (lvItem.SubItems.Count < 2)
            {
                return;
            }

            if (bIsSet)
            {
                switch (lvItem.Index)
                {
                    case 0: lvItem.SubItems[1].BackColor = Color.Cyan; break;
                    case 1: lvItem.SubItems[1].BackColor = Color.Purple; break;
                    case 2: lvItem.SubItems[1].BackColor = Color.Red; break;
                    case 3: lvItem.SubItems[1].BackColor = Color.Yellow; break;
                    case 4: lvItem.SubItems[1].BackColor = Color.Blue; break;
                    case 5: lvItem.SubItems[1].BackColor = Color.Green; break;
                    case 6: lvItem.SubItems[1].BackColor = Color.DarkGray; break;
                    case 7: lvItem.SubItems[1].BackColor = Color.LightGray; break;
                }
                lvItem.ImageIndex = 4;
            }
            else
            {
                lvItem.SubItems[1].BackColor = lvItem.SubItems[0].BackColor;
                lvItem.ImageIndex = 3;
            }
        }

        private void button_AggregatedStatus_Send_Click(object sender, EventArgs e)
        {
            cRoadSideObject RoadSideObject = (cRoadSideObject)treeView_SitesAndObjects.SelectedNode.Tag;
            RSMPGS.JSon.CreateAndSendAggregatedStatusMessage(RoadSideObject);
        }

    }
}