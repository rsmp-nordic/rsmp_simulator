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
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sDescription;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sExternalAlarmCodeId;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sExternalNTSAlarmCodeId;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sPriority;
      lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.sCategory;

      foreach (cAlarmReturnValue AlarmReturnValue in AlarmObject.AlarmReturnValues)
      {
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.sName;
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.sType;
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.sValue;
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.sComment;
      }

    }

    public void UpdateAlarmListView(cRoadSideObject RoadSideObject)
    {

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
      }
      else
      {
        ToolStripMenuItem_Acknowledge.Enabled = false;
        ToolStripMenuItem_Suspend.Enabled = false;
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
          RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation_Acknowledge);
          break;
        case "SuspendAndSend":
          RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation_Suspend);
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

      ListViewItem lvItem = listView_AlarmEvents.Items.Add(AlarmEvent.sTimeStamp);
      lvItem.SubItems.Add(AlarmObject.RoadSideObject.sComponentId + " / " + AlarmObject.RoadSideObject.sObject);
      lvItem.SubItems.Add(AlarmEvent.sMessageId);
      lvItem.SubItems.Add(AlarmEvent.sAlarmCodeId);
      lvItem.SubItems.Add(AlarmEvent.sDirection);
      lvItem.SubItems.Add(AlarmEvent.sEvent);

      foreach (cAlarmEventReturnValue AlarmReturnValues in AlarmEvent.AlarmEventReturnValues)
      {
        lvItem.SubItems.Add(AlarmReturnValues.sName);
        lvItem.SubItems.Add(AlarmReturnValues.sValue);
      }

    }

  }
}