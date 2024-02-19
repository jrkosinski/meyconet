using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    //CACHED Enter Misc Order -> Select Customer -> Terms 
    public class CustomerTermsMethods : WSGDataAccess
    {
        private static alereds AlereSelectorDs = new alereds();
        private static ObjectCache dataCache = new ObjectCache(Int32.Parse(ConfigurationManager.AppSettings["CacheRetentionSeconds_CustomerTerm"]));

        public alereds AlereDs = new alereds();
        public string termid = "";

        private FrmSelectTerms frmSelectTerms = new FrmSelectTerms();

        public CustomerTermsMethods()
            : base("SQL", "SQLConnString")
        {
            SetEvents();
        }

        public string SelectTerms()
        {
            if (dataCache.IsInvalid)
            {
                string CommandString = "SELECT * FROM coterms ORDER BY termid";
                AlereSelectorDs.coterms.Rows.Clear();
                ClearParameters();
                FillData(AlereSelectorDs, "coterms", CommandString, CommandType.Text);
                dataCache.Refresh(AlereSelectorDs);
            }

            frmSelectTerms.dataGridViewTermsSelector.AutoGenerateColumns = false;
            frmSelectTerms.dataGridViewTermsSelector.DataSource = frmSelectTerms.bindingTermsData;
            frmSelectTerms.dataGridViewTermsSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
            frmSelectTerms.dataGridViewTermsSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            frmSelectTerms.bindingTermsData.DataSource = AlereSelectorDs.coterms;
            frmSelectTerms.ShowDialog();
            return termid;
        }

        public void SetEvents()
        {
            frmSelectTerms.dataGridViewTermsSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(processDoubleClick);
            frmSelectTerms.dataGridViewTermsSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(processKeyDown);
            frmSelectTerms.buttonClose.Click += new System.EventHandler(buttonClose_Click);
        }

        private void processDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureTermId();
        }

        private void processKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureTermId();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            frmSelectTerms.Close();
        }

        private void CaptureTermId()
        {
            termid = CaptureDataGridColumn(frmSelectTerms.dataGridViewTermsSelector, "termid");
            AlereDs.coterms.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@termid", termid, "SQL");
            string CommandString = "SELECT * FROM coterms WHERE termid = @termid";
            this.FillData(AlereDs, "coterms", CommandString, CommandType.Text);
            frmSelectTerms.Close();
        }
    }

    public class AlereDataMethods : WSGDataAccess
    {
        private alereds AlereSelectorDs = new alereds();
        public alereds AlereDs = new alereds();
        public string termid = "";

        private FrmSelectTerms frmSelectTerms = new FrmSelectTerms();

        public AlereDataMethods()
            : base("SQL", "SQLConnString")
        {
        }

        public void InsertAlereGLPost(DataRow Dr)
        {
            string SaveCommand = "";
            string ValueString = "";
            string OpeningNameBracket = "";
            string ClosingNameBracket = "";
            OpeningNameBracket = "[";
            ClosingNameBracket = "]";
            Dr["userchg"] = AppUserClass.AppUserId;
            Dr["lastchg"] = DateTime.Now;

            SaveCommand = "Insert INTO " + Dr.Table.TableName + "(";
            ValueString = " VALUES (";
            for (int i = 0; i < Dr.Table.Columns.Count; i++)
            {
                if (!SaveCommand.EndsWith("("))
                {
                    SaveCommand += ", ";
                }

                SaveCommand += OpeningNameBracket + Dr.Table.Columns[i].ColumnName + ClosingNameBracket;
                if (!ValueString.EndsWith("("))
                {
                    ValueString += ", ";
                }
                ValueString += "@" + Dr.Table.Columns[i].ColumnName;
            }
            SaveCommand += ")";
            ValueString += ")";
            SaveCommand += ValueString;
            //      MessageBox.Show(SaveCommand);
            SetAllParameters(Dr);
            this.ExecuteCommand(SaveCommand, CommandType.Text);
        }

        public void GenerateAlereTableRowSave(DataRow Dr, bool Inserting, string UpdateString)
        {
            string SaveCommand = "";
            string ValueString = "";
            string OpeningNameBracket = "";
            string ClosingNameBracket = "";
            OpeningNameBracket = "[";
            ClosingNameBracket = "]";
            if (Inserting)
            {
                Dr["userid"] = AppUserClass.AppUserId;
                Dr["timestmp"] = DateTime.Now;
                Dr["userchg"] = AppUserClass.AppUserId;
                Dr["lastchg"] = DateTime.Now;

                SaveCommand = "Insert INTO " + Dr.Table.TableName + "(";
                ValueString = " VALUES (";
                for (int i = 0; i < Dr.Table.Columns.Count; i++)
                {
                    if (!SaveCommand.EndsWith("("))
                    {
                        SaveCommand += ", ";
                    }

                    SaveCommand += OpeningNameBracket + Dr.Table.Columns[i].ColumnName + ClosingNameBracket;
                    if (!ValueString.EndsWith("("))
                    {
                        ValueString += ", ";
                    }
                    ValueString += "@" + Dr.Table.Columns[i].ColumnName;
                }
                SaveCommand += ")";
                ValueString += ")";
                SaveCommand += ValueString;
            }
            else
            {
                Dr["userchg"] = AppUserClass.AppUserId;
                Dr["lastchg"] = DateTime.Now;

                SaveCommand = "UPDATE " + Dr.Table.TableName + " SET ";
                for (int i = 0; i < Dr.Table.Columns.Count; i++)
                {
                    if (!SaveCommand.TrimEnd().EndsWith("SET"))
                    {
                        SaveCommand += ", ";
                    }
                    SaveCommand += OpeningNameBracket + Dr.Table.Columns[i].ColumnName + ClosingNameBracket;
                    SaveCommand += " = @" + Dr.Table.Columns[i].ColumnName;
                }
                SaveCommand += " WHERE " + UpdateString;
            }
            //      MessageBox.Show(SaveCommand);
            SetAllParameters(Dr);

            this.ExecuteCommand(SaveCommand, CommandType.Text);
        }
    }

    public class AlereCodeMethods : WSGDataAccess
    {
        private alereds AlereSelectorDs = new alereds();
        public alereds AlereDs = new alereds();
        public string codetype = "";
        public string codename = "";
        private FrmSelectAlereCode frmSelectAlereCode = new FrmSelectAlereCode();

        public AlereCodeMethods()
            : base("SQL", "SQLConnString")
        {
            SetEvents();
        }

        public string SelectCode(string codetype)
        {
            BindingSource bindingCodesData = new BindingSource();
            string CommandString = "SELECT * FROM cocodes WHERE codetype = @codetype ORDER BY codedesc";
            AlereSelectorDs.cocodes.Rows.Clear();
            ClearParameters();
            this.AddParms("@codetype", codetype, "SQL");
            FillData(AlereSelectorDs, "cocodes", CommandString, CommandType.Text);
            frmSelectAlereCode.dataGridViewCodeSelector.AutoGenerateColumns = false;
            frmSelectAlereCode.dataGridViewCodeSelector.DataSource = bindingCodesData;
            frmSelectAlereCode.dataGridViewCodeSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
            frmSelectAlereCode.dataGridViewCodeSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            bindingCodesData.DataSource = AlereSelectorDs.cocodes;
            frmSelectAlereCode.ShowDialog();
            return codename;
        }

        public void SetEvents()
        {
            frmSelectAlereCode.dataGridViewCodeSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(processDoubleClick);
            frmSelectAlereCode.dataGridViewCodeSelector.KeyDown += new System.Windows.Forms.KeyEventHandler(processKeyDown);
            frmSelectAlereCode.buttonClose.Click += new System.EventHandler(buttonClose_Click);
        }

        private void processDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureAlereCode();
        }

        private void processKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CaptureAlereCode();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            frmSelectAlereCode.Close();
        }

        private void CaptureAlereCode()
        {
            codename = CaptureDataGridColumn(frmSelectAlereCode.dataGridViewCodeSelector, "codename");
            frmSelectAlereCode.Close();
        }
    }
}