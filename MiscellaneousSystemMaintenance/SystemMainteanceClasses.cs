using CommonAppClasses;
using System.Data;
using System.Data.SqlClient;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public class MiscSysInf : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private quote quoteds { get; set; }

        public MiscSysInf(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
        }

        public void ClearSomastLocks()
        {
            this.ClearParameters();
            try
            {
                ExecuteCommand("wsgsp_clearsomastlocks", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Somast locks cleared");
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void ClearCapacityCalendar()
        {
            this.ClearParameters();
            try
            {
                ExecuteCommand("wsgsp_clearCapacityCalendar", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Capacity Calendar locks cleared");
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void ClearProductionUnitSchedule()
        {
            this.ClearParameters();
            try
            {
                ExecuteCommand("wsgsp_clearproductionunitschedule", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Production Unit Schedule locks cleared");
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void ClearEmailAddress()
        {
            this.ClearParameters();
            try
            {
                ExecuteCommand("wsgsp_clearemailaddress", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Email Address locks cleared");
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void ClearAracadrLocks()
        {
            this.ClearParameters();
            try
            {
                ExecuteCommand("wsgsp_cleararacadrlocks", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Aracadr locks cleared");
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void ClearWarrantyLocks()
        {
            this.ClearParameters();
            try
            {
                ExecuteCommand("wsgsp_clearwarrantylocks", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Warranty locks cleared");
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void ClearArcustLocks()
        {
            this.ClearParameters();
            try
            {
                ExecuteCommand("wsgsp_cleararcustlocks", CommandType.StoredProcedure);
                wsgUtilities.wsgNotice("Arcust locks cleared");
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }
        }

        public void ClearInvoicing()
        {
            UnlockInvoicing();
            wsgUtilities.wsgNotice("Invoicing cleared");
        }

        public void ConvertWarranty()
        {
            WarrantySynchronization ws = new WarrantySynchronization("SQL", "SQLConnString");
            quote quoteds = new quote();
            if (wsgUtilities.wsgReply("Convert Warranty?"))
            {
                string commandstring = "SELECT * from somast WHERE RTRIM(invno) <> ''";
                quoteds.somast.Rows.Clear();
                this.ClearParameters();
                this.FillData(quoteds, "somast", commandstring, CommandType.Text);
                if (quoteds.somast.Rows.Count > 0)
                {
                    for (int i = 0; i < quoteds.somast.Rows.Count; i++)
                    {
                        ws.SynchronizeWarranty(quoteds.somast[i].sono);
                    }
                }
                wsgUtilities.wsgNotice("Warranty Conversion Complete");
            }
        }
    }
}