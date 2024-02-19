using CommonAppClasses;
using Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousOrderEntry
{
    # region SO Information

    public class MiscordInformation : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        public InventoryMethods invMethods = new InventoryMethods("SQL", "SQLConnString");
        public price prds { get; set; }
        public order somastds { get; set; }
        public customer ards { get; set; }
        public order solineds { get; set; }
        public order resultsds { get; set; }
        public FrmMiscOrder parentform { get; set; }
        public order soitemsds { get; set; }
        public system referencequeryds { get; set; }
        public order tempds { get; set; }
        public order dupecheckds { get; set; }
        public order socurrentitemsds { get; set; }
        public item itemds { get; set; }
        public orderrpt orderrptds { get; set; }
        public reference soreferenceds { get; set; }
        public item featureds { get; set; }
        public DataTable dtresults { get; set; }
        public DataTable dttotals { get; set; }
        public DataTable dtfeatures { get; set; }
        public DataTable dtsolineOriginal { get; set; }
        private InvoicingMethods invoicingMethods = new InvoicingMethods();
        public DataTable dtcoverdimensions { get; set; }
        public Dictionary<string, string> errorlist = new Dictionary<string, string>();
        public CustomerAccess custAccess = new CustomerAccess("SQL", "SQLConnString");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");
        public ImmasterAccess immasterAccess = new ImmasterAccess("SQL", "SQLConnString");

        public MiscordInformation(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            somastds = new order();
            SetIdcol(somastds.somast.idcolColumn);
            SetIdcol(somastds.soline.idcolColumn);
            SetIdcol(somastds.soaddr.idcolColumn);
            SetIdcol(somastds.somast.idcolColumn);
            SetIdcol(somastds.soline.idcolColumn);
            SetIdcol(somastds.soaddr.idcolColumn);
            dtsolineOriginal = new DataTable();
            resultsds = new order();
            referencequeryds = new system();
            orderrptds = new orderrpt();
            prds = new price();
            ards = new customer();
            SetIdcol(ards.arcust.idcolColumn);
            SetIdcol(ards.aracadr.idcolColumn);
            CreateResultsTable();
            CreateTotalsTable();
            CreateFeaturesTable();
            itemds = new item();
            tempds = new order();
            dupecheckds = new order();
            socurrentitemsds = new order();
            SetIdcol(socurrentitemsds.soline.idcolColumn);
            soreferenceds = new reference();
            soitemsds = new order();
            SetIdcol(soitemsds.soline.idcolColumn);
            featureds = new item();
            errorlist.Add("500", "Sample Error");
        }

        // Set dedicated items

        public void getSingleSoImmasterData(string item)
        {
            itemds.immaster.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.FillData(itemds, "immaster", "wsgsp_getsingleimmasterdata", CommandType.StoredProcedure);
        }

        public string getsono()
        {
            return appInformation.GetNextSono();
        }

        public string CreateInvoice(order sods, DateTime invdate)
        {
            string invno = invoicingMethods.CreateInvoice(sods.somast[0].sono, invdate);
            return invno;
        }

        public void ProcessMiscellaneousOrderInventory(order sods, DateTime tdate)
        {
            invMethods.ProcessMiscellaneousOrderShipment(sods.somast[0].sono, sods.somast[0].defloc, tdate);
        }

        public void getallsoreportdata(string sono)
        {
            // somast
            orderrptds.somast.Rows.Clear();
            orderrptds.somast.ImportRow(somastds.somast.Rows[0]);
            // soaddr
            orderrptds.soaddr.Rows.Clear();
            orderrptds.soaddr.ImportRow(somastds.soaddr.Rows[0]);
            // arcust
            orderrptds.arcust.Rows.Clear();
            orderrptds.arcust.ImportRow(ards.arcust.Rows[0]);
            orderrptds.soline.Rows.Clear();
            orderrptds.view_expandedsoline.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(orderrptds, "view_expandedsoline", "wsgsp_findViewExpandedSolineDatabySono", CommandType.StoredProcedure);
        }

        public void EstablishFeatureds()
        {
            featureds.view_immasterdata.Rows.Clear();
        }

        public void CombineCurrentItems()
        {
            // Save any line items from the current feature
            if (socurrentitemsds.soline.Rows.Count > 0)
            {
                string CurrentFeature = socurrentitemsds.soline[0].source;
                //If there are items for that feature, delete them first
                foreach (DataRow row in soitemsds.soline)
                {
                    string source = row["source"].ToString().TrimEnd();
                    if (source == CurrentFeature.TrimEnd())
                    {
                        row.Delete();
                    }
                }
            }
            soitemsds.AcceptChanges();
            for (int r1 = 0; r1 <= socurrentitemsds.soline.Rows.Count - 1; r1++)
            {
                soitemsds.soline.ImportRow(socurrentitemsds.soline.Rows[r1]);
            }
            soitemsds.AcceptChanges();

            // Clear the current items table
            socurrentitemsds.soline.Rows.Clear();
            socurrentitemsds.AcceptChanges();
        }

        public void UnlockSomast(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "somast", "SQL");
            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void CloseMiscSostat(string sono)
        {
            string commandtext = "UPDATE somast SET sostat = 'C' WHERE sono = @sono";
            ClearParameters();
            AddParms("@sono", sono, "SQL");
            try
            {
                ExecuteCommand(commandtext, CommandType.Text);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public string LockSomast(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "somast", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public string GetFeatures(string featurecode, string CurrentFeature)
        {
            // Save any line items from a previous feature
            CombineCurrentItems();

            // Load any prior items for this feature
            socurrentitemsds.soline.Rows.Clear();
            for (int r = 0; r <= soitemsds.soline.Rows.Count - 1; r++)
            {
                if (soitemsds.soline[r].source.TrimEnd() == featurecode.TrimEnd())
                {
                    socurrentitemsds.soline.ImportRow(soitemsds.soline.Rows[r]);
                }
            }
            socurrentitemsds.AcceptChanges();
            // Populate the feature selector table
            featureds.view_immasterdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@misccode", featurecode, "SQL");
            this.FillData(featureds, "view_immasterdata", "wsgsp_getorderitem", CommandType.StoredProcedure);
            featureds.view_immasterdata.DefaultView.Sort = "shortdescrip";

            return featurecode;
        }

        // soitemsds holds all non cover items for this order
        public void EstablishSoitemsTable()
        {
            soitemsds.soline.Rows.Clear();
        }

        // socurrentitemsds holds the items for the feature currently worked on
        public void EstablishCurrentitemsTable()
        {
            socurrentitemsds.soline.Rows.Clear();
        }

        public bool CheckDiscount(string item)
        {
            bool applydiscount = false;
            itemds.immaster.Rows.Clear();
            string CommandString = "SELECT * FROM immaster WHERE item = @item";
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.FillData(itemds, "immaster", CommandString, CommandType.Text);
            if (itemds.immaster.Rows.Count > 0)
            {
                applydiscount = itemds.immaster[0].appldisc;
            }
            return applydiscount;
        }

        public void SetSolineItem(string item, string source, Int32 qtyord, decimal price, string version, string cover)
        {
            string itmdesc = "";
            decimal itemprice = 0;
            // Locate itemid
            item finditemds = new item();
            finditemds.immaster.Clear();
            this.ClearParameters();
            this.AddParms("item", item, "SQL");
            this.FillData(finditemds, "immaster", "wsgsp_getitem", CommandType.StoredProcedure);
            if (finditemds.immaster.Rows.Count == 0)
            {
                wsgUtilities.wsgNotice("Cannot locate item " + item + " get help");
                return;
            }
            else
            {
                itmdesc = finditemds.immaster[0].descrip;

                // Use item price if not a hardware item
                if (source.Substring(0, 2) != "HW")
                {
                    itemprice = finditemds.immaster[0].regprice;
                }
                else
                {
                    itemprice = price;
                }
                //If there are lines that item, delete them first
                foreach (DataRow row in soitemsds.soline)
                {
                    string lineitem = (string)row["item"];
                    if (lineitem == item)
                    {
                        row.Delete();
                    }
                }
                soitemsds.soline.Rows.Add();
                int rowpointer = soitemsds.soline.Rows.Count - 1;
                InitializeDataTable(soitemsds.soline, rowpointer);
                soitemsds.soline[rowpointer].source = source;
                soitemsds.soline[rowpointer].descrip = itmdesc;
                soitemsds.soline[rowpointer].item = item;
                soitemsds.soline[rowpointer].price = itemprice;
                soitemsds.soline[rowpointer].qtyord = qtyord;
            }
            soitemsds.soline.AcceptChanges();
        }

        public void GetSOItemData(string item, string CurrentFeature)
        {
            string strExpr = "item = '" + item.TrimStart().TrimEnd() + "'";
            // Use the Select method to find all rows matching the filter.
            DataRow[] foundItemRows =
            featureds.view_immasterdata.Select(strExpr);
            socurrentitemsds.soline.Rows.Add();
            int rowpointer = socurrentitemsds.soline.Rows.Count - 1;
            InitializeDataTable(socurrentitemsds.soline, rowpointer);
            socurrentitemsds.soline[rowpointer].source = foundItemRows[0]["misccode"].ToString();
            socurrentitemsds.soline[rowpointer].descrip = foundItemRows[0]["shortdescrip"].ToString();
            socurrentitemsds.soline[rowpointer].item = foundItemRows[0]["item"].ToString();
            socurrentitemsds.soline[rowpointer].qtyord = 1;
            socurrentitemsds.soline[rowpointer].qtyact = 1;
            socurrentitemsds.soline[rowpointer].price = CalculateItemPrice(foundItemRows[0]["item"].ToString(), socurrentitemsds.soline[rowpointer].qtyord);
            socurrentitemsds.soline[rowpointer].disc = somastds.somast[0].salesdisc;
        }

        #region Convert Reference table ID to description

        public string GetItemDescription(string item)
        {
            string ItemDesc = "";

            if (item != "")
            {
                getSingleSoImmasterData(item);
                if (itemds.immaster.Rows.Count > 0)
                {
                    ItemDesc = itemds.immaster[0].descrip;
                }
                else
                {
                    ItemDesc = "Item not found";
                }
            }
            else
            {
                ItemDesc = "Zero Item code";
            }
            return ItemDesc;
        }

        public string GetSoStatus()
        {
            string SoStatus = "";
            switch (somastds.somast[0].sostat)
            {
                case "C":
                    {
                        SoStatus = "Closed";
                        break;
                    }
                case "V":
                    {
                        SoStatus = "Voided";
                        break;
                    }
                default:
                    {
                        SoStatus = "Open";
                        break;
                    }
            }

            return SoStatus;
        }

        #region Refresh Totals

        public void RefreshTotals()
        {
            dttotals.Rows.Clear();
            DataRow totalsrow = dttotals.NewRow();
            somastds.somast[0].ordamt = 0;
            somastds.somast[0].tax = 0;
            somastds.somast[0].subtotal = 0;
            somastds.somast[0].taxsamt = 0;
            ClearResultsTable();
            // Results table row pointer
            int rrow = 0;

            // Add any miscellaneous items for the cover
            int currentrow = 1;
            while (currentrow <= soitemsds.soline.Rows.Count)
            {
                if (soitemsds.soline[currentrow - 1].qtyord != 0)
                {
                    soitemsds.soline[currentrow - 1].extprice = decimal.Round((soitemsds.soline[currentrow - 1].price * soitemsds.soline[currentrow - 1].qtyord), 2);
                    // Add to cover total
                    somastds.somast[0].subtotal += soitemsds.soline[currentrow - 1].extprice;
                    resultsds.soline.Rows.Add();
                    resultsds.soline[rrow].descrip = soitemsds.soline[currentrow - 1].descrip;
                    resultsds.soline[rrow].source = soitemsds.soline[currentrow - 1].source;
                    resultsds.soline[rrow].qtyord = soitemsds.soline[currentrow - 1].qtyord;
                    resultsds.soline[rrow].price = soitemsds.soline[currentrow - 1].price;
                    resultsds.soline[rrow].disc = 0;
                    resultsds.soline[rrow].extprice = soitemsds.soline[currentrow - 1].extprice;
                    rrow++;
                }
                currentrow++;
            }

            // Calculate tax here
            if (somastds.somast[0].taxrate != 0)
            {
                somastds.somast[0].taxsamt = somastds.somast[0].subtotal + somastds.somast[0].shpamt;
                somastds.somast[0].tax = decimal.Round(somastds.somast[0].taxsamt *
                somastds.somast[0].taxrate / 100, 2); ;
            }
            else
            {
                somastds.somast[0].tax = 0;
            }
            somastds.somast[0].ordamt = somastds.somast[0].subtotal + somastds.somast[0].shpamt + somastds.somast[0].tax;
            somastds.AcceptChanges();
            resultsds.soline.DefaultView.Sort = "source,descrip";
            resultsds.soline.AcceptChanges();
        }

        #endregion Refresh Totals

        public void SaveSoLineitems()
        {
            CombineCurrentItems();
            foreach (DataRow row in soitemsds.soline)
            {
                row["sono"] = somastds.somast[0].sono;
                GenerateAppTableRowSave(row);
            }

            // Locate any items that were in the orginal items and are no longer present, and delete them
            foreach (DataRow row in dtsolineOriginal.Rows)
            {
                string strExpr = "idcol = " + row["idcol"].ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundsoline = soitemsds.soline.Select(strExpr);
                if (foundsoline.Length < 1)
                {
                    // The original item no longer exists. Delete it from the source table
                    this.ClearParameters();
                    this.AddParms("@idcol", row["idcol"], "SQL");
                    try
                    {
                        ExecuteCommand("wsgsp_deletesoline", CommandType.StoredProcedure);
                    }
                    catch (SqlException ex)
                    {
                        HandleException(ex);
                    }
                }
            }
        }

        public void DeleteCurrentItemsRow(DataGridView myDataGridView)
        {
            CurrencyManager xCM =
           (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
            myDataGridView.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            string item = (string)xRow["item"];
            // Delete any similar items in the soitemsda.soline table
            //If there are items for that feature, delete them first
            foreach (DataRow row in soitemsds.soline)
            {
                if (item == (string)row["item"])
                {
                    row.Delete();
                }
            }
            soitemsds.soline.AcceptChanges();
            xDRV.Row.Delete();
        }

        public void EstablishSolineWorkTable()
        {
            order ds = new order();
            solineds = ds;
        }

        public void CreateTotalsTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("allcovers", typeof(Decimal));
            dt.Columns.Add("thiscover", typeof(Decimal));
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = 0;
            dttotals = dt;
        }

        public void CreateFeaturesTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("feature", typeof(String));
            dt.Columns.Add("code", typeof(String));
            DataRow dr = dt.NewRow();
            dr[0] = "";
            dr[1] = "";
            dtfeatures = dt;
        }

        public void CreateResultsTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("descrip", typeof(String));
            dt.Columns.Add("qtyord", typeof(Int32));
            dt.Columns.Add("price", typeof(Decimal));
            dt.Columns.Add("source", typeof(string));
            dt.Columns.Add("extprice", typeof(Decimal));
            dt.Columns.Add("disc", typeof(Decimal));
            dt.DefaultView.Sort = "source";
            dtresults = dt;
        }

        public void ClearResultsTable()
        {
            resultsds.soline.Rows.Clear();
            resultsds.AcceptChanges();
        }

        public void EstablishBlankSOarcustData()
        {
            EstablishBlankDataTableRow(ards.arcust);
        }

        public void EstablishBlankSoaddrData()
        {
            EstablishBlankDataTableRow(somastds.soaddr);
            somastds.soaddr[0].custno = somastds.somast[0].custno;
            somastds.soaddr[0].sono = somastds.somast[0].sono;
            somastds.soaddr[0].custno = somastds.somast[0].custno;
            somastds.soaddr[0].somastid = somastds.somast[0].idcol;
            somastds.soaddr[0].notify1 = "Y";
            somastds.soaddr[0].notify2 = "EMAIL";
        }

        public void EstablishBlankSolineData()
        {
            solineds.soline.Rows.Add();
            InitializeDataTable(solineds.soline, solineds.soline.Rows.Count - 1);
            solineds.soline[0].sono = somastds.somast[0].sono;
        }

        public int GetSomastBySono(string sono)
        {
            somastds.somast.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(somastds, "somast", "wsgsp_getsomastbysono", CommandType.StoredProcedure);
            if (somastds.somast.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                // Get Soaddr data
                somastds.soaddr.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@somastid", somastds.somast[0].idcol, "SQL");
                this.FillData(somastds, "soaddr", "wsgsp_getsinglesoaddrdata", CommandType.StoredProcedure);
                if (somastds.soaddr.Rows.Count == 0)
                {
                    // Add a blank soaddr record
                    EstablishBlankSoaddrData();
                }

                return somastds.somast[0].idcol;
            }
        }

        public void getSingleCustomerData(int Custid)
        {
            ards.arcust.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", Custid, "SQL");
            this.FillData(ards, "arcust", "wsgsp_getcustomerdata", CommandType.StoredProcedure);
        }

        public void EstablishBlankSomastData()
        {
            EstablishBlankDataTableRow(somastds.somast);
            somastds.somast[0].sodate = DateTime.Now;
            somastds.somast[0].ordate = DateTime.Now;
            somastds.somast[0].adduser = AppUserClass.AppUserId;
            somastds.somast[0].lckuser = AppUserClass.AppUserId;
            somastds.somast[0].lckdate = DateTime.Now;
            somastds.somast[0].adddate = DateTime.Now;
            somastds.somast[0].tax = 0.00M;
        }

        public void SaveOrderData()
        {
            CombineCurrentItems();
            RefreshTotals();
            // Check for duplicate order on addition
            if (somastds.somast[0].idcol < 0)
            {
                dupecheckds.somast.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@custno", somastds.somast[0].custno, "SQL");
                this.AddParms("@ponum", somastds.somast[0].ponum, "SQL");
                this.AddParms("@oldplan", somastds.somast[0].oldplan, "SQL");
                this.FillData(dupecheckds, "somast", "wsgsp_getsomastbycustnoponum", CommandType.StoredProcedure);
                if (dupecheckds.somast.Rows.Count > 0)
                {
                    BindingSource bindingSoDupeData = new BindingSource();
                    FrmSoDupes frmSoDupes = new FrmSoDupes();

                    frmSoDupes.dataGridViewDupes.RowsDefaultCellStyle.BackColor = Color.LightGray;
                    frmSoDupes.dataGridViewDupes.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
                    frmSoDupes.dataGridViewDupes.AutoGenerateColumns = false;
                    frmSoDupes.dataGridViewDupes.DataSource = bindingSoDupeData;
                    bindingSoDupeData.DataSource = dupecheckds.somast;
                    frmSoDupes.ShowDialog();
                }
            }

            SaveSomastData();
            SaveSoLineitems();
        }

        public void ProcessShipToSelection(int SelectedShipToId)
        {
            if (SelectedShipToId == 888888)
            {
                somastds.soaddr[0].cshipno = ards.arcust[0].custno;
                somastds.soaddr[0].adtype = "D";
                somastds.soaddr[0].company = ards.arcust[0].company;
                somastds.soaddr[0].address1 = ards.arcust[0].address1;
                somastds.soaddr[0].address2 = ards.arcust[0].address2;
                somastds.soaddr[0].email = ards.arcust[0].email;
                somastds.soaddr[0].city = ards.arcust[0].city;
                somastds.soaddr[0].state = ards.arcust[0].state;
                somastds.soaddr[0].zip = ards.arcust[0].zip;
                somastds.soaddr[0].lckdate = DateTime.Now;
                somastds.soaddr[0].lckuser = AppUserClass.AppUserId;
            }
            else
            {
                ards.aracadr.Rows.Clear();
                ClearParameters();
                this.ClearParameters();
                this.AddParms("@idcol", SelectedShipToId, "SQL");
                string commandtext = "SELECT * FROM aracadr WHERE idcol = @idcol";
                this.FillData(ards, "aracadr", commandtext, CommandType.Text);
                if (ards.aracadr[0].defaship == "Y")
                {
                    somastds.soaddr[0].adtype = "D";
                }
                else
                {
                    somastds.soaddr[0].adtype = "S";
                }
                somastds.soaddr[0].cshipno = ards.aracadr[0].cshipno;
                getSingleShipToData(SelectedShipToId);
                somastds.soaddr[0].company = ards.aracadr[0].company;
                somastds.soaddr[0].address1 = ards.aracadr[0].address1;
                somastds.soaddr[0].address2 = ards.aracadr[0].address2;
                somastds.soaddr[0].email = ards.aracadr[0].email;
                somastds.soaddr[0].city = ards.aracadr[0].city;
                somastds.soaddr[0].state = ards.aracadr[0].state;
                somastds.soaddr[0].zip = ards.aracadr[0].zip;
                somastds.soaddr[0].lckdate = DateTime.Now;
                somastds.soaddr[0].lckuser = AppUserClass.AppUserId;
            }
        }

        public void ClearLineItemData()
        {
            featureds.view_immasterdata.Rows.Clear();
        }

        public void PopulateShipTo()
        {
            // Populate the ship-to from somast
            somastds.soaddr[0].adtype = "O";
            somastds.soaddr[0].cshipno = "999999";
            somastds.soaddr[0].company = somastds.somast[0].fname.TrimEnd() +
             " " + somastds.somast[0].lname.TrimEnd();
            somastds.soaddr[0].address1 = somastds.somast[0].address.TrimEnd();
            somastds.soaddr[0].city = somastds.somast[0].city.TrimEnd();
            somastds.soaddr[0].state = somastds.somast[0].state.TrimEnd();
            somastds.soaddr[0].zip = somastds.somast[0].zip.TrimEnd();
            somastds.soaddr.AcceptChanges();
        }

        public bool SaveSomastData()
        {
            // Save ornum to meycono
            // Use the order amount and tax from the first version
            GenerateAppTableRowSave(somastds.somast[0]);

            // Ship to address
            // Retrieve
            string holdsono = somastds.somast[0].sono;
            somastds.somast.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", holdsono, "SQL");
            this.FillData(somastds, "somast", "wsgsp_getsomastbysono", CommandType.StoredProcedure);

            somastds.soaddr[0].sono = somastds.somast[0].sono;
            somastds.soaddr[0].somastid = somastds.somast[0].idcol;
            GenerateAppTableRowSave(somastds.soaddr[0]);
            return true;
        }

        public bool ValidateSO(string CurentFeature, bool saving)
        {
            bool SoOk = true;
            if (SoOk == true)
            {
                SoOk = ValidateSOHead(saving);
            }
            return SoOk;
        }

        public bool ValidateSOHead(bool saving)
        {
            bool SomastOk = true;
            return SomastOk;
        }

        public void LogOrderError(string errorcode, bool fatalerror)
        {
            string errormessage = "";
            try
            {
                errormessage = errorlist[errorcode];
            }
            catch (KeyNotFoundException)
            {
                errormessage = "Undefined Error";
            }

            wsgUtilities.wsgNotice("Cover Error " + errorcode + " " + errormessage);
            if (fatalerror == true)
            {
                wsgUtilities.wsgNotice("Error must be corrected before saving.");
            }
        }

        public void getSingleShipToData(int idcol)
        {
            ards.aracadr.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(ards, "aracadr", "wsgsp_getsingleshiptodata", CommandType.StoredProcedure);
        }

        public void getDefaultShipToData(int Custid)
        {
            ards.aracadr.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@custid", Custid, "SQL");
            this.FillData(ards, "aracadr", "wsgsp_getdefaultshipto", CommandType.StoredProcedure);
        }

        public void GetSoSearchData(string sono, string ponum, string custno, string lname, string meycono,
         DateTime begindate, DateTime enddate)
        {
            somastds.view_somastdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@ponum", ponum, "SQL");
            this.AddParms("@custno", custno, "SQL");
            this.AddParms("@lname", lname, "SQL");
            this.AddParms("@meycono", meycono, "SQL");
            this.AddParms("@begindate", begindate, "SQL");
            this.AddParms("@enddate", enddate, "SQL");
            this.FillData(somastds, "view_somastdata", "wsgsp_searchsomast", CommandType.StoredProcedure);
        }

        public int getCustomerDatabyCustno(string Custno)
        {
            this.ClearParameters();
            this.AddParms("@custno", Custno, "SQL");
            this.FillData(ards, "arcust", "wsgsp_getcustomerdatabycustno", CommandType.StoredProcedure);

            if (ards.arcust.Rows.Count > 0)
            {
                return ards.arcust[0].idcol;
            }
            else
            {
                return 0;
            }
        }

        public string CaptureSono(DataGridView myDataGridView)
        {
            CurrencyManager xCM =
           (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
           myDataGridView.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Return the sono
            return (string)xRow["sono"];
        }

        public void setfeaturerow(string feature, string code)
        {
            DataRow newFeaturesRow = dtfeatures.NewRow();
            newFeaturesRow["feature"] = feature;
            newFeaturesRow["code"] = code;
            dtfeatures.Rows.Add(newFeaturesRow);
        }

        public void LoadLineItems()
        {
            // This line prevents the deletion of the lines being copied
            dtsolineOriginal.Rows.Clear();
            DataRow itemRow;

            soitemsds.soline.Rows.Clear();
            socurrentitemsds.soline.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", somastds.somast[0].sono, "SQL");
            this.FillData(soitemsds, "soline", "wsgsp_findSolineDatabySono", CommandType.StoredProcedure);
            // Copy idcolumn of items to holding table. We'll use that for tracking deletions
            DataTable dt = new DataTable();
            dt.Columns.Add("idcol", typeof(Int32));
            foreach (DataRow row in soitemsds.soline)
            {
                dt.ImportRow(row);
            }
            dtsolineOriginal = dt;

            // Load miscellaneous line items
            int itemrowcount = 1;
            int itemrow = 0;
            // Transfer the miscellaneous line items back and forth
            tempds.soline.Rows.Clear();
            while (itemrowcount <= soitemsds.soline.Rows.Count)
            {
                tempds.soline.Rows.Add();
                CopyDatarow(soitemsds.soline[itemrow], tempds.soline[itemrow]);
                itemrow++;
                itemrowcount++;
            }
            tempds.soline.AcceptChanges();
            itemrow = 0;
            itemrowcount = 1;
            soitemsds.soline.Rows.Clear();
            while (itemrowcount <= tempds.soline.Rows.Count)
            {
                itemRow = soitemsds.soline.NewRow();
                InitializeDataRow(itemRow);
                soitemsds.soline.Rows.Add(itemRow);
                CopyDatarow(tempds.soline[itemrow], soitemsds.soline[itemrow]);
                itemrow++;
                itemrowcount++;
            }
            socurrentitemsds.soline.Rows.Clear();
        }

        public string getSysreferenceDescrip(int idcol)
        {
            string refdescrip = "Unidentified";
            referencequeryds.sysreference.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(referencequeryds, "sysreference", "wsgsp_getsinglesysreference", CommandType.StoredProcedure);
            if (referencequeryds.sysreference.Rows.Count > 0)
            {
                refdescrip = referencequeryds.sysreference[0].refdescrip;
            }
            return refdescrip;
        }

        public decimal CalculateItemPrice(string item, decimal qtyord)
        {
            // Load the item table and establish item variables
            itemds.immaster.Rows.Clear();
            string CommandString = "SELECT * FROM immaster WHERE item = @item";
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.FillData(itemds, "immaster", CommandString, CommandType.Text);
            decimal itemprice = itemds.immaster[0].regprice;
            bool ItemPriced = false;
            decimal foundprice = 0;

            // If the customer is marked as List Price, use list price
            if (ards.arcust[0].pricecode == "LST")
            {
                ItemPriced = true;
                foundprice = itemds.immaster[0].regprice;
            }

            // Not list price
            if (!ItemPriced)
            {
                somastds.slprice.Rows.Clear();
                CommandString = "SELECT * FROM slprice WHERE partgr = @item AND delmark = 0";
                this.ClearParameters();
                this.AddParms("@item", item, "SQL");
                this.FillData(somastds, "slprice", CommandString, CommandType.Text);

                if (somastds.slprice.Rows.Count > 0)
                {
                    // There are special prices for this item.

                    // Look for a specific price for this item for this customer.
                    for (int r = 0; r <= somastds.slprice.Rows.Count - 1; r++)
                    {
                        if (somastds.slprice[r].custcode == "S" && somastds.slprice[r].customer == ards.arcust[0].custno)
                        {
                            ItemPriced = true;
                            foundprice = FindItemPrice(qtyord, r, itemprice);
                            break;
                        }
                    }
                    // Look for a specific price schedule for this item for this customer.
                    for (int r = 0; r <= somastds.slprice.Rows.Count - 1; r++)
                    {
                        if (somastds.slprice[r].custcode == "L" && somastds.slprice[r].customer.TrimEnd() == ards.arcust[0].pricecode.TrimEnd())
                        {
                            ItemPriced = true;
                            foundprice = FindItemPrice(qtyord, r, itemprice);
                            break;
                        }
                    }

                    if (!ItemPriced)
                    {
                        // No special prices for customer
                        // Look for a  price schedule for this item with the default price code.
                        for (int r = 0; r <= somastds.slprice.Rows.Count - 1; r++)
                        {
                            // Ignore customer based price schedule
                            if (somastds.slprice[r].custcode == "S")
                            {
                                continue;
                            }
                            if (somastds.slprice[r].custcode.TrimEnd() == "L" && somastds.slprice[r].customer.TrimEnd() == "DEF")
                            {
                                ItemPriced = true;
                                foundprice = FindItemPrice(qtyord, r, itemprice);
                                break;
                            }
                        }
                        if (!ItemPriced)
                        {
                            foundprice = itemprice;
                        }
                    }
                }
                else
                {
                    foundprice = itemprice;
                }
            }
            return foundprice;
        }

        public decimal FindItemPrice(decimal qtyord, int r1, decimal itemprice)
        {
            bool pricefound = false;
            decimal foundprice = 0;
            // Check each quantity bracket to find the price for that that quantity
            // Bracket 1
            if (qtyord >= somastds.slprice[r1].tierqty1 && (qtyord < somastds.slprice[r1].tierqty2 || somastds.slprice[r1].tierqty2 == 0))
            {
                pricefound = true;
                if (somastds.slprice[r1].disctype == "U")
                {
                    foundprice = somastds.slprice[r1].amount1;
                }
                else
                {
                    foundprice = ComputeDiscountedPrice(somastds.slprice[r1].disctype, somastds.slprice[r1].amount1, itemprice);
                }
            }
            if (!pricefound)
            {
                // Bracket 2
                if (qtyord >= somastds.slprice[r1].tierqty2 && (qtyord < somastds.slprice[r1].tierqty3 || somastds.slprice[r1].tierqty3 == 0))
                {
                    pricefound = true;
                    if (somastds.slprice[r1].disctype == "U")
                    {
                        foundprice = somastds.slprice[r1].amount2;
                    }
                    else
                    {
                        foundprice = ComputeDiscountedPrice(somastds.slprice[r1].disctype, somastds.slprice[r1].amount2, itemprice);
                    }
                }
            }
            if (!pricefound)
            {
                // Bracket  3
                if (qtyord >= somastds.slprice[r1].tierqty3 && (qtyord < somastds.slprice[r1].tierqty4 || somastds.slprice[r1].tierqty4 == 0))
                {
                    pricefound = true;
                    if (somastds.slprice[r1].disctype == "U")
                    {
                        foundprice = somastds.slprice[r1].amount3;
                    }
                    else
                    {
                        foundprice = ComputeDiscountedPrice(somastds.slprice[r1].disctype, somastds.slprice[r1].amount3, itemprice);
                    }
                }
            }
            if (!pricefound)
            {
                // Bracket 4
                if (qtyord >= somastds.slprice[r1].tierqty4 && (qtyord < somastds.slprice[r1].tierqty5 || somastds.slprice[r1].tierqty5 == 0))
                {
                    pricefound = true;
                    if (somastds.slprice[r1].disctype == "U")
                    {
                        foundprice = somastds.slprice[r1].amount4;
                    }
                    else
                    {
                        foundprice = ComputeDiscountedPrice(somastds.slprice[r1].disctype, somastds.slprice[r1].amount4, itemprice);
                    }
                }
            }

            if (!pricefound)
            {
                // Bracket 5
                if (qtyord >= somastds.slprice[r1].tierqty5 && (qtyord < somastds.slprice[r1].tierqty6 || somastds.slprice[r1].tierqty6 == 0))
                {
                    pricefound = true;
                    if (somastds.slprice[r1].disctype == "U")
                    {
                        foundprice = somastds.slprice[r1].amount5;
                    }
                    else
                    {
                        foundprice = ComputeDiscountedPrice(somastds.slprice[r1].disctype, somastds.slprice[r1].amount5, itemprice);
                    }
                }
            }

            if (!pricefound)
            {
                // Bracket 6
                if (qtyord >= somastds.slprice[r1].tierqty6)
                {
                    pricefound = true;
                    if (somastds.slprice[r1].disctype == "U")
                    {
                        foundprice = somastds.slprice[r1].amount6;
                    }
                    else
                    {
                        foundprice = ComputeDiscountedPrice(somastds.slprice[r1].disctype, somastds.slprice[r1].amount6, itemprice);
                    }
                }
            }

            return foundprice;
        }

        public decimal ComputeDiscountedPrice(string pmeth, decimal factor, decimal itemprice)
        {
            decimal foundprice = itemprice;
            switch (pmeth)
            {
                case "D":
                    {
                        foundprice = itemprice - factor;
                        break;
                    }

                case "%":
                    {
                        foundprice = itemprice - decimal.Round(itemprice * (factor / 100), 2);
                        break;
                    }
            }
            return foundprice;
        }

        public string RouteSo()
        {
            string trackingresult = trackingInf.RouteToNextStep(somastds.somast[0].sono);
            return trackingresult;
        }
    } // class

    #endregion Convert Reference table ID to description
} // namespace

#endregion