using System.Windows.Forms;

namespace Inspection
{
    public partial class FrmRepairInspection : WSGUtilitieslib.Telemetry.Form
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;

        public FrmRepairInspection()
        {
            InitializeComponent();

            this.SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]{
                this.textBoxSoNo,
                this.buttonGetSO,
                this.textBoxCustno,
                this.textBoxCompany,
                this.textBoxMeycono,
                this.textBoxMeycono,
                this.textBoxPhone,
                this.textBoxFaxNo,
                this.textBoxEmail,
                this.textBoxRecdate,
                this.textBoxReceivedBy,
                this.textBoxInbCarrier,
                this.textBoxFrtTerms,
                this.dateTimePickerRecdate,
                this.comboBoxInbCarrier,
                this.comboBoxFrtTerms,
                this.textBoxLocation,
                this.comboBoxLocation,
                this.textBoxOldPlan,
                this.textBoxCommlRes,
                this.comboBoxCommlRes,
                this.textBoxCleanliness,
                this.comboBoxCleanliness,
                this.textBoxInspectedBy,
                this.textBoxManufacturer,
                this.comboBoxManufacturer,
                this.textBoxDescrip,
                this.buttonSelectCover,
                this.textBoxMaterial,
                this.comboBoxMaterial,
                this.textBoxColor,
                this.comboBoxColor,
                this.textBoxSpacing,
                this.comboBoxSpacing,
                this.textBoxStraps,
                this.textBoxCoverMeasured,
                this.textBoxCwidft,
                this.textBoxCwidin,
                this.textBoxClenft,
                this.textBoxClenin,
                this.textBoxX1widft,
                this.textBoxX1widin,
                this.textBoxX1lenft,
                this.textBoxX1lenin,
                this.textBoxX2widft,
                this.textBoxX2widin,
                this.textBoxX2lenft,
                this.textBoxX2lenin,
                this.textBoxX3widft,
                this.textBoxX3widin,
                this.textBoxX3lenft,
                this.textBoxX3lenin,
                this.textBoxX4widft,
                this.textBoxX4widin,
                this.textBoxX4lenft,
                this.textBoxX4lenin,
                this.textBoxMaterialCondition,
                this.comboBoxMaterialCondition,
                this.textBoxThreadCondition,
                this.comboBoxThreadCondition,
                this.textBoxWebCondition,
                this.comboBoxWebCondition,
                this.textBoxSpringsCondition,
                this.comboBoxSpringsCondition,
                this.textBoxRecommendation,
                this.comboBoxRecommendation,
                this.textBoxObservations,
                this.textBoxSprings,
                this.textBoxSpringCovers,
                this.textBoxInstructions,
                this.textBoxStowBag,
                this.textBoxPumps,
                this.textBoxPhotos,
                this.textBoxNotes,
                this.buttonSaveLines,
                this.buttonCancelLines,
                this.buttonSave,
                this.buttonClose
            });
        }
    } // form class
} // namespace