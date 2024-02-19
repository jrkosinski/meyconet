using CommonAppClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Tracking
{
    //CACHED Tracking -> Queue Proceessing -> Choose Tracking Code
    public partial class FrmGetTrackingCode : WSGBaseClassLibrary.WSGFrmBase
    {
        private static BindingSource bindingTrackingCodes = null;
        private static ObjectCache dataCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_TrackingCodes"]));

        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null; //TODO: Why all lowercase? 
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Tracking Code Selector");

        public FrmGetTrackingCode()
        {
            InitializeComponent();

            // Set the DataGridView control's border.
            dataGridViewTrackingCodes.BorderStyle = BorderStyle.Fixed3D;

            conn.ConnectionString = myAppconstants.SQLConnectionString;

            if (dataCache.IsInvalid)
            {
                // Fill the grid with data
                filldatagrid();
            }
            else
            {
                dataGridViewTrackingCodes.DataSource = bindingTrackingCodes;
            }

            // The value for alternating rows overrides the value for all rows.
            dataGridViewTrackingCodes.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewTrackingCodes.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewTrackingCodes.Focus();
        } // end constructor

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SelectedCode = "Cancelled";
            this.Close();
        }

        private void FrmGetTrackingCode_Load(object sender, EventArgs e)
        {
        }

        private void filldatagrid()
        {
            bindingTrackingCodes = new BindingSource();
            DataTable dtTrackingCodes = new DataTable();
            SqlCommand cmd = new SqlCommand("dbo.sp_gettrackingcode");
            appUtilities.makeSQLCommand(ref cmd, ref conn);

            try
            {
                conn.Open();
                dtTrackingCodes.Load(cmd.ExecuteReader());
                conn.Close();
                bindingTrackingCodes.DataSource = dtTrackingCodes;
                dataGridViewTrackingCodes.DataSource = bindingTrackingCodes;
                dataCache.Refresh(bindingTrackingCodes);
            }
            catch (Exception ex)
            {
                conn.Close();
                HandleException(ex);
            }
        }

        private void dataGridViewTrackingCodes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureStepKeyData();

            this.Close();
        }

        private string selectedCode;

        public string SelectedCode
        {
            get
            {
                return selectedCode;
            }
            set
            {
                selectedCode = value;
            }
        }

        private string selectedDescrip;

        public string SelectedDescrip
        {
            get
            {
                return selectedDescrip;
            }
            set
            {
                selectedDescrip = value;
            }
        }

        private int selectedId;

        public int SelectedId
        {
            get
            {
                return selectedId;
            }
            set
            {
                selectedId = value;
            }
        }

        public void CaptureStepKeyData()
        {
            CurrencyManager xCM =
      (CurrencyManager)dataGridViewTrackingCodes.BindingContext[dataGridViewTrackingCodes.DataSource,
           dataGridViewTrackingCodes.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Save the select SO number

            SelectedCode = xRow["code"].ToString();
            SelectedDescrip = xRow["descrip"].ToString();
            SelectedId = (int)xRow["idcol"];
        }

        private void dataGridViewTrackingCodes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewTrackingCodes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)

            {
                CaptureStepKeyData();
                this.Close();
            }
        }

        private void dataGridViewTrackingCodes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}