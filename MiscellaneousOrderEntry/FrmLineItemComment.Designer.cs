namespace MiscellaneousOrderEntry
{
  partial class FrmLineItemComment
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dataGridViewSystemComments = new System.Windows.Forms.DataGridView();
      this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnStrpmult = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxCustcomments = new System.Windows.Forms.TextBox();
      this.textBoxIntcomments = new System.Windows.Forms.TextBox();
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSystemComments)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridViewSystemComments
      // 
      dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewSystemComments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
      this.dataGridViewSystemComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewSystemComments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip,
            this.ColumnStrpmult});
      dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridViewSystemComments.DefaultCellStyle = dataGridViewCellStyle16;
      this.dataGridViewSystemComments.EnableHeadersVisualStyles = false;
      this.dataGridViewSystemComments.Location = new System.Drawing.Point(6, 186);
      this.dataGridViewSystemComments.Margin = new System.Windows.Forms.Padding(2);
      this.dataGridViewSystemComments.Name = "dataGridViewSystemComments";
      this.dataGridViewSystemComments.RowHeadersVisible = false;
      this.dataGridViewSystemComments.RowTemplate.Height = 24;
      this.dataGridViewSystemComments.Size = new System.Drawing.Size(884, 124);
      this.dataGridViewSystemComments.TabIndex = 134;
      this.dataGridViewSystemComments.Visible = false;
      this.dataGridViewSystemComments.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSystemComments_CellContentDoubleClick);
      // 
      // ColumnDescrip
      // 
      this.ColumnDescrip.DataPropertyName = "code";
      dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
      this.ColumnDescrip.DefaultCellStyle = dataGridViewCellStyle14;
      this.ColumnDescrip.HeaderText = "Code";
      this.ColumnDescrip.Name = "ColumnDescrip";
      this.ColumnDescrip.Width = 150;
      // 
      // ColumnStrpmult
      // 
      this.ColumnStrpmult.DataPropertyName = "descrip";
      dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle15.Format = "N3";
      dataGridViewCellStyle15.NullValue = null;
      this.ColumnStrpmult.DefaultCellStyle = dataGridViewCellStyle15;
      this.ColumnStrpmult.HeaderText = "Comment";
      this.ColumnStrpmult.Name = "ColumnStrpmult";
      this.ColumnStrpmult.Width = 700;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(469, 12);
      this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(120, 13);
      this.label2.TabIndex = 133;
      this.label2.Text = "Customer Comments";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(30, 12);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(111, 13);
      this.label1.TabIndex = 132;
      this.label1.Text = "Internal Comments";
      // 
      // textBoxCustcomments
      // 
      this.textBoxCustcomments.Location = new System.Drawing.Point(456, 30);
      this.textBoxCustcomments.Margin = new System.Windows.Forms.Padding(2);
      this.textBoxCustcomments.Multiline = true;
      this.textBoxCustcomments.Name = "textBoxCustcomments";
      this.textBoxCustcomments.Size = new System.Drawing.Size(294, 144);
      this.textBoxCustcomments.TabIndex = 131;
      this.textBoxCustcomments.DoubleClick += new System.EventHandler(this.textBoxCustcomments_DoubleClick);
      // 
      // textBoxIntcomments
      // 
      this.textBoxIntcomments.Location = new System.Drawing.Point(6, 30);
      this.textBoxIntcomments.Margin = new System.Windows.Forms.Padding(2);
      this.textBoxIntcomments.Multiline = true;
      this.textBoxIntcomments.Name = "textBoxIntcomments";
      this.textBoxIntcomments.Size = new System.Drawing.Size(434, 144);
      this.textBoxIntcomments.TabIndex = 130;
      this.textBoxIntcomments.DoubleClick += new System.EventHandler(this.textBoxIntcomments_DoubleClick);
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(798, 11);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(92, 37);
      this.buttonClose.TabIndex = 135;
      this.buttonClose.Text = "Save";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // FrmLineItemComment
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(902, 338);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.dataGridViewSystemComments);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxCustcomments);
      this.Controls.Add(this.textBoxIntcomments);
      this.Name = "FrmLineItemComment";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Line Item Comments";
      this.Load += new System.EventHandler(this.FrmLineItemComment_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSystemComments)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewSystemComments;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStrpmult;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxCustcomments;
    private System.Windows.Forms.TextBox textBoxIntcomments;
    private System.Windows.Forms.Button buttonClose;
  }
}