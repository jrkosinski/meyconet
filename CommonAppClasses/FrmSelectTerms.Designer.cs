namespace CommonAppClasses
{
    partial class FrmSelectTerms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectTerms));
            this.dataGridViewTermsSelector = new System.Windows.Forms.DataGridView();
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            this.ColumnTermid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPayterms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTermsSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTermsSelector
            // 
            this.dataGridViewTermsSelector.AllowUserToAddRows = false;
            this.dataGridViewTermsSelector.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTermsSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTermsSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTermsSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTermid,
            this.ColumnPayterms});
            this.dataGridViewTermsSelector.EnableHeadersVisualStyles = false;
            this.dataGridViewTermsSelector.Location = new System.Drawing.Point(12, 51);
            this.dataGridViewTermsSelector.Name = "dataGridViewTermsSelector";
            this.dataGridViewTermsSelector.RowHeadersVisible = false;
            this.dataGridViewTermsSelector.Size = new System.Drawing.Size(355, 150);
            this.dataGridViewTermsSelector.TabIndex = 0;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(263, 2);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(82, 33);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // ColumnTermid
            // 
            this.ColumnTermid.DataPropertyName = "termid";
            this.ColumnTermid.HeaderText = "Code";
            this.ColumnTermid.Name = "ColumnTermid";
            // 
            // ColumnPayterms
            // 
            this.ColumnPayterms.DataPropertyName = "payterms";
            this.ColumnPayterms.HeaderText = "Terms";
            this.ColumnPayterms.Name = "ColumnPayterms";
            this.ColumnPayterms.Width = 250;
            // 
            // FrmSelectTerms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 261);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.dataGridViewTermsSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSelectTerms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Terms";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTermsSelector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTermid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPayterms;
        public System.Windows.Forms.DataGridView dataGridViewTermsSelector;
        public System.Windows.Forms.Button buttonClose;
    }
}