namespace Print
{
  partial class FrmPrintSoDocumentsBatch
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintSoDocumentsBatch));
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      this.buttonPrintCustomCoverOrders = new WSGUtilitieslib.Telemetry.Button();
      this.buttonPrintStockCoverOrders = new WSGUtilitieslib.Telemetry.Button();
      this.buttonPrintRepairOrders = new WSGUtilitieslib.Telemetry.Button();
      this.buttonPrintMiscellaneousOrders = new WSGUtilitieslib.Telemetry.Button();
      this.radioButtonInvoices = new System.Windows.Forms.RadioButton();
      this.radioButtonWorkOrders = new System.Windows.Forms.RadioButton();
      this.radioButtonPackingLists = new System.Windows.Forms.RadioButton();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.radioButtonSewnOnLabels = new System.Windows.Forms.RadioButton();
      this.radioButtonIdentityLabels = new System.Windows.Forms.RadioButton();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(415, 8);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(56, 28);
      this.buttonClose.TabIndex = 1;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // buttonPrintCustomCoverOrders
      // 
      this.buttonPrintCustomCoverOrders.Location = new System.Drawing.Point(10, 12);
      this.buttonPrintCustomCoverOrders.Margin = new System.Windows.Forms.Padding(2);
      this.buttonPrintCustomCoverOrders.Name = "buttonPrintCustomCoverOrders";
      this.buttonPrintCustomCoverOrders.Size = new System.Drawing.Size(157, 28);
      this.buttonPrintCustomCoverOrders.TabIndex = 2;
      this.buttonPrintCustomCoverOrders.Text = "Custom Cover Orders";
      this.buttonPrintCustomCoverOrders.UseVisualStyleBackColor = true;
      this.buttonPrintCustomCoverOrders.Click += new System.EventHandler(this.buttonPrintCustomCoverOrders_Click);
      // 
      // buttonPrintStockCoverOrders
      // 
      this.buttonPrintStockCoverOrders.Location = new System.Drawing.Point(10, 44);
      this.buttonPrintStockCoverOrders.Margin = new System.Windows.Forms.Padding(2);
      this.buttonPrintStockCoverOrders.Name = "buttonPrintStockCoverOrders";
      this.buttonPrintStockCoverOrders.Size = new System.Drawing.Size(157, 28);
      this.buttonPrintStockCoverOrders.TabIndex = 3;
      this.buttonPrintStockCoverOrders.Text = "Stock Cover Orders";
      this.buttonPrintStockCoverOrders.UseVisualStyleBackColor = true;
      this.buttonPrintStockCoverOrders.Click += new System.EventHandler(this.buttonPrintStockCoverOrders_Click);
      // 
      // buttonPrintRepairOrders
      // 
      this.buttonPrintRepairOrders.Location = new System.Drawing.Point(10, 76);
      this.buttonPrintRepairOrders.Margin = new System.Windows.Forms.Padding(2);
      this.buttonPrintRepairOrders.Name = "buttonPrintRepairOrders";
      this.buttonPrintRepairOrders.Size = new System.Drawing.Size(157, 28);
      this.buttonPrintRepairOrders.TabIndex = 4;
      this.buttonPrintRepairOrders.Text = "Repair Orders";
      this.buttonPrintRepairOrders.UseVisualStyleBackColor = true;
      this.buttonPrintRepairOrders.Click += new System.EventHandler(this.buttonPrintRepairOrders_Click);
      // 
      // buttonPrintMiscellaneousOrders
      // 
      this.buttonPrintMiscellaneousOrders.Location = new System.Drawing.Point(10, 108);
      this.buttonPrintMiscellaneousOrders.Margin = new System.Windows.Forms.Padding(2);
      this.buttonPrintMiscellaneousOrders.Name = "buttonPrintMiscellaneousOrders";
      this.buttonPrintMiscellaneousOrders.Size = new System.Drawing.Size(157, 28);
      this.buttonPrintMiscellaneousOrders.TabIndex = 6;
      this.buttonPrintMiscellaneousOrders.Text = "Miscellaneous Orders";
      this.buttonPrintMiscellaneousOrders.UseVisualStyleBackColor = true;
      this.buttonPrintMiscellaneousOrders.Click += new System.EventHandler(this.buttonPrintMiscellaneousOrders_Click);
      // 
      // radioButtonInvoices
      // 
      this.radioButtonInvoices.AutoSize = true;
      this.radioButtonInvoices.Location = new System.Drawing.Point(24, 54);
      this.radioButtonInvoices.Name = "radioButtonInvoices";
      this.radioButtonInvoices.Size = new System.Drawing.Size(65, 17);
      this.radioButtonInvoices.TabIndex = 7;
      this.radioButtonInvoices.TabStop = true;
      this.radioButtonInvoices.Text = "Invoices";
      this.radioButtonInvoices.UseVisualStyleBackColor = true;
      this.radioButtonInvoices.CheckedChanged += new System.EventHandler(this.radioButtonInvoices_CheckedChanged);
      // 
      // radioButtonWorkOrders
      // 
      this.radioButtonWorkOrders.AutoSize = true;
      this.radioButtonWorkOrders.Location = new System.Drawing.Point(24, 33);
      this.radioButtonWorkOrders.Name = "radioButtonWorkOrders";
      this.radioButtonWorkOrders.Size = new System.Drawing.Size(85, 17);
      this.radioButtonWorkOrders.TabIndex = 8;
      this.radioButtonWorkOrders.TabStop = true;
      this.radioButtonWorkOrders.Text = "Work Orders";
      this.radioButtonWorkOrders.UseVisualStyleBackColor = true;
      this.radioButtonWorkOrders.CheckedChanged += new System.EventHandler(this.radioButtonWorkOrders_CheckedChanged);
      // 
      // radioButtonPackingLists
      // 
      this.radioButtonPackingLists.AutoSize = true;
      this.radioButtonPackingLists.Location = new System.Drawing.Point(24, 12);
      this.radioButtonPackingLists.Name = "radioButtonPackingLists";
      this.radioButtonPackingLists.Size = new System.Drawing.Size(88, 17);
      this.radioButtonPackingLists.TabIndex = 9;
      this.radioButtonPackingLists.TabStop = true;
      this.radioButtonPackingLists.Text = "Packing Lists";
      this.radioButtonPackingLists.UseVisualStyleBackColor = true;
      this.radioButtonPackingLists.CheckedChanged += new System.EventHandler(this.radioButtonPackingLists_CheckedChanged);
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.radioButtonIdentityLabels);
      this.panel1.Controls.Add(this.radioButtonSewnOnLabels);
      this.panel1.Controls.Add(this.radioButtonPackingLists);
      this.panel1.Controls.Add(this.radioButtonWorkOrders);
      this.panel1.Controls.Add(this.radioButtonInvoices);
      this.panel1.Location = new System.Drawing.Point(17, 33);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(154, 138);
      this.panel1.TabIndex = 10;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(30, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(124, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Select Document to print";
      // 
      // panel2
      // 
      this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel2.Controls.Add(this.buttonPrintStockCoverOrders);
      this.panel2.Controls.Add(this.buttonPrintCustomCoverOrders);
      this.panel2.Controls.Add(this.buttonPrintRepairOrders);
      this.panel2.Controls.Add(this.buttonPrintMiscellaneousOrders);
      this.panel2.Location = new System.Drawing.Point(208, 33);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(178, 152);
      this.panel2.TabIndex = 11;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(227, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(127, 13);
      this.label2.TabIndex = 12;
      this.label2.Text = "Select Product to Include";
      // 
      // radioButtonSewnOnLabels
      // 
      this.radioButtonSewnOnLabels.AutoSize = true;
      this.radioButtonSewnOnLabels.Location = new System.Drawing.Point(24, 77);
      this.radioButtonSewnOnLabels.Name = "radioButtonSewnOnLabels";
      this.radioButtonSewnOnLabels.Size = new System.Drawing.Size(103, 17);
      this.radioButtonSewnOnLabels.TabIndex = 10;
      this.radioButtonSewnOnLabels.TabStop = true;
      this.radioButtonSewnOnLabels.Text = "Sewn On Labels";
      this.radioButtonSewnOnLabels.UseVisualStyleBackColor = true;
      this.radioButtonSewnOnLabels.CheckedChanged += new System.EventHandler(this.radioButtonSewnOnLabels_CheckedChanged);
      // 
      // radioButtonIdentityLabels
      // 
      this.radioButtonIdentityLabels.AutoSize = true;
      this.radioButtonIdentityLabels.Location = new System.Drawing.Point(25, 99);
      this.radioButtonIdentityLabels.Name = "radioButtonIdentityLabels";
      this.radioButtonIdentityLabels.Size = new System.Drawing.Size(93, 17);
      this.radioButtonIdentityLabels.TabIndex = 11;
      this.radioButtonIdentityLabels.TabStop = true;
      this.radioButtonIdentityLabels.Text = "Identity Labels";
      this.radioButtonIdentityLabels.UseVisualStyleBackColor = true;
      this.radioButtonIdentityLabels.CheckedChanged += new System.EventHandler(this.radioButtonIdentityLabels_CheckedChanged);
      // 
      // FrmPrintSoDocumentsBatch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(492, 218);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.buttonClose);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FrmPrintSoDocumentsBatch";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Print So Documents - Batch";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Button buttonPrintCustomCoverOrders;
    private System.Windows.Forms.Button buttonPrintStockCoverOrders;
    private System.Windows.Forms.Button buttonPrintRepairOrders;
    private System.Windows.Forms.Button buttonPrintMiscellaneousOrders;
    private System.Windows.Forms.RadioButton radioButtonInvoices;
    private System.Windows.Forms.RadioButton radioButtonPackingLists;
    private System.Windows.Forms.RadioButton radioButtonWorkOrders;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.RadioButton radioButtonIdentityLabels;
    private System.Windows.Forms.RadioButton radioButtonSewnOnLabels;
  }
}