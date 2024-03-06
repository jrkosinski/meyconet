using System;
using System.Data;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    #region SO Search Information

    public class SoSearchInf : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Search Information");
        public quote somastds { get; set; }
        public FrmSOSearch parentform { get; set; }

        public SoSearchInf(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
            somastds = new quote();
        }

        public void GetSoSearchData(string sono, string ponum, string custno, string includetype, string lname, string meycono,
          DateTime begindate, DateTime enddate, string enterqu = null)
        {
            somastds.view_somastdata.Rows.Clear();
            string spName = "wsgsp_searchsomast";
            if (enterqu != null)
            {
                this.AddParms("@enterqu", enterqu, "SQL");
            }
            this.FillData(somastds, "view_somastdata", spName, CommandType.StoredProcedure);
        }

        public string CaptureSono(DataGridView myDataGridView)
        {
            CurrencyManager xCM =
           (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
           myDataGridView.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Return the sono
            return (string)xRow["sono"];
        }
    } // class

    #endregion SO Search Information
} // namespace