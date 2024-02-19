using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class FrmGetShipToAddress : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Ship To");

        // Create the customer processing object
        private CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");

        private BindingSource bindingShipToData = new BindingSource();
        public int CustId { get; set; }
        public string Custno { get; set; }
        public string CurrentState { get; set; }
        public int SelectedShipToId { get; set; }
        public bool InsertingShipTo { get; set; }

        public FrmGetShipToAddress()
        {
            InitializeComponent();
            dataGridViewShipToAddresses.AutoGenerateColumns = false;
            dataGridViewShipToAddresses.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewShipToAddresses.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            SelectedShipToId = 0;
        }

        private void FrmGetShipToAddress_Shown(object sender, EventArgs e)
        {
            customerAccess.getCustomerShipToData(CustId);
            if (customerAccess.ards.view_customershiptolist.Rows.Count != 0)
            {
                bindingShipToData.DataSource = customerAccess.ards.view_customershiptolist;
                dataGridViewShipToAddresses.DataSource = bindingShipToData;
                dataGridViewShipToAddresses.Focus();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no ship to addresses for this customer.");
                dataGridViewShipToAddresses.Visible = false;
            }
        }

        private void dataGridViewShipToAddresses_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SelectedShipToId = customerAccess.CaptureIdCol(dataGridViewShipToAddresses);
                customerAccess.getSingleShipToData(SelectedShipToId);
                this.Close();
            }
        }

        private void dataGridViewShipToAddresses_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedShipToId = customerAccess.CaptureIdCol(dataGridViewShipToAddresses);
            customerAccess.getSingleShipToData(SelectedShipToId);
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            // Use the Select method to find all rows matching the filter.
            DataRow[] foundrow = customerAccess.ards.view_customershiptolist.Select("defaship = 'Y' ");
            if (foundrow.Length > 0)
            {
                SelectedShipToId = Convert.ToInt32(foundrow[0]["idcol"]);
                this.Close();
            }
            else
            {
                wsgUtilities.wsgNotice("There is no default ShipTo for this customer.");
            }
        }

        private void buttonBillTo_Click(object sender, EventArgs e)
        {
            SelectedShipToId = 888888;
            this.Close();
        }
    }
}