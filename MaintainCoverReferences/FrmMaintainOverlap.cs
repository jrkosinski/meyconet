using CommonAppClasses;
using System;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainCoverReferences
{
    public partial class FrmMaintainOverlap : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Overlap Information");

        // Create the Spacing processing object
        private ReferenceMaintenance refdata = new ReferenceMaintenance("SQL", "SQLConnString");

        private BindingSource bindingOverlapData = new BindingSource();

        public FrmMaintainOverlap()
        {
            InitializeComponent();

            dataGridViewOverlap.AutoGenerateColumns = false;
            dataGridViewOverlap.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewOverlap.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            bindingOverlapData.DataSource = refdata.referenceds.view_quoverlapdata;
            dataGridViewOverlap.DataSource = bindingOverlapData;
            refdata.GetOverlapData();
            SetBindings();
            CurrentState = "Select";
            RefreshControls();
        }

        public string CurrentState { get; set; }
        public int SelectedOverlapId { get; set; }
        public bool InsertingOverlap { get; set; }

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

        private void RefreshControls()
        {
            buttonDelete.Enabled = refdata.HasSelectedOverlap;

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
                        dataGridViewOverlap.Enabled = true;
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
                        dataGridViewOverlap.Enabled = true;
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
                        dataGridViewOverlap.Enabled = true;
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
                        dataGridViewOverlap.Enabled = false;
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
                        dataGridViewOverlap.Enabled = false;
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;
                        break;
                    }
            }
            this.Update();
        }

        private void SetBindings()
        {
            foreach (Control d in this.Controls)
                if (d is TextBox)
                {
                    d.DataBindings.Clear();
                    int length = d.Name.ToString().Length;
                    string columnname = d.Name.ToString().Substring(7, length - 7);
                    d.DataBindings.Add(new Binding("Text", refdata.referenceds, "quoverlap." + columnname));
                }
        }

        private void dataGridViewOverlap_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedOverlapId = refdata.CaptureIdCol(dataGridViewOverlap);
            refdata.getSingleOverlapData(SelectedOverlapId);
            CurrentState = "View";
            RefreshControls();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            refdata.EstablishBlankQuOverlapData();
            SetBindings();
            CurrentState = "Insert";
            RefreshControls();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            refdata.SaveOverlapData();
            refdata.GetOverlapData();
            refdata.ClearQuOverlapData();
            CurrentState = "View";
            RefreshControls();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (refdata.HasSelectedOverlap)
            {
                if (wsgUtilities.wsgReply("Delete this item") == true)
                {
                    refdata.DeleteQuoverlapRow();
                    refdata.GetOverlapData();
                    refdata.ClearQuOverlapData();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState == "Edit")
                    {
                        refdata.UnlockQuOverlap(SelectedOverlapId);
                    }
                    refdata.ClearQuOverlapData();
                    refdata.GetOverlapData();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                refdata.ClearQuOverlapData();
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = refdata.LockQuOverlap(SelectedOverlapId);
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
    }
}