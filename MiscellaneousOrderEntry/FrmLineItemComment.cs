using CommonAppClasses;
using System;
using System.Windows.Forms;

namespace MiscellaneousOrderEntry
{
    public partial class FrmLineItemComment : WSGUtilitieslib.Telemetry.Form
    {
        private BindingSource bindingSystemCommentsData = new BindingSource();
        private SystemCommentsMaintenance syscomm = new SystemCommentsMaintenance("SQL", "SQLConnString");
        public String CurrentCommenttype { get; set; }
        public order commentds { get; set; }

        public FrmLineItemComment()
        {
            InitializeComponent();
            commentds = new order();
            syscomm.GetSystemCommentsData();
            bindingSystemCommentsData.DataSource = syscomm.systemds.view_systemcomments;
            dataGridViewSystemComments.DataSource = bindingSystemCommentsData;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            commentds.AcceptChanges();
            this.Close();
        }

        private void dataGridViewSystemComments_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            syscomm.getSingleSystemcommentsData(syscomm.CaptureIdCol(dataGridViewSystemComments));
            if (CurrentCommenttype == "Internal")
            {
                commentds.soline[0].intmemo += Environment.NewLine + syscomm.systemds.systemcomments[0].descrip +
                Environment.NewLine;
            }
            else
            {
                commentds.soline[0].custmemo += Environment.NewLine + syscomm.systemds.systemcomments[0].descrip +
                Environment.NewLine;
            }
            dataGridViewSystemComments.Visible = false;
            commentds.AcceptChanges();
            this.Update();
        }

        private void FrmLineItemComment_Load(object sender, EventArgs e)
        {
            textBoxCustcomments.DataBindings.Clear();
            textBoxCustcomments.DataBindings.Add("Text", commentds.soline, "custmemo");
            textBoxIntcomments.DataBindings.Clear();
            textBoxIntcomments.DataBindings.Add("Text", commentds.soline, "intmemo");
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
    }
}