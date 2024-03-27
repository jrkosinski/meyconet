using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainSystemReferences
{
    public partial class FrmMaintainSysReference : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();

        // Create the Inspection Information processing object
        public FrmMaintainSysReference()
        {
            InitializeComponent();
            // Create the Inspection Information processing object
            MaintainSysReference MaintainSysRef = new MaintainSysReference("SQL", "SQLConnString", this);

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.comboBoxRefrenceGroups,
                this.buttonInsert,
                this.buttonEdit,
                this.buttonDelete,
                this.buttonClear,
                this.textBoxDescrip,
                this.buttonSave,
                this.buttonClose
            });
        }

    }
}