namespace MiscellaneousSystemMaintenance
{
   partial class FrmMaintainWorkgroup
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
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
          this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
          this.dataGridViewWorkgroups = new System.Windows.Forms.DataGridView();
          this.ColumnGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.labelGroupName = new System.Windows.Forms.Label();
          this.buttonSteps = new WSGUtilitieslib.Telemetry.Button();
          this.textBoxGroupName = new System.Windows.Forms.TextBox();
          this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
          this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
          this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
          this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkgroups)).BeginInit();
          this.SuspendLayout();
          // 
          // buttonCancel
          // 
          this.buttonCancel.Location = new System.Drawing.Point(332, 13);
          this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.buttonCancel.Name = "buttonCancel";
          this.buttonCancel.Size = new System.Drawing.Size(56, 19);
          this.buttonCancel.TabIndex = 0;
          this.buttonCancel.Text = "Cancel";
          this.buttonCancel.UseVisualStyleBackColor = true;
          this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
          // 
          // dataGridViewWorkgroups
          // 
          dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dataGridViewWorkgroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.dataGridViewWorkgroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.dataGridViewWorkgroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnGroupName});
          this.dataGridViewWorkgroups.EnableHeadersVisualStyles = false;
          this.dataGridViewWorkgroups.Location = new System.Drawing.Point(9, 64);
          this.dataGridViewWorkgroups.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.dataGridViewWorkgroups.MultiSelect = false;
          this.dataGridViewWorkgroups.Name = "dataGridViewWorkgroups";
          this.dataGridViewWorkgroups.ReadOnly = true;
          this.dataGridViewWorkgroups.RowHeadersVisible = false;
          this.dataGridViewWorkgroups.RowTemplate.Height = 24;
          this.dataGridViewWorkgroups.Size = new System.Drawing.Size(407, 177);
          this.dataGridViewWorkgroups.TabIndex = 1;
          this.dataGridViewWorkgroups.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWorkgroups_CellContentDoubleClick);
          this.dataGridViewWorkgroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewWorkgroups_KeyDown);
          // 
          // ColumnGroupName
          // 
          this.ColumnGroupName.DataPropertyName = "groupname";
          this.ColumnGroupName.HeaderText = "Group Name";
          this.ColumnGroupName.Name = "ColumnGroupName";
          this.ColumnGroupName.ReadOnly = true;
          this.ColumnGroupName.Width = 400;
          // 
          // labelGroupName
          // 
          this.labelGroupName.AutoSize = true;
          this.labelGroupName.Location = new System.Drawing.Point(11, 43);
          this.labelGroupName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
          this.labelGroupName.Name = "labelGroupName";
          this.labelGroupName.Size = new System.Drawing.Size(67, 13);
          this.labelGroupName.TabIndex = 2;
          this.labelGroupName.Text = "Group Name";
          // 
          // buttonSteps
          // 
          this.buttonSteps.Location = new System.Drawing.Point(9, 13);
          this.buttonSteps.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.buttonSteps.Name = "buttonSteps";
          this.buttonSteps.Size = new System.Drawing.Size(60, 19);
          this.buttonSteps.TabIndex = 3;
          this.buttonSteps.Text = "Steps";
          this.buttonSteps.UseVisualStyleBackColor = true;
          this.buttonSteps.Click += new System.EventHandler(this.buttonSteps_Click);
          // 
          // textBoxGroupName
          // 
          this.textBoxGroupName.Location = new System.Drawing.Point(85, 40);
          this.textBoxGroupName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.textBoxGroupName.Name = "textBoxGroupName";
          this.textBoxGroupName.Size = new System.Drawing.Size(175, 20);
          this.textBoxGroupName.TabIndex = 4;
          // 
          // buttonSave
          // 
          this.buttonSave.Enabled = false;
          this.buttonSave.Location = new System.Drawing.Point(268, 13);
          this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.buttonSave.Name = "buttonSave";
          this.buttonSave.Size = new System.Drawing.Size(56, 19);
          this.buttonSave.TabIndex = 5;
          this.buttonSave.Text = "Save";
          this.buttonSave.UseVisualStyleBackColor = true;
          this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
          // 
          // buttonDelete
          // 
          this.buttonDelete.Enabled = false;
          this.buttonDelete.Location = new System.Drawing.Point(140, 13);
          this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.buttonDelete.Name = "buttonDelete";
          this.buttonDelete.Size = new System.Drawing.Size(56, 19);
          this.buttonDelete.TabIndex = 6;
          this.buttonDelete.Text = "Delete";
          this.buttonDelete.UseVisualStyleBackColor = true;
          this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
          // 
          // buttonInsert
          // 
          this.buttonInsert.Location = new System.Drawing.Point(76, 13);
          this.buttonInsert.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.buttonInsert.Name = "buttonInsert";
          this.buttonInsert.Size = new System.Drawing.Size(56, 19);
          this.buttonInsert.TabIndex = 7;
          this.buttonInsert.Text = "Insert";
          this.buttonInsert.UseVisualStyleBackColor = true;
          this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
          // 
          // buttonEdit
          // 
          this.buttonEdit.Enabled = false;
          this.buttonEdit.Location = new System.Drawing.Point(204, 13);
          this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.buttonEdit.Name = "buttonEdit";
          this.buttonEdit.Size = new System.Drawing.Size(56, 19);
          this.buttonEdit.TabIndex = 8;
          this.buttonEdit.Text = "Edit";
          this.buttonEdit.UseVisualStyleBackColor = true;
          this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
          // 
          // FrmMaintainWorkgroup
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
          this.ClientSize = new System.Drawing.Size(435, 294);
          this.Controls.Add(this.buttonEdit);
          this.Controls.Add(this.buttonInsert);
          this.Controls.Add(this.buttonDelete);
          this.Controls.Add(this.buttonSave);
          this.Controls.Add(this.textBoxGroupName);
          this.Controls.Add(this.buttonSteps);
          this.Controls.Add(this.labelGroupName);
          this.Controls.Add(this.dataGridViewWorkgroups);
          this.Controls.Add(this.buttonCancel);
          this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
          this.Name = "FrmMaintainWorkgroup";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Workgroup Maintenance";
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkgroups)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.DataGridView dataGridViewWorkgroups;
      private System.Windows.Forms.Label labelGroupName;
      private System.Windows.Forms.Button buttonSteps;
      private System.Windows.Forms.TextBox textBoxGroupName;
      private System.Windows.Forms.Button buttonSave;
      private System.Windows.Forms.Button buttonDelete;
      private System.Windows.Forms.Button buttonInsert;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGroupName;
      private System.Windows.Forms.Button buttonEdit;
   }
}