using CommonAppClasses;
using System.Data;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Tracking
{
    internal class TrackingProcessing : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        public order somastds = new order();
        public customer ards = new customer();
        public order solineds = new order();
        public FrmTrackingSearch sosearchForm { get; set; }
        public tracking trackingds = new tracking();
        public tracking listtrackingds = new tracking();
        private Estimating.Soinf soinf = new Estimating.Soinf("SQL", "SQLConnString");

        public TrackingProcessing(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
        }

        public void ShowSO(string sono, string inspectionstep, Form mdiparent)
        {
            // Test for blank and skip it
            if (sono != "")
            {
                // Get the somast record
                soinf.GetSomastBySono(sono);
                if (soinf.somastds.somast[0].enterqu == "Y")
                {
                    if (inspectionstep != "Y")
                    {
                        Estimating.FrmSOHead frmSoHead = new Estimating.FrmSOHead();
                        frmSoHead.PassedSono = sono;
                        frmSoHead.MdiParent = mdiparent;
                        frmSoHead.Show();
                    }
                    else
                    {
                        //Create the Inspection Information processing object
                        Inspection.FrmRepairInspection frmRepairInspection = new Inspection.FrmRepairInspection();
                        Inspection.InspInf inspInf = new Inspection.InspInf("SQL", "SQLConnString", frmRepairInspection);
                        inspInf.PassedSono = sono;
                        frmRepairInspection.Show();
                    }
                }
                else
                {
                    MiscellaneousOrderEntry.FrmMiscOrder frmMiscOrder = new MiscellaneousOrderEntry.FrmMiscOrder();
                    frmMiscOrder.PassedSono = sono;
                    frmMiscOrder.MdiParent = mdiparent;
                    frmMiscOrder.Show();
                }
            }
        }

        public void SearchSoTracking()
        {
            this.ClearParameters();
            trackingds.view_latestsotrackingstepdata.Rows.Clear();
            string commandstring = "SELECT *  from view_latestsotrackingstepdata WHERE ";
            commandstring += " LTRIM(sono) LIKE  @sono ";
            commandstring += " AND UPPER(ponum) LIKE @ponum ";
            if (sosearchForm.textBoxInvno.Text.TrimEnd() != "")
            {
                commandstring += " AND LTRIM(invno) LIKE @invno  ";
            }
            switch (sosearchForm.IncludeScope)
            {
                case "Estimates Only":
                    {
                        commandstring += " AND sotype = 'B'  ";
                        break;
                    }
                case "Orders Only":
                    {
                        commandstring += " AND sotype = 'O'  ";
                        break;
                    }
            }
            commandstring += " AND UPPER(custno) LIKE @custno  ";
            commandstring += " AND UPPER(lname) LIKE @lname  ";
            commandstring += " AND UPPER(meycono) LIKE @meycono ";
            commandstring += " AND ordate BETWEEN @begindate AND @enddate ";
            commandstring += "  AND sostat <> 'V' ";
            commandstring += "  ORDER BY sono ";

            this.AddParms("@sono", sosearchForm.textBoxSono.Text.TrimStart().TrimEnd() + "%", "SQL");
            this.AddParms("@meycono", sosearchForm.textBoxMeycono.Text.TrimEnd() + "%", "SQL");
            this.AddParms("@lname", sosearchForm.textBoxLname.Text.TrimEnd() + "%", "SQL");
            this.AddParms("@ponum", sosearchForm.textBoxPonum.Text.TrimEnd() + "%", "SQL");
            if (sosearchForm.textBoxInvno.Text.TrimEnd() != "")
            {
                this.AddParms("@invno", sosearchForm.textBoxInvno.Text.TrimEnd() + "%", "SQL");
            }
            this.AddParms("@custno", sosearchForm.textBoxCustno.Text.TrimEnd().ToUpper() + "%", "SQL");
            this.AddParms("@begindate", sosearchForm.dateTimePickerShipFirstDate.Value, "SQL");
            this.AddParms("@enddate", sosearchForm.dateTimePickerShipLastDate.Value, "SQL");
            this.FillData(trackingds, "view_latestsotrackingstepdata", commandstring, CommandType.Text);
        }
    }
}