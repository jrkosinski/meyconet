using CommonAppClasses;
using System;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainCoverReferences
{
    public partial class FrmMaintainColor : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Color Information");

        // Create the Spacing processing object
        private ReferenceMaintenance refdata = new ReferenceMaintenance("SQL", "SQLConnString");

        private BindingSource bindingColorData = new BindingSource();

        public FrmMaintainColor()
        {
            InitializeComponent();
            dataGridViewColor.AutoGenerateColumns = false;
            dataGridViewColor.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewColor.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            bindingColorData.DataSource = refdata.referenceds.view_qucolordata;
            dataGridViewColor.DataSource = bindingColorData;
            refdata.GetColorData();
            SetBindings();
            CurrentState = "Select";
            RefreshControls();

            SetTabOrder();
        }

        public string CurrentState { get; set; }
        public int SelectedColorId { get; set; }
        public bool InsertingColor { get; set; }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonInsert,
                this.textBoxDescrip,
                this.buttonSave,
                this.buttonClose
            });
        }

        private void RefreshControls()
        {
            buttonDelete.Enabled = refdata.HasSelectedColor;

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
                        dataGridViewColor.Enabled = true;
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
                        dataGridViewColor.Enabled = true;
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
                        dataGridViewColor.Enabled = true;
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
                        dataGridViewColor.Enabled = false;
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
                        dataGridViewColor.Enabled = false;
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
                    d.DataBindings.Add(new Binding("Text", refdata.referenceds, "qucolor." + columnname));
                }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            refdata.EstablishBlankQuColorData();
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
                        refdata.UnlockQuColor(SelectedColorId);
                    }
                    refdata.ClearQuColorData();
                    refdata.GetColorData();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                refdata.ClearQuColorData();
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = refdata.LockQuColor(SelectedColorId);
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (refdata.HasSelectedColor)
            {
                if (wsgUtilities.wsgReply("Delete this item") == true)
                {
                    refdata.DeleteQucolorRow();
                    refdata.GetColorData();
                    refdata.ClearQuColorData();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            refdata.SaveColorData();
            refdata.GetColorData();
            refdata.ClearQuColorData();
            CurrentState = "View";
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

        private void dataGridViewColor_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedColorId = refdata.CaptureIdCol(dataGridViewColor);
            refdata.getSingleColorData(SelectedColorId);
            CurrentState = "View";
            RefreshControls();
        }
    }
}