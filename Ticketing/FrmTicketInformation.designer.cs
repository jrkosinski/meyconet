namespace Ticketing
{
    partial class FrmTicketInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTicketInformation));
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            this.buttonContact = new WSGUtilitieslib.Telemetry.Button();
            this.buttonAddNote = new WSGUtilitieslib.Telemetry.Button();
            this.textBoxTicketnotes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LabelContact = new System.Windows.Forms.Label();
            this.buttonConfirmwithemail = new WSGUtilitieslib.Telemetry.Button();
            this.buttonConfirmwithtext = new WSGUtilitieslib.Telemetry.Button();
            this.labelCustno = new System.Windows.Forms.Label();
            this.labelSono = new System.Windows.Forms.Label();
            this.labelPonum = new System.Windows.Forms.Label();
            this.buttonEdit = new WSGUtilitieslib.Telemetry.Button();
            this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDepartment = new System.Windows.Forms.TextBox();
            this.comboBoxDepartment = new System.Windows.Forms.ComboBox();
            this.comboBoxCommvia = new System.Windows.Forms.ComboBox();
            this.textBoxCommvia = new System.Windows.Forms.TextBox();
            this.groupBoxTicketStatus = new System.Windows.Forms.GroupBox();
            this.radioButtonCancelled = new System.Windows.Forms.RadioButton();
            this.radioButtonClosed = new System.Windows.Forms.RadioButton();
            this.radioButtonOpen = new System.Windows.Forms.RadioButton();
            this.buttonContactDetails = new WSGUtilitieslib.Telemetry.Button();
            this.checkBoxConfirmContent = new System.Windows.Forms.CheckBox();
            this.groupBoxTicketStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(469, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonContact
            // 
            this.buttonContact.Location = new System.Drawing.Point(13, 66);
            this.buttonContact.Name = "buttonContact";
            this.buttonContact.Size = new System.Drawing.Size(123, 23);
            this.buttonContact.TabIndex = 1;
            this.buttonContact.Text = "Select Contact";
            this.buttonContact.UseVisualStyleBackColor = true;
            // 
            // buttonAddNote
            // 
            this.buttonAddNote.Location = new System.Drawing.Point(127, 12);
            this.buttonAddNote.Name = "buttonAddNote";
            this.buttonAddNote.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNote.TabIndex = 2;
            this.buttonAddNote.Text = "Add Note";
            this.buttonAddNote.UseVisualStyleBackColor = true;
            // 
            // textBoxTicketnotes
            // 
            this.textBoxTicketnotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTicketnotes.Location = new System.Drawing.Point(203, 169);
            this.textBoxTicketnotes.Multiline = true;
            this.textBoxTicketnotes.Name = "textBoxTicketnotes";
            this.textBoxTicketnotes.Size = new System.Drawing.Size(425, 301);
            this.textBoxTicketnotes.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(200, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Notes";
            // 
            // LabelContact
            // 
            this.LabelContact.AutoSize = true;
            this.LabelContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelContact.Location = new System.Drawing.Point(17, 52);
            this.LabelContact.Name = "LabelContact";
            this.LabelContact.Size = new System.Drawing.Size(119, 13);
            this.LabelContact.TabIndex = 7;
            this.LabelContact.Text = "Communicated With";
            // 
            // buttonConfirmwithemail
            // 
            this.buttonConfirmwithemail.Location = new System.Drawing.Point(458, 75);
            this.buttonConfirmwithemail.Name = "buttonConfirmwithemail";
            this.buttonConfirmwithemail.Size = new System.Drawing.Size(112, 23);
            this.buttonConfirmwithemail.TabIndex = 10;
            this.buttonConfirmwithemail.Text = "Confirm With Email";
            this.buttonConfirmwithemail.UseVisualStyleBackColor = true;
            // 
            // buttonConfirmwithtext
            // 
            this.buttonConfirmwithtext.Location = new System.Drawing.Point(458, 99);
            this.buttonConfirmwithtext.Name = "buttonConfirmwithtext";
            this.buttonConfirmwithtext.Size = new System.Drawing.Size(112, 23);
            this.buttonConfirmwithtext.TabIndex = 11;
            this.buttonConfirmwithtext.Text = "Confirm With Text";
            this.buttonConfirmwithtext.UseVisualStyleBackColor = true;
            // 
            // labelCustno
            // 
            this.labelCustno.AutoSize = true;
            this.labelCustno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustno.Location = new System.Drawing.Point(18, 132);
            this.labelCustno.Name = "labelCustno";
            this.labelCustno.Size = new System.Drawing.Size(46, 13);
            this.labelCustno.TabIndex = 15;
            this.labelCustno.Text = "Custno";
            // 
            // labelSono
            // 
            this.labelSono.AutoSize = true;
            this.labelSono.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSono.Location = new System.Drawing.Point(18, 151);
            this.labelSono.Name = "labelSono";
            this.labelSono.Size = new System.Drawing.Size(36, 13);
            this.labelSono.TabIndex = 16;
            this.labelSono.Text = "Sono";
            // 
            // labelPonum
            // 
            this.labelPonum.AutoSize = true;
            this.labelPonum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPonum.Location = new System.Drawing.Point(17, 172);
            this.labelPonum.Name = "labelPonum";
            this.labelPonum.Size = new System.Drawing.Size(45, 13);
            this.labelPonum.TabIndex = 17;
            this.labelPonum.Text = "Ponum";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(241, 12);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 18;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(355, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 21;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(336, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Contacted Via";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(226, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Department";
            // 
            // textBoxDepartment
            // 
            this.textBoxDepartment.Location = new System.Drawing.Point(203, 78);
            this.textBoxDepartment.Name = "textBoxDepartment";
            this.textBoxDepartment.ReadOnly = true;
            this.textBoxDepartment.Size = new System.Drawing.Size(119, 20);
            this.textBoxDepartment.TabIndex = 28;
            // 
            // comboBoxDepartment
            // 
            this.comboBoxDepartment.FormattingEnabled = true;
            this.comboBoxDepartment.Location = new System.Drawing.Point(202, 103);
            this.comboBoxDepartment.Name = "comboBoxDepartment";
            this.comboBoxDepartment.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDepartment.TabIndex = 27;
            // 
            // comboBoxCommvia
            // 
            this.comboBoxCommvia.FormattingEnabled = true;
            this.comboBoxCommvia.Location = new System.Drawing.Point(336, 103);
            this.comboBoxCommvia.Name = "comboBoxCommvia";
            this.comboBoxCommvia.Size = new System.Drawing.Size(107, 21);
            this.comboBoxCommvia.TabIndex = 30;
            // 
            // textBoxCommvia
            // 
            this.textBoxCommvia.Location = new System.Drawing.Point(339, 78);
            this.textBoxCommvia.Name = "textBoxCommvia";
            this.textBoxCommvia.ReadOnly = true;
            this.textBoxCommvia.Size = new System.Drawing.Size(100, 20);
            this.textBoxCommvia.TabIndex = 31;
            // 
            // groupBoxTicketStatus
            // 
            this.groupBoxTicketStatus.Controls.Add(this.radioButtonCancelled);
            this.groupBoxTicketStatus.Controls.Add(this.radioButtonClosed);
            this.groupBoxTicketStatus.Controls.Add(this.radioButtonOpen);
            this.groupBoxTicketStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTicketStatus.Location = new System.Drawing.Point(16, 206);
            this.groupBoxTicketStatus.Name = "groupBoxTicketStatus";
            this.groupBoxTicketStatus.Size = new System.Drawing.Size(101, 100);
            this.groupBoxTicketStatus.TabIndex = 33;
            this.groupBoxTicketStatus.TabStop = false;
            this.groupBoxTicketStatus.Text = "Ticket Status";
            // 
            // radioButtonCancelled
            // 
            this.radioButtonCancelled.AutoSize = true;
            this.radioButtonCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCancelled.Location = new System.Drawing.Point(9, 67);
            this.radioButtonCancelled.Name = "radioButtonCancelled";
            this.radioButtonCancelled.Size = new System.Drawing.Size(72, 17);
            this.radioButtonCancelled.TabIndex = 35;
            this.radioButtonCancelled.TabStop = true;
            this.radioButtonCancelled.Text = "Cancelled";
            this.radioButtonCancelled.UseVisualStyleBackColor = true;
            // 
            // radioButtonClosed
            // 
            this.radioButtonClosed.AutoSize = true;
            this.radioButtonClosed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonClosed.Location = new System.Drawing.Point(9, 42);
            this.radioButtonClosed.Name = "radioButtonClosed";
            this.radioButtonClosed.Size = new System.Drawing.Size(57, 17);
            this.radioButtonClosed.TabIndex = 34;
            this.radioButtonClosed.TabStop = true;
            this.radioButtonClosed.Text = "Closed";
            this.radioButtonClosed.UseVisualStyleBackColor = true;
            // 
            // radioButtonOpen
            // 
            this.radioButtonOpen.AutoSize = true;
            this.radioButtonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonOpen.Location = new System.Drawing.Point(9, 17);
            this.radioButtonOpen.Name = "radioButtonOpen";
            this.radioButtonOpen.Size = new System.Drawing.Size(51, 17);
            this.radioButtonOpen.TabIndex = 33;
            this.radioButtonOpen.TabStop = true;
            this.radioButtonOpen.Text = "Open";
            this.radioButtonOpen.UseVisualStyleBackColor = true;
            // 
            // buttonContactDetails
            // 
            this.buttonContactDetails.Location = new System.Drawing.Point(13, 92);
            this.buttonContactDetails.Name = "buttonContactDetails";
            this.buttonContactDetails.Size = new System.Drawing.Size(123, 23);
            this.buttonContactDetails.TabIndex = 34;
            this.buttonContactDetails.Text = "Contact Details";
            this.buttonContactDetails.UseVisualStyleBackColor = true;
            // 
            // checkBoxConfirmContent
            // 
            this.checkBoxConfirmContent.AutoSize = true;
            this.checkBoxConfirmContent.Location = new System.Drawing.Point(458, 128);
            this.checkBoxConfirmContent.Name = "checkBoxConfirmContent";
            this.checkBoxConfirmContent.Size = new System.Drawing.Size(138, 17);
            this.checkBoxConfirmContent.TabIndex = 35;
            this.checkBoxConfirmContent.Text = "Send Current Note Only";
            this.checkBoxConfirmContent.UseVisualStyleBackColor = true;
            // 
            // FrmTicketInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 482);
            this.Controls.Add(this.checkBoxConfirmContent);
            this.Controls.Add(this.buttonContactDetails);
            this.Controls.Add(this.groupBoxTicketStatus);
            this.Controls.Add(this.textBoxCommvia);
            this.Controls.Add(this.comboBoxCommvia);
            this.Controls.Add(this.textBoxDepartment);
            this.Controls.Add(this.comboBoxDepartment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.labelPonum);
            this.Controls.Add(this.labelSono);
            this.Controls.Add(this.labelCustno);
            this.Controls.Add(this.buttonConfirmwithtext);
            this.Controls.Add(this.buttonConfirmwithemail);
            this.Controls.Add(this.LabelContact);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTicketnotes);
            this.Controls.Add(this.buttonAddNote);
            this.Controls.Add(this.buttonContact);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTicketInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ticket Information";
            this.groupBoxTicketStatus.ResumeLayout(false);
            this.groupBoxTicketStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxTicketnotes;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label LabelContact;
        public System.Windows.Forms.Button buttonConfirmwithemail;
        public System.Windows.Forms.Button buttonConfirmwithtext;
        public System.Windows.Forms.Label labelCustno;
        public System.Windows.Forms.Label labelSono;
        public System.Windows.Forms.Label labelPonum;
        public System.Windows.Forms.Button buttonEdit;
        public System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.Button buttonClose;
        public System.Windows.Forms.Button buttonContact;
        public System.Windows.Forms.Button buttonAddNote;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBoxDepartment;
        public System.Windows.Forms.ComboBox comboBoxDepartment;
        public System.Windows.Forms.ComboBox comboBoxCommvia;
        public System.Windows.Forms.TextBox textBoxCommvia;
        public System.Windows.Forms.GroupBox groupBoxTicketStatus;
        public System.Windows.Forms.RadioButton radioButtonCancelled;
        public System.Windows.Forms.RadioButton radioButtonClosed;
        public System.Windows.Forms.RadioButton radioButtonOpen;
        public System.Windows.Forms.Button buttonContactDetails;
        public System.Windows.Forms.CheckBox checkBoxConfirmContent;


    }
}