namespace Estimating
{
  partial class FrmSelectVersion
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
        this.dataGridViewVersionSelector = new System.Windows.Forms.DataGridView();
        this.ColumnVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.material = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.color = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Descrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.buttonCancel = new WSGUtilitieslib.Telemetry.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVersionSelector)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridViewVersionSelector
        // 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridViewVersionSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridViewVersionSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewVersionSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnVersion,
            this.Column1,
            this.material,
            this.color,
            this.Descrip});
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridViewVersionSelector.DefaultCellStyle = dataGridViewCellStyle2;
        this.dataGridViewVersionSelector.EnableHeadersVisualStyles = false;
        this.dataGridViewVersionSelector.Location = new System.Drawing.Point(26, 48);
        this.dataGridViewVersionSelector.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridViewVersionSelector.Name = "dataGridViewVersionSelector";
        this.dataGridViewVersionSelector.ReadOnly = true;
        this.dataGridViewVersionSelector.RowHeadersVisible = false;
        this.dataGridViewVersionSelector.RowTemplate.Height = 24;
        this.dataGridViewVersionSelector.Size = new System.Drawing.Size(744, 162);
        this.dataGridViewVersionSelector.TabIndex = 0;
        this.dataGridViewVersionSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVersionSelector_CellContentDoubleClick);
        this.dataGridViewVersionSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewVersionSelector_KeyDown);
        // 
        // ColumnVersion
        // 
        this.ColumnVersion.DataPropertyName = "Version";
        this.ColumnVersion.HeaderText = "Version";
        this.ColumnVersion.Name = "ColumnVersion";
        this.ColumnVersion.ReadOnly = true;
        this.ColumnVersion.Width = 50;
        // 
        // Column1
        // 
        this.Column1.DataPropertyName = "cover";
        this.Column1.HeaderText = "Cover";
        this.Column1.Name = "Column1";
        this.Column1.ReadOnly = true;
        this.Column1.Width = 50;
        // 
        // material
        // 
        this.material.DataPropertyName = "material";
        this.material.HeaderText = "Material";
        this.material.Name = "material";
        this.material.ReadOnly = true;
        this.material.Width = 150;
        // 
        // color
        // 
        this.color.DataPropertyName = "color";
        this.color.HeaderText = "Color";
        this.color.Name = "color";
        this.color.ReadOnly = true;
        this.color.Width = 150;
        // 
        // Descrip
        // 
        this.Descrip.DataPropertyName = "descrip";
        this.Descrip.HeaderText = "Description";
        this.Descrip.Name = "Descrip";
        this.Descrip.ReadOnly = true;
        this.Descrip.Width = 300;
        // 
        // buttonCancel
        // 
        this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
        this.buttonCancel.Location = new System.Drawing.Point(26, 11);
        this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(70, 26);
        this.buttonCancel.TabIndex = 1;
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.UseVisualStyleBackColor = false;
        this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
        // 
        // FrmSelectVersion
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(791, 234);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.dataGridViewVersionSelector);
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmSelectVersion";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Versions";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVersionSelector)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewVersionSelector;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVersion;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn material;
    private System.Windows.Forms.DataGridViewTextBoxColumn color;
    private System.Windows.Forms.DataGridViewTextBoxColumn Descrip;
  }
}