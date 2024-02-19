using System;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmGetSpacing : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Spacing Information");

        // Create the Spacing processing object
        private ReferenceMaintenance refdata = new ReferenceMaintenance("SQL", "SQLConnString");

        private BindingSource bindingSpacingData = new BindingSource();

        public int SelectedSpacingId { get; set; }

        public FrmGetSpacing()
        {
            InitializeComponent();
            SelectedSpacingId = 0;
            dataGridViewSpacing.AutoGenerateColumns = false;
            dataGridViewSpacing.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewSpacing.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            refdata.GetSpacingData();
            bindingSpacingData.DataSource = refdata.referenceds.view_quspacingdata;
            dataGridViewSpacing.DataSource = bindingSpacingData;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewSpacing_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedSpacingId = refdata.CaptureIdCol(dataGridViewSpacing);
            this.Close();
        }

        private void dataGridViewSpacing_KeyDown(object sender, KeyEventArgs e)
        {
            SelectedSpacingId = refdata.CaptureIdCol(dataGridViewSpacing);
            this.Close();
        }
    }
}