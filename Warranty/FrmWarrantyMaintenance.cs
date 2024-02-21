namespace Warranty
{
    public partial class FrmWarrantyMaintenance : WSGUtilitieslib.Telemetry.Form
    {
        public FrmWarrantyMaintenance()
        {
            InitializeComponent();

            this.SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]{
                this.textBoxOrnumS,
                this.textBoxSonoS,
                this.textBoxPonumS,
                this.dateTimePickerShipFirstDate,
                this.dateTimePickerShipLastDate,
                this.textBoxLnameS,
                this.textBoxFnameS,
                this.textBoxAddressS,
                this.textBoxCityS,
                this.textBoxStateS,
                this.textBoxZipS,
                this.textBoxCustnoS,
                this.textBoxDealerS,
                this.textBoxDealaddr1S,
                this.textBoxDealcityS,
                this.textBoxDealstateS,
                this.textBoxDealzipS,
                this.buttonSearch,
                this.buttonClose,
                this.buttonClear,
                this.buttonEdit,
                this.textBoxOrnum,
                this.textBoxSono,
                this.textBoxPonum,
                this.textBoxShipdate,
                this.textBoxFname,
                this.textBoxLname,
                this.textBoxAddress,
                this.textBoxCity,
                this.textBoxState,
                this.textBoxZip,
                this.textBoxReplacedby,
                this.checkBoxDisableprt,
                this.textBoxHoemailaddr,
                this.textBoxOrigowner,
                this.textBoxRepairnotes,
                this.textBoxAltnotes,
                this.textBoxCustno,
                this.buttonGetCustomer,
                this.textBoxDealer,
                this.textBoxDealaddr1,
                this.textBoxDealaddr2,
                this.textBoxDealcity,
                this.textBoxDealstate,
                this.textBoxDealzip,
                this.textBoxNotes,
                this.textBoxLastupdate,
                this.buttonSave
            });

        }
    }
}