using CommonAppClasses;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public partial class FrmMaintainWorkgroupSteps : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingWorkgroupStepData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Work Group Step Maintenance");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");
        public int CurrentWorkgroupId { get; set; }

        public FrmMaintainWorkgroupSteps()
        {
            InitializeComponent();
            conn.ConnectionString = myAppconstants.SQLConnectionString;

            dataGridViewWorkgroupSteps.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewWorkgroupSteps.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewWorkgroupSteps.AutoGenerateColumns = false;
            bindingWorkgroupStepData.DataSource = trackingInf.listtrackingds.view_workgroupstepdata;
            dataGridViewWorkgroupSteps.DataSource = bindingWorkgroupStepData;
            dataGridViewWorkgroupSteps.Focus();
            CurrentState = "Select";
            labelDelete.Visible = false;
        }

        public int CurrentWorkgroupStepIdcol { get; set; }
        public int CurrentWorkgroupStepId { get; set; }
        public string CurrentWorkgroupStepCode { get; set; }
        public string CurrentWorkgroupStepDescrip { get; set; }
        public string CurrentState { get; set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void filldatagrid()
        {
            trackingInf.GetWorkGroupStepData(CurrentWorkgroupId);
        } // end filldatagrid

        public void CaptureWorkgroupStepData()
        {
            bool WorkGroupStepOK = true;
            CurrentWorkgroupStepCode = "";
            CurrentWorkgroupStepDescrip = "Not Found";
            CurrentWorkgroupStepIdcol = trackingInf.CaptureIdCol(dataGridViewWorkgroupSteps);
            trackingInf.GetSingleWorkGroupStep(CurrentWorkgroupStepIdcol);
            if (trackingInf.trackingds.workgroupstep.Rows.Count > 0)
            {
                CurrentWorkgroupStepId = trackingInf.trackingds.workgroupstep[0].stepid;
                trackingInf.GetSingleTrackingStep(CurrentWorkgroupStepId);
                if (trackingInf.trackingds.step.Rows.Count > 0)
                {
                    CurrentWorkgroupStepCode = trackingInf.trackingds.step[0].code;
                    CurrentWorkgroupStepDescrip = trackingInf.trackingds.step[0].descrip;
                }
                else
                {
                    WorkGroupStepOK = false;
                }
            }
            else
            {
                WorkGroupStepOK = false;
            }

            if (WorkGroupStepOK == false)
            {
                wsgUtilities.wsgNotice("Work Group Step Eror. Get Help.");
            }
        } // end captureworkgroupstepdata

        private void dataGridViewWorkgroupSteps_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureWorkgroupStepData();
        } // end cellcontentclick

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            // User selects Step to be inserted
            FrmGetTrackingCode myfrmGetTrackingCode = new FrmGetTrackingCode();
            myfrmGetTrackingCode.ShowDialog();
            if (myfrmGetTrackingCode.SelectedCode != "Cancelled")
            {
                CurrentWorkgroupStepId = myfrmGetTrackingCode.SelectedId;
                string InsertMessage = trackingInf.InsertWorkGroupStep(CurrentWorkgroupId, CurrentWorkgroupStepId);
                if (InsertMessage.TrimEnd() == "Step Inserted")
                {
                    wsgUtilities.wsgNotice("Step Inserted");
                    CurrentState = "Select";
                    filldatagrid();
                }
                else
                {
                    wsgUtilities.wsgNotice("Insertion not completed. " + InsertMessage);
                }
            } // endif != cancelled
            else
            {
                wsgUtilities.wsgNotice("Operation Cancelled");
            } // end else
        } // end insertclick

        private void FrmMaintainWorkgroupSteps_Shown(object sender, EventArgs e)
        {
            filldatagrid();
        }

        private void dataGridViewWorkgroupSteps_DoubleClick(object sender, EventArgs e)
        {
            CaptureWorkgroupStepData();

            if (wsgUtilities.wsgReply("Delete " + CurrentWorkgroupStepDescrip.Trim() + "?"))
            {
                trackingInf.DeleteWorkGroupStep(CurrentWorkgroupStepIdcol);
                wsgUtilities.wsgNotice("Work Group Step Detleted ");
                filldatagrid();
            }
            else
            {
                wsgUtilities.wsgNotice("Deletion Cancelled");
            }
        }
    } // end form
}//end namespace