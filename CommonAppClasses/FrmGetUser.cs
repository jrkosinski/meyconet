using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmGetUser : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingUserData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("User Maintenance");
        private MiscellaneousDataMethods miscdata = new MiscellaneousDataMethods("SQL", "SQLConnString");

        public FrmGetUser()
        {
            InitializeComponent();
            bindingUserData.DataSource = miscdata.listsystemds.appuser;
            dataGridViewUserData.DataSource = bindingUserData;
            dataGridViewUserData.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewUserData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewUserData.AutoGenerateColumns = false;
            dataGridViewUserData.Focus();
            filldatagrid();
        }

        public int SelectedUserId { get; set; }
        public bool ShowInactive { get; set; }

        private void filldatagrid()
        {
            miscdata.GetAppUsers(ShowInactive);
            if (miscdata.listsystemds.appuser.Rows.Count < 1)
            {
                wsgUtilities.wsgNotice("There are no active users.");
            }
        } // end filldatagrid

        public void CaptureUserId()
        {
            SelectedUserId = miscdata.CaptureIdCol(dataGridViewUserData);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SelectedUserId = 0;
            this.Close();
        } // end click cancel button

        private void dataGridViewUserData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureUserId();

            this.Close();
        }

        private void dataGridViewUserData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureUserId();
                this.Close();
            }
        } // End double click
    }
}