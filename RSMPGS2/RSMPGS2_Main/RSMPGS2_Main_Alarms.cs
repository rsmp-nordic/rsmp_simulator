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

    public void UpdateAlarmListView(cSiteIdObject SiteIdObject, cRoadSideObject RoadSideObject)
    {

      listView_Alarms.Items.Clear();
      listView_AlarmEvents.Items.Clear();

      if (SiteIdObject == null && RoadSideObject == null)
      {
        return;
      }

      listView_Alarms.BeginUpdate();
      listView_Alarms.StopSorting();

      listView_AlarmEvents.StopSorting();

      if (RoadSideObject != null)
      {
        listView_Alarms.ShowGroups = ToolStripMenuItem_View_AlwaysShowGroupHeaders.Checked;
        UpdateAlarmListView(RoadSideObject);
      }
      else
      {
        listView_Alarms.ShowGroups = true;
        foreach (cRoadSideObject roadSideObject in SiteIdObject.RoadSideObjects)
        {
          UpdateAlarmListView(roadSideObject);
        }
      }

      listView_Alarms.ResumeSorting();
      listView_Alarms.EndUpdate();

      listView_AlarmEvents.ResumeSorting();

    }

    public void UpdateAlarmListView(cAlarmObject AlarmObject)
    {
      foreach (ListViewItem lvItem in listView_Alarms.Items)
      {
        if (lvItem.Tag == AlarmObject)
        {
          UpdateAlarmListView(lvItem);
          break;
        }
      }
    }

    public void UpdateAlarmListView(ListViewItem lvItem)
    {
      cAlarmObject AlarmObject = (cAlarmObject)lvItem.Tag;

      // alarmCodeId;description;externalAlarmCodeId;externalNtSAlarmCodeId;priority;category;name;type;value;Kommentar
      lvItem.Text = AlarmObject.StatusAsText();
      int iSubItemIndex = 1;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.AlarmCount > 0 ? AlarmObject.AlarmCount.ToString() : "";
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sAlarmCodeId;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sDescription.Split('\n').First().TrimEnd('.');
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sExternalAlarmCodeId;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sExternalNTSAlarmCodeId;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sPriority;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sCategory;

      foreach (cAlarmReturnValue AlarmReturnValue in AlarmObject.AlarmReturnValues)
      {
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.sName;
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.Value.GetValueType();
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.Value.GetValueType().Equals("array", StringComparison.OrdinalIgnoreCase)  ? "(array)" : AlarmReturnValue.Value.GetValue().ToString();
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.sComment.Replace("\n", " / ");
      }

    }

    public void UpdateAlarmListView(cRoadSideObject RoadSideObject)
    {

      RoadSideObject.AlarmsGroup.Items.Clear();

      //
      // View alarm objects for the selected RoadSide object
      //
      foreach (cAlarmObject AlarmObject in RoadSideObject.AlarmObjects)
      {
        ListViewItem lvItem = new ListViewItem();
        for (int iSubItemIndex = 0; iSubItemIndex < 7 + RSMPGS.ProcessImage.MaxAlarmReturnValues * 4; iSubItemIndex++)
        {
          lvItem.SubItems.Add("");
        }
        lvItem.Tag = AlarmObject;
        UpdateAlarmListView(lvItem);
        listView_Alarms.Items.Add(lvItem);
        lvItem.Group = RoadSideObject.AlarmsGroup;

        foreach (cAlarmEvent AlarmEvent in AlarmObject.AlarmEvents)
        {
          AddAlarmEventToList(AlarmObject, AlarmEvent);
        }
      }
    }

    private void contextMenuStrip_Alarm_Opening(object sender, CancelEventArgs e)
    {
      ListViewItem lvItem = (listView_Alarms.SelectedItems.Count > 0) ? listView_Alarms.SelectedItems[0] : null;

      if (lvItem != null && RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected)
      {
        cAlarmObject AlarmObject = (cAlarmObject)lvItem.Tag;

        if (!AlarmObject.bActive && AlarmObject.bAcknowledged &&
            RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected)
        {
          ToolStripMenuItem_Acknowledge.Enabled = false;
        }
        else
        {
          ToolStripMenuItem_Acknowledge.Enabled = true;
        }
        ToolStripMenuItem_Suspend.Enabled = true;
        ToolStripMenuItem_Suspend.Checked = AlarmObject.bSuspended == true;
        toolStripMenuItem_Alarm_RequestCurrentState.Enabled = (RSMPGS.JSon.NegotiatedRSMPVersion >= cJSon.RSMPVersion.RSMP_3_1_5) ? true : false;
      }
      else
      {
        ToolStripMenuItem_Acknowledge.Enabled = false;
        ToolStripMenuItem_Suspend.Enabled = false;
        toolStripMenuItem_Alarm_RequestCurrentState.Enabled = false;

      }
    }

    private void ToolStripMenuItem_SendAlarmEvent_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
      ListViewItem lvItem = listView_Alarms.SelectedItems[0];
      cAlarmObject AlarmObject = (cAlarmObject)lvItem.Tag;

      switch (menuitem.Tag.ToString())
      {
        case "AcknowledgeAndSend":
          RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Acknowledge);
          break;
        case "SuspendAndSend":
          RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, AlarmObject.bSuspended ? cJSon.AlarmSpecialisation.Resume : cJSon.AlarmSpecialisation.Suspend);
          break;
        case "RequestAndSend":
          RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Request);
          break;
      }
      lvItem.SubItems[0].Text = AlarmObject.StatusAsText();
    }

    public void AddAlarmEventToAlarmObjectAndToList(cAlarmObject AlarmObject, cAlarmEvent AlarmEvent)
    {

      if (RSMPGS_Main.bWriteEventsContinous)
      {
        RSMPGS.SysLog.EventLog("Alarm;{0}\tMId: {1}\tComponentId: {2}\tAlarmCodeId: {3}\tDirection: {4}\tEvent: {5}",
        AlarmEvent.sTimeStamp, AlarmEvent.sMessageId, AlarmObject.RoadSideObject.sComponentId, AlarmEvent.sAlarmCodeId, AlarmEvent.sDirection, AlarmEvent.sEvent);
      }

      if (RSMPGS_Main.iMaxEventsPerObject <= 0)
      {
        return;
      }

      AlarmObject.AlarmEvents.Add(AlarmEvent);

      while (AlarmObject.AlarmEvents.Count > RSMPGS_Main.iMaxEventsPerObject)
      {
        listView_AlarmEvents.Items.RemoveAt(0);
      }

      AddAlarmEventToList(AlarmObject, AlarmEvent);
    }

    public void AddAlarmEventToList(cAlarmObject AlarmObject, cAlarmEvent AlarmEvent)
    {

      if (bIsUpdatingAlarmEventList == false)
      {
        listView_AlarmEvents.StopSorting();
        listView_AlarmEvents.BeginUpdate();
        bIsUpdatingAlarmEventList = true;
      }

      ListViewItem lvItem = new ListViewItem(AlarmEvent.sTimeStamp);
      lvItem.SubItems.Add(AlarmObject.RoadSideObject.sComponentId + " / " + AlarmObject.RoadSideObject.sObject);
      lvItem.SubItems.Add(AlarmEvent.sMessageId);
      lvItem.SubItems.Add(AlarmEvent.sAlarmCodeId);
      lvItem.SubItems.Add(AlarmEvent.sDirection);
      lvItem.SubItems.Add(AlarmEvent.sEvent);

      foreach (cAlarmEventReturnValue AlarmReturnValues in AlarmEvent.AlarmEventReturnValues)
      {
        lvItem.SubItems.Add(AlarmReturnValues.sName);
        lvItem.SubItems.Add(AlarmReturnValues.oValue.ToString());
      }

      listView_AlarmEvents.Items.Add(lvItem);

    }
    private void listView_Alarms_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      ListView listview = (ListView)sender;
      ListViewItem lvItem;

      int iSelectedColumn = 0;

      if (listview.SelectedItems.Count == 0)
      {
        return;
      }

      lvItem = listview.SelectedItems[0];

      ListViewHitTestInfo lvHitTest = listview.HitTest(e.X, e.Y);

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
        // Tag is ex Value_2
        if ((listview.Columns[iSelectedColumn].Tag != null) && (listview.Columns[iSelectedColumn].Tag.ToString().StartsWith("Value", StringComparison.OrdinalIgnoreCase)))
        {
          int iIndex = Int32.Parse(listview.Columns[iSelectedColumn].Tag.ToString().Substring(6));
          cAlarmObject AlarmObject = (cAlarmObject)lvItem.Tag;
          cAlarmReturnValue AlarmReturnValue = AlarmObject.AlarmReturnValues[iIndex];
          string sValue = lvHitTest.SubItem.Text;
          List<Dictionary<string, object>> array = AlarmReturnValue.Value.GetArray();
          cFormsHelper.InputStatusBoxValueType("View alarm", ref sValue, ref array, AlarmReturnValue.Value, AlarmReturnValue.sComment, true, true);
        }
      }
      catch
      { }
    }
  }
}