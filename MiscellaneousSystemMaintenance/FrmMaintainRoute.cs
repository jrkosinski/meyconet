using CommonAppClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public partial class FrmMaintainRoute : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingRouteData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Route Maintenance");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");

        public FrmMaintainRoute()
        {
            InitializeComponent();
            buttonInsert.Enabled = false;
            CurrentState = "Select";
            bindingRouteData.DataSource = trackingInf.trackingds.view_routedata;
            dataGridViewRouteData.DataSource = bindingRouteData;
            dataGridViewRouteData.AutoGenerateColumns = false;
            dataGridViewRouteData.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewRouteData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        private void buttonSelectPhase_Click(object sender, EventArgs e)
        {
            // User selects Route to be processed
            FrmGetTrackingCode myfrmGetTrackingCode = new FrmGetTrackingCode();
            myfrmGetTrackingCode.ShowDialog();
            if (myfrmGetTrackingCode.SelectedCode != "Cancelled")
            {
                CurrentRouteId = myfrmGetTrackingCode.SelectedId;
                labelRouteCaption.Visible = true;
                labelCurrentRoute.Visible = true;
                labelCurrentRoute.Text = myfrmGetTrackingCode.SelectedCode +
                   " - " + myfrmGetTrackingCode.SelectedDescrip;

                buttonInsert.Enabled = true;
                filldatagrid();
            } // endif != cancelled
            else
            {
                wsgUtilities.wsgNotice("Operation Cancelled");
            }
        }

        public int CurrentRouteId { get; set; }
        public int CurrentRouteStepId { get; set; }
        public string CurrentRouteStepCode { get; set; }
        public string CurrentRouteStepDescrip { get; set; }
        public string CurrentState { get; set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Select")
                this.Close();
            else
            {
                if (wsgUtilities.wsgReply("Abandon Edit?"))
                {
                    CurrentState = "Select";
                }
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            // User selects Route to be processed
            FrmGetTrackingCode myfrmGetTrackingCode = new FrmGetTrackingCode();
            myfrmGetTrackingCode.ShowDialog();
            if (myfrmGetTrackingCode.SelectedCode != "Cancelled")
            {
                CurrentRouteStepId = myfrmGetTrackingCode.SelectedId;
                labelRouteCaption.Visible = true;
                labelCurrentRoute.Visible = true;
                string InsertMessage = trackingInf.InsertRouteStep(CurrentRouteId, CurrentRouteStepId);
                // Insert processing
                if (InsertMessage.TrimEnd() == "Step Inserted")
                {
                    wsgUtilities.wsgNotice("Step Inserted");
                    CurrentState = "Select";
                    filldatagrid();
                }
                else
                {
                    wsgUtilities.wsgNotice("Insertion not completed. " + InsertMessage);
                }
            } // endif != cancelled
            else
            {
                wsgUtilities.wsgNotice("Operation Cancelled");
            } // end else
        } // end insert click

        private void filldatagrid()
        {
            trackingInf.GetRouteData(CurrentRouteId);
            if (trackingInf.trackingds.view_routedata.Rows.Count > 0)
            {
                dataGridViewRouteData.Visible = true;
                dataGridViewRouteData.Focus();
            }
            else
            {
                dataGridViewRouteData.Visible = false;
            }
        } // end filldatagrid

        public void CaptureRouteStepData()
        {
            CurrencyManager xCM =
           (CurrencyManager)dataGridViewRouteData.BindingContext[dataGridViewRouteData.DataSource,
           dataGridViewRouteData.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Save the select SO number

            CurrentRouteStepId = (int)xRow["stepid"];
            CurrentRouteStepCode = (string)xRow["code"];
            CurrentRouteStepDescrip = (string)xRow["descrip"];
        }

        private void dataGridViewRouteData_DoubleClick(object sender, EventArgs e)
        {
            CaptureRouteStepData();

            if (wsgUtilities.wsgReply("Delete " + CurrentRouteStepDescrip.TrimEnd() + "?"))
            {
                // Deletion processing
                trackingInf.DeleteRouteStep(CurrentRouteId, CurrentRouteStepId);
                wsgUtilities.wsgNotice("Route Step Detleted ");
                filldatagrid();
            }
            else
            {
                wsgUtilities.wsgNotice("Deletion Cancelled");
            }
        }

        private void dataGridViewRouteData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentrouteid = trackingInf.CaptureIdCol(dataGridViewRouteData);
                trackingInf.ToggleCustStep(currentrouteid);
                filldatagrid();
            }
        }

        private void dataGridViewRouteData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewRouteData.Columns[e.ColumnIndex].Name.Equals("ColumnCuststep"))
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString().Trim() == "Y")
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.SelectionBackColor = Color.DarkRed;
                    }
                }
            }
        }
    } // end form class
} // end namespace