namespace Estimating

{
  partial class FrmCoverSODocumentViewer
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
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.crystalReportViewerWSG = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
        this.SuspendLayout();
        // 
        // buttonClose
        // 
        this.buttonClose.Location = new System.Drawing.Point(12, -3);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(106, 31);
        this.buttonClose.TabIndex = 0;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click_1);
        // 
        // crystalReportViewerWSG
        // 
        this.crystalReportViewerWSG.ActiveViewIndex = -1;
        this.crystalReportViewerWSG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.crystalReportViewerWSG.Location = new System.Drawing.Point(12, 30);
        this.crystalReportViewerWSG.Name = "crystalReportViewerWSG";
        this.crystalReportViewerWSG.SelectionFormula = "";
        this.crystalReportViewerWSG.Size = new System.Drawing.Size(1218, 959);
        this.crystalReportViewerWSG.TabIndex = 1;
        this.crystalReportViewerWSG.ViewTimeSelectionFormula = "";
        this.crystalReportViewerWSG.Load += new System.EventHandler(this.crystalReportViewerWSG_Load);
        // 
        // FrmCoverSODocumentViewer
        // 
        this.ClientSize = new System.Drawing.Size(1242, 1001);
        this.Controls.Add(this.crystalReportViewerWSG);
        this.Controls.Add(this.buttonClose);
        this.Name = "FrmCoverSODocumentViewer";
        this.ResumeLayout(false);

    }

    #endregion

      private System.Windows.Forms.Button buttonClose;
      private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerWSG;
  }
}