namespace MiscellaneousOrderEntry
{
  partial class FrmPDFViewer
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
      this.buttonInvoicePDF = new WSGUtilitieslib.Telemetry.Button();
      this.buttonMainSOPDF = new WSGUtilitieslib.Telemetry.Button();
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      this.buttonPricePDF = new WSGUtilitieslib.Telemetry.Button();
      this.SuspendLayout();
      // 
      // buttonInvoicePDF
      // 
      this.buttonInvoicePDF.Location = new System.Drawing.Point(24, 17);
      this.buttonInvoicePDF.Name = "buttonInvoicePDF";
      this.buttonInvoicePDF.Size = new System.Drawing.Size(141, 46);
      this.buttonInvoicePDF.TabIndex = 0;
      this.buttonInvoicePDF.Text = "Invoice PDF";
      this.buttonInvoicePDF.UseVisualStyleBackColor = true;
      this.buttonInvoicePDF.Click += new System.EventHandler(this.buttonInvoicePDF_Click);
      // 
      // buttonMainSOPDF
      // 
      this.buttonMainSOPDF.Location = new System.Drawing.Point(25, 69);
      this.buttonMainSOPDF.Name = "buttonMainSOPDF";
      this.buttonMainSOPDF.Size = new System.Drawing.Size(141, 46);
      this.buttonMainSOPDF.TabIndex = 3;
      this.buttonMainSOPDF.Text = "Main SO PDF";
      this.buttonMainSOPDF.UseVisualStyleBackColor = true;
      this.buttonMainSOPDF.Click += new System.EventHandler(this.buttonMainSOPDF_Click);
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(24, 199);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(141, 46);
      this.buttonClose.TabIndex = 4;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // buttonPricePDF
      // 
      this.buttonPricePDF.Location = new System.Drawing.Point(25, 130);
      this.buttonPricePDF.Name = "buttonPricePDF";
      this.buttonPricePDF.Size = new System.Drawing.Size(141, 46);
      this.buttonPricePDF.TabIndex = 5;
      this.buttonPricePDF.Text = "Price PDF";
      this.buttonPricePDF.UseVisualStyleBackColor = true;
      this.buttonPricePDF.Click += new System.EventHandler(this.buttonPricePDF_Click);
      // 
      // FrmPDFViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(191, 307);
      this.Controls.Add(this.buttonPricePDF);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.buttonMainSOPDF);
      this.Controls.Add(this.buttonInvoicePDF);
      this.Name = "FrmPDFViewer";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "PDF Viewer";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonInvoicePDF;
    private System.Windows.Forms.Button buttonMainSOPDF;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Button buttonPricePDF;
  }
}