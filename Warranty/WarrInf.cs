using CommonAppClasses;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using WSGUtilitieslib;

namespace Warranty
{
    public class WarrInf : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Inspection");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private Estimating.Soinf soinf = new Estimating.Soinf("SQL", "SQLConnString");
        private static ObjectCacheWithParams dataCache = new ObjectCacheWithParams(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_Warranty"]));

        public FrmWarrantyMaintenance parentform = new FrmWarrantyMaintenance();
        public quote quoteds = new quote();
        public quote quotesearchds = new quote();
        public string CurrentState { get; set; }
        public BindingSource bindingWarrantySelector { get; set; }

        //TODO: Enter should Search 
        public WarrInf(string DataStore, string AppConfigName, Form Menuform)
            : base(DataStore, AppConfigName)
        {
            parentform.MdiParent = Menuform;

            if (!dataCache.IsInvalid)
            {
                //TODO: this clone does not actually copy any data 
                this.quotesearchds = (quote)((quote)dataCache.CachedObject).Clone(); 
            }
        }

        public void SetEvents()
        {
            parentform.buttonSearch.Click += new System.EventHandler(SearchWarranty);
            parentform.buttonSave.Click += new System.EventHandler(SaveWarranty);
            parentform.buttonCancel.Click += new System.EventHandler(CancelProcess);
            parentform.buttonClose.Click += new System.EventHandler(CloseForm);
            parentform.buttonClear.Click += new System.EventHandler(ClearForm);
            parentform.buttonDelete.Click += new System.EventHandler(ProcessDeletion);
            parentform.buttonCreateQuote.Click += new System.EventHandler(CreateQuote);
            parentform.buttonProcessQuote.Click += new System.EventHandler(ProcessQuote);
            parentform.buttonEdit.Click += new System.EventHandler(SetEditState);
            parentform.buttonInsert.Click += new System.EventHandler(SetInsertState);
            parentform.buttonGetCustomer.Click += new System.EventHandler(SelectCustomer);
            parentform.dataGridViewWarrantySearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_CellContentDoubleClick);
            parentform.dataGridViewWarrantySearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_CellContentDoubleClick);
            parentform.dataGridViewWarrantySearch.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridView_KeyDown);
        }

        public void SetBindings()
        {
            parentform.textBoxOrnum.DataBindings.Add("text", quoteds.warranty, "ornum");
            parentform.textBoxSono.DataBindings.Add("text", quoteds.warranty, "sono");
            parentform.textBoxPonum.DataBindings.Add("text", quoteds.warranty, "ponum");
            parentform.textBoxShipdate.DataBindings.Add("text", quoteds.warranty, "shipdate");
            parentform.textBoxLastupdate.DataBindings.Add("text", quoteds.warranty, "lastupdate");

            // Pool Owner
            parentform.textBoxLname.DataBindings.Add("text", quoteds.warranty, "lname");
            parentform.textBoxFname.DataBindings.Add("text", quoteds.warranty, "fname");
            parentform.textBoxAddress.DataBindings.Add("text", quoteds.warranty, "address");
            parentform.textBoxCity.DataBindings.Add("text", quoteds.warranty, "city");
            parentform.textBoxState.DataBindings.Add("text", quoteds.warranty, "state");
            parentform.textBoxZip.DataBindings.Add("text", quoteds.warranty, "zip");

            // Dealer
            parentform.textBoxCustno.DataBindings.Add("text", quoteds.warranty, "custno");
            parentform.textBoxDealer.DataBindings.Add("text", quoteds.warranty, "dealer");
            parentform.textBoxDealaddr1.DataBindings.Add("text", quoteds.warranty, "dealaddr1");
            parentform.textBoxDealaddr2.DataBindings.Add("text", quoteds.warranty, "dealaddr2");
            parentform.textBoxDealcity.DataBindings.Add("text", quoteds.warranty, "dealcity");
            parentform.textBoxDealstate.DataBindings.Add("text", quoteds.warranty, "dealstate");
            parentform.textBoxDealzip.DataBindings.Add("text", quoteds.warranty, "dealzip");
            // Notes, etc
            parentform.textBoxReplacedby.DataBindings.Add("text", quoteds.warranty, "replacedby");
            parentform.textBoxHoemailaddr.DataBindings.Add("text", quoteds.warranty, "hoemailaddr");
            parentform.textBoxAltnotes.DataBindings.Add("text", quoteds.warranty, "altnotes");
            parentform.textBoxRepairnotes.DataBindings.Add("text", quoteds.warranty, "repairnotes");
            parentform.textBoxNotes.DataBindings.Add("text", quoteds.warranty, "notes");
            parentform.textBoxOrigowner.DataBindings.Add("text", quoteds.warranty, "origowner");
            parentform.checkBoxDisableprt.DataBindings.Add("checked", quoteds.warranty, "disableprt");
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProcessSelection();
        }

        private void ProcessSelection()
        {
            int warridcol = CaptureIdCol(parentform.dataGridViewWarrantySearch);
            string commandstring = "SELECT * FROM warranty WHERE idcol = @idcol";
            quoteds.warranty.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", warridcol, "SQL");
            this.FillData(quoteds, "warranty", commandstring, CommandType.Text);
            CurrentState = "View";
            RefreshControls();
        }

        public void SetEditState(object sender, EventArgs e)
        {
            if (LockTableRow(quoteds.warranty[0].idcol, "warranty") == "OK")
            {
                quoteds.warranty[0].lastupdate = DateTime.Now;
                CurrentState = "Edit";
                RefreshControls();
            }
        }

        public void SetInsertState(object sender, EventArgs e)
        {
            quoteds.warranty.Rows.Clear();
            EstablishBlankDataTableRow(quoteds.warranty);
            quoteds.warranty[0].lastupdate = DateTime.Now;
            quoteds.warranty[0].shipdate = DateTime.Now;
            CurrentState = "Insert";
            RefreshControls();
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ProcessSelection();
            }
        }

        public void ExecuteSearch()
        {
            //TODO: CACHE (maybe?) Processing -> Warranty Processing -> Search 
            ClearParameters();

            quotesearchds.warranty.Rows.Clear();
            string commandstring = "SELECT * FROM warranty WHERE 1 = 1 ";

            string ordNumValue = parentform.textBoxOrnumS.Text.TrimEnd().ToUpper();
            string sonoValue = parentform.textBoxSonoS.Text.TrimEnd().ToUpper();
            string ponumValue = parentform.textBoxPonumS.Text.TrimEnd().ToUpper();
            string lnameValue = parentform.textBoxLnameS.Text.TrimEnd().ToUpper();
            string fnameValue = parentform.textBoxFnameS.Text.TrimEnd().ToUpper();
            string addressValue = parentform.textBoxAddressS.Text.TrimEnd().ToUpper();
            string cityValue = parentform.textBoxCityS.Text.TrimEnd().ToUpper();
            string stateValue = parentform.textBoxStateS.Text.TrimEnd().ToUpper();
            string zipValue = parentform.textBoxZipS.Text.TrimEnd().ToUpper();
            string custnoValue = parentform.textBoxCustnoS.Text.TrimEnd().ToUpper();
            string dealerValue = parentform.textBoxDealerS.Text.TrimEnd().ToUpper();
            string dealerAddrValue = parentform.textBoxDealaddr1S.Text.TrimEnd().ToUpper();
            string dealerCityValue = parentform.textBoxDealcityS.Text.TrimEnd().ToUpper();
            string dealerStateValue = parentform.textBoxDealstateS.Text.TrimEnd().ToUpper();
            string dealerZipValue = parentform.textBoxDealzipS.Text.TrimEnd().ToUpper();
            DateTime shipFirstDateValue = parentform.dateTimePickerShipFirstDate.Value;
            DateTime shipLastDateValue = parentform.dateTimePickerShipLastDate.Value;

            dataCache.SearchParams["ordNumValue"] = ordNumValue;
            dataCache.SearchParams["sonoValue"] = sonoValue;
            dataCache.SearchParams["ponumValue"] = ponumValue;
            dataCache.SearchParams["lnameValue"] = lnameValue;
            dataCache.SearchParams["fnameValue"] = fnameValue;
            dataCache.SearchParams["addressValue"] = addressValue;
            dataCache.SearchParams["cityValue"] = cityValue;
            dataCache.SearchParams["stateValue"] = stateValue;
            dataCache.SearchParams["zipValue"] = zipValue;
            dataCache.SearchParams["custnoValue"] = custnoValue;
            dataCache.SearchParams["dealerValue"] = dealerValue;
            dataCache.SearchParams["dealerAddrValue"] = dealerAddrValue;
            dataCache.SearchParams["dealerCityValue"] = dealerCityValue;
            dataCache.SearchParams["dealerStateValue"] = dealerStateValue;
            dataCache.SearchParams["dealerZipValue"] = dealerZipValue;
            dataCache.SearchParams["shipFirstDateValue"] = shipFirstDateValue.ToString();
            dataCache.SearchParams["shipLastDateValue"] = shipLastDateValue.ToString();

            if (ordNumValue.Length > 0)
            {
                AddParms("@ornum", ordNumValue.ToUpper() + "%", "SQL");
                commandstring += " AND UPPER(ornum) LIKE  @ornum";
            }
            if (sonoValue.Length > 0)
            {
                AddParms("@sono", "%" + sonoValue.ToUpper() + "%", "SQL");
                commandstring += " AND UPPER(sono) LIKE  @sono";
            }
            if (ponumValue.Length > 0)
            {
                AddParms("@ponum", "%" + ponumValue.ToUpper() + "%", "SQL");
                commandstring += " AND UPPER(ponum) LIKE  @ponum";
            }
            if (lnameValue.Length > 0)
            {
                AddParms("@lname", lnameValue.ToUpper() + "%", "SQL");
                commandstring += " AND UPPER(lname) LIKE  @lname";
            }
            if (fnameValue.Length > 0)
            {
                AddParms("@fname", fnameValue.ToUpper() + "%", "SQL");
                commandstring += " AND UPPER(fname) LIKE  @fname";
            }
            if (addressValue.Length > 0)
            {
                AddParms("@address", "%" + addressValue.ToUpper() + "%", "SQL");
                commandstring += " AND UPPER(address) LIKE  @address";
            }
            if (cityValue.Length > 0)
            {
                AddParms("@city", cityValue.ToUpper() + "%", "SQL");
                commandstring += " AND UPPER(city) LIKE  @city";
            }
            if (stateValue.Length > 0)
            {
                AddParms("@state", stateValue.ToUpper() + "%", "SQL");
                commandstring += " AND UPPER(state) LIKE  @state";
            }
            if (zipValue.Length > 0)
            {
                AddParms("@zip", zipValue + "%", "SQL");
                commandstring += " AND UPPER(zip) LIKE  @zip";
            }
            if (custnoValue.Length > 0)
            {
                AddParms("@custno", custnoValue + "%", "SQL");
                commandstring += " AND UPPER(custno) LIKE  @custno";
            }
            if (dealerValue.Length > 0)
            {
                AddParms("@dealer", dealerValue.TrimEnd() + "%", "SQL");
                commandstring += " AND UPPER(dealer) LIKE  @dealer";
            }
            if (dealerAddrValue.Length > 0)
            {
                AddParms("@dealaddr1", "%" + dealerAddrValue + "%", "SQL");
                commandstring += " AND UPPER(dealaddr1) LIKE  @dealaddr1";
            }
            if (dealerCityValue.Length > 0)
            {
                AddParms("@dealcity", dealerCityValue.ToUpper().TrimEnd() + "%", "SQL");
                commandstring += " AND UPPER(dealcity) LIKE  @dealcity";
            }
            if (dealerStateValue.Length > 0)
            {
                AddParms("@dealstate", dealerStateValue.ToUpper().TrimEnd() + "%", "SQL");
                commandstring += " AND UPPER(dealstate) LIKE  @dealstate";
            }
            if (dealerZipValue.Length > 0)
            {
                AddParms("@dealzip", dealerZipValue.ToUpper().TrimEnd() + "%", "SQL");
                commandstring += " AND UPPER(dealzip) LIKE  @dealzip";
            }
            if ((shipFirstDateValue != Convert.ToDateTime("01/01/1900")) &&
             (shipFirstDateValue < shipLastDateValue))
            {
                AddParms("@firstshipdate", shipFirstDateValue, "SQL");
                AddParms("@lastshipdate", shipLastDateValue, "SQL");
                commandstring += " AND shipdate BETWEEN @firstshipdate AND @lastshipdate ";
            }

            if (commandstring != "SELECT * FROM warranty WHERE 1 = 1 ")
            {
                commandstring += " ORDER BY lname, fname ";

                this.FillData(quotesearchds, "warranty", commandstring, CommandType.Text);
                dataCache.Refresh(quotesearchds);

                if (quotesearchds.warranty.Rows.Count < 1)
                {
                    wsgUtilities.wsgNotice("No matching records");
                }
            }
            else
            {
                wsgUtilities.wsgNotice("No search fields were entered");
            }
        }

        public void SearchWarranty(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        public void SelectCustomer(object sender, EventArgs e)
        {
            GetCustomerMethods getCust = new GetCustomerMethods("SQL", "SQLConnString");
            if (getCust.SelectedCustid > 0)
            {
                string commandstring = "SELECT * FROM arcust WHERE idcol = @idcol";
                quoteds.arcust.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@idcol", getCust.SelectedCustid, "SQL");
                this.FillData(quoteds, "arcust", commandstring, CommandType.Text);
                parentform.textBoxCustno.Text = quoteds.arcust[0].custno;
                parentform.textBoxDealer.Text = quoteds.arcust[0].company;
                parentform.textBoxDealaddr1.Text = quoteds.arcust[0].address1;
                parentform.textBoxDealaddr2.Text = quoteds.arcust[0].address2;
                parentform.textBoxDealcity.Text = quoteds.arcust[0].city;
                parentform.textBoxDealstate.Text = quoteds.arcust[0].state;
                parentform.textBoxDealzip.Text = quoteds.arcust[0].zip;
            }
        }

        public void ActivateInputs()
        {
            // Basic data
            parentform.textBoxOrnum.Enabled = true;
            parentform.textBoxSono.Enabled = true;
            parentform.textBoxPonum.Enabled = true;
            parentform.textBoxShipdate.Enabled = true;
            parentform.textBoxLastupdate.Enabled = true;

            // Pool Owner
            parentform.textBoxLname.Enabled = true;
            parentform.textBoxFname.Enabled = true;
            parentform.textBoxAddress.Enabled = true;
            parentform.textBoxCity.Enabled = true;
            parentform.textBoxState.Enabled = true;
            parentform.textBoxZip.Enabled = true;
            // Dealer
            parentform.buttonGetCustomer.Enabled = true;
            parentform.textBoxCustno.Enabled = true;
            parentform.textBoxDealer.Enabled = true;
            parentform.textBoxDealaddr1.Enabled = true;
            parentform.textBoxDealaddr2.Enabled = true;
            parentform.textBoxDealcity.Enabled = true;
            parentform.textBoxDealstate.Enabled = true;
            parentform.textBoxDealzip.Enabled = true;
            // Notes, etc
            parentform.textBoxReplacedby.Enabled = true;
            parentform.textBoxHoemailaddr.Enabled = true;
            parentform.textBoxAltnotes.Enabled = true;
            parentform.textBoxRepairnotes.Enabled = true;
            parentform.textBoxNotes.Enabled = true;
            parentform.textBoxOrigowner.Enabled = true;
            parentform.checkBoxDisableprt.Enabled = true;
        }

        public void ProcessQuote(object sender, EventArgs e)
        {
            FrmChooseQuoteOption frmChooseQuoteOption = new FrmChooseQuoteOption();
            SOSelector soSelector = new SOSelector("SQL", "SQLConnString");
            string commandstring = "";
            string sonotoprocess = soSelector.SelectSO(quoteds.warranty[0].ornum);
            if (sonotoprocess != "")
            {
                if (sonotoprocess != "Cancelled" && sonotoprocess != "")
                {
                    // Process the quote
                    frmChooseQuoteOption.ShowDialog();

                    if (frmChooseQuoteOption.QuoteOption != "Cancel")
                    {
                        if (frmChooseQuoteOption.QuoteOption == "Copy")
                        {
                            string newsono = soinf.CopyQuote(sonotoprocess);
                            if (newsono.TrimEnd() != "")
                            {
                                soinf.somastds.somast.Rows.Clear();
                                ClearParameters();
                                AddParms("@sono", newsono, "SQL");
                                commandstring = "SELECT * FROM somast WHERE sono = @sono";
                                this.FillData(soinf.somastds, "somast", commandstring, CommandType.Text);
                                soinf.somastds.somast[0].ponum = quoteds.warranty[0].lname.TrimEnd() + "\\" + quoteds.warranty[0].ornum;
                                soinf.somastds.somast[0].oldplan = quoteds.warranty[0].ornum;
                                // Save somast again to post ponum and old plan
                                GenerateAppTableRowSave(soinf.somastds.somast[0]);
                                // Activate Estimating form
                                Estimating.FrmSOHead frmSoHead = new Estimating.FrmSOHead();
                                frmSoHead.PassedSono = soinf.somastds.somast[0].sono;
                                frmSoHead.IsCopyQuote = true;
                                frmSoHead.MdiParent = parentform.MdiParent;
                                frmSoHead.Show();
                            }
                        }
                        else
                        {
                            soinf.somastds.somast.Rows.Clear();
                            ClearParameters();
                            AddParms("@sono", sonotoprocess, "SQL");
                            commandstring = "SELECT * FROM somast WHERE sono = @sono";
                            this.FillData(soinf.somastds, "somast", commandstring, CommandType.Text);
                            Estimating.FrmSOHead frmSoHead = new Estimating.FrmSOHead();
                            frmSoHead.PassedSono = soinf.somastds.somast[0].sono;
                            frmSoHead.MdiParent = parentform.MdiParent;
                            frmSoHead.Show();
                        }
                    }
                    else
                    {
                        wsgUtilities.wsgNotice("Operation Cancelled");
                    }
                }
                else
                {
                    wsgUtilities.wsgNotice("Operation Cancelled");
                }
            }
            else
            {
                wsgUtilities.wsgNotice("There are no quotes from this plan number");
            }
        }

        public void CreateQuote(object sender, EventArgs e)
        {
            string commandstring = "";
            // Check for the existence of the SO that created this warranty.
            // If there is one, use that data to populate the quote.
            // If not, create blank version and cover data.
            quotesearchds.somast.Rows.Clear();
            if (quoteds.warranty[0].sono.TrimEnd() != "")
            {
                ClearParameters();
                AddParms("@sono", quoteds.warranty[0].sono, "SQL");
                commandstring = "SELECT * FROM somast WHERE sono = @sono";
                this.FillData(quotesearchds, "somast", commandstring, CommandType.Text);
            }
            if (quotesearchds.somast.Rows.Count > 0)
            {
                // Copy the quote if found
                string newsono = soinf.CopyQuote(quoteds.warranty[0].sono);
                if (newsono.TrimEnd() != "")
                {
                    soinf.somastds.somast.Rows.Clear();
                    ClearParameters();
                    AddParms("@sono", newsono, "SQL");
                    commandstring = "SELECT * FROM somast WHERE sono = @sono";
                    this.FillData(soinf.somastds, "somast", commandstring, CommandType.Text);
                    soinf.somastds.somast[0].ponum = quoteds.warranty[0].lname.TrimEnd() + "\\" + quoteds.warranty[0].ornum;
                    soinf.somastds.somast[0].oldplan = quoteds.warranty[0].ornum;
                    // Save somast again to post ponum and old plan
                    GenerateAppTableRowSave(soinf.somastds.somast[0]);
                    // Activate Estimating form
                    Estimating.FrmSOHead frmSoHead = new Estimating.FrmSOHead();
                    frmSoHead.PassedSono = soinf.somastds.somast[0].sono;
                    frmSoHead.IsCopyQuote = true;
                    frmSoHead.MdiParent = parentform.MdiParent;
                    frmSoHead.Show();
                }
            }
            else
            {
                GetCustomerMethods getCust = new GetCustomerMethods("SQL", "SQLConnString");
                if (getCust.SelectedCustid != 0)
                {
                    soinf.getSingleCustomerData(getCust.SelectedCustid);
                    // Establish blank tables
                    soinf.EstablishBlankSomastData();
                    soinf.EstablishSomastBasicData();
                    // Establish somast data from warranty table
                    soinf.somastds.somast[0].lname = quoteds.warranty[0].lname;
                    soinf.somastds.somast[0].fname = quoteds.warranty[0].fname;
                    soinf.somastds.somast[0].address = quoteds.warranty[0].address;
                    soinf.somastds.somast[0].city = quoteds.warranty[0].city;
                    soinf.somastds.somast[0].state = quoteds.warranty[0].state;
                    soinf.somastds.somast[0].zip = quoteds.warranty[0].zip;
                    soinf.somastds.somast[0].oldplan = quoteds.warranty[0].ornum;
                    soinf.somastds.somast[0].ponum = quoteds.warranty[0].lname.TrimEnd() + "\\" + quoteds.warranty[0].ornum;
                    // Save somast
                    GenerateAppTableRowSave(soinf.somastds.somast[0]);
                    quotesearchds.soversion.Rows.Clear();
                    soinf.EstablishBlankSoversionData();
                    // Populate version
                    soinf.somastds.soversion[0].sono = soinf.somastds.somast[0].sono;
                    soinf.somastds.soversion[0].version = "A";
                    // Save soversion
                    GenerateAppTableRowSave(soinf.somastds.soversion[0]);
                    // Populate covers
                    soinf.somastds.socover.Rows.Clear();
                    EstablishBlankDataTableRow(soinf.somastds.socover);
                    soinf.somastds.socover[0].sono = soinf.somastds.somast[0].sono;
                    soinf.somastds.socover[0].version = "A";
                    soinf.somastds.socover[0].cover = "A";
                    soinf.somastds.socover[0].qtyord = 1;
                    soinf.somastds.socover[0].product = "Worksheet";
                    soinf.somastds.socover[0].covertype = "C";
                    GenerateAppTableRowSave(soinf.somastds.socover[0]);
                    soinf.somastds.socover.Rows.Clear();
                    EstablishBlankDataTableRow(soinf.somastds.socover);
                    soinf.somastds.socover[0].sono = soinf.somastds.somast[0].sono;
                    soinf.somastds.socover[0].version = "A";
                    soinf.somastds.socover[0].cover = "A";
                    soinf.somastds.socover[0].product = "Worksheet";
                    soinf.somastds.socover[0].covertype = "X1";
                    GenerateAppTableRowSave(soinf.somastds.socover[0]);
                    soinf.somastds.socover.Rows.Clear();
                    EstablishBlankDataTableRow(soinf.somastds.socover);
                    soinf.somastds.socover[0].sono = soinf.somastds.somast[0].sono;
                    soinf.somastds.socover[0].version = "A";
                    soinf.somastds.socover[0].cover = "A";
                    soinf.somastds.socover[0].product = "Worksheet";
                    soinf.somastds.socover[0].covertype = "X2";
                    GenerateAppTableRowSave(soinf.somastds.socover[0]);
                    soinf.somastds.socover.Rows.Clear();
                    EstablishBlankDataTableRow(soinf.somastds.socover);
                    soinf.somastds.socover[0].sono = soinf.somastds.somast[0].sono;
                    soinf.somastds.socover[0].version = "A";
                    soinf.somastds.socover[0].cover = "A";
                    soinf.somastds.socover[0].product = "Worksheet";
                    soinf.somastds.socover[0].covertype = "X3";
                    GenerateAppTableRowSave(soinf.somastds.socover[0]);
                    soinf.somastds.socover.Rows.Clear();
                    EstablishBlankDataTableRow(soinf.somastds.socover);
                    soinf.somastds.socover[0].sono = soinf.somastds.somast[0].sono;
                    soinf.somastds.socover[0].version = "A";
                    soinf.somastds.socover[0].cover = "A";
                    soinf.somastds.socover[0].product = "Worksheet";
                    soinf.somastds.socover[0].covertype = "X4";
                    GenerateAppTableRowSave(soinf.somastds.socover[0]);
                    // Activate Estimating form
                    Estimating.FrmSOHead frmSoHead = new Estimating.FrmSOHead();
                    frmSoHead.PassedSono = soinf.somastds.somast[0].sono;
                    frmSoHead.MdiParent = parentform.MdiParent;
                    frmSoHead.Show();
                }
                else
                {
                    wsgUtilities.wsgNotice("Process Cancelled");
                } // Customer selected
            } // (quotesearchds.somast.Rows.Count > 0)
        }

        public void ProcessDeletion(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this item") == true)
            {
                DeleteTableRow("warranty", quoteds.warranty[0].idcol);
                quoteds.warranty.Rows.Clear();
                ExecuteSearch();
                CurrentState = "Search";
                RefreshControls();
            }
        }

        public void CancelProcess(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Abandon Edit") == true)
            {
                if (CurrentState != "Insert")
                {
                    UnlockTableRow(quoteds.warranty[0].idcol, "warranty");
                }
                else
                {
                    quoteds.warranty.Rows.Clear();
                }
                CurrentState = "Search";
                RefreshControls();
            }
        }

        public void ClearForm(object sender, EventArgs e)
        {
            dataCache.SearchParams.Clear();
            ClearSearchFields();
            quoteds.warranty.Rows.Clear();
            CurrentState = "Search";
            RefreshControls();
        }

        public void CloseForm(object sender, EventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        UnlockTableRow(quoteds.warranty[0].idcol, "warranty");
                        CurrentState = "Search";
                        parentform.Close();
                    }
                    else
                    {
                        quoteds.warranty.Rows.Clear();
                        parentform.Close();
                    }
                }
            }
            else
            {
                parentform.Close();
            }
        }

        public void ShowParent()
        {
            bindingWarrantySelector = new BindingSource();
            CurrentState = "Select";
            SetIdcol(quoteds.warranty.idcolColumn);
            SetEvents();
            parentform.dataGridViewWarrantySearch.AutoGenerateColumns = false;
            bindingWarrantySelector.DataSource = quotesearchds.warranty;
            parentform.dataGridViewWarrantySearch.DataSource = bindingWarrantySelector;
            parentform.dataGridViewWarrantySearch.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentform.dataGridViewWarrantySearch.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            SetBindings();
            ClearSearchFields();
            parentform.Show();
            CurrentState = "Search";
            RefreshControls();
        }

        public void ClearSearchFields()
        {
            parentform.Invoke(new Action(() =>
            {
                parentform.dateTimePickerShipFirstDate.Value = Convert.ToDateTime("01/01/1900");
                parentform.dateTimePickerShipLastDate.Value = Convert.ToDateTime("01/01/1900");
                parentform.textBoxOrnumS.Text = "";
                parentform.textBoxSonoS.Text = "";
                parentform.textBoxPonumS.Text = "";
                parentform.textBoxFnameS.Text = "";
                parentform.textBoxLnameS.Text = "";
                parentform.textBoxAddressS.Text = "";
                parentform.textBoxCityS.Text = "";
                parentform.textBoxStateS.Text = "";
                parentform.textBoxZipS.Text = "";
                parentform.textBoxDealerS.Text = "";
                parentform.textBoxDealaddr1S.Text = "";
                parentform.textBoxDealcityS.Text = "";
                parentform.textBoxDealstateS.Text = "";
                parentform.textBoxDealzipS.Text = "";

                parentform.textBoxOrnumS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["ordNumValue"];
                parentform.textBoxSonoS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["sonoValue"];
                parentform.textBoxPonumS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["ponumValue"];
                parentform.textBoxLnameS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["lnameValue"];
                parentform.textBoxFnameS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["fnameValue"];
                parentform.textBoxAddressS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["addressValue"];
                parentform.textBoxCityS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["cityValue"];
                parentform.textBoxStateS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["stateValue"];
                parentform.textBoxZipS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["zipValue"];
                parentform.textBoxCustnoS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["custnoValue"];
                parentform.textBoxDealerS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["dealerValue"];
                parentform.textBoxDealaddr1S.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["dealerAddrValue"];
                parentform.textBoxDealcityS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["dealerCityValue"];
                parentform.textBoxDealstateS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["dealerStateValue"];
                parentform.textBoxDealzipS.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["dealerZipValue"];
                if (!String.IsNullOrEmpty(dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["shipFirstDateValue"]))
                {
                    parentform.dateTimePickerShipFirstDate.Value = DateTime.Parse(dataCache.SearchParams["shipFirstDateValue"]);
                }
                if (!String.IsNullOrEmpty(dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["shipLastDateValue"]))
                {
                    parentform.dateTimePickerShipLastDate.Value = DateTime.Parse(dataCache.SearchParams["shipLastDateValue"]);
                }
            }));
        }

        public void RefreshControls()
        {
            DisableControls();
            switch (CurrentState)
            {
                case "Search":
                    {
                        parentform.buttonSearch.Enabled = true;
                        parentform.buttonInsert.Enabled = true;
                        parentform.buttonClear.Enabled = true;
                        parentform.textBoxOrnumS.Enabled = true;
                        parentform.textBoxSonoS.Enabled = true;
                        parentform.textBoxPonumS.Enabled = true;
                        parentform.textBoxFnameS.Enabled = true;
                        parentform.textBoxLnameS.Enabled = true;
                        parentform.textBoxAddressS.Enabled = true;
                        parentform.textBoxCityS.Enabled = true;
                        parentform.textBoxStateS.Enabled = true;
                        parentform.textBoxZipS.Enabled = true;
                        parentform.textBoxCustnoS.Enabled = true;
                        parentform.textBoxDealerS.Enabled = true;
                        parentform.textBoxDealaddr1S.Enabled = true;
                        parentform.textBoxDealcityS.Enabled = true;
                        parentform.textBoxDealstateS.Enabled = true;
                        parentform.textBoxDealzipS.Enabled = true;
                        parentform.dateTimePickerShipFirstDate.Enabled = true;
                        parentform.dateTimePickerShipLastDate.Enabled = true;
                        parentform.dataGridViewWarrantySearch.Enabled = true;

                        break;
                    }
                case "Edit":
                    {
                        ActivateInputs();
                        parentform.buttonSave.Enabled = true;
                        parentform.buttonCancel.Enabled = true;
                        break;
                    }
                case "Insert":
                    {
                        ActivateInputs();
                        parentform.buttonSave.Enabled = true;
                        parentform.buttonCancel.Enabled = true;
                        break;
                    }
                case "View":
                    {
                        parentform.buttonEdit.Enabled = true;
                        parentform.buttonDelete.Enabled = true;
                        parentform.buttonSearch.Enabled = true;
                        parentform.buttonCreateQuote.Enabled = true;
                        parentform.buttonProcessQuote.Enabled = true;
                        parentform.buttonClear.Enabled = true;
                        parentform.textBoxOrnumS.Enabled = true;
                        parentform.textBoxSonoS.Enabled = true;
                        parentform.textBoxPonumS.Enabled = true;
                        parentform.textBoxFnameS.Enabled = true;
                        parentform.textBoxLnameS.Enabled = true;
                        parentform.textBoxAddressS.Enabled = true;
                        parentform.textBoxCityS.Enabled = true;
                        parentform.textBoxStateS.Enabled = true;
                        parentform.textBoxZipS.Enabled = true;
                        parentform.textBoxCustnoS.Enabled = true;
                        parentform.textBoxDealerS.Enabled = true;
                        parentform.textBoxDealaddr1S.Enabled = true;
                        parentform.textBoxDealcityS.Enabled = true;
                        parentform.textBoxDealstateS.Enabled = true;
                        parentform.textBoxDealzipS.Enabled = true;
                        parentform.dateTimePickerShipFirstDate.Enabled = true;
                        parentform.dateTimePickerShipLastDate.Enabled = true;
                        parentform.dataGridViewWarrantySearch.Enabled = true;

                        break;
                    }
            }
        }

        public void DisableControls()
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in parentform.Controls)
            {
                if (!(c is Label))
                {
                    c.Enabled = false;
                    foreach (Control d in c.Controls)
                    {
                        d.Enabled = false;
                    }
                }
            }
            // Close is always enabled
            parentform.buttonClose.Enabled = true;
        }

        public void SaveWarranty(object sender, EventArgs e)
        {
            GenerateAppTableRowSave(quoteds.warranty[0]);
            wsgUtilities.wsgNotice("Update Complete");

            if (CurrentState == "Insert")
            {
                CurrentState = "View";
            }
            else
            {
                quoteds.warranty.Rows.Clear();
                CurrentState = "Search";
            }

            RefreshControls();
        }
    }

    public class SOSelector : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Inspection");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        public FrmSelectSoToCopy parentform = new FrmSelectSoToCopy();
        private Estimating.Soinf soinf = new Estimating.Soinf("SQL", "SQLConnString");
        public quote quoteds = new quote();
        public quote quotesearchds = new quote();
        public BindingSource bindingSOSelector = new BindingSource();
        public string SelectedSO = "";

        public SOSelector(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            SetBindings();
            SetEvents();
        }

        public string SelectSO(string ornum)
        {
            SelectedSO = "";
            string commandstring = "";
            quotesearchds.view_somastdata.Rows.Clear();
            ClearParameters();
            AddParms("@ornum", ornum.Substring(0, 10), "SQL");
            commandstring = "SELECT * FROM view_somastdata WHERE oldplan = @ornum ORDER BY sono";
            this.FillData(quotesearchds, "view_somastdata", commandstring, CommandType.Text);
            if (quotesearchds.view_somastdata.Rows.Count > 0)
            {
                parentform.ShowDialog();
            }

            return SelectedSO;
        }

        public void SetBindings()
        {
            parentform.dataGridViewSoToCopy.AutoGenerateColumns = false;
            bindingSOSelector.DataSource = quotesearchds.view_somastdata;
            parentform.dataGridViewSoToCopy.DataSource = bindingSOSelector;
            parentform.dataGridViewSoToCopy.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentform.dataGridViewSoToCopy.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        public void SetEvents()
        {
            parentform.buttonCancel.Click += new System.EventHandler(CancelProcess);
            parentform.dataGridViewSoToCopy.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_CellContentDoubleClick);
            parentform.dataGridViewSoToCopy.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_CellContentDoubleClick);
            parentform.dataGridViewSoToCopy.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridView_KeyDown);
        }

        public void CancelProcess(object sender, EventArgs e)
        {
            SelectedSO = "Cancelled";
            parentform.Close();
        }

        public void ProcessSelection()
        {
            SelectedSO = (string)parentform.dataGridViewSoToCopy.Rows[parentform.dataGridViewSoToCopy.SelectedCells[0].RowIndex].Cells[0].Value;
            parentform.Close();
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProcessSelection();
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ProcessSelection();
            }
        }
    }
}