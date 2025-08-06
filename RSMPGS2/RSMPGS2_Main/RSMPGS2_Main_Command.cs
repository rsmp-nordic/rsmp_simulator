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

    private void contextMenuStrip_Commands_Opening(object sender, CancelEventArgs e)
    {
      ToolStripMenuItem_Commands.Enabled = listView_Commands.SelectedItems.Count > 0 && RSMPGS.RSMPConnection.ConnectionStatus() == cTcpSocket.ConnectionStatus_Connected;
    }

    private void ToolStripMenuItem_Commands_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;

      cRoadSideObject RoadSideObject = ((cCommandReturnValue)listView_Commands.SelectedItems[0].Tag).CommandObject.RoadSideObject;
      cCommandObject CommandObject = ((cCommandReturnValue)listView_Commands.SelectedItems[0].Tag).CommandObject;

      RSMPGS2_CommandForm CommandForm = new RSMPGS2_CommandForm(RoadSideObject, CommandObject);

      CommandForm.ShowDialog(this);
      /*
      List<cCommandReturnValue> selectedCommands = CommandForm.GetSelectedCommands();
      if (selectedCommands != null && selectedCommands.Count > 0)
      {
        RSMPGS.JSon.CreateAndSendCommandMessage(CommandObject.getRoadSideObject(), selectedCommands);
      }
      */
    }

    public void UpdateCommandListView(cSiteIdObject SiteIdObject, cRoadSideObject RoadSideObject)
    {

      //
      // View command objects for the selected RoadSide object
      //

      listView_Commands.Items.Clear();
      listView_CommandEvents.Items.Clear();

      if (SiteIdObject == null && RoadSideObject == null)
      {
        return;
      }

      listView_Commands.BeginUpdate();
      listView_Commands.StopSorting();

      listView_CommandEvents.BeginUpdate();

      if (RoadSideObject != null)
      {
        listView_Commands.ShowGroups = ToolStripMenuItem_View_AlwaysShowGroupHeaders.Checked;
        UpdateCommandListView(RoadSideObject);
      }
      else
      {
        listView_Commands.ShowGroups = true;
        foreach (cRoadSideObject roadSideObject in SiteIdObject.RoadSideObjects)
        {
          UpdateCommandListView(roadSideObject);
        }
      }

      listView_Commands.ResumeSorting();
      listView_Commands.EndUpdate();

      listView_CommandEvents.EndUpdate();

    }

    public void UpdateCommandListView(cRoadSideObject RoadSideObject)
    {
      //
      // View command objects for the selected RoadSide object
      //


      if (RoadSideObject == null)
      {
        return;
      }

      RoadSideObject.CommandsGroup.Items.Clear();

      foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
      {
        // Object type;Object (optional);Description;commandCodeId;name;command;type;value;Comment;name;command;type;value;Comment;


        foreach (cCommandReturnValue CommandReturnValue in CommandObject.CommandReturnValues)
        {

          ListViewItem lvItem = new ListViewItem(CommandObject.sCommandCodeId);
          lvItem.SubItems.Add(CommandObject.sDescription.Split('\n').First().TrimEnd('.'));
          lvItem.SubItems.Add(CommandReturnValue.sName);
          lvItem.SubItems.Add(CommandReturnValue.sCommand.Replace("\"-", "").Replace("\n-", "/").Replace("\n", "/").Replace("\"", ""));
          lvItem.SubItems.Add(CommandReturnValue.Value.GetValueType());
#if _RSMPGS2
          lvItem.SubItems.Add(CommandReturnValue.sLastRecValue);
          lvItem.SubItems.Add(CommandReturnValue.sLastRecAge);
#endif
#if _RSMPGS1
          lvItem.SubItems.Add(CommandReturnValue.sValue);
#endif
          lvItem.SubItems.Add(CommandReturnValue.sComment);
          lvItem.Tag = CommandReturnValue;

          listView_Commands.Items.Add(lvItem);

          lvItem.Group = RoadSideObject.CommandsGroup;

        }

      }

      foreach (cCommandEvent CommandEvent in RoadSideObject.CommandEvents)
      {
        AddCommandEventToList(RoadSideObject, CommandEvent);
      }

    }

    private void AddCommandEventToList(cRoadSideObject RoadSideObject, cCommandEvent CommandEvent)
    {
      ListViewItem lvItem = listView_CommandEvents.Items.Add(CommandEvent.sTimeStamp.ToString());
      lvItem.SubItems.Add(CommandEvent.sMessageId);
      lvItem.SubItems.Add(CommandEvent.sEvent);
      lvItem.SubItems.Add(CommandEvent.sCommandCodeId);
      lvItem.SubItems.Add(CommandEvent.sName);
      lvItem.SubItems.Add(CommandEvent.sCommand);
      lvItem.SubItems.Add(CommandEvent.oValue.ToString());
      lvItem.SubItems.Add(CommandEvent.sAge);


    }

    public void HandleCommandListUpdate(cRoadSideObject RoadSideObject, string sntsOId, string scId, cCommandEvent CommandEvent, bool bSend, bool bUseCaseSensitiveIds)
    {

      if (SelectedRoadSideObject == null)
      {
        cRoadSideObject ScanRoadSideObject = cHelper.FindRoadSideObject(sntsOId, scId, bUseCaseSensitiveIds);

          if (ScanRoadSideObject != null)
          {
            if (!bSend)
            {
              for (int i = 0; i < listView_Commands.Items.Count; i++)
              {
                ListViewItem lvItem = listView_Commands.Items[i];
                if (lvItem.SubItems[3].Text.Equals(CommandEvent.sCommandCodeId, StringComparison.OrdinalIgnoreCase)
                && lvItem.SubItems[4].Text.Equals(CommandEvent.sName, StringComparison.OrdinalIgnoreCase))
                {
                  lvItem.SubItems[7].Text = CommandEvent.oValue.ToString();
                  lvItem.SubItems[8].Text = CommandEvent.sAge;

                  break;
                }
              }
            }
            AddCommandEventToList(RoadSideObject, CommandEvent);

          }
      }
      else
      {
        if (SelectedRoadSideObject == RoadSideObject)
        {
          if (!bSend)
          {
            for (int i = 0; i < listView_Commands.Items.Count; i++)
            {

              ListViewItem lvItem = listView_Commands.Items[i];
              if (lvItem.SubItems[0].Text.Equals(CommandEvent.sCommandCodeId, StringComparison.OrdinalIgnoreCase)
              && lvItem.SubItems[2].Text.Equals(CommandEvent.sName, StringComparison.OrdinalIgnoreCase))
              {
                lvItem.SubItems[5].Text = CommandEvent.oValue.ToString();
                lvItem.SubItems[6].Text = CommandEvent.sAge;

                break;
              }
            }
          }
          AddCommandEventToList(RoadSideObject, CommandEvent);
        }
      }
    }
  }
}
