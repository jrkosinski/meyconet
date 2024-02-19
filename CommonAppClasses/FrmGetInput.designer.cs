namespace CommonAppClasses
{
  partial class FrmGetInput
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
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.labelRequest = new System.Windows.Forms.Label();
        this.textBoxData = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // buttonClose
        // 
        this.buttonClose.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonClose.Location = new System.Drawing.Point(116, 35);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 19);
        this.buttonClose.TabIndex = 2;
        this.buttonClose.Text = "Return";
        this.buttonClose.UseVisualStyleBackColor = false;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // labelRequest
        // 
        this.labelRequest.AutoSize = true;
        this.labelRequest.Location = new System.Drawing.Point(23, 9);
        this.labelRequest.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.labelRequest.Name = "labelRequest";
        this.labelRequest.Size = new System.Drawing.Size(127, 13);
        this.labelRequest.TabIndex = 3;
        this.labelRequest.Text = "Data Request Goes Here";
        // 
        // textBoxData
        // 
        this.textBoxData.Location = new System.Drawing.Point(26, 34);
        this.textBoxData.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxData.Name = "textBoxData";
        this.textBoxData.Size = new System.Drawing.Size(76, 20);
        this.textBoxData.TabIndex = 1;
        // 
        // FrmGetInput
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(247, 75);
        this.Controls.Add(this.textBoxData);
        this.Controls.Add(this.labelRequest);
        this.Controls.Add(this.buttonClose);
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmGetInput";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Enter Requested Data";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Label labelRequest;
    private System.Windows.Forms.TextBox textBoxData;
  }
}