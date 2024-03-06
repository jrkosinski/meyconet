using CommonAppClasses;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousOrderEntry
{
    public partial class FrmMiscOrder : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");

        // Create the App Information processing object
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");

        private CustomerTermsMethods customerTermsMethods = new CustomerTermsMethods();
        private AlereCodeMethods alereCodeMethods = new AlereCodeMethods();

        // Create the SO Information processing object
        private BindingSource bindingResults = new BindingSource();

        private BindingSource bindingCurrentItems = new BindingSource();
        private MiscordInformation miscordinf = new MiscordInformation("SQL", "SQLConnString");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");
        private BindingSource bindingFeatureSelector = new BindingSource();

        public FrmMiscOrder()
        {
            miscordinf.parentform = this;
            PassedSono = "";
            CurrentState = "Select";
            InitializeComponent();
            // Set the timer interval here
            // Time is in milliseconds 10,000 = 10 seconds
            timerSohead.Interval = 20000;
            LoadingLine = true;
            miscordinf.EstablishSoitemsTable();
            // Set all bindings
            SetSoHeadBindings();
            SetCustomerBindings();
            SetShiptoBindings();
            SetLineItemBindings();
            labelTrackingData.Text = "";
            miscordinf.EstablishBlankSomastData();
            CurrentFeature = "";
            dataGridViewResults.AutoGenerateColumns = false;
            bindingResults.DataSource = miscordinf.resultsds.soline;
            dataGridViewResults.DataSource = bindingResults;
            dataGridViewResults.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewResults.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            miscordinf.EstablishCurrentitemsTable();
            TabControlOrderEntry.SelectedTab = tabPageOrderData;
            textBoxShiptocompany.SelectionStart = 0;
        }

        public int CurrentCustid { get; set; }
        public string CurrentCustno { get; set; }

        public string CurrentState { get; set; }
        public string PassedSono { get; set; }
        public int CurrentSomastid { get; set; }
        public string CurrentItem { get; set; }
        public bool Quoting { get; set; }
        public bool NewOrder { get; set; }
        public bool CustomCover { get; set; }
        public bool StockCover { get; set; }
        public bool LoadingLine { get; set; }
        public string Featurekey { get; set; }
        public string CurrentFeature { get; set; }

        private void buttonSoHeadClose_Click(object sender, EventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Enter Order")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Enter Order")
                    {
                        miscordinf.UnlockSomast(miscordinf.somastds.somast[0].idcol);
                    }
                    // Note: We need the following line to prevent the closing event from duplicating the question
                    CurrentState = "Select";
                    this.Close();
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
            if (getCust.SelectedCustid != 0)
            {
                miscordinf.getSingleCustomerData(CurrentCustid);
                InitializeOrder();
                CurrentState = "Enter Order";
                ProcessSo();
            }
        }

        #region Set Bindings

        public void SetShiptoBindings()
        {
            textBoxShiptocompany.DataBindings.Add("Text", miscordinf.somastds.soaddr, "company");
            textBoxShiptoaddress1.DataBindings.Add("Text", miscordinf.somastds.soaddr, "address1");
            textBoxShiptoaddress2.DataBindings.Add("Text", miscordinf.somastds.soaddr, "address2");
            textBoxShiptocity.DataBindings.Add("Text", miscordinf.somastds.soaddr, "city");
            textBoxShiptostate.DataBindings.Add("Text", miscordinf.somastds.soaddr, "state");
            textBoxShiptozip.DataBindings.Add("Text", miscordinf.somastds.soaddr, "zip");
        }

        public void SetSoHeadBindings()
        {
            // SO number
            textBoxSoNo.DataBindings.Clear();
            textBoxSoNo.DataBindings.Add("Text", miscordinf.somastds.somast, "sono");

            // PO
            textBoxPonum.DataBindings.Clear();
            textBoxPonum.DataBindings.Add("Text", miscordinf.somastds.somast, "ponum");
            // Ship via, FOB
            textBoxFob.DataBindings.Add("Text", miscordinf.somastds.somast, "fob");
            textBoxShipvia.DataBindings.Clear();
            textBoxShipvia.DataBindings.Add("Text", miscordinf.somastds.somast, "shipvia");
            // Ordered by
            textBoxOrderby.DataBindings.Add("Text", miscordinf.somastds.somast, "orderby");
            // Old Plan
            textBoxOldplan.DataBindings.Add("Text", miscordinf.somastds.somast, "oldplan");
            // Pool Owner Information
            textBoxFname.DataBindings.Clear();
            textBoxFname.DataBindings.Add("Text", miscordinf.somastds.somast, "fname");
            textBoxLname.DataBindings.Clear();
            textBoxLname.DataBindings.Add("Text", miscordinf.somastds.somast, "lname");
            textBoxAddress.DataBindings.Clear();
            textBoxAddress.DataBindings.Add("Text", miscordinf.somastds.somast, "address");
            textBoxCity.DataBindings.Clear();
            textBoxCity.DataBindings.Add("Text", miscordinf.somastds.somast, "city");
            textBoxState.DataBindings.Clear();
            textBoxState.DataBindings.Add("Text", miscordinf.somastds.somast, "state");
            textBoxZip.DataBindings.Clear();
            textBoxZip.DataBindings.Add("Text", miscordinf.somastds.somast, "zip");
            // Reference, Order date and ship date
            textBoxMeycono.DataBindings.Clear();
            textBoxMeycono.DataBindings.Add("Text", miscordinf.somastds.somast, "meycono");
            //    dateTimePickerOrdate.DataBindings.Add("Value", miscordinf.somastds.somast, "ordate", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxOrdate.DataBindings.Add("Text", miscordinf.somastds.somast, "ordate");
            // dateTimePickerSodate.DataBindings.Add("Value", miscordinf.somastds.somast, "sodate", true, DataSourceUpdateMode.OnPropertyChanged);
            textBoxSodate.DataBindings.Add("Text", miscordinf.somastds.somast, "sodate");
            // Sales Rep
            textBoxSalesmn.DataBindings.Clear();
            textBoxSalesmn.DataBindings.Add("Text", miscordinf.somastds.somast, "salesmn");
            // Payment Terms
            textBoxPterms.DataBindings.Clear();
            textBoxPterms.DataBindings.Add("Text", miscordinf.somastds.somast, "pterms");
            // GL Link Code
            textBoxGlarec.DataBindings.Clear();
            textBoxGlarec.DataBindings.Add("Text", miscordinf.somastds.somast, "glarec");
            // Tax rate, Tax Dist, Territory
            SetTextBoxCurrencyBinding(textBoxTaxrate, miscordinf.somastds, "somast.taxrate");
            textBoxTaxdist.DataBindings.Clear();
            textBoxTaxdist.DataBindings.Add("Text", miscordinf.somastds.somast, "taxdist");
            textBoxTerr.DataBindings.Clear();
            textBoxTerr.DataBindings.Add("Text", miscordinf.somastds.somast, "terr");
            // Total Area
            textBoxSubtotal.DataBindings.Add("Text", miscordinf.somastds.somast, "subtotal");
            // Bind the Shipping
            SetTextBoxCurrencyBinding(textBoxShipping, miscordinf.somastds, "somast.shpamt");
            //    textBoxShipping.DataBindings.Add("Text", miscordinf.somastds.somast, "shpamt");
            //      textBoxOrdamt.DataBindings.Add("Text", miscordinf.somastds.somast, "ordamt");
            SetTextBoxCurrencyBinding(textBoxOrdamt, miscordinf.somastds, "somast.ordamt");
            textBoxTax.DataBindings.Add("Text", miscordinf.somastds.somast, "tax");
            textBoxTrackno.DataBindings.Add("Text", miscordinf.somastds.somast, "trackno");
        }

        public void SetLineItemBindings()
        {
            miscordinf.setfeaturerow("Standard Anchors", "HWA");
            miscordinf.setfeaturerow("Spacial Anchors", "HW1A");
            miscordinf.setfeaturerow("Anchor Accessories", "MIHA");
            miscordinf.setfeaturerow("Spring", "HWS");
            miscordinf.setfeaturerow("Spring Covers", "HWSC");
            miscordinf.setfeaturerow("Cover Accessories", "MIH");
            miscordinf.setfeaturerow("Cable", "MICH");
            miscordinf.setfeaturerow("Patch Kits", "MI1");
            miscordinf.setfeaturerow("Misc Material", "MI2");
            miscordinf.setfeaturerow("Drain", "DRAIN");
            miscordinf.setfeaturerow("Tools", "MIHT");
            miscordinf.setfeaturerow("Literature Samples", "LIT");
            miscordinf.setfeaturerow("Custom Tarps", "MT");
            miscordinf.setfeaturerow("BBQ Covers", "BBQ");
            listBoxFeatures.ValueMember = "code";
            listBoxFeatures.DisplayMember = "feature";
            listBoxFeatures.DataSource = miscordinf.dtfeatures;
        }

        public void SetCustomerBindings()
        {
            textBoxCustno.DataBindings.Add("Text", miscordinf.ards.arcust, "custno");
            textBoxEmail.DataBindings.Add("Text", miscordinf.ards.arcust, "email");
            textBoxPhone.DataBindings.Add("Text", miscordinf.ards.arcust, "phone");
            textBoxCompany.DataBindings.Add("Text", miscordinf.ards.arcust, "company");
            textBoxFaxNo.DataBindings.Add("Text", miscordinf.ards.arcust, "faxno");
            // Notes
            textBoxAcctMemo.DataBindings.Add("Text", miscordinf.ards.arcust, "acctmemo");
            textBoxDPref.DataBindings.Add("Text", miscordinf.ards.arcust, "dpref");

            // Use a custom function to display the fields as currency
            // Discounts and upcharge
            SetTextBoxDollarsBinding(textBoxDisc, miscordinf.somastds, "somast.salesdisc");
            SetTextBoxDollarsBinding(textBoxStockdisc, miscordinf.somastds, "somast.stockdisc");
            SetTextBoxDollarsBinding(textBoxStanddisc, miscordinf.somastds, "somast.standdisc");
            SetTextBoxDollarsBinding(textBoxCommldisc, miscordinf.somastds, "somast.commldisc");
            SetTextBoxDollarsBinding(textBoxRepdisc, miscordinf.somastds, "somast.repdisc");
            SetTextBoxDollarsBinding(textBoxRepldisc, miscordinf.somastds, "somast.repldisc");
            SetTextBoxDollarsBinding(textBoxShipdisc, miscordinf.somastds, "somast.shipdisc");
            SetTextBoxDollarsBinding(textBoxUpcharge, miscordinf.somastds, "somast.upcharge");
            // Deposit Requirements
            SetTextBoxDollarsBinding(textBoxDepover0, miscordinf.ards, "arcust.depover0");
            SetTextBoxDollarsBinding(textBoxDepover1, miscordinf.ards, "arcust.depover1");
            SetTextBoxDollarsBinding(textBoxDepover2, miscordinf.ards, "arcust.depover2");
            SetTextBoxDollarsBinding(textBoxDepover3, miscordinf.ards, "arcust.depover3");
        }

        #endregion Set Bindings

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

        public void RefreshControls()
        {
            buttonGetSO.Enabled = false;
            textBoxCustno.ReadOnly = true;
            textBoxSoNo.ReadOnly = true;
            textBoxSoNo.Enabled = false;
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
            textBoxTaxrate.Enabled = false;
            groupBoxTotals.Enabled = false;
            switch (CurrentState)
            {
                case "View":
                    {
                        DisableControls();
                        groupBoxTotals.Enabled = true;
                        buttonCreateInvoice.Enabled = true;
                        TabControlDiscountData.Enabled = true;
                        TabControlOrderEntry.Enabled = true;
                        buttonClearOrder.Enabled = true;
                        buttonEditLineItems.Enabled = true;
                        buttonEditSoHead.Enabled = true;
                        buttonCancelEdit.Enabled = false;
                        buttonVoid.Enabled = true;
                        buttonRoute.Enabled = true;
                        buttonViewPDF.Enabled = true;
                        buttonViewOrder.Enabled = true;
                        if (miscordinf.somastds.somast[0].sotype == "O")
                        {
                            buttonCreateInvoice.Enabled = true;
                            buttonConvert.Enabled = false;
                        }
                        else
                        {
                            buttonCreateInvoice.Enabled = false;
                            buttonConvert.Enabled = true;
                        }
                        break;
                    }

                case "Edit Line":
                    {
                        DisableControls();

                        TabControlDiscountData.Enabled = true;
                        panelDiscounts.Enabled = true;
                        TabControlOrderEntry.Enabled = true;
                        textBoxShipping.Enabled = true;
                        buttonSave.Enabled = true;
                        TabControlOrderEntry.Enabled = true;
                        buttonCancelEdit.Enabled = true;
                        buttonSave.Enabled = true;
                        EnableTabPageDataControls("TABPAGELINEITEMINFO");
                        groupBoxTotals.Enabled = true;
                        // Disable add version and add cover
                        buttonEditLineItems.Enabled = false;
                        buttonEditSoHead.Enabled = false;
                        break;
                    }
                case "Enter Order":
                    {
                        DisableControls();
                        textBoxTaxdist.Enabled = false;
                        textBoxTaxrate.Enabled = false;
                        TabControlOrderEntry.Enabled = true;
                        TabControlDiscountData.Enabled = true;
                        buttonSave.Enabled = true;
                        EnableTabPageDataControls("TABPAGEORDERDATA");
                        EnableTabPageDataControls("TABPAGELINEITEMINFO");
                        buttonEditSoHead.Enabled = false;
                        // Disable add version and add cover
                        buttonEditLineItems.Enabled = false;
                        buttonEditSoHead.Enabled = false;
                        break;
                    }
                case "Edit SOHead":
                    {
                        DisableControls();
                        textBoxTaxdist.Enabled = false;
                        textBoxTaxrate.Enabled = false;
                        TabControlDiscountData.Enabled = true;
                        TabControlOrderEntry.Enabled = true;
                        buttonSave.Enabled = true;
                        EnableTabPageDataControls("TABPAGEORDERDATA");
                        buttonEditSoHead.Enabled = false;
                        break;
                    }
                case "Select":
                    {
                        DisableControls();
                        LabelBid.Visible = false;
                        if (NewOrder == true)
                        {
                            buttonGetCustomer.Enabled = true;
                            buttonClose.Enabled = true;
                        }
                        else
                        {
                            buttonGetSO.Enabled = true;
                            textBoxSoNo.ReadOnly = false;
                            textBoxSoNo.Enabled = true;
                            buttonGetSO.Enabled = true;
                            buttonClose.Enabled = true;
                        }
                        break;
                    }
                case "SoHeadEntry":
                    {
                        DisableControls();
                        TabControlCustomerNotes.Enabled = true;
                        TabControlDiscountData.Enabled = true;
                        TabControlOrderEntry.Enabled = true;
                        buttonClose.Enabled = true;
                        buttonSave.Enabled = true;
                        buttonClose.Enabled = true;
                        break;
                    }
            }
            textBoxTaxdist.Enabled = false;
            textBoxTaxrate.Enabled = false;
            TabControlCustomerNotes.Enabled = true;
            LabelBid.Enabled = true;
            this.Update();
        }

        #endregion Refresh Controls

        #region Initialize Order

        private void InitializeOrder()
        {
            miscordinf.EstablishBlankSomastData();

            // Esttablish the customer table
            miscordinf.getSingleCustomerData(CurrentCustid);

            CurrentCustno = miscordinf.ards.arcust[0].custno;
            miscordinf.somastds.somast[0].sono = miscordinf.getsono();
            miscordinf.somastds.somast[0].custno = CurrentCustno;
            miscordinf.somastds.somast[0].custid = CurrentCustid;
            miscordinf.somastds.somast[0].pdays = miscordinf.ards.arcust[0].pdays;
            miscordinf.somastds.somast[0].pnet = miscordinf.ards.arcust[0].pnet;
            miscordinf.somastds.somast[0].pdisc = miscordinf.ards.arcust[0].pdisc;
            miscordinf.somastds.somast[0].pterms = miscordinf.ards.arcust[0].pterms;
            miscordinf.somastds.somast[0].termid = miscordinf.ards.arcust[0].termid;
            miscordinf.somastds.somast[0].taxrate = miscordinf.ards.arcust[0].tax;
            miscordinf.somastds.somast[0].fob = miscordinf.ards.arcust[0].fob;
            miscordinf.somastds.somast[0].shipvia = miscordinf.ards.arcust[0].shipvia;
            miscordinf.somastds.somast[0].glarec = miscordinf.ards.arcust[0].gllink;
            miscordinf.somastds.somast[0].salesmn = miscordinf.ards.arcust[0].salesmn;
            miscordinf.somastds.somast[0].salesdisc = miscordinf.ards.arcust[0].disc;
            miscordinf.somastds.somast[0].taxdist = miscordinf.ards.arcust[0].taxdist;
            miscordinf.somastds.somast[0].terr = miscordinf.ards.arcust[0].terr;
            miscordinf.somastds.somast[0].taxrate = miscordinf.ards.arcust[0].tax;
            miscordinf.somastds.somast[0].taxst = miscordinf.ards.arcust[0].state;
            miscordinf.somastds.somast[0].glarec = miscordinf.ards.arcust[0].gllink;
            miscordinf.somastds.somast[0].stockdisc = miscordinf.ards.arcust[0].stockdisc;
            miscordinf.somastds.somast[0].standdisc = miscordinf.ards.arcust[0].standdisc;
            miscordinf.somastds.somast[0].commldisc = miscordinf.ards.arcust[0].commldisc;
            miscordinf.somastds.somast[0].repldisc = miscordinf.ards.arcust[0].repldisc;
            miscordinf.somastds.somast[0].repdisc = miscordinf.ards.arcust[0].repdisc;
            miscordinf.somastds.somast[0].shipdisc = miscordinf.ards.arcust[0].shipdisc;
            miscordinf.somastds.somast[0].upcharge = miscordinf.ards.arcust[0].upcharge;
            // Establish Order record type
            miscordinf.somastds.somast[0].enterqu = "N";

            // Establish default location
            miscordinf.somastds.somast[0].defloc = appInformation.GetStkloc();

            if (Quoting == true)
            {
                miscordinf.somastds.somast[0].sotype = "B";
            }
            else
            {
                miscordinf.somastds.somast[0].sotype = "O";
            }
            // Establish the Ship To table and initialize the fields
            miscordinf.EstablishBlankSoaddrData();

            if (wsgUtilities.wsgReply("Enter Pool Owner Information") == true)
            {
                FrmPoolOwnerData frmPoolOwnerData = new FrmPoolOwnerData();
                frmPoolOwnerData.ShowDialog();
                if (frmPoolOwnerData.SaveData == true)
                {
                    miscordinf.somastds.somast[0].lname = frmPoolOwnerData.Lname;
                    miscordinf.somastds.somast[0].ponum = frmPoolOwnerData.Lname;
                    miscordinf.somastds.somast[0].fname = frmPoolOwnerData.Fname;
                    miscordinf.somastds.somast[0].address = frmPoolOwnerData.Address;
                    miscordinf.somastds.somast[0].city = frmPoolOwnerData.City;
                    miscordinf.somastds.somast[0].state = frmPoolOwnerData.State;
                    miscordinf.somastds.somast[0].zip = frmPoolOwnerData.Zip;

                    if (wsgUtilities.wsgReply("Copy Pool Owner Information to Ship-To") == true)
                    {
                        // Populate the ship-to if indicated
                        miscordinf.somastds.soaddr[0].company = miscordinf.somastds.somast[0].fname.TrimEnd() +
                        " " + miscordinf.somastds.somast[0].lname.TrimEnd();
                        miscordinf.somastds.soaddr[0].address1 = miscordinf.somastds.somast[0].address.TrimEnd();
                        miscordinf.somastds.soaddr[0].city = miscordinf.somastds.somast[0].city.TrimEnd();
                        miscordinf.somastds.soaddr[0].state = miscordinf.somastds.somast[0].state.TrimEnd();
                        miscordinf.somastds.soaddr[0].zip = miscordinf.somastds.somast[0].zip.TrimEnd();
                        miscordinf.somastds.soaddr.AcceptChanges();
                    }
                }
            }
            //Develop Ship-to Information if needed
            if (miscordinf.somastds.soaddr[0].company.TrimEnd() == "")
            {
                // Get the default ship to address for this customer, if any
                miscordinf.getDefaultShipToData(CurrentCustid);
                if (miscordinf.ards.aracadr.Rows.Count != 0)
                {
                    miscordinf.somastds.soaddr[0].cshipno = miscordinf.ards.aracadr[0].cshipno;
                    miscordinf.somastds.soaddr[0].adtype = "D";
                    miscordinf.somastds.soaddr[0].company = miscordinf.ards.aracadr[0].company;
                    miscordinf.somastds.soaddr[0].address1 = miscordinf.ards.aracadr[0].address1;
                    miscordinf.somastds.soaddr[0].address2 = miscordinf.ards.aracadr[0].address2;
                    miscordinf.somastds.soaddr[0].email = miscordinf.ards.aracadr[0].email;
                    miscordinf.somastds.soaddr[0].city = miscordinf.ards.aracadr[0].city;
                    miscordinf.somastds.soaddr[0].state = miscordinf.ards.aracadr[0].state;
                    miscordinf.somastds.soaddr[0].zip = miscordinf.ards.aracadr[0].zip;
                }
                else
                {
                    // If no ship to information, use customer information
                    miscordinf.somastds.soaddr[0].cshipno = miscordinf.ards.arcust[0].custno;
                    miscordinf.somastds.soaddr[0].adtype = "C";
                    miscordinf.somastds.soaddr[0].company = miscordinf.ards.arcust[0].company;
                    miscordinf.somastds.soaddr[0].address1 = miscordinf.ards.arcust[0].address1;
                    miscordinf.somastds.soaddr[0].address2 = miscordinf.ards.arcust[0].address2;
                    miscordinf.somastds.soaddr[0].email = miscordinf.ards.arcust[0].email;
                    miscordinf.somastds.soaddr[0].city = miscordinf.ards.arcust[0].city;
                    miscordinf.somastds.soaddr[0].state = miscordinf.ards.arcust[0].state;
                    miscordinf.somastds.soaddr[0].zip = miscordinf.ards.arcust[0].zip;
                }
            }
        }

        #endregion Initialize Order

        public void ShowSOSearch()
        {
            FrmSOSearch frmSoSearch = new FrmSOSearch(this.textBoxSoNo.Text, "N");
            frmSoSearch.ShowDialog();
            if (frmSoSearch.SelectedSono.TrimEnd() != "")
            {
                CurrentSomastid = miscordinf.GetSomastBySono(frmSoSearch.SelectedSono);
                
                CurrentCustno = miscordinf.somastds.somast[0].custno;
                CurrentCustid = miscordinf.somastds.somast[0].custid;
                // Load customer data for this SO
                miscordinf.getSingleCustomerData(CurrentCustid);
                CurrentState = "View";
                ProcessSo();
                
                RefreshControls();
            }
        }

        private void TextBoxCustno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CurrentCustid = miscordinf.getCustomerDatabyCustno(textBoxCustno.Text);
                if (CurrentCustid != 0)
                {
                    InitializeOrder();
                    CurrentState = "Enter Order";
                    ProcessSo();
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

        private void SetTextBoxCurrencyBinding(TextBox txtbox, order ds, string fieldname)
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
                ProcessExistingOrder(PassedSono);
                labelTaxdescrip.Text = appInformation.GetDistrictDescription(miscordinf.somastds.somast[0].taxdist);
            }
            RefreshControls();
        }

        private void dateTimePickerSodate_ValueChanged(object sender, EventArgs e)
        {
            miscordinf.somastds.somast[0].sodate = dateTimePickerSodate.Value;
            miscordinf.somastds.somast.AcceptChanges();
            this.Update();
        }

        private void dateTimePickerOrdate_ValueChanged(object sender, EventArgs e)
        {
            miscordinf.somastds.somast[0].ordate = dateTimePickerOrdate.Value;
            miscordinf.somastds.somast.AcceptChanges();
            this.Update();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveSo();
        }

        #region Save SO

        public void SaveSo()
        {
            if (miscordinf.ValidateSO(CurrentFeature, true) == true)
            {
                buttonSave.Enabled = false;
                // Save Order
                miscordinf.somastds.somast[0].defloc = comboBoxLocation.SelectedItem.ToString();
                miscordinf.SaveOrderData();
                if (trackingInf.GetLastSOTrackingData(miscordinf.somastds.somast[0].sono) == "No tracking data")
                {
                    miscordinf.RouteSo();
                }

                // Save the sono for PDF Creation
                string SaveSono = miscordinf.somastds.somast[0].sono;
                MakeSOPDF(miscordinf.somastds.somast[0].sono, miscordinf.somastds.somast[0].sotype);
                CurrentState = "View";
                // Reload Somast dataset
                CurrentSomastid = miscordinf.GetSomastBySono(SaveSono);
                ProcessSo();
                RefreshControls();
            }
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
                miscordinf.ProcessShipToSelection(frmGetShipAddress.SelectedShipToId);
            }
        }

        private bool MakeSOPDF(string sono, string sotype)
        {
            //  public ReportDocument rptOut {get; set;}
            bool pdfok = true;
            OrderRpt orderRpt = new OrderRpt();
            pdfok = MakeReportPDF(miscordinf.somastds.somast[0].sono, (sotype == "B" ? "E" : sotype), orderRpt);
            return pdfok;
        }

        private bool MakeReportPDF(string sono, string fileprefix, ReportClass rpt)
        {
            bool pdfOk = true;
            string fileName = fileprefix + sono.TrimStart() + ".pdf";
            string filePath = ConfigurationManager.AppSettings["SOPDFPath"] + fileName;

            if (appUtilities.IsFileOpen(filePath) == false)
            {
                miscordinf.getallsoreportdata(sono);

                rpt.SetDataSource(miscordinf.orderrptds);
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
            bool pdfOk = true;

            string fileName = "I" + sono.TrimStart() + ".pdf";
            string filepath = ConfigurationManager.AppSettings["SOPDFPath"] + fileName;

            if (appUtilities.IsFileOpen(filepath) == false)
            {
                miscordinf.getallsoreportdata(sono);

                InvoiceRpt rpt = new InvoiceRpt();
                rpt.SetDataSource(miscordinf.orderrptds);
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

        private void textBoxSoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ShowSOSearch();
            }
        }

        #region Process SO

        public void ProcessSo()
        {
            // This routine establishes all data tables for the order
            CurrentFeature = " ";
            // Develop BillTo information
            comboBoxLocation.SelectedItem = miscordinf.somastds.somast[0].defloc.TrimEnd().TrimStart(); ;
            textBoxBillto.Text = miscordinf.ards.arcust[0].company + Environment.NewLine;
            textBoxBillto.Text += miscordinf.ards.arcust[0].address1 + Environment.NewLine;
            // Establish date picker values
            dateTimePickerOrdate.Value = miscordinf.somastds.somast[0].ordate;
            dateTimePickerSodate.Value = miscordinf.somastds.somast[0].sodate;
            if (miscordinf.ards.arcust[0].address2.TrimEnd() != "")
            {
                textBoxBillto.Text += miscordinf.ards.arcust[0].address2 + Environment.NewLine;
            }
            textBoxBillto.Text += miscordinf.ards.arcust[0].city + Environment.NewLine;
            textBoxBillto.Text += miscordinf.ards.arcust[0].state.TrimEnd() + ", " +
            miscordinf.ards.arcust[0].zip + Environment.NewLine;
            if (miscordinf.somastds.somast[0].idcol > 0)
            {
                labelTrackingData.Text = trackingInf.GetLastSOTrackingData(miscordinf.somastds.somast[0].sono);
            }
            else
            {
                labelTrackingData.Text = "New Order";
            }
            // Loading Line variable prevents the selectedindex change method from firing on comboboxes
            LoadingLine = true;
            // Establish and clear Features Dataset
            dataGridViewFeatureSelector.AutoGenerateColumns = false;
            dataGridViewFeatureSelector.DataSource = bindingFeatureSelector;
            miscordinf.LoadLineItems();
            if (miscordinf.somastds.somast[0].sotype == "B")
            {
                LabelBid.Visible = true;
            }
            else
            {
                LabelBid.Visible = false;
            }
            miscordinf.RefreshTotals();
            RefreshControls();
            LoadingLine = false;
        }

        #endregion Process SO

        private void timerSohead_Tick(object sender, EventArgs e)
        {
            timerSohead.Enabled = false;
        }

        private void SetNumberText(TextBox textbox)
        {
        }

        private void SetStringTextBoxStart(TextBox textbox)
        {
            // MessageBox.Show("here");
            SendKeys.Send("{ENTER}");
            SendKeys.Send("{HOME}");
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

        private void SendTabonEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dataGridViewSelectedItems_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (wsgUtilities.wsgReply("Delete this item?"))
                {
                    miscordinf.DeleteCurrentRow(dataGridViewSelectedItems);
                    miscordinf.socurrentitemsds.soline.AcceptChanges();
                    this.Update();
                }
            }
        }

        private void buttonEditLine_Click(object sender, EventArgs e)
        {
            string editstatus = miscordinf.LockSomast(miscordinf.somastds.somast[0].idcol);
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
            ClearSO();
            ClearLineitems();
            miscordinf.ClearResultsTable();
            bindingResults.DataSource = miscordinf.resultsds.soline;
            CurrentState = "Select";
            RefreshControls();
        }

        private void ClearSO()
        {
            // Clear Order datatables
            miscordinf.somastds.somast.Rows.Clear();
            miscordinf.ards.arcust.Rows.Clear();
            miscordinf.somastds.soaddr.Rows.Clear();
        }

        private void ClearLineitems()
        {
            miscordinf.ClearLineItemData();
            // Clear the line selector grids
            dataGridViewSelectedItems.AutoGenerateColumns = false;
            bindingCurrentItems.DataSource = miscordinf.socurrentitemsds.soline;
            dataGridViewSelectedItems.DataSource = bindingCurrentItems;
            bindingFeatureSelector.DataSource = miscordinf.featureds.view_immasterdata;
        }

        private void buttonClearLine_Click(object sender, EventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        miscordinf.UnlockSomast(miscordinf.somastds.somast[0].idcol);
                    }

                    ClearLineitems();
                }
            }
            else
            {
                ClearLineitems();
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

        private void buttonEditSoHead_click(object sender, EventArgs e)
        {
            string editstatus = miscordinf.LockSomast(miscordinf.somastds.somast[0].idcol);
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
                        miscordinf.UnlockSomast(miscordinf.somastds.somast[0].idcol);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void SaveLineItems()
        {
            if (miscordinf.socurrentitemsds.soline.Rows.Count > 0)
            {
                miscordinf.CombineCurrentItems();
                // Clear the current items table rows
                miscordinf.featureds.view_immasterdata.Rows.Clear();
                miscordinf.featureds.view_immasterdata.AcceptChanges();
                CurrentFeature = "";
                miscordinf.RefreshTotals();
                RefreshControls();
            }
        }

        private void CancelLineItems()
        {
            // Clear the current items table rows
            miscordinf.socurrentitemsds.soline.Rows.Clear();
            miscordinf.socurrentitemsds.AcceptChanges();
            miscordinf.featureds.view_immasterdata.Rows.Clear();
            miscordinf.featureds.view_immasterdata.AcceptChanges();
            CurrentFeature = "";
        }

        private void buttonEditLine_click(object sender, EventArgs e)
        {
            string SoStatus = miscordinf.GetSoStatus();
            if (SoStatus == "Open")
            {
                string editstatus = miscordinf.LockSomast(miscordinf.somastds.somast[0].idcol);
                if (editstatus == "OK")
                {
                    CurrentState = "Edit Line";
                    CurrentFeature = "";
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

        private void buttonEditSoHead_Click(object sender, EventArgs e)
        {
            string SoStatus = miscordinf.GetSoStatus();

            if (SoStatus == "Open")
            {
                string editstatus = miscordinf.LockSomast(miscordinf.somastds.somast[0].idcol);
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
                miscordinf.somastds.somast[0].salesmn = salescode.Substring(0, 4);
            }
            else
            {
                miscordinf.somastds.somast[0].salesmn = " ";
            }
            miscordinf.somastds.somast.AcceptChanges();
            this.Update();
        }

        private void listBoxFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadingLine != true)
            {
                dataGridViewSelectedItems.AutoGenerateColumns = false;
                dataGridViewSelectedItems.DataSource = miscordinf.socurrentitemsds.soline;
                dataGridViewSelectedItems.RowsDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewSelectedItems.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
                // Initialize the search key
                Featurekey = "";
                // Load the features table and establish the current feature
                CurrentFeature = miscordinf.GetFeatures(listBoxFeatures.SelectedValue.ToString(), CurrentFeature);
                dataGridViewFeatureSelector.AutoGenerateColumns = false;
                bindingFeatureSelector.DataSource = miscordinf.featureds.view_immasterdata;
                dataGridViewFeatureSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewFeatureSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
                dataGridViewFeatureSelector.Focus();
            }
        }

        private void dataGridViewFeatureSelector_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            CurrentItem = miscordinf.CaptureDataGridColumn(dataGridViewFeatureSelector, "item");
            miscordinf.GetSOItemData(CurrentItem, CurrentFeature);
            this.Update();
        }

        private void dataGridViewResults_KeyDown(object sender, KeyEventArgs e)
        {
            int ix = 0;
            switch (e.KeyCode)
            {
                case Keys.Return:
                    {
                        miscordinf.CaptureDataGridColumn(dataGridViewFeatureSelector, "item");
                        miscordinf.GetSOItemData(CurrentItem, CurrentFeature);
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
                    miscordinf.DeleteCurrentItemsRow(dataGridViewSelectedItems);
                    miscordinf.socurrentitemsds.soline.AcceptChanges();
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
                        miscordinf.UnlockSomast(miscordinf.somastds.somast[0].idcol);
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
            /*
            FrmEstimateComment frmEstimateComment = new FrmEstimateComment();
            frmEstimateComment.versionds = miscordinf.somastds;
            frmEstimateComment.ShowDialog();

             */
        }

        private void ProcessLineItemComment()
        {
            CurrencyManager xCM =
           (CurrencyManager)dataGridViewSelectedItems.BindingContext[dataGridViewSelectedItems.DataSource,
            dataGridViewSelectedItems.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            string descrip = xRow["descrip"].ToString();
            FrmLineItemComment frmLineItemComment = new FrmLineItemComment();
            int CurrentLineIndex = dataGridViewSelectedItems.SelectedCells[0].OwningRow.Index;
            frmLineItemComment.commentds.soline.Rows.Clear();
            frmLineItemComment.commentds.soline.ImportRow(miscordinf.socurrentitemsds.soline.Rows[CurrentLineIndex]);
            frmLineItemComment.ShowDialog();
            miscordinf.socurrentitemsds.soline[CurrentLineIndex].intmemo = frmLineItemComment.commentds.soline[0].intmemo;
            miscordinf.socurrentitemsds.soline[CurrentLineIndex].custmemo = frmLineItemComment.commentds.soline[0].custmemo;
        }

        private void dataGridViewSelectedItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProcessLineItemComment();
        }

        private void dataGridViewFeatureSelector_KeyDown(object sender, KeyEventArgs e)
        {
            int ix = 0;
            switch (e.KeyCode)
            {
                case Keys.Return:
                    {
                        CurrentItem = miscordinf.CaptureDataGridColumn(dataGridViewFeatureSelector, "item");
                        miscordinf.GetSOItemData(CurrentItem, CurrentFeature);
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
            miscordinf.PopulateShipTo();
            this.Update();
        }

        private void buttonViewPDF_Click(object sender, EventArgs e)
        {
            FrmPDFViewer frmPDFViewer = new FrmPDFViewer();
            frmPDFViewer.ds = miscordinf.somastds;
            frmPDFViewer.ShowDialog();
        }

        private void buttonCreateInvoice_Click(object sender, EventArgs e)
        {
            bool OKtoBill = true;
            if (miscordinf.somastds.somast[0].sostat != " ")
            {
                wsgUtilities.wsgNotice("This order has been closed.");
                OKtoBill = false;
            }
            if (OKtoBill == true)
            {
                if (miscordinf.somastds.somast[0].sotype != "O")
                {
                    wsgUtilities.wsgNotice("You can only invoice orders.");
                    OKtoBill = false;
                }
            }
            if (OKtoBill == true)
            {
                OKtoBill = trackingInf.IsStepOKToInvoice(miscordinf.somastds.somast[0].sono);
            }
            // Lock somast
            if (OKtoBill == true)
            {
                string lockstatus = miscordinf.LockSomast(miscordinf.somastds.somast[0].idcol);
                if (lockstatus != "OK")
                {
                    wsgUtilities.wsgNotice(lockstatus);
                    OKtoBill = false;
                }
            }
            // Handle orders with no amount
            if (OKtoBill == true)
            {
                if (miscordinf.somastds.somast[0].ordamt == 0)
                {
                    wsgUtilities.wsgNotice("Zero amount order closed. No invoice created");
                    // Close the order
                    miscordinf.CloseMiscSostat(miscordinf.somastds.somast[0].sono);
                    // Process Inventory
                    miscordinf.ProcessMiscellaneousOrderInventory(miscordinf.somastds, DateTime.Now.Date);
                    OKtoBill = false;
                    miscordinf.UnlockSomast(miscordinf.somastds.somast[0].idcol);
                }
            }

            if (OKtoBill == true)
            {
                string InvoiceMesage = miscordinf.LockInvoicing();
                if (InvoiceMesage != "OK")
                {
                    OKtoBill = false;
                    miscordinf.UnlockSomast(miscordinf.somastds.somast[0].idcol);
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
                    miscordinf.somastds.somast[0].invno = miscordinf.CreateInvoice(miscordinf.somastds, Invdate);
                    miscordinf.somastds.somast[0].invdte = Invdate;
                    miscordinf.somastds.somast[0].sostat = "C";

                    if (miscordinf.somastds.somast[0].invno.TrimEnd() != "")
                    {
                        MakeInvoicePDF(miscordinf.somastds.somast[0].sono, miscordinf.somastds.somast[0].invno.TrimStart());
                        wsgUtilities.wsgNotice("Invoice number " + miscordinf.somastds.somast[0].invno.TrimStart() + " has been created.");
                        miscordinf.SaveOrderData();
                    }
                }
                else
                {
                    wsgUtilities.wsgNotice("Invoice creation cancelled");
                }
                miscordinf.UnlockSomast(miscordinf.somastds.somast[0].idcol);
                miscordinf.UnlockInvoicing();
            }
        }

        private async Task buttonViewInvoice_Click(object sender, EventArgs e)
        {
            // Locate the pdf for this SO
            if (!await PdfStorage.OpenPdf("*I" + miscordinf.somastds.somast[0].sono.TrimStart() + "*.pdf"))
            {
                wsgUtilities.wsgNotice("There are no invoice PDFs for this Sales Order");
            }
        }

        private void buttonCad_Click(object sender, EventArgs e)
        {
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            bool OKtoConvert = true;
            if (miscordinf.somastds.somast[0].sostat.TrimEnd() != "")
            {
                wsgUtilities.wsgNotice("This order has been closed.");
                OKtoConvert = false;
            }
            if (OKtoConvert == true)
            {
                if (miscordinf.somastds.somast[0].sotype != "B")
                {
                    wsgUtilities.wsgNotice("You can only convert bids.");
                    OKtoConvert = false;
                }
            }
            if (OKtoConvert == true)
            {
                string lockstatus = miscordinf.LockSomast(miscordinf.somastds.somast[0].idcol);
                if (lockstatus == "OK")
                {
                    miscordinf.somastds.somast[0].sotype = "O";
                    Quoting = false;
                    LabelBid.Visible = false;
                    SaveSo();
                    wsgUtilities.wsgNotice("Conversion Complete.");
                }
                else
                {
                    wsgUtilities.wsgNotice(lockstatus);
                }
            }
        }

        private void buttonViewOrder_Click(object sender, EventArgs e)
        {
            FrmMiscSODocumentViewer frmSODocumentViewer = new FrmMiscSODocumentViewer();
            frmSODocumentViewer.CurrentSono = miscordinf.somastds.somast[0].sono;
            frmSODocumentViewer.SODocument = "Estimate";
            frmSODocumentViewer.ShowDialog();
        }

        private void buttonCancelEdit_Click(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Abandon Edit") == true)
            {
                if (miscordinf.somastds.somast[0].idcol > 0)
                {
                    miscordinf.UnlockSomast(miscordinf.somastds.somast[0].idcol);
                }
                if (CurrentState == "Enter Order")
                {
                    ClearSO();
                    ClearLineitems();
                    miscordinf.ClearResultsTable();
                    bindingResults.DataSource = miscordinf.resultsds.soline;
                    CurrentState = "Select";
                    RefreshControls();
                }
                else
                {
                    CurrentState = "View";
                    ProcessSo();
                    RefreshControls();
                }
            }
        }

        private void buttonGetSO_Click(object sender, EventArgs e)
        {
            ShowSOSearch();
        }

        private void buttonVoid_Click(object sender, EventArgs e)
        {
            string SoStatus = miscordinf.GetSoStatus();
            if (SoStatus == "Open")
            {
                if (wsgUtilities.wsgReply("Are you sure that you want to void this order?") == true)
                {
                    string lockstatus = miscordinf.LockSomast(miscordinf.somastds.somast[0].idcol);
                    if (lockstatus == "OK")
                    {
                        miscordinf.somastds.somast[0].sostat = "V";
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

        private void dataGridViewSelectedItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSelectedItems.SelectedCells.Count > 0)
            {
                // Check to see if the quantity has been changed
                if (e.ColumnIndex == 1)
                {
                    int currentrow = dataGridViewSelectedItems.SelectedCells[0].OwningRow.Index;
                    string item = miscordinf.socurrentitemsds.soline[currentrow].item;
                    decimal qtyord = miscordinf.socurrentitemsds.soline[currentrow].qtyord;
                    miscordinf.socurrentitemsds.soline[currentrow].qtyact = miscordinf.socurrentitemsds.soline[currentrow].qtyord;
                    miscordinf.socurrentitemsds.soline[currentrow].price = miscordinf.CalculateItemPrice(item, qtyord);
                    miscordinf.socurrentitemsds.AcceptChanges();
                }
                if (CurrentState != "Select")
                {
                    miscordinf.RefreshTotals();
                }
            }
        }

        private void buttonRoute_Click(object sender, EventArgs e)
        {
            miscordinf.RouteSo();
            labelTrackingData.Text = trackingInf.GetLastSOTrackingData(miscordinf.somastds.somast[0].sono);
        }

        public void ProcessExistingOrder(string sono)
        {
            CurrentSomastid = miscordinf.GetSomastBySono(sono);
            if (CurrentSomastid != 0)
            {
                if (miscordinf.somastds.somast[0].enterqu != "Y")
                {
                    CurrentCustno = miscordinf.somastds.somast[0].custno;
                    CurrentCustid = miscordinf.somastds.somast[0].custid;
                    // Load customer data for this SO
                    miscordinf.getSingleCustomerData(CurrentCustid);
                    CurrentState = "View";
                    ProcessSo();
                }
                else
                {
                    wsgUtilities.wsgNotice("This is a cover order.");
                    textBoxSoNo.Text = "";
                }
                labelTaxdescrip.Text = appInformation.GetDistrictDescription(miscordinf.somastds.somast[0].taxdist);
                RefreshControls();
            }
            else
            {
                wsgUtilities.wsgNotice("Sales Order Not Found. Click Find to Search");
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {
        }

        private void buttonPterms_Click(object sender, EventArgs e)
        {
            string termid = customerTermsMethods.SelectTerms();
            if (termid.TrimEnd() != "")
            {
                miscordinf.somastds.somast[0].termid = customerTermsMethods.AlereDs.coterms[0].termid;
                miscordinf.somastds.somast[0].pdays = customerTermsMethods.AlereDs.coterms[0].discdays;
                miscordinf.somastds.somast[0].pnet = customerTermsMethods.AlereDs.coterms[0].duedays;
                miscordinf.somastds.somast[0].pdisc = customerTermsMethods.AlereDs.coterms[0].discrate;
                miscordinf.somastds.somast[0].pterms = customerTermsMethods.AlereDs.coterms[0].payterms.Substring(0, 20);
            }
        }

        private void buttonTaxDistrict_Click(object sender, EventArgs e)
        {
            string taxdistrict = appInformation.SelectTaxTable();
            if (taxdistrict != "")
            {
                miscordinf.somastds.somast[0].taxdist = taxdistrict;
                miscordinf.somastds.somast[0].taxrate = appInformation.GetDistrictTaxRate(taxdistrict);
            }
            labelTaxdescrip.Text = appInformation.GetDistrictDescription(miscordinf.somastds.somast[0].taxdist);
        }
    } // class
}// namespace