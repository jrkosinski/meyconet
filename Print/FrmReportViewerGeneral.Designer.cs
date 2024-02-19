namespace Print
{
  partial class FrmReportViewerGeneral
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportViewerGeneral));
        this.crystalReportViewerGeneral = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
        this.SuspendLayout();
        // 
        // crystalReportViewerGeneral
        // 
        this.crystalReportViewerGeneral.ActiveViewIndex = -1;
        this.crystalReportViewerGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.crystalReportViewerGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
        this.crystalReportViewerGeneral.Location = new System.Drawing.Point(0, 0);
        this.crystalReportViewerGeneral.Name = "crystalReportViewerGeneral";
        this.crystalReportViewerGeneral.SelectionFormula = "";
        this.crystalReportViewerGeneral.Size = new System.Drawing.Size(1406, 905);
        this.crystalReportViewerGeneral.TabIndex = 0;
        this.crystalReportViewerGeneral.ViewTimeSelectionFormula = "";
        // 
        // FrmReportViewerGeneral
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1406, 905);
        this.Controls.Add(this.crystalReportViewerGeneral);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "FrmReportViewerGeneral";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Report Viewer";
        this.ResumeLayout(false);

    }

    #endregion

    public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerGeneral;

  }
}