using System.Windows.Forms;

namespace MiscellaneousSystemMaintenance
{
    public partial class FrmDepartmentEmployee : WSGUtilitieslib.Telemetry.Form
    {
        public FrmDepartmentEmployee()
        {
            InitializeComponent();

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonReturn,
                this.listBoxDepartment,
                this.listBoxAssigned,
                this.listBoxUnassigned,
                this.buttonSave,
            });
        }
    }
}