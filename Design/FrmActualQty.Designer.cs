namespace Design
{
  partial class FrmActualQty
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
        this.buttonReturn = new WSGUtilitieslib.Telemetry.Button();
        this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
        this.listBoxFeatures = new System.Windows.Forms.ListBox();
        this.dataGridViewFeatureSelector = new System.Windows.Forms.DataGridView();
        this.ColumnItmDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dataGridViewSolines = new System.Windows.Forms.DataGridView();
        this.ColumnShortdescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnItemqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnItemprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeatureSelector)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSolines)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonReturn
        // 
        this.buttonReturn.Location = new System.Drawing.Point(692, 35);
        this.buttonReturn.Name = "buttonReturn";
        this.buttonReturn.Size = new System.Drawing.Size(85, 32);
        this.buttonReturn.TabIndex = 0;
        this.buttonReturn.Text = "Return";
        this.buttonReturn.UseVisualStyleBackColor = true;
        // 
        // buttonSave
        // 
        this.buttonSave.Location = new System.Drawing.Point(592, 35);
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Size = new System.Drawing.Size(85, 32);
        this.buttonSave.TabIndex = 1;
        this.buttonSave.Text = "Save";
        this.buttonSave.UseVisualStyleBackColor = true;
        // 
        // listBoxFeatures
        // 
        this.listBoxFeatures.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.listBoxFeatures.FormattingEnabled = true;
        this.listBoxFeatures.ItemHeight = 16;
        this.listBoxFeatures.Location = new System.Drawing.Point(68, 83);
        this.listBoxFeatures.Margin = new System.Windows.Forms.Padding(2);
        this.listBoxFeatures.Name = "listBoxFeatures";
        this.listBoxFeatures.Size = new System.Drawing.Size(112, 116);
        this.listBoxFeatures.TabIndex = 391;
        // 
        // dataGridViewFeatureSelector
        // 
        this.dataGridViewFeatureSelector.AllowUserToAddRows = false;
        this.dataGridViewFeatureSelector.AllowUserToDeleteRows = false;
        this.dataGridViewFeatureSelector.BackgroundColor = System.Drawing.Color.LightSteelBlue;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewFeatureSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewFeatureSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewFeatureSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnItmDesc});
        this.dataGridViewFeatureSelector.EnableHeadersVisualStyles = false;
        this.dataGridViewFeatureSelector.GridColor = System.Drawing.Color.Wheat;
        this.dataGridViewFeatureSelector.Location = new System.Drawing.Point(196, 83);
        this.dataGridViewFeatureSelector.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewFeatureSelector.Name = "dataGridViewFeatureSelector";
        this.dataGridViewFeatureSelector.ReadOnly = true;
        this.dataGridViewFeatureSelector.RowHeadersVisible = false;
        this.dataGridViewFeatureSelector.RowTemplate.Height = 24;
        this.dataGridViewFeatureSelector.Size = new System.Drawing.Size(253, 224);
        this.dataGridViewFeatureSelector.TabIndex = 404;
        // 
        // ColumnItmDesc
        // 
        this.ColumnItmDesc.DataPropertyName = "shortdescrip";
        dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue;
        dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Red;
        this.ColumnItmDesc.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnItmDesc.HeaderText = "Item";
        this.ColumnItmDesc.Name = "ColumnItmDesc";
        this.ColumnItmDesc.ReadOnly = true;
        this.ColumnItmDesc.ToolTipText = "Double Click Item to add to Order";
        this.ColumnItmDesc.Width = 230;
        // 
        // dataGridViewSolines
        // 
        this.dataGridViewSolines.AllowUserToAddRows = false;
        this.dataGridViewSolines.AllowUserToDeleteRows = false;
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewSolines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
        this.dataGridViewSolines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewSolines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnShortdescrip,
            this.ColumnItemqty,
            this.ColumnItemprice});
        this.dataGridViewSolines.EnableHeadersVisualStyles = false;
        this.dataGridViewSolines.Location = new System.Drawing.Point(473, 83);
        this.dataGridViewSolines.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewSolines.Name = "dataGridViewSolines";
        this.dataGridViewSolines.RowHeadersVisible = false;
        this.dataGridViewSolines.RowTemplate.Height = 24;
        this.dataGridViewSolines.Size = new System.Drawing.Size(357, 224);
        this.dataGridViewSolines.TabIndex = 405;
        // 
        // ColumnShortdescrip
        // 
        this.ColumnShortdescrip.DataPropertyName = "descrip";
        this.ColumnShortdescrip.HeaderText = "Item Description";
        this.ColumnShortdescrip.Name = "ColumnShortdescrip";
        this.ColumnShortdescrip.ReadOnly = true;
        this.ColumnShortdescrip.Width = 200;
        // 
        // ColumnItemqty
        // 
        this.ColumnItemqty.DataPropertyName = "qtyord";
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle4.Format = "N0";
        dataGridViewCellStyle4.NullValue = null;
        this.ColumnItemqty.DefaultCellStyle = dataGridViewCellStyle4;
        this.ColumnItemqty.HeaderText = "Qty Ord";
        this.ColumnItemqty.Name = "ColumnItemqty";
        this.ColumnItemqty.ReadOnly = true;
        this.ColumnItemqty.Width = 50;
        // 
        // ColumnItemprice
        // 
        this.ColumnItemprice.DataPropertyName = "qtyact";
        dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle5.Format = "N0";
        dataGridViewCellStyle5.NullValue = null;
        this.ColumnItemprice.DefaultCellStyle = dataGridViewCellStyle5;
        this.ColumnItemprice.HeaderText = "Qty Act";
        this.ColumnItemprice.Name = "ColumnItemprice";
        this.ColumnItemprice.Width = 50;
        // 
        // buttonClear
        // 
        this.buttonClear.Location = new System.Drawing.Point(487, 35);
        this.buttonClear.Name = "buttonClear";
        this.buttonClear.Size = new System.Drawing.Size(85, 32);
        this.buttonClear.TabIndex = 406;
        this.buttonClear.Text = "Clear";
        this.buttonClear.UseVisualStyleBackColor = true;
        // 
        // FrmActualQty
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(867, 345);
        this.Controls.Add(this.buttonClear);
        this.Controls.Add(this.dataGridViewSolines);
        this.Controls.Add(this.dataGridViewFeatureSelector);
        this.Controls.Add(this.listBoxFeatures);
        this.Controls.Add(this.buttonSave);
        this.Controls.Add(this.buttonReturn);
        this.Name = "FrmActualQty";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Enter Actual Quantities";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeatureSelector)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSolines)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItmDesc;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnShortdescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemqty;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemprice;
    public System.Windows.Forms.Button buttonReturn;
    public System.Windows.Forms.Button buttonSave;
    public System.Windows.Forms.ListBox listBoxFeatures;
    public System.Windows.Forms.DataGridView dataGridViewFeatureSelector;
    public System.Windows.Forms.DataGridView dataGridViewSolines;
    public System.Windows.Forms.Button buttonClear;
  }
}