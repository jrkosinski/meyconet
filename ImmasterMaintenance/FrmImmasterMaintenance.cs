using System.Windows.Forms;

namespace ImmasterMaintenance
{
    public partial class FrmImmasterMaintenance : WSGUtilitieslib.Telemetry.Form
    {
        public FrmImmasterMaintenance()
        {
            InitializeComponent();

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonSelect,
                this.buttonEdit,
                this.textBoxPrcdesc,
                this.textBoxPwidthft,
                this.textBoxPwidthin,
                this.textBoxPlenft,
                this.textBoxPlenin,
                this.textBoxPextwidft,
                this.textBoxPextwidin,
                this.textBoxPextlenft,
                this.textBoxPextlenin,
                this.textBoxCwidthft,
                this.textBoxCwidthin,
                this.textBoxClenft,
                this.textBoxClenin,
                this.textBoxEwidthft,
                this.textBoxEwidthin,
                this.textBoxElenft,
                this.textBoxElenin,
                this.textBoxSqft,
                this.textBoxEsqft,
                this.radioButtonStock,
                this.radioButtonStd,
                this.comboBoxColor,
                this.comboBoxMaterial,
                this.checkBoxAppldisc,
                this.textBoxPrwcov,
                this.textBoxStrap,
                this.textBoxCustplan,
                this.textBoxSpechard,
                this.textBoxPrccom,
                this.buttonSave,
                this.buttonClose
            });
        }
    }
}