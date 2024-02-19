namespace CommonAppClasses
{
   partial class frmMaintainComment
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
          this.textBoxComment = new System.Windows.Forms.TextBox();
          this.button1 = new WSGUtilitieslib.Telemetry.Button();
          this.button2 = new WSGUtilitieslib.Telemetry.Button();
          this.SuspendLayout();
          // 
          // textBoxComment
          // 
          this.textBoxComment.Location = new System.Drawing.Point(11, 43);
          this.textBoxComment.Margin = new System.Windows.Forms.Padding(2);
          this.textBoxComment.Multiline = true;
          this.textBoxComment.Name = "textBoxComment";
          this.textBoxComment.Size = new System.Drawing.Size(349, 215);
          this.textBoxComment.TabIndex = 0;
          // 
          // button1
          // 
          this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
          this.button1.Location = new System.Drawing.Point(228, 13);
          this.button1.Margin = new System.Windows.Forms.Padding(2);
          this.button1.Name = "button1";
          this.button1.Size = new System.Drawing.Size(56, 26);
          this.button1.TabIndex = 2;
          this.button1.Text = "Cancel";
          this.button1.UseVisualStyleBackColor = false;
          this.button1.Click += new System.EventHandler(this.button1_Click);
          // 
          // button2
          // 
          this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
          this.button2.Location = new System.Drawing.Point(300, 13);
          this.button2.Margin = new System.Windows.Forms.Padding(2);
          this.button2.Name = "button2";
          this.button2.Size = new System.Drawing.Size(56, 26);
          this.button2.TabIndex = 1;
          this.button2.Text = "Save";
          this.button2.UseVisualStyleBackColor = false;
          this.button2.Click += new System.EventHandler(this.button2_Click);
          // 
          // frmMaintainComment
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.LightSteelBlue;
          this.ClientSize = new System.Drawing.Size(371, 285);
          this.Controls.Add(this.button2);
          this.Controls.Add(this.button1);
          this.Controls.Add(this.textBoxComment);
          this.Name = "frmMaintainComment";
          this.Text = "Comment Maintenance";
          this.Load += new System.EventHandler(this.frmMaintainComment_Load);
          this.Shown += new System.EventHandler(this.frmMaintainComment_Shown);
          this.ResumeLayout(false);
          this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox textBoxComment;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button button2;
   }
}