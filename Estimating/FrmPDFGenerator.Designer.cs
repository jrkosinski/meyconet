namespace Estimating
{
  partial class FrmPDFGenerator
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPDFGenerator));
        this.buttonMeasurementPDF = new WSGUtilitieslib.Telemetry.Button();
        this.buttonReplacementPDF = new WSGUtilitieslib.Telemetry.Button();
        this.buttonInformationPDF = new WSGUtilitieslib.Telemetry.Button();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.buttonRGAPDF = new WSGUtilitieslib.Telemetry.Button();
        this.SuspendLayout();
        // 
        // buttonMeasurementPDF
        // 
        this.buttonMeasurementPDF.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonMeasurementPDF.Location = new System.Drawing.Point(29, 161);
        this.buttonMeasurementPDF.Name = "buttonMeasurementPDF";
        this.buttonMeasurementPDF.Size = new System.Drawing.Size(141, 46);
        this.buttonMeasurementPDF.TabIndex = 11;
        this.buttonMeasurementPDF.Text = "Measurement PDF";
        this.buttonMeasurementPDF.UseVisualStyleBackColor = false;
        this.buttonMeasurementPDF.Click += new System.EventHandler(this.buttonMeasurementPDF_Click);
        // 
        // buttonReplacementPDF
        // 
        this.buttonReplacementPDF.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonReplacementPDF.Location = new System.Drawing.Point(29, 63);
        this.buttonReplacementPDF.Name = "buttonReplacementPDF";
        this.buttonReplacementPDF.Size = new System.Drawing.Size(141, 46);
        this.buttonReplacementPDF.TabIndex = 10;
        this.buttonReplacementPDF.Text = "Replacement PDF";
        this.buttonReplacementPDF.UseVisualStyleBackColor = false;
        this.buttonReplacementPDF.Click += new System.EventHandler(this.buttonReplacementPDF_Click);
        // 
        // buttonInformationPDF
        // 
        this.buttonInformationPDF.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonInformationPDF.Location = new System.Drawing.Point(29, 14);
        this.buttonInformationPDF.Name = "buttonInformationPDF";
        this.buttonInformationPDF.Size = new System.Drawing.Size(141, 46);
        this.buttonInformationPDF.TabIndex = 9;
        this.buttonInformationPDF.Text = "Information PDF";
        this.buttonInformationPDF.UseVisualStyleBackColor = false;
        this.buttonInformationPDF.Click += new System.EventHandler(this.buttonInformationPDF_Click);
        // 
        // buttonClose
        // 
        this.buttonClose.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonClose.Location = new System.Drawing.Point(29, 235);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(141, 46);
        this.buttonClose.TabIndex = 12;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = false;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // buttonRGAPDF
        // 
        this.buttonRGAPDF.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonRGAPDF.Location = new System.Drawing.Point(29, 112);
        this.buttonRGAPDF.Name = "buttonRGAPDF";
        this.buttonRGAPDF.Size = new System.Drawing.Size(141, 46);
        this.buttonRGAPDF.TabIndex = 13;
        this.buttonRGAPDF.Text = "RGA PDF";
        this.buttonRGAPDF.UseVisualStyleBackColor = false;
        this.buttonRGAPDF.Click += new System.EventHandler(this.buttonRGAPDF_Click);
        // 
        // FrmPDFGenerator
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(206, 296);
        this.Controls.Add(this.buttonRGAPDF);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.buttonMeasurementPDF);
        this.Controls.Add(this.buttonReplacementPDF);
        this.Controls.Add(this.buttonInformationPDF);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "FrmPDFGenerator";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "PDF Generator";
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonMeasurementPDF;
    private System.Windows.Forms.Button buttonReplacementPDF;
    private System.Windows.Forms.Button buttonInformationPDF;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Button buttonRGAPDF;
  }
}