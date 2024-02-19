using CommonAppClasses;
using System.Data;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Estimating
{
    public class CopyQuoteMethods : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Inspection");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private Estimating.Soinf soinf = new Estimating.Soinf("SQL", "SQLConnString");
        public quote quoteds = new quote();
        public quote quotesearchds = new quote();
        public string CurrentState { get; set; }
        public GetSoMethods getsomethods = new GetSoMethods("SQL", "SQLConnString");

        public CopyQuoteMethods(string DataStore, string AppConfigName, Form Menuform)
          : base(DataStore, AppConfigName)
        {
        }

        public void StartApp(Form Menuform)
        {
            string oldsono = getsomethods.GetSono();
            if (oldsono.TrimEnd() != "")
            {
                // Copy the quote if found
                string newsono = soinf.CopyQuote(oldsono);
                if (newsono.TrimEnd() != "")
                {
                    soinf.somastds.somast.Rows.Clear();
                    ClearParameters();
                    AddParms("@sono", newsono, "SQL");
                    string commandstring = "SELECT * FROM somast WHERE sono = @sono";
                    this.FillData(soinf.somastds, "somast", commandstring, CommandType.Text);
                    // Activate Estimating form
                    Estimating.FrmSOHead frmSoHead = new Estimating.FrmSOHead();
                    frmSoHead.PassedSono = soinf.somastds.somast[0].sono;
                    frmSoHead.MdiParent = Menuform;
                    frmSoHead.Show();
                }
            }
        }
    }
}