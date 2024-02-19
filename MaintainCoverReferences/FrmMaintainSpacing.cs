using CommonAppClasses;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainCoverReferences
{
    //CACHED  Maintain -> Spacing 
    public partial class FrmMaintainSpacing : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Spacing Information");

        // Create the Spacing processing object
        private ReferenceMaintenance refdata = new ReferenceMaintenance("SQL", "SQLConnString");

        private static BindingSource bindingSpacingData = new BindingSource();
        private static ObjectCache dataCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_Spacing"]));

        public string CurrentState { get; set; }
        public int SelectedSpacingId { get; set; }
        public bool InsertingSpacing { get; set; }

        public FrmMaintainSpacing()
        {
            InitializeComponent();
            dataGridViewSpacing.AutoGenerateColumns = false;
            dataGridViewSpacing.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewSpacing.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            if (dataCache.IsInvalid)
            {
                refdata.GetSpacingData();
                bindingSpacingData.DataSource = refdata.referenceds.view_quspacingdata;
                dataCache.Refresh(bindingSpacingData);
            }
            dataGridViewSpacing.DataSource = bindingSpacingData;
            CurrentState = "Select";
            RefreshControls();
        }

        private void SetBindings()
        {
            foreach (Control d in this.Controls)
                if (d is TextBox)
                {
                    d.DataBindings.Clear();
                    int length = d.Name.ToString().Length;
                    string columnname = d.Name.ToString().Substring(7, length - 7);
                    d.DataBindings.Add(new Binding("Text", refdata.referenceds, "quspacing." + columnname));
                }
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
                        dataGridViewSpacing.Enabled = true;
                        textBoxDescrip.Focus();
                        buttonClear.Enabled = true;
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
                        dataGridViewSpacing.Enabled = true;
                        textBoxDescrip.Focus();
                        buttonClear.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonInsert.Enabled = false;
                        buttonSave.Enabled = false;
                        break;
                    }

                case "Select":
                    {
                        // Loop thru all the controls and disable text boxes
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Enabled = false;
                                continue;
                            }
                        }
                        dataGridViewSpacing.Enabled = true;
                        buttonClear.Enabled = false;
                        buttonInsert.Enabled = true;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = false;
                        break;
                    }

                case "Edit":
                    {
                        // Loop thru all the controls  disable text boxes
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Enabled = true;
                                continue;
                            }
                        }
                        dataGridViewSpacing.Enabled = false;
                        textBoxDescrip.Focus();
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;

                        break;
                    }
                case "Insert":
                    {
                        // Loop thru all the controls and enable text boxes
                        foreach (Control c in Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Enabled = true;
                                continue;
                            }
                        }
                        dataGridViewSpacing.Enabled = false;
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewSpacing_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedSpacingId = refdata.CaptureIdCol(dataGridViewSpacing);
            refdata.getSingleSpacingData(SelectedSpacingId);
            SetBindings();
            CurrentState = "View";
            RefreshControls();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            refdata.EstablishBlankQuSpacingData();
            SetBindings();
            CurrentState = "Insert";
            RefreshControls();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState == "Edit")
                    {
                        refdata.UnlockQuSpacing(SelectedSpacingId);
                    }
                    refdata.ClearQuSpacingData();
                    SetBindings();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                refdata.ClearQuSpacingData();
                SetBindings();
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = refdata.LockQuSpacing(SelectedSpacingId);
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
            refdata.SaveSpacingData();

            refdata.ClearQuSpacingData();
            SetBindings();
            CurrentState = "Select";
            // Refresh the grid source data table

            refdata.GetSpacingData();
            bindingSpacingData.DataSource = refdata.referenceds.view_quspacingdata;
            dataGridViewSpacing.DataSource = bindingSpacingData;

            RefreshControls();
        }
    }
}