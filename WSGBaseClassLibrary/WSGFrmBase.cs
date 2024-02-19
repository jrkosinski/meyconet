using WSGUtilitieslib;

namespace WSGBaseClassLibrary
{
    public partial class WSGFrmBase : WSGUtilitieslib.Telemetry.Form
    {

        WSGUtilities wsgUtilities = new WSGUtilities("Meyco Sales Order Tracking");
        public WSGFrmBase()
        {
            InitializeComponent();
        }
    }
}
