using CommonAppClasses;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Design
{
    public class DesignClasses : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        public BindingSource bindingFeatureSelector = new BindingSource();
        public BindingSource bindingSolines = new BindingSource();
        public FrmActualQty frmActualQty = new FrmActualQty();
        public item featureds = new item();
        public DataTable dtfeatures = new DataTable();
        public quote socurrentitemsds = new quote();
        public GetSoMethods getSoMethods = new GetSoMethods("SQL", "SQLConnString");
        public MiscellaneousDataMethods miscDataMethods = new MiscellaneousDataMethods("SQL", "SQLConnString");
        public string CurrentState = "";
        public string Sono = "";
        public string Cover = "";
        public string Version = "";
        public quote somastds = new quote();

        public DesignClasses(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
            CreateFeaturesTable();
            SetIdcol(somastds.soline.idcolColumn);
            SetIdcol(socurrentitemsds.soline.idcolColumn);
        }

        public void EnterActualQuantiites(Form CallingForm)
        {
            SetActQtyEvents();

            while (true)
            {
                bool oktoproceess = true;
                getSoMethods.returnsono = "";
                getSoMethods.GetSono();
                if (!getSoMethods.wascancelled)
                {
                    if (getSoMethods.returnsono.TrimEnd() != "")
                    {
                        // Skip if not a quote
                        if (getSoMethods.quoteds.somast[0].enterqu != "Y")
                        {
                            wsgUtilities.wsgNotice("Sales Order " + getSoMethods.returnsono.TrimEnd().TrimStart() + " is not a cover estimate");
                            oktoproceess = false;
                        }
                        // Skip if not a quote
                        if (oktoproceess && getSoMethods.quoteds.somast[0].sotype != "O")
                        {
                            wsgUtilities.wsgNotice("Estimate " + getSoMethods.returnsono.TrimEnd().TrimStart() + " has not been converted to an order");
                            oktoproceess = false;
                        }
                    }
                    if (oktoproceess)
                    {
                        LoadVersionViewData(getSoMethods.quoteds.somast[0].sono);
                        if (somastds.view_versiondata.Rows.Count > 0)
                        {
                            // Locate the cover for this SO, Version
                            Cover = miscDataMethods.GetSOVersionCover(getSoMethods.quoteds.somast[0].sono, somastds.view_versiondata[0].version);
                            if (Cover == "")
                            {
                                oktoproceess = false;
                            }
                        }
                        else
                        {
                            wsgUtilities.wsgNotice("Version data for " + getSoMethods.returnsono.TrimEnd() + " is missing. Processing halted.");
                            oktoproceess = false;
                        }

                        if (oktoproceess)
                        {
                            CurrentState = "Select";
                            Version = somastds.view_versiondata[0].version;
                            Sono = somastds.view_versiondata[0].sono;
                            socurrentitemsds.soline.Rows.Clear();
                            socurrentitemsds.AcceptChanges();
                            featureds.view_immasterdata.Rows.Clear();
                            featureds.AcceptChanges();
                            RefreshFrmActualQtyControls();
                            frmActualQty.ShowDialog();
                        }
                    }
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        public void LoadVersionViewData(string sono)
        {
            somastds.view_versiondata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(somastds, "view_versiondata", "wsgsp_getsoversions", CommandType.StoredProcedure);
        }

        public void LoadCoverViewData(string sono, string version)
        {
            somastds.view_coverdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@version", version, "SQL");
            this.FillData(somastds, "view_coverdata", "wsgsp_getsoversioncovers", CommandType.StoredProcedure);
        }

        public void CreateFeaturesTable()
        {
            DataTable dt = new DataTable();
            dtfeatures.Columns.Add("feature", typeof(String));
            dtfeatures.Columns.Add("code", typeof(String));
            DataRow dr = dtfeatures.NewRow();
            dr[0] = "";
            dr[1] = "";
            setfeaturerow("Additional Desc", "CD");
            setfeaturerow("Hardware", "HW");
            setfeaturerow("Obstructions", "OB");
            setfeaturerow("Drainage", "DR");
            setfeaturerow("Padding", "PA");
            setfeaturerow("Miscellaneous", "MI");
            setfeaturerow("Commercial Item", "CO");
        }

        public void setfeaturerow(string feature, string code)
        {
            DataRow newFeaturesRow = dtfeatures.NewRow();
            newFeaturesRow["feature"] = feature;
            newFeaturesRow["code"] = code;
            dtfeatures.Rows.Add(newFeaturesRow);
        }

        public void GetFeatureItems(string featurecode)
        {
            // Populate the feature selector table
            featureds.view_immasterdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@code", featurecode, "SQL");
            this.FillData(featureds, "view_immasterdata", "wsgsp_getquoteitem", CommandType.StoredProcedure);
        }

        public void GetNewLineItemData(string item, string sono, string version, string cover)
        {
            // This routine establishes a new row in current soline table based on the item passed

            string strExpr = "item = '" + item + "'";
            DataRow[] foundItemRows =
            featureds.view_immasterdata.Select(strExpr);
            socurrentitemsds.soline.Rows.Add();
            int rowpointer = socurrentitemsds.soline.Rows.Count - 1;
            InitializeDataTable(socurrentitemsds.soline, rowpointer);
            socurrentitemsds.soline[rowpointer].source = foundItemRows[0]["misccode"].ToString(); ;
            socurrentitemsds.soline[rowpointer].descrip = foundItemRows[0]["shortdescrip"].ToString();
            socurrentitemsds.soline[rowpointer].item = foundItemRows[0]["item"].ToString();
            socurrentitemsds.soline[rowpointer].price = 0;
            socurrentitemsds.soline[rowpointer].disc = 0;
            socurrentitemsds.soline[rowpointer].version = version;
            socurrentitemsds.soline[rowpointer].cover = cover;
            socurrentitemsds.soline[rowpointer].sono = sono;
            socurrentitemsds.soline[rowpointer].qtyord = 1;
            socurrentitemsds.soline[rowpointer].qtyact = 1;
        }

        public void LoadMiscellandousSoLineData(string featurecode, string sono, string version, string cover)
        {
            string commandtext = "SELECT * FROM soline WHERE sono = @sono AND version  =  @version ";
            commandtext += " AND cover = @cover AND source <> 'C' AND LEFT(source,2) = @featurecode ORDER BY descrip";
            socurrentitemsds.soline.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@version", version, "SQL");
            this.AddParms("@cover", cover, "SQL");
            this.AddParms("@featurecode", featurecode, "SQL");
            this.FillData(socurrentitemsds, "soline", commandtext, CommandType.Text);
        }

        public void SetActQtyEvents()
        {
            frmActualQty.listBoxFeatures.ValueMember = "code";
            frmActualQty.listBoxFeatures.DisplayMember = "feature";
            frmActualQty.listBoxFeatures.DataSource = dtfeatures;
            frmActualQty.listBoxFeatures.SelectedIndexChanged += new System.EventHandler(listBoxFeatures_SelectedIndexChanged);
            frmActualQty.dataGridViewFeatureSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFeatureSelector_CellContentDoubleClick);
            frmActualQty.buttonReturn.Click += new System.EventHandler(buttonReturn_Click);
            frmActualQty.buttonSave.Click += new System.EventHandler(buttonSave_Click);
            frmActualQty.buttonClear.Click += new System.EventHandler(buttonClear_Click);
        }

        private void dataGridViewFeatureSelector_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string item = CaptureDataGridColumn(frmActualQty.dataGridViewFeatureSelector, "item");
            GetNewLineItemData(item, Sono, Version, Cover);
            frmActualQty.Update();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit")
            {
                if (wsgUtilities.wsgReply("Abandon Edit?"))
                {
                    frmActualQty.Close();
                }
            }
            else
            {
                frmActualQty.Close();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            socurrentitemsds.soline.Rows.Clear();
            socurrentitemsds.AcceptChanges();
            featureds.view_immasterdata.Rows.Clear();
            featureds.AcceptChanges();
            CurrentState = "Select";
            RefreshFrmActualQtyControls();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveSoLineitems();
            wsgUtilities.wsgNotice("Line items saved");
            socurrentitemsds.soline.Rows.Clear();
            socurrentitemsds.AcceptChanges();
            featureds.view_immasterdata.Rows.Clear();
            featureds.AcceptChanges();
            CurrentState = "Select";
            RefreshFrmActualQtyControls();
        }

        private void listBoxFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load the items for this  features
            LoadMiscellandousSoLineData(frmActualQty.listBoxFeatures.SelectedValue.ToString(), Sono, Version, Cover);
            frmActualQty.dataGridViewSolines.AutoGenerateColumns = false;
            frmActualQty.dataGridViewSolines.DataSource = bindingSolines;
            bindingSolines.DataSource = socurrentitemsds.soline;
            frmActualQty.dataGridViewSolines.RowsDefaultCellStyle.BackColor = Color.LightGray;
            frmActualQty.dataGridViewSolines.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            GetFeatureItems(frmActualQty.listBoxFeatures.SelectedValue.ToString());
            frmActualQty.dataGridViewFeatureSelector.DataSource = bindingFeatureSelector;
            frmActualQty.dataGridViewFeatureSelector.AutoGenerateColumns = false;
            bindingFeatureSelector.DataSource = featureds.view_immasterdata;
            frmActualQty.dataGridViewFeatureSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
            frmActualQty.dataGridViewFeatureSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            frmActualQty.dataGridViewFeatureSelector.Focus();
            CurrentState = "Edit";
            RefreshFrmActualQtyControls();
        }

        public void RefreshFrmActualQtyControls()
        {
            frmActualQty.buttonReturn.Enabled = true;
            frmActualQty.listBoxFeatures.Enabled = false;
            frmActualQty.dataGridViewFeatureSelector.Enabled = false;
            frmActualQty.dataGridViewSolines.Enabled = false;
            frmActualQty.buttonSave.Enabled = false;
            frmActualQty.buttonClear.Enabled = false;
            switch (CurrentState)
            {
                case "Select":
                    {
                        frmActualQty.listBoxFeatures.Enabled = true;
                        break;
                    }
                case "Edit":
                    {
                        frmActualQty.listBoxFeatures.Enabled = false;
                        frmActualQty.dataGridViewFeatureSelector.Enabled = true;
                        frmActualQty.dataGridViewSolines.Enabled = true;
                        frmActualQty.buttonSave.Enabled = true;
                        frmActualQty.buttonClear.Enabled = true;
                        break;
                    }
            }
        }

        public void SaveSoLineitems()
        {
            foreach (DataRow row in socurrentitemsds.soline)
            {
                GenerateAppTableRowSave(row);
            }
        }
    }
}