namespace CommonAppClasses
{
  partial class FrmSelector
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dataGridViewSelector = new System.Windows.Forms.DataGridView();
      this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelector)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridViewSelector
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridViewSelector.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dataGridViewSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewSelector.EnableHeadersVisualStyles = false;
      this.dataGridViewSelector.Location = new System.Drawing.Point(53, 64);
      this.dataGridViewSelector.Name = "dataGridViewSelector";
      this.dataGridViewSelector.ReadOnly = true;
      this.dataGridViewSelector.Size = new System.Drawing.Size(351, 227);
      this.dataGridViewSelector.TabIndex = 3;
      // 
      // buttonClose
      // 
      this.buttonClose.Location = new System.Drawing.Point(53, 24);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(75, 24);
      this.buttonClose.TabIndex = 4;
      this.buttonClose.Text = "Close";
      this.buttonClose.UseVisualStyleBackColor = true;
      // 
      // FrmSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(478, 351);
      this.Controls.Add(this.dataGridViewSelector);
      this.Controls.Add(this.buttonClose);
      this.Name = "FrmSelector";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "FrmSelector";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelector)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.DataGridView dataGridViewSelector;
    public System.Windows.Forms.Button buttonClose;
  }
}