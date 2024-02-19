using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainSystemReferences
{
    public class MaintainSysReference : WSGDataAccess

    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("System Reference");
        private BindingSource bindingReferenceData = new BindingSource();
        public string CurrentState { get; set; }
        public int CurrentGroup { get; set; }
        public string CurrentGroupName { get; set; }
        public int CurrentReferenceID { get; set; }
        public FrmMaintainSysReference parentform { get; set; }
        public system appinfods;
        public system referencequeryds;

        public MaintainSysReference(string DataStore, string AppConfigName, FrmMaintainSysReference callingform)
         : base(DataStore, AppConfigName)
        {
            parentform = callingform;
            appinfods = new system();
            referencequeryds = new system();
            SetIdcol(appinfods.sysreference.idcolColumn);
            SetBindings();
            CurrentGroupName = "";
            SetGroupItems();
            SetEvents();
            CurrentState = "Select";
            parentform.dataGridViewReferenceDetails.AutoGenerateColumns = false;
            parentform.dataGridViewReferenceDetails.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentform.dataGridViewReferenceDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            bindingReferenceData.DataSource = appinfods.view_sysreference;
            parentform.dataGridViewReferenceDetails.DataSource = bindingReferenceData;
        }

        public void SetGroupItems()
        {
            appinfods.sysrefgroup.Rows.Clear();
            this.ClearParameters();
            this.FillData(appinfods, "sysrefgroup", "wsgsp_getsysrefgroup", CommandType.StoredProcedure);
            if (appinfods.sysrefgroup.Rows.Count > 0)
            {
                parentform.comboBoxRefrenceGroups.DataSource = appinfods.sysrefgroup;
                parentform.comboBoxRefrenceGroups.DisplayMember = "groupname";
                parentform.comboBoxRefrenceGroups.ValueMember = "idcol";
            }
        }

        public void SetBindings()
        {
            parentform.textBoxDescrip.DataBindings.Add("text", appinfods.sysreference, "refdescrip");
        }

        public void SetEvents()
        {
            parentform.buttonClose.Click += new System.EventHandler(CloseForm);
            parentform.buttonInsert.Click += new System.EventHandler(SetInsert);
            parentform.buttonDelete.Click += new System.EventHandler(DeleteEntry);
            parentform.buttonEdit.Click += new System.EventHandler(SetEdit);
            parentform.buttonSave.Click += new System.EventHandler(SaveReferenceData);
            parentform.comboBoxRefrenceGroups.SelectedIndexChanged += new System.EventHandler(SetGroup);
            parentform.dataGridViewReferenceDetails.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(CaptureReferenceID);
            parentform.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormClosing);
        }

        public void SetGroup(object sender, EventArgs e)
        {
            CurrentGroupName = parentform.comboBoxRefrenceGroups.SelectedItem.ToString();

            CurrentGroup = Convert.ToInt32(parentform.comboBoxRefrenceGroups.SelectedValue);
            GetGroupDetails();
        }

        public void GetGroupDetails()
        {
            appinfods.view_sysreference.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@groupid", CurrentGroup, "SQL");
            this.FillData(appinfods, "view_sysreference", "wsgsp_getsysreferences", CommandType.StoredProcedure);
            CurrentState = "Select";
            RefreshParentControls();
        }

        public void GetSingleReference(int idcol)
        {
            appinfods.sysreference.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(appinfods, "sysreference", "wsgsp_getsinglesysreference", CommandType.StoredProcedure);
            CurrentState = "View";
            RefreshParentControls();
        }

        public void SetEdit(object sender, EventArgs e)
        {
            string editstatus = LockSysreference();
            if (editstatus == "OK")
            {
                CurrentState = "Edit";
                RefreshParentControls();
            }
            else
            {
                wsgUtilities.wsgNotice(editstatus);
            }
        }

        public void DeleteEntry(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this item?"))
            {
                DeleteTableRow("sysreference", CurrentReferenceID);
                CurrentState = "Select";
                GetGroupDetails();
                RefreshParentControls();
            }
        }

        public void SetInsert(object sender, EventArgs e)
        {
            EstablishBlankDataTableRow(appinfods.sysreference);
            appinfods.sysreference[0].groupid = CurrentGroup;
            appinfods.sysreference[0].adduser = AppUserClass.AppUserId;
            appinfods.sysreference[0].lckuser = AppUserClass.AppUserId;
            appinfods.sysreference[0].lckdate = DateTime.Now;
            appinfods.sysreference[0].adddate = DateTime.Now;
            CurrentState = "Insert";
            RefreshParentControls();
        }

        public void CloseForm(object sender, EventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        UnlockSysreference();
                    }
                }
                parentform.Close();
            }
            else
            {
                parentform.Close();
            }
        }

        public void RefreshParentControls()
        {
            DisableControls();

            switch (CurrentState)
            {
                case "View":
                    {
                        parentform.comboBoxRefrenceGroups.Enabled = true;
                        parentform.buttonEdit.Enabled = true;
                        parentform.buttonDelete.Enabled = true;
                        break;
                    }
                case "Select":
                    {
                        parentform.comboBoxRefrenceGroups.Enabled = true;
                        parentform.buttonInsert.Enabled = true;
                        parentform.dataGridViewReferenceDetails.Enabled = true;
                        break;
                    }
                case "Edit":
                    {
                        parentform.textBoxDescrip.Enabled = true;
                        parentform.buttonSave.Enabled = true;
                        break;
                    }
                case "Insert":
                    {
                        parentform.textBoxDescrip.Enabled = true;
                        parentform.buttonSave.Enabled = true;
                        break;
                    }
            }
            parentform.buttonClose.Enabled = true;
            parentform.Update();
        }

        public void DisableControls()
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in parentform.Controls)
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
            parentform.buttonClose.Enabled = true;
        }

        private void CaptureReferenceID(object sender, DataGridViewCellEventArgs e)
        {
            CurrentReferenceID = CaptureIdCol(parentform.dataGridViewReferenceDetails);
            GetSingleReference(CurrentReferenceID);
            CurrentState = "View";
            RefreshParentControls();
        }

        public void SaveReferenceData(object sender, EventArgs e)
        {
            GenerateAppTableRowSave(appinfods.sysreference[0]);
            GetGroupDetails();
        }

        public string LockSysreference()
        {
            this.ClearParameters();
            this.AddParms("@idcol", CurrentReferenceID, "SQL");
            this.AddParms("@tablename", "sysreference", "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                HandleException(ex);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentState.Substring(0, 4) == "Edit" || CurrentState == "Insert")
            {
                if (wsgUtilities.wsgReply("Abandon Edit") == true)
                {
                    if (CurrentState != "Insert")
                    {
                        UnlockSysreference();
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        public void UnlockSysreference()
        {
            this.ClearParameters();
            this.AddParms("@idcol", CurrentReferenceID, "SQL");
            this.AddParms("@tablename", "sysreference", "SQL");
            try
            {
                ExecuteCommand("wsgsp_unlocktable", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public string getSysreferenceDescrip(int idcol)
        {
            string refdescrip = "Unidentified";
            referencequeryds.sysreference.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.FillData(referencequeryds, "sysreference", "wsgsp_getsinglesysreference", CommandType.StoredProcedure);
            return refdescrip;
        }
    }
}