namespace Estimating
{
  partial class FrmProductSelector
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
            this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
            this.listBoxProducts = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(13, 187);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(56, 27);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listBoxProducts
            // 
            this.listBoxProducts.FormattingEnabled = true;
            this.listBoxProducts.Items.AddRange(new object[] {
            "Custom Cover",
            "Stock Cover",
            "Worksheet",
            "Custom Cover Other",
            "Non-Meyco Custom",
            "Non-Meyco Stock",
            "Spa Cover",
            "Kiddie Pool",
            "Wading Pool",
            "Catch Basin",
            "Cover Repair",
            "Cover Alteration"});
            this.listBoxProducts.Location = new System.Drawing.Point(11, 11);
            this.listBoxProducts.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxProducts.Name = "listBoxProducts";
            this.listBoxProducts.Size = new System.Drawing.Size(176, 173);
            this.listBoxProducts.TabIndex = 1;
            this.listBoxProducts.SelectedIndexChanged += new System.EventHandler(this.listBoxProducts_SelectedIndexChanged);
            // 
            // FrmProductSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(225, 223);
            this.Controls.Add(this.listBoxProducts);
            this.Controls.Add(this.buttonCancel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmProductSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a Product";
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.ListBox listBoxProducts;
  }
}