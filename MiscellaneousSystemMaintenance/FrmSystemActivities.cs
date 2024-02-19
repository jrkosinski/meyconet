using CommonAppClasses;
using System;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public partial class FrmSystemActivities : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");

        // Create the App Information processing object
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");

        // Create the Information processing object
        private MiscSysInf miscsysinf = new MiscSysInf("SQL", "SQLConnString");

        public FrmSystemActivities()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClearSomast_Click(object sender, EventArgs e)
        {
            miscsysinf.ClearSomastLocks();
        }

        private void buttonClearArcust_Click(object sender, EventArgs e)
        {
            miscsysinf.ClearArcustLocks();
        }

        private void buttonClearInvoicing_Click(object sender, EventArgs e)
        {
            miscsysinf.ClearInvoicing();
        }

        private void buttonClearAracadr_Click(object sender, EventArgs e)
        {
            miscsysinf.ClearAracadrLocks();
        }

        private void buttonClearWarranty_Click(object sender, EventArgs e)
        {
            miscsysinf.ClearWarrantyLocks();
        }

        private void buttonClearEmailAddress_Click(object sender, EventArgs e)
        {
            miscsysinf.ClearEmailAddress();
        }

        private void buttonClearProductionUnitSchedule_Click(object sender, EventArgs e)
        {
            miscsysinf.ClearProductionUnitSchedule();
        }

        private void buttonClearCapacityCalendar_Click(object sender, EventArgs e)
        {
            miscsysinf.ClearCapacityCalendar();
        }
    }
}