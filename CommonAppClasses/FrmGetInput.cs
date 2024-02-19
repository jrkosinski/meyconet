using System;

namespace CommonAppClasses
{
    public partial class FrmGetInput : WSGUtilitieslib.Telemetry.Form
    {
        public string InputQuestion { get; set; }
        public int Response { get; set; }

        public FrmGetInput()
        {
            InitializeComponent();
            textBoxData.DataBindings.Add("Text", this, "Response");
            labelRequest.DataBindings.Add("Text", this, "InputQuestion");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}