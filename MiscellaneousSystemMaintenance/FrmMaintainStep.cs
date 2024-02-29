using CommonAppClasses;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public partial class FrmMaintainStep : WSGUtilitieslib.Telemetry.Form
    {
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Tracking Code Selector");
        private TrackingInf trackingInf = new TrackingInf("SQL", "SQLConnString");

        public FrmMaintainStep()
        {
            InitializeComponent();
            conn.ConnectionString = myAppconstants.SQLConnectionString;
            DisableFields();
            buttonDelete.Enabled = false;
            buttonSave.Enabled = false;
            buttonEdit.Enabled = false;
            buttonSubscribers.Enabled = false;
            CurrentState = "Select";
            textBoxIdcol.DataBindings.Add("Text", trackingInf.trackingds.step, "idcol");
            textBoxCode.DataBindings.Add("Text", trackingInf.trackingds.step, "code"); ;
            textBoxDescrip.DataBindings.Add("Text", trackingInf.trackingds.step, "descrip"); ;
            textBoxMessage.DataBindings.Add("Text", trackingInf.trackingds.step, "message");
            textBoxAlertMessage.DataBindings.Add("Text", trackingInf.trackingds.step, "alertmessage");
            textBoxAlertInterval.DataBindings.Add("Text", trackingInf.trackingds.step, "alertinterval"); ;
            textBoxOndescrip.DataBindings.Add("Text", trackingInf.trackingds.step, "ondescrip");
            textBoxInternalMail.DataBindings.Add("Text", trackingInf.trackingds.step, "internalmail");
            textBoxMaxcode.DataBindings.Add("Text", trackingInf.trackingds.step, "maxcode"); ;
            textBoxPrintInvoice.DataBindings.Add("Text", trackingInf.trackingds.step, "printinvoice"); ;
            textBoxPrintWorkOrder.DataBindings.Add("Text", trackingInf.trackingds.step, "printworkorder");
            textBoxPrintPackList.DataBindings.Add("Text", trackingInf.trackingds.step, "printpacklist"); ;
            textBoxInspection.DataBindings.Add("Text", trackingInf.trackingds.step, "inspection");
            textBoxOKToInvoice.DataBindings.Add("Text", trackingInf.trackingds.step, "oktoinvoice");
            textBoxMustBeInvoiced.DataBindings.Add("Text", trackingInf.trackingds.step, "mustbeinvoiced");
            textBoxPrintSewnOnLabel.DataBindings.Add("Text", trackingInf.trackingds.step, "printsewnonlabel");
            textBoxPrintIdentityLabel.DataBindings.Add("Text", trackingInf.trackingds.step, "printidentitylabel");

            SetTabOrder();
        }

        protected override void SetTabOrder()
        {
            this.SetTabOrder(new Control[]
            {
                this.buttonSelectPhase,
                this.buttonEdit,
                this.panel1,
                this.textBoxIdcol,
                this.textBoxCode,
                this.textBoxDescrip,
                this.textBoxOndescrip,
                this.textBoxSoclose,
                this.textBoxInspection,
                this.textBoxMaxcode,
                this.panel2,
                this.listBoxFileToSend,
                this.textBoxSendFTP,
                this.textBoxPrintInvoice,
                this.textBoxInternalMail,
                this.textBoxPrintWorkOrder,
                this.textBoxPrintIdentityLabel,
                this.textBoxPrintPackList,
                this.textBoxPrintSewnOnLabel,
                this.textBoxMessage,
                this.listBoxLocation,
                this.textBoxOKToInvoice,
                this.textBoxMustBeInvoiced,
                this.textBoxAlertInterval,
                this.textBoxAlertMessage,

                this.buttonSave,
                this.buttonDelete,
                this.buttonCancel,
                this.buttonSubscribers,
                this.buttonInsert
            });
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (CurrentState == "Select")
                this.Close();
            else
            {
                if (wsgUtilities.wsgReply("Abandon Edit?"))
                {
                    DisableFields();
                    InitializeFields();
                    buttonSave.Enabled = false;
                    CurrentState = "Select";
                }
            }
        }

        private void buttonSelectPhase_Click(object sender, EventArgs e)
        {
            DisableFields();

            // User selects code to be processed
            FrmGetTrackingCode myfrmGetTrackingCode = new FrmGetTrackingCode();
            myfrmGetTrackingCode.ShowDialog();
            if (myfrmGetTrackingCode.SelectedCode != "Cancelled")
            {
                CurrentStepid = myfrmGetTrackingCode.SelectedId;
                trackingInf.GetSingleTrackingStep(CurrentStepid);
                if (trackingInf.trackingds.step.Rows.Count != 0)
                {
                    listBoxLocation.SelectedItem = trackingInf.trackingds.step[0].location;
                    //Load the form's fields from the datatable
                    switch (trackingInf.trackingds.step[0].filetosend.Substring(0, 1))
                    {
                        case "E":
                            listBoxFileToSend.SelectedItem = "Estimate";
                            break;

                        case "I":
                            listBoxFileToSend.SelectedItem = "Invoice";
                            break;

                        case "O":
                            listBoxFileToSend.SelectedItem = "Order";
                            break;

                        default:
                            listBoxFileToSend.SelectedItem = "None";
                            break;
                    }

                    if (!trackingInf.trackingds.step[0].sendftp)
                    {
                        textBoxSendFTP.Text = "N";
                    }
                    else
                    {
                        textBoxSendFTP.Text = "Y";
                    }
                    textBoxSoclose.Text = trackingInf.trackingds.step[0].soclose;
                    DisableFields();
                    buttonDelete.Enabled = true;
                    buttonSave.Enabled = false;
                    buttonEdit.Enabled = true;
                    buttonSubscribers.Enabled = true;
                }
                else
                {
                    wsgUtilities.wsgNotice("There has been an error. Please reselect " + CurrentStepid.ToString());
                }
            } // endif != cancelled
            else
            {
                wsgUtilities.wsgNotice("Operation Cancelled");
            }
        }// end click

        public int CurrentStepid { get; set; }
        public string CurrentState { get; set; }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            trackingInf.trackingds.step[0].filetosend = listBoxFileToSend.SelectedItem.ToString().Substring(0, 1);
            if (textBoxSendFTP.Text.TrimStart().TrimEnd().ToUpper() == "Y")
            {
                trackingInf.trackingds.step[0].sendftp = true;
            }
            else
            {
                trackingInf.trackingds.step[0].sendftp = false;
            }
            trackingInf.trackingds.step[0].location = listBoxLocation.SelectedItem.ToString();

            if (trackingInf.SaveTrackingStep() == true)
            {
                if (CurrentState == "Edit")
                {
                    buttonSave.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonSubscribers.Enabled = false;
                    DisableFields();
                    InitializeFields();
                    CurrentState = "Select";
                } // endif currentstate == "Edit"
                else
                {
                    buttonSave.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonSubscribers.Enabled = false;
                    DisableFields();
                    InitializeFields();
                    CurrentState = "Select";
                }
                wsgUtilities.wsgNotice("Step Saved");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this item?"))
            {
                if (trackingInf.DeleteTrackingStep(CurrentStepid).TrimEnd() == "Step Deleted")
                {
                    wsgUtilities.wsgNotice("Step Deleted");
                    buttonSave.Enabled = false;
                    buttonEdit.Enabled = false;
                    buttonSubscribers.Enabled = false;
                    buttonDelete.Enabled = false;
                    DisableFields();
                    InitializeFields();
                    CurrentState = "Select";
                }
                else
                {
                    wsgUtilities.wsgNotice("This step in use. Deletion Cancelled.");
                }
            } // end  if (wsgUtilities.wsgReply("Delete this item?"))
        }// end click

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            CurrentState = "Insert";
            EnableFields();
            InitializeFields();
            buttonEdit.Enabled = false;
            buttonDelete.Enabled = false;
            buttonSave.Enabled = true;
            buttonSubscribers.Enabled = false;
        }

        private void DisableFields()
        {
            //Initialize the form's fields
            textBoxCode.Enabled = false;
            textBoxDescrip.Enabled = false;
            textBoxMessage.Enabled = false;
            textBoxAlertMessage.Enabled = false;
            textBoxAlertInterval.Enabled = false;
            textBoxOndescrip.Enabled = false;
            textBoxInternalMail.Enabled = false;
            textBoxMaxcode.Enabled = false;
            listBoxFileToSend.Enabled = false;
            textBoxSendFTP.Enabled = false;
            textBoxSoclose.Enabled = false;
            textBoxOKToInvoice.Enabled = false;
            textBoxMustBeInvoiced.Enabled = false;
            textBoxPrintInvoice.Enabled = false;
            textBoxPrintPackList.Enabled = false;
            textBoxPrintWorkOrder.Enabled = false;
            textBoxInspection.Enabled = false;
            listBoxLocation.Enabled = false;
        }

        private void EnableFields()
        {
            //Initialize the form's fields
            textBoxCode.Enabled = true;
            textBoxDescrip.Enabled = true;
            textBoxMessage.Enabled = true;
            textBoxAlertMessage.Enabled = true;
            textBoxAlertInterval.Enabled = true;
            textBoxOndescrip.Enabled = true;
            textBoxInternalMail.Enabled = true;
            textBoxMaxcode.Enabled = true;
            listBoxFileToSend.Enabled = true;
            textBoxSendFTP.Enabled = true;
            textBoxSoclose.Enabled = true;
            textBoxMustBeInvoiced.Enabled = true;
            textBoxOKToInvoice.Enabled = true;
            textBoxPrintInvoice.Enabled = true;
            textBoxPrintPackList.Enabled = true;
            textBoxPrintWorkOrder.Enabled = true;
            textBoxInspection.Enabled = true;
            listBoxLocation.Enabled = true;
        }

        private void InitializeFields()
        {
            trackingInf.InitializeStep();
            listBoxFileToSend.SelectedItem = "None";
            listBoxLocation.SelectedItem = "NY";
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            CurrentState = "Edit";
            EnableFields();
            buttonDelete.Enabled = true;
            buttonSave.Enabled = true;
            buttonEdit.Enabled = false;
            buttonInsert.Enabled = false;
            buttonSubscribers.Enabled = true;
        }

        private void textBoxAlertInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }

        private void buttonSubscribers_Click(object sender, EventArgs e)
        {
            // User selects code to be processed
            FrmMaintainSubscribers myfrmMaintainSubscribers = new FrmMaintainSubscribers();
            myfrmMaintainSubscribers.CurrentStepid = CurrentStepid;
            myfrmMaintainSubscribers.Text = "Subscribers to: " + textBoxDescrip.Text;
            myfrmMaintainSubscribers.ShowDialog();
        }
    }
}