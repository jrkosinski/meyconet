using System.Windows.Forms;

namespace CommonAppClasses
{
    public partial class FrmSelectTerms : WSGUtilitieslib.Telemetry.Form
    {
        public BindingSource bindingTermsData = new BindingSource();

        public FrmSelectTerms()
        {
            InitializeComponent();
        }
    }
}