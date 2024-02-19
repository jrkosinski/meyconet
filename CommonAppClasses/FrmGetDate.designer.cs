namespace CommonAppClasses
{
  partial class FrmGetDate
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
        this.dateTimePickerSelectDate = new System.Windows.Forms.DateTimePicker();
        this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.labelDateInformation = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // dateTimePickerSelectDate
        // 
        this.dateTimePickerSelectDate.Location = new System.Drawing.Point(14, 34);
        this.dateTimePickerSelectDate.Margin = new System.Windows.Forms.Padding(2);
        this.dateTimePickerSelectDate.Name = "dateTimePickerSelectDate";
        this.dateTimePickerSelectDate.Size = new System.Drawing.Size(200, 20);
        this.dateTimePickerSelectDate.TabIndex = 0;
        this.dateTimePickerSelectDate.ValueChanged += new System.EventHandler(this.dateTimePickerSelectDate_ValueChanged);
        // 
        // buttonCancel
        // 
        this.buttonCancel.Location = new System.Drawing.Point(64, 58);
        this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(56, 27);
        this.buttonCancel.TabIndex = 1;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // buttonClose
        // 
        this.buttonClose.Location = new System.Drawing.Point(158, 58);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 27);
        this.buttonClose.TabIndex = 2;
        this.buttonClose.Text = "Ship";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // labelDateInformation
        // 
        this.labelDateInformation.AutoSize = true;
        this.labelDateInformation.Location = new System.Drawing.Point(11, 9);
        this.labelDateInformation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelDateInformation.Name = "labelDateInformation";
        this.labelDateInformation.Size = new System.Drawing.Size(35, 13);
        this.labelDateInformation.TabIndex = 3;
        this.labelDateInformation.Text = "label1";
        // 
        // FrmGetDate
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(255, 107);
        this.Controls.Add(this.labelDateInformation);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.dateTimePickerSelectDate);
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmGetDate";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Enter Date";
        this.Shown += new System.EventHandler(this.FrmGetDate_Shown);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DateTimePicker dateTimePickerSelectDate;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Label labelDateInformation;
  }
}