using System.Windows.Forms;

namespace WSGUtilitieslib
{
    public class WSGUtilities
    {
        // Contructor to initialize elements
        public WSGUtilities(string appname)
        {
            ApplicationName = appname; // establish application name
        } // end constructor

        private string applicationName; // Name of application using the class

        public string ApplicationName
        {
            get
            {
                return applicationName;
            }// end get
            set
            {
                applicationName = value;
            } // end set
        } // end property ApplicationName

        public bool wsgReply(string wsgQuestion)
        {
            return System.Windows.Forms.MessageBox.Show(wsgQuestion, ApplicationName,
             MessageBoxButtons.YesNo, MessageBoxIcon.Question)
             == DialogResult.Yes;
        } // end wsgreply

        public void wsgNotice(string wsgNotice)
        {
            MessageBox.Show(wsgNotice, ApplicationName,
             MessageBoxButtons.OK, MessageBoxIcon.Information);
        } // end wsgreply
    }// End Class wsgutilities
}