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
                this.comboBoxIssue,
                this.comboBoxRootcause,
                this.comboBoxResolution,
                this.comboBoxFindingdept,
                this.comboBoxCausingdept,
                this.comboBoxEmployee,
                this.textBoxNotes,
                this.buttonSave,
                this.buttonCancel
            });
        }
    }
}