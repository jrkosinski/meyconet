using CommonAppClasses;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using WSGUtilitieslib;

namespace Estimating
{
    public partial class FrmSOHead : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");
        private CustomerTermsMethods customerTermsMethods = new CustomerTermsMethods();
        private AlereCodeMethods alereCodeMethods = new AlereCodeMethods();
        private ScrollingVersionsPanel versionsList; 

        // Create the App Information processing object
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");

        public MiscellaneousDataMethods miscDataMethods = new MiscellaneousDataMethods("SQL", "SQLConnString");

        // Create the SO Information processing object
        private BindingSource bindingResults = new BindingSource();

        private Soinf soinf = new Soinf("SQL", "SQLConnString");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");
        private BindingSource bindingFeatureSelector = new BindingSource();

        public Soinf Soinf { get { return this.soinf; } }

        public FrmSOHead()
        {
            soinf.parentform = this;
            CurrentState = "Select";
            //     soinf.CreateResultsTable();
            InitializeComponent();
            SetTabOrder();
            // Set the timer interval here
            // Time is in milliseconds 10,000 = 10 seconds
            timerSohead.Interval = 20000;
            LoadingLine = true;
            soinf.EstablishSoitemsTable();
            // Create the views for spacing, cover, material, color
            soinf.EstablishReferenceTables();
            // Set all bindings
            SetSoHeadBindings();
            SetCustomerBindings();
            SetShiptoBindings();
            soinf.EstablishBlankSoversionData();
            soinf.EstablishBlankSomastData();
            setVersionBindings();
            soinf.EstablishBlankCoverDataTables();
            setCoverBindings();
            SetDimensionBindings();
            CurrentFeature = String.Empty;
            PassedSono = String.Empty;
            dataGridViewResults.AutoGenerateColumns = false;
            bindingResults.DataSource = soinf.resultsds.soline;
            dataGridViewResults.DataSource = bindingResults;
            dataGridViewResults.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewResults.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            soinf.EstablishCurrentitemsTable();
            TabControlOrderEntry.SelectedTab = tabPageOrderData;
            textBoxShiptocompany.SelectionStart = 0;
            labelTrackingData.Text = "";
        }

