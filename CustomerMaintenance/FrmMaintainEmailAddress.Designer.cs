namespace CustomerMaintenance
{
  partial class FrmMaintainEmailAddress
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
      this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
      this.textBoxEmailAddress = new System.Windows.Forms.TextBox();
      this.dataGridViewEmailAddresses = new System.Windows.Forms.DataGridView();
      this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
      this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
      this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
      this.ColumnEmailAdresss = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmailAddresses)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonEdit
      // 
      this.buttonEdit.Location = new System.Drawing.Point(9, 12);
      this.buttonEdit.Name = "buttonEdit";
      this.buttonEdit.Size = new System.Drawing.Size(75, 23);
      this.buttonEdit.TabIndex = 4;
      this.buttonEdit.Text = "Edit";
      this.buttonEdit.UseVisualStyleBackColor = true;
      // 
      // textBoxEmailAddress
      // 
      this.textBoxEmailAddress.Location = new System.Drawing.Point(62, 81);
      this.textBoxEmailAddress.Name = "textBoxEmailAddress";
      this.textBoxEmailAddress.Size = new System.Drawing.Size(300, 20);
      this.textBoxEmailAddress.TabIndex = 8;
      // 
      // dataGridViewEmailAddresses
      // 
      this.dataGridViewEmailAddresses.AllowUserToAddRows = false;
      this.dataGridViewEmailAddresses.AllowUserToDeleteRows = false;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewEmailAddresses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridViewEmailAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewEmailAddresses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnEmailAdresss});
      this.dataGridViewEmailAddresses.EnableHeadersVisualStyles = false;
      this.dataGridViewEmailAddresses.Location = new System.Drawing.Point(53, 148);
      this.dataGridViewEmailAddresses.Name = "dataGridViewEmailAddresses";
      this.dataGridViewEmailAddresses.RowHeadersVisible = false;
      this.dataGridViewEmailAddresses.Size = new System.Drawing.Size(353, 150);
      this.dataGridViewEmailAddresses.TabIndex = 1;
      // 
      // buttonSave
      // 
      this.buttonSave.Location = new System.Drawing.Point(426, 12);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new System.Drawing.Size(75, 23);
      this.buttonSave.TabIndex = 9;
      this.buttonSave.Text = "Save";
      this.buttonSave.UseVisualStyleBackColor = true;
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(345, 12);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(75, 23);
      this.buttonClose.TabIndex = 0;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      // 
      // buttonCancel
      // 
      this.buttonCancel.Location = new System.Drawing.Point(261, 12);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 3;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // buttonInsert
      // 
      this.buttonInsert.Location = new System.Drawing.Point(177, 12);
      this.buttonInsert.Name = "buttonInsert";
      this.buttonInsert.Size = new System.Drawing.Size(75, 23);
      this.buttonInsert.TabIndex = 6;
      this.buttonInsert.Text = "Insert";
      this.buttonInsert.UseVisualStyleBackColor = true;
      // 
      // buttonDelete
      // 
      this.buttonDelete.Location = new System.Drawing.Point(93, 12);
      this.buttonDelete.Name = "buttonDelete";
      this.buttonDelete.Size = new System.Drawing.Size(75, 23);
      this.buttonDelete.TabIndex = 5;
      this.buttonDelete.Text = "Delete";
      this.buttonDelete.UseVisualStyleBackColor = true;
      // 
      // ColumnEmailAdresss
      // 
      this.ColumnEmailAdresss.DataPropertyName = "emailaddress";
      this.ColumnEmailAdresss.HeaderText = "Email Address";
      this.ColumnEmailAdresss.Name = "ColumnEmailAdresss";
      this.ColumnEmailAdresss.Width = 350;
      // 
      // FrmMaintainEmailAddress
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(510, 351);
      this.Controls.Add(this.buttonSave);
      this.Controls.Add(this.textBoxEmailAddress);
      this.Controls.Add(this.buttonInsert);
      this.Controls.Add(this.buttonDelete);
      this.Controls.Add(this.buttonEdit);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.dataGridViewEmailAddresses);
      this.Controls.Add(this.buttonClose);
      this.Name = "FrmMaintainEmailAddress";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Email Addresses";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmailAddresses)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.Button buttonEdit;
    public System.Windows.Forms.TextBox textBoxEmailAddress;
    public System.Windows.Forms.DataGridView dataGridViewEmailAddresses;
    public System.Windows.Forms.Button buttonSave;
    public System.Windows.Forms.Button buttonClose;
    public System.Windows.Forms.Button buttonCancel;
    public System.Windows.Forms.Button buttonInsert;
    public System.Windows.Forms.Button buttonDelete;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEmailAdresss;

  }
}