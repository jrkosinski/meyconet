using CommonAppClasses;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CustomerMaintenance
{
    public class EmailAddressMethods : WSGDataAccess
    {
        private customer custds = new customer();
        private customer custsearchds = new customer();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        public FrmMaintainEmailAddress parentForm = new FrmMaintainEmailAddress();
        public string CurrentState = "";
        private BindingSource emailbinding = new BindingSource();
        public string addresstype = "";
        public string custno = "";

        public EmailAddressMethods(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
            SetEvents();
            SetIdcol(custds.emailaddress.idcolColumn);
        }

        public void ShowParent()
        {
            CurrentState = "Select";
            parentForm.dataGridViewEmailAddresses.AutoGenerateColumns = false;
            emailbinding.DataSource = custsearchds.emailaddress;
            parentForm.dataGridViewEmailAddresses.DataSource = emailbinding;
            parentForm.dataGridViewEmailAddresses.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentForm.dataGridViewEmailAddresses.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            SetBindings();
            RefreshControls();

            switch (addresstype)
            {
                case "O":
                    {
                        parentForm.Text = "Order Email Addresses";
                        break;
                    }
                case "Q":
                    {
                        parentForm.Text = "Quote Email Addresses";
                        break;
                    }
                default:
                    {
                        parentForm.Text = "Invoice Email Addresses";
                        break;
                    }
            }
            parentForm.ShowDialog();
        }

        public void FillDataGrid()
        {
            custsearchds.emailaddress.Rows.Clear();
            string commandtext = "SELECT * FROM emailaddress WHERE custno = @custno AND addresstype = @addresstype";
            ClearParameters();
            this.AddParms("@custno", custno, "SQL");
            this.AddParms("@addresstype", addresstype, "SQL");
            FillData(custsearchds, "emailaddress", commandtext, CommandType.Text);
        }

        public void SetBindings()
        {
            parentForm.textBoxEmailAddress.DataBindings.Clear();
            parentForm.textBoxEmailAddress.DataBindings.Add("Text", custds.emailaddress, "emailaddress");
        }

        public void SetEvents()
        {
            parentForm.dataGridViewEmailAddresses.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(DataGridViewEmailAddresses_CellContentDoubleClick);
            parentForm.dataGridViewEmailAddresses.KeyDown += new System.Windows.Forms.KeyEventHandler(DataGridViewEmailAddresses_KeyDown);
            parentForm.Shown += new System.EventHandler(parentform_Shown);
            parentForm.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            parentForm.buttonInsert.Click += new System.EventHandler(buttonInsert_Click);
            parentForm.buttonSave.Click += new System.EventHandler(buttonSave_Click);
            parentForm.buttonEdit.Click += new System.EventHandler(SetEditState);
            parentForm.buttonDelete.Click += new System.EventHandler(ProcessDelete);
            parentForm.buttonCancel.Click += new System.EventHandler(CancelProcess);
        }

        public void GetSingleEmailAddress(int idcol)
        {
            custds.emailaddress.Rows.Clear();
            string commandtext = "SELECT * FROM emailaddress WHERE idcol = @idcol";
            ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            FillData(custds, "emailaddress", commandtext, CommandType.Text);
        }

        public void RefreshControls()
        {
            // Loop thru all the controls text boxes,  buttons and datagrid
            foreach (Control c in parentForm.Controls)
            {
                if (c is TextBox || c is Button || c is DataGridView)
                {
                    c.Enabled = false;
                }
            }
            switch (CurrentState)
            {
                case "Select":
                    {
                        parentForm.dataGridViewEmailAddresses.Enabled = true;
                        parentForm.buttonInsert.Enabled = true;
                        break;
                    }
                case "View":
                    {
                        parentForm.buttonEdit.Enabled = true;
                        parentForm.buttonDelete.Enabled = true;
                        break;
                    }
                case "Edit":
                    {
                        parentForm.textBoxEmailAddress.Enabled = true;
                        parentForm.buttonSave.Enabled = true;
                        parentForm.buttonCancel.Enabled = true;
                        break;
                    }
                case "Insert":
                    {
                        parentForm.textBoxEmailAddress.Enabled = true;
                        parentForm.textBoxEmailAddress.Focus();
                        parentForm.buttonSave.Enabled = true;
                        parentForm.buttonCancel.Enabled = true;
                        break;
                    }
            }
            parentForm.buttonClose.Enabled = true;
            parentForm.Update();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            custds.emailaddress.Rows.Clear();
            EstablishBlankDataTableRow(custds.emailaddress);
            custds.emailaddress[0].custno = custno;
            custds.emailaddress[0].addresstype = addresstype;
            CurrentState = "Insert";
            RefreshControls();
        }

        private void parentform_Shown(object sender, EventArgs e)
        {
            custds.emailaddress.Rows.Clear();
            custds.emailaddress.AcceptChanges();
            FillDataGrid();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            GenerateAppTableRowSave(custds.emailaddress[0]);
            custds.emailaddress.Rows.Clear();
            FillDataGrid();
            CurrentState = "Select";
            RefreshControls();
        }

        public void SetEditState(object sender, EventArgs e)
        {
            if (LockTableRow(custds.emailaddress[0].idcol, "emailaddress") == "OK")
            {
                CurrentState = "Edit";
                RefreshControls();
            }
        }

        private void DataGridViewEmailAddresses_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int addressid = CaptureIdCol(parentForm.dataGridViewEmailAddresses);
            GetSingleEmailAddress(addressid);
            SetBindings();
            CurrentState = "View";
            RefreshControls();
        }

        private void DataGridViewEmailAddresses_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                int addressid = CaptureIdCol(parentForm.dataGridViewEmailAddresses);
                GetSingleEmailAddress(addressid);
                SetBindings();
                CurrentState = "View";
                RefreshControls();
            }
        }

        public void CancelProcess(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Abandon Edit") == true)
            {
                if (CurrentState != "Insert")
                {
                    UnlockTableRow(custds.emailaddress[0].idcol, "emailaddress");
                }
                custds.emailaddress.Rows.Clear();
                CurrentState = "Search";
                RefreshControls();
            }
        }

        public void ProcessDelete(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this item") == true)
            {
                string commandtext = "DELETE FROM emailaddress WHERE idcol = @idcol";
                ClearParameters();
                this.AddParms("@idcol", custds.emailaddress[0].idcol, "SQL");
                ExecuteCommand(commandtext, CommandType.Text);
                wsgUtilities.wsgNotice("Deletion Complete");
                custds.emailaddress.Rows.Clear();
                FillDataGrid();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    // Unlock Customer table
                    if (CurrentState == "Edit")
                    {
                        UnlockTableRow(custds.emailaddress[0].idcol, "emailaddress");
                    }
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
                parentForm.Close();
            }
        }
    }
}