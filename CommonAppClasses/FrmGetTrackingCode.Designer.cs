namespace CommonAppClasses
{
   partial class FrmGetTrackingCode
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
          this.dataGridViewTrackingCodes = new System.Windows.Forms.DataGridView();
          this.label1 = new System.Windows.Forms.Label();
          this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingCodes)).BeginInit();
          this.SuspendLayout();
          // 
          // buttonCancel
          // 
          this.buttonCancel.Location = new System.Drawing.Point(320, 11);
          this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
          this.buttonCancel.Name = "buttonCancel";
          this.buttonCancel.Size = new System.Drawing.Size(56, 19);
          this.buttonCancel.TabIndex = 2;
          this.buttonCancel.Text = "Cancel";
          this.buttonCancel.UseVisualStyleBackColor = true;
          this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
          // 
          // dataGridViewTrackingCodes
          // 
          this.dataGridViewTrackingCodes.AllowUserToAddRows = false;
          this.dataGridViewTrackingCodes.AllowUserToDeleteRows = false;
          this.dataGridViewTrackingCodes.AllowUserToOrderColumns = true;
          this.dataGridViewTrackingCodes.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
          dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dataGridViewTrackingCodes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.dataGridViewTrackingCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.dataGridViewTrackingCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCode,
            this.ColumnDescrip});
          this.dataGridViewTrackingCodes.EnableHeadersVisualStyles = false;
          this.dataGridViewTrackingCodes.Location = new System.Drawing.Point(11, 34);
          this.dataGridViewTrackingCodes.Margin = new System.Windows.Forms.Padding(2);
          this.dataGridViewTrackingCodes.Name = "dataGridViewTrackingCodes";
          this.dataGridViewTrackingCodes.RowHeadersVisible = false;
          this.dataGridViewTrackingCodes.RowTemplate.Height = 24;
          this.dataGridViewTrackingCodes.Size = new System.Drawing.Size(386, 255);
          this.dataGridViewTrackingCodes.TabIndex = 1;
          this.dataGridViewTrackingCodes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTrackingCodes_CellDoubleClick);
          this.dataGridViewTrackingCodes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTrackingCodes_CellClick);
          this.dataGridViewTrackingCodes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewTrackingCodes_KeyDown);
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.Location = new System.Drawing.Point(51, 17);
          this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(265, 13);
          this.label1.TabIndex = 3;
          this.label1.Text = "Select Code And Double-click or Press Enter.";
          // 
          // ColumnCode
          // 
          this.ColumnCode.DataPropertyName = "code";
          this.ColumnCode.HeaderText = "Code";
          this.ColumnCode.Name = "ColumnCode";
          this.ColumnCode.ReadOnly = true;
          this.ColumnCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
          // FrmGetTrackingCode
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
          this.ClientSize = new System.Drawing.Size(412, 326);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.dataGridViewTrackingCodes);
          this.Controls.Add(this.buttonCancel);
          this.Name = "FrmGetTrackingCode";
          this.Text = "Select Tracking Code";
          this.Load += new System.EventHandler(this.FrmGetTrackingCode_Load);
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingCodes)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.DataGridView dataGridViewTrackingCodes;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
   }
}