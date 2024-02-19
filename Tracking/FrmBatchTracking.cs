using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Tracking
{
    public partial class FrmBatchTracking : WSGUtilitieslib.Telemetry.Form
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Batch Tracking");
        public SqlConnection conn = new SqlConnection();
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private OleDbConnection vfpconn = new OleDbConnection();

        public FrmBatchTracking()
        {
            InitializeComponent();

            // Create the VFP Connection String
            myAppconstants.VfpConnstring = myAppconstants.VfpConnstring + myAppconstants.MeycoPath;
            // Estabish the VFP Connection
            vfpconn.ConnectionString = myAppconstants.VfpConnstring; //TODO: not used?
            // Establish the SQL Connection string
            conn.ConnectionString = myAppconstants.SQLConnectionString;
            textBoxTrackingDate.Text = String.Format("{0:M/d/yyyy}", DateTime.Today);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int currentStepid;

        public int CurrentStepid
        {
            get
            {
                return currentStepid;
            }
            set
            {
                currentStepid = value;
            }
        }

        private string currentStepDescrip;

        public string CurrentStepDescrip
        {
            get
            {
                return currentStepDescrip;
            }
            set
            {
                currentStepDescrip = value;
            }
        }

        private string currentSono;

        public string CurrentSono
        {
            get
            {
                return currentSono;
            }
            set
            {
                currentSono = value;
            }
        }

        private void buttonSelectTrackingStep_Click(object sender, EventArgs e)
        {
            // User selects code to be processed
            FrmGetTrackingCode myfrmGetTrackingCode = new FrmGetTrackingCode();
            myfrmGetTrackingCode.ShowDialog();
            if (myfrmGetTrackingCode.SelectedCode != "Cancelled")
            {
                CurrentStepid = myfrmGetTrackingCode.SelectedId;
                CurrentStepDescrip = myfrmGetTrackingCode.SelectedDescrip;
                this.Text = "Processing " + CurrentStepDescrip;
            } // endif
        }

        private void dateTimePickerShipFirstDate_ValueChanged(object sender, EventArgs e)
        {
            textBoxTrackingDate.Text = String.Format("{0:M/d/yyyy}", dateTimePickerTrackingDate.Value);
        } // value changed

        private void routeSO()
        {
            // Route the Sales Order to the designated step - CurrentStepID
            DateTime Trackdate = Convert.ToDateTime(textBoxTrackingDate.Text);
            string timestring = DateTime.Now.ToString("HH:mm:ss tt");
            string datestring = Trackdate.ToString("yyyy-MM-dd");
            string datetimestring = datestring + " " + timestring;
            DateTime TrackdateTime = DateTime.ParseExact(datetimestring, "yyyy-MM-dd HH:mm:ss tt", null);
            SqlCommand cmd = new SqlCommand("dbo.sp_inserttrackingevent");
            appUtilities.makeSQLCommand(ref cmd, ref conn);
            cmd.Parameters.Add("@stepid", SqlDbType.Int);
            cmd.Parameters["@stepid"].Value = CurrentStepid;
            cmd.Parameters.Add("@sono", SqlDbType.Char);
            cmd.Parameters["@sono"].Value = CurrentSono;
            cmd.Parameters.Add("@trackdate", SqlDbType.DateTime);
            cmd.Parameters["@trackdate"].Value = TrackdateTime;
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar);
            cmd.Parameters["@comment"].Value = "";
            cmd.Parameters.Add("@userid", SqlDbType.Char);
            cmd.Parameters["@userid"].Value = AppUserClass.AppUserId;
            conn.Open();
            try
            {
                WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
                this.Update();
                wsgUtilities.wsgNotice("Routing complete");
            }
            catch (Exception ex)
            {
                conn.Close();
                HandleException(ex);
            } // end catch
        }

        public bool checksono(string sono)
        {
            // Establish the somast query
            DataTable dtSomast = new DataTable();
            SqlCommand cmdSomast = new SqlCommand("dbo.wsgsp_getview_somastdatabysono");
            appUtilities.makeSQLCommand(ref cmdSomast, ref conn);
            cmdSomast.Parameters.Add("@sono", SqlDbType.Char);
            cmdSomast.Parameters["@sono"].Value = sono;
            try
            {
                conn.Open();
                dtSomast.Load(cmdSomast.ExecuteReader());
                conn.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                conn.Close();
            }

            if (dtSomast.Rows.Count == 0)
            {
                MessageBox.Show("Sales Order " + sono + " not found");
                return false;
            }
            else
            {
                return true;
            }
        } // check sono

        private void textBoxSono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (CurrentStepid != 0)
                {
                    if (textBoxSono.Text != "") // check for blank sono
                                                // scanner sends an extra carriage return and that
                                                // causes a blank sono to appear
                    {
                        if (checksono(textBoxSono.Text.PadLeft(10, ' ')))
                        {
                            CurrentSono = textBoxSono.Text.PadLeft(10, ' ');
                            routeSO();
                            textBoxSono.Text = "";
                            textBoxSono.Focus();
                        }
                    }
                }
                else
                {
                    textBoxSono.Text = "";
                    wsgUtilities.wsgNotice("Please select a routing step");
                } // endif
            } //  (e.KeyCode == Keys.Return)
        }
    } // form
} // namespace