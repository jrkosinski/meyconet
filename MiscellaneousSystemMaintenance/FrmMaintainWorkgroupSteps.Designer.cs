namespace MiscellaneousSystemMaintenance
{
   partial class FrmMaintainWorkgroupSteps
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
        this.dataGridViewWorkgroupSteps = new System.Windows.Forms.DataGridView();
        this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnStepId = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
        this.labelDelete = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkgroupSteps)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonCancel
        // 
        this.buttonCancel.Location = new System.Drawing.Point(246, 5);
        this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(57, 25);
        this.buttonCancel.TabIndex = 1;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // dataGridViewWorkgroupSteps
        // 
        this.dataGridViewWorkgroupSteps.AllowUserToOrderColumns = true;
        this.dataGridViewWorkgroupSteps.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewWorkgroupSteps.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewWorkgroupSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewWorkgroupSteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCode,
            this.ColumnDescrip,
            this.ColumnStepId});
        this.dataGridViewWorkgroupSteps.EnableHeadersVisualStyles = false;
        this.dataGridViewWorkgroupSteps.Location = new System.Drawing.Point(11, 37);
        this.dataGridViewWorkgroupSteps.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewWorkgroupSteps.Name = "dataGridViewWorkgroupSteps";
        this.dataGridViewWorkgroupSteps.RowHeadersVisible = false;
        this.dataGridViewWorkgroupSteps.RowTemplate.Height = 24;
        this.dataGridViewWorkgroupSteps.Size = new System.Drawing.Size(377, 259);
        this.dataGridViewWorkgroupSteps.TabIndex = 33;
        this.dataGridViewWorkgroupSteps.DoubleClick += new System.EventHandler(this.dataGridViewWorkgroupSteps_DoubleClick);
        this.dataGridViewWorkgroupSteps.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWorkgroupSteps_CellContentClick);
        // 
        // ColumnCode
        // 
        this.ColumnCode.DataPropertyName = "code";
        this.ColumnCode.HeaderText = "Code";
        this.ColumnCode.Name = "ColumnCode";
        this.ColumnCode.ReadOnly = true;
        this.ColumnCode.Width = 75;
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "descrip";
        this.ColumnDescrip.HeaderText = "Description";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.ReadOnly = true;
        this.ColumnDescrip.Width = 300;
        // 
        // ColumnStepId
        // 
        this.ColumnStepId.HeaderText = "Step ID";
        this.ColumnStepId.Name = "ColumnStepId";
        this.ColumnStepId.ReadOnly = true;
        this.ColumnStepId.Visible = false;
        // 
        // buttonInsert
        // 
        this.buttonInsert.Location = new System.Drawing.Point(186, 5);
        this.buttonInsert.Margin = new System.Windows.Forms.Padding(2);
        this.buttonInsert.Name = "buttonInsert";
        this.buttonInsert.Size = new System.Drawing.Size(57, 25);
        this.buttonInsert.TabIndex = 34;
        this.buttonInsert.Text = "Insert";
        this.buttonInsert.UseVisualStyleBackColor = true;
        this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
        // 
        // labelDelete
        // 
        this.labelDelete.AutoSize = true;
        this.labelDelete.Location = new System.Drawing.Point(39, 16);
        this.labelDelete.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelDelete.Name = "labelDelete";
        this.labelDelete.Size = new System.Drawing.Size(143, 13);
        this.labelDelete.TabIndex = 35;
        this.labelDelete.Text = "Double Click a step to delete";
        // 
        // FrmMaintainWorkgroupSteps
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
        this.ClientSize = new System.Drawing.Size(402, 319);
        this.Controls.Add(this.labelDelete);
        this.Controls.Add(this.buttonInsert);
        this.Controls.Add(this.dataGridViewWorkgroupSteps);
        this.Controls.Add(this.buttonCancel);
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmMaintainWorkgroupSteps";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "FrmMaintainWorkgroupSteps";
        this.Shown += new System.EventHandler(this.FrmMaintainWorkgroupSteps_Shown);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkgroupSteps)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.DataGridView dataGridViewWorkgroupSteps;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStepId;
      private System.Windows.Forms.Button buttonInsert;
      private System.Windows.Forms.Label labelDelete;
   }
}