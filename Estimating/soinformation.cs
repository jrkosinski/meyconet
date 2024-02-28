using CommonAppClasses;
using Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Ticketing;
using WSGUtilitieslib;

namespace Estimating
{
    # region SO Information

    public class Soinf : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private MiscellaneousDataMethods miscDataMethods = new MiscellaneousDataMethods("SQL", "SQLConnString");
        private AlereDataMethods alereDataMethods = new AlereDataMethods();
        private InvoicingMethods invoicingMethods = new InvoicingMethods();
        public Form menuForm { get; set; }
        public GetSoMethods getSoMethods = new GetSoMethods("SQL", "SQLConnString");
        public price prds { get; set; }
        public quote somastds { get; set; }
        public customer ards { get; set; }
        public quoterpt quorptds { get; set; }
        public quote clineds { get; set; }
        public alereds AlereDs = new alereds();
        public quote tempext1lineds { get; set; }
        public quote quotesearchds = new quote();
        public quote quoteds = new quote();
        public quote tempext2lineds { get; set; }
        public quote tempext3lineds { get; set; }
        private WarrantySynchronization warrantySynch = new WarrantySynchronization("SQL", "SQLConnString");
        public quote dupecheckds { get; set; }
        public quote tempext4lineds { get; set; }
        public quote solineds { get; set; }
        public FrmSOHead parentform { get; set; }
        public quote soitemsds { get; set; }
        public system referencequeryds { get; set; }
        public quote tempds { get; set; }
        public inspection inspds { get; set; }
        public quote socurrentitemsds { get; set; }
        public item itemds { get; set; }
        public InventoryMethods invMethods = new InventoryMethods("SQL", "SQLConnString");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");
        public reference soreferenceds { get; set; }
        private ticketds ticketDs = new ticketds();
        public item featureds { get; set; }
        public quote resultsds { get; set; }
        public DataTable dtresults { get; set; }
        public DataTable dttotals { get; set; }
        public DataTable dtfeatures { get; set; }
        public DataTable dtsolineOriginal { get; set; }
        public DataTable dtcoverdimensions { get; set; }
        public DataTable dtdeposittiers { get; set; }
        public Dictionary<string, string> errorlist = new Dictionary<string, string>();
        public ImmasterAccess immasterAccess = new ImmasterAccess("SQL", "SQLConnString");

        public bool IsOpen
        {
            get { return this.GetSoStatus() == "Open";  }
        }

        public Soinf(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            somastds = new quote();
            SetIdcol(somastds.somast.idcolColumn);
            SetIdcol(somastds.socover.idcolColumn);
            SetIdcol(somastds.soversion.idcolColumn);
            SetIdcol(somastds.soline.idcolColumn);
            SetIdcol(somastds.soaddr.idcolColumn);
            SetIdcol(somastds.somast.idcolColumn);
            SetIdcol(somastds.socover.idcolColumn);
            SetIdcol(somastds.soversion.idcolColumn);
            SetIdcol(somastds.soline.idcolColumn);
            SetIdcol(somastds.soaddr.idcolColumn);
            resultsds = new quote();
            referencequeryds = new system();
            clineds = new quote();
            inspds = new inspection();
            SetIdcol(clineds.socover.idcolColumn);
            prds = new price();
            SetIdcol(prds.quprsdetail.idcolColumn);
            SetIdcol(prds.quprshead.idcolColumn);
            SetIdcol(prds.quprslocator.idcolColumn);
            ards = new customer();
            SetIdcol(ards.arcust.idcolColumn);
            SetIdcol(ards.aracadr.idcolColumn);
            quorptds = new quoterpt();
            CreateResultsTable();
            CreateTotalsTable();
            CreateFeaturesTable();
            itemds = new item();
            tempext1lineds = new quote();
            tempext2lineds = new quote();
            tempext3lineds = new quote();
            tempext4lineds = new quote();
            dupecheckds = new quote();
            SetIdcol(tempext1lineds.socover.idcolColumn);
            SetIdcol(tempext2lineds.socover.idcolColumn);
            SetIdcol(tempext3lineds.socover.idcolColumn);
            SetIdcol(tempext4lineds.socover.idcolColumn);
            tempds = new quote();
            socurrentitemsds = new quote();
            SetIdcol(socurrentitemsds.soline.idcolColumn);
            soreferenceds = new reference();
            soitemsds = new quote();
            SetIdcol(soitemsds.soline.idcolColumn);
            featureds = new item();
            errorlist.Add("100", "You must specify an item for the cover");
            errorlist.Add("101", "The item specified for the cover is invalid");
            errorlist.Add("102", "There is no price per square foot for this cover. Correct before saving.");
            errorlist.Add("200", "Square footage exceeds 1500. The overlap must be at least 18");
            errorlist.Add("201", "Square footage exceeds 1150. The overlap must be at least 15");
            errorlist.Add("203", "Obstructions over 12 feet require 3X3 spacing");
            errorlist.Add("204", "Material Solid with Pump requires a pump item");
            errorlist.Add("205", "Grecian Cover requires cut corners");
            errorlist.Add("206", "Mocha or Gray Color are only available in 3x3 spacing... If you must do it in something other than 3x3 then it must have a 15% Upcharge");
            errorlist.Add("207", "The number of Lawn Pins exceeds 25% of the ending hardware");
            errorlist.Add("208", "The number of Short Springss exceeds 35% of the ending hardware");
            errorlist.Add("209", "The number of Lawn Stakes exceeds 25% of the ending hardware");
            errorlist.Add("210", "Material Solid with Drain requires a drain item");
            errorlist.Add("211", "The number of pipes exceeds 35% of the ending hardware");
            errorlist.Add("300", "The color on the stock cover has changed. The cover price should be higher than the item price");
        }

        // Set dedicated items
        public string PumpItem = "PUMP";

        public string DrainItem = "DRAIN";
        public string GrecianCornerItem = "CC";
        public string StanchorItem = "SCRANC";
        public string DeckFlangeItem = "DKFLG";
        public string PopupAnchorItem = "POPUP";
        public string SpringCoverItem = "SPRCOV";
        public string RegularSpringItem = "REGSPR";
        public string ShortSpringItem = "SHTSPR";
        public string HDSpringItem = "HDSPR";
        public string Pipe9Item = "HPAS9";
        public string Pipe18Item = "HPAS";
        public string LawnstakeItem = "HALSPK";
        public string LawnPinItem = "HAL";
        public string[] MaterialDescription = { " " };
        public string[] ColorDescription = { " " };
        public string[] SpacingDescription = { " " };
        public string[] OverlapDescription = { " " };


        public bool ColorHasChanged
        {
            get { return this.clineds.socover != null && this.clineds.socover.Count > 0 && this.clineds.socover[0].colorChanged; }
        }

        public bool MaterialHasChanged
        {
            get { return this.clineds.socover != null && this.clineds.socover.Count > 0 && this.clineds.socover[0].materialChanged; }
        }

        public void getSingleImmasterData(string item)
        {
            itemds.immaster.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.FillData(itemds, "immaster", "wsgsp_getsingleimmasterdata", CommandType.StoredProcedure);
        }

        public void LoadAllCoverData(string sono, string version, string cover, string product, bool refreshLineItemsFromImmaster = false)
        {
            LoadCoverlineData(sono, version, cover, product);
            LoadExtensionLine1(sono, version, cover);
            LoadExtensionLine2(sono, version, cover);
            LoadExtensionLine3(sono, version, cover);
            LoadExtensionLine4(sono, version, cover);
            LoadMiscellandousSoLineData(sono, version, cover, refreshLineItemsFromImmaster);
        }

        public string getsono()
        {
            return appInformation.GetNextSono();
        }

        public string CreateInvoice(quote sods, DateTime invdate)
        {
            string invno = invoicingMethods.CreateInvoice(sods.somast[0].sono, invdate);
            sods.somast[0].invno = invno;
            sods.somast[0].invno = invno;
            sods.somast[0].invdte = invdate;
            sods.somast[0].sostat = "C";
            SaveSomastData();

            // Update warranty data
            warrantySynch.SynchronizeWarranty(sods.somast[0].sono);
            // Update inventory for items being shipped
            invMethods.ProcessCoverShipment(sods.somast[0].sono, sods.somast[0].defloc, invdate);
            return invno;
        }

        public void getallsoreportdata(string sono)
        {
            if (!String.IsNullOrEmpty(sono))
            {
                quorptds.view_soreportlinedata.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@sono", sono, "SQL");
                this.FillData(quorptds, "view_soreportlinedata", "wsgsp_getallsoreportlinedata", CommandType.StoredProcedure);
                // somast
                quorptds.somast.Rows.Clear();
                quorptds.somast.ImportRow(somastds.somast.Rows[0]);
                // soaddr
                quorptds.soaddr.Rows.Clear();
                quorptds.soaddr.ImportRow(somastds.soaddr.Rows[0]);
                // arcust
                quorptds.arcust.Rows.Clear();
                quorptds.arcust.ImportRow(ards.arcust.Rows[0]);
            }
        }

