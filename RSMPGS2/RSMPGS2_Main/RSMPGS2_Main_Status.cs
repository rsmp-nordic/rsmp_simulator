using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using nsRSMPGS;
using RSMP_Messages;

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

        ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval.Enabled = RSMPGS.JSon.NegotiatedRSMPVersion > cJSon.RSMPVersion.RSMP_3_1_4;

       // ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly
       // ToolStripMenuItem_StatusSubscribe_UpdateOnInterval

      }
    }

    private void ToolStripMenuItem_Status_Click(object sender, EventArgs e)
    {

      cRoadSideObject RoadSideObject = null;

      bool bAlwaysUpdateOnChange = false;

      cJSon.StatusMsgType statusMsgType;

      ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;

      string sUpdateRate = "";

      if (menuitem.Equals(ToolStripMenuItem_StatusRequest))
      {
        statusMsgType = cJSon.StatusMsgType.Request;
      }
      else if (menuitem.Equals(ToolStripMenuItem_StatusUnsubscribe))
      {
        statusMsgType = cJSon.StatusMsgType.UnSubscribe;
      }
      else if (menuitem.Equals(ToolStripMenuItem_StatusSubscribe_UpdateOnChangeOnly))
      {
        bAlwaysUpdateOnChange = true;
        sUpdateRate = "0";
        statusMsgType = cJSon.StatusMsgType.Subscribe;
      }
      else if (menuitem.Equals(ToolStripMenuItem_StatusSubscribe_UpdateOnInterval_Manually) || menuitem.Equals(ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval_Manually))
      {
        if (cFormsHelper.InputBox("Enter Update Rate (seconds)", "UpdateRate:", ref sUpdateRate, false, true) != DialogResult.OK)
        {
          return;
        }
        if (sUpdateRate == "")
        {
          return;
        }
        bAlwaysUpdateOnChange = menuitem.OwnerItem.Equals(ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval);
        statusMsgType = cJSon.StatusMsgType.Subscribe;
      }
      else
      {
        sUpdateRate = menuitem.Tag.ToString();
        bAlwaysUpdateOnChange = menuitem.OwnerItem.Equals(ToolStripMenuItem_StatusSubscribe_UpdateOnChangeAndInterval);
        statusMsgType = cJSon.StatusMsgType.Subscribe;
      }

      RoadSideObject = null;

      List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4> StatusSubscribeValues = new List<RSMP_Messages.StatusSubscribe_Status_Over_3_1_4>();

      foreach( ListViewItem lvItem in listView_Status.Items )
      {
        if (lvItem.Selected)
        {

          cStatusReturnValue StatusReturnValue = (cStatusReturnValue)lvItem.Tag;

          if (RoadSideObject == null)
          {
            RoadSideObject = StatusReturnValue.StatusObject.RoadSideObject;
          }

          RSMP_Messages.StatusSubscribe_Status_Over_3_1_4 statusSubscribe_Status = new RSMP_Messages.StatusSubscribe_Status_Over_3_1_4();

          statusSubscribe_Status.sCI = StatusReturnValue.StatusObject.sStatusCodeId;
          statusSubscribe_Status.n = StatusReturnValue.sName;

          if (statusMsgType == cJSon.StatusMsgType.Subscribe)
          {
            statusSubscribe_Status.uRt = sUpdateRate;
            statusSubscribe_Status.sOc = bAlwaysUpdateOnChange;
          }
          else
          {
            statusSubscribe_Status.uRt = null;
            statusSubscribe_Status.sOc = false;
          }

          StatusReturnValue.sLastUpdateRate = statusSubscribe_Status.uRt;
          StatusReturnValue.bLastUpdateOnChange = statusSubscribe_Status.sOc;

          StatusSubscribeValues.Add(statusSubscribe_Status);
        }
      }

      if (StatusSubscribeValues.Count > 0 && RoadSideObject != null)
      {
        // Send message with status selected for this object
        switch (statusMsgType)
        {
          case cJSon.StatusMsgType.Subscribe:
            RSMPGS.JSon.CreateAndSendSubscriptionMessage(RoadSideObject, StatusSubscribeValues);
            break;

          case cJSon.StatusMsgType.Request:
            RSMPGS.JSon.CreateAndSendStatusMessage(RoadSideObject, StatusSubscribeValues, "StatusRequest");
            break;

          case cJSon.StatusMsgType.UnSubscribe:
            RSMPGS.JSon.CreateAndSendStatusMessage(RoadSideObject, StatusSubscribeValues, "StatusUnsubscribe");
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

      RoadSideObject.StatusGroup.Items.Clear();

      foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
      {
        // Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;

        foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
        {
          string sKey = StatusObject.sStatusCodeId.ToUpper() + "/" + StatusReturnValue.sName.ToUpper();

          ListViewItem lvItem = new ListViewItem (StatusObject.sStatusCodeId, -1);
          lvItem.Name = sKey;

          string[] sValues = new string[8];

          //ListViewItem lvItem = listView_Status.Items.Add(sKey, StatusObject.sDescription, -1);

          sValues[0] = StatusObject.sDescription.Split('\n').First().TrimEnd('.');
          sValues[1] = StatusReturnValue.sName;
          sValues[2] = StatusReturnValue.Value.GetValueType();
          sValues[3] = StatusReturnValue.Value.GetValueType().Equals("array", StringComparison.OrdinalIgnoreCase) ? "(array)" : StatusReturnValue.Value.GetValue().ToString();
          sValues[4] = StatusReturnValue.sQuality;
          sValues[5] = StatusReturnValue.sLastUpdateRate == null ?  "" : StatusReturnValue.sLastUpdateRate;
          sValues[6] = StatusReturnValue.bLastUpdateOnChange.ToString();
          sValues[7] = StatusReturnValue.sComment.Replace("\n", " / ");
          /*
           * 
          lvItem.SubItems.Add(StatusObject.sDescription);
          lvItem.SubItems.Add(StatusReturnValue.sName);
          lvItem.SubItems.Add(StatusReturnValue.sType);
          lvItem.SubItems.Add(StatusReturnValue.sStatus);
          lvItem.SubItems.Add(StatusReturnValue.sQuality);
          lvItem.SubItems.Add(StatusReturnValue.sLastUpdateRate);
          lvItem.SubItems.Add(StatusReturnValue.bLastUpdateOnChange.ToString());
          lvItem.SubItems.Add(StatusReturnValue.sComment);
          */
          lvItem.SubItems.AddRange(sValues);

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

      if (bIsUpdatingStatusEventList == false)
      {
        listView_StatusEvents.StopSorting();
        listView_StatusEvents.BeginUpdate();
        bIsUpdatingStatusEventList = true;
      }

      ListViewItem lvItem = new ListViewItem(StatusEvent.sTimeStamp.ToString());
      lvItem.SubItems.Add(StatusEvent.sMessageId);
      lvItem.SubItems.Add(StatusEvent.sEvent);
      lvItem.SubItems.Add(StatusEvent.sStatusCodeId);
      lvItem.SubItems.Add(StatusEvent.sName);
      lvItem.SubItems.Add(StatusEvent.sStatus);
      lvItem.SubItems.Add(StatusEvent.sQuality);
      lvItem.SubItems.Add(StatusEvent.sUpdateRate);
      lvItem.SubItems.Add(StatusEvent.bUpdateOnChange.ToString());
      listView_StatusEvents.Items.Add(lvItem);
    }

    public void HandleStatusListUpdate(cRoadSideObject RoadSideObject, cStatusEvent StatusEvent, bool bSend)
    {

      string sKey = StatusEvent.sStatusCodeId.ToUpper() + "/" + StatusEvent.sName.ToUpper();

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
          lvItem.SubItems[7].Text = StatusEvent.bUpdateOnChange.ToString();
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
    private void listView_Status_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      ListView listview = (ListView)sender;
      ListViewItem lvItem;

      if (listview.SelectedItems.Count == 0)
      {
        return;
      }

      lvItem = listview.SelectedItems[0];
      cStatusReturnValue StatusReturnValue = (cStatusReturnValue)lvItem.Tag;
      cStatusObject StatusObject = StatusReturnValue.StatusObject;

      try
      {
        string sValue = StatusReturnValue.Value.GetValue().ToString();
        List<Dictionary<string, object>> array = StatusReturnValue.Value.GetArray();
        cFormsHelper.InputStatusBoxValueType("View status", ref sValue, ref array, StatusReturnValue.Value, StatusReturnValue.sComment, true, true);
      }
      catch
      { }
    }
  }
}


