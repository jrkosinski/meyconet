using System;

namespace Estimating
{
    public partial class FrmProductSelector : WSGUtilitieslib.Telemetry.Form
    {
        public string SelectedProduct { get; set; }

        public FrmProductSelector()
        {
            InitializeComponent();
            SelectedProduct = "";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            SelectedProduct = "";
            this.Close();
        }

        private void listBoxProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProducts.SelectedItem != null)
            {
                SelectedProduct = listBoxProducts.SelectedItem.ToString();
            }
            else
            {
                SelectedProduct = "";
            }
            this.Close();
        }
    }
}