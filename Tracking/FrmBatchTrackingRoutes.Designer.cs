namespace Tracking
{
  partial class FrmBatchTrackingRoutes
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchTrackingRoutes));
      this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
      this.textBoxSono = new System.Windows.Forms.TextBox();
      this.labelSono = new System.Windows.Forms.Label();
      this.dateTimePickerTrackingDate = new System.Windows.Forms.DateTimePicker();
      this.textBoxTrackingDate = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Location = new System.Drawing.Point(268, 17);
      this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(63, 19);
      this.buttonCancel.TabIndex = 33;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // textBoxSono
      // 
      this.textBoxSono.Location = new System.Drawing.Point(169, 85);
      this.textBoxSono.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.textBoxSono.MaxLength = 10;
      this.textBoxSono.Name = "textBoxSono";
      this.textBoxSono.Size = new System.Drawing.Size(76, 20);
      this.textBoxSono.TabIndex = 32;
      this.textBoxSono.TextChanged += new System.EventHandler(this.textBoxSono_TextChanged);
      this.textBoxSono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSono_KeyDown);
      // 
      // labelSono
      // 
      this.labelSono.AutoSize = true;
      this.labelSono.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelSono.Location = new System.Drawing.Point(44, 85);
      this.labelSono.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.labelSono.Name = "labelSono";
      this.labelSono.Size = new System.Drawing.Size(120, 13);
      this.labelSono.TabIndex = 31;
      this.labelSono.Text = "SO #";
      // 
      // dateTimePickerTrackingDate
      // 
      this.dateTimePickerTrackingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dateTimePickerTrackingDate.Location = new System.Drawing.Point(169, 56);
      this.dateTimePickerTrackingDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.dateTimePickerTrackingDate.Name = "dateTimePickerTrackingDate";
      this.dateTimePickerTrackingDate.Size = new System.Drawing.Size(73, 20);
      this.dateTimePickerTrackingDate.TabIndex = 30;
      this.dateTimePickerTrackingDate.ValueChanged += new System.EventHandler(this.dateTimePickerTrackingDate_ValueChanged);
      // 
      // textBoxTrackingDate
      // 
      this.textBoxTrackingDate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
      this.textBoxTrackingDate.Location = new System.Drawing.Point(245, 56);
      this.textBoxTrackingDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.textBoxTrackingDate.Name = "textBoxTrackingDate";
      this.textBoxTrackingDate.ReadOnly = true;
      this.textBoxTrackingDate.Size = new System.Drawing.Size(64, 20);
      this.textBoxTrackingDate.TabIndex = 29;
      this.textBoxTrackingDate.Text = " ";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(76, 60);
      this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(88, 13);
      this.label5.TabIndex = 28;
      this.label5.Text = "Tracking Date";
      // 
      // FrmBatchTrackingRoutes
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
      this.ClientSize = new System.Drawing.Size(375, 157);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.textBoxSono);
      this.Controls.Add(this.labelSono);
      this.Controls.Add(this.dateTimePickerTrackingDate);
      this.Controls.Add(this.textBoxTrackingDate);
      this.Controls.Add(this.label5);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.Name = "FrmBatchTrackingRoutes";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Batch SO Tracking - Routes";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.TextBox textBoxSono;
    private System.Windows.Forms.Label labelSono;
    private System.Windows.Forms.DateTimePicker dateTimePickerTrackingDate;
    private System.Windows.Forms.TextBox textBoxTrackingDate;
    private System.Windows.Forms.Label label5;
  }
}