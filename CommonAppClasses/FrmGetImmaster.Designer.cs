namespace CommonAppClasses
{
    partial class FrmGetImmaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetImmaster));
            this.dataGridViewGetImmaster = new System.Windows.Forms.DataGridView();
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            this.ColumnItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrcdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaterial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGetImmaster)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewGetImmaster
            // 
            this.dataGridViewGetImmaster.AllowUserToAddRows = false;
            this.dataGridViewGetImmaster.AllowUserToDeleteRows = false;
            this.dataGridViewGetImmaster.AllowUserToOrderColumns = true;
            this.dataGridViewGetImmaster.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewGetImmaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewGetImmaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGetImmaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnItem,
            this.ColumnPrcdesc,
            this.ColumnItemdesc,
            this.ColumnColor,
            this.ColumnMaterial,
            this.ColumnPrice});
            this.dataGridViewGetImmaster.EnableHeadersVisualStyles = false;
            this.dataGridViewGetImmaster.Location = new System.Drawing.Point(28, 51);
            this.dataGridViewGetImmaster.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewGetImmaster.Name = "dataGridViewGetImmaster";
            this.dataGridViewGetImmaster.ReadOnly = true;
            this.dataGridViewGetImmaster.RowHeadersVisible = false;
            this.dataGridViewGetImmaster.RowTemplate.Height = 24;
            this.dataGridViewGetImmaster.Size = new System.Drawing.Size(677, 197);
            this.dataGridViewGetImmaster.TabIndex = 1;
            this.dataGridViewGetImmaster.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewGetImmaster_CellContentDoubleClick);
            this.dataGridViewGetImmaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewGetImmaster_KeyDown);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonClose.Location = new System.Drawing.Point(637, 11);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(56, 26);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ColumnItem
            // 
            this.ColumnItem.DataPropertyName = "Item";
            this.ColumnItem.HeaderText = "Item";
            this.ColumnItem.Name = "ColumnItem";
            this.ColumnItem.ReadOnly = true;
            this.ColumnItem.Width = 125;
            // 
            // ColumnPrcdesc
            // 
            this.ColumnPrcdesc.DataPropertyName = "prcdesc";
            this.ColumnPrcdesc.HeaderText = "Price Description";
            this.ColumnPrcdesc.Name = "ColumnPrcdesc";
            this.ColumnPrcdesc.ReadOnly = true;
            this.ColumnPrcdesc.Width = 200;
            // 
            // ColumnItemdesc
            // 
            this.ColumnItemdesc.DataPropertyName = "descrip";
            this.ColumnItemdesc.HeaderText = "Description";
            this.ColumnItemdesc.Name = "ColumnItemdesc";
            this.ColumnItemdesc.ReadOnly = true;
            this.ColumnItemdesc.Width = 275;
            // 
            // ColumnColor
            // 
            this.ColumnColor.DataPropertyName = "color";
            this.ColumnColor.HeaderText = "Color";
            this.ColumnColor.Name = "ColumnColor";
            this.ColumnColor.ReadOnly = true;
            // 
            // ColumnMaterial
            // 
            this.ColumnMaterial.DataPropertyName = "material";
            this.ColumnMaterial.HeaderText = "Material";
            this.ColumnMaterial.Name = "ColumnMaterial";
            this.ColumnMaterial.ReadOnly = true;
            // 
            // ColumnPrice
            // 
            this.ColumnPrice.DataPropertyName = "price";
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnPrice.HeaderText = "Price";
            this.ColumnPrice.Name = "ColumnPrice";
            this.ColumnPrice.ReadOnly = true;
            // 
            // FrmGetImmaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(736, 276);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.dataGridViewGetImmaster);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmGetImmaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Item";
            this.Shown += new System.EventHandler(this.FrmGetImmaster_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGetImmaster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewGetImmaster;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrcdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaterial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrice;
    }
}