using System;
using System.Windows.Forms;

namespace Print
{
    public partial class FrmPrintSoDocumentsBatch : WSGUtilitieslib.Telemetry.Form

    {
        private PrintInf printInf = new PrintInf("SQL", "SQLConnString");

        public FrmPrintSoDocumentsBatch()
        {
            InitializeComponent();
            PrintDocument = "";
        }

        public string PrintLocation { get; set; }
        public string PrintDocument { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonPrintRepairOrders_Click(object sender, EventArgs e)
        {
            printInf.ProcessBatch(PrintDocument, "Repair Orders", PrintLocation);
        }

        private void buttonPrintStockCoverOrders_Click(object sender, EventArgs e)
        {
            printInf.ProcessBatch(PrintDocument, "Stock Covers", PrintLocation);
        }

        private void buttonPrintMiscellaneousOrders_Click(object sender, EventArgs e)
        {
            printInf.ProcessBatch(PrintDocument, "Miscellaneous Products", PrintLocation);
        }

        private void buttonPrintCustomCoverOrders_Click(object sender, EventArgs e)
        {
            printInf.ProcessBatch(PrintDocument, "Custom Covers", PrintLocation);
        }

        private void radioButtonPackingLists_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd.Checked)
            {
                PrintDocument = "Packing Lists";
            }
            else
            {
                PrintDocument = "";
            }
        }

        private void radioButtonWorkOrders_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd.Checked)
            {
                PrintDocument = "Work Orders";
            }
            else
            {
                PrintDocument = "";
            }
        }

        private void radioButtonInvoices_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd.Checked)
            {
                PrintDocument = "Invoices";
            }
            else
            {
                PrintDocument = "";
            }
        }

        private void radioButtonSewnOnLabels_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd.Checked)
            {
                PrintDocument = "Sewn On Labels";
            }
            else
            {
                PrintDocument = "";
            }
        }

        private void radioButtonIdentityLabels_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd.Checked)
            {
                PrintDocument = "Identity Labels";
            }
            else
            {
                PrintDocument = "";
            }
        }
    }
}