namespace Tracking
{
   partial class FrmTrackingQueue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTrackingQueue));
            this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
            this.dataGridViewTrackingQueue = new System.Windows.Forms.DataGridView();
            this.ColumnSono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStepid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSodate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnShipdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPoNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnProctime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnActivityDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonChooseTrackingCode = new WSGUtilitieslib.Telemetry.Button();
            this.buttonChooseWorkGroup = new WSGUtilitieslib.Telemetry.Button();
            this.buttonRefresh = new WSGUtilitieslib.Telemetry.Button();
            this.dataGridViewTrackingActivity = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnActivityComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddedby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddedat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUPSTrack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRoute = new WSGUtilitieslib.Telemetry.Button();
            this.labelComment = new System.Windows.Forms.Label();
            this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
            this.ButtonMainPdf = new WSGUtilitieslib.Telemetry.Button();
            this.buttonSuspendSO = new WSGUtilitieslib.Telemetry.Button();
            this.labelSOCount = new System.Windows.Forms.Label();
            this.buttonPricePdf = new WSGUtilitieslib.Telemetry.Button();
            this.labelInstructions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingActivity)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(461, 3);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(56, 19);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // dataGridViewTrackingQueue
            // 
            this.dataGridViewTrackingQueue.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTrackingQueue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTrackingQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTrackingQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSono,
            this.ColumnStepid,
            this.ColumnSodate,
            this.ColumnShipdate,
            this.ColumnCompany,
            this.ColumnPoNum,
            this.ColumnLname,
            this.ColumnProctime,
            this.ColumnActivityDate,
            this.ColumnLocation});
            this.dataGridViewTrackingQueue.EnableHeadersVisualStyles = false;
            this.dataGridViewTrackingQueue.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridViewTrackingQueue.Location = new System.Drawing.Point(11, 42);
            this.dataGridViewTrackingQueue.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewTrackingQueue.Name = "dataGridViewTrackingQueue";
            this.dataGridViewTrackingQueue.RowHeadersVisible = false;
            this.dataGridViewTrackingQueue.RowTemplate.Height = 24;
            this.dataGridViewTrackingQueue.Size = new System.Drawing.Size(823, 271);
            this.dataGridViewTrackingQueue.TabIndex = 1;
            this.dataGridViewTrackingQueue.Visible = false;
            this.dataGridViewTrackingQueue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTrackingQueue_CellClick);
            this.dataGridViewTrackingQueue.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTrackingQueue_CellFormatting);
            this.dataGridViewTrackingQueue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewTrackingQueue_KeyDown);
            this.dataGridViewTrackingQueue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewTrackingQueue_MouseUp);
            // 
            // ColumnSono
            // 
            this.ColumnSono.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnSono.DataPropertyName = "sono";
            this.ColumnSono.HeaderText = "Sales Order";
            this.ColumnSono.Name = "ColumnSono";
            this.ColumnSono.ReadOnly = true;
            this.ColumnSono.Width = 87;
            // 
            // ColumnStepid
            // 
            this.ColumnStepid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnStepid.DataPropertyName = "stepid";
            this.ColumnStepid.HeaderText = "Step ID";
            this.ColumnStepid.Name = "ColumnStepid";
            this.ColumnStepid.Visible = false;
            // 
            // ColumnSodate
            // 
            this.ColumnSodate.DataPropertyName = "sodate";
            this.ColumnSodate.HeaderText = "Order Date";
            this.ColumnSodate.Name = "ColumnSodate";
            this.ColumnSodate.ReadOnly = true;
            this.ColumnSodate.Width = 70;
            // 
            // ColumnShipdate
            // 
            this.ColumnShipdate.DataPropertyName = "ordate";
            this.ColumnShipdate.HeaderText = "Ship Date";
            this.ColumnShipdate.Name = "ColumnShipdate";
            this.ColumnShipdate.ReadOnly = true;
            this.ColumnShipdate.Width = 70;
            // 
            // ColumnCompany
            // 
            this.ColumnCompany.DataPropertyName = "company";
            this.ColumnCompany.HeaderText = "Dealer";
            this.ColumnCompany.Name = "ColumnCompany";
            this.ColumnCompany.ReadOnly = true;
            // 
            // ColumnPoNum
            // 
            this.ColumnPoNum.DataPropertyName = "ponum";
            this.ColumnPoNum.HeaderText = "PO#";
            this.ColumnPoNum.Name = "ColumnPoNum";
            this.ColumnPoNum.ReadOnly = true;
            this.ColumnPoNum.Width = 150;
            // 
            // ColumnLname
            // 
            this.ColumnLname.DataPropertyName = "lname";
            this.ColumnLname.HeaderText = "Job Name";
            this.ColumnLname.Name = "ColumnLname";
            this.ColumnLname.ReadOnly = true;
            // 
            // ColumnProctime
            // 
            this.ColumnProctime.DataPropertyName = "code";
            this.ColumnProctime.HeaderText = "Code";
            this.ColumnProctime.Name = "ColumnProctime";
            this.ColumnProctime.ReadOnly = true;
            this.ColumnProctime.Width = 50;
            // 
            // ColumnActivityDate
            // 
            this.ColumnActivityDate.DataPropertyName = "trackdate";
            this.ColumnActivityDate.HeaderText = "Tracking Date";
            this.ColumnActivityDate.Name = "ColumnActivityDate";
            this.ColumnActivityDate.ReadOnly = true;
            // 
            // ColumnLocation
            // 
            this.ColumnLocation.DataPropertyName = "location";
            this.ColumnLocation.HeaderText = "Location";
            this.ColumnLocation.Name = "ColumnLocation";
            this.ColumnLocation.Width = 70;
            // 
            // buttonChooseTrackingCode
            // 
            this.buttonChooseTrackingCode.BackColor = System.Drawing.SystemColors.Control;
            this.buttonChooseTrackingCode.Location = new System.Drawing.Point(35, 2);
            this.buttonChooseTrackingCode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonChooseTrackingCode.Name = "buttonChooseTrackingCode";
            this.buttonChooseTrackingCode.Size = new System.Drawing.Size(138, 19);
            this.buttonChooseTrackingCode.TabIndex = 2;
            this.buttonChooseTrackingCode.Text = "Choose Tracking Code";
            this.buttonChooseTrackingCode.UseVisualStyleBackColor = false;
            this.buttonChooseTrackingCode.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // buttonChooseWorkGroup
            // 
            this.buttonChooseWorkGroup.BackColor = System.Drawing.SystemColors.Control;
            this.buttonChooseWorkGroup.Location = new System.Drawing.Point(177, 2);
            this.buttonChooseWorkGroup.Margin = new System.Windows.Forms.Padding(2);
            this.buttonChooseWorkGroup.Name = "buttonChooseWorkGroup";
            this.buttonChooseWorkGroup.Size = new System.Drawing.Size(138, 19);
            this.buttonChooseWorkGroup.TabIndex = 19;
            this.buttonChooseWorkGroup.Text = "Choose Work Group";
            this.buttonChooseWorkGroup.UseVisualStyleBackColor = false;
            this.buttonChooseWorkGroup.Click += new System.EventHandler(this.buttonChooseWorkGroup_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRefresh.Enabled = false;
            this.buttonRefresh.Location = new System.Drawing.Point(319, 2);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(138, 19);
            this.buttonRefresh.TabIndex = 20;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
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
            this.dataGridViewTextBoxColumn1,
            this.ColumnCode,
            this.ColumnDescrip,
            this.ColumnActivityComment,
            this.ColumnAddedby,
            this.ColumnAddedat,
            this.ColumnUPSTrack});
            this.dataGridViewTrackingActivity.EnableHeadersVisualStyles = false;
            this.dataGridViewTrackingActivity.Location = new System.Drawing.Point(5, 365);
            this.dataGridViewTrackingActivity.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewTrackingActivity.Name = "dataGridViewTrackingActivity";
            this.dataGridViewTrackingActivity.RowHeadersVisible = false;
            this.dataGridViewTrackingActivity.RowTemplate.Height = 24;
            this.dataGridViewTrackingActivity.Size = new System.Drawing.Size(765, 188);
            this.dataGridViewTrackingActivity.TabIndex = 21;
            this.dataGridViewTrackingActivity.Visible = false;
            this.dataGridViewTrackingActivity.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTrackingActivity_CellContentDoubleClick);
            this.dataGridViewTrackingActivity.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTrackingActivity_CellFormatting);
            this.dataGridViewTrackingActivity.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridViewTrackingActivity_MouseUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "trackdate";
            this.dataGridViewTextBoxColumn1.HeaderText = "Activity Date";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
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
            // 
            // ColumnActivityComment
            // 
            this.ColumnActivityComment.DataPropertyName = "comment";
            this.ColumnActivityComment.HeaderText = "Comment";
            this.ColumnActivityComment.Name = "ColumnActivityComment";
            this.ColumnActivityComment.ReadOnly = true;
            this.ColumnActivityComment.Width = 225;
            // 
            // ColumnAddedby
            // 
            this.ColumnAddedby.DataPropertyName = "adduser";
            this.ColumnAddedby.HeaderText = "Added By";
            this.ColumnAddedby.Name = "ColumnAddedby";
            this.ColumnAddedby.ReadOnly = true;
            this.ColumnAddedby.Width = 50;
            // 
            // ColumnAddedat
            // 
            this.ColumnAddedat.DataPropertyName = "addtime";
            this.ColumnAddedat.HeaderText = "Added At";
            this.ColumnAddedat.Name = "ColumnAddedat";
            this.ColumnAddedat.ReadOnly = true;
            this.ColumnAddedat.Width = 70;
            // 
            // ColumnUPSTrack
            // 
            this.ColumnUPSTrack.DataPropertyName = "upstrack";
            this.ColumnUPSTrack.HeaderText = "UPS Tracking #";
            this.ColumnUPSTrack.Name = "ColumnUPSTrack";
            this.ColumnUPSTrack.ReadOnly = true;
            this.ColumnUPSTrack.Width = 120;
            // 
            // buttonRoute
            // 
            this.buttonRoute.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRoute.Location = new System.Drawing.Point(560, 1);
            this.buttonRoute.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRoute.Name = "buttonRoute";
            this.buttonRoute.Size = new System.Drawing.Size(138, 20);
            this.buttonRoute.TabIndex = 22;
            this.buttonRoute.Text = "Route to next step";
            this.buttonRoute.UseVisualStyleBackColor = false;
            this.buttonRoute.Visible = false;
            this.buttonRoute.Click += new System.EventHandler(this.buttonRoute_Click);
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelComment.ForeColor = System.Drawing.Color.Firebrick;
            this.labelComment.Location = new System.Drawing.Point(7, 331);
            this.labelComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(500, 16);
            this.labelComment.TabIndex = 24;
            this.labelComment.Text = "Double Click a Step to Enter a Comment - Right Click to Cancel the Step";
            this.labelComment.Visible = false;
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.SystemColors.Control;
            this.buttonClear.Location = new System.Drawing.Point(582, 326);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(73, 35);
            this.buttonClear.TabIndex = 25;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Visible = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // ButtonMainPdf
            // 
            this.ButtonMainPdf.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonMainPdf.Location = new System.Drawing.Point(656, 317);
            this.ButtonMainPdf.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonMainPdf.Name = "ButtonMainPdf";
            this.ButtonMainPdf.Size = new System.Drawing.Size(56, 44);
            this.ButtonMainPdf.TabIndex = 26;
            this.ButtonMainPdf.Text = "Maint PDF";
            this.ButtonMainPdf.UseVisualStyleBackColor = false;
            this.ButtonMainPdf.Visible = false;
            this.ButtonMainPdf.Click += new System.EventHandler(this.buttonMainPdf_Click);
            // 
            // buttonSuspendSO
            // 
            this.buttonSuspendSO.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSuspendSO.Location = new System.Drawing.Point(506, 326);
            this.buttonSuspendSO.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSuspendSO.Name = "buttonSuspendSO";
            this.buttonSuspendSO.Size = new System.Drawing.Size(73, 35);
            this.buttonSuspendSO.TabIndex = 27;
            this.buttonSuspendSO.Text = "Suspend SO";
            this.buttonSuspendSO.UseVisualStyleBackColor = false;
            this.buttonSuspendSO.Visible = false;
            this.buttonSuspendSO.Click += new System.EventHandler(this.buttonSuspendSO_Click);
            // 
            // labelSOCount
            // 
            this.labelSOCount.AutoSize = true;
            this.labelSOCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSOCount.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSOCount.Location = new System.Drawing.Point(7, 26);
            this.labelSOCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSOCount.Name = "labelSOCount";
            this.labelSOCount.Size = new System.Drawing.Size(176, 15);
            this.labelSOCount.TabIndex = 28;
            this.labelSOCount.Text = "Active SO Count goes here";
            this.labelSOCount.Visible = false;
            // 
            // buttonPricePdf
            // 
            this.buttonPricePdf.BackColor = System.Drawing.SystemColors.Control;
            this.buttonPricePdf.Location = new System.Drawing.Point(714, 317);
            this.buttonPricePdf.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPricePdf.Name = "buttonPricePdf";
            this.buttonPricePdf.Size = new System.Drawing.Size(56, 44);
            this.buttonPricePdf.TabIndex = 29;
            this.buttonPricePdf.Text = "Price PDF";
            this.buttonPricePdf.UseVisualStyleBackColor = false;
            this.buttonPricePdf.Visible = false;
            this.buttonPricePdf.Click += new System.EventHandler(this.buttonPricePdf_Click);
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstructions.ForeColor = System.Drawing.Color.Black;
            this.labelInstructions.Location = new System.Drawing.Point(196, 26);
            this.labelInstructions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(358, 15);
            this.labelInstructions.TabIndex = 30;
            this.labelInstructions.Text = "Right Click to process SO. Click to view tracking activity";
            // 
            // FrmTrackingQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(839, 575);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.buttonPricePdf);
            this.Controls.Add(this.labelSOCount);
            this.Controls.Add(this.buttonSuspendSO);
            this.Controls.Add(this.ButtonMainPdf);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.buttonRoute);
            this.Controls.Add(this.dataGridViewTrackingActivity);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonChooseWorkGroup);
            this.Controls.Add(this.buttonChooseTrackingCode);
            this.Controls.Add(this.dataGridViewTrackingQueue);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "FrmTrackingQueue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.Text = "Tracking Queue";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingActivity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.DataGridView dataGridViewTrackingQueue;
      private System.Windows.Forms.Button buttonChooseTrackingCode;
      private System.Windows.Forms.Button buttonChooseWorkGroup;
      private System.Windows.Forms.Button buttonRefresh;
      private System.Windows.Forms.DataGridView dataGridViewTrackingActivity;
      private System.Windows.Forms.Button buttonRoute;
      private System.Windows.Forms.Label labelComment;
      private System.Windows.Forms.Button buttonClear;
      private System.Windows.Forms.Button ButtonMainPdf;
      private System.Windows.Forms.Button buttonSuspendSO;
      private System.Windows.Forms.Label labelSOCount;
      private System.Windows.Forms.Button buttonPricePdf;
      private System.Windows.Forms.Label labelInstructions;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSono;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStepid;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSodate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnShipdate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompany;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPoNum;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLname;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProctime;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnActivityDate;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLocation;
      private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnActivityComment;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddedby;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddedat;
      private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUPSTrack;
   }
}

