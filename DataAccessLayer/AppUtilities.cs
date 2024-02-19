using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public class AppUtilities
    {
        public string SMTPServer = ConfigurationManager.AppSettings["SMTPServer"];
        public string SMTPUser = ConfigurationManager.AppSettings["SMTPUser"];
        public string SMTPNotifyAddress = ConfigurationManager.AppSettings["NotifyAddress"];
        public string SMTPPassword = ConfigurationManager.AppSettings["SMTPassword"];

        public string SMTPFrom = ConfigurationManager.AppSettings["SMTPFromName"] + "<" +
          ConfigurationManager.AppSettings["SMTPFromAddress"] + ">";

        public string SMTPMailCC = "";

        public void makeSQLCommand(ref SqlCommand mysqlcommand, ref SqlConnection mysqlconnection)
        {
            mysqlcommand.CommandType = CommandType.StoredProcedure;
            mysqlcommand.Connection = mysqlconnection;
            //   mysqlconnection.ConnectionString = myAppContants.SQLConnectionString;
        }

        public bool IsFileOpen(string filePath)
        {
            bool rtnvalue = false;
            try
            {
                System.IO.FileStream fs = System.IO.File.OpenWrite(filePath);
                fs.Close();
            }
            catch (System.IO.IOException)
            {
                rtnvalue = true;
            }
            return rtnvalue;
        }

        public void setScreenSize(Form formObject)
        {
            // Retrieve the working rectangle from the Screen class
            // using the PrimaryScreen and the WorkingArea properties.
            System.Drawing.Rectangle workingRectangle =
                Screen.PrimaryScreen.WorkingArea;

            // Set the size of the form slightly less than size of
            // working rectangle.
            formObject.Size = new System.Drawing.Size(
                workingRectangle.Width - 60, workingRectangle.Height - 60);

            // Set the location so the entire form is visible.
            formObject.Location = new System.Drawing.Point(5, 5);
        }

        public void logEvent(SqlConnection sqlconn, string EventDesc, string EventResult, string UserID, bool sendmail)
        {
            if (sendmail == true)
            {
            }// end if sendmail == true

            SqlCommand cmd = new SqlCommand("dbo.sp_insertlogevent");
            this.makeSQLCommand(ref cmd, ref sqlconn);
            this.makeSQLCommand(ref cmd, ref sqlconn);
            cmd.Parameters.Add("@eventdesc", SqlDbType.Char);
            cmd.Parameters["@eventdesc"].Value = EventDesc;
            cmd.Parameters.Add("@eventresult", SqlDbType.Char);
            cmd.Parameters["@eventresult"].Value = EventResult;
            cmd.Parameters.Add("@userid", SqlDbType.Char);
            cmd.Parameters["@userid"].Value = UserID;
            try
            {
                sqlconn.Open();
                cmd.ExecuteNonQuery();
                sqlconn.Close();
            } // end try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL Error");
                sqlconn.Close();
            } // end catch
        } // end log event
    } // end class Apputilities
} // end namespace
