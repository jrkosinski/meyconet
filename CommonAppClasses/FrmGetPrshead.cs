using System;
using System.Drawing;
using System.Windows.Forms;

namespace CommonAppClasses
{
    public partial class FrmGetPrshead : WSGUtilitieslib.Telemetry.Form
    {
        // Create the price processing object
        private PriceMaintenance priceMaintenance = new PriceMaintenance("SQL", "SQLConnString");

        private BindingSource bindingPrsHeadLocatorData = new BindingSource();
        public int SelectedPrsHeadId { get; set; }

        public FrmGetPrshead()
        {
            InitializeComponent();
            SelectedPrsHeadId = 0;
            dataGridViewPrsHeadLocator.AutoGenerateColumns = false;
            dataGridViewPrsHeadLocator.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewPrsHeadLocator.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            priceMaintenance.GetQuprsheadData();
            bindingPrsHeadLocatorData.DataSource = priceMaintenance.prds.view_quprsheaddata;
            dataGridViewPrsHeadLocator.DataSource = bindingPrsHeadLocatorData;
            dataGridViewPrsHeadLocator.Focus();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewPrsHeadLocator_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedPrsHeadId = priceMaintenance.CaptureIdCol(dataGridViewPrsHeadLocator);
            this.Close();
        }

        private void dataGridViewPrsHeadLocator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SelectedPrsHeadId = priceMaintenance.CaptureIdCol(dataGridViewPrsHeadLocator);
                this.Close();
            }
        }
    }
}