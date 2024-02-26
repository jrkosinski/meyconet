using CommonAppClasses;
using System;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainSystemReference
{
    public partial class FrmMaintainSystemComments : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("System Comments");

        // Create the System Comments processing object
        private SystemCommentsMaintenance syscomm = new SystemCommentsMaintenance("SQL", "SQLConnString");

        private BindingSource bindingSystemCommentsData = new BindingSource();

        public string CurrentState { get; set; }
        public int SelectedSystemcommentsId { get; set; }
        public bool InsertingSystemcomments { get; set; }

        public FrmMaintainSystemComments()
        {
            InitializeComponent();

            dataGridViewSystemComments.AutoGenerateColumns = false;
            dataGridViewSystemComments.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewSystemComments.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            syscomm.GetSystemCommentsData();
            bindingSystemCommentsData.DataSource = syscomm.systemds.view_systemcomments;
            dataGridViewSystemComments.DataSource = bindingSystemCommentsData;
            CurrentState = "Select";

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonInsert,
                this.buttonEdit,
                this.textBoxCode,
                this.textBoxDescrip,
                this.buttonSave,
                this.buttonClose
            });
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

        private void SetBindings()
        {
            foreach (Control d in this.Controls)
                if (d is TextBox)
                {
                    d.DataBindings.Clear();
                    int length = d.Name.ToString().Length;
                    string columnname = d.Name.ToString().Substring(7, length - 7);
                    d.DataBindings.Add(new Binding("Text", syscomm.systemds, "systemcomments." + columnname));
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
                            if (c is TextBox)
                            {
                                c.Enabled = false;
                                continue;
                            }
                        }
                        dataGridViewSystemComments.Enabled = true;
                        textBoxCode.Focus();
                        buttonClear.Enabled = true;
                        buttonDelete.Enabled = true;
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
                        dataGridViewSystemComments.Enabled = true;
                        textBoxCode.Focus();
                        buttonClear.Enabled = false;
                        buttonDelete.Enabled = false;
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
                        dataGridViewSystemComments.Enabled = true;
                        buttonClear.Enabled = false;
                        buttonDelete.Enabled = false;
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
                        dataGridViewSystemComments.Enabled = false;
                        textBoxCode.Focus();
                        buttonClear.Enabled = true;
                        buttonDelete.Enabled = true;
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
                        dataGridViewSystemComments.Enabled = false;
                        buttonClear.Enabled = true;
                        buttonInsert.Enabled = false;
                        buttonDelete.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;
                        break;
                    }
            }
            this.Update();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            syscomm.EstablishBlankSystemCommentsData();
            SetBindings();
            CurrentState = "Insert";
            RefreshControls();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            syscomm.SaveSystemComments();
            syscomm.ClearSystemcommentsData();
            SetBindings();
            CurrentState = "Select";
            // Refresh the grid source data table
            syscomm.GetSystemCommentsData();
            bindingSystemCommentsData.DataSource = syscomm.systemds.view_systemcomments;
            dataGridViewSystemComments.DataSource = bindingSystemCommentsData;
            RefreshControls();
        }

        private void dataGridViewSystemComments_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedSystemcommentsId = syscomm.CaptureIdCol(dataGridViewSystemComments);
            syscomm.getSingleSystemcommentsData(SelectedSystemcommentsId);
            SetBindings();
            CurrentState = "View";
            RefreshControls();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string editstatus = syscomm.LockSystemcomments(SelectedSystemcommentsId);
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
                        syscomm.UnlockSystemcomments(SelectedSystemcommentsId);
                    }
                    syscomm.ClearSystemcommentsData();
                    SetBindings();
                    CurrentState = "Select";
                    RefreshControls();
                }
            }
            else
            {
                syscomm.ClearSystemcommentsData();
                SetBindings();
                CurrentState = "Select";
                RefreshControls();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this record?") == true)
            {
                syscomm.DeleteSystemcommentsrow();
                syscomm.ClearSystemcommentsData();
                syscomm.GetSystemCommentsData();
                bindingSystemCommentsData.DataSource = syscomm.systemds.view_systemcomments;
                dataGridViewSystemComments.DataSource = bindingSystemCommentsData;
                CurrentState = "Select";
                SetBindings();
                RefreshControls();
            }
        }
    }
}