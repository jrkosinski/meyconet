using System;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Print
{
    public partial class FrmPrintSalesAnalysis : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");
        private SalesAnalysis1 salesAnalysis1 = new SalesAnalysis1();
        private PrintInf printInf = new PrintInf("SQL", "SQLConnString");

        public FrmPrintSalesAnalysis()
        {
            InitializeComponent();
            dateTimePickerStart.Value = Convert.ToDateTime("01/01/2010");
            dateTimePickerEnd.Value = Convert.ToDateTime("31/12/2998");
            SoStartDate = dateTimePickerStart.Value;
            SoEndDate = dateTimePickerEnd.Value;

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonGenerate,
                this.dateTimePickerStart,
                this.dateTimePickerEnd,
                this.buttonClose
            });
        }

        public DateTime SoStartDate { get; set; }
        public DateTime SoEndDate { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            FrmReportViewerGeneral frmReportViewerGeneral = new FrmReportViewerGeneral();
            printInf.GetSalesData(dateTimePickerStart.Value, dateTimePickerEnd.Value);
            if (printInf.salesrptds.view_salesanalysislines.Rows.Count > 0)
            {
                salesAnalysis1.SetDataSource(printInf.salesrptds);
                frmReportViewerGeneral.crystalReportViewerGeneral.ReportSource = salesAnalysis1;
                frmReportViewerGeneral.Show();
            }
            else
            {
                wsgUtilities.wsgNotice("No Matching Records");
            }
        }
    }
}