namespace BusinessProcessing
{
   partial class FrmLogin
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
          this.label1 = new System.Windows.Forms.Label();
          this.buttonProceed = new WSGUtilitieslib.Telemetry.Button();
          this.textBoxPassword = new System.Windows.Forms.TextBox();
          this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
          this.textBoxUserId = new System.Windows.Forms.TextBox();
          this.label2 = new System.Windows.Forms.Label();
          this.SuspendLayout();
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.Location = new System.Drawing.Point(51, 17);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(50, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "User ID";
          // 
          // buttonProceed
          // 
          this.buttonProceed.BackColor = System.Drawing.SystemColors.ButtonFace;
          this.buttonProceed.Location = new System.Drawing.Point(39, 94);
          this.buttonProceed.Name = "buttonProceed";
          this.buttonProceed.Size = new System.Drawing.Size(96, 32);
          this.buttonProceed.TabIndex = 2;
          this.buttonProceed.Text = "Proceed";
          this.buttonProceed.UseVisualStyleBackColor = false;
          this.buttonProceed.Click += new System.EventHandler(this.buttonProceed_Click);
          // 
          // textBoxPassword
          // 
          this.textBoxPassword.Location = new System.Drawing.Point(123, 52);
          this.textBoxPassword.MaxLength = 4;
          this.textBoxPassword.Name = "textBoxPassword";
          this.textBoxPassword.PasswordChar = '*';
          this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
          this.textBoxPassword.TabIndex = 1;
          this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyDown);
          // 
          // buttonCancel
          // 
          this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
          this.buttonCancel.Location = new System.Drawing.Point(160, 94);
          this.buttonCancel.Name = "buttonCancel";
          this.buttonCancel.Size = new System.Drawing.Size(96, 32);
          this.buttonCancel.TabIndex = 3;
          this.buttonCancel.Text = "Cancel";
          this.buttonCancel.UseVisualStyleBackColor = false;
          this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
          // 
          // textBoxUserId
          // 
          this.textBoxUserId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
          this.textBoxUserId.Location = new System.Drawing.Point(123, 12);
          this.textBoxUserId.MaxLength = 4;
          this.textBoxUserId.Name = "textBoxUserId";
          this.textBoxUserId.Size = new System.Drawing.Size(64, 20);
          this.textBoxUserId.TabIndex = 0;
          this.textBoxUserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUserId_KeyDown);
          this.textBoxUserId.Leave += new System.EventHandler(this.textBoxUserId_Leave);
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label2.Location = new System.Drawing.Point(36, 57);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(61, 13);
          this.label2.TabIndex = 5;
          this.label2.Text = "Password";
          // 
          // FrmLogin
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
          this.BackColor = System.Drawing.Color.LightSteelBlue;
          this.ClientSize = new System.Drawing.Size(339, 157);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.textBoxUserId);
          this.Controls.Add(this.buttonCancel);
          this.Controls.Add(this.textBoxPassword);
          this.Controls.Add(this.buttonProceed);
          this.Controls.Add(this.label1);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.Name = "FrmLogin";
          this.Text = "Log In";
          this.ResumeLayout(false);
          this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button buttonProceed;
      private System.Windows.Forms.TextBox textBoxPassword;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.TextBox textBoxUserId;
      private System.Windows.Forms.Label label2;
   }
}