namespace Contacts
{
    partial class FrmContactSelector
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContactSelector));
            this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
            this.buttonInsert = new WSGUtilitieslib.Telemetry.Button();
            this.dataGridViewContactlist = new System.Windows.Forms.DataGridView();
            this.ColumnContactname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnContactphone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnContactemail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContactlist)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(591, 23);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(493, 23);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 2;
            this.buttonInsert.Text = "Insert";
            this.buttonInsert.UseVisualStyleBackColor = true;
            // 
            // dataGridViewContactlist
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewContactlist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewContactlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewContactlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnContactname,
            this.ColumnContactphone,
            this.ColumnContactemail});
            this.dataGridViewContactlist.EnableHeadersVisualStyles = false;
            this.dataGridViewContactlist.Location = new System.Drawing.Point(61, 79);
            this.dataGridViewContactlist.Name = "dataGridViewContactlist";
            this.dataGridViewContactlist.RowHeadersVisible = false;
            this.dataGridViewContactlist.Size = new System.Drawing.Size(705, 199);
            this.dataGridViewContactlist.TabIndex = 3;
            // 
            // ColumnContactname
            // 
            this.ColumnContactname.DataPropertyName = "contactname";
            this.ColumnContactname.HeaderText = "Contact Name";
            this.ColumnContactname.Name = "ColumnContactname";
            this.ColumnContactname.Width = 250;
            // 
            // ColumnContactphone
            // 
            this.ColumnContactphone.DataPropertyName = "contactphone";
            this.ColumnContactphone.HeaderText = "Phone";
            this.ColumnContactphone.Name = "ColumnContactphone";
            this.ColumnContactphone.Width = 150;
            // 
            // ColumnContactemail
            // 
            this.ColumnContactemail.DataPropertyName = "contactemail";
            this.ColumnContactemail.HeaderText = "Email";
            this.ColumnContactemail.Name = "ColumnContactemail";
            this.ColumnContactemail.Width = 300;
            // 
            // FrmContactSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 357);
            this.Controls.Add(this.dataGridViewContactlist);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmContactSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contact Selector";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContactlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewContactlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnContactname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnContactphone;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnContactemail;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonInsert;
    }
}