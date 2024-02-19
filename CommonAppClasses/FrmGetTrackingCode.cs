using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    //CACHED  Maintain -> Tracking Codes -> Select Step 
    public partial class FrmGetTrackingCode : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingTrackingCodes = new BindingSource();
        private SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Tracking Code Selector");

        private static TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");
        private static ObjectCache dataCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_TrackingCodes"]));

        public FrmGetTrackingCode()
        {
            InitializeComponent();
            BindingSource bindingTrackingCodes = new BindingSource();
            bindingTrackingCodes.DataSource = trackingInf.trackingds.step;
            dataGridViewTrackingCodes.DataSource = bindingTrackingCodes;
            // Set the DataGridView control's border.
            dataGridViewTrackingCodes.BorderStyle = BorderStyle.Fixed3D;

            // Fill the grid with data
            if (dataCache.IsInvalid)
            {
                filldatagrid();
                dataCache.Refresh(trackingInf);
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
            trackingInf.GetTrackingCodes();
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
            CaptureStepKeyData();
            this.Close();
        }

        private void dataGridViewTrackingCodes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)

            {
                CaptureStepKeyData();
                this.Close();
            }
        }
    }
}