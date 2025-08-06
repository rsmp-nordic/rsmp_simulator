using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using static nsRSMPGS.cJSon;

namespace nsRSMPGS
{

  public partial class RSMPGS_Main
  {

    public void UpdateStatusListView(cSiteIdObject SiteIdObject, cRoadSideObject RoadSideObject)
    {
      //
      // View status objects for the selected RoadSide object
      //

      listView_Status.Items.Clear();

      if (SiteIdObject == null && RoadSideObject == null)
      {
        return;
      }

      listView_Status.BeginUpdate();
      listView_Status.StopSorting();

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

    }

    public void UpdateStatusListView(cRoadSideObject RoadSideObject)
    {

      RoadSideObject.StatusGroup.Items.Clear();

      foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
      {

        foreach (cStatusReturnValue StatusReturnValue in StatusObject.StatusReturnValues)
        {
          string sKey = StatusObject.sStatusCodeId.ToUpper() + "/" + StatusReturnValue.sName.ToUpper();

          ListViewItem lvItem = new ListViewItem(StatusObject.sStatusCodeId, -1);
          lvItem.Name = sKey;

          string[] sValues = new string[5];

          sValues[0] = StatusObject.sDescription.Split('\n').First().TrimEnd('.');
          sValues[1] = StatusReturnValue.sName;
          sValues[2] = StatusReturnValue.Value.GetValueType();
          sValues[3] = StatusReturnValue.Value.GetValue().ToString();
          sValues[4] = StatusReturnValue.sComment.Replace("\n", " / ");

          lvItem.SubItems.AddRange(sValues);

          //ListViewItem lvItem = listView_Status.Items.Add(sKey, StatusObject.sDescription, -1);

          /*
          lvItem.SubItems.Add(StatusObject.sDescription);
          lvItem.SubItems.Add(StatusReturnValue.sName);
          lvItem.SubItems.Add(StatusReturnValue.sType);
          lvItem.SubItems.Add(StatusReturnValue.sStatus);
          lvItem.SubItems.Add(StatusReturnValue.sComment);
          */

          lvItem.Tag = StatusReturnValue;

          listView_Status.Items.Add(lvItem);

          lvItem.Group = RoadSideObject.StatusGroup;

        }
      }

     // lvItem.SubItems.Add(StatusObject.sDescription.Replace("\"-", "").Replace("\n-", "/").Replace("\n", "/").Replace("\"", ""));


    }

    private void listView_Status_MouseClick(object sender, MouseEventArgs e)
    {
      LastMouseX = e.X;
      LastMouseY = e.Y;
    }

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
        if (cFormsHelper.InputStatusBoxValueType("Enter new value", ref sValue, ref array, StatusReturnValue.Value, StatusReturnValue.sComment, true, false) == DialogResult.OK)
        {
          StatusReturnValue.Value.SetValue(sValue);
          StatusReturnValue.Value.SetArray(array);
          lvItem.SubItems[4].Text = sValue;

          // Find out if this status is subscribed
          foreach (cSubscription Subscription in StatusObject.RoadSideObject.Subscriptions)
          {
            if (Subscription.StatusReturnValue == StatusReturnValue)
            {
              if (Subscription.SubscribeStatus == cSubscription.SubscribeMethod.OnChange || Subscription.SubscribeStatus == cSubscription.SubscribeMethod.IntervalAndOnChange)
              {
                List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();
                RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                s.sCI = StatusObject.sStatusCodeId;
                s.n = StatusReturnValue.sName;
                RSMPGS.ProcessImage.UpdateStatusValue(ref s, StatusReturnValue.Value.GetValueType(), StatusReturnValue.Value.GetValue(), Subscription.StatusReturnValue.Value.GetArray());
                sS.Add(s);

                // On RSMP 3.2.2 and later, the interval time is reset if the value changes
                if (RSMPGS.JSon.NegotiatedRSMPVersion >= RSMPVersion.RSMP_3_2_2)
                {
                  Subscription.LastUpdate = DateTime.Now;
                }

                RSMPGS.JSon.CreateAndSendStatusUpdateMessage(StatusObject.RoadSideObject, sS);
              }
            }
          }
        }
      }
      catch
      {
      }


      /*
      int iSelectedColumn = 0;

      if (listview.SelectedItems.Count == 0)
      {
        return;
      }

      lvItem = listview.SelectedItems[0];

      ListViewHitTestInfo lvHitTest = listview.HitTest(LastMouseX, LastMouseY);

      foreach (ListViewItem.ListViewSubItem ScanSubItem in lvItem.SubItems)
      {
        if (lvHitTest.SubItem == ScanSubItem)
        {
          break;
        }
        iSelectedColumn++;
      }

      try
      {
        // Tag is ex Status_2
        if (listview.Columns[iSelectedColumn].Tag.ToString().StartsWith("Status", StringComparison.OrdinalIgnoreCase))
        {
          string sType = lvItem.SubItems[iSelectedColumn - 1].Text;
          string sText = lvHitTest.SubItem.Text;
          int iIndex = Int32.Parse(listview.Columns[iSelectedColumn].Tag.ToString().Substring(7));
          if (cFormsHelper.InputBox("Enter new status", "Status", ref sText, sType.Equals("base64", StringComparison.OrdinalIgnoreCase), true) == DialogResult.OK)
          {
            cStatusObject StatusObject = (cStatusObject)lvItem.Tag;
            StatusObject.StatusReturnValues[iIndex].sStatus = sText;
            lvHitTest.SubItem.Text = sText;
            // Find out if this status is subscribed
            foreach (cSubscription Subscription in StatusObject.RoadSideObject.Subscriptions)
            {
              if (Subscription.StatusReturnValue == StatusObject.StatusReturnValues[iIndex])
              {
                if (Subscription.SubscribeStatus == cSubscription.Subscribe_OnChange)
                {
                  List<RSMP_Messages.Status_VTQ> sS = new List<RSMP_Messages.Status_VTQ>();
                  RSMP_Messages.Status_VTQ s = new RSMP_Messages.Status_VTQ();
                  s.sCI = StatusObject.sStatusCodeId;
                  s.n = StatusObject.StatusReturnValues[iIndex].sName;
                  RSMPGS.ProcessImage.UpdateStatusValue(ref s, StatusObject.StatusReturnValues[iIndex].sType, StatusObject.StatusReturnValues[iIndex].sStatus);
                  sS.Add(s);
                  RSMPGS.JSon.CreateAndSendStatusUpdateMessage(StatusObject.RoadSideObject, sS);
                }
              }
            }
         }
        }
      }
      catch
      {
      }
      */

    }

  }
}
