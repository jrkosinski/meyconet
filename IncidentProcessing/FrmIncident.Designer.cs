namespace IncidentProcessing
{
    partial class FrmIncident
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIncident));
         this.buttonReturn = new WSGUtilitieslib.Telemetry.Button();
         this.buttonDelete = new WSGUtilitieslib.Telemetry.Button();
         this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
         this.dateTimePickerIncidentdate = new System.Windows.Forms.DateTimePicker();
         this.labelIncidentdate = new System.Windows.Forms.Label();
         this.labelSono = new System.Windows.Forms.Label();
         this.buttonSelectso = new WSGUtilitieslib.Telemetry.Button();
         this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
         this.comboBoxIssue = new System.Windows.Forms.ComboBox();
         this.textBoxIssue = new System.Windows.Forms.TextBox();
         this.labelIssue = new System.Windows.Forms.Label();
         this.labelRootcause = new System.Windows.Forms.Label();
         this.textBoxRootcause = new System.Windows.Forms.TextBox();
         this.comboBoxRootcause = new System.Windows.Forms.ComboBox();
         this.labelResolution = new System.Windows.Forms.Label();
         this.textBoxResolution = new System.Windows.Forms.TextBox();
         this.comboBoxResolution = new System.Windows.Forms.ComboBox();
         this.labelFindingdept = new System.Windows.Forms.Label();
         this.textBoxFindingdept = new System.Windows.Forms.TextBox();
         this.comboBoxFindingdept = new System.Windows.Forms.ComboBox();
         this.labelEmployee = new System.Windows.Forms.Label();
         this.textBoxEmployee = new System.Windows.Forms.TextBox();
         this.comboBoxEmployee = new System.Windows.Forms.ComboBox();
         this.labelCausingdept = new System.Windows.Forms.Label();
         this.textBoxCausingdept = new System.Windows.Forms.TextBox();
         this.comboBoxCausingdept = new System.Windows.Forms.ComboBox();
         this.textBoxCost = new System.Windows.Forms.TextBox();
         this.textBoxNotes = new System.Windows.Forms.TextBox();
         this.labelFixCost = new System.Windows.Forms.Label();
         this.labelCustno = new System.Windows.Forms.Label();
         this.labelSovalue = new System.Windows.Forms.Label();
         this.labelNotes = new System.Windows.Forms.Label();
         this.labelUsers = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // buttonReturn
         // 
         this.buttonReturn.Location = new System.Drawing.Point(255, 12);
         this.buttonReturn.Name = "buttonReturn";
         this.buttonReturn.Size = new System.Drawing.Size(53, 23);
         this.buttonReturn.TabIndex = 11;
         this.buttonReturn.Text = "Return";
         this.buttonReturn.UseVisualStyleBackColor = true;
         // 
         // buttonDelete
         // 
         this.buttonDelete.Location = new System.Drawing.Point(366, 12);
         this.buttonDelete.Name = "buttonDelete";
         this.buttonDelete.Size = new System.Drawing.Size(59, 23);
         this.buttonDelete.TabIndex = 12;
         this.buttonDelete.Text = "Delete";
         this.buttonDelete.UseVisualStyleBackColor = true;
         // 
         // buttonSave
         // 
         this.buttonSave.Location = new System.Drawing.Point(430, 12);
         this.buttonSave.Name = "buttonSave";
         this.buttonSave.Size = new System.Drawing.Size(62, 23);
         this.buttonSave.TabIndex = 13;
         this.buttonSave.Text = "Save";
         this.buttonSave.UseVisualStyleBackColor = true;
         // 
         // dateTimePickerIncidentdate
         // 
         this.dateTimePickerIncidentdate.CustomFormat = "mm/dd/yyyy";
         this.dateTimePickerIncidentdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
         this.dateTimePickerIncidentdate.Location = new System.Drawing.Point(157, 87);
         this.dateTimePickerIncidentdate.Name = "dateTimePickerIncidentdate";
         this.dateTimePickerIncidentdate.Size = new System.Drawing.Size(88, 20);
         this.dateTimePickerIncidentdate.TabIndex = 14;
         // 
         // labelIncidentdate
         // 
         this.labelIncidentdate.AutoSize = true;
         this.labelIncidentdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelIncidentdate.Location = new System.Drawing.Point(64, 91);
         this.labelIncidentdate.Name = "labelIncidentdate";
         this.labelIncidentdate.Size = new System.Drawing.Size(84, 13);
         this.labelIncidentdate.TabIndex = 15;
         this.labelIncidentdate.Text = "Incident Date";
         // 
         // labelSono
         // 
         this.labelSono.AutoSize = true;
         this.labelSono.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelSono.Location = new System.Drawing.Point(68, 38);
         this.labelSono.Name = "labelSono";
         this.labelSono.Size = new System.Drawing.Size(71, 13);
         this.labelSono.TabIndex = 16;
         this.labelSono.Text = "SO Number";
         // 
         // buttonSelectso
         // 
         this.buttonSelectso.Location = new System.Drawing.Point(21, 12);
         this.buttonSelectso.Name = "buttonSelectso";
         this.buttonSelectso.Size = new System.Drawing.Size(136, 23);
         this.buttonSelectso.TabIndex = 17;
         this.buttonSelectso.Text = "Select Sales Order";
         this.buttonSelectso.UseVisualStyleBackColor = true;
         // 
         // buttonCancel
         // 
         this.buttonCancel.Location = new System.Drawing.Point(312, 12);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(53, 23);
         this.buttonCancel.TabIndex = 18;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.UseVisualStyleBackColor = true;
         // 
         // comboBoxIssue
         // 
         this.comboBoxIssue.FormattingEnabled = true;
         this.comboBoxIssue.Location = new System.Drawing.Point(64, 181);
         this.comboBoxIssue.Name = "comboBoxIssue";
         this.comboBoxIssue.Size = new System.Drawing.Size(139, 21);
         this.comboBoxIssue.TabIndex = 19;
         // 
         // textBoxIssue
         // 
         this.textBoxIssue.Location = new System.Drawing.Point(64, 159);
         this.textBoxIssue.Name = "textBoxIssue";
         this.textBoxIssue.ReadOnly = true;
         this.textBoxIssue.Size = new System.Drawing.Size(140, 20);
         this.textBoxIssue.TabIndex = 20;
         // 
         // labelIssue
         // 
         this.labelIssue.AutoSize = true;
         this.labelIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelIssue.Location = new System.Drawing.Point(64, 141);
         this.labelIssue.Name = "labelIssue";
         this.labelIssue.Size = new System.Drawing.Size(120, 13);
         this.labelIssue.TabIndex = 21;
         this.labelIssue.Text = "What Type Of Issue";
         // 
         // labelRootcause
         // 
         this.labelRootcause.AutoSize = true;
         this.labelRootcause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelRootcause.Location = new System.Drawing.Point(229, 141);
         this.labelRootcause.Name = "labelRootcause";
         this.labelRootcause.Size = new System.Drawing.Size(73, 13);
         this.labelRootcause.TabIndex = 24;
         this.labelRootcause.Text = "Root Cause";
         // 
         // textBoxRootcause
         // 
         this.textBoxRootcause.Location = new System.Drawing.Point(229, 159);
         this.textBoxRootcause.Name = "textBoxRootcause";
         this.textBoxRootcause.ReadOnly = true;
         this.textBoxRootcause.Size = new System.Drawing.Size(140, 20);
         this.textBoxRootcause.TabIndex = 23;
         // 
         // comboBoxRootcause
         // 
         this.comboBoxRootcause.FormattingEnabled = true;
         this.comboBoxRootcause.Location = new System.Drawing.Point(229, 181);
         this.comboBoxRootcause.Name = "comboBoxRootcause";
         this.comboBoxRootcause.Size = new System.Drawing.Size(139, 21);
         this.comboBoxRootcause.TabIndex = 22;
         // 
         // labelResolution
         // 
         this.labelResolution.AutoSize = true;
         this.labelResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelResolution.Location = new System.Drawing.Point(425, 142);
         this.labelResolution.Name = "labelResolution";
         this.labelResolution.Size = new System.Drawing.Size(156, 13);
         this.labelResolution.TabIndex = 27;
         this.labelResolution.Text = "What Was The Resolution";
         // 
         // textBoxResolution
         // 
         this.textBoxResolution.Location = new System.Drawing.Point(425, 159);
         this.textBoxResolution.Name = "textBoxResolution";
         this.textBoxResolution.ReadOnly = true;
         this.textBoxResolution.Size = new System.Drawing.Size(140, 20);
         this.textBoxResolution.TabIndex = 26;
         // 
         // comboBoxResolution
         // 
         this.comboBoxResolution.FormattingEnabled = true;
         this.comboBoxResolution.Location = new System.Drawing.Point(425, 181);
         this.comboBoxResolution.Name = "comboBoxResolution";
         this.comboBoxResolution.Size = new System.Drawing.Size(139, 21);
         this.comboBoxResolution.TabIndex = 25;
         // 
         // labelFindingdept
         // 
         this.labelFindingdept.AutoSize = true;
         this.labelFindingdept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelFindingdept.Location = new System.Drawing.Point(64, 239);
         this.labelFindingdept.Name = "labelFindingdept";
         this.labelFindingdept.Size = new System.Drawing.Size(138, 13);
         this.labelFindingdept.TabIndex = 30;
         this.labelFindingdept.Text = "What Dept Found Error";
         // 
         // textBoxFindingdept
         // 
         this.textBoxFindingdept.Location = new System.Drawing.Point(64, 256);
         this.textBoxFindingdept.Name = "textBoxFindingdept";
         this.textBoxFindingdept.ReadOnly = true;
         this.textBoxFindingdept.Size = new System.Drawing.Size(140, 20);
         this.textBoxFindingdept.TabIndex = 29;
         // 
         // comboBoxFindingdept
         // 
         this.comboBoxFindingdept.FormattingEnabled = true;
         this.comboBoxFindingdept.Location = new System.Drawing.Point(64, 278);
         this.comboBoxFindingdept.Name = "comboBoxFindingdept";
         this.comboBoxFindingdept.Size = new System.Drawing.Size(139, 21);
         this.comboBoxFindingdept.TabIndex = 28;
         // 
         // labelEmployee
         // 
         this.labelEmployee.AutoSize = true;
         this.labelEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelEmployee.Location = new System.Drawing.Point(425, 238);
         this.labelEmployee.Name = "labelEmployee";
         this.labelEmployee.Size = new System.Drawing.Size(192, 13);
         this.labelEmployee.TabIndex = 33;
         this.labelEmployee.Text = "What Operator Was Responsible";
         // 
         // textBoxEmployee
         // 
         this.textBoxEmployee.Location = new System.Drawing.Point(425, 255);
         this.textBoxEmployee.Name = "textBoxEmployee";
         this.textBoxEmployee.ReadOnly = true;
         this.textBoxEmployee.Size = new System.Drawing.Size(140, 20);
         this.textBoxEmployee.TabIndex = 32;
         // 
         // comboBoxEmployee
         // 
         this.comboBoxEmployee.FormattingEnabled = true;
         this.comboBoxEmployee.Location = new System.Drawing.Point(425, 277);
         this.comboBoxEmployee.Name = "comboBoxEmployee";
         this.comboBoxEmployee.Size = new System.Drawing.Size(139, 21);
         this.comboBoxEmployee.TabIndex = 31;
         // 
         // labelCausingdept
         // 
         this.labelCausingdept.AutoSize = true;
         this.labelCausingdept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelCausingdept.Location = new System.Drawing.Point(229, 239);
         this.labelCausingdept.Name = "labelCausingdept";
         this.labelCausingdept.Size = new System.Drawing.Size(170, 13);
         this.labelCausingdept.TabIndex = 36;
         this.labelCausingdept.Text = "What Dept Was Responsible";
         // 
         // textBoxCausingdept
         // 
         this.textBoxCausingdept.Location = new System.Drawing.Point(229, 256);
         this.textBoxCausingdept.Name = "textBoxCausingdept";
         this.textBoxCausingdept.ReadOnly = true;
         this.textBoxCausingdept.Size = new System.Drawing.Size(140, 20);
         this.textBoxCausingdept.TabIndex = 35;
         // 
         // comboBoxCausingdept
         // 
         this.comboBoxCausingdept.FormattingEnabled = true;
         this.comboBoxCausingdept.Location = new System.Drawing.Point(229, 278);
         this.comboBoxCausingdept.Name = "comboBoxCausingdept";
         this.comboBoxCausingdept.Size = new System.Drawing.Size(139, 21);
         this.comboBoxCausingdept.TabIndex = 34;
         // 
         // textBoxCost
         // 
         this.textBoxCost.Location = new System.Drawing.Point(157, 114);
         this.textBoxCost.Name = "textBoxCost";
         this.textBoxCost.Size = new System.Drawing.Size(100, 20);
         this.textBoxCost.TabIndex = 37;
         this.textBoxCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // textBoxNotes
         // 
         this.textBoxNotes.Location = new System.Drawing.Point(64, 330);
         this.textBoxNotes.Multiline = true;
         this.textBoxNotes.Name = "textBoxNotes";
         this.textBoxNotes.Size = new System.Drawing.Size(500, 74);
         this.textBoxNotes.TabIndex = 38;
         // 
         // labelFixCost
         // 
         this.labelFixCost.AutoSize = true;
         this.labelFixCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelFixCost.Location = new System.Drawing.Point(64, 116);
         this.labelFixCost.Name = "labelFixCost";
         this.labelFixCost.Size = new System.Drawing.Size(86, 13);
         this.labelFixCost.TabIndex = 39;
         this.labelFixCost.Text = "Cost Of Fixing";
         // 
         // labelCustno
         // 
         this.labelCustno.AutoSize = true;
         this.labelCustno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelCustno.Location = new System.Drawing.Point(178, 38);
         this.labelCustno.Name = "labelCustno";
         this.labelCustno.Size = new System.Drawing.Size(46, 13);
         this.labelCustno.TabIndex = 40;
         this.labelCustno.Text = "Custno";
         // 
         // labelSovalue
         // 
         this.labelSovalue.AutoSize = true;
         this.labelSovalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelSovalue.Location = new System.Drawing.Point(305, 38);
         this.labelSovalue.Name = "labelSovalue";
         this.labelSovalue.Size = new System.Drawing.Size(60, 13);
         this.labelSovalue.TabIndex = 41;
         this.labelSovalue.Text = "SO Value";
         // 
         // labelNotes
         // 
         this.labelNotes.AutoSize = true;
         this.labelNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelNotes.Location = new System.Drawing.Point(64, 310);
         this.labelNotes.Name = "labelNotes";
         this.labelNotes.Size = new System.Drawing.Size(40, 13);
         this.labelNotes.TabIndex = 42;
         this.labelNotes.Text = "Notes";
         // 
         // labelUsers
         // 
         this.labelUsers.AutoSize = true;
         this.labelUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelUsers.Location = new System.Drawing.Point(64, 68);
         this.labelUsers.Name = "labelUsers";
         this.labelUsers.Size = new System.Drawing.Size(104, 13);
         this.labelUsers.TabIndex = 43;
         this.labelUsers.Text = "User Information ";
         // 
         // FrmIncident
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(633, 458);
         this.Controls.Add(this.labelUsers);
         this.Controls.Add(this.labelNotes);
         this.Controls.Add(this.labelSovalue);
         this.Controls.Add(this.labelCustno);
         this.Controls.Add(this.labelFixCost);
         this.Controls.Add(this.textBoxNotes);
         this.Controls.Add(this.textBoxCost);
         this.Controls.Add(this.labelCausingdept);
         this.Controls.Add(this.textBoxCausingdept);
         this.Controls.Add(this.comboBoxCausingdept);
         this.Controls.Add(this.labelEmployee);
         this.Controls.Add(this.textBoxEmployee);
         this.Controls.Add(this.comboBoxEmployee);
         this.Controls.Add(this.labelFindingdept);
         this.Controls.Add(this.textBoxFindingdept);
         this.Controls.Add(this.comboBoxFindingdept);
         this.Controls.Add(this.labelResolution);
         this.Controls.Add(this.textBoxResolution);
         this.Controls.Add(this.comboBoxResolution);
         this.Controls.Add(this.labelRootcause);
         this.Controls.Add(this.textBoxRootcause);
         this.Controls.Add(this.comboBoxRootcause);
         this.Controls.Add(this.labelIssue);
         this.Controls.Add(this.textBoxIssue);
         this.Controls.Add(this.comboBoxIssue);
         this.Controls.Add(this.buttonCancel);
         this.Controls.Add(this.buttonSelectso);
         this.Controls.Add(this.labelSono);
         this.Controls.Add(this.labelIncidentdate);
         this.Controls.Add(this.dateTimePickerIncidentdate);
         this.Controls.Add(this.buttonSave);
         this.Controls.Add(this.buttonDelete);
         this.Controls.Add(this.buttonReturn);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "FrmIncident";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Incident Information";
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonReturn;
        public System.Windows.Forms.Button buttonDelete;
        public System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.DateTimePicker dateTimePickerIncidentdate;
        public System.Windows.Forms.Label labelIncidentdate;
        public System.Windows.Forms.Label labelSono;
        public System.Windows.Forms.Button buttonSelectso;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Label labelIssue;
        public System.Windows.Forms.Label labelRootcause;
        public System.Windows.Forms.Label labelResolution;
        public System.Windows.Forms.Label labelFindingdept;
        public System.Windows.Forms.Label labelEmployee;
        public System.Windows.Forms.ComboBox comboBoxIssue;
        public System.Windows.Forms.TextBox textBoxIssue;
        public System.Windows.Forms.TextBox textBoxRootcause;
        public System.Windows.Forms.ComboBox comboBoxRootcause;
        public System.Windows.Forms.TextBox textBoxResolution;
        public System.Windows.Forms.ComboBox comboBoxResolution;
        public System.Windows.Forms.TextBox textBoxFindingdept;
        public System.Windows.Forms.ComboBox comboBoxFindingdept;
        public System.Windows.Forms.TextBox textBoxEmployee;
        public System.Windows.Forms.ComboBox comboBoxEmployee;
        public System.Windows.Forms.Label labelCausingdept;
        public System.Windows.Forms.TextBox textBoxCausingdept;
        public System.Windows.Forms.ComboBox comboBoxCausingdept;
        public System.Windows.Forms.TextBox textBoxCost;
        public System.Windows.Forms.TextBox textBoxNotes;
        public System.Windows.Forms.Label labelFixCost;
        public System.Windows.Forms.Label labelCustno;
        public System.Windows.Forms.Label labelSovalue;
        public System.Windows.Forms.Label labelNotes;
        public System.Windows.Forms.Label labelUsers;

    }
}

