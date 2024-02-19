namespace CommonAppClasses
{
   partial class FrmSOSearch
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSOSearch));
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
          this.textBoxFirstSoDate = new System.Windows.Forms.TextBox();
          this.label5 = new System.Windows.Forms.Label();
          this.dataGridviewSoSearch = new System.Windows.Forms.DataGridView();
          this.ColumnSono = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.ColumnCustno = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.ColumnSodate = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.ColumnOrdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.ColumnCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.ColumnPonum = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.ColumnLname = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.ColumnMeycono = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.dateTimePickerFirstSoDate = new System.Windows.Forms.DateTimePicker();
          this.panel1 = new System.Windows.Forms.Panel();
          this.label6 = new System.Windows.Forms.Label();
          this.listBoxInclude = new System.Windows.Forms.ListBox();
          this.dateTimePickerLastSoDate = new System.Windows.Forms.DateTimePicker();
          this.labelInclude = new System.Windows.Forms.Label();
          this.textBoxLastSoDate = new System.Windows.Forms.TextBox();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridviewSoSearch)).BeginInit();
          this.panel1.SuspendLayout();
          this.SuspendLayout();
          // 
          // buttonCancel
          // 
          this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
          this.buttonCancel.Location = new System.Drawing.Point(742, 43);
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
          this.buttonSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
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
          this.labelSono.Size = new System.Drawing.Size(120, 13);
          this.labelSono.TabIndex = 2;
          this.labelSono.Text = "SO #";
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
          this.textBoxCustno.Location = new System.Drawing.Point(142, 15);
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
          this.label2.Location = new System.Drawing.Point(139, 1);
          this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(71, 13);
          this.label2.TabIndex = 6;
          this.label2.Text = "Customer #";
          // 
          // textBoxLname
          // 
          this.textBoxLname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
          this.textBoxLname.Location = new System.Drawing.Point(142, 52);
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
          this.label3.Location = new System.Drawing.Point(139, 37);
          this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(67, 13);
          this.label3.TabIndex = 8;
          this.label3.Text = "Last Name";
          // 
          // textBoxPonum
          // 
          this.textBoxPonum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
          this.textBoxPonum.Location = new System.Drawing.Point(248, 17);
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
          this.label4.Location = new System.Drawing.Point(245, 1);
          this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(71, 13);
          this.label4.TabIndex = 10;
          this.label4.Text = "PO Number";
          // 
          // textBoxFirstSoDate
          // 
          this.textBoxFirstSoDate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
          this.textBoxFirstSoDate.Location = new System.Drawing.Point(570, 42);
          this.textBoxFirstSoDate.Margin = new System.Windows.Forms.Padding(2);
          this.textBoxFirstSoDate.Name = "textBoxFirstSoDate";
          this.textBoxFirstSoDate.ReadOnly = true;
          this.textBoxFirstSoDate.Size = new System.Drawing.Size(64, 20);
          this.textBoxFirstSoDate.TabIndex = 13;
          this.textBoxFirstSoDate.Text = " ";
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
          // dataGridviewSoSearch
          // 
          this.dataGridviewSoSearch.BackgroundColor = System.Drawing.Color.LightSteelBlue;
          dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dataGridviewSoSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.dataGridviewSoSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSono,
            this.ColumnCustno,
            this.ColumnSodate,
            this.ColumnOrdate,
            this.ColumnCompany,
            this.ColumnPonum,
            this.ColumnLname,
            this.ColumnMeycono});
          this.dataGridviewSoSearch.EnableHeadersVisualStyles = false;
          this.dataGridviewSoSearch.GridColor = System.Drawing.Color.SteelBlue;
          this.dataGridviewSoSearch.Location = new System.Drawing.Point(13, 104);
          this.dataGridviewSoSearch.Margin = new System.Windows.Forms.Padding(2);
          this.dataGridviewSoSearch.Name = "dataGridviewSoSearch";
          this.dataGridviewSoSearch.RowHeadersVisible = false;
          this.dataGridviewSoSearch.RowTemplate.Height = 24;
          this.dataGridviewSoSearch.Size = new System.Drawing.Size(838, 281);
          this.dataGridviewSoSearch.TabIndex = 14;
          this.dataGridviewSoSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridviewSoSearch_CellContentDoubleClick);
          this.dataGridviewSoSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTrackingSearch_CellClick);
          this.dataGridviewSoSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridviewSoSearch_KeyDown);
          // 
          // ColumnSono
          // 
          this.ColumnSono.DataPropertyName = "sono";
          this.ColumnSono.HeaderText = "SO #";
          this.ColumnSono.Name = "ColumnSono";
          this.ColumnSono.ReadOnly = true;
          this.ColumnSono.Width = 80;
          // 
          // ColumnCustno
          // 
          this.ColumnCustno.DataPropertyName = "custno";
          this.ColumnCustno.HeaderText = "Custno";
          this.ColumnCustno.Name = "ColumnCustno";
          this.ColumnCustno.Width = 80;
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
          this.ColumnCompany.Width = 200;
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
          // ColumnMeycono
          // 
          this.ColumnMeycono.DataPropertyName = "meycono";
          this.ColumnMeycono.HeaderText = "Meyco Number";
          this.ColumnMeycono.Name = "ColumnMeycono";
          this.ColumnMeycono.ReadOnly = true;
          this.ColumnMeycono.Width = 80;
          // 
          // dateTimePickerFirstSoDate
          // 
          this.dateTimePickerFirstSoDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
          this.dateTimePickerFirstSoDate.Location = new System.Drawing.Point(561, 19);
          this.dateTimePickerFirstSoDate.Margin = new System.Windows.Forms.Padding(2);
          this.dateTimePickerFirstSoDate.Name = "dateTimePickerFirstSoDate";
          this.dateTimePickerFirstSoDate.Size = new System.Drawing.Size(73, 20);
          this.dateTimePickerFirstSoDate.TabIndex = 16;
          this.dateTimePickerFirstSoDate.ValueChanged += new System.EventHandler(this.dateTimePickerFirstSoDate_ValueChanged);
          // 
          // panel1
          // 
          this.panel1.Controls.Add(this.label6);
          this.panel1.Controls.Add(this.listBoxInclude);
          this.panel1.Controls.Add(this.dateTimePickerLastSoDate);
          this.panel1.Controls.Add(this.labelInclude);
          this.panel1.Controls.Add(this.textBoxLastSoDate);
          this.panel1.Controls.Add(this.dateTimePickerFirstSoDate);
          this.panel1.Controls.Add(this.textBoxFirstSoDate);
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
          // label6
          // 
          this.label6.AutoSize = true;
          this.label6.Location = new System.Drawing.Point(639, 37);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(16, 13);
          this.label6.TabIndex = 21;
          this.label6.Text = "to";
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
          this.listBoxInclude.Location = new System.Drawing.Point(402, 2);
          this.listBoxInclude.Margin = new System.Windows.Forms.Padding(2);
          this.listBoxInclude.Name = "listBoxInclude";
          this.listBoxInclude.Size = new System.Drawing.Size(131, 68);
          this.listBoxInclude.TabIndex = 20;
          this.listBoxInclude.SelectedIndexChanged += new System.EventHandler(this.listBoxInclude_SelectedIndexChanged);
          // 
          // dateTimePickerLastSoDate
          // 
          this.dateTimePickerLastSoDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
          this.dateTimePickerLastSoDate.Location = new System.Drawing.Point(654, 20);
          this.dateTimePickerLastSoDate.Margin = new System.Windows.Forms.Padding(2);
          this.dateTimePickerLastSoDate.Name = "dateTimePickerLastSoDate";
          this.dateTimePickerLastSoDate.Size = new System.Drawing.Size(73, 20);
          this.dateTimePickerLastSoDate.TabIndex = 18;
          this.dateTimePickerLastSoDate.ValueChanged += new System.EventHandler(this.dateTimePickerLastSoDate_ValueChanged);
          // 
          // labelInclude
          // 
          this.labelInclude.AutoSize = true;
          this.labelInclude.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.labelInclude.Location = new System.Drawing.Point(349, 1);
          this.labelInclude.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
          this.labelInclude.Name = "labelInclude";
          this.labelInclude.Size = new System.Drawing.Size(49, 13);
          this.labelInclude.TabIndex = 19;
          this.labelInclude.Text = "Include";
          // 
          // textBoxLastSoDate
          // 
          this.textBoxLastSoDate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
          this.textBoxLastSoDate.Location = new System.Drawing.Point(660, 43);
          this.textBoxLastSoDate.Margin = new System.Windows.Forms.Padding(2);
          this.textBoxLastSoDate.Name = "textBoxLastSoDate";
          this.textBoxLastSoDate.ReadOnly = true;
          this.textBoxLastSoDate.Size = new System.Drawing.Size(67, 20);
          this.textBoxLastSoDate.TabIndex = 17;
          this.textBoxLastSoDate.Text = " ";
          this.textBoxLastSoDate.DoubleClick += new System.EventHandler(this.textBoxShipLastDate_DoubleClick);
          // 
          // FrmSOSearch
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
          this.BackColor = System.Drawing.Color.LightSteelBlue;
          this.ClientSize = new System.Drawing.Size(875, 440);
          this.Controls.Add(this.panel1);
          this.Controls.Add(this.dataGridviewSoSearch);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.Margin = new System.Windows.Forms.Padding(3);
          this.Name = "FrmSOSearch";
          this.Text = "SO Search";
          ((System.ComponentModel.ISupportInitialize)(this.dataGridviewSoSearch)).EndInit();
          this.panel1.ResumeLayout(false);
          this.panel1.PerformLayout();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonSearch;
      private System.Windows.Forms.Label labelSono;
      private System.Windows.Forms.TextBox textBoxSono;
      private System.Windows.Forms.TextBox textBoxMeycono;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox textBoxCustno;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox textBoxLname;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox textBoxPonum;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox textBoxFirstSoDate;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.DataGridView dataGridviewSoSearch;
      private System.Windows.Forms.DateTimePicker dateTimePickerFirstSoDate;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.DateTimePicker dateTimePickerLastSoDate;
      private System.Windows.Forms.TextBox textBoxLastSoDate;
      private System.Windows.Forms.ListBox listBoxInclude;
      private System.Windows.Forms.Label labelInclude;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSono;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCustno;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSodate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOrdate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompany;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPonum;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLname;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeycono;
   }
}

