using System.Windows.Forms;

namespace Inventory
{
    public partial class FrmInventoryTransaction : WSGUtilitieslib.Telemetry.Form
    {
        public FrmInventoryTransaction()
        {
            InitializeComponent();

            this.SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[] {
                this.textBoxItem,
                this.buttonGetItem,
                this.comboBoxLocation,
                this.dateTimePickerTdate,
                this.textBoxQty,
                this.textBoxCost,
                this.textBoxNotes,
                this.buttonSave,
                this.buttonClose,
            });
        }
    }
}