namespace CommonAppClasses
{
  partial class FrmCoverSelector
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
        this.dataGridViewCoverSelector = new System.Windows.Forms.DataGridView();
        this.ColumnProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnCover = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.labelDataGridCount = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCoverSelector)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonCancel
        // 
        this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonCancel.Location = new System.Drawing.Point(738, 2);
        this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(70, 35);
        this.buttonCancel.TabIndex = 0;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = false;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // dataGridViewCoverSelector
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewCoverSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewCoverSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewCoverSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnProduct,
            this.ColumnCover,
            this.ColumnMaterial,
            this.ColumnColor,
            this.ColumnDescrip});
        this.dataGridViewCoverSelector.EnableHeadersVisualStyles = false;
        this.dataGridViewCoverSelector.Location = new System.Drawing.Point(11, 50);
        this.dataGridViewCoverSelector.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewCoverSelector.Name = "dataGridViewCoverSelector";
        this.dataGridViewCoverSelector.RowHeadersVisible = false;
        this.dataGridViewCoverSelector.RowTemplate.Height = 24;
        this.dataGridViewCoverSelector.Size = new System.Drawing.Size(797, 212);
        this.dataGridViewCoverSelector.TabIndex = 1;
        this.dataGridViewCoverSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCoverSelector_CellContentDoubleClick);
        this.dataGridViewCoverSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewCoverSelector_KeyDown);
        // 
        // ColumnProduct
        // 
        this.ColumnProduct.DataPropertyName = "product";
        this.ColumnProduct.HeaderText = "Product";
        this.ColumnProduct.Name = "ColumnProduct";
        this.ColumnProduct.Width = 175;
        // 
        // ColumnCover
        // 
        this.ColumnCover.DataPropertyName = "cover";
        this.ColumnCover.HeaderText = "Cover";
        this.ColumnCover.Name = "ColumnCover";
        this.ColumnCover.Width = 75;
        // 
        // ColumnMaterial
        // 
        this.ColumnMaterial.DataPropertyName = "material";
        this.ColumnMaterial.HeaderText = "Material";
        this.ColumnMaterial.Name = "ColumnMaterial";
        this.ColumnMaterial.Width = 175;
        // 
        // ColumnColor
        // 
        this.ColumnColor.DataPropertyName = "color";
        this.ColumnColor.HeaderText = "Color";
        this.ColumnColor.Name = "ColumnColor";
        this.ColumnColor.Width = 175;
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "descrip";
        this.ColumnDescrip.HeaderText = "Cover Type";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.Width = 175;
        //
        // labelDataGridCount
        //
        this.labelDataGridCount.AutoSize = true;
        this.labelDataGridCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelDataGridCount.Location = new System.Drawing.Point(13, 265);
        this.labelDataGridCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelDataGridCount.Name = "labelDataGridCount";
        this.labelDataGridCount.Size = new System.Drawing.Size(43, 13);
        this.labelDataGridCount.TabIndex = 3;
        this.labelDataGridCount.Text = "Returned {n} items";

        // 
        // FrmCoverSelector
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(835, 300);
        this.Controls.Add(this.dataGridViewCoverSelector);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.labelDataGridCount);
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmCoverSelector";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Select a Cover";
        this.Load += new System.EventHandler(this.FrmCoverSelector_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCoverSelector)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProduct;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCover;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaterial;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColor;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    public System.Windows.Forms.DataGridView dataGridViewCoverSelector;
    public System.Windows.Forms.Label labelDataGridCount;
  }
}