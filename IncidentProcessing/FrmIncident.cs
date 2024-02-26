using System.Windows.Forms;

namespace IncidentProcessing
{
    public partial class FrmIncident : WSGUtilitieslib.Telemetry.Form
    {
        public FrmIncident()
        {
            InitializeComponent();

            this.SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[] {
                this.buttonSelectso,
                this.dateTimePickerIncidentdate,
                this.textBoxCost,
                this.textBoxIssue,
                this.comboBoxIssue,
                this.textBoxRootcause,
                this.comboBoxRootcause,
                this.textBoxResolution,
                this.comboBoxResolution,
                this.textBoxFindingdept,
                this.comboBoxFindingdept,
                this.textBoxCausingdept,
                this.comboBoxCausingdept,
                this.textBoxEmployee,
                this.comboBoxEmployee,
                this.textBoxNotes,
                this.buttonSave,
                this.buttonCancel
            });
        }
    }
}