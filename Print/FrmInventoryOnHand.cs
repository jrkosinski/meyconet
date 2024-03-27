using System;
using System.Windows.Forms;

namespace Print
{
    public partial class FrmInventoryOnHand : WSGUtilitieslib.Telemetry.Form
    {
        public FrmInventoryOnHand()
        {
            InitializeComponent();

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonGenerate,
                this.dateTimePickerCutoff,
                this.textBoxItem,
                this.buttonClose
            });
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}