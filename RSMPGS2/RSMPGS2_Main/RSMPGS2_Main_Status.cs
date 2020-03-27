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
		private void contextMenuStrip_Status_Opening(object sender, CancelEventArgs e)
		{
			if (listView_Status.SelectedItems.Count == 0 || RSMPGS.RSMPConnection.ConnectionStatus() != cTcpSocket.ConnectionStatus_Connected)
			{
				ToolStripMenuItem_StatusRequest.Enabled = false;
				ToolStripMenuItem_StatusSubscribe.Enabled = false;
				ToolStripMenuItem_StatusUnsubscribe.Enabled = false;

			}
			else
			{
				ToolStripMenuItem_StatusRequest.Enabled = true;
				ToolStripMenuItem_StatusSubscribe.Enabled = true;
				ToolStripMenuItem_StatusUnsubscribe.Enabled = true;
			}
		}

    private void ToolStripMenuItem_Status_Click(object sender, EventArgs e)
    {

      ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;

      string sMenuItemTag = menuitem.Tag.ToString().Split('_')[0];

      string sUpdateRate = "";

      // Enter updaterate manually
      if (sMenuItemTag.StartsWith("StatusSubscribe"))
      {
        if (menuitem.Tag.ToString() == "StatusSubscribe_x")
        {
          if (cFormsHelper.InputBox("Enter Update Rate (seconds)", "UpdateRate:", ref sUpdateRate, false, true) != DialogResult.OK)
          {
            return;
          }
          if (sUpdateRate == "")
          {
            return;
          }
        }
        else
        {
          sUpdateRate = menuitem.Tag.ToString().Split('_')[1];
        }
      }

      // Each group belongs to a RoadSide object
      foreach (ListViewGroup lvGroup in listView_Status.Groups)
      {

        List<cStatusReturnValue> StatusReturnValues = new List<cStatusReturnValue>();

        foreach (ListViewItem lvItem in lvGroup.Items)
        {
          if (lvItem.Selected)
          {

            cStatusReturnValue StatusReturnValue = (cStatusReturnValue)lvItem.Tag;

            switch (sMenuItemTag)
            {
              case "StatusSubscribe":
                StatusReturnValue.sLastUpdateRate = sUpdateRate;
                break;

              case "StatusUnsubscribe":
                StatusReturnValue.sLastUpdateRate = null;
                break;

            }

            StatusReturnValues.Add(StatusReturnValue);
          }
        }

        if (StatusReturnValues.Count == 0)
        {
          continue;
        }

        cStatusObject StatusObject = StatusReturnValues[0].StatusObject;

        switch (sMenuItemTag)
        {
          case "StatusSubscribe":
            RSMPGS.JSon.CreateAndSendSubscriptionMessage(StatusObject.RoadSideObject, StatusReturnValues);
            break;

          case "StatusRequest":
            RSMPGS.JSon.CreateAndSendStatusMessage(StatusObject.RoadSideObject, StatusReturnValues, "StatusRequest");
            break;

          case "StatusUnsubscribe":
            RSMPGS.JSon.CreateAndSendStatusMessage(StatusObject.RoadSideObject, StatusReturnValues, "StatusUnsubscribe");
            break;

          default:
            return;
        }


      }
    }

    /*
     		private void ToolStripMenuItem_Status_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
			ListViewItem lvItem = listView_Status.SelectedItems[0];
			cStatusObject StatusObject = (cStatusObject)lvItem.Tag;
			bool bStatReq = false;
			bool bStatSub = true;
			string sStatusType = null;

			if (menuitem.Tag.ToString().Equals("SelectReqAndSend", StringComparison.OrdinalIgnoreCase))
			{
				sStatusType = "StatusRequest";
				bStatReq = true;
			}
			else if (menuitem.Tag.ToString().Equals("SelectUnsubAndSend", StringComparison.OrdinalIgnoreCase))
			{
				sStatusType = "StatusUnsubscribe";
				bStatSub = false;
			}

			RSMPGS2_StatusForm StatusForm = new RSMPGS2_StatusForm(StatusObject.getRoadSideObject(), bStatReq, bStatSub);
			if (!bStatReq && bStatSub)
			{
				StatusForm.Text = "RSMPGS2 - Status subscription";
			}
			else if (!bStatReq && !bStatSub)
			{
				StatusForm.Text = "RSMPGS2 - Status unsubscription";
			}
			else
			{
				StatusForm.Text = "RSMPGS2 - Status request";
			}


			StatusForm.ShowDialog(this);
			List<cStatusReturnValue> selectedStatus = StatusForm.GetSelectedStatus();

			if (selectedStatus != null && selectedStatus.Count > 0)
			{
				if (bStatReq || !bStatSub)
				{
					RSMPGS.JSon.CreateAndSendStatusMessage(StatusObject.getRoadSideObject(), selectedStatus, sStatusType);
				}
				else
				{
					RSMPGS.JSon.CreateAndSendSubscriptionMessage(StatusObject.getRoadSideObject(), selectedStatus);
				}

			}
		}
    */

    public void UpdateStatusListView(cSiteIdObject SiteIdObject, cRoadSideObject RoadSideObject)
		{

      //
      // View status objects for the selected RoadSide object
      //

      listView_Status.Items.Clear();

      listView_StatusEvents.Items.Clear();

      if (SiteIdObject == null && RoadSideObject == null)
			{
				return;
			}

			listView_Status.BeginUpdate();
      listView_Status.StopSorting();

      listView_StatusEvents.BeginUpdate();

			if (RoadSideObject != null)
			{
        listView_Status.ShowGroups = ToolStripMenuItem_View_AlwaysShowGroupHeaders.Checked;
        UpdateStatusListView(RoadSideObject);
      }
      else
			{
        listView_Status.ShowGroups = true;
        foreach (cRoadSideObject roadSideObject in SiteIdObject.RoadSideObjects)
				{
					UpdateStatusListView(roadSideObject);
        }
      }

			listView_Status.ResumeSorting();
			listView_Status.EndUpdate();

			listView_StatusEvents.EndUpdate();

		}

		public void UpdateStatusListView(cRoadSideObject RoadSideObject)
		{

			foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
			{
				// Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;

				foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
				{
          string sKey = StatusObject.sStatusCodeId.ToUpper() + "/" + StatusReturnValue.sName.ToUpper();

          ListViewItem lvItem = new ListViewItem (StatusObject.sStatusCodeId, -1);
          lvItem.Name = sKey;
          //ListViewItem lvItem = listView_Status.Items.Add(sKey, StatusObject.sDescription, -1);
					lvItem.SubItems.Add(StatusObject.sDescription);
					lvItem.SubItems.Add(StatusReturnValue.sName);
					lvItem.SubItems.Add(StatusReturnValue.sType);
					lvItem.SubItems.Add(StatusReturnValue.sStatus);
					lvItem.SubItems.Add(StatusReturnValue.sQuality);
					lvItem.SubItems.Add(StatusReturnValue.sLastUpdateRate);
          lvItem.SubItems.Add(StatusReturnValue.sComment);

          listView_Status.Items.Add(lvItem);

          lvItem.Tag = StatusReturnValue;
          lvItem.Group = RoadSideObject.StatusGroup;

        }
			}

			foreach (cStatusEvent StatusEvent in RoadSideObject.StatusEvents)
			{
				AddStatusEventToList(RoadSideObject, StatusEvent);
			}

		}

		private void AddStatusEventToList(cRoadSideObject RoadSideObject, cStatusEvent StatusEvent)
		{
			ListViewItem lvItem = new ListViewItem(StatusEvent.sTimeStamp.ToString());
			lvItem.SubItems.Add(StatusEvent.sMessageId);
			lvItem.SubItems.Add(StatusEvent.sEvent);
			lvItem.SubItems.Add(StatusEvent.sStatusCommandId);
			lvItem.SubItems.Add(StatusEvent.sName);
			lvItem.SubItems.Add(StatusEvent.sStatus);
			lvItem.SubItems.Add(StatusEvent.sQuality);
			lvItem.SubItems.Add(StatusEvent.sUpdateRate);
      listView_StatusEvents.Items.Add(lvItem);
    }

    public void HandleStatusListUpdate(cRoadSideObject RoadSideObject, cStatusEvent StatusEvent, bool bSend)
		{

      string sKey = StatusEvent.sStatusCommandId.ToUpper() + "/" + StatusEvent.sName.ToUpper();

      if (RoadSideObject.StatusGroup.Items.ContainsKey(sKey))
      {
        ListViewItem lvItem = RoadSideObject.StatusGroup.Items[sKey];
        if (!bSend)
        {
          lvItem.SubItems[4].Text = StatusEvent.sStatus;
          lvItem.SubItems[5].Text = StatusEvent.sQuality;
        }
        else
        {
          lvItem.SubItems[6].Text = StatusEvent.sUpdateRate;
        }
      }
      AddStatusEventToList(RoadSideObject, StatusEvent);
    }

    /*

    if (SelectedRoadSideObject == RoadSideObject)
    { 
      if (!bSend)
      {
        for (int i = 0; i < listView_Status.Items.Count; i++)
        {
          ListViewItem lvItem = listView_Status.Items[i];
          if (lvItem.SubItems[3].Text.Equals(StatusEvent.sStatusCommandId, StringComparison.OrdinalIgnoreCase)
          && lvItem.SubItems[4].Text.Equals(StatusEvent.sName, StringComparison.OrdinalIgnoreCase))
          {
            lvItem.SubItems[4].Text = StatusEvent.sStatus;
            lvItem.SubItems[5].Text = StatusEvent.sQuality;
            break;
          }
        }
      }
      else
      {
        for (int i = 0; i < listView_Status.Items.Count; i++)
        {
          ListViewItem lvItem = listView_Status.Items[i];
          if (lvItem.SubItems[3].Text.Equals(StatusEvent.sStatusCommandId, StringComparison.OrdinalIgnoreCase)
          && lvItem.SubItems[4].Text.Equals(StatusEvent.sName, StringComparison.OrdinalIgnoreCase))
          {
            lvItem.SubItems[6].Text = StatusEvent.sUpdateRate;
            break;
          }
        }
      }
      AddStatusEventToList(RoadSideObject, StatusEvent);

    }
    */

  }
}
