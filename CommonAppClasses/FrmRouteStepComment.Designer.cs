namespace CommonAppClasses
{
  partial class FrmRouteStepComment
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRouteStepComment));
      this.dataGridViewRouteData = new System.Windows.Forms.DataGridView();
      this.ColumnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnStepId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
      this.textBoxComment = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.dateTimePickerTrackdate = new System.Windows.Forms.DateTimePicker();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRouteData)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridViewRouteData
      // 
      this.dataGridViewRouteData.AllowUserToOrderColumns = true;
      this.dataGridViewRouteData.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewRouteData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridViewRouteData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewRouteData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCode,
            this.ColumnDescrip,
            this.ColumnStepId});
      this.dataGridViewRouteData.EnableHeadersVisualStyles = false;
      this.dataGridViewRouteData.Location = new System.Drawing.Point(28, 43);
      this.dataGridViewRouteData.Margin = new System.Windows.Forms.Padding(2);
      this.dataGridViewRouteData.Name = "dataGridViewRouteData";
      this.dataGridViewRouteData.RowHeadersVisible = false;
      this.dataGridViewRouteData.RowTemplate.Height = 24;
      this.dataGridViewRouteData.Size = new System.Drawing.Size(285, 198);
      this.dataGridViewRouteData.TabIndex = 34;
      this.dataGridViewRouteData.Visible = false;
      this.dataGridViewRouteData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewRouteData_KeyDown);
      this.dataGridViewRouteData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRouteData_CellContentClick);
      // 
      // ColumnCode
      // 
      this.ColumnCode.DataPropertyName = "code";
      this.ColumnCode.HeaderText = "Code";
      this.ColumnCode.Name = "ColumnCode";
      this.ColumnCode.ReadOnly = true;
      this.ColumnCode.Width = 75;
      // 
      // ColumnDescrip
      // 
      this.ColumnDescrip.DataPropertyName = "descrip";
      this.ColumnDescrip.HeaderText = "Description";
      this.ColumnDescrip.Name = "ColumnDescrip";
      this.ColumnDescrip.ReadOnly = true;
      this.ColumnDescrip.Width = 300;
      // 
      // ColumnStepId
      // 
      this.ColumnStepId.HeaderText = "Step ID";
      this.ColumnStepId.Name = "ColumnStepId";
      this.ColumnStepId.ReadOnly = true;
      this.ColumnStepId.Visible = false;
      // 
      // buttonCancel
      // 
      this.buttonCancel.Location = new System.Drawing.Point(227, 11);
      this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(56, 28);
      this.buttonCancel.TabIndex = 36;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // textBoxComment
      // 
      this.textBoxComment.Location = new System.Drawing.Point(27, 347);
      this.textBoxComment.Margin = new System.Windows.Forms.Padding(2);
      this.textBoxComment.Multiline = true;
      this.textBoxComment.Name = "textBoxComment";
      this.textBoxComment.Size = new System.Drawing.Size(286, 64);
      this.textBoxComment.TabIndex = 37;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.Red;
      this.label2.Location = new System.Drawing.Point(88, 291);
      this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(151, 17);
      this.label2.TabIndex = 39;
      this.label2.Text = "Enter Comment And";
      // 
      // dateTimePickerTrackdate
      // 
      this.dateTimePickerTrackdate.Location = new System.Drawing.Point(82, 265);
      this.dateTimePickerTrackdate.Margin = new System.Windows.Forms.Padding(2);
      this.dateTimePickerTrackdate.Name = "dateTimePickerTrackdate";
      this.dateTimePickerTrackdate.Size = new System.Drawing.Size(185, 20);
      this.dateTimePickerTrackdate.TabIndex = 401;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.Red;
      this.label1.Location = new System.Drawing.Point(47, 325);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(236, 17);
      this.label1.TabIndex = 402;
      this.label1.Text = "Before Selecting The Next Step";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.Red;
      this.label3.Location = new System.Drawing.Point(47, 308);
      this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(239, 17);
      this.label3.TabIndex = 403;
      this.label3.Text = "Set Tracking Date If Necessary ";
      // 
      // FrmRouteStepComment
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
      this.ClientSize = new System.Drawing.Size(359, 431);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dateTimePickerTrackdate);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textBoxComment);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.dataGridViewRouteData);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "FrmRouteStepComment";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Route Step Selector";
      this.Shown += new System.EventHandler(this.FrmRouteStepComment_Shown);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRouteData)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewRouteData;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCode;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStepId;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.TextBox textBoxComment;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker dateTimePickerTrackdate;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label3;
  }
}