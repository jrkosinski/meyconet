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
          DateTime begindate, DateTime enddate)
        {
            somastds.view_somastdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@ponum", ponum, "SQL");
            this.AddParms("@custno", custno, "SQL");
            this.AddParms("@includetype", includetype, "SQL");
            this.AddParms("@lname", lname, "SQL");
            this.AddParms("@meycono", meycono, "SQL");
            this.AddParms("@begindate", begindate.Date, "SQL");
            this.AddParms("@enddate", enddate.Date, "SQL");
            this.FillData(somastds, "view_somastdata", "wsgsp_searchsomast", CommandType.StoredProcedure);
        }

        public void GetSoSearchData(string sono, string ponum, string custno, string includetype, string lname, string meycono,
          DateTime begindate, DateTime enddate, string enterqu)
        {
            somastds.view_somastdata.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            this.AddParms("@ponum", ponum, "SQL");
            this.AddParms("@custno", custno, "SQL");
            this.AddParms("@includetype", includetype, "SQL");
            this.AddParms("@lname", lname, "SQL");
            this.AddParms("@meycono", meycono, "SQL");
            this.AddParms("@begindate", begindate.Date, "SQL");
            this.AddParms("@enddate", enddate.Date, "SQL");
            this.AddParms("@enterqu", enterqu, "SQL");
            this.FillData(somastds, "view_somastdata", "wsgsp_searchsomastqu", CommandType.StoredProcedure);
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