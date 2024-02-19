using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Estimating
{
    public partial class FrmPDFGenerator : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");

        public FrmPDFGenerator()
        {
            InitializeComponent();
        }

        public Soinf soinf { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonMeasurementPDF_Click(object sender, EventArgs e)
        {
            measurement rpt = new measurement();
            Task.Run(async () => await ProcessPDF(rpt, "MS"));
        }

        private void buttonReplacementPDF_Click(object sender, EventArgs e)
        {
            replacement rpt = new replacement();
            Task.Run(async () => await ProcessPDF(rpt, "RC"));
        }

        private void buttonInformationPDF_Click(object sender, EventArgs e)
        {
            InfoRequest rpt = new InfoRequest();
            Task.Run(async () => await ProcessPDF(rpt, "INF"));
        }

        public async Task ProcessPDF(ReportClass rpt, string fileprefix)
        {
            if (await MakeReportPDF(soinf.somastds.somast[0].sono, fileprefix, rpt) == true)
            {
                string sono = soinf.somastds.somast[0].sono.TrimStart();
                string filename = fileprefix + sono + ".pdf";
                if (!await PdfStorage.OpenPdf(filename))
                {
                    wsgUtilities.wsgNotice($"There are no PDFs for {sono} found at {System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], filename)}");
                }
            }
        }

        private async Task<bool> MakeReportPDF(string sono, string fileprefix, ReportClass rpt)
        {
            bool pdfOk = true;
            string fileName = fileprefix + sono.TrimStart() + ".pdf";
            string filePath = System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], fileName);
            FileInfo pdfFile = await PdfStorage.GetFile(fileName);

            if (pdfFile != null)
            {
                if (wsgUtilities.wsgReply("Overwrite Existing PDF?") != true)
                {
                    pdfOk = false;
                }
            }
            if (pdfOk)
            {
                if (await PdfStorage.FileIsAvailable(fileName))
                {
                    soinf.getallsoreportdata(sono);
                    rpt.SetDataSource(soinf.quorptds);
                    try
                    {
                        await PdfStorage.WriteFileFromReport(rpt, fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    wsgUtilities.wsgNotice("The PDF for this SO is in use. Close it and retry.");
                    pdfOk = false;
                }
            } // pdfok
            return pdfOk;
        }

        private void buttonInspectionPDF_Click(object sender, EventArgs e)
        {
        }

        private void buttonRGAPDF_Click(object sender, EventArgs e)
        {
            RepairEstimate rpt = new RepairEstimate();
            Task.Run(async () => await ProcessPDF(rpt, "R"));
        }
    }
}