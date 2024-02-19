using CommonAppClasses;
using System;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainCoverReferences
{
    public partial class FrmMaintainMaterial : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Material Information");

        // Create the Spacing processing object
        private ReferenceMaintenance refdata = new ReferenceMaintenance("SQL", "SQLConnString");

        private BindingSource bindingMaterialData = new BindingSource();

        public FrmMaintainMaterial()
        {
            InitializeComponent();
            dataGridViewMaterial.AutoGenerateColumns = false;
            dataGridViewMaterial.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewMaterial.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            bindingMaterialData.DataSource = refdata.referenceds.view_qumaterialdata;
            dataGridViewMaterial.DataSource = bindingMaterialData;
            refdata.GetMaterialData();
            SetBindings();
            CurrentState = "Select";
            RefreshControls();
        }

        public string CurrentState { get; set; }
        public int SelectedMaterialId { get; set; }
        public bool InsertingMaterial { get; set; }

        #region refresh controls

        private void RefreshControls()
        {
            buttonDelete.Enabled = refdata.HasSelectedMaterial;

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
                        dataGridViewMaterial.Enabled = true;
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
                        dataGridViewMaterial.Enabled = true;
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
                        dataGridViewMaterial.Enabled = true;
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
                        dataGridViewMaterial.Enabled = false;
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
                        dataGridViewMaterial.Enabled = false;
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

        private void SetBindings()
        {
            foreach (Control d in this.Controls)
                if (d is TextBox)
                {
                    d.DataBindings.Clear();
                    int length = d.Name.ToString().Length;
                    string columnname = d.Name.ToString().Substring(7, length - 7);
                    d.DataBindings.Add(new Binding("Text", refdata.referenceds, "qumaterial." + columnname));
                }
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

        private void dataGridViewMaterial_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedMaterialId = refdata.CaptureIdCol(dataGridViewMaterial);
            refdata.getSingleMaterialData(SelectedMaterialId);
            CurrentState = "View";
            RefreshControls();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = refdata.LockQuMaterial(SelectedMaterialId);
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState == "Edit")
                    {
                        refdata.UnlockQuMaterial(SelectedMaterialId);
                    }
                    refdata.ClearQuMaterialData();
                    refdata.GetMaterialData();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                refdata.ClearQuMaterialData();
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (refdata.HasSelectedMaterial)
            {
                if (wsgUtilities.wsgReply("Delete this item") == true)
                {
                    refdata.DeleteQumaterialRow();
                    refdata.GetMaterialData();
                    refdata.ClearQuMaterialData();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            refdata.EstablishBlankQuMaterialData();
            SetBindings();
            CurrentState = "Insert";
            RefreshControls();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            refdata.SaveMaterialData();
            refdata.GetMaterialData();
            refdata.ClearQuMaterialData();
            CurrentState = "View";
            RefreshControls();
        }

        private void textBoxPrcmatrl_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBoxDescrip_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridViewMaterial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}