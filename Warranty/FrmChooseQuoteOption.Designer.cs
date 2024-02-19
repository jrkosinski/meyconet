namespace Warranty
{
    partial class FrmChooseQuoteOption
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
            this.buttonCopyQuote = new WSGUtilitieslib.Telemetry.Button();
            this.buttonViewQuote = new WSGUtilitieslib.Telemetry.Button();
            this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
            this.SuspendLayout();
            // 
            // buttonCopyQuote
            // 
            this.buttonCopyQuote.Location = new System.Drawing.Point(26, 24);
            this.buttonCopyQuote.Name = "buttonCopyQuote";
            this.buttonCopyQuote.Size = new System.Drawing.Size(305, 23);
            this.buttonCopyQuote.TabIndex = 0;
            this.buttonCopyQuote.Text = "Copy Quote";
            this.buttonCopyQuote.UseVisualStyleBackColor = true;
            this.buttonCopyQuote.Click += new System.EventHandler(this.buttonCopyQuote_Click);
            // 
            // buttonViewQuote
            // 
            this.buttonViewQuote.Location = new System.Drawing.Point(26, 53);
            this.buttonViewQuote.Name = "buttonViewQuote";
            this.buttonViewQuote.Size = new System.Drawing.Size(305, 23);
            this.buttonViewQuote.TabIndex = 1;
            this.buttonViewQuote.Text = "View Quote";
            this.buttonViewQuote.UseVisualStyleBackColor = true;
            this.buttonViewQuote.Click += new System.EventHandler(this.buttonViewQuote_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(26, 82);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(305, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FrmChooseQuoteOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 161);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonViewQuote);
            this.Controls.Add(this.buttonCopyQuote);
            this.Name = "FrmChooseQuoteOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose Quote Option";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonCopyQuote;
        public System.Windows.Forms.Button buttonViewQuote;
        public System.Windows.Forms.Button buttonCancel;
    }
}