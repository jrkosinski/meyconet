using System;
using System.Data;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public class FrmSelectorMethods : WSGDataAccess
    {
        public FrmSelector frmSelector = new FrmSelector();
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Selector");
        public DataTable dtSource = new DataTable();
        public int SelectedIdcol = 0;
        public int columncount = 0;
        public int[] colwidth;
        public bool findidcol;
        public string returnkey;
        public string searchcolumm;
        public string[] colname;
        public string[] colheadertext;
        public string[] coldatapropertyname;
        public string[] coldefaultcellstyle;
        public string CurrentState = "Select";
        public string FormText { get; set; }

        public FrmSelectorMethods()
            : base("SQL", "SQLConnString")
        {
            SetEvents();
            dtSource.Rows.Clear();
        }

        public void SetEvents()
        {
            frmSelector.dataGridViewSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(CaptureDoubleClick);
            frmSelector.dataGridViewSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(CaptureEnter);
            frmSelector.buttonClose.Click += new System.EventHandler(CaptureClose);
        }

        public void SetColumns()
        {
            colwidth = new int[columncount];
            colname = new string[columncount];
            colheadertext = new string[columncount];
            coldefaultcellstyle = new string[columncount];
            coldatapropertyname = new string[columncount];
            frmSelector.dataGridViewSelector.ColumnCount = columncount;
        }

        public void ShowSelector()
        {
            //   SetGrid();
            frmSelector.ShowDialog();
        }

        public void SetGrid()
        {
            BindingSource bindingSelectorData = new BindingSource();
            bindingSelectorData.DataSource = dtSource;
            frmSelector.Text = FormText;
            frmSelector.dataGridViewSelector.Left = frmSelector.Left + 10;
            frmSelector.dataGridViewSelector.Width = 0;
            frmSelector.dataGridViewSelector.ReadOnly = true;
            for (int i = 0; i <= columncount - 1; i++)
            {
                frmSelector.dataGridViewSelector.Columns[i].Name = colname[i];
                frmSelector.dataGridViewSelector.Columns[i].DataPropertyName = coldatapropertyname[i];
                frmSelector.dataGridViewSelector.Columns[i].HeaderText = colheadertext[i];
                frmSelector.dataGridViewSelector.Columns[i].Width = colwidth[i];
                if (coldefaultcellstyle[i] != null)
                {
                    frmSelector.dataGridViewSelector.Columns[i].DefaultCellStyle.Format = coldefaultcellstyle[i];
                    frmSelector.dataGridViewSelector.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                frmSelector.dataGridViewSelector.Columns[i].ReadOnly = true;
                frmSelector.dataGridViewSelector.Width += colwidth[i];
            }

            frmSelector.Width = frmSelector.dataGridViewSelector.Width + 30;
            frmSelector.dataGridViewSelector.RowHeadersVisible = false;
            frmSelector.dataGridViewSelector.AutoGenerateColumns = false;
            frmSelector.dataGridViewSelector.AllowUserToAddRows = false;
            frmSelector.dataGridViewSelector.DataSource = bindingSelectorData;
            frmSelector.buttonClose.Left = frmSelector.Width - 90;
            frmSelector.dataGridViewSelector.Columns[0].Selected = true;
            if (frmSelector.dataGridViewSelector.Rows.Count > 0)
            {
                frmSelector.dataGridViewSelector.Rows[0].Selected = true;
            }
        }

        public void SetGridWithFormatting(DataGridViewCellFormattingEventHandler formatHandler)
        {
            BindingSource bindingSelectorData = new BindingSource();
            bindingSelectorData.DataSource = dtSource;
            frmSelector.Text = FormText;
            frmSelector.dataGridViewSelector.Left = frmSelector.Left + 10;
            frmSelector.dataGridViewSelector.ColumnCount = columncount;
            frmSelector.dataGridViewSelector.Width = 0;
            frmSelector.dataGridViewSelector.ReadOnly = true;
            for (int i = 0; i <= columncount - 1; i++)
            {
                frmSelector.dataGridViewSelector.Columns[i].Name = colname[i];
                frmSelector.dataGridViewSelector.Columns[i].DataPropertyName = coldatapropertyname[i];
                frmSelector.dataGridViewSelector.Columns[i].HeaderText = colheadertext[i];
                frmSelector.dataGridViewSelector.Columns[i].Width = colwidth[i];
                frmSelector.dataGridViewSelector.Columns[i].ReadOnly = true;
                frmSelector.dataGridViewSelector.Width += colwidth[i];
            }

            frmSelector.Width = frmSelector.dataGridViewSelector.Width + 30;
            frmSelector.dataGridViewSelector.RowHeadersVisible = false;
            frmSelector.dataGridViewSelector.AutoGenerateColumns = false;
            frmSelector.dataGridViewSelector.AllowUserToAddRows = false;
            frmSelector.dataGridViewSelector.DataSource = bindingSelectorData;
            frmSelector.buttonClose.Left = frmSelector.Width - 90;
            frmSelector.dataGridViewSelector.Columns[0].Selected = true;
            if (frmSelector.dataGridViewSelector.Rows.Count > 0)
            {
                frmSelector.dataGridViewSelector.Rows[0].Selected = true;
            }

            frmSelector.dataGridViewSelector.CellFormatting += formatHandler;
        }

        public void CaptureIdcol()
        {
            SelectedIdcol = CaptureIdCol(frmSelector.dataGridViewSelector);
        }

        public void CaptureReturnKey()
        {
            returnkey = CaptureDataGridColumn(frmSelector.dataGridViewSelector, searchcolumm);
        }

        private void CaptureDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (findidcol)
            {
                CaptureIdcol();
            }
            else
            {
                CaptureReturnKey();
            }
            frmSelector.Close();
        }

        private void CaptureClose(object sender, EventArgs e)
        {
            frmSelector.Close();
        }

        private void CaptureEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (findidcol)
                {
                    CaptureIdcol();
                }
                else
                {
                    CaptureReturnKey();
                }
                frmSelector.Close();
            }
        }
    }
}