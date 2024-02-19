namespace Print
{
  partial class FrmPrintSODocumentsIndividual
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintSODocumentsIndividual));
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxSono = new System.Windows.Forms.TextBox();
      this.buttonGenerate = new WSGUtilitieslib.Telemetry.Button();
      this.buttonGetSO = new WSGUtilitieslib.Telemetry.Button();
      this.labelSelectedSO = new System.Windows.Forms.Label();
      this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
      this.checkBoxOrder = new System.Windows.Forms.CheckBox();
      this.checkBoxInvoice = new System.Windows.Forms.CheckBox();
      this.checkBoxWorkOrder = new System.Windows.Forms.CheckBox();
      this.checkBoxPackingList = new System.Windows.Forms.CheckBox();
      this.groupBoxDocuments = new System.Windows.Forms.GroupBox();
      this.checkBoxIdentityLabel = new System.Windows.Forms.CheckBox();
      this.checkBoxSewnOnLabel = new System.Windows.Forms.CheckBox();
      this.groupBoxDocuments.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(410, 23);
      this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(56, 28);
      this.buttonClose.TabIndex = 0;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(30, 49);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(71, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "SO Number";
      // 
      // textBoxSono
      // 
      this.textBoxSono.Location = new System.Drawing.Point(103, 49);
      this.textBoxSono.Margin = new System.Windows.Forms.Padding(2);
      this.textBoxSono.Name = "textBoxSono";
      this.textBoxSono.Size = new System.Drawing.Size(84, 20);
      this.textBoxSono.TabIndex = 2;
      this.textBoxSono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSono_KeyDown);
      // 
      // buttonGenerate
      // 
      this.buttonGenerate.Location = new System.Drawing.Point(329, 23);
      this.buttonGenerate.Margin = new System.Windows.Forms.Padding(2);
      this.buttonGenerate.Name = "buttonGenerate";
      this.buttonGenerate.Size = new System.Drawing.Size(76, 28);
      this.buttonGenerate.TabIndex = 3;
      this.buttonGenerate.Text = "Generate";
      this.buttonGenerate.UseVisualStyleBackColor = true;
      this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
      // 
      // buttonGetSO
      // 
      this.buttonGetSO.Image = ((System.Drawing.Image)(resources.GetObject("buttonGetSO.Image")));
      this.buttonGetSO.Location = new System.Drawing.Point(211, 47);
      this.buttonGetSO.Margin = new System.Windows.Forms.Padding(2);
      this.buttonGetSO.Name = "buttonGetSO";
      this.buttonGetSO.Size = new System.Drawing.Size(24, 22);
      this.buttonGetSO.TabIndex = 201;
      this.buttonGetSO.UseVisualStyleBackColor = true;
      this.buttonGetSO.Click += new System.EventHandler(this.buttonGetSO_Click);
      // 
      // labelSelectedSO
      // 
      this.labelSelectedSO.AutoSize = true;
      this.labelSelectedSO.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelSelectedSO.Location = new System.Drawing.Point(73, 79);
      this.labelSelectedSO.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.labelSelectedSO.Name = "labelSelectedSO";
      this.labelSelectedSO.Size = new System.Drawing.Size(64, 13);
      this.labelSelectedSO.TabIndex = 210;
      this.labelSelectedSO.Text = "Select SO";
      // 
      // buttonClear
      // 
      this.buttonClear.Location = new System.Drawing.Point(268, 23);
      this.buttonClear.Margin = new System.Windows.Forms.Padding(2);
      this.buttonClear.Name = "buttonClear";
      this.buttonClear.Size = new System.Drawing.Size(56, 28);
      this.buttonClear.TabIndex = 211;
      this.buttonClear.Text = "Clear";
      this.buttonClear.UseVisualStyleBackColor = true;
      this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
      // 
      // checkBoxOrder
      // 
      this.checkBoxOrder.AutoSize = true;
      this.checkBoxOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.checkBoxOrder.Location = new System.Drawing.Point(23, 31);
      this.checkBoxOrder.Name = "checkBoxOrder";
      this.checkBoxOrder.Size = new System.Drawing.Size(131, 20);
      this.checkBoxOrder.TabIndex = 212;
      this.checkBoxOrder.Text = "Order/Estimate";
      this.checkBoxOrder.UseVisualStyleBackColor = true;
      // 
      // checkBoxInvoice
      // 
      this.checkBoxInvoice.AutoSize = true;
      this.checkBoxInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.checkBoxInvoice.Location = new System.Drawing.Point(23, 57);
      this.checkBoxInvoice.Name = "checkBoxInvoice";
      this.checkBoxInvoice.Size = new System.Drawing.Size(77, 20);
      this.checkBoxInvoice.TabIndex = 213;
      this.checkBoxInvoice.Text = "Invoice";
      this.checkBoxInvoice.UseVisualStyleBackColor = true;
      // 
      // checkBoxWorkOrder
      // 
      this.checkBoxWorkOrder.AutoSize = true;
      this.checkBoxWorkOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.checkBoxWorkOrder.Location = new System.Drawing.Point(23, 83);
      this.checkBoxWorkOrder.Name = "checkBoxWorkOrder";
      this.checkBoxWorkOrder.Size = new System.Drawing.Size(106, 20);
      this.checkBoxWorkOrder.TabIndex = 214;
      this.checkBoxWorkOrder.Text = "Work Order";
      this.checkBoxWorkOrder.UseVisualStyleBackColor = true;
      // 
      // checkBoxPackingList
      // 
      this.checkBoxPackingList.AutoSize = true;
      this.checkBoxPackingList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.checkBoxPackingList.Location = new System.Drawing.Point(23, 109);
      this.checkBoxPackingList.Name = "checkBoxPackingList";
      this.checkBoxPackingList.Size = new System.Drawing.Size(111, 20);
      this.checkBoxPackingList.TabIndex = 215;
      this.checkBoxPackingList.Text = "Packing List";
      this.checkBoxPackingList.UseVisualStyleBackColor = true;
      // 
      // groupBoxDocuments
      // 
      this.groupBoxDocuments.Controls.Add(this.checkBoxSewnOnLabel);
      this.groupBoxDocuments.Controls.Add(this.checkBoxIdentityLabel);
      this.groupBoxDocuments.Controls.Add(this.checkBoxPackingList);
      this.groupBoxDocuments.Controls.Add(this.checkBoxWorkOrder);
      this.groupBoxDocuments.Controls.Add(this.checkBoxInvoice);
      this.groupBoxDocuments.Controls.Add(this.checkBoxOrder);
      this.groupBoxDocuments.Location = new System.Drawing.Point(53, 111);
      this.groupBoxDocuments.Name = "groupBoxDocuments";
      this.groupBoxDocuments.Size = new System.Drawing.Size(181, 201);
      this.groupBoxDocuments.TabIndex = 216;
      this.groupBoxDocuments.TabStop = false;
      this.groupBoxDocuments.Text = "Select Documents";
      // 
      // checkBoxIdentityLabel
      // 
      this.checkBoxIdentityLabel.AutoSize = true;
      this.checkBoxIdentityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.checkBoxIdentityLabel.Location = new System.Drawing.Point(22, 135);
      this.checkBoxIdentityLabel.Name = "checkBoxIdentityLabel";
      this.checkBoxIdentityLabel.Size = new System.Drawing.Size(120, 20);
      this.checkBoxIdentityLabel.TabIndex = 216;
      this.checkBoxIdentityLabel.Text = "Identity Label";
      this.checkBoxIdentityLabel.UseVisualStyleBackColor = true;
      // 
      // checkBoxSewnOnLabel
      // 
      this.checkBoxSewnOnLabel.AutoSize = true;
      this.checkBoxSewnOnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.checkBoxSewnOnLabel.Location = new System.Drawing.Point(23, 160);
      this.checkBoxSewnOnLabel.Name = "checkBoxSewnOnLabel";
      this.checkBoxSewnOnLabel.Size = new System.Drawing.Size(130, 20);
      this.checkBoxSewnOnLabel.TabIndex = 217;
      this.checkBoxSewnOnLabel.Text = "Sewn On Label";
      this.checkBoxSewnOnLabel.UseVisualStyleBackColor = true;
      // 
      // FrmPrintSODocumentsIndividual
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(574, 332);
      this.Controls.Add(this.groupBoxDocuments);
      this.Controls.Add(this.buttonClear);
      this.Controls.Add(this.labelSelectedSO);
      this.Controls.Add(this.buttonGetSO);
      this.Controls.Add(this.buttonGenerate);
      this.Controls.Add(this.textBoxSono);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonClose);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "FrmPrintSODocumentsIndividual";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Print SO Documents";
      this.groupBoxDocuments.ResumeLayout(false);
      this.groupBoxDocuments.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxSono;
    private System.Windows.Forms.Button buttonGenerate;
    private System.Windows.Forms.Button buttonGetSO;
    private System.Windows.Forms.Label labelSelectedSO;
    private System.Windows.Forms.Button buttonClear;
    private System.Windows.Forms.CheckBox checkBoxOrder;
    private System.Windows.Forms.CheckBox checkBoxInvoice;
    private System.Windows.Forms.CheckBox checkBoxWorkOrder;
    private System.Windows.Forms.CheckBox checkBoxPackingList;
    private System.Windows.Forms.GroupBox groupBoxDocuments;
    private System.Windows.Forms.CheckBox checkBoxIdentityLabel;
    private System.Windows.Forms.CheckBox checkBoxSewnOnLabel;
  }
}