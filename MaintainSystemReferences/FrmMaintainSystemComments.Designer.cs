namespace MaintainSystemReference
{
  partial class FrmMaintainSystemComments
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        this.textBoxDescrip = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.textBoxCode = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.dataGridViewSystemComments = new System.Windows.Forms.DataGridView();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnStrpmult = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
        this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
        this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
        this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSystemComments)).BeginInit();
        this.SuspendLayout();
        // 
        // textBoxDescrip
        // 
        this.textBoxDescrip.Location = new System.Drawing.Point(82, 328);
        this.textBoxDescrip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxDescrip.Multiline = true;
        this.textBoxDescrip.Name = "textBoxDescrip";
        this.textBoxDescrip.Size = new System.Drawing.Size(323, 86);
        this.textBoxDescrip.TabIndex = 132;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(28, 330);
        this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(58, 13);
        this.label2.TabIndex = 131;
        this.label2.Text = "Comment";
        // 
        // textBoxCode
        // 
        this.textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxCode.Location = new System.Drawing.Point(82, 292);
        this.textBoxCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxCode.Name = "textBoxCode";
        this.textBoxCode.Size = new System.Drawing.Size(108, 20);
        this.textBoxCode.TabIndex = 130;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(28, 294);
        this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(36, 13);
        this.label1.TabIndex = 129;
        this.label1.Text = "Code";
        // 
        // dataGridViewSystemComments
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewSystemComments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewSystemComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewSystemComments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip,
            this.ColumnStrpmult});
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridViewSystemComments.DefaultCellStyle = dataGridViewCellStyle3;
        this.dataGridViewSystemComments.EnableHeadersVisualStyles = false;
        this.dataGridViewSystemComments.Location = new System.Drawing.Point(20, 59);
        this.dataGridViewSystemComments.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.dataGridViewSystemComments.Name = "dataGridViewSystemComments";
        this.dataGridViewSystemComments.RowHeadersVisible = false;
        this.dataGridViewSystemComments.RowTemplate.Height = 24;
        this.dataGridViewSystemComments.Size = new System.Drawing.Size(587, 210);
        this.dataGridViewSystemComments.TabIndex = 128;
        this.dataGridViewSystemComments.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSystemComments_CellContentDoubleClick);
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "code";
        this.ColumnDescrip.HeaderText = "Code";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.Width = 150;
        // 
        // ColumnStrpmult
        // 
        this.ColumnStrpmult.DataPropertyName = "descrip";
        dataGridViewCellStyle2.Format = "N3";
        dataGridViewCellStyle2.NullValue = null;
        this.ColumnStrpmult.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnStrpmult.HeaderText = "Comment";
        this.ColumnStrpmult.Name = "ColumnStrpmult";
        this.ColumnStrpmult.Width = 500;
        // 
        // buttonClear
        // 
        this.buttonClear.Enabled = false;
        this.buttonClear.Location = new System.Drawing.Point(144, 21);
        this.buttonClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClear.Name = "buttonClear";
        this.buttonClear.Size = new System.Drawing.Size(56, 27);
        this.buttonClear.TabIndex = 127;
        this.buttonClear.Text = "Clear";
        this.buttonClear.UseVisualStyleBackColor = true;
        this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
        // 
        // buttonEdit
        // 
        this.buttonEdit.Enabled = false;
        this.buttonEdit.Location = new System.Drawing.Point(207, 21);
        this.buttonEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonEdit.Name = "buttonEdit";
        this.buttonEdit.Size = new System.Drawing.Size(56, 27);
        this.buttonEdit.TabIndex = 126;
        this.buttonEdit.Text = "Edit";
        this.buttonEdit.UseVisualStyleBackColor = true;
        this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
        // 
        // buttonInsert
        // 
        this.buttonInsert.Location = new System.Drawing.Point(22, 21);
        this.buttonInsert.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonInsert.Name = "buttonInsert";
        this.buttonInsert.Size = new System.Drawing.Size(56, 27);
        this.buttonInsert.TabIndex = 125;
        this.buttonInsert.Text = "Insert";
        this.buttonInsert.UseVisualStyleBackColor = true;
        this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
        // 
        // buttonSave
        // 
        this.buttonSave.Location = new System.Drawing.Point(267, 21);
        this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Size = new System.Drawing.Size(56, 27);
        this.buttonSave.TabIndex = 124;
        this.buttonSave.Text = "Save";
        this.buttonSave.UseVisualStyleBackColor = true;
        this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
        // 
        // buttonClose
        // 
        this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonClose.Location = new System.Drawing.Point(327, 21);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 27);
        this.buttonClose.TabIndex = 123;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // buttonDelete
        // 
        this.buttonDelete.Enabled = false;
        this.buttonDelete.Location = new System.Drawing.Point(82, 21);
        this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonDelete.Name = "buttonDelete";
        this.buttonDelete.Size = new System.Drawing.Size(56, 27);
        this.buttonDelete.TabIndex = 133;
        this.buttonDelete.Text = "Delete";
        this.buttonDelete.UseVisualStyleBackColor = true;
        this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
        // 
        // FrmMaintainSystemComments
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(620, 491);
        this.Controls.Add(this.buttonDelete);
        this.Controls.Add(this.textBoxDescrip);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.textBoxCode);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.dataGridViewSystemComments);
        this.Controls.Add(this.buttonClear);
        this.Controls.Add(this.buttonEdit);
        this.Controls.Add(this.buttonInsert);
        this.Controls.Add(this.buttonSave);
        this.Controls.Add(this.buttonClose);
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.Name = "FrmMaintainSystemComments";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "System Comments";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSystemComments)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBoxDescrip;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxCode;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridView dataGridViewSystemComments;
    private System.Windows.Forms.Button buttonClear;
    private System.Windows.Forms.Button buttonEdit;
    private System.Windows.Forms.Button buttonInsert;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStrpmult;
    private System.Windows.Forms.Button buttonDelete;
  }
}