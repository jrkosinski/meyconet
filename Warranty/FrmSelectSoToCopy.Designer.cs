namespace Warranty
{
    partial class FrmSelectSoToCopy
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
            this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
            this.dataGridViewSoToCopy = new System.Windows.Forms.DataGridView();
            this.ColumnSoNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSoDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSoToCopy)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(409, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSoToCopy
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSoToCopy.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSoToCopy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSoToCopy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSoNo,
            this.ColumnSoDate,
            this.ColumnCompany});
            this.dataGridViewSoToCopy.EnableHeadersVisualStyles = false;
            this.dataGridViewSoToCopy.Location = new System.Drawing.Point(33, 64);
            this.dataGridViewSoToCopy.Name = "dataGridViewSoToCopy";
            this.dataGridViewSoToCopy.RowHeadersVisible = false;
            this.dataGridViewSoToCopy.Size = new System.Drawing.Size(454, 150);
            this.dataGridViewSoToCopy.TabIndex = 1;
            // 
            // ColumnSoNo
            // 
            this.ColumnSoNo.DataPropertyName = "sono";
            this.ColumnSoNo.HeaderText = "SO Number";
            this.ColumnSoNo.Name = "ColumnSoNo";
            // 
            // ColumnSoDate
            // 
            this.ColumnSoDate.DataPropertyName = "sodate";
            this.ColumnSoDate.HeaderText = "SO Date";
            this.ColumnSoDate.Name = "ColumnSoDate";
            // 
            // ColumnCompany
            // 
            this.ColumnCompany.DataPropertyName = "company";
            this.ColumnCompany.HeaderText = "Dealer";
            this.ColumnCompany.Name = "ColumnCompany";
            this.ColumnCompany.Width = 250;
            // 
            // FrmSelectSoToCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 284);
            this.Controls.Add(this.dataGridViewSoToCopy);
            this.Controls.Add(this.buttonCancel);
            this.Name = "FrmSelectSoToCopy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select SO To Copy";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSoToCopy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.DataGridView dataGridViewSoToCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSoNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSoDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompany;
    }
}