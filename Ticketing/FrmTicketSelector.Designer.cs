namespace Ticketing
{
    partial class FrmTicketSelector
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTicketSelector));
            this.dataGridViewTicketSelector = new System.Windows.Forms.DataGridView();
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            this.buttonAddTicket = new WSGUtilitieslib.Telemetry.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTicketInformation = new System.Windows.Forms.Label();
            this.ColumnIdcol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTicketnotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTicketstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicketSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTicketSelector
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTicketSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTicketSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTicketSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIdcol,
            this.ColumnDeptName,
            this.ColumnTicketnotes,
            this.ColumnTicketstatus,
            this.ColumnDateTime,
            this.ColumnUser});
            this.dataGridViewTicketSelector.EnableHeadersVisualStyles = false;
            this.dataGridViewTicketSelector.Location = new System.Drawing.Point(27, 155);
            this.dataGridViewTicketSelector.Name = "dataGridViewTicketSelector";
            this.dataGridViewTicketSelector.RowHeadersVisible = false;
            this.dataGridViewTicketSelector.Size = new System.Drawing.Size(754, 263);
            this.dataGridViewTicketSelector.TabIndex = 0;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(595, 32);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(87, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonAddTicket
            // 
            this.buttonAddTicket.Location = new System.Drawing.Point(502, 32);
            this.buttonAddTicket.Name = "buttonAddTicket";
            this.buttonAddTicket.Size = new System.Drawing.Size(87, 23);
            this.buttonAddTicket.TabIndex = 2;
            this.buttonAddTicket.Text = "Add Ticket";
            this.buttonAddTicket.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "DoubleClick For More Details ";
            // 
            // labelTicketInformation
            // 
            this.labelTicketInformation.AutoSize = true;
            this.labelTicketInformation.Location = new System.Drawing.Point(166, 71);
            this.labelTicketInformation.Name = "labelTicketInformation";
            this.labelTicketInformation.Size = new System.Drawing.Size(114, 13);
            this.labelTicketInformation.TabIndex = 4;
            this.labelTicketInformation.Text = "Ticket Owner Data";
            // 
            // ColumnIdcol
            // 
            this.ColumnIdcol.DataPropertyName = "sequno";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnIdcol.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnIdcol.HeaderText = "Ticket #";
            this.ColumnIdcol.Name = "ColumnIdcol";
            this.ColumnIdcol.Width = 50;
            // 
            // ColumnDeptName
            // 
            this.ColumnDeptName.DataPropertyName = "department";
            this.ColumnDeptName.HeaderText = "Department";
            this.ColumnDeptName.Name = "ColumnDeptName";
            // 
            // ColumnTicketnotes
            // 
            this.ColumnTicketnotes.DataPropertyName = "ticketnotes";
            this.ColumnTicketnotes.HeaderText = "Notes";
            this.ColumnTicketnotes.Name = "ColumnTicketnotes";
            this.ColumnTicketnotes.Width = 250;
            // 
            // ColumnTicketstatus
            // 
            this.ColumnTicketstatus.DataPropertyName = "ticketstatus";
            this.ColumnTicketstatus.HeaderText = "Status";
            this.ColumnTicketstatus.Name = "ColumnTicketstatus";
            // 
            // ColumnDateTime
            // 
            this.ColumnDateTime.DataPropertyName = "adddate";
            this.ColumnDateTime.HeaderText = "Date/Time";
            this.ColumnDateTime.Name = "ColumnDateTime";
            this.ColumnDateTime.Width = 150;
            // 
            // ColumnUser
            // 
            this.ColumnUser.DataPropertyName = "adduser";
            this.ColumnUser.HeaderText = "User";
            this.ColumnUser.Name = "ColumnUser";
            // 
            // FrmTicketSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 465);
            this.Controls.Add(this.labelTicketInformation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddTicket);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.dataGridViewTicketSelector);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTicketSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ticket Selector";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicketSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewTicketSelector;
        public System.Windows.Forms.Button buttonClose;
        public System.Windows.Forms.Button buttonAddTicket;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label labelTicketInformation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIdcol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTicketnotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTicketstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUser;
    }
}