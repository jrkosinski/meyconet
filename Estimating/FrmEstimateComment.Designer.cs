namespace Estimating
{
  partial class FrmEstimateComment
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.textBoxIntcomments = new System.Windows.Forms.TextBox();
        this.textBoxCustcomments = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.checkBoxManalert = new System.Windows.Forms.CheckBox();
        this.checkBoxDesalert = new System.Windows.Forms.CheckBox();
        this.checkBoxCsalert = new System.Windows.Forms.CheckBox();
        this.checkBoxQcalert = new System.Windows.Forms.CheckBox();
        this.checkBoxAralert = new System.Windows.Forms.CheckBox();
        this.checkBoxShipalert = new System.Windows.Forms.CheckBox();
        this.dataGridViewSystemComments = new System.Windows.Forms.DataGridView();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnStrpmult = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSystemComments)).BeginInit();
        this.SuspendLayout();
        // 
        // buttonClose
        // 
        this.buttonClose.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonClose.Location = new System.Drawing.Point(817, 7);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(92, 37);
        this.buttonClose.TabIndex = 0;
        this.buttonClose.Text = "Save";
        this.buttonClose.UseVisualStyleBackColor = false;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // textBoxIntcomments
        // 
        this.textBoxIntcomments.Location = new System.Drawing.Point(11, 51);
        this.textBoxIntcomments.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxIntcomments.Multiline = true;
        this.textBoxIntcomments.Name = "textBoxIntcomments";
        this.textBoxIntcomments.Size = new System.Drawing.Size(434, 144);
        this.textBoxIntcomments.TabIndex = 1;
        this.textBoxIntcomments.DoubleClick += new System.EventHandler(this.textBoxIntcomments_DoubleClick);
        // 
        // textBoxCustcomments
        // 
        this.textBoxCustcomments.Location = new System.Drawing.Point(461, 51);
        this.textBoxCustcomments.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxCustcomments.Multiline = true;
        this.textBoxCustcomments.Name = "textBoxCustcomments";
        this.textBoxCustcomments.Size = new System.Drawing.Size(294, 144);
        this.textBoxCustcomments.TabIndex = 2;
        this.textBoxCustcomments.DoubleClick += new System.EventHandler(this.textBoxCustcomments_DoubleClick);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(9, 31);
        this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(111, 13);
        this.label1.TabIndex = 3;
        this.label1.Text = "Internal Comments";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(459, 35);
        this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(120, 13);
        this.label2.TabIndex = 4;
        this.label2.Text = "Customer Comments";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(764, 31);
        this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(40, 13);
        this.label3.TabIndex = 5;
        this.label3.Text = "Notify";
        // 
        // checkBoxManalert
        // 
        this.checkBoxManalert.AutoSize = true;
        this.checkBoxManalert.Location = new System.Drawing.Point(786, 48);
        this.checkBoxManalert.Margin = new System.Windows.Forms.Padding(2);
        this.checkBoxManalert.Name = "checkBoxManalert";
        this.checkBoxManalert.Size = new System.Drawing.Size(94, 17);
        this.checkBoxManalert.TabIndex = 6;
        this.checkBoxManalert.Text = "Manufacturing";
        this.checkBoxManalert.UseVisualStyleBackColor = true;
        // 
        // checkBoxDesalert
        // 
        this.checkBoxDesalert.AutoSize = true;
        this.checkBoxDesalert.Location = new System.Drawing.Point(786, 73);
        this.checkBoxDesalert.Margin = new System.Windows.Forms.Padding(2);
        this.checkBoxDesalert.Name = "checkBoxDesalert";
        this.checkBoxDesalert.Size = new System.Drawing.Size(59, 17);
        this.checkBoxDesalert.TabIndex = 7;
        this.checkBoxDesalert.Text = "Design";
        this.checkBoxDesalert.UseVisualStyleBackColor = true;
        // 
        // checkBoxCsalert
        // 
        this.checkBoxCsalert.AutoSize = true;
        this.checkBoxCsalert.Location = new System.Drawing.Point(786, 121);
        this.checkBoxCsalert.Margin = new System.Windows.Forms.Padding(2);
        this.checkBoxCsalert.Name = "checkBoxCsalert";
        this.checkBoxCsalert.Size = new System.Drawing.Size(109, 17);
        this.checkBoxCsalert.TabIndex = 9;
        this.checkBoxCsalert.Text = "Customer Service";
        this.checkBoxCsalert.UseVisualStyleBackColor = true;
        // 
        // checkBoxQcalert
        // 
        this.checkBoxQcalert.AutoSize = true;
        this.checkBoxQcalert.Location = new System.Drawing.Point(786, 97);
        this.checkBoxQcalert.Margin = new System.Windows.Forms.Padding(2);
        this.checkBoxQcalert.Name = "checkBoxQcalert";
        this.checkBoxQcalert.Size = new System.Drawing.Size(94, 17);
        this.checkBoxQcalert.TabIndex = 8;
        this.checkBoxQcalert.Text = "Quality Control";
        this.checkBoxQcalert.UseVisualStyleBackColor = true;
        // 
        // checkBoxAralert
        // 
        this.checkBoxAralert.AutoSize = true;
        this.checkBoxAralert.Location = new System.Drawing.Point(786, 170);
        this.checkBoxAralert.Margin = new System.Windows.Forms.Padding(2);
        this.checkBoxAralert.Name = "checkBoxAralert";
        this.checkBoxAralert.Size = new System.Drawing.Size(46, 17);
        this.checkBoxAralert.TabIndex = 11;
        this.checkBoxAralert.Text = "A/R";
        this.checkBoxAralert.UseVisualStyleBackColor = true;
        // 
        // checkBoxShipalert
        // 
        this.checkBoxShipalert.AutoSize = true;
        this.checkBoxShipalert.Location = new System.Drawing.Point(786, 146);
        this.checkBoxShipalert.Margin = new System.Windows.Forms.Padding(2);
        this.checkBoxShipalert.Name = "checkBoxShipalert";
        this.checkBoxShipalert.Size = new System.Drawing.Size(67, 17);
        this.checkBoxShipalert.TabIndex = 10;
        this.checkBoxShipalert.Text = "Shipping";
        this.checkBoxShipalert.UseVisualStyleBackColor = true;
        // 
        // dataGridViewSystemComments
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewSystemComments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewSystemComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewSystemComments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip,
            this.ColumnStrpmult});
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridViewSystemComments.DefaultCellStyle = dataGridViewCellStyle4;
        this.dataGridViewSystemComments.EnableHeadersVisualStyles = false;
        this.dataGridViewSystemComments.Location = new System.Drawing.Point(11, 207);
        this.dataGridViewSystemComments.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewSystemComments.Name = "dataGridViewSystemComments";
        this.dataGridViewSystemComments.RowHeadersVisible = false;
        this.dataGridViewSystemComments.RowTemplate.Height = 24;
        this.dataGridViewSystemComments.Size = new System.Drawing.Size(884, 244);
        this.dataGridViewSystemComments.TabIndex = 129;
        this.dataGridViewSystemComments.Visible = false;
        this.dataGridViewSystemComments.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSystemComments_CellContentDoubleClick);
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "code";
        dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
        this.ColumnDescrip.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnDescrip.HeaderText = "Code";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.Width = 150;
        // 
        // ColumnStrpmult
        // 
        this.ColumnStrpmult.DataPropertyName = "descrip";
        dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle3.Format = "N3";
        dataGridViewCellStyle3.NullValue = null;
        this.ColumnStrpmult.DefaultCellStyle = dataGridViewCellStyle3;
        this.ColumnStrpmult.HeaderText = "Comment";
        this.ColumnStrpmult.Name = "ColumnStrpmult";
        this.ColumnStrpmult.Width = 700;
        // 
        // FrmEstimateComment
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoSize = true;
        this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(954, 475);
        this.Controls.Add(this.dataGridViewSystemComments);
        this.Controls.Add(this.checkBoxAralert);
        this.Controls.Add(this.checkBoxShipalert);
        this.Controls.Add(this.checkBoxCsalert);
        this.Controls.Add(this.checkBoxQcalert);
        this.Controls.Add(this.checkBoxDesalert);
        this.Controls.Add(this.checkBoxManalert);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBoxCustcomments);
        this.Controls.Add(this.textBoxIntcomments);
        this.Controls.Add(this.buttonClose);
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmEstimateComment";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Comments";
        this.Load += new System.EventHandler(this.FrmEstimateComment_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSystemComments)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.TextBox textBoxIntcomments;
    private System.Windows.Forms.TextBox textBoxCustcomments;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.CheckBox checkBoxManalert;
    private System.Windows.Forms.CheckBox checkBoxDesalert;
    private System.Windows.Forms.CheckBox checkBoxCsalert;
    private System.Windows.Forms.CheckBox checkBoxQcalert;
    private System.Windows.Forms.CheckBox checkBoxAralert;
    private System.Windows.Forms.CheckBox checkBoxShipalert;
    private System.Windows.Forms.DataGridView dataGridViewSystemComments;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStrpmult;
  }
}