namespace IncidentProcessing
{
   partial class FrmIncidentSelector
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIncidentSelector));
         this.buttonNewincident = new WSGUtilitieslib.Telemetry.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.dataGridViewIncidentSelector = new System.Windows.Forms.DataGridView();
         this.ColumnIsse = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.ColumnIncidenDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIncidentSelector)).BeginInit();
         this.SuspendLayout();
         // 
         // buttonNewincident
         // 
         this.buttonNewincident.Location = new System.Drawing.Point(139, 12);
         this.buttonNewincident.Name = "buttonNewincident";
         this.buttonNewincident.Size = new System.Drawing.Size(152, 23);
         this.buttonNewincident.TabIndex = 0;
         this.buttonNewincident.Text = "New Incident";
         this.buttonNewincident.UseVisualStyleBackColor = true;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.Location = new System.Drawing.Point(26, 64);
         this.label1.MinimumSize = new System.Drawing.Size(0, 25);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(383, 25);
         this.label1.TabIndex = 1;
         this.label1.Text = "Select an Existing Incident, Record a New Incident or Cancel";
         // 
         // dataGridViewIncidentSelector
         // 
         dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
         dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.WindowText;
         dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
         dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
         this.dataGridViewIncidentSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
         this.dataGridViewIncidentSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridViewIncidentSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIsse,
            this.ColumnIncidenDate});
         this.dataGridViewIncidentSelector.EnableHeadersVisualStyles = false;
         this.dataGridViewIncidentSelector.Location = new System.Drawing.Point(48, 113);
         this.dataGridViewIncidentSelector.Name = "dataGridViewIncidentSelector";
         this.dataGridViewIncidentSelector.ReadOnly = true;
         this.dataGridViewIncidentSelector.RowHeadersVisible = false;
         this.dataGridViewIncidentSelector.Size = new System.Drawing.Size(306, 150);
         this.dataGridViewIncidentSelector.TabIndex = 2;
         // 
         // ColumnIsse
         // 
         this.ColumnIsse.DataPropertyName = "issue";
         this.ColumnIsse.HeaderText = "Incident";
         this.ColumnIsse.Name = "ColumnIsse";
         this.ColumnIsse.ReadOnly = true;
         this.ColumnIsse.Width = 200;
         // 
         // ColumnIncidenDate
         // 
         this.ColumnIncidenDate.DataPropertyName = "incidentdate";
         this.ColumnIncidenDate.HeaderText = "Incident Date";
         this.ColumnIncidenDate.Name = "ColumnIncidenDate";
         this.ColumnIncidenDate.ReadOnly = true;
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(311, 12);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(75, 23);
         this.buttonCancel.TabIndex = 3;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.UseVisualStyleBackColor = true;
         // 
         // FrmIncidentSelector
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(442, 319);
         this.Controls.Add(this.buttonCancel);
         this.Controls.Add(this.dataGridViewIncidentSelector);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.buttonNewincident);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "FrmIncidentSelector";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Incident Selector";
         ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIncidentSelector)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIsse;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIncidenDate;
      public System.Windows.Forms.Button buttonNewincident;
      public System.Windows.Forms.Label label1;
      public System.Windows.Forms.DataGridView dataGridViewIncidentSelector;
      public System.Windows.Forms.Button buttonCancel;
   }
}