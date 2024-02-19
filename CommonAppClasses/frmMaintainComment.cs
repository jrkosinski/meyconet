using System;
using System.Data;
using System.Data.SqlClient;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public partial class frmMaintainComment : WSGBaseClassLibrary.WSGFrmBase
    {
        public SqlConnection conn = new SqlConnection();
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Comment Mainenance");

        public frmMaintainComment()
        {
            InitializeComponent();
            conn.ConnectionString = myAppconstants.SQLConnectionString;
        }

        private string commentText;

        public string CommentText
        {
            get
            {
                return commentText;
            }
            set
            {
                commentText = value;
            }
        }

        private int currentIdcol;

        public int CurrentIdcol
        {
            get
            {
                return currentIdcol;
            }
            set
            {
                currentIdcol = value;
            }
        }

        private void frmMaintainComment_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Save comments
            // Establish the SQL command and its parameters
            SqlCommand cmd = new SqlCommand("dbo.sp_updatetrackingcomment");
            appUtilities.makeSQLCommand(ref cmd, ref conn);
            cmd.Parameters.Add("@idcol", SqlDbType.Int);
            cmd.Parameters["@idcol"].Value = CurrentIdcol;
            cmd.Parameters.Add("@comment", SqlDbType.VarChar);
            cmd.Parameters["@comment"].Value = textBoxComment.Text;
            cmd.Parameters.Add("@userid", SqlDbType.Char);
            cmd.Parameters["@userid"].Value = AppUserClass.AppUserId;

            try
            {
                conn.Open();
                WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
                wsgUtilities.wsgNotice("Comment Saved");
                this.Close();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMaintainComment_Shown(object sender, EventArgs e)
        {
            // Populate the text box
            this.textBoxComment.Text = CommentText;
        }
    }
}