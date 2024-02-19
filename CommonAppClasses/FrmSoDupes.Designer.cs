namespace CommonAppClasses
{
  partial class FrmSoDupes
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSoDupes));
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      this.dataGridViewDupes = new System.Windows.Forms.DataGridView();
      this.label1 = new System.Windows.Forms.Label();
      this.Columnsono = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnSoDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnPonum = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDupes)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(638, 31);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(75, 23);
      this.buttonClose.TabIndex = 0;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // dataGridViewDupes
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewDupes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridViewDupes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewDupes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columnsono,
            this.ColumnSoDate,
            this.ColumnPonum,
            this.ColumnAddress,
            this.ColumnCity,
            this.ColumnState});
      this.dataGridViewDupes.EnableHeadersVisualStyles = false;
      this.dataGridViewDupes.Location = new System.Drawing.Point(36, 73);
      this.dataGridViewDupes.Name = "dataGridViewDupes";
      this.dataGridViewDupes.RowHeadersVisible = false;
      this.dataGridViewDupes.Size = new System.Drawing.Size(782, 150);
      this.dataGridViewDupes.TabIndex = 1;
      this.dataGridViewDupes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDupes_CellContentDoubleClick);
      this.dataGridViewDupes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewDupes_KeyDown);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(48, 41);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(262, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Double Click SO or Press Enter To View PDF";
      // 
      // Columnsono
      // 
      this.Columnsono.DataPropertyName = "sono";
      this.Columnsono.HeaderText = "SO Number";
      this.Columnsono.Name = "Columnsono";
      // 
      // ColumnSoDate
      // 
      this.ColumnSoDate.DataPropertyName = "sodate";
      this.ColumnSoDate.HeaderText = "SO Date";
      this.ColumnSoDate.Name = "ColumnSoDate";
      // 
      // ColumnPonum
      // 
      this.ColumnPonum.DataPropertyName = "ponum";
      this.ColumnPonum.HeaderText = "PO Number";
      this.ColumnPonum.Name = "ColumnPonum";
      this.ColumnPonum.Width = 200;
      // 
      // ColumnAddress
      // 
      this.ColumnAddress.DataPropertyName = "oldplan";
      this.ColumnAddress.HeaderText = "Old Plan";
      this.ColumnAddress.Name = "ColumnAddress";
      this.ColumnAddress.Width = 125;
      // 
      // ColumnCity
      // 
      this.ColumnCity.DataPropertyName = "city";
      this.ColumnCity.HeaderText = "City";
      this.ColumnCity.Name = "ColumnCity";
      this.ColumnCity.Width = 150;
      // 
      // ColumnState
      // 
      this.ColumnState.DataPropertyName = "state";
      this.ColumnState.HeaderText = "State";
      this.ColumnState.Name = "ColumnState";
      // 
      // FrmSoDupes
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(848, 273);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dataGridViewDupes);
      this.Controls.Add(this.buttonClose);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FrmSoDupes";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Possible Duplicate Orders";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDupes)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.Button buttonClose;
    public System.Windows.Forms.DataGridView dataGridViewDupes;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Columnsono;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSoDate;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPonum;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddress;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCity;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnState;
  }
}