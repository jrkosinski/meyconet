using CommonAppClasses;
using System;
using System.Data;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Estimating
{
    public class CustomerChangeMethods : WSGDataAccess
    {
        public Form menuForm { get; set; }
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private string CurrentSono = "";
        private string CurrentCustno = "";
        private quote quoteds = new quote();
        private WSGUtilities wsgUtilities = new WSGUtilities("Order Entry");

        // Create the App Information processing object
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");

        public GetSoMethods getSoMethods = new GetSoMethods("SQL", "SQLConnString");
        private Soinf soinf = new Soinf("SQL", "SQLConnString");
        private FrmChangeSOCustomer frmChangeSOCustomer = new FrmChangeSOCustomer();

        public CustomerChangeMethods(string DataStore, string AppConfigName)
             : base(DataStore, AppConfigName)
        {
        }

        public void StartApp()
        {
            CurrentCustno = "";
            CurrentSono = "";
            SetEvents();
            frmChangeSOCustomer.ShowDialog();
        }

        public void SetEvents()
        {
            frmChangeSOCustomer.buttonReturn.Click += new System.EventHandler(CloseForm);
            frmChangeSOCustomer.buttonSO.Click += new EventHandler(buttonSelectSoClick);
            frmChangeSOCustomer.buttonCustomer.Click += new EventHandler(buttonSelectCustomerClick);
            frmChangeSOCustomer.buttonProceed.Click += new EventHandler(buttonProceedClick);
        }

        public void CloseForm(object sender, EventArgs e)
        {
            frmChangeSOCustomer.Close();
        }

        private void buttonSelectCustomerClick(object sender, EventArgs e)
        {
            GetCustomerMethods getCust = new GetCustomerMethods("SQL", "SQLConnString");

            if (getCust.SelectedCustid != 0)
            {
                // Get customer
                this.ClearParameters();
                this.AddParms("@idcol", getCust.SelectedCustid, "SQL");
                quoteds.arcust.Rows.Clear();
                string CommandString = "SELECT * FROM arcust WHERE idcol = @idcol";
                FillData(quoteds, "arcust", CommandString, CommandType.Text);
                frmChangeSOCustomer.labelCustno.Text = quoteds.arcust[0].custno;
                CurrentCustno = quoteds.arcust[0].custno;
            }
            else
            {
                CurrentCustno = "";
            }
            RefreshControls();
        }

        private void buttonProceedClick(object sender, EventArgs e)
        {
            if (quoteds.somast[0].custno == quoteds.arcust[0].custno)
            {
                wsgUtilities.wsgNotice("The new dealer is the same as the old dealer");
            }
            else
            {
                if (wsgUtilities.wsgReply("Replace dealer " + quoteds.somast[0].custno + " with " + quoteds.arcust[0].custno + "?"))
                {
                    // Update somast with the new customer data
                    quoteds.somast[0].custno = quoteds.arcust[0].custno;
                    quoteds.somast[0].custid = quoteds.arcust[0].idcol;
                    quoteds.somast[0].pdays = quoteds.arcust[0].pdays;
                    quoteds.somast[0].pnet = quoteds.arcust[0].pnet;
                    quoteds.somast[0].pdisc = quoteds.arcust[0].pdisc;
                    quoteds.somast[0].pterms = quoteds.arcust[0].pterms;
                    quoteds.somast[0].taxrate = quoteds.arcust[0].tax;
                    quoteds.somast[0].fob = quoteds.arcust[0].fob;
                    quoteds.somast[0].shipvia = quoteds.arcust[0].shipvia;
                    quoteds.somast[0].glarec = quoteds.arcust[0].gllink;
                    quoteds.somast[0].salesmn = quoteds.arcust[0].salesmn;
                    quoteds.somast[0].salesdisc = quoteds.arcust[0].disc;
                    quoteds.somast[0].taxdist = quoteds.arcust[0].taxdist;
                    quoteds.somast[0].terr = quoteds.arcust[0].terr;
                    quoteds.somast[0].taxrate = quoteds.arcust[0].tax;
                    quoteds.somast[0].taxst = quoteds.arcust[0].state;
                    quoteds.somast[0].glarec = quoteds.arcust[0].gllink;
                    quoteds.somast[0].stockdisc = quoteds.arcust[0].stockdisc;
                    quoteds.somast[0].standdisc = quoteds.arcust[0].standdisc;
                    quoteds.somast[0].commldisc = quoteds.arcust[0].commldisc;
                    quoteds.somast[0].repldisc = quoteds.arcust[0].repldisc;
                    quoteds.somast[0].repdisc = quoteds.arcust[0].repdisc;
                    quoteds.somast[0].shipdisc = quoteds.arcust[0].shipdisc;
                    quoteds.somast[0].upcharge = quoteds.arcust[0].upcharge;
                    GenerateAppTableRowSave(quoteds.somast[0]);
                    soinf.CalcSOPrices(quoteds.somast[0].sono);
                    wsgUtilities.wsgNotice("Dealer Change Complete");
                }
                else
                {
                    wsgUtilities.wsgNotice("Dealer Change Aborted");
                }
            }
        }

        private void buttonSelectSoClick(object sender, EventArgs e)
        {
            CurrentSono = "";
            getSoMethods.returnsono = "";
            getSoMethods.GetSono();
            if (!getSoMethods.wascancelled)
            {
                if (getSoMethods.returnsono.TrimEnd() != "")
                {
                    frmChangeSOCustomer.labelSono.Text = "SO -" + getSoMethods.returnsono.TrimEnd();
                    CurrentSono = getSoMethods.returnsono;
                    // Get somastr
                    this.ClearParameters();
                    this.AddParms("@sono", CurrentSono, "SQL");
                    quoteds.somast.Rows.Clear();
                    string CommandString = "SELECT * FROM somast WHERE sono = @sono";
                    FillData(quoteds, "somast", CommandString, CommandType.Text);
                }
            }
            RefreshControls();
        }

        private void RefreshControls()
        {
            if (CurrentSono.TrimEnd() != "" && CurrentCustno.TrimEnd() != "")
            {
                frmChangeSOCustomer.buttonProceed.Enabled = true;
            }
            else
            {
                frmChangeSOCustomer.buttonProceed.Enabled = false;
            }
        }
    }
}