        public int CurrentCustid { get; set; }
        public string CurrentCustno { get; set; }
        private string _currentVersion = String.Empty;
        private string _currentCover = String.Empty;
        public string CurrentVersion
        {
            get { return _currentVersion; }
            set
            {
                if (value != _currentVersion)
                {
                    _currentVersion = value;

                    if (_selectedVersionChanged != null)
                        _selectedVersionChanged.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public string CurrentCover
        {
            get { return _currentCover; }
            set
            {
                if (value != _currentCover)
                {
                    _currentCover = value;

                    if (_selectedCoverChanged != null)
                        _selectedCoverChanged.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public string CurrentState { get; set; }
        public string PassedSono { get; set; }
        public string CurrentSono { get; set; }
        public int CurrentSomastid { get; set; }
        public string CurrentItem { get; set; }

        public bool Quoting { get; set; }
        public bool NewOrder { get; set; }
        public bool CustomCover { get; set; }
        public bool StockCover { get; set; }
        public bool LoadingLine { get; set; }
        public string Featurekey { get; set; }
        public string CurrentFeature { get; set; }
        public bool IsEditing
        {
            get
            {
                return (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Enter Order");
            }
        }

        private EventHandler _selectedVersionChanged;
        public event EventHandler SelectedVersionChanged
        {
            add { _selectedVersionChanged += value; }
            remove { _selectedVersionChanged -= value; }
        }

        private EventHandler _selectedCoverChanged;
        public event EventHandler SelectedCoverChanged
        {
            add { _selectedCoverChanged += value; }
            remove { _selectedCoverChanged -= value; }
        }

        private void buttonSoHeadClose_Click(object sender, EventArgs e)
        {
            if (IsEditing)
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Enter Order")
                    {
                        soinf.UnlockSomast(soinf.somastds.somast[0].idcol);
                    }
                    this.CancelEdit(); 
                    // Note: We need the following line to prevent the closing event from duplicating the question
                    CurrentState = "View";
                    //this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void buttonGetCustomer_Click(object sender, EventArgs e)
        {
            GetCustomerMethods getCust = new GetCustomerMethods("SQL", "SQLConnString");
            CurrentCustid = getCust.SelectedCustid;
            CurrentCustid = getCust.SelectedCustid;
            if (getCust.SelectedCustid != 0)
            {
                soinf.getSingleCustomerData(CurrentCustid);
                InitializeOrder();
                CurrentState = "Enter Order";
                ProcessSo("", "");
            }
        }

        private void checkBoxDefaultCustTiers_Click(object sender, EventArgs e)
        {
            this.SetDepositTiersDefaultState(this.checkBoxDefaultCustTiers.Checked);
        }

        #region Set Bindings

        public void SetShiptoBindings()
        {
            textBoxShiptocompany.DataBindings.Add("Text", soinf.somastds.soaddr, "company");
            textBoxShiptoaddress1.DataBindings.Add("Text", soinf.somastds.soaddr, "address1");
            textBoxShiptoaddress2.DataBindings.Add("Text", soinf.somastds.soaddr, "address2");
            textBoxShiptocity.DataBindings.Add("Text", soinf.somastds.soaddr, "city");
            textBoxShiptostate.DataBindings.Add("Text", soinf.somastds.soaddr, "state");
            textBoxShiptozip.DataBindings.Add("Text", soinf.somastds.soaddr, "zip");
        }

        public void setVersionBindings()
        {
            textBoxVersion.DataBindings.Add("Text", soinf.somastds.soversion, "version");
            // Bind the Subtotal textbox
            SetTextBoxCurrencyBinding(textBoxSubtotal, soinf.somastds, "soversion.subtotal");
            // Bind the Shipping
            SetTextBoxCurrencyBinding(textBoxShipping, soinf.somastds, "soversion.shipping");
            // Bind the Additional Discount Rate
            SetTextBoxDollarsBinding(textBoxAdddiscrate, soinf.somastds, "soversion.adddiscrate");
            // Bind the Additional Discount Amount
            SetTextBoxCurrencyBinding(textBoxAdddisc, soinf.somastds, "soversion.adddisc");
            // Bind the Tax textbox
            SetTextBoxCurrencyBinding(textBoxTax, soinf.somastds, "soversion.tax");
            // Bind the Order total textbox
            SetTextBoxCurrencyBinding(textBoxOrdamt, soinf.somastds, "soversion.ordamt");
            // Bind the Deposit textbox
            SetTextBoxCurrencyBinding(textBoxDeposit, soinf.somastds, "soversion.depositreq");
            // Bind the manual shipping checkbox
            checkBoxManlship.DataBindings.Add("Checked", soinf.somastds.soversion, "manlship");
            // Bind the actual deposit
            SetTextBoxCurrencyBinding(textBoxDepositact, soinf.somastds, "soversion.depositact");
            // Bind the cover all covers totals
            SetTextBoxCurrencyBindingT(textBoxAllCoversTotal, soinf.dttotals, "allcovers");
            SetTextBoxCurrencyBindingT(textBoxThisCoverTotal, soinf.dttotals, "thiscover");
        }

        public void setCoverBindings()
        {
            textBoxCover.DataBindings.Add("Text", soinf.clineds.socover, "cover");
            textBoxProduct.DataBindings.Add("Text", soinf.clineds.socover, "product");
            textBoxTotalSqft.DataBindings.Add("Text", soinf.clineds.socover, "sqft");
            textBoxCoverDesc.DataBindings.Add("Text", soinf.clineds.socover, "descrip");
            // Establish Combobox sources
            comboBoxMaterial.ValueMember = "idcol";
            comboBoxMaterial.DisplayMember = "descrip";
            comboBoxMaterial.DataSource = soinf.soreferenceds.view_qumaterialdata;
            comboBoxColor.ValueMember = "idcol";
            comboBoxColor.DisplayMember = "descrip";
            comboBoxColor.DataSource = soinf.soreferenceds.view_qucolordata;
            comboBoxSpacing.ValueMember = "idcol";
            comboBoxSpacing.DisplayMember = "descrip";
            comboBoxSpacing.DataSource = soinf.soreferenceds.view_quspacingdata;
            comboBoxOverlap.ValueMember = "idcol";
            comboBoxOverlap.DisplayMember = "descrip";
            comboBoxOverlap.DataSource = soinf.soreferenceds.view_quoverlapdata;
            listBoxFeatures.ValueMember = "code";
            listBoxFeatures.DisplayMember = "feature";
            listBoxFeatures.DataSource = soinf.dtfeatures;
        }

        public void SetDimensionBindings()
        {
            foreach (Control control in groupBoxDimensions.Controls)
            {
                if (control is TextBox)
                {
                    int length = control.Name.ToString().Length;
                    string columnname = control.Name.ToString().Substring(7, length - 7).ToLower();
                    if (columnname.Substring(0, 3).ToUpper() != "EXT")
                    {
                        // Skip Total Square Feet
                        if (columnname.ToUpper() == "TOTALSQFT")
                        {
                            continue;
                        }

                        // Format money fields
                        if (columnname.ToUpper() == "PRCSQFT")
                        {
                            control.DataBindings.Clear();
                            SetTextBoxCurrencyBinding(textBoxPrcsqft, soinf.clineds, "socover.prcsqft");
                            continue;
                        }
                        if (columnname.ToUpper() == "PRICE")
                        {
                            control.DataBindings.Clear();
                            SetTextBoxCurrencyBinding(textBoxPrice, soinf.clineds, "socover.price");
                            continue;
                        }
                        // Upcharge special processing
                        if (columnname.ToUpper() == "LINEUPCHARGE")
                        {
                            control.DataBindings.Clear();
                            SetTextBoxDollarsBinding(textBoxLineupcharge, soinf.clineds, "socover.upcharge");
                            continue;
                        }
                        control.DataBindings.Clear();
                        control.DataBindings.Add("Text", soinf.clineds.socover, columnname);
                    }
                    else
                    {
                        string extnumber = columnname.Substring(3, 1);
                        columnname = control.Name.ToString().Substring(11, length - 11).ToLower();
                        control.DataBindings.Clear();
                        switch (extnumber)
                        {
                            case "1":
                                {
                                    control.DataBindings.Add("Text", soinf.tempext1lineds.socover, columnname);
                                    break;
                                }
                            case "2":
                                {
                                    control.DataBindings.Add("Text", soinf.tempext2lineds.socover, columnname);
                                    break;
                                }

                            case "3":
                                {
                                    control.DataBindings.Add("Text", soinf.tempext3lineds.socover, columnname);
                                    break;
                                }
                            case "4":
                                {
                                    control.DataBindings.Add("Text", soinf.tempext4lineds.socover, columnname);
                                    break;
                                }
                        }
                    }
                } // if (control is TextBox)
                else
                {
                    if (control is CheckBox)
                    {
                        control.DataBindings.Clear();
                        switch (control.Name.ToString())
                        {
                            case "checkBoxManlprc":
                                {
                                    checkBoxManlprc.DataBindings.Add("Checked", soinf.clineds.socover, "manlprc");
                                    break;
                                }
                            case "checkBoxManlstr":
                                {
                                    checkBoxManlstr.DataBindings.Add("Checked", soinf.clineds.socover, "manlstr");
                                    break;
                                }
                        }
                    }
                }
            } // foreach
        } // dimension bindings

        public void SetSoHeadBindings()
        {
            // SO number
            textBoxSoNo.DataBindings.Clear();
            textBoxSoNo.DataBindings.Add("Text", soinf.somastds.somast, "sono");

            // PO
            textBoxPonum.DataBindings.Clear();
            textBoxPonum.DataBindings.Add("Text", soinf.somastds.somast, "ponum");
            // Ship via, FOB
            textBoxFob.DataBindings.Add("Text", soinf.somastds.somast, "fob");
            textBoxShipvia.DataBindings.Clear();
            textBoxShipvia.DataBindings.Add("Text", soinf.somastds.somast, "shipvia");
            // Ordered by
            textBoxOrderby.DataBindings.Add("Text", soinf.somastds.somast, "orderby");
            // Old Plan
            textBoxOldplan.DataBindings.Add("Text", soinf.somastds.somast, "oldplan");
            // Pool Owner Information
            textBoxFname.DataBindings.Clear();
            textBoxFname.DataBindings.Add("Text", soinf.somastds.somast, "fname");
            textBoxLname.DataBindings.Clear();
            textBoxLname.DataBindings.Add("Text", soinf.somastds.somast, "lname");
            textBoxAddress.DataBindings.Clear();
            textBoxAddress.DataBindings.Add("Text", soinf.somastds.somast, "address");
            textBoxCity.DataBindings.Clear();
            textBoxCity.DataBindings.Add("Text", soinf.somastds.somast, "city");
            textBoxState.DataBindings.Clear();
            textBoxState.DataBindings.Add("Text", soinf.somastds.somast, "state");
            textBoxZip.DataBindings.Clear();
            textBoxZip.DataBindings.Add("Text", soinf.somastds.somast, "zip");
            textBoxHoemailaddr.DataBindings.Clear();
            textBoxHoemailaddr.DataBindings.Add("Text", soinf.somastds.somast, "hoemailaddr");
            // Reference, Order date and ship date
            textBoxMeycono.DataBindings.Clear();
            textBoxMeycono.DataBindings.Add("Text", soinf.somastds.somast, "meycono");
            //    dateTimePickerOrdate.DataBindings.Add("Value", soinf.somastds.somast, "ordate", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxOrdate.DataBindings.Add("Text", soinf.somastds.somast, "ordate");
            // dateTimePickerSodate.DataBindings.Add("Value", soinf.somastds.somast, "sodate", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxSodate.DataBindings.Add("Text", soinf.somastds.somast, "sodate");
            // Sales Rep
            textBoxSalesmn.DataBindings.Clear();
            textBoxSalesmn.DataBindings.Add("Text", soinf.somastds.somast, "salesmn");
            // Payment Terms
            textBoxPterms.DataBindings.Clear();
            textBoxPterms.DataBindings.Add("Text", soinf.somastds.somast, "pterms");

            // Tax rate, Tax Dist, Territory
            SetTextBoxCurrencyBinding(textBoxTaxrate, soinf.somastds, "somast.taxrate");
            textBoxTaxdist.DataBindings.Clear();
            textBoxTaxdist.DataBindings.Add("Text", soinf.somastds.somast, "taxdist");
            textBoxTerr.DataBindings.Clear();
            textBoxTerr.DataBindings.Add("Text", soinf.somastds.somast, "terr");
            textBoxTrackno.DataBindings.Add("Text", soinf.somastds.somast, "trackno");
        }

        public void SetCustomerBindings()
        {
            textBoxCustno.DataBindings.Add("Text", soinf.ards.arcust, "custno");
            textBoxEmail.DataBindings.Add("Text", soinf.ards.arcust, "email");
            textBoxPhone.DataBindings.Add("Text", soinf.ards.arcust, "phone");
            textBoxCompany.DataBindings.Add("Text", soinf.ards.arcust, "company");
            textBoxFaxNo.DataBindings.Add("Text", soinf.ards.arcust, "faxno");
            // Notes
            textBoxAcctMemo.DataBindings.Add("Text", soinf.ards.arcust, "acctmemo");
            textBoxDPref.DataBindings.Add("Text", soinf.ards.arcust, "dpref");

            // Use a custom function to display the fields as currency
            // Discounts and upcharge
            SetTextBoxDollarsBinding(textBoxDisc, soinf.somastds, "somast.salesdisc");
            SetTextBoxDollarsBinding(textBoxStockdisc, soinf.somastds, "somast.stockdisc");
            SetTextBoxDollarsBinding(textBoxStanddisc, soinf.somastds, "somast.standdisc");
            SetTextBoxDollarsBinding(textBoxCommldisc, soinf.somastds, "somast.commldisc");
            SetTextBoxDollarsBinding(textBoxRepdisc, soinf.somastds, "somast.repdisc");
            SetTextBoxDollarsBinding(textBoxRepldisc, soinf.somastds, "somast.repldisc");
            SetTextBoxDollarsBinding(textBoxShipdisc, soinf.somastds, "somast.shipdisc");
            SetTextBoxDollarsBinding(textBoxUpcharge, soinf.somastds, "somast.upcharge");
        }

        #endregion Set Bindings

        public void CancelEdit()
        {
            if (soinf.somastds.somast[0].idcol > 0)
            {
                soinf.UnlockSomast(soinf.somastds.somast[0].idcol);
            }
            if (CurrentState == "Enter Order")
            {
                ClearQuote();
                ClearLineitems();
                soinf.ClearResultsTable();
                bindingResults.DataSource = soinf.resultsds.soline;
                CurrentState = "Select";
                RefreshControls();
            }
            else
            {
                ClearQuote();
                ClearLineitems();
                soinf.ClearResultsTable();

                soinf.GetSomastBySono(CurrentSono);
                soinf.getSingleCustomerData(CurrentCustid);

                bindingResults.DataSource = soinf.resultsds.soline;

                CurrentState = "View";
                ProcessSo("", "");
                RefreshControls();
            }
        }

        public void DisableControls()
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in this.Controls)
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
            buttonClose.Enabled = true;
        }

        #region Refresh Controls

        public void EnterEditCoverMode()
        {
            this.CurrentState = "Edit Line";
            this.TabControlOrderEntry.SelectedIndex = 0;
            this.RefreshControls(); 
        }

        public void RefreshControls()
        {
            buttonGetSO.Enabled = false;
            textBoxSoNo.ReadOnly = true;
            textBoxSoNo.Enabled = false;
            textBoxCustno.ReadOnly = true;
            textBoxCustno.Enabled = false;
            textBoxPhone.ReadOnly = true;
            textBoxPhone.Enabled = false;
            textBoxCompany.ReadOnly = true;
            textBoxCompany.Enabled = false;
            textBoxFaxNo.ReadOnly = true;
            textBoxFaxNo.Enabled = false;
            textBoxEmail.ReadOnly = true;
            textBoxEmail.Enabled = false;
            textBoxTaxdist.Enabled = false;
            textBoxTrackno.Enabled = false;
            textBoxTaxrate.Enabled = false;
            buttonTickets.BackColor = System.Drawing.SystemColors.Control;
            buttonConvert.Text = "Convert to Order";
            buttonVoid.Text = "Void";
            buttonClose.Text = this.IsEditing ? "Cancel":  "Close";

            switch (CurrentState)
            {
                case "View":
                    {
                        // Set tickets button color
                        if (soinf.SoHasTickets(soinf.somastds.somast[0].sono))
                        {
                            buttonTickets.BackColor = Color.Cyan;
                        }

                        if (soinf.somastds.somast[0].sotype == "B")
                        {
                            buttonConvert.Text = "Convert to Order";
                        }
                        else
                        {
                            buttonConvert.Text = "Revert to Quote";
                        }

                        // Establish Void Button Text

                        if (soinf.somastds.somast[0].sostat == "V")
                        {
                            buttonVoid.Text = "ReOpen";
                        }
                        else
                        {
                            buttonVoid.Text = "Void";
                        }
                        DisableControls();
                        groupBoxTotals.Enabled = true;
                        if (soinf.clineds.socover[0].product.TrimEnd() != "Worksheet")
                        {
                            buttonCreatenewcover.Enabled = true;
                            buttonCreatenewversion.Enabled = true;
                            buttonCreateInvoice.Enabled = true;
                            buttonVersion.Enabled = true;
                            buttonCover.Enabled = true;
                        }
                        buttonTickets.Enabled = true;
                        buttonTrackingHistory.Enabled = true;
                        checkBoxManlship.Enabled = false;
                        textBoxShipping.Enabled = false;
                        buttonImportInspection.Enabled = true;
                        TabControlDiscountData.Enabled = true;
                        TabControlOrderEntry.Enabled = true;
                        buttonClearOrder.Enabled = true;
                        buttonEditLine.Enabled = true;
                        buttonEditSoHead.Enabled = true;
                        buttonCancelEdit.Enabled = false;
                        buttonVoid.Enabled = true;
                        buttonRoute.Enabled = true;
                        buttonGeneratePDFs.Enabled = true;
                        buttonViewPDF.Enabled = true;
                        buttonViewQuote.Enabled = true;
                        buttonConvert.Enabled = true;
                        buttonDeleteCover.Enabled = true;
                        buttonReinitialize.Enabled = false;
                        buttonDeleteVersion.Enabled = true;
                        buttonConvertWorksheet.Enabled = true;
                        labelSoType.Visible = true;
                        versionsList.Enabled = true;
                        versionsList.Enable(true);
                        break;
                    }

                case "Edit Line": //aka Edit Cover
                    {
                        DisableControls();
                        soinf.clineds.socover[0].EndEdit();
                        soinf.clineds.socover.AcceptChanges();

                        if (soinf.clineds.socover[0].product.TrimEnd() != "Worksheet")
                        {
                            groupBoxDimensions.Enabled = true;
                            groupBoxTotals.Enabled = true;
                            TabControlDiscountData.Enabled = true;
                            panelDiscounts.Enabled = true;
                            TabControlOrderEntry.Enabled = true;
                            textBoxShipping.Enabled = true;
                            checkBoxManlship.Enabled = true;
                            buttonSave.Enabled = true;
                            versionsList.Enabled = true;
                            //  EnableTabPageDataControls("TABPAGECOVERINFO");
                            EnableCoverTabPageDataControls(soinf.clineds.socover[0].product.TrimEnd());
                        }
                        else
                        {
                            buttonCalccover.Enabled = false;
                            TabControlOrderEntry.Enabled = true;
                            buttonCancelEdit.Enabled = true;
                            buttonComments.Enabled = true;
                            buttonConvertWorksheet.Enabled = true;
                            buttonSave.Enabled = true;
                            buttonCoverItem.Enabled = true;
                        }

                        if (CurrentFeature == "HW")
                        {
                            buttonCalchardware.Enabled = false;
                        }
                        else
                        {
                            buttonCalchardware.Enabled = true;
                        }
                        // Disable add version and add cover
                        buttonCreatenewcover.Enabled = false;
                        buttonVersion.Enabled = false;
                        buttonCover.Enabled = false;
                        buttonCreatenewversion.Enabled = false;
                        buttonConvertWorksheet.Enabled = false;
                        buttonEditLine.Enabled = false;
                        buttonEditSoHead.Enabled = false;
                        versionsList.Enable(false);
                        buttonReinitialize.Enabled = this.soinf.IsOpen;
                        break;
                    }
                case "Enter Order":
                    {
                        DisableControls();
                        TabControlOrderEntry.Enabled = true;
                        TabControlDiscountData.Enabled = true;
                        buttonSave.Enabled = true;
                        EnableTabPageDataControls("TABPAGEORDERDATA");
                        EnableTabPageDataControls("TABPAGECOVERINFO");
                        buttonEditSoHead.Enabled = false;
                        // Disable add version and add cover
                        buttonCreatenewcover.Enabled = false;
                        buttonVersion.Enabled = false;
                        buttonCover.Enabled = false;
                        buttonCreatenewversion.Enabled = false;
                        buttonEditLine.Enabled = false;
                        buttonEditSoHead.Enabled = false;
                        buttonConvertWorksheet.Enabled = false;
                        textBoxTaxdist.Enabled = false;
                        textBoxTaxrate.Enabled = false;
                        versionsList.Enable(false);
                        break;
                    }
                case "Edit SOHead":
                    {
                        DisableControls();
                        TabControlDiscountData.Enabled = true;
                        TabControlOrderEntry.Enabled = true;
                        buttonSave.Enabled = true;
                        EnableTabPageDataControls("TABPAGEORDERDATA");
                        buttonEditSoHead.Enabled = false;
                        labelTrackingData.Enabled = true;
                        textBoxTaxdist.Enabled = false;
                        textBoxTaxrate.Enabled = false;
                        versionsList.Enable(false);
                        buttonReinitialize.Enabled = this.soinf.IsOpen;

                        this.SetDepositTiersDefaultState(this.DepositTiersUsingDefaults());

                        break;
                    }
                case "SelectVersion":
                    {
                        break;
                    }

                case "Select":
                    {
                        DisableControls();
                        labelSoType.Visible = false;
                        if (NewOrder == true)
                        {
                            buttonGetCustomer.Enabled = true;
                        }
                        else
                        {
                            buttonGetSO.Enabled = true;
                            textBoxSoNo.ReadOnly = false;
                            textBoxSoNo.Enabled = true;
                            buttonGetSO.Enabled = true;
                        }
                        buttonClose.Enabled = true;
                        break;
                    }
                case "SoHeadEntry":
                    {
                        DisableControls();
                        labelTrackingData.Enabled = true;
                        TabControlCustomerNotes.Enabled = true;
                        TabControlDiscountData.Enabled = true;
                        TabControlOrderEntry.Enabled = true;
                        buttonClose.Enabled = true;
                        buttonSave.Enabled = true;
                        buttonClose.Enabled = true;
                        buttonCreatenewversion.Enabled = true;
                        break;
                    }
            }
            // Never allow changes to the product
            textBoxProduct.Enabled = false;
            TabControlCustomerNotes.Enabled = true;
            this.Update();
        }

        public void SelectVersionPanel(string version, string cover)
        {
            this.versionsList.SelectVersionPanel(version);

            if (!this.IsEditing)
                this.ProcessSo(version, cover, forceReloadVersionsList: false);
        }

        #endregion Refresh Controls

        #region Initialize Order

        private void InitializeOrder()
        {
            soinf.EstablishBlankSomastData();

            // Esttablish the customer table
            soinf.getSingleCustomerData(CurrentCustid);

            CurrentCustno = soinf.ards.arcust[0].custno;

            soinf.somastds.somast[0].sono = soinf.getsono(); 
            soinf.somastds.somast[0].custno = CurrentCustno;
            soinf.somastds.somast[0].custid = CurrentCustid;
            soinf.somastds.somast[0].pdays = soinf.ards.arcust[0].pdays;
            soinf.somastds.somast[0].pnet = soinf.ards.arcust[0].pnet;
            soinf.somastds.somast[0].pdisc = soinf.ards.arcust[0].pdisc;
            soinf.somastds.somast[0].pterms = soinf.ards.arcust[0].pterms;
            soinf.somastds.somast[0].termid = soinf.ards.arcust[0].termid;
            soinf.somastds.somast[0].taxrate = soinf.ards.arcust[0].tax;
            soinf.somastds.somast[0].fob = soinf.ards.arcust[0].fob;
            soinf.somastds.somast[0].shipvia = soinf.ards.arcust[0].shipvia;
            soinf.somastds.somast[0].glarec = soinf.ards.arcust[0].gllink;
            soinf.somastds.somast[0].salesmn = soinf.ards.arcust[0].salesmn;
            soinf.somastds.somast[0].salesdisc = soinf.ards.arcust[0].disc;
            soinf.somastds.somast[0].taxdist = soinf.ards.arcust[0].taxdist;
            soinf.somastds.somast[0].terr = soinf.ards.arcust[0].terr;
            soinf.somastds.somast[0].taxrate = soinf.ards.arcust[0].tax;
            soinf.somastds.somast[0].taxst = soinf.ards.arcust[0].state;
            soinf.somastds.somast[0].glarec = soinf.ards.arcust[0].gllink;
            soinf.somastds.somast[0].stockdisc = soinf.ards.arcust[0].stockdisc;
            soinf.somastds.somast[0].standdisc = soinf.ards.arcust[0].standdisc;
            soinf.somastds.somast[0].commldisc = soinf.ards.arcust[0].commldisc;
            soinf.somastds.somast[0].repldisc = soinf.ards.arcust[0].repldisc;
            soinf.somastds.somast[0].repdisc = soinf.ards.arcust[0].repdisc;
            soinf.somastds.somast[0].shipdisc = soinf.ards.arcust[0].shipdisc;
            soinf.somastds.somast[0].upcharge = soinf.ards.arcust[0].upcharge;

            // Establish Quote record type
            soinf.somastds.somast[0].enterqu = "Y";

            // Establish default location
            soinf.somastds.somast[0].defloc = appInformation.GetStkloc();

            if (Quoting == true)
            {
                soinf.somastds.somast[0].sotype = "B";
            }
            else
            {
                soinf.somastds.somast[0].sotype = "O";
            }
            // Establish the Ship To table and initialize the fields
            soinf.EstablishBlankSoaddrData();

            if (wsgUtilities.wsgReply("Enter Pool Owner Information") == true)
            {
                FrmPoolOwnerData frmPoolOwnerData = new FrmPoolOwnerData();
                frmPoolOwnerData.ShowDialog();
                if (frmPoolOwnerData.SaveData == true)
                {
                    soinf.somastds.somast[0].lname = frmPoolOwnerData.Lname;
                    soinf.somastds.somast[0].ponum = frmPoolOwnerData.Lname;
                    soinf.somastds.somast[0].fname = frmPoolOwnerData.Fname;
                    soinf.somastds.somast[0].address = frmPoolOwnerData.Address;
                    soinf.somastds.somast[0].city = frmPoolOwnerData.City;
                    soinf.somastds.somast[0].state = frmPoolOwnerData.State;
                    soinf.somastds.somast[0].zip = frmPoolOwnerData.Zip;

                    if (wsgUtilities.wsgReply("Copy Pool Owner Information to Ship-To") == true)
                    {
                        // Populate the ship-to if indicated
                        soinf.somastds.soaddr[0].adtype = "O";
                        soinf.somastds.soaddr[0].cshipno = "999999";
                        soinf.somastds.soaddr[0].company = soinf.somastds.somast[0].fname.TrimEnd() +
                        " " + soinf.somastds.somast[0].lname.TrimEnd();
                        soinf.somastds.soaddr[0].address1 = soinf.somastds.somast[0].address.TrimEnd();
                        soinf.somastds.soaddr[0].city = soinf.somastds.somast[0].city.TrimEnd();
                        soinf.somastds.soaddr[0].state = soinf.somastds.somast[0].state.TrimEnd();
                        soinf.somastds.soaddr[0].zip = soinf.somastds.somast[0].zip.TrimEnd();
                        soinf.somastds.soaddr.AcceptChanges();
                    }
                }
            }
            //Develop Ship-to Information if needed
            if (soinf.somastds.soaddr[0].company.TrimEnd() == "")
            {
                // Get the default ship to address for this customer, if any
                soinf.getDefaultShipToData(CurrentCustid);
                if (soinf.ards.aracadr.Rows.Count != 0)
                {
                    soinf.somastds.soaddr[0].adtype = "D";
                    soinf.somastds.soaddr[0].cshipno = soinf.ards.aracadr[0].cshipno;
                    soinf.somastds.soaddr[0].company = soinf.ards.aracadr[0].company;
                    soinf.somastds.soaddr[0].address1 = soinf.ards.aracadr[0].address1;
                    soinf.somastds.soaddr[0].address2 = soinf.ards.aracadr[0].address2;
                    soinf.somastds.soaddr[0].city = soinf.ards.aracadr[0].city;
                    soinf.somastds.soaddr[0].state = soinf.ards.aracadr[0].state;
                    soinf.somastds.soaddr[0].zip = soinf.ards.aracadr[0].zip;
                }
                else
                {
                    // If no ship to information, use customer information
                    soinf.somastds.soaddr[0].adtype = "C";
                    soinf.somastds.soaddr[0].cshipno = soinf.ards.arcust[0].custno;
                    soinf.somastds.soaddr[0].company = soinf.ards.arcust[0].company;
                    soinf.somastds.soaddr[0].address1 = soinf.ards.arcust[0].address1;
                    soinf.somastds.soaddr[0].address2 = soinf.ards.arcust[0].address2;
                    soinf.somastds.soaddr[0].city = soinf.ards.arcust[0].city;
                    soinf.somastds.soaddr[0].state = soinf.ards.arcust[0].state;
                    soinf.somastds.soaddr[0].zip = soinf.ards.arcust[0].zip;
                }
            }
        }

        #endregion Initialize Order

        private void TextBoxCustno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CurrentCustid = soinf.getCustomerDatabyCustno(textBoxCustno.Text);
                if (CurrentCustid != 0)
                {
                    InitializeOrder();
                    CurrentState = "Enter Order";
                    ProcessSo("", "");
                }
                else
                {
                    wsgUtilities.wsgNotice("Customer Not Found. Click Find to Search");
                    CurrentState = "SelectCustno";
                    RefreshControls();
                }
            }
        }

        #region String/Decimal Conversion

        private void DecimalToCurrencyString(object sender, ConvertEventArgs cevent)
        {
            // The method converts only to string type. Test this using the DesiredType.
            if (cevent.DesiredType != typeof(string)) return;

            // Use the ToString method to format the value as currency ("c").
            cevent.Value = ((decimal)cevent.Value).ToString("N2");
        }

        private void CurrencyStringToDecimal(object sender, ConvertEventArgs cevent)
        {
            // The method converts back to decimal type only.
            if (cevent.DesiredType != typeof(decimal)) return;

            // Converts the string back to decimal using the static Parse method.
            cevent.Value = Decimal.Parse(cevent.Value.ToString(),
            NumberStyles.Currency, null);
        }

        private void SetTextBoxCurrencyBindingT(TextBox txtbox, DataTable dt, string fieldname)
        {
            Binding b = new Binding("Text", dt, fieldname);
            {
                b.Format += new ConvertEventHandler(DecimalToCurrencyString);
                b.Parse += new ConvertEventHandler(CurrencyStringToDecimal);
            }
            txtbox.DataBindings.Clear();
            txtbox.DataBindings.Add(b);
        }

        private void SetTextBoxCurrencyBinding(TextBox txtbox, quote ds, string fieldname)
        {
            Binding b = new Binding("Text", ds, fieldname);
            {
                b.Format += new ConvertEventHandler(DecimalToCurrencyString);
                b.Parse += new ConvertEventHandler(CurrencyStringToDecimal);
            }
            txtbox.DataBindings.Clear();
            txtbox.DataBindings.Add(b);
        }

        private void CurrencyStringToDollars(object sender, ConvertEventArgs cevent)
        {
            // The method converts back to decimal type only.
            if (cevent.DesiredType != typeof(Int32)) return;

            // Converts the string back to Integer using the static Parse method.
            cevent.Value = Int32.Parse(cevent.Value.ToString(),
            NumberStyles.Currency, null);
        }

        private void SetTextBoxDollarsBinding(TextBox txtbox, DataSet ds, string fieldname)
        {
            Binding b = new Binding("Text", ds, fieldname);
            {
                b.Format += new ConvertEventHandler(DecimalToDollarsString);
                b.Parse += new ConvertEventHandler(CurrencyStringToDecimal);
            }
            txtbox.DataBindings.Clear();
            txtbox.DataBindings.Add(b);
        }

        private void DecimalToDollarsString(object sender, ConvertEventArgs cevent)
        {
            // The method converts only to string type. Test this using the DesiredType.
            if (cevent.DesiredType != typeof(string)) return;

            // Use the ToString method to format the value as currency ("c").
            cevent.Value = ((Decimal)cevent.Value).ToString("N0");
        }

        #endregion String/Decimal Conversion

        private void FrmSOHead_Shown(object sender, EventArgs e)
        {
            if (PassedSono.TrimEnd() != "")
            {
                ProcessExistingQuote(PassedSono);
            }

            // Deposit Requirements
            SetTextBoxDollarsBinding(textBoxDepover0, soinf.soitemsds, "deposittiers.depover0");
            SetTextBoxDollarsBinding(textBoxDepover1, soinf.soitemsds, "deposittiers.depover1");
            SetTextBoxDollarsBinding(textBoxDepover2, soinf.soitemsds, "deposittiers.depover2");
            SetTextBoxDollarsBinding(textBoxDepover3, soinf.soitemsds, "deposittiers.depover3");

            RefreshControls();

            this.FormClosing += FrmSOHead_FormClosing1;
        }

        private void FrmSOHead_FormClosing1(object sender, FormClosingEventArgs e)
        {
            if (soinf != null && soinf.ards != null)
            {
                soinf.ards.Clear();
                soinf.ards = null;
            }
        }

        private void dateTimePickerSodate_ValueChanged(object sender, EventArgs e)
        {
            soinf.somastds.somast[0].sodate = dateTimePickerSodate.Value;
            soinf.somastds.somast.AcceptChanges();
            this.Update();
        }

        private void dateTimePickerOrdate_ValueChanged(object sender, EventArgs e)
        {
            soinf.somastds.somast[0].ordate = dateTimePickerOrdate.Value;
            soinf.somastds.somast.AcceptChanges();
            this.Update();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveSo();
        }

        #region Save SO

        public void SaveSo(bool silent = false, bool loadVersionsList = true)
        {
            if (soinf.ValidateSO(CurrentFeature, true, silent: silent) == true)
            {
                CurrentFeature = "";
                buttonSave.Enabled = false;
                string thisversion = soinf.somastds.soversion[0].version;
                string thiscover = soinf.clineds.socover[0].cover;
                if (trackingInf.GetLastSOTrackingData(soinf.somastds.somast[0].sono) == "No tracking data")
                {
                    soinf.RouteSo();
                }
                // Save Order
                soinf.somastds.somast[0].defloc = comboBoxLocation.SelectedItem.ToString();
                soinf.SaveOrderData(this.checkBoxDefaultCustTiers.Checked, silent);

                // Save the sono for PDF Creation
                string saveSono = soinf.somastds.somast[0].sono;
                MakeQuotePDF(saveSono, soinf.somastds.somast[0].sotype);
                CurrentState = "View";

                // Reload Somast dataset
                CurrentSomastid = soinf.GetSomastBySono(saveSono);
                CurrentSono = saveSono; 

                // Check for mulitple covers. If there multiple covers, force selection
                if (!silent && soinf.GetVersionCoverCount(saveSono, thisversion) > 1)
                {
                    //if (wsgUtilities.wsgReply("There are other covers. Edit them?"))
                    //{
                    //    thiscover = miscDataMethods.GetSOVersionCover(saveSono, thisversion);
                    //}
                }

                // Clear all other datasets
                soinf.ClearCoverVersionLine();
                ProcessSo(thisversion, thiscover, loadVersionsList:loadVersionsList, forceReloadVersionsList:true);

                RefreshControls();
            }
        }

        public void SaveSoVersionComments(string version, string intComment, string custComment)
        {
            soinf.UpdateSOVersionComments(soinf.somastds.somast[0].sono, version, intComment, custComment);
        }

        #endregion Save SO

        private void buttonShipto_Click(object sender, EventArgs e)
        {
            // Show the ship to selector
            FrmGetShipToAddress frmGetShipAddress = new FrmGetShipToAddress();
            frmGetShipAddress.CustId = CurrentCustid;
            frmGetShipAddress.ShowDialog();
            if (frmGetShipAddress.SelectedShipToId != 0)
            {
                soinf.ProcessShipToSelection(frmGetShipAddress.SelectedShipToId);
            }
        }

        private bool MakeQuotePDF(string sono, string sotype)
        {
            string fileprefix = "";
            if (sotype == "B")
            {
                fileprefix = "E";
            }
            else
            {
                fileprefix = "O";
            }
            string fileName = fileprefix + sono.TrimStart() + ".pdf";
            string filePath = System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], fileName);
            bool pdfOk = true;

            if (appUtilities.IsFileOpen(filePath) == false)
            {
                soinf.getallsoreportdata(sono);

                Estimate rpt = new Estimate();
                rpt.SetDataSource(soinf.quorptds);
                try
                {
                    PdfStorage.WriteFileFromReport(rpt, fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                wsgUtilities.wsgNotice("The PDF for this SO is in use.");
                pdfOk = false;
            }
            return pdfOk;
        }

        private bool MakeReportPDF(string sono, string fileprefix, ReportClass rpt)
        {
            string fileName = fileprefix + sono.TrimStart() + ".pdf";
            string filePath = System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], fileName);
            bool pdfOk = true;

            if (appUtilities.IsFileOpen(filePath) == false)
            {
                soinf.getallsoreportdata(sono);

                rpt.SetDataSource(soinf.quorptds);
                try
                {
                    PdfStorage.WriteFileFromReport(rpt, fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                wsgUtilities.wsgNotice("The PDF for this SO is in use. Close it and retry.");
                pdfOk = false;
            }
            return pdfOk;
        }

        private bool MakeInvoicePDF(string sono, string invno)
        {
            string fileName = "I" + sono.TrimStart() + ".pdf";
            string filePath = System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], fileName);
            bool pdfOk = true;

            if (appUtilities.IsFileOpen(filePath) == false)
            {
                soinf.getallsoreportdata(sono);

                Invoice rpt = new Invoice();
                rpt.SetDataSource(soinf.quorptds);
                try
                {
                    PdfStorage.WriteFileFromReport(rpt, fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                wsgUtilities.wsgNotice("The PDF for this invoice is in use. Close it and retry.");
                pdfOk = false;
            }
            return pdfOk;
        }


        public void ProcessExistingQuote(string sono)
        {
            CurrentSomastid = soinf.GetSomastBySono(sono);
            CurrentSono = sono; 

            if (CurrentSomastid != 0)
            {
                if (soinf.somastds.somast[0].enterqu == "Y")
                {
                    CurrentCustno = soinf.somastds.somast[0].custno;
                    CurrentCustid = soinf.somastds.somast[0].custid;
                    // Load customer data for this SO
                    soinf.getSingleCustomerData(CurrentCustid);
                    CurrentVersion = "";
                    CurrentState = "View";
                    ProcessSo("", "");
                    RefreshControls();
                }
                else
                {
                    ShowSOSearch();
                    textBoxSoNo.Text = "";
                }
            }
            else
            {
                textBoxSoNo.Text = CurrentSono;
                CurrentState = "Select";
                ShowSOSearch();
                textBoxSoNo.Text = "";
            }
        }

        public void ShowSOSearch()
        {
            FrmSOSearch frmSoSearch = new FrmSOSearch(this.textBoxSoNo.Text, "Y");
            frmSoSearch.ShowDialog();
            if (frmSoSearch.SelectedSono.TrimEnd() != "")
            {
                CurrentSomastid = soinf.GetSomastBySono(frmSoSearch.SelectedSono);
                CurrentSono = frmSoSearch.SelectedSono;

                CurrentCustno = soinf.somastds.somast[0].custno;
                CurrentCustid = soinf.somastds.somast[0].custid;
                // Load customer data for this SO
                soinf.getSingleCustomerData(CurrentCustid);
                CurrentVersion = "";
                CurrentState = "View";
                ProcessSo("", "");
                
                RefreshControls();
            }
        }

        private void textBoxSoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ProcessExistingQuote(textBoxSoNo.Text.TrimEnd().TrimStart().PadLeft(10));
            }
        }

        #region Process SO

        public void ProcessSo(string version, string cover, bool reloadLineItems = false, bool loadVersionsList = true, bool forceReloadVersionsList = false)
        {
            /* This routine establishes all data tables for the estimate and postions the
               estimate at the current version and cover
               If there are no rows for the version, cover and lines, blank rows are created
            */
            CurrentFeature = "";
            // Develop BillTo information
            textBoxBillto.Text = soinf.ards.arcust[0].company + Environment.NewLine;
            textBoxBillto.Text += soinf.ards.arcust[0].address1 + Environment.NewLine;
            // Establish date picker values
            dateTimePickerOrdate.Value = soinf.somastds.somast[0].ordate;
            dateTimePickerSodate.Value = soinf.somastds.somast[0].sodate;
            // Clear textboxes
            textBoxOverlapdesc.Text = "";
            textBoxMaterialDesc.Text = "";
            textBoxColordesc.Text = "";
            textBoxSpacingdesc.Text = "";
            if (soinf.somastds.somast[0].lckstat.TrimEnd() == "")
            {
                labelLckUser.Text = "";
            }
            else
            {
                labelLckUser.Text = soinf.somastds.somast[0].lckuser;
            }

            if (soinf.ards.arcust[0].address2.TrimEnd() != "")
            {
                textBoxBillto.Text += soinf.ards.arcust[0].address2 + Environment.NewLine;
            }
            textBoxBillto.Text += soinf.ards.arcust[0].city + Environment.NewLine;
            textBoxBillto.Text += soinf.ards.arcust[0].state.TrimEnd() + ", " +
            soinf.ards.arcust[0].zip + Environment.NewLine;
            if (soinf.somastds.somast[0].idcol > 0)
            {
                labelTrackingData.Text = trackingInf.GetLastSOTrackingData(soinf.somastds.somast[0].sono);
            }
            if (soinf.somastds.somast[0].sotype == "B")
            {
                labelSoType.Text = "This is an estimate";
            }
            else
            {
                labelSoType.Text = "This is an order";
            }
            comboBoxLocation.SelectedItem = soinf.somastds.somast[0].defloc.TrimStart().TrimEnd();
            // Loading Line variable prevents the selectedindex change method from firing on comboboxes
            LoadingLine = true;

            // Locate the version view data for the current sono
            soinf.LoadVersionViewData(soinf.somastds.somast[0].sono);

            if (soinf.somastds.view_versiondata.Rows.Count == 0)
            {
                soinf.EstablishBlankSoversionData();
                if (version == "")
                {
                    soinf.somastds.soversion[0].version = "A";
                    CurrentVersion = "A";
                }
                else
                {
                    // Creating a new version with no prior versions
                    soinf.somastds.soversion[0].version = version;
                    CurrentVersion = version;
                }
            }
            else
            {
                // There are existing versions
                if (version == "")
                {
                    // If not targeting a version, use the first version
                    CurrentVersion = soinf.somastds.view_versiondata[0].version;
                    soinf.somastds.soversion.Rows.Clear();
                    soinf.somastds.soversion.ImportRow(soinf.somastds.view_versiondata.Rows[0]);
                }
                else
                {
                    DataRow[] foundRows;
                    // Retrieve Version Row from view
                    foundRows = soinf.somastds.view_versiondata.Select("version = '" + version + "'");
                    if (foundRows.Length != 0)
                    {
                        soinf.somastds.soversion.Rows.Clear();
                        soinf.somastds.soversion.ImportRow(foundRows[0]);
                        CurrentVersion = version;
                    }
                    else
                    {
                        soinf.EstablishBlankSoversionData();
                        soinf.somastds.soversion[0].version = version;
                        CurrentVersion = version;
                    }
                }
            }

            // Establish cover line, extensions and miscellaneous items
            // Locate the cover view data for the current sono, version
            soinf.LoadCoverViewData(soinf.somastds.somast[0].sono, CurrentVersion);

            if (soinf.somastds.view_coverdata.Rows.Count == 0)
            {
                // No rows. Use "A" if none specified
                if (cover == "")
                {
                    CurrentCover = "A";
                }
                else
                {
                    CurrentCover = cover;
                }
            }
            else
            {
                // Rows found
                if (cover == "")
                {
                    // Use the first cover is none is specified
                    CurrentCover = soinf.somastds.view_coverdata[0].cover;
                }
                else
                {
                    CurrentCover = cover;
                }
            }
            string product = "";
            bool productOK = true;
            string strExpr = "cover = '" + CurrentCover + "'";
            // Use the Select method to the current cover, if it exists.
            DataRow[] foundCoverRows = soinf.somastds.view_coverdata.Select(strExpr);

            if (foundCoverRows.Length < 1)
            {
                // If the requested cover doesn't exist, prompt for the product before continuing.

                string selectedproduct = SelectProduct();
                if (selectedproduct == "")
                {
                    wsgUtilities.wsgNotice("You must select a product for the cover");
                    productOK = false;
                }
                else
                {
                    product = selectedproduct;
                    if (product.TrimEnd() == "Worksheet" &&
                    ((soinf.clineds.socover[0].cover.TrimEnd() != "" && soinf.clineds.socover[0].cover.TrimEnd() != "A")
                     || (soinf.clineds.socover[0].version.TrimEnd() != "" && soinf.clineds.socover[0].version.TrimEnd() != "A")))
                    {
                        wsgUtilities.wsgNotice("A Worksheet must be the first version and first item");
                        productOK = false;
                    }
                }
            }

            //establish cover items & load deposit tiers
            if (productOK == true)
            {
                soinf.LoadAllCoverData(soinf.somastds.somast[0].sono, CurrentVersion, CurrentCover, product, reloadLineItems);
                // Establish cover items table
                // Note: The items will be specific to the cover type
                soinf.EstablishCoverItemsTable(soinf.clineds.socover[0].product);
                if (soinf.soreferenceds.view_immasterdata.Rows.Count == 0)
                {
                    wsgUtilities.wsgNotice("There are no items for the product you have selected");
                    productOK = false;
                }

                soinf.LoadDepositTiers(soinf.somastds.somast[0].idcol);
            }

            // If new cover, check for import
            if (productOK == true)
            {
                if (soinf.clineds.socover[0].idcol < 1)
                {
                    productOK = soinf.CheckInspImport();
                }
            }

            // establish and clear Features Dataset, set upcharge info
            if (productOK == true)
            {
                // Establish and clear Features Dataset
                soinf.EstablishFeatureds();
                dataGridViewFeatureSelector.AutoGenerateColumns = false;
                dataGridViewFeatureSelector.DataSource = bindingFeatureSelector;
                // Set Upcharge information
                soinf.clineds.socover[0].upcharge = soinf.ards.arcust[0].upcharge;

                RefreshCover();

                soinf.RefreshTotals();
                RefreshControls();
            }

            labelTaxdescrip.Text = appInformation.GetDistrictDescription(soinf.somastds.somast[0].taxdist);
            LoadingLine = false;

            if (loadVersionsList || forceReloadVersionsList)
                this.LoadVersionsList(forceReloadVersionsList);
        }

        #endregion Process SO

        # region Combo Box SelectedIndexChanged Methods

        private void comboBoxCover_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                soinf.clineds.socover[0].descrip = soinf.GetItemDescription(soinf.clineds.socover[0].item);
                RefreshCover();
                this.Update();
            }
        }

