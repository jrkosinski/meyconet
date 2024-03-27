using System;
using System.Windows.Forms;

namespace Print
{
    public partial class FrmPrintStockCoverLabels : WSGUtilitieslib.Telemetry.Form
    {
        public FrmPrintStockCoverLabels()
        {
            InitializeComponent();

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonClear,
                this.groupBoxLabelType,
                this.radioButtonSewnOnLabel,
                this.radioButtonIdentityLabel,
                this.textBoxFirstFileNumber,
                this.textBoxLastFileNumber,
                this.buttonMarkLabelsPrinted,
                this.buttonGenerate,
                this.buttonClose
            });
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
        }
    }
}