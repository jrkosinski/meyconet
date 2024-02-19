namespace CommonAppClasses
{
  partial class FrmGetSono
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetSono));
        this.textBoxSono = new System.Windows.Forms.TextBox();
        this.buttonGetSO = new WSGUtilitieslib.Telemetry.Button();
        this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
        this.SuspendLayout();
        // 
        // textBoxSono
        // 
        this.textBoxSono.Location = new System.Drawing.Point(38, 28);
        this.textBoxSono.Name = "textBoxSono";
        this.textBoxSono.Size = new System.Drawing.Size(154, 20);
        this.textBoxSono.TabIndex = 0;
        // 
        // buttonGetSO
        // 
        this.buttonGetSO.Image = ((System.Drawing.Image)(resources.GetObject("buttonGetSO.Image")));
        this.buttonGetSO.Location = new System.Drawing.Point(211, 28);
        this.buttonGetSO.Margin = new System.Windows.Forms.Padding(2);
        this.buttonGetSO.Name = "buttonGetSO";
        this.buttonGetSO.Size = new System.Drawing.Size(24, 22);
        this.buttonGetSO.TabIndex = 201;
        this.buttonGetSO.UseVisualStyleBackColor = true;
        // 
        // buttonCancel
        // 
        this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonCancel.Location = new System.Drawing.Point(253, 28);
        this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(71, 22);
        this.buttonCancel.TabIndex = 202;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = false;
        // 
        // FrmGetSono
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.CancelButton = this.buttonCancel;
        this.ClientSize = new System.Drawing.Size(376, 88);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.buttonGetSO);
        this.Controls.Add(this.textBoxSono);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "FrmGetSono";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Select Sales Order";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.TextBox textBoxSono;
    public System.Windows.Forms.Button buttonGetSO;
    public System.Windows.Forms.Button buttonCancel;

  }
}