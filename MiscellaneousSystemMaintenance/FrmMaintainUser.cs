using CommonAppClasses;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public partial class FrmMaintainUser : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingUserData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("User Maintenance");

        private MiscellaneousDataMethods miscdata = new MiscellaneousDataMethods("SQL", "SQLConnString");

        public FrmMaintainUser()
        {
            conn.ConnectionString = myAppconstants.SQLConnectionString;
            InitializeComponent();
            CurrentState = "Select";
            RefreshControls();

            // Data bindings
            textBoxEmailAddress.DataBindings.Add("Text", miscdata.systemds.appuser, "emailaddress");
            textBoxUsername.DataBindings.Add("Text", miscdata.systemds.appuser, "username");
            textBoxUserid.DataBindings.Add("Text", miscdata.systemds.appuser, "userid");
            textBoxPassword.DataBindings.Add("Text", miscdata.systemds.appuser, "passwd");

            SetTabOrder();
        }

        public int SelectedUserId { get; set; }
        public string CurrentState { get; set; }
        public string AppUserstatus { get; set; }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonSelectUser,
                this.buttonEdit,
                this.textBoxUserid,
                this.textBoxUsername,
                this.textBoxPassword,
                this.textBoxPassword,
                this.textBoxEmailAddress,
                this.listBoxUserrole,
                this.buttonSave
            });
        }

        private void buttonSelectUser_Click(object sender, EventArgs e)
        {
            // Selects User to to be processed
            if (miscdata.GetUserId(true).TrimEnd() != "")
            {
                LoadUser();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Edit")
            {
                this.CancelEdit();
            }
            else
            {
                this.Close();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (AppUserstatus == "Active")
            {
                miscdata.systemds.appuser[0].userstatus = "A";
            }
            else
            {
                miscdata.systemds.appuser[0].userstatus = "I";
            }
            miscdata.systemds.appuser[0].userrole = listBoxUserrole.SelectedItem.ToString().Substring(0, 4);
            if (miscdata.SaveAppuser())
            {
                wsgUtilities.wsgNotice("User data updated");
                CurrentState = "Select";
                RefreshControls();
            }
            else
            {
                wsgUtilities.wsgNotice("User data update cancelled");
            }
        } //end save

        private void LoadUser()
        {
            if (miscdata.systemds.appuser[0].userstatus == "I")
            {
                AppUserstatus = "Inactive";
            }
            else
            {
                AppUserstatus = "Active";
            }

            int userlistindex = 0;
            // Locate selected role, if any
            foreach (var listrole in listBoxUserrole.Items)
            {
                if (listrole.ToString().Substring(0, 4) == miscdata.systemds.appuser[0].userrole)
                {
                    break;
                }
                userlistindex++;
                listBoxUserrole.Visible = true;
            }
            listBoxUserrole.SelectedIndex = userlistindex;
            CurrentState = "View";
            RefreshControls();
        }

        private void CancelEdit()
        {
            miscdata.GetSingleAppUser(miscdata.systemds.appuser[0].idcol);
            this.LoadUser();
        }

        private void DisableFields()
        {
            //Disable the form's fields
            textBoxUserid.Enabled = false;
            textBoxUsername.Enabled = false;
            textBoxPassword.Enabled = false;
            textBoxEmailAddress.Enabled = false;
            listBoxUserrole.Enabled = false;
        }

        private void EnableFields()
        {
            //Enable the form's fields
            textBoxUsername.Enabled = true;
            textBoxPassword.Enabled = true;
            textBoxEmailAddress.Enabled = true;
            listBoxUserrole.Enabled = true;
        }

        private void RefreshControls()
        {
            switch (CurrentState)
            {
                case "Select":
                    //Initialize the form's fields
                    labelUserStatus.Text = "";
                    textBoxUserid.Text = " ";
                    textBoxUsername.Text = " ";
                    textBoxPassword.Text = " ";
                    textBoxEmailAddress.Text = " ";
                    listBoxUserrole.SelectedIndex = 0;
                    // Disable the form's Controls
                    DisableFields();
                    // Establish the proper button status
                    buttonCancel.Enabled = true;
                    buttonInsert.Enabled = true;
                    buttonSelectUser.Enabled = true;
                    buttonEdit.Enabled = false;
                    buttonSave.Enabled = false;
                    buttonUserstatus.Enabled = false;

                    break;

                case "View":
                    // Disable the form's Controls
                    DisableFields();
                    if (AppUserstatus == "Active")
                    {
                        labelUserStatus.Text = "User status = Active";
                        buttonUserstatus.Text = "Deactivate";
                    }
                    else
                    {
                        labelUserStatus.Text = "User status = Inactive";
                        buttonUserstatus.Text = "Activate";
                    }
                    // Establish the proper button status
                    buttonCancel.Enabled = true;
                    buttonInsert.Enabled = true;
                    buttonSelectUser.Enabled = true;
                    buttonEdit.Enabled = true;
                    buttonSave.Enabled = false;
                    buttonUserstatus.Enabled = false;
                    break;

                case "Edit":
                    // Enable the form's Controls
                    EnableFields();
                    // Establish the proper button status
                    buttonCancel.Enabled = true;
                    buttonInsert.Enabled = false;
                    buttonSelectUser.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonSave.Enabled = true;
                    buttonUserstatus.Enabled = true;

                    break;

                case "Insert":
                    //Initialize the form's fields
                    textBoxUserid.Text = "    ";
                    textBoxUsername.Text = " ";
                    textBoxPassword.Text = " ";
                    textBoxEmailAddress.Text = " ";
                    listBoxUserrole.SelectedIndex = 0;
                    // Enable the form's Controls
                    EnableFields();
                    textBoxUserid.Enabled = true;
                    // Establish the proper button status
                    buttonCancel.Enabled = true;
                    buttonInsert.Enabled = false;
                    buttonSelectUser.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonSave.Enabled = true;
                    buttonUserstatus.Enabled = false;
                    break;
            }
            this.Update();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            CurrentState = "Edit";
            RefreshControls();
        }

        private void FrmMaintainUser_Load(object sender, EventArgs e)
        {
        }

        private void buttonUserstatus_Click(object sender, EventArgs e)
        {
            if (AppUserstatus == "Active")
            {
                AppUserstatus = "Inactive";
                labelUserStatus.Text = "User status = Inactive";
                buttonUserstatus.Text = "Activate";
            }
            else
            {
                AppUserstatus = "Active";
                labelUserStatus.Text = "User status = Active";
                buttonUserstatus.Text = "Deactivate";
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            miscdata.InitializeAppuser();
            miscdata.systemds.appuser[0].userstatus = "A";
            AppUserstatus = "Active";
            listBoxUserrole.SelectedIndex = 0;
            labelUserStatus.Text = "User status = Active";
            buttonUserstatus.Text = "Deactivate";
            CurrentState = "Insert";
            RefreshControls();
        }
    }
}