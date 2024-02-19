using CommonAppClasses;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Contacts
{
    public class ContactMethods : WSGDataAccess
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Contact Processing");
        public customer customerds { get; set; }
        public customer misccustomerds { get; set; }

        public customer customerselectords { get; set; }
        private int SelectedContactId = 0;
        public FrmContactInformation frmcontactInformation = new FrmContactInformation();
        private FrmContactSelector frmcontactselector = new FrmContactSelector();
        public BindingSource contactBinding = new BindingSource();
        private AutoCompleteStringCollection emaildata = new AutoCompleteStringCollection();

        private string CommandString = "";
        private string CurrentCustno = "";

        public ContactMethods(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            misccustomerds = new customer();
            customerselectords = new customer();
            customerds = new customer();
            SetIdcol(customerds.contact.idcolColumn);
            SetBindings();
            SetEvents();
        }

        public void AddContact(string custno)
        {
            EstabishEmailAddresses(custno);
            RefreshfrmContactMaintenanceControls("Insert");
            customerds.contact.Rows.Clear();
            EstablishBlankDataTableRow(customerds.contact);
            customerds.contact[0].custno = custno;

            frmcontactInformation.ShowDialog();
            // Refresh the grid
            CommandString = "SELECT  * from contact WHERE custno = @custno ORDER BY contactname";
            this.ClearParameters();
            customerselectords.contact.Rows.Clear();
            this.AddParms("@custno", custno, "SQL");
            this.FillData(customerselectords, "contact", CommandString, CommandType.Text);
        }

        public void EstabishEmailAddresses(string custno)
        {
            CommandString = "SELECT  * from contact WHERE custno = @custno ORDER BY contactemail";
            this.ClearParameters();
            misccustomerds.contact.Rows.Clear();
            this.AddParms("@custno", custno, "SQL");
            this.FillData(misccustomerds, "contact", CommandString, CommandType.Text);
            emaildata.Clear();

            for (int r = 0; r < misccustomerds.contact.Rows.Count - 1; r++)
            {
                emaildata.Add(misccustomerds.contact[r].contactemail.TrimEnd());
            }
            frmcontactInformation.textBoxContactemail.AutoCompleteCustomSource = emaildata;
        }

        public int SelectContact(string custno)
        {
            CurrentCustno = custno;
            SelectedContactId = 0;
            CommandString = "SELECT  * from contact WHERE custno = @custno ORDER BY contactname";
            this.ClearParameters();
            customerselectords.contact.Rows.Clear();
            this.AddParms("@custno", custno, "SQL");
            this.FillData(customerselectords, "contact", CommandString, CommandType.Text);
            frmcontactselector.ShowDialog();
            return SelectedContactId;
        }

        public void ProcessContact(int idcol)
        {
            RefreshfrmContactMaintenanceControls("Views");
            customerds.contact.Rows.Clear();
            CommandString = "SELECT * FROM  contact WHERE idcol = @idcol ";
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(customerds, "contact", CommandString, CommandType.Text);
            RefreshfrmContactMaintenanceControls("View");
            EstabishEmailAddresses(customerds.contact[0].custno);

            frmcontactInformation.ShowDialog();
        }

        public void RefreshfrmContactMaintenanceControls(string CurrentState)
        {
            DisableControls(frmcontactInformation);
            frmcontactInformation.buttonClose.Enabled = true;

            switch (CurrentState)
            {
                case "Edit":
                    {
                        frmcontactInformation.textBoxContactname.Enabled = true;
                        frmcontactInformation.textBoxContactemail.Enabled = true;
                        frmcontactInformation.textBoxContactPhone.Enabled = true;
                        frmcontactInformation.textBoxContactext.Enabled = true;
                        frmcontactInformation.textBoxContactnotes.Enabled = true;
                        frmcontactInformation.listBoxCarrier.Enabled = true;
                        frmcontactInformation.buttonSave.Enabled = true;
                        break;
                    }

                case "Insert":
                    {
                        frmcontactInformation.textBoxContactname.Enabled = true;
                        frmcontactInformation.textBoxContactemail.Enabled = true;
                        frmcontactInformation.textBoxContactPhone.Enabled = true;
                        frmcontactInformation.textBoxContactext.Enabled = true;
                        frmcontactInformation.textBoxContactnotes.Enabled = true;
                        frmcontactInformation.listBoxCarrier.Enabled = true;
                        frmcontactInformation.buttonSave.Enabled = true;
                        break;
                    }
                case "View":
                    {
                        frmcontactInformation.buttonEdit.Enabled = true;
                        break;
                    }
            }
        }

        public void SetEvents()
        {
            frmcontactInformation.buttonClose.Click += buttonClose_Click;
            frmcontactInformation.buttonSave.Click += buttonSave_Click;
            frmcontactInformation.buttonEdit.Click += buttonEdit_Click;
            frmcontactselector.dataGridViewContactlist.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(datadataGridViewContactlist_CellContentDoubleClick);
            frmcontactselector.buttonInsert.Click += frmcontactselectorButtonInsert_Click;
            frmcontactselector.buttonCancel.Click += frmcontactselectorButtonCancel_Click;
        }

        private void frmcontactselectorButtonInsert_Click(object sender, EventArgs e)
        {
            AddContact(CurrentCustno);
        }

        private void frmcontactselectorButtonCancel_Click(object sender, EventArgs e)
        {
            frmcontactselector.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            RefreshfrmContactMaintenanceControls("Edit");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (frmcontactInformation.buttonSave.Enabled == true)
            {
                if (wsgUtilities.wsgReply("Abandon Edit"))
                {
                    frmcontactInformation.Close();
                }
            }
            else
            {
                frmcontactInformation.Close();
            }
        }

        public string GetContactName(int idcol)
        {
            string contactname = "";
            customerds.contact.Rows.Clear();
            CommandString = "SELECT * FROM  contact WHERE idcol = @idcol ";
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(customerds, "contact", CommandString, CommandType.Text);
            if (customerds.contact.Rows.Count > 0)
            {
                contactname = customerds.contact[0].contactname;
            }
            else
            {
                contactname = "Unknown";
            }
            return contactname;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            GenerateAppTableRowSave(customerds.contact[0]);
            customerds.contact.Rows.Clear();
            CommandString = "SELECT TOP (1) * FROM  contact ORDER BY idcol DESC ";
            this.ClearParameters();
            this.FillData(customerds, "contact", CommandString, CommandType.Text);
            RefreshfrmContactMaintenanceControls("View");
        }

        public void SetBindings()
        {
            string[] CellCarriers = ConfigurationManager.AppSettings["CELLCARRIERS"].Split(new char[] { ';' });
            for (int i = 0; i < CellCarriers.Length; i++)
            {
                if (CellCarriers[i].IndexOf("@") > 0)
                {
                    CellCarriers[i] = CellCarriers[i].Substring(0, CellCarriers[i].IndexOf("@"));
                }
            }
            frmcontactInformation.listBoxCarrier.DataSource = CellCarriers;
            frmcontactInformation.textBoxContactname.DataBindings.Add("Text", customerds.contact, "contactname");
            frmcontactInformation.textBoxContactPhone.DataBindings.Add("Text", customerds.contact, "contactphone");
            frmcontactInformation.textBoxContactext.DataBindings.Add("Text", customerds.contact, "contactext");
            frmcontactInformation.textBoxContactnotes.DataBindings.Add("Text", customerds.contact, "contactnotes");
            frmcontactInformation.textBoxContactemail.DataBindings.Add("Text", customerds.contact, "contactemail");
            frmcontactInformation.listBoxCarrier.DataBindings.Add("SelectedItem", customerds.contact, "contactcarrier");

            frmcontactselector.dataGridViewContactlist.AutoGenerateColumns = false;
            frmcontactselector.dataGridViewContactlist.DataSource = contactBinding;
            contactBinding.DataSource = customerselectords.contact;
            frmcontactselector.dataGridViewContactlist.RowsDefaultCellStyle.BackColor = Color.LightGray;
            frmcontactselector.dataGridViewContactlist.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        private void datadataGridViewContactlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedContactId = CaptureIdCol(frmcontactselector.dataGridViewContactlist);
            frmcontactselector.Close();
        }

        public void DisableControls(Form form)
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in form.Controls)
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
        }
    }
}