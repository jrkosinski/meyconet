using System.Configuration;

namespace DataAccessLayer
{
    public class AppConstants
    {
        private string sqlConnectionString = ConfigurationManager.AppSettings["SQLConnString"];

        public string SQLConnectionString
        {
            get
            {
                return sqlConnectionString;
            }
            set
            {
                sqlConnectionString = value;
            }
        } // end property SQLConnectionString

        // Establish meycopath property
        private string meycoPath = ConfigurationManager.AppSettings["VFPDataPath"]; //TODO: what is this used for? 

        public string MeycoPath
        {
            get
            {
                return meycoPath;
            }
            set
            {
                meycoPath = value;
            }
        } // end meycopath

        // Establish meycosyspath property
        private string meycoSysPath = ConfigurationManager.AppSettings["VFPSysDataPath"];

        public string MeycoSysPath
        {
            get
            {
                return meycoSysPath;
            }
            set
            {
                meycoSysPath = value;
            }
        } // end meycopath

        // Establish VFP Connection String property
        private string vfpSysConnstring = @"Provider=VFPOLEDB;data source= ";

        public string VfpSysConnstring
        {
            get
            {
                return vfpSysConnstring;
            }
            set
            {
                vfpSysConnstring = value;
            }
        } // end VfpConnstring

        // Establish VFP Connection String property
        private string vfpConnstring = @"Provider=VFPOLEDB;data source= ";

        public string VfpConnstring
        {
            get
            {
                return vfpConnstring;
            }
            set
            {
                vfpConnstring = value;
            }
        } // end VfpConnstring
    } // end class AppConstants
}
