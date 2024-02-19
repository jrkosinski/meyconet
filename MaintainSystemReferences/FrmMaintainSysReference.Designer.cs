namespace MaintainSystemReferences
{
  partial class FrmMaintainSysReference
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
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      this.comboBoxRefrenceGroups = new System.Windows.Forms.ComboBox();
      this.dataGridViewReferenceDetails = new System.Windows.Forms.DataGridView();
      this.ColumnRefdescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label2 = new System.Windows.Forms.Label();
      this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
      this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
      this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
      this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
      this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
      this.textBoxDescrip = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReferenceDetails)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(436, 98);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(73, 27);
      this.buttonClose.TabIndex = 0;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      // 
      // comboBoxRefrenceGroups
      // 
      this.comboBoxRefrenceGroups.FormattingEnabled = true;
      this.comboBoxRefrenceGroups.Location = new System.Drawing.Point(88, 59);
      this.comboBoxRefrenceGroups.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.comboBoxRefrenceGroups.Name = "comboBoxRefrenceGroups";
      this.comboBoxRefrenceGroups.Size = new System.Drawing.Size(129, 21);
      this.comboBoxRefrenceGroups.TabIndex = 1;
      // 
      // dataGridViewReferenceDetails
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewReferenceDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridViewReferenceDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewReferenceDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnRefdescrip});
      this.dataGridViewReferenceDetails.EnableHeadersVisualStyles = false;
      this.dataGridViewReferenceDetails.Location = new System.Drawing.Point(135, 160);
      this.dataGridViewReferenceDetails.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.dataGridViewReferenceDetails.Name = "dataGridViewReferenceDetails";
      this.dataGridViewReferenceDetails.RowHeadersVisible = false;
      this.dataGridViewReferenceDetails.RowTemplate.Height = 24;
      this.dataGridViewReferenceDetails.Size = new System.Drawing.Size(401, 232);
      this.dataGridViewReferenceDetails.TabIndex = 2;
      // 
      // ColumnRefdescrip
      // 
      this.ColumnRefdescrip.DataPropertyName = "refdescrip";
      this.ColumnRefdescrip.HeaderText = "Description";
      this.ColumnRefdescrip.Name = "ColumnRefdescrip";
      this.ColumnRefdescrip.Width = 400;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(100, 21);
      this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(92, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Select a Group";
      // 
      // buttonDelete
      // 
      this.buttonDelete.Location = new System.Drawing.Point(316, 98);
      this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.buttonDelete.Name = "buttonDelete";
      this.buttonDelete.Size = new System.Drawing.Size(56, 27);
      this.buttonDelete.TabIndex = 151;
      this.buttonDelete.Text = "Delete";
      this.buttonDelete.UseVisualStyleBackColor = true;
      // 
      // buttonClear
      // 
      this.buttonClear.Location = new System.Drawing.Point(196, 98);
      this.buttonClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.buttonClear.Name = "buttonClear";
      this.buttonClear.Size = new System.Drawing.Size(56, 27);
      this.buttonClear.TabIndex = 150;
      this.buttonClear.Text = "Clear";
      this.buttonClear.UseVisualStyleBackColor = true;
      // 
      // buttonEdit
      // 
      this.buttonEdit.Location = new System.Drawing.Point(256, 98);
      this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.buttonEdit.Name = "buttonEdit";
      this.buttonEdit.Size = new System.Drawing.Size(56, 27);
      this.buttonEdit.TabIndex = 149;
      this.buttonEdit.Text = "Edit";
      this.buttonEdit.UseVisualStyleBackColor = true;
      // 
      // buttonInsert
      // 
      this.buttonInsert.Location = new System.Drawing.Point(136, 98);
      this.buttonInsert.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.buttonInsert.Name = "buttonInsert";
      this.buttonInsert.Size = new System.Drawing.Size(56, 27);
      this.buttonInsert.TabIndex = 148;
      this.buttonInsert.Text = "Insert";
      this.buttonInsert.UseVisualStyleBackColor = true;
      // 
      // buttonSave
      // 
      this.buttonSave.Location = new System.Drawing.Point(376, 98);
      this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new System.Drawing.Size(56, 27);
      this.buttonSave.TabIndex = 147;
      this.buttonSave.Text = "Save";
      this.buttonSave.UseVisualStyleBackColor = true;
      // 
      // textBoxDescrip
      // 
      this.textBoxDescrip.Location = new System.Drawing.Point(235, 134);
      this.textBoxDescrip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.textBoxDescrip.Name = "textBoxDescrip";
      this.textBoxDescrip.Size = new System.Drawing.Size(225, 20);
      this.textBoxDescrip.TabIndex = 153;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(157, 139);
      this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(71, 13);
      this.label3.TabIndex = 152;
      this.label3.Text = "Description";
      // 
      // FrmMaintainSysReference
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(627, 435);
      this.Controls.Add(this.textBoxDescrip);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.buttonDelete);
      this.Controls.Add(this.buttonClear);
      this.Controls.Add(this.buttonEdit);
      this.Controls.Add(this.buttonInsert);
      this.Controls.Add(this.buttonSave);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.dataGridViewReferenceDetails);
      this.Controls.Add(this.comboBoxRefrenceGroups);
      this.Controls.Add(this.buttonClose);
      this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.Name = "FrmMaintainSysReference";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Maintain Reference Tables";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReferenceDetails)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.Button buttonClose;
    public System.Windows.Forms.DataGridView dataGridViewReferenceDetails;
    public System.Windows.Forms.ComboBox comboBoxRefrenceGroups;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.Button buttonDelete;
    public System.Windows.Forms.Button buttonClear;
    public System.Windows.Forms.Button buttonEdit;
    public System.Windows.Forms.Button buttonInsert;
    public System.Windows.Forms.Button buttonSave;
    public System.Windows.Forms.TextBox textBoxDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRefdescrip;
  }
}