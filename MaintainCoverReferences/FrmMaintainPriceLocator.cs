using CommonAppClasses;
using System;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainCoverReferences
{
    //CACHED  Maintain -> Price Locator
    public partial class FrmMaintainPriceLocator : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Spacing Information");

        // Create the price processing object
        private PriceMaintenance priceMaintenance = new PriceMaintenance("SQL", "SQLConnString");

        // Create the item access object
        private ReferenceMaintenance refdata = new ReferenceMaintenance("SQL", "SQLConnString");

        private ImmasterAccess immasterAccess = new ImmasterAccess("SQL", "SQLConnString");
        private NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        private static BindingSource bindingPriceLocatorData = new BindingSource();
        private static ObjectCache dataCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_PriceLocator"]));

        public int SelectPrsLocatorId { get; set; }
        public string CurrentItem { get; set; }
        public int CurrentSpacingId { get; set; }
        public int CurrentPrsHeadId { get; set; }
        public string CurrentState { get; set; }

        public FrmMaintainPriceLocator()
        {
            InitializeComponent();
            CurrentSpacingId = 0;
            CurrentItem = "";
            CurrentPrsHeadId = 0;
            SelectPrsLocatorId = 0;
            dataGridViewPriceLocator.AutoGenerateColumns = false;
            dataGridViewPriceLocator.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewPriceLocator.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            if (dataCache.IsInvalid)
            {
                priceMaintenance.GetPriceLocatorData();
                bindingPriceLocatorData.DataSource = priceMaintenance.prds.view_prslocatordata;
                dataCache.Refresh(bindingPriceLocatorData);
            }

            dataGridViewPriceLocator.DataSource = bindingPriceLocatorData;
            dataGridViewPriceLocator.Focus();
            CurrentState = "Select";
            RefreshLabels();
            RefreshControls();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewPriceLocator_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureQuPrsLocatorData();
        }

        private void dataGridViewPriceLocator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureQuPrsLocatorData();
            }
        }

        private void CaptureQuPrsLocatorData()
        {
            SelectPrsLocatorId = priceMaintenance.CaptureIdCol(dataGridViewPriceLocator);
            priceMaintenance.getSinglePriceLocatorData(SelectPrsLocatorId);
            CurrentItem = priceMaintenance.prds.quprslocator[0].item;
            CurrentPrsHeadId = priceMaintenance.prds.quprslocator[0].pschedid;
            CurrentSpacingId = priceMaintenance.prds.quprslocator[0].spacingid;
            SetBindings();
            CurrentState = "View";
            RefreshLabels();
            RefreshControls();
        }

        private void RefreshLabels()
        {
            // item label
            if (CurrentItem != "")
            {
                // Locate data for that item
                immasterAccess.getSingleImmasterData(CurrentItem);
                labelItem.Text = immasterAccess.itemds.immaster[0].item;
            }
            else
            {
                labelItem.Text = "Unspecified";
            }
            // Spacing Label
            if (CurrentSpacingId != 0)
            {
                // Locate data for that Spacing
                refdata.getSingleSpacingData(CurrentSpacingId);
                labelSpacing.Text = refdata.referenceds.quspacing[0].descrip.TrimEnd();
            }
            else
            {
                labelSpacing.Text = "Unspecified";
            }

            if (CurrentPrsHeadId != 0)
            {
                // Locate data for that Schedule
                priceMaintenance.getSingleQuprsheadData(CurrentPrsHeadId);
                labelPrssched.Text = priceMaintenance.prds.quprshead[0].descrip.TrimEnd();
            }
            else
            {
                labelPrssched.Text = "Unspecified";
            }
        }

        private void buttonItem_Click(object sender, EventArgs e)
        {
            // Show the item selector screen
            FrmGetImmaster frmGetImmaster = new FrmGetImmaster();
            frmGetImmaster.SelectedCode = "CU";
            frmGetImmaster.ShowDialog();
            CurrentItem = frmGetImmaster.SelectedItem;
            if (CurrentItem != "")
            {
                // Locate data for that item
                immasterAccess.getSingleImmasterData(CurrentItem);
                labelItem.Text = immasterAccess.itemds.immaster[0].item;
                priceMaintenance.prds.quprslocator[0].item = CurrentItem;
            }
        }

        private void buttonSpacing_Click(object sender, EventArgs e)
        {
            // Show the item spacing screen
            FrmGetSpacing frmGetSpacing = new FrmGetSpacing();
            frmGetSpacing.ShowDialog();
            CurrentSpacingId = frmGetSpacing.SelectedSpacingId;
            if (CurrentSpacingId != 0)
            {
                // Locate data for that Spacing
                refdata.getSingleSpacingData(CurrentSpacingId);
                labelSpacing.Text = refdata.referenceds.quspacing[0].descrip.TrimEnd();
                priceMaintenance.prds.quprslocator[0].spacingid = CurrentSpacingId;
            }
        }

        private void buttonPrsschd_Click(object sender, EventArgs e)
        {
            // Show the item spacing screen
            FrmGetPrshead frmGetPrshead = new FrmGetPrshead();
            frmGetPrshead.ShowDialog();
            CurrentPrsHeadId = frmGetPrshead.SelectedPrsHeadId;
            if (CurrentPrsHeadId != 0)
            {
                // Locate data for that Schedule
                priceMaintenance.getSingleQuprsheadData(CurrentPrsHeadId);
                labelPrssched.Text = priceMaintenance.prds.quprshead[0].descrip.TrimEnd();
                priceMaintenance.prds.quprslocator[0].pschedid = CurrentPrsHeadId;
            }
        }

        private void RefreshControls()
        {
            switch (CurrentState)
            {
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
                        buttonClear.Enabled = true;
                        buttonEdit.Enabled = true;
                        buttonDelete.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonClose.Enabled = true;
                        break;
                    }

                case "ViewOnly":
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
                        buttonClear.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonDelete.Enabled = false;
                        buttonInsert.Enabled = false;
                        buttonSave.Enabled = false;
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
                        labelItem.Text = "Unspecified";
                        labelPrssched.Text = "Unspecified";
                        labelSpacing.Text = "Unspecified";
                        buttonClear.Enabled = false;
                        buttonDelete.Enabled = false;
                        buttonInsert.Enabled = true;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = false;
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
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonDelete.Enabled = false;
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
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonDelete.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;
                        buttonClose.Enabled = true;
                        break;
                    }
            }
            this.Update();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = priceMaintenance.LockQuPrsLocator(SelectPrsLocatorId);
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (priceMaintenance.ValidatePriceLocatorData() == true)
            {
                priceMaintenance.SavePriceLocatorData(SelectPrsLocatorId, CurrentState);
                priceMaintenance.ClearQuprsLocatorData();
                SetBindings();
                CurrentState = "Select";
                // Refresh the grid source data table
                priceMaintenance.ClearQuprsLocatorData();
                priceMaintenance.GetPriceLocatorData();
                bindingPriceLocatorData.DataSource = priceMaintenance.prds.view_prslocatordata;
                dataGridViewPriceLocator.DataSource = bindingPriceLocatorData;
                RefreshControls();
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            priceMaintenance.EstablishBlankPriceLocatorData();
            SetBindings();
            CurrentState = "Insert";
            RefreshControls();
        }

        public void SetBindings()
        {
            textBoxPrcfact.DataBindings.Clear();
            textBoxPrcfact.DataBindings.Add(new Binding("Text", priceMaintenance.prds, "quprslocator.prcfact"));
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState == "Edit")
                    {
                        priceMaintenance.UnlockQuprsLocator(SelectPrsLocatorId);
                    }
                    priceMaintenance.ClearQuprsLocatorData();
                    SetBindings();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                priceMaintenance.ClearQuprsLocatorData();
                SetBindings();
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this item?"))
            {
                priceMaintenance.DeleteQuprsLocatorData(SelectPrsLocatorId);
                wsgUtilities.wsgNotice("The item has been deleted.");
                priceMaintenance.ClearQuprsLocatorData();
                SetBindings();
                CurrentState = "Select";
                // Refresh the grid source data table
                priceMaintenance.ClearQuprsLocatorData();
                priceMaintenance.GetPriceLocatorData();
                bindingPriceLocatorData.DataSource = priceMaintenance.prds.view_prslocatordata;
                dataGridViewPriceLocator.DataSource = bindingPriceLocatorData;
                RefreshControls();
            }
        }
    }
}