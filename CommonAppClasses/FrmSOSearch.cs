using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    //CACHED 
    public partial class FrmSOSearch : WSGBaseClassLibrary.WSGFrmBase
    {
        public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Search");

        // Create the App Information processing object
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");

        // Create the SO Information processing object
        private static SoSearchInf soSearchInf = new SoSearchInf("SQL", "SQLConnString");

        public string SelectedSono { get; set; }
        public string IncludeType { get; set; }
        public string ItemSearchKey { get; set; }
        public bool wascancelled = false;
        public string Enterqu = null;

        private BindingSource bindingSoSelectionData = new BindingSource();
        private static ObjectCacheWithParams dataCache = new ObjectCacheWithParams(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_SOSearch"]));

        public FrmSOSearch(string sono = null, string enterqu = null)
        {
            InitializeComponent();

            // Set the screen size
            // appUtilities.setScreenSize(this);

            SelectedSono = "";
            // Default to include Orders and Estimates
            IncludeType = "OB";
            Enterqu = enterqu;
            // The value for alternating rows overrides the value for all rows.
            dataGridviewSoSearch.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridviewSoSearch.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dataGridviewSoSearch.AutoGenerateColumns = false;

            if (!dataCache.IsInvalid && String.IsNullOrEmpty(sono))
                bindingSoSelectionData.DataSource = soSearchInf.somastds.view_somastdata;
            dataGridviewSoSearch.DataSource = bindingSoSelectionData;

            this.AcceptButton = this.buttonSearch;

            initializeSearchBoxes();

            if (!String.IsNullOrEmpty(sono))
                this.textBoxSono.Text = sono;
        }

        #region methods

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            wascancelled = true;
            this.Close();
        }

        #region buttonSearch_Click

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            soSearchInf.GetSoSearchData(textBoxSono.Text.TrimStart().TrimEnd(), textBoxPonum.Text.TrimStart().TrimEnd(),
            textBoxCustno.Text.TrimStart().TrimEnd(), IncludeType, textBoxLname.Text.TrimStart().TrimEnd(),
            textBoxMeycono.Text.TrimStart().TrimEnd(), dateTimePickerFirstSoDate.Value, dateTimePickerLastSoDate.Value, Enterqu);
            dataGridviewSoSearch.Focus();

            dataCache.SearchParams["custno"] = textBoxCustno.Text.TrimStart().TrimEnd();
            dataCache.SearchParams["lname"] = textBoxLname.Text.TrimStart().TrimEnd();
            dataCache.SearchParams["meycono"] = textBoxMeycono.Text.TrimStart().TrimEnd();
            dataCache.SearchParams["ponum"] = textBoxPonum.Text.TrimStart().TrimEnd();
            dataCache.SearchParams["sono"] = textBoxSono.Text.TrimStart().TrimEnd();
            dataCache.SearchParams["date1"] = textBoxFirstSoDate.Text;
            dataCache.SearchParams["date2"] = textBoxLastSoDate.Text;

            dataCache.Refresh(soSearchInf);
            bindingSoSelectionData.DataSource = soSearchInf.somastds.view_somastdata;

        } // end buttonSearch_Click

        #endregion buttonSearch_Click

        private void textBoxCustno_Leave(object sender, EventArgs e)
        {
            this.textBoxCustno.Text = this.textBoxCustno.Text.ToUpper();
        }

        public static DataSet AddTables(ref DataSet dSet, DataTable newTable)
        {
            dSet.Tables.Add(newTable);
            return dSet;
        }

        #region dataGridViewTrackingSearch_CellClick

        private void dataGridViewTrackingSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        } // end cell click

        #endregion dataGridViewTrackingSearch_CellClick

        private void initializeSearchBoxes()
        {
            //Initialize search boxes
            textBoxPonum.Text = "";
            textBoxSono.Text = "";
            textBoxMeycono.Text = "";
            textBoxLname.Text = "";
            textBoxCustno.Text = "";
            textBoxFirstSoDate.Text = "";
            dateTimePickerFirstSoDate.Value -= TimeSpan.Parse("365.00:00:00");
            dateTimePickerLastSoDate.Value = DateTime.Now;

            this.textBoxCustno.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["custno"];
            this.textBoxLname.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["lname"];
            this.textBoxMeycono.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["meycono"];
            this.textBoxPonum.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["ponum"];
            this.textBoxSono.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["sono"];

            if (!String.IsNullOrEmpty(dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["date1"]))
            {
                this.textBoxFirstSoDate.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["date1"];
                this.dateTimePickerFirstSoDate.Value = DateTime.ParseExact(this.textBoxFirstSoDate.Text, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["date2"]))
            {
                this.textBoxLastSoDate.Text = dataCache.IsInvalid ? String.Empty : dataCache.SearchParams["date2"];
                this.dateTimePickerLastSoDate.Value = DateTime.ParseExact(this.textBoxLastSoDate.Text,"M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Clear the search boxes, grid and buttons
            initializeSearchBoxes();
        }

        private void textBoxShipFirstDate_DoubleClick(object sender, EventArgs e)
        {
            textBoxFirstSoDate.Text = "";
        }

        private void textBoxShipLastDate_DoubleClick(object sender, EventArgs e)
        {
            textBoxLastSoDate.Text = "";
        }

        #endregion methods

        private void dateTimePickerFirstSoDate_ValueChanged(object sender, EventArgs e)
        {
            textBoxFirstSoDate.Text = String.Format("{0:M/d/yyyy}", dateTimePickerFirstSoDate.Value);
        }

        private void dateTimePickerLastSoDate_ValueChanged(object sender, EventArgs e)
        {
            textBoxLastSoDate.Text = String.Format("{0:M/d/yyyy}", dateTimePickerLastSoDate.Value);
        }

        private void dataGridviewSoSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ProcessSelection();
        }

        public void ProcessSelection()
        {
            SelectedSono = soSearchInf.CaptureSono(dataGridviewSoSearch);
            this.Close();
        }

        private void dataGridviewSoSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ProcessSelection();
            }
        }

        private void listBoxInclude_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBoxInclude.SelectedIndex)
            {
                case 0:
                    {
                        IncludeType = "OB";
                        break;
                    }
                case 1:
                    {
                        IncludeType = "B";
                        break;
                    }
                case 2:
                    {
                        IncludeType = "O";
                        break;
                    }
            }
        }
    }// end form class
}// end name space