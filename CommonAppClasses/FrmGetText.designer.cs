namespace CommonAppClasses
{
  partial class FrmGetText
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
      this.textBoxContent = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(463, 12);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(97, 39);
      this.buttonClose.TabIndex = 0;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
      // 
      // labelRequest
      // 
      this.labelRequest.AutoSize = true;
      this.labelRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelRequest.Location = new System.Drawing.Point(135, 52);
      this.labelRequest.Name = "labelRequest";
      this.labelRequest.Size = new System.Drawing.Size(52, 17);
      this.labelRequest.TabIndex = 1;
      this.labelRequest.Text = "label1";
      // 
      // textBoxContent
      // 
      this.textBoxContent.Location = new System.Drawing.Point(75, 88);
      this.textBoxContent.Multiline = true;
      this.textBoxContent.Name = "textBoxContent";
      this.textBoxContent.Size = new System.Drawing.Size(485, 248);
      this.textBoxContent.TabIndex = 2;
      // 
      // FrmGetText
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(648, 387);
      this.Controls.Add(this.textBoxContent);
      this.Controls.Add(this.labelRequest);
      this.Controls.Add(this.buttonClose);
      this.Name = "FrmGetText";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Enter Requested Information";
      this.Shown += new System.EventHandler(this.FrmGetText_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Label labelRequest;
    private System.Windows.Forms.TextBox textBoxContent;
  }
}