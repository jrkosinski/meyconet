namespace MaintainCoverReferences
{
  partial class FrmMaintainColor
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
      this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
      this.textBoxDescrip = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.dataGridViewColor = new System.Windows.Forms.DataGridView();
      this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
      this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
      this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
      this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColor)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonDelete
      // 
      this.buttonDelete.Location = new System.Drawing.Point(388, 87);
      this.buttonDelete.Name = "buttonDelete";
      this.buttonDelete.Size = new System.Drawing.Size(75, 33);
      this.buttonDelete.TabIndex = 157;
      this.buttonDelete.Text = "Delete";
      this.buttonDelete.UseVisualStyleBackColor = true;
      this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
      // 
      // textBoxDescrip
      // 
      this.textBoxDescrip.Location = new System.Drawing.Point(330, 318);
      this.textBoxDescrip.Name = "textBoxDescrip";
      this.textBoxDescrip.Size = new System.Drawing.Size(142, 22);
      this.textBoxDescrip.TabIndex = 154;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(225, 324);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(90, 17);
      this.label1.TabIndex = 153;
      this.label1.Text = "Description";
      // 
      // dataGridViewColor
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewColor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridViewColor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewColor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip});
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridViewColor.DefaultCellStyle = dataGridViewCellStyle2;
      this.dataGridViewColor.EnableHeadersVisualStyles = false;
      this.dataGridViewColor.Location = new System.Drawing.Point(255, 162);
      this.dataGridViewColor.Name = "dataGridViewColor";
      this.dataGridViewColor.RowHeadersVisible = false;
      this.dataGridViewColor.RowTemplate.Height = 24;
      this.dataGridViewColor.Size = new System.Drawing.Size(208, 108);
      this.dataGridViewColor.TabIndex = 152;
      this.dataGridViewColor.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewColor_CellContentDoubleClick);
      // 
      // ColumnDescrip
      // 
      this.ColumnDescrip.DataPropertyName = "descrip";
      this.ColumnDescrip.HeaderText = "Description";
      this.ColumnDescrip.Name = "ColumnDescrip";
      this.ColumnDescrip.Width = 200;
      // 
      // buttonClear
      // 
      this.buttonClear.Location = new System.Drawing.Point(180, 87);
      this.buttonClear.Name = "buttonClear";
      this.buttonClear.Size = new System.Drawing.Size(75, 33);
      this.buttonClear.TabIndex = 151;
      this.buttonClear.Text = "Clear";
      this.buttonClear.UseVisualStyleBackColor = true;
      this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
      // 
      // buttonEdit
      // 
      this.buttonEdit.Location = new System.Drawing.Point(284, 87);
      this.buttonEdit.Name = "buttonEdit";
      this.buttonEdit.Size = new System.Drawing.Size(75, 33);
      this.buttonEdit.TabIndex = 150;
      this.buttonEdit.Text = "Edit";
      this.buttonEdit.UseVisualStyleBackColor = true;
      this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
      // 
      // buttonInsert
      // 
      this.buttonInsert.Location = new System.Drawing.Point(76, 87);
      this.buttonInsert.Name = "buttonInsert";
      this.buttonInsert.Size = new System.Drawing.Size(75, 33);
      this.buttonInsert.TabIndex = 149;
      this.buttonInsert.Text = "Insert";
      this.buttonInsert.UseVisualStyleBackColor = true;
      this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
      // 
      // buttonSave
      // 
      this.buttonSave.Location = new System.Drawing.Point(492, 87);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new System.Drawing.Size(75, 33);
      this.buttonSave.TabIndex = 148;
      this.buttonSave.Text = "Save";
      this.buttonSave.UseVisualStyleBackColor = true;
      this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
      // 
      // buttonClose
      // 
      this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonClose.Location = new System.Drawing.Point(596, 87);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(75, 33);
      this.buttonClose.TabIndex = 147;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // FrmMaintainColor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(746, 441);
      this.Controls.Add(this.buttonDelete);
      this.Controls.Add(this.textBoxDescrip);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dataGridViewColor);
      this.Controls.Add(this.buttonClear);
      this.Controls.Add(this.buttonEdit);
      this.Controls.Add(this.buttonInsert);
      this.Controls.Add(this.buttonSave);
      this.Controls.Add(this.buttonClose);
      this.Name = "FrmMaintainColor";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Maintain Color";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColor)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonDelete;
    private System.Windows.Forms.TextBox textBoxDescrip;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridView dataGridViewColor;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.Button buttonClear;
    private System.Windows.Forms.Button buttonEdit;
    private System.Windows.Forms.Button buttonInsert;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonClose;
  }
}