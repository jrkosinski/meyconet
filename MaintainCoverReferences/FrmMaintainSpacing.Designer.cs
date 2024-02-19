namespace MaintainCoverReferences
{
  partial class FrmMaintainSpacing
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaintainSpacing));
        this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
        this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
        this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
        this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.dataGridViewSpacing = new System.Windows.Forms.DataGridView();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnStrpmult = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.label1 = new System.Windows.Forms.Label();
        this.textBoxDescrip = new System.Windows.Forms.TextBox();
        this.textBoxStrpmult = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpacing)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonClear
        // 
        this.buttonClear.Location = new System.Drawing.Point(87, 10);
        this.buttonClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClear.Name = "buttonClear";
        this.buttonClear.Size = new System.Drawing.Size(56, 27);
        this.buttonClear.TabIndex = 117;
        this.buttonClear.Text = "Clear";
        this.buttonClear.UseVisualStyleBackColor = true;
        this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
        // 
        // buttonEdit
        // 
        this.buttonEdit.Location = new System.Drawing.Point(154, 10);
        this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonEdit.Name = "buttonEdit";
        this.buttonEdit.Size = new System.Drawing.Size(56, 27);
        this.buttonEdit.TabIndex = 116;
        this.buttonEdit.Text = "Edit";
        this.buttonEdit.UseVisualStyleBackColor = true;
        this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
        // 
        // buttonInsert
        // 
        this.buttonInsert.Location = new System.Drawing.Point(20, 10);
        this.buttonInsert.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonInsert.Name = "buttonInsert";
        this.buttonInsert.Size = new System.Drawing.Size(56, 27);
        this.buttonInsert.TabIndex = 115;
        this.buttonInsert.Text = "Insert";
        this.buttonInsert.UseVisualStyleBackColor = true;
        this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
        // 
        // buttonSave
        // 
        this.buttonSave.Location = new System.Drawing.Point(221, 10);
        this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Size = new System.Drawing.Size(56, 27);
        this.buttonSave.TabIndex = 114;
        this.buttonSave.Text = "Save";
        this.buttonSave.UseVisualStyleBackColor = true;
        this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
        // 
        // buttonClose
        // 
        this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonClose.Location = new System.Drawing.Point(288, 10);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 27);
        this.buttonClose.TabIndex = 113;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // dataGridViewSpacing
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewSpacing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewSpacing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewSpacing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip,
            this.ColumnStrpmult});
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridViewSpacing.DefaultCellStyle = dataGridViewCellStyle3;
        this.dataGridViewSpacing.EnableHeadersVisualStyles = false;
        this.dataGridViewSpacing.Location = new System.Drawing.Point(20, 41);
        this.dataGridViewSpacing.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.dataGridViewSpacing.Name = "dataGridViewSpacing";
        this.dataGridViewSpacing.RowHeadersVisible = false;
        this.dataGridViewSpacing.RowTemplate.Height = 24;
        this.dataGridViewSpacing.Size = new System.Drawing.Size(318, 141);
        this.dataGridViewSpacing.TabIndex = 118;
        this.dataGridViewSpacing.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSpacing_CellContentDoubleClick);
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "descrip";
        this.ColumnDescrip.HeaderText = "Spacing";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.Width = 200;
        // 
        // ColumnStrpmult
        // 
        this.ColumnStrpmult.DataPropertyName = "strpmult";
        dataGridViewCellStyle2.Format = "N3";
        dataGridViewCellStyle2.NullValue = null;
        this.ColumnStrpmult.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnStrpmult.HeaderText = "Strap Multiplier";
        this.ColumnStrpmult.Name = "ColumnStrpmult";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(21, 202);
        this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(53, 13);
        this.label1.TabIndex = 119;
        this.label1.Text = "Spacing";
        // 
        // textBoxDescrip
        // 
        this.textBoxDescrip.Location = new System.Drawing.Point(82, 200);
        this.textBoxDescrip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxDescrip.Name = "textBoxDescrip";
        this.textBoxDescrip.Size = new System.Drawing.Size(108, 20);
        this.textBoxDescrip.TabIndex = 120;
        // 
        // textBoxStrpmult
        // 
        this.textBoxStrpmult.Location = new System.Drawing.Point(305, 197);
        this.textBoxStrpmult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxStrpmult.Name = "textBoxStrpmult";
        this.textBoxStrpmult.Size = new System.Drawing.Size(72, 20);
        this.textBoxStrpmult.TabIndex = 122;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(209, 200);
        this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(92, 13);
        this.label2.TabIndex = 121;
        this.label2.Text = "Strap Multiplier";
        // 
        // FrmMaintainSpacing
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(459, 284);
        this.Controls.Add(this.textBoxStrpmult);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.textBoxDescrip);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.dataGridViewSpacing);
        this.Controls.Add(this.buttonClear);
        this.Controls.Add(this.buttonEdit);
        this.Controls.Add(this.buttonInsert);
        this.Controls.Add(this.buttonSave);
        this.Controls.Add(this.buttonClose);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.Name = "FrmMaintainSpacing";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Spacing Maintenance";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpacing)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonClear;
    private System.Windows.Forms.Button buttonEdit;
    private System.Windows.Forms.Button buttonInsert;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.DataGridView dataGridViewSpacing;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxDescrip;
    private System.Windows.Forms.TextBox textBoxStrpmult;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStrpmult;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
  }
}