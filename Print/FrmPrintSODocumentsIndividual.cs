using CommonAppClasses;
using System;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Print
{
    public partial class FrmPrintSODocumentsIndividual : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");

        // Create the App Information processing object
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");

        private Estimating.FrmCoverSODocumentViewer frmCoverSODocumentViewer = new Estimating.FrmCoverSODocumentViewer();

        private MiscellaneousOrderEntry.FrmMiscSODocumentViewer frmMiscSODocumentViewer =
        new MiscellaneousOrderEntry.FrmMiscSODocumentViewer();

        // Create the SO Information processing object
        private Estimating.Soinf soinf = new Estimating.Soinf("SQL", "SQLConnString");

        public FrmPrintSODocumentsIndividual()
        {
            InitializeComponent();
            CurrentState = "Select";
            RefreshControls();
        }

        public string CurrentSono { get; set; }
        public string CurrentOrderType { get; set; }
        public string CurrentState { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxSono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                int CurrentSomastid = soinf.GetSomastBySono(textBoxSono.Text.TrimEnd().TrimStart().PadLeft(10));
                if (CurrentSomastid != 0)
                {
                    CurrentSono = soinf.somastds.somast[0].sono;
                    if (soinf.somastds.somast[0].enterqu == "Y")
                    {
                        CurrentOrderType = "Cover";
                    }
                    else
                    {
                        CurrentOrderType = "Miscellaneous";
                    }
                    labelSelectedSO.Text = "Processing SO " + CurrentSono.TrimStart();
                    CurrentState = "View";
                    RefreshControls();
                }
                else
                {
                    wsgUtilities.wsgNotice("Sales Order Not Found. Click Find to Search");
                }
            }
        }

        private void RefreshControls()
        {
            // Disable all controls
            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }

            switch (CurrentState)
            {
                case "Select":
                    {
                        labelSelectedSO.Text = "Select SO";
                        textBoxSono.Enabled = true;
                        buttonClose.Enabled = true;
                        buttonGetSO.Enabled = true;
                        checkBoxInvoice.Checked = false;
                        checkBoxOrder.Checked = false;
                        checkBoxPackingList.Checked = false;
                        checkBoxWorkOrder.Checked = false;
                        checkBoxIdentityLabel.Checked = false;
                        checkBoxSewnOnLabel.Checked = false;
                        break;
                    }
                case "View":
                    {
                        buttonClose.Enabled = true;
                        buttonClear.Enabled = true;
                        buttonGenerate.Enabled = true;
                        groupBoxDocuments.Enabled = true;
                        checkBoxInvoice.Checked = false;
                        checkBoxOrder.Checked = false;
                        checkBoxPackingList.Checked = false;
                        checkBoxWorkOrder.Checked = false;
                        checkBoxIdentityLabel.Checked = false;
                        checkBoxSewnOnLabel.Checked = false;
                        break;
                    }
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (checkBoxIdentityLabel.Checked)
            {
                ShowForm("Identity Label");
            }

            if (checkBoxSewnOnLabel.Checked)
            {
                ShowForm("Sewn On Label");
            }

            if (checkBoxOrder.Checked)
            {
                ShowForm("Estimate");
            }
            if (checkBoxInvoice.Checked)
            {
                ShowForm("Invoice");
            }

            if (checkBoxWorkOrder.Checked)
            {
                ShowForm("Work Order");
            }

            if (checkBoxPackingList.Checked)
            {
                ShowForm("Packing List");
            }
            CurrentState = "View";
            RefreshControls();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxSono.Text = "";
            CurrentState = "Select";
            RefreshControls();
        }

        private void ShowForm(string SoDocument)
        {
            if (CurrentOrderType == "Cover")
            {
                frmCoverSODocumentViewer.CurrentSono = CurrentSono;
                frmCoverSODocumentViewer.SODocument = SoDocument;
                frmCoverSODocumentViewer.ShowDialog();
            }
            else
            {
                frmMiscSODocumentViewer.CurrentSono = CurrentSono;
                frmMiscSODocumentViewer.SODocument = SoDocument;
                frmMiscSODocumentViewer.ShowDialog();
            }
        }

        private void buttonGetSO_Click(object sender, EventArgs e)
        {
            FrmSOSearch frmSoSearch = new FrmSOSearch(this.textBoxSono.Text);
            frmSoSearch.ShowDialog();
            if (frmSoSearch.SelectedSono.TrimEnd() != "")
            {
                CurrentSono = frmSoSearch.SelectedSono;
                soinf.GetSomastBySono(CurrentSono);
                if (soinf.somastds.somast.Rows.Count > 0)
                {
                    if (soinf.somastds.somast[0].enterqu == "Y")
                    {
                        CurrentOrderType = "Cover";
                    }
                    else
                    {
                        CurrentOrderType = "Miscellaneous";
                    }
                    labelSelectedSO.Text = "Processing SO " + CurrentSono.TrimStart();
                    CurrentState = "View";
                    RefreshControls();
                }
                else
                {
                    wsgUtilities.wsgNotice("SO not found");
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
        }
    }
}