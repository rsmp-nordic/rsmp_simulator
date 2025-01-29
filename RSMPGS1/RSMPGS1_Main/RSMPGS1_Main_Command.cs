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

    public void UpdateCommandListView(cSiteIdObject SiteIdObject, cRoadSideObject RoadSideObject)
    {

      //
      // View command objects for the selected RoadSide object
      //

      listView_Commands.Items.Clear();

      if (SiteIdObject == null && RoadSideObject == null)
      {
        return;
      }

      listView_Commands.BeginUpdate();
      listView_Commands.StopSorting();

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
    }


    public void UpdateCommandListView(cRoadSideObject RoadSideObject)
    {

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
          lvItem.SubItems.Add(CommandReturnValue.sCommand.Replace("\"-", "").Replace("\n-", " / ").Replace("\n", " / ").Replace("\"", ""));
          lvItem.SubItems.Add(CommandReturnValue.Value.GetValueType());
#if _RSMPGS2
          lvItem.SubItems.Add(CommandReturnValue.sLastRecValue);
					lvItem.SubItems.Add(CommandReturnValue.sLastRecAge);
#endif
#if _RSMPGS1
          lvItem.SubItems.Add(CommandReturnValue.Value.GetValue().ToString());
          lvItem.SubItems.Add("");
#endif
          lvItem.SubItems.Add(CommandReturnValue.sComment.Replace("\n", " / "));
          lvItem.Tag = CommandReturnValue;
          listView_Commands.Items.Add(lvItem);
          lvItem.Group = RoadSideObject.CommandsGroup;

        }
      }
    }

    public void HandleCommandListUpdate(cRoadSideObject RoadSideObject, cCommandObject CommandObject, cCommandReturnValue CommandReturnValue)
    {

      foreach (ListViewItem lvItem in listView_Commands.Items)
      {
        if ((cCommandReturnValue)lvItem.Tag == CommandReturnValue)
        {
          lvItem.SubItems[5].Text = CommandReturnValue.Value.GetValue().ToString();
          lvItem.SubItems[6].Text = CommandReturnValue.Value.Quality.ToString();
          break;
        }
      }
    }
  }
}
