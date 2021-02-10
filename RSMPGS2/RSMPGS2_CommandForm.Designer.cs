namespace nsRSMPGS
{
    partial class RSMPGS2_CommandForm
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
            this.groupBox_Commands = new System.Windows.Forms.GroupBox();
            this.dataGridView_Commands = new System.Windows.Forms.DataGridView();
            this.button_Commands_Send = new System.Windows.Forms.Button();
            this.button_Commands_Cancel = new System.Windows.Forms.Button();
            this.Column_Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_CommandCodeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Value = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox_Commands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Commands)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_Commands
            // 
            this.groupBox_Commands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Commands.Controls.Add(this.dataGridView_Commands);
            this.groupBox_Commands.Location = new System.Drawing.Point(3, 1);
            this.groupBox_Commands.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_Commands.Name = "groupBox_Commands";
            this.groupBox_Commands.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_Commands.Size = new System.Drawing.Size(537, 358);
            this.groupBox_Commands.TabIndex = 0;
            this.groupBox_Commands.TabStop = false;
            this.groupBox_Commands.Text = "groupBox1";
            // 
            // dataGridView_Commands
            // 
            this.dataGridView_Commands.AllowUserToAddRows = false;
            this.dataGridView_Commands.AllowUserToDeleteRows = false;
            this.dataGridView_Commands.AllowUserToResizeColumns = false;
            this.dataGridView_Commands.AllowUserToResizeRows = false;
            this.dataGridView_Commands.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Commands.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView_Commands.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_Commands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Commands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Selected,
            this.Column_CommandCodeId,
            this.Column_Name,
            this.Column_Command,
            this.Column_Value});
            this.dataGridView_Commands.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Commands.Location = new System.Drawing.Point(4, 19);
            this.dataGridView_Commands.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_Commands.MultiSelect = false;
            this.dataGridView_Commands.Name = "dataGridView_Commands";
            this.dataGridView_Commands.RowHeadersVisible = false;
            this.dataGridView_Commands.Size = new System.Drawing.Size(529, 335);
            this.dataGridView_Commands.TabIndex = 0;
            this.dataGridView_Commands.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Commands_CellClick);
            this.dataGridView_Commands.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Commands_CellContentClick);
            this.dataGridView_Commands.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_Commands_CellMouseClick);
            this.dataGridView_Commands.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_Commands_CurrentCellDirtyStateChanged);
            // 
            // button_Commands_Send
            // 
            this.button_Commands_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Commands_Send.Location = new System.Drawing.Point(301, 367);
            this.button_Commands_Send.Margin = new System.Windows.Forms.Padding(4);
            this.button_Commands_Send.Name = "button_Commands_Send";
            this.button_Commands_Send.Size = new System.Drawing.Size(131, 28);
            this.button_Commands_Send.TabIndex = 1;
            this.button_Commands_Send.Text = "Send UnSubscription";
            this.button_Commands_Send.UseVisualStyleBackColor = true;
            this.button_Commands_Send.Click += new System.EventHandler(this.button_Commands_Send_Click);
            // 
            // button_Commands_Cancel
            // 
            this.button_Commands_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Commands_Cancel.Location = new System.Drawing.Point(440, 367);
            this.button_Commands_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.button_Commands_Cancel.Name = "button_Commands_Cancel";
            this.button_Commands_Cancel.Size = new System.Drawing.Size(100, 28);
            this.button_Commands_Cancel.TabIndex = 2;
            this.button_Commands_Cancel.Text = "Cancel";
            this.button_Commands_Cancel.UseVisualStyleBackColor = true;
            this.button_Commands_Cancel.Click += new System.EventHandler(this.button_Commands_Cancel_Click);
            // 
            // Column_Selected
            // 
            this.Column_Selected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column_Selected.FalseValue = "";
            this.Column_Selected.HeaderText = "";
            this.Column_Selected.Name = "Column_Selected";
            this.Column_Selected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column_Selected.ToolTipText = "Select Commands";
            this.Column_Selected.TrueValue = "";
            this.Column_Selected.Width = 21;
            // 
            // Column_CommandCodeId
            // 
            this.Column_CommandCodeId.HeaderText = "CommandCodeId";
            this.Column_CommandCodeId.Name = "Column_CommandCodeId";
            this.Column_CommandCodeId.ReadOnly = true;
            this.Column_CommandCodeId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_CommandCodeId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_Name
            // 
            this.Column_Name.HeaderText = "Name";
            this.Column_Name.Name = "Column_Name";
            this.Column_Name.ReadOnly = true;
            this.Column_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_Command
            // 
            this.Column_Command.HeaderText = "Command";
            this.Column_Command.Name = "Column_Command";
            this.Column_Command.ReadOnly = true;
            this.Column_Command.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_Command.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_Value
            // 
            this.Column_Value.HeaderText = "Value";
            this.Column_Value.Name = "Column_Value";
            this.Column_Value.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // RSMPGS2_CommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(544, 401);
            this.Controls.Add(this.button_Commands_Cancel);
            this.Controls.Add(this.button_Commands_Send);
            this.Controls.Add(this.groupBox_Commands);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RSMPGS2_CommandForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RSMPGS2 - Send Commands";
            this.groupBox_Commands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Commands)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Commands;
        private System.Windows.Forms.Button button_Commands_Send;
        private System.Windows.Forms.Button button_Commands_Cancel;
        private System.Windows.Forms.DataGridView dataGridView_Commands;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_CommandCodeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Command;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_Value;
    }
}