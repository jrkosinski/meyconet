using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace BusinessProcessing
{
    public partial class FrmMenu : WSGBaseClassLibrary.WSGFrmBase
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Sales Order Processing System");
        public SqlConnection conn = new SqlConnection();
        private bool telemetryUploaded = false;

        public FrmMenu()
        {
            InitializeComponent();
            conn.ConnectionString = myAppconstants.SQLConnectionString;
            appUtilities.setScreenSize(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void salesOrderTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void queueProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tracking.FrmTrackingQueue frmTrackingQueue = new Tracking.FrmTrackingQueue();
            frmTrackingQueue.MdiParent = this;
            frmTrackingQueue.Show();
        }

        private void trackingCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
            }
        }

        private void routToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
            }
        }

        private bool IsRoleAuthorized(string requiredrole)
        {
            if (ConfigurationManager.AppSettings["TestMode"] != "True")
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_checkuserrole");
                appUtilities.makeSQLCommand(ref cmd, ref conn);
                cmd.Parameters.Add("@userid", SqlDbType.Char);
                cmd.Parameters["@userid"].Value = AppUserClass.AppUserId;
                cmd.Parameters.Add("@requiredrole", SqlDbType.Char);
                cmd.Parameters["@requiredrole"].Value = requiredrole;
                SqlParameter RoleMessage = new SqlParameter("@RoleMessage", SqlDbType.Char, 20);
                RoleMessage.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(RoleMessage);

                conn.Open();
                try
                {
                    WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(cmd);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    if (cmd.Parameters["@RoleMessage"].Value.ToString().Trim() == "OK")
                    {
                        return true;
                    }
                    else
                    {
                        wsgUtilities.wsgNotice(cmd.Parameters["@RoleMessage"].Value.ToString());
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    HandleException(ex);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private bool IsUserOK()
        {
            if (ConfigurationManager.AppSettings["TestMode"] != "True")
            {
                AppUserClass.AppUserId = "";
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.ShowDialog();
                if (AppUserClass.AppUserId == "")
                    return false;
                else
                    return true;
            }
            else
            {
                AppUserClass.AppUserId = "WSG";
                return true;
            }
        }

        private void FrmMenu_Shown(object sender, EventArgs e)
        {
            if (!IsUserOK())
            {
                wsgUtilities.wsgNotice("Login Cancelled");
                this.Close();
            }
        }

        private void FrmMenu_Closing(object sender, FormClosingEventArgs e)
        {
            if (!this.telemetryUploaded)
            {
                e.Cancel = true;

                System.Threading.Tasks.Task.Run(async () =>
                {
                    await WSGUtilitieslib.Telemetry.Telemetry.StoreData();
                    this.telemetryUploaded = true;

                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Close();
                    });
                });
            }
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerMaintenance.CustomerMaintenanceMethods custMethods = new CustomerMaintenance.CustomerMaintenanceMethods("SQL", "SQLConnstring");
            custMethods.menuForm = this;
            custMethods.ShowParent();
        }

        private void pricingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                // Call the Pricing Maintenance Form
                MaintainCoverReferences.FrmMaintainPriceLocator frmMaintainPriceLocator = new MaintainCoverReferences.FrmMaintainPriceLocator();
                frmMaintainPriceLocator.MdiParent = this;
                frmMaintainPriceLocator.Show();
            }
        }

        private void spacingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MaintainCoverReferences.FrmMaintainSpacing frmMaintainSpacing = new MaintainCoverReferences.FrmMaintainSpacing();
                frmMaintainSpacing.MdiParent = this;
                frmMaintainSpacing.Show();
            }
        }

        private void priceScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                // Call the Price Schedule Detail Form

                MaintainCoverReferences.FrmMaintainPriceDetail frmMaintainPriceDetail = new MaintainCoverReferences.FrmMaintainPriceDetail();
                frmMaintainPriceDetail.MdiParent = this;
                frmMaintainPriceDetail.Show();
            }
        }

        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call the Cover Order Entry Screen
            Estimating.FrmSOHead frmSoHead = new Estimating.FrmSOHead();
            frmSoHead.MdiParent = this;
            frmSoHead.NewOrder = true;
            frmSoHead.CustomCover = true;
            frmSoHead.Quoting = true;
            frmSoHead.Show();
        }

        private void changeCoverOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call the Cover Order Entry Screen
            Estimating.FrmSOHead frmSoHead = new Estimating.FrmSOHead();
            frmSoHead.MdiParent = this;
            frmSoHead.NewOrder = false;
            frmSoHead.CustomCover = true;
            frmSoHead.Quoting = true;
            frmSoHead.Show();
        }

        private void systemCommentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MaintainSystemReference.FrmMaintainSystemComments frmMaintainSystemComments = new MaintainSystemReference.FrmMaintainSystemComments();
                frmMaintainSystemComments.MdiParent = this;
                frmMaintainSystemComments.Show();
            }
        }

        private void materialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MaintainCoverReferences.FrmMaintainMaterial frmMaintainMaterial = new MaintainCoverReferences.FrmMaintainMaterial();
                frmMaintainMaterial.MdiParent = this;
                frmMaintainMaterial.Show();
            }
        }

        private void overlapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MaintainCoverReferences.FrmMaintainOverlap frmMaintainOverlap = new MaintainCoverReferences.FrmMaintainOverlap();
                frmMaintainOverlap.MdiParent = this;
                frmMaintainOverlap.Show();
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MaintainCoverReferences.FrmMaintainColor frmMaintainColor = new MaintainCoverReferences.FrmMaintainColor();
                frmMaintainColor.MdiParent = this;
                frmMaintainColor.Show();
            }
        }

        private void sODocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.FrmPrintSODocumentsIndividual frmPrintSoDocuments = new Print.FrmPrintSODocumentsIndividual();
            frmPrintSoDocuments.MdiParent = this;
            frmPrintSoDocuments.Show();
        }

        private void repairInspectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inspection.FrmRepairInspection frmRepairInspection = new Inspection.FrmRepairInspection();
            frmRepairInspection.MdiParent = this;
            //Create the Inspection Information processing object
            Inspection.InspInf inspInf = new Inspection.InspInf("SQL", "SQLConnString", frmRepairInspection);
            frmRepairInspection.Show();
        }

        private void systemReferenceTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MaintainSystemReferences.FrmMaintainSysReference frmMaintainSysReference =
                 new MaintainSystemReferences.FrmMaintainSysReference();
                frmMaintainSysReference.MdiParent = this;
                frmMaintainSysReference.Show();
            }
        }

        private void enterMiscellaneousOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiscellaneousOrderEntry.FrmMiscOrder frmMiscOrder = new MiscellaneousOrderEntry.FrmMiscOrder();
            frmMiscOrder.NewOrder = true;
            frmMiscOrder.Quoting = false;
            frmMiscOrder.MdiParent = this;
            frmMiscOrder.Show();
        }

        private void changeMiscellaneousOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiscellaneousOrderEntry.FrmMiscOrder frmMiscOrder = new MiscellaneousOrderEntry.FrmMiscOrder();
            frmMiscOrder.NewOrder = false;
            frmMiscOrder.MdiParent = this;
            frmMiscOrder.Show();
        }

        private void enterMiscellaneousBidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiscellaneousOrderEntry.FrmMiscOrder frmMiscOrder = new MiscellaneousOrderEntry.FrmMiscOrder();
            frmMiscOrder.NewOrder = true;
            frmMiscOrder.Quoting = true;
            frmMiscOrder.MdiParent = this;
            frmMiscOrder.Show();
        }

        private void sODocumentsBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.FrmPrintSoDocumentsBatch frmPrintSoDocumentsBatch = new Print.FrmPrintSoDocumentsBatch();
            frmPrintSoDocumentsBatch.MdiParent = this;
            frmPrintSoDocumentsBatch.PrintLocation = "NY";
            frmPrintSoDocumentsBatch.Show();
        }

        private void sODocumentsBatchSCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.FrmPrintSoDocumentsBatch frmPrintSoDocumentsBatch = new Print.FrmPrintSoDocumentsBatch();
            frmPrintSoDocumentsBatch.MdiParent = this;
            frmPrintSoDocumentsBatch.PrintLocation = "SC";
            frmPrintSoDocumentsBatch.Show();
        }

        private void salesAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.FrmPrintSalesAnalysis frmPrintSalesAnalysis = new Print.FrmPrintSalesAnalysis();
            frmPrintSalesAnalysis.MdiParent = this;
            frmPrintSalesAnalysis.Show();
        }

        private void stepsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MiscellaneousSystemMaintenance.FrmMaintainStep frmMaintainStep = new MiscellaneousSystemMaintenance.FrmMaintainStep();
                frmMaintainStep.MdiParent = this;
                frmMaintainStep.Show();
            }
        }

        private void routesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MiscellaneousSystemMaintenance.FrmMaintainRoute frmMaintainRoute = new MiscellaneousSystemMaintenance.FrmMaintainRoute();
                frmMaintainRoute.MdiParent = this;
                frmMaintainRoute.Show();
            }
        }

        private void workGroupsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MiscellaneousSystemMaintenance.FrmMaintainWorkgroup frmMaintainWorkGroup = new MiscellaneousSystemMaintenance.FrmMaintainWorkgroup();
                frmMaintainWorkGroup.MdiParent = this;
                frmMaintainWorkGroup.Show();
            }
        }

        private void usersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MiscellaneousSystemMaintenance.FrmMaintainUser frmMaintainUser = new MiscellaneousSystemMaintenance.FrmMaintainUser();
                frmMaintainUser.MdiParent = this;
                frmMaintainUser.Show();
            }
        }

        private void synchronzieWarrantyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiscellaneousSystemMaintenance.MiscSysInf miscSysInf = new MiscellaneousSystemMaintenance.MiscSysInf("SQL", "SQLConnString");
            miscSysInf.ConvertWarranty();
        }

        private void warrantyProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Warranty.WarrInf warrInf = new Warranty.WarrInf("SQL", "SQLConnString", this);
            warrInf.ShowParent();
        }

        private void copyAQuoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Estimating.CopyQuoteMethods copyQuote = new Estimating.CopyQuoteMethods("SQL", "SQLConnString", this);
            copyQuote.StartApp(this);
        }

        private void clearLocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiscellaneousSystemMaintenance.FrmSystemActivities frmSystemActivities = new MiscellaneousSystemMaintenance.FrmSystemActivities();
            frmSystemActivities.MdiParent = this;
            frmSystemActivities.Show();
        }

        private void batchTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("TRAD") == true)
            {
                Tracking.FrmBatchTracking frmBatchTracking = new Tracking.FrmBatchTracking();
                frmBatchTracking.MdiParent = this;
                frmBatchTracking.Show();
            }
        }

        private void salesOrderTrackingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Tracking.FrmTrackingSearch frmTrackingSearch = new Tracking.FrmTrackingSearch();
            frmTrackingSearch.MdiParent = this;
            frmTrackingSearch.Width = this.Width - 30;
            frmTrackingSearch.Height = this.Height - 150;
            frmTrackingSearch.Show();
        }

        private void batchTrackingRoutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tracking.FrmBatchTrackingRoutes frmBatchTrackingRoutes = new Tracking.FrmBatchTrackingRoutes();
            frmBatchTrackingRoutes.MdiParent = this;
            frmBatchTrackingRoutes.Show();
        }

        private void productionUnitScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MaintainSystemReferences.MaintainProductionUnitMethods prodschedunitmethods = new MaintainSystemReferences.MaintainProductionUnitMethods("SQL", "SQLConnstring");
                prodschedunitmethods.menuForm = this;
                prodschedunitmethods.ShowParent();
            }
        }

        private void productionCapacityCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRoleAuthorized("SYAD") == true)
            {
                MaintainSystemReferences.CapacityCalendarMethods capacityprodschedunitmethods = new MaintainSystemReferences.CapacityCalendarMethods("SQL", "SQLConnstring");
                capacityprodschedunitmethods.menuForm = this;
                capacityprodschedunitmethods.StartApp();
            }
        }

        private void calculateShipDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonAppClasses.MiscellaneousDataMethods miscdatamethods = new CommonAppClasses.MiscellaneousDataMethods("SQL", "SQLConnstring");
            miscdatamethods.GetShipDateInformation("");
        }

        private void productionDemandBySalesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.PrintInf printinf = new Print.PrintInf("SQL", "SQLConnString");
            printinf.ProductionDemandBySO();
        }

        private void enterActualCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Design.DesignClasses designinf = new Design.DesignClasses("SQL", "SQLConnString");
            designinf.EnterActualQuantiites(this);
        }

        private void inventoryReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory.InventoryMethods invMethods = new Inventory.InventoryMethods("SQL", "SQLConnString");
            invMethods.InvtranType = "R";
            invMethods.InventoryTransactionsStart(this);
        }

        private void inventoryIssuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory.InventoryMethods invMethods = new Inventory.InventoryMethods("SQL", "SQLConnString");
            invMethods.InvtranType = "I";
            invMethods.InventoryTransactionsStart(this);
        }

        private void inventoryTransactionAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.InventoryPrintInf inventoryPrintInf = new Print.InventoryPrintInf("SQL", "SQLConnString");
            inventoryPrintInf.frmInventoryTransactionAnalysis.MdiParent = this;
            inventoryPrintInf.StartInventoryTransactionAnalysis();
        }

        private void inventoryAvailabiltyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.InventoryPrintInf inventoryPrintInf = new Print.InventoryPrintInf("SQL", "SQLConnString");
            inventoryPrintInf.frmInventoryOnHand.MdiParent = this;
            inventoryPrintInf.StartInventoryAvailability();
        }

        private void inventoryValuationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.InventoryPrintInf inventoryPrintInf = new Print.InventoryPrintInf("SQL", "SQLConnString");
            inventoryPrintInf.frmInventoryOnHand.MdiParent = this;
            inventoryPrintInf.StartInventoryValuation();
        }

        private void stockShortagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.InventoryPrintInf inventoryPrintInf = new Print.InventoryPrintInf("SQL", "SQLConnString");
            inventoryPrintInf.frmInventoryOnHand.MdiParent = this;
            inventoryPrintInf.StartStockShortages();
        }

        private void allocationsBySalesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.InventoryPrintInf inventoryPrintInf = new Print.InventoryPrintInf("SQL", "SQLConnString");
            inventoryPrintInf.frmInventoryOnHand.MdiParent = this;
            inventoryPrintInf.StartAllocationsBySalesOrder();
        }

        private void assignstockcoverfilenumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory.InventoryMethods invMethods = new Inventory.InventoryMethods("SQL", "SQLConnString");
            invMethods.InvtranType = "H";
            invMethods.InventoryTransactionsStart(this);
        }

        private void fileNumberListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.InventoryPrintInf inventoryPrintInf = new Print.InventoryPrintInf("SQL", "SQLConnString");
            inventoryPrintInf.GenerateFileNumberListing();
        }

        private void meycoFileLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.InventoryPrintInf inventoryPrintInf = new Print.InventoryPrintInf("SQL", "SQLConnString");
            inventoryPrintInf.frmPrintStockCoverLabels.MdiParent = this;
            inventoryPrintInf.StartStockCoverLabels();
        }

        private void customerTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print.PrintInf printinf = new Print.PrintInf("SQL", "SQLConnString");
            printinf.StartCustomerTransactions();
        }

        private void incidentProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IncidentProcessing.IncidentProcessingInformation incidentProcessinginf = new IncidentProcessing.IncidentProcessingInformation("SQL", "SQLConnString");
            incidentProcessinginf.menuForm = this;
            incidentProcessinginf.StartApp();
        }

        private void departmentEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MiscellaneousSystemMaintenance.DepartmentEmployeeInf deptEmployeeinf = new MiscellaneousSystemMaintenance.DepartmentEmployeeInf("SQL", "SQLConnString");
            deptEmployeeinf.menuForm = this;
            deptEmployeeinf.StartApp();
        }

        private void sOTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ticketing.TicketMethods ticketmethods = new Ticketing.TicketMethods("SQL", "SQLConnString");
            ticketmethods.menuForm = this;
            ticketmethods.StartSoTicket("");
        }

        private void customerTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ticketing.TicketMethods ticketmethods = new Ticketing.TicketMethods("SQL", "SQLConnString");
            ticketmethods.menuForm = this;
            ticketmethods.StartCustomerTicket("");
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
        }

        private void changeSODealerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Estimating.CustomerChangeMethods customerChangeMethods = new Estimating.CustomerChangeMethods("SQL", "SQLConnString");
            customerChangeMethods.menuForm = this;
            customerChangeMethods.StartApp();
        }

        private void inventoryItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImmasterMaintenance.ImmasterMaintenanceMethods immasterMaintenanceMethods = new ImmasterMaintenance.ImmasterMaintenanceMethods();
            immasterMaintenanceMethods.menuform = this;
            immasterMaintenanceMethods.StartApp();
        }
    }
}