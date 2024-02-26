using CommonAppClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CustomerMaintenance
{
    public class CustomerMaintenanceMethods : WSGDataAccess
    {
        // Create the customer processing object
        private CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");

        private CustomerTermsMethods customerTermsMethods = new CustomerTermsMethods();
        private AlereCodeMethods alereCodeMethods = new AlereCodeMethods();
        private WSGUtilities wsgUtilities = new WSGUtilities("Customer Maintenance");
        private AlereDataMethods alereDataMethods = new AlereDataMethods();
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        public customer ards { get; set; }
        public customer custsearch { get; set; }
        public system allalerts { get; set; }
        public system unassignedalerts { get; set; }
        private incidentds inds = new incidentds();
        private BindingSource bindingSourceIncidents = new BindingSource();
        private tracking unassignedtrackingds = new tracking();
        private tracking assignedtrackingds = new tracking();
        private tracking trackingds = new tracking();
        private ticketds ticketDs = new ticketds();
        public system prioralerts { get; set; }
        public system assignedalerts { get; set; }
        public Form menuForm { get; set; }
        public int CurrentCustid = 0;
        public item unassignedHWpreferences = new item();
        public customer assignedHWpreferences = new customer();
        public Contacts.ContactMethods contactMethods = new Contacts.ContactMethods("SQL", "SQLConnstring");
        public customer tempHWpreferences = new customer();
        public BindingSource assignedHWBinding = new BindingSource();
        public BindingSource contactBinding = new BindingSource();
        public string CurrentState { get; set; }
        public string currentcustno { get; set; }
        private EmailAddressMethods emailMethods = new CustomerMaintenance.EmailAddressMethods("SQL", "SQLConnstring");
        public FrmMaintainCustomer parentForm = new FrmMaintainCustomer();
        public DataSet ds { get; set; }
        public alereds AlereDs = new alereds();
        public DataSet vfpsysds { get; set; }
        private MiscellaneousDataMethods miscdata = new MiscellaneousDataMethods("SQL", "SQLConnString");

        public CustomerMaintenanceMethods(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            assignedalerts = new system();
            unassignedalerts = new system();
            prioralerts = new system();
            custsearch = new customer();
            ards = new customer();
            SetIdcol(ards.arcust.idcolColumn);
            SetIdcol(ards.aracadr.idcolColumn);
            SetIdcol(ards.custalerts.idcolColumn);
            SetIdcol(assignedHWpreferences.custhwpref.idcolColumn);
            SetIdcol(trackingds.custroutestep.idcolColumn);
            SetEvents();
            parentForm.tabControlCustomer.SelectedIndex = 0;
            CurrentState = "Select";
            RefreshControls();
        }

        public void SetEvents()
        {
            parentForm.buttonInvoiceEmail.Click += new System.EventHandler(buttonInvoiceEmail_Click);
            parentForm.buttonOrderEmail.Click += new System.EventHandler(buttonOrderEmail_Click);
            parentForm.buttonQuoteEmail.Click += new System.EventHandler(buttonQuoteEmail_Click);
            parentForm.buttonAddContact.Click += new System.EventHandler(buttonAddcontact_Click);
            parentForm.buttonDefaultEmail.Click += new System.EventHandler(buttonDefaultEmail_Click);
            parentForm.buttonClear.Click += new System.EventHandler(buttonClear_Click);
            parentForm.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            parentForm.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            parentForm.buttonCancelEdit.Click += new System.EventHandler(this.buttonCancelEdit_Click);
            parentForm.buttonTickets.Click += new System.EventHandler(this.buttonTickets_Click);
            parentForm.buttonTaxDistrict.Click += new System.EventHandler(this.buttonSelectTaxDistrict_Click);

            parentForm.buttonFindTerms.Click += new System.EventHandler(this.buttonFindTerms_Click);
            parentForm.buttonGetSalesmn.Click += new System.EventHandler(this.buttonGetSalesmn_Click);
            parentForm.buttonGetServrep.Click += new System.EventHandler(this.buttonGetServrep_Click);
            parentForm.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            parentForm.buttonSelectCustomer.Click += new System.EventHandler(this.buttonSelectCustomer_Click);
            parentForm.buttonShipTo.Click += new System.EventHandler(this.buttonShipTo_Click);
            parentForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmMaintainCustomer_FormClosing);
            parentForm.textBoxCustno.KeyDown += new System.Windows.Forms.KeyEventHandler(textBoxCustno_KeyDown);
            parentForm.buttonSave.Click += new System.EventHandler(buttonSave_Click);
            parentForm.listBoxUnassignedAlerts.DoubleClick += new System.EventHandler(listBoxUnassignedAlerts_DoubleClick);
            parentForm.listBoxAssignedAlerts.DoubleClick += new System.EventHandler(listBoxAssignedAlerts_DoubleClick);
            parentForm.listBoxUnassignedRoutes.DoubleClick += new System.EventHandler(listBoxUnassignedRoutes_DoubleClick);
            parentForm.listBoxAssignedRoutes.DoubleClick += new System.EventHandler(listBoxAssignedRoutes_DoubleClick);
            parentForm.listBoxUnassignedHWPreferences.DoubleClick += new System.EventHandler(listBoxUnAssignedHWPreferences_DoubleClick);
            parentForm.dataGridViewAssignedHWPreferences.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAssignedHWPreferences_CellContentDoubleClick);
            parentForm.dataGridViewContactlist.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(datadataGridViewContactlist_CellContentDoubleClick);
        }

        public void EstablishCustomerRoutes()
        {
            // Unassigned
            string commandString = "SELECT  * from view_expandedroutedata where custstep = 'Y' AND idcol NOT  IN (SELECT routestepid FROM custroutestep WHERE custno = @custno)";
            this.ClearParameters();
            unassignedtrackingds.view_expandedroutedata.Rows.Clear();
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.FillData(unassignedtrackingds, "view_expandedroutedata", commandString, CommandType.Text);

            // Assigned
            commandString = "SELECT  * from view_expandedroutedata where custstep = 'Y' AND idcol  IN (SELECT routestepid FROM custroutestep WHERE custno = @custno)";
            this.ClearParameters();
            assignedtrackingds.view_expandedroutedata.Rows.Clear();
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.FillData(assignedtrackingds, "view_expandedroutedata", commandString, CommandType.Text);
        }

        public void EstablishContacts()
        {
            // Unassigned
            string commandString = "SELECT  * from contact WHERE custno = @custno ORDER BY contactname";
            this.ClearParameters();
            ards.contact.Rows.Clear();
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.FillData(ards, "contact", commandString, CommandType.Text);
        }

        public void EstablishIncidents()
        {
            // Unassigned
            string commandString = "SELECT  * from view_expandedincident WHERE custno = @custno ORDER BY sono";
            this.ClearParameters();
            inds.view_expandedincident.Rows.Clear();
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.FillData(inds, "view_expandedincident", commandString, CommandType.Text);
        }

        private void ProcessHardwareitem(string item, string custno, decimal customerprice)
        {
            if (customerprice != 0)
            {
                decimal pricecode = 0;
                if (customerprice > 0)
                {
                    pricecode = customerprice;
                }
                assignedHWpreferences.custhwpref.Rows.Clear();
                EstablishBlankDataTableRow(assignedHWpreferences.custhwpref);
                assignedHWpreferences.custhwpref[0].custno = custno;
                assignedHWpreferences.custhwpref[0].item = item;
                assignedHWpreferences.custhwpref[0].pricecode = pricecode;
                GenerateAppTableRowSave(assignedHWpreferences.custhwpref[0]);
            }
        }

        public void EstablishHWPreferences()
        {
            // Unassigned
            string commandString = "SELECT * FROM view_immasterdata WHERE LEFT(misccode,2) = 'HW' AND item NOT IN (SELECT item FROM view_expandedcusthwpref WHERE custno = @custno) ORDER BY shortdescrip ";
            this.ClearParameters();
            unassignedHWpreferences.view_immasterdata.Rows.Clear();
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.FillData(unassignedHWpreferences, "view_immasterdata", commandString, CommandType.Text);
            // Assigned
            commandString = "SELECT * FROM view_expandedcusthwpref WHERE custno = @custno";
            this.ClearParameters();
            assignedHWpreferences.view_expandedcusthwpref.Rows.Clear();
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.FillData(assignedHWpreferences, "view_expandedcusthwpref", commandString, CommandType.Text);
        }

        public void EstablishAlerts()
        {
            // Unassigned
            string commandString = "SELECT  * from sysreference where idcol NOT IN (SELECT alertid FROM custalerts WHERE custno = @custno)";
            commandString += " AND groupid IN (SELECT idcol FROM sysrefgroup WHERE groupname = 'Alerts')";
            this.ClearParameters();
            unassignedalerts.sysreference.Rows.Clear();
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.FillData(unassignedalerts, "sysreference", commandString, CommandType.Text);
            // Assigned Existing Alerts for this customer
            commandString = "SELECT  * from sysreference where idcol IN (SELECT alertid FROM custalerts WHERE custno = @custno)";
            this.ClearParameters();
            assignedalerts.sysreference.Rows.Clear();
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.FillData(assignedalerts, "sysreference", commandString, CommandType.Text);
        }

        public void EstablishBlankCustomerData()
        {
            EstablishBlankDataTableRow(ards.arcust);
            ards.arcust[0].country = "USA";
            ards.arcust[0].currency = "USD";
            ards.arcust[0].nocable = "N";
            ards.arcust[0].nopump = "N";
            ards.arcust[0].oringsvssnaps = "N";
            ards.arcust[0].perimpadpref = "N";
            ards.arcust[0].pinsplanters = "N";
            ards.arcust[0].pipesplanters = "N";
            ards.arcust[0].stakesplanters = "N";
            ards.arcust[0].quomeycolite = "N";
            ards.arcust[0].quoruggedmesh = "N";
            ards.arcust[0].quosoliddrain = "N";
            ards.arcust[0].quosolidpump = "N";
        }

        public void ClearCustomerData()
        {
            ClearDataTable(ards.arcust);
            ards.arcust.Clear();
        }

        public void ShowParent()
        {
            parentForm.MdiParent = menuForm;
            parentForm.Show();
        }

        public int SaveCustomerData()
        {
            int customerreturn = 1;

            // Check for a valid tax district
            if (appInformation.GetDistrictDescription(ards.arcust[0].taxdist) == "Unknown")
            {
                wsgUtilities.wsgNotice("There must be a valid tax district");
                customerreturn = 0;
            }

            if (customerreturn > 0)
            {
                if (ards.arcust[0].idcol < 0)
                {
                    if (!CheckNewCustno())
                    {
                        customerreturn = 0;
                    }
                }
            }
            if (customerreturn > 0)
            {
                // Save arcust
                GenerateAppTableRowSave(ards.arcust[0]);
                //Populate Alere tables
                SaveAlereCustomerData();
                // Save custalerts
                // Remove all custalerts rows for this customer
                this.ClearParameters();
                this.AddParms("@custno", ards.arcust[0].custno, "SQL");
                string commandString = "DELETE FROM custalerts WHERE custno = @custno";
                try
                {
                    ExecuteCommand(commandString, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    SqlExceptionHandler.HandleException(ex);
                }
                for (int i = 0; i <= assignedalerts.sysreference.Rows.Count - 1; i++)
                {
                    ards.custalerts.Rows.Clear();
                    ards.custalerts.AcceptChanges();
                    EstablishBlankDataTableRow(ards.custalerts);
                    ards.custalerts[0].alertid = assignedalerts.sysreference[i].idcol;
                    ards.custalerts[0].custno = ards.arcust[0].custno;
                    GenerateAppTableRowSave(ards.custalerts[0]);
                }
                // Remove all route steps rows for this customer
                this.ClearParameters();
                this.AddParms("@custno", ards.arcust[0].custno, "SQL");
                commandString = "DELETE FROM custroutestep WHERE custno = @custno";
                try
                {
                    ExecuteCommand(commandString, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    SqlExceptionHandler.HandleException(ex);
                }
                for (int i = 0; i <= assignedtrackingds.view_expandedroutedata.Rows.Count - 1; i++)
                {
                    trackingds.custroutestep.Rows.Clear();
                    trackingds.custroutestep.AcceptChanges();
                    EstablishBlankDataTableRow(trackingds.custroutestep);
                    trackingds.custroutestep[0].custno = ards.arcust[0].custno;
                    trackingds.custroutestep[0].routestepid = assignedtrackingds.view_expandedroutedata[i].idcol;
                    GenerateAppTableRowSave(trackingds.custroutestep[0]);
                }

                // Remove all hardware preferences for this customer
                this.ClearParameters();
                this.AddParms("@custno", ards.arcust[0].custno, "SQL");
                commandString = "DELETE FROM custhwpref WHERE custno = @custno";
                try
                {
                    ExecuteCommand(commandString, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    SqlExceptionHandler.HandleException(ex);
                }
                for (int i = 0; i <= assignedHWpreferences.view_expandedcusthwpref.Rows.Count - 1; i++)
                {
                    assignedHWpreferences.custhwpref.Rows.Clear();
                    assignedHWpreferences.custhwpref.AcceptChanges();
                    EstablishBlankDataTableRow(assignedHWpreferences.custhwpref);
                    assignedHWpreferences.custhwpref[0].custno = ards.arcust[0].custno;
                    assignedHWpreferences.custhwpref[0].item = assignedHWpreferences.view_expandedcusthwpref[i].item;
                    assignedHWpreferences.custhwpref[0].pricecode = assignedHWpreferences.view_expandedcusthwpref[i].pricecode;
                    GenerateAppTableRowSave(assignedHWpreferences.custhwpref[0]);
                }
            }
            return customerreturn;
        }

        public void SaveAlereCustomerData()
        {
            bool inserting = false;
            String CommandString = "SELECT * FROM coaddr WHERE coid = @coid";
            AlereDs.coaddr.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@coid", ards.arcust[0].custno, "SQL");
            this.FillData(AlereDs, "coaddr", CommandString, CommandType.Text);
            if (AlereDs.coaddr.Rows.Count == 0)
            {
                inserting = true;
            }
            // coaddr table
            AlereDs.coaddr.Rows.Clear();
            AlereDs.coaddr.Rows.Add();
            EstablishBlankDataTableRow(AlereDs.coaddr);
            AlereDs.coaddr[0].coid = ards.arcust[0].custno;

            AlereDs.coaddr[0].coname = ards.arcust[0].company;
            AlereDs.coaddr[0].street1 = ards.arcust[0].address1;
            AlereDs.coaddr[0].street2 = ards.arcust[0].address2;
            AlereDs.coaddr[0].city = ards.arcust[0].city;
            AlereDs.coaddr[0].state = ards.arcust[0].state;
            AlereDs.coaddr[0].zip = ards.arcust[0].zip;
            AlereDs.coaddr[0].country = ards.arcust[0].country;
            AlereDs.coaddr[0].phone = ards.arcust[0].phone;
            AlereDs.coaddr[0].email = ards.arcust[0].email;
            AlereDs.coaddr[0].fax = ards.arcust[0].faxno;
            AlereDs.coaddr[0].salesman = ards.arcust[0].salesmn;
            AlereDs.coaddr[0].active = true;
            AlereDs.coaddr[0].locid = ards.arcust[0].custno;
            AlereDs.coaddr[0].locdesc = "Meyco Products";
            AlereDs.coaddr[0].rptname = "C";
            AlereDs.coaddr[0].billid = ards.arcust[0].custno;
            AlereDs.coaddr[0].billloc = ards.arcust[0].custno;
            AlereDs.coaddr[0].remitid = ards.arcust[0].custno;
            AlereDs.coaddr[0].remitloc = ards.arcust[0].custno;
            AlereDs.coaddr[0].mainlocn = true;
            AlereDs.coaddr[0].isbill = true;
            AlereDs.coaddr[0].isshipto = true;
            AlereDs.coaddr[0].isremit = true;
            AlereDs.coaddr[0].cotype = "C";
            AlereDs.coaddr[0].costatus = "";
            AlereDs.coaddr[0].taxdist = ards.arcust[0].taxdist;
            if (inserting)
            {
                AlereDs.coaddr[0].nextcall = Convert.ToDateTime("01/01/1900 00:00:00.00");
                AlereDs.coaddr[0].lastcall = Convert.ToDateTime("01/01/1900 00:00:00.00");
                AlereDs.coaddr[0].active = true;

                alereDataMethods.GenerateAlereTableRowSave(AlereDs.coaddr[0], true, "");
            }
            else
            {
                alereDataMethods.GenerateAlereTableRowSave(AlereDs.coaddr[0], false, " coid = @coid");
            }
            inserting = false;
            CommandString = "SELECT * FROM slcust WHERE coid = @coid";
            AlereDs.slcust.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@coid", ards.arcust[0].custno, "SQL");
            this.FillData(AlereDs, "slcust", CommandString, CommandType.Text);
            if (AlereDs.slcust.Rows.Count == 0)
            {
                inserting = true;
            }
            else
            {
                inserting = false;
            }
            AlereDs.slcust.Rows.Clear();
            AlereDs.slcust.Rows.Add();
            EstablishBlankDataTableRow(AlereDs.slcust);
            AlereDs.slcust[0].coid = ards.arcust[0].custno;
            AlereDs.slcust[0].terms = ards.arcust[0].termid;
            AlereDs.slcust[0].crlimit = ards.arcust[0].limit;
            AlereDs.slcust[0].prlevel = "DEF";
            AlereDs.slcust[0].crstatus = "O";
            AlereDs.slcust[0].locid = ards.arcust[0].custno;
            AlereDs.slcust[0].shiploc = ards.arcust[0].custno;
            AlereDs.slcust[0].finance = "18";
            AlereDs.slcust[0].currency = "USD";
            if (inserting)
            {
                AlereDs.slcust[0].lastsale = Convert.ToDateTime("01/01/1900 00:00:00.00");
                AlereDs.slcust[0].lastpaid = Convert.ToDateTime("01/01/1900 00:00:00.00");
                AlereDs.slcust[0].shipid = ards.arcust[0].custno;
                AlereDs.slcust[0].lastpaid = Convert.ToDateTime("01/01/1900 00:00:00.00");
                alereDataMethods.GenerateAlereTableRowSave(AlereDs.slcust[0], true, "");
            }
            else
            {
                alereDataMethods.GenerateAlereTableRowSave(AlereDs.slcust[0], false, " coid = @coid");
            }
        }

        private bool CheckNewCustno()
        {
            bool newcustnook = true;
            this.ClearParameters();
            // Prevent duplicate customer number
            this.AddParms("@custno", ards.arcust[0].custno, "SQL");
            this.AddStringOutputParm("@custmessage", 40);
            if (this.ExecuteStringOutputCommand("wsgsp_checkcustno", CommandType.StoredProcedure).Trim() == "OK")
            {
                // Check the Alere table
                AlereDs.coaddr.Rows.Clear();
                string CommandString = "SELECT coid FROM coaddr WHERE coid = @custno";
                this.FillData(AlereDs, "coaddr", CommandString, CommandType.Text);
                if (AlereDs.coaddr.Rows.Count > 0)
                {
                    wsgUtilities.wsgNotice(ards.arcust[0].custno + " is already in the Alere table");
                    newcustnook = false;
                }
            }
            else
            {
                wsgUtilities.wsgNotice(ards.arcust[0].custno + " is already in the customer table");
                newcustnook = false;
            }
            return newcustnook;
        }

        public string LockCustomer(int Custid)
        {
            this.ClearParameters();
            this.AddParms("@idcol", Custid, "SQL");
            this.AddParms("@tablename", "arcust", "SQL");
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

        public void UnlockCustomer(int Custid)
        {
            this.ClearParameters();
            this.AddParms("@idcol", Custid, "SQL");
            this.AddParms("@tablename", "arcust", "SQL");

            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                SqlExceptionHandler.HandleException(ex);
            }
        }

        public void RefreshControls()
        {
            parentForm.buttonTickets.BackColor = System.Drawing.SystemColors.Control;
            parentForm.textBoxTax.Enabled = false;
            parentForm.textBoxTaxdist.Enabled = false;
            parentForm.buttonCancelEdit.Enabled = false;
            switch (CurrentState)
            {
                case "View":
                    {
                        // Loop thru all the controls on each tab page and disable text boxes, list boxes and  buttons
                        foreach (Control c in parentForm.tabControlCustomer.Controls)
                        {
                            foreach (Control d in c.Controls)
                            {
                                if (d is TextBox || d is Button || d is CheckBox || d is ListBox)
                                {
                                    d.Enabled = false;
                                    continue;
                                }
                            }
                        }

                        if (CustomerHasTickets(ards.arcust[0].custno))
                        {
                            parentForm.buttonTickets.BackColor = Color.Cyan;
                        }

                        parentForm.buttonQuoteEmail.Enabled = true;
                        parentForm.buttonInvoiceEmail.Enabled = true;
                        parentForm.buttonOrderEmail.Enabled = true;
                        parentForm.buttonDefaultEmail.Enabled = true;
                        parentForm.buttonSelectCustomer.Enabled = true;
                        parentForm.buttonEdit.Enabled = true;
                        parentForm.buttonShipTo.Enabled = true;
                        parentForm.buttonInsert.Enabled = false;
                        parentForm.buttonTickets.Enabled = true;
                        parentForm.buttonSave.Enabled = false;
                        parentForm.buttonShipTo.Enabled = true;
                        parentForm.buttonAddContact.Enabled = true;
                        break;
                    }
                case "Select":
                    {
                        // Loop thru all the controls on each tab page and disable text boxes and  buttons
                        foreach (Control c in parentForm.tabControlCustomer.Controls)
                        {
                            foreach (Control d in c.Controls)
                                if (d is TextBox || d is Button || d is CheckBox || d is ListBox)
                                {
                                    d.Enabled = false;
                                }
                        }
                        parentForm.textBoxCustno.Enabled = true;
                        parentForm.buttonSelectCustomer.Enabled = true;
                        parentForm.buttonInsert.Enabled = true;
                        parentForm.buttonShipTo.Enabled = false;
                        parentForm.buttonEdit.Enabled = false;
                        parentForm.buttonSave.Enabled = false;
                        parentForm.buttonTickets.Enabled = false;
                        break;
                    }
                case "Edit":
                    {
                        // Loop thru all the controls on each tab page and enable text boxes and  buttons
                        foreach (Control c in parentForm.tabControlCustomer.Controls)
                        {
                            foreach (Control d in c.Controls)
                                if (d is TextBox || d is Button || d is CheckBox || d is ListBox)
                                {
                                    d.Enabled = true;
                                }
                        }
                        parentForm.textBoxTaxdist.Enabled = false;
                        parentForm.textBoxTax.Enabled = false;
                        parentForm.textBoxCustno.Enabled = false;
                        parentForm.buttonShipTo.Enabled = false;
                        parentForm.buttonInsert.Enabled = false;
                        parentForm.buttonSelectCustomer.Enabled = false;
                        parentForm.buttonEdit.Enabled = false;
                        parentForm.buttonSave.Enabled = true;
                        parentForm.textBoxCustno.Enabled = false;
                        parentForm.buttonCancelEdit.Enabled = true;
                        parentForm.tabPageGeneral.Focus();
                        break;
                    }
                case "Insert":
                    {
                        // Loop thru all the controls on each tab page and enable text boxes and  buttons
                        foreach (Control c in parentForm.tabControlCustomer.Controls)
                        {
                            foreach (Control d in c.Controls)
                                if (d is TextBox || d is Button || d is CheckBox || d is ListBox)
                                {
                                    d.Enabled = true;
                                }
                        }
                        parentForm.textBoxTaxdist.Enabled = false;
                        parentForm.textBoxTax.Enabled = false;
                        parentForm.buttonSelectCustomer.Enabled = false;
                        parentForm.buttonShipTo.Enabled = true;
                        parentForm.buttonInsert.Enabled = false;
                        parentForm.buttonEdit.Enabled = false;
                        parentForm.buttonSave.Enabled = true;
                        parentForm.tabPageGeneral.Focus();
                        break;
                    }
            }
            parentForm.buttonTickets.Invalidate();
            parentForm.Update();
            parentForm.Refresh();
        }

        public void SetBindings()
        {
            foreach (Control c in parentForm.tabControlCustomer.Controls)
            {
                foreach (Control d in c.Controls)
                {
                    if (d is TextBox)
                    {
                        int length = d.Name.ToString().Length;
                        string columnname = d.Name.ToString().Substring(7, length - 7);
                        if (columnname.Substring(0, 3).ToUpper() == "DEP" || columnname.TrimEnd().ToUpper() == "LIMIT")
                        {
                            SetTextBoxCurrencyBinding((TextBox)d, ards, "arcust." + columnname);
                        }
                        else
                        {
                            d.DataBindings.Clear();
                            d.DataBindings.Add("Text", ards.arcust, columnname);
                        }
                        continue;
                    }
                }
            }
            // Unassigned Alerts
            parentForm.listBoxUnassignedAlerts.ValueMember = "idcol";
            parentForm.listBoxUnassignedAlerts.DisplayMember = "refdescrip";
            parentForm.listBoxUnassignedAlerts.DataSource = unassignedalerts.sysreference;
            // Assigned Alerts
            parentForm.listBoxAssignedAlerts.ValueMember = "idcol";
            parentForm.listBoxAssignedAlerts.DisplayMember = "refdescrip";
            parentForm.listBoxAssignedAlerts.DataSource = assignedalerts.sysreference;
            // Unassigned Route Steps
            parentForm.listBoxUnassignedRoutes.ValueMember = "idcol";
            parentForm.listBoxUnassignedRoutes.DisplayMember = "fullroutestepname";
            parentForm.listBoxUnassignedRoutes.DataSource = unassignedtrackingds.view_expandedroutedata;
            // Assigned Route Steps
            parentForm.listBoxAssignedRoutes.ValueMember = "idcol";
            parentForm.listBoxAssignedRoutes.DisplayMember = "fullroutestepname";
            parentForm.listBoxAssignedRoutes.DataSource = assignedtrackingds.view_expandedroutedata;
            // Unassigned HW Preferences
            parentForm.listBoxUnassignedHWPreferences.ValueMember = "item";
            parentForm.listBoxUnassignedHWPreferences.DisplayMember = "shortdescrip";
            parentForm.listBoxUnassignedHWPreferences.DataSource = unassignedHWpreferences.view_immasterdata;
            // Assigned HW Preferences Datagridview
            parentForm.dataGridViewAssignedHWPreferences.AutoGenerateColumns = false;
            assignedHWBinding.DataSource = assignedHWpreferences.view_expandedcusthwpref;
            parentForm.dataGridViewAssignedHWPreferences.DataSource = assignedHWBinding;
            parentForm.dataGridViewAssignedHWPreferences.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentForm.dataGridViewAssignedHWPreferences.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            // Contacts
            parentForm.dataGridViewContactlist.AutoGenerateColumns = false;
            parentForm.dataGridViewContactlist.DataSource = contactBinding;
            contactBinding.DataSource = ards.contact;
            parentForm.dataGridViewContactlist.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentForm.dataGridViewContactlist.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            // Incidents
            parentForm.dataGridViewIncidents.AutoGenerateColumns = false;
            parentForm.dataGridViewIncidents.DataSource = bindingSourceIncidents;
            bindingSourceIncidents.DataSource = inds.view_expandedincident;
            parentForm.dataGridViewIncidents.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentForm.dataGridViewIncidents.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        private void SetTextBoxCurrencyBinding(TextBox txtbox, DataSet ds, string fieldname)
        {
            Binding b = new Binding("Text", ds, fieldname);
            {
                b.Format += new ConvertEventHandler(DecimalToCurrencyString);
                b.Parse += new ConvertEventHandler(CurrencyStringToDecimal);
            }
            txtbox.DataBindings.Clear();
            txtbox.DataBindings.Add(b);
        }

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

        #region Events

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    // Unlock Customer table
                    if (CurrentState == "Edit")
                    {
                        UnlockCustomer(CurrentCustid);
                    }
                    CurrentState = "Select";
                    parentForm.Close();
                }
                else
                {
                    CurrentState = "Select";
                    parentForm.Close();
                }
            }
            else
            {
                CurrentState = "Select";
                parentForm.Close();
            }
        }

        private void buttonSelectTaxDistrict_Click(object sender, EventArgs e)
        {
            string taxdistrict = appInformation.SelectTaxTable();
            if (taxdistrict != "")
            {
                ards.arcust[0].taxdist = taxdistrict;
                ards.arcust[0].tax = appInformation.GetDistrictTaxRate(taxdistrict);
                parentForm.labelTaxdescrip.Text = appInformation.GetDistrictDescription(taxdistrict);
            }
        }

        private void buttonSelectCustomer_Click(object sender, EventArgs e)
        {
            GetCustomerMethods getCust = new GetCustomerMethods("SQL", "SQLConnString");
            CurrentCustid = getCust.SelectedCustid;
            if (getCust.SelectedCustid != 0)
            {
                this.LoadCustomer(CurrentCustid);
            }
            else
            {
                CurrentState = "Select";
            }
            RefreshControls();
            parentForm.Update();
        }

        private void buttonAddcontact_Click(object sender, EventArgs e)
        {
            contactMethods.AddContact(ards.arcust[0].custno);
            EstablishContacts();
        }

        private void buttonQuoteEmail_Click(object sender, EventArgs e)
        {
            emailMethods.custno = ards.arcust[0].custno;
            emailMethods.addresstype = "Q";
            emailMethods.ShowParent();
        }

        private void buttonDefaultEmail_Click(object sender, EventArgs e)
        {
            emailMethods.custno = ards.arcust[0].custno;
            emailMethods.addresstype = "D";
            emailMethods.ShowParent();
        }

        private void buttonOrderEmail_Click(object sender, EventArgs e)
        {
            emailMethods.custno = ards.arcust[0].custno;
            emailMethods.addresstype = "O";
            emailMethods.ShowParent();
        }

        private void buttonInvoiceEmail_Click(object sender, EventArgs e)
        {
            emailMethods.custno = ards.arcust[0].custno;
            emailMethods.addresstype = "I";
            emailMethods.ShowParent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (SaveCustomerData() > 0)
            {
                parentForm.tabControlCustomer.SelectedTab = parentForm.tabControlCustomer.TabPages[0];
                CurrentState = "View";
                RefreshControls();
            }
        }

        private void FrmMaintainCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    // Unlock Customer table
                    if (CurrentState == "Edit")
                    {
                        UnlockCustomer(CurrentCustid);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void buttonGetServrep_Click(object sender, EventArgs e)
        {
            // Selects User to to be processed
            if (miscdata.GetUserId(true).TrimEnd() != "")
            {
                ards.arcust[0].servrep = miscdata.systemds.appuser[0].userid;
            }
            ards.arcust.AcceptChanges();
            parentForm.Update();
        }

        private void buttonGetSalesmn_Click(object sender, EventArgs e)
        {
            // Show the selector screen
            string salescode = alereCodeMethods.SelectCode("SLS");
            ards.arcust[0].salesmn = salescode.Substring(0, 4);
            ards.arcust.AcceptChanges();
            parentForm.Update();
        }

        private void buttonFindTerms_Click(object sender, EventArgs e)
        {
            string termid = customerTermsMethods.SelectTerms();
            if (termid.TrimEnd() != "")
            {
                ards.arcust[0].termid = customerTermsMethods.AlereDs.coterms[0].termid;
                ards.arcust[0].pdays = customerTermsMethods.AlereDs.coterms[0].discdays;
                ards.arcust[0].pnet = customerTermsMethods.AlereDs.coterms[0].duedays;
                ards.arcust[0].pdisc = customerTermsMethods.AlereDs.coterms[0].discrate;
                ards.arcust[0].pterms = customerTermsMethods.AlereDs.coterms[0].payterms.Substring(0, 20);
            }

            ards.arcust.AcceptChanges();
            parentForm.Update();
        }

        private void buttonShipTo_Click(object sender, EventArgs e)
        {
            FrmMaintainShipTo myfrmMaintainShipTo = new FrmMaintainShipTo();
            myfrmMaintainShipTo.CustId = CurrentCustid;
            myfrmMaintainShipTo.Custno = ards.arcust[0].custno;
            myfrmMaintainShipTo.CurrentState = "Select";
            myfrmMaintainShipTo.ShowDialog();
        }

        private void buttonTickets_Click(object sender, EventArgs e)
        {
            Ticketing.TicketMethods ticketMethods = new Ticketing.TicketMethods("SQL", "SQLConnString");
            ticketMethods.StartCustomerTicket(ards.arcust[0].custno);
            RefreshControls();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = LockCustomer(CurrentCustid);
            if (editstatus == "OK")
            {
                CurrentState = "Edit";
                RefreshControls();
            }
            else
            {
                wsgUtilities.wsgNotice(editstatus);
            }
        }

        private void buttonCancelEdit_Click(object sender, EventArgs e)
        {
            UnlockCustomer(CurrentCustid);
            this.ReloadCustomer();
        }

        private void textBoxCustno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (CurrentState == "Select")
                    CurrentCustid = customerAccess.getCustomerDatabyCustno(parentForm.textBoxCustno.Text);

                this.LoadCustomer(CurrentCustid);
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            CurrentState = "Insert";
            EstablishBlankCustomerData();
            SetBindings();
            RefreshControls();
        }


        private void ReloadCustomer()
        {
            this.LoadCustomer(this.CurrentCustid);
        }

        private void LoadCustomer(int customerId)
        {
            CurrentCustid = customerId;
            if (CurrentCustid != 0)
            {
                CurrentState = "View";
                customerAccess.getSingleCustomerData(customerId);
                ards = customerAccess.ards;
                SetBindings();
                EstablishAlerts();
                EstablishIncidents();
                EstablishContacts();
                EstablishHWPreferences();
                EstablishCustomerRoutes();
                RefreshControls();
                parentForm.labelTaxdescrip.Text = appInformation.GetDistrictDescription(ards.arcust[0].taxdist);
            }
            else
            {
                wsgUtilities.wsgNotice("Customer Not Found. Press Select Customer To Search Or Add A Customer");
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void listBoxAssignedAlerts_DoubleClick(object sender, EventArgs e)
        {
            if (assignedalerts.sysreference.Rows.Count > 0)
            {
                unassignedalerts.sysreference.ImportRow(assignedalerts.sysreference.Rows[parentForm.listBoxAssignedAlerts.SelectedIndex]);
                assignedalerts.sysreference.Rows[parentForm.listBoxAssignedAlerts.SelectedIndex].Delete();
                assignedalerts.sysreference.AcceptChanges();
                unassignedalerts.sysreference.AcceptChanges();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no assigned alerts");
            }
        }

        private void listBoxAssignedRoutes_DoubleClick(object sender, EventArgs e)
        {
            if (assignedtrackingds.view_expandedroutedata.Rows.Count > 0)
            {
                unassignedtrackingds.view_expandedroutedata.ImportRow(assignedtrackingds.view_expandedroutedata.Rows[parentForm.listBoxAssignedRoutes.SelectedIndex]);
                assignedtrackingds.view_expandedroutedata.Rows[parentForm.listBoxAssignedRoutes.SelectedIndex].Delete();
                unassignedtrackingds.view_expandedroutedata.AcceptChanges();
                assignedtrackingds.view_expandedroutedata.AcceptChanges();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no assigned routes");
            }
        }

        private void listBoxUnassignedRoutes_DoubleClick(object sender, EventArgs e)
        {
            if (unassignedtrackingds.view_expandedroutedata.Rows.Count > 0)
            {
                assignedtrackingds.view_expandedroutedata.ImportRow(unassignedtrackingds.view_expandedroutedata.Rows[parentForm.listBoxUnassignedRoutes.SelectedIndex]);
                unassignedtrackingds.view_expandedroutedata.Rows[parentForm.listBoxUnassignedRoutes.SelectedIndex].Delete();
                unassignedtrackingds.view_expandedroutedata.AcceptChanges();
                assignedtrackingds.view_expandedroutedata.AcceptChanges();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no unassigned routes");
            }
        }

        private void listBoxUnassignedAlerts_DoubleClick(object sender, EventArgs e)
        {
            if (unassignedalerts.sysreference.Rows.Count > 0)
            {
                assignedalerts.sysreference.ImportRow(unassignedalerts.sysreference.Rows[parentForm.listBoxUnassignedAlerts.SelectedIndex]);
                unassignedalerts.sysreference.Rows[parentForm.listBoxUnassignedAlerts.SelectedIndex].Delete();
                assignedalerts.sysreference.AcceptChanges();
                unassignedalerts.sysreference.AcceptChanges();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no unassigned alerts");
            }
        }

        private void dataGridViewAssignedHWPreferences_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            unassignedHWpreferences.view_immasterdata.Rows.Add();
            unassignedHWpreferences.view_immasterdata[unassignedHWpreferences.view_immasterdata.Rows.Count - 1].shortdescrip =
            assignedHWpreferences.view_expandedcusthwpref[parentForm.dataGridViewAssignedHWPreferences.CurrentRow.Index].shortdescrip;
            unassignedHWpreferences.view_immasterdata[unassignedHWpreferences.view_immasterdata.Rows.Count - 1].item =
            assignedHWpreferences.view_expandedcusthwpref[parentForm.dataGridViewAssignedHWPreferences.CurrentRow.Index].item;
            assignedHWpreferences.view_expandedcusthwpref.Rows[parentForm.dataGridViewAssignedHWPreferences.CurrentRow.Index].Delete();
            unassignedHWpreferences.view_immasterdata.AcceptChanges();
            assignedHWpreferences.view_expandedcusthwpref.AcceptChanges();
        }

        private void datadataGridViewContactlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectedContactId = CaptureIdCol(parentForm.dataGridViewContactlist);
            contactMethods.ProcessContact(SelectedContactId);
            EstablishContacts();
        }

        private void listBoxUnAssignedHWPreferences_DoubleClick(object sender, EventArgs e)
        {
            if (unassignedHWpreferences.view_immasterdata.Rows.Count > 0)
            {
                assignedHWpreferences.view_expandedcusthwpref.Rows.Add();
                assignedHWpreferences.view_expandedcusthwpref[assignedHWpreferences.view_expandedcusthwpref.Rows.Count - 1].shortdescrip =
                parentForm.listBoxUnassignedHWPreferences.Text;
                assignedHWpreferences.view_expandedcusthwpref[assignedHWpreferences.view_expandedcusthwpref.Rows.Count - 1].item =
                parentForm.listBoxUnassignedHWPreferences.SelectedValue.ToString();
                assignedHWpreferences.view_expandedcusthwpref[assignedHWpreferences.view_expandedcusthwpref.Rows.Count - 1].pricecode = 0;
                unassignedHWpreferences.view_immasterdata.Rows[parentForm.listBoxUnassignedHWPreferences.SelectedIndex].Delete();
                unassignedHWpreferences.view_immasterdata.AcceptChanges();
                assignedHWpreferences.view_expandedcusthwpref.AcceptChanges();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no Unassigned HW Preferences");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    // Unlock Customer table
                    if (CurrentState == "Edit")
                    {
                        UnlockCustomer(CurrentCustid);
                    }
                    ClearCustomerData();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                ClearCustomerData();
                CurrentState = "Select";
                RefreshControls();
            }
            parentForm.tabControlCustomer.SelectedTab = parentForm.tabControlCustomer.TabPages[0];
            parentForm.textBoxCustno.Focus();
            parentForm.Update();
        }

        public bool CustomerHasTickets(string custno)
        {
            bool hastickets = false;
            ticketDs.view_expandedticket.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@custno", custno, "SQL");
            string CommandString = "SELECT * FROM view_expandedticket WHERE RTRIM(ticketstatus) = 'Open' AND RTRIM(sono) = '' AND custno = @custno";
            FillData(ticketDs, "view_expandedticket", CommandString, CommandType.Text);
            if (ticketDs.view_expandedticket.Rows.Count > 0)
            {
                hastickets = true;
            }
            return hastickets;
        }

        #endregion Events
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