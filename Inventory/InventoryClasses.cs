using CommonAppClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Inventory
{
    public class InventoryMethods : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private FrmInventoryTransaction frmInventoryTransacations = new FrmInventoryTransaction();
        public string InvtranType = "";
        public string CurrentState = "";
        public int NumberOfInvCostPlaces = 2;
        public item itemds = new item();
        public quote quoteds = new quote();
        public order orderds = new order();
        public inventoryds calcinvds = new inventoryds();
        public inventoryds tempinvds = new inventoryds();
        public inventoryds invds = new inventoryds();

        private static ObjectCache itemSelectorDsCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_InventoryItems"]));
        private static ObjectCache dataCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_InventoryItems"]));
        public static item _itemSelectorDs = new item();

        public InventoryMethods(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            SetIdcol(invds.ictran.idcolColumn);
        }

        public void InventoryTransactionsStart(Form CallingForm)
        {
            SetInventoryTransactionEvents();
            frmInventoryTransacations.MdiParent = CallingForm;
            CurrentState = "SelectItem";
            SetInventoryTransactionBindings();
            RefreshInventoryTransactionControls();
            // Create a blank row in ictran
            InitializeInventoryTransaction();
            frmInventoryTransacations.Show();
        }

        public void SetInventoryTransactionEvents()
        {
            frmInventoryTransacations.comboBoxLocation.SelectedIndex = 0;
            frmInventoryTransacations.buttonSave.Click += new System.EventHandler(buttonSave_Click);
            frmInventoryTransacations.buttonClear.Click += new System.EventHandler(buttonClear_Click);
            frmInventoryTransacations.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            frmInventoryTransacations.buttonGetItem.Click += new System.EventHandler(buttonGetItem_Click);
            frmInventoryTransacations.textBoxItem.KeyDown += new System.Windows.Forms.KeyEventHandler(TextBoxItem_KeyDown);
            frmInventoryTransacations.textBoxQty.GotFocus += new System.EventHandler(textBoxQty_GotFocus);
        }

        public void textBoxQty_GotFocus(object sender, EventArgs e)
        {
            if (CurrentState == "Edit")
            {
                if (invds.ictran[0].price == 0 && invds.ictran[0].item.TrimEnd() != "" && invds.ictran[0].loctid.TrimEnd() != "")
                {
                    invds.ictran[0].price = GetLastReceiptPrice(invds.ictran[0].item, invds.ictran[0].loctid);
                    invds.ictran.AcceptChanges();
                }
            }
        }

        public void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                getImmasterByItem(frmInventoryTransacations.textBoxItem.Text);
                if (itemds.immaster.Rows.Count > 0)
                {
                    frmInventoryTransacations.labelItmdesc.Text = itemds.immaster[0].descrip;
                    CurrentState = "Edit";
                    RefreshInventoryTransactionControls();
                }
                else
                {
                    wsgUtilities.wsgNotice("That item cannot be found. Use Search Button");
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            invds.ictran[0].loctid = frmInventoryTransacations.comboBoxLocation.GetItemText(frmInventoryTransacations.comboBoxLocation.SelectedItem);
            switch (InvtranType)
            {
                case "R":
                    {
                        // Inventory Receipt
                        ProcessInventoryReceiptTransaction(invds.ictran[0].item, invds.ictran[0].loctid, invds.ictran[0].tdate, invds.ictran[0].tqty, invds.ictran[0].price, invds.ictran[0].tnotes, invds.ictran[0].docno, invds.ictran[0].meycono);
                        break;
                    }
                case "I":
                    {
                        // Inventory Issue
                        ProcessInventoryIssueTransaction(invds.ictran[0].item, invds.ictran[0].loctid, invds.ictran[0].tdate, invds.ictran[0].tqty, invds.ictran[0].tnotes, "Manual", invds.ictran[0].meycono);
                        break;
                    }
                case "H":
                    {
                        // Stock Cover File Numbers
                        ProcessStockCoverFileNumbers(invds.ictran[0].item, invds.ictran[0].loctid, invds.ictran[0].tqty, invds.ictran[0].tnotes);
                        break;
                    }
            }
            invds.ictran.Rows.Clear();
            InitializeInventoryTransaction();
            wsgUtilities.wsgNotice("Save Complete");
            CurrentState = "SelectItem";
            RefreshInventoryTransactionControls();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit")
            {
                if (wsgUtilities.wsgReply("Abandon Edit?"))
                {
                    CurrentState = "SelectItem";
                    RefreshInventoryTransactionControls();
                }
            }
            else
            {
                CurrentState = "SelectItem";
                RefreshInventoryTransactionControls();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit")
            {
                if (wsgUtilities.wsgReply("Abandon Edit?"))
                {
                    frmInventoryTransacations.Close();
                }
            }
            else
            {
                frmInventoryTransacations.Close();
            }
        }

        private void buttonGetItem_Click(object sender, EventArgs e)
        {
            string SelectedItem = SelectImmaster(frmInventoryTransacations.textBoxItem.Text.TrimEnd());
            if (SelectedItem != "")
            {
                getImmasterByItem(SelectedItem);
                invds.ictran[0].item = itemds.immaster[0].item;
                frmInventoryTransacations.labelItmdesc.Text = itemds.immaster[0].descrip;
                CurrentState = "Edit";
                RefreshInventoryTransactionControls();
            }
        }

        public void RefreshInventoryTransactionControls()
        {
            switch (InvtranType)
            {
                case "I":
                    {
                        frmInventoryTransacations.Text = "Inventory Issue";
                        frmInventoryTransacations.textBoxCost.Visible = false;
                        frmInventoryTransacations.labelCost.Visible = false;
                        break;
                    }
                case "R":
                    {
                        frmInventoryTransacations.Text = "Inventory Receipt";
                        break;
                    }
                case "H":
                    {
                        frmInventoryTransacations.Text = "Store Cover File Number Creation";
                        frmInventoryTransacations.textBoxCost.Visible = false;
                        frmInventoryTransacations.labelCost.Visible = false;
                        frmInventoryTransacations.dateTimePickerTdate.Visible = false;
                        frmInventoryTransacations.labelTdate.Visible = false;
                        break;
                    }
            }
            // Disable all controls
            foreach (Control c in frmInventoryTransacations.Controls)
            {
                c.Enabled = false;
            }
            frmInventoryTransacations.buttonClose.Enabled = true;
            frmInventoryTransacations.labelItmdesc.Visible = false;
            switch (CurrentState)
            {
                case "SelectItem":
                    {
                        frmInventoryTransacations.textBoxItem.Enabled = true;
                        frmInventoryTransacations.buttonGetItem.Enabled = true;
                        break;
                    }
                case "Edit":
                    {
                        frmInventoryTransacations.labelItmdesc.Visible = true;
                        frmInventoryTransacations.buttonClear.Enabled = true;
                        frmInventoryTransacations.textBoxQty.Enabled = true;
                        frmInventoryTransacations.comboBoxLocation.Enabled = true;
                        frmInventoryTransacations.dateTimePickerTdate.Enabled = true;
                        frmInventoryTransacations.textBoxNotes.Enabled = true;
                        frmInventoryTransacations.textBoxCost.Enabled = true;
                        frmInventoryTransacations.buttonSave.Enabled = true;
                        break;
                    }
            }
            frmInventoryTransacations.Update();
        }

        public void SetInventoryTransactionBindings()
        {
            // Bind the numeric textboxes
            SetTextBoxCurrencyBinding(frmInventoryTransacations.textBoxCost, invds, "ictran.price");
            SetTextBoxIntegerBinding(frmInventoryTransacations.textBoxQty, invds, "ictran.tqty");

            // Text fields
            frmInventoryTransacations.textBoxNotes.DataBindings.Clear();
            frmInventoryTransacations.textBoxNotes.DataBindings.Add("Text", invds.ictran, "tnotes");
            frmInventoryTransacations.textBoxItem.DataBindings.Clear();
            frmInventoryTransacations.textBoxItem.DataBindings.Add("Text", invds.ictran, "item");

            // Datetimepicker binding and formatting
            frmInventoryTransacations.dateTimePickerTdate.DataBindings.Clear();
            frmInventoryTransacations.dateTimePickerTdate.DataBindings.Add("Value", invds.ictran, "tdate", true, DataSourceUpdateMode.OnPropertyChanged);
            frmInventoryTransacations.dateTimePickerTdate.Format = DateTimePickerFormat.Custom;
            frmInventoryTransacations.dateTimePickerTdate.CustomFormat = "MMMM dd, yyyy";
        }

        public void getImmasterByItem(string item)
        {
            itemds.immaster.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.FillData(itemds, "immaster", "wsgsp_getitembyitem", CommandType.StoredProcedure);
        }

        // Decimal and currency conversions
        private void DecimalToCurrencyString(object sender, ConvertEventArgs cevent)
        {
            // The method converts only to string type. Test this using the DesiredType.
            if (cevent.DesiredType != typeof(string)) return;

            // Use the ToString method to format the value as currency ("c").
            cevent.Value = ((decimal)cevent.Value).ToString("N2");
            //  + NumberOfInvCostPlaces.ToString().TrimEnd()
        }

        private void CurrencyStringToDecimal(object sender, ConvertEventArgs cevent)
        {
            // The method converts back to decimal type only.
            if (cevent.DesiredType != typeof(decimal)) return;

            // Converts the string back to decimal using the static Parse method.
            cevent.Value = Decimal.Parse(cevent.Value.ToString(),
            NumberStyles.Currency, null);
        }

        private void SetTextBoxCurrencyBinding(TextBox txtbox, inventoryds ds, string fieldname)
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

        private void SetTextBoxIntegerBinding(TextBox txtbox, DataSet ds, string fieldname)
        {
            Binding b = new Binding("Text", ds, fieldname);
            {
                b.Format += new ConvertEventHandler(DecimalToIntegerString);
                b.Parse += new ConvertEventHandler(CurrencyStringToDecimal);
            }
            txtbox.DataBindings.Clear();
            txtbox.DataBindings.Add(b);
        }

        private void DecimalToIntegerString(object sender, ConvertEventArgs cevent)
        {
            // The method converts only to string type. Test this using the DesiredType.
            if (cevent.DesiredType != typeof(string)) return;

            // Use the ToString method to format the value as currency ("c").
            cevent.Value = ((Decimal)cevent.Value).ToString("N0");
        }

        public decimal CalcInventoryItemCost(string item, string loctid, DateTime calcdate)
        {
            decimal InventoryItemCost = 0;

            return InventoryItemCost;
        }

        public void InitializeInventoryTransaction()
        {
            EstablishBlankDataTableRow(invds.ictran);
            invds.ictran[0].trantyp = InvtranType;
            invds.ictran[0].tdate = DateTime.Now.Date;
            invds.ictran[0].docno = "Manual";
            invds.ictran[0].sewnonlabelprinted = "N";
        }

        public string SelectImmaster(string itemseek)
        {
            string item = "";
            string commandtext = "";
            FrmSelector frmSelector = new FrmSelector();
            FrmSelectorMethods frmSelectorMethods = new FrmSelectorMethods();

            //CACHED: Processing -> Inventory Processing -> Inventory Receipt -> Search
            ClearParameters();
            if (itemseek.TrimEnd() == "")
            {
                if (itemSelectorDsCache.IsInvalid)
                {
                    _itemSelectorDs.immaster.Rows.Clear();
                    commandtext = "SELECT * FROM immaster ORDER BY item ";
                    FillData(_itemSelectorDs, "immaster", commandtext, CommandType.Text);
                    itemSelectorDsCache.Refresh(_itemSelectorDs);
                }
            }
            else
            {
                //TODO: this could also be cached (with search term) 
                _itemSelectorDs.immaster.Rows.Clear();
                AddParms("@seeker", itemseek.TrimEnd() + "%", "SQL");
                commandtext = " SELECT * from immaster where item like @seeker";
                FillData(_itemSelectorDs, "immaster", commandtext, CommandType.Text);
                itemSelectorDsCache.Invalidate();
            }

            frmSelectorMethods.FormText = "Select Inventory Item";
            frmSelectorMethods.dtSource = _itemSelectorDs.immaster;
            frmSelectorMethods.columncount = 2;
            frmSelectorMethods.SetColumns();
            frmSelectorMethods.colname[0] = "Itemcol";
            frmSelectorMethods.colheadertext[0] = "Item";
            frmSelectorMethods.coldatapropertyname[0] = "item";
            frmSelectorMethods.colwidth[0] = 150;
            frmSelectorMethods.colname[1] = "Itmdesccol";
            frmSelectorMethods.colheadertext[1] = "Description";
            frmSelectorMethods.coldatapropertyname[1] = "descrip";
            frmSelectorMethods.colwidth[1] = 400;
            frmSelectorMethods.SetGrid();
            frmSelector.Width = 700;
            frmSelectorMethods.searchcolumm = "item";
            frmSelectorMethods.ShowSelector();

            // If a row has been selected fill the data and process
            if (frmSelectorMethods.returnkey != null && frmSelectorMethods.returnkey.TrimEnd() != "")
            {
                //TODO: CACHED need separate dataset for 'all' data 
                _itemSelectorDs.immaster.Rows.Clear();
                itemSelectorDsCache.Invalidate();

                ClearParameters();
                AddParms("@item", frmSelectorMethods.returnkey, "SQL");
                commandtext = "SELECT * FROM  immaster WHERE  item = @item";
                FillData(_itemSelectorDs, "immaster", commandtext, CommandType.Text);
                if (_itemSelectorDs.immaster.Count > 0)
                {
                    item = _itemSelectorDs.immaster[0].item;
                }
            }

            return item;
        }

        public string SelectIcloct(string locseek)
        {
            string loctid = "";
            return loctid;
        }

        public decimal GetItemLoctidAvailability(string item, string loctid)
        {
            decimal ItemLoctidQtyAvailable = 0;
            string commandtext = "SELECT * FROM view_itemloctidonhand WHERE item = @item AND loctid = @loctid";
            ClearParameters();
            invds.view_itemloctidonhand.Rows.Clear();
            AddParms("@item", item, "SQL");
            AddParms("@loctid", loctid, "SQL");
            this.FillData(invds, "view_itemloctidonhand", commandtext, CommandType.Text);
            if (invds.view_itemloctidonhand.Rows.Count > 0)
            {
                ItemLoctidQtyAvailable = invds.view_itemloctidonhand[0].tqty - invds.view_itemloctidonhand[0].allocated; ;
            }
            return ItemLoctidQtyAvailable;
        }

        public void ProcessStockCoverFileNumbers(string item, string loctid, decimal tqty, string tnotes)
        {
            string meycono = "";
            // Loop until the number of file numbers has been created
            for (int i = 1; i <= tqty; i++)
            {
                // Get the next file number
                meycono = appInformation.GetMeycono();
                if (meycono.TrimEnd() != "")
                {
                    ClearParameters();
                    this.AddParms("@loctid", loctid, "SQL");
                    this.AddParms("@item", item, "SQL");
                    this.AddParms("@tnotes", tnotes, "SQL");
                    this.AddParms("@meycono", meycono, "SQL");
                    this.AddParms("@user", AppUserClass.AppUserId, "SQL");
                    try
                    {
                        ExecuteCommand("wsgsp_insertictranstockcoverhold", CommandType.StoredProcedure);
                    }
                    catch (SqlException ex)
                    {
                        HandleException(ex);
                    }
                }
                else
                {
                    wsgUtilities.wsgNotice("There has been an error creating the file number.");
                    break;
                }
            }
        }

        public void ProcessInventoryReceiptTransaction(string item, string loctid, DateTime tdate, decimal tqty, decimal price, string tnotes, string docno, string meycono)
        {
            calcinvds.ictran.Rows.Clear();
            // Record receipt
            int receiptid = InsertInventoryReceiptTransaction(" ", item, loctid, tdate, tqty, price, tnotes, docno, meycono);
            // Establish Accounting Row
            InsertInventoryAccountingTransaction(item, loctid, tdate, 0, tqty, price, tnotes, docno, "R", receiptid);
        }

        public void ProcessInventoryIssueTransaction(string item, string loctid, DateTime tdate, decimal tqty, string tnotes, string docno, string meycono)
        {
            decimal NeededQty = tqty;
            invds.view_itemloctidtdateonhand.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@item", item, "SQL");
            this.AddParms("@loctid", loctid, "SQL");
            this.FillData(invds, "view_itemloctidtdateonhand", "wsgsp_getopenitemloctid", CommandType.StoredProcedure);
            if (invds.view_itemloctidtdateonhand.Rows.Count > 0)
            {
                // Loop until the requented quantity has been issued
                for (int r = 0; r <= invds.view_itemloctidtdateonhand.Rows.Count - 1; r++)
                {
                    if (invds.view_itemloctidtdateonhand[r].onhand >= NeededQty)
                    {
                        InsertInventoryIssue(item, loctid, tdate, 0, -NeededQty, invds.view_itemloctidtdateonhand[r].price, meycono, tnotes, docno, invds.view_itemloctidtdateonhand[r].parenttransid);
                        // Establish Accounting Row
                        InsertInventoryAccountingTransaction(item, loctid, tdate, 0, tqty, invds.view_itemloctidtdateonhand[r].price, tnotes, docno, "I", 0);
                        NeededQty = 0;
                        break;
                    }
                    else
                    {
                        InsertInventoryIssue(item, loctid, tdate, 0, -invds.view_itemloctidtdateonhand[r].onhand, invds.view_itemloctidtdateonhand[r].price, meycono, tnotes, docno, invds.view_itemloctidtdateonhand[r].parenttransid);
                        InsertInventoryAccountingTransaction(item, loctid, tdate, 0, tqty, invds.view_itemloctidtdateonhand[r].price, tnotes, docno, "I", 0);

                        NeededQty -= invds.view_itemloctidtdateonhand[r].onhand;
                        continue;
                    }
                }
            }
            if (NeededQty != 0)
            {
                decimal IssuePrice = 0;
                //Use price of the last receipt, or 0 if there were no receipts
                if (invds.view_itemloctidtdateonhand.Rows.Count > 0)
                {
                    IssuePrice = invds.view_itemloctidtdateonhand[invds.view_itemloctidtdateonhand.Rows.Count - 1].price;
                }
                // Create a receipt and issue for the item and location at the issue price.
                InsertInventoryReceiptAndIssueTransaction("", item, loctid, tdate, NeededQty, IssuePrice, tnotes, docno, "", "N");
            }
        }

        public void InsertInventoryAccountingTransaction(string item, string loctid, DateTime tdate, int sourcelineno, decimal tqty, decimal price, string tnotes, string docno, string trantyp, int parenttransid)
        {
            calcinvds.ictran.Rows.Add();
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].item = item;
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].loctid = loctid;
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].tdate = tdate;
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].sourcelineno = sourcelineno;
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].tqty = tqty;
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].price = price;
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].tnotes = tnotes;
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].docno = docno;
            calcinvds.ictran[calcinvds.ictran.Rows.Count - 1].parenttransid = parenttransid;
        }

        public void InsertInventoryIssue(string item, string loctid, DateTime tdate, int sourcelineno, decimal tqty, decimal price, string meycono, string tnotes, string docno, int parentransid)
        {
            ClearParameters();
            this.AddParms("@tranno", "", "SQL");
            this.AddParms("@loctid", loctid, "SQL");
            this.AddParms("@item", item, "SQL");
            this.AddParms("@trantyp", "I", "SQL");
            this.AddParms("@tdate", tdate, "SQL");
            this.AddParms("@parenttransid", parentransid, "SQL");
            this.AddParms("@sourcelineno", sourcelineno, "SQL");
            this.AddParms("@tqty", tqty, "SQL");
            this.AddParms("@tnotes", tnotes, "SQL");
            this.AddParms("@docno", docno, "SQL");
            this.AddParms("@price", price, "SQL");
            this.AddParms("@meycono", meycono, "SQL");
            this.AddParms("@user", AppUserClass.AppUserId, "SQL");
            try
            {
                ExecuteCommand("wsgsp_insertictranissue", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void InsertInventoryReceiptAndIssueTransaction(string tranno, string item, string loctid, DateTime tdate, decimal tqty, decimal price, string tnotes, string docno, string meycono, string labelprinted)
        {
            // Record receipt
            int receiptid = InsertInventoryReceiptTransaction(" ", item, loctid, tdate, tqty, price, tnotes, docno, meycono);
            // Establish Accounting Row
            InsertInventoryAccountingTransaction(item, loctid, tdate, 0, tqty, price, tnotes, docno, "R", receiptid);
            // Record Issue
            InsertInventoryIssue(item, loctid, tdate, 0, -tqty, price, meycono, tnotes, docno, receiptid);
            InsertInventoryAccountingTransaction(item, loctid, tdate, 0, -tqty, price, tnotes, docno, "I", 0);
        }

        public int InsertInventoryReceiptTransaction(string tranno, string item, string loctid, DateTime tdate, decimal tqty, decimal price, string tnotes, string docno, string meycono)
        {
            int returnvalue = 0;
            ClearParameters();
            this.AddParms("@tranno", tranno, "SQL");
            this.AddParms("@loctid", loctid, "SQL");
            this.AddParms("@item", item, "SQL");
            this.AddParms("@trantyp", "R", "SQL");
            this.AddParms("@tdate", tdate, "SQL");
            this.AddParms("@sourcelineno", 0, "SQL");
            this.AddParms("@tqty", tqty, "SQL");
            this.AddParms("@tnotes", tnotes, "SQL");
            this.AddParms("@docno", docno, "SQL");
            this.AddParms("@price", price, "SQL");
            this.AddParms("@meycono", meycono, "SQL");
            this.AddParms("@user", AppUserClass.AppUserId, "SQL");
            this.AddParms("@ReceiptId", 0, ParameterDirection.Output, "SQL");
            try
            {
                // The procedure will return the ID of the added row
                returnvalue = ExecuteIntOutputCommand("wsgsp_insertictranreceipt", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
            return returnvalue;
        }

        public void ProcessMiscellaneousOrderShipment(string sono, string loctid, DateTime tdate)
        {
            string CommandString = "SELECT * FROM view_expandedsoline WHERE sono = @sono";
            orderds.view_expandedsoline.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(orderds, "view_expandedsoline", CommandString, CommandType.Text);
            // Loop through lines and process inventory for each line
            for (int r = 0; r <= orderds.view_expandedsoline.Rows.Count - 1; r++)
            {
                // Process stock items with no zero quantities
                if (orderds.view_expandedsoline[r].stkcode == "Y" && orderds.view_expandedsoline[r].qtyact != 0)
                {
                    // Issue the inventory
                    ProcessInventoryIssueTransaction(orderds.view_expandedsoline[r].item, loctid, tdate,
                    orderds.view_expandedsoline[r].qtyact, "SO Shipment", sono, "");
                }
                else
                {
                    continue;
                }
            }
        }

        public void ProcessCoverShipment(string sono, string loctid, DateTime tdate)
        {
            quoteds.view_soreportlinedata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.FillData(quoteds, "view_soreportlinedata", "wsgsp_getallsoreportlinedata", CommandType.StoredProcedure);
            // Loop through lines and process inventory for each line
            if (quoteds.view_soreportlinedata.Rows.Count > 0)
            {
                for (int r = 0; r <= quoteds.view_soreportlinedata.Rows.Count - 1; r++)
                {
                    // Process stock items with no zero quantities
                    if (quoteds.view_soreportlinedata[r].stocking == true && quoteds.view_soreportlinedata[r].qtyact != 0)
                    {
                        // Issue the inventory
                        ProcessInventoryIssueTransaction(quoteds.view_soreportlinedata[r].item, loctid, tdate,
                         quoteds.view_soreportlinedata[r].qtyact, "SO Shipment", sono, "");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public decimal GetLastReceiptPrice(string item, string loctid)
        {
            string CommandString = "SELECT TOP 1 * FROM ictran WHERE trantyp = 'R' and tstat = 'A' ";
            CommandString += " AND item = @item and loctid = @loctid ORDER BY tdate,adddate DESC";
            decimal lastprice = 0;
            ClearParameters();
            AddParms("@loctid", loctid, "SQL");
            AddParms("@item", item, "SQL");
            this.FillData(tempinvds, "ictran", CommandString, CommandType.Text);
            if (tempinvds.ictran.Rows.Count > 0)
            {
                lastprice = tempinvds.ictran[0].price;
            }
            return lastprice;
        }
    }
}