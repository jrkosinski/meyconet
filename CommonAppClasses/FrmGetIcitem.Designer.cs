namespace CommonAppClasses
{
  partial class FrmGetIcitem
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetIcitem));
        this.dataGridViewGetIcitem = new System.Windows.Forms.DataGridView();
        this.ColumnItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnPrcdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnItemdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonClose = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGetIcitem)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridViewGetIcitem
        // 
        this.dataGridViewGetIcitem.AllowUserToAddRows = false;
        this.dataGridViewGetIcitem.AllowUserToDeleteRows = false;
        this.dataGridViewGetIcitem.AllowUserToOrderColumns = true;
        this.dataGridViewGetIcitem.BackgroundColor = System.Drawing.Color.LightSteelBlue;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewGetIcitem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewGetIcitem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewGetIcitem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnItem,
            this.ColumnPrcdesc,
            this.ColumnItemdesc,
            this.ColumnColor,
            this.ColumnMaterial,
            this.ColumnPrice});
        this.dataGridViewGetIcitem.EnableHeadersVisualStyles = false;
        this.dataGridViewGetIcitem.Location = new System.Drawing.Point(25, 41);
        this.dataGridViewGetIcitem.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewGetIcitem.Name = "dataGridViewGetIcitem";
        this.dataGridViewGetIcitem.ReadOnly = true;
        this.dataGridViewGetIcitem.RowHeadersVisible = false;
        this.dataGridViewGetIcitem.RowTemplate.Height = 24;
        this.dataGridViewGetIcitem.Size = new System.Drawing.Size(677, 197);
        this.dataGridViewGetIcitem.TabIndex = 0;
        this.dataGridViewGetIcitem.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewGetIcitem_CellContentDoubleClick);
        this.dataGridViewGetIcitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewGetIcitem_KeyDown);
        // 
        // ColumnItem
        // 
        this.ColumnItem.DataPropertyName = "Item";
        this.ColumnItem.HeaderText = "Item";
        this.ColumnItem.Name = "ColumnItem";
        this.ColumnItem.ReadOnly = true;
        this.ColumnItem.Width = 125;
        // 
        // ColumnPrcdesc
        // 
        this.ColumnPrcdesc.DataPropertyName = "prcdesc";
        this.ColumnPrcdesc.HeaderText = "Price Description";
        this.ColumnPrcdesc.Name = "ColumnPrcdesc";
        this.ColumnPrcdesc.ReadOnly = true;
        this.ColumnPrcdesc.Width = 200;
        // 
        // ColumnItemdesc
        // 
        this.ColumnItemdesc.DataPropertyName = "itmdesc";
        this.ColumnItemdesc.HeaderText = "Description";
        this.ColumnItemdesc.Name = "ColumnItemdesc";
        this.ColumnItemdesc.ReadOnly = true;
        this.ColumnItemdesc.Width = 275;
        // 
        // ColumnColor
        // 
        this.ColumnColor.DataPropertyName = "color";
        this.ColumnColor.HeaderText = "Color";
        this.ColumnColor.Name = "ColumnColor";
        this.ColumnColor.ReadOnly = true;
        // 
        // ColumnMaterial
        // 
        this.ColumnMaterial.DataPropertyName = "material";
        this.ColumnMaterial.HeaderText = "Material";
        this.ColumnMaterial.Name = "ColumnMaterial";
        this.ColumnMaterial.ReadOnly = true;
        // 
        // ColumnPrice
        // 
        this.ColumnPrice.DataPropertyName = "price";
        dataGridViewCellStyle2.Format = "C2";
        dataGridViewCellStyle2.NullValue = null;
        this.ColumnPrice.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnPrice.HeaderText = "Price";
        this.ColumnPrice.Name = "ColumnPrice";
        this.ColumnPrice.ReadOnly = true;
        // 
        // buttonClose
        // 
        this.buttonClose.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonClose.Location = new System.Drawing.Point(614, 10);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 26);
        this.buttonClose.TabIndex = 1;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = false;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // FrmGetIcitem
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(718, 289);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.dataGridViewGetIcitem);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmGetIcitem";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Item Selector";
        this.Load += new System.EventHandler(this.FrmGetIcitem_Load);
        this.Shown += new System.EventHandler(this.FrmGetIcitem_Shown);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGetIcitem)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewGetIcitem;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItem;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrcdesc;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemdesc;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColor;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaterial;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrice;
  }
}