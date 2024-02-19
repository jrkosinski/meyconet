using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Tracking
{
    public partial class FrmGetNextRouteStep : WSGBaseClassLibrary.WSGFrmBase
    {
        private BindingSource bindingRouteData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Route Maintenance");

        public FrmGetNextRouteStep()
        {
            InitializeComponent();
            dataGridViewRouteData.BorderStyle = BorderStyle.Fixed3D;
            conn.ConnectionString = myAppconstants.SQLConnectionString;

            // The value for alternating rows overrides the value for all rows.
            dataGridViewRouteData.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewRouteData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewRouteData.AutoGenerateColumns = false;
            dataGridViewRouteData.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewRouteData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int currentRouteId;

        public int CurrentRouteId
        {
            get
            {
                return currentRouteId;
            }
            set
            {
                currentRouteId = value;
            }
        }

        private int selectedStepId;

        public int SelectedStepId
        {
            get
            {
                return selectedStepId;
            }
            set
            {
                selectedStepId = value;
            }
        }

        private void filldatagrid()
        {
            DataTable dtRouteData = new DataTable();
            SqlCommand cmd = new SqlCommand("dbo.sp_getroutedata");
            appUtilities.makeSQLCommand(ref cmd, ref conn);
            cmd.Parameters.Add("@route", SqlDbType.Int);
            cmd.Parameters["@route"].Value = CurrentRouteId;

            try
            {
                conn.Open();
                dtRouteData.Load(cmd.ExecuteReader());
                conn.Close();
                if (dtRouteData.Rows.Count > 0)
                {
                    bindingRouteData.DataSource = dtRouteData;
                    dataGridViewRouteData.DataSource = bindingRouteData;

                    // The value for alternating rows overrides the value for all rows.
                    dataGridViewRouteData.Visible = true;
                    dataGridViewRouteData.RowsDefaultCellStyle.BackColor = Color.LightGray;
                    dataGridViewRouteData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
                    dataGridViewRouteData.Focus();
                }
                else
                {
                    wsgUtilities.wsgNotice("There are no next steps for this step.");
                    dataGridViewRouteData.Visible = false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                HandleException(ex);
            }
        } // end filldatagrid

        private void FrmGetNextRouteStep_Shown(object sender, EventArgs e)
        {
            filldatagrid();
        }

        public void CaptureStepKeyData()
        {
            CurrencyManager xCM =
      (CurrencyManager)dataGridViewRouteData.BindingContext[dataGridViewRouteData.DataSource,
           dataGridViewRouteData.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Save the selected step id
            SelectedStepId = (int)xRow["stepid"];
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.SelectedStepId = 0;
            this.Close();
        }

        private void dataGridViewRouteData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureStepKeyData();
            this.Close();
        }

        private void dataGridViewRouteData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureStepKeyData();
                this.Close();
            }
        }

        private void FrmGetNextRouteStep_Load(object sender, EventArgs e)
        {
        }
    }
}