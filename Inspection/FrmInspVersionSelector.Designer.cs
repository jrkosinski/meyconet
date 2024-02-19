namespace Inspection
{
  partial class FrmInspVersionSelector
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInspVersionSelector));
      this.dataGridViewVersionSelector = new System.Windows.Forms.DataGridView();
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      this.ColumnVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnDescrip = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVersionSelector)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridViewVersionSelector
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewVersionSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridViewVersionSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewVersionSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnVersion,
            this.ColumnDescrip});
      this.dataGridViewVersionSelector.EnableHeadersVisualStyles = false;
      this.dataGridViewVersionSelector.Location = new System.Drawing.Point(37, 51);
      this.dataGridViewVersionSelector.Name = "dataGridViewVersionSelector";
      this.dataGridViewVersionSelector.RowHeadersVisible = false;
      this.dataGridViewVersionSelector.RowTemplate.Height = 24;
      this.dataGridViewVersionSelector.Size = new System.Drawing.Size(381, 250);
      this.dataGridViewVersionSelector.TabIndex = 0;
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(282, 11);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(75, 34);
      this.buttonClose.TabIndex = 1;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      // 
      // ColumnVersion
      // 
      this.ColumnVersion.DataPropertyName = "version";
      this.ColumnVersion.HeaderText = "Option";
      this.ColumnVersion.Name = "ColumnVersion";
      // 
      // ColumnDescrip
      // 
      this.ColumnDescrip.DataPropertyName = "linedescrip";
      this.ColumnDescrip.HeaderText = "Description";
      this.ColumnDescrip.Name = "ColumnDescrip";
      this.ColumnDescrip.Width = 275;
      // 
      // FrmInspVersionSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(465, 336);
      this.Controls.Add(this.buttonClose);
      this.Controls.Add(this.dataGridViewVersionSelector);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FrmInspVersionSelector";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Option Selector";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVersionSelector)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.Button buttonClose;
    public System.Windows.Forms.DataGridView dataGridViewVersionSelector;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVersion;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescrip;
  }
}