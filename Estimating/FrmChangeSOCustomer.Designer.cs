namespace Estimating
{
    partial class FrmChangeSOCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChangeSOCustomer));
            this.buttonSO = new WSGUtilitieslib.Telemetry.Button();
            this.labelSono = new System.Windows.Forms.Label();
            this.labelCustno = new System.Windows.Forms.Label();
            this.buttonCustomer = new WSGUtilitieslib.Telemetry.Button();
            this.buttonProceed = new WSGUtilitieslib.Telemetry.Button();
            this.buttonReturn = new WSGUtilitieslib.Telemetry.Button();
            this.SuspendLayout();
            // 
            // buttonSO
            // 
            this.buttonSO.Location = new System.Drawing.Point(155, 58);
            this.buttonSO.Name = "buttonSO";
            this.buttonSO.Size = new System.Drawing.Size(75, 23);
            this.buttonSO.TabIndex = 0;
            this.buttonSO.Text = "Select SO";
            this.buttonSO.UseVisualStyleBackColor = true;
            // 
            // labelSono
            // 
            this.labelSono.AutoSize = true;
            this.labelSono.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSono.Location = new System.Drawing.Point(25, 66);
            this.labelSono.Name = "labelSono";
            this.labelSono.Size = new System.Drawing.Size(64, 13);
            this.labelSono.TabIndex = 1;
            this.labelSono.Text = "Select SO";
            // 
            // labelCustno
            // 
            this.labelCustno.AutoSize = true;
            this.labelCustno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustno.Location = new System.Drawing.Point(23, 102);
            this.labelCustno.Name = "labelCustno";
            this.labelCustno.Size = new System.Drawing.Size(113, 13);
            this.labelCustno.TabIndex = 4;
            this.labelCustno.Text = "Select New Dealer";
            // 
            // buttonCustomer
            // 
            this.buttonCustomer.Location = new System.Drawing.Point(152, 96);
            this.buttonCustomer.Name = "buttonCustomer";
            this.buttonCustomer.Size = new System.Drawing.Size(103, 23);
            this.buttonCustomer.TabIndex = 3;
            this.buttonCustomer.Text = "Select Customer";
            this.buttonCustomer.UseVisualStyleBackColor = true;
            // 
            // buttonProceed
            // 
            this.buttonProceed.Location = new System.Drawing.Point(77, 12);
            this.buttonProceed.Name = "buttonProceed";
            this.buttonProceed.Size = new System.Drawing.Size(91, 23);
            this.buttonProceed.TabIndex = 5;
            this.buttonProceed.Text = "Proceed";
            this.buttonProceed.UseVisualStyleBackColor = true;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(203, 12);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(91, 23);
            this.buttonReturn.TabIndex = 6;
            this.buttonReturn.Text = "Return";
            this.buttonReturn.UseVisualStyleBackColor = true;
            // 
            // FrmChangeSOCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 189);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.buttonProceed);
            this.Controls.Add(this.labelCustno);
            this.Controls.Add(this.buttonCustomer);
            this.Controls.Add(this.labelSono);
            this.Controls.Add(this.buttonSO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmChangeSOCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change SO Dealer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonSO;
        public System.Windows.Forms.Label labelSono;
        public System.Windows.Forms.Label labelCustno;
        public System.Windows.Forms.Button buttonCustomer;
        public System.Windows.Forms.Button buttonProceed;
        public System.Windows.Forms.Button buttonReturn;
    }
}