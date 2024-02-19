using CommonAppClasses;
using System;
using WSGUtilitieslib;

namespace Estimating
{
    public partial class FrmCoverSODocumentViewer : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Customer Information");

        // Create the customer processing object
        private CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");

        // Create the App Information processing object
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");

        private Soinf soinf = new Soinf("SQL", "SQLConnString");

        public FrmCoverSODocumentViewer()
        {
            InitializeComponent();
        }

        public string CurrentSono { get; set; }
        public string SODocument { get; set; }

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void crystalReportViewerWSG_Load(object sender, EventArgs e)
        {
            // Establish somastds dataset, somast and soaddr tables
            soinf.GetSomastBySono(CurrentSono);
            // Establish customer table
            soinf.getSingleCustomerData(soinf.somastds.somast[0].custid);
            // Establish Sales order line view
            soinf.somastds.view_soreportlinedata.Rows.Clear();
            soinf.getallsoreportdata(soinf.somastds.somast[0].sono);
            Estimate estrpt = new Estimate();
            Invoice invrpt = new Invoice();
            WorkOrder worpt = new WorkOrder();
            IdentityLabel idlabelrpt = new IdentityLabel();
            SewnOnLabel solablerpt = new SewnOnLabel();
            PackingList plrpt = new PackingList();
            switch (SODocument)
            {
                case "Identity Label":
                    {
                        idlabelrpt.SetDataSource(soinf.quorptds);
                        crystalReportViewerWSG.ReportSource = idlabelrpt;
                        break;
                    }

                case "Sewn On Label":
                    {
                        solablerpt.SetDataSource(soinf.quorptds);
                        crystalReportViewerWSG.ReportSource = solablerpt;
                        break;
                    }

                case "Invoice":
                    {
                        invrpt.SetDataSource(soinf.quorptds);
                        crystalReportViewerWSG.ReportSource = invrpt;
                        break;
                    }

                case "Work Order":
                    {
                        worpt.SetDataSource(soinf.quorptds);
                        crystalReportViewerWSG.ReportSource = worpt;
                        break;
                    }
                case "Packing List":
                    {
                        plrpt.SetDataSource(soinf.quorptds);
                        crystalReportViewerWSG.ReportSource = plrpt;
                        break;
                    }
                case "Estimate":
                    {
                        estrpt.SetDataSource(soinf.quorptds);
                        crystalReportViewerWSG.ReportSource = estrpt;
                        break;
                    }

                default:
                    {
                        estrpt.SetDataSource(soinf.quorptds);
                        crystalReportViewerWSG.ReportSource = estrpt;
                        break;
                    }
            }
        }
    }
}