namespace Print
{
  partial class FrmInventoryOnHand
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
            this.labelItem = new System.Windows.Forms.Label();
            this.textBoxItem = new System.Windows.Forms.TextBox();
            this.buttonGenerate = new WSGUtilitieslib.Telemetry.Button();
            this.labelCutoffDate = new System.Windows.Forms.Label();
            this.dateTimePickerCutoff = new System.Windows.Forms.DateTimePicker();
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            this.SuspendLayout();
            // 
            // labelItem
            // 
            this.labelItem.AutoSize = true;
            this.labelItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItem.Location = new System.Drawing.Point(83, 134);
            this.labelItem.Name = "labelItem";
            this.labelItem.Size = new System.Drawing.Size(31, 13);
            this.labelItem.TabIndex = 22;
            this.labelItem.Text = "Item";
            // 
            // textBoxItem
            // 
            this.textBoxItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxItem.Location = new System.Drawing.Point(83, 153);
            this.textBoxItem.Name = "textBoxItem";
            this.textBoxItem.Size = new System.Drawing.Size(100, 20);
            this.textBoxItem.TabIndex = 2;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(304, 42);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 22);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            // 
            // labelCutoffDate
            // 
            this.labelCutoffDate.AutoSize = true;
            this.labelCutoffDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCutoffDate.Location = new System.Drawing.Point(83, 78);
            this.labelCutoffDate.Name = "labelCutoffDate";
            this.labelCutoffDate.Size = new System.Drawing.Size(72, 13);
            this.labelCutoffDate.TabIndex = 17;
            this.labelCutoffDate.Text = "Cutoff Date";
            // 
            // dateTimePickerCutoff
            // 
            this.dateTimePickerCutoff.Location = new System.Drawing.Point(83, 106);
            this.dateTimePickerCutoff.Name = "dateTimePickerCutoff";
            this.dateTimePickerCutoff.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerCutoff.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(413, 42);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 22);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FrmInventoryOnHand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 235);
            this.Controls.Add(this.labelItem);
            this.Controls.Add(this.textBoxItem);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.labelCutoffDate);
            this.Controls.Add(this.dateTimePickerCutoff);
            this.Controls.Add(this.buttonClose);
            this.Name = "FrmInventoryOnHand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory On Hand";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.TextBox textBoxItem;
    public System.Windows.Forms.DateTimePicker dateTimePickerCutoff;
    public System.Windows.Forms.Label labelCutoffDate;
    public System.Windows.Forms.Label labelItem;
        public WSGUtilitieslib.Telemetry.Button buttonGenerate;
        public WSGUtilitieslib.Telemetry.Button buttonClose;
    }
}