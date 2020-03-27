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
	public partial class RSMPGS2_StatusForm : Form
	{
		private List<cStatusReturnValue> selectedStatus;
		private cRoadSideObject RoadSideObject;
		private bool bStatReq;
		private bool bStatSub;

		public RSMPGS2_StatusForm(cRoadSideObject RoadSideObject, bool bStatReq, bool bStatSub)
		{
			InitializeComponent();
			this.RoadSideObject = RoadSideObject;
			this.bStatReq = bStatReq;
			this.bStatSub = bStatSub;
			groupBox_Status.Text = "Object: " + RoadSideObject.sComponentId;

			if (!bStatReq && bStatSub)
			{
				dataGridView_Status.ColumnCount = 4;
				dataGridView_Status.Columns[3].Name = "Column_UpdateRate";
				dataGridView_Status.Columns[3].HeaderText = "UpdateRate";
				dataGridView_Status.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
				dataGridView_Status.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
			}

			int i = 0;

			foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
			{
				foreach (cStatusReturnValue StatusArguments in StatusObject.StatusReturnValues)
				{
					if (!bStatReq && bStatSub && (StatusArguments.sLastUpdateRate != null) && (StatusArguments.sLastUpdateRate.Length > 0))
					{
						this.dataGridView_Status.Rows.Add(false, StatusArguments.sStatusCommandId, StatusArguments.sName, StatusArguments.sLastUpdateRate);
					}
					else
					{
						this.dataGridView_Status.Rows.Add(false, StatusArguments.sStatusCommandId, StatusArguments.sName, "");
					}

					if (!bStatReq && bStatSub)
					{
						this.dataGridView_Status.Rows[i].Cells[dataGridView_Status.ColumnCount - 1].ReadOnly = false;
					}
					i++;
				}
			}
		}

		private void button_Commands_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button_StatusReq_Send_Click(object sender, EventArgs e)
		{

			List<cStatusReturnValue> lSelectedStatus = new List<cStatusReturnValue>();
			int i = 0;
			foreach (cStatusObject StatusObject in RoadSideObject.StatusObjects)
			{
				foreach (cStatusReturnValue StatusArguments in StatusObject.StatusReturnValues)
				{
					if (this.dataGridView_Status.Rows[i].Cells[0].Value != null &&
							(bool)this.dataGridView_Status.Rows[i].Cells[0].Value == true)
					{
						cStatusReturnValue StatusReturnValue = new cStatusReturnValue();
						StatusReturnValue.sName = StatusArguments.sName;
						StatusReturnValue.sStatusCommandId = StatusArguments.sStatusCommandId;
						StatusReturnValue.sType = StatusArguments.sType;

						if (bStatReq)
						{
							// Status Request
						}
						else if (bStatSub)
						{
							// Status subscribe
							if (this.dataGridView_Status.Rows[i].Cells[this.dataGridView_Status.ColumnCount - 1].Value == null
									|| this.dataGridView_Status.Rows[i].Cells[this.dataGridView_Status.ColumnCount - 1].Value.ToString().Trim().Length == 0)
							{
								MessageBox.Show("Can´t send message with empty value!");
								return;
							}

							StatusReturnValue.sLastUpdateRate = this.dataGridView_Status.Rows[i].Cells[this.dataGridView_Status.ColumnCount - 1].Value.ToString().Trim();
							StatusArguments.sLastUpdateRate = StatusReturnValue.sLastUpdateRate;
						}
						else
						{
							// Status unsubscribe
							StatusArguments.sLastUpdateRate = null;
						}
						lSelectedStatus.Add(StatusReturnValue);
					}
					i++;
				}
			}
			if (lSelectedStatus.Count > 0)
			{
				selectedStatus = lSelectedStatus;
				Close();
			}
			else
			{
				MessageBox.Show("No Status is selected!");
			}
		}

		public List<cStatusReturnValue> GetSelectedStatus()
		{
			return selectedStatus;
		}
	}
}
