using System.Windows.Forms;

namespace MaintainSystemReferences
{
    public partial class FrmMaintainProductionUnitSchedule : WSGUtilitieslib.Telemetry.Form
    {
        public FrmMaintainProductionUnitSchedule()
        {
            InitializeComponent();

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonInsert,
                //this.buttonEdit,
                //this.buttonDelete,
                this.dateTimePickerEffectiveDate,
                this.textBoxLevel1price,
                this.textBoxLevel1units,
                this.textBoxLevel1buffer,
                this.textBoxLevel2price,
                this.textBoxLevel2units,
                this.textBoxLevel2buffer,
                this.textBoxLevel3price,
                this.textBoxLevel3units,
                this.textBoxLevel3buffer,
                this.textBoxLevel4price,
                this.textBoxLevel4units,
                this.textBoxLevel4buffer,
                this.textBoxLevel5price,
                this.textBoxLevel5units,
                this.textBoxLevel5buffer,
                this.textBoxLevel6price,
                this.textBoxLevel6units,
                this.textBoxLevel6buffer,
                this.textBoxLevel7price,
                this.textBoxLevel7units,
                this.textBoxLevel7buffer,
                this.textBoxLevel8price,
                this.textBoxLevel8units,
                this.textBoxLevel8buffer,
                this.textBoxLevel9price,
                this.textBoxLevel9units,
                this.textBoxLevel9buffer,
                this.textBoxLevel10price,
                this.textBoxLevel10units,
                this.textBoxLevel10buffer,
                this.buttonSave,
                this.buttonCancel,
                this.buttonClose
            });
        }
    }
}