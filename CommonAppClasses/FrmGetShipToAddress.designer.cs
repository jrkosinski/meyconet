namespace CommonAppClasses
{
  partial class FrmGetShipToAddress
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        this.dataGridViewShipToAddresses = new System.Windows.Forms.DataGridView();
        this.ColumnCustno = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.buttonDefault = new WSGUtilitieslib.Telemetry.Button();
        this.buttonBillTo = new WSGUtilitieslib.Telemetry.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShipToAddresses)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridViewShipToAddresses
        // 
        this.dataGridViewShipToAddresses.AllowUserToAddRows = false;
        this.dataGridViewShipToAddresses.AllowUserToDeleteRows = false;
        this.dataGridViewShipToAddresses.AllowUserToOrderColumns = true;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewShipToAddresses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewShipToAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewShipToAddresses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCustno,
            this.ColumnCompany,
            this.ColumnAddress,
            this.ColumnCity,
            this.ColumnState,
            this.ColumnZip,
            this.ColumnPhone});
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridViewShipToAddresses.DefaultCellStyle = dataGridViewCellStyle3;
        this.dataGridViewShipToAddresses.EnableHeadersVisualStyles = false;
        this.dataGridViewShipToAddresses.Location = new System.Drawing.Point(11, 56);
        this.dataGridViewShipToAddresses.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewShipToAddresses.Name = "dataGridViewShipToAddresses";
        this.dataGridViewShipToAddresses.ReadOnly = true;
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewShipToAddresses.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        this.dataGridViewShipToAddresses.RowHeadersVisible = false;
        this.dataGridViewShipToAddresses.RowTemplate.Height = 24;
        this.dataGridViewShipToAddresses.Size = new System.Drawing.Size(869, 177);
        this.dataGridViewShipToAddresses.TabIndex = 109;
        this.dataGridViewShipToAddresses.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewShipToAddresses_CellContentDoubleClick);
        this.dataGridViewShipToAddresses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewShipToAddresses_KeyDown);
        // 
        // ColumnCustno
        // 
        this.ColumnCustno.DataPropertyName = "cshipno";
        dataGridViewCellStyle2.Format = "N2";
        dataGridViewCellStyle2.NullValue = null;
        this.ColumnCustno.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnCustno.HeaderText = "Ship No";
        this.ColumnCustno.Name = "ColumnCustno";
        this.ColumnCustno.ReadOnly = true;
        this.ColumnCustno.Width = 75;
        // 
        // ColumnCompany
        // 
        this.ColumnCompany.DataPropertyName = "company";
        this.ColumnCompany.HeaderText = "Company";
        this.ColumnCompany.Name = "ColumnCompany";
        this.ColumnCompany.ReadOnly = true;
        this.ColumnCompany.Width = 225;
        // 
        // ColumnAddress
        // 
        this.ColumnAddress.DataPropertyName = "address1";
        this.ColumnAddress.HeaderText = "Address";
        this.ColumnAddress.Name = "ColumnAddress";
        this.ColumnAddress.ReadOnly = true;
        this.ColumnAddress.Width = 150;
        // 
        // ColumnCity
        // 
        this.ColumnCity.DataPropertyName = "city";
        this.ColumnCity.HeaderText = "City";
        this.ColumnCity.Name = "ColumnCity";
        this.ColumnCity.ReadOnly = true;
        this.ColumnCity.Width = 150;
        // 
        // ColumnState
        // 
        this.ColumnState.DataPropertyName = "state";
        this.ColumnState.HeaderText = "State";
        this.ColumnState.Name = "ColumnState";
        this.ColumnState.ReadOnly = true;
        this.ColumnState.Width = 50;
        // 
        // ColumnZip
        // 
        this.ColumnZip.DataPropertyName = "zip";
        this.ColumnZip.HeaderText = "Zip";
        this.ColumnZip.Name = "ColumnZip";
        this.ColumnZip.ReadOnly = true;
        // 
        // ColumnPhone
        // 
        this.ColumnPhone.DataPropertyName = "phone";
        this.ColumnPhone.HeaderText = "Phone";
        this.ColumnPhone.Name = "ColumnPhone";
        this.ColumnPhone.ReadOnly = true;
        // 
        // buttonClose
        // 
        this.buttonClose.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonClose.Location = new System.Drawing.Point(248, 11);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(66, 31);
        this.buttonClose.TabIndex = 110;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = false;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // buttonDefault
        // 
        this.buttonDefault.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonDefault.Location = new System.Drawing.Point(25, 11);
        this.buttonDefault.Margin = new System.Windows.Forms.Padding(2);
        this.buttonDefault.Name = "buttonDefault";
        this.buttonDefault.Size = new System.Drawing.Size(98, 31);
        this.buttonDefault.TabIndex = 111;
        this.buttonDefault.Text = "Use Default";
        this.buttonDefault.UseVisualStyleBackColor = false;
        this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
        // 
        // buttonBillTo
        // 
        this.buttonBillTo.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonBillTo.Location = new System.Drawing.Point(138, 11);
        this.buttonBillTo.Margin = new System.Windows.Forms.Padding(2);
        this.buttonBillTo.Name = "buttonBillTo";
        this.buttonBillTo.Size = new System.Drawing.Size(95, 31);
        this.buttonBillTo.TabIndex = 112;
        this.buttonBillTo.Text = "Use Bill To";
        this.buttonBillTo.UseVisualStyleBackColor = false;
        this.buttonBillTo.Click += new System.EventHandler(this.buttonBillTo_Click);
        // 
        // FrmGetShipToAddress
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(896, 281);
        this.Controls.Add(this.buttonBillTo);
        this.Controls.Add(this.buttonDefault);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.dataGridViewShipToAddresses);
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmGetShipToAddress";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Ship To Address Selector";
        this.Shown += new System.EventHandler(this.FrmGetShipToAddress_Shown);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShipToAddresses)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewShipToAddresses;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCustno;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompany;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddress;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCity;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnState;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnZip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPhone;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Button buttonDefault;
    private System.Windows.Forms.Button buttonBillTo;
  }
}