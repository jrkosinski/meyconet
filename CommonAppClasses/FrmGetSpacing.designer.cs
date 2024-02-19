namespace CommonAppClasses
{
  partial class FrmGetSpacing
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetSpacing));
        this.dataGridViewSpacing = new System.Windows.Forms.DataGridView();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpacing)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridViewSpacing
        // 
        this.dataGridViewSpacing.AllowUserToAddRows = false;
        this.dataGridViewSpacing.AllowUserToDeleteRows = false;
        this.dataGridViewSpacing.AllowUserToOrderColumns = true;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewSpacing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewSpacing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewSpacing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip});
        this.dataGridViewSpacing.EnableHeadersVisualStyles = false;
        this.dataGridViewSpacing.Location = new System.Drawing.Point(11, 50);
        this.dataGridViewSpacing.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.dataGridViewSpacing.Name = "dataGridViewSpacing";
        this.dataGridViewSpacing.ReadOnly = true;
        this.dataGridViewSpacing.RowHeadersVisible = false;
        this.dataGridViewSpacing.RowTemplate.Height = 24;
        this.dataGridViewSpacing.Size = new System.Drawing.Size(142, 130);
        this.dataGridViewSpacing.TabIndex = 0;
        this.dataGridViewSpacing.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSpacing_CellContentDoubleClick);
        this.dataGridViewSpacing.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewSpacing_KeyDown);
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "descrip";
        this.ColumnDescrip.HeaderText = "Spacing";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.ReadOnly = true;
        this.ColumnDescrip.Width = 125;
        // 
        // buttonClose
        // 
        this.buttonClose.Location = new System.Drawing.Point(11, 11);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 26);
        this.buttonClose.TabIndex = 1;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // FrmGetSpacing
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(156, 235);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.dataGridViewSpacing);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.Name = "FrmGetSpacing";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Spacing Selector";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpacing)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewSpacing;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.Button buttonClose;
  }
}