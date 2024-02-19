using System;

namespace Print
{
    public partial class FrmInventoryOnHand : WSGUtilitieslib.Telemetry.Form
    {
        public FrmInventoryOnHand()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}