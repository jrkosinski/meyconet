namespace Tracking
{
   partial class FrmTrackingSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTrackingSearch));
            this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
            this.buttonSearch = new WSGUtilitieslib.Telemetry.Button();
            this.labelSono = new System.Windows.Forms.Label();
            this.textBoxSono = new System.Windows.Forms.TextBox();
            this.textBoxMeycono = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCustno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPonum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxShipFirstDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewTrackingSearch = new System.Windows.Forms.DataGridView();
            this.ColumnSono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSodate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOrdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCustno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPonum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTrakcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMeycono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnProcdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTrackingActivity = new System.Windows.Forms.DataGridView();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddedby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUPSTrack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePickerShipFirstDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxInvno = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonPricePdf = new WSGUtilitieslib.Telemetry.Button();
            this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
            this.buttonMainPdf = new WSGUtilitieslib.Telemetry.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonSuspendSO = new WSGUtilitieslib.Telemetry.Button();
            this.listBoxInclude = new System.Windows.Forms.ListBox();
            this.dateTimePickerShipLastDate = new System.Windows.Forms.DateTimePicker();
            this.labelInclude = new System.Windows.Forms.Label();
            this.textBoxShipLastDate = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.labelInstructions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingActivity)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.AcceptButton = this.buttonSearch;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(742, 34);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(56, 19);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSearch.Location = new System.Drawing.Point(742, 12);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(56, 19);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // labelSono
            // 
            this.labelSono.AutoSize = true;
            this.labelSono.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSono.Location = new System.Drawing.Point(-1, 1);
            this.labelSono.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSono.Name = "labelSono";
            this.labelSono.Size = new System.Drawing.Size(85, 13);
            this.labelSono.TabIndex = 2;
            this.labelSono.Text = "Sales Order #";
            // 
            // textBoxSono
            // 
            this.textBoxSono.Location = new System.Drawing.Point(2, 15);
            this.textBoxSono.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSono.Name = "textBoxSono";
            this.textBoxSono.Size = new System.Drawing.Size(76, 20);
            this.textBoxSono.TabIndex = 3;
            // 
            // textBoxMeycono
            // 
            this.textBoxMeycono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxMeycono.Location = new System.Drawing.Point(0, 52);
            this.textBoxMeycono.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMeycono.Name = "textBoxMeycono";
            this.textBoxMeycono.Size = new System.Drawing.Size(76, 20);
            this.textBoxMeycono.TabIndex = 5;
            this.textBoxMeycono.Text = " ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-3, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Plan Number";
            // 
            // textBoxCustno
            // 
            this.textBoxCustno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxCustno.Location = new System.Drawing.Point(97, 15);
            this.textBoxCustno.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCustno.Name = "textBoxCustno";
            this.textBoxCustno.Size = new System.Drawing.Size(76, 20);
            this.textBoxCustno.TabIndex = 7;
            this.textBoxCustno.Text = " ";
            this.textBoxCustno.Leave += new System.EventHandler(this.textBoxCustno_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Customer #";
            // 
            // textBoxLname
            // 
            this.textBoxLname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxLname.Location = new System.Drawing.Point(97, 52);
            this.textBoxLname.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLname.Name = "textBoxLname";
            this.textBoxLname.Size = new System.Drawing.Size(127, 20);
            this.textBoxLname.TabIndex = 9;
            this.textBoxLname.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(94, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Last Name";
            // 
            // textBoxPonum
            // 
            this.textBoxPonum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPonum.Location = new System.Drawing.Point(185, 17);
            this.textBoxPonum.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPonum.Name = "textBoxPonum";
            this.textBoxPonum.Size = new System.Drawing.Size(88, 20);
            this.textBoxPonum.TabIndex = 11;
            this.textBoxPonum.Text = " ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(182, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "PO Number";
            // 
            // textBoxShipFirstDate
            // 
            this.textBoxShipFirstDate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.textBoxShipFirstDate.Location = new System.Drawing.Point(570, 42);
            this.textBoxShipFirstDate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxShipFirstDate.Name = "textBoxShipFirstDate";
            this.textBoxShipFirstDate.ReadOnly = true;
            this.textBoxShipFirstDate.Size = new System.Drawing.Size(64, 20);
            this.textBoxShipFirstDate.TabIndex = 13;
            this.textBoxShipFirstDate.Text = " ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(594, 4);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Ship Date Range";
            // 
            // dataGridViewTrackingSearch
            // 
            this.dataGridViewTrackingSearch.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTrackingSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTrackingSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTrackingSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSono,
            this.ColumnSodate,
            this.ColumnOrdate,
            this.ColumnCompany,
            this.ColumnCustno,
            this.ColumnPonum,
            this.ColumnLname,
            this.ColumnTrakcode,
            this.ColumnMeycono,
            this.ColumnProcdate,
            this.ColumnLocation});
            this.dataGridViewTrackingSearch.Enabled = false;
            this.dataGridViewTrackingSearch.EnableHeadersVisualStyles = false;
            this.dataGridViewTrackingSearch.Location = new System.Drawing.Point(-2, 113);
            this.dataGridViewTrackingSearch.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewTrackingSearch.Name = "dataGridViewTrackingSearch";
            this.dataGridViewTrackingSearch.RowHeadersVisible = false;
            this.dataGridViewTrackingSearch.RowTemplate.Height = 24;
            this.dataGridViewTrackingSearch.Size = new System.Drawing.Size(917, 221);
            this.dataGridViewTrackingSearch.TabIndex = 14;
            this.dataGridViewTrackingSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTrackingSearch_CellClick);
            this.dataGridViewTrackingSearch.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTrackingSearch_CellFormatting);
            this.dataGridViewTrackingSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewTrackingSearch_MouseUp);
            // 
            // ColumnSono
            // 
            this.ColumnSono.DataPropertyName = "sono";
            this.ColumnSono.HeaderText = "SO #";
            this.ColumnSono.Name = "ColumnSono";
            this.ColumnSono.ReadOnly = true;
            this.ColumnSono.Width = 80;
            // 
            // ColumnSodate
            // 
            this.ColumnSodate.DataPropertyName = "sodate";
            this.ColumnSodate.HeaderText = "SO Date";
            this.ColumnSodate.Name = "ColumnSodate";
            this.ColumnSodate.ReadOnly = true;
            this.ColumnSodate.Width = 70;
            // 
            // ColumnOrdate
            // 
            this.ColumnOrdate.DataPropertyName = "ordate";
            this.ColumnOrdate.HeaderText = "Ship Date";
            this.ColumnOrdate.Name = "ColumnOrdate";
            this.ColumnOrdate.ReadOnly = true;
            this.ColumnOrdate.Width = 70;
            // 
            // ColumnCompany
            // 
            this.ColumnCompany.DataPropertyName = "company";
            this.ColumnCompany.HeaderText = "Dealer";
            this.ColumnCompany.Name = "ColumnCompany";
            this.ColumnCompany.ReadOnly = true;
            // 
            // ColumnCustno
            // 
            this.ColumnCustno.DataPropertyName = "custno";
            this.ColumnCustno.HeaderText = "Custno";
            this.ColumnCustno.Name = "ColumnCustno";
            this.ColumnCustno.Width = 80;
            // 
            // ColumnPonum
            // 
            this.ColumnPonum.DataPropertyName = "ponum";
            this.ColumnPonum.HeaderText = "PO #";
            this.ColumnPonum.Name = "ColumnPonum";
            this.ColumnPonum.ReadOnly = true;
            this.ColumnPonum.Width = 125;
            // 
            // ColumnLname
            // 
            this.ColumnLname.DataPropertyName = "lname";
            this.ColumnLname.HeaderText = "Last Name";
            this.ColumnLname.Name = "ColumnLname";
            this.ColumnLname.ReadOnly = true;
            // 
            // ColumnTrakcode
            // 
            this.ColumnTrakcode.DataPropertyName = "code";
            this.ColumnTrakcode.FillWeight = 75F;
            this.ColumnTrakcode.HeaderText = "Tracking Code";
            this.ColumnTrakcode.Name = "ColumnTrakcode";
            this.ColumnTrakcode.Width = 50;
            // 
            // ColumnMeycono
            // 
            this.ColumnMeycono.DataPropertyName = "meycono";
            this.ColumnMeycono.HeaderText = "Meyco Number";
            this.ColumnMeycono.Name = "ColumnMeycono";
            this.ColumnMeycono.ReadOnly = true;
            this.ColumnMeycono.Width = 80;
            // 
            // ColumnProcdate
            // 
            this.ColumnProcdate.DataPropertyName = "procdate";
            this.ColumnProcdate.HeaderText = "Last Tracking Date";
            this.ColumnProcdate.Name = "ColumnProcdate";
            this.ColumnProcdate.Width = 70;
            // 
            // ColumnLocation
            // 
            this.ColumnLocation.DataPropertyName = "location";
            this.ColumnLocation.HeaderText = "Location";
            this.ColumnLocation.Name = "ColumnLocation";
            this.ColumnLocation.Width = 70;
            // 
            // dataGridViewTrackingActivity
            // 
            this.dataGridViewTrackingActivity.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTrackingActivity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTrackingActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTrackingActivity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDate,
            this.ColumnCode,
            this.ColumnDescrip,
            this.ColumnComment,
            this.ColumnAddedby,
            this.ColumnUPSTrack});
            this.dataGridViewTrackingActivity.EnableHeadersVisualStyles = false;
            this.dataGridViewTrackingActivity.Location = new System.Drawing.Point(11, 364);
            this.dataGridViewTrackingActivity.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewTrackingActivity.Name = "dataGridViewTrackingActivity";
            this.dataGridViewTrackingActivity.RowHeadersVisible = false;
            this.dataGridViewTrackingActivity.RowTemplate.Height = 24;
            this.dataGridViewTrackingActivity.Size = new System.Drawing.Size(759, 157);
            this.dataGridViewTrackingActivity.TabIndex = 15;
            this.dataGridViewTrackingActivity.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTrackingActivity_CellContentDoubleClick);
            this.dataGridViewTrackingActivity.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTrackingActivity_CellFormatting);
            // 
            // ColumnDate
            // 
            this.ColumnDate.DataPropertyName = "trackdate";
            this.ColumnDate.HeaderText = "Activity Date";
            this.ColumnDate.Name = "ColumnDate";
            this.ColumnDate.ReadOnly = true;
            // 
            // ColumnCode
            // 
            this.ColumnCode.DataPropertyName = "code";
            this.ColumnCode.HeaderText = "Code";
            this.ColumnCode.Name = "ColumnCode";
            this.ColumnCode.ReadOnly = true;
            this.ColumnCode.Width = 50;
            // 
            // ColumnDescrip
            // 
            this.ColumnDescrip.DataPropertyName = "descrip";
            this.ColumnDescrip.HeaderText = "Description";
            this.ColumnDescrip.Name = "ColumnDescrip";
            this.ColumnDescrip.ReadOnly = true;
            this.ColumnDescrip.Width = 200;
            // 
            // ColumnComment
            // 
            this.ColumnComment.DataPropertyName = "comment";
            this.ColumnComment.HeaderText = "Comment";
            this.ColumnComment.Name = "ColumnComment";
            this.ColumnComment.ReadOnly = true;
            this.ColumnComment.Width = 175;
            // 
            // ColumnAddedby
            // 
            this.ColumnAddedby.DataPropertyName = "adduser";
            this.ColumnAddedby.HeaderText = "Added By";
            this.ColumnAddedby.Name = "ColumnAddedby";
            this.ColumnAddedby.ReadOnly = true;
            // 
            // ColumnUPSTrack
            // 
            this.ColumnUPSTrack.DataPropertyName = "upstrack";
            this.ColumnUPSTrack.HeaderText = "UPS Tracking #";
            this.ColumnUPSTrack.Name = "ColumnUPSTrack";
            this.ColumnUPSTrack.ReadOnly = true;
            // 
            // dateTimePickerShipFirstDate
            // 
            this.dateTimePickerShipFirstDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerShipFirstDate.Location = new System.Drawing.Point(561, 19);
            this.dateTimePickerShipFirstDate.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerShipFirstDate.Name = "dateTimePickerShipFirstDate";
            this.dateTimePickerShipFirstDate.Size = new System.Drawing.Size(73, 20);
            this.dateTimePickerShipFirstDate.TabIndex = 16;
            this.dateTimePickerShipFirstDate.ValueChanged += new System.EventHandler(this.dateTimePickerSoDate_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxInvno);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.buttonPricePdf);
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.buttonMainPdf);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.buttonSuspendSO);
            this.panel1.Controls.Add(this.listBoxInclude);
            this.panel1.Controls.Add(this.dateTimePickerShipLastDate);
            this.panel1.Controls.Add(this.labelInclude);
            this.panel1.Controls.Add(this.textBoxShipLastDate);
            this.panel1.Controls.Add(this.dateTimePickerShipFirstDate);
            this.panel1.Controls.Add(this.textBoxShipFirstDate);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonSearch);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBoxPonum);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBoxLname);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxCustno);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxMeycono);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxSono);
            this.panel1.Controls.Add(this.labelSono);
            this.panel1.Location = new System.Drawing.Point(11, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 78);
            this.panel1.TabIndex = 17;
            // 
            // textBoxInvno
            // 
            this.textBoxInvno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxInvno.Location = new System.Drawing.Point(279, 17);
            this.textBoxInvno.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxInvno.Name = "textBoxInvno";
            this.textBoxInvno.Size = new System.Drawing.Size(88, 20);
            this.textBoxInvno.TabIndex = 24;
            this.textBoxInvno.Text = " ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(276, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Invoice #";
            // 
            // buttonPricePdf
            // 
            this.buttonPricePdf.BackColor = System.Drawing.SystemColors.Control;
            this.buttonPricePdf.Location = new System.Drawing.Point(493, 43);
            this.buttonPricePdf.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPricePdf.Name = "buttonPricePdf";
            this.buttonPricePdf.Size = new System.Drawing.Size(56, 35);
            this.buttonPricePdf.TabIndex = 22;
            this.buttonPricePdf.Text = "Price Pdf";
            this.buttonPricePdf.UseVisualStyleBackColor = false;
            this.buttonPricePdf.Visible = false;
            this.buttonPricePdf.Click += new System.EventHandler(this.buttonPricePdf_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.SystemColors.Control;
            this.buttonClear.Location = new System.Drawing.Point(742, 55);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(56, 19);
            this.buttonClear.TabIndex = 18;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonMainPdf
            // 
            this.buttonMainPdf.BackColor = System.Drawing.SystemColors.Control;
            this.buttonMainPdf.Location = new System.Drawing.Point(433, 43);
            this.buttonMainPdf.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMainPdf.Name = "buttonMainPdf";
            this.buttonMainPdf.Size = new System.Drawing.Size(56, 35);
            this.buttonMainPdf.TabIndex = 20;
            this.buttonMainPdf.Text = "Main Pdf";
            this.buttonMainPdf.UseVisualStyleBackColor = false;
            this.buttonMainPdf.Visible = false;
            this.buttonMainPdf.Click += new System.EventHandler(this.buttonMainPdf_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(639, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "to";
            // 
            // buttonSuspendSO
            // 
            this.buttonSuspendSO.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSuspendSO.Location = new System.Drawing.Point(291, 59);
            this.buttonSuspendSO.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSuspendSO.Name = "buttonSuspendSO";
            this.buttonSuspendSO.Size = new System.Drawing.Size(138, 19);
            this.buttonSuspendSO.TabIndex = 19;
            this.buttonSuspendSO.Text = "Suspend Sales Order";
            this.buttonSuspendSO.UseVisualStyleBackColor = false;
            this.buttonSuspendSO.Visible = false;
            this.buttonSuspendSO.Click += new System.EventHandler(this.buttonSuspendSO_Click);
            // 
            // listBoxInclude
            // 
            this.listBoxInclude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxInclude.FormattingEnabled = true;
            this.listBoxInclude.ItemHeight = 16;
            this.listBoxInclude.Items.AddRange(new object[] {
            "All",
            "Estimates Only",
            "Orders Only"});
            this.listBoxInclude.Location = new System.Drawing.Point(425, 2);
            this.listBoxInclude.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxInclude.Name = "listBoxInclude";
            this.listBoxInclude.Size = new System.Drawing.Size(131, 36);
            this.listBoxInclude.TabIndex = 20;
            this.listBoxInclude.SelectedIndexChanged += new System.EventHandler(this.listBoxInclude_SelectedIndexChanged);
            // 
            // dateTimePickerShipLastDate
            // 
            this.dateTimePickerShipLastDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerShipLastDate.Location = new System.Drawing.Point(654, 20);
            this.dateTimePickerShipLastDate.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerShipLastDate.Name = "dateTimePickerShipLastDate";
            this.dateTimePickerShipLastDate.Size = new System.Drawing.Size(73, 20);
            this.dateTimePickerShipLastDate.TabIndex = 18;
            this.dateTimePickerShipLastDate.ValueChanged += new System.EventHandler(this.dateTimePickerShipLastDate_ValueChanged);
            // 
            // labelInclude
            // 
            this.labelInclude.AutoSize = true;
            this.labelInclude.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInclude.Location = new System.Drawing.Point(372, 1);
            this.labelInclude.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInclude.Name = "labelInclude";
            this.labelInclude.Size = new System.Drawing.Size(49, 13);
            this.labelInclude.TabIndex = 19;
            this.labelInclude.Text = "Include";
            // 
            // textBoxShipLastDate
            // 
            this.textBoxShipLastDate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.textBoxShipLastDate.Location = new System.Drawing.Point(660, 43);
            this.textBoxShipLastDate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxShipLastDate.Name = "textBoxShipLastDate";
            this.textBoxShipLastDate.ReadOnly = true;
            this.textBoxShipLastDate.Size = new System.Drawing.Size(67, 20);
            this.textBoxShipLastDate.TabIndex = 17;
            this.textBoxShipLastDate.Text = " ";
            this.textBoxShipLastDate.DoubleClick += new System.EventHandler(this.textBoxShipLastDate_DoubleClick);
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComment.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelComment.Location = new System.Drawing.Point(21, 349);
            this.labelComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(298, 17);
            this.labelComment.TabIndex = 18;
            this.labelComment.Text = "Double Click a Step to Enter a Comment";
            this.labelComment.Visible = false;
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstructions.ForeColor = System.Drawing.Color.Black;
            this.labelInstructions.Location = new System.Drawing.Point(261, 96);
            this.labelInstructions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(358, 15);
            this.labelInstructions.TabIndex = 31;
            this.labelInstructions.Text = "Right Click to process SO. Click to view tracking activity";
            // 
            // FrmTrackingSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(926, 532);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridViewTrackingActivity);
            this.Controls.Add(this.dataGridViewTrackingSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "FrmTrackingSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.Text = "Tracking Search";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingActivity)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonSearch;
      private System.Windows.Forms.Label labelSono;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.DataGridView dataGridViewTrackingSearch;
      private System.Windows.Forms.DataGridView dataGridViewTrackingActivity;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComment;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddedby;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUPSTrack;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button buttonClear;
      private System.Windows.Forms.ListBox listBoxInclude;
      private System.Windows.Forms.Label labelInclude;
      private System.Windows.Forms.Button buttonSuspendSO;
      private System.Windows.Forms.Button buttonMainPdf;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label labelComment;
      private System.Windows.Forms.Button buttonPricePdf;
      private System.Windows.Forms.Label label7;
      public System.Windows.Forms.TextBox textBoxSono;
      public System.Windows.Forms.TextBox textBoxMeycono;
      public System.Windows.Forms.TextBox textBoxCustno;
      public System.Windows.Forms.TextBox textBoxLname;
      public System.Windows.Forms.TextBox textBoxPonum;
      public System.Windows.Forms.TextBox textBoxShipFirstDate;
      public System.Windows.Forms.DateTimePicker dateTimePickerShipFirstDate;
      public System.Windows.Forms.DateTimePicker dateTimePickerShipLastDate;
      public System.Windows.Forms.TextBox textBoxShipLastDate;
      public System.Windows.Forms.TextBox textBoxInvno;
      private System.Windows.Forms.Label labelInstructions;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSono;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSodate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOrdate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompany;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCustno;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPonum;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLname;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTrakcode;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeycono;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProcdate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLocation;
   }
}

