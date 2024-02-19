using CommonAppClasses;
using System;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CustomerMaintenance
{
    public partial class FrmMaintainShipTo : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Customer Information");

        // Create the customer processing object
        private CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");

        private BindingSource bindingShipToData = new BindingSource();
        public int CustId { get; set; }
        public string Custno { get; set; }
        public string CurrentState { get; set; }
        public int SelectedShipToId { get; set; }
        public bool InsertingShipTo { get; set; }

        public FrmMaintainShipTo()
        {
            InitializeComponent();
            dataGridViewShipToAddresses.AutoGenerateColumns = false;
            dataGridViewShipToAddresses.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewShipToAddresses.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        private void FrmMaintainShipTo_Load(object sender, EventArgs e)
        {
        }

        private void SetBindings()
        {
            foreach (Control d in this.Controls)
                if (d is TextBox)
                {
                    d.DataBindings.Clear();
                    int length = d.Name.ToString().Length;
                    string columnname = d.Name.ToString().Substring(7, length - 7);
                    d.DataBindings.Add(new Binding("Text", customerAccess.ards, "aracadr." + columnname));
                }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            customerAccess.SaveShipToData(SelectedShipToId, CurrentState, Custno);
            // Refresh the grid source data table
            customerAccess.getCustomerShipToData(CustId);
            bindingShipToData.DataSource = customerAccess.ards.view_customershiptolist;
            dataGridViewShipToAddresses.DataSource = bindingShipToData;
            customerAccess.ClearShipToData(CustId, Custno);
            SetBindings();
            CurrentState = "Select";
            RefreshControls();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = customerAccess.LockShipTo(SelectedShipToId);
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

        private void FrmMaintainShipTo_Shown(object sender, EventArgs e)
        {
            customerAccess.getCustomerShipToData(CustId);
            bindingShipToData.DataSource = customerAccess.ards.view_customershiptolist;
            dataGridViewShipToAddresses.DataSource = bindingShipToData;
            dataGridViewShipToAddresses.Focus();
            RefreshControls();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            customerAccess.ClearShipToData(CustId, Custno);
            SetBindings();
            CurrentState = "Insert";
            RefreshControls();
        }

        #region refresh controls

        private void RefreshControls()

        {
            switch (CurrentState)
            {
                case "View":
                    {
                        // Loop thru all the controls on each tab page and disable text boxes and  buttons
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Enabled = false;
                                continue;
                            }
                        }
                        dataGridViewShipToAddresses.Enabled = true;
                        textBoxCompany.Focus();
                        buttonClear.Enabled = false;
                        buttonEdit.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonSave.Enabled = false;
                        break;
                    }

                case "ViewOnly":
                    {
                        // Loop thru all the controls on each tab page and disable text boxes and  buttons
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Enabled = false;
                                continue;
                            }
                        }
                        dataGridViewShipToAddresses.Enabled = true;
                        textBoxCompany.Focus();
                        buttonClear.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonInsert.Enabled = false;
                        buttonSave.Enabled = false;
                        break;
                    }

                case "Select":
                    {
                        // Loop thru all the controls on each tab page and disable text boxes and  buttons
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Enabled = false;
                                continue;
                            }
                        }
                        dataGridViewShipToAddresses.Enabled = true;
                        buttonClear.Enabled = false;
                        buttonInsert.Enabled = true;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = false;
                        break;
                    }

                case "Edit":
                    {
                        // Loop thru all the controls on each tab page and disable text boxes and  buttons
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Enabled = true;
                                continue;
                            }
                        }
                        dataGridViewShipToAddresses.Enabled = false;
                        textBoxCompany.Focus();
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;
                        textBoxCshipno.Enabled = false;

                        break;
                    }
                case "Insert":
                    {
                        // Loop thru all the controls on each tab page and enable text boxes and  buttons
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Enabled = true;
                                continue;
                            }
                        }
                        dataGridViewShipToAddresses.Enabled = false;
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;
                        break;
                    }
            }
            this.Update();
        }

        #endregion refresh controls

        private void dataGridViewShipToAddresses_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SelectedShipToId = customerAccess.CaptureIdCol(dataGridViewShipToAddresses);
                customerAccess.getSingleShipToData(SelectedShipToId);
                SetBindings();
                CurrentState = "View";
                RefreshControls();
            }
        }

        private void dataGridViewShipToAddresses_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedShipToId = customerAccess.CaptureIdCol(dataGridViewShipToAddresses);
            customerAccess.getSingleShipToData(SelectedShipToId);
            SetBindings();
            CurrentState = "View";
            RefreshControls();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    // Unlock ShipTo table
                    if (CurrentState == "Edit")
                    {
                        customerAccess.UnlockShipTo(SelectedShipToId);
                    }
                    customerAccess.ClearShipToData(CustId, Custno);
                    SetBindings();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                customerAccess.ClearShipToData(CustId, Custno);
                CurrentState = "Select";
                RefreshControls();
            }
        }
    }
}