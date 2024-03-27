using System;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmGetTwoDates : WSGUtilitieslib.Telemetry.Form
    {
        public Boolean DateOk = true;
        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }
        private WSGUtilities wsgUtilities = new WSGUtilities("Maintain Event");

        public FrmGetTwoDates()
        {
            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
            InitializeComponent();

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.dateTimePickerStart,
                this.dateTimePickerEnd,
                this.buttonOK,
                this.buttonCancel
            });
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DateOk = false;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SelectedEndDate < SelectedStartDate)
            {
                wsgUtilities.wsgNotice("The Ending Date is earlier than the starting date");
            }
            else
            {
                this.Close();
            }
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            SelectedStartDate = dateTimePickerStart.Value.Date;
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            SelectedEndDate = dateTimePickerEnd.Value.Date;
        }
    }
}