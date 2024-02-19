using System;

namespace Warranty
{
    public partial class FrmChooseQuoteOption : WSGUtilitieslib.Telemetry.Form
    {
        public string QuoteOption = "Cancel";

        public FrmChooseQuoteOption()
        {
            InitializeComponent();
        }

        private void buttonCopyQuote_Click(object sender, EventArgs e)
        {
            QuoteOption = "Copy";
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            QuoteOption = "Cancel";
            this.Close();
        }

        private void buttonViewQuote_Click(object sender, EventArgs e)
        {
            QuoteOption = "View";
            this.Close();
        }
    }
}