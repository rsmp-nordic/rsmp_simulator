using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsRSMPGS
{
    public partial class RSMPGS2_CommandForm : Form
    {
        //private List<cCommandReturnValue> selectedCommands;
        private cRoadSideObject RoadSideObject;
        private List<int> iRows = new List<int>();

        public RSMPGS2_CommandForm(cRoadSideObject RoadSideObject, List<cCommandReturnValue> SelectedCRVs)
        {

            InitializeComponent();

            this.RoadSideObject = RoadSideObject;

            groupBox_Commands.Text = "Object: " + RoadSideObject.sComponentId;

            int i = 0;


            foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
            {
                foreach (cCommandReturnValue CommandArguments in CommandObject.CommandReturnValues)
                {
                    bool bWasSelected = SelectedCRVs.IndexOf(CommandArguments) >= 0 ? true : false;

                    string[] aCommands;
                    if (CommandArguments.Value.ValueTypeObject.SelectableValues != null && CommandArguments.Value.ValueTypeObject.SelectableValues.Count > 0)
                    {
                        aCommands = CommandArguments.Value.ValueTypeObject.SelectableValues.Values.ToArray<string>();
                    }
                    else
                    {
                        aCommands = CommandArguments.Value.ValueTypeObject.sRange.Split('\n');

                        for (int j = 0; j < aCommands.Length; j++)
                        {
                            aCommands[j] = aCommands[j].TrimStart('"');
                            aCommands[j] = aCommands[j].TrimStart('-');
                            aCommands[j] = aCommands[j].TrimEnd('"');
                        }
                    }


                    /*
                        string[] aCommands = CommandArguments.sValue.Split('\n');

                    for (int j = 0; j < aCommands.Length; j++)
                    {
                      aCommands[j] = aCommands[j].TrimStart('"');
                      aCommands[j] = aCommands[j].TrimStart('-');
                      aCommands[j] = aCommands[j].TrimEnd('"');
                    }
                    */
                    if ((CommandArguments.Value.ValueTypeObject.sRange == "" || aCommands.Length < 2))
                    {
                        DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                        this.dataGridView_Commands.Rows.Add(bWasSelected, CommandObject.sCommandCodeId, CommandArguments.sName, CommandArguments.sCommand);
                        this.dataGridView_Commands.Rows[i].Cells[4] = txtcell;

                        if (CommandArguments.Value.GetValueType().Equals("base64", StringComparison.OrdinalIgnoreCase))
                        {
                            iRows.Add(i);
                        }

                        this.dataGridView_Commands.Rows[i].Cells[4].Value = CommandArguments.Value.GetValue();
                        //this.dataGridView_Commands.Rows[i].Cells[4].ReadOnly = CommandArguments.sValue == "" || CommandArguments.sValue.EndsWith("]") ? false : true;
                    }
                    else
                    {
                        this.dataGridView_Commands.Rows.Add(bWasSelected, CommandObject.sCommandCodeId, CommandArguments.sName, CommandArguments.sCommand);
                        DataGridViewComboBoxCell combocell = (DataGridViewComboBoxCell)dataGridView_Commands.Rows[i].Cells[4];
                        combocell.Items.AddRange(aCommands);
                        this.dataGridView_Commands.Rows[i].Cells[4].Value = aCommands[0];
                    }
                    i++;
                }
            }
        }

        private void dataGridView_Commands_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            foreach (int iRow in iRows)
            {
                if (iRow == e.RowIndex && e.ColumnIndex == 4)
                {
                    DataGridViewCell cell = (DataGridViewCell)
                    dataGridView_Commands.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "All files|*.*";
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView_Commands.Rows[e.RowIndex].Cells[4].Value = openFileDialog.FileName;
                    }

                    break;
                }
            }
        }

        private void button_Commands_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_Commands_Send_Click(object sender, EventArgs e)
        {

            bool bUseCaseSensitiveIds = cHelper.IsSettingChecked("UseCaseSensitiveIds");

            List<cCommandReturnValue> lSelectedCommands = new List<cCommandReturnValue>();
            int i = 0;
            foreach (cCommandObject CommandObject in RoadSideObject.CommandObjects)
            {
                foreach (cCommandReturnValue CommandArguments in CommandObject.CommandReturnValues)
                {
                    if (this.dataGridView_Commands.Rows[i].Cells[0].Value != null &&
                            (bool)this.dataGridView_Commands.Rows[i].Cells[0].Value == true)
                    {
                        cCommandReturnValue CommandReturnValue = new cCommandReturnValue(CommandArguments.CommandObject);
                        //CommandReturnValue.sCommandCodeId = CommandArguments.sCommandCodeId;
                        CommandReturnValue.sName = CommandArguments.sName;
                        CommandReturnValue.sCommand = CommandArguments.sCommand;
                        CommandReturnValue.Value = new cValue(CommandArguments.Value.ValueTypeObject, false);
                        CommandReturnValue.CommandObject = CommandObject;

                        if (this.dataGridView_Commands.Rows[i].Cells[4].Value == null
                                            || this.dataGridView_Commands.Rows[i].Cells[4].Value.ToString().Trim().Length == 0)
                        {
                            MessageBox.Show(this, "Can't send message with empty value!");
                            return;
                        }

                        //if (CommandArguments.sValue.Length == 0)
                        //{
                        CommandReturnValue.Value.SetValue(this.dataGridView_Commands.Rows[i].Cells[4].Value.ToString().Trim());

                        lSelectedCommands.Add(CommandReturnValue);

                    }
                    i++;
                }
            }

            if (lSelectedCommands.Count > 0)
            {
                if (lSelectedCommands.Count > 0)
                {
                    RSMPGS.JSon.CreateAndSendCommandMessage(RoadSideObject, lSelectedCommands, bUseCaseSensitiveIds);
                }
                //Close();
            }
            else
            {
                MessageBox.Show(this, "No command is selected!");
            }
        }

        private void dataGridView_Commands_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.ColumnIndex == 0)
            {
              dataGridView_Commands.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
            }
            */
        }

        private void dataGridView_Commands_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            /*
            if (dataGridView_Commands.IsCurrentCellDirty)
            {
              dataGridView_Commands.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            */
        }

        private void dataGridView_Commands_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            // The checkbox does not change by clicking any more (due to .NET changes it failed when updating to 4.6.2 ?)

            if (e.ColumnIndex == 0)
            {
                bool bValue = (bool)dataGridView_Commands.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                bValue = bValue ? false : true;
                dataGridView_Commands.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = bValue;
                dataGridView_Commands.EndEdit();
                //dataGridView_Commands.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }

        /*
      public List<cCommandReturnValue> GetSelectedCommands()
      {
          return selectedCommands;
      }
    */
    }
}
