using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace BusinessProcessing
{
    public partial class FrmLogin : WSGBaseClassLibrary.WSGFrmBase
    {
        private BindingSource bindingRouteData = new BindingSource();
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("User Login");

        public FrmLogin()
        {
            InitializeComponent();
            conn.ConnectionString = myAppconstants.SQLConnectionString;

            if (Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["TestMode"]))
            {
                this.textBoxUserId.Text = "ADMN";
                this.textBoxPassword.Text = "1536";
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.textBoxUserId.Text = "";
            this.textBoxPassword.Text = "";

            this.Close();
        }

        private void buttonProceed_Click(object sender, EventArgs e)
        {
            string userId = textBoxUserId.Text.ToUpper().TrimEnd(); 

            // Login processing
            SqlCommand cmd = new SqlCommand("dbo.sp_applogin");
            appUtilities.makeSQLCommand(ref cmd, ref conn);
            cmd.Parameters.Add("@userid", SqlDbType.Char);
            cmd.Parameters["@userid"].Value = textBoxUserId.Text.ToUpper().TrimEnd();
            cmd.Parameters.Add("@passwd", SqlDbType.Char);
            cmd.Parameters["@passwd"].Value = textBoxPassword.Text.ToUpper().TrimEnd();
            SqlParameter LoginMessage = new SqlParameter("@LoginMessage", SqlDbType.Char, 20);
            LoginMessage.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(LoginMessage);

            conn.Open();
            try
            {
                WSGUtilitieslib.Telemetry.Telemetry.SetUserId(userId); 
                WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();

                if (cmd.Parameters["@LoginMessage"].Value.ToString().Trim() == "OK")
                {
                    AppUserClass.AppUserId = textBoxUserId.Text.TrimEnd();
                    this.Close();
                }
                else
                {
                    wsgUtilities.wsgNotice(cmd.Parameters["@LoginMessage"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                HandleException(ex);
            }
        }

        private void textBoxUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBoxUserId_Leave(object sender, EventArgs e)
        {
            textBoxUserId.Text = textBoxUserId.Text.ToUpper();
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}