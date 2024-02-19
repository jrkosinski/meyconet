namespace CommonAppClasses
{
  partial class FrmGetPrshead
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetPrshead));
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.dataGridViewPrsHeadLocator = new System.Windows.Forms.DataGridView();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrsHeadLocator)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonClose
        // 
        this.buttonClose.Location = new System.Drawing.Point(66, 18);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 25);
        this.buttonClose.TabIndex = 0;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // dataGridViewPrsHeadLocator
        // 
        this.dataGridViewPrsHeadLocator.AllowUserToAddRows = false;
        this.dataGridViewPrsHeadLocator.AllowUserToDeleteRows = false;
        this.dataGridViewPrsHeadLocator.AllowUserToOrderColumns = true;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewPrsHeadLocator.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewPrsHeadLocator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewPrsHeadLocator.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip});
        this.dataGridViewPrsHeadLocator.EnableHeadersVisualStyles = false;
        this.dataGridViewPrsHeadLocator.Location = new System.Drawing.Point(20, 62);
        this.dataGridViewPrsHeadLocator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.dataGridViewPrsHeadLocator.Name = "dataGridViewPrsHeadLocator";
        this.dataGridViewPrsHeadLocator.ReadOnly = true;
        this.dataGridViewPrsHeadLocator.RowHeadersVisible = false;
        this.dataGridViewPrsHeadLocator.RowTemplate.Height = 24;
        this.dataGridViewPrsHeadLocator.Size = new System.Drawing.Size(133, 122);
        this.dataGridViewPrsHeadLocator.TabIndex = 1;
        this.dataGridViewPrsHeadLocator.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPrsHeadLocator_CellContentDoubleClick);
        this.dataGridViewPrsHeadLocator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPrsHeadLocator_KeyDown);
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "descrip";
        this.ColumnDescrip.HeaderText = "Schedule";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.ReadOnly = true;
        this.ColumnDescrip.Width = 125;
        // 
        // FrmGetPrshead
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(164, 249);
        this.Controls.Add(this.dataGridViewPrsHeadLocator);
        this.Controls.Add(this.buttonClose);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.Name = "FrmGetPrshead";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Schedules";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrsHeadLocator)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.DataGridView dataGridViewPrsHeadLocator;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
  }
}