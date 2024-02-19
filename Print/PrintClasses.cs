using CommonAppClasses;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Print
{
    public class PrintInf : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private Estimating.Soinf Coversoinf = new Estimating.Soinf("SQL", "SQLConnString");
        public FrmReportViewerGeneral frmReportViewerGeneral = new FrmReportViewerGeneral();
        public ProductionDemandBySO productionDemandBySO = new ProductionDemandBySO();
        private MiscellaneousOrderEntry.MiscordInformation miscSoinf = new MiscellaneousOrderEntry.MiscordInformation("SQL", "SQLConnString");
        private MiscellaneousDataMethods miscdatamethods = new MiscellaneousDataMethods("SQL", "SQLConnString");
        public quote quoteds = new quote();
        public quoterpt quorptds = new quoterpt();
        public orderrpt salesrptds { get; set; }
        public DataSet trackingds;
        public DataTable dttracking;

        public PrintInf(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
            salesrptds = new orderrpt();
        }

        public void ProductionDemandBySO()
        {
            if (miscdatamethods.GetTwoDates("Enter Ship Date Range"))
            {
                string commandtext = "SELECT * FROM view_somastdata WHERE sotype = 'O' AND sostat <> 'V' AND produnits <> 0 AND  ordate BETWEEN @startdate AND @enddate";
                quoteds.view_somastdata.Rows.Clear();
                ClearParameters();
                AddParms("@startdate", miscdatamethods.SelectedStartDate, "SQL");
                AddParms("@enddate", miscdatamethods.SelectedEndDate, "SQL");
                FillData(quoteds, "view_somastdata", commandtext, CommandType.Text);
                if (quoteds.view_somastdata.Rows.Count > 0)
                {
                    productionDemandBySO.SetDataSource((DataTable)quoteds.view_somastdata);
                    frmReportViewerGeneral.crystalReportViewerGeneral.ReportSource = productionDemandBySO;
                    frmReportViewerGeneral.Show();
                }
            }
        }

        public void StartCustomerTransactions()
        {
            GetCustomerMethods getCust = new GetCustomerMethods("SQL", "SQLConnString");
            string CurrentCustno = getCust.SelectedCustno;
            if (CurrentCustno.TrimEnd() != "")
            {
                if (miscdatamethods.GetTwoDates("Enter Sales Order Date Range"))
                {
                    RunCustomerTransactionsReport(CurrentCustno, miscdatamethods.SelectedStartDate, miscdatamethods.SelectedEndDate);
                }
            }
        }

        public void RunCustomerTransactionsReport(string Custno, DateTime StartDate, DateTime EndDate)
        {
            string commandtext = "SELECT * FROM view_customertransactions WHERE custno = @custno AND sodate BETWEEN @startdate AND @enddate ORDER BY recordtype, sono";
            quorptds.view_customertransactions.Rows.Clear();
            ClearParameters();
            AddParms("@custno", Custno, "SQL");
            AddParms("@startdate", StartDate, "SQL");
            AddParms("@enddate", EndDate, "SQL");
            FillData(quorptds, "view_customertransactions", commandtext, CommandType.Text);
            if (quorptds.view_customertransactions.Rows.Count > 0)
            {
                CustomerTransactions customerTransactions = new CustomerTransactions();
                customerTransactions.SetDataSource((DataTable)quorptds.view_customertransactions);
                frmReportViewerGeneral.crystalReportViewerGeneral.ReportSource = customerTransactions;
                frmReportViewerGeneral.Show();
            }
            else
            {
                wsgUtilities.wsgNotice("No Matching Records");
            }
        }

        public void GetSalesData(DateTime StartDate, DateTime EndDate)
        {
            salesrptds.view_salesanalysislines.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@startdate", StartDate, "SQL");
            this.AddParms("@enddate", EndDate, "SQL");
            this.FillData(salesrptds, "view_salesanalysislines", "wsgsp_getviewsalesanalysislines", CommandType.StoredProcedure);
        }

        public void ProcessBatch(string documentname, string productname, string printlocation)
        {
            ReportDocument rd = new ReportDocument();
            Estimating.Invoice CoverInvoice = new Estimating.Invoice();
            Estimating.WorkOrder CoverWorkOrder = new Estimating.WorkOrder();
            Estimating.PackingList CoverPackList = new Estimating.PackingList();
            Estimating.SewnOnLabel CoverSewnOnLabel = new Estimating.SewnOnLabel();
            Estimating.IdentityLabel CoverIdentityLabel = new Estimating.IdentityLabel();

            MiscellaneousOrderEntry.InvoiceRpt MiscInvoice = new MiscellaneousOrderEntry.InvoiceRpt();
            MiscellaneousOrderEntry.MiscOrdWorkOrder MiscWorkOrder = new MiscellaneousOrderEntry.MiscOrdWorkOrder();
            MiscellaneousOrderEntry.MiscOrdPackingList MiscPackList = new MiscellaneousOrderEntry.MiscOrdPackingList();
            PrintDocument printDoc = new PrintDocument();
            PrintDialog printDialog = new PrintDialog();
            DataSet trackingds = new DataSet();
            DataTable dttracking = trackingds.Tables.Add("dttracking");
            dttracking.Columns.Add("sono", typeof(string));
            string CurrentSono = "";
            bool DocOK = true;
            bool IncludeProduct = false;
            string procname = "";
            string soproduct = "";
            switch (documentname)
            {
                case "Invoices":
                    {
                        procname = "wsgsp_getbatchinvoices";
                        break;
                    }
                case "Work Orders":
                    {
                        procname = "wsgsp_getbatchworkorders";
                        break;
                    }
                case "Packing Lists":
                    {
                        procname = "wsgsp_getbatchpacklists";
                        break;
                    }
                case "Sewn On Labels":
                    {
                        procname = "wsgsp_getbatchsewnonlabels";
                        break;
                    }
                case "Identity Labels":
                    {
                        procname = "wsgsp_getbatchidentitylabels";
                        break;
                    }
                default:
                    {
                        DocOK = false;
                        wsgUtilities.wsgNotice("Please select a document");

                        break;
                    }
            }
            if (DocOK)
            {
                dttracking.Rows.Clear();
                this.ClearParameters();
                this.FillData(trackingds, "dttracking", procname, CommandType.StoredProcedure);

                if (dttracking.Rows.Count < 1)
                {
                    wsgUtilities.wsgNotice("No unprinted documents found.");
                }
                else
                {
                    printDialog.Document = printDoc;
                    DialogResult dr = printDialog.ShowDialog();
                    if (dr != DialogResult.OK)
                    {
                        wsgUtilities.wsgNotice("Printing Cancelled.");
                    }
                    else
                    {
                        int nCopy = printDoc.PrinterSettings.Copies;
                        //Get the number of Start Page
                        int sPage = printDoc.PrinterSettings.FromPage;
                        //Get the number of End Page
                        int ePage = printDoc.PrinterSettings.ToPage;
                        //Get the printer name
                        string PrinterName = printDoc.PrinterSettings.PrinterName;
                        foreach (DataRow trackingrow in dttracking.Rows)
                        {
                            if (trackingrow.Field<string>("location").ToString() != printlocation)
                            {
                                continue;
                            }
                            CurrentSono = trackingrow.Field<string>("sono").ToString();
                            Coversoinf.GetSomastBySono(CurrentSono);
                            IncludeProduct = false;
                            // Retrieve the SO product
                            soproduct = Coversoinf.getSoProduct(CurrentSono).TrimEnd();
                            switch (soproduct)
                            {
                                case "None":
                                    {
                                        if (productname == "Miscellaneous Products")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                // Stock Covers
                                case "Non-Meyco Stock":
                                    {
                                        if (productname == "Stock Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                case "Stock Cover":
                                    {
                                        if (productname == "Stock Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                // Repair Orders
                                case "Cover Repair":
                                    {
                                        if (productname == "Repair Orders")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                case "Cover Alteration":
                                    {
                                        if (productname == "Repair Orders")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                // Custom Covers
                                case "Custom Cover":
                                    {
                                        if (productname == "Custom Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                case "Non-Meyco Custom":
                                    {
                                        if (productname == "Custom Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                case "Worksheet":
                                    {
                                        if (productname == "Custom Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                case "Custom Cover Other":
                                    {
                                        if (productname == "Custom Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                case "Catch Basin":
                                    {
                                        if (productname == "Custom Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                case "Spa Cover":
                                    {
                                        if (productname == "Custom Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }

                                case "Kiddie Pool":
                                    {
                                        if (productname == "Custom Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                                case "Wading Pool":
                                    {
                                        if (productname == "Custom Covers")
                                        {
                                            IncludeProduct = true;
                                        }
                                        break;
                                    }
                            } // end switch
                            if (!IncludeProduct)
                            {
                                continue;
                            }
                            if (Coversoinf.somastds.somast[0].sdate.ToString("MM-dd-yyyy") == "12-30-1899")
                            {
                                wsgUtilities.wsgNotice("SO " + CurrentSono + " is being skipped- missing data.");
                                continue;
                            }
                            if (Coversoinf.somastds.somast[0].sotype != "O")
                            {
                                wsgUtilities.wsgNotice("SO " + CurrentSono + " is being skipped- not an order.");
                                continue;
                            }
                            if (Coversoinf.somastds.somast[0].enterqu == "Y")
                            {
                                // Cover Orders
                                switch (documentname)
                                {
                                    case "Invoices":
                                        {
                                            rd = CoverInvoice;
                                            break;
                                        }
                                    case "Work Orders":
                                        {
                                            rd = CoverWorkOrder;
                                            break;
                                        }
                                    case "Packing Lists":
                                        {
                                            rd = CoverPackList;
                                            break;
                                        }
                                    case "Sewn On Labels":
                                        {
                                            rd = CoverSewnOnLabel;
                                            break;
                                        }
                                    case "Identity Labels":
                                        {
                                            rd = CoverIdentityLabel;
                                            break;
                                        }
                                }
                                // Establish customer table
                                Coversoinf.getSingleCustomerData(Coversoinf.somastds.somast[0].custid);
                                // Establish Sales order line view
                                Coversoinf.somastds.view_soreportlinedata.Rows.Clear();
                                // Get report data
                                Coversoinf.getallsoreportdata(Coversoinf.somastds.somast[0].sono);
                                rd.SetDataSource(Coversoinf.quorptds);
                            }
                            else
                            {
                                // Miscellaneous Orders
                                switch (documentname)
                                {
                                    case "Invoices":
                                        {
                                            rd = MiscInvoice;
                                            break;
                                        }
                                    case "Work Orders":
                                        {
                                            rd = MiscWorkOrder;
                                            break;
                                        }
                                    case "Packing Lists":
                                        {
                                            rd = MiscPackList;
                                            break;
                                        }
                                }
                                // Miscellaneous Orders
                                miscSoinf.GetSomastBySono(CurrentSono);
                                miscSoinf.getSingleCustomerData(miscSoinf.somastds.somast[0].custid);
                                miscSoinf.getallsoreportdata(miscSoinf.somastds.somast[0].sono);
                                rd.SetDataSource(miscSoinf.orderrptds);
                            } // (Coversoinf.somastds.somast[0].enterqu == "Y")
                            rd.PrintOptions.PrinterName = PrinterName;
                            rd.PrintToPrinter(1, false, 0, 0);
                        }
                    }// (dr == DialogResult.OK)
                } // (dttracking.Rows.Count < 1)
            } // DocOK
        }// ProcessBatch(string documentname)
    } // class
} // Namespace