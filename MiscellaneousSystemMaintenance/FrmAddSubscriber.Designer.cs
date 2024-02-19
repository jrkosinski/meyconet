namespace MiscellaneousSystemMaintenance
{
  partial class FrmAddSubscriber
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddSubscriber));
      this.buttonReturn = new WSGUtilitieslib.Telemetry.Button();
      this.dataGridViewUserData = new System.Windows.Forms.DataGridView();
      this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserData)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonReturn
      // 
      this.buttonReturn.Location = new System.Drawing.Point(355, 16);
      this.buttonReturn.Name = "buttonReturn";
      this.buttonReturn.Size = new System.Drawing.Size(75, 23);
      this.buttonReturn.TabIndex = 0;
      this.buttonReturn.Text = "Return";
      this.buttonReturn.UseVisualStyleBackColor = true;
      this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
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
      this.dataGridViewUserData.Location = new System.Drawing.Point(49, 49);
      this.dataGridViewUserData.Name = "dataGridViewUserData";
      this.dataGridViewUserData.ReadOnly = true;
      this.dataGridViewUserData.RowHeadersVisible = false;
      this.dataGridViewUserData.RowTemplate.Height = 24;
      this.dataGridViewUserData.Size = new System.Drawing.Size(382, 240);
      this.dataGridViewUserData.TabIndex = 36;
      this.dataGridViewUserData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUserData_CellContentDoubleClick);
      this.dataGridViewUserData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewUserData_KeyDown);
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
      // FrmAddSubscriber
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(483, 323);
      this.Controls.Add(this.dataGridViewUserData);
      this.Controls.Add(this.buttonReturn);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FrmAddSubscriber";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Add Subscriber";
      this.Activated += new System.EventHandler(this.FrmAddSubscriber_Activated);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserData)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonReturn;
    private System.Windows.Forms.DataGridView dataGridViewUserData;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
  }
}