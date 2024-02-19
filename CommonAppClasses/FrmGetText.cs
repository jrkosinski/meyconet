using System;

namespace CommonAppClasses
{
    public partial class FrmGetText : WSGUtilitieslib.Telemetry.Form
    {
        public string textcontent { get; set; }
        public string textrequest { get; set; }

        public FrmGetText()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            textcontent = textBoxContent.Text;
            this.Close();
        }

        private void FrmGetText_Shown(object sender, EventArgs e)
        {
            textBoxContent.Text = textcontent;
            labelRequest.Text = textrequest;
        }
    }
}