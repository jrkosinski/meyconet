using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Estimating
{
    public partial class FrmSelectVersion : WSGUtilitieslib.Telemetry.Form
    {
        public BindingSource bindingVersionData = new BindingSource();

        public FrmSelectVersion()
        {
            InitializeComponent();
            SelectedVersion = "";
            dataGridViewVersionSelector.AutoGenerateColumns = false;
            dataGridViewVersionSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewVersionSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewVersionSelector.DataSource = bindingVersionData;
        }

        public string SelectedVersion { get; set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SelectedVersion = "";
            this.Close();
        }

        public void CaptureSession()
        {
            CurrencyManager xCM =
           (CurrencyManager)dataGridViewVersionSelector.BindingContext[dataGridViewVersionSelector.DataSource,
             dataGridViewVersionSelector.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Return the ID
            SelectedVersion = (string)xRow["version"];
        }

        private void dataGridViewVersionSelector_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureSession();
            this.Close();
        }

        private void dataGridViewVersionSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CaptureSession();
                this.Close();
            }
        }
    }
}