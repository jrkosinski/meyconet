namespace Contacts

{
    partial class FrmContactInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContactInformation));
            this.textBoxContactname = new System.Windows.Forms.TextBox();
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
            this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
            this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxContactPhone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxContactext = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxContactemail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxCarrier = new System.Windows.Forms.ListBox();
            this.labelCustomername = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxContactnotes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxContactname
            // 
            this.textBoxContactname.Location = new System.Drawing.Point(150, 117);
            this.textBoxContactname.Name = "textBoxContactname";
            this.textBoxContactname.Size = new System.Drawing.Size(362, 20);
            this.textBoxContactname.TabIndex = 0;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(518, 51);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(406, 51);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(285, 51);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(163, 51);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Phone";
            // 
            // textBoxContactPhone
            // 
            this.textBoxContactPhone.Location = new System.Drawing.Point(150, 144);
            this.textBoxContactPhone.Name = "textBoxContactPhone";
            this.textBoxContactPhone.Size = new System.Drawing.Size(208, 20);
            this.textBoxContactPhone.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(390, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ext";
            // 
            // textBoxContactext
            // 
            this.textBoxContactext.Location = new System.Drawing.Point(430, 145);
            this.textBoxContactext.Name = "textBoxContactext";
            this.textBoxContactext.Size = new System.Drawing.Size(36, 20);
            this.textBoxContactext.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(46, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Email Address";
            // 
            // textBoxContactemail
            // 
            this.textBoxContactemail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxContactemail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxContactemail.Location = new System.Drawing.Point(152, 177);
            this.textBoxContactemail.Name = "textBoxContactemail";
            this.textBoxContactemail.Size = new System.Drawing.Size(391, 20);
            this.textBoxContactemail.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(537, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Carrier";
            // 
            // listBoxCarrier
            // 
            this.listBoxCarrier.FormattingEnabled = true;
            this.listBoxCarrier.Items.AddRange(new object[] {
            "Verizon",
            "Sprint",
            "TMobile",
            "ATT"});
            this.listBoxCarrier.Location = new System.Drawing.Point(587, 144);
            this.listBoxCarrier.Name = "listBoxCarrier";
            this.listBoxCarrier.Size = new System.Drawing.Size(111, 69);
            this.listBoxCarrier.TabIndex = 14;
            // 
            // labelCustomername
            // 
            this.labelCustomername.AutoSize = true;
            this.labelCustomername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomername.Location = new System.Drawing.Point(156, 89);
            this.labelCustomername.Name = "labelCustomername";
            this.labelCustomername.Size = new System.Drawing.Size(95, 13);
            this.labelCustomername.TabIndex = 15;
            this.labelCustomername.Text = "Customer Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(47, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Notes";
            // 
            // textBoxContactnotes
            // 
            this.textBoxContactnotes.Location = new System.Drawing.Point(153, 208);
            this.textBoxContactnotes.Multiline = true;
            this.textBoxContactnotes.Name = "textBoxContactnotes";
            this.textBoxContactnotes.Size = new System.Drawing.Size(391, 61);
            this.textBoxContactnotes.TabIndex = 16;
            // 
            // FrmContactInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 302);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxContactnotes);
            this.Controls.Add(this.labelCustomername);
            this.Controls.Add(this.listBoxCarrier);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxContactemail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxContactext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxContactPhone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxContactname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmContactInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contact Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelCustomername;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxContactname;
        public System.Windows.Forms.Button buttonClose;
        public System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.Button buttonEdit;
        public System.Windows.Forms.Button buttonDelete;
        public System.Windows.Forms.TextBox textBoxContactPhone;
        public System.Windows.Forms.TextBox textBoxContactext;
        public System.Windows.Forms.TextBox textBoxContactemail;
        public System.Windows.Forms.ListBox listBoxCarrier;
        public System.Windows.Forms.TextBox textBoxContactnotes;
    }
}