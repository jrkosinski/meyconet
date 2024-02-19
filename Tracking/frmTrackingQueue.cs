using CommonAppClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Tracking
{
    public partial class FrmTrackingQueue : WSGBaseClassLibrary.WSGFrmBase
    {
        // Establish the binding sources
        private BindingSource bindingTrackingCodes = new BindingSource();

        private BindingSource bindingTrackingQueue = new BindingSource();
        private BindingSource bindingTrackingActivity = new BindingSource();
        public SqlConnection conn = new SqlConnection();

        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private TrackingInf trackInf = new TrackingInf("SQL", "SQLConnString");
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Tracking Queue Processing");
        private OleDbConnection vfpconn = new OleDbConnection();
        private TrackingProcessing trackProc = new TrackingProcessing("SQL", "SQLConnString");

        public FrmTrackingQueue()
        {
            InitializeComponent();

            #region initialization

            // Set the DataGridView control's border.
            dataGridViewTrackingQueue.BorderStyle = BorderStyle.Fixed3D;

            conn.ConnectionString = myAppconstants.SQLConnectionString;

            // The value for alternating rows overrides the value for all rows.
            // Tracking datagridview
            dataGridViewTrackingQueue.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewTrackingQueue.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewTrackingQueue.AutoGenerateColumns = false;

            dataGridViewTrackingQueue.AutoGenerateColumns = false;
            bindingTrackingQueue.DataSource = trackInf.trackingds.view_latestsotrackingstepdata;
            dataGridViewTrackingQueue.DataSource = bindingTrackingQueue;
            dataGridViewTrackingQueue.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewTrackingQueue.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            // Tracking activity datagridview
            dataGridViewTrackingActivity.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewTrackingActivity.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewTrackingActivity.AutoGenerateColumns = false;

            // Create the VFP Connection String
            myAppconstants.VfpConnstring = myAppconstants.VfpConnstring + myAppconstants.MeycoPath;

            // Estabish the VFP Connection
            vfpconn.ConnectionString = myAppconstants.VfpConnstring;

            #endregion initialization
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Properties

        public int CurrentStepid { get; set; }
        public int RouteToStepid { get; set; }
        public int CurrentWorkgroupId { get; set; }
        public string CurrentWorkgroupName { get; set; }
        public int RoutedToStepid { get; set; }
        public string ExtractProcedure { get; set; }
        public string CurrentStepDescrip { get; set; }
        public string CurrentComment { get; set; }
        public string CurrentSono { get; set; }
        public string InspectionStep { get; set; }

        #endregion Properties

        #region methods

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            // User selects code to be processed
            FrmGetTrackingCode myfrmGetTrackingCode = new FrmGetTrackingCode();
            myfrmGetTrackingCode.ShowDialog();
            dataGridViewTrackingQueue.Visible = false;
            if (myfrmGetTrackingCode.SelectedCode != "Cancelled")
            {
                CurrentStepid = myfrmGetTrackingCode.SelectedId;
                CurrentWorkgroupId = 0;
                CurrentStepDescrip = myfrmGetTrackingCode.SelectedDescrip;
                this.Text = "Processing " + CurrentStepDescrip;
                if (LoadTrackingStepData() == true)
                {
                    labelSOCount.Text = "Active SO's " +
                    trackInf.trackingds.view_latestsotrackingstepdata.Rows.Count.ToString().Trim();
                    labelSOCount.Visible = true;
                    labelInstructions.Visible = true;
                    dataGridViewTrackingQueue.Visible = true;
                    buttonRefresh.Enabled = true;
                }
                else
                {
                    labelSOCount.Text = "";
                    labelInstructions.Visible = false;
                    wsgUtilities.wsgNotice("No matching SO records");
                }
            } // endif not cancelled
        } // end click

        public static DataSet AddTables(ref DataSet dSet, DataTable newTable)
        {
            dSet.Tables.Add(newTable);
            return dSet;
        }

        private void dataGridViewTrackingActivity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewTrackingQueue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureSoData();
        } // end cell click

        private void CaptureSoData()
        {
            CurrencyManager xCM =
      (CurrencyManager)dataGridViewTrackingQueue.BindingContext[dataGridViewTrackingQueue.DataSource,
         dataGridViewTrackingQueue.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;

            // Save the selected SO number
            CurrentSono = xRow["sono"].ToString();
            InspectionStep = xRow["inspection"].ToString();
            // Test for blank and skip it
            if (CurrentSono != "")
            {
                CurrentStepid = (Int32)xRow["stepid"];
                fillactivitygrid();
                dataGridViewTrackingActivity.Visible = true;
                buttonRoute.Visible = true;
                ButtonMainPdf.Visible = true;
                buttonPricePdf.Visible = true;
                buttonSuspendSO.Visible = true;
                buttonClear.Visible = true;
                labelComment.Visible = true;
            }
        } // end CaptureSoData()

        private void dataGridViewTrackingQueue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureSoData();
            }
        }

        private void buttonChooseWorkGroup_Click(object sender, EventArgs e)
        {
            // User selects code to be processed
            CommonAppClasses.FrmGetWorkgroup frmGetWorkGroup = new CommonAppClasses.FrmGetWorkgroup();
            frmGetWorkGroup.ShowDialog();
            if (frmGetWorkGroup.SelectedWorkgroupId != 0)
            {
                CurrentWorkgroupName = frmGetWorkGroup.SelectedWorkgroupName;
                CurrentWorkgroupId = frmGetWorkGroup.SelectedWorkgroupId;
                this.Text = "Processing " + CurrentWorkgroupName;
                if (LoadWorkGroupData(CurrentWorkgroupId))
                {
                    labelSOCount.Text = "Active SO's " +
                    trackInf.trackingds.view_latestsotrackingstepdata.Rows.Count.ToString().Trim();
                    labelSOCount.Visible = true;
                    labelInstructions.Visible = true;
                    dataGridViewTrackingQueue.Visible = true;
                    buttonRefresh.Enabled = true;
                }
                else
                {
                    labelSOCount.Text = "";
                    labelInstructions.Visible = false;
                    wsgUtilities.wsgNotice("No matching SO records");
                }
            }
        }

        private void dataGridViewTrackingQueue_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex <= trackInf.trackingds.view_latestsotrackingstepdata.Rows.Count - 1)
            {
                if (trackInf.trackingds.view_latestsotrackingstepdata[e.RowIndex].comment.TrimEnd() != "")
                {
                    this.dataGridViewTrackingQueue.Rows[e.RowIndex].Cells["ColumnSono"].Style.BackColor = Color.Red;
                    this.dataGridViewTrackingQueue.Rows[e.RowIndex].Cells["ColumnSono"].Style.SelectionBackColor = Color.DarkRed;
                }

                if (trackInf.SoHasTickets(trackInf.trackingds.view_latestsotrackingstepdata[e.RowIndex].sono))
                {
                    this.dataGridViewTrackingQueue.Rows[e.RowIndex].Cells["ColumnPoNum"].Style.BackColor = Color.Green;
                    this.dataGridViewTrackingQueue.Rows[e.RowIndex].Cells["ColumnPoNum"].Style.SelectionBackColor = Color.DarkGreen;
                }
            }
            if (e.RowIndex <= trackInf.trackingds.view_latestsotrackingstepdata.Rows.Count - 1)
            {
                if (trackInf.trackingds.view_latestsotrackingstepdata[e.RowIndex].lckstat.TrimEnd() != "")
                {
                    this.dataGridViewTrackingQueue.Rows[e.RowIndex].Cells["ColumnSodate"].Style.BackColor = Color.Green;
                    this.dataGridViewTrackingQueue.Rows[e.RowIndex].Cells["ColumnSodate"].Style.SelectionBackColor = Color.DarkGreen;
                    //        e.CellStyle.BackColor = Color.Red;
                    //       e.CellStyle.SelectionBackColor = Color.DarkRed;
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (CurrentWorkgroupId == 0)
            {
                LoadTrackingStepData();
            }
            else
            {
                LoadWorkGroupData(CurrentWorkgroupId);
            }
            labelSOCount.Text = "Active SO's " +
               trackInf.trackingds.view_latestsotrackingstepdata.Rows.Count.ToString().Trim();
        }

        private void routeSO()
        {
            DateTime Trackdate = DateTime.Now;
            // Route the Sales Order to the designated step - RoutedToStepID
            SqlCommand cmd = new SqlCommand("dbo.sp_inserttrackingevent");
            appUtilities.makeSQLCommand(ref cmd, ref conn);
            cmd.Parameters.Add("@stepid", SqlDbType.Int);
            cmd.Parameters["@stepid"].Value = RouteToStepid;
            cmd.Parameters.Add("@sono", SqlDbType.Char);
            cmd.Parameters["@sono"].Value = CurrentSono;
            cmd.Parameters.Add("@trackdate", SqlDbType.DateTime);
            cmd.Parameters["@trackdate"].Value = Trackdate;
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
                LoadTrackingStepData();
                wsgUtilities.wsgNotice("Routing complete");
            }
            catch (Exception ex)
            {
                conn.Close();
                HandleException(ex);
            } // end catch
        } // routeso

        #region fillactivitygrid

        public void fillactivitygrid()
        {
            // Establish the SQL command and its parameters
            SqlCommand cmd = new SqlCommand("dbo.sp_getsotracking");
            appUtilities.makeSQLCommand(ref cmd, ref conn);
            cmd.Parameters.Add("@sono", SqlDbType.Char);
            cmd.Parameters["@sono"].Value = CurrentSono;
            DataTable dtsoTrak = new DataTable();

            try
            {
                conn.Open();
                dtsoTrak.Load(cmd.ExecuteReader());

                if (dtsoTrak.Rows.Count > 0)
                {
                    bindingTrackingActivity.DataSource = dtsoTrak;
                    dataGridViewTrackingActivity.DataSource = bindingTrackingActivity;
                    dataGridViewTrackingActivity.Visible = true;
                    labelComment.Visible = true;
                    dataGridViewTrackingActivity.Visible = true;
                    dataGridViewTrackingActivity.Focus();
                } // end if
                else
                {
                    wsgUtilities.wsgNotice("No matching activity records");
                    this.Close();
                } // end else
                conn.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                conn.Close();
            }
        } // end fillactivitygrid()

        #endregion fillactivitygrid

        private void buttonRoute_Click(object sender, EventArgs e)
        {
            FrmRouteStepComment myFrmRouteStepComment = new FrmRouteStepComment();
            myFrmRouteStepComment.CurrentRouteId = CurrentStepid;
            myFrmRouteStepComment.CurrentSono = CurrentSono;
            string timestring = DateTime.Now.ToString("HH:mm:ss tt");
            string datestring = DateTime.Now.ToString("yyyy-MM-dd");
            string datetimestring = datestring + " " + timestring;
            DateTime MyDateTime = DateTime.ParseExact(datetimestring, "yyyy-MM-dd HH:mm:ss tt", null);
            myFrmRouteStepComment.TrackDate = MyDateTime;
            myFrmRouteStepComment.ShowDialog();
            LoadTrackingStepData();
            dataGridViewTrackingActivity.Visible = false;
            ButtonMainPdf.Visible = false;
            buttonPricePdf.Visible = false;
            buttonSuspendSO.Visible = false;
            buttonClear.Visible = false;
            buttonRoute.Visible = false;
            labelComment.Visible = false;
        } // end buttonRoute_Click

        private void dataGridViewTrackingActivity_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (IsRoleAuthorized("TRAD") == true)
                {
                    if (wsgUtilities.wsgReply("Cancel this item?"))
                    {
                        CurrencyManager xCM =
                         (CurrencyManager)dataGridViewTrackingActivity.BindingContext[dataGridViewTrackingActivity.DataSource,
                           dataGridViewTrackingActivity.DataMember];
                        DataRowView xDRV = (DataRowView)xCM.Current;
                        DataRow xRow = xDRV.Row;
                        // Save the select SO number

                        int thisidcol = (Int32)xRow["idcol"];

                        SqlCommand cmd = new SqlCommand("dbo.sp_canceltrackingevent");
                        appUtilities.makeSQLCommand(ref cmd, ref conn);
                        cmd.Parameters.Add("@idcol", SqlDbType.Int);
                        cmd.Parameters["@idcol"].Value = (Int32)xRow["idcol"];
                        cmd.Parameters.Add("@userid", SqlDbType.Char);
                        cmd.Parameters["@userid"].Value = AppUserClass.AppUserId;
                        conn.Open();
                        try
                        {
                            WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(cmd);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            fillactivitygrid();
                            this.Update();
                            wsgUtilities.wsgNotice("Tracking Event Cancelled");
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            HandleException(ex);
                        } // end catch
                    } // end if
                } // endif authorized
            } // end if (e.Button == MouseButtons.Right)
        } // end dataGridViewTrackingActivity_MouseUp

        private void dataGridViewTrackingActivity_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewTrackingActivity.Columns[e.ColumnIndex].Name.Equals("ColumnActivityComment"))
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString().Trim() != "")
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.SelectionBackColor = Color.DarkRed;
                    }
                }
            }
        } // end dataGridViewTrackingActivity_CellFormatting

        private void dataGridViewTrackingActivity_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CurrencyManager xCM =
      (CurrencyManager)dataGridViewTrackingActivity.BindingContext[dataGridViewTrackingActivity.DataSource,
      dataGridViewTrackingActivity.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            frmMaintainComment myfrmMaintComment = new frmMaintainComment();
            myfrmMaintComment.CommentText = xRow["comment"].ToString();
            myfrmMaintComment.CurrentIdcol = (Int32)xRow["idcol"];
            myfrmMaintComment.ShowDialog();
            fillactivitygrid();
        } //end dataGridViewTrackingActivity_CellContentDoubleClick

        private void buttonClear_Click(object sender, EventArgs e)
        {
            dataGridViewTrackingActivity.Visible = false;
            ButtonMainPdf.Visible = false;
            buttonPricePdf.Visible = false;
            buttonSuspendSO.Visible = false;
            buttonClear.Visible = false;
            buttonRoute.Visible = false;
            labelComment.Visible = false;
            buttonRefresh.Enabled = false;
        }

        private void buttonMainPdf_Click(object sender, EventArgs e)
        {
            // Locate the pdf for this SO

            DirectoryInfo pdfdir = new DirectoryInfo(ConfigurationManager.AppSettings["PDFSTORAGEPath"]);

            string sono = CurrentSono.TrimStart().TrimEnd();
            FileInfo[] pdffiles = pdfdir.GetFiles("*" + sono + "*.pdf");

            if (pdffiles.Length > 0)
                foreach (FileInfo f in pdffiles)
                {
                    Process.Start(f.FullName);
                } // end foreach
            else
                wsgUtilities.wsgNotice($"There are no PDFs for {sono} found at {pdfdir.FullName}");
        }

        private void buttonPricePdf_Click(object sender, EventArgs e)
        {
            // Locate the pdf for this SO
            string sono = CurrentSono.TrimStart().TrimEnd();
            DirectoryInfo pdfdir = new DirectoryInfo(ConfigurationManager.AppSettings["SOPDFPath"]);
            FileInfo[] pdffiles = pdfdir.GetFiles("*" + sono + "*.pdf");

            if (pdffiles.Length > 0)
                foreach (FileInfo f in pdffiles)
                {
                    Process.Start(f.FullName);
                } // end foreach
            else
                wsgUtilities.wsgNotice($"There are no PDFs for {sono} found at {pdfdir.FullName}");
        }

        private void buttonSuspendSO_Click(object sender, EventArgs e)
        {
            // Establish the SQL command to locate the Suspend Step
            SqlCommand cmd = new SqlCommand("dbo.sp_getsuspendstepid");
            appUtilities.makeSQLCommand(ref cmd, ref conn);
            DataTable dtsoSuspendID = new DataTable();

            try
            {
                conn.Open();
                dtsoSuspendID.Load(cmd.ExecuteReader());

                if (dtsoSuspendID.Rows.Count > 0)
                {
                    RouteToStepid = Convert.ToInt32(dtsoSuspendID.Rows[0]["idcol"]);
                    conn.Close(); // Close the connection. It will opened in the routeSO module
                    routeSO();
                    dataGridViewTrackingActivity.Visible = false;
                    ButtonMainPdf.Visible = false;
                    buttonPricePdf.Visible = false;

                    buttonSuspendSO.Visible = false;
                    buttonClear.Visible = false;
                    buttonRoute.Visible = false;
                    labelComment.Visible = false;
                } // end if
                else
                {
                    wsgUtilities.wsgNotice("There is no Suspend Step.");
                } // end else
            }
            catch (Exception ex)
            {
                HandleException(ex);
                conn.Close();
            }
        } // end button Suspend SO click

        private bool IsRoleAuthorized(string requiredrole)
        {
            SqlCommand cmd = new SqlCommand("dbo.sp_checkuserrole");
            appUtilities.makeSQLCommand(ref cmd, ref conn);
            cmd.Parameters.Add("@userid", SqlDbType.Char);
            cmd.Parameters["@userid"].Value = AppUserClass.AppUserId;
            cmd.Parameters.Add("@requiredrole", SqlDbType.Char);
            cmd.Parameters["@requiredrole"].Value = requiredrole;
            SqlParameter RoleMessage = new SqlParameter("@RoleMessage", SqlDbType.Char, 20);
            RoleMessage.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(RoleMessage);

            conn.Open();
            try
            {
                WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();

                if (cmd.Parameters["@RoleMessage"].Value.ToString().Trim() == "OK")
                {
                    return true;
                }
                else
                {
                    wsgUtilities.wsgNotice(cmd.Parameters["@RoleMessage"].Value.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                HandleException(ex);
                return false;
            }
        }

        #endregion methods

        public bool LoadWorkGroupData(int workgroupid)
        {
            bool DataFound = true;
            trackInf.GetLatestTrackingStepsbyWorkgroupId(workgroupid);
            if (trackInf.trackingds.view_latestsotrackingstepdata.Rows.Count < 1)
            {
                DataFound = false;
            }
            return DataFound;
        }

        public bool LoadTrackingStepData()
        {
            bool DataFound = true;
            trackInf.GetLatestTrackingStepsbyStepid(CurrentStepid);
            if (trackInf.trackingds.view_latestsotrackingstepdata.Rows.Count < 1)
            {
                DataFound = false;
            }
            return DataFound;
        }

        private void dataGridViewTrackingQueue_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CaptureSoData();
                trackProc.ShowSO(CurrentSono, InspectionStep, this.MdiParent);
            }
        }
    } // end class
}