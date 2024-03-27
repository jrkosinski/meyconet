using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public class SystemCommentsMaintenance : WSGDataAccess
    {
        //   public system systemds { get; set; }
        public DataSet ds { get; set; }

        public system systemds { get; set; }

        public SystemCommentsMaintenance(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            systemds = new system();
            SetIdcol(systemds.systemcomments.idcolColumn);
        }

        public void DeleteSystemcommentsrow()
        {
            DeleteTableRow("systemcomments", systemds.systemcomments[0].idcol);
        }

        public void GetSystemCommentsData()
        {
            systemds.view_systemcomments.Clear();
            this.ClearParameters();
            this.FillData(systemds, "view_systemcomments", "wsgsp_getsystemcommentsdata", CommandType.StoredProcedure);
        }

        public void UnlockSystemcomments(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "systemcomments", "SQL");
            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public string LockSystemcomments(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "systemcomments", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void getSingleSystemcommentsData(int idcol)
        {
            systemds.systemcomments.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(systemds, "systemcomments", "wsgsp_getsinglesystemcommentsdata", CommandType.StoredProcedure);
        }

        public void EstablishBlankSystemCommentsData()
        {
            EstablishBlankDataTableRow(systemds.systemcomments);
        }

        public void ClearSystemcommentsData()
        {
            ClearDataTable(systemds.systemcomments);
            systemds.systemcomments.Clear();
        }

        public void SaveSystemComments()
        {
            GenerateAppTableRowSave(systemds.systemcomments[0]);
        }
    }

    #region Reference Maintenance

    public class ReferenceMaintenance : WSGDataAccess
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Reference Data");
        public reference referenceds { get; set; }
        public DataSet ds { get; set; }

        public bool HasSelectedMaterial
        {
            get { return (referenceds.qumaterial != null && referenceds.qumaterial.Count > 0); }
        }

        public bool HasSelectedColor
        {
            get { return (referenceds.qucolor != null && referenceds.qucolor.Count > 0); }
        }

        public bool HasSelectedOverlap
        {
            get { return (referenceds.quoverlap != null && referenceds.quoverlap.Count > 0); }
        }

        public ReferenceMaintenance(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            referenceds = new reference();
            SetIdcol(referenceds.quspacing.idcolColumn);
            SetIdcol(referenceds.qumaterial.idcolColumn);
            SetIdcol(referenceds.quoverlap.idcolColumn);
            SetIdcol(referenceds.qucolor.idcolColumn);
        }

        # region Spacing Maintenance

        public void GetSpacingData()
        {
            referenceds.view_quspacingdata.Clear();
            this.ClearParameters();
            this.FillData(referenceds, "view_quspacingdata", "wsgsp_getquspacingdata", CommandType.StoredProcedure);
        }

        public void SaveSpacingData()
        {
            GenerateAppTableRowSave(referenceds.quspacing[0]);
        }

        //TODO: these locking methods are repetitive 
        public string LockQuSpacing(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "quspacing", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void EstablishBlankQuSpacingData()
        {
            EstablishBlankDataTableRow(referenceds.quspacing);
            referenceds.quspacing[0].strpmult = 0.000M;
        }

        public void ClearQuSpacingData()
        {
            ClearDataTable(referenceds.quspacing);
            referenceds.quspacing.Clear();
        }

        public void UnlockQuSpacing(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "quspacing", "SQL");
            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public void getSingleSpacingData(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(referenceds, "quspacing", "wsgsp_getsinglequspacingdata", CommandType.StoredProcedure);
        }

        #endregion Reference Maintenance
        #region Material Maintenance

        public void getSingleMaterialData(int idcol)
        {
            referenceds.qumaterial.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "qumaterial", "SQL");
            this.FillData(referenceds, "qumaterial", "wsgsp_getsingletablerow", CommandType.StoredProcedure);
            referenceds.qumaterial.AcceptChanges();
        }

        public void GetMaterialData()
        {
            referenceds.view_qumaterialdata.Clear();
            this.ClearParameters();
            this.FillData(referenceds, "view_qumaterialdata", "wsgsp_getqumaterialdata", CommandType.StoredProcedure);
        }

        public void DeleteQumaterialRow()
        {
            if (this.HasSelectedMaterial)
            {
                DeleteTableRow("qumaterial", referenceds.qumaterial[0].idcol);
            }
        }

        public string LockQuMaterial(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "qumaterial", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void UnlockQuMaterial(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "qumaterial", "SQL");
            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public void SaveMaterialData()
        {
            GenerateAppTableRowSave(referenceds.qumaterial[0]);
        }

        public void EstablishBlankQuMaterialData()
        {
            EstablishBlankDataTableRow(referenceds.qumaterial);
        }

        public void ClearQuMaterialData()
        {
            ClearDataTable(referenceds.qumaterial);
            referenceds.qumaterial.Clear();
        }

        #endregion Material Maintenance

        #region Overlap Maintenance

        public void getSingleOverlapData(int idcol)
        {
            referenceds.quoverlap.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "quoverlap", "SQL");
            this.FillData(referenceds, "quoverlap", "wsgsp_getsingletablerow", CommandType.StoredProcedure);
            referenceds.qumaterial.AcceptChanges();
        }

        public void GetOverlapData()
        {
            referenceds.view_quoverlapdata.Clear();
            this.ClearParameters();
            this.FillData(referenceds, "view_quoverlapdata", "wsgsp_getquoverlapdata", CommandType.StoredProcedure);
        }

        public void DeleteQuoverlapRow()
        {
            if (this.HasSelectedOverlap)
            {
                DeleteTableRow("quoverlap", referenceds.quoverlap[0].idcol);
            }
        }

        public string LockQuOverlap(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "quoverlap", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void UnlockQuOverlap(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "quoverlap", "SQL");
            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public void SaveOverlapData()
        {
            GenerateAppTableRowSave(referenceds.quoverlap[0]);
        }

        public void EstablishBlankQuOverlapData()
        {
            EstablishBlankDataTableRow(referenceds.quoverlap);
        }

        public void ClearQuOverlapData()
        {
            ClearDataTable(referenceds.quoverlap);
            referenceds.quoverlap.Clear();
        }

        #endregion Overlap Maintenance

        #region Color Maintenance

        public void getSingleColorData(int idcol)
        {
            referenceds.qucolor.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "qucolor", "SQL");
            this.FillData(referenceds, "qucolor", "wsgsp_getsingletablerow", CommandType.StoredProcedure);
            referenceds.qumaterial.AcceptChanges();
        }

        public void GetColorData()
        {
            referenceds.view_qucolordata.Clear();
            this.ClearParameters();
            this.FillData(referenceds, "view_qucolordata", "wsgsp_getqucolordata", CommandType.StoredProcedure);
        }

        public void DeleteQucolorRow()
        {
            if (this.HasSelectedColor)
            {
                DeleteTableRow("qucolor", referenceds.qucolor[0].idcol);
            }
        }

        public string LockQuColor(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "qucolor", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void UnlockQuColor(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "qucolor", "SQL");
            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public void SaveColorData()
        {
            GenerateAppTableRowSave(referenceds.qucolor[0]);
        }

        public void EstablishBlankQuColorData()
        {
            EstablishBlankDataTableRow(referenceds.qucolor);
        }

        public void ClearQuColorData()
        {
            ClearDataTable(referenceds.qucolor);
            referenceds.qucolor.Clear();
        }

        #endregion Color Maintenance
    } // end class Reference Maintenace

    #endregion

    #region Item Synchronization

    #endregion

    #region Item Access

    public class ImmasterAccess : WSGDataAccess
    {
        private static ObjectCache itemDsCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_InventoryItems"]));
        public static item _itemds = new item();

        public item itemds { get { return _itemds; } }
        public DataSet ds { get; set; }

        public ImmasterAccess(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
        }

        public void GetSelectedItemGroup(string code)
        {
            this.ClearParameters();
            this.AddParms("@code", code, "SQL");
            this.FillData(_itemds, "view_immasterdata", "wsgsp_getquoteitem", CommandType.StoredProcedure);
        }

        public void GetImmasterData()
        {
            //CACHED Maintain -> Inventory Items
            if (itemDsCache.IsInvalid)
            {
                itemds.view_immasterdata.Rows.Clear();
                this.ClearParameters();
                this.FillData(_itemds, "view_immasterdata", "wsgsp_getimmasterdata", CommandType.StoredProcedure);
                itemDsCache.Refresh(_itemds);
            }
        }

        public void getSingleImmasterData(string item)
        {
            itemds.immaster.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.FillData(_itemds, "immaster", "wsgsp_getsingleimmasterdata", CommandType.StoredProcedure);
        }
    } // end class ItemAccess

    #endregion

    public class UserInformation
    {
        public string username = "";
        public string passwd = "";
        public string userrole = "";
        public string userstatus = "";
        public string emailaddress = "";
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("User Information");

        public UserInformation(string userid)
        {
            GetUserData(userid);
        }

        public bool GetUserData(string userid)
        {
            bool userdataOK = true;

            AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
            appInformation.GetUserData(userid);
            if (appInformation.systemds.appuser.Rows.Count > 0)
            {
                username = appInformation.systemds.appuser[0].username;
                passwd = appInformation.systemds.appuser[0].passwd;
                userrole = appInformation.systemds.appuser[0].userrole;
                userstatus = appInformation.systemds.appuser[0].userstatus;
                emailaddress = appInformation.systemds.appuser[0].emailaddress;
            }
            else
            {
                wsgUtilities.wsgNotice("Error retrieving user data");
                userdataOK = false;
            }
            return userdataOK;
        }
    }

    #region App Information

    public class AppInformation : WSGDataAccess
    {
        public system appinfods { get; set; }
        public quote quods { get; set; }
        public system systemds { get; set; }
        public inventoryds invds { get; set; }
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Tracking Information");

        public AppInformation(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            appinfods = new system();
            quods = new quote();
            systemds = new system();
            invds = new inventoryds();
            SetIdcol(appinfods.appinfo.idcolColumn);
        }

        public string GetStkloc()
        {
            appinfods.appinfo.Clear();
            this.ClearParameters();
            this.FillData(appinfods, "appinfo", "wsgsp_getappinfo", CommandType.StoredProcedure);
            return appinfods.appinfo[0].stkloc;
        }

        public void GetUserData(string userid)
        {
            string CommandString = "SELECT * FROM appuser where userid = @userid";
            systemds.appuser.Rows.Clear();
            ClearParameters();
            this.AddParms("userid", userid, "SQL");
            this.FillData(systemds, "appuser", CommandString, CommandType.Text);
        }

        public string GetMeycono()
        {
            int RetryLimit = 500;
            bool MeycnoOK = false;
            appinfods.appinfo.Clear();
            this.ClearParameters();
            this.FillData(appinfods, "appinfo", "wsgsp_getappinfo", CommandType.StoredProcedure);
            string[] decades = new string[11] { "O", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };
            // Create the prefix based on the decade and year
            int DecadeNumber = Convert.ToInt32(String.Format("{0:yy/MM/dd/}", DateTime.Now.Date).Substring(0, 1));
            string MeyconoPrefix = decades[DecadeNumber] + String.Format("{0:yy/MM/dd/}", DateTime.Now.Date).Substring(0, 2);
            string Somastcommandstring = " SELECT * FROM somast WHERE RTRIM(meycono) = @meycono ";
            string Ictrancommandstring = " SELECT * FROM ictran WHERE RTRIM(meycono) = @meycono ";
            for (int i = 1; i < RetryLimit; i++)
            {
                if (appinfods.appinfo[0].lastmeycono.StartsWith(MeyconoPrefix))
                {
                    appinfods.appinfo[0].lastmeycono = MeyconoPrefix + (Convert.ToInt32(appinfods.appinfo[0].lastmeycono.Substring(3, 5)) + i).ToString().PadLeft(5, '0');
                }
                else
                {
                    // Nothing found. Start the year
                    appinfods.appinfo[0].lastmeycono = MeyconoPrefix + "00001";
                }
                appinfods.appinfo.AcceptChanges();
                GenerateAppTableRowSave(appinfods.appinfo[0]);
                // Check somast
                quods.somast.Rows.Clear();
                ClearParameters();
                AddParms("@meycono", appinfods.appinfo[0].lastmeycono, "SQL");
                FillData(quods, "somast", Somastcommandstring, CommandType.Text);
                if (quods.somast.Rows.Count != 0)
                {
                    continue;
                }
                // Check ictran
                invds.ictran.Rows.Clear();
                ClearParameters();
                AddParms("@meycono", appinfods.appinfo[0].lastmeycono, "SQL");
                FillData(invds, "ictran", Ictrancommandstring, CommandType.Text);
                if (invds.ictran.Rows.Count != 0)
                {
                    continue;
                }
                MeycnoOK = true;
                break;
            }
            if (!MeycnoOK)
            {
                appinfods.appinfo[0].lastmeycono = "";
                wsgUtilities.wsgNotice("There has been an error developing the Meyco Plan Number. Get Help.");
            }
            return appinfods.appinfo[0].lastmeycono;
        }

        public string GetNextSono()
        {
            string sono = "";
            decimal nextsono = 0;
            appinfods.appinfo.Rows.Clear();
            this.ClearParameters();
            this.FillData(appinfods, "appinfo", "wsgsp_getappinfo", CommandType.StoredProcedure);
            nextsono = appinfods.appinfo[0].nextsono;
            nextsono = ValidateSono(nextsono);
            sono = nextsono.ToString().PadLeft(10);
            nextsono++;
            this.ClearParameters();
            this.AddParms("@nextsono", nextsono, "SQL");

            try
            {
                ExecuteCommand("wsgsp_updatenextsono", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }

            // update next sons here
            return sono;
        }

        public decimal GetDistrictTaxRate(string taxdist)
        {
            taxdata taxdatads = new taxdata();
            decimal taxrate = 0;
            string commandtext = "SELECT * FROM view_taxdistrictrate WHERE taxdist = @taxdist";
            taxdatads.view_taxdistrictrate.Rows.Clear();
            ClearParameters();
            AddParms("@taxdist", taxdist, "SQL");
            FillData(taxdatads, "view_taxdistrictrate", commandtext, CommandType.Text);
            if (taxdatads.view_taxdistrictrate.Rows.Count > 0)
            {
                taxrate = taxdatads.view_taxdistrictrate[0].taxrate;
            }
            return taxrate;
        }

        public string GetDistrictDescription(string taxdist)
        {
            taxdata taxdatads = new taxdata();
            string descrip = "Unknown";
            taxdatads.cotaxtbl.Rows.Clear();
            string commandtext = "SELECT * FROM cotaxtbl WHERE taxdist = @taxdist AND delmark = 0 and active = 1";
            ClearParameters();
            AddParms("@taxdist", taxdist, "SQL");
            FillData(taxdatads, "cotaxtbl", commandtext, CommandType.Text);
            if (taxdatads.cotaxtbl.Rows.Count > 0)
            {
                descrip = taxdatads.cotaxtbl[0].descrip;
            }
            return descrip;
        }

        public string SelectTaxTable()
        {
            string taxdist = "";
            string commandtext = "SELECT * FROM cotaxtbl WHERE delmark = 0 and active = 1  ORDER BY descrip";
            FrmSelector frmSelector = new FrmSelector();
            KeyedSelectorMethods frmSelectorMethods = new KeyedSelectorMethods();
            taxdata taxdatads = new taxdata();
            taxdata taxselectords = new taxdata();
            FillData(taxdatads, "cotaxtbl", commandtext, CommandType.Text);
            frmSelectorMethods.FormText = "Select Tax District";
            frmSelectorMethods.dtSource = taxdatads.cotaxtbl;
            frmSelectorMethods.columncount = 2;
            frmSelectorMethods.SetColumns();
            frmSelectorMethods.colname[0] = "TaxDistcol";
            frmSelectorMethods.colheadertext[0] = "District";
            frmSelectorMethods.coldatapropertyname[0] = "taxdist";
            frmSelectorMethods.colwidth[0] = 150;
            frmSelectorMethods.colname[1] = "Decripcol";
            frmSelectorMethods.colheadertext[1] = "Description";
            frmSelectorMethods.coldatapropertyname[1] = "descrip";
            frmSelectorMethods.colwidth[1] = 400;
            frmSelectorMethods.SetGrid();
            frmSelector.Width = 700;
            frmSelectorMethods.ShowStringSelector("taxdist");

            // If a row has been selected fill the data and process
            if (frmSelectorMethods.SelectedString != null)
            {
                if (frmSelectorMethods.SelectedString.TrimEnd() != "")
                {
                    taxselectords.cotaxtbl.Rows.Clear();
                    ClearParameters();
                    AddParms("@taxdist", frmSelectorMethods.SelectedString, "SQL");
                    commandtext = "SELECT * FROM  cotaxtbl  WHERE taxdist = @taxdist";
                    FillData(taxselectords, "cotaxtbl", commandtext, CommandType.Text);
                    if (taxselectords.cotaxtbl.Count > 0)
                    {
                        taxdist = taxselectords.cotaxtbl[0].taxdist;
                    }
                }
            }
            return taxdist;
        }

        public decimal ValidateSono(decimal nextsono)
        {
            int retrylimit = 500;
            decimal testsono = 0;
            int retrycount = 0;
            bool searchfound = false;

            while (retrycount < retrylimit)
            {
                searchfound = false;
                testsono = nextsono + retrycount;
                quods.somast.Rows.Clear();
                this.ClearParameters();
                this.ClearParameters();
                this.AddParms("@sono", testsono.ToString().PadLeft(10), "SQL");
                this.FillData(quods, "somast", "wsgsp_getsomastbysono", CommandType.StoredProcedure);

                if (quods.somast.Rows.Count > 0)
                {
                    searchfound = true;
                }
                if (searchfound == false)
                {
                    break;
                }
                else
                {
                    retrycount++;
                    if (retrycount == retrylimit)
                    {
                        testsono = 0;
                        break;
                    }
                }
            }
            return testsono;
        }
    }

    #endregion

    #region Tracking Class

    public class TrackingInf : WSGDataAccess
    {
        public tracking trackingds { get; set; }
        public tracking testtrackingds { get; set; }
        public tracking listtrackingds { get; set; }
        private ticketds ticketDs = new ticketds();
        public quote somastds { get; set; }
        public string currentsono { get; set; }
        private WSGUtilities wsgUtilities = new WSGUtilities("Tracking Information");
        public SqlConnection conn = new SqlConnection();
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();

        public TrackingInf(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            // Establish the SQL Connection string
            conn.ConnectionString = myAppconstants.SQLConnectionString;
            trackingds = new tracking();
            testtrackingds = new tracking();
            listtrackingds = new tracking();
            somastds = new quote();
            SetIdcol(trackingds.jtrak.idcolColumn);
            SetIdcol(trackingds.workgroup.idcolColumn);
            SetIdcol(trackingds.step.idcolColumn);
            SetIdcol(trackingds.route.idcolColumn);
            SetIdcol(trackingds.stepsubscriber.idcolColumn);
            SetIdcol(trackingds.appuser.idcolColumn);
        }

        #region Tracking Methods

        public bool IsStepOKToInvoice(string Sono)
        {
            bool oktoinvoce = false;
            GetView_solasttrackingstepdata(Sono);
            if (trackingds.view_solasttrackingdata.Rows.Count > 0)
            {
                if (trackingds.view_solasttrackingdata[0].oktoinvoice == "Y")
                {
                    oktoinvoce = true;
                }
                else
                {
                    wsgUtilities.wsgNotice("Step " + trackingds.view_solasttrackingdata[0].code.TrimEnd() + " does not permit invoicing.");
                }
            }
            else
            {
                wsgUtilities.wsgNotice("This SO has not been tracked. Invoicing Cancelled.");
            }
            return oktoinvoce;
        }

        public bool SoHasTickets(string sono)
        {
            bool hastickets = false;
            ticketDs.view_expandedticket.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            string CommandString = "SELECT * FROM view_expandedticket WHERE RTRIM(ticketstatus) = 'Open' AND  sono = @sono";
            FillData(ticketDs, "view_expandedticket", CommandString, CommandType.Text);
            if (ticketDs.view_expandedticket.Rows.Count > 0)
            {
                hastickets = true;
            }
            return hastickets;
        }

        public string RouteToNextStep(string CurrentSono)
        {
            string ReturnMessage = "OK";
            int CurrentStepid = GetCurrentStepid(CurrentSono);
            if (CurrentStepid > 0)
            {
                FrmRouteStepComment frmRouteStepComment = new FrmRouteStepComment();
                frmRouteStepComment.CurrentSono = CurrentSono;
                frmRouteStepComment.CurrentRouteId = CurrentStepid;
                frmRouteStepComment.ShowDialog();
            }
            return ReturnMessage;
        }

        public string GetSoStatus(string sono)
        {
            string sostatus = "";
            ClearParameters();
            somastds.somast.Rows.Clear();
            this.AddParms("@sono", sono, "SQL");
            string comandtext = "SELECT * FROM somast WHERE sono = @sono";
            FillData(somastds, "somast", comandtext, CommandType.Text);
            if (somastds.somast.Rows.Count > 0)
            {
                sostatus = somastds.somast[0].sostat;
            }
            return sostatus;
        }

        public void GetView_solasttrackingstepdata(string sono)
        {
            trackingds.view_solasttrackingdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(trackingds, "view_solasttrackingdata", "wsgsp_getsolasttrackingdata", CommandType.StoredProcedure);
        }

        public void GetLatestTrackingStepsbyStepid(int stepid)
        {
            this.ClearParameters();
            trackingds.view_latestsotrackingstepdata.Rows.Clear();
            this.AddParms("@stepid", stepid, "SQL");
            this.FillData(trackingds, "view_latestsotrackingstepdata", "wsgsp_GetLatestTrackingStepsbyStepid", CommandType.StoredProcedure);
        }

        public string GetLastSOTrackingData(string sono)
        {
            string trackingdata = "";
            GetView_solasttrackingstepdata(sono);
            if (trackingds.view_solasttrackingdata.Rows.Count > 0)
            {
                trackingdata = "Last tracked to: " + trackingds.view_solasttrackingdata[0].code.TrimEnd() +
                " " + trackingds.view_solasttrackingdata[0].descrip.TrimEnd() +
                " Date: " + trackingds.view_solasttrackingdata[0].trackdate.ToString("MM/dd/yy");
            }
            else
            {
                trackingdata = "No tracking data";
            }
            return trackingdata;
        }

        public void SetCancelledTrackingStep(string sono)
        {
            // Establish the SQL command to locate the CF Step
            string Commandstring = "SELECT * FROM step WHERE code = 'CF'";
            ClearParameters();
            this.FillData(trackingds, "step", Commandstring, CommandType.Text);
            if (trackingds.step.Rows.Count > 0)
            {
                routeSO(trackingds.step[0].idcol, sono, "Void", DateTime.Now);
            }
            else
            {
                wsgUtilities.wsgNotice("No CF trackng Step Located");
            }
        }

        public bool SuspendSO(string sono)
        {
            bool sosuspended = true;
            // Establish the SQL command to locate the Suspend Step
            string Commandstring = "SELECT * FROM step WHERE code = 'SUSP'";
            ClearParameters();
            this.FillData(trackingds, "step", Commandstring, CommandType.Text);
            if (trackingds.step.Rows.Count > 0)
            {
                routeSO(trackingds.step[0].idcol, sono, "Suspended", DateTime.Now);
            }
            else
            {
                wsgUtilities.wsgNotice("No Suspend Step Located");
                sosuspended = false;
            }
            return sosuspended;
        }

        public void GetSoTrackingSteps(string sono)
        {
            this.ClearParameters();
            trackingds.view_trackingstepdata.Rows.Clear();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(trackingds, "view_trackingstepdata", "wsgsp_getsotracking", CommandType.StoredProcedure);
        }

        public int GetCurrentStepid(string CurrentSono)
        {
            int CurrentStepid = 0;
            trackingds.view_solasttrackingdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", CurrentSono, "SQL");
            this.FillData(trackingds, "view_solasttrackingdata", "wsgsp_getsolasttrackingdata", CommandType.StoredProcedure);
            if (trackingds.view_solasttrackingdata.Rows.Count > 0)
            {
                CurrentStepid = trackingds.view_solasttrackingdata[0].stepid;
            }
            else
            {
                // Establish the SQL command to locate the INIT Step
                SqlCommand cmd2 = new SqlCommand("dbo.sp_getinitid");
                appUtilities.makeSQLCommand(ref cmd2, ref conn);
                DataTable dtInitID = new DataTable();
                try
                {
                    conn.Open();
                    dtInitID.Load(cmd2.ExecuteReader());
                    if (dtInitID.Rows.Count > 0)
                    {
                        CurrentStepid = Convert.ToInt32(dtInitID.Rows[0]["idcol"]);
                        conn.Close(); // Close the connection. It will opened in the routeSO module
                    } // end if
                    else
                    {
                        wsgUtilities.wsgNotice("There is no INIT Step.");
                    } // end else
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    conn.Close();
                }
            }
            return CurrentStepid;
        }

        public void GetLatestTrackingStepsbyWorkgroupId(int workgroupid)
        {
            this.ClearParameters();
            trackingds.view_latestsotrackingstepdata.Rows.Clear();
            this.AddParms("@workgroupid", workgroupid, "SQL");
            this.FillData(trackingds, "view_latestsotrackingstepdata", "wsgp_GetLatestTrackingStepsbyWorkgroupId", CommandType.StoredProcedure);
        }

        #endregion

        public void routeSO(int RouteToStepId, string CurrentSono, string Comment, DateTime Trackdate)
        {
            this.ClearParameters();
            this.AddParms("@stepid", RouteToStepId, "SQL");
            this.AddParms("@sono", CurrentSono, "SQL");
            this.AddParms("@trackdate", Trackdate, "SQL");
            this.AddParms("@comment", Comment, "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            try
            {
                ExecuteCommand("dbo.sp_inserttrackingevent", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Routing complete");
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        #region Workgroup Processing

        public string InsertWorkGroupStep(int workgroupid, int workgroupstepid)
        {
            this.ClearParameters();
            this.AddParms("@workgroup", workgroupid, "SQL");
            this.AddParms("@stepid", workgroupstepid, "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@InsertMessage", 30);
            return this.ExecuteStringOutputCommand("dbo.wsgsp_insertworkgroupstep", CommandType.StoredProcedure);
        }

        public void DeleteWorkGroupStep(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            ExecuteCommand("dbo.wsgsp_deleteworkgroupstep", CommandType.StoredProcedure);
        }

        public void GetSingleWorkGroupStep(int idcol)
        {
            this.ClearParameters();
            trackingds.workgroupstep.Rows.Clear();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(trackingds, "workgroupstep", "wsgsp_getsingleworkgroupstep", CommandType.StoredProcedure);
        }

        public void GetWorkGroupStepData(int workgroupid)
        {
            this.ClearParameters();
            listtrackingds.view_workgroupstepdata.Clear();
            this.AddParms("@workgroupid", workgroupid, "SQL");
            this.FillData(listtrackingds, "view_workgroupstepdata", "wsgsp_getworkgroupstepdata", CommandType.StoredProcedure);
        }

        public void GetWorkGroups()
        {
            this.ClearParameters();
            listtrackingds.workgroup.Rows.Clear();
            this.FillData(listtrackingds, "workgroup", "wsgsp_getworkgroups", CommandType.StoredProcedure);
        }

        public void GetSingleWorkGroup(int idcol)
        {
            this.ClearParameters();
            trackingds.workgroup.Rows.Clear();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(trackingds, "workgroup", "wsgsp_getsingleworkgroup", CommandType.StoredProcedure);
        }

        public void IntializeWorkGroup()
        {
            trackingds.workgroup.Rows.Clear();
            EstablishBlankDataTableRow(trackingds.workgroup);
        }

        public void SaveWorkGroup()
        {
            GenerateAppTableRowSave(trackingds.workgroup[0]);
        }

        #endregion

        #region Tracking Step Maintenance

        public void GetNonSubscribers(int stepid)
        {
            this.ClearParameters();
            trackingds.appuser.Rows.Clear();
            this.AddParms("@stepid", stepid, "SQL");
            this.FillData(trackingds, "appuser", "wsgsp_getnonsubscribers", CommandType.StoredProcedure);
        }

        public string DeleteTrackingStep(int idcol)
        {
            this.ClearParameters();
            string returnmessage = "";
            this.AddParms("idcol", idcol, "SQL");
            this.AddStringOutputParm("@DeleteMessage", 30);
            returnmessage = this.ExecuteStringOutputCommand("dbo.wsgsp_deletetrackingstep", CommandType.StoredProcedure);
            return returnmessage;
        }

        public void DeleteSubscription(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            try
            {
                ExecuteCommand("dbo.wsgsp_deletesubscription", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Subscription Deleted");
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public void SaveNewSubscriber(int stepid, int stepuserid)
        {
            EstablishBlankDataTableRow(trackingds.stepsubscriber);
            trackingds.stepsubscriber[0].stepid = stepid;
            trackingds.stepsubscriber[0].stepuserid = stepuserid;
            GenerateAppTableRowSave(trackingds.stepsubscriber[0]);
        }

        public void InitializeStep()
        {
            EstablishBlankDataTableRow(trackingds.step);
            //Initialize the form's fields
            trackingds.step[0].location = "NY";
            trackingds.step[0].alertinterval = 0;
            trackingds.step[0].internalmail = "N";
            trackingds.step[0].maxcode = "N";
            trackingds.step[0].sendftp = true;
            trackingds.step[0].soclose = "N";
            trackingds.step[0].printinvoice = "N";
            trackingds.step[0].printworkorder = "N";
            trackingds.step[0].printpacklist = "N";
            trackingds.step[0].printidentitylabel = "N";
            trackingds.step[0].printsewnonlabel = "N";
        }

        public bool SaveTrackingStep()
        {
            bool OkToSave = true;
            if (trackingds.step[0].idcol < 1)
            {
                // test code
                this.ClearParameters();
                testtrackingds.step.Rows.Clear();
                this.AddParms("@code", trackingds.step[0].code, "SQL");
                this.FillData(testtrackingds, "step", "wsgsp_gettrackingstepbycode", CommandType.StoredProcedure);
                if (testtrackingds.step.Rows.Count > 0)
                {
                    wsgUtilities.wsgNotice("The code already exists");
                    OkToSave = false;
                }
            }
            if (OkToSave)
            {
                GenerateAppTableRowSave(trackingds.step[0]);
            }
            return OkToSave;
        }

        public void GetSubscribers(int stepid)
        {
            this.ClearParameters();
            trackingds.view_stepsubscriberdata.Rows.Clear();
            this.AddParms("@stepid", stepid, "SQL");
            this.FillData(trackingds, "view_stepsubscriberdata", "wsgsp_getsubscribers", CommandType.StoredProcedure);
        }

        public void GetSingleTrackingStep(int idcol)
        {
            this.ClearParameters();
            trackingds.step.Rows.Clear();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(trackingds, "step", "wsgsp_getsingletrackingstep", CommandType.StoredProcedure);
        }

        public void GetTrackingCodes()
        {
            this.ClearParameters();
            trackingds.step.Rows.Clear();
            this.FillData(trackingds, "step", "wsgsp_gettrackingcodedata", CommandType.StoredProcedure);
        }

        #endregion
        #region Route Data

        public void GetAllCustomerRoutes(string custno)
        {
            string commandString = "SELECT  * from view_expandedroutedata where custstep = 'Y' AND idcol  IN (SELECT routestepid FROM custroutestep WHERE custno = @custno)";
            this.ClearParameters();
            trackingds.view_expandedroutedata.Rows.Clear();
            this.AddParms("@custno", custno, "SQL");
            this.FillData(trackingds, "view_expandedroutedata", commandString, CommandType.Text);
        }

        public void GetCustomerRouteSteps(string custno, int route)
        {
            string commandString = "SELECT  * from view_routedata where custstep = 'Y' ";
            commandString += " AND route = @route  AND idcol  IN (SELECT routestepid FROM custroutestep WHERE custno = @custno )";

            this.ClearParameters();
            trackingds.view_routedata.Rows.Clear();
            this.AddParms("@custno", custno, "SQL");
            this.AddParms("@route", route, "SQL");
            this.FillData(trackingds, "view_routedata", commandString, CommandType.Text);
        }

        public void GetRouteSteps(int route)
        {
            this.ClearParameters();
            trackingds.view_routedata.Rows.Clear();
            this.AddParms("@route", route, "SQL");
            this.FillData(trackingds, "view_routedata", "wsgsp_getroutedata", CommandType.StoredProcedure);
        }

        public void DeleteRouteStep(int route, int stepid)
        {
            this.ClearParameters();
            this.AddParms("@route", route, "SQL");
            this.AddParms("@stepid", stepid, "SQL");
            ExecuteCommand("dbo.wsgsp_deleteroutestep", CommandType.StoredProcedure);
        }

        public string InsertRouteStep(int route, int stepid)
        {
            this.ClearParameters();
            this.AddParms("@route", route, "SQL");
            this.AddParms("@stepid", stepid, "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@InsertMessage", 30);
            return this.ExecuteStringOutputCommand("dbo.wsgsp_insertroutestep", CommandType.StoredProcedure);
        }

        public void GetNonCustomRouteData(int CurrentRouteId)
        {
            string commandtext = " SELECT * FROM view_routedata WHERE route = @route AND custstep <> 'Y' ORDER BY code";
            this.ClearParameters();
            trackingds.view_routedata.Rows.Clear();
            this.AddParms("@route", CurrentRouteId, "SQL");
            this.FillData(trackingds, "view_routedata", commandtext, CommandType.Text);
        }

        public void CheckInvoiceSteps(string sono)
        {
            // Remove must be invoiced steps in the SO has not been closed.
            for (int i = 0; i < trackingds.view_routedata.Rows.Count; i++)
            {
                if (trackingds.view_routedata[i].mustbeinvoiced == "Y")
                {
                    if (GetSoStatus(sono) == "C")
                    {
                        continue;
                    }
                    else
                    {
                        trackingds.view_routedata.Rows[i].Delete();
                    }
                }
            }
        }

        public void GetRouteData(int CurrentRouteId)
        {
            this.ClearParameters();
            trackingds.view_routedata.Rows.Clear();
            this.AddParms("@route", CurrentRouteId, "SQL");
            this.FillData(trackingds, "view_routedata", "sp_getroutedata", CommandType.StoredProcedure);
        }

        public void ToggleCustStep(int idcol)
        {
            string commandtext = "SELECT * FROM view_routedata WHERE idcol = @idcol";
            string custstep = "";
            this.ClearParameters();
            trackingds.view_routedata.Rows.Clear();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(trackingds, "view_routedata", commandtext, CommandType.Text);
            if (trackingds.view_routedata[0].custstep == "Y")
            {
                custstep = "N";
            }
            else
            {
                custstep = "Y";
            }
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@custstep", custstep, "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            commandtext = "UPDATE route SET custstep = @custstep, lckdate = GETDATE(), lckuser = @userid  WHERE idcol = @idcol";
            ExecuteCommand(commandtext, CommandType.Text);
        }

        #endregion
    }

    #endregion
    #region Price Maintenance

    public class PriceMaintenance : WSGDataAccess
    {
        public price prds { get; set; }
        public string currentcustno { get; set; }
        public DataSet ds { get; set; }

        public PriceMaintenance(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            prds = new price();
            SetIdcol(prds.quprsdetail.idcolColumn);
            SetIdcol(prds.quprshead.idcolColumn);
            SetIdcol(prds.quprslocator.idcolColumn);
        }

        public void EstablishBlankPriceLocatorData()
        {
            prds.quprslocator.Rows.Clear();
            prds.quprslocator.Rows.Add();
            InitializeDataTable(prds.quprslocator, 0);
            prds.quprslocator[0].prcfact = 0.00M;
            prds.quprslocator[0].pschedid = 0;
            prds.quprslocator[0].item = "";
            prds.quprslocator[0].spacingid = 0;
        }

        public void ClearQuprsLocatorData()
        {
            ClearDataTable(prds.quprslocator);
            prds.quprslocator.Clear();
        }

        public void ClearPriceDetailData()
        {
            ClearDataTable(prds.quprsdetail);
            prds.quprsdetail.Clear();
        }

        public void EstablishBlankPriceDetailData(int pschedid)
        {
            EstablishBlankDataTableRow(prds.quprsdetail);
            prds.quprsdetail[0].pschedid = pschedid;
        }

        public void SavePriceDetailData(int idcol, int pschedid, string CurrentState)
        {
            GenerateAppTableRowSave(prds.quprsdetail[0]);
        }

        public string LockPriceDetail(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "quprsdetail", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void GetPriceLocatorData()
        {
            prds.view_prslocatordata.Clear();
            this.ClearParameters();
            this.FillData(prds, "view_prslocatordata", "wsgsp_getprslocatordata", CommandType.StoredProcedure);
        }

        public void SavePriceLocatorData(int idcol, string CurrentState)
        {
            GenerateAppTableRowSave(prds.quprslocator[0]);
        }

        public bool ValidatePriceLocatorData()
        {
            string errormessage = "";
            if (prds.quprslocator[0].item.TrimEnd() == "")
            {
                errormessage += "Item cannot be blank" + Environment.NewLine;
            }
            if (prds.quprslocator[0].pschedid == 0)
            {
                errormessage += "Price Schedule cannot be zero" + Environment.NewLine;
            }
            if (prds.quprslocator[0].spacingid == 0)
            {
                errormessage += "Spacing cannot be zero" + Environment.NewLine;
            }

            if (errormessage == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(errormessage);
                return false;
            }
        }

        public void FindPriceLocatorData(int item, int spacind)
        {
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.AddParms("@spacing", item, "SQL");
            this.FillData(prds, "quprslocator", "wsgsp_findprslocatordata", CommandType.StoredProcedure);
        }

        public void getSinglePriceLocatorData(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(prds, "quprslocator", "wsgsp_getsinglequprslocatordata", CommandType.StoredProcedure);
        }

        public void UnlockQuprsLocator(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "quprslocator", "SQL");
            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public void DeleteQuprsLocatorData(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", prds.quprslocator[0].idcol, "SQL");
            try
            {
                ExecuteCommand("wsgsp_deletequprslocator", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public string LockQuPrsLocator(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "quprslocator", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void GetPriceScheduleDetailData(int pschedid)
        {
            prds.view_quprsdetaildata.Clear();
            this.ClearParameters();
            this.AddParms("@pschedid", pschedid, "SQL");
            this.FillData(prds, "view_quprsdetaildata", "wsgsp_getquprsdetaildata", CommandType.StoredProcedure);
        }

        public void getSinglePriceScheduleDetailData(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(prds, "quprsdetail", "wsgsp_getsinglequprsdetaildata", CommandType.StoredProcedure);
        }

        public void GetQuprsheadData()
        {
            prds.view_quprsheaddata.Clear();
            this.ClearParameters();
            this.FillData(prds, "view_quprsheaddata", "wsgsp_getquprsheaddata", CommandType.StoredProcedure);
        }

        public void getSingleQuprsheadData(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(prds, "quprshead", "wsgsp_getsinglequprsheaddata", CommandType.StoredProcedure);
        }
    } // end class PriceMaintenace

    #endregion

    #region Get Customer Class

    //CACHED 
    public class GetCustomerMethods : WSGDataAccess
    {
        private CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        public customer ards { get; set; }
        public int SelectedCustid { get; set; }
        public string SelectedCustno { get; set; }
        public bool InsertingCustomer { get; set; }
        private FrmGetCustomer ParentForm = new FrmGetCustomer();
        public customer custsearch { get; set; }

        private static BindingSource bindingCustomerData = new BindingSource();
        private static ObjectCacheWithParams dataCache = new ObjectCacheWithParams(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_CustomerSearch"]));

        public GetCustomerMethods(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            custsearch = new customer();
            SelectedCustid = 0;
            SelectedCustno = "";
            InsertingCustomer = false;
            ards = new customer();
            SetEvents();
            SetBindings();
            ParentForm.dataGridViewCustomers.AutoGenerateColumns = false;
            ParentForm.dataGridViewCustomers.RowsDefaultCellStyle.BackColor = Color.LightGray;
            ParentForm.dataGridViewCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            if (dataCache != null && !dataCache.IsInvalid)
                ParentForm.dataGridViewCustomers.DataSource = bindingCustomerData;

            ParentForm.textBoxCustno.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["cust"];
            ParentForm.textBoxCompany.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["company"];
            ParentForm.textBoxState.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["state"];
            ParentForm.textBoxZip.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["zip"];
            ParentForm.textBoxPhone.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["phone"];

            InsertingCustomer = false;
            ParentForm.buttonAccept.Enabled = false;
            ParentForm.ShowDialog();
        }

        public void SetEvents()
        {
            ParentForm.buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
            ParentForm.buttonAccept.Click += new System.EventHandler(buttonAccept_Click);
            ParentForm.dataGridViewCustomers.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomers_CellContentDoubleClick);
            ParentForm.dataGridViewCustomers.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridViewCustomers_KeyDown);
            ParentForm.buttonSearch.Click += new System.EventHandler(buttonSearch_Click);
            ParentForm.AcceptButton = ParentForm.buttonSearch;
            ParentForm.Load += ParentForm_Load;
        }

        private void ParentForm_Load(object sender, EventArgs e)
        {
            ParentForm.labelDataCount.Visible = false;

            if (dataCache != null && !dataCache.IsInvalid)
            {
                ParentForm.labelDataCount.Text = customerAccess.custsearch.arcust.Count > 0 ? customerAccess.custsearch.arcust.Count.ToString() + " records found" : "No records found";
                ParentForm.labelDataCount.Visible = true;
                ParentForm.labelDataCount.Update();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SelectedCustid = 0;
            ParentForm.Close();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ParentForm.labelDataCount.Visible = true;
            ParentForm.labelDataCount.Text = "Searching...";
            ParentForm.labelDataCount.Update();

            customerAccess.GetCustomerSelectorInfo(ParentForm.textBoxCustno.Text, ParentForm.textBoxCompany.Text,
            ParentForm.textBoxState.Text, ParentForm.textBoxZip.Text, ParentForm.textBoxPhone.Text);
            bindingCustomerData.DataSource = customerAccess.custsearch.arcust;
            ParentForm.dataGridViewCustomers.DataSource = bindingCustomerData;

            dataCache.Refresh(bindingCustomerData);

            dataCache.SearchParams["cust"] = ParentForm.textBoxCustno.Text;
            dataCache.SearchParams["company"] = ParentForm.textBoxCompany.Text;
            dataCache.SearchParams["state"] = ParentForm.textBoxState.Text;
            dataCache.SearchParams["zip"] = ParentForm.textBoxZip.Text;
            dataCache.SearchParams["phone"] = ParentForm.textBoxPhone.Text;

            ParentForm.AcceptButton = ParentForm.buttonAccept;

            ParentForm.labelDataCount.Text = customerAccess.custsearch.arcust.Count > 0 ? customerAccess.custsearch.arcust.Count.ToString() + " records found" : "No records found";
        }

        public void ProcessSelection()
        {
            customerAccess.getSingleCustomerData(SelectedCustid);
            SelectedCustno = customerAccess.ards.arcust[0].custno;
            // Establish Ship to Table
            customerAccess.ards.aracadr.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@custid", SelectedCustid, "SQL");
            this.FillData(customerAccess.ards, "aracadr", "wsgsp_getdefaultshipto", CommandType.StoredProcedure);
            customerAccess.ProcessAlerts(customerAccess.ards.arcust[0].custno);
            ParentForm.buttonAccept.Enabled = true;
        }

        private void dataGridViewCustomers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedCustid = customerAccess.CaptureIdCol(ParentForm.dataGridViewCustomers);
            ProcessSelection();
        }

        private void dataGridViewCustomers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SelectedCustid = customerAccess.CaptureIdCol(ParentForm.dataGridViewCustomers);
                if (SelectedCustid >= 0)
                    ProcessSelection();
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            InsertingCustomer = true;
            ParentForm.Close();
        }

        public void SetBindings()
        {
            // Customer
            ParentForm.textBoxBillEmail.DataBindings.Add("Text", customerAccess.ards.arcust, "email");
            ParentForm.textBoxBillPhone.DataBindings.Add("Text", customerAccess.ards.arcust, "phone");
            ParentForm.textBoxBillFaxNo.DataBindings.Add("Text", customerAccess.ards.arcust, "faxno");
            ParentForm.textBoxBillCompany.DataBindings.Add("Text", customerAccess.ards.arcust, "company");
            ParentForm.textBoxBillAddress1.DataBindings.Add("Text", customerAccess.ards.arcust, "address1");
            ParentForm.textBoxBillAddress2.DataBindings.Add("Text", customerAccess.ards.arcust, "address2");
            ParentForm.textBoxBillCity.DataBindings.Add("Text", customerAccess.ards.arcust, "city");
            ParentForm.textBoxBillState.DataBindings.Add("Text", customerAccess.ards.arcust, "state");
            ParentForm.textBoxBillZip.DataBindings.Add("Text", customerAccess.ards.arcust, "zip");

            // Ship To
            ParentForm.textBoxShiptocompany.DataBindings.Add("Text", customerAccess.ards.aracadr, "company");
            ParentForm.textBoxShiptoaddress1.DataBindings.Add("Text", customerAccess.ards.aracadr, "address1");
            ParentForm.textBoxShiptoaddress2.DataBindings.Add("Text", customerAccess.ards.aracadr, "address2");
            ParentForm.textBoxShiptocity.DataBindings.Add("Text", customerAccess.ards.aracadr, "city");
            ParentForm.textBoxShiptostate.DataBindings.Add("Text", customerAccess.ards.aracadr, "state");
            ParentForm.textBoxShiptozip.DataBindings.Add("Text", customerAccess.ards.aracadr, "zip");
        }
    }

    #endregion

    #region Customer Access Class

    public class CustomerAccess : WSGDataAccess
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        public customer ards { get; set; }
        public customer custsearch { get; set; }
        public string currentcustno { get; set; }
        public DataSet ds { get; set; }

        public CustomerAccess(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            custsearch = new customer();
            ards = new customer();
            SetIdcol(ards.arcust.idcolColumn);
            SetIdcol(ards.aracadr.idcolColumn);
            SetIdcol(ards.custalerts.idcolColumn);
        }

        #region Public Methods

        public void ProcessAlerts(string custno)
        {
            string commandString = "SELECT * FROM view_expandedcustalerts WHERE custno = @custno ORDER BY refdescrip ";
            ards.view_expandedcustalerts.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@custno", custno, "SQL");
            this.FillData(ards, "view_expandedcustalerts", commandString, CommandType.Text);
            for (int i = 0; i <= ards.view_expandedcustalerts.Rows.Count - 1; i++)
            {
                wsgUtilities.wsgNotice(ards.view_expandedcustalerts[i].refdescrip);
            }
        }

        public void getSingleCustomerData(int Custid)
        {
            ards.arcust.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", Custid, "SQL");
            this.FillData(ards, "arcust", "wsgsp_getcustomerdata", CommandType.StoredProcedure);
            currentcustno = ards.arcust[0].custno;
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

        #region customer selector

        public void GetCustomerSelectorInfo(string Custno, string Company, string State, string Zip, string Phone)
        {
            custsearch.arcust.Rows.Clear();
            this.ClearParameters();
            bool firstquery = true;
            string SQLCommandString = "SELECT custno, company, address1, city, state, zip, phone, idcol FROM arcust ";

            if (Custno.Trim() != "")
            {
                SQLCommandString += "WHERE custno LIKE @custno ";
                this.AddStringParm("@custno", SqlDbType.NVarChar, 6, Custno.Trim() + "%");
                firstquery = false;
            }

            if (Company.Trim() != "")
            {
                if (firstquery == true)
                {
                    SQLCommandString += " WHERE ";
                }
                else
                {
                    SQLCommandString += " AND ";
                }
                SQLCommandString += " company LIKE @company ";
                this.AddStringParm("@company", SqlDbType.NVarChar, 35, Company.Trim() + "%");
                firstquery = false;
            }

            if (State.Trim() != "")
            {
                if (firstquery == true)
                {
                    SQLCommandString += " WHERE ";
                }
                else
                {
                    SQLCommandString += " AND ";
                }
                SQLCommandString += " state LIKE @state ";
                this.AddStringParm("@state", SqlDbType.NVarChar, 10, State.Trim() + "%");
                firstquery = false;
            }

            if (Zip.Trim() != "")
            {
                if (firstquery == true)
                {
                    SQLCommandString += " WHERE ";
                }
                else
                {
                    SQLCommandString += " AND ";
                }
                SQLCommandString += " zip LIKE @zip ";
                this.AddStringParm("@zip", SqlDbType.NVarChar, 10, Zip.Trim() + "%");
                firstquery = false;
            }

            if (Phone.Trim() != "")
            {
                if (firstquery == true)
                {
                    SQLCommandString += " WHERE ";
                }
                else
                {
                    SQLCommandString += " AND ";
                }
                SQLCommandString += " phone LIKE @phone ";
                this.AddStringParm("@phone", SqlDbType.NVarChar, 20, Phone.Trim() + "%");
                firstquery = false;
            }

            SQLCommandString += " ORDER BY custno ";

            this.FillData(custsearch, "arcust", SQLCommandString, CommandType.Text);
        }

        #endregion

        #region Ship To Methods

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

        public void getCustomerShipToData(int Custid)
        {
            ards.view_customershiptolist.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@custid", Custid, "SQL");
            this.FillData(ards, "view_customershiptolist", "wsgsp_getcustomershiptodata", CommandType.StoredProcedure);
        }

        public void SaveShipToData(int idcol, string CurrentState, string Custno)
        {
            if ((ards.aracadr[0].idcol < 0 && CheckShipTo() == "OK") ||
                ards.aracadr[0].idcol > 0)
            {
                GenerateAppTableRowSave(ards.aracadr[0]);
            }
            else
            {
                wsgUtilities.wsgNotice("That ship-to code already exists.");
            }
        }

        public void ClearShipToData(int Custid, string Custno)
        {
            ards.aracadr.Rows.Clear();
            ards.aracadr.Rows.Add();
            InitializeDataTable(ards.aracadr, 0);
            //  MessageBox.Show(Custno);
            ards.aracadr[0].custno = Custno;
            ards.aracadr[0].defaship = "N";
            ards.aracadr[0].custid = Custid;
            ards.aracadr[0].country = "USA";
        }

        public string CheckShipTo()
        {
            // Prevent duplicate ship to number
            this.ClearParameters();
            this.AddParms("@cshipno", ards.aracadr[0].cshipno, "SQL");
            this.AddParms("@custid", ards.aracadr[0].custid, "SQL");
            this.AddStringOutputParm("@cshipnomessage", 40);
            return this.ExecuteStringOutputCommand("wsgsp_checkcshipno", CommandType.StoredProcedure).Trim();
        }

        public int InsertShipToData(string Custno)
        {
            this.ClearParameters();
            // Prevent duplicate ship to number
            this.AddParms("@cshipno", ards.aracadr.Rows[0].Field<string>("cshipno"), "SQL");
            this.AddParms("@custid", ards.aracadr.Rows[0].Field<int>("custid"), "SQL");
            this.AddStringOutputParm("@cshipnomessage", 40);
            string result = this.ExecuteStringOutputCommand("wsgsp_checkcshipno", CommandType.StoredProcedure).Trim();
            if (result == "OK")
            {
                this.ClearParameters();
                this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
                this.AddParms("@newshiptoid", 0, ParameterDirection.Output, "SQL");
                try
                {
                    int newshiptoid = ExecuteIntOutputCommand("wsgsp_insertshiptodata",
                    CommandType.StoredProcedure);
                    UpdateShipToData(newshiptoid);
                    this.ClearShipToData(ards.aracadr.Rows[0].Field<int>("custid"), Custno);
                    return newshiptoid;
                }
                catch (SqlException ex)
                {
                    SqlExceptionHandler.HandleException(ex);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show(result);
                return -1;
            }
        } // insertshiptodata

        public string LockShipTo(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "aracadr", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);
            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void UnlockShipTo(int idcol)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", "aracadr", "SQL");

            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        # region Update ShipTo Data

        public void UpdateShipToData(int idcol)
        {
            this.ClearParameters();
            for (int i = 0; i < ards.aracadr.Columns.Count; i++)
            {
                if (ards.aracadr.Columns[i].ColumnName == "idcol")
                    continue;
                if (ards.aracadr.Columns[i].ColumnName == "adddate")
                    continue;
                if (ards.aracadr.Columns[i].ColumnName == "lckdate")
                    continue;
                if (ards.aracadr.Columns[i].ColumnName == "adduser")
                    continue;
                if (ards.aracadr.Columns[i].ColumnName == "lckuser")
                    continue;
                if (ards.aracadr.Columns[i].ColumnName == "lckstat")
                    continue;
                string columnname = ards.aracadr.Columns[i].ColumnName;
                if (ards.aracadr.Columns[i].DataType == typeof(System.String))
                {
                    this.AddParms("@" + columnname, ards.aracadr.Rows[0].Field<string>(columnname), "SQL");
                    continue;
                }
                if (ards.aracadr.Columns[i].DataType == typeof(System.Decimal))
                {
                    this.AddParms("@" + columnname, ards.aracadr.Rows[0].Field<Decimal>(columnname), "SQL");
                    continue;
                }
                if (ards.aracadr.Columns[i].DataType == typeof(System.DateTime))
                {
                    this.AddParms("@" + columnname, ards.aracadr.Rows[0].Field<DateTime>(columnname), "SQL");
                    continue;
                }

                if (ards.aracadr.Columns[i].DataType == typeof(System.Boolean))
                {
                    this.AddParms("@" + columnname, ards.aracadr.Rows[0].Field<Boolean>(columnname), "SQL");
                    continue;
                }
                if (ards.aracadr.Columns[i].DataType == typeof(System.Int16))
                {
                    this.AddParms("@" + columnname, ards.aracadr.Rows[0].Field<Int16>(columnname), "SQL");
                    continue;
                }
                if (ards.aracadr.Columns[i].DataType == typeof(System.Int32))
                {
                    this.AddParms("@" + columnname, ards.aracadr.Rows[0].Field<Int32>(columnname), "SQL");
                    continue;
                }
            }
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");

            try
            {
                ExecuteCommand("wsgsp_updateshiptodata", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        #endregion

        #endregion

        #endregion
    }   // class

    #endregion
    #region feet and inches

    public class feetandinches
    {
        private decimal feet;

        public decimal Feet
        {
            get
            {
                return feet;
            }
            set
            {
                feet = value;
            }
        } // feet

        private decimal inches;

        public decimal Inches
        {
            get
            {
                return inches;
            }
            set
            {
                inches = value;
            }
        } // inches
    }

    #endregion Feet and inches class

    public class GetSoMethods : WSGDataAccess
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Reference Data");
        public FrmGetSono frmGetsono = new FrmGetSono();
        public string returnsono = "";
        public bool wascancelled = false;
        public quote quoteds = new quote();

        public GetSoMethods(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            returnsono = "";
            SetEvents();
        }

        public string GetSono()
        {
            frmGetsono.textBoxSono.Text = "";
            frmGetsono.ShowDialog();
            return returnsono;
        }

        public void SetEvents()
        {
            frmGetsono.buttonGetSO.Click += buttonGetSO_Click;
            frmGetsono.textBoxSono.KeyDown += textBoxSoNo_KeyDown;
            frmGetsono.buttonCancel.Click += buttonCancel_Click;
            frmGetsono.FormClosing += parentFormClosing;
        }

        private void buttonGetSO_Click(object sender, EventArgs e)
        {
            FrmSOSearch frmSoSearch = new FrmSOSearch(frmGetsono.textBoxSono.Text);
            frmSoSearch.ShowDialog();
            if (frmSoSearch.SelectedSono.TrimEnd() != "")
            {
                returnsono = frmSoSearch.SelectedSono.TrimEnd();
                quoteds.somast.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@sono", frmSoSearch.SelectedSono.TrimEnd().TrimStart().PadLeft(10), "SQL");
                this.FillData(quoteds, "somast", "wsgsp_getsomastbysono", CommandType.StoredProcedure);
                frmGetsono.Close();
            }
        }

        public void textBoxSoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                quoteds.somast.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@sono", frmGetsono.textBoxSono.Text.TrimEnd().TrimStart().PadLeft(10), "SQL");
                this.FillData(quoteds, "somast", "wsgsp_getsomastbysono", CommandType.StoredProcedure);
                if (quoteds.somast.Rows.Count == 0)
                {
                    wsgUtilities.wsgNotice("SO number not found");
                }
                else
                {
                    returnsono = frmGetsono.textBoxSono.Text.TrimEnd().TrimStart().PadLeft(10);
                    frmGetsono.Close();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            wascancelled = true;
            returnsono = "";
            frmGetsono.Close();
        }

        private void parentFormClosing(object sender, FormClosingEventArgs e)
        {
            //wascancelled = true;
        }
    }

    public class WarrantySynchronization : WSGDataAccess
    {
        public quote quoteds = new quote();

        public WarrantySynchronization(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            SetIdcol(quoteds.warranty.idcolColumn);
        }

        public void SynchronizeWarranty(string sono)
        {
            string commandstring = "";
            quoteds.somast.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(quoteds, "somast", "wsgsp_getsomastbysono", CommandType.StoredProcedure);
            if (quoteds.somast.Rows.Count > 0)
            {
                if (quoteds.somast[0].sostat != "V" && quoteds.somast[0].invno.TrimEnd() != "")
                {
                    // Check for cover product
                    commandstring = "SELECT * FROM socover WHERE covertype = 'C' AND  product LIKE '%Cover' AND sono = @sono";
                    quoteds.socover.Rows.Clear();
                    this.ClearParameters();
                    this.AddParms("@sono", sono, "SQL");
                    this.FillData(quoteds, "socover", commandstring, CommandType.Text);
                    if (quoteds.socover.Rows.Count > 0)
                    {
                        commandstring = "SELECT * FROM warranty WHERE sono = @sono";
                        quoteds.warranty.Rows.Clear();
                        this.ClearParameters();
                        this.AddParms("@sono", sono, "SQL");
                        this.FillData(quoteds, "warranty", commandstring, CommandType.Text);
                        if (quoteds.warranty.Rows.Count < 1)
                        {
                            EstablishBlankDataTableRow(quoteds.warranty);
                            quoteds.warranty[0].sono = quoteds.somast[0].sono;
                            quoteds.warranty[0].custno = quoteds.somast[0].custno;
                            quoteds.warranty[0].ornum = quoteds.somast[0].meycono;
                            quoteds.warranty[0].ponum = quoteds.somast[0].ponum;
                            quoteds.warranty[0].shipdate = quoteds.somast[0].invdte;
                            quoteds.warranty[0].lastupdate = DateTime.Now;
                            quoteds.warranty[0].fname = quoteds.somast[0].fname;
                            quoteds.warranty[0].lname = quoteds.somast[0].lname;
                            quoteds.warranty[0].address = quoteds.somast[0].address;
                            quoteds.warranty[0].city = quoteds.somast[0].city;
                            quoteds.warranty[0].state = quoteds.somast[0].state;
                            quoteds.warranty[0].hoemailaddr = quoteds.somast[0].hoemailaddr;
                            quoteds.warranty[0].zip = quoteds.somast[0].zip;
                            quoteds.warranty[0].poolowner = quoteds.somast[0].fname.TrimEnd() + " " + quoteds.somast[0].lname.TrimEnd();
                            quoteds.warranty[0].descrip = quoteds.socover[0].descrip;
                            commandstring = "SELECT * FROM arcust WHERE custno = @custno";
                            quoteds.arcust.Rows.Clear();
                            this.ClearParameters();
                            this.AddParms("@custno", quoteds.somast[0].custno, "SQL");
                            this.FillData(quoteds, "arcust", commandstring, CommandType.Text);
                            if (quoteds.arcust.Rows.Count > 0)
                            {
                                quoteds.warranty[0].dealer = quoteds.arcust[0].company;
                                quoteds.warranty[0].dealaddr1 = quoteds.arcust[0].address1;
                                quoteds.warranty[0].dealaddr2 = quoteds.arcust[0].address2;
                                quoteds.warranty[0].dealcity = quoteds.arcust[0].city;
                                quoteds.warranty[0].dealstate = quoteds.arcust[0].state;
                                quoteds.warranty[0].dealzip = quoteds.arcust[0].zip;
                                quoteds.warranty[0].dealindust = quoteds.arcust[0].indust;
                            }
                            // Save new record
                            GenerateAppTableRowSave(quoteds.warranty[0]);
                        } // (quoteds.warranty.Rows.Count < 1)
                    } // Cover product
                    if (quoteds.somast[0].oldplan.TrimEnd() != "")
                    {
                        // Check for old plan number that refers to another cover
                        commandstring = "SELECT * FROM warranty WHERE RTRIM(LTRIM( ornum)) = @oldplan";
                        quoteds.warranty.Rows.Clear();
                        this.ClearParameters();
                        this.AddParms("@oldplan", quoteds.somast[0].oldplan.TrimEnd().TrimStart(), "SQL");
                        this.FillData(quoteds, "warranty", commandstring, CommandType.Text);
                        if (quoteds.warranty.Rows.Count > 0)
                        {
                            // Check for cover product
                            commandstring = "SELECT * FROM socover WHERE covertype = 'C' AND  product LIKE '%Cover' AND sono = @sono";
                            quoteds.socover.Rows.Clear();
                            this.ClearParameters();
                            this.AddParms("@sono", sono, "SQL");
                            this.FillData(quoteds, "socover", commandstring, CommandType.Text);
                            if (quoteds.socover.Rows.Count > 0)
                            {
                                quoteds.warranty[0].replacedby = quoteds.somast[0].sono;
                                quoteds.warranty[0].disableprt = true;
                            }
                            else
                            {
                                // Check for Repair product
                                commandstring = "SELECT * FROM socover WHERE covertype = 'C' AND  product LIKE '%Repair' AND sono = @sono";
                                quoteds.socover.Rows.Clear();
                                this.ClearParameters();
                                this.AddParms("@sono", sono, "SQL");
                                this.FillData(quoteds, "socover", commandstring, CommandType.Text);
                                if (quoteds.socover.Rows.Count > 0)
                                {
                                    quoteds.warranty[0].repairnotes = "Repaired on SO " + quoteds.somast[0].sono;
                                }
                                else
                                {
                                    // Check for Alteration product
                                    commandstring = "SELECT * FROM socover WHERE covertype = 'C' AND  product LIKE '%Alteration' AND sono = @sono";
                                    quoteds.socover.Rows.Clear();
                                    this.ClearParameters();
                                    this.AddParms("@sono", sono, "SQL");
                                    this.FillData(quoteds, "socover", commandstring, CommandType.Text);
                                    if (quoteds.socover.Rows.Count > 0)
                                    {
                                        quoteds.warranty[0].altnotes = "Alterations on SO " + quoteds.somast[0].sono;
                                    }
                                }
                            }
                            // Save original warranty record
                            GenerateAppTableRowSave(quoteds.warranty[0]);
                        } // Previous warranty found
                    } // quoteds.somast[0].oldplan.TrimEnd() != "")
                } // quoteds.somast[0].sostat != "V")
            } // (quoteds.somast.Rows.Count > 0)
        } //SynchronizeWarranty
    } // class

    public class MiscellaneousDataMethods : WSGDataAccess
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Miscellaneous Data");
        public quote quoteds = new quote();
        public quote quotetempds = new quote();
        public ShipDateInformation shipinfo = new ShipDateInformation();
        public system systemds = new system();
        public inventoryds invds = new inventoryds();
        public quote somastds = new quote();
        public reference refds = new reference();
        public GetSoMethods getsomethods = new GetSoMethods("SQL", "SQLConnString");
        public reference searchrefds = new reference();
        public DateTime SelectedStartDate = new DateTime();
        public DateTime SelectedEndDate = new DateTime();
        public system listsystemds = new system();
        public system testsystemds = new system();

        public MiscellaneousDataMethods(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            SetIdcol(systemds.appuser.idcolColumn);
        }

        public string GetSOVersionCover(string sono, string version)
        {
            bool singleCover;
            return this.GetSOVersionCover(sono, version, out singleCover);
        }

        public string GetSOVersionCover(string sono, string version, out bool singleCover)
        {
            singleCover = false;
            string cover = "";
            int rowcount = 1;
            int rrow = 0;
            somastds.view_coverdata.Rows.Clear();
            ClearParameters();
            AddParms("@sono", sono, "SQL");
            AddParms("@version", version, "SQL");
            FillData(somastds, "view_coverdata", "wsgsp_getsoversioncovers", CommandType.StoredProcedure);
            while (rowcount <= somastds.view_coverdata.Rows.Count)
            {
                if (somastds.view_coverdata[rrow].covertype != "C ")
                {
                    somastds.view_coverdata[rrow].Delete();
                }
                rowcount++;
                rrow++;
            }
            somastds.view_coverdata.AcceptChanges();
            if (somastds.view_coverdata.Rows.Count > 0)
            {
                if (somastds.view_coverdata.Rows.Count > 1)
                {
                    FrmCoverSelector frmCoverSelector = new FrmCoverSelector();
                    BindingSource bindingCovers = new BindingSource();
                    bindingCovers.DataSource = somastds.view_coverdata;
                    frmCoverSelector.dataGridViewCoverSelector.DataSource = bindingCovers;
                    frmCoverSelector.labelDataGridCount.Text = $"Returned {bindingCovers.Count} items";
                    frmCoverSelector.ShowDialog();
                    cover = frmCoverSelector.SelectedCover;
                }
                else
                {
                    cover = somastds.view_coverdata[0].cover;
                    singleCover = true;
                }
            }
            else
            {
                // No cover found. Return blank
                cover = "";
            }

            return cover;
        }

        public bool GetTwoDates(string labeltext)
        {
            FrmGetTwoDates frmGetTwoDates = new FrmGetTwoDates();
            frmGetTwoDates.LabelDateText.Text = labeltext;
            frmGetTwoDates.ShowDialog();
            SelectedStartDate = frmGetTwoDates.SelectedStartDate.Date;
            SelectedEndDate = frmGetTwoDates.SelectedEndDate.Date;
            return frmGetTwoDates.DateOk;
        }

        public string GetSoCustno(string sono)
        {
            string custno = "";
            string commandstring = "SELECT * FROM somast WHERE sono = @sono";
            quoteds.somast.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            FillData(quoteds, "somast", commandstring, CommandType.Text);
            if (quoteds.somast.Rows.Count > 0)
            {
                custno = quoteds.somast[0].custno;
            }
            return custno;
        }

        #region User Processing

        public bool SaveAppuser()
        {
            bool OkToSave = true;
            if (systemds.appuser[0].idcol < 1)
            {
                // test code
                this.ClearParameters();
                testsystemds.appuser.Rows.Clear();
                this.AddParms("@userid", systemds.appuser[0].userid, "SQL");
                this.FillData(testsystemds, "appuser", "wsgsp_getappuserbyuserid", CommandType.StoredProcedure);
                if (testsystemds.appuser.Rows.Count > 0)
                {
                    wsgUtilities.wsgNotice("The user id already exists");
                    OkToSave = false;
                }
            }
            if (OkToSave)
            {
                GenerateAppTableRowSave(systemds.appuser[0]);
            }
            return OkToSave;
        }

        public void InitializeAppuser()
        {
            EstablishBlankDataTableRow(systemds.appuser);
        }

        public string GetUserId(bool showinactive)
        {
            string userid = "";
            FrmGetUser frmGetUser = new FrmGetUser();
            frmGetUser.ShowInactive = showinactive;
            frmGetUser.ShowDialog();
            if (frmGetUser.SelectedUserId != 0)
            {
                GetSingleAppUser(frmGetUser.SelectedUserId);
                userid = systemds.appuser[0].userid;
            }
            return userid;
        }

        public void GetAppUsers(bool IncludeInactive)
        {
            string commandstring = "";
            if (IncludeInactive)
            {
                commandstring = " SELECT * from appuser ORDER BY userid ";
            }
            else
            {
                commandstring = " SELECT * from appuser WHERE userstatus <> 'I' ORDER BY userid ";
            }
            this.ClearParameters();
            listsystemds.appuser.Rows.Clear();
            this.FillData(listsystemds, "appuser", commandstring, CommandType.Text);
        }

        public void GetSingleAppUser(int idcol)
        {
            this.ClearParameters();
            systemds.appuser.Rows.Clear();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(systemds, "appuser", "wsgsp_getsingleappuser", CommandType.StoredProcedure);
        }

        #endregion

        public void GetShipDateInformation(string sono)
        {
            shipinfo.listprice = 0;
            shipinfo.priorproductionload = 0;
            shipinfo.productionload = 0;
            shipinfo.productionunits = 0;
            shipinfo.buffer = 0;
            shipinfo.addbuffer = 0;
            shipinfo.sono = "";
            string commandtext = "";
            shipinfo.calcok = true;

            if (sono.TrimEnd() == "")
            {
                shipinfo.sono = getsomethods.GetSono();
                if (shipinfo.sono.TrimEnd() == "")
                {
                    shipinfo.calcok = false;
                }
            }
            else
            {
                shipinfo.sono = sono;
            }
            if (shipinfo.calcok)
            {
                if (IsOrderCoverStock(sono))
                {
                    shipinfo.customstock = "S";
                }
                else
                {
                    shipinfo.customstock = "C";
                }
            }

            if (shipinfo.calcok)
            {
                shipinfo.shipdate = new DateTime(2001, 1, 1);
                shipinfo.productionunits = 0;
                quoteds.somast.Rows.Clear();
                commandtext = "SELECT * FROM somast WHERE sono = @sono";
                ClearParameters();
                this.AddParms("@sono", shipinfo.sono, "SQL");
                FillData(quoteds, "somast", commandtext, CommandType.Text);
                if (quoteds.somast.Rows.Count > 0)
                {
                    shipinfo.sodate = quoteds.somast[0].sodate;
                }
                else
                {
                    shipinfo.calcok = false;
                }
            }

            if (shipinfo.calcok)
            {
                refds.productionunitschedule.Rows.Clear();
                commandtext = "SELECT TOP 1 * FROM productionunitschedule WHERE effectivedate <= @sodate ORDER BY effectivedate DESC";
                ClearParameters();
                this.AddParms("@sodate", shipinfo.sodate, "SQL");
                FillData(refds, "productionunitschedule", commandtext, CommandType.Text);
                if (refds.productionunitschedule.Rows.Count < 1)
                {
                    wsgUtilities.wsgNotice("There is no production unit schedule for this date. Ship date cannot be calculated.");
                    shipinfo.calcok = false;
                }
            }
            if (shipinfo.calcok)
            {
                refds.capacitycalendar.Rows.Clear();
                commandtext = "SELECT * FROM capacitycalendar WHERE proddate = @sodate";
                ClearParameters();
                this.AddParms("@sodate", shipinfo.sodate.Date, "SQL");
                FillData(refds, "capacitycalendar", commandtext, CommandType.Text);
                if (refds.capacitycalendar.Rows.Count < 1)
                {
                    wsgUtilities.wsgNotice("There is no capacity calendar for " + String.Format("{0:MM/dd/yyyy}", shipinfo.sodate) + ". Ship date cannot be calculated.");
                    shipinfo.calcok = false;
                }
                else
                {
                    if (shipinfo.customstock == "C")
                    {
                        shipinfo.buffer = refds.capacitycalendar[0].custombuffer;
                    }
                    else
                    {
                        shipinfo.buffer = refds.capacitycalendar[0].stockbuffer;
                    }
                }
            }
            if (shipinfo.calcok)
            {
                if (shipinfo.customstock == "S")
                {
                    shipinfo.stockcoveronhand = GetStockCoverAvailability(quoteds.somast[0].sono, false, quoteds.somast[0].defloc) > 0;
                    if (shipinfo.stockcoveronhand)
                    {
                        shipinfo.productionunits = 0;
                    }
                    else
                    {
                        GetListPrice();
                        if (shipinfo.listprice != 0)
                        {
                            GetSoProductionUnits();
                        }
                        else
                        {
                            shipinfo.calcok = false;
                        }
                    }
                }
                else
                {
                    GetListPrice();
                    if (shipinfo.listprice != 0)
                    {
                        GetSoProductionUnits();
                    }
                    else
                    {
                        shipinfo.calcok = false;
                    }
                }
            } // shipinfo.calcok
            if (shipinfo.calcok)
            {
                CalculateShipDate();
            }
            if (shipinfo.calcok)
            {
                if (sono.TrimEnd() == "" || ConfigurationManager.AppSettings["ShowShipInfo"] == "True")
                {
                    MessageBox.Show("SO Date: " + String.Format("{0:MM/dd/yyyy}", shipinfo.sodate));
                    MessageBox.Show("Ship Date: " + String.Format("{0:MM/dd/yyyy}", shipinfo.shipdate));
                    MessageBox.Show("Buffer: " + refds.capacitycalendar[0].custombuffer.ToString());
                    MessageBox.Show("Addl Buffer: " + String.Format("{0:N2}", shipinfo.addbuffer));
                    MessageBox.Show("List Price: " + String.Format("{0:N2}", shipinfo.listprice));
                    MessageBox.Show("SO Production Units: " + shipinfo.productionunits.ToString());
                    MessageBox.Show("Prior Production Units: " + shipinfo.priorproductionload.ToString());
                }
            }
        }

        private void CalculateShipDate()
        {
            // Tracks the number days with production capacity
            int proddays = 0;
            int extrastockcapacity = 0;
            string commandtext = "";
            shipinfo.calcok = false;
            DateTime testdate = shipinfo.sodate.AddDays(1).Date;
            // Loop till the capacity of the day will support the current units and prior units.
            for (int i = 0; i <= 365; i++)
            {
                shipinfo.priorproductionload = GetPriorShipDateUnits(testdate);
                commandtext = "SELECT * FROM capacitycalendar WHERE proddate = @testdate";
                ClearParameters();
                searchrefds.capacitycalendar.Rows.Clear();
                this.AddParms("@testdate", testdate, "SQL");
                FillData(searchrefds, "capacitycalendar", commandtext, CommandType.Text);
                if (searchrefds.capacitycalendar.Rows.Count < 1)
                {
                    wsgUtilities.wsgNotice("There is no capacity calendar for " + String.Format("{0:MM/dd/yyyy}", testdate) + ". Ship date cannot be calculated.");
                    shipinfo.calcok = false;
                    break;
                }
                else
                {
                    if (searchrefds.capacitycalendar[0].capacity == 0)
                    {
                        // Move the date forward if there is no capacity
                        testdate = testdate.AddDays(1);
                        continue;
                    }
                    // If there are no production and this is a stock cover, use the first day with capacity
                    if (shipinfo.customstock == "S" && shipinfo.productionunits == 0)
                    {
                        shipinfo.shipdate = testdate;
                        shipinfo.calcok = true;
                        break;
                    }

                    // We have a day with capacity
                    proddays++;
                    // See if the number of production days has exceeded the buffer period

                    if (proddays < (shipinfo.buffer + shipinfo.addbuffer))
                    {
                        testdate = testdate.AddDays(1);
                        continue;
                    }

                    // For stock cover capture extra stock production capacity for that day
                    // Make it 0 for custom covers
                    if (shipinfo.customstock == "S")
                    {
                        extrastockcapacity = searchrefds.capacitycalendar[0].extrastockcapacity;
                    }
                    else
                    {
                        extrastockcapacity = 0;
                    }
                    // Use the test data if production is available
                    if (searchrefds.capacitycalendar[0].capacity + extrastockcapacity >= shipinfo.productionunits + shipinfo.priorproductionload)
                    {
                        shipinfo.shipdate = testdate;
                        shipinfo.calcok = true;
                        break;
                    }
                    else
                    {
                        if (i > 200)
                        {
                            wsgUtilities.wsgNotice("After 200 attempts, capacity cannot be found. Terminating.");
                            break;
                        }
                    }
                    testdate = testdate.AddDays(1);
                }
            }//for (int i = 0; i <= 365; i++)
        }

        public void GetSoProductionUnits()
        {
            shipinfo.productionunits = 0;
            while (1 == 1)
            {
                // Worksheets have a synthetic price = -9999 - use level one units
                if (shipinfo.listprice == -9999)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level1buffer;
                    shipinfo.productionunits = refds.productionunitschedule[0].level1units;
                    break;
                }
                if (shipinfo.listprice <= refds.productionunitschedule[0].level1price)
                {
                    shipinfo.addbuffer = 0;
                    shipinfo.productionunits = CalcultateProductionUnits(0, refds.productionunitschedule[0].level1price,
                    0, refds.productionunitschedule[0].level1units);
                    break;
                }
                if (shipinfo.listprice <= refds.productionunitschedule[0].level2price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level1buffer;
                    shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level1price, refds.productionunitschedule[0].level2price,
                    refds.productionunitschedule[0].level1units, refds.productionunitschedule[0].level2units);
                    break;
                }
                if (shipinfo.listprice <= refds.productionunitschedule[0].level3price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level2buffer;
                    shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level2price, refds.productionunitschedule[0].level3price,
                    refds.productionunitschedule[0].level2units, refds.productionunitschedule[0].level3units);
                    break;
                }

                if (shipinfo.listprice <= refds.productionunitschedule[0].level4price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level3buffer;
                    shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level3price, refds.productionunitschedule[0].level4price,
                    refds.productionunitschedule[0].level3units, refds.productionunitschedule[0].level4units);
                    break;
                }
                if (shipinfo.listprice <= refds.productionunitschedule[0].level5price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level4buffer;
                    shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level4price, refds.productionunitschedule[0].level5price,
                    refds.productionunitschedule[0].level4units, refds.productionunitschedule[0].level5units);
                    break;
                }
                if (shipinfo.listprice <= refds.productionunitschedule[0].level6price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level5buffer;
                    shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level5price, refds.productionunitschedule[0].level6price,
                    refds.productionunitschedule[0].level5units, refds.productionunitschedule[0].level6units);
                    break;
                }
                if (shipinfo.listprice <= refds.productionunitschedule[0].level7price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level6buffer;
                    shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level6price, refds.productionunitschedule[0].level7price,
                     refds.productionunitschedule[0].level6units, refds.productionunitschedule[0].level7units);
                    break;
                }
                if (shipinfo.listprice <= refds.productionunitschedule[0].level8price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level7buffer;
                    shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level7price, refds.productionunitschedule[0].level8price,
                     refds.productionunitschedule[0].level7units, refds.productionunitschedule[0].level8units);
                    break;
                }
                if (shipinfo.listprice <= refds.productionunitschedule[0].level9price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level8buffer;
                    shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level8price, refds.productionunitschedule[0].level9price,
                     refds.productionunitschedule[0].level8units, refds.productionunitschedule[0].level9units);

                    break;
                }
                shipinfo.addbuffer = refds.productionunitschedule[0].level9buffer;
                shipinfo.addbuffer = refds.productionunitschedule[0].level10buffer;
                shipinfo.productionunits = CalcultateProductionUnits(refds.productionunitschedule[0].level9price, refds.productionunitschedule[0].level10price,
                 refds.productionunitschedule[0].level9units, refds.productionunitschedule[0].level10units);
                if (shipinfo.listprice > refds.productionunitschedule[0].level10price)
                {
                    shipinfo.addbuffer = refds.productionunitschedule[0].level10buffer;
                }
                break;
            }
        }

        public decimal CalcultateProductionUnits(decimal firstprice, decimal lastprice, decimal firstunits, decimal lastunits)
        {
            decimal returnunits = 0;
            // Protect against division by zero
            if (firstunits == lastunits || firstprice == lastprice)
            {
                returnunits = firstunits;
            }
            else
            {
                returnunits = firstunits + ((shipinfo.listprice - firstprice) / (lastprice - firstprice) *
                (lastunits - firstunits));
            }
            return returnunits;
        }

        public decimal GetPriorShipDateUnits(DateTime ordate)
        {
            string commandtext = "";
            decimal dateload = 0;
            quotetempds.somast.Rows.Clear();
            commandtext = "select ordate, SUM(produnits) AS produnits FROM somast ";
            commandtext += "WHERE ordate = @ordate AND produnits <> 0 AND sostat <> 'V' AND sotype = 'O' ";
            commandtext += "AND sono <> @sono GROUP BY ordate ";
            ClearParameters();
            this.AddParms("@ordate", ordate, "SQL");
            this.AddParms("@sono", shipinfo.sono, "SQL");
            FillData(quotetempds, "somast", commandtext, CommandType.Text);
            if (quotetempds.somast.Rows.Count > 0)
            {
                dateload = quotetempds.somast[0].produnits;
            }
            return dateload;
        }

        public decimal GetAllShipDateUnits(DateTime ordate)
        {
            string commandtext = "";
            decimal dateload = 0;
            quotetempds.somast.Rows.Clear();
            commandtext = "select ordate, SUM(produnits) AS produnits FROM somast ";
            commandtext += "WHERE ordate = @ordate AND produnits <> 0 AND sostat <> 'V' AND sotype = 'O' ";
            commandtext += "GROUP BY ordate ";
            ClearParameters();
            this.AddParms("@ordate", ordate, "SQL");
            FillData(quotetempds, "somast", commandtext, CommandType.Text);
            if (quotetempds.somast.Rows.Count > 0)
            {
                dateload = quotetempds.somast[0].produnits;
            }
            return dateload;
        }

        public void GetListPrice()
        {
            // All prior tests have been passed. Compute list price and locate units for that price.
            string commandtext = " SELECT * from socover WHERE sono = @sono AND covertype = 'C' AND ";
            commandtext += "RTRIM(product) <> 'Non-Meyco Custom' AND ";
            commandtext += "RTRIM(product) <> 'Non-Meyco Stock' AND ";
            commandtext += "RTRIM(product) <> 'Cover Repair' AND ";
            commandtext += "RTRIM(product) <> 'Cover Alteration' ";
            ClearParameters();
            this.AddParms("@sono", shipinfo.sono, "SQL");
            somastds.socover.Rows.Clear();
            FillData(somastds, "socover", commandtext, CommandType.Text);
            shipinfo.listprice = 0;
            if (somastds.socover.Rows.Count > 0)
            {
                if (somastds.socover[0].product.TrimEnd() != "Worksheet")
                {
                    for (int i = 0; i <= somastds.socover.Rows.Count - 1; i++)
                    {
                        // Add the cover price
                        shipinfo.listprice += (somastds.socover[i].qtyord * somastds.socover[i].price);
                        // Find the lines associated with this cover
                        commandtext = "SELECT * FROM soline WHERE sono = @sono AND cover = @cover";
                        ClearParameters();
                        this.AddParms("@sono", shipinfo.sono, "SQL");
                        this.AddParms("@cover", somastds.socover[i].cover, "SQL");
                        somastds.soline.Clear();
                        FillData(somastds, "soline", commandtext, CommandType.Text);
                        if (somastds.soline.Rows.Count > 0)
                        {
                            for (int j = 0; j <= somastds.soline.Rows.Count - 1; j++)
                            {
                                if (somastds.soline[j].source != "HW")
                                {
                                    shipinfo.listprice += (somastds.soline[j].qtyord * somastds.soline[j].price);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Force -9999 list price for worksheet
                    shipinfo.listprice = -9999;
                }
            }
        }

        public bool IsOrderCoverStock(string sono)
        {
            bool IsStockCover = false;
            quoteds.view_coverdata.Rows.Clear();
            string commandtext = "SELECT * from view_coverdata WHERE covertype = 'C' AND sono = @sono";
            ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            FillData(quoteds, "view_coverdata", commandtext, CommandType.Text);
            if (quoteds.view_coverdata.Rows.Count > 0)
            {
                switch (quoteds.view_coverdata[0].product.TrimEnd())
                {
                    case "Stock Cover":
                    case "Non-Meyco Stock":
                        {
                            IsStockCover = true;
                            break;
                        }
                }
            }
            return IsStockCover;
        }

        public bool IsOrderRepairOrAlteration(string sono)
        {
            bool IsRepairOrAlteration = false;
            quoteds.view_coverdata.Rows.Clear();
            string commandtext = "SELECT * from view_coverdata WHERE covertype = 'C' AND sono = @sono";
            ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            FillData(quoteds, "view_coverdata", commandtext, CommandType.Text);
            if (quoteds.view_coverdata.Rows.Count > 0)
            {
                switch (quoteds.view_coverdata[0].product.TrimEnd())
                {
                    case "Cover Repair":
                    case "Cover Alteration":
                        {
                            IsRepairOrAlteration = true;
                            break;
                        }
                }
            }
            return IsRepairOrAlteration;
        }

        public decimal GetStockCoverAvailability(string sono, bool adjustforcurrentcover, string loctid)
        {
            decimal QtyAvailable = 0;
            somastds.view_coverdata.Rows.Clear();
            string CommandString = "SELECT * FROM view_coverdata WHERE sono = @sono AND covertype = 'C'";
            ClearParameters();
            AddParms("@sono", sono, "SQL");
            this.FillData(somastds, "view_coverdata", CommandString, CommandType.Text);
            if (somastds.view_coverdata.Rows.Count > 0)
            {
                CommandString = "SELECT * FROM view_itemloctidonhand WHERE item = @item AND loctid = @loctid";
                ClearParameters();
                invds.view_itemloctidonhand.Rows.Clear();
                AddParms("@item", somastds.view_coverdata[0].item, "SQL");
                AddParms("@loctid", loctid, "SQL");
                this.FillData(invds, "view_itemloctidonhand", CommandString, CommandType.Text);
                if (invds.view_itemloctidonhand.Rows.Count > 0)
                {
                    QtyAvailable = invds.view_itemloctidonhand[0].tqty - invds.view_itemloctidonhand[0].allocated; ;

                    if (adjustforcurrentcover)
                    {
                        // Increment the quantity available to account for the current cover
                        QtyAvailable++;
                    }
                }
            }
            return QtyAvailable;
        }
    } // Miscellaneous Data Methods

    public class ShipDateInformation
    {
        public string sono { get; set; }
        public bool calcok { get; set; }
        public decimal listprice { get; set; }
        public DateTime sodate { get; set; }
        public DateTime shipdate { get; set; }
        public string customstock { get; set; }
        public int buffer { get; set; }
        public decimal addbuffer { get; set; }
        public bool stockcoveronhand { get; set; }
        public decimal productionunits { get; set; }
        public decimal priorproductionload { get; set; }
        public decimal productionload { get; set; }
    }

    public class InventoryUtilities : WSGDataAccess
    {
        public inventoryds invds = new inventoryds();

        public InventoryUtilities(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            SetIdcol(invds.ictran.idcolColumn);
        }
    }

    public class EmailMethods
    {
        public void SendEmail(System.Net.Mail.MailMessage emailMessage, string subject, string body, string fromaddress)
        {
            string SMTPServer = ConfigurationManager.AppSettings["SMTPServer"];
            string SMTPUser = ConfigurationManager.AppSettings["SMTPUser"];
            string SMTPNotifyAddress = ConfigurationManager.AppSettings["NotifyAddress"];
            string SMTPPassword = ConfigurationManager.AppSettings["SMTPassword"];
            string SMTPFrom = "";
            if (fromaddress.TrimEnd() == "")
            {
                SMTPFrom = ConfigurationManager.AppSettings["SMTPFromName"] + "<" + ConfigurationManager.AppSettings["SMTPFromAddress"] + ">";
            }
            else
            {
                SMTPFrom = fromaddress;
            }
            // Subject
            emailMessage.Subject = subject;
            // Body
            emailMessage.Body = body;
            // From address
            emailMessage.From = new System.Net.Mail.MailAddress(SMTPFrom);

            // Send the email
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SMTPServer, 25);
            smtp.Credentials = new NetworkCredential(SMTPUser, SMTPPassword);
            smtp.Send(emailMessage);
        }
    }

    internal class SqlExceptionHandler
    {
        public static void HandleException(Exception e)
        {
            MessageBox.Show(e.Message);
            WSGUtilitieslib.Telemetry.Telemetry.AddErrorEvent(e.ToString());
        }
    }
}