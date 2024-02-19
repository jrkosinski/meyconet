namespace MiscellaneousSystemMaintenance
{
  partial class FrmMaintainSubscribers
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaintainSubscribers));
      this.buttonReturn = new WSGUtilitieslib.Telemetry.Button();
      this.buttonAddUser = new WSGUtilitieslib.Telemetry.Button();
      this.dataGridViewUserData = new System.Windows.Forms.DataGridView();
      this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserData)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonReturn
      // 
      this.buttonReturn.Location = new System.Drawing.Point(325, 23);
      this.buttonReturn.Name = "buttonReturn";
      this.buttonReturn.Size = new System.Drawing.Size(75, 23);
      this.buttonReturn.TabIndex = 0;
      this.buttonReturn.Text = "Return";
      this.buttonReturn.UseVisualStyleBackColor = true;
      this.buttonReturn.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // buttonAddUser
      // 
      this.buttonAddUser.Location = new System.Drawing.Point(172, 23);
      this.buttonAddUser.Name = "buttonAddUser";
      this.buttonAddUser.Size = new System.Drawing.Size(147, 23);
      this.buttonAddUser.TabIndex = 1;
      this.buttonAddUser.Text = "Add Subscriber";
      this.buttonAddUser.UseVisualStyleBackColor = true;
      this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
      // 
      // dataGridViewUserData
      // 
      this.dataGridViewUserData.AllowUserToOrderColumns = true;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewUserData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridViewUserData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewUserData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCode,
            this.ColumnDescrip});
      this.dataGridViewUserData.EnableHeadersVisualStyles = false;
      this.dataGridViewUserData.Location = new System.Drawing.Point(34, 88);
      this.dataGridViewUserData.Name = "dataGridViewUserData";
      this.dataGridViewUserData.ReadOnly = true;
      this.dataGridViewUserData.RowHeadersVisible = false;
      this.dataGridViewUserData.RowTemplate.Height = 24;
      this.dataGridViewUserData.Size = new System.Drawing.Size(382, 240);
      this.dataGridViewUserData.TabIndex = 35;
      this.dataGridViewUserData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUserData_CellContentDoubleClick);
      // 
      // ColumnCode
      // 
      this.ColumnCode.DataPropertyName = "userid";
      this.ColumnCode.HeaderText = "User ID";
      this.ColumnCode.Name = "ColumnCode";
      this.ColumnCode.ReadOnly = true;
      this.ColumnCode.Width = 75;
      // 
      // ColumnDescrip
      // 
      this.ColumnDescrip.DataPropertyName = "username";
      this.ColumnDescrip.FillWeight = 250F;
      this.ColumnDescrip.HeaderText = "User Name";
      this.ColumnDescrip.Name = "ColumnDescrip";
      this.ColumnDescrip.ReadOnly = true;
      this.ColumnDescrip.Width = 300;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(122, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(197, 17);
      this.label1.TabIndex = 36;
      this.label1.Text = "Double Click User To Remove";
      // 
      // FrmMaintainSubscribers
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(446, 395);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dataGridViewUserData);
      this.Controls.Add(this.buttonAddUser);
      this.Controls.Add(this.buttonReturn);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FrmMaintainSubscribers";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Maintain Subscribers";
      this.Activated += new System.EventHandler(this.FrmMaintainSubscribers_Activated);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserData)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonReturn;
    private System.Windows.Forms.Button buttonAddUser;
    private System.Windows.Forms.DataGridView dataGridViewUserData;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.Label label1;
  }
}