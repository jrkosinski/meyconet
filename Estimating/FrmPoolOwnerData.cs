using System;
using WSGUtilitieslib;

namespace Estimating
{
    public partial class FrmPoolOwnerData : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Customer Information");

        // Create the SO Information processing object
        public FrmPoolOwnerData()
        {
            InitializeComponent();
        }

        public bool SaveData { get; set; }
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            SaveData = false;
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Lname = textBoxLname.Text;
            Fname = textBoxFname.Text;
            Address = textBoxAddress.Text;
            City = textBoxCity.Text;
            State = textBoxState.Text;
            Zip = textBoxZip.Text;
            SaveData = true;
            this.Close();
        }
    }
}