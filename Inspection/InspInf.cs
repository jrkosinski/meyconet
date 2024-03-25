using CommonAppClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Inspection
{
    #region Inspection Information

    public class InspInf : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Inspection");
        public int CurrentCustid { get; set; }
        public string CurrentCustno { get; set; }
        public string CurrentVersion { get; set; }
        public string CurrentCover { get; set; }
        public string CurrentState { get; set; }
        public string CurrentFeature { get; set; }
        public string CurrentSono { get; set; }
        public string Featurekey { get; set; }
        public int CurrentSomastid { get; set; }
        public string CurrentItem { get; set; }
        public bool LoadingCover { get; set; }
        public price prds { get; set; }
        public quote somastds { get; set; }
        public customer ards { get; set; }
        public inspection inspds { get; set; }
        public FrmRepairInspection parentform { get; set; }
        public inspection soitemsds { get; set; }
        public quote tempds { get; set; }
        public inspection socurrentitemsds { get; set; }
        public item itemds { get; set; }
        public reference soreferenceds { get; set; }
        public item featureds { get; set; }
        public system appinfods { get; set; }
        public quote resultsds { get; set; }
        public system matcondds { get; set; }
        public system inbcarrierds { get; set; }
        public string PassedSono = "";
        public string InspectionItem = "RINSP";
        public string MinimumLaborItem = "ROADD";
        public string CommercialInspectionItem = "RCOMM";
        public system threadcondds { get; set; }
        public system webcondds { get; set; }
        public system springscondds { get; set; }
        public system locationds { get; set; }
        public system manufacturerds { get; set; }
        public DataTable dtresults { get; set; }
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");
        public DataTable dtfeatures { get; set; }
        public BindingSource bindingFeatureSelector { get; set; }
        public BindingSource bindingSelectedItems { get; set; }
        public DataTable dtInsplineOriginal { get; set; }
        public DataTable dtcoverdimensions { get; set; }
        public Dictionary<string, string> errorlist = new Dictionary<string, string>();
        public ImmasterAccess immasterAccess = new ImmasterAccess("SQL", "SQLConnString");

        public InspInf(string DataStore, string AppConfigName, FrmRepairInspection callingform)
          : base(DataStore, AppConfigName)
        {
            somastds = new quote();
            appinfods = new system();
            ards = new customer();
            inspds = new inspection();
            inbcarrierds = new system();
            matcondds = new system();
            springscondds = new system();
            webcondds = new system();
            threadcondds = new system();
            manufacturerds = new system();
            locationds = new system();
            bindingFeatureSelector = new BindingSource();
            bindingSelectedItems = new BindingSource();
            SetIdcol(somastds.soaddr.idcolColumn);
            SetIdcol(somastds.somast.idcolColumn);
            SetIdcol(ards.arcust.idcolColumn);
            SetIdcol(ards.aracadr.idcolColumn);
            SetIdcol(inspds.inspmast.idcolColumn);
            SetIdcol(inspds.inspversion.idcolColumn);
            SetIdcol(inspds.inspline.idcolColumn);
            CreateFeaturesTable();
            featureds = new item();
            itemds = new item();
            tempds = new quote();
            socurrentitemsds = new inspection();
            SetIdcol(socurrentitemsds.inspline.idcolColumn);
            soreferenceds = new reference();
            soitemsds = new inspection();
            CreateReferenceTables();
            SetIdcol(soitemsds.inspline.idcolColumn);
            featureds = new item();
            dtInsplineOriginal = new DataTable();
            errorlist.Add("100", "You must specify an item for the cover");
            LoadingCover = true;
            EstablishReferenceTables();
            parentform = callingform;
            parentform.labelTrackingData.Text = "";
            EstablishBlankInspmastData();
            EstablishBlankSomastData();
            SetBindings();
            SetEvents();
            CurrentFeature = "";
            CurrentState = "Select";
            RefreshParentControls();
        }

        #region Set Events

        public void SetEvents()
        {
            parentform.Shown += new System.EventHandler(ShowForm);
            parentform.buttonEdit.Click += new System.EventHandler(SetEditState);
            parentform.buttonRoute.Click += new System.EventHandler(RouteSO);
            parentform.buttonCancelEdit.Click += new System.EventHandler(CancelEdit);
            parentform.buttonClear.Click += new System.EventHandler(ClearAll);
            parentform.buttonClose.Click += new System.EventHandler(CloseForm);
            parentform.buttonAttachPDF.Click += new System.EventHandler(AttachPDF);
            parentform.buttonSave.Click += new System.EventHandler(SaveInspection);
            parentform.buttonGetSO.Click += new System.EventHandler(GetSo);
            parentform.buttonSelectCover.Click += new System.EventHandler(SelectCover);
            parentform.buttonSelectVersion.Click += new System.EventHandler(ActivateVersion);
            parentform.buttonViewReport.Click += new System.EventHandler(ViewReport);
            parentform.buttonViewLabel.Click += new System.EventHandler(ViewLabel);
            parentform.buttonViewMainPdf.Click += new System.EventHandler(ViewMainPDF);
            parentform.buttonDeleteOption.Click += new System.EventHandler(DeleteVersion);
            parentform.buttonCreateNewOption.Click += new System.EventHandler(CreateNewVersion);
            parentform.comboBoxMaterial.SelectedIndexChanged += new System.EventHandler(SetMaterial);
            parentform.comboBoxMaterial.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxColor.SelectedIndexChanged += new System.EventHandler(SetColor);
            parentform.comboBoxColor.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxSpacing.SelectedIndexChanged += new System.EventHandler(SetSpacing);
            parentform.comboBoxSpacing.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxRecommendation.SelectedIndexChanged += new System.EventHandler(SetRecommendation);
            parentform.comboBoxRecommendation.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxCommlRes.SelectedIndexChanged += new System.EventHandler(SetCommlRes);
            parentform.comboBoxCommlRes.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxCleanliness.SelectedIndexChanged += new System.EventHandler(SetCleanliness);
            parentform.comboBoxCleanliness.LostFocus += new System.EventHandler(HideComboBox);

            parentform.comboBoxFrtTerms.SelectedIndexChanged += new System.EventHandler(SetFrtTerms);
            parentform.listBoxFeatures.SelectedIndexChanged += new System.EventHandler(SetFeatures);
            parentform.dateTimePickerRecdate.ValueChanged += new System.EventHandler(SetRecDate);
            parentform.textBoxCwidin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxClenin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxX1widin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxX1lenin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxX2widin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxX2lenin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxX3widin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxX3lenin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxX4widin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxX4lenin.Validating += new System.ComponentModel.CancelEventHandler(CheckTextBoxInches);
            parentform.textBoxSoNo.KeyDown += new System.Windows.Forms.KeyEventHandler(ProcessSOKey);
            parentform.dataGridViewFeatureSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(ProcessFeatureSelectorKey);

            // Reference combo boxes
            parentform.comboBoxMaterialCondition.SelectedIndexChanged += new System.EventHandler(SetMaterialCondition);
            parentform.comboBoxMaterialCondition.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxSpringsCondition.SelectedIndexChanged += new System.EventHandler(SetSpringsCondition);
            parentform.comboBoxSpringsCondition.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxWebCondition.SelectedIndexChanged += new System.EventHandler(SetWebCondition);
            parentform.comboBoxWebCondition.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxThreadCondition.SelectedIndexChanged += new System.EventHandler(SetThreadCondition);
            parentform.comboBoxThreadCondition.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxManufacturer.SelectedIndexChanged += new System.EventHandler(SetManufacturer);
            parentform.comboBoxManufacturer.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxInbCarrier.SelectedIndexChanged += new System.EventHandler(SetInbCarrier);
            parentform.comboBoxInbCarrier.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxFrtTerms.SelectedIndexChanged += new System.EventHandler(SetFrtTerms);
            parentform.comboBoxFrtTerms.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxLocation.SelectedIndexChanged += new System.EventHandler(SetLocation);
            parentform.comboBoxLocation.LostFocus += new System.EventHandler(HideComboBox);

            // Closing Event
            parentform.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormClosing);

            // Line item buttons
            parentform.buttonCancelLines.Click += new System.EventHandler(CancelLineItems);
            parentform.buttonSaveLines.Click += new System.EventHandler(SaveLineItems);
            // Feature Selector datagridview
            parentform.dataGridViewFeatureSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(CaptureSelectedFeatureItem);
            //Selected features datagridview
            parentform.dataGridViewSelectedItems.MouseUp += new System.Windows.Forms.MouseEventHandler(DeleteSelectedItem);

            // Activate comboboxes when textboxes are clicked
            parentform.textBoxMaterialCondition.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxFrtTerms.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxSpringsCondition.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxInbCarrier.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxManufacturer.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxLocation.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxThreadCondition.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxWebCondition.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxInbCarrier.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxColor.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxMaterial.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxSpacing.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxCommlRes.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxCleanliness.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxRecommendation.Click += new System.EventHandler(ShowComboBox);
        }

        #endregion Set Events

        public void LoadInspection(string sono)
        {
            LoadingCover = true;
            CurrentSomastid = GetSomastBySono(sono);
            if (CurrentSomastid > 0)
            {
                GetInspmastBySono(sono);
                CurrentCustid = somastds.somast[0].custid;
                CurrentCustno = ards.arcust[0].custno;
                CurrentVersion = "";
                ProcessCover("");
                CurrentState = "View";
                LoadingCover = false;
            }
            LoadingCover = false;
            parentform.labelTrackingData.Text = trackingInf.GetLastSOTrackingData(somastds.somast[0].sono);
        }

        public void RefreshCover()
        {
            // Refresh Text boxes where necessary
            parentform.textBoxMaterial.Text = GetMaterialDescription(inspds.inspmast[0].materialid);
            parentform.textBoxColor.Text = GetColorDescription(inspds.inspmast[0].colorid);
            parentform.textBoxSpacing.Text = GetSpacingDescription(inspds.inspmast[0].spacingid);
            SetReferenceTextboxes();
        }

        public int GetInspmastBySono(string sono)
        {
            inspds.inspmast.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(inspds, "inspmast", "wsgsp_getinspmastbysono", CommandType.StoredProcedure);
            if (inspds.inspmast.Rows.Count == 0)
            {
                EstablishBlankInspmastData();
                InitializeInspmast();
                // Establish Meyco File number here
                GetFileNumber();
            }
            return inspds.inspmast[0].idcol;
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
                // Load customer data for this SO
                getSingleCustomerData(somastds.somast[0].custid);
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

        public void SaveInspection(object sender, EventArgs e)
        {
            SaveInspMast();
            SaveSomastData();
            SaveInspVersion();
            SaveInspLineitems();
            MakeReportPDF();
            LoadInspection(somastds.somast[0].sono);
            CurrentState = "View";
            RefreshParentControls();
        }

        public void SaveInspMast()
        {
            // Calculate the string version of the cover dimensions
            inspds.inspmast[0].coverstring = "";
            // Cover
            // Width feet
            if (inspds.inspmast[0].cwidft != 0)
            {
                inspds.inspmast[0].coverstring += inspds.inspmast[0].cwidft.ToString().TrimEnd().TrimStart() + "'";
            }
            // Width inches
            if (inspds.inspmast[0].cwidin != 0)
            {
                inspds.inspmast[0].coverstring += inspds.inspmast[0].cwidin.ToString().TrimEnd().TrimStart() + '"';
            }

            inspds.inspmast[0].coverstring += "X";
            // Length feet        f
            if (inspds.inspmast[0].clenft != 0)
            {
                inspds.inspmast[0].coverstring += inspds.inspmast[0].clenft.ToString().TrimEnd().TrimStart() + "'";
            }
            // Length inches
            if (inspds.inspmast[0].clenin != 0)
            {
                inspds.inspmast[0].coverstring += inspds.inspmast[0].clenin.ToString().TrimEnd().TrimStart() + '"';
            }

            // Extension 1
            // Width feet
            if (inspds.inspmast[0].x1widft != 0)
            {
                inspds.inspmast[0].coverstring += " + ";
                inspds.inspmast[0].coverstring += inspds.inspmast[0].x1widft.ToString().TrimEnd().TrimStart() + "'";

                // Width inches
                if (inspds.inspmast[0].x1widin != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x1widin.ToString().TrimEnd().TrimStart() + '"';
                }

                inspds.inspmast[0].coverstring += "X";
                // Length feet
                if (inspds.inspmast[0].x1lenft != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x1lenft.ToString().TrimEnd().TrimStart() + "'";
                }
                // Length inches
                if (inspds.inspmast[0].x1lenin != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x1lenin.ToString().TrimEnd().TrimStart() + '"';
                }
            }
            // Extension 2
            // Width feet
            if (inspds.inspmast[0].x2widft != 0)
            {
                inspds.inspmast[0].coverstring += " + ";
                inspds.inspmast[0].coverstring += inspds.inspmast[0].x2widft.ToString().TrimEnd().TrimStart() + "'";

                // Width inches
                if (inspds.inspmast[0].x2widin != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x2widin.ToString().TrimEnd().TrimStart() + '"';
                }

                inspds.inspmast[0].coverstring += "X";
                // Length feet
                if (inspds.inspmast[0].x2lenft != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x2lenft.ToString().TrimEnd().TrimStart() + "'";
                }
                // Length inches
                if (inspds.inspmast[0].x2lenin != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x2lenin.ToString().TrimEnd().TrimStart() + '"';
                }
            }

            // Extension 3
            // Width feet
            if (inspds.inspmast[0].x3widft != 0)
            {
                inspds.inspmast[0].coverstring += " + ";
                inspds.inspmast[0].coverstring += inspds.inspmast[0].x3widft.ToString().TrimEnd().TrimStart() + "'";

                // Width inches
                if (inspds.inspmast[0].x3widin != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x3widin.ToString().TrimEnd().TrimStart() + '"';
                }

                inspds.inspmast[0].coverstring += "X";
                // Length feet
                if (inspds.inspmast[0].x3lenft != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x3lenft.ToString().TrimEnd().TrimStart() + "'";
                }
                // Length inches
                if (inspds.inspmast[0].x3lenin != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x3lenin.ToString().TrimEnd().TrimStart() + '"';
                }
            }

            // Extension 4
            // Width feet
            if (inspds.inspmast[0].x4widft != 0)
            {
                inspds.inspmast[0].coverstring += " + ";
                inspds.inspmast[0].coverstring += inspds.inspmast[0].x4widft.ToString().TrimEnd().TrimStart() + "'";

                // Width inches
                if (inspds.inspmast[0].x4widin != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x4widin.ToString().TrimEnd().TrimStart() + '"';
                }

                inspds.inspmast[0].coverstring += "X";
                // Length feet
                if (inspds.inspmast[0].x4lenft != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x4lenft.ToString().TrimEnd().TrimStart() + "'";
                }
                // Length inches
                if (inspds.inspmast[0].x4lenin != 0)
                {
                    inspds.inspmast[0].coverstring += inspds.inspmast[0].x4lenin.ToString().TrimEnd().TrimStart() + '"';
                }
            }
            GenerateAppTableRowSave(inspds.inspmast[0], excludeColumns: new string[] { "prwcov" });
        }

        public void EstablishBlankInspmastData()
        {
            EstablishBlankDataTableRow(inspds.inspmast);
        }

        public void InitializeInspmast()
        {
            inspds.inspmast[0].sono = somastds.somast[0].sono;
            inspds.inspmast[0].somastid = somastds.somast[0].idcol;
            inspds.inspmast[0].recdate = DateTime.Now;
            inspds.inspmast[0].receivedby = AppUserClass.AppUserId;
            inspds.inspmast[0].instructions = "N";
            inspds.inspmast[0].photos = "N";
            inspds.inspmast[0].covermeasured = "N";
            inspds.inspmast[0].adduser = AppUserClass.AppUserId;
            inspds.inspmast[0].lckuser = AppUserClass.AppUserId;
            inspds.inspmast[0].lckdate = DateTime.Now;
            inspds.inspmast[0].adddate = DateTime.Now;
        }

        public void SelectCover(object sender, EventArgs e)
        {
            // Show the item selector screen
            FrmGetImmaster frmGetImmaster = new FrmGetImmaster();
            // Show custom pool covers only
            frmGetImmaster.SelectedCode = "CU";
            frmGetImmaster.ShowDialog();
            if (frmGetImmaster.SelectedItem != "")
            {
                inspds.inspmast[0].item = frmGetImmaster.SelectedItem;
                string CommandString = "SELECT * FROM immaster WHERE item = @item";
                this.ClearParameters();
                this.AddParms("@item", inspds.inspmast[0].item, "SQL");
                this.FillData(itemds, "immaster", CommandString, CommandType.Text);

                inspds.inspmast[0].descrip = itemds.immaster[0].descrip;
                inspds.inspmast.AcceptChanges();
                parentform.Update();
            }
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

        public void getSingleCustomerData(int Custid)
        {
            ards.arcust.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", Custid, "SQL");
            this.FillData(ards, "arcust", "wsgsp_getcustomerdata", CommandType.StoredProcedure);
        }

        public void setfeaturerow(string feature, string code)
        {
            DataRow newFeaturesRow = dtfeatures.NewRow();
            newFeaturesRow["feature"] = feature;
            newFeaturesRow["code"] = code;
            dtfeatures.Rows.Add(newFeaturesRow);
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

        public string GetItemDescription(string item)
        {
            string ItemDesc = "";

            if (item != "")
            {
                itemds.immaster.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@item", item, "SQL");
                this.FillData(itemds, "immaster", "wsgsp_getsingleimmasterdata", CommandType.StoredProcedure);
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

        public void ClearAll(object sender, EventArgs e)
        {
            inspds.inspmast.Rows.Clear();
            somastds.somast.Rows.Clear();
            ards.arcust.Rows.Clear();
            soitemsds.inspline.Rows.Clear();
            socurrentitemsds.inspline.Rows.Clear();
            CurrentState = "Select";
            RefreshParentControls();
        }

        public void RefreshParentControls()
        {
            LoadingCover = true;
            dtfeatures.Rows.Clear();
            setfeaturerow("Patches", "RP");
            setfeaturerow("Panels", "RN");
            setfeaturerow("Webbing", "RW");
            setfeaturerow("Special Applications", "RS");
            setfeaturerow("Padding", "RQ");
            setfeaturerow("Hardware", "HW");
            setfeaturerow("Miscellaneous", "RM");
            LoadingCover = false;
            switch (CurrentState)
            {
                case "Select":
                    {
                        DisableControls();
                        parentform.textBoxSoNo.Enabled = true;
                        parentform.buttonGetSO.Enabled = true;
                        parentform.buttonClose.Enabled = true;
                        break;
                    }

                case "View":
                    {
                        DisableControls();
                        parentform.buttonEdit.Enabled = true;
                        parentform.buttonRoute.Enabled = true;
                        parentform.buttonClear.Enabled = true;
                        parentform.buttonCreateNewOption.Enabled = true;
                        parentform.buttonSelectVersion.Enabled = true;
                        parentform.buttonDeleteOption.Enabled = true;
                        parentform.buttonViewReport.Enabled = true;
                        parentform.buttonViewLabel.Enabled = true;
                        parentform.buttonViewMainPdf.Enabled = true;
                        //      parentform.buttonAttachPDF.Enabled = true;
                        break;
                    }
                case "Edit Cover":
                    {
                        EnableControls();
                        parentform.buttonEdit.Enabled = false;
                        parentform.buttonClear.Enabled = false;
                        parentform.buttonCreateNewOption.Enabled = false;
                        parentform.buttonViewReport.Enabled = false;
                        parentform.buttonViewMainPdf.Enabled = true;
                        parentform.buttonAttachPDF.Enabled = false;
                        parentform.buttonRoute.TabStop = false;
                        parentform.buttonClear.TabStop = false;

                        parentform.textBoxCompany.Focus();
                        break;
                    }
            }
            parentform.buttonClose.Enabled = true;
            parentform.Update();
        }

        public void EnableControls()
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in parentform.Controls)
            {
                c.Enabled = true;
                foreach (Control d in c.Controls)
                {
                    if (d is TabPage)
                        foreach (Control ctl in d.Controls)
                        {
                            ctl.Enabled = true;
                        }
                    else
                    {
                        d.Enabled = true;
                    }
                }
            }
        }

        public void DisableControls()
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in parentform.Controls)
            {
                c.Enabled = false;
                foreach (Control d in c.Controls)
                    if (d is TabPage)
                        foreach (Control ctl in d.Controls)
                        {
                            if (ctl is Label)
                            {
                                ctl.Enabled = true;
                            }
                            else
                            {
                                ctl.Enabled = false;
                            }
                        }
                    else
                    {
                        d.Enabled = false;
                    }
            }
            // Close is always enabled
            parentform.buttonClose.Enabled = true;
        }

        public void SetRecDate(object sender, EventArgs e)
        {
            inspds.inspmast[0].recdate = parentform.dateTimePickerRecdate.Value;
            inspds.inspmast.AcceptChanges();
            parentform.Update();
        }

        private void ProcessSo(string version, string cover)
        {
            parentform.dateTimePickerRecdate.Value = inspds.inspmast[0].recdate;
            RefreshCover();
        }

        #region Set Bindings

        public void SetBindings()
        {
            // Customer
            parentform.textBoxCustno.DataBindings.Add("Text", ards.arcust, "custno");
            parentform.textBoxEmail.DataBindings.Add("Text", ards.arcust, "email");
            parentform.textBoxPhone.DataBindings.Add("Text", ards.arcust, "phone");
            parentform.textBoxCompany.DataBindings.Add("Text", ards.arcust, "company");
            parentform.textBoxFaxNo.DataBindings.Add("Text", ards.arcust, "faxno");
            // SO Head
            parentform.textBoxSoNo.DataBindings.Add("Text", somastds.somast, "sono");
            parentform.textBoxMeycono.DataBindings.Add("Text", somastds.somast, "meycono");
            parentform.textBoxOldPlan.DataBindings.Add("Text", somastds.somast, "oldplan");
            // Inspection
            parentform.textBoxFrtTerms.DataBindings.Add("text", inspds.inspmast, "frtterms");
            parentform.textBoxSprings.DataBindings.Add("text", inspds.inspmast, "springs");
            parentform.textBoxStowBag.DataBindings.Add("text", inspds.inspmast, "stowbag");
            parentform.textBoxSpringCovers.DataBindings.Add("text", inspds.inspmast, "springcovers");
            parentform.textBoxPumps.DataBindings.Add("text", inspds.inspmast, "pumps");
            parentform.textBoxInstructions.DataBindings.Add("text", inspds.inspmast, "instructions");
            parentform.textBoxPhotos.DataBindings.Add("text", inspds.inspmast, "photos");
            parentform.textBoxNotes.DataBindings.Add("text", inspds.inspmast, "notes");
            parentform.textBoxRecdate.DataBindings.Add("text", inspds.inspmast, "recdate");
            parentform.textBoxReceivedBy.DataBindings.Add("text", inspds.inspmast, "receivedby");
            parentform.textBoxDescrip.DataBindings.Add("text", inspds.inspmast, "descrip");
            parentform.textBoxCwidft.DataBindings.Add("text", inspds.inspmast, "cwidft");
            parentform.textBoxCwidin.DataBindings.Add("text", inspds.inspmast, "cwidin");
            parentform.textBoxClenft.DataBindings.Add("text", inspds.inspmast, "clenft");
            parentform.textBoxClenin.DataBindings.Add("text", inspds.inspmast, "clenin");
            parentform.textBoxX1widft.DataBindings.Add("text", inspds.inspmast, "x1widft");
            parentform.textBoxX1widin.DataBindings.Add("text", inspds.inspmast, "x1widin");
            parentform.textBoxX1lenft.DataBindings.Add("text", inspds.inspmast, "x1lenft");
            parentform.textBoxX1lenin.DataBindings.Add("text", inspds.inspmast, "x1lenin");
            parentform.textBoxX2widft.DataBindings.Add("text", inspds.inspmast, "x2widft");
            parentform.textBoxX2widin.DataBindings.Add("text", inspds.inspmast, "x2widin");
            parentform.textBoxX2lenft.DataBindings.Add("text", inspds.inspmast, "x2lenft");
            parentform.textBoxX2lenin.DataBindings.Add("text", inspds.inspmast, "x2lenin");
            parentform.textBoxX3widft.DataBindings.Add("text", inspds.inspmast, "x3widft");
            parentform.textBoxX3widin.DataBindings.Add("text", inspds.inspmast, "x3widin");
            parentform.textBoxX3lenft.DataBindings.Add("text", inspds.inspmast, "x3lenft");
            parentform.textBoxX3lenin.DataBindings.Add("text", inspds.inspmast, "x3lenin");
            parentform.textBoxX4widft.DataBindings.Add("text", inspds.inspmast, "x4widft");
            parentform.textBoxX4widin.DataBindings.Add("text", inspds.inspmast, "x4widin");
            parentform.textBoxX4lenft.DataBindings.Add("text", inspds.inspmast, "x4lenft");
            parentform.textBoxX4lenin.DataBindings.Add("text", inspds.inspmast, "x4lenin");
            parentform.textBoxStraps.DataBindings.Add("text", inspds.inspmast, "straps");
            parentform.textBoxCoverMeasured.DataBindings.Add("text", inspds.inspmast, "covermeasured");
            parentform.textBoxObservations.DataBindings.Add("text", inspds.inspmast, "observations");
            parentform.textBoxRecommendation.DataBindings.Add("text", inspds.inspmast, "recommendation");
            parentform.textBoxInspectedBy.DataBindings.Add("text", inspds.inspmast, "inspectedby");
            parentform.textBoxVersion.DataBindings.Add("text", inspds.inspversion, "version");
            parentform.textBoxCommlRes.DataBindings.Add("text", inspds.inspmast, "commlres");
            parentform.textBoxCleanliness.DataBindings.Add("text", inspds.inspmast, "cleanliness");
            parentform.listBoxFeatures.ValueMember = "code";
            parentform.listBoxFeatures.DisplayMember = "feature";
            parentform.listBoxFeatures.DataSource = dtfeatures;
            parentform.comboBoxMaterial.ValueMember = "idcol";
            parentform.comboBoxMaterial.DisplayMember = "descrip";
            parentform.comboBoxMaterial.DataSource = soreferenceds.view_qumaterialdata;
            parentform.comboBoxColor.ValueMember = "idcol";
            parentform.comboBoxColor.DisplayMember = "descrip";
            parentform.comboBoxColor.DataSource = soreferenceds.view_qucolordata;
            parentform.comboBoxSpacing.ValueMember = "idcol";
            parentform.comboBoxSpacing.DisplayMember = "descrip";
            parentform.comboBoxSpacing.DataSource = soreferenceds.view_quspacingdata;
            //  Feature Selector grid
            parentform.dataGridViewFeatureSelector.AutoGenerateColumns = false;
            parentform.dataGridViewFeatureSelector.DataSource = bindingFeatureSelector;
            bindingFeatureSelector.DataSource = featureds.view_immasterdata;
            bindingFeatureSelector.Sort = "shortdescrip";
            parentform.dataGridViewFeatureSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentform.dataGridViewFeatureSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            // Selected Items grid
            parentform.dataGridViewSelectedItems.AutoGenerateColumns = false;
            parentform.dataGridViewSelectedItems.DataSource = bindingSelectedItems;
            bindingSelectedItems.DataSource = socurrentitemsds.inspline;
            parentform.dataGridViewSelectedItems.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentform.dataGridViewSelectedItems.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            // Reference comboboxes
            parentform.comboBoxMaterialCondition.DataSource = matcondds.view_sysreference;
            parentform.comboBoxMaterialCondition.DisplayMember = "refdescrip";
            parentform.comboBoxMaterialCondition.ValueMember = "idcol";
            parentform.comboBoxThreadCondition.DataSource = threadcondds.view_sysreference;
            parentform.comboBoxThreadCondition.DisplayMember = "refdescrip";
            parentform.comboBoxThreadCondition.ValueMember = "idcol";
            parentform.comboBoxSpringsCondition.DataSource = springscondds.view_sysreference;
            parentform.comboBoxSpringsCondition.DisplayMember = "refdescrip";
            parentform.comboBoxSpringsCondition.ValueMember = "idcol";
            parentform.comboBoxWebCondition.DataSource = webcondds.view_sysreference;
            parentform.comboBoxWebCondition.DisplayMember = "refdescrip";
            parentform.comboBoxWebCondition.ValueMember = "idcol";
            parentform.comboBoxManufacturer.DataSource = manufacturerds.view_sysreference;
            parentform.comboBoxManufacturer.DisplayMember = "refdescrip";
            parentform.comboBoxManufacturer.ValueMember = "idcol";
            parentform.comboBoxInbCarrier.DataSource = inbcarrierds.view_sysreference;
            parentform.comboBoxInbCarrier.DisplayMember = "refdescrip";
            parentform.comboBoxInbCarrier.ValueMember = "idcol";
            parentform.comboBoxLocation.DataSource = locationds.view_sysreference;
            parentform.comboBoxLocation.DisplayMember = "refdescrip";
            parentform.comboBoxLocation.ValueMember = "idcol";

            // Shipto Information
            parentform.textBoxShiptocompany.DataBindings.Add("text", somastds.soaddr, "company");
            parentform.textBoxShiptoaddress1.DataBindings.Add("Text", somastds.soaddr, "address1");
            parentform.textBoxShiptoaddress2.DataBindings.Add("Text", somastds.soaddr, "address2");
            parentform.textBoxShiptocity.DataBindings.Add("Text", somastds.soaddr, "city");
            parentform.textBoxShiptostate.DataBindings.Add("Text", somastds.soaddr, "state");
            parentform.textBoxShiptozip.DataBindings.Add("Text", somastds.soaddr, "zip");
            // Pool Owner Information
            parentform.textBoxFname.DataBindings.Add("Text", somastds.somast, "fname");
            parentform.textBoxLname.DataBindings.Add("Text", somastds.somast, "lname");
            parentform.textBoxAddress.DataBindings.Add("Text", somastds.somast, "address");
            parentform.textBoxCity.DataBindings.Add("Text", somastds.somast, "city");
            parentform.textBoxState.DataBindings.Add("Text", somastds.somast, "state");
            parentform.textBoxZip.DataBindings.Add("Text", somastds.somast, "zip");
        }

        #endregion Set Bindings

        public void ProcessSOKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.LoadSono(parentform.textBoxSoNo.Text.TrimEnd().TrimStart().PadLeft(10));
            }
        }

        public void GetSo(object sender, EventArgs e)
        {
            FrmSOSearch frmSoSearch = new FrmSOSearch(parentform.textBoxSoNo.Text);
            frmSoSearch.ShowDialog();
            if (frmSoSearch.SelectedSono.TrimEnd() != "")
            {
                this.LoadSono(frmSoSearch.SelectedSono.TrimEnd());
            }
        }

        private void LoadSono(string sono)
        {
            this.CurrentSono = sono;
            LoadInspection(sono);
            if (CurrentSomastid > 0)
            {
                CurrentState = "View";
                ProcessSo("", "");
                RefreshParentControls();
            }
            else
            {
                wsgUtilities.wsgNotice("Sales Order Not Found. Click Find to Search");
                CurrentState = "Select";
                RefreshParentControls();
            }
        }

        public void SetEditState(object sender, EventArgs e)
        {
            string editstatus = LockSomast(somastds.somast[0].idcol);
            if (editstatus == "OK")
            {
                CurrentState = "Edit Cover";
                RefreshParentControls();
            }
            else
            {
                wsgUtilities.wsgNotice(editstatus);
            }
        }

        public void SetFrtTerms(object sender, EventArgs e)
        {
            if (LoadingCover != true)
            {
                inspds.inspmast[0].frtterms = parentform.comboBoxFrtTerms.SelectedItem.ToString();
                inspds.inspmast.AcceptChanges();
                RefreshCover();
            }
        }

        public void SetCommlRes(object sender, EventArgs e)
        {
            if (LoadingCover != true)
            {
                inspds.inspmast[0].commlres = parentform.comboBoxCommlRes.SelectedItem.ToString().Substring(0, 1);
                inspds.inspmast.AcceptChanges();
                RefreshCover();
            }
        }

        public void SetCleanliness(object sender, EventArgs e)
        {
            if (LoadingCover != true)
            {
                inspds.inspmast[0].cleanliness = parentform.comboBoxCleanliness.SelectedItem.ToString().Substring(0, 1);
                inspds.inspmast.AcceptChanges();
                RefreshCover();
            }
        }

        public void SetRecommendation(object sender, EventArgs e)
        {
            if (LoadingCover != true)
            {
                inspds.inspmast[0].recommendation = parentform.comboBoxRecommendation.SelectedItem.ToString();
                inspds.inspmast.AcceptChanges();
                RefreshCover();
            }
        }

        public void SetMaterial(object sender, EventArgs e)
        {
            if (LoadingCover != true)
            {
                inspds.inspmast[0].materialid = Convert.ToInt32(parentform.comboBoxMaterial.SelectedValue);
                inspds.inspmast.AcceptChanges();
                RefreshCover();
            }
        }

        public void SetColor(object sender, EventArgs e)
        {
            if (LoadingCover != true)
            {
                inspds.inspmast[0].colorid = Convert.ToInt32(parentform.comboBoxColor.SelectedValue);
                inspds.inspmast.AcceptChanges();
                RefreshCover();
            }
        }

        public void ShowForm(object sender, EventArgs e)
        {
            if (PassedSono.TrimEnd() != "")
            {
                LoadInspection(PassedSono);
                ProcessSo("", "");
                CurrentState = "View";
                RefreshParentControls();
            }
        }

        public void SetSpacing(object sender, EventArgs e)
        {
            if (LoadingCover != true)
            {
                inspds.inspmast[0].spacingid = Convert.ToInt32(parentform.comboBoxSpacing.SelectedValue);
                inspds.inspmast.AcceptChanges();
                RefreshCover();
            }
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

        public bool Checkinches(string stringinches)
        {
            Decimal inches = 0;
            bool Inchesok = true;
            try
            {
                inches = Convert.ToDecimal(stringinches);
            }
            catch
            {
                inches = 0;
            }
            if (inches > 11)
            {
                wsgUtilities.wsgNotice("Please enter a number less than 12");
                Inchesok = false;
            }
            return Inchesok;
        }

        public void CheckTextBoxInches(object sender, CancelEventArgs e)
        {
            if (Checkinches(((TextBox)sender).Text) != true)
            {
                e.Cancel = true;
            }
        }

        public void CancelEdit(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Abandon Edit") == true)
            {
                if (CurrentState != "Insert")
                {
                    UnlockSomast(somastds.somast[0].idcol);
                }
                inspds.inspmast.Rows.Clear();
                somastds.somast.Rows.Clear();
                ards.arcust.Rows.Clear();
                soitemsds.inspline.Rows.Clear();
                socurrentitemsds.inspline.Rows.Clear();
                CurrentState = "Select";
                RefreshParentControls();
                this.LoadSono(this.CurrentSono);
            }
        }

        public void CloseForm(object sender, EventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        UnlockSomast(somastds.somast[0].idcol);
                        CurrentState = "Select";
                        parentform.Close();
                    }
                }
            }
            else
            {
                CurrentState = "Select";
                parentform.Close();
            }
        }

        public void CreateReferenceTables()
        {
            FillReferenceTable(manufacturerds, "Manufacturers");
            FillReferenceTable(locationds, "Locations");
            FillReferenceTable(webcondds, "Web Condition");
            FillReferenceTable(threadcondds, "Thread Condition");
            FillReferenceTable(matcondds, "Material Condition");
            FillReferenceTable(springscondds, "Springs Condition");
            FillReferenceTable(inbcarrierds, "Carriers");
        }

        private void FillReferenceTable(system ds, string GroupName)
        {
            ds.view_sysreference.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@groupname", GroupName, "SQL");
            this.FillData(ds, "view_sysreference", "wsgsp_getsysreferencesbyname", CommandType.StoredProcedure);
        }

        public void SetMaterialCondition(object sender, EventArgs e)
        {
            inspds.inspmast[0].matcond = Convert.ToInt32(parentform.comboBoxMaterialCondition.SelectedValue);
            SetReferenceTextboxes();
        }

        public void SetWebCondition(object sender, EventArgs e)
        {
            inspds.inspmast[0].webcond = Convert.ToInt32(parentform.comboBoxWebCondition.SelectedValue);
            SetReferenceTextboxes();
        }

        public void SetThreadCondition(object sender, EventArgs e)
        {
            inspds.inspmast[0].threadcond = Convert.ToInt32(parentform.comboBoxThreadCondition.SelectedValue);
            SetReferenceTextboxes();
        }

        public void SetSpringsCondition(object sender, EventArgs e)
        {
            inspds.inspmast[0].springcond = Convert.ToInt32(parentform.comboBoxSpringsCondition.SelectedValue);
            SetReferenceTextboxes();
        }

        public void SetManufacturer(object sender, EventArgs e)
        {
            inspds.inspmast[0].manufacturer = Convert.ToInt32(parentform.comboBoxManufacturer.SelectedValue);
            SetReferenceTextboxes();
        }

        public void SetLocation(object sender, EventArgs e)
        {
            inspds.inspmast[0].locationid = Convert.ToInt32(parentform.comboBoxLocation.SelectedValue);
            SetReferenceTextboxes();
        }

        public void SetInbCarrier(object sender, EventArgs e)
        {
            inspds.inspmast[0].inbcarrierid = Convert.ToInt32(parentform.comboBoxInbCarrier.SelectedValue);
            SetReferenceTextboxes();
        }

        public void SetReferenceTextboxes()
        {
            parentform.textBoxMaterialCondition.Text = GetReferenceDescription(matcondds, inspds.inspmast[0].matcond);
            parentform.textBoxWebCondition.Text = GetReferenceDescription(webcondds, inspds.inspmast[0].webcond);
            parentform.textBoxSpringsCondition.Text = GetReferenceDescription(springscondds, inspds.inspmast[0].springcond);
            parentform.textBoxThreadCondition.Text = GetReferenceDescription(threadcondds, inspds.inspmast[0].threadcond);
            parentform.textBoxManufacturer.Text = GetReferenceDescription(manufacturerds, inspds.inspmast[0].manufacturer);
            parentform.textBoxInbCarrier.Text = GetReferenceDescription(inbcarrierds, inspds.inspmast[0].inbcarrierid);
            parentform.textBoxLocation.Text = GetReferenceDescription(locationds, inspds.inspmast[0].locationid);
        }

        public string GetReferenceDescription(system ds, int idcol)
        {
            string Descrip = "Unknown";
            if (idcol > 0)
            {
                string strExpr = "idcol = " + idcol.ToString().TrimStart().TrimEnd();
                // Use the Select method to find the row matching the filter.
                DataRow[] foundRow = ds.view_sysreference.Select(strExpr);
                Descrip = foundRow[0]["refdescrip"].ToString();
            }
            return Descrip;
        }

        public void ProcessCover(string NewVersion)
        {
            // Locate the version view data for the current sono
            LoadVersionViewData(inspds.inspmast[0].sono);

            if (inspds.view_inspversiondata.Rows.Count == 0)
            {
                EstablishBlankDataTableRow(inspds.inspversion);
                if (NewVersion == "")
                {
                    inspds.inspversion[0].version = "A";
                    CurrentVersion = "A";
                }
                else
                {
                    // Creating a new version with no prior versions
                    inspds.inspversion[0].version = NewVersion;
                    CurrentVersion = NewVersion;
                }
            }
            else
            {
                // There are existing versions
                if (NewVersion == "")
                {
                    // If not targeting a version, use the first version
                    CurrentVersion = inspds.view_inspversiondata[0].version;
                    inspds.inspversion.Rows.Clear();
                    inspds.inspversion.ImportRow(inspds.view_inspversiondata.Rows[0]);
                }
                else
                {
                    DataRow[] foundRows;
                    // Retrieve Version Row from view
                    foundRows = inspds.view_inspversiondata.Select("version = '" + NewVersion + "'");
                    if (foundRows.Length != 0)
                    {
                        inspds.inspversion.Rows.Clear();
                        inspds.inspversion.ImportRow(foundRows[0]);
                        CurrentVersion = NewVersion;
                    }
                    else
                    {
                        EstablishBlankDataTableRow(inspds.inspversion);
                        inspds.inspversion[0].version = NewVersion;
                        CurrentVersion = NewVersion;
                    }
                }
                LoadMiscellandousInspLineData(CurrentVersion);
            }
        }

        public void SaveInspVersion()
        {
            inspds.inspversion[0].sono = inspds.inspmast[0].sono;
            GenerateAppTableRowSave(inspds.inspversion[0]);
        }

        private void SetFeatures(object sender, EventArgs e)
        {
            if (LoadingCover != true)
            {
                // Initialize the search key
                Featurekey = "";
                // Load the features table and establish the current feature
                CurrentFeature = GetFeatures(parentform.listBoxFeatures.SelectedValue.ToString(), CurrentFeature);
                parentform.dataGridViewFeatureSelector.Focus();
            }
        }

        public string GetFeatures(string featurecode, string CurrentFeature)
        {
            // Save any line items from a previous feature
            CombineCurrentItems();

            // Load any prior items for this feature
            socurrentitemsds.inspline.Rows.Clear();
            for (int r = 0; r <= soitemsds.inspline.Rows.Count - 1; r++)
            {
                if (soitemsds.inspline[r].source.Substring(0, 2) == featurecode)
                {
                    socurrentitemsds.inspline.ImportRow(soitemsds.inspline.Rows[r]);
                }
            }
            socurrentitemsds.AcceptChanges();
            // Populate the feature selector table
            featureds.view_immasterdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@code", featurecode, "SQL");
            this.FillData(featureds, "view_immasterdata", "wsgsp_getquoteitem", CommandType.StoredProcedure);
            return featurecode;
        }

        public void CombineCurrentItems()
        {
            // Save any line items from the current feature
            if (socurrentitemsds.inspline.Rows.Count > 0)
            {
                string CurrentFeature = socurrentitemsds.inspline[0].source.Substring(0, 2);
                //If there are items for that feature, delete them first
                foreach (DataRow row in soitemsds.inspline)
                {
                    string source = row["source"].ToString().Substring(0, 2);
                    if (source == CurrentFeature)
                    {
                        row.Delete();
                    }
                }
            }
            soitemsds.AcceptChanges();
            for (int r1 = 0; r1 <= socurrentitemsds.inspline.Rows.Count - 1; r1++)
            {
                soitemsds.inspline.ImportRow(socurrentitemsds.inspline.Rows[r1]);
            }
            soitemsds.AcceptChanges();

            // Clear the current items table
            socurrentitemsds.inspline.Rows.Clear();
            socurrentitemsds.AcceptChanges();
        }

        public void CaptureSelectedFeatureItem(object sender, DataGridViewCellEventArgs e)
        {
            CurrentItem = CaptureDataGridColumn(parentform.dataGridViewFeatureSelector, "item");

            GetInspItemData(CurrentItem, CurrentFeature);
        }

        public void GetInspItemData(string item, string CurrentFeature)
        {
            string strExpr = "item =  '" + item.TrimStart().TrimEnd() + "'";
            // Use the Select method to find all rows matching the filter.
            DataRow[] foundItemRows =
            featureds.view_immasterdata.Select(strExpr);
            SetInsplineItem(foundItemRows[0]["item"].ToString(), foundItemRows[0]["misccode"].ToString(),
               inspds.inspversion[0].version);
        }

        private void SaveLineItems(object sender, EventArgs e)
        {
            if (socurrentitemsds.inspline.Rows.Count > 0)
            {
                CombineCurrentItems();
                // Clear the current items table rows
                featureds.view_immasterdata.Rows.Clear();
                featureds.view_immasterdata.AcceptChanges();
                CurrentFeature = "";
                RefreshParentControls();
            }
        }

        private void CancelLineItems(object sender, EventArgs e)
        {
            // Clear the current items table rows
            socurrentitemsds.inspline.Rows.Clear();
            socurrentitemsds.AcceptChanges();
            featureds.view_immasterdata.Rows.Clear();
            featureds.view_immasterdata.AcceptChanges();
            CurrentFeature = "";
        }

        public void LoadVersionViewData(string sono)
        {
            inspds.view_inspversiondata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(inspds, "view_inspversiondata", "wsgsp_getinspversions", CommandType.StoredProcedure);
        }

        public void SaveInspLineitems()
        {
            CombineCurrentItems();
            SetInsplineItem("RINSP", "RM", inspds.inspversion[0].version);
            SetInsplineItem("R0ADD", "RM", inspds.inspversion[0].version);
            if (inspds.inspmast[0].commlres == "C")
            {
                SetInsplineItem("RCOMM", "RM", inspds.inspversion[0].version);
            }
            CombineCurrentItems();
            foreach (DataRow row in soitemsds.inspline)
            {
                row["sono"] = somastds.somast[0].sono;
                GenerateAppTableRowSave(row, excludeColumns: new string[] { "prwcov" });
            }

            // Locate any items that were in the orginal items and are no longer present, and delete them
            foreach (DataRow row in dtInsplineOriginal.Rows)
            {
                string strExpr = "idcol = " + row["idcol"].ToString().TrimStart().TrimEnd();
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundinspline = soitemsds.inspline.Select(strExpr);
                if (foundinspline.Length < 1)
                {
                    // The original item no longer exists. Delete it from the source table
                    this.ClearParameters();
                    this.AddParms("@idcol", row["idcol"], "SQL");
                    try
                    {
                        ExecuteCommand("wsgsp_deleteinspline", CommandType.StoredProcedure);
                    }
                    catch (SqlException ex)
                    {
                        HandleException(ex);
                    }
                }
            }
        }

        public void LoadMiscellandousInspLineData(string version)
        {
            soitemsds.inspline.Rows.Clear();
            socurrentitemsds.inspline.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", somastds.somast[0].sono, "SQL");
            this.AddParms("@version", version, "SQL");
            this.FillData(soitemsds, "inspline", "wsgsp_findMiscellaneousInsplineData", CommandType.StoredProcedure);

            // Copy idcolumn of items to holding table. We'll use that for tracking deletions
            DataTable dt = new DataTable();
            dt.Columns.Add("idcol", typeof(Int32));
            foreach (DataRow row in soitemsds.inspline)
            {
                dt.ImportRow(row);
            }
            dtInsplineOriginal = dt;
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

        private void DeleteSelectedItem(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (wsgUtilities.wsgReply("Delete this item?"))
                {
                    CurrencyManager xCM =
                   (CurrencyManager)parentform.dataGridViewSelectedItems.BindingContext[parentform.dataGridViewSelectedItems.DataSource,
                    parentform.dataGridViewSelectedItems.DataMember];
                    DataRowView xDRV = (DataRowView)xCM.Current;
                    DataRow xRow = xDRV.Row;
                    string item = (string)xRow["item"];
                    // Delete any similar items in the soitemsds.inspline table
                    //If there are items for that feature, delete them first
                    foreach (DataRow row in soitemsds.inspline)
                    {
                        if (item == (string)row["item"])
                        {
                            row.Delete();
                        }
                    }
                    soitemsds.inspline.AcceptChanges();
                    xDRV.Row.Delete();
                    socurrentitemsds.inspline.AcceptChanges();
                    parentform.Update();
                }
            }
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        UnlockSomast(somastds.somast[0].idcol);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        public void CreateNewVersion(object sender, EventArgs e)
        {
            string NewVersion = "";
            LoadVersionViewData(somastds.somast[0].sono);
            if (inspds.view_inspversiondata.Rows.Count > 0)
            {
                // Find the last row in the version view and increment that version
                int lastrow = inspds.view_inspversiondata.Rows.Count - 1;
                char c = Convert.ToChar(inspds.view_inspversiondata[lastrow].version);
                c++;
                NewVersion = c.ToString();
                CurrentVersion = NewVersion;
            }
            else
            {
                NewVersion = "A";
            }
            ProcessCover(NewVersion);
            CurrentState = "Edit Cover";
            RefreshParentControls();
        }

        public void SaveSomastData()
        {
            GenerateAppTableRowSave(somastds.somast[0]);
            // Ship to address
            GenerateAppTableRowSave(somastds.soaddr[0]);
        }

        public void ActivateVersion(object sender, EventArgs e)
        {
            string SelectedVersion = SelectVersion();
            if (SelectedVersion.TrimEnd() != "")
            {
                ProcessCover(SelectedVersion);
                RefreshParentControls();
            }
        }

        public void DeleteVersion(object sender, EventArgs e)
        {
            string SelectedVersion = SelectVersion();
            if (SelectedVersion.TrimEnd() != "")
            {
                if (wsgUtilities.wsgReply("Delete This Option?") == true)
                {
                    this.ClearParameters();
                    this.AddParms("@sono", somastds.somast[0].sono, "SQL");
                    this.AddParms("@version", SelectedVersion, "SQL");
                    try
                    {
                        ExecuteCommand("wsgsp_deleteallinspversiondata", CommandType.StoredProcedure);
                    }
                    catch (SqlException ex)
                    {
                        HandleException(ex);
                    }
                    wsgUtilities.wsgNotice("Version Deleted.");
                    RefreshParentControls();
                }
            }
        }

        public string SelectVersion()
        {
            FrmInspVersionSelector frmInspVersionSelector = new FrmInspVersionSelector();
            frmInspVersionSelector.versionSelector = new VersionSelector("SQL", "SQLConnString", frmInspVersionSelector);
            frmInspVersionSelector.versionSelector.CurrentSono = somastds.somast[0].sono;
            frmInspVersionSelector.versionSelector.SelectedVersion = "";
            frmInspVersionSelector.versionSelector.LoadVersionViewData();
            frmInspVersionSelector.versionSelector.SetBindings();
            frmInspVersionSelector.versionSelector.SetEvents();
            frmInspVersionSelector.ShowDialog();
            return frmInspVersionSelector.versionSelector.SelectedVersion;
        }

        public void GetFileNumber()
        {
            appinfods.appinfo.Clear();
            this.ClearParameters();
            this.FillData(appinfods, "appinfo", "wsgsp_getappinfo", CommandType.StoredProcedure);
            somastds.somast[0].meycono = "R" + DateTime.Now.ToString("yy") + "-"
             + appinfods.appinfo[0].nextplannumber.ToString().TrimStart().TrimEnd();
            this.ClearParameters();
            this.AddParms("@nextplannumber", appinfods.appinfo[0].nextplannumber + 1, "SQL");
            try
            {
                ExecuteCommand("wsgsp_updatenextplannumber", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void ViewLabel(object sender, EventArgs e)
        {
            EstablishReportData();
            FrmWSGDocumentViewer frmWSGDocumentViewer = new FrmWSGDocumentViewer();
            InspLabel insprpt = new InspLabel();
            insprpt.SetDataSource(inspds);
            frmWSGDocumentViewer.crystalReportViewerWSG.ReportSource = insprpt;
            frmWSGDocumentViewer.ShowDialog();
        }

        public void ViewReport(object sender, EventArgs e)
        {
            EstablishReportData();
            FrmWSGDocumentViewer frmWSGDocumentViewer = new FrmWSGDocumentViewer();
            InspectionRpt insprpt = new InspectionRpt();
            insprpt.SetDataSource(inspds);
            frmWSGDocumentViewer.crystalReportViewerWSG.ReportSource = insprpt;
            frmWSGDocumentViewer.ShowDialog();
        }

        private void EstablishReportData()
        {
            inspds.view_inspreport.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", somastds.somast[0].sono, "SQL");
            this.FillData(inspds, "view_inspreport", "wsgsp_getinspreportdata", CommandType.StoredProcedure);
            // somast
            inspds.somast.Rows.Clear();
            inspds.somast.ImportRow(somastds.somast.Rows[0]);
            // soaddr
            inspds.soaddr.Rows.Clear();
            inspds.soaddr.ImportRow(somastds.soaddr.Rows[0]);
            // arcust
            inspds.arcust.Rows.Clear();
            inspds.arcust.ImportRow(ards.arcust.Rows[0]);
        }

        public void HideComboBox(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.Visible = false;
            parentform.Update();
        }

        public void ShowComboBox(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string SenderName = tb.Name.ToUpper();
            switch (SenderName)
            {
                case "TEXTBOXMATERIALCONDITION":
                    {
                        parentform.comboBoxMaterialCondition.Visible = true;
                        break;
                    }
                case "TEXTBOXSPRINGSCONDITION":
                    {
                        parentform.comboBoxSpringsCondition.Visible = true;
                        break;
                    }
                case "TEXTBOXINBCARRIER":
                    {
                        parentform.comboBoxInbCarrier.Visible = true;
                        break;
                    }
                case "TEXTBOXMANUFACTURER":
                    {
                        parentform.comboBoxManufacturer.Visible = true;
                        break;
                    }
                case "TEXTBOXLOCATION":
                    {
                        parentform.comboBoxLocation.Visible = true;
                        break;
                    }

                case "TEXTBOXTHREADCONDITION":
                    {
                        parentform.comboBoxThreadCondition.Visible = true;
                        break;
                    }
                case "TEXTBOXWEBCONDITION":
                    {
                        parentform.comboBoxWebCondition.Visible = true;
                        break;
                    }
                case "TEXTBOXCOLOR":
                    {
                        parentform.comboBoxColor.Visible = true;
                        break;
                    }
                case "TEXTBOXMATERIAL":
                    {
                        parentform.comboBoxMaterial.Visible = true;
                        break;
                    }
                case "TEXTBOXSPACING":
                    {
                        parentform.comboBoxSpacing.Visible = true;
                        break;
                    }
                case "TEXTBOXCOMMLRES":
                    {
                        parentform.comboBoxCommlRes.Visible = true;
                        break;
                    }
                case "TEXTBOXCLEANLINESS":
                    {
                        parentform.comboBoxCleanliness.Visible = true;
                        break;
                    }
                case "TEXTBOXRECOMMENDATION":
                    {
                        parentform.comboBoxRecommendation.Visible = true;
                        break;
                    }
                case "TEXTBOXFRTTERMS":
                    {
                        parentform.comboBoxFrtTerms.Visible = true;
                        break;
                    }
            }
            parentform.Update();
        }

        private void AttachPDF(object sender, EventArgs e)
        {
        }

        private bool MakeReportPDF()
        {
            string fileName = "INS" + somastds.somast[0].sono.TrimStart() + ".pdf";
            string filePath = System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], fileName);
            bool pdfOk = true;

            if (appUtilities.IsFileOpen(filePath) == false)
            {
                EstablishReportData();
                InspectionRpt insprpt = new InspectionRpt();
                insprpt.SetDataSource(inspds);
                try
                {
                    PdfStorage.WriteFileFromReport(insprpt, fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                wsgUtilities.wsgNotice("The PDF is in use. Close it and retry.");
                pdfOk = false;
            }
            return pdfOk;
        }

        public void ProcessFeatureSelectorKey(object sender, KeyEventArgs e)
        {
            int ix = 0;
            switch (e.KeyCode)
            {
                case Keys.Return:
                    {
                        CurrentItem = CaptureDataGridColumn(parentform.dataGridViewFeatureSelector, "item");
                        GetInspItemData(CurrentItem, CurrentFeature);
                        break;
                    }
                case Keys.Home:
                    {
                        Featurekey = "";
                        parentform.dataGridViewFeatureSelector.CurrentCell = parentform.dataGridViewFeatureSelector.Rows[0].Cells[0];
                        break;
                    }
                default:
                    {
                        if (Featurekey != null && Featurekey.Length > 4)
                        {
                            Featurekey = "";
                        }
                        Featurekey += Convert.ToChar(e.KeyCode).ToString().ToUpper();
                        while (ix < parentform.dataGridViewFeatureSelector.RowCount - 1)
                        {
                            string x = parentform.dataGridViewFeatureSelector.Rows[ix].Cells[0].Value.ToString().ToUpper();
                            if (x.Substring(0, Featurekey.Length) == Featurekey)
                            {
                                parentform.dataGridViewFeatureSelector.CurrentCell = parentform.dataGridViewFeatureSelector.Rows[ix].Cells[0];
                                break;
                            }
                            else
                            {
                                ix++;
                                continue;
                            }
                        }
                        break;
                    }
            }
            parentform.Update();
        }

        public void ViewMainPDF(object sender, EventArgs e)
        {
            // Locate the pdf for this SO
            DirectoryInfo pdfdir = new DirectoryInfo(ConfigurationManager.AppSettings["PDFSTORAGEPath"]);
            string sono = somastds.somast[0].sono.TrimStart();
            string filename = sono + ".pdf";
            FileInfo[] pdffiles = pdfdir.GetFiles(filename);
            if (pdffiles.Length > 0)
            {
                Process.Start(pdffiles[0].FullName);
            }
            else
            {
                wsgUtilities.wsgNotice($"There are no PDFs for {sono} found at {System.IO.Path.Combine(pdfdir.FullName, filename)}");
            }
        }

        public void RouteSO(object sender, EventArgs e)
        {
            string editstatus = LockSomast(somastds.somast[0].idcol);
            if (editstatus == "OK")
            {
                string trackingresult = trackingInf.RouteToNextStep(somastds.somast[0].sono);
            }
            else
            {
                wsgUtilities.wsgNotice(editstatus);
            }
            UnlockSomast(somastds.somast[0].idcol);
            parentform.labelTrackingData.Text = trackingInf.GetLastSOTrackingData(somastds.somast[0].sono);
        }

        public void SetInsplineItem(string item, string source, string version)
        {
            string itmdesc = "";
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

                //If there are lines that item, delete them first
                foreach (DataRow row in socurrentitemsds.inspline)
                {
                    string lineitem = (string)row["item"];
                    if (lineitem == item)
                    {
                        row.Delete();
                    }
                }
                socurrentitemsds.inspline.Rows.Add();
                int rowpointer = socurrentitemsds.inspline.Rows.Count - 1;
                InitializeDataTable(socurrentitemsds.inspline, rowpointer);
                socurrentitemsds.inspline[rowpointer].source = source;
                socurrentitemsds.inspline[rowpointer].descrip = itmdesc;
                socurrentitemsds.inspline[rowpointer].item = item;
                socurrentitemsds.inspline[rowpointer].version = version;
                socurrentitemsds.inspline[rowpointer].price = finditemds.immaster[0].regprice;
                socurrentitemsds.inspline[rowpointer].qtyord = 1;
            }
            socurrentitemsds.inspline.AcceptChanges();
        }
    } // class

    #endregion Inspection Information
} // Namespace