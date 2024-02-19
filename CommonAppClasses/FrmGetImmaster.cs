using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    //CACHED   Maintain -> Inventory Items -> Select
    public partial class FrmGetImmaster : WSGUtilitieslib.Telemetry.Form
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("Item Selector");

        // Create the Immaster processing object
        private static ImmasterAccess immasterAccess = new ImmasterAccess("SQL", "SQLConnString");

        private static ObjectCache dataCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_InventoryItems"]));
        private BindingSource bindingImmasterData = new BindingSource();

        public string SelectedCode = "";
        public string CurrentState = "";
        public string SelectedItem = "";
        public string ItemSearchKey = "";

        public FrmGetImmaster()
        {
            InitializeComponent();
            SelectedItem = "";
            dataGridViewGetImmaster.AutoGenerateColumns = false;
            dataGridViewGetImmaster.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewGetImmaster.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            CurrentState = "Select";
            ItemSearchKey = "";
        }

        private void FrmGetImmaster_Shown(object sender, EventArgs e)
        {
            if (SelectedCode.TrimEnd() == "")
            {
                if (dataCache.IsInvalid)
                {
                    immasterAccess.GetImmasterData();
                    dataCache.Refresh(immasterAccess);
                }
            }
            else
            {
                immasterAccess.GetSelectedItemGroup(SelectedCode);
                dataCache.Invalidate();
            }

            bindingImmasterData.DataSource = immasterAccess.itemds.view_immasterdata;
            dataGridViewGetImmaster.DataSource = bindingImmasterData;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ProcessSelection()
        {
            SelectedItem = CaptureDataGridColumn(this.dataGridViewGetImmaster, "item");
            this.Close();
        }

        public string CaptureDataGridColumn(DataGridView myDataGridView, string Column)
        {
            CurrencyManager xCM =
           (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
           myDataGridView.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Return the selected rule description
            return xRow[Column].ToString();
        }

        private void dataGridViewGetImmaster_CellContentDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            ProcessSelection();
        }

        private void dataGridViewGetImmaster_KeyDown(object sender, KeyEventArgs e)
        {
            int ix = 0;
            switch (e.KeyCode)
            {
                case Keys.Return:
                    {
                        ProcessSelection();
                        break;
                    }
                case Keys.Home:
                    {
                        ItemSearchKey = "";
                        dataGridViewGetImmaster.CurrentCell = dataGridViewGetImmaster.Rows[0].Cells[0];
                        break;
                    }
                default:
                    {
                        if (ItemSearchKey.Length > 4)
                        {
                            ItemSearchKey = "";
                        }
                        ItemSearchKey += Convert.ToChar(e.KeyCode).ToString().ToUpper();
                        while (ix < dataGridViewGetImmaster.RowCount - 1)
                        {
                            string x = dataGridViewGetImmaster.Rows[ix].Cells[0].Value.ToString().ToUpper();
                            if (x.Substring(0, ItemSearchKey.Length) == ItemSearchKey)
                            {
                                dataGridViewGetImmaster.CurrentCell = dataGridViewGetImmaster.Rows[ix].Cells[0];
                                break;
                            }
                            else
                            {
                                ix++;
                                continue;
                            }
                        }
                        break;
                    }
            }
        }
    }
}