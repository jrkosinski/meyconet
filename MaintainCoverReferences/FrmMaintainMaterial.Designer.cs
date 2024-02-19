namespace MaintainCoverReferences
{
  partial class FrmMaintainMaterial
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
        this.textBoxPrcmatrl = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.textBoxDescrip = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.dataGridViewMaterial = new System.Windows.Forms.DataGridView();
        this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
        this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
        this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
        this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
        this.label3 = new System.Windows.Forms.Label();
        this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.ColumnStrpmult = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterial)).BeginInit();
        this.SuspendLayout();
        // 
        // textBoxPrcmatrl
        // 
        this.textBoxPrcmatrl.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxPrcmatrl.Location = new System.Drawing.Point(394, 195);
        this.textBoxPrcmatrl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxPrcmatrl.MaxLength = 1;
        this.textBoxPrcmatrl.Name = "textBoxPrcmatrl";
        this.textBoxPrcmatrl.Size = new System.Drawing.Size(22, 20);
        this.textBoxPrcmatrl.TabIndex = 132;
        this.textBoxPrcmatrl.TextChanged += new System.EventHandler(this.textBoxPrcmatrl_TextChanged);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(298, 197);
        this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(90, 13);
        this.label2.TabIndex = 131;
        this.label2.Text = "Material Group";
        this.label2.Click += new System.EventHandler(this.label2_Click);
        // 
        // textBoxDescrip
        // 
        this.textBoxDescrip.Location = new System.Drawing.Point(171, 197);
        this.textBoxDescrip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.textBoxDescrip.Name = "textBoxDescrip";
        this.textBoxDescrip.Size = new System.Drawing.Size(108, 20);
        this.textBoxDescrip.TabIndex = 130;
        this.textBoxDescrip.TextChanged += new System.EventHandler(this.textBoxDescrip_TextChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(110, 200);
        this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(52, 13);
        this.label1.TabIndex = 129;
        this.label1.Text = "Material";
        this.label1.Click += new System.EventHandler(this.label1_Click);
        // 
        // dataGridViewMaterial
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewMaterial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewMaterial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDescrip,
            this.ColumnStrpmult});
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridViewMaterial.DefaultCellStyle = dataGridViewCellStyle4;
        this.dataGridViewMaterial.EnableHeadersVisualStyles = false;
        this.dataGridViewMaterial.Location = new System.Drawing.Point(107, 71);
        this.dataGridViewMaterial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.dataGridViewMaterial.Name = "dataGridViewMaterial";
        this.dataGridViewMaterial.RowHeadersVisible = false;
        this.dataGridViewMaterial.RowTemplate.Height = 24;
        this.dataGridViewMaterial.Size = new System.Drawing.Size(334, 106);
        this.dataGridViewMaterial.TabIndex = 128;
        this.dataGridViewMaterial.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMaterial_CellContentDoubleClick);
        this.dataGridViewMaterial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMaterial_CellContentClick);
        // 
        // buttonClear
        // 
        this.buttonClear.Location = new System.Drawing.Point(131, 40);
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
        this.buttonEdit.Location = new System.Drawing.Point(209, 40);
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
        this.buttonInsert.Location = new System.Drawing.Point(53, 40);
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
        this.buttonSave.Location = new System.Drawing.Point(365, 40);
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
        this.buttonClose.Location = new System.Drawing.Point(443, 40);
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
        this.buttonDelete.Location = new System.Drawing.Point(287, 40);
        this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.buttonDelete.Name = "buttonDelete";
        this.buttonDelete.Size = new System.Drawing.Size(56, 27);
        this.buttonDelete.TabIndex = 133;
        this.buttonDelete.Text = "Delete";
        this.buttonDelete.UseVisualStyleBackColor = true;
        this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(298, 211);
        this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(81, 13);
        this.label3.TabIndex = 134;
        this.label3.Text = "M, R, S, L, T";
        this.label3.Click += new System.EventHandler(this.label3_Click);
        // 
        // ColumnDescrip
        // 
        this.ColumnDescrip.DataPropertyName = "descrip";
        dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
        this.ColumnDescrip.DefaultCellStyle = dataGridViewCellStyle2;
        this.ColumnDescrip.HeaderText = "Material";
        this.ColumnDescrip.Name = "ColumnDescrip";
        this.ColumnDescrip.Width = 200;
        // 
        // ColumnStrpmult
        // 
        this.ColumnStrpmult.DataPropertyName = "prcmatrl";
        dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle3.Format = "N3";
        dataGridViewCellStyle3.NullValue = null;
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
        this.ColumnStrpmult.DefaultCellStyle = dataGridViewCellStyle3;
        this.ColumnStrpmult.HeaderText = "Material Group";
        this.ColumnStrpmult.Name = "ColumnStrpmult";
        // 
        // FrmMaintainMaterial
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(514, 286);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.buttonDelete);
        this.Controls.Add(this.textBoxPrcmatrl);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.textBoxDescrip);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.dataGridViewMaterial);
        this.Controls.Add(this.buttonClear);
        this.Controls.Add(this.buttonEdit);
        this.Controls.Add(this.buttonInsert);
        this.Controls.Add(this.buttonSave);
        this.Controls.Add(this.buttonClose);
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.Name = "FrmMaintainMaterial";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Maintain Material";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterial)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBoxPrcmatrl;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxDescrip;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridView dataGridViewMaterial;
    private System.Windows.Forms.Button buttonClear;
    private System.Windows.Forms.Button buttonEdit;
    private System.Windows.Forms.Button buttonInsert;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Button buttonDelete;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStrpmult;
  }
}