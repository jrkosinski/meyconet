namespace CommonAppClasses
{
  partial class FrmWSGDocumentViewer
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
      this.crystalReportViewerWSG = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
      this.SuspendLayout();
      // 
      // crystalReportViewerWSG
      // 
      this.crystalReportViewerWSG.ActiveViewIndex = -1;
      this.crystalReportViewerWSG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.crystalReportViewerWSG.Dock = System.Windows.Forms.DockStyle.Fill;
      this.crystalReportViewerWSG.Location = new System.Drawing.Point(0, 0);
      this.crystalReportViewerWSG.Name = "crystalReportViewerWSG";
      this.crystalReportViewerWSG.SelectionFormula = "";
      this.crystalReportViewerWSG.Size = new System.Drawing.Size(723, 262);
      this.crystalReportViewerWSG.TabIndex = 0;
      this.crystalReportViewerWSG.ViewTimeSelectionFormula = "";
      // 
      // FrmWSGDocumentViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(723, 262);
      this.Controls.Add(this.crystalReportViewerWSG);
      this.Name = "FrmWSGDocumentViewer";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Report Viewer";
      this.ResumeLayout(false);

    }

    #endregion

    public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerWSG;

  }
}