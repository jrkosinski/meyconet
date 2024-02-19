using CommonAppClasses;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using WSGUtilitieslib;

namespace Estimating
{
    public partial class FrmPDFViewer : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");
        public quote ds;

        public FrmPDFViewer()
        {
            InitializeComponent();
        }

        public async Task ViewPDF(string fileprefix, string subfolder = "SOPDFPath")
        {
            string sono = ds.somast[0].sono.TrimStart();
            string filename = fileprefix + sono + ".pdf";
            if (!await PdfStorage.OpenPdf(filename, subfolder))
            {
                wsgUtilities.wsgNotice($"There are no PDFs for {sono} found at {System.IO.Path.Combine(ConfigurationManager.AppSettings[subfolder], filename)}");
            }
        }

        private void buttonInvoicePDF_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ViewPDF("I"));
        }

        private void buttonPricePDF_Click(object sender, EventArgs e)
        {
            DirectoryInfo pdfdir = new DirectoryInfo(ConfigurationManager.AppSettings["SOPDFPath"]);
            string fileprefix = "";
            if (ds.somast[0].sotype == "B")
            {
                fileprefix = "E";
            }
            else
            {
                fileprefix = "O";
            }
            Task.Run(async () => await ViewPDF(fileprefix));
        }

        private void buttonMainSOPDF_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ViewPDF(""));
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonInspectionPDF_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ViewPDF("INS"));
        }

        private void buttonInformationPDF_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ViewPDF("INF"));
        }

        private void buttonMeasurementPDF_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ViewPDF("MS"));
        }

        private void buttonReplacementPDF_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ViewPDF("RC"));
        }

        private void buttonRGAPDF_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ViewPDF("R"));
        }

        private void buttonDRAFTINGPDF_Click(object sender, EventArgs e)
        {
            Task.Run(async () => await ViewPDF(""));
        }
    }
}