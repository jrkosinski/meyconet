using System;

namespace CommonAppClasses
{
    public partial class FrmGetDate : WSGUtilitieslib.Telemetry.Form
    {
        public FrmGetDate()
        {
            InitializeComponent();
            SelectedDate = DateTime.Now;
            CaptionText = "Select a Date. Press Close to continue, Cancel to abort.";
            DateSelected = false;
        }

        public DateTime SelectedDate { get; set; }
        public String CaptionText { get; set; }
        public bool DateSelected { get; set; }

        private void dateTimePickerSelectDate_ValueChanged(object sender, EventArgs e)
        {
            SelectedDate = dateTimePickerSelectDate.Value;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DateSelected = false;
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DateSelected = true;
            this.Close();
        }

        private void FrmGetDate_Shown(object sender, EventArgs e)
        {
            labelDateInformation.Text = CaptionText;
        }
    }
}