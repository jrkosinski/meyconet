using CommonAppClasses;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Estimating
{
    public class VersionSelector : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Inpsection");
        public string CurrentSono { get; set; }
        public string CurrentState { get; set; }
        public string SelectedVersion { get; set; }
        public inspection inspds = new inspection();
        public FrmInspVersionSelector parentform { get; set; }
        public BindingSource bindingVersionSelector { get; set; }

        public VersionSelector(string DataStore, string AppConfigName, FrmInspVersionSelector callingform)
         : base(DataStore, AppConfigName)
        {
            bindingVersionSelector = new BindingSource();
            parentform = callingform;

            //      SetEvents();
            CurrentState = "Select";
            //      RefreshParentControls();
        }

        public void SetEvents()
        {
            parentform.dataGridViewVersionSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(CaptureVersion);
            parentform.buttonClose.Click += new System.EventHandler(CloseForm);
        }

        public void SetBindings()
        {
            parentform.dataGridViewVersionSelector.AutoGenerateColumns = false;
            parentform.dataGridViewVersionSelector.DataSource = bindingVersionSelector;
            bindingVersionSelector.DataSource = inspds.view_inspreport;
            parentform.dataGridViewVersionSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
            parentform.dataGridViewVersionSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        public void LoadVersionViewData()
        {
            inspds.view_inspreport.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", CurrentSono, "SQL");
            this.FillData(inspds, "view_inspreport", "wsgsp_getinspreportdata", CommandType.StoredProcedure);
        }

        private void CaptureVersion(object sender, DataGridViewCellEventArgs e)
        {
            SelectedVersion = CaptureDataGridColumn(parentform.dataGridViewVersionSelector, "version");
            parentform.Close();
        }

        public void CloseForm(object sender, EventArgs e)
        {
            parentform.Close();
        }
    } // class
} // namespace