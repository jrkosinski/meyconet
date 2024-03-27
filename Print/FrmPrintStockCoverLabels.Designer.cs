namespace Print
{
  partial class FrmPrintStockCoverLabels
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
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            this.buttonGenerate = new WSGUtilitieslib.Telemetry.Button();
            this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
            this.textBoxFirstFileNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxLastFileNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonMarkLabelsPrinted = new WSGUtilitieslib.Telemetry.Button();
            this.groupBoxLabelType = new System.Windows.Forms.GroupBox();
            this.radioButtonIdentityLabel = new System.Windows.Forms.RadioButton();
            this.radioButtonSewnOnLabel = new System.Windows.Forms.RadioButton();
            this.groupBoxLabelType.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(443, 29);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(324, 29);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 5;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(83, 29);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 0;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxFirstFileNumber
            // 
            this.textBoxFirstFileNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxFirstFileNumber.Location = new System.Drawing.Point(323, 93);
            this.textBoxFirstFileNumber.MaxLength = 15;
            this.textBoxFirstFileNumber.Name = "textBoxFirstFileNumber";
            this.textBoxFirstFileNumber.Size = new System.Drawing.Size(117, 20);
            this.textBoxFirstFileNumber.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(215, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "First File Number";
            // 
            // textBoxLastFileNumber
            // 
            this.textBoxLastFileNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxLastFileNumber.Location = new System.Drawing.Point(323, 119);
            this.textBoxLastFileNumber.MaxLength = 15;
            this.textBoxLastFileNumber.Name = "textBoxLastFileNumber";
            this.textBoxLastFileNumber.Size = new System.Drawing.Size(117, 20);
            this.textBoxLastFileNumber.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(215, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Last File Number";
            // 
            // buttonMarkLabelsPrinted
            // 
            this.buttonMarkLabelsPrinted.Location = new System.Drawing.Point(175, 29);
            this.buttonMarkLabelsPrinted.Name = "buttonMarkLabelsPrinted";
            this.buttonMarkLabelsPrinted.Size = new System.Drawing.Size(135, 23);
            this.buttonMarkLabelsPrinted.TabIndex = 4;
            this.buttonMarkLabelsPrinted.Text = "Mark Labels Printed";
            this.buttonMarkLabelsPrinted.UseVisualStyleBackColor = true;
            // 
            // groupBoxLabelType
            // 
            this.groupBoxLabelType.Controls.Add(this.radioButtonIdentityLabel);
            this.groupBoxLabelType.Controls.Add(this.radioButtonSewnOnLabel);
            this.groupBoxLabelType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxLabelType.Location = new System.Drawing.Point(52, 70);
            this.groupBoxLabelType.Name = "groupBoxLabelType";
            this.groupBoxLabelType.Size = new System.Drawing.Size(136, 81);
            this.groupBoxLabelType.TabIndex = 1;
            this.groupBoxLabelType.TabStop = false;
            this.groupBoxLabelType.Text = "Label Type";
            // 
            // radioButtonIdentityLabel
            // 
            this.radioButtonIdentityLabel.AutoSize = true;
            this.radioButtonIdentityLabel.Location = new System.Drawing.Point(20, 46);
            this.radioButtonIdentityLabel.Name = "radioButtonIdentityLabel";
            this.radioButtonIdentityLabel.Size = new System.Drawing.Size(102, 17);
            this.radioButtonIdentityLabel.TabIndex = 1;
            this.radioButtonIdentityLabel.TabStop = true;
            this.radioButtonIdentityLabel.Text = "Identity Label";
            this.radioButtonIdentityLabel.UseVisualStyleBackColor = true;
            // 
            // radioButtonSewnOnLabel
            // 
            this.radioButtonSewnOnLabel.AutoSize = true;
            this.radioButtonSewnOnLabel.Location = new System.Drawing.Point(20, 23);
            this.radioButtonSewnOnLabel.Name = "radioButtonSewnOnLabel";
            this.radioButtonSewnOnLabel.Size = new System.Drawing.Size(111, 17);
            this.radioButtonSewnOnLabel.TabIndex = 0;
            this.radioButtonSewnOnLabel.TabStop = true;
            this.radioButtonSewnOnLabel.Text = "Sewn On Label";
            this.radioButtonSewnOnLabel.UseVisualStyleBackColor = true;
            // 
            // FrmPrintStockCoverLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 200);
            this.Controls.Add(this.groupBoxLabelType);
            this.Controls.Add(this.buttonMarkLabelsPrinted);
            this.Controls.Add(this.textBoxLastFileNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxFirstFileNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonClose);
            this.Name = "FrmPrintStockCoverLabels";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Stock Cover Labels";
            this.groupBoxLabelType.ResumeLayout(false);
            this.groupBoxLabelType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    public System.Windows.Forms.TextBox textBoxFirstFileNumber;
    public System.Windows.Forms.Label label4;
    public System.Windows.Forms.TextBox textBoxLastFileNumber;
    public System.Windows.Forms.Label label5;
    public System.Windows.Forms.GroupBox groupBoxLabelType;
    public System.Windows.Forms.RadioButton radioButtonIdentityLabel;
    public System.Windows.Forms.RadioButton radioButtonSewnOnLabel;
        public WSGUtilitieslib.Telemetry.Button buttonMarkLabelsPrinted;
        public WSGUtilitieslib.Telemetry.Button buttonClose;
        public WSGUtilitieslib.Telemetry.Button buttonGenerate;
        public WSGUtilitieslib.Telemetry.Button buttonClear;
    }
}