namespace CommonAppClasses
{
  partial class FrmSoTrackingActivity
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSoTrackingActivity));
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      this.dataGridViewTrackingActivity = new System.Windows.Forms.DataGridView();
      this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnAddedby = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnUPSTrack = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.labelInstructions = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingActivity)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(613, 12);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(75, 31);
      this.buttonClose.TabIndex = 0;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // dataGridViewTrackingActivity
      // 
      this.dataGridViewTrackingActivity.BackgroundColor = System.Drawing.Color.LightSteelBlue;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewTrackingActivity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridViewTrackingActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewTrackingActivity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDate,
            this.ColumnCode,
            this.ColumnDescrip,
            this.ColumnComment,
            this.ColumnAddedby,
            this.ColumnUPSTrack});
      this.dataGridViewTrackingActivity.EnableHeadersVisualStyles = false;
      this.dataGridViewTrackingActivity.Location = new System.Drawing.Point(8, 48);
      this.dataGridViewTrackingActivity.Margin = new System.Windows.Forms.Padding(2);
      this.dataGridViewTrackingActivity.Name = "dataGridViewTrackingActivity";
      this.dataGridViewTrackingActivity.RowHeadersVisible = false;
      this.dataGridViewTrackingActivity.RowTemplate.Height = 24;
      this.dataGridViewTrackingActivity.Size = new System.Drawing.Size(729, 226);
      this.dataGridViewTrackingActivity.TabIndex = 16;
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
      // labelInstructions
      // 
      this.labelInstructions.AutoSize = true;
      this.labelInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelInstructions.Location = new System.Drawing.Point(151, 22);
      this.labelInstructions.Name = "labelInstructions";
      this.labelInstructions.Size = new System.Drawing.Size(209, 13);
      this.labelInstructions.TabIndex = 17;
      this.labelInstructions.Text = "Double Click Step To Add Comment";
      // 
      // FrmSoTrackingActivity
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(780, 285);
      this.Controls.Add(this.labelInstructions);
      this.Controls.Add(this.dataGridViewTrackingActivity);
      this.Controls.Add(this.buttonClose);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FrmSoTrackingActivity";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SO Tracking Activity";
      this.Shown += new System.EventHandler(this.FrmSoTrackingActivity_Shown);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTrackingActivity)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.DataGridView dataGridViewTrackingActivity;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComment;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddedby;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUPSTrack;
    private System.Windows.Forms.Label labelInstructions;
  }
}