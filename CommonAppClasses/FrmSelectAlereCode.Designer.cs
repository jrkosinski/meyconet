namespace CommonAppClasses
{
    partial class FrmSelectAlereCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectAlereCode));
            this.dataGridViewCodeSelector = new System.Windows.Forms.DataGridView();
            this.ColumnTermid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPayterms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCodeSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCodeSelector
            // 
            this.dataGridViewCodeSelector.AllowUserToAddRows = false;
            this.dataGridViewCodeSelector.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCodeSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCodeSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCodeSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTermid,
            this.ColumnPayterms});
            this.dataGridViewCodeSelector.EnableHeadersVisualStyles = false;
            this.dataGridViewCodeSelector.Location = new System.Drawing.Point(17, 61);
            this.dataGridViewCodeSelector.Name = "dataGridViewCodeSelector";
            this.dataGridViewCodeSelector.RowHeadersVisible = false;
            this.dataGridViewCodeSelector.Size = new System.Drawing.Size(355, 229);
            this.dataGridViewCodeSelector.TabIndex = 1;
            // 
            // ColumnTermid
            // 
            this.ColumnTermid.DataPropertyName = "codename";
            this.ColumnTermid.HeaderText = "Code";
            this.ColumnTermid.Name = "ColumnTermid";
            // 
            // ColumnPayterms
            // 
            this.ColumnPayterms.DataPropertyName = "codedesc";
            this.ColumnPayterms.HeaderText = "Description";
            this.ColumnPayterms.Name = "ColumnPayterms";
            this.ColumnPayterms.Width = 250;
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(278, 11);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(82, 33);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // FrmSelectAlereCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 302);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.dataGridViewCodeSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSelectAlereCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Alere Code";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCodeSelector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewCodeSelector;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTermid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPayterms;
        public System.Windows.Forms.Button buttonClose;
    }
}