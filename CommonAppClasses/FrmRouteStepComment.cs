using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmRouteStepComment : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingRouteData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        public quote quoteds = new quote();
        public tracking trackingds = new tracking();
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Step Routing - Comments");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");
        private MiscellaneousDataMethods miscdatamethods = new MiscellaneousDataMethods("SQL", "SQLConnString");

        public FrmRouteStepComment()
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
            bindingRouteData.DataSource = trackingInf.trackingds.view_routedata;
            dataGridViewRouteData.DataSource = bindingRouteData;
            dateTimePickerTrackdate.Value = DateTime.Now;
        }

        public int CurrentRouteId { get; set; }
        public DateTime TrackDate { get; set; }
        public int SelectedStepId { get; set; }
        public int RouteToStepId { get; set; }
        public string CurrentSono { get; set; }

        public void CaptureStepKeyData()
        {
            CurrencyManager xCM =
           (CurrencyManager)dataGridViewRouteData.BindingContext[dataGridViewRouteData.DataSource,
           dataGridViewRouteData.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Save the selected step id
            RouteToStepId = (int)xRow["stepid"];
            routeSO();
            this.Close();
        }

        private void filldatagrid()
        {
            bool custstep = false;
            trackingInf.trackingds.view_routedata.Rows.Clear();
            // See if this customer has any specified steps for this route
            string custno = miscdatamethods.GetSoCustno(CurrentSono);
            if (custno.TrimEnd() != "")
            {
                trackingInf.GetCustomerRouteSteps(custno, CurrentRouteId);
                if (trackingInf.trackingds.view_routedata.Rows.Count > 0)
                {
                    custstep = true;
                }
            }
            if (!custstep)
            {
                trackingInf.GetNonCustomRouteData(CurrentRouteId);
            }
            // Check for steps that require that the SO be invoiced.
            // Those steps will removed if sostat != "C"
            trackingInf.CheckInvoiceSteps(CurrentSono);
            if (trackingInf.trackingds.view_routedata.Rows.Count > 0)
            {
                dataGridViewRouteData.Visible = true;
                dataGridViewRouteData.Focus();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no next steps for this step.");
                dataGridViewRouteData.Visible = false;
            }
        } // end filldatagrid

        private void routeSO()
        {
            trackingInf.routeSO(RouteToStepId, CurrentSono, textBoxComment.Text, dateTimePickerTrackdate.Value);
        }

        private void dataGridViewRouteData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureStepKeyData();
        }

        private void dataGridViewRouteData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureStepKeyData();
                this.Close();
            }
        }

        private void FrmRouteStepComment_Shown(object sender, EventArgs e)
        {
            filldatagrid();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}