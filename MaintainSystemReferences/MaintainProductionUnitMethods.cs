using CommonAppClasses;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainSystemReferences
{
    public class MaintainProductionUnitMethods : WSGDataAccess
    {
        // Create the customer processing object
        private CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");

        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        public Form menuForm { get; set; }
        public FrmMaintainProductionUnitSchedule parentForm = new FrmMaintainProductionUnitSchedule();
        private BindingSource prodctionunitschedulebinding = new BindingSource();
        public int CurrentCustid = 0;
        public string CurrentState { get; set; }
        private reference refds = new reference();
        private reference refsearchds = new reference();

        public MaintainProductionUnitMethods(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
            SetIdcol(refds.productionunitschedule.idcolColumn);
            SetEvents();
            CurrentState = "Select";
            RefreshControls();
        }

        public void SetBindings()
        {
            foreach (Control d in parentForm.Controls)
            {
                if (d is TextBox)
                {
                    int length = d.Name.ToString().Length;
                    string columnname = d.Name.ToString().Substring(7, length - 7);
                    if (d.Name.ToLower().EndsWith("price"))
                    {
                        SetTextBoxCurrencyBinding((TextBox)d, refds, "productionunitschedule." + columnname);
                    }
                    else
                    {
                        SetTextBoxDecimalBinding((TextBox)d, refds, "productionunitschedule." + columnname);
                    }
                }
            }
        }

        public void SetEvents()
        {
            parentForm.dataGridViewProductionUnitSchedule.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridViewProductionUnitSchedule_CellContentDoubleClick);
            parentForm.dataGridViewProductionUnitSchedule.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridViewProductionUnitSchedule_KeyDown);
            parentForm.Shown += new System.EventHandler(parentform_Shown);
            parentForm.buttonClose.Click += new System.EventHandler(buttonClose_Click);
            parentForm.buttonInsert.Click += new System.EventHandler(buttonInsert_Click);
            parentForm.buttonSave.Click += new System.EventHandler(buttonSave_Click);
            parentForm.buttonEdit.Click += new System.EventHandler(SetEditState);
            parentForm.buttonDelete.Click += new System.EventHandler(ProcessDelete);
            parentForm.buttonCancel.Click += new System.EventHandler(CancelProcess);
        }

        public void ShowParent()
        {
            CurrentState = "Select";
            parentForm.dataGridViewProductionUnitSchedule.AutoGenerateColumns = false;
            prodctionunitschedulebinding.DataSource = refsearchds.productionunitschedule;
            parentForm.dataGridViewProductionUnitSchedule.DataSource = prodctionunitschedulebinding;
            parentForm.dataGridViewProductionUnitSchedule.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentForm.dataGridViewProductionUnitSchedule.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            SetBindings();
            RefreshControls();
            parentForm.MdiParent = menuForm;
            parentForm.Show();
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
                        UnlockTableRow(refds.productionunitschedule[0].idcol, "productionunitschedule");
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

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            refds.productionunitschedule.Rows.Clear();
            EstablishBlankDataTableRow(refds.productionunitschedule);
            CurrentState = "Insert";
            RefreshControls();
        }

        public void SetEditState(object sender, EventArgs e)
        {
            if (LockTableRow(refds.productionunitschedule[0].idcol, "productionunitschedule") == "OK")
            {
                CurrentState = "Edit";
                RefreshControls();
            }
        }

        public void CancelProcess(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Abandon Edit") == true)
            {
                if (CurrentState != "Insert")
                {
                    UnlockTableRow(refds.productionunitschedule[0].idcol, "emailaddress");
                }
                refds.productionunitschedule.Rows.Clear();
                CurrentState = "Search";
                RefreshControls();
            }
        }

        public void ProcessDelete(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this item") == true)
            {
                string commandtext = "DELETE FROM productionunitschedule WHERE idcol = @idcol";
                ClearParameters();
                this.AddParms("@idcol", refds.productionunitschedule[0].idcol, "SQL");
                ExecuteCommand(commandtext, CommandType.Text);
                wsgUtilities.wsgNotice("Deletion Complete");
                refds.productionunitschedule.Rows.Clear();
                FillDataGrid();
            }
        }

        private void dataGridViewProductionUnitSchedule_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int productionunitscheduleid = CaptureIdCol(parentForm.dataGridViewProductionUnitSchedule);
            GetSingleProductionUnitSchedule(productionunitscheduleid);
            SetBindings();
            CurrentState = "View";
            RefreshControls();
        }

        private void dataGridViewProductionUnitSchedule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                int productionunitscheduleid = CaptureIdCol(parentForm.dataGridViewProductionUnitSchedule);
                GetSingleProductionUnitSchedule(productionunitscheduleid);
                SetBindings();
                CurrentState = "View";
                RefreshControls();
            }
        }

        private void parentform_Shown(object sender, EventArgs e)
        {
            refds.productionunitschedule.Rows.Clear();
            refds.productionunitschedule.AcceptChanges();
            FillDataGrid();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            refds.productionunitschedule[0].effectivedate = parentForm.dateTimePickerEffectiveDate.Value;
            GenerateAppTableRowSave(refds.productionunitschedule[0]);
            refds.productionunitschedule.Rows.Clear();
            FillDataGrid();
            CurrentState = "Select";
            RefreshControls();
        }

        public void RefreshControls()
        {
            // Loop thru all the controls text boxes,  buttons and datagrid
            foreach (Control c in parentForm.Controls)
            {
                if (c is TextBox || c is Button || c is DataGridView || c is DateTimePicker)
                {
                    c.Enabled = false;
                }
            }
            switch (CurrentState)
            {
                case "Select":
                    {
                        parentForm.dataGridViewProductionUnitSchedule.Enabled = true;
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
                        foreach (Control c in parentForm.Controls)
                        {
                            if (c is TextBox || c is DateTimePicker)
                            {
                                c.Enabled = true;
                            }
                        }
                        parentForm.buttonSave.Enabled = true;
                        parentForm.buttonCancel.Enabled = true;
                        break;
                    }
                case "Insert":
                    {
                        foreach (Control c in parentForm.Controls)
                        {
                            if (c is TextBox || c is DateTimePicker)
                            {
                                c.Enabled = true;
                            }
                        }
                        parentForm.buttonSave.Enabled = true;
                        parentForm.buttonCancel.Enabled = true;
                        break;
                    }
            }
            parentForm.buttonClose.Enabled = true;
            parentForm.Update();
        }

        public void FillDataGrid()
        {
            refsearchds.productionunitschedule.Rows.Clear();
            string commandtext = "SELECT * FROM productionunitschedule ORDER BY effectivedate";
            ClearParameters();
            FillData(refsearchds, "productionunitschedule", commandtext, CommandType.Text);
        }

        public void GetSingleProductionUnitSchedule(int idcol)
        {
            refds.productionunitschedule.Rows.Clear();
            string commandtext = "SELECT * FROM productionunitschedule WHERE idcol = @idcol";
            ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            FillData(refds, "productionunitschedule", commandtext, CommandType.Text);
            parentForm.dateTimePickerEffectiveDate.Value = refds.productionunitschedule[0].effectivedate;
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

        private void SetTextBoxDecimalBinding(TextBox txtbox, DataSet ds, string fieldname)
        {
            Binding b = new Binding("Text", ds, fieldname);
            {
                b.Format += new ConvertEventHandler(DecimalToDecimalString);
                b.Parse += new ConvertEventHandler(DecimalStringToDecimal);
            }
            txtbox.DataBindings.Clear();
            txtbox.DataBindings.Add(b);
        }

        private void DecimalToCurrencyString(object sender, ConvertEventArgs cevent)
        {
            // The method converts only to string type. Test this using the DesiredType.
            if (cevent.DesiredType != typeof(string)) return;

            // Use the ToString method to format the value as currency ("c").
            cevent.Value = ((decimal)cevent.Value).ToString("N0");
        }

        private void CurrencyStringToDecimal(object sender, ConvertEventArgs cevent)
        {
            // The method converts back to decimal type only.
            if (cevent.DesiredType != typeof(decimal)) return;

            // Converts the string back to decimal using the static Parse method.
            cevent.Value = Decimal.Parse(cevent.Value.ToString(),
            NumberStyles.Currency, null);
        }

        private void DecimalToDecimalString(object sender, ConvertEventArgs cevent)
        {
            // The method converts only to string type. Test this using the DesiredType.
            if (cevent.DesiredType != typeof(string)) return;

            // Use the ToString method to format the value as currency ("c").
            cevent.Value = ((decimal)cevent.Value).ToString("N3");
        }

        private void DecimalStringToDecimal(object sender, ConvertEventArgs cevent)
        {
            // The method converts back to decimal type only.
            if (cevent.DesiredType != typeof(decimal)) return;

            // Converts the string back to decimal using the static Parse method.
            cevent.Value = Decimal.Parse(cevent.Value.ToString(),
            NumberStyles.Float, null);
        }
    }
}