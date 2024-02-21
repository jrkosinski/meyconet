using System;

namespace CustomerMaintenance
{
    public partial class FrmMaintainCustomer : WSGUtilitieslib.Telemetry.Form
    {
        public FrmMaintainCustomer()
        {
            InitializeComponent();
            this.SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[] {
                this.buttonSelectCustomer,
                this.buttonEdit,
                this.tabPageGeneral,
                this.textBoxCustno,
                this.textBoxCompany,
                this.textBoxAddress1,
                this.textBoxAddress2,
                this.textBoxCity,
                this.textBoxState,
                this.textBoxZip,
                this.textBoxCountry,
                this.textBoxEmail,
                this.textBoxUrl,
                this.textBoxPhone,
                this.textBoxFaxno,
                this.textBoxContact,
                this.textBoxTitle,
                this.textBoxPhone2,

                this.tabPageDetails,
                this.textBoxIndust,
                this.textBoxType,
                this.textBoxCode,
                this.textBoxSource,
                this.textBoxDealer,
                this.textBoxComment,
                this.textBoxMaxdays,
                this.textBoxMaxdollars,
                this.textBoxMaxcount,

                this.tabPageSales,
                this.textBoxSalesmn,
                this.buttonGetSalesmn,
                this.textBoxLimit,
                this.textBoxTax,
                this.textBoxServrep,
                this.textBoxTaxdist,
                this.textBoxTerr,
                this.textBoxPricecode,
                this.buttonFindTerms,
                this.textBoxSvc,

                this.tabPageNotes,
                this.textBoxCstmemo,
                this.textBoxAcctMemo,

                this.tabPageProfile,
                this.buttonQuoteEmail,
                this.buttonOrderEmail,
                this.buttonInvoiceEmail,
                this.buttonDefaultEmail,
                this.textBoxFaxQuote,
                this.textBoxFaxOrder,
                this.textBoxFaxInvoice,

                this.tabPageAlerts,
                this.tabPageRoutes,
                this.tabPageHWProfile,
                this.tabPageIncidents,
                this.tabPageContacts,
                this.buttonAddContact,

                this.buttonSave,
                this.buttonClose
            });
        }

        private void label219_Click(object sender, EventArgs e)
        {
        }

        private void label235_Click(object sender, EventArgs e)
        {
        }

        private void label106_Click(object sender, EventArgs e)
        {
        }
    } // class
} // namespace