        public Dictionary<string, List<quoterpt.view_soreportlinedataRow>> GetCoversByVersion(string sono)
        {
            var output = new Dictionary<string, List<quoterpt.view_soreportlinedataRow>>(); 

            //populate the data if not yet populated 
            getallsoreportdata(sono);

            for(int n=0; n<this.quorptds.view_soreportlinedata.Rows.Count; n++) {
                quoterpt.view_soreportlinedataRow row = this.quorptds.view_soreportlinedata[n];
                if (row.source.Trim().ToUpper() == "C")
                {
                    if (!output.ContainsKey(row.version))
                        output.Add(row.version, new List<quoterpt.view_soreportlinedataRow>());

                    output[row.version].Add(row);
                }
            }

            return output; 
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
                string CurrentFeature = socurrentitemsds.soline[0].source.Substring(0, 2);
                //If there are items for that feature, delete them first
                foreach (DataRow row in soitemsds.soline)
                {
                    string source = row["source"].ToString().Substring(0, 2);
                    if (source == CurrentFeature)
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

        public void ShowTickets()
        {
            TicketMethods ticketMethods = new TicketMethods("SQL", "SQLConnString");
            ticketMethods.StartSoTicket(somastds.somast[0].sono);
        }

        public void DeleteSocover(string version, string cover)
        {
            this.ClearParameters();
            this.AddParms("@sono", somastds.somast[0].sono, "SQL");
            this.AddParms("@version", version, "SQL");
            this.AddParms("@cover", cover, "SQL");
            try
            {
                ExecuteCommand("wsgsp_deletesocover", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void DeleteSoversion(string sono, string version)
        {
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@version", version, "SQL");
            try
            {
                ExecuteCommand("wsgsp_deletesoversion", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void FillColorAndMaterial(string item)
        {
            immasterAccess.getSingleImmasterData(item);
            if (immasterAccess.itemds.immaster.Rows.Count != 0)
            {
                clineds.socover[0].colorid = GetColorIdcol(immasterAccess.itemds.immaster[0].color);
                clineds.socover[0].materialid = GetMaterialIdcol(immasterAccess.itemds.immaster[0].material);
            }
        }

        public string LockSomast(int idcol)
        {
            string returnmessage = "";
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "somast", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);
            try
            {
                string somaststatus = this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
                if (somaststatus != "OK")
                {
                    if (wsgUtilities.wsgReply(somaststatus + ". Do you want to override the lock?"))
                    {
                        string commandstring = " UPDATE somast SET lckstat = 'L' , lckdate = GETDATE(), lckuser = @userid  where idcol= @idcol ";
                        this.ClearParameters();
                        this.AddParms("@idcol", idcol, "SQL");
                        this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
                        try
                        {
                            ExecuteCommand(commandstring, CommandType.Text);
                            returnmessage = "OK";
                        }
                        catch (SqlException ex)
                        {
                            HandleException(ex);
                            returnmessage = ex.Message;
                        }
                    }
                    else
                    {
                        returnmessage = "Failed to lock";
                    }
                }
                else
                {
                    returnmessage = somaststatus;
                }
            }
            catch (SqlException ex)
            {
                HandleException(ex);
                returnmessage = "Locking failed";
            }
            return returnmessage;
        }

        public string GetFeatures(string featurecode, string CurrentFeature)
        {
            // Save any line items from a previous feature
            CombineCurrentItems();

            if (clineds.socover[0].product.TrimEnd() != "Stock Cover" || ((clineds.socover[0].product.TrimEnd() == "Stock Cover") &&
               (featurecode == "HW" || featurecode == "MI" || featurecode == "DR")))
            {
                // Load any prior items for this feature
                socurrentitemsds.soline.Rows.Clear();
                for (int r = 0; r <= soitemsds.soline.Rows.Count - 1; r++)
                {
                    if (soitemsds.soline[r].source.Substring(0, 2) == featurecode)
                    {
                        socurrentitemsds.soline.ImportRow(soitemsds.soline.Rows[r]);
                    }
                }
                socurrentitemsds.AcceptChanges();
                // Populate the feature selector table
                featureds.view_immasterdata.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@code", featurecode, "SQL");
                this.FillData(featureds, "view_immasterdata", "wsgsp_getquoteitem", CommandType.StoredProcedure);
            }
            else
            {
                wsgUtilities.wsgNotice("That feature is not available with this cover");
                featurecode = "";
            }
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

        public void EstablishCoverItemsTable(string product)
        {
            string ItemProduct = product.TrimEnd();
            string covercode = "";
            switch (ItemProduct)
            {
                case "Stock Cover":
                    {
                        covercode = "ST";
                        break;
                    }
                case "Worksheet":
                    {
                        covercode = "WK";
                        break;
                    }
                default:
                    {
                        covercode = "CU";
                        break;
                    }
            }
            // Covers
            soreferenceds.view_immasterdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@code", covercode, "SQL");
            this.FillData(soreferenceds, "view_immasterdata", "wsgsp_getquoteitem", CommandType.StoredProcedure);
        }

        public void EstablishReferenceTables()
        {
            this.ClearParameters();
            // Spacing
            soreferenceds.view_quspacingdata.Rows.Clear();
            this.FillData(soreferenceds, "view_quspacingdata", "wsgsp_getquspacingdata", CommandType.StoredProcedure);
            // Material
            soreferenceds.view_qumaterialdata.Rows.Clear();
            this.FillData(soreferenceds, "view_qumaterialdata", "wsgsp_getqumaterialdata", CommandType.StoredProcedure);
            // Color
            soreferenceds.view_qucolordata.Rows.Clear();
            this.FillData(soreferenceds, "view_qucolordata", "wsgsp_getqucolordata", CommandType.StoredProcedure);
            // Overlap
            soreferenceds.view_quoverlapdata.Rows.Clear();
            this.FillData(soreferenceds, "view_quoverlapdata", "wsgsp_getquoverlapdata", CommandType.StoredProcedure);
        }

        public bool CheckDiscount(string item)
        {
            bool applydiscount = false;
            itemds.immaster.Rows.Clear();
            string CommandString = "SELECT * FROM immaster WHERE item = @item";
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.FillData(itemds, "immaster", CommandString, CommandType.Text);
            if (itemds.immaster.Count > 0)
            {
                applydiscount = itemds.immaster[0].appldisc;
            }
            return applydiscount;
        }

        public void SetSolineItem(string item, string source, Int32 qtyord, decimal price, string version, string cover)
        {
            string itmdesc = "";
            decimal itemprice = 0;
            // Locate item
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
                item = finditemds.immaster[0].item;
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
                    string lineitem = (String)row["item"];
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
                soitemsds.soline[rowpointer].version = version;
                soitemsds.soline[rowpointer].cover = cover;
                soitemsds.soline[rowpointer].qtyord = qtyord;
                soitemsds.soline[rowpointer].qtyact = qtyord;
            }
            soitemsds.soline.AcceptChanges();
        }

        public void GetQuoteItemData(string item, string CurrentFeature)
        {
            string strExpr = "item = '" + item.TrimStart().TrimEnd() + "'";
            // Use the Select method to find all rows matching the filter.
            DataRow[] foundItemRows =
            featureds.view_immasterdata.Select(strExpr);
            socurrentitemsds.soline.Rows.Add();
            int rowpointer = socurrentitemsds.soline.Rows.Count - 1;
            InitializeDataTable(socurrentitemsds.soline, rowpointer);
            socurrentitemsds.soline[rowpointer].source = foundItemRows[0]["misccode"].ToString(); ;
            socurrentitemsds.soline[rowpointer].descrip = foundItemRows[0]["shortdescrip"].ToString();
            socurrentitemsds.soline[rowpointer].item = (String)foundItemRows[0]["item"];
            socurrentitemsds.soline[rowpointer].price = Convert.ToDecimal(foundItemRows[0]["prwcov"].ToString());
            socurrentitemsds.soline[rowpointer].disc = somastds.somast[0].salesdisc;
            socurrentitemsds.soline[rowpointer].version = clineds.socover[0].version;
            socurrentitemsds.soline[rowpointer].cover = clineds.socover[0].cover;
            socurrentitemsds.soline[rowpointer].qtyord = 1;
            socurrentitemsds.soline[rowpointer].qtyact = 1;
        }

        #region Convert Reference table ID to description

        public string GetItemDescription(string item)
        {
            string ItemDesc = "";

            if (item.TrimEnd() != "")
            {
                //    getSingImmasterdata(item);
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

        public string GetColorDescription(int idcol)
        {
            string Descrip = "Undefined";
            if (idcol != 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows =
                soreferenceds.view_qucolordata.Select(strExpr);
                Descrip = foundRows[0]["descrip"].ToString();
            }
            return Descrip;
        }

        public string GetSpacingDescription(int idcol)
        {
            string Descrip = "Undefined";
            if (idcol != 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows =
                soreferenceds.view_quspacingdata.Select(strExpr);
                Descrip = foundRows[0]["descrip"].ToString();
            }

            return Descrip;
        }

        public decimal GetStrapMultiplier(int idcol)
        {
            decimal strpmult = 0;
            if (idcol != 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows =
                soreferenceds.view_quspacingdata.Select(strExpr);
                strpmult = Convert.ToDecimal(foundRows[0]["strpmult"].ToString());
            }
            else
            {
                strpmult = 0;
            }
            return strpmult;
        }

        public void CreateNewVersionData(string sono, string priorversion, string newversion,
         string custcomments, string intcomments)
        {
            EstablishBlankSoversionData();
            somastds.soversion[0].version = newversion;
            somastds.soversion[0].custcomments = custcomments;
            somastds.soversion[0].intcomments = intcomments;
            SaveSoVersion();
            DataRow itemRow;

            int itemrowcount = 1;
            int itemrow = 0;
            int rowcount = 1;
            int rrow = 0;
            LoadCoverViewData(sono, priorversion);
            while (rowcount <= somastds.view_coverdata.Rows.Count)
            {
                if (somastds.view_coverdata[rrow].covertype == "C ")
                {
                    LoadAllCoverData(sono, priorversion, somastds.view_coverdata[rrow].cover, "C ");

                    // Copy the cover data to a temp row and back. That eliminates the idcol.
                    // Cover
                    tempds.socover.Rows.Clear();
                    tempds.socover.Rows.Add();
                    CopyDatarow(clineds.socover[0], tempds.socover[0]);
                    clineds.socover.Rows.Clear();
                    EstablishBlankDataTableRow(clineds.socover);
                    CopyDatarow(tempds.socover[0], clineds.socover[0]);
                    clineds.socover[0].version = newversion;
                    // Extension 1
                    tempds.socover.Rows.Clear();
                    tempds.socover.Rows.Add();
                    CopyDatarow(tempext1lineds.socover[0], tempds.socover[0]);
                    tempext1lineds.socover.Rows.Clear();
                    tempext1lineds.socover.Rows.Add();
                    CopyDatarow(tempds.socover[0], tempext1lineds.socover[0]);
                    tempext1lineds.socover[0].version = newversion;
                    // Extension 2
                    tempds.socover.Rows.Clear();
                    tempds.socover.Rows.Add();
                    CopyDatarow(tempext2lineds.socover[0], tempds.socover[0]);
                    tempext2lineds.socover.Rows.Clear();
                    tempext2lineds.socover.Rows.Add();
                    CopyDatarow(tempds.socover[0], tempext2lineds.socover[0]);
                    tempext2lineds.socover[0].version = newversion;
                    // Extension 3
                    tempds.socover.Rows.Clear();
                    tempds.socover.Rows.Add();
                    CopyDatarow(tempext3lineds.socover[0], tempds.socover[0]);
                    tempext3lineds.socover.Rows.Clear();
                    tempext3lineds.socover.Rows.Add();
                    CopyDatarow(tempds.socover[0], tempext3lineds.socover[0]);
                    tempext3lineds.socover[0].version = newversion;
                    // Extension 4
                    tempds.socover.Rows.Clear();
                    tempds.socover.Rows.Add();
                    CopyDatarow(tempext4lineds.socover[0], tempds.socover[0]);
                    tempext4lineds.socover.Rows.Clear();
                    tempext4lineds.socover.Rows.Add();
                    CopyDatarow(tempds.socover[0], tempext4lineds.socover[0]);
                    tempext4lineds.socover[0].version = newversion;
                    SaveCoverLines();

                    // This line prevents the deletion of the lines being copied
                    dtsolineOriginal.Rows.Clear();

                    // Load miscellaneous line items
                    itemrow = 0;
                    itemrowcount = 1;
                    // Transfer the miscellaneous line items back and forth
                    tempds.soline.Rows.Clear();
                    while (itemrowcount <= soitemsds.soline.Rows.Count)
                    {
                        tempds.soline.Rows.Add();
                        CopyDatarow(soitemsds.soline[itemrow], tempds.soline[itemrow]);
                        tempds.soline[itemrow].version = newversion;
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
                    SaveSoLineitems();
                } // if (somastds.view_coverdata[rrow].covertype == "C ")
                rowcount++;
                rrow++;
            }
        }

        public decimal CalculateCoverprice(string item, decimal sqft, int spacingid, int materialid, decimal upcharge)
        {
            decimal coverprice = 0;
            if (item.TrimEnd() != "" && sqft != 0 && spacingid != 0 && materialid != 0)
            {
                // Find the price locator which indicates the price schedule, based on the item and the spacing.
                FindPriceLocatorData(item, spacingid);
                if (prds.quprslocator.Rows.Count < 1)
                {
                    wsgUtilities.wsgNotice("There is no pricing information for this item and spacing");
                    coverprice = 0;
                }
                else
                {
                    FindPriceScheduleData(prds.quprslocator[0].pschedid, sqft);
                    if (prds.quprsdetail.Rows.Count > 0)
                    {
                        // Select the price for the material chosen
                        string materialprefix = GetMaterialCode(materialid).ToUpper();
                        switch (materialprefix)
                        {
                            case "M":
                                {
                                    coverprice = prds.quprsdetail[0].mesh;
                                    break;
                                }
                            case "S":
                                {
                                    coverprice = prds.quprsdetail[0].solid;
                                    break;
                                }
                            case "T":
                                {
                                    coverprice = prds.quprsdetail[0].trampol;
                                    break;
                                }
                            case "L":
                                {
                                    coverprice = prds.quprsdetail[0].lamsolid;
                                    break;
                                }
                            case "R":
                                {
                                    coverprice = prds.quprsdetail[0].rugmesh;
                                    break;
                                }
                        }
                        if (coverprice != 0)
                        {
                            // Add price factor from price schedule
                            coverprice = coverprice + prds.quprslocator[0].prcfact;
                            // Add upcharge if needed
                            if (upcharge != 0)
                            {
                                coverprice += decimal.Round((coverprice * (upcharge / 100)), 2);
                            }
                        }
                        else
                        {
                            wsgUtilities.wsgNotice("The calculated price for this cover is zero and not allowable.");
                        }
                    }
                    else
                    {
                        wsgUtilities.wsgNotice("There is no pricing information for this item and spacing");
                    } // if (prds.quprsdetail.Rows.Count > 0)
                } //if (prds.quprslocator.Rows.Count < 1)
            }// if (itemi != "" && sqft != 0 && spacingid != 0 && materialid != 0)
            return coverprice;
        }

        public void EstablishBlankCoverDataTables()
        {
            // Cover Line
            EstablishBlankDataTableRow(clineds.socover);
            // Extension Lines
            EstablishBlankDataTableRow(tempext1lineds.socover);
            EstablishBlankDataTableRow(tempext2lineds.socover);
            EstablishBlankDataTableRow(tempext3lineds.socover);
            EstablishBlankDataTableRow(tempext4lineds.socover);

            // Miscellaneous Items
            socurrentitemsds.soline.Rows.Clear();
            EstablishBlankDataTableRow(soitemsds.soline);
            featureds.view_immasterdata.Rows.Clear();
        }

        public void SaveCoverLines()
        {
            // Save the Cover Line
            SaveSoCover(clineds);
            // Save the extensions
            SaveSoCover(tempext1lineds);
            SaveSoCover(tempext2lineds);
            SaveSoCover(tempext3lineds);
            SaveSoCover(tempext4lineds);
        }

        #region Refresh Cover

        public void RefreshCover()
        {
            parentform.LoadingLine = true;

            MaterialDescription[0] = "Unidentified";
            ColorDescription[0] = "Unidentified";
            SpacingDescription[0] = "Unidentified";
            OverlapDescription[0] = "Unidentified";

            if (parentform.textBoxCoverDesc.Text.TrimEnd().TrimStart() != "" &&
              parentform.textBoxMaterialDesc.Text.TrimEnd().TrimStart() != "" &&
              parentform.textBoxColordesc.Text.TrimEnd().TrimStart() != "" &&
              parentform.textBoxSpacingdesc.Text.TrimEnd().TrimStart() != "" &&
              parentform.textBoxOverlapdesc.Text.TrimEnd().TrimStart() != "")
            {
                parentform.groupBoxDimensions.Enabled = true;
            }
            else
            {
                parentform.groupBoxDimensions.Enabled = false;
            }

            itemds.immaster.Rows.Clear();
            bool ItemOk = true;
            // Nothing can be done until a valid cover item has been selected
            if (clineds.socover[0].item.TrimEnd() != "")
            {
                // Locate the item for the cover
                getSingleImmasterData(clineds.socover[0].item);
                if (itemds.immaster.Rows.Count > 0)
                {
                    clineds.socover[0].descrip = itemds.immaster[0].descrip;
                }
                else
                {
                    ItemOk = false;
                }
            }
            else
            {
                ItemOk = false;
            }

            if (ItemOk == true)
            {
                // Establish the features data table.
                // It is the data source of the listbox showing the features available
                dtfeatures.Rows.Clear();
                if (clineds.socover[0].product.TrimEnd() != "Cover Repair" &&
                 clineds.socover[0].product.TrimEnd() != "Cover Alteration")
                {
                    setfeaturerow("Additional Desc", "CD");
                    setfeaturerow("Hardware", "HW");
                    setfeaturerow("Obstructions", "OB");
                    setfeaturerow("Drainage", "DR");
                    setfeaturerow("Padding", "PA");
                    setfeaturerow("Miscellaneous", "MI");
                    setfeaturerow("Commercial Item", "CO");
                }
                else
                {
                    setfeaturerow("Patches", "RP");
                    setfeaturerow("Panels", "RN");
                    setfeaturerow("Webbing", "RW");
                    setfeaturerow("Special Applications", "RS");
                    setfeaturerow("Padding", "RQ");
                    setfeaturerow("Hardware", "HW");
                    setfeaturerow("Miscellaneous", "RM");
                }
            }
            if (ItemOk == true)
            {
                OverlapDescription[0] = GetOverlapDescription(clineds.socover[0].overlapid);
                ColorDescription[0] = GetColorDescription(clineds.socover[0].colorid);
                MaterialDescription[0] = GetMaterialDescription(clineds.socover[0].materialid);
                SpacingDescription[0] = GetSpacingDescription(clineds.socover[0].spacingid);

                if (clineds.socover[0].product.TrimEnd() != "Cover Repair" && clineds.socover[0].product.TrimEnd() != "Cover Alteration")
                {
                    bool CustomOverlap = false;
                    if (GetOverlapDescription(clineds.socover[0].overlapid).TrimEnd().ToUpper() == "CUSTOM OVERLAP")
                    {
                        CustomOverlap = true;
                    }
                    else
                    {
                        // Clear the pool dimensions if not custom overlap
                        ClearCoverDimensions(clineds.socover);
                        ClearCoverDimensions(tempext1lineds.socover);
                        ClearCoverDimensions(tempext2lineds.socover);
                        ClearCoverDimensions(tempext3lineds.socover);
                        ClearCoverDimensions(tempext4lineds.socover);
                    }

                    // Clear the price per sqft
                    clineds.socover[0].prcsqft = 0;

                    if (clineds.socover[0].product.TrimEnd() != "Stock Cover")
                    {
                        #region custom cover processing

                        clineds.socover[0].sqft = 0;

                        feetandinches Overlaplength = new feetandinches();

                        if (CustomOverlap == false && clineds.socover[0].plenft != 0)
                        {
                            Overlaplength = GetOverlapValue(clineds.socover[0].overlapid);
                            feetandinches coverwidth = new feetandinches();
                            coverwidth = CalculateFeetandInches(clineds.socover[0].pwidft,
                              clineds.socover[0].pwidin, Overlaplength.Feet * 2, Overlaplength.Inches * 2);
                            clineds.socover[0].cwidft = coverwidth.Feet;
                            clineds.socover[0].cwidin = coverwidth.Inches;
                            feetandinches coverlength = new feetandinches();
                            // Double overlap for pool cover.
                            coverlength = CalculateFeetandInches(clineds.socover[0].plenft,
                              clineds.socover[0].plenin, Overlaplength.Feet * 2, Overlaplength.Inches * 2);
                            clineds.socover[0].clenft = coverlength.Feet;
                            clineds.socover[0].clenin = coverlength.Inches;
                        }
                        if (clineds.socover[0].manlstr == false)
                        {
                            // Compute straps for this segment of the cover
                            clineds.socover[0].straps = CalculateCoverStraps(clineds.socover[0].cwidft, clineds.socover[0].cwidin,
                            clineds.socover[0].clenft, clineds.socover[0].clenin, clineds.socover[0].spacingid);
                        }

                        // Establish upcharge for the cover if the color is not green
                        if (ColorDescription[0].Substring(0, 5).ToUpper() != "GREEN")
                        {
                            clineds.socover[0].upcharge = somastds.somast[0].upcharge;
                        }
                        else
                        {
                            clineds.socover[0].upcharge = 0;
                        }
                        // Calculate the square feet for the cover
                        clineds.socover[0].sqft = CalculateSqft(clineds.socover[0].cwidft, clineds.socover[0].cwidin, clineds.socover[0].clenft,
                           clineds.socover[0].clenin);

                        // Calculate the price for the cover
                        clineds.socover[0].prcsqft = CalculateCoverprice(clineds.socover[0].item, clineds.socover[0].sqft, clineds.socover[0].spacingid,
                        clineds.socover[0].materialid, clineds.socover[0].upcharge);

                        // Manual price stops calculation
                        if (clineds.socover[0].manlprc == false)
                        {
                            clineds.socover[0].price = decimal.Round(clineds.socover[0].prcsqft * clineds.socover[0].sqft, 2);
                        }

                        clineds.socover[0].EndEdit();
                        // Figure cover size, pricing and straps for extensions
                        // Ext 1
                        if ((tempext1lineds.socover[0].pwidft != 0 &&
                           tempext1lineds.socover[0].plenft != 0) ||
                           (CustomOverlap == true && tempext1lineds.socover[0].cwidft != 0 &&
                            tempext1lineds.socover[0].clenft != 0))
                        {
                            CalculateExtensionDimensionsAndPrice(tempext1lineds.socover, CustomOverlap, Overlaplength);
                        }
                        // Ext 2
                        if ((tempext2lineds.socover[0].pwidft != 0 &&
                           tempext2lineds.socover[0].plenft != 0) ||
                           (CustomOverlap == true && tempext2lineds.socover[0].cwidft != 0 &&
                            tempext2lineds.socover[0].clenft != 0))
                        {
                            CalculateExtensionDimensionsAndPrice(tempext2lineds.socover, CustomOverlap, Overlaplength);
                        }
                        // Ext 3
                        if ((tempext3lineds.socover[0].pwidft != 0 &&
                           tempext3lineds.socover[0].plenft != 0) ||
                           (CustomOverlap == true && tempext3lineds.socover[0].cwidft != 0 &&
                            tempext3lineds.socover[0].clenft != 0))
                        {
                            CalculateExtensionDimensionsAndPrice(tempext3lineds.socover, CustomOverlap, Overlaplength);
                        }
                        // Ext 4
                        if ((tempext4lineds.socover[0].pwidft != 0 &&
                           tempext4lineds.socover[0].plenft != 0) ||
                           (CustomOverlap == true && tempext4lineds.socover[0].cwidft != 0 &&
                            tempext4lineds.socover[0].clenft != 0))
                        {
                            CalculateExtensionDimensionsAndPrice(tempext4lineds.socover, CustomOverlap, Overlaplength);
                        }
                        clineds.socover[0].sqft = CalculateCoverSqftWithExt();
                        #endregion custom cover processing
                    }// Custom cover processing
                    else
                    {
                        #region stock and standard covers
                        // Stock and standard covers
                        // Pool dimensions
                        clineds.socover[0].plenft = itemds.immaster[0].plenft;
                        clineds.socover[0].plenin = itemds.immaster[0].plenin;
                        clineds.socover[0].pwidft = itemds.immaster[0].pwidthft;
                        clineds.socover[0].pwidin = itemds.immaster[0].pwidthin;

                        // Cover dimensions
                        clineds.socover[0].clenft = itemds.immaster[0].clenft;
                        clineds.socover[0].clenin = itemds.immaster[0].clenin;
                        clineds.socover[0].cwidft = itemds.immaster[0].cwidthft;
                        clineds.socover[0].cwidin = itemds.immaster[0].cwidthin;
                        // Extension
                        // Pool Length
                        tempext1lineds.socover[0].plenft = itemds.immaster[0].pextlenft;
                        tempext1lineds.socover[0].plenin = itemds.immaster[0].pextlenin;
                        // Pool Width
                        tempext1lineds.socover[0].pwidft = itemds.immaster[0].pextwidft;
                        tempext1lineds.socover[0].pwidin = itemds.immaster[0].pextwidin;
                        // Cover Length
                        tempext1lineds.socover[0].clenft = itemds.immaster[0].elenft;
                        tempext1lineds.socover[0].clenin = itemds.immaster[0].elenin;
                        // Cover Width
                        tempext1lineds.socover[0].cwidft = itemds.immaster[0].ewidthft;
                        tempext1lineds.socover[0].cwidin = itemds.immaster[0].ewidthin;

                        // Manual straps stops calculation
                        if (clineds.socover[0].manlstr == false)
                        {
                            clineds.socover[0].straps = itemds.immaster[0].straps;
                        }
                        // Manual price stops calculation
                        if (clineds.socover[0].manlprc == false)
                        {
                            clineds.socover[0].price = itemds.immaster[0].regprice;
                        }
                        // Establish Color, material, spacing and overlap
                        // Use existing color if there is one
                        if (clineds.socover[0].colorid == 0)
                        {
                            clineds.socover[0].colorid = GetColorIdcol(itemds.immaster[0].color);
                        }
                        clineds.socover[0].materialid = GetMaterialIdcol(itemds.immaster[0].material);
                        clineds.socover[0].overlapid = 0;
                        clineds.socover[0].spacingid = 0;

                        #endregion stock and standard covers
                    }
                } // Cover Repair
                // Prepare the dimension strings for printing
                clineds.socover[0].coverstring = "";
                clineds.socover[0].poolstring = "";
                PrepareStringDimensons(clineds.socover);
                PrepareStringDimensons(tempext1lineds.socover);
                PrepareStringDimensons(tempext2lineds.socover);
                PrepareStringDimensons(tempext3lineds.socover);
                PrepareStringDimensons(tempext4lineds.socover);
                clineds.AcceptChanges();
                clineds.socover[0].AcceptChanges();
            }// ItemOk = true
            parentform.textBoxMaterialDesc.Text = MaterialDescription[0];
            parentform.textBoxColordesc.Text = ColorDescription[0];
            parentform.textBoxSpacingdesc.Text = SpacingDescription[0];
            parentform.textBoxOverlapdesc.Text = OverlapDescription[0];
            parentform.LoadingLine = false;
            RefreshTotals();
            parentform.Update();
        } // end refresh cover

        #endregion Refresh Cover

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

        public void PrepareStringDimensons(quote.socoverDataTable dtcover)
        {
            string thiscoverstring = "";
            string thispoolstring = "";
            if (dtcover[0].cwidft != 0)
            {
                if (clineds.socover[0].coverstring.TrimEnd() != "")
                {
                    // There is a prior cover
                    thiscoverstring += "+";
                }
                thiscoverstring += dtcover[0].cwidft.ToString().TrimEnd().TrimStart() + "'";
                // Width inches
                if (dtcover[0].cwidin != 0)
                {
                    thiscoverstring += dtcover[0].cwidin.ToString().TrimEnd().TrimStart() + "''";
                }
                if (dtcover[0].clenft != 0)
                {
                    thiscoverstring += "X";

                    thiscoverstring += dtcover[0].clenft.ToString().TrimEnd().TrimStart() + "'";
                }
                if (dtcover[0].clenin != 0)
                {
                    thiscoverstring += dtcover[0].clenin.ToString().TrimEnd().TrimStart() + "''";
                }
            }
            clineds.socover[0].coverstring += thiscoverstring;
            if (dtcover[0].pwidft != 0)
            {
                if (clineds.socover[0].poolstring.TrimEnd() != "")
                {
                    // There is a prior pool
                    thispoolstring = "+";
                }
                thispoolstring += dtcover[0].pwidft.ToString().TrimEnd().TrimStart() + "'";
                if (dtcover[0].pwidin != 0)
                {
                    thispoolstring += dtcover[0].pwidin.ToString().TrimEnd().TrimStart() + "''";
                }
            }

            // Length feet
            if (dtcover[0].plenft != 0)
            {
                thispoolstring += "X";
                thispoolstring += dtcover[0].plenft.ToString().TrimEnd().TrimStart() + "'";
            }
            if (dtcover[0].plenin != 0)
            {
                thispoolstring += dtcover[0].plenin.ToString().TrimEnd().TrimStart() + "''";
            }
            clineds.socover[0].poolstring += thispoolstring;
        }

        public void CalculateExtensionDimensionsAndPrice(quote.socoverDataTable dtextension, bool CustomOverlap, feetandinches Overlaplength)
        {
            if (CustomOverlap == false)
            {
                // Length -- Adjust length but not width
                feetandinches extensionlength = new feetandinches();

                extensionlength = CalculateFeetandInches(dtextension[0].plenft,
                dtextension[0].plenin, Overlaplength.Feet * 2, Overlaplength.Inches * 2);
                dtextension[0].clenft = extensionlength.Feet;
                dtextension[0].clenin = extensionlength.Inches;
                // Width
                dtextension[0].cwidft = dtextension[0].pwidft;
                dtextension[0].cwidin = dtextension[0].pwidin;
            }
            // Calculate the square feet for this extension
            decimal sqft = CalculateSqft(dtextension[0].cwidft, dtextension[0].cwidin,
              dtextension[0].clenft,
              dtextension[0].clenin);

            // Calculate the price for the this extension
            dtextension[0].prcsqft = CalculateExtensionPrice(clineds.socover[0].item, sqft,
            clineds.socover[0].spacingid,
            clineds.socover[0].materialid, clineds.socover[0].upcharge);

            // Manual straps stops calculation
            if (clineds.socover[0].manlstr == false)
            {
                // Use the computed straps
                clineds.socover[0].straps += CalculateExtensionStraps(dtextension[0].cwidft,
                dtextension[0].cwidin,
                 dtextension[0].clenft, dtextension[0].clenin, clineds.socover[0].spacingid); ;
            }
            // Manual price stops calculation
            if (clineds.socover[0].manlprc == false)
            {
                clineds.socover[0].price += decimal.Round(dtextension[0].prcsqft * sqft, 2);
            }
            dtextension.AcceptChanges();
            clineds.socover.AcceptChanges();
        }

        public decimal CalculateCoverStraps(decimal widft, decimal widin, decimal lenft, decimal lenin, int spacingid)
        {
            decimal segmentstrapcount = 0;

            if (widin > 0)
            {
                widft++;
            }
            if (lenin > 0)
            {
                lenft++;
            }
            // Calculate the straps
            segmentstrapcount = Math.Ceiling((widft + lenft) * GetStrapMultiplier(spacingid));
            return segmentstrapcount;
        }

        public decimal CalculateExtensionStraps(decimal widft, decimal widin, decimal lenft, decimal lenin, int spacingid)
        {
            decimal segmentstrapcount = 0;

            if (widin > 0)
            {
                widft++;
            }
            if (lenin > 0)
            {
                lenft++;
            }
            // Calculate the straps
            segmentstrapcount = Math.Ceiling((widft + lenft) * GetStrapMultiplier(spacingid) / 2);
            return segmentstrapcount;
        }

        private feetandinches CalculateFeetandInches(decimal poolfeet, decimal poolinches, decimal overlapfeet, decimal overlapinches)
        {
            feetandinches returnsize = new feetandinches();
            int intfeet = 0;
            intfeet = (int)(((poolfeet + overlapfeet) * 12) + poolinches + overlapinches) / 12;
            returnsize.Feet = Convert.ToDecimal(intfeet);
            returnsize.Inches = ((poolfeet + overlapfeet) * 12) + poolinches + overlapinches - (returnsize.Feet * 12);
            return returnsize;
        }

        #region Refresh Totals

        public void RefreshTotals()
        {
            decimal allcoverstotal = 0;
            dttotals.Rows.Clear();
            DataRow totalsrow = dttotals.NewRow();
            decimal thiscovertotal = 0;
            totalsrow["allcovers"] = 0;
            decimal coverdiscountrate = 0;
            decimal adddiscountrate = somastds.soversion[0].adddiscrate;
            bool coverok = true;
            ClearResultsTable();
            // Results table row pointer
            int rrow = 0;

            if (clineds.socover[0].item == "")
            {
                coverok = false;
            }
            else
            {
                // Locate the item for the cover
                getSingleImmasterData(clineds.socover[0].item);
                if (itemds.immaster.Rows.Count == 0)
                {
                    coverok = false;
                }
            }
            if (coverok == true)
            {
                if (clineds.socover[0].product.TrimEnd().ToUpper() != "COVER REPAIR" && clineds.socover[0].product.TrimEnd().ToUpper() != "COVER ALTERATION"
                && clineds.socover[0].product.TrimEnd().ToUpper() != "WORKSHEET"
                && clineds.socover[0].price == 0)
                {
                    coverok = false;
                }
            }
            if (coverok == true)
            {
                // Establish cover discount rate
                coverdiscountrate = somastds.somast[0].salesdisc;

                // Cover price
                somastds.soversion[0].tax = 0;

                decimal listprice = 0;
                decimal discountedprice = 0;

                // Process currrent cover if there is price
                // Discount the cover
                listprice = Convert.ToDecimal(clineds.socover[0].price) * Convert.ToDecimal(clineds.socover[0].qtyord);
                discountedprice = listprice - (listprice * (coverdiscountrate / 100));
                clineds.socover[0].disc = coverdiscountrate;
                clineds.socover[0].extprice = Decimal.Round(discountedprice, 2);
                // Add to cover total
                thiscovertotal += clineds.socover[0].extprice;

                // Bring in currrent cover line
                resultsds.soline.Rows.Add();
                resultsds.soline[rrow].descrip = clineds.socover[0].descrip;
                resultsds.soline[rrow].price = clineds.socover[0].price;
                resultsds.soline[rrow].extprice = clineds.socover[0].extprice;
                resultsds.soline[rrow].disc = coverdiscountrate;
                resultsds.soline[rrow].qtyord = clineds.socover[0].qtyord;
                resultsds.soline[rrow].source = " C";
                rrow++;
                CombineCurrentItems();

                // Add any miscellaneous items for the cover
                int currentrow = 1;
                while (currentrow <= soitemsds.soline.Rows.Count)
                {
                    if (soitemsds.soline[currentrow - 1].qtyord != 0)
                    {
                        decimal linediscountrate = 0;
                        // See if the item should receive a discount
                        bool applydiscount = CheckDiscount(soitemsds.soline[currentrow - 1].item);
                        if (applydiscount == true)
                        {
                            linediscountrate = coverdiscountrate;
                        }
                        else
                        {
                            linediscountrate = 0;
                        }
                        // Apply discount to line and extend it

                        soitemsds.soline[currentrow - 1].extprice = decimal.Round((soitemsds.soline[currentrow - 1].price * soitemsds.soline[currentrow - 1].qtyord)
                        - ((soitemsds.soline[currentrow - 1].price * soitemsds.soline[currentrow - 1].qtyord) * (linediscountrate / 100)), 2);
                        soitemsds.soline[currentrow - 1].disc = linediscountrate;
                        // Add to cover total
                        thiscovertotal += soitemsds.soline[currentrow - 1].extprice;
                        resultsds.soline.Rows.Add();
                        resultsds.soline[rrow].descrip = soitemsds.soline[currentrow - 1].descrip;
                        resultsds.soline[rrow].source = soitemsds.soline[currentrow - 1].source;
                        resultsds.soline[rrow].qtyord = soitemsds.soline[currentrow - 1].qtyord;
                        resultsds.soline[rrow].price = soitemsds.soline[currentrow - 1].price;
                        resultsds.soline[rrow].disc = soitemsds.soline[currentrow - 1].disc;
                        resultsds.soline[rrow].extprice = soitemsds.soline[currentrow - 1].extprice;
                        rrow++;
                    }
                    currentrow++;
                }
            } // if coverok == true

            // Establish Version total here
            somastds.soversion[0].subtotal = 0;
            somastds.soversion[0].ordamt = 0;
            string stockitem = "";
            // Get all saved covers and lines for this version
            GetVersioncoverandlines(somastds.soversion[0].sono, somastds.soversion[0].version);
            for (int r1 = 0; r1 <= somastds.view_coverandlines.Rows.Count - 1; r1++)
            {
                // Establish subtotals for prior Covers
                if (somastds.view_coverandlines[r1].cover == clineds.socover[0].cover)
                {
                    continue;
                }
                allcoverstotal += somastds.view_coverandlines[r1].extprice;

                if (somastds.view_coverandlines[r1].product.TrimEnd().ToUpper() == "STOCK COVER")
                {
                    // Save the item for the stock cover
                    stockitem = somastds.view_coverandlines[r1].item;
                }
            }

            // Check  present cover
            if (clineds.socover[0].product.TrimEnd().ToUpper() == "STOCK COVER")
            {
                stockitem = clineds.socover[0].item;
            }

            // Apply the stock discount if stock cover exists and no other additional discount exits
            if (stockitem != "" && adddiscountrate == 0 && (somastds.somast[0].standdisc != 0 || somastds.somast[0].stockdisc != 0))
            {
                getSingleImmasterData(stockitem);
                if (itemds.immaster.Rows.Count != 0)
                {
                    if (itemds.immaster[0].stkstd.ToUpper().TrimEnd() == "STK")
                    {
                        if (somastds.somast[0].stockdisc != 0)
                        {
                            adddiscountrate = somastds.somast[0].stockdisc;
                            somastds.soversion[0].adddiscnote =
                            "Stock cover discount of " + adddiscountrate.ToString("N0").TrimEnd() + "%  applied";
                        }
                    }
                    else
                    {
                        if (somastds.somast[0].standdisc != 0)
                        {
                            adddiscountrate = somastds.somast[0].standdisc;
                            somastds.soversion[0].adddiscnote =
                            "Standard cover discount of " + adddiscountrate.ToString("N0").TrimEnd() + "%  applied";
                        }
                    }
                }
            } // stockitem

            allcoverstotal += thiscovertotal;
            if (adddiscountrate != 0)
            {
                // Compute the addtional discount if needed
                somastds.soversion[0].adddisc = decimal.Round((allcoverstotal * adddiscountrate / 100), 2);
            }
            else
            {
                somastds.soversion[0].adddisc = 0;
            }
            // Subtotal prior to shipping
            somastds.soversion[0].subtotal = allcoverstotal - somastds.soversion[0].adddisc;
            if (somastds.soversion[0].manlship == false)
            {
                somastds.soversion[0].shipping = 0;
                // Calculate shipping if needed
                if (somastds.somast[0].shipdisc != 0)
                {
                    somastds.soversion[0].shipping = decimal.Round(somastds.soversion[0].subtotal *
                   (somastds.somast[0].shipdisc / 100), 2);
                }
            }
            somastds.soversion.AcceptChanges();
            // Calculate tax here
            if (somastds.somast[0].taxrate != 0)
            {
                somastds.soversion[0].taxsamt = somastds.soversion[0].subtotal + somastds.soversion[0].shipping;
                somastds.soversion[0].tax = decimal.Round((somastds.soversion[0].subtotal +
                somastds.soversion[0].shipping) * (somastds.somast[0].taxrate / 100), 2);
            }
            else
            {
                somastds.soversion[0].tax = 0;
            }
            somastds.soversion.AcceptChanges();

            somastds.soversion[0].ordamt = somastds.soversion[0].subtotal +
                somastds.soversion[0].shipping +
                somastds.soversion[0].tax;

            // Calculate the deposit
            if (somastds.soversion[0].ordamt < 1000)
            {
                somastds.soversion[0].depositreq = decimal.Round((somastds.soversion[0].ordamt *
                (ards.arcust[0].depover0 / 100)), 2);
            }
            else
            {
                if (somastds.soversion[0].ordamt < 2000)
                {
                    somastds.soversion[0].depositreq = decimal.Round((somastds.soversion[0].ordamt *
                  (ards.arcust[0].depover1 / 100)), 2);
                }
                else
                {
                    if (somastds.soversion[0].ordamt < 3000)
                    {
                        somastds.soversion[0].depositreq = decimal.Round((somastds.soversion[0].ordamt *
                       (ards.arcust[0].depover2 / 100)), 2);
                    }
                    else
                    {
                        somastds.soversion[0].depositreq = decimal.Round((somastds.soversion[0].ordamt *
                       (ards.arcust[0].depover3 / 100)), 2);
                    }
                }
            }
            totalsrow["thiscover"] = thiscovertotal;
            totalsrow["allcovers"] = allcoverstotal;
            dttotals.Rows.Add(totalsrow);
            clineds.socover.AcceptChanges();
            tempext1lineds.socover.AcceptChanges();
            tempext2lineds.socover.AcceptChanges();
            tempext3lineds.socover.AcceptChanges();
            tempext4lineds.socover.AcceptChanges();
            somastds.somast.AcceptChanges();
            somastds.soline.AcceptChanges();
            somastds.soversion.AcceptChanges();
            resultsds.soline.DefaultView.Sort = "source,descrip";
            resultsds.soline.AcceptChanges();
        }

        #endregion Refresh Totals

        public void GetVersioncoverandlines(string sono, string version)
        {
            somastds.view_coverandlines.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@version", version, "SQL");
            this.FillData(somastds, "view_coverandlines", "wsgsp_getcoverandlines", CommandType.StoredProcedure);
        }

        public void SaveSoCover(quote ds)
        {
            ds.socover[0].sono = somastds.somast[0].sono;
            GenerateAppTableRowSave(ds.socover[0]);
        }

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

        public void SaveSoVersion()
        {
            somastds.soversion[0].sono = somastds.somast[0].sono;
            GenerateAppTableRowSave(somastds.soversion[0]);
        }

        public decimal CalculateExtensionPrice(string item, decimal sqft, int spacingid, int materialid, decimal upcharge)
        {
            decimal extensionprice = 0;
            // Find the price locator which indicates the price schedule, based on the item and the spacing.
            FindPriceLocatorData(item, spacingid);
            if (prds.quprslocator.Rows.Count < 1)
            {
                wsgUtilities.wsgNotice("There is no pricing information for this item and spacing");
                extensionprice = 0;
            }
            else
            {
                FindPriceScheduleData(prds.quprslocator[0].pschedid, sqft);
                if (prds.quprsdetail.Rows.Count > 0)
                {
                    // Select the price for the material chosen
                    string materialprefix = GetMaterialCode(materialid).ToUpper();
                    switch (materialprefix)
                    {
                        case "M":
                            {
                                extensionprice = prds.quprsdetail[0].meshex;
                                break;
                            }
                        case "S":
                            {
                                extensionprice = prds.quprsdetail[0].solidex;
                                break;
                            }
                        case "T":
                            {
                                extensionprice = prds.quprsdetail[0].trampolex;
                                break;
                            }
                        case "L":
                            {
                                extensionprice = prds.quprsdetail[0].lamsolidex;
                                break;
                            }
                        case "R":
                            {
                                extensionprice = prds.quprsdetail[0].rugmeshex;
                                break;
                            }
                    }
                }
            } // (prds.quprslocator.Rows.Count < 1)
            if (extensionprice != 0)
            {
                // Add upcharge if needed
                if (upcharge != 0)
                {
                    extensionprice += decimal.Round((extensionprice * (upcharge / 100)), 2);
                }
            }
            else
            {
                extensionprice = clineds.socover[0].prcsqft;
            }
            return extensionprice;
        }

        public void EstablishSolineWorkTable()
        {
            quote ds = new quote();
            solineds = ds;
        }

        public void FindPriceScheduleData(int pschedid, decimal sqft)
        {
            prds.quprsdetail.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@pschedid", pschedid, "SQL");
            this.AddParms("@sqft", sqft, "SQL");
            this.FillData(prds, "quprsdetail", "wsgsp_findPriceScheduleData", CommandType.StoredProcedure);
        }

        public void FindPriceLocatorData(string item, int spacingid)
        {
            prds.quprslocator.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.AddParms("@spacingid", spacingid, "SQL");
            this.FillData(prds, "quprslocator", "wsgsp_findpricelocatordata", CommandType.StoredProcedure);
        }

        public decimal CalculateSqft(decimal widft, decimal widin, decimal lenft, decimal lenin)
        {
            decimal sqft = 0;

            if (widin > 0)
            {
                widft += 1;
            }
            if (lenin > 0)
            {
                lenft += 1;
            }
            sqft = widft * lenft;
            return sqft;
        }

        public string GetOverlapDescription(int idcol)
        {
            string Descrip = "Undefined";
            if (idcol != 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows =
                soreferenceds.view_quoverlapdata.Select(strExpr);
                if (foundRows.Length > 0)
                {
                    Descrip = foundRows[0]["descrip"].ToString();
                }
            }
            return Descrip;
        }

        public feetandinches SpacingValue(int idcol)
        {
            feetandinches Spacinglength = new feetandinches();
            Spacinglength.Feet = 0;
            Spacinglength.Inches = 0;
            if (idcol != 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows =
                soreferenceds.view_quspacingdata.Select(strExpr);
                Spacinglength.Feet = Convert.ToDecimal(foundRows[0]["feet"]);
                Spacinglength.Inches = Convert.ToDecimal(foundRows[0]["inches"]);
            }
            return Spacinglength;
        }

        public feetandinches GetOverlapValue(int idcol)
        {
            feetandinches Overlaplength = new feetandinches();
            Overlaplength.Feet = 0;
            Overlaplength.Inches = 0;
            if (idcol != 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows =
                soreferenceds.view_quoverlapdata.Select(strExpr);
                Overlaplength.Feet = Convert.ToDecimal(foundRows[0]["feet"]);
                Overlaplength.Inches = Convert.ToDecimal(foundRows[0]["inches"]);
            }
            return Overlaplength;
        }

        public string GetMaterialCode(int idcol)
        {
            string prcmatrl = "";
            if (idcol != 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows =
                soreferenceds.view_qumaterialdata.Select(strExpr);
                prcmatrl = foundRows[0]["prcmatrl"].ToString();
            }
            else
            {
                prcmatrl = "";
            }
            return prcmatrl;
        }

        public string GetMaterialDescription(int idcol)
        {
            string Descrip = "Undefined";
            if (idcol != 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows =
                soreferenceds.view_qumaterialdata.Select(strExpr);
                Descrip = foundRows[0]["descrip"].ToString();
            }
            return Descrip;
        }

        #endregion Convert Reference table ID to description

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

        public int GetVersionCoverCount(string sono, string version, bool customOnly = false)
        {
            int covercount = 0;
            int rowcount = 0;
            LoadCoverViewData(sono, version);
            while (rowcount <= somastds.view_coverdata.Rows.Count - 1)
            {
                bool itCounts = (somastds.view_coverdata[rowcount].covertype == "C "); 
                if (customOnly)
                {
                    itCounts = itCounts && (somastds.view_coverdata[rowcount].product.Trim() != "Stock Cover");
                }
                if (itCounts)
                {
                    covercount++;
                }
                rowcount++;
            }
            return covercount;
        }

        public void EstablishBlankSOarcustData()
        {
            EstablishBlankDataTableRow(ards.arcust);
        }

        public void EstablishBlankSoaddrData()
        {
            EstablishBlankDataTableRow(somastds.soaddr);
            somastds.soaddr[0].sono = somastds.somast[0].sono;
            somastds.soaddr[0].custno = somastds.somast[0].custno;
            somastds.soaddr[0].somastid = somastds.somast[0].idcol;
            somastds.soaddr[0].notify1 = "Y";
            somastds.soaddr[0].notify2 = "EMAIL";
            // Use the invoice email address if there is one
            ards.emailaddress.Rows.Clear();
            string commandtext = "SELECT * FROM emailaddress WHERE custno = @custno AND addresstype = 'I'";
            ClearParameters();
            this.AddParms("@custno", somastds.somast[0].custno, "SQL");
            FillData(ards, "emailaddress", commandtext, CommandType.Text);
            if (ards.emailaddress.Rows.Count > 0)
            {
                somastds.soaddr[0].email = ards.emailaddress[0].emailaddress;
            }
            else
            {
                somastds.soaddr[0].email = "";
            }
        }

        public void EstablishBlankSoversionData()
        {
            EstablishBlankDataTableRow(somastds.soversion);
        }

        public void EstablishBlankSocoverData()
        {
            EstablishBlankDataTableRow(clineds.socover);
        }

        public void LoadCoverViewData(string sono, string version)
        {
            somastds.view_coverdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@version", version, "SQL");
            this.FillData(somastds, "view_coverdata", "wsgsp_getsoversioncovers", CommandType.StoredProcedure);
        }

        public void LoadVersionViewData(string sono)
        {
            somastds.view_versiondata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(somastds, "view_versiondata", "wsgsp_getsoversions", CommandType.StoredProcedure);
        }

        public void LoadCoverlineData(string sono, string version, string cover, string product)
        {
            clineds.socover.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@version", version, "SQL");
            this.AddParms("@cover", cover, "SQL");
            this.AddParms("@covertype ", "C ", "SQL");

            this.FillData(clineds, "socover", "wsgsp_getsinglesocoverdata", CommandType.StoredProcedure);
            if (clineds.socover.Rows.Count == 0)
            {
                // Create Cover Line if needed
                EstablishBlankDataTableRow(clineds.socover);
                clineds.socover[0].sono = sono;
                clineds.socover[0].qtyord = 1;
                clineds.socover[0].version = version;
                clineds.socover[0].disc = somastds.somast[0].salesdisc;
                clineds.socover[0].covertype = "C ";
                clineds.socover[0].cover = cover;
                clineds.socover[0].product = product;
            }
            clineds.socover.AcceptChanges();
        }

        public void LoadExtensionLine1(string sono, string version, string cover)
        {
            LoadExtensionLineData("X1", tempext1lineds);
        }

        public void LoadExtensionLine2(string sono, string version, string cover)
        {
            LoadExtensionLineData("X2", tempext2lineds);
        }

        public void LoadExtensionLine3(string sono, string version, string cover)
        {
            LoadExtensionLineData("X3", tempext3lineds);
        }

        public void LoadExtensionLine4(string sono, string version, string cover)
        {
            LoadExtensionLineData("X4", tempext4lineds);
        }

        public void LoadExtensionLineData(string extname, quote ds)
        {
            ds.socover.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", clineds.socover[0].sono, "SQL");
            this.AddParms("@version", clineds.socover[0].version, "SQL");
            this.AddParms("@cover", clineds.socover[0].cover, "SQL");
            this.AddParms("@covertype", extname, "SQL");

            this.FillData(ds, "socover", "wsgsp_getsinglesocoverdata", CommandType.StoredProcedure);
            if (ds.socover.Rows.Count == 0)
            {
                // Create Extline Line if needed
                ds.socover.Rows.Add();
                InitializeDataTable(ds.socover, 0);
                ds.socover[0].sono = clineds.socover[0].sono;
                ds.socover[0].descrip = "Extension";
                ds.socover[0].version = clineds.socover[0].version; ;
                ds.socover[0].cover = clineds.socover[0].cover;
                ds.socover[0].product = clineds.socover[0].product;
                ds.socover[0].covertype = extname;
            }
        }

        public void LoadMiscellandousSoLineData(string sono, string version, string cover, bool refreshFromImmaster = false)
        {
            soitemsds.soline.Rows.Clear();
            socurrentitemsds.soline.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@version", version, "SQL");
            this.AddParms("@cover", cover, "SQL");

            string spName = "wsgsp_findMiscellaneousSolineData";
            if (refreshFromImmaster)
                spName = "jksp_getReinitSoLineItems"; 

            this.FillData(soitemsds, "soline", spName, CommandType.StoredProcedure);

            // Copy idcolumn of items to holding table. We'll use that for tracking deletions
            DataTable dt = new DataTable();
            dt.Columns.Add("idcol", typeof(Int32));

            foreach (DataRow row in soitemsds.soline)
            {
                dt.ImportRow(row);
            }
            dtsolineOriginal = dt;
        }

        public void LoadDepositTiers(int somastid)
        {
            this.ClearParameters();
            this.AddParms("@somastid", somastid, "SQL");

            if (soitemsds.Tables["deposittiers"] != null && soitemsds.Tables["deposittiers"].Rows.Count > 0)
                soitemsds.Tables["deposittiers"].Rows.Clear();

            this.FillData(soitemsds, "deposittiers", "jksp_getSomastDepositTiers", CommandType.StoredProcedure);
            dtdeposittiers = soitemsds.Tables["deposittiers"];
        }

        public void EstablishBlankTempextlineData()
        {
            quote ds2 = new quote();
            ds2.soline.Rows.Clear();
            ds2.soline.Rows.Add();
            InitializeDataTable(ds2.soline, 0);
            ds2.soline[0].sono = somastds.somast[0].sono;
            ds2.soline[0].descrip = "Extension";
            ds2.soline[0].source = "X2";
            tempext2lineds = ds2;
            quote ds3 = new quote();
            ds3.soline.Rows.Clear();
            ds3.soline.Rows.Add();
            InitializeDataTable(ds3.soline, 0);
            ds3.soline[0].sono = somastds.somast[0].sono;
            ds3.soline[0].descrip = "Extension";
            ds3.soline[0].source = "X3";
            tempext3lineds = ds3;
            quote ds4 = new quote();
            ds4.soline.Rows.Clear();
            ds4.soline.Rows.Add();
            InitializeDataTable(ds4.soline, 0);
            ds4.soline[0].sono = somastds.somast[0].sono;
            ds4.soline[0].descrip = "Extension";
            ds4.soline[0].source = "X4";
            tempext4lineds = ds4;
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

                if (somastds.soversion.Rows.Count == 0)
                {
                    // Add a blank soversion record
                    EstablishBlankSoversionData();
                    somastds.soversion[0].version = "A";
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

        public void SaveOrderData(bool restoreDefaultDepositTiers = false, bool silent = false)
        {
            RefreshCover();
            RefreshTotals();
            SaveCoverLines();
            SaveSoLineitems();
            SaveSoVersion();
            SaveSomastData();
            SaveDepositTiers(restoreDefaultDepositTiers);

            string thisSono = this.somastds.somast[0].sono;
            string thisVersion = this.clineds.socover[0].version; 

            if (this.GetVersionCoverCount(thisSono, thisVersion, customOnly:true) > 1)
            {
                if (this.ColorHasChanged)
                {
                    this.UpdateColorForAllCovers(thisSono, thisVersion, this.clineds.socover[0].colorid, silent);
                }

                if (this.MaterialHasChanged)
                {
                    this.UpdateMaterialForAllCovers(thisSono, thisVersion, this.clineds.socover[0].materialid, silent);
                }
            }

            if (somastds.somast[0].sotype == "O" && somastds.somast[0].sostat != "V")
            {
                if (!miscDataMethods.IsOrderRepairOrAlteration(thisSono))
                {
                    // Update production units for custom cover or for selected stock covers
                    if (miscDataMethods.IsOrderCoverStock(thisSono))
                    {
                        // -99 in produnits indicates that the cover was changed
                        if (somastds.somast[0].produnits == -99)
                        {
                            somastds.somast[0].produnits = 0;
                            UpdateProductionUnits();
                        }
                    }
                    else
                    {
                        // Custom Cover
                        UpdateProductionUnits();
                    }

                    SaveSomastData();
                }
                else
                {
                    somastds.somast[0].produnits = 0;
                }
                SaveSomastData();
            }
            else
            {
                //JK: why twice???
                SaveSomastData();
            }
        }

        public void UpdateProductionUnits()
        {
            miscDataMethods.GetShipDateInformation(somastds.somast[0].sono);
            if (miscDataMethods.shipinfo.calcok)
            {
                if (miscDataMethods.shipinfo.customstock == "S")
                {
                    // Update stock cover ship date if the original date has not been changed by the user
                    if (somastds.somast[0].ordate <= somastds.somast[0].sodate)
                    {
                        somastds.somast[0].ordate = miscDataMethods.shipinfo.shipdate;
                    }
                    else
                    {
                        if (!wsgUtilities.wsgReply("Do you want to override the calculated Ship Date?"))
                        {
                            somastds.somast[0].ordate = miscDataMethods.shipinfo.shipdate;
                        }
                    }
                }
                else
                {
                    // Update custom cover ship date only if production units are 0 - first time calculation
                    if (somastds.somast[0].produnits <= 0)
                    {
                        somastds.somast[0].ordate = miscDataMethods.shipinfo.shipdate;
                    }
                }
                somastds.somast[0].produnits = miscDataMethods.shipinfo.productionunits;
            }
        }

        public void ProcessShipToSelection(int SelectedShipToId)
        {
            if (SelectedShipToId == 888888)
            {
                somastds.soaddr[0].cshipno = ards.arcust[0].custno;
                somastds.soaddr[0].adtype = "C";
                somastds.soaddr[0].company = ards.arcust[0].company;
                somastds.soaddr[0].address1 = ards.arcust[0].address1;
                somastds.soaddr[0].address2 = ards.arcust[0].address2;
                somastds.soaddr[0].city = ards.arcust[0].city;
                somastds.soaddr[0].state = ards.arcust[0].state;
                somastds.soaddr[0].zip = ards.arcust[0].zip;
                somastds.soaddr[0].lckdate = DateTime.Now;
                somastds.soaddr[0].lckuser = AppUserClass.AppUserId;
            }
            else
            {
                getSingleShipToData(SelectedShipToId);
                if (ards.aracadr[0].defaship == "Y")
                {
                    somastds.soaddr[0].adtype = "D";
                }
                else
                {
                    somastds.soaddr[0].adtype = "S";
                }
                somastds.soaddr[0].cshipno = ards.aracadr[0].cshipno;
                somastds.soaddr[0].company = ards.aracadr[0].company;
                somastds.soaddr[0].address1 = ards.aracadr[0].address1;
                somastds.soaddr[0].address2 = ards.aracadr[0].address2;
                somastds.soaddr[0].city = ards.aracadr[0].city;
                somastds.soaddr[0].state = ards.aracadr[0].state;
                somastds.soaddr[0].zip = ards.aracadr[0].zip;
                somastds.soaddr[0].lckdate = DateTime.Now;
                somastds.soaddr[0].lckuser = AppUserClass.AppUserId;
            }
        }

        public void ClearCoverVersionLine()
        {
            clineds.socover.Rows.Clear();
            tempext1lineds.socover.Rows.Clear();
            tempext2lineds.socover.Rows.Clear();
            tempext3lineds.socover.Rows.Clear();
            tempext4lineds.socover.Rows.Clear();
            somastds.soversion.Rows.Clear();
            somastds.soline.Rows.Clear();
        }

        public void ClearLineItemData()
        {
            EstablishBlankSoversionData();
            EstablishBlankCoverDataTables();
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

            // Use the order amount and tax from the first version
            somastds.somast[0].ordamt = somastds.soversion[0].ordamt;
            somastds.somast[0].shpamt = somastds.soversion[0].shipping;
            somastds.somast[0].tax = somastds.soversion[0].tax;
            somastds.somast[0].taxsamt = somastds.soversion[0].taxsamt;
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

        private void SaveDepositTiers(bool restoreDefaults = false)
        {
            if (restoreDefaults)
            {
                this.ClearParameters();
                this.AddParms("@somastid", this.somastds.somast[0].idcol, "SQL");
                ExecuteCommand("jksp_somastDepositTiersRestoreDefaults", CommandType.StoredProcedure);
            }
            else
            {
                //if changed, update 
                if (this.DepositTiersChanged())
                {
                    this.ClearParameters();
                    this.AddParms("@somastid", this.somastds.somast[0].idcol, "SQL");
                    this.AddParms("@sono", this.somastds.somast[0].sono, "SQL");
                    this.AddParms("@depover0", this.soitemsds.Tables["deposittiers"].Rows[0].ItemArray[0].ToString(), "SQL");
                    this.AddParms("@depover1", this.soitemsds.Tables["deposittiers"].Rows[0].ItemArray[1].ToString(), "SQL");
                    this.AddParms("@depover2", this.soitemsds.Tables["deposittiers"].Rows[0].ItemArray[2].ToString(), "SQL");
                    this.AddParms("@depover3", this.soitemsds.Tables["deposittiers"].Rows[0].ItemArray[3].ToString(), "SQL");

                    ExecuteCommand("jksp_updateSomastDepositTiers", CommandType.StoredProcedure);
                }
            }
        }

        private bool DepositTiersChanged()
        {
            this.ClearParameters();
            this.AddParms("@somastid", this.somastds.somast[0].idcol, "SQL");

            //check first if deposit tiers have changed 
            DataSet depositTiersDs = new DataSet();
            this.FillData(depositTiersDs, "deposittiers", "jksp_getSomastDepositTiers", CommandType.StoredProcedure);

            //compare to current dataset for deposit tiers 
            var dtDepositTiers = this.soitemsds.Tables["deposittiers"];
            if (dtDepositTiers != null && dtDepositTiers.Rows.Count > 0)
            {
                var depositTiersCurrent = this.soitemsds.Tables["deposittiers"].Rows[0].ItemArray;
                var depositTiersOrig = depositTiersDs.Tables[0].Rows[0].ItemArray;

                //determine if any changes
                if (depositTiersOrig.Length != depositTiersCurrent.Length)
                    return true;

                for (int n = 0; n < depositTiersCurrent.Length; n++)
                {
                    if (Decimal.Parse(depositTiersCurrent[n].ToString()) != Decimal.Parse(depositTiersOrig[n].ToString()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool ValidateSO(string CurentFeature, bool saving, bool silent = false)
        {
            bool SoOk = true;
            SoOk = ValidateSOCover(CurentFeature, saving, silent:silent);
            if (SoOk == true)
            {
                SoOk = ValidateSOHead(saving, silent: silent);
            }
            return SoOk;
        }

        public bool ValidateSOHead(bool saving, bool silent = false)
        {
            bool SomastOk = true;
            return SomastOk;
        }

        public bool ValidateSOCover(string CurrentFeature, bool saving, bool silent = false)
        {
            // Rule 100 and 101 - Validate the cover item

            CombineCurrentItems();
            bool SocoverOk = true;
            if (clineds.socover[0].item == "")
            {
                if (!silent) LogCoverError("100", true);
                SocoverOk = false;
            }
            if (SocoverOk == true)
            {
                // Locate the item for the cover
                getSingleImmasterData(clineds.socover[0].item);
                if (itemds.immaster.Rows.Count == 0)
                {
                    if (!silent) LogCoverError("101", true);
                    SocoverOk = false;
                }
            }
            if (clineds.socover[0].product.TrimEnd().ToUpper() != "COVER REPAIR" &&
                clineds.socover[0].product.TrimEnd().ToUpper() != "COVER ALTERATION" &&
                clineds.socover[0].product.TrimEnd().ToUpper() != "WORKSHEET")
            {
                if (SocoverOk == true)
                {
                    // Check the price/sqft
                    if (clineds.socover[0].prcsqft <= 0 && clineds.socover[0].product.TrimEnd() != "Stock Cover")
                        clineds.socover[0].prcsqft = 1;
                    if (clineds.socover[0].prcsqft <= 0 && clineds.socover[0].product.TrimEnd() != "Stock Cover")
                    {
                        if (!silent) LogCoverError("102", true);
                        SocoverOk = false;
                    }
                }
                #region Tests for both Custom and Stock

                if (SocoverOk == true)
                {
                    // Caculate the total sqft of the cover with extensions
                    decimal sqftwithext = CalculateCoverSqftWithExt();

                    // Check for solid cover  and require drain
                    string MaterialSDescription = GetMaterialDescription(clineds.socover[0].materialid).TrimEnd().ToUpper();

                    if (MaterialSDescription.EndsWith("PUMP") || MaterialSDescription.EndsWith("DRAIN"))
                    {
                        bool drainfound = false;
                        // Check for drains
                        var drains = from allitems in soitemsds.soline
                                     where allitems.source.Substring(0, 2) == "DR"
                                     select allitems;
                        if (drains.Any())
                        {
                            drainfound = true;
                        }
                        if (drainfound == false)
                        {
                            if (saving == true)
                            {
                                if (!silent) LogCoverError("210", false);
                            }
                            else
                            {
                                getDrainage(GetMaterialDescription(clineds.socover[0].materialid).TrimEnd().ToUpper());
                            }
                        }
                    }
                }
                if (SocoverOk == true)
                {
                    // If Gray or Mocha Color, cannot have 5X5 spacing
                    string covercolor = GetColorDescription(clineds.socover[0].colorid);
                    string coverspacing = GetSpacingDescription(clineds.socover[0].spacingid);
                    if ((covercolor.TrimEnd().ToUpper() == "GRAY" ||
                      covercolor.TrimEnd().ToUpper() == "MOCHA") && coverspacing.Substring(0, 1) == "5")
                    {
                        if (!silent) LogCoverError("206", false);
                    }
                }

                if (SocoverOk == true)
                {
                    // Check Lawn Pins as a percentage of the number of straps
                    decimal pinlimit = .25m;
                    var lawnpins = from allitems in soitemsds.soline
                                   where allitems.item.TrimEnd() == LawnPinItem
                                   select allitems;
                    if (lawnpins.Any())
                    {
                        foreach (DataRow drow in lawnpins)
                        {
                            if (decimal.Round(drow.Field<Decimal>("qtyord") / clineds.socover[0].straps, 2) >= pinlimit)
                            {
                                if (!silent) LogCoverError("207", false);
                            }
                        }
                    }
                }

                if (SocoverOk == true)
                {
                    // Check Pipes as a percentage of the number of straps
                    decimal pipeslimit = .25m;
                    var pipes = from allitems in soitemsds.soline
                                where allitems.item == Pipe9Item || allitems.item == Pipe18Item
                                select allitems;
                    if (pipes.Any())
                    {
                        foreach (DataRow drow in pipes)
                        {
                            if (decimal.Round(drow.Field<Decimal>("qtyord") / clineds.socover[0].straps, 2) >= pipeslimit)
                            {
                                if (!silent) LogCoverError("211", false);
                            }
                        }
                    }
                }
                if (SocoverOk == true)
                {
                    // Check Short Springs as a percentage of the number of straps
                    decimal sslimit = .35m;
                    var shortsprings = from allitems in soitemsds.soline
                                       where allitems.item.TrimEnd() == ShortSpringItem
                                       select allitems;
                    if (shortsprings.Any())
                    {
                        foreach (DataRow drow in shortsprings)
                        {
                            if (decimal.Round(drow.Field<Decimal>("qtyord") / clineds.socover[0].straps, 2) >= sslimit)
                            {
                                if (!silent) LogCoverError("208", false);
                            }
                        }
                    }
                }
                if (SocoverOk == true)
                {
                    // Check Lawn Stakes as a percentage of the number of straps
                    decimal lslimit = .25m;
                    var lawnstakes = from allitems in soitemsds.soline
                                     where allitems.item.TrimEnd() == LawnstakeItem
                                     select allitems;
                    if (lawnstakes.Any())
                    {
                        foreach (DataRow drow in lawnstakes)
                        {
                            if (decimal.Round(drow.Field<Decimal>("qtyord") / clineds.socover[0].straps, 2) >= lslimit)
                            {
                                if (!silent) LogCoverError("209", false);
                            }
                        }
                    }
                }
                #endregion Tests for both Custom and Stock
                if (clineds.socover[0].product.TrimEnd().ToUpper() != "STOCK COVER")
                {
                    #region Tests for Custom Only
                    // Custom Cover
                    if (SocoverOk == true)
                    {
                        // Caculate the total sqft of the cover with extensions
                        decimal sqftwithext = CalculateCoverSqftWithExt();

                        feetandinches Overlaplength = new feetandinches();
                        Overlaplength = GetOverlapValue(clineds.socover[0].overlapid);
                        decimal Overlapinches = (Overlaplength.Feet * 12) + Overlaplength.Inches;
                        // These two tests check square footage against overlap
                        if (sqftwithext > 1500)
                        {
                            if (Overlapinches < 18)
                            {
                                if (!silent) LogCoverError("200", false);
                            }
                        }
                        else
                        {
                            if (sqftwithext > 1150 && Overlapinches < 15)
                            {
                                if (!silent) LogCoverError("201", false);
                            }
                        }
                    }

                    if (SocoverOk == true)
                    {
                        // Check for obstructions
                        var obstructs = from allitems in soitemsds.soline
                                        where allitems.source.Substring(0, 2) == "OB"
                                        select allitems;
                        if (obstructs.Any())
                        {
                            string spacingdescrip = GetSpacingDescription(clineds.socover[0].spacingid).TrimEnd().ToUpper();
                            foreach (var t in obstructs)
                            {
                                if (t.qtyord > 12 && spacingdescrip != "3'X3'")
                                {
                                    if (!silent) LogCoverError("203", false);
                                }
                            }
                        }
                    }
                    if (SocoverOk == true)
                    {
                        // Check for cut corners with a Grecian Cover
                        if (clineds.socover[0].descrip.ToUpper().IndexOf("GRECIAN") != -1)
                        {
                            bool cornersfound = false;
                            // Check for corners
                            var corners = from allitems in soitemsds.soline
                                          where allitems.source.Substring(0, 2) == "CD" && allitems.item.TrimEnd() == GrecianCornerItem
                                          select allitems;
                            if (corners.Any())
                            {
                                cornersfound = true;
                            }
                            if (cornersfound == false)
                            {
                                if (saving == true)
                                {
                                    if (!silent) LogCoverError("205", false);
                                }
                                else
                                {
                                    getGrecianCorners();
                                }
                            }
                        }
                    }

                    #endregion Tests for Custom Only
                } // Custom Cover
                else
                {
                    if (SocoverOk == true)
                    {
                        // Stock Cover
                        // Rule 300 If cover color has changed, the price must change
                        if (clineds.socover[0].colorid != GetColorIdcol(itemds.immaster[0].color) &&
                         clineds.socover[0].price <= itemds.immaster[0].regprice)
                        {
                            if (!silent) LogCoverError("300", false);
                        }
                    } // coverok == true
                } // Stock Cover
            } // Cover Repair

            return SocoverOk;
        }

        public int GetOverlapIdcol(string code)
        {
            int idcol = 0;
            int rrow = 0;
            if (code != "")
            {
                while (rrow <= soreferenceds.view_quoverlapdata.Rows.Count - 1)
                {
                    if (soreferenceds.view_quoverlapdata[rrow].code.TrimEnd().ToUpper()
                      == code.TrimEnd().ToUpper())
                    {
                        idcol = soreferenceds.view_quoverlapdata[rrow].idcol;
                        break;
                    }
                    else
                    {
                        rrow++;
                        continue;
                    }
                }
            }
            return idcol;
        }

        public int GetSpacingIdcol(string code)
        {
            int idcol = 0;
            int rrow = 0;
            if (code != "")
            {
                while (rrow <= soreferenceds.view_quspacingdata.Rows.Count - 1)
                {
                    if (soreferenceds.view_quspacingdata[rrow].code.TrimEnd().ToUpper()
                      == code.TrimEnd().ToUpper())
                    {
                        idcol = soreferenceds.view_quspacingdata[rrow].idcol;
                        break;
                    }
                    else
                    {
                        rrow++;
                        continue;
                    }
                }
            }
            return idcol;
        }

        public int GetColorIdcol(string code)
        {
            int idcol = 0;
            int rrow = 0;
            if (code != "")
            {
                while (rrow <= soreferenceds.view_qucolordata.Rows.Count - 1)
                {
                    if (soreferenceds.view_qucolordata[rrow].code.TrimEnd().ToUpper()
                      == code.TrimEnd().ToUpper())
                    {
                        idcol = soreferenceds.view_qucolordata[rrow].idcol;
                        break;
                    }
                    else
                    {
                        rrow++;
                        continue;
                    }
                }
            }
            return idcol;
        }

        public decimal CalculateCoverSqftWithExt()
        {
            // Cover
            clineds.socover[0].sqft = CalculateSqft(clineds.socover[0].cwidft, clineds.socover[0].cwidin, clineds.socover[0].clenft,
                     clineds.socover[0].clenin);
            // First extension
            clineds.socover[0].sqft += CalculateSqft(tempext1lineds.socover[0].cwidft, tempext1lineds.socover[0].cwidin,
            tempext1lineds.socover[0].clenft, tempext1lineds.socover[0].clenin);
            // Second extension
            clineds.socover[0].sqft += CalculateSqft(tempext2lineds.socover[0].cwidft, tempext2lineds.socover[0].cwidin,
             tempext2lineds.socover[0].clenft, tempext2lineds.socover[0].clenin);
            // Third extension
            clineds.socover[0].sqft += CalculateSqft(tempext3lineds.socover[0].cwidft, tempext3lineds.socover[0].cwidin,
             tempext3lineds.socover[0].clenft, tempext3lineds.socover[0].clenin);
            // Fourth extension
            clineds.socover[0].sqft += CalculateSqft(tempext4lineds.socover[0].cwidft, tempext4lineds.socover[0].cwidin,
             tempext4lineds.socover[0].clenft, tempext4lineds.socover[0].clenin);

            return clineds.socover[0].sqft;
        }

        public int GetMaterialIdcol(string code)
        {
            int idcol = 0;
            int rrow = 0;
            if (code != "")
            {
                while (rrow <= soreferenceds.view_qumaterialdata.Rows.Count - 1)
                {
                    if (soreferenceds.view_qumaterialdata[rrow].code.TrimEnd().ToUpper()
                      == code.TrimEnd().ToUpper())
                    {
                        idcol = soreferenceds.view_qumaterialdata[rrow].idcol;
                        break;
                    }
                    else
                    {
                        rrow++;
                        continue;
                    }
                }
            }
            return idcol;
        }

        public void getGrecianCorners()
        {
            int cornercount = 0;
            FrmGetInput frmGetInput = new FrmGetInput();
            frmGetInput.InputQuestion = "How many corner cuts are there?";
            frmGetInput.Response = 0;
            frmGetInput.ShowDialog();
            cornercount = frmGetInput.Response;
            // Add the corners to the soitems table

            SetSolineItem(GrecianCornerItem, "CDESC",
               cornercount, 0, clineds.socover[0].version,
                 clineds.socover[0].cover);
        }

        public void getDrainage(string material)
        {
            string inputquestion = "";
            string drainitem = "";
            if (material.EndsWith("PUMP") == true)
            {
                inputquestion = "How many pumps are there?";
                drainitem = PumpItem;
            }
            else
            {
                inputquestion = "How many drains are there?";
                drainitem = DrainItem;
            }

            int drainagecount = 0;
            FrmGetInput frmGetInput = new FrmGetInput();
            frmGetInput.InputQuestion = inputquestion;
            frmGetInput.Response = 0;
            frmGetInput.ShowDialog();
            drainagecount = frmGetInput.Response;
            // Add the corners to the soitems table
            if (drainagecount > 0)
            {
                SetSolineItem(drainitem, "DRAIN",
                 drainagecount, 0, clineds.socover[0].version,
                clineds.socover[0].cover);
            }
        }

        public void LogCoverError(string errorcode, bool fatalerror)
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

        public void CalcHardware()
        {
            // Locate hardware preferences for this customer
            string commandString = "SELECT * FROM view_expandedcusthwpref WHERE custno = @custno";
            this.ClearParameters();
            ards.view_expandedcusthwpref.Rows.Clear();
            this.AddParms("@custno", somastds.somast[0].custno, "SQL");
            this.FillData(ards, "view_expandedcusthwpref", commandString, CommandType.Text);
            for (int i = 0; i <= ards.view_expandedcusthwpref.Rows.Count - 1; i++)
            {
                SetSolineItem(ards.view_expandedcusthwpref[i].item, "HW",
                Convert.ToInt16(clineds.socover[0].straps), ards.view_expandedcusthwpref[i].pricecode, clineds.socover[0].version,
                  clineds.socover[0].cover);
            }
        }

        public void ConvertBid(string CurrentVersion)
        {
            if (somastds.somast[0].sotype != "B")
            {
                wsgUtilities.wsgNotice("This order was previously Converted.");
            }
            else
            {
                // Delete all versions and related tables for all but the current version
                LoadVersionViewData(somastds.somast[0].sono);
                for (int i = 0; i < somastds.view_versiondata.Rows.Count; i++)
                {
                    if (somastds.view_versiondata[i].version == CurrentVersion)
                    {
                        continue;
                    }
                    DeleteSoversion(somastds.somast[0].sono, somastds.view_versiondata[i].version);
                }
                // Force a routing everytime a bid is converted
                RouteSo();
                somastds.somast[0].sotype = "O";
                // Exclude Repairs, Alterations and records which have the Meycono field populated
                if ((!miscDataMethods.IsOrderRepairOrAlteration(somastds.somast[0].sono)) && (somastds.somast[0].meycono.TrimEnd() == ""))
                {
                    // Update meycono if this a customcover or if this a stock cover and there is nothing on hand.
                    if (miscDataMethods.IsOrderCoverStock(somastds.somast[0].sono))
                    {
                        if (miscDataMethods.GetStockCoverAvailability(somastds.somast[0].sono, false, somastds.somast[0].defloc) < 1)
                        {
                            somastds.somast[0].meycono = appInformation.GetMeycono();
                        }
                    }
                    else
                    {
                        somastds.somast[0].meycono = appInformation.GetMeycono();
                    }
                }
            }
        }

        public void RevertOrder()
        {
            if (wsgUtilities.wsgReply("Revert this order?"))
            {
                somastds.somast[0].sotype = "B";
                somastds.somast[0].produnits = 0;
            }
        }

        public void ClearCoverDimensions(quote.socoverDataTable dtcover)
        {
            dtcover[0].cwidft = 0;
            dtcover[0].cwidin = 0;
            dtcover[0].clenft = 0;
            dtcover[0].clenin = 0;
            dtcover.AcceptChanges();
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

        public void ConvertWorksheet()
        {
            if (clineds.socover[0].product.TrimEnd().ToUpper() == "WORKSHEET")
            {
                FrmProductSelector frmProductSelector = new FrmProductSelector();
                frmProductSelector.ShowDialog();
                string newproduct = frmProductSelector.SelectedProduct;
                if (newproduct.TrimEnd() != "")
                {
                    clineds.socover[0].product = newproduct;
                    clineds.AcceptChanges();
                    RefreshCover();
                }
                else
                {
                    wsgUtilities.wsgNotice("No new product was selected");
                }
            }
            else
            {
                wsgUtilities.wsgNotice("The current cover is not a worksheet");
            }
        }

        public void ImportInspection()
        {
            bool ImportOK = true;
            if (somastds.somast[0].sotype != "B")
            {
                wsgUtilities.wsgNotice("Importing to an order is not permitted");
                ImportOK = false;
            }
            if (ImportOK == true)
            {
                if (clineds.socover[0].product.TrimEnd() != "Cover Repair" &&
                clineds.socover[0].product.TrimEnd() != "Cover Alteration")
                {
                    wsgUtilities.wsgNotice("Importing is only allowed for cover repairs or alterations");
                    ImportOK = false;
                }
            }
            if (ImportOK == true)
            {
                inspds.inspmast.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@sono", somastds.somast[0].sono, "SQL");
                this.FillData(inspds, "inspmast", "wsgsp_getinspmastbysono", CommandType.StoredProcedure);
                if (inspds.inspmast.Rows.Count == 0)
                {
                    wsgUtilities.wsgNotice("There is no inspection data for this Estimate");
                    ImportOK = false;
                }
            }
            if (ImportOK == true)
            {
                if (clineds.socover[0].product.TrimEnd() == "Stock Cover")
                {
                    wsgUtilities.wsgNotice("Importing to a stock cover is not permitted");
                    ImportOK = false;
                }
            }
            if (ImportOK == true)
            {
                if (clineds.socover[0].product.TrimEnd() == "Worksheet")
                {
                    wsgUtilities.wsgNotice("Importing to a worksheet is not permitted");
                    ImportOK = false;
                }
            }
            if (ImportOK == true)
            {
                ImportCoverData();
                SaveSoVersion();
                SaveCoverLines();
                if (clineds.socover[0].product.TrimEnd() != "Cover Repair" ||
                clineds.socover[0].product.TrimEnd() != "Cover Alteration")
                {
                    string InspVersion = "";
                    inspds.inspversion.Rows.Clear();
                    string CommandString = "SELECT * FROM inspversion WHERE sono = @sono";
                    this.ClearParameters();
                    this.AddParms("@sono", somastds.somast[0].sono, "SQL");
                    this.FillData(inspds, "inspversion", CommandString, CommandType.Text);
                    if (inspds.inspversion.Rows.Count > 1)
                    {
                        FrmInspVersionSelector frmInspVersionSelector = new FrmInspVersionSelector();
                        frmInspVersionSelector.versionSelector = new VersionSelector("SQL", "SQLConnString", frmInspVersionSelector);
                        frmInspVersionSelector.versionSelector.CurrentSono = somastds.somast[0].sono;
                        frmInspVersionSelector.versionSelector.SelectedVersion = "";
                        frmInspVersionSelector.versionSelector.LoadVersionViewData();
                        frmInspVersionSelector.versionSelector.SetBindings();
                        frmInspVersionSelector.versionSelector.SetEvents();
                        frmInspVersionSelector.ShowDialog();
                        InspVersion = frmInspVersionSelector.versionSelector.SelectedVersion;
                    }
                    else
                    {
                        InspVersion = inspds.inspversion[0].version;
                    }
                    if (InspVersion.TrimEnd() != "")
                    {
                        this.ClearParameters();
                        inspds.inspline.Rows.Clear();
                        this.AddParms("@sono", somastds.somast[0].sono, "SQL");
                        this.AddParms("@version", InspVersion, "SQL");
                        this.FillData(inspds, "inspline", "wsgsp_findMiscellaneousInsplineData", CommandType.StoredProcedure);
                        if (inspds.inspline.Rows.Count > 0)
                        {
                            // Fill  the current items table as a work table.
                            socurrentitemsds.soline.Rows.Clear();
                            for (int r1 = 0; r1 <= inspds.inspline.Rows.Count - 1; r1++)
                            {
                                socurrentitemsds.soline.Rows.Add();
                                int rowpointer = socurrentitemsds.soline.Rows.Count - 1;
                                InitializeDataTable(socurrentitemsds.soline, rowpointer);
                                socurrentitemsds.soline[rowpointer].sono = inspds.inspline[r1].sono;
                                socurrentitemsds.soline[rowpointer].source = inspds.inspline[r1].source;
                                socurrentitemsds.soline[rowpointer].price = inspds.inspline[r1].price;
                                if (inspds.inspline != null && inspds.inspline[r1] != null && inspds.inspline[r1].prwcov != null && inspds.inspline[r1].prwcov.HasValue)
                                    socurrentitemsds.soline[rowpointer].price = inspds.inspline[r1].prwcov.Value;
                                socurrentitemsds.soline[rowpointer].descrip = inspds.inspline[r1].descrip;
                                socurrentitemsds.soline[rowpointer].item = inspds.inspline[r1].item;
                                socurrentitemsds.soline[rowpointer].version = clineds.socover[0].version;
                                socurrentitemsds.soline[rowpointer].cover = clineds.socover[0].cover;
                                socurrentitemsds.soline[rowpointer].qtyord = inspds.inspline[r1].qtyord;
                                socurrentitemsds.soline[rowpointer].qtyact = inspds.inspline[r1].qtyord;
                            }
                            socurrentitemsds.AcceptChanges();
                            SaveSoLineitems();
                        }
                    } //(InspVersion.TrimEnd() != "")
                    else
                    {
                        wsgUtilities.wsgNotice("No version selected. Lines were not imported");
                    } //(InspVersion.TrimEnd() != "")
                } // if (clineds.socover[0].product.TrimEnd() != "Cover Repair" ||
                string thiscover = clineds.socover[0].cover;
                string thisversion = clineds.socover[0].version;
                // Clear the covers
                ClearCoverVersionLine();
                parentform.ProcessSo(thisversion, thiscover);
                parentform.RefreshControls();
            } // importOK == true
        }

        public bool CheckInspImport()
        {
            bool ImportOK = true;
            if (clineds.socover[0].product.TrimEnd() != "Worksheet" && clineds.socover[0].product.TrimEnd() != "Stock Cover")
            {
                // If not a stock cover or worksheet, prompt for improrting inpsection, if any
                inspds.inspmast.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@sono", somastds.somast[0].sono, "SQL");
                this.FillData(inspds, "inspmast", "wsgsp_getinspmastbysono", CommandType.StoredProcedure);
                if (inspds.inspmast.Rows.Count > 0)
                {
                    if (wsgUtilities.wsgReply("There is inpsection Data. Import?"))
                    {
                        ImportCoverData();

                        if (clineds.socover[0].product.TrimEnd() == "Cover Repair")
                        {
                            LoadInspectionLineItems();
                        }
                    }
                }
            }
            return ImportOK;
        }

        public void ImportCoverData()
        {
            // Import cover dimensions and other fields
            clineds.socover[0].clenft = inspds.inspmast[0].clenft;
            clineds.socover[0].clenin = inspds.inspmast[0].clenin;
            clineds.socover[0].cwidft = inspds.inspmast[0].cwidft;
            clineds.socover[0].cwidin = inspds.inspmast[0].cwidin;
            clineds.socover[0].descrip = inspds.inspmast[0].descrip;
            clineds.socover[0].item = inspds.inspmast[0].item;
            clineds.socover[0].materialid = inspds.inspmast[0].materialid;
            clineds.socover[0].colorid = inspds.inspmast[0].colorid;
            clineds.socover[0].spacingid = inspds.inspmast[0].spacingid;
            clineds.socover[0].straps = inspds.inspmast[0].straps;

            // Force manual straps
            clineds.socover[0].manlstr = true;
            // Force Custom Overlap
            clineds.socover[0].overlapid = GetOverLapId("CUSTOM");

            clineds.socover.AcceptChanges();
            // Extension 1
            tempext1lineds.socover[0].clenft = inspds.inspmast[0].x1lenft;
            tempext1lineds.socover[0].clenin = inspds.inspmast[0].x1lenin;
            tempext1lineds.socover[0].cwidft = inspds.inspmast[0].x1widft;
            tempext1lineds.socover[0].cwidin = inspds.inspmast[0].x1widin;
            tempext1lineds.socover.AcceptChanges();

            // Extension 2
            tempext2lineds.socover[0].clenft = inspds.inspmast[0].x2lenft;
            tempext2lineds.socover[0].clenin = inspds.inspmast[0].x2lenin;
            tempext2lineds.socover[0].cwidft = inspds.inspmast[0].x2widft;
            tempext2lineds.socover[0].cwidin = inspds.inspmast[0].x2widin;
            tempext2lineds.socover.AcceptChanges();

            // Extension 3
            tempext3lineds.socover[0].clenft = inspds.inspmast[0].x3lenft;
            tempext3lineds.socover[0].clenin = inspds.inspmast[0].x3lenin;
            tempext3lineds.socover[0].cwidft = inspds.inspmast[0].x3widft;
            tempext3lineds.socover[0].cwidin = inspds.inspmast[0].x3widin;
            tempext3lineds.socover.AcceptChanges();

            // Extension 4
            tempext4lineds.socover[0].clenft = inspds.inspmast[0].x4lenft;
            tempext4lineds.socover[0].clenin = inspds.inspmast[0].x4lenin;
            tempext4lineds.socover[0].cwidft = inspds.inspmast[0].x4widft;
            tempext4lineds.socover[0].cwidin = inspds.inspmast[0].x4widin;
            tempext4lineds.socover.AcceptChanges();
            parentform.RefreshCover();
            // Comments
            somastds.soversion[0].intcomments += Environment.NewLine + "Inspection notes: " + inspds.inspmast[0].notes +
            Environment.NewLine;
            somastds.soversion[0].intcomments += "Sent with the cover: " + Environment.NewLine;
            somastds.soversion[0].intcomments += "Springs: " + inspds.inspmast[0].springs.ToString(NumberFormatInfo.InvariantInfo);
            somastds.soversion[0].intcomments += Environment.NewLine;
            somastds.soversion[0].intcomments += "Springs Covers: " + inspds.inspmast[0].springcovers.ToString(NumberFormatInfo.InvariantInfo);
            somastds.soversion[0].intcomments += Environment.NewLine;
            somastds.soversion[0].intcomments += "Pumps: " + inspds.inspmast[0].pumps.ToString(NumberFormatInfo.InvariantInfo);
            somastds.soversion[0].intcomments += Environment.NewLine;
            somastds.soversion[0].intcomments += "Stow Bag: " + inspds.inspmast[0].stowbag.ToString(NumberFormatInfo.InvariantInfo);
            somastds.soversion[0].intcomments += Environment.NewLine;
            somastds.soversion[0].intcomments += "Instructions: " + inspds.inspmast[0].instructions;
            somastds.soversion[0].intcomments += Environment.NewLine;
            somastds.soversion[0].intcomments += "Photos: " + inspds.inspmast[0].photos;
            somastds.soversion[0].intcomments += Environment.NewLine;
            somastds.soversion[0].intcomments += "Location: " + getSysreferenceDescrip(inspds.inspmast[0].locationid);
        }

        public void LoadInspectionLineItems()
        {
            string InspVersion = "";
            inspds.inspversion.Rows.Clear();
            string CommandString = "SELECT * FROM inspversion WHERE sono = @sono";
            this.ClearParameters();
            this.AddParms("@sono", somastds.somast[0].sono, "SQL");
            this.FillData(inspds, "inspversion", CommandString, CommandType.Text);
            if (inspds.inspversion.Rows.Count > 1)
            {
                FrmInspVersionSelector frmInspVersionSelector = new FrmInspVersionSelector();
                frmInspVersionSelector.versionSelector = new VersionSelector("SQL", "SQLConnString", frmInspVersionSelector);
                frmInspVersionSelector.versionSelector.CurrentSono = somastds.somast[0].sono;
                frmInspVersionSelector.versionSelector.SelectedVersion = "";
                frmInspVersionSelector.versionSelector.LoadVersionViewData();
                frmInspVersionSelector.versionSelector.SetBindings();
                frmInspVersionSelector.versionSelector.SetEvents();
                frmInspVersionSelector.ShowDialog();
                InspVersion = frmInspVersionSelector.versionSelector.SelectedVersion;
            }
            else
            {
                InspVersion = inspds.inspversion[0].version;
            }
            if (InspVersion.TrimEnd() != "")
            {
                this.ClearParameters();
                inspds.inspline.Rows.Clear();
                this.AddParms("@sono", somastds.somast[0].sono, "SQL");
                this.AddParms("@version", InspVersion, "SQL");
                this.FillData(inspds, "inspline", "wsgsp_findMiscellaneousInsplineData", CommandType.StoredProcedure);
                if (inspds.inspline.Rows.Count > 0)
                {
                    // Addd the lines items .
                    for (int r1 = 0; r1 <= inspds.inspline.Rows.Count - 1; r1++)
                    {
                        soitemsds.soline.Rows.Add();
                        int rowpointer = soitemsds.soline.Rows.Count - 1;
                        InitializeDataTable(soitemsds.soline, rowpointer);
                        soitemsds.soline[rowpointer].sono = inspds.inspline[r1].sono;
                        soitemsds.soline[rowpointer].source = inspds.inspline[r1].source;
                        soitemsds.soline[rowpointer].price = inspds.inspline[r1].price;
                        soitemsds.soline[rowpointer].descrip = inspds.inspline[r1].descrip;
                        soitemsds.soline[rowpointer].item = inspds.inspline[r1].item;
                        soitemsds.soline[rowpointer].version = clineds.socover[0].version;
                        soitemsds.soline[rowpointer].cover = clineds.socover[0].cover;
                        soitemsds.soline[rowpointer].qtyord = inspds.inspline[r1].qtyord;
                        soitemsds.soline[rowpointer].qtyact = inspds.inspline[r1].qtyord;
                    }
                    soitemsds.AcceptChanges();
                }
            } // (InspVersion.TrimEnd() != "")
        }

        //
        public int GetOverLapId(string Overlap)
        {
            int overlapid = 0;
            string strExpr = "code = 'CUSTOM'";
            // Use the Select method to find all rows matching the filter.
            DataRow[] foundItemRows =
             soreferenceds.view_quoverlapdata.Select(strExpr);
            if (foundItemRows.Length > 0)
            {
                DataRow dr = foundItemRows[0];
                overlapid = Convert.ToInt32(dr["idcol"]);
            }

            return overlapid;
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

        public void EstablishSomastBasicData()
        {
            // Establish somast basic data from customer
            somastds.somast[0].custno = ards.arcust[0].custno;
            somastds.somast[0].custid = ards.arcust[0].idcol;
            somastds.somast[0].sono = appInformation.GetNextSono();
            somastds.somast[0].enterqu = "Y";
            somastds.somast[0].sotype = "B";
            somastds.somast[0].pdays = ards.arcust[0].pdays;
            somastds.somast[0].pnet = ards.arcust[0].pnet;
            somastds.somast[0].pdisc = ards.arcust[0].pdisc;
            somastds.somast[0].termid = ards.arcust[0].termid;
            somastds.somast[0].pterms = ards.arcust[0].pterms;
            somastds.somast[0].taxrate = ards.arcust[0].tax;
            somastds.somast[0].fob = ards.arcust[0].fob;
            somastds.somast[0].shipvia = ards.arcust[0].shipvia;
            somastds.somast[0].glarec = ards.arcust[0].gllink;
            somastds.somast[0].salesmn = ards.arcust[0].salesmn;
            somastds.somast[0].salesdisc = ards.arcust[0].disc;
            somastds.somast[0].taxdist = ards.arcust[0].taxdist;
            somastds.somast[0].terr = ards.arcust[0].terr;
            somastds.somast[0].taxrate = ards.arcust[0].tax;
            somastds.somast[0].taxst = ards.arcust[0].state;
            somastds.somast[0].glarec = ards.arcust[0].gllink;
            somastds.somast[0].stockdisc = ards.arcust[0].stockdisc;
            somastds.somast[0].standdisc = ards.arcust[0].standdisc;
            somastds.somast[0].commldisc = ards.arcust[0].commldisc;
            somastds.somast[0].repldisc = ards.arcust[0].repldisc;
            somastds.somast[0].repdisc = ards.arcust[0].repdisc;
            somastds.somast[0].shipdisc = ards.arcust[0].shipdisc;
            somastds.somast[0].upcharge = ards.arcust[0].upcharge;
            // Establish Quote record type
            somastds.somast[0].enterqu = "Y";
            // Establish default location
            somastds.somast[0].defloc = appInformation.GetStkloc();
        }

        public string CopyQuote(string sono)
        {
            string newsono = "";
            bool singleversion = false;
            string commandstring = "";
            GetCustomerMethods getCust = new GetCustomerMethods("SQL", "SQLConnString");
            if (getCust.SelectedCustid != 0)
            {
                getSingleCustomerData(getCust.SelectedCustid);
                quotesearchds.somast.Rows.Clear();
                ClearParameters();
                AddParms("@sono", sono, "SQL");
                commandstring = "SELECT * FROM somast WHERE sono = @sono";
                this.FillData(quotesearchds, "somast", commandstring, CommandType.Text);
                // Old quote found
                if (quotesearchds.somast.Rows.Count > 0)
                {
                    // Establish blank tables
                    EstablishBlankSomastData();
                    // Copy PO and Old Plan number
                    somastds.somast[0].ponum = quotesearchds.somast[0].ponum;
                    somastds.somast[0].oldplan = quotesearchds.somast[0].oldplan;
                    // Copy pool owner data
                    somastds.somast[0].lname = quotesearchds.somast[0].lname;
                    somastds.somast[0].fname = quotesearchds.somast[0].fname;
                    somastds.somast[0].address = quotesearchds.somast[0].address;
                    somastds.somast[0].city = quotesearchds.somast[0].city;
                    somastds.somast[0].state = quotesearchds.somast[0].state;
                    somastds.somast[0].zip = quotesearchds.somast[0].zip;
                    EstablishSomastBasicData();
                    newsono = somastds.somast[0].sono;
                    GenerateAppTableRowSave(somastds.somast[0]);
                    // Recall the new SO  information
                    somastds.somast.Rows.Clear();
                    ClearParameters();
                    AddParms("@sono", newsono, "SQL");
                    commandstring = "SELECT * FROM somast WHERE sono = @sono";
                    this.FillData(somastds, "somast", commandstring, CommandType.Text);
                    // Get the default ship to address for this customer, if any
                    EstablishBlankSoaddrData();
                    getDefaultShipToData(getCust.SelectedCustid);
                    if (ards.aracadr.Rows.Count != 0)
                    {
                        somastds.soaddr[0].adtype = "D";
                        somastds.soaddr[0].sono = somastds.somast[0].sono;
                        somastds.soaddr[0].custno = somastds.somast[0].custno;
                        somastds.soaddr[0].cshipno = ards.aracadr[0].cshipno;
                        somastds.soaddr[0].company = ards.aracadr[0].company;
                        somastds.soaddr[0].address1 = ards.aracadr[0].address1;
                        somastds.soaddr[0].address2 = ards.aracadr[0].address2;
                        somastds.soaddr[0].city = ards.aracadr[0].city;
                        somastds.soaddr[0].state = ards.aracadr[0].state;
                        somastds.soaddr[0].zip = ards.aracadr[0].zip;
                    }
                    else
                    {
                        // If no ship to information, use customer information
                        somastds.soaddr[0].custno = somastds.somast[0].custno;
                        somastds.soaddr[0].sono = somastds.somast[0].sono;
                        somastds.soaddr[0].adtype = "C";
                        somastds.soaddr[0].cshipno = ards.arcust[0].custno;
                        somastds.soaddr[0].company = ards.arcust[0].company;
                        somastds.soaddr[0].address1 = ards.arcust[0].address1;
                        somastds.soaddr[0].address2 = ards.arcust[0].address2;
                        somastds.soaddr[0].city = ards.arcust[0].city;
                        somastds.soaddr[0].state = ards.arcust[0].state;
                        somastds.soaddr[0].zip = ards.arcust[0].zip;
                    }
                    GenerateAppTableRowSave(somastds.soaddr[0]);

                    // Version
                    quotesearchds.soversion.Rows.Clear();
                    ClearParameters();
                    AddParms("@sono", sono, "SQL");
                    commandstring = "SELECT * FROM soversion WHERE sono = @sono";
                    this.FillData(quotesearchds, "soversion", commandstring, CommandType.Text);

                    if (quotesearchds.soversion.Rows.Count > 0)
                    {
                        if (quotesearchds.soversion.Rows.Count == 1)
                        {
                            singleversion = true;
                        }
                        else
                        {
                            singleversion = false;
                        }
                        for (int x = 0; x <= quotesearchds.soversion.Rows.Count - 1; x++)
                        {
                            EstablishBlankSoversionData();
                            somastds.soversion[0].sono = somastds.somast[0].sono;
                            if (!singleversion)
                            {
                                somastds.soversion[0].version = quotesearchds.soversion[x].version;
                            }
                            else
                            {
                                somastds.soversion[0].version = "A";
                            }
                            somastds.soversion[0].adddisc = quotesearchds.soversion[x].adddisc;
                            somastds.soversion[0].adddiscnote = quotesearchds.soversion[x].adddiscnote;
                            somastds.soversion[0].adddiscrate = quotesearchds.soversion[x].adddiscrate;
                            somastds.soversion[0].aralert = quotesearchds.soversion[x].aralert;
                            somastds.soversion[0].csalert = quotesearchds.soversion[x].csalert;
                            somastds.soversion[0].custcomments = quotesearchds.soversion[x].custcomments;
                            somastds.soversion[0].intcomments = quotesearchds.soversion[x].intcomments;
                            somastds.soversion[0].manalert = quotesearchds.soversion[x].manalert;
                            somastds.soversion[0].manlship = quotesearchds.soversion[x].manlship;
                            somastds.soversion[0].ordamt = quotesearchds.soversion[x].ordamt;
                            somastds.soversion[0].product = quotesearchds.soversion[x].product;
                            somastds.soversion[0].qcalert = quotesearchds.soversion[x].qcalert;
                            somastds.soversion[0].shipping = quotesearchds.soversion[x].shipping;
                            somastds.soversion[0].shipalert = quotesearchds.soversion[x].shipalert;
                            somastds.soversion[0].subtotal = quotesearchds.soversion[x].subtotal;
                            somastds.soversion[0].tax = quotesearchds.soversion[x].tax;
                            somastds.soversion[0].taxsamt = quotesearchds.soversion[x].taxsamt;
                            // Save soversion
                            GenerateAppTableRowSave(somastds.soversion[0]);
                        }
                    } // Version
                    // Covers
                    // Cover
                    quotesearchds.socover.Rows.Clear();
                    ClearParameters();
                    AddParms("@sono", sono, "SQL");
                    commandstring = "SELECT * FROM socover WHERE sono = @sono";
                    this.FillData(quotesearchds, "socover", commandstring, CommandType.Text);
                    if (quotesearchds.socover.Rows.Count >= 1)
                    {
                        for (int x = 0; x <= quotesearchds.socover.Rows.Count - 1; x++)
                        {
                            EstablishBlankDataTableRow(somastds.socover);
                            somastds.socover[0].sono = somastds.somast[0].sono;
                            if (!singleversion)
                            {
                                somastds.socover[0].version = quotesearchds.socover[x].version;
                            }
                            else
                            {
                                somastds.socover[0].version = "A";
                            }

                            somastds.socover[0].cover = quotesearchds.socover[x].cover;
                            somastds.socover[0].qtyord = quotesearchds.socover[x].qtyord;
                            somastds.socover[0].manlstr = quotesearchds.socover[x].manlstr;
                            somastds.socover[0].item = quotesearchds.socover[x].item;
                            somastds.socover[0].straps = quotesearchds.socover[x].straps;
                            somastds.socover[0].covertype = quotesearchds.socover[x].covertype;
                            somastds.socover[0].coverstring = quotesearchds.socover[x].coverstring;
                            somastds.socover[0].descrip = quotesearchds.socover[x].descrip;
                            somastds.socover[0].product = quotesearchds.socover[x].product;
                            somastds.socover[0].materialid = quotesearchds.socover[x].materialid;
                            somastds.socover[0].overlapid = quotesearchds.socover[x].overlapid;
                            somastds.socover[0].colorid = quotesearchds.socover[x].colorid;
                            somastds.socover[0].spacingid = quotesearchds.socover[x].spacingid;
                            somastds.socover[0].poolstring = quotesearchds.socover[x].poolstring;
                            somastds.socover[0].cwidft = quotesearchds.socover[x].cwidft;
                            somastds.socover[0].cwidin = quotesearchds.socover[x].cwidin;
                            somastds.socover[0].clenft = quotesearchds.socover[x].clenft;
                            somastds.socover[0].clenin = quotesearchds.socover[x].clenin;
                            somastds.socover[0].pwidft = quotesearchds.socover[x].pwidft;
                            somastds.socover[0].pwidin = quotesearchds.socover[x].pwidin;
                            somastds.socover[0].plenft = quotesearchds.socover[x].plenft;
                            somastds.socover[0].plenin = quotesearchds.socover[x].plenin;
                            GenerateAppTableRowSave(somastds.socover[0]);
                        }
                    }
                    // Lines
                    ClearParameters();
                    quotesearchds.soline.Rows.Clear();
                    AddParms("@sono", sono, "SQL");
                    AddParms("@cover", String.Empty, "SQL");
                    AddParms("@version", String.Empty, "SQL");
                    //commandstring = "SELECT * FROM soline WHERE sono = @sono";
                    commandstring = "jksp_getReinitSoLineItems";

                    this.FillData(quotesearchds, "soline", commandstring, CommandType.StoredProcedure);
                    if (quotesearchds.soline.Rows.Count > 0)
                    {
                        for (int x = 0; x <= quotesearchds.soline.Rows.Count - 1; x++)
                        {
                            EstablishBlankDataTableRow(somastds.soline);
                            somastds.soline[0].sono = somastds.somast[0].sono;
                            if (!singleversion)
                            {
                                somastds.soline[0].version = quotesearchds.soline[x].version;
                            }
                            else
                            {
                                somastds.soline[0].version = "A";
                            }
                            somastds.soline[0].source = quotesearchds.soline[x].source;
                            somastds.soline[0].item = quotesearchds.soline[x].item;
                            somastds.soline[0].descrip = quotesearchds.soline[x].descrip;
                            somastds.soline[0].qtyord = quotesearchds.soline[x].qtyord;
                            somastds.soline[0].qtyact = quotesearchds.soline[x].qtyact;
                            somastds.soline[0].price = quotesearchds.soline[x].price;
                            somastds.soline[0].cover = quotesearchds.soline[x].cover;
                            somastds.soline[0].stkcode = quotesearchds.soline[x].stkcode;
                            somastds.soline[0].taxable = quotesearchds.soline[x].taxable;
                            somastds.soline[0].loctid = quotesearchds.soline[x].loctid;
                            somastds.soline[0].version = quotesearchds.soline[x].version;
                            somastds.soline[0].umfact = quotesearchds.soline[x].umfact;
                            somastds.soline[0].umeasur = quotesearchds.soline[x].umeasur;
                            somastds.soline[0].glsale = quotesearchds.soline[x].glsale;
                            somastds.soline[0].glasst = quotesearchds.soline[x].glasst;
                            somastds.soline[0].glsale = quotesearchds.soline[x].glsale;
                            somastds.soline[0].sostat = quotesearchds.soline[x].sostat;
                            GenerateAppTableRowSave(somastds.soline[0]);
                        }
                    }
                }
            }
            else
            {
                wsgUtilities.wsgNotice("Process Cancelled");
            }
            return newsono;
        }

        public string getSoProduct(string sono)
        {
            string product = "None";
            somastds.socover.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(somastds, "socover", "wsgsp_getsoproduct", CommandType.StoredProcedure);
            if (somastds.socover.Rows.Count > 0)
            {
                product = somastds.socover[0].product;
            }
            return product;
        }

        public bool SoHasTickets(string sono)
        {
            bool hastickets = false;
            ticketDs.view_expandedticket.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            string CommandString = "SELECT * FROM view_expandedticket WHERE RTRIM(ticketstatus) = 'Open' AND sono = @sono";
            FillData(ticketDs, "view_expandedticket", CommandString, CommandType.Text);
            if (ticketDs.view_expandedticket.Rows.Count > 0)
            {
                hastickets = true;
            }
            return hastickets;
        }

        public string RouteSo()
        {
            string trackingresult = trackingInf.RouteToNextStep(somastds.somast[0].sono);
            return trackingresult;
        }

        public void CalcSOPrices(string sono)
        {
            string commandstring = "SELECT * FROM somast WHERE sono = @sono";
            decimal adddiscountrate = 0;
            decimal allcoverstotal = 0;
            string stockitem = "";
            somastds.somast.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            FillData(somastds, "somast", commandstring, CommandType.Text);
            if (somastds.somast.Rows.Count > 0)
            {
                // Locate the customer
                ClearParameters();
                AddParms("@custno", somastds.somast[0].custno, "SQL");
                commandstring = "SELECT * FROM arcust  WHERE custno = @custno";
                this.FillData(somastds, "arcust", commandstring, CommandType.Text);
                if (somastds.arcust.Rows.Count > 0)
                {
                    ClearParameters();
                    AddParms("@sono", somastds.somast[0].sono, "SQL");
                    commandstring = "SELECT * FROM soversion WHERE sono = @sono";
                    this.FillData(somastds, "soversion", commandstring, CommandType.Text);
                    if (somastds.soversion.Rows.Count > 0)
                    {
                        for (int v = 0; v <= somastds.soversion.Rows.Count - 1; v++)
                        {
                            // Process covers
                            somastds.socover.Rows.Clear();
                            this.ClearParameters();
                            this.AddParms("@sono", sono, "SQL");
                            this.AddParms("@version", somastds.soversion[v].version, "SQL");
                            commandstring = "SELECT * FROM socover WHERE sono = @sono AND version = @version";
                            this.FillData(somastds, "socover", commandstring, CommandType.Text);
                            stockitem = "";
                            // First check for the presence of a stock cover
                            for (int c = 0; c <= somastds.socover.Rows.Count - 1; c++)
                            {
                                if (somastds.socover[c].product.TrimEnd().ToUpper() == "STOCK COVER")
                                {
                                    // Save the item for the stock cover
                                    stockitem = somastds.socover[c].item;
                                }
                            }

                            if ((stockitem.TrimEnd() != "") && somastds.soversion[v].adddiscrate == 0 && (somastds.somast[0].standdisc != 0 || somastds.somast[0].stockdisc != 0))
                            {
                                itemds.immaster.Rows.Clear();
                                string CommandString = "SELECT * FROM immaster WHERE item = @item";
                                this.ClearParameters();
                                this.AddParms("@item", stockitem, "SQL");
                                this.FillData(itemds, "immaster", CommandString, CommandType.Text);
                                if (itemds.immaster.Rows.Count != 0)
                                {
                                    if (itemds.immaster[0].stkstd.ToUpper().TrimEnd() == "STK")
                                    {
                                        if (somastds.somast[0].stockdisc != 0)
                                        {
                                            adddiscountrate = somastds.somast[0].stockdisc;
                                            somastds.soversion[v].adddiscnote =
                                            "Stock cover discount of " + adddiscountrate.ToString("N0").TrimEnd() + "%  applied";
                                        }
                                    }
                                    else
                                    {
                                        if (somastds.somast[0].standdisc != 0)
                                        {
                                            adddiscountrate = somastds.somast[0].standdisc;
                                            somastds.soversion[v].adddiscnote =
                                            "Standard cover discount of " + adddiscountrate.ToString("N0").TrimEnd() + "%  applied";
                                        }
                                    }
                                }
                            } // stockitem

                            if (adddiscountrate != 0)
                            {
                                // Compute the addtional discount if needed
                                somastds.soversion[v].adddisc = decimal.Round((allcoverstotal * adddiscountrate / 100), 2);
                            }
                            else
                            {
                                somastds.soversion[v].adddisc = 0;
                            }

                            for (int c = 0; c <= somastds.socover.Rows.Count - 1; c++)
                            {
                                // Skip extensions
                                if (somastds.socover[c].covertype.TrimEnd() != "C")
                                {
                                    continue;
                                }
                                // Discount the cover
                                somastds.socover[c].disc = somastds.somast[0].salesdisc;
                                somastds.socover[c].extprice = (Convert.ToDecimal(somastds.socover[c].price) * Convert.ToDecimal(somastds.socover[c].qtyord)) -
                                     ((Convert.ToDecimal(somastds.socover[c].price) * Convert.ToDecimal(somastds.socover[c].qtyord)) * (somastds.somast[0].salesdisc / 100));
                                Decimal.Round(somastds.socover[c].extprice, 2);
                                // Add to cover total
                                allcoverstotal += somastds.socover[c].extprice;
                                // Reprice Line Items for the cover
                                somastds.soline.Rows.Clear();
                                adddiscountrate = somastds.soversion[v].adddiscrate;
                                this.ClearParameters();
                                this.AddParms("@sono", sono, "SQL");
                                this.AddParms("@version", somastds.soversion[v].version, "SQL");
                                this.AddParms("@cover", somastds.socover[c].cover, "SQL");
                                commandstring = "SELECT * FROM soline WHERE sono = @sono AND version =@version AND cover = @cover";
                                this.FillData(somastds, "soline", commandstring, CommandType.Text);
                                for (int l = 0; l <= somastds.soline.Rows.Count - 1; l++)
                                {
                                    // Apply discount to line and extend it
                                    if (CheckDiscount(somastds.soline[l].item))
                                    {
                                        somastds.soline[l].disc = somastds.somast[0].salesdisc;
                                    }
                                    else
                                    {
                                        somastds.soline[l].disc = 0;
                                    }
                                    somastds.soline.AcceptChanges();
                                    somastds.soline[l].extprice = decimal.Round((somastds.soline[l].price * somastds.soline[l].qtyord)
                                             - ((somastds.soline[l].price * somastds.soline[l].qtyord) * (somastds.soline[l].disc / 100)), 2);
                                    somastds.soline.AcceptChanges();
                                    GenerateAppTableRowSave(somastds.soline[l]);
                                    allcoverstotal += somastds.soline[l].extprice;
                                }
                                GenerateAppTableRowSave(somastds.socover[c]);
                            }// cover loop

                            somastds.soversion[v].subtotal = allcoverstotal - somastds.soversion[v].adddisc;
                            if (somastds.soversion[v].manlship == false)
                            {
                                somastds.soversion[v].shipping = 0;
                                // Calculate shipping if needed
                                if (somastds.somast[0].shipdisc != 0)
                                {
                                    somastds.soversion[v].shipping = decimal.Round(somastds.soversion[v].subtotal *
                                   (somastds.somast[0].shipdisc / 100), 2);
                                }
                            }
                            somastds.soversion.AcceptChanges();
                            // Calculate tax here
                            if (somastds.somast[0].taxrate != 0)
                            {
                                somastds.soversion[v].taxsamt = somastds.soversion[v].subtotal +
                                 somastds.soversion[v].shipping;

                                somastds.soversion[v].tax = decimal.Round((somastds.soversion[v].subtotal +
                                somastds.soversion[v].shipping) * (somastds.somast[0].taxrate / 100), 2);
                            }
                            else
                            {
                                somastds.soversion[v].taxsamt = 0;
                                somastds.soversion[v].tax = 0;
                            }
                            somastds.soversion.AcceptChanges();

                            somastds.soversion[v].ordamt = somastds.soversion[v].subtotal +
                                somastds.soversion[v].shipping +
                                somastds.soversion[v].tax;

                            // Calculate the deposit
                            if (somastds.soversion[v].ordamt < 1000)
                            {
                                somastds.soversion[v].depositreq = decimal.Round((somastds.soversion[v].ordamt *
                                (somastds.arcust[0].depover0 / 100)), 2);
                            }
                            else
                            {
                                if (somastds.soversion[v].ordamt < 2000)
                                {
                                    somastds.soversion[v].depositreq = decimal.Round((somastds.soversion[v].ordamt *
                                  (somastds.arcust[0].depover1 / 100)), 2);
                                }
                                else
                                {
                                    if (somastds.soversion[v].ordamt < 3000)
                                    {
                                        somastds.soversion[v].depositreq = decimal.Round((somastds.soversion[v].ordamt *
                                       (somastds.arcust[0].depover2 / 100)), 2);
                                    }
                                    else
                                    {
                                        somastds.soversion[v].depositreq = decimal.Round((somastds.soversion[v].ordamt *
                                       (somastds.arcust[0].depover3 / 100)), 2);
                                    }
                                }
                            }

                            GenerateAppTableRowSave(somastds.soversion[v]);
                        } // version loop
                    } // version count
                    else
                    {
                        wsgUtilities.wsgNotice("There is no version data for this SO");
                    }
                } // arcust count
                else
                {
                    wsgUtilities.wsgNotice("Cannot locate the customer for this SO");
                }
            } // somast count
            else
            {
                wsgUtilities.wsgNotice("Cannot locate thiss SO");
            }
        } // CalcSoPrices

        public void UpdateColorForAllCovers(string sono, string version, int colorId, bool silent = false)
        {
            bool agree = silent ? true : wsgUtilities.wsgReply($"Color has changed in one cover for version {version}. Would you like to update it for all covers in that version? ");
            if (agree)
            {
                this.ClearParameters();
                this.AddParms("@sono", sono, "SQL");
                this.AddParms("@version", version, "SQL");
                this.AddParms("@colorId", colorId, "SQL");
                try
                {
                    ExecuteCommand("jksp_updateColorAllSoCovers", CommandType.StoredProcedure);
                }
                catch (SqlException ex)
                {
                    HandleException(ex);
                }
            }
        }

        public void UpdateMaterialForAllCovers(string sono, string version, int materialId, bool silent = false)
        {
            bool agree = silent ? true : wsgUtilities.wsgReply($"Material has changed in one cover for version {version}. Would you like to update it for all covers in that version? ");
            if (agree)
            {
                this.ClearParameters();
                this.AddParms("@sono", sono, "SQL");
                this.AddParms("@version", version, "SQL");
                this.AddParms("@materialId", materialId, "SQL");
                try
                {
                    ExecuteCommand("jksp_updateMaterialAllSoCovers", CommandType.StoredProcedure);
                }
                catch (SqlException ex)
                {
                    HandleException(ex);
                }
            }
        }

    } // class

    #endregion
} // namespace