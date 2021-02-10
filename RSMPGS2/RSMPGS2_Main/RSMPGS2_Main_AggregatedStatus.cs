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
            }
            else
            {
                lvItem.SubItems[1].BackColor = lvItem.SubItems[0].BackColor;
            }
        }

        private void AddAggregatedStatusEventToList(cRoadSideObject RoadSideObject, cAggregatedStatusEvent AggregatedStatusEvent)
        {

            if (bIsUpdatingAggregatedStatusEventList == false)
            {
                listView_AggregatedStatusEvents.StopSorting();
                listView_AggregatedStatusEvents.BeginUpdate();
                bIsUpdatingAggregatedStatusEventList = true;
            }

            ListViewItem lvItem = listView_AggregatedStatusEvents.Items.Add(AggregatedStatusEvent.sTimeStamp.ToString());
            lvItem.SubItems.Add(AggregatedStatusEvent.sMessageId);
            lvItem.SubItems.Add(AggregatedStatusEvent.sBitStatus);
            lvItem.SubItems.Add(AggregatedStatusEvent.sFunctionalPosition);
            lvItem.SubItems.Add(AggregatedStatusEvent.sFunctionalState);

        }

        public void HandleAggregatedStatusListUpdate(cRoadSideObject RoadSideObject, cAggregatedStatusEvent AggregatedStatusEvent)
        {
            if (SelectedRoadSideObject == RoadSideObject)
            {
                for (int iIndex = 0; iIndex < RoadSideObject.bBitStatus.GetLength(0); iIndex++)
                {
                    SetStatusBitColor(listView_AggregatedStatus_StatusBits.Items[iIndex], RoadSideObject.bBitStatus[iIndex]);
                }
            }
            AddAggregatedStatusEventToList(RoadSideObject, AggregatedStatusEvent);
        }
    }
}