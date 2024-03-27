using System.Windows.Forms;

namespace Print
{
    public partial class FrmInventoryTransactionAnalysis : WSGUtilitieslib.Telemetry.Form
    {
        public FrmInventoryTransactionAnalysis()
        {
            InitializeComponent();
            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonGenerate,
                this.dateTimePickerStart,
                this.dateTimePickerEnd,
                this.textBoxItem,
                this.buttonClose
            });
        }
    }
}