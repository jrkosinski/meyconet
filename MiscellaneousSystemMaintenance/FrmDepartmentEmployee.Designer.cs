namespace MiscellaneousSystemMaintenance
{
    partial class FrmDepartmentEmployee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDepartmentEmployee));
            this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
            this.listBoxUnassigned = new System.Windows.Forms.ListBox();
            this.listBoxAssigned = new System.Windows.Forms.ListBox();
            this.buttonReturn = new WSGUtilitieslib.Telemetry.Button();
            this.listBoxDepartment = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(350, 22);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // listBoxUnassigned
            // 
            this.listBoxUnassigned.FormattingEnabled = true;
            this.listBoxUnassigned.Location = new System.Drawing.Point(19, 259);
            this.listBoxUnassigned.Name = "listBoxUnassigned";
            this.listBoxUnassigned.Size = new System.Drawing.Size(194, 199);
            this.listBoxUnassigned.TabIndex = 2;
            // 
            // listBoxAssigned
            // 
            this.listBoxAssigned.FormattingEnabled = true;
            this.listBoxAssigned.Location = new System.Drawing.Point(242, 259);
            this.listBoxAssigned.Name = "listBoxAssigned";
            this.listBoxAssigned.Size = new System.Drawing.Size(194, 199);
            this.listBoxAssigned.TabIndex = 3;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(269, 22);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(75, 23);
            this.buttonReturn.TabIndex = 5;
            this.buttonReturn.Text = "Return";
            this.buttonReturn.UseVisualStyleBackColor = true;
            // 
            // listBoxDepartment
            // 
            this.listBoxDepartment.FormattingEnabled = true;
            this.listBoxDepartment.Location = new System.Drawing.Point(132, 80);
            this.listBoxDepartment.Name = "listBoxDepartment";
            this.listBoxDepartment.Size = new System.Drawing.Size(194, 160);
            this.listBoxDepartment.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(187, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Department";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Unassigned";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(312, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Assigned";
            // 
            // FrmDepartmentEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 478);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxDepartment);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.listBoxAssigned);
            this.Controls.Add(this.listBoxUnassigned);
            this.Controls.Add(this.buttonSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDepartmentEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Department Employees";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.ListBox listBoxUnassigned;
        public System.Windows.Forms.ListBox listBoxAssigned;
        public System.Windows.Forms.Button buttonReturn;
        public System.Windows.Forms.ListBox listBoxDepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

    }
}