        private void comboBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                soinf.clineds.socover[0].materialid = Convert.ToInt32(comboBoxMaterial.SelectedValue);
                comboBoxMaterial.Visible = false;
                RefreshCover();
            }
        }

        private void comboBoxSpacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                soinf.clineds.socover[0].spacingid = Convert.ToInt32(comboBoxSpacing.SelectedValue);
                textBoxSpacingdesc.Text = soinf.GetSpacingDescription(soinf.clineds.socover[0].spacingid);
                comboBoxSpacing.Visible = false;
                RefreshCover();
                this.Update();
            }
        }

        private void comboBoxOverlap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                soinf.clineds.socover[0].overlapid = Convert.ToInt32(comboBoxOverlap.SelectedValue);
                textBoxOverlapdesc.Text = soinf.GetOverlapDescription(soinf.clineds.socover[0].overlapid);
                comboBoxOverlap.Visible = false;
                RefreshCover();
                this.Update();
            }
        }

        #endregion

        private void timerSohead_Tick(object sender, EventArgs e)
        {
            comboBoxMaterial.Visible = false;
            comboBoxColor.Visible = false;
            comboBoxOverlap.Visible = false;
            comboBoxSpacing.Visible = false;
            timerSohead.Enabled = false;
        }

        private void textBoxMaterialDesc_Enter(object sender, EventArgs e)
        {
            comboBoxMaterial.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxMaterial.Focus();
        }

        private void textBoxSpacingdesc_Enter(object sender, EventArgs e)
        {
            comboBoxSpacing.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxSpacing.Focus();
        }

        private void textBoxOverlapdesc_Enter(object sender, EventArgs e)
        {
            comboBoxOverlap.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxOverlap.Focus();
        }

        private void textBoxPwidft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void SetNumberText(TextBox textbox)
        {
            if (textbox.Text.TrimEnd() == "0")
            {
                textbox.Text = "";
            }
            textbox.SelectionStart = textBoxPwidft.MaxLength;
        }

        private void SetStringTextBoxStart(TextBox textbox)
        {
            // MessageBox.Show("here");
            SendKeys.Send("{ENTER}");
            SendKeys.Send("{HOME}");
        }

        public void RefreshCover()
        {
            soinf.RefreshCover();
        }

        private bool Checkinches(string stringinches)
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

        private feetandinches CalculateFeetandInches(decimal poolfeet, decimal poolinches, decimal overlapfeet, decimal overlapinches)
        {
            feetandinches returnsize = new feetandinches();
            int intfeet = 0;
            intfeet = (int)(((poolfeet + overlapfeet) * 12) + poolinches + overlapinches) / 12;
            returnsize.Feet = Convert.ToDecimal(intfeet);
            returnsize.Inches = ((poolfeet + overlapfeet) * 12) + poolinches + overlapinches - (returnsize.Feet * 12);
            return returnsize;
        }

        # region Enter Methods

        private void textBoxPwidft_Enter(object sender, EventArgs e)
        {
            RefreshCover();
            SetNumberText(textBoxPwidft);
        }

        private void textBoxPwidin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxPwidin);
            RefreshCover();
        }

        private void textBoxPlenft_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxPlenft);
            RefreshCover();
        }

        private void textBoxPlenin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxPlenin);
            RefreshCover();
        }

        private void textBoxExt1pwidft_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt1Pwidft);
            RefreshCover();
        }

        private void textBoxExt1Pwidin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt1Pwidin);
            RefreshCover();
        }

        private void textBoxExt1Plenft_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt1Plenft);
            RefreshCover();
        }

        private void textBoxExt1Plenin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt1Plenin);
            RefreshCover();
        }

        private void textBoxExt2Pwidft_Enter(object sender, EventArgs e)
        {
            RefreshCover();
            SetNumberText(textBoxExt2Pwidft);
        }

        private void textBoxExt2Plenft_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt2Plenft);
            RefreshCover();
        }

        private void textBoxExt3Pwidft_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt3Pwidft);
            RefreshCover();
        }

        private void textBoxExt3Plenft_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt3Plenft);
            RefreshCover();
        }

        private void textBoxExt4Pwidft_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt4Pwidft);
            RefreshCover();
        }

        private void textBoxExt2Plenin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt2Plenin);
            RefreshCover();
        }

        private void textBoxExt3Pwidin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt3Pwidin);
            RefreshCover();
        }

        private void textBoxExt3Plenin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt3Plenin);
            RefreshCover();
        }

        private void textBoxExt4Pwidin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt4Pwidin);
            RefreshCover();
        }

        private void textBoxExt4Plenft_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt4Plenft);
            RefreshCover();
        }

        private void textBoxExt4Plenin_Enter(object sender, EventArgs e)
        {
            SetNumberText(textBoxExt4Plenin);
            RefreshCover();
        }

        #endregion

        private void CheckTextBoxInches(object sender, CancelEventArgs e)
        {
            if (Checkinches(((TextBox)sender).Text) != true)
            {
                e.Cancel = true;
            }
        }

        private void SendTabonEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dataGridViewSelectedItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (soinf.socurrentitemsds.soline.Rows.Count > 0)
            {
                if (e.ColumnIndex == 1)
                {
                    soinf.socurrentitemsds.soline[e.RowIndex].qtyact = soinf.socurrentitemsds.soline[e.RowIndex].qtyord;
                    soinf.socurrentitemsds.soline.AcceptChanges();
                }
            }
        }

        private void dataGridViewSelectedItems_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (wsgUtilities.wsgReply("Delete this item?"))
                {
                    soinf.DeleteCurrentRow(dataGridViewSelectedItems);
                    soinf.socurrentitemsds.soline.AcceptChanges();
                    this.Update();
                }
            }
        }

        private void buttonEditLine_Click(object sender, EventArgs e)
        {
            string editstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
            if (editstatus == "OK")
            {
                CurrentState = "Edit Line";
                RefreshControls();
            }
            else
            {
                wsgUtilities.wsgNotice(editstatus);
            }
        }

        private void buttonClearOrder_Click(object sender, EventArgs e)
        {
            ClearQuote();
            ClearLineitems();
            this.versionsList.Clear();
            this.ShowVersionsList(false);
            soinf.ClearResultsTable();
            bindingResults.DataSource = soinf.resultsds.soline;
            CurrentState = "Select";
            RefreshControls();
        }

        private void buttonReinitialize_Click(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("This action will reinitialize the prices of all line items from the master table prices for those items. It may also take up to a minute for orders with many versions. Are you sure you want to proceed?"))
            {
                this.ShowLoadingScreen(true);
                try
                {
                    this.RefreshLineItems();
                }
                finally
                {
                    this.ShowLoadingScreen(false);
                }
            }
        }

        private void RefreshLineItems()
        {
            string originalVersion = this.CurrentVersion;
            string originalCover = this.CurrentCover; 

            for (int n=0; n< soinf.somastds.view_versiondata.Rows.Count; n++)
            {
                ProcessSo(soinf.somastds.view_versiondata.Rows[n]["version"].ToString(), "", reloadLineItems: true, loadVersionsList: false);
                SaveSo(silent:true, loadVersionsList: false);
            }

            ProcessSo(originalVersion, originalCover); 
        }

        private void ShowVersionsList(bool show = true)
        {
            if (show)
                this.versionsList.Show();
            else
                this.versionsList.Hide();
            this.labelVersions.Visible = show;
        }

        private void ClearQuote()
        {
            // Clear Order datatables
            soinf.somastds.somast.Rows.Clear();
            soinf.ards.arcust.Rows.Clear();
            soinf.somastds.soaddr.Rows.Clear();
        }

        private void ClearLineitems()
        {
            soinf.ClearLineItemData();
            // Clear the line selector grids
            dataGridViewSelectedItems.AutoGenerateColumns = false;
            dataGridViewSelectedItems.DataSource = soinf.socurrentitemsds.soline;
            bindingFeatureSelector.DataSource = soinf.featureds.view_immasterdata;
        }

        private void buttonClearLine_Click(object sender, EventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        soinf.UnlockSomast(soinf.somastds.somast[0].idcol);
                    }

                    ClearLineitems();
                }
            }
            else
            {
                ClearLineitems();
            }
        }

        private void EnableCoverTabPageDataControls(string Product)
        {
            foreach (Control c in TabControlOrderEntry.Controls)
            {
                if (c is TabPage)
                {
                    if (c.Name.ToUpper() == "TABPAGECOVERINFO")
                    {
                        foreach (Control d in c.Controls)
                        {
                            if (d is Label)
                            {
                                d.Enabled = true;
                                continue;
                            }
                            else
                            {
                                if (d is GroupBox)
                                {
                                    d.Enabled = true;
                                    foreach (Control f in d.Controls)
                                    {
                                        f.Enabled = true;
                                    }
                                }
                                else
                                {
                                    if (d.Name.ToUpper() == "TEXTBOXMATERIALDESC" || d.Name.ToUpper() == "TEXTBOXCOLORDESC")
                                    {
                                        if (Product != "Stock Cover")
                                        {
                                            d.Enabled = true;
                                        }
                                    }
                                    else
                                    {
                                        d.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void EnableTabPageDataControls(string TabpageName)
        {
            foreach (Control c in TabControlOrderEntry.Controls)
            {
                if (c is TabPage)
                {
                    if (c.Name.ToUpper() == TabpageName)
                    {
                        foreach (Control d in c.Controls)
                        {
                            if (d is Label)
                            {
                                d.Enabled = true;
                                continue;
                            }
                            else
                            {
                                if (d is GroupBox)
                                {
                                    d.Enabled = true;
                                    foreach (Control f in d.Controls)
                                    {
                                        f.Enabled = true;
                                    }
                                }
                                else
                                {
                                    d.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private string SelectProduct()
        {
            FrmProductSelector frmProductSelector = new FrmProductSelector();
            frmProductSelector.ShowDialog();
            return frmProductSelector.SelectedProduct;
        }

        private void buttonEditSoHead_click(object sender, EventArgs e)
        {
            string editstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
            if (editstatus == "OK")
            {
                CurrentState = "Edit SOHead";
                RefreshControls();
            }
            else
            {
                wsgUtilities.wsgNotice(editstatus);
            }
        }

        private void FrmSOHead_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        soinf.UnlockSomast(soinf.somastds.somast[0].idcol);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        # region Hardware Processing

        private void buttonCalchardware_Click_1(object sender, EventArgs e)
        {
            // Combine the items tables
            soinf.CombineCurrentItems();
            CurrentFeature = "";
            soinf.CalcHardware();
            soinf.RefreshTotals();
        }

        #endregion Hardware Processing

        private void SaveLineItems()
        {
            if (soinf.socurrentitemsds.soline.Rows.Count > 0)
            {
                soinf.CombineCurrentItems();
                // Clear the current items table rows
                soinf.featureds.view_immasterdata.Rows.Clear();
                soinf.featureds.view_immasterdata.AcceptChanges();
                CurrentFeature = "";
                soinf.RefreshTotals();
                RefreshControls();
            }
        }

        private void CancelLineItems()
        {
            // Clear the current items table rows
            soinf.socurrentitemsds.soline.Rows.Clear();
            soinf.socurrentitemsds.AcceptChanges();
            soinf.featureds.view_immasterdata.Rows.Clear();
            soinf.featureds.view_immasterdata.AcceptChanges();
            CurrentFeature = "";
        }

        private void buttonEditLine_click(object sender, EventArgs e)
        {
            string SoStatus = soinf.GetSoStatus();
            if (SoStatus == "Open")
            {
                string editstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
                if (editstatus == "OK")
                {
                    CurrentState = "Edit Line";
                    CurrentFeature = "";
                    RefreshControls();
                    RefreshCover();
                }
                else
                {
                    wsgUtilities.wsgNotice(editstatus);
                }
            }
            else
            {
                wsgUtilities.wsgNotice("This order has been " + SoStatus);
            }
        }

        private void buttonEditSoHead_Click(object sender, EventArgs e)
        {
            string SoStatus = soinf.GetSoStatus();

            if (SoStatus == "Open")
            {
                string editstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
                if (editstatus == "OK")
                {
                    CurrentState = "Edit SOHead";
                    RefreshControls();
                }
                else
                {
                    wsgUtilities.wsgNotice(editstatus);
                }
            }
            else
            {
                wsgUtilities.wsgNotice("This order has been " + SoStatus);
            }
        }

        private void buttonSalesmn_click(object sender, EventArgs e)
        {
            // Show the selector screen
            string salescode = alereCodeMethods.SelectCode("SLS");
            if (salescode.Trim() != "")
            {
                soinf.somastds.somast[0].salesmn = salescode.Substring(0, 4);
            }
            else
            {
                soinf.somastds.somast[0].salesmn = " ";
            }
            soinf.somastds.somast.AcceptChanges();
            this.Update();
        }

        private void buttonVersion_Click_1(object sender, EventArgs e)
        {
            // Display a form to allow version selection
            if (soinf.somastds.view_versiondata.Rows.Count != 0)
            {
                // For version selection, use report format
                soinf.somastds.view_soreportlinedata.Rows.Clear();
                soinf.getallsoreportdata(soinf.somastds.somast[0].sono);
                // Delete all but cover rows
                for (int i = 0; i < soinf.quorptds.view_soreportlinedata.Rows.Count; i++)
                {
                    if (soinf.quorptds.view_soreportlinedata[i].source.TrimEnd() != "C")
                    {
                        soinf.quorptds.view_soreportlinedata.Rows[i].Delete();
                    }
                }
                FrmSelectVersion frmSelectVersion = new FrmSelectVersion();
                frmSelectVersion.bindingVersionData.DataSource = soinf.quorptds.view_soreportlinedata;
                frmSelectVersion.ShowDialog();
                string newversion = frmSelectVersion.SelectedVersion;
                if (newversion.TrimEnd() != "")
                {
                    ProcessSo(newversion, "");
                }
            }
            else
            {
                wsgUtilities.wsgNotice("There are no saved versions for this Estimate");
            }
        }

        private void listBoxFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                dataGridViewSelectedItems.AutoGenerateColumns = false;
                dataGridViewSelectedItems.DataSource = soinf.socurrentitemsds.soline;
                dataGridViewSelectedItems.RowsDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewSelectedItems.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
                // Initialize the search key
                Featurekey = "";
                // Load the features table and establish the current feature
                CurrentFeature = soinf.GetFeatures(listBoxFeatures.SelectedValue.ToString(), CurrentFeature);
                dataGridViewFeatureSelector.AutoGenerateColumns = false;
                bindingFeatureSelector.DataSource = soinf.featureds.view_immasterdata;
                dataGridViewFeatureSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewFeatureSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
                dataGridViewFeatureSelector.Focus();
            }
        }

        public string CreateNewVersion(string selectedVersion = null)
        {
            if (soinf.somastds.somast[0].sotype == "B")
            {
                string newversion = String.Empty;
                string priorversion = String.Empty;
                soinf.LoadVersionViewData(soinf.somastds.somast[0].sono);
                if (soinf.somastds.view_versiondata.Rows.Count > 0)
                {
                    // Find the last row in the version view and increment that version
                    int lastrow = soinf.somastds.view_versiondata.Rows.Count - 1;
                    char c = Convert.ToChar(soinf.somastds.view_versiondata[lastrow].version);
                    if (string.IsNullOrEmpty(selectedVersion))
                        priorversion = c.ToString();
                    else
                        priorversion = selectedVersion;

                    c++;
                    newversion = c.ToString();
                    CurrentVersion = newversion;
                    if (wsgUtilities.wsgReply("Copy all version data?"))
                    {
                        soinf.CreateNewVersionData(soinf.somastds.somast[0].sono, priorversion, newversion, soinf.somastds.soversion[0].custcomments,
                        soinf.somastds.soversion[0].intcomments);
                    }
                }
                else
                {
                    newversion = "A";
                }
                ProcessSo(newversion, "");
            }
            else
            {
                wsgUtilities.wsgNotice("This order was Converted. Versions can't be added.");
            }

            return CurrentVersion;
        }

        private void dataGridViewFeatureSelector_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            CurrentItem = soinf.CaptureDataGridColumn(dataGridViewFeatureSelector, "item");
            soinf.GetQuoteItemData(CurrentItem, CurrentFeature);
            this.Update();
        }

        private void dataGridViewResults_KeyDown(object sender, KeyEventArgs e)
        {
            int ix = 0;
            switch (e.KeyCode)
            {
                case Keys.Return:
                    {
                        CurrentItem = soinf.CaptureDataGridColumn(dataGridViewFeatureSelector, "item");
                        soinf.GetQuoteItemData(CurrentItem, CurrentFeature);
                        this.Update();
                        break;
                    }
                case Keys.Home:
                    {
                        Featurekey = "";
                        dataGridViewFeatureSelector.CurrentCell = dataGridViewFeatureSelector.Rows[0].Cells[0];
                        break;
                    }
                default:
                    {
                        if (this.Featurekey != null)
                        {
                            if (Featurekey.Length > 4)
                            {
                                Featurekey = "";
                            }
                            Featurekey += Convert.ToChar(e.KeyCode).ToString().ToUpper();
                            while (ix < dataGridViewFeatureSelector.RowCount - 1)
                            {
                                string x = dataGridViewFeatureSelector.Rows[ix].Cells[0].Value.ToString().ToUpper();
                                if (x.Substring(0, Featurekey.Length) == Featurekey)
                                {
                                    dataGridViewFeatureSelector.CurrentCell = dataGridViewFeatureSelector.Rows[ix].Cells[0];
                                    break;
                                }
                                else
                                {
                                    ix++;
                                    continue;
                                }
                            }
                        }
                        break;
                    }
            }
        }

        private void dataGridViewSelectedItems_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (wsgUtilities.wsgReply("Delete this item?"))
                {
                    soinf.DeleteCurrentItemsRow(dataGridViewSelectedItems);
                    soinf.socurrentitemsds.soline.AcceptChanges();
                    this.Update();
                }
            }
        }

        private void buttonClearLine_Click_1(object sender, EventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        soinf.UnlockSomast(soinf.somastds.somast[0].idcol);
                    }
                    ClearLineitems();
                }
            }
            else
            {
                ClearLineitems();
            }
        }

        private void buttonLineItemsSave_Click_1(object sender, EventArgs e)
        {
            SaveLineItems();
        }

        private void buttonLineitemsCancel_Click_1(object sender, EventArgs e)
        {
            CancelLineItems();
        }

        private void buttonComments_Click(object sender, EventArgs e)
        {
            FrmEstimateComment frmEstimateComment = new FrmEstimateComment();
            frmEstimateComment.versionds = soinf.somastds;
            frmEstimateComment.ShowDialog();
        }

        private void comboBoxMaterial_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                soinf.clineds.socover[0].materialid = Convert.ToInt32(comboBoxMaterial.SelectedValue);
                comboBoxMaterial.Visible = false;
                RefreshCover();
                this.Update();
                this.versionsList.UpdateVersionCover(this.CurrentVersion, this.CurrentCover);
            }
        }

        private void comboBoxColor_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                soinf.clineds.socover[0].colorid = Convert.ToInt32(comboBoxColor.SelectedValue);
                comboBoxColor.Visible = false;
                RefreshCover();
                this.versionsList.UpdateVersionCover(this.CurrentVersion, this.CurrentCover);
            }
        }

        private void comboBoxSpacing_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                soinf.clineds.socover[0].spacingid = Convert.ToInt32(comboBoxSpacing.SelectedValue);
                comboBoxSpacing.Visible = false;
                RefreshCover();
                this.Update();
                this.versionsList.UpdateVersionCover(this.CurrentVersion, this.CurrentCover);
            }
        }

        private void comboBoxOverlap_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                soinf.clineds.socover[0].overlapid = Convert.ToInt32(comboBoxOverlap.SelectedValue);
                comboBoxOverlap.Visible = false;
                RefreshCover();
                this.Update();
                this.versionsList.UpdateVersionCover(this.CurrentVersion, this.CurrentCover);
            }
        }

        private void comboBoxColor_Enter(object sender, EventArgs e)
        {
            comboBoxColor.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxColor.Focus();
        }

        private void textBoxSpacingdesc_Enter_1(object sender, EventArgs e)
        {
            comboBoxSpacing.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxSpacing.Focus();
        }

        private void comboBoxOverlap_Enter(object sender, EventArgs e)
        {
            comboBoxOverlap.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxOverlap.Focus();
        }

        private void textBoxMaterialDesc_Enter_1(object sender, EventArgs e)
        {
            comboBoxMaterial.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxMaterial.Focus();
        }

        private void textBoxColordesc_Enter_1(object sender, EventArgs e)
        {
            comboBoxColor.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxColor.Focus();
        }

        private void textBoxOverlapdesc_Enter_1(object sender, EventArgs e)
        {
            comboBoxOverlap.Visible = true;
            timerSohead.Enabled = false;
            timerSohead.Enabled = true;
            comboBoxOverlap.Focus();
        }

        private void ProcessLineItemComment()
        {
            CurrencyManager xCM =
           (CurrencyManager)dataGridViewSelectedItems.BindingContext[dataGridViewSelectedItems.DataSource,
            dataGridViewSelectedItems.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            string descrip = xRow["descrip"].ToString();
            soinf.somastds.soversion[0].intcomments += Environment.NewLine + descrip;
            FrmEstimateComment frmEstimateComment = new FrmEstimateComment();
            frmEstimateComment.versionds = soinf.somastds;
            frmEstimateComment.ShowDialog();
        }

        private void dataGridViewSelectedItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProcessLineItemComment();
        }

        private void buttonCalccover_Click(object sender, EventArgs e)
        {
            CurrentFeature = "";
            RefreshCover();
            soinf.ValidateSOCover(CurrentFeature, false);
            soinf.RefreshTotals();
            RefreshControls();
        }

        private void buttonDeleteVersion_Click(object sender, EventArgs e)
        {
            if (soinf.somastds.view_versiondata.Rows.Count != 0)
            {
                // For version selection, use report format
                soinf.somastds.view_soreportlinedata.Rows.Clear();
                soinf.getallsoreportdata(soinf.somastds.somast[0].sono);
                // Delete all but cover rows
                for (int i = 0; i < soinf.quorptds.view_soreportlinedata.Rows.Count; i++)
                {
                    if (soinf.quorptds.view_soreportlinedata[i].source.TrimEnd() != "C")
                    {
                        soinf.quorptds.view_soreportlinedata.Rows[i].Delete();
                    }
                }
                FrmSelectVersion frmSelectVersion = new FrmSelectVersion();
                frmSelectVersion.bindingVersionData.DataSource = soinf.quorptds.view_soreportlinedata; ;
                frmSelectVersion.ShowDialog();
                string selectedVersion = frmSelectVersion.SelectedVersion;
                if (selectedVersion.TrimEnd() != "")
                {
                    this.DeleteVersion(selectedVersion);
                }
            }
            else
            {
                wsgUtilities.wsgNotice("There are no saved versions to delete");
                ProcessSo("", "");
            }
        }

        public void DeleteVersion(string version)
        {
            if (soinf.somastds.view_versiondata.Rows.Count != 0)
            {
                if (wsgUtilities.wsgReply("Delete Version " + version + "?") == true)
                {
                    soinf.DeleteSoversion(soinf.somastds.somast[0].sono, version);
                    ProcessSo("", "", loadVersionsList: true, forceReloadVersionsList:true);
                }
            }
            else
            {
                wsgUtilities.wsgNotice("There are no saved versions to delete");
                ProcessSo("", "");
            }
        }

        private void dataGridViewFeatureSelector_KeyDown(object sender, KeyEventArgs e)
        {
            int ix = 0;
            switch (e.KeyCode)
            {
                case Keys.Return:
                    {
                        CurrentItem = soinf.CaptureDataGridColumn(dataGridViewFeatureSelector, "item");
                        soinf.GetQuoteItemData(CurrentItem, CurrentFeature);
                        this.Update();
                        break;
                    }
                case Keys.Home:
                    {
                        Featurekey = "";
                        dataGridViewFeatureSelector.CurrentCell = dataGridViewFeatureSelector.Rows[0].Cells[0];
                        break;
                    }
                default:
                    {
                        if (this.Featurekey != null)
                        {
                            if (Featurekey.Length > 4)
                            {
                                Featurekey = "";
                            }
                            Featurekey += Convert.ToChar(e.KeyCode).ToString().ToUpper();
                            while (ix < dataGridViewFeatureSelector.RowCount - 1)
                            {
                                string x = dataGridViewFeatureSelector.Rows[ix].Cells[0].Value.ToString().ToUpper();
                                if (x.Substring(0, Featurekey.Length) == Featurekey)
                                {
                                    dataGridViewFeatureSelector.CurrentCell = dataGridViewFeatureSelector.Rows[ix].Cells[0];
                                    break;
                                }
                                else
                                {
                                    ix++;
                                    continue;
                                }
                            }
                        }
                        break;
                    }
            }
        }

        private void buttonPopulateShipto_Click(object sender, EventArgs e)
        {
            // Populate the ship-to if indicated
            soinf.PopulateShipTo();
            this.Update();
        }

        private void buttonCreatenewcover_Click(object sender, EventArgs e)
        {
            CreateNewCover();
        }

        public void CreateNewCover()
        {
            string newcover = "";
            soinf.LoadCoverViewData(soinf.somastds.somast[0].sono, CurrentVersion);
            if (soinf.somastds.view_coverdata.Rows.Count > 0)
            {
                // Find the last row in the cover view and increment that cover
                int lastrow = soinf.somastds.view_coverdata.Rows.Count - 1;
                char c = Convert.ToChar(soinf.somastds.view_coverdata[lastrow].cover);
                c++;
                newcover = c.ToString();
            }
            else
            {
                newcover = "A";
            }
            CurrentState = "Edit Line";
            ProcessSo(soinf.somastds.soversion[0].version, newcover);
        }

        private void buttonCover_Click(object sender, EventArgs e)
        {
            bool singleCover = false;
            string SelectedCover = miscDataMethods.GetSOVersionCover(soinf.somastds.soversion[0].sono, soinf.somastds.soversion[0].version, out singleCover);
            if (SelectedCover != "" && !singleCover)
            {
                ProcessSo(soinf.somastds.soversion[0].version, SelectedCover);
            }
        }

        private void buttonDeleteCover_Click(object sender, EventArgs e)
        {
            string SelectedCover = miscDataMethods.GetSOVersionCover(soinf.somastds.soversion[0].sono, soinf.somastds.soversion[0].version);
            if (SelectedCover != "")
            {
                if (wsgUtilities.wsgReply("Delete Cover " + SelectedCover + "?") == true)
                {
                    soinf.DeleteSocover(soinf.somastds.soversion[0].version, SelectedCover);
                    ProcessSo(soinf.somastds.soversion[0].version, "", forceReloadVersionsList:true);
                }
            }
        }

        private void buttonViewPDF_Click(object sender, EventArgs e)
        {
            FrmPDFViewer frmPDFViewer = new FrmPDFViewer();
            frmPDFViewer.ds = soinf.somastds;
            frmPDFViewer.ShowDialog();
        }

        private void buttonCreatenewversion_Click(object sender, EventArgs e)
        {
            CreateNewVersion();
        }

        private void labelAddlDisc_Click(object sender, EventArgs e)
        {
            FrmGetText frmGetText = new FrmGetText();
            frmGetText.textcontent = soinf.somastds.soversion[0].adddiscnote.TrimEnd();
            frmGetText.textrequest = "Additional Discount Notes";
            frmGetText.ShowDialog();
            soinf.somastds.soversion[0].adddiscnote = frmGetText.textcontent;
        }

        private void labelDepositReceived_Click(object sender, EventArgs e)
        {
            FrmGetText frmGetText = new FrmGetText();
            frmGetText.textcontent = soinf.somastds.soversion[0].depositreqnote.TrimEnd();
            frmGetText.textrequest = "Deposit Received Notes";
            frmGetText.ShowDialog();
            soinf.somastds.soversion[0].depositreqnote = frmGetText.textcontent;
        }

        private void buttonCreateInvoice_Click(object sender, EventArgs e)
        {
            bool OKtoBill = true;

            if (soinf.somastds.somast[0].sostat != " ")
            {
                wsgUtilities.wsgNotice("This order has been closed.");
                OKtoBill = false;
            }
            if (OKtoBill == true)
            {
                if (soinf.somastds.somast[0].sotype != "O")
                {
                    wsgUtilities.wsgNotice("You can only invoice orders.");
                    OKtoBill = false;
                }
            }
            if (OKtoBill == true)
            {
                // Check to see if the current step permits invoicing
                OKtoBill = trackingInf.IsStepOKToInvoice(soinf.somastds.somast[0].sono);
            }
            if (OKtoBill == true)
            {
                string lockstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
                if (lockstatus != "OK")
                {
                    wsgUtilities.wsgNotice(lockstatus);
                    OKtoBill = false;
                }
            }
            if (OKtoBill == true)
            {
                string InvoiceMesage = soinf.LockInvoicing();
                if (InvoiceMesage != "OK")
                {
                    OKtoBill = false;
                }
            }
            if (OKtoBill == true)
            {
                DateTime Invdate = DateTime.Now;
                FrmGetDate frmGetDate = new FrmGetDate();
                frmGetDate.ShowDialog();
                if (frmGetDate.DateSelected == true)
                {
                    Invdate = frmGetDate.SelectedDate;
                    soinf.somastds.somast[0].invno = soinf.CreateInvoice(soinf.somastds, Invdate);
                    if (soinf.somastds.somast[0].invno.TrimEnd() != "")
                    {
                        MakeInvoicePDF(soinf.somastds.somast[0].sono, soinf.somastds.somast[0].invno.TrimStart());
                        soinf.UnlockInvoicing();
                        SaveSo();
                        wsgUtilities.wsgNotice("Invoice number " + soinf.somastds.somast[0].invno.TrimStart() + " has been created.");
                    }
                }
                else
                {
                    wsgUtilities.wsgNotice("Invoice creation cancelled");
                }
                soinf.UnlockSomast(soinf.somastds.somast[0].idcol);
            }
            else
            {
                wsgUtilities.wsgNotice("Invoice creation cancelled");
            }
            soinf.UnlockInvoicing();
            soinf.UnlockSomast(soinf.somastds.somast[0].idcol);
        }

        private async Task buttonViewInvoice_Click(object sender, EventArgs e)
        {
            // Locate the pdf for this SO
            if (!await PdfStorage.OpenPdf("*I" + soinf.somastds.somast[0].sono.TrimStart() + "*.pdf"))
            {
                wsgUtilities.wsgNotice("There are no invoice PDFs for this Sales Order");
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            bool OKtoConvert = true;

            if (buttonConvert.Text.StartsWith("Conv"))
            {
                if (soinf.somastds.somast[0].sostat.TrimEnd() != "")
                {
                    wsgUtilities.wsgNotice("This order has been closed.");
                    OKtoConvert = false;
                }
                if (OKtoConvert == true)
                {
                    if (soinf.somastds.somast[0].sotype != "B")
                    {
                        wsgUtilities.wsgNotice("You can only convert bids.");
                        OKtoConvert = false;
                    }
                }
                if (OKtoConvert == true)
                {
                    string lockstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
                    if (lockstatus == "OK")
                    {
                        soinf.ConvertBid(CurrentVersion);
                        soinf.SaveOrderData();
                        // Save the sono for PDF Creation
                        string SaveSono = soinf.somastds.somast[0].sono;
                        MakeQuotePDF(SaveSono, soinf.somastds.somast[0].sotype);
                        wsgUtilities.wsgNotice("Conversion Complete.");
                        labelSoType.Text = "This is an order";
                        this.LoadVersionsList(true);
                        RefreshControls();
                    }
                    else
                    {
                        wsgUtilities.wsgNotice(lockstatus);
                    }
                }
            }
            else
            {
                if (soinf.somastds.somast[0].sostat.TrimEnd() != "")
                {
                    wsgUtilities.wsgNotice("This order has been closed.");
                    OKtoConvert = false;
                }
                if (OKtoConvert == true)
                {
                    if (soinf.somastds.somast[0].sotype != "O")
                    {
                        wsgUtilities.wsgNotice("You can only revert orders.");
                        OKtoConvert = false;
                    }
                }
                if (OKtoConvert == true)
                {
                    string lockstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
                    if (lockstatus == "OK")
                    {
                        soinf.RevertOrder();
                        soinf.SaveOrderData();
                        // Save the sono for PDF Creation
                        string SaveSono = soinf.somastds.somast[0].sono;
                        MakeQuotePDF(SaveSono, soinf.somastds.somast[0].sotype);
                        wsgUtilities.wsgNotice("Revert Complete.");
                        labelSoType.Text = "This is an estimate";
                        RefreshControls();
                    }
                    else
                    {
                        wsgUtilities.wsgNotice(lockstatus);
                    }
                }
            }
        }

        private void buttonPterms_Click(object sender, EventArgs e)
        {
            // Show the selector screen

            string termid = customerTermsMethods.SelectTerms();
            if (termid.TrimEnd() != "")
            {
                soinf.somastds.somast[0].termid = customerTermsMethods.AlereDs.coterms[0].termid;
                soinf.somastds.somast[0].pdays = customerTermsMethods.AlereDs.coterms[0].discdays;
                soinf.somastds.somast[0].pnet = customerTermsMethods.AlereDs.coterms[0].duedays;
                soinf.somastds.somast[0].pdisc = customerTermsMethods.AlereDs.coterms[0].discrate;
                soinf.somastds.somast[0].pterms = customerTermsMethods.AlereDs.coterms[0].payterms.Substring(0, 20);
            }
        }

        private void buttonViewQuote_Click(object sender, EventArgs e)
        {
            FrmCoverSODocumentViewer frmSODocumentViewer = new FrmCoverSODocumentViewer();
            frmSODocumentViewer.CurrentSono = soinf.somastds.somast[0].sono;
            frmSODocumentViewer.SODocument = "Estimate";
            frmSODocumentViewer.ShowDialog();
        }

        private void buttonCancelEdit_Click(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Abandon Edit") == true)
            {
                this.CancelEdit();
            }
        }

        private void buttonGetSO_Click(object sender, EventArgs e)
        {
            ShowSOSearch();
        }

        private void buttonCoverItem_Click(object sender, EventArgs e)
        {
            // Show the item selector screen
            FrmGetImmaster frmGetImmaster = new FrmGetImmaster();
            switch (soinf.clineds.socover[0].product.TrimEnd())
            {
                case "Stock Cover":
                    {
                        frmGetImmaster.SelectedCode = "ST";
                        break;
                    }
                case "Worksheet":
                    {
                        frmGetImmaster.SelectedCode = "WK";
                        break;
                    }
                default:
                    {
                        frmGetImmaster.SelectedCode = "CU";
                        break;
                    }
            }
            frmGetImmaster.ShowDialog();

            if (frmGetImmaster.SelectedItem != "")
            {
                // For stock covers, -99 will force a reca
                if (frmGetImmaster.SelectedCode == "ST")
                {
                    soinf.somastds.somast[0].produnits = -99;
                }
                soinf.clineds.socover[0].item = frmGetImmaster.SelectedItem;
                soinf.clineds.socover[0].descrip = soinf.GetItemDescription(soinf.clineds.socover[0].item);
                soinf.FillColorAndMaterial(frmGetImmaster.SelectedItem);
                RefreshCover();
            }
        }

        private void buttonVoid_Click(object sender, EventArgs e)
        {
            string SoStatus = soinf.GetSoStatus();
            if (SoStatus == "Open")
            {
                if (wsgUtilities.wsgReply("Are you sure that you want to void this order?") == true)
                {
                    string lockstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
                    if (lockstatus == "OK")
                    {
                        soinf.somastds.somast[0].sostat = "V";
                        trackingInf.SetCancelledTrackingStep(soinf.somastds.somast[0].sono);
                        SaveSo();
                    }
                    else
                    {
                        wsgUtilities.wsgNotice(lockstatus);
                    }
                }
            }
            else
            {
                if (SoStatus == "Voided")
                {
                    if (wsgUtilities.wsgReply("Are you sure that you want to re-open this order?") == true)
                    {
                        string lockstatus = soinf.LockSomast(soinf.somastds.somast[0].idcol);
                        if (lockstatus == "OK")
                        {
                            soinf.somastds.somast[0].sostat = " ";
                            SaveSo();
                        }
                        else
                        {
                            wsgUtilities.wsgNotice(lockstatus);
                        }
                    }
                }
                else
                {
                    wsgUtilities.wsgNotice("This order has been " + SoStatus);
                }
            }
        }

        private async Task buttonRepairPDF_Click(object sender, EventArgs e)
        {
            string fileprefix = "R";

            RepairEstimate rpt = new RepairEstimate();
            if (MakeReportPDF(soinf.somastds.somast[0].sono, fileprefix, rpt) == true)
            {
                // Locate the pdf for this SO
                string sono = soinf.somastds.somast[0].sono.TrimStart();
                string filename = fileprefix + sono + ".pdf";
                if (!await PdfStorage.OpenPdf(filename))
                {
                    wsgUtilities.wsgNotice($"There are no PDFs for {sono} found at {System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], filename)}");
                }
            }
        }

        private async Task buttonReplacementPDF_Click(object sender, EventArgs e)
        {
            string fileprefix = "RC";

            replacement rpt = new replacement();
            if (MakeReportPDF(soinf.somastds.somast[0].sono, fileprefix, rpt) == true)
            {
                // Locate the pdf for this SO
                string sono = soinf.somastds.somast[0].sono.TrimStart();
                string filename = fileprefix + sono + ".pdf";
                if (!await PdfStorage.OpenPdf(filename))
                {
                    wsgUtilities.wsgNotice($"There are no PDFs for {sono} found at {System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], filename)}");
                }
            }
        }

        private async Task buttonMeasurementPDF_Click(object sender, EventArgs e)
        {
            string fileprefix = "MS";

            measurement rpt = new measurement();
            if (MakeReportPDF(soinf.somastds.somast[0].sono, fileprefix, rpt) == true)
            {
                // Locate the pdf for this SO
                string sono = soinf.somastds.somast[0].sono.TrimStart();
                string filename = fileprefix + sono + ".pdf";
                if (!await PdfStorage.OpenPdf(filename))
                {
                    wsgUtilities.wsgNotice($"There are no PDFs for {sono} found at {System.IO.Path.Combine(ConfigurationManager.AppSettings["SOPDFPath"], filename)}");
                }
            }
        }

        private void buttonConvertWorksheet_Click(object sender, EventArgs e)
        {
            soinf.ConvertWorksheet();
            if (soinf.clineds.socover[0].product.TrimEnd().ToUpper() != "WORKSHEET")
            {
                RefreshControls();
            }
        }

        private void buttonImportInspection_Click(object sender, EventArgs e)
        {
            soinf.ImportInspection();
            this.Update();
        }

        private void buttonGeneratePDFs_Click(object sender, EventArgs e)
        {
            FrmPDFGenerator frmPDFGenerator = new FrmPDFGenerator();
            frmPDFGenerator.soinf = soinf;
            frmPDFGenerator.ShowDialog();
        }

        private void buttonRoute_Click(object sender, EventArgs e)
        {
            soinf.RouteSo();
            labelTrackingData.Text = trackingInf.GetLastSOTrackingData(soinf.somastds.somast[0].sono);
        }

        private void FrmSOHead_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width + 450, this.Size.Height);

            this.versionsList = new ScrollingVersionsPanel(this, this.panelVersions);
            this.ShowVersionsList(false);
        }

        private void labelTrackingData_Click(object sender, EventArgs e)
        {
        }

        private void buttonTrackingHistory_Click(object sender, EventArgs e)
        {
            if (soinf.somastds.somast.Rows.Count > 0)
            {
                FrmSoTrackingActivity frmSoTrackingActivity = new FrmSoTrackingActivity();
                frmSoTrackingActivity.Sono = soinf.somastds.somast[0].sono;
                frmSoTrackingActivity.MdiParent = this.MdiParent;
                frmSoTrackingActivity.Show();
            }
        }

        private void buttonFileNumber_Click(object sender, EventArgs e)
        {
            if (soinf.somastds.somast[0].sotype == "O")
            {
                if (soinf.somastds.somast[0].meycono.TrimEnd() != "")
                {
                    wsgUtilities.wsgNotice("The file number must be blank before a new one can be assigned.");
                }
                else
                {
                    soinf.somastds.somast[0].meycono = appInformation.GetMeycono();
                    soinf.somastds.somast.AcceptChanges();
                    this.Update();
                }
            }
            else
            {
                wsgUtilities.wsgNotice("The file number can only be changed on orders.");
            }
        }

        private void buttonTickets_Click(object sender, EventArgs e)
        {
            soinf.ShowTickets();
        }

        private void buttonTaxDistrict_Click(object sender, EventArgs e)
        {
            string taxdistrict = appInformation.SelectTaxTable();
            if (taxdistrict != "")
            {
                soinf.somastds.somast[0].taxdist = taxdistrict;
                soinf.somastds.somast[0].taxrate = appInformation.GetDistrictTaxRate(taxdistrict);
                labelTaxdescrip.Text = appInformation.GetDistrictDescription(soinf.somastds.somast[0].taxdist);
            }
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]{
                this.textBoxCustno,
                this.buttonGetCustomer,
                this.textBoxSoNo,
                this.buttonGetSO,
                this.textBoxEmail,
                this.textBoxPhone,
                this.textBoxFaxNo,
                this.buttonVoid,
                this.buttonCreateInvoice,
                this.buttonConvert,
                this.buttonTickets,
                this.TabPageAcctDetails,
                this.textBoxAcctMemo,
                this.TabPageDiscounts, 

                //main form buttons 
                this.buttonRoute,
                this.buttonClearOrder,
                this.buttonClose,
                this.buttonEditSoHead,
                this.buttonEditLine,
                this.buttonSave,
                this.buttonCad,
                this.buttonImportInspection,
                this.buttonDeleteVersion,
                this.buttonDeleteCover,
                this.buttonGeneratePDFs,
                this.buttonViewPDF,
                this.buttonViewQuote,
                this.buttonTrackingHistory,

                //cover data tab
                this.textBoxVersion,
                this.textBoxCover,
                this.dataGridViewFeatureSelector,
                this.textBoxDepositact,
                this.textBoxAdddiscrate,
                this.dataGridViewSelectedItems,
                this.listBoxFeatures,
                this.dataGridViewResults,
                this.textBoxCoverDesc,
                this.textBoxProduct,
                this.comboBoxMaterial,
                this.textBoxMaterialDesc,
                this.comboBoxColor,
                this.comboBoxSpacing,
                this.textBoxColordesc,
                this.textBoxSpacingdesc,
                this.comboBoxOverlap,
                this.textBoxOverlapdesc,
                this.buttonCreatenewversion,
                this.buttonCoverItem,
                this.buttonLineItemsSave,
                this.buttonCalchardware,
                this.buttonCalccover,
                this.buttonLineitemsCancel,
                this.buttonConvertWorksheet,
                this.buttonCancelEdit,
                this.buttonVersion,
                this.buttonCover,
                this.buttonCreatenewcover,
                this.buttonComments,

                //header info tab
                
                this.textBoxBillto,
                this.buttonShipto,
                this.textBoxShiptocompany,
                this.textBoxShiptoaddress1,
                this.textBoxShiptoaddress2,
                this.textBoxShiptocity,
                this.textBoxShiptostate,
                this.textBoxShiptozip,
                this.textBoxFname,
                this.textBoxLname,
                this.textBoxAddress,
                this.textBoxCity,
                this.textBoxState,
                this.textBoxZip,
                this.textBoxHoemailaddr,
                this.buttonPopulateShipto,

                this.textBoxSodate,
                this.dateTimePickerSodate,
                this.textBoxOrdate,
                this.dateTimePickerOrdate,
                this.textBoxPonum,
                this.textBoxMeycono,
                this.buttonFileNumber,
                this.textBoxOldplan,
                this.textBoxOrderby,
                this.textBoxTaxrate,
                this.textBoxSalesmn,
                this.buttonSalesmn,
                this.textBoxShipvia,
                this.textBoxFob,
                this.textBoxPterms,
                this.buttonPterms,
                this.textBoxTerr,
                this.buttonTaxDistrict,
                this.textBoxTaxdist,
                this.comboBoxLocation,
                this.textBoxTrackno,

                //Cover data
                this.textBoxVersion,
                this.buttonVersion,
                this.textBoxCover,
                this.buttonCover,
                this.buttonCreatenewcover,
                this.buttonConvertWorksheet,
                this.buttonCancelEdit,
                this.buttonComments,
                this.textBoxDepositact,
                this.textBoxProduct,
                this.textBoxCoverDesc,
                this.textBoxMaterialDesc,
                this.comboBoxMaterial,
                this.textBoxColordesc,
                this.comboBoxColor,
                this.textBoxSpacingdesc,
                this.comboBoxSpacing,
                this.textBoxOverlapdesc,
                this.comboBoxOverlap,
                this.textBoxAdddiscrate,
                this.buttonCoverItem,
                this.buttonCalccover,

                this.dataGridViewFeatureSelector,
                this.dataGridViewSelectedItems,
                this.dataGridViewResults,

                this.listBoxFeatures,

                this.buttonCalchardware,
                this.buttonLineItemsSave,
                this.buttonLineitemsCancel,
                this.buttonCreatenewversion,
            });
        }

        private void ClearTabOrder()
        {
            for (int n = 0; n < this.Controls.Count; n++)
            {
                this.ClearTabOrder(this.Controls[n]);
            }
        }

        private void ClearTabOrder(Control c)
        {
            c.TabIndex = 10000;
            for (int n = 0; n < c.Controls.Count; n++)
            {
                this.ClearTabOrder(c.Controls[n]);
            }
        }

        private void SetDepositTiersDefaultState(bool useDefault)
        {
            checkBoxDefaultCustTiers.Visible = true;
            checkBoxDefaultCustTiers.Enabled = true;
            labelDefaultCustTiers.Visible = true;

            //if default, set the values of the textboxes from arcust defaults 
            if (useDefault)
            {
                SetTextBoxDollarsBinding(textBoxDepover0, soinf.ards, "arcust.depover0");
                SetTextBoxDollarsBinding(textBoxDepover1, soinf.ards, "arcust.depover1");
                SetTextBoxDollarsBinding(textBoxDepover2, soinf.ards, "arcust.depover2");
                SetTextBoxDollarsBinding(textBoxDepover3, soinf.ards, "arcust.depover3");
            }
            else
            {
                SetTextBoxDollarsBinding(textBoxDepover0, soinf.soitemsds, "deposittiers.depover0");
                SetTextBoxDollarsBinding(textBoxDepover1, soinf.soitemsds, "deposittiers.depover1");
                SetTextBoxDollarsBinding(textBoxDepover2, soinf.soitemsds, "deposittiers.depover2");
                SetTextBoxDollarsBinding(textBoxDepover3, soinf.soitemsds, "deposittiers.depover3");
            }

            this.textBoxDepover0.Enabled = !useDefault;
            this.textBoxDepover1.Enabled = !useDefault;
            this.textBoxDepover2.Enabled = !useDefault;
            this.textBoxDepover3.Enabled = !useDefault;
            this.checkBoxDefaultCustTiers.Checked = useDefault;
        }

        private bool DepositTiersUsingDefaults()
        {
            var currentValues = this.soinf.soitemsds.Tables["deposittiers"].Rows[0].ItemArray;
            decimal[] defaultValues =  {
                this.soinf.ards.arcust[0].depover0, this.soinf.ards.arcust[0].depover1,
                this.soinf.ards.arcust[0].depover2, this.soinf.ards.arcust[0].depover3
            };
            
            for (int n=0; n< defaultValues.Length; n++)
            {
                var currentValue = Decimal.Parse(currentValues[n].ToString());
                if (currentValue != defaultValues[n])
                    return false;
            }

            return true;
        }

        private void LoadVersionsList(bool force = false)
        {
            if (this.versionsList.Count > 0)
            {
                if (!force)
                {
                    //soft reload
                    this.versionsList.SoftReload();
                    return;
                }
            }
            this.versionsList.Clear();

            var versionCovers = soinf.GetCoversByVersion(CurrentSono); 

            foreach(string version in versionCovers.Keys)
            {
                this.versionsList.AddVersion(version, versionCovers[version]);
            }

            this.ShowVersionsList(this.versionsList.Count >= 1);
        }

    } // class
}// namespace