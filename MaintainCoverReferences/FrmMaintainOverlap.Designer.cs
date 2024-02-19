namespace MaintainCoverReferences
{
  partial class FrmMaintainOverlap
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
        this.textBoxFeet = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.textBoxDescrip = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.dataGridViewOverlap = new System.Windows.Forms.DataGridView();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnFeet = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnInches = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
        this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
        this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
        this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.textBoxInches = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOverlap)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonDelete
        // 
        this.buttonDelete.Location = new System.Drawing.Point(208, 40);
        this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonDelete.Name = "buttonDelete";
        this.buttonDelete.Size = new System.Drawing.Size(56, 27);
        this.buttonDelete.TabIndex = 145;
        this.buttonDelete.Text = "Delete";
        this.buttonDelete.UseVisualStyleBackColor = true;
        this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
        // 
        // textBoxFeet
        // 
        this.textBoxFeet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxFeet.Location = new System.Drawing.Point(315, 209);
        this.textBoxFeet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxFeet.MaxLength = 1;
        this.textBoxFeet.Name = "textBoxFeet";
        this.textBoxFeet.Size = new System.Drawing.Size(22, 20);
        this.textBoxFeet.TabIndex = 144;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(235, 214);
        this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(78, 13);
        this.label2.TabIndex = 143;
        this.label2.Text = "Feet, Inches";
        // 
        // textBoxDescrip
        // 
        this.textBoxDescrip.Location = new System.Drawing.Point(107, 209);
        this.textBoxDescrip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxDescrip.Name = "textBoxDescrip";
        this.textBoxDescrip.Size = new System.Drawing.Size(108, 20);
        this.textBoxDescrip.TabIndex = 142;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(29, 214);
        this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(71, 13);
        this.label1.TabIndex = 141;
        this.label1.Text = "Description";
        // 
        // dataGridViewOverlap
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewOverlap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewOverlap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewOverlap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip,
            this.ColumnFeet,
            this.ColumnInches});
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridViewOverlap.DefaultCellStyle = dataGridViewCellStyle4;
        this.dataGridViewOverlap.EnableHeadersVisualStyles = false;
        this.dataGridViewOverlap.Location = new System.Drawing.Point(28, 83);
        this.dataGridViewOverlap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.dataGridViewOverlap.Name = "dataGridViewOverlap";
        this.dataGridViewOverlap.RowHeadersVisible = false;
        this.dataGridViewOverlap.RowTemplate.Height = 24;
        this.dataGridViewOverlap.Size = new System.Drawing.Size(402, 111);
        this.dataGridViewOverlap.TabIndex = 140;
        this.dataGridViewOverlap.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOverlap_CellContentDoubleClick);
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "descrip";
        this.ColumnDescrip.HeaderText = "Description";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.Width = 200;
        // 
        // ColumnFeet
        // 
        this.ColumnFeet.DataPropertyName = "feet";
        dataGridViewCellStyle2.Format = "N0";
        dataGridViewCellStyle2.NullValue = null;
        this.ColumnFeet.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnFeet.HeaderText = "Feet";
        this.ColumnFeet.Name = "ColumnFeet";
        this.ColumnFeet.Width = 75;
        // 
        // ColumnInches
        // 
        this.ColumnInches.DataPropertyName = "inches";
        dataGridViewCellStyle3.Format = "N0";
        dataGridViewCellStyle3.NullValue = null;
        this.ColumnInches.DefaultCellStyle = dataGridViewCellStyle3;
        this.ColumnInches.HeaderText = "Inches";
        this.ColumnInches.Name = "ColumnInches";
        this.ColumnInches.Width = 75;
        // 
        // buttonClear
        // 
        this.buttonClear.Location = new System.Drawing.Point(88, 40);
        this.buttonClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClear.Name = "buttonClear";
        this.buttonClear.Size = new System.Drawing.Size(56, 27);
        this.buttonClear.TabIndex = 139;
        this.buttonClear.Text = "Clear";
        this.buttonClear.UseVisualStyleBackColor = true;
        this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
        // 
        // buttonEdit
        // 
        this.buttonEdit.Location = new System.Drawing.Point(148, 40);
        this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonEdit.Name = "buttonEdit";
        this.buttonEdit.Size = new System.Drawing.Size(56, 27);
        this.buttonEdit.TabIndex = 138;
        this.buttonEdit.Text = "Edit";
        this.buttonEdit.UseVisualStyleBackColor = true;
        this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
        // 
        // buttonInsert
        // 
        this.buttonInsert.Location = new System.Drawing.Point(28, 40);
        this.buttonInsert.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonInsert.Name = "buttonInsert";
        this.buttonInsert.Size = new System.Drawing.Size(56, 27);
        this.buttonInsert.TabIndex = 137;
        this.buttonInsert.Text = "Insert";
        this.buttonInsert.UseVisualStyleBackColor = true;
        this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
        // 
        // buttonSave
        // 
        this.buttonSave.Location = new System.Drawing.Point(268, 40);
        this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Size = new System.Drawing.Size(56, 27);
        this.buttonSave.TabIndex = 136;
        this.buttonSave.Text = "Save";
        this.buttonSave.UseVisualStyleBackColor = true;
        this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
        // 
        // buttonClose
        // 
        this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonClose.Location = new System.Drawing.Point(328, 40);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 27);
        this.buttonClose.TabIndex = 135;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // textBoxInches
        // 
        this.textBoxInches.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxInches.Location = new System.Drawing.Point(345, 209);
        this.textBoxInches.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxInches.MaxLength = 2;
        this.textBoxInches.Name = "textBoxInches";
        this.textBoxInches.Size = new System.Drawing.Size(22, 20);
        this.textBoxInches.TabIndex = 146;
        // 
        // FrmMaintainOverlap
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(445, 280);
        this.Controls.Add(this.textBoxInches);
        this.Controls.Add(this.buttonDelete);
        this.Controls.Add(this.textBoxFeet);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.textBoxDescrip);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.dataGridViewOverlap);
        this.Controls.Add(this.buttonClear);
        this.Controls.Add(this.buttonEdit);
        this.Controls.Add(this.buttonInsert);
        this.Controls.Add(this.buttonSave);
        this.Controls.Add(this.buttonClose);
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.Name = "FrmMaintainOverlap";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Maintain Overlap";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOverlap)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonDelete;
    private System.Windows.Forms.TextBox textBoxFeet;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxDescrip;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridView dataGridViewOverlap;
    private System.Windows.Forms.Button buttonClear;
    private System.Windows.Forms.Button buttonEdit;
    private System.Windows.Forms.Button buttonInsert;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.TextBox textBoxInches;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFeet;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInches;
  }
}