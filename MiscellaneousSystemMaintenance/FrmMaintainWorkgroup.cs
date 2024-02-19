using CommonAppClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    //CACHED  Maintain -> Work groups
    public partial class FrmMaintainWorkgroup : WSGUtilitieslib.Telemetry.Form
    {
        private static BindingSource bindingWorkgroupData = new BindingSource();
        private static ObjectCache dataCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_WorkGroups"]));
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Work Group Maintenance");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");

        public FrmMaintainWorkgroup()
        {
            InitializeComponent();
            conn.ConnectionString = myAppconstants.SQLConnectionString;
            if (dataCache.IsInvalid)
            {
                bindingWorkgroupData.DataSource = trackingInf.listtrackingds.workgroup;
            }
            dataGridViewWorkgroups.DataSource = bindingWorkgroupData;
            dataGridViewWorkgroups.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewWorkgroups.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewWorkgroups.AutoGenerateColumns = false;
            dataGridViewWorkgroups.Focus();
            textBoxGroupName.DataBindings.Add("Text", trackingInf.trackingds.workgroup, "groupname");
            CurrentState = "Select";
            labelGroupName.Visible = false;

            if (dataCache.IsInvalid)
            {
                filldatagrid();
                dataCache.Refresh(bindingWorkgroupData);
            }
            RefreshControls();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            trackingInf.IntializeWorkGroup();
            CurrentState = "Insert";
            RefreshControls();
        }

        public string CurrentState { get; set; }
        public int CurrentWorkgroupId { get; set; }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            trackingInf.SaveWorkGroup();
            wsgUtilities.wsgNotice("Work Group Saved.");
            CurrentState = "Select";
            filldatagrid();
            RefreshControls();
        } // end of save processing

        private void RefreshControls()
        // Establish the proper control status
        {
            switch (CurrentState)
            {
                case "Select":
                    buttonCancel.Enabled = true;
                    textBoxGroupName.Enabled = false;
                    buttonInsert.Enabled = true;
                    buttonEdit.Enabled = false;
                    buttonSave.Enabled = false;
                    buttonSteps.Enabled = false;
                    break;

                case "View":
                    buttonSteps.Enabled = true;
                    textBoxGroupName.Enabled = false;
                    buttonCancel.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonInsert.Enabled = true;
                    buttonEdit.Enabled = true;
                    buttonSave.Enabled = false;
                    break;

                case "Edit":
                    buttonSteps.Enabled = false;
                    textBoxGroupName.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonInsert.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonSave.Enabled = true;
                    break;

                case "Insert":
                    buttonSteps.Enabled = false;
                    textBoxGroupName.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonInsert.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonSave.Enabled = true;
                    break;
            } // end switch
        } // end RefreshControls()

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            CurrentState = "Edit";
            RefreshControls();
        }

        private void filldatagrid()
        {
            trackingInf.GetWorkGroups();
            if (trackingInf.listtrackingds.workgroup.Rows.Count < 1)
            {
                wsgUtilities.wsgNotice("There are no active groups.");
            }
        } // end filldatagrid

        public void CaptureWorkgroupId()
        {
            CurrentWorkgroupId = trackingInf.CaptureIdCol(dataGridViewWorkgroups);
            trackingInf.GetSingleWorkGroup(CurrentWorkgroupId);
            CurrentState = "View";
            RefreshControls();
        }

        private void dataGridViewWorkgroups_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureWorkgroupId();
        }

        private void dataGridViewWorkgroups_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureWorkgroupId();
            }
        }

        private void buttonSteps_Click(object sender, EventArgs e)
        {
            FrmMaintainWorkgroupSteps frmMaintainWorkgroupsteps = new FrmMaintainWorkgroupSteps();
            frmMaintainWorkgroupsteps.CurrentWorkgroupId = CurrentWorkgroupId;
            frmMaintainWorkgroupsteps.Text = "Steps in work group " + textBoxGroupName.Text.Trim();
            frmMaintainWorkgroupsteps.ShowDialog();
            CurrentState = "Select";
            RefreshControls();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Deletion processing
            if (wsgUtilities.wsgReply("Delete this group?"))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_deleteworkgroup");
                appUtilities.makeSQLCommand(ref cmd, ref conn);
                cmd.Parameters.Add("@idcol", SqlDbType.Int);
                cmd.Parameters["@idcol"].Value = CurrentWorkgroupId;
                SqlParameter DeleteMessage = new SqlParameter("@DeleteMessage", SqlDbType.Char, 20);
                DeleteMessage.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(DeleteMessage);

                conn.Open();
                try
                {
                    WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(cmd);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    if (cmd.Parameters["@DeleteMessage"].Value.ToString().Trim() == "Work Group Deleted")
                    {
                        wsgUtilities.wsgNotice("Group Deleted");
                        CurrentState = "Select";
                        filldatagrid();
                    }
                    else
                    {
                        wsgUtilities.wsgNotice(cmd.Parameters["@DeleteMessage"].Value.ToString());
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    HandleException(ex);
                }
            }
            else
            {
                wsgUtilities.wsgNotice("Deletion Cancelled");
            }
        } // end deleteclick
    } // end form
} // end namespace