using CommonAppClasses;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Print
{
    public class InventoryPrintInf : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private Estimating.Soinf Coversoinf = new Estimating.Soinf("SQL", "SQLConnString");
        private MiscellaneousDataMethods miscdatamethods = new MiscellaneousDataMethods("SQL", "SQLConnString");
        public quote quoteds = new quote();
        public orderrpt salesrptds { get; set; }
        public inventoryds invds = new inventoryds();
        public Form ParentForm = new Form();
        public DataSet trackingds;
        public DataTable dttracking;
        public FrmPrintStockCoverLabels frmPrintStockCoverLabels = new FrmPrintStockCoverLabels();
        public FrmInventoryOnHand frmInventoryOnHand = new FrmInventoryOnHand();
        public FrmInventoryTransactionAnalysis frmInventoryTransactionAnalysis = new FrmInventoryTransactionAnalysis();

        public InventoryPrintInf(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
            salesrptds = new orderrpt();
        }

        public void StartInventoryTransactionAnalysis()
        {
            ParentForm = frmInventoryTransactionAnalysis;
            frmInventoryTransactionAnalysis.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            frmInventoryTransactionAnalysis.buttonGenerate.Click += new System.EventHandler(GenerateInventoryTransactionAnalysis);
            ParentForm.Show();
        }

        public void StartInventoryValuation()
        {
            ParentForm = frmInventoryOnHand;
            frmInventoryOnHand.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            frmInventoryOnHand.buttonGenerate.Click += new System.EventHandler(GenerateInventoryValuation);
            frmInventoryOnHand.Text = "Inventory Valuation";
            ParentForm.Show();
        }

        public void StartInventoryAvailability()
        {
            ParentForm = frmInventoryOnHand;
            frmInventoryOnHand.labelCutoffDate.Visible = false;
            frmInventoryOnHand.dateTimePickerCutoff.Visible = false;
            frmInventoryOnHand.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            frmInventoryOnHand.buttonGenerate.Click += new System.EventHandler(GenerateInventoryAvailability);
            frmInventoryOnHand.Text = "Inventory Availability";

            ParentForm.Show();
        }

        public void StartAllocationsBySalesOrder()
        {
            ParentForm = frmInventoryOnHand;
            frmInventoryOnHand.labelCutoffDate.Visible = false;
            frmInventoryOnHand.dateTimePickerCutoff.Visible = false;
            frmInventoryOnHand.labelItem.Visible = true;
            frmInventoryOnHand.textBoxItem.Visible = true;
            frmInventoryOnHand.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            frmInventoryOnHand.buttonGenerate.Click += new System.EventHandler(GenerateAllocationsBySalesOrder);
            frmInventoryOnHand.Text = "Allocations By Sales Order";
            ParentForm.Show();
        }

        public void StartStockCoverLabels()
        {
            ParentForm = frmPrintStockCoverLabels;
            frmPrintStockCoverLabels.radioButtonSewnOnLabel.Checked = true;
            frmPrintStockCoverLabels.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            frmPrintStockCoverLabels.buttonGenerate.Click += new System.EventHandler(GenerateStockCoverLabels);
            frmPrintStockCoverLabels.buttonMarkLabelsPrinted.Click += new System.EventHandler(MarkStockCoverLabelsPrinted);
            ParentForm.Show();
        }

        public void StartStockShortages()
        {
            ParentForm = frmInventoryOnHand;
            frmInventoryOnHand.labelCutoffDate.Visible = false;
            frmInventoryOnHand.dateTimePickerCutoff.Visible = false;
            frmInventoryOnHand.labelItem.Visible = false;
            frmInventoryOnHand.textBoxItem.Visible = false;
            frmInventoryOnHand.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            frmInventoryOnHand.buttonGenerate.Click += new System.EventHandler(GenerateStockShortages);
            frmInventoryOnHand.Text = "Stock Shortages";
            ParentForm.Show();
        }

        public void GetStockCoverLabelData()
        {
            this.ClearParameters();
            invds.view_expandedictran.Rows.Clear();

            string CommandString = "";
            string StartMeycono = "";
            string EndMeycono = "";
            if (frmPrintStockCoverLabels.textBoxFirstFileNumber.Text.TrimEnd() == "")
            {
                CommandString = "SELECT * FROM view_expandedictran WHERE trantyp = 'H' AND sewnonlabelprinted = 'N' ORDER BY meycono";
            }
            else
            {
                StartMeycono = frmPrintStockCoverLabels.textBoxFirstFileNumber.Text.TrimEnd();
                if (frmPrintStockCoverLabels.textBoxLastFileNumber.Text.TrimEnd() == "")
                {
                    EndMeycono = frmPrintStockCoverLabels.textBoxFirstFileNumber.Text.TrimEnd();
                }
                else
                {
                    EndMeycono = frmPrintStockCoverLabels.textBoxLastFileNumber.Text.TrimEnd();
                }
                this.AddParms("@startmeycono", StartMeycono, "SQL");
                this.AddParms("@endmeycono", EndMeycono, "SQL");
                CommandString = "SELECT * FROM view_expandedictran WHERE trantyp = 'H' AND RTRIM(meycono) BETWEEN @startmeycono AND @endmeycono ORDER BY meycono";
            }
            this.FillData(invds, "view_expandedictran", CommandString, CommandType.Text);
        }

        public void GetFileNumberData()
        {
            string CommandString = "SELECT * FROM view_expandedictran WHERE trantyp = 'H' ORDER BY meycono";
            invds.view_expandedictran.Rows.Clear();
            this.ClearParameters();
            this.FillData(invds, "view_expandedictran", CommandString, CommandType.Text);
        }

        public void GetInventoryTransactionData(DateTime startdate, DateTime enddate, string item)
        {
            string CommandString = "";
            invds.view_expandedictran.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@startdate", startdate, "SQL");
            this.AddParms("@enddate", enddate, "SQL");
            if (item.TrimEnd() != "")
            {
                this.AddParms("@item", item.TrimEnd() + "%", "SQL");
                CommandString = "SELECT * FROM view_expandedictran  WHERE tdate BETWEEN @startdate AND @enddate AND item LIKE @item";
            }
            else
            {
                CommandString = "SELECT * FROM view_expandedictran  WHERE tdate BETWEEN @startdate AND @enddate";
            }
            this.FillData(invds, "view_expandedictran", CommandString, CommandType.Text);
        }

        public void GetInventoryAvailabilityData(string item)
        {
            string CommandString = "";
            invds.view_itemloctidonhand.Rows.Clear();
            this.ClearParameters();
            if (item.TrimEnd() != "")
            {
                this.AddParms("@item", item.TrimEnd() + "%", "SQL");
                CommandString = "SELECT * FROM view_itemloctidonhand  WHERE item LIKE @item ORDER BY item";
            }
            else
            {
                CommandString = "SELECT * FROM view_itemloctidonhand ORDER BY item ";
            }
            this.FillData(invds, "view_itemloctidonhand", CommandString, CommandType.Text);
        }

        public void GetStockShortagesData()
        {
            string CommandString = "";
            invds.view_itemloctidonhand.Rows.Clear();
            this.ClearParameters();
            CommandString = "SELECT * FROM view_itemloctidonhand WHERE allocated > tqty  ORDER BY item ";
            this.FillData(invds, "view_itemloctidonhand", CommandString, CommandType.Text);
        }

        public void GetAllocationsBySalesOrderData(string item)
        {
            string CommandString = "";
            invds.view_openorderstockitems.Rows.Clear();
            this.ClearParameters();
            if (item.TrimEnd() != "")
            {
                this.AddParms("@item", item.TrimEnd() + "%", "SQL");
                CommandString = "SELECT * FROM view_openorderstockitems  WHERE item LIKE @item ORDER BY item";
            }
            else
            {
                CommandString = "SELECT * FROM view_openorderstockitems ORDER BY item ";
            }
            this.FillData(invds, "view_openorderstockitems", CommandString, CommandType.Text);
        }

        public void GetInventoryValuationData(DateTime cutoffdate, string item)
        {
            string CommandString = "";
            invds.view_expandedictran.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@cutoffdate", cutoffdate, "SQL");
            if (item.TrimEnd() == "")
            {
                CommandString = "SELECT *  FROM view_expandedictran  WHERE tdate <= @cutoffdate";
            }
            else
            {
                this.AddParms("@item", item.TrimEnd() + "%", "SQL");

                CommandString = "SELECT * FROM view_expandedictran WHERE tdate <= @cutoffdate AND item LIKE @item";
            }
            this.FillData(invds, "view_expandedictran", CommandString, CommandType.Text);
        }

        public void GenerateFileNumberListing()
        {
            FileNumberList filenumberlist = new FileNumberList();
            GetFileNumberData();
            OutputInventoryCrystalReport(filenumberlist, (DataTable)invds.view_expandedictran, "Stock Cover File Number List");
        }

        private void GenerateStockCoverLabels(object sender, EventArgs e)
        {
            ReportDocument rd = new ReportDocument();
            if (frmPrintStockCoverLabels.radioButtonSewnOnLabel.Checked)
            {
                rd = new StockCoverSewnOnLabel();
            }
            else
            {
                rd = new StockCoverIdentityLabel();
            }
            GetStockCoverLabelData();
            OutputGeneralCrystalReport(rd, (DataTable)invds.view_expandedictran);
        }

        private void GenerateInventoryTransactionAnalysis(object sender, EventArgs e)
        {
            InventoryTransactionAnalysis invtransanalysis = new InventoryTransactionAnalysis();
            GetInventoryTransactionData(frmInventoryTransactionAnalysis.dateTimePickerStart.Value.Date, frmInventoryTransactionAnalysis.dateTimePickerEnd.Value.Date, frmInventoryTransactionAnalysis.textBoxItem.Text);
            OutputInventoryCrystalReport(invtransanalysis, (DataTable)invds.view_expandedictran, "Inventory Transaction Analysis From " + frmInventoryTransactionAnalysis.dateTimePickerStart.Value.ToShortDateString() + " thru " + frmInventoryTransactionAnalysis.dateTimePickerEnd.Value.ToShortDateString());
        }

        private void GenerateInventoryValuation(object sender, EventArgs e)
        {
            InventoryValuation inventoryValuation = new InventoryValuation();
            GetInventoryValuationData(frmInventoryOnHand.dateTimePickerCutoff.Value.Date, frmInventoryOnHand.textBoxItem.Text.TrimEnd());
            OutputInventoryCrystalReport(inventoryValuation, (DataTable)invds.view_expandedictran, "Inventory Valuation As Of " + frmInventoryOnHand.dateTimePickerCutoff.Value.ToShortDateString());
        }

        private void GenerateAllocationsBySalesOrder(object sender, EventArgs e)
        {
            OpenOrderStockItems openOrderStockItems = new OpenOrderStockItems();
            GetAllocationsBySalesOrderData(frmInventoryOnHand.textBoxItem.Text.TrimEnd());
            OutputInventoryCrystalReport(openOrderStockItems, (DataTable)invds.view_openorderstockitems, "Allocations By Sales Order");
        }

        private void MarkStockCoverLabelsPrinted(object sender, EventArgs e)
        {
            string CommandString = "UPDATE ictran SET sewnonlabelprinted = 'Y' WHERE trantyp = 'H'";
            this.ClearParameters();
            ExecuteCommand(CommandString, CommandType.Text);
            wsgUtilities.wsgNotice("Update Complete");
        }

        private void GenerateStockShortages(object sender, EventArgs e)
        {
            InventoryAvailability inventoryAvailability = new InventoryAvailability();
            GetStockShortagesData();
            OutputInventoryCrystalReport(inventoryAvailability, (DataTable)invds.view_itemloctidonhand, "Stock Shortages");
        }

        private void GenerateInventoryAvailability(object sender, EventArgs e)
        {
            InventoryAvailability inventoryAvailability = new InventoryAvailability();
            GetInventoryAvailabilityData(frmInventoryOnHand.textBoxItem.Text.TrimEnd());
            OutputInventoryCrystalReport(inventoryAvailability, (DataTable)invds.view_itemloctidonhand, "Item Availability");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
        }

        public void OutputInventoryCrystalReport(ReportDocument rd, DataTable dt, string RptTitle)
        {
            if (dt.Rows.Count > 0)
            {
                FrmReportViewerGeneral frmViewer = new FrmReportViewerGeneral();
                rd.SetDataSource(dt);
                rd.DataDefinition.FormulaFields["RptTitle"].Text = "'" + RptTitle + "'";
                frmViewer.crystalReportViewerGeneral.ReportSource = rd;
                frmViewer.Show();
            }
            else
            {
                wsgUtilities.wsgNotice("No matching records");
            }
        }

        public void OutputGeneralCrystalReport(ReportDocument rd, DataTable dt)
        {
            FrmReportViewerGeneral frmViewer = new FrmReportViewerGeneral();
            if (dt.Rows.Count > 0)
            {
                rd.SetDataSource(dt);
                frmViewer.crystalReportViewerGeneral.ReportSource = rd;
                frmViewer.Show();
            }
            else
            {
                wsgUtilities.wsgNotice("No matching records");
            }
        }
    } // class
} // Namespace