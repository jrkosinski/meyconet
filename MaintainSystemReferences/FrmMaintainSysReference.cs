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
        }
    }
}