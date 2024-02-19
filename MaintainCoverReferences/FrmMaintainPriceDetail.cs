using CommonAppClasses;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainCoverReferences
{
    public partial class FrmMaintainPriceDetail : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Pricing Information");

        // Create the price processing object
        private PriceMaintenance priceMaintenance = new PriceMaintenance("SQL", "SQLConnString");

        private BindingSource bindingPrsHeadLocatorData = new BindingSource();
        private BindingSource bindingPriceDetailLocatorData = new BindingSource();
        public int SelectedPriceHeadId { get; set; }
        public int SelectedPriceDetailId { get; set; }
        public string CurrentState { get; set; }

        public FrmMaintainPriceDetail()
        {
            InitializeComponent();
            SelectedPriceHeadId = 0;
            // Price Schedule Detail Grid
            dataGridViewQuprsdetail.AutoGenerateColumns = false;
            dataGridViewQuprsdetail.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewQuprsdetail.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            // Price Schedule Head Grid
            dataGridViewPrsHeadLocator.AutoGenerateColumns = false;
            dataGridViewPrsHeadLocator.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewPrsHeadLocator.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            priceMaintenance.GetQuprsheadData();
            bindingPrsHeadLocatorData.DataSource = priceMaintenance.prds.view_quprsheaddata;
            dataGridViewPrsHeadLocator.DataSource = bindingPrsHeadLocatorData;
            dataGridViewPrsHeadLocator.Focus();

            CurrentState = "SelectSchedule";
            RefreshControls();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void dataGridViewPrsHeadLocator_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CapturePriceScheduledHeadData();
        }

        private void dataGridViewPrsHeadLocator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CapturePriceScheduledHeadData();
            }
        }

        public void CapturePriceScheduledHeadData()
        {
            SelectedPriceHeadId = priceMaintenance.CaptureIdCol(dataGridViewPrsHeadLocator);
            priceMaintenance.GetPriceScheduleDetailData(SelectedPriceHeadId);
            bindingPriceDetailLocatorData.DataSource = priceMaintenance.prds.view_quprsdetaildata;
            dataGridViewQuprsdetail.DataSource = bindingPriceDetailLocatorData;
            CurrentState = "Select";
            RefreshControls();
            dataGridViewQuprsdetail.Focus();
        }

        private void SetBindings()
        {
            foreach (Control d in this.Controls)
                if (d is TextBox)
                {
                    d.DataBindings.Clear();

                    int length = d.Name.ToString().Length;
                    string columnname = d.Name.ToString().Substring(7, length - 7);
                    Binding b = new Binding("Text", priceMaintenance.prds, "quprsdetail." + columnname);
                    if (columnname.ToUpper() != "SQFT")
                    {
                        b.Format += new ConvertEventHandler(DecimalToCurrencyString);
                        b.Parse += new ConvertEventHandler(CurrencyStringToDecimal);
                    }
                    d.DataBindings.Add(b);
                }
        }

        private void RefreshControls()
        {
            switch (CurrentState)
            {
                case "SelectSchedule":

                    {
                        // Loop thru all the controls on each tab page and disable text boxes and  buttons
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox || c is Button)
                            {
                                c.Enabled = false;
                                continue;
                            }
                        }
                        buttonClose.Enabled = true;
                        dataGridViewPrsHeadLocator.Enabled = true;
                        dataGridViewQuprsdetail.Enabled = false;
                        break;
                    }

                case "View":
                    {
                        // Loop thru all the controls on each tab page and disable text boxes and  buttons
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox || c is Button)
                            {
                                c.Enabled = false;
                                continue;
                            }
                        }

                        dataGridViewPrsHeadLocator.Enabled = false;
                        buttonClear.Enabled = true;
                        buttonEdit.Enabled = true;
                        buttonClose.Enabled = true;
                        break;
                    }

                case "Select":
                    {
                        // Loop thru all the controls and disable text boxes
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox || c is Button)
                            {
                                c.Enabled = false;
                                continue;
                            }
                        }
                        dataGridViewPrsHeadLocator.Enabled = true;
                        dataGridViewQuprsdetail.Enabled = true;
                        buttonInsert.Enabled = true;
                        buttonClose.Enabled = true;
                        break;
                    }

                case "Edit":
                    {
                        // Loop thru all the controls  disable text boxes
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox || c is Button)
                            {
                                c.Enabled = true;
                                continue;
                            }
                        }
                        dataGridViewQuprsdetail.Enabled = false;
                        dataGridViewPrsHeadLocator.Enabled = false;
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;
                        buttonClose.Enabled = true;
                        break;
                    }
                case "Insert":
                    {
                        // Loop thru all the controls and enable text boxes
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox || c is Button)
                            {
                                c.Enabled = true;
                                continue;
                            }
                        }
                        dataGridViewPrsHeadLocator.Enabled = false;
                        dataGridViewQuprsdetail.Enabled = false;
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;
                        buttonClose.Enabled = true;

                        break;
                    }
            }
            this.Update();
        }

        private void dataGridViewQuprsdetail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedPriceDetailId = priceMaintenance.CaptureIdCol(dataGridViewQuprsdetail);
            priceMaintenance.getSinglePriceScheduleDetailData(SelectedPriceDetailId);
            SetBindings();
            CurrentState = "View";
            RefreshControls();
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    // Unlock ShipTo table
                    if (CurrentState == "Edit")
                    {
                        //         customerAccess.UnlockShipTo(SelectedShipToId);
                    }
                    priceMaintenance.ClearPriceDetailData();
                    SetBindings();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                priceMaintenance.ClearPriceDetailData();
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = priceMaintenance.LockPriceDetail(SelectedPriceDetailId);
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

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            priceMaintenance.EstablishBlankPriceDetailData(SelectedPriceHeadId);
            SetBindings();
            CurrentState = "Insert";
            RefreshControls();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            priceMaintenance.SavePriceDetailData(SelectedPriceDetailId, SelectedPriceHeadId, CurrentState);
            priceMaintenance.ClearPriceDetailData();
            SetBindings();
            CurrentState = "Select";
            // Refresh the grid source data table
            priceMaintenance.GetPriceScheduleDetailData(SelectedPriceHeadId);
            bindingPriceDetailLocatorData.DataSource = priceMaintenance.prds.view_quprsdetaildata;
            dataGridViewQuprsdetail.DataSource = bindingPriceDetailLocatorData;
            CurrentState = "Select";
            RefreshControls();
            dataGridViewQuprsdetail.Focus();
        }

        private void FrmMaintainPriceDetail_Load(object sender, EventArgs e)
        {
        }
    }
}