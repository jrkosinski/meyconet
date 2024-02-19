using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmSoTrackingActivity : WSGUtilitieslib.Telemetry.Form
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Tracking Information");
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        public string Sono { get; set; }
        private TrackingInf trackInf = new TrackingInf("SQL", "SQLConnString");
        private BindingSource bindingSourceTrackingActivity = new BindingSource();

        public FrmSoTrackingActivity()
        {
            InitializeComponent();
            dataGridViewTrackingActivity.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewTrackingActivity.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewTrackingActivity.AutoGenerateColumns = false;
            dataGridViewTrackingActivity.DataSource = bindingSourceTrackingActivity;
            bindingSourceTrackingActivity.DataSource = trackInf.trackingds.view_trackingstepdata;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSoTrackingActivity_Shown(object sender, EventArgs e)
        {
            trackInf.GetSoTrackingSteps(Sono);
            if (trackInf.trackingds.view_trackingstepdata.Rows.Count < 1)
            {
                wsgUtilities.wsgNotice("There is no tracking data for this SO");
                this.Close();
            }
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
            trackInf.GetSoTrackingSteps(Sono);
            this.Update();
        }
    }
}