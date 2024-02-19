namespace Estimating
{
    public partial class FrmInspVersionSelector : WSGUtilitieslib.Telemetry.Form
    {
        public string SelectedVersion { get; set; }
        public VersionSelector versionSelector { get; set; }

        public FrmInspVersionSelector()
        {
            InitializeComponent();
        }
    }
}