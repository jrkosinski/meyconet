using CommonAppClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public partial class FrmMaintainSubscribers : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingUserData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Step Subscribers");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");

        public FrmMaintainSubscribers()
        {
            InitializeComponent();
            bindingUserData.DataSource = trackingInf.trackingds.view_stepsubscriberdata;
            dataGridViewUserData.DataSource = bindingUserData;
            dataGridViewUserData.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewUserData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewUserData.AutoGenerateColumns = false;
            dataGridViewUserData.Focus();
        }

        public int CurrentStepid { get; set; }
        public int CurrentUserid { get; set; }
        public int NewUserid { get; set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            // User selects code to be processed
            FrmAddSubscriber myFrmAddSubscriber = new FrmAddSubscriber();
            myFrmAddSubscriber.CurrentStepid = CurrentStepid;
            myFrmAddSubscriber.ShowDialog();
        }

        private void filldatagrid()
        {
            trackingInf.GetSubscribers(CurrentStepid);
        }

        private void FrmMaintainSubscribers_Activated(object sender, EventArgs e)
        {
            filldatagrid();
        }

        private void dataGridViewUserData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this subscription?"))
            {
                CurrencyManager xCM =
                 (CurrencyManager)dataGridViewUserData.BindingContext[dataGridViewUserData.DataSource,
                   dataGridViewUserData.DataMember];
                DataRowView xDRV = (DataRowView)xCM.Current;
                DataRow xRow = xDRV.Row;
                // Save the select SO number

                int thisdcol = (Int32)xRow["idcol"];
                trackingInf.DeleteSubscription(thisdcol);
            } // end if
        } // end double click
    }
}