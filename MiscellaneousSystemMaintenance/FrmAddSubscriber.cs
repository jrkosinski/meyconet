using CommonAppClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public partial class FrmAddSubscriber : WSGUtilitieslib.Telemetry.Form
    {
        public int CurrentStepid { get; set; }
        public int NewUserid { get; set; }

        private BindingSource bindingUserData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Step Maintenance");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");

        public FrmAddSubscriber()
        {
            InitializeComponent();
            bindingUserData.DataSource = trackingInf.trackingds.appuser;
            dataGridViewUserData.DataSource = bindingUserData;
            dataGridViewUserData.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewUserData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewUserData.AutoGenerateColumns = false;
            dataGridViewUserData.Focus();
        }

        private void filldatagrid()
        {
            trackingInf.GetNonSubscribers(CurrentStepid);
            if (trackingInf.trackingds.appuser.Rows.Count < 1)
            {
                wsgUtilities.wsgNotice("There are no unassigned subscribers.");
            }
        } // end filldatagrid

        public void CaptureUserId()
        {
            CurrencyManager xCM =
           (CurrencyManager)dataGridViewUserData.BindingContext[dataGridViewUserData.DataSource,
              dataGridViewUserData.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Save the selected UserID

            NewUserid = (int)xRow["idcol"];
            trackingInf.SaveNewSubscriber(CurrentStepid, NewUserid);
            filldatagrid();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewUserData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureUserId();
            }
        }

        private void FrmAddSubscriber_Activated(object sender, EventArgs e)
        {
            filldatagrid();
        }

        private void dataGridViewUserData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureUserId();
        }
    }
}