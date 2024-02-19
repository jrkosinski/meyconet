namespace MiscellaneousSystemMaintenance
{
   partial class FrmMaintainRoute
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
        this.buttonSelectRoute = new WSGUtilitieslib.Telemetry.Button();
        this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
        this.dataGridViewRouteData = new System.Windows.Forms.DataGridView();
        this.labelCurrentRoute = new System.Windows.Forms.Label();
        this.labelRouteCaption = new System.Windows.Forms.Label();
        this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnStepId = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnCuststep = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRouteData)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonCancel
        // 
        this.buttonCancel.Location = new System.Drawing.Point(216, 7);
        this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(56, 19);
        this.buttonCancel.TabIndex = 1;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // buttonSelectRoute
        // 
        this.buttonSelectRoute.Location = new System.Drawing.Point(11, 6);
        this.buttonSelectRoute.Margin = new System.Windows.Forms.Padding(2);
        this.buttonSelectRoute.Name = "buttonSelectRoute";
        this.buttonSelectRoute.Size = new System.Drawing.Size(90, 20);
        this.buttonSelectRoute.TabIndex = 26;
        this.buttonSelectRoute.Text = "Select Route";
        this.buttonSelectRoute.UseVisualStyleBackColor = true;
        this.buttonSelectRoute.Click += new System.EventHandler(this.buttonSelectPhase_Click);
        // 
        // buttonInsert
        // 
        this.buttonInsert.Location = new System.Drawing.Point(117, 7);
        this.buttonInsert.Margin = new System.Windows.Forms.Padding(2);
        this.buttonInsert.Name = "buttonInsert";
        this.buttonInsert.Size = new System.Drawing.Size(84, 19);
        this.buttonInsert.TabIndex = 28;
        this.buttonInsert.Text = "Insert a Step";
        this.buttonInsert.UseVisualStyleBackColor = true;
        this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
        // 
        // dataGridViewRouteData
        // 
        this.dataGridViewRouteData.AllowUserToOrderColumns = true;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewRouteData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewRouteData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewRouteData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCode,
            this.ColumnDescrip,
            this.ColumnStepId,
            this.ColumnCuststep});
        this.dataGridViewRouteData.EnableHeadersVisualStyles = false;
        this.dataGridViewRouteData.Location = new System.Drawing.Point(27, 76);
        this.dataGridViewRouteData.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewRouteData.Name = "dataGridViewRouteData";
        this.dataGridViewRouteData.RowHeadersVisible = false;
        this.dataGridViewRouteData.RowTemplate.Height = 24;
        this.dataGridViewRouteData.Size = new System.Drawing.Size(407, 197);
        this.dataGridViewRouteData.TabIndex = 32;
        this.dataGridViewRouteData.Visible = false;
        this.dataGridViewRouteData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewRouteData_MouseDown);
        this.dataGridViewRouteData.DoubleClick += new System.EventHandler(this.dataGridViewRouteData_DoubleClick);
        this.dataGridViewRouteData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewRouteData_CellFormatting);
        // 
        // labelCurrentRoute
        // 
        this.labelCurrentRoute.AutoSize = true;
        this.labelCurrentRoute.Location = new System.Drawing.Point(11, 52);
        this.labelCurrentRoute.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelCurrentRoute.Name = "labelCurrentRoute";
        this.labelCurrentRoute.Size = new System.Drawing.Size(90, 13);
        this.labelCurrentRoute.TabIndex = 33;
        this.labelCurrentRoute.Text = "To be overwritten";
        this.labelCurrentRoute.Visible = false;
        // 
        // labelRouteCaption
        // 
        this.labelRouteCaption.AutoSize = true;
        this.labelRouteCaption.Location = new System.Drawing.Point(11, 39);
        this.labelRouteCaption.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelRouteCaption.Name = "labelRouteCaption";
        this.labelRouteCaption.Size = new System.Drawing.Size(106, 13);
        this.labelRouteCaption.TabIndex = 34;
        this.labelRouteCaption.Text = "Steps that can follow";
        this.labelRouteCaption.Visible = false;
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
        // ColumnCuststep
        // 
        this.ColumnCuststep.DataPropertyName = "custstep";
        this.ColumnCuststep.HeaderText = "Cust";
        this.ColumnCuststep.Name = "ColumnCuststep";
        this.ColumnCuststep.ReadOnly = true;
        this.ColumnCuststep.Width = 25;
        // 
        // FrmMaintainRoute
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(466, 297);
        this.Controls.Add(this.labelRouteCaption);
        this.Controls.Add(this.labelCurrentRoute);
        this.Controls.Add(this.dataGridViewRouteData);
        this.Controls.Add(this.buttonInsert);
        this.Controls.Add(this.buttonSelectRoute);
        this.Controls.Add(this.buttonCancel);
        this.Name = "FrmMaintainRoute";
        this.Text = "Route Maintenance";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRouteData)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonSelectRoute;
      private System.Windows.Forms.Button buttonInsert;
      private System.Windows.Forms.DataGridView dataGridViewRouteData;
      private System.Windows.Forms.Label labelCurrentRoute;
      private System.Windows.Forms.Label labelRouteCaption;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStepId;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCuststep;
   }
}