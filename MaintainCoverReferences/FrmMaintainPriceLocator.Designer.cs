namespace MaintainCoverReferences
{
  partial class FrmMaintainPriceLocator
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaintainPriceLocator));
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.dataGridViewPriceLocator = new System.Windows.Forms.DataGridView();
        this.ColumnItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnSpacing = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnPsdescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnPrcfact = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonItem = new WSGUtilitieslib.Telemetry.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.buttonSpacing = new WSGUtilitieslib.Telemetry.Button();
        this.buttonPrsschd = new WSGUtilitieslib.Telemetry.Button();
        this.textBoxPrcfact = new System.Windows.Forms.TextBox();
        this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
        this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
        this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
        this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
        this.labelItem = new System.Windows.Forms.Label();
        this.labelSpacing = new System.Windows.Forms.Label();
        this.labelPrssched = new System.Windows.Forms.Label();
        this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPriceLocator)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonClose
        // 
        this.buttonClose.Location = new System.Drawing.Point(383, 11);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(74, 27);
        this.buttonClose.TabIndex = 0;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // dataGridViewPriceLocator
        // 
        this.dataGridViewPriceLocator.AllowUserToAddRows = false;
        this.dataGridViewPriceLocator.AllowUserToDeleteRows = false;
        this.dataGridViewPriceLocator.AllowUserToOrderColumns = true;
        this.dataGridViewPriceLocator.BorderStyle = System.Windows.Forms.BorderStyle.None;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewPriceLocator.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewPriceLocator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewPriceLocator.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnItem,
            this.ColumnSpacing,
            this.ColumnPsdescrip,
            this.ColumnPrcfact});
        this.dataGridViewPriceLocator.EnableHeadersVisualStyles = false;
        this.dataGridViewPriceLocator.Location = new System.Drawing.Point(11, 42);
        this.dataGridViewPriceLocator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.dataGridViewPriceLocator.Name = "dataGridViewPriceLocator";
        this.dataGridViewPriceLocator.ReadOnly = true;
        this.dataGridViewPriceLocator.RowHeadersVisible = false;
        this.dataGridViewPriceLocator.RowTemplate.Height = 24;
        this.dataGridViewPriceLocator.Size = new System.Drawing.Size(522, 124);
        this.dataGridViewPriceLocator.TabIndex = 1;
        this.dataGridViewPriceLocator.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPriceLocator_CellContentDoubleClick);
        this.dataGridViewPriceLocator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPriceLocator_KeyDown);
        // 
        // ColumnItem
        // 
        this.ColumnItem.DataPropertyName = "item";
        this.ColumnItem.HeaderText = "Item";
        this.ColumnItem.Name = "ColumnItem";
        this.ColumnItem.ReadOnly = true;
        this.ColumnItem.Width = 200;
        // 
        // ColumnSpacing
        // 
        this.ColumnSpacing.DataPropertyName = "spacing";
        this.ColumnSpacing.HeaderText = "Spacing";
        this.ColumnSpacing.Name = "ColumnSpacing";
        this.ColumnSpacing.ReadOnly = true;
        // 
        // ColumnPsdescrip
        // 
        this.ColumnPsdescrip.DataPropertyName = "psdescrip";
        this.ColumnPsdescrip.HeaderText = "Schedule";
        this.ColumnPsdescrip.Name = "ColumnPsdescrip";
        this.ColumnPsdescrip.ReadOnly = true;
        // 
        // ColumnPrcfact
        // 
        this.ColumnPrcfact.DataPropertyName = "prcfact";
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle2.Format = "N2";
        dataGridViewCellStyle2.NullValue = null;
        this.ColumnPrcfact.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnPrcfact.HeaderText = "Price Factor";
        this.ColumnPrcfact.Name = "ColumnPrcfact";
        this.ColumnPrcfact.ReadOnly = true;
        // 
        // buttonItem
        // 
        this.buttonItem.Location = new System.Drawing.Point(14, 191);
        this.buttonItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonItem.Name = "buttonItem";
        this.buttonItem.Size = new System.Drawing.Size(56, 19);
        this.buttonItem.TabIndex = 2;
        this.buttonItem.Text = "Item";
        this.buttonItem.UseVisualStyleBackColor = true;
        this.buttonItem.Click += new System.EventHandler(this.buttonItem_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(346, 177);
        this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(76, 13);
        this.label1.TabIndex = 3;
        this.label1.Text = "Price Factor";
        // 
        // buttonSpacing
        // 
        this.buttonSpacing.Location = new System.Drawing.Point(124, 191);
        this.buttonSpacing.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonSpacing.Name = "buttonSpacing";
        this.buttonSpacing.Size = new System.Drawing.Size(56, 19);
        this.buttonSpacing.TabIndex = 5;
        this.buttonSpacing.Text = "Spacing";
        this.buttonSpacing.UseVisualStyleBackColor = true;
        this.buttonSpacing.Click += new System.EventHandler(this.buttonSpacing_Click);
        // 
        // buttonPrsschd
        // 
        this.buttonPrsschd.Location = new System.Drawing.Point(215, 191);
        this.buttonPrsschd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonPrsschd.Name = "buttonPrsschd";
        this.buttonPrsschd.Size = new System.Drawing.Size(89, 19);
        this.buttonPrsschd.TabIndex = 7;
        this.buttonPrsschd.Text = "Price Schedule";
        this.buttonPrsschd.UseVisualStyleBackColor = true;
        this.buttonPrsschd.Click += new System.EventHandler(this.buttonPrsschd_Click);
        // 
        // textBoxPrcfact
        // 
        this.textBoxPrcfact.Location = new System.Drawing.Point(349, 190);
        this.textBoxPrcfact.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxPrcfact.Name = "textBoxPrcfact";
        this.textBoxPrcfact.Size = new System.Drawing.Size(46, 20);
        this.textBoxPrcfact.TabIndex = 9;
        // 
        // buttonClear
        // 
        this.buttonClear.Location = new System.Drawing.Point(87, 11);
        this.buttonClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClear.Name = "buttonClear";
        this.buttonClear.Size = new System.Drawing.Size(56, 27);
        this.buttonClear.TabIndex = 121;
        this.buttonClear.Text = "Clear";
        this.buttonClear.UseVisualStyleBackColor = true;
        this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
        // 
        // buttonEdit
        // 
        this.buttonEdit.Location = new System.Drawing.Point(235, 11);
        this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonEdit.Name = "buttonEdit";
        this.buttonEdit.Size = new System.Drawing.Size(56, 27);
        this.buttonEdit.TabIndex = 120;
        this.buttonEdit.Text = "Edit";
        this.buttonEdit.UseVisualStyleBackColor = true;
        this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
        // 
        // buttonInsert
        // 
        this.buttonInsert.Location = new System.Drawing.Point(13, 11);
        this.buttonInsert.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonInsert.Name = "buttonInsert";
        this.buttonInsert.Size = new System.Drawing.Size(56, 27);
        this.buttonInsert.TabIndex = 119;
        this.buttonInsert.Text = "Insert";
        this.buttonInsert.UseVisualStyleBackColor = true;
        this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
        // 
        // buttonSave
        // 
        this.buttonSave.Location = new System.Drawing.Point(309, 11);
        this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Size = new System.Drawing.Size(56, 27);
        this.buttonSave.TabIndex = 118;
        this.buttonSave.Text = "Save";
        this.buttonSave.UseVisualStyleBackColor = true;
        this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
        // 
        // labelItem
        // 
        this.labelItem.AutoSize = true;
        this.labelItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelItem.Location = new System.Drawing.Point(17, 177);
        this.labelItem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelItem.Name = "labelItem";
        this.labelItem.Size = new System.Drawing.Size(31, 13);
        this.labelItem.TabIndex = 122;
        this.labelItem.Text = "Item";
        // 
        // labelSpacing
        // 
        this.labelSpacing.AutoSize = true;
        this.labelSpacing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelSpacing.Location = new System.Drawing.Point(127, 177);
        this.labelSpacing.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelSpacing.Name = "labelSpacing";
        this.labelSpacing.Size = new System.Drawing.Size(53, 13);
        this.labelSpacing.TabIndex = 123;
        this.labelSpacing.Text = "Spacing";
        // 
        // labelPrssched
        // 
        this.labelPrssched.AutoSize = true;
        this.labelPrssched.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelPrssched.Location = new System.Drawing.Point(218, 177);
        this.labelPrssched.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelPrssched.Name = "labelPrssched";
        this.labelPrssched.Size = new System.Drawing.Size(93, 13);
        this.labelPrssched.TabIndex = 124;
        this.labelPrssched.Text = "Price Schedule";
        // 
        // buttonDelete
        // 
        this.buttonDelete.Location = new System.Drawing.Point(161, 11);
        this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonDelete.Name = "buttonDelete";
        this.buttonDelete.Size = new System.Drawing.Size(56, 27);
        this.buttonDelete.TabIndex = 125;
        this.buttonDelete.Text = "Delete";
        this.buttonDelete.UseVisualStyleBackColor = true;
        this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
        // 
        // FrmMaintainPriceLocator
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(586, 242);
        this.Controls.Add(this.buttonDelete);
        this.Controls.Add(this.labelPrssched);
        this.Controls.Add(this.labelSpacing);
        this.Controls.Add(this.labelItem);
        this.Controls.Add(this.buttonClear);
        this.Controls.Add(this.buttonEdit);
        this.Controls.Add(this.buttonInsert);
        this.Controls.Add(this.buttonSave);
        this.Controls.Add(this.textBoxPrcfact);
        this.Controls.Add(this.buttonPrsschd);
        this.Controls.Add(this.buttonSpacing);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.buttonItem);
        this.Controls.Add(this.dataGridViewPriceLocator);
        this.Controls.Add(this.buttonClose);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.Name = "FrmMaintainPriceLocator";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Maintain Price Locator";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPriceLocator)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.DataGridView dataGridViewPriceLocator;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItem;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpacing;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPsdescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrcfact;
    private System.Windows.Forms.Button buttonItem;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button buttonSpacing;
    private System.Windows.Forms.Button buttonPrsschd;
    private System.Windows.Forms.TextBox textBoxPrcfact;
    private System.Windows.Forms.Button buttonClear;
    private System.Windows.Forms.Button buttonEdit;
    private System.Windows.Forms.Button buttonInsert;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Label labelItem;
    private System.Windows.Forms.Label labelSpacing;
    private System.Windows.Forms.Label labelPrssched;
    private System.Windows.Forms.Button buttonDelete;
  }
}