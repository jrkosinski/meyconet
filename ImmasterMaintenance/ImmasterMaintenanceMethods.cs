using CommonAppClasses;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace ImmasterMaintenance
{
    public class ImmasterMaintenanceMethods : WSGDataAccess
    {
        private alereds AlereDs = new alereds();
        private alereds AlereTestDs = new alereds();
        private AlereDataMethods alereDataMethods = new AlereDataMethods();
        private reference ReferenceDs = new reference();
        private reference TempReferenceDs = new reference();
        private string CurrentState = "";
        //private string CurrentItem = "";
        private string CommandString = "";
        public Form menuform = new Form();
        private WSGUtilities wsgUtilities = new WSGUtilities("Item Maintenance");
        private FrmGetImmaster frmGetImmaster = new FrmGetImmaster();
        private FrmImmasterMaintenance parentForm = new FrmImmasterMaintenance();

        public ImmasterMaintenanceMethods()
            : base("SQL", "SQLConnString")
        {
            SetEvents();
            SetBindings();
            CurrentState = "Select";
            RefreshControls();
        }

        public void SetEvents()
        {
            parentForm.buttonSelect.Click += new System.EventHandler(ButtonSelect_click);
            parentForm.buttonClose.Click += new System.EventHandler(ButtonClose_click);
            parentForm.buttonEdit.Click += new System.EventHandler(ButtonEdit_click);
            parentForm.buttonCancel.Click += new System.EventHandler(ButtonCancel_click);
            parentForm.buttonSave.Click += new System.EventHandler(ButtonSave_click);
        }

        public void SetBindings()
        {
            parentForm.textBoxItem.DataBindings.Clear();
            parentForm.textBoxItem.DataBindings.Add("Text", AlereDs.immaster, "item");
            parentForm.textBoxDescrip.DataBindings.Clear();
            parentForm.textBoxDescrip.DataBindings.Add("Text", AlereDs.immaster, "descrip");
            parentForm.textBoxPrcdesc.DataBindings.Add("Text", AlereDs.immaster, "prcdesc");
            // Pool Dimensions
            SetTextBoxDollarsBinding(parentForm.textBoxPwidthft, AlereDs, "immaster.pwidthft");
            SetTextBoxDollarsBinding(parentForm.textBoxPwidthin, AlereDs, "immaster.pwidthin");
            SetTextBoxDollarsBinding(parentForm.textBoxPlenft, AlereDs, "immaster.plenft");
            SetTextBoxDollarsBinding(parentForm.textBoxPlenin, AlereDs, "immaster.plenin");
            // Pool Extension
            SetTextBoxDollarsBinding(parentForm.textBoxPextwidft, AlereDs, "immaster.pextwidft");
            SetTextBoxDollarsBinding(parentForm.textBoxPextwidin, AlereDs, "immaster.pextwidin");
            SetTextBoxDollarsBinding(parentForm.textBoxPextlenft, AlereDs, "immaster.pextlenft");
            SetTextBoxDollarsBinding(parentForm.textBoxPextlenin, AlereDs, "immaster.pextlenin");

            // Cover Dimensions
            SetTextBoxDollarsBinding(parentForm.textBoxCwidthft, AlereDs, "immaster.cwidthft");
            SetTextBoxDollarsBinding(parentForm.textBoxCwidthin, AlereDs, "immaster.cwidthin");
            SetTextBoxDollarsBinding(parentForm.textBoxClenft, AlereDs, "immaster.clenft");
            SetTextBoxDollarsBinding(parentForm.textBoxClenin, AlereDs, "immaster.clenin");

            //Cover Extension
            SetTextBoxDollarsBinding(parentForm.textBoxEwidthft, AlereDs, "immaster.ewidthft");
            SetTextBoxDollarsBinding(parentForm.textBoxEwidthin, AlereDs, "immaster.ewidthin");
            SetTextBoxDollarsBinding(parentForm.textBoxElenft, AlereDs, "immaster.elenft");
            SetTextBoxDollarsBinding(parentForm.textBoxElenin, AlereDs, "immaster.elenin");

            parentForm.checkBoxAppldisc.DataBindings.Add("Checked", AlereDs.immaster, "appldisc");

            SetTextBoxCurrencyBinding(parentForm.textBoxPrwcov, AlereDs, "immaster.prwcov");
            parentForm.textBoxCustplan.DataBindings.Clear();
            parentForm.textBoxCustplan.DataBindings.Add("Text", AlereDs.immaster, "custplan");
            parentForm.textBoxSpechard.DataBindings.Clear();
            parentForm.textBoxSpechard.DataBindings.Add("Text", AlereDs.immaster, "spechard");
            parentForm.textBoxPrccom.DataBindings.Clear();
            parentForm.textBoxPrccom.DataBindings.Add("Text", AlereDs.immaster, "prccom");
            parentForm.textBoxStrap.DataBindings.Clear();
            parentForm.textBoxStrap.DataBindings.Add("Text", AlereDs.immaster, "straps");

            // Combox boxes

            parentForm.comboBoxColor.ValueMember = "code";
            parentForm.comboBoxColor.DisplayMember = "descrip";
            parentForm.comboBoxColor.DataSource = ReferenceDs.view_qucolordata;

            parentForm.comboBoxMaterial.ValueMember = "code";
            parentForm.comboBoxMaterial.DisplayMember = "descrip";
            parentForm.comboBoxMaterial.DataSource = ReferenceDs.view_qumaterialdata;
        }

        public void RefreshControls()
        {
            DisableControls();
            parentForm.buttonClose.Enabled = true;
            switch (CurrentState)
            {
                case "Select":
                    {
                        parentForm.buttonSelect.Enabled = true;
                        break;
                    }
                case "View":
                    {
                        parentForm.buttonCancel.Enabled = true;
                        parentForm.buttonEdit.Enabled = true;
                        break;
                    }
                case "Edit":
                    {
                        EnableControls();
                        parentForm.buttonEdit.Enabled = false;
                        parentForm.buttonSelect.Enabled = false;
                        break;
                    }
            }
        }

        public void StartApp()
        {
            // Material
            ReferenceDs.view_qumaterialdata.Rows.Clear();
            this.FillData(ReferenceDs, "view_qumaterialdata", "wsgsp_getqumaterialdata", CommandType.StoredProcedure);
            TempReferenceDs.view_qumaterialdata.Rows.Clear();
            TempReferenceDs.view_qumaterialdata.Rows.Add();
            EstablishBlankDataTableRow(TempReferenceDs.view_qumaterialdata);
            TempReferenceDs.view_qumaterialdata[0].code = "None";
            TempReferenceDs.view_qumaterialdata[0].descrip = "None";
            ReferenceDs.view_qumaterialdata.ImportRow(TempReferenceDs.view_qumaterialdata[0]);
            // Color
            ReferenceDs.view_qucolordata.Rows.Clear();
            this.FillData(ReferenceDs, "view_qucolordata", "wsgsp_getqucolordata", CommandType.StoredProcedure);
            TempReferenceDs.view_qucolordata.Rows.Clear();
            TempReferenceDs.view_qucolordata.Rows.Add();
            EstablishBlankDataTableRow(TempReferenceDs.view_qucolordata);
            TempReferenceDs.view_qucolordata[0].code = "None";
            TempReferenceDs.view_qucolordata[0].descrip = "None";
            ReferenceDs.view_qucolordata.ImportRow(TempReferenceDs.view_qucolordata[0]);

            parentForm.MdiParent = menuform;
            parentForm.Show();
        }

        public void ButtonSelect_click(object sender, EventArgs e)
        {
            frmGetImmaster.ShowDialog();
            if (frmGetImmaster.SelectedItem != "")
            {
                AlereTestDs.immaster.Rows.Clear();
                CommandString = "SELECT  * from immaster WHERE item = @item";
                this.ClearParameters();
                this.AddParms("@item", frmGetImmaster.SelectedItem, "SQL");
                this.FillData(AlereDs, "immaster", CommandString, CommandType.Text);
                CurrentState = "View";

                if (AlereDs.immaster[0].stkstd == "STK")
                {
                    parentForm.radioButtonStock.Checked = true;
                }
                else
                {
                    parentForm.radioButtonStd.Checked = true;
                }
                if (AlereDs.immaster[0].color.TrimEnd() != "")
                {
                    parentForm.comboBoxColor.SelectedValue = AlereDs.immaster[0].color;
                }
                else
                {
                    parentForm.comboBoxColor.SelectedValue = "None";
                }

                if (AlereDs.immaster[0].material.TrimEnd() != "")
                {
                    parentForm.comboBoxMaterial.SelectedValue = AlereDs.immaster[0].material;
                }
                else
                {
                    parentForm.comboBoxMaterial.SelectedValue = "None";
                }

                RefreshControls();
            }
        }

        public void ButtonSave_click(object sender, EventArgs e)
        {
            if (parentForm.radioButtonStock.Checked)
            {
                AlereDs.immaster[0].stkstd = "STK";
            }
            else
            {
                AlereDs.immaster[0].stkstd = "STD";
            }
            if ((string)parentForm.comboBoxColor.SelectedValue != "None")
            {
                AlereDs.immaster[0].color = (string)parentForm.comboBoxColor.SelectedValue;
            }
            else
            {
                AlereDs.immaster[0].color = "";
            }

            if ((string)parentForm.comboBoxMaterial.SelectedValue != "None")
            {
                AlereDs.immaster[0].material = (string)parentForm.comboBoxMaterial.SelectedValue;
            }
            else
            {
                AlereDs.immaster[0].material = "";
            }
            AlereDs.immaster[0].userchg = AppUserClass.AppUserId;
            AlereDs.immaster[0].lastchg = DateTime.Now.Date;
            ClearParameters();
            SetAllParameters(AlereDs.immaster[0]);

            CommandString = "UPDATE immaster SET ";
            CommandString += "prcdesc = @prcdesc, ";
            CommandString += "pwidthft = @pwidthft, ";
            CommandString += "pwidthin = @pwidthin, ";
            CommandString += "plenft = @plenft, ";
            CommandString += "plenin = @plenin, ";
            CommandString += "pextwidft = @pextwidft, ";
            CommandString += "pextwidin = @pextwidin, ";
            CommandString += "pextlenft = @pextlenft, ";
            CommandString += "pextlenin = @pextlenin, ";
            CommandString += "cwidthft = @cwidthft, ";
            CommandString += "cwidthin = @cwidthin, ";
            CommandString += "clenft = @clenft, ";
            CommandString += "clenin = @clenin ,";
            CommandString += "ewidthft = @ewidthft, ";
            CommandString += "ewidthin = @ewidthin, ";
            CommandString += "elenft = @elenft, ";
            CommandString += "elenin = @elenin, ";
            CommandString += "appldisc = @appldisc ,";
            CommandString += "prwcov = @prwcov,";
            CommandString += "custplan = @custplan, ";
            CommandString += "spechard = @spechard, ";
            CommandString += "prccom = @prccom, ";
            CommandString += "material = @material, ";
            CommandString += "lastchg = @lastchg, ";
            CommandString += "userchg = @userchg, ";

            CommandString += "color = @color ";

            CommandString += " WHERE item = @item";

            try
            {
                ExecuteCommand(CommandString, CommandType.Text);
                wsgUtilities.wsgNotice("Update Complete");
                CurrentState = "View";
                RefreshControls();
            }
            catch (Exception ex)
            {
                WSGUtilitieslib.Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                wsgUtilities.wsgNotice("SQL Error " + ex.Message);
            }
        }

        public void ButtonCancel_click(object sender, EventArgs e)
        {
            AlereDs.immaster.Rows.Clear();
            CurrentState = "Select";
            RefreshControls();
        }

        public void ButtonEdit_click(object sender, EventArgs e)
        {
            CurrentState = "Edit";
            RefreshControls();
        }

        public void ButtonClose_click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit")
            {
                if (wsgUtilities.wsgReply("Abandon Edit?"))
                {
                    parentForm.Close();
                }
            }
            else
            {
                parentForm.Close();
            }
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
                b.Parse += new ConvertEventHandler(CurrencyStringToDollars);
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

        public void DisableControls()
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in parentForm.Controls)
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
            parentForm.buttonClose.Enabled = false;
        }

        public void EnableControls()
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in parentForm.Controls)
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
            parentForm.buttonClose.Enabled = true;
        }
    }
}