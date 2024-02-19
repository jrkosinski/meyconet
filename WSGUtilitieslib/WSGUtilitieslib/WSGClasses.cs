using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;

namespace WSGUtilitieslib
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
        private string meycoPath = ConfigurationManager.AppSettings["VFPDataPath"]; //TODO: used for? 

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
        private string meycoSysPath = ConfigurationManager.AppSettings["VFPSysDataPath"]; //TODO: what is it used for?

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

        // Establish PDFPath property
        private string pDFPath = ConfigurationManager.AppSettings["PDFpath"];

        public string PDFPath
        {
            get
            {
                return pDFPath;
            }
            set
            {
                pDFPath = value;
            }
        } // end PDFPath

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
            catch (System.IO.IOException ex)
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
                SMTPMailer mySMTPMailer = new SMTPMailer();
                mySMTPMailer.SendMailMessasge(SMTPNotifyAddress, SMTPFrom, "Meyco System Notification", SMTPServer,
                 SMTPUser, SMTPPassword, SMTPMailCC, EventDesc + Environment.NewLine + EventResult);
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
                WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(cmd);
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

    public static class AppUserClass
    {
        private static string appUserId;

        public static string AppUserId
        {
            get
            {
                return appUserId;
            }
            set
            {
                appUserId = value;
            }
        } // end property AppUserId
    } // end class AppUser

    public class SMTPMailer
    {
        public void SendMailMessasge(string MailSendToAddress, string MailSendFrom,
            string MailSubject, string MailSMTPServer, string MailSMTPUser, string MailSMTPPassword, string MailCC, string MailBody)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(MailSendToAddress);
            if (!(MailCC.TrimEnd() == ""))
                message.CC.Add(MailCC);
            message.Subject = MailSubject;
            message.From = new System.Net.Mail.MailAddress(MailSendFrom);
            //      System.IO.TextReader txtRead = new System.IO.StreamReader(@"D:\Attachments\Directory\CM-ODM Directory\13 06\checksum.txt");
            message.Body = MailBody;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(MailSMTPServer, 25);
            smtp.Credentials = new NetworkCredential(MailSMTPUser, MailSMTPPassword);
            smtp.Send(message);
        } // end  SendMailMessage
    } // end class SMTP mailer
} // end namespace
