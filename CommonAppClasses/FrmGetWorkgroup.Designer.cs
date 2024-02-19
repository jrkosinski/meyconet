namespace CommonAppClasses
{
   partial class FrmGetWorkgroup
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetWorkgroup));
        this.dataGridViewWorkgroups = new System.Windows.Forms.DataGridView();
        this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonButtonCancel = new WSGUtilitieslib.Telemetry.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkgroups)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridViewWorkgroups
        // 
        this.dataGridViewWorkgroups.AllowUserToAddRows = false;
        this.dataGridViewWorkgroups.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
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
            this.Column1,
            this.Column2});
        this.dataGridViewWorkgroups.EnableHeadersVisualStyles = false;
        this.dataGridViewWorkgroups.Location = new System.Drawing.Point(11, 43);
        this.dataGridViewWorkgroups.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewWorkgroups.Name = "dataGridViewWorkgroups";
        this.dataGridViewWorkgroups.RowHeadersVisible = false;
        this.dataGridViewWorkgroups.RowTemplate.Height = 24;
        this.dataGridViewWorkgroups.Size = new System.Drawing.Size(350, 208);
        this.dataGridViewWorkgroups.TabIndex = 1;
        this.dataGridViewWorkgroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewWorkgroups_KeyDown);
        this.dataGridViewWorkgroups.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWorkgroups_CellContentClick);
        // 
        // Column1
        // 
        this.Column1.DataPropertyName = "groupname";
        this.Column1.HeaderText = "Group Name";
        this.Column1.Name = "Column1";
        this.Column1.Width = 350;
        // 
        // Column2
        // 
        this.Column2.DataPropertyName = "idcol";
        this.Column2.HeaderText = "Column2";
        this.Column2.Name = "Column2";
        this.Column2.Visible = false;
        // 
        // buttonButtonCancel
        // 
        this.buttonButtonCancel.Location = new System.Drawing.Point(316, 20);
        this.buttonButtonCancel.Margin = new System.Windows.Forms.Padding(2);
        this.buttonButtonCancel.Name = "buttonButtonCancel";
        this.buttonButtonCancel.Size = new System.Drawing.Size(56, 19);
        this.buttonButtonCancel.TabIndex = 2;
        this.buttonButtonCancel.Text = "Cancel";
        this.buttonButtonCancel.UseVisualStyleBackColor = true;
        this.buttonButtonCancel.Click += new System.EventHandler(this.buttonButtonCancel_Click);
        // 
        // FrmGetWorkgroup
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
        this.ClientSize = new System.Drawing.Size(385, 285);
        this.Controls.Add(this.buttonButtonCancel);
        this.Controls.Add(this.dataGridViewWorkgroups);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmGetWorkgroup";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Select Workgroup";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkgroups)).EndInit();
        this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView dataGridViewWorkgroups;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
      private System.Windows.Forms.Button buttonButtonCancel;
   }
}