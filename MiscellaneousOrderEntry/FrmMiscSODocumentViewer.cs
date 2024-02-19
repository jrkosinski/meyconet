using CommonAppClasses;
using System;
using WSGUtilitieslib;

namespace MiscellaneousOrderEntry
{
    public partial class FrmMiscSODocumentViewer : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Customer Information");

        // Create the customer processing object
        private CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");

        // Create the App Information processing object
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");

        private MiscordInformation miscordinf = new MiscordInformation("SQL", "SQLConnString");

        public FrmMiscSODocumentViewer()
        {
            InitializeComponent();
        }

        public string CurrentSono { get; set; }
        public string SODocument { get; set; }

        private void crystalReportViewerWSG_Load(object sender, EventArgs e)
        {
            miscordinf.GetSomastBySono(CurrentSono);
            miscordinf.getSingleCustomerData(miscordinf.somastds.somast[0].custid);
            miscordinf.getallsoreportdata(miscordinf.somastds.somast[0].sono);
            OrderRpt orderRpt = new OrderRpt();
            // MiscOrdPackingList packlistRpt = new MiscOrdPackingList();
            MiscOrdPackingList packlistRpt = new MiscOrdPackingList();
            MiscOrdWorkOrder workorderrRpt = new MiscOrdWorkOrder();
            InvoiceRpt invoiceRpt = new InvoiceRpt();
            switch (SODocument)
            {
                case "Estimate":
                    {
                        orderRpt.SetDataSource(miscordinf.orderrptds);
                        crystalReportViewerWSG.ReportSource = orderRpt;
                        break;
                    }
                case "Packing List":
                    {
                        packlistRpt.SetDataSource(miscordinf.orderrptds);
                        crystalReportViewerWSG.ReportSource = packlistRpt;
                        break;
                    }
                case "Invoice":
                    {
                        invoiceRpt.SetDataSource(miscordinf.orderrptds);
                        crystalReportViewerWSG.ReportSource = invoiceRpt;
                        break;
                    }

                case "Work Order":
                    {
                        workorderrRpt.SetDataSource(miscordinf.orderrptds);
                        crystalReportViewerWSG.ReportSource = workorderrRpt;
                        break;
                    }
            }
        }

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMiscSODocumentViewer_Load(object sender, EventArgs e)
        {
        }
    }
}