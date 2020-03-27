namespace nsRSMPGS
{
    partial class RSMPGS2_StatusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
					this.button_Commands_Cancel = new System.Windows.Forms.Button();
					this.button_StatusReq_Send = new System.Windows.Forms.Button();
					this.groupBox_Status = new System.Windows.Forms.GroupBox();
					this.dataGridView_Status = new System.Windows.Forms.DataGridView();
					this.Column_Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
					this.Column_StatusCommandId = new System.Windows.Forms.DataGridViewTextBoxColumn();
					this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
					this.groupBox_Status.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.dataGridView_Status)).BeginInit();
					this.SuspendLayout();
					// 
					// button_Commands_Cancel
					// 
					this.button_Commands_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.button_Commands_Cancel.Location = new System.Drawing.Point(359, 324);
					this.button_Commands_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
					this.button_Commands_Cancel.Name = "button_Commands_Cancel";
					this.button_Commands_Cancel.Size = new System.Drawing.Size(100, 28);
					this.button_Commands_Cancel.TabIndex = 4;
					this.button_Commands_Cancel.Text = "Cancel";
					this.button_Commands_Cancel.UseVisualStyleBackColor = true;
					this.button_Commands_Cancel.Click += new System.EventHandler(this.button_Commands_Cancel_Click);
					// 
					// button_StatusReq_Send
					// 
					this.button_StatusReq_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.button_StatusReq_Send.Location = new System.Drawing.Point(199, 324);
					this.button_StatusReq_Send.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
					this.button_StatusReq_Send.Name = "button_StatusReq_Send";
					this.button_StatusReq_Send.Size = new System.Drawing.Size(152, 28);
					this.button_StatusReq_Send.TabIndex = 3;
					this.button_StatusReq_Send.Text = "Send";
					this.button_StatusReq_Send.UseVisualStyleBackColor = true;
					this.button_StatusReq_Send.Click += new System.EventHandler(this.button_StatusReq_Send_Click);
					// 
					// groupBox_Status
					// 
					this.groupBox_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
											| System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.groupBox_Status.Controls.Add(this.dataGridView_Status);
					this.groupBox_Status.Location = new System.Drawing.Point(-1, 4);
					this.groupBox_Status.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
					this.groupBox_Status.Name = "groupBox_Status";
					this.groupBox_Status.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
					this.groupBox_Status.Size = new System.Drawing.Size(460, 313);
					this.groupBox_Status.TabIndex = 5;
					this.groupBox_Status.TabStop = false;
					this.groupBox_Status.Text = "groupBox1";
					// 
					// dataGridView_Status
					// 
					this.dataGridView_Status.AllowUserToAddRows = false;
					this.dataGridView_Status.AllowUserToDeleteRows = false;
					this.dataGridView_Status.AllowUserToResizeColumns = false;
					this.dataGridView_Status.AllowUserToResizeRows = false;
					this.dataGridView_Status.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
					this.dataGridView_Status.BackgroundColor = System.Drawing.SystemColors.Window;
					this.dataGridView_Status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
					this.dataGridView_Status.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
					this.dataGridView_Status.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Selected,
            this.Column_StatusCommandId,
            this.Column_Name});
					this.dataGridView_Status.Cursor = System.Windows.Forms.Cursors.Default;
					this.dataGridView_Status.Dock = System.Windows.Forms.DockStyle.Fill;
					this.dataGridView_Status.Location = new System.Drawing.Point(4, 19);
					this.dataGridView_Status.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
					this.dataGridView_Status.MultiSelect = false;
					this.dataGridView_Status.Name = "dataGridView_Status";
					this.dataGridView_Status.RowHeadersVisible = false;
					this.dataGridView_Status.RowTemplate.Height = 24;
					this.dataGridView_Status.Size = new System.Drawing.Size(452, 290);
					this.dataGridView_Status.TabIndex = 0;
					// 
					// Column_Selected
					// 
					this.Column_Selected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
					this.Column_Selected.HeaderText = "";
					this.Column_Selected.Name = "Column_Selected";
					this.Column_Selected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
					this.Column_Selected.ToolTipText = "Select Commands";
					this.Column_Selected.Width = 21;
					// 
					// Column_StatusCommandId
					// 
					this.Column_StatusCommandId.HeaderText = "StatusCommandId";
					this.Column_StatusCommandId.Name = "Column_StatusCommandId";
					this.Column_StatusCommandId.ReadOnly = true;
					this.Column_StatusCommandId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
					// 
					// Column_Name
					// 
					this.Column_Name.HeaderText = "Name";
					this.Column_Name.Name = "Column_Name";
					this.Column_Name.ReadOnly = true;
					this.Column_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
					// 
					// RSMPGS2_StatusForm
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(464, 361);
					this.Controls.Add(this.groupBox_Status);
					this.Controls.Add(this.button_Commands_Cancel);
					this.Controls.Add(this.button_StatusReq_Send);
					this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
					this.Name = "RSMPGS2_StatusForm";
					this.Text = "RSMPGS2 - Status Request";
					this.groupBox_Status.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.dataGridView_Status)).EndInit();
					this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Commands_Cancel;
        private System.Windows.Forms.Button button_StatusReq_Send;
        private System.Windows.Forms.GroupBox groupBox_Status;
        private System.Windows.Forms.DataGridView dataGridView_Status;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_StatusCommandId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;

    }
}