using System;

//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmCoverSelector : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Cover Selector");
        private quote somastds = new quote();

        public FrmCoverSelector()
        {
            InitializeComponent();
            SelectedCover = "";
            dataGridViewCoverSelector.AutoGenerateColumns = false;
            dataGridViewCoverSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewCoverSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        public string SelectedCover { get; set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CaptureCover()
        {
            CurrencyManager xCM =
           (CurrencyManager)dataGridViewCoverSelector.BindingContext[dataGridViewCoverSelector.DataSource,
             dataGridViewCoverSelector.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Capture the cover
            SelectedCover = (string)xRow["cover"];
        }

        private void FrmCoverSelector_Load(object sender, EventArgs e)
        {
        }

        private void dataGridViewCoverSelector_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureCover();
            this.Close();
        }

        private void dataGridViewCoverSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureCover();
                this.Close();
            }
        }
    }
}