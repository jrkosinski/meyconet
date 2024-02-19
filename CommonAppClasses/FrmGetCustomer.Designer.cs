namespace CommonAppClasses
{
  partial class FrmGetCustomer
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetCustomer));
        this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
        this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
        this.ColumnCustno = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.label5 = new System.Windows.Forms.Label();
        this.textBoxPhone = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.textBoxZip = new System.Windows.Forms.TextBox();
        this.textBoxState = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.textBoxCompany = new System.Windows.Forms.TextBox();
        this.textBoxCustno = new System.Windows.Forms.TextBox();
        this.buttonSearch = new WSGUtilitieslib.Telemetry.Button();
        this.textBoxBillAddress1 = new System.Windows.Forms.TextBox();
        this.label126 = new System.Windows.Forms.Label();
        this.textBoxShiptozip = new System.Windows.Forms.TextBox();
        this.textBoxShiptostate = new System.Windows.Forms.TextBox();
        this.textBoxShiptocity = new System.Windows.Forms.TextBox();
        this.textBoxShiptoaddress2 = new System.Windows.Forms.TextBox();
        this.textBoxShiptoaddress1 = new System.Windows.Forms.TextBox();
        this.textBoxShiptocompany = new System.Windows.Forms.TextBox();
        this.label114 = new System.Windows.Forms.Label();
        this.textBoxBillEmail = new System.Windows.Forms.TextBox();
        this.Label13 = new System.Windows.Forms.Label();
        this.textBoxBillCompany = new System.Windows.Forms.TextBox();
        this.textBoxBillFaxNo = new System.Windows.Forms.TextBox();
        this.Label12 = new System.Windows.Forms.Label();
        this.textBoxBillPhone = new System.Windows.Forms.TextBox();
        this.Label11 = new System.Windows.Forms.Label();
        this.buttonAccept = new WSGUtilitieslib.Telemetry.Button();
        this.textBoxBillAddress2 = new System.Windows.Forms.TextBox();
        this.textBoxBillCity = new System.Windows.Forms.TextBox();
        this.textBoxBillState = new System.Windows.Forms.TextBox();
        this.textBoxBillZip = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonCancel
        // 
        this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonCancel.Location = new System.Drawing.Point(78, 138);
        this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(67, 33);
        this.buttonCancel.TabIndex = 1;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = false;
        // 
        // dataGridViewCustomers
        // 
        this.dataGridViewCustomers.AllowUserToAddRows = false;
        this.dataGridViewCustomers.AllowUserToDeleteRows = false;
        this.dataGridViewCustomers.AllowUserToOrderColumns = true;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCustno,
            this.ColumnCompany,
            this.ColumnAddress,
            this.ColumnCity,
            this.ColumnState,
            this.ColumnZip,
            this.ColumnPhone});
        this.dataGridViewCustomers.EnableHeadersVisualStyles = false;
        this.dataGridViewCustomers.Location = new System.Drawing.Point(18, 176);
        this.dataGridViewCustomers.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewCustomers.Name = "dataGridViewCustomers";
        this.dataGridViewCustomers.ReadOnly = true;
        this.dataGridViewCustomers.RowHeadersVisible = false;
        this.dataGridViewCustomers.RowTemplate.Height = 24;
        this.dataGridViewCustomers.Size = new System.Drawing.Size(891, 180);
        this.dataGridViewCustomers.TabIndex = 0;
        // 
        // ColumnCustno
        // 
        this.ColumnCustno.DataPropertyName = "custno";
        this.ColumnCustno.HeaderText = "Custno";
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
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(18, 97);
        this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(43, 13);
        this.label5.TabIndex = 26;
        this.label5.Text = "Phone";
        // 
        // textBoxPhone
        // 
        this.textBoxPhone.Location = new System.Drawing.Point(120, 93);
        this.textBoxPhone.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxPhone.Name = "textBoxPhone";
        this.textBoxPhone.Size = new System.Drawing.Size(152, 20);
        this.textBoxPhone.TabIndex = 25;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(18, 77);
        this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(25, 13);
        this.label4.TabIndex = 24;
        this.label4.Text = "Zip";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(18, 60);
        this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(37, 13);
        this.label3.TabIndex = 23;
        this.label3.Text = "State";
        // 
        // textBoxZip
        // 
        this.textBoxZip.Location = new System.Drawing.Point(120, 74);
        this.textBoxZip.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxZip.Name = "textBoxZip";
        this.textBoxZip.Size = new System.Drawing.Size(72, 20);
        this.textBoxZip.TabIndex = 22;
        // 
        // textBoxState
        // 
        this.textBoxState.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxState.Location = new System.Drawing.Point(120, 55);
        this.textBoxState.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxState.MaxLength = 2;
        this.textBoxState.Name = "textBoxState";
        this.textBoxState.Size = new System.Drawing.Size(72, 20);
        this.textBoxState.TabIndex = 21;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(18, 41);
        this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(58, 13);
        this.label2.TabIndex = 20;
        this.label2.Text = "Company";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(18, 21);
        this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(59, 13);
        this.label1.TabIndex = 19;
        this.label1.Text = "Customer";
        // 
        // textBoxCompany
        // 
        this.textBoxCompany.Location = new System.Drawing.Point(120, 36);
        this.textBoxCompany.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxCompany.Name = "textBoxCompany";
        this.textBoxCompany.Size = new System.Drawing.Size(204, 20);
        this.textBoxCompany.TabIndex = 18;
        // 
        // textBoxCustno
        // 
        this.textBoxCustno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxCustno.Location = new System.Drawing.Point(120, 17);
        this.textBoxCustno.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxCustno.MaxLength = 6;
        this.textBoxCustno.Name = "textBoxCustno";
        this.textBoxCustno.Size = new System.Drawing.Size(72, 20);
        this.textBoxCustno.TabIndex = 17;
        // 
        // buttonSearch
        // 
        this.buttonSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonSearch.Location = new System.Drawing.Point(18, 138);
        this.buttonSearch.Margin = new System.Windows.Forms.Padding(2);
        this.buttonSearch.Name = "buttonSearch";
        this.buttonSearch.Size = new System.Drawing.Size(56, 33);
        this.buttonSearch.TabIndex = 27;
        this.buttonSearch.Text = "Search";
        this.buttonSearch.UseVisualStyleBackColor = false;
        // 
        // textBoxBillAddress1
        // 
        this.textBoxBillAddress1.Location = new System.Drawing.Point(329, 93);
        this.textBoxBillAddress1.Multiline = true;
        this.textBoxBillAddress1.Name = "textBoxBillAddress1";
        this.textBoxBillAddress1.ReadOnly = true;
        this.textBoxBillAddress1.Size = new System.Drawing.Size(187, 20);
        this.textBoxBillAddress1.TabIndex = 383;
        // 
        // label126
        // 
        this.label126.AutoSize = true;
        this.label126.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label126.Location = new System.Drawing.Point(335, 52);
        this.label126.Name = "label126";
        this.label126.Size = new System.Drawing.Size(57, 16);
        this.label126.TabIndex = 384;
        this.label126.Text = "Bill To:";
        // 
        // textBoxShiptozip
        // 
        this.textBoxShiptozip.Enabled = false;
        this.textBoxShiptozip.Location = new System.Drawing.Point(579, 150);
        this.textBoxShiptozip.Name = "textBoxShiptozip";
        this.textBoxShiptozip.Size = new System.Drawing.Size(89, 20);
        this.textBoxShiptozip.TabIndex = 410;
        // 
        // textBoxShiptostate
        // 
        this.textBoxShiptostate.Enabled = false;
        this.textBoxShiptostate.Location = new System.Drawing.Point(521, 150);
        this.textBoxShiptostate.Name = "textBoxShiptostate";
        this.textBoxShiptostate.Size = new System.Drawing.Size(54, 20);
        this.textBoxShiptostate.TabIndex = 409;
        // 
        // textBoxShiptocity
        // 
        this.textBoxShiptocity.Enabled = false;
        this.textBoxShiptocity.Location = new System.Drawing.Point(521, 131);
        this.textBoxShiptocity.MaxLength = 45;
        this.textBoxShiptocity.Name = "textBoxShiptocity";
        this.textBoxShiptocity.Size = new System.Drawing.Size(192, 20);
        this.textBoxShiptocity.TabIndex = 408;
        // 
        // textBoxShiptoaddress2
        // 
        this.textBoxShiptoaddress2.Enabled = false;
        this.textBoxShiptoaddress2.Location = new System.Drawing.Point(521, 112);
        this.textBoxShiptoaddress2.MaxLength = 45;
        this.textBoxShiptoaddress2.Name = "textBoxShiptoaddress2";
        this.textBoxShiptoaddress2.Size = new System.Drawing.Size(192, 20);
        this.textBoxShiptoaddress2.TabIndex = 407;
        // 
        // textBoxShiptoaddress1
        // 
        this.textBoxShiptoaddress1.Enabled = false;
        this.textBoxShiptoaddress1.Location = new System.Drawing.Point(521, 93);
        this.textBoxShiptoaddress1.MaxLength = 45;
        this.textBoxShiptoaddress1.Name = "textBoxShiptoaddress1";
        this.textBoxShiptoaddress1.Size = new System.Drawing.Size(192, 20);
        this.textBoxShiptoaddress1.TabIndex = 406;
        // 
        // textBoxShiptocompany
        // 
        this.textBoxShiptocompany.Enabled = false;
        this.textBoxShiptocompany.Location = new System.Drawing.Point(521, 74);
        this.textBoxShiptocompany.MaxLength = 45;
        this.textBoxShiptocompany.Name = "textBoxShiptocompany";
        this.textBoxShiptocompany.Size = new System.Drawing.Size(192, 20);
        this.textBoxShiptocompany.TabIndex = 405;
        // 
        // label114
        // 
        this.label114.AutoSize = true;
        this.label114.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label114.Location = new System.Drawing.Point(523, 54);
        this.label114.Name = "label114";
        this.label114.Size = new System.Drawing.Size(66, 16);
        this.label114.TabIndex = 404;
        this.label114.Text = "Ship To:";
        // 
        // textBoxBillEmail
        // 
        this.textBoxBillEmail.Location = new System.Drawing.Point(773, 131);
        this.textBoxBillEmail.Name = "textBoxBillEmail";
        this.textBoxBillEmail.ReadOnly = true;
        this.textBoxBillEmail.Size = new System.Drawing.Size(181, 20);
        this.textBoxBillEmail.TabIndex = 411;
        // 
        // Label13
        // 
        this.Label13.AutoSize = true;
        this.Label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label13.Location = new System.Drawing.Point(725, 133);
        this.Label13.Name = "Label13";
        this.Label13.Size = new System.Drawing.Size(47, 16);
        this.Label13.TabIndex = 412;
        this.Label13.Text = "Email";
        // 
        // textBoxBillCompany
        // 
        this.textBoxBillCompany.Location = new System.Drawing.Point(329, 74);
        this.textBoxBillCompany.Name = "textBoxBillCompany";
        this.textBoxBillCompany.ReadOnly = true;
        this.textBoxBillCompany.Size = new System.Drawing.Size(187, 20);
        this.textBoxBillCompany.TabIndex = 413;
        // 
        // textBoxBillFaxNo
        // 
        this.textBoxBillFaxNo.Location = new System.Drawing.Point(773, 105);
        this.textBoxBillFaxNo.Name = "textBoxBillFaxNo";
        this.textBoxBillFaxNo.ReadOnly = true;
        this.textBoxBillFaxNo.Size = new System.Drawing.Size(114, 20);
        this.textBoxBillFaxNo.TabIndex = 417;
        // 
        // Label12
        // 
        this.Label12.AutoSize = true;
        this.Label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label12.Location = new System.Drawing.Point(724, 107);
        this.Label12.Name = "Label12";
        this.Label12.Size = new System.Drawing.Size(33, 16);
        this.Label12.TabIndex = 418;
        this.Label12.Text = "Fax";
        // 
        // textBoxBillPhone
        // 
        this.textBoxBillPhone.Location = new System.Drawing.Point(773, 77);
        this.textBoxBillPhone.Name = "textBoxBillPhone";
        this.textBoxBillPhone.ReadOnly = true;
        this.textBoxBillPhone.Size = new System.Drawing.Size(97, 20);
        this.textBoxBillPhone.TabIndex = 415;
        // 
        // Label11
        // 
        this.Label11.AutoSize = true;
        this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Label11.Location = new System.Drawing.Point(723, 81);
        this.Label11.Name = "Label11";
        this.Label11.Size = new System.Drawing.Size(52, 16);
        this.Label11.TabIndex = 416;
        this.Label11.Text = "Phone";
        // 
        // buttonAccept
        // 
        this.buttonAccept.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonAccept.Location = new System.Drawing.Point(149, 138);
        this.buttonAccept.Margin = new System.Windows.Forms.Padding(2);
        this.buttonAccept.Name = "buttonAccept";
        this.buttonAccept.Size = new System.Drawing.Size(67, 33);
        this.buttonAccept.TabIndex = 419;
        this.buttonAccept.Text = "Accept";
        this.buttonAccept.UseVisualStyleBackColor = false;
        // 
        // textBoxBillAddress2
        // 
        this.textBoxBillAddress2.Location = new System.Drawing.Point(329, 112);
        this.textBoxBillAddress2.Multiline = true;
        this.textBoxBillAddress2.Name = "textBoxBillAddress2";
        this.textBoxBillAddress2.ReadOnly = true;
        this.textBoxBillAddress2.Size = new System.Drawing.Size(187, 20);
        this.textBoxBillAddress2.TabIndex = 420;
        // 
        // textBoxBillCity
        // 
        this.textBoxBillCity.Location = new System.Drawing.Point(329, 131);
        this.textBoxBillCity.Multiline = true;
        this.textBoxBillCity.Name = "textBoxBillCity";
        this.textBoxBillCity.ReadOnly = true;
        this.textBoxBillCity.Size = new System.Drawing.Size(187, 20);
        this.textBoxBillCity.TabIndex = 421;
        // 
        // textBoxBillState
        // 
        this.textBoxBillState.Location = new System.Drawing.Point(330, 151);
        this.textBoxBillState.Multiline = true;
        this.textBoxBillState.Name = "textBoxBillState";
        this.textBoxBillState.ReadOnly = true;
        this.textBoxBillState.Size = new System.Drawing.Size(50, 20);
        this.textBoxBillState.TabIndex = 422;
        // 
        // textBoxBillZip
        // 
        this.textBoxBillZip.Location = new System.Drawing.Point(384, 151);
        this.textBoxBillZip.Multiline = true;
        this.textBoxBillZip.Name = "textBoxBillZip";
        this.textBoxBillZip.ReadOnly = true;
        this.textBoxBillZip.Size = new System.Drawing.Size(101, 20);
        this.textBoxBillZip.TabIndex = 423;
        // 
        // FrmGetCustomer
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(976, 397);
        this.Controls.Add(this.textBoxBillZip);
        this.Controls.Add(this.textBoxBillState);
        this.Controls.Add(this.textBoxBillCity);
        this.Controls.Add(this.textBoxBillAddress2);
        this.Controls.Add(this.buttonAccept);
        this.Controls.Add(this.textBoxBillFaxNo);
        this.Controls.Add(this.Label12);
        this.Controls.Add(this.textBoxBillPhone);
        this.Controls.Add(this.Label11);
        this.Controls.Add(this.textBoxBillCompany);
        this.Controls.Add(this.textBoxBillEmail);
        this.Controls.Add(this.Label13);
        this.Controls.Add(this.textBoxShiptozip);
        this.Controls.Add(this.textBoxShiptostate);
        this.Controls.Add(this.textBoxShiptocity);
        this.Controls.Add(this.textBoxShiptoaddress2);
        this.Controls.Add(this.textBoxShiptoaddress1);
        this.Controls.Add(this.textBoxShiptocompany);
        this.Controls.Add(this.label114);
        this.Controls.Add(this.label126);
        this.Controls.Add(this.textBoxBillAddress1);
        this.Controls.Add(this.buttonSearch);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.textBoxPhone);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.textBoxZip);
        this.Controls.Add(this.textBoxState);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBoxCompany);
        this.Controls.Add(this.textBoxCustno);
        this.Controls.Add(this.dataGridViewCustomers);
        this.Controls.Add(this.buttonCancel);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmGetCustomer";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Customer Selector";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCustno;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompany;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddress;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCity;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnState;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnZip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPhone;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    internal System.Windows.Forms.Label label126;
    internal System.Windows.Forms.Label label114;
    internal System.Windows.Forms.Label Label13;
    internal System.Windows.Forms.Label Label12;
    internal System.Windows.Forms.Label Label11;
    public System.Windows.Forms.Button buttonAccept;
    public System.Windows.Forms.Button buttonCancel;
    public System.Windows.Forms.TextBox textBoxPhone;
    public System.Windows.Forms.TextBox textBoxZip;
    public System.Windows.Forms.TextBox textBoxState;
    public System.Windows.Forms.TextBox textBoxCompany;
    public System.Windows.Forms.TextBox textBoxCustno;
    public System.Windows.Forms.Button buttonSearch;
    public System.Windows.Forms.TextBox textBoxBillAddress1;
    public System.Windows.Forms.TextBox textBoxShiptozip;
    public System.Windows.Forms.TextBox textBoxShiptostate;
    public System.Windows.Forms.TextBox textBoxShiptocity;
    public System.Windows.Forms.TextBox textBoxShiptoaddress2;
    public System.Windows.Forms.TextBox textBoxShiptoaddress1;
    public System.Windows.Forms.TextBox textBoxShiptocompany;
    public System.Windows.Forms.TextBox textBoxBillEmail;
    public System.Windows.Forms.TextBox textBoxBillCompany;
    public System.Windows.Forms.TextBox textBoxBillFaxNo;
    public System.Windows.Forms.TextBox textBoxBillPhone;
    public System.Windows.Forms.DataGridView dataGridViewCustomers;
    public System.Windows.Forms.TextBox textBoxBillAddress2;
    public System.Windows.Forms.TextBox textBoxBillCity;
    public System.Windows.Forms.TextBox textBoxBillState;
    public System.Windows.Forms.TextBox textBoxBillZip;
  }
}