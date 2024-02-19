namespace Tracking
{
   partial class FrmBatchTracking
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchTracking));
        this.dateTimePickerTrackingDate = new System.Windows.Forms.DateTimePicker();
        this.buttonSelectTrackingStep = new WSGUtilitieslib.Telemetry.Button();
        this.textBoxTrackingDate = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.labelSono = new System.Windows.Forms.Label();
        this.textBoxSono = new System.Windows.Forms.TextBox();
        this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
        this.SuspendLayout();
        // 
        // dateTimePickerTrackingDate
        // 
        this.dateTimePickerTrackingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dateTimePickerTrackingDate.Location = new System.Drawing.Point(203, 64);
        this.dateTimePickerTrackingDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        this.dateTimePickerTrackingDate.Name = "dateTimePickerTrackingDate";
        this.dateTimePickerTrackingDate.Size = new System.Drawing.Size(96, 22);
        this.dateTimePickerTrackingDate.TabIndex = 22;
        this.dateTimePickerTrackingDate.ValueChanged += new System.EventHandler(this.dateTimePickerShipFirstDate_ValueChanged);
        // 
        // buttonSelectTrackingStep
        // 
        this.buttonSelectTrackingStep.Location = new System.Drawing.Point(8, 16);
        this.buttonSelectTrackingStep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        this.buttonSelectTrackingStep.Name = "buttonSelectTrackingStep";
        this.buttonSelectTrackingStep.Size = new System.Drawing.Size(184, 23);
        this.buttonSelectTrackingStep.TabIndex = 23;
        this.buttonSelectTrackingStep.Text = "Select Tracking Step";
        this.buttonSelectTrackingStep.UseVisualStyleBackColor = true;
        this.buttonSelectTrackingStep.Click += new System.EventHandler(this.buttonSelectTrackingStep_Click);
        // 
        // textBoxTrackingDate
        // 
        this.textBoxTrackingDate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        this.textBoxTrackingDate.Location = new System.Drawing.Point(305, 64);
        this.textBoxTrackingDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        this.textBoxTrackingDate.Name = "textBoxTrackingDate";
        this.textBoxTrackingDate.ReadOnly = true;
        this.textBoxTrackingDate.Size = new System.Drawing.Size(84, 22);
        this.textBoxTrackingDate.TabIndex = 21;
        this.textBoxTrackingDate.Text = " ";
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(80, 69);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(110, 17);
        this.label5.TabIndex = 20;
        this.label5.Text = "Tracking Date";
        // 
        // labelSono
        // 
        this.labelSono.AutoSize = true;
        this.labelSono.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelSono.Location = new System.Drawing.Point(37, 102);
        this.labelSono.Name = "labelSono";
        this.labelSono.Size = new System.Drawing.Size(156, 17);
        this.labelSono.TabIndex = 25;
        this.labelSono.Text = "SO #";
        // 
        // textBoxSono
        // 
        this.textBoxSono.Location = new System.Drawing.Point(203, 102);
        this.textBoxSono.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        this.textBoxSono.MaxLength = 10;
        this.textBoxSono.Name = "textBoxSono";
        this.textBoxSono.Size = new System.Drawing.Size(100, 22);
        this.textBoxSono.TabIndex = 26;
        this.textBoxSono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSono_KeyDown);
        // 
        // buttonCancel
        // 
        this.buttonCancel.Location = new System.Drawing.Point(336, 16);
        this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(84, 23);
        this.buttonCancel.TabIndex = 27;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = true;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // FrmBatchTracking
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
        this.ClientSize = new System.Drawing.Size(432, 154);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.textBoxSono);
        this.Controls.Add(this.labelSono);
        this.Controls.Add(this.dateTimePickerTrackingDate);
        this.Controls.Add(this.buttonSelectTrackingStep);
        this.Controls.Add(this.textBoxTrackingDate);
        this.Controls.Add(this.label5);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        this.Name = "FrmBatchTracking";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Batch Tracking";
        this.ResumeLayout(false);
        this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.DateTimePicker dateTimePickerTrackingDate;
      private System.Windows.Forms.Button buttonSelectTrackingStep;
      private System.Windows.Forms.TextBox textBoxTrackingDate;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label labelSono;
      private System.Windows.Forms.TextBox textBoxSono;
      private System.Windows.Forms.Button buttonCancel;
   }
}