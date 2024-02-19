using CommonAppClasses;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Tracking
{
    public partial class FrmBatchTrackingRoutes : WSGUtilitieslib.Telemetry.Form
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Batch Tracking");
        public SqlConnection conn = new SqlConnection();
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private OleDbConnection vfpconn = new OleDbConnection();

        public FrmBatchTrackingRoutes()
        {
            InitializeComponent();
            // Create the VFP Connection String
            myAppconstants.VfpConnstring = myAppconstants.VfpConnstring + myAppconstants.MeycoPath;
            // Estabish the VFP Connection
            vfpconn.ConnectionString = myAppconstants.VfpConnstring;
            // Establish the SQL Connection string
            conn.ConnectionString = myAppconstants.SQLConnectionString;
            textBoxTrackingDate.Text = String.Format("{0:M/d/yyyy}", DateTime.Today);
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

        private int routeToStepid;

        public int RouteToStepid
        {
            get
            {
                return routeToStepid;
            }
            set
            {
                routeToStepid = value;
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

        private void buttonComment_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePickerTrackingDate_ValueChanged(object sender, EventArgs e)
        {
            textBoxTrackingDate.Text = String.Format("{0:M/d/yyyy}", dateTimePickerTrackingDate.Value);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxSono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                {
                    if (textBoxSono.Text != "") // check for blank sono
                                                // scanner sends an extra carriage return and that
                                                // causes a blank sono to appear
                    {
                        if (checksono(textBoxSono.Text.PadLeft(10, ' ')))
                        {
                            CurrentStepid = 0;
                            CurrentSono = textBoxSono.Text.PadLeft(10, ' ');
                            // Establish the SQL command and its parameters
                            DataTable dtlatestJtrak = new DataTable();
                            SqlCommand cmd = new SqlCommand("dbo.sp_getsorangetracking");
                            appUtilities.makeSQLCommand(ref cmd, ref conn);
                            cmd.Parameters.Add("@beginsono", SqlDbType.Char);
                            cmd.Parameters["@beginsono"].Value = CurrentSono;
                            cmd.Parameters.Add("@endsono", SqlDbType.Char);
                            cmd.Parameters["@endsono"].Value = CurrentSono;
                            try
                            {
                                conn.Open();
                                dtlatestJtrak.Load(cmd.ExecuteReader());
                                if (dtlatestJtrak.Rows.Count > 0)
                                {
                                    foreach (DataRow latestJtrakRow in dtlatestJtrak.Rows)
                                    {
                                        CurrentStepid = latestJtrakRow.Field<int>("stepid");
                                    }
                                }
                                else
                                {
                                    // Establish the SQL command to locate the INIT Step
                                    SqlCommand cmd2 = new SqlCommand("dbo.sp_getinitid");
                                    appUtilities.makeSQLCommand(ref cmd2, ref conn);
                                    DataTable dtInitID = new DataTable();
                                    try
                                    {
                                        dtInitID.Load(cmd2.ExecuteReader());
                                        if (dtInitID.Rows.Count > 0)
                                        {
                                            CurrentStepid = Convert.ToInt32(dtInitID.Rows[0]["idcol"]);
                                            conn.Close(); // Close the connection. It will opened in the routeSO module
                                        } // end if
                                        else
                                        {
                                            wsgUtilities.wsgNotice("There is no INIT Step.");
                                        } // end else
                                    }
                                    catch (Exception ex)
                                    {
                                        HandleException(ex);
                                        conn.Close();
                                    }
                                }
                                conn.Close();
                            }
                            catch (Exception ex)
                            {
                                HandleException(ex);
                                conn.Close();
                            }

                            // If a route has been selected, proceed
                            if (CurrentStepid != 0)
                            {
                                FrmRouteStepComment myFrmRouteStepComment = new FrmRouteStepComment();
                                myFrmRouteStepComment.CurrentRouteId = CurrentStepid;
                                myFrmRouteStepComment.CurrentSono = CurrentSono;
                                DateTime trackdate = Convert.ToDateTime(textBoxTrackingDate.Text);
                                string timestring = DateTime.Now.ToString("HH:mm:ss tt");
                                string datestring = trackdate.ToString("yyyy-MM-dd");
                                string datetimestring = datestring + " " + timestring;
                                DateTime MyDateTime = DateTime.ParseExact(datetimestring, "yyyy-MM-dd HH:mm:ss tt", null);
                                myFrmRouteStepComment.TrackDate = MyDateTime;
                                myFrmRouteStepComment.ShowDialog();
                            }
                            //
                            textBoxSono.Text = "";
                            textBoxSono.Focus();
                        } // (textBoxSono.Text != "")
                    }
                } // (textBoxSono.Text != "")
            } // (e.KeyCode == Keys.Return)
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
        }

        private void textBoxSono_TextChanged(object sender, EventArgs e)
        {
        } // check sono
    } // partial class
}// namespace