using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmGetWorkgroup : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingWorkgroupData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Work Group Selector");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");

        public FrmGetWorkgroup()
        {
            InitializeComponent();
            bindingWorkgroupData.DataSource = trackingInf.listtrackingds.workgroup;
            dataGridViewWorkgroups.DataSource = bindingWorkgroupData;

            // Set the DataGridView control's border.
            dataGridViewWorkgroups.BorderStyle = BorderStyle.Fixed3D;
            conn.ConnectionString = myAppconstants.SQLConnectionString;
            // Fill the grid with data
            filldatagrid();

            // The value for alternating rows overrides the value for all rows.
            dataGridViewWorkgroups.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewWorkgroups.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewWorkgroups.AutoGenerateColumns = false;
            dataGridViewWorkgroups.Focus();
        }

        public int SelectedWorkgroupId { get; set; }
        public string SelectedWorkgroupName { get; set; }

        private void filldatagrid()
        {
            trackingInf.GetWorkGroups();
            if (trackingInf.listtrackingds.workgroup.Rows.Count < 1)
            {
                wsgUtilities.wsgNotice("There are no active groups.");
                this.Close();
            }
        }

        public void CaptureWorkgroupKeyData()
        {
            SelectedWorkgroupId = trackingInf.CaptureIdCol(dataGridViewWorkgroups);
            trackingInf.GetSingleWorkGroup(SelectedWorkgroupId);
            SelectedWorkgroupName = trackingInf.trackingds.workgroup[0].groupname;
            this.Close();
        }

        private void buttonButtonCancel_Click(object sender, EventArgs e)
        {
            SelectedWorkgroupId = 0;
            this.Close();
        }

        private void dataGridViewWorkgroups_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureWorkgroupKeyData();
            }
        }

        private void dataGridViewWorkgroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureWorkgroupKeyData();
        }
    }
}