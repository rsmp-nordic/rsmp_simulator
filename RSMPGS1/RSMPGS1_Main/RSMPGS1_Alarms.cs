using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using RSMP_Messages;
using System.Reflection;

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
      //lvItem.SubItems[iSubItemIndex++].Text = AlarmObject.AlarmEvents.Count() > 0 ? AlarmObject.AlarmEvents.Count().ToString() : "";
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
        lvItem.SubItems[iSubItemIndex++].Text = AlarmReturnValue.Value.GetValue().ToString(); ;
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
      if (lvItem == null || (RSMPGS.RSMPConnection.ConnectionStatus() != cTcpSocket.ConnectionStatus_Connected &&
				cHelper.IsSettingChecked("BufferAndSendAlarmsWhenConnect") == false))
       {
        ToolStripMenuItem_Active.Enabled = false;
        ToolStripMenuItem_Acknowledge.Enabled = false;
        ToolStripMenuItem_Suspend.Enabled = false;
      }
      else
      { 
        cAlarmObject AlarmObject = (cAlarmObject)lvItem.Tag;
        ToolStripMenuItem_Active.Enabled = true;
        ToolStripMenuItem_Acknowledge.Enabled = AlarmObject.bAcknowledged == false;
        ToolStripMenuItem_Suspend.Enabled = true;
        ToolStripMenuItem_Active.Checked = AlarmObject.bActive == true;
        ToolStripMenuItem_Suspend.Checked = AlarmObject.bSuspended == true;
      }
    }

    private void ToolStripMenuItem_SendAlarmEvent_Click(object sender, EventArgs e)
    {

      if (listView_Alarms.SelectedItems.Count == 0)
      {
        return;
      }

      ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
      ListViewItem lvItem = listView_Alarms.SelectedItems[0];
      cAlarmObject AlarmObject = (cAlarmObject)lvItem.Tag;
      cAlarmEvent AlarmEvent = null;

      AlarmObject.dtLastChangedAlarmStatus = DateTime.Now;

      switch (menuitem.Tag.ToString())
      {

        case "ActiveAndSend":

          AlarmObject.bActive = AlarmObject.bActive == false;
					if (AlarmObject.bActive)
					{
            AlarmObject.AlarmCount++;
            AlarmObject.bAcknowledged = false;
					}
          if (!AlarmObject.bSuspended)
          {
            RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Issue, out AlarmEvent);
          }
          break;

        case "AcknowledgeAndSend":

          AlarmObject.bAcknowledged = true;
          if (!AlarmObject.bSuspended)
          {
            RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Acknowledge, out AlarmEvent);
          }
          //JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation_Acknowledge);
          //foreach (cAlarmEvent ScanAlarmEvent in AlarmObject.AlarmEvents)
          //{
          RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Local ack of alarm: " + AlarmObject.sAlarmCodeId);
          //RSMPGS.SysLog.SysLog(cSysLogAndDebug.Severity.Info, "Local ack of alarm, TimeStamp: " + ScanAlarmEvent.sTimeStamp + ", MsgId: " + ScanAlarmEvent.sMessageId);// + ", SequenceNumber: " + ScanAlarmEvent.sSequenceNumber);
          //}
          //AlarmObject.AlarmEvents.Clear();
          //while (AlarmObject.AlarmEvents.Count > 0) AlarmObject.AlarmEvents.RemoveAt(0);
          //listView_AlarmEvents.Items.Clear();
          break;

        case "SuspendAndSend":

          AlarmObject.bSuspended = AlarmObject.bSuspended == false;
          RSMPGS.JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation.Suspend, out AlarmEvent);
					//JSon.CreateAndSendAlarmMessage(AlarmObject, cJSon.AlarmSpecialisation_Suspend);
          break;

      }

      if (AlarmEvent != null)
      {
        AddAlarmEventToAlarmObjectAndToList(AlarmObject, AlarmEvent);
      }

      UpdateAlarmListView(AlarmObject);

    }
    /*
    private void listView_Alarms_SelectedIndexChanged(object sender, EventArgs e)
    {
      listView_AlarmEvents.Items.Clear();
      if (listView_Alarms.SelectedItems.Count == 0)
      {
        return;
      }
      cAlarmObject AlarmObject = (cAlarmObject)listView_Alarms.SelectedItems[0].Tag;
      foreach (cAlarmEvent AlarmEvent in AlarmObject.AlarmEvents)
      {
        AddAlarmEventToList(AlarmObject, listView_Alarms.SelectedItems[0], AlarmEvent);
      }
    }
    */

    public void AddAlarmEventToAlarmObjectAndToList(cAlarmObject AlarmObject, cAlarmEvent AlarmEvent)
    {

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

        if (AlarmReturnValues.oValue.GetType() == typeof(string))
        {
          lvItem.SubItems.Add(AlarmReturnValues.oValue.ToString());
        }
        else
        {
          lvItem.SubItems.Add("(array)");
        }
      }
    }

    private void listView_Alarms_MouseClick(object sender, MouseEventArgs e)
    {
      LastMouseX = e.X;
      LastMouseY = e.Y;
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
        if(iSelectedColumn == 4) // ExternalAlarmCodeId
        {
          cAlarmObject AlarmObject = (cAlarmObject)lvItem.Tag;
          string sValue = lvHitTest.SubItem.Text;
          List<Dictionary<string, object>> array = new List<Dictionary<string, object>>();
          cValueTypeObject sExternalAlarmCodeId_type = new cValueTypeObject(null, null, "string", null, 0, 0, null, null);
          cValue cExternalAlarmCodeId = new cValue(sExternalAlarmCodeId_type, true);
          cExternalAlarmCodeId.SetValue(AlarmObject.sExternalAlarmCodeId);

          if (cFormsHelper.InputStatusBoxValueType("Enter new value", ref sValue, ref array, cExternalAlarmCodeId, "Manufacturer specific alarm code and alarm description", true, false) == DialogResult.OK)
          {
            AlarmObject.sExternalAlarmCodeId = sValue;
            lvHitTest.SubItem.Text = sValue;
          }
        }

        // Tag is ex Value_2
        if ((listview.Columns[iSelectedColumn].Tag != null) && (listview.Columns[iSelectedColumn].Tag.ToString().StartsWith("Value", StringComparison.OrdinalIgnoreCase)))
        {
          int iIndex = Int32.Parse(listview.Columns[iSelectedColumn].Tag.ToString().Substring(6));
          cAlarmObject AlarmObject = (cAlarmObject)lvItem.Tag;
          cAlarmReturnValue AlarmReturnValue = AlarmObject.AlarmReturnValues[iIndex];
          string sValue = lvHitTest.SubItem.Text;
          List<Dictionary<string, object>> array = AlarmReturnValue.Value.GetArray();
          if (cFormsHelper.InputStatusBoxValueType("Enter new value", ref sValue, ref array, AlarmReturnValue.Value, AlarmReturnValue.sComment, true, false) == DialogResult.OK)
          {
            AlarmReturnValue.Value.SetValue(sValue);
            AlarmReturnValue.Value.SetArray(array);
            lvHitTest.SubItem.Text = sValue;            
          }
        }
      }
      catch
      {
      }

    }
  }
}