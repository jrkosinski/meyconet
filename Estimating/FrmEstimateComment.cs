using CommonAppClasses;
using System;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Estimating
{
    public partial class FrmEstimateComment : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Estimate Comments");
        // Create the SO Information processing object

        // Create the System Comments processing object

        private BindingSource bindingSystemCommentsData = new BindingSource();

        public FrmEstimateComment()
        {
            InitializeComponent();
            dataGridViewSystemComments.AutoGenerateColumns = false;
            dataGridViewSystemComments.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewSystemComments.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            syscomm.GetSystemCommentsData();
            bindingSystemCommentsData.DataSource = syscomm.systemds.view_systemcomments;
            dataGridViewSystemComments.DataSource = bindingSystemCommentsData;
        }

        private SystemCommentsMaintenance syscomm = new SystemCommentsMaintenance("SQL", "SQLConnString");
        public quote versionds { get; set; }
        public String CurrentCommenttype { get; set; }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewSystemComments_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            syscomm.getSingleSystemcommentsData(syscomm.CaptureIdCol(dataGridViewSystemComments));

            if (CurrentCommenttype == "Internal")
            {
                versionds.soversion[0].intcomments += Environment.NewLine + syscomm.systemds.systemcomments[0].descrip +
                Environment.NewLine;
            }
            else
            {
                versionds.soversion[0].custcomments += Environment.NewLine + syscomm.systemds.systemcomments[0].descrip +
                Environment.NewLine;
            }
            dataGridViewSystemComments.Visible = false;
            versionds.AcceptChanges();
            this.Update();
        }

        private void textBoxIntcomments_DoubleClick(object sender, EventArgs e)
        {
            CurrentCommenttype = "Internal";
            dataGridViewSystemComments.Visible = true;
            this.Update();
        }

        private void textBoxCustcomments_DoubleClick(object sender, EventArgs e)
        {
            CurrentCommenttype = "Customer";
            dataGridViewSystemComments.Visible = true;
            this.Update();
        }

        private void FrmEstimateComment_Load(object sender, EventArgs e)
        {
            checkBoxManalert.DataBindings.Clear();
            checkBoxManalert.DataBindings.Add("Checked", versionds.soversion, "manalert");
            checkBoxDesalert.DataBindings.Add("Checked", versionds.soversion, "desalert");
            checkBoxQcalert.DataBindings.Add("Checked", versionds.soversion, "qcalert");
            checkBoxCsalert.DataBindings.Add("Checked", versionds.soversion, "csalert");
            checkBoxShipalert.DataBindings.Add("Checked", versionds.soversion, "shipalert");
            checkBoxAralert.DataBindings.Add("Checked", versionds.soversion, "aralert");
            textBoxCustcomments.DataBindings.Clear();
            textBoxCustcomments.DataBindings.Add("Text", versionds.soversion, "custcomments");
            textBoxIntcomments.DataBindings.Clear();
            textBoxIntcomments.DataBindings.Add("Text", versionds.soversion, "intcomments");
        }
    }
}