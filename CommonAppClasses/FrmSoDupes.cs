using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmSoDupes : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");

        public FrmSoDupes()
        {
            InitializeComponent();
            CurrentSono = "";
        }

        public DirectoryInfo pdfdir = new DirectoryInfo(ConfigurationManager.AppSettings["PDFSTORAGEPath"]);

        public string CurrentSono { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ProcessSo()
        {
            CurrencyManager xCM =
           (CurrencyManager)dataGridViewDupes.BindingContext[dataGridViewDupes.DataSource,
           dataGridViewDupes.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            CurrentSono = (string)xRow["sono"];

            string filename = CurrentSono.TrimStart() + ".pdf";
            FileInfo[] pdffiles = pdfdir.GetFiles(filename);
            if (pdffiles.Length > 0)
            {
                Process.Start(pdffiles[0].FullName);
            }
            else
            {
                wsgUtilities.wsgNotice($"There are no PDFs for {CurrentSono} found at {System.IO.Path.Combine(pdfdir.FullName, filename)}");
            }
        }

        private void dataGridViewDupes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProcessSo();
        }

        private void dataGridViewDupes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ProcessSo();
            }
        }
    }
}