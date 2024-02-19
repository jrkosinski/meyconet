using CommonAppClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Tracking
{
    public partial class FrmTrackingSearch : WSGBaseClassLibrary.WSGFrmBase
    {
        //CACHED  Tracking -> Sales Order Tracking
        private static BindingSource bindingSourceTrackingSearch = new BindingSource();

        public string IncludeScope = "All";
        public SqlConnection conn = new SqlConnection();
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Tracking Search");
        private TrackingProcessing trackProc = new TrackingProcessing("SQL", "SQLConnString");
        private TrackingInf trackInf = new TrackingInf("SQL", "SQLConnString");
        private BindingSource bindingSourceTrackingActivity = new BindingSource();
        private static ObjectCacheWithParams dataCache = new ObjectCacheWithParams(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_TrackingActivity"]));

        private OleDbConnection vfpconn = new OleDbConnection();

        public FrmTrackingSearch()
        {
            trackProc.sosearchForm = this;
            InitializeComponent();

            #region initialization

            // Set the screen size
            // appUtilities.setScreenSize(this);
            dateTimePickerShipFirstDate.Value = DateTime.Now.AddDays(-365);
            dateTimePickerShipLastDate.Value = DateTime.Now.AddDays(365);

            // The value for alternating rows overrides the value for all rows.
            dataGridViewTrackingSearch.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewTrackingSearch.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewTrackingSearch.AutoGenerateColumns = false;
            dataGridViewTrackingSearch.Visible = false;
            labelInstructions.Visible = false;

            if (!dataCache.IsInvalid)
            {
                dataGridViewTrackingSearch.DataSource = bindingSourceTrackingSearch;
                this.dataGridViewTrackingSearch.Focus();
                this.dataGridViewTrackingSearch.Enabled = true;
                dataGridViewTrackingSearch.Visible = true;
                clearsodata();
            }

            dataGridViewTrackingActivity.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewTrackingActivity.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewTrackingActivity.AutoGenerateColumns = false;
            dataGridViewTrackingActivity.Visible = false;
            listBoxInclude.SelectedItem = "All";

            // Clear the search boxes
            initializeSearchBoxes();

            #endregion initialization


            this.SetTabOrder();
        }

        #region methods

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region buttonSearch_Click

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // Clear the search boxes, grid and buttons
            buttonSuspendSO.Visible = false;
            buttonMainPdf.Visible = false;
            buttonPricePdf.Visible = false;

            dataCache.SearchParams["ponum"] = textBoxPonum.Text;
            dataCache.SearchParams["invno"] = textBoxInvno.Text;
            dataCache.SearchParams["sono"] = textBoxSono.Text;
            dataCache.SearchParams["meycono"] = textBoxMeycono.Text;
            dataCache.SearchParams["lname"] = textBoxLname.Text;
            dataCache.SearchParams["custno"] = textBoxCustno.Text;
            dataCache.SearchParams["shpdate"] = textBoxShipFirstDate.Text;

            // Clear the activity grid
            dataGridViewTrackingActivity.Visible = false;
            trackProc.SearchSoTracking();
            if (trackProc.trackingds.view_latestsotrackingstepdata.Rows.Count < 1)
                wsgUtilities.wsgNotice("No Matching Records");
            else
            {
                // Remove any unwanted rows
                for (int i = 0; i < trackProc.trackingds.view_latestsotrackingstepdata.Rows.Count - 1; i++)
                {
                    if (this.listBoxInclude.SelectedItem.ToString().Trim() != "All")
                    {
                        if (this.listBoxInclude.SelectedItem.ToString().Trim() == "Estimates Only")
                        {
                            if (trackProc.trackingds.view_latestsotrackingstepdata[i].sotype != "B")
                            {
                                trackProc.trackingds.view_latestsotrackingstepdata.Rows[i].Delete();
                                continue;
                            }
                        }
                        else
                        {
                            if (trackProc.trackingds.view_latestsotrackingstepdata[i].sotype == "B")
                            {
                                trackProc.trackingds.view_latestsotrackingstepdata.Rows[i].Delete();
                                continue;
                            }
                        }
                    }
                } // for each
                trackProc.trackingds.view_latestsotrackingstepdata.AcceptChanges();

                // Set focus to the datagridview and enable it
                dataGridViewTrackingSearch.Visible = true;
                labelInstructions.Visible = true;
                bindingSourceTrackingSearch.DataSource = trackProc.trackingds.view_latestsotrackingstepdata;
                dataGridViewTrackingSearch.DataSource = bindingSourceTrackingSearch;
                dataGridViewTrackingActivity.DataSource = bindingSourceTrackingActivity;
                this.dataGridViewTrackingSearch.Focus();
                this.dataGridViewTrackingSearch.Enabled = true;
                dataCache.Refresh(bindingSourceTrackingSearch);
                clearsodata();
            } // (dtSomast.Rows.Count == 0)
        } // end buttonSearch_Click

        #endregion buttonSearch_Click

        private void textBoxCustno_Leave(object sender, EventArgs e)
        {
            this.textBoxCustno.Text = this.textBoxCustno.Text.ToUpper();
        }

        public static DataSet AddTables(ref DataSet dSet, DataTable newTable)
        {
            dSet.Tables.Add(newTable);
            return dSet;
        }

        #region dataGridViewTrackingSearch_CellClick

        private void dataGridViewTrackingSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureSono();
            fillactivitygrid();
        } // end cell click

        #endregion dataGridViewTrackingSearch_CellClick

        private void dateTimePickerSoDate_ValueChanged(object sender, EventArgs e)
        {
            textBoxShipFirstDate.Text = String.Format("{0:M/d/yyyy}", dateTimePickerShipFirstDate.Value);
        }

        private void initializeSearchBoxes()
        {
            //Initialize search boxes
            textBoxPonum.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["ponum"];
            textBoxInvno.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["invno"];
            textBoxSono.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["sono"];
            textBoxMeycono.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["meycono"];
            textBoxLname.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["lname"];
            textBoxCustno.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["custno"];
            textBoxShipFirstDate.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["shpdate"];
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Clear the search boxes, grid and buttons
            initializeSearchBoxes();
            dataGridViewTrackingSearch.Visible = false;
            labelInstructions.Visible = false;
            clearsodata();
        }

        private void textBoxShipFirstDate_DoubleClick(object sender, EventArgs e)
        {
            textBoxShipFirstDate.Text = "";
        }

        private void textBoxShipLastDate_DoubleClick(object sender, EventArgs e)
        {
            textBoxShipLastDate.Text = "";
        }

        private void dateTimePickerShipLastDate_ValueChanged(object sender, EventArgs e)
        {
            textBoxShipLastDate.Text = String.Format("{0:M/d/yyyy}", dateTimePickerShipLastDate.Value);
        }

        private void buttonSuspendSO_Click(object sender, EventArgs e)
        {
            trackInf.SuspendSO(Currentso);
        }

        private void buttonMainPdf_Click(object sender, EventArgs e)
        {
            // Locate the pdf for this SO
            Task.Run(async () =>
            {
                if (!await PdfStorage.OpenPdfs("*" + Currentso.TrimStart().TrimEnd() + "*.pdf", "PDFSTORAGEPath"))
                    wsgUtilities.wsgNotice($"There are no PDFs for {Currentso.TrimStart().TrimEnd()} found at {ConfigurationManager.AppSettings["PDFSTORAGEPath"]}");
            });
        }

        #endregion methods

        #region properties

        // Establish Currentso property
        public string Currentso { get; set; }

        #endregion properties

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]{
                this.textBoxSono,
                this.textBoxCustno,
                this.textBoxPonum,
                this.textBoxInvno,
                this.textBoxMeycono,
                this.textBoxLname,
                this.dateTimePickerShipFirstDate,
                this.textBoxShipFirstDate,
                this.dateTimePickerShipLastDate,
                this.textBoxShipLastDate,
                this.buttonSearch,
                this.buttonCancel,
                this.buttonClear
            });
        }

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
        }

        private void fillactivitygrid()
        {
            trackInf.GetSoTrackingSteps(Currentso);
            buttonSuspendSO.Visible = true;
            buttonMainPdf.Visible = true;
            buttonPricePdf.Visible = true;

            if (trackInf.trackingds.view_trackingstepdata.Rows.Count > 0)
            {
                labelComment.Visible = true;
                bindingSourceTrackingActivity.DataSource = trackInf.trackingds.view_trackingstepdata;
                dataGridViewTrackingActivity.DataSource = bindingSourceTrackingActivity;
                dataGridViewTrackingActivity.Visible = true;
                this.dataGridViewTrackingActivity.Focus();
            } // end if
            else
            {
                labelComment.Visible = false;
                dataGridViewTrackingActivity.Visible = false;
                wsgUtilities.wsgNotice("No matching records");
            } // end else
        } // end fillactivitygrid

        private void clearsodata()
        {
            dataGridViewTrackingActivity.Visible = false;
            buttonSuspendSO.Visible = false;
            buttonMainPdf.Visible = false;
            buttonPricePdf.Visible = false;
            labelComment.Visible = false;
        }

        private void dataGridViewTrackingActivity_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewTrackingActivity.Columns[e.ColumnIndex].Name.Equals("ColumnComment"))
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
        }

        private void dataGridViewTrackingSearch_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex <= trackProc.trackingds.view_latestsotrackingstepdata.Rows.Count - 1)
            {
                if (trackProc.trackingds.view_latestsotrackingstepdata[e.RowIndex].comment.TrimEnd() != "")
                {
                    this.dataGridViewTrackingSearch.Rows[e.RowIndex].Cells["ColumnSono"].Style.BackColor = Color.Red;
                    this.dataGridViewTrackingSearch.Rows[e.RowIndex].Cells["ColumnSono"].Style.SelectionBackColor = Color.DarkRed;
                }

                if (trackInf.SoHasTickets(trackProc.trackingds.view_latestsotrackingstepdata[e.RowIndex].sono))
                {
                    this.dataGridViewTrackingSearch.Rows[e.RowIndex].Cells["ColumnPonum"].Style.BackColor = Color.Green;
                    this.dataGridViewTrackingSearch.Rows[e.RowIndex].Cells["ColumnPonum"].Style.SelectionBackColor = Color.DarkGreen;
                }
            }
        }

        private void buttonPricePdf_Click(object sender, EventArgs e)
        {
            // Locate the pdf for this SO
            Task.Run(async () =>
            {
                if (!await PdfStorage.OpenPdfs("*" + Currentso.TrimStart().TrimEnd() + "*.pdf"))
                    wsgUtilities.wsgNotice($"There are no PDFs for {Currentso.TrimStart().TrimEnd()} found at {ConfigurationManager.AppSettings["SOPDFPath"]}");
            });
        }

        private void dataGridViewTrackingSearch_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CaptureSono();
                trackProc.ShowSO(Currentso, "N", this.MdiParent);
            }
        }

        private void CaptureSono()
        {
            CurrencyManager xCM =
          (CurrencyManager)dataGridViewTrackingSearch.BindingContext[dataGridViewTrackingSearch.DataSource,
               dataGridViewTrackingSearch.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Save the select SO number
            Currentso = xRow["sono"].ToString();
        }

        private void listBoxInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeScope = listBoxInclude.SelectedItem.ToString().TrimEnd();
        }
    }// end form class
}// end name space