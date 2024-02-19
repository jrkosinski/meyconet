﻿using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace WSGUtilitieslib
{
    public class WSGConnection
    {
        #region Declaration

        // Use a variable to store the connection string, if you try and get it from the SqlConnection object, it strips the password
        public string ConnectionString = "";

        #endregion Declaration

        #region Methods

        public IDbConnection GetConnection(string DataStore, string AppConfigName)
        {
            return this.GetConnection("", DataStore, AppConfigName);
        }

        public IDbConnection GetConnection(string tConnectionString, string DataStore, string AppConfigName)
        {
            string cConnection = tConnectionString;
            if (DataStore == "SQL")
            {
                if (tConnectionString == "")
                    cConnection = ConfigurationManager.AppSettings["SQLConnString"];
                SqlConnection conn = new SqlConnection(cConnection);
                this.ConnectionString = cConnection;
                return conn;
            }
            else
            {
                // Establish VFP Connection String
                cConnection = @"Provider=VFPOLEDB;data source= " + ConfigurationManager.AppSettings[AppConfigName];
                OleDbConnection conn = new OleDbConnection(cConnection);
                this.ConnectionString = cConnection;
                return conn;
            }
        }

        #endregion Methods
    }// WSGConnection

    public class WSGDataAccess
    {
        #region Declarations

        private IDbConnection oConnection;
        private IDbCommand oCommand;
        public IDbDataAdapter oAdapter;
        //private IDataParameterCollection oParms;
        public string ConnectionString = "";
        protected string ErrorMessage = "";

        #endregion Declarations

        #region Constructor/Destructor

        public WSGDataAccess(string DataStore, string AppConfigName)
        {
            WSGConnection o = new WSGConnection();
            this.oConnection = o.GetConnection(DataStore, AppConfigName);
            this.ConnectionString = o.ConnectionString;
            if (DataStore == "SQL")
            {
                this.oCommand = this.GetIDbCommand(DataStore);
            }
            else
            {
                this.oCommand = new OleDbCommand();
            }
            this.oAdapter = this.GetIDbDataAdapter(this.oCommand, DataStore);
            this.oCommand.Connection = this.oConnection;
            this.oCommand.CommandTimeout = 180;
            //  AppConstants
        }

        #endregion Constructor/Destructor

        #region Get/Use IDb Interface Objects

        private static int DeadlockRollbackError = 1205;
        protected string AtSign = "@";

        protected IDbCommand GetIDbCommand(string DataStore)
        {
            if (DataStore == "SQL")
            {
                return new SqlCommand();
            }
            else
            {
                return new OleDbCommand();
            }
        }

        protected IDbDataAdapter GetIDbDataAdapter(IDbCommand command, string DataStore)
        {
            if (DataStore == "SQL")
            {
                return new SqlDataAdapter((SqlCommand)command);
            }
            else
            {
                return new OleDbDataAdapter((OleDbCommand)command);
            }
        }

        protected IDbDataParameter GetIDbOutputStringParameter(string ParmName, object ParmValue)
        {
            return new SqlParameter(ParmName, ParmValue);
        }

        protected IDbDataParameter GetIDbDataParameter(string ParmName, object ParmValue, string DataStore)
        {
            if (DataStore == "SQL")
            {
                return new SqlParameter(ParmName, ParmValue);
            }
            else
            {
                return new OleDbParameter(ParmName, ParmValue);
            }
        }

        protected void DeriveParameters(IDbCommand command)
        {
            SqlCommandBuilder.DeriveParameters((SqlCommand)command);
        }

        #endregion Get/Use IDb Interface Objects

        // Then there are the various protected methods for adding and setting Parameters, filling a DataSet, etc.
        // It's these various methods that get used in the classes that are sub-classed from this "base" class.
        // Here's just a few of them:

        protected void ClearParameters()
        {
            this.oCommand.Parameters.Clear();
            this.ErrorMessage = "";
        }

        protected void CloseConnection()
        {
            this.oConnection.Close();
        }

        protected void AddParms(string ParmName, object ParmValue, string DataStore)
        {
            try
            {
                if (ParmName.StartsWith(this.AtSign) == false)
                    ParmName = this.AtSign + ParmName;

                if (ParmValue != DBNull.Value)
                {
                    //  MessageBox.Show(ParmName);
                    this.oCommand.Parameters.Add(this.GetIDbDataParameter(ParmName, ParmValue, DataStore));
                }
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
            }
        }

        protected void SetParmDirection(string ParmName, ParameterDirection direction)
        {
            SqlParameter ThisParm = (SqlParameter)this.oCommand.Parameters[ParmName];
            ThisParm.Direction = direction;
        }

        protected void AddStringParm(string ParmName, SqlDbType ParmType, int ParmLength, string ParmValue)
        {
            // Sets output parameters for a command.
            // Necessary into order to establish the size property.
            try
            {
                if (ParmName.StartsWith(this.AtSign) == false)
                    ParmName = this.AtSign + ParmName;

                this.oCommand.Parameters.Add(this.GetIDbDataParameter(ParmName, ParmValue, "SQL"));
                SqlParameter ThisParm = (SqlParameter)this.oCommand.Parameters[ParmName];
                ThisParm.Direction = ParameterDirection.Input;
                ThisParm.SqlDbType = ParmType;
                ThisParm.Size = ParmLength;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
            }
        }

        protected void AddVFPBooleanParm(string ParmName, Boolean ParmValue)
        {
            try
            {
                if (ParmName.StartsWith(this.AtSign) == false)
                    ParmName = this.AtSign + ParmName;

                this.oCommand.Parameters.Add(this.GetIDbDataParameter(ParmName, ParmValue, "VFP"));
                IDbDataParameter ThisParm = (IDbDataParameter)this.oCommand.Parameters[ParmName];
                ThisParm.Direction = ParameterDirection.Input;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
            }
        }

        protected void AddVFPDateParm(string ParmName, DateTime ParmValue)
        {
            // Necessary into order to establish the size property.
            try
            {
                if (ParmName.StartsWith(this.AtSign) == false)
                    ParmName = this.AtSign + ParmName;

                this.oCommand.Parameters.Add(this.GetIDbDataParameter(ParmName, ParmValue, "VFP"));
                IDbDataParameter ThisParm = (IDbDataParameter)this.oCommand.Parameters[ParmName];
                ThisParm.Direction = ParameterDirection.Input;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
            }
        }

        protected void AddVFPDecimalParm(string ParmName, int ParmLength, int ParmPrecision, decimal ParmValue)
        {
            // Necessary into order to establish the size property.
            try
            {
                if (ParmName.StartsWith(this.AtSign) == false)
                    ParmName = this.AtSign + ParmName;

                this.oCommand.Parameters.Add(this.GetIDbDataParameter(ParmName, ParmValue, "VFP"));
                IDbDataParameter ThisParm = (IDbDataParameter)this.oCommand.Parameters[ParmName];
                ThisParm.Direction = ParameterDirection.Input;
                ThisParm.Size = ParmLength;
                ThisParm.Precision = (byte)ParmPrecision;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
            }
        }

        protected void AddVFPMemoParm(string ParmName, OleDbType ParmType, string ParmValue)
        {
            // Sets output parameters for a command.
            // Necessary into order to establish the size property.
            try
            {
                if (ParmName.StartsWith(this.AtSign) == false)
                {
                    ParmName = this.AtSign + ParmName;
                }

                this.oCommand.Parameters.Add(this.GetIDbDataParameter(ParmName, ParmValue, "VFP"));
                IDbDataParameter ThisParm = (IDbDataParameter)this.oCommand.Parameters[ParmName];
                ThisParm.Direction = ParameterDirection.Input;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
            }
        }

        protected void AddVFPStringParm(string ParmName, OleDbType ParmType, int ParmLength, string ParmValue)
        {
            // Sets output parameters for a command.
            // Necessary into order to establish the size property.
            try
            {
                if (ParmName.StartsWith(this.AtSign) == false)
                {
                    ParmName = this.AtSign + ParmName;
                }

                this.oCommand.Parameters.Add(this.GetIDbDataParameter(ParmName, ParmValue, "VFP"));
                IDbDataParameter ThisParm = (IDbDataParameter)this.oCommand.Parameters[ParmName];
                ThisParm.Direction = ParameterDirection.Input;
                ThisParm.Size = ParmLength;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
            }
        }

        protected void AddStringOutputParm(string ParmName, int ParmLength)
        {
            // Sets output parameters for a command.
            // Necessary into order to establish the size property.
            try
            {
                if (ParmName.StartsWith(this.AtSign) == false)
                    ParmName = this.AtSign + ParmName;

                this.oCommand.Parameters.Add(this.GetIDbDataParameter(ParmName, "", "SQL"));
                SqlParameter ThisParm = (SqlParameter)this.oCommand.Parameters[ParmName];
                ThisParm.Direction = ParameterDirection.Output;
                ThisParm.SqlDbType = SqlDbType.Char;
                ThisParm.Size = ParmLength;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
            }
        }

        protected void AddParms(string ParmName, object ParmValue, ParameterDirection direction,
         string DataStore)
        {
            this.AddParms(ParmName, ParmValue, DataStore);
            this.SetParmDirection(ParmName, direction);
        }

        protected bool FillData(DataSet ds, string tablename, string CommandText, CommandType CommandType)
        {
            DebugWrite($"FillData: {CommandText}, table: {tablename}");

            ds.EnforceConstraints = false;
            Telemetry.Telemetry.AddSqlQueryEvent($"FillData: {CommandText}");

            try
            {
                this.oCommand.CommandType = CommandType;

                this.oCommand.CommandText = CommandText;
                this.oAdapter.SelectCommand = oCommand;
                this.oAdapter.TableMappings.Clear();
                this.oAdapter.TableMappings.Add("Table", tablename);
                this.oAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
                MessageBox.Show("Error Filling Table " + tablename);
                MessageBox.Show(ex.Message);
                this.CloseConnection();
                return false;
            }
            return true;
        }

        #region Database Methods, using the IDb interface members

        protected int ExecuteCommand(string CommandText, CommandType CommandType)
        {
            DebugWrite($"ExecuteCommand: {CommandText}");

            int commandresult = 0;
            this.oCommand.CommandType = CommandType;
            bool IsAlreadyOpen = (this.oConnection.State == ConnectionState.Open);

            this.OpenConnection();
            this.oCommand.CommandText = CommandText;

            try
            {
                Telemetry.Telemetry.AddSqlCommandEvent(this.oCommand);
                commandresult = this.oCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // Check if the sql exception is because of a deadlock rollback
                // If so we can attempt a retry
                if (ex.Number == DeadlockRollbackError)
                    throw new BBRetryException("Deadlock", ex);
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                throw ex;
            }
            finally
            {
                if (IsAlreadyOpen == false)
                {
                    this.CloseConnection();
                }
            }
            return commandresult;
        }

        protected string ExecuteStringOutputCommand(string CommandText, CommandType CommandType)
        {
            DebugWrite($"ExecuteStringOutputCommand: {CommandText}");

            this.oCommand.CommandType = CommandType;
            string returnstring = "OK";
            bool IsAlreadyOpen = (this.oConnection.State == ConnectionState.Open);

            this.OpenConnection();
            this.oCommand.CommandText = CommandText;

            //
            try
            {
                WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(this.oCommand);
                this.oCommand.ExecuteNonQuery();
                foreach (IDataParameter p in oCommand.Parameters)
                {
                    if (p.Direction == ParameterDirection.Output)
                    {
                        returnstring = p.Value.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Check if the sql exception is because of a deadlock rollback
                // If so we can attempt a retry
                returnstring = "Failure";
                if (ex.Number == DeadlockRollbackError)
                    throw new BBRetryException("Deadlock", ex);
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                returnstring = "Failure";
                throw ex;
            }
            finally
            {
                if (IsAlreadyOpen == false)
                {
                    this.CloseConnection();
                }
            }
            return returnstring;
        }

        //
        protected int ExecuteIntOutputCommand(string CommandText, CommandType CommandType)
        {
            DebugWrite($"ExecuteIntOutputCommand: {CommandText}");

            this.oCommand.CommandType = CommandType;
            int returnint = 0;
            bool IsAlreadyOpen = (this.oConnection.State == ConnectionState.Open);

            this.OpenConnection();
            this.oCommand.CommandText = CommandText;

            //
            try
            {
                WSGUtilitieslib.Telemetry.Telemetry.AddSqlCommandEvent(this.oCommand);
                this.oCommand.ExecuteNonQuery();
                foreach (IDataParameter p in oCommand.Parameters)
                {
                    if (p.Direction == ParameterDirection.Output)
                    {
                        returnint = (Int32)p.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Check if the sql exception is because of a deadlock rollback
                // If so we can attempt a retry
                returnint = -1;
                if (ex.Number == DeadlockRollbackError)
                    throw new BBRetryException("Deadlock", ex);
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                returnint = -1;
                throw ex;
            }
            finally
            {
                if (IsAlreadyOpen == false)
                {
                    this.CloseConnection();
                }
            }
            return returnint;
        }

        public int CaptureIdCol(DataGridView myDataGridView)
        {
            if (myDataGridView.Rows.Count > 0)
            {
                CurrencyManager xCM =
               (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
               myDataGridView.DataMember];
                DataRowView xDRV = (DataRowView)xCM.Current;
                DataRow xRow = xDRV.Row;
                // Return the ID
                return (int)xRow["idcol"];
            }
            return -1;
        }

        public void DeleteCurrentRow(DataGridView myDataGridView)
        {
            DebugWrite($"DeleteCurrentRow");
            CurrencyManager xCM =
           (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
            myDataGridView.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            xDRV.Row.Delete();
        }

        public string CaptureRuleDescrip(DataGridView myDataGridView)
        {
            CurrencyManager xCM =
           (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
           myDataGridView.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Return the selcted rule description
            return xRow["descrip"].ToString();
        }

        public string CaptureDataGridColumn(DataGridView myDataGridView, string Column)
        {
            CurrencyManager xCM =
           (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
           myDataGridView.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            return xRow[Column].ToString();
        }

        public string CaptureRuleChrvl(DataGridView myDataGridView)
        {
            CurrencyManager xCM =
           (CurrencyManager)myDataGridView.BindingContext[myDataGridView.DataSource,
           myDataGridView.DataMember];
            DataRowView xDRV = (DataRowView)xCM.Current;
            DataRow xRow = xDRV.Row;
            // Return the selcted rule description
            return xRow["chrvl"].ToString();
        }

        public bool UpdateData(DataSet ds, string tablename)
        {
            DebugWrite($"UpdateData: {tablename}");
            SqlDataAdapter sqlDataAdapter = (SqlDataAdapter)this.oAdapter;
            try
            {
                SqlCommandBuilder objCommandbuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Update(ds, tablename);
            }
            catch (Exception ex)
            {
                Telemetry.Telemetry.AddErrorEvent(ex.ToString());
                this.ErrorMessage += ex.Message;
                MessageBox.Show(ex.Message);
                this.CloseConnection();
                return false;
            }
            return true;
        }

        protected int SaveDatatableRow(DataTable Table, string Procedure, int Row, bool Inserting)
        {
            DebugWrite($"SaveDatatableRow: {Table.TableName}");

            int returnvalue = 0;
            this.ClearParameters();
            for (int i = 0; i < Table.Columns.Count; i++)
            {
                // Omit idcol parameter when inserting
                if ((Inserting == true) && Table.Columns[i].ColumnName == "idcol")
                {
                    continue;
                }
                string columnname = Table.Columns[i].ColumnName;
                if (Table.Columns[i].DataType == typeof(System.String))
                {
                    this.AddParms("@" + columnname, Table.Rows[Row].Field<string>(columnname), "SQL");
                    continue;
                }
                if (Table.Columns[i].DataType == typeof(System.Decimal))
                {
                    this.AddParms("@" + columnname, Table.Rows[Row].Field<Decimal>(columnname), "SQL");
                    continue;
                }
                if (Table.Columns[i].DataType == typeof(System.Byte[]))
                {
                    this.AddParms("@" + columnname, Table.Rows[Row].Field<Byte[]>(columnname), "SQL");
                    continue;
                }
                if (Table.Columns[i].DataType == typeof(System.DateTime))
                {
                    this.AddParms("@" + columnname, Table.Rows[Row].Field<DateTime>(columnname), "SQL");
                    continue;
                }

                if (Table.Columns[i].DataType == typeof(System.Boolean))
                {
                    this.AddParms("@" + columnname, Table.Rows[Row].Field<Boolean>(columnname), "SQL");
                    continue;
                }

                if (Table.Columns[i].DataType == typeof(System.Int32))
                {
                    this.AddParms("@" + columnname, Table.Rows[Row].Field<Int32>(columnname), "SQL");
                    continue;
                }

                if (Table.Columns[i].DataType == typeof(System.Int16))
                {
                    this.AddParms("@" + columnname, Table.Rows[Row].Field<Int16>(columnname), "SQL");
                    continue;
                }
            }
            if (Inserting == true)
            {
                this.AddParms("@ReferenceId", 0, ParameterDirection.Output, "SQL");
            }

            try
            {
                if (Inserting == true)
                {
                    // The procedure will return the ID of the added row
                    returnvalue = ExecuteIntOutputCommand(Procedure, CommandType.StoredProcedure);
                }
                else
                {
                    ExecuteCommand(Procedure, CommandType.StoredProcedure);
                    returnvalue = 0;
                }
                return returnvalue;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        protected int SaveDataRow(DataRow Row, string Procedure, bool Inserting)
        {
            DebugWrite($"SaveDataRow: {Procedure}");

            int returnvalue = 0;
            this.ClearParameters();
            for (int i = 0; i < Row.Table.Columns.Count; i++)
            {
                // Omit idcol parameter when inserting
                if ((Inserting == true) && Row.Table.Columns[i].ColumnName == "idcol")
                    continue;
                string columnname = Row.Table.Columns[i].ColumnName;
                if (Row.Table.Columns[i].DataType == typeof(System.String))
                {
                    this.AddParms("@" + columnname, Row.Field<string>(columnname), "SQL");
                    continue;
                }
                if (Row.Table.Columns[i].DataType == typeof(System.Decimal))
                {
                    this.AddParms("@" + columnname, Row.Field<Decimal>(columnname), "SQL");
                    continue;
                }
                if (Row.Table.Columns[i].DataType == typeof(System.DateTime))
                {
                    this.AddParms("@" + columnname, Row.Field<DateTime>(columnname), "SQL");
                    continue;
                }

                if (Row.Table.Columns[i].DataType == typeof(System.Boolean))
                {
                    this.AddParms("@" + columnname, Row.Field<Boolean>(columnname), "SQL");
                    continue;
                }

                if (Row.Table.Columns[i].DataType == typeof(System.Int32))
                {
                    this.AddParms("@" + columnname, Row.Field<Int32>(columnname), "SQL");
                    continue;
                }

                if (Row.Table.Columns[i].DataType == typeof(System.Int16))
                {
                    this.AddParms("@" + columnname, Row.Field<Int16>(columnname), "SQL");
                    continue;
                }
            }
            if (Inserting == true)
            {
                this.AddParms("@ReferenceId", 0, ParameterDirection.Output, "SQL");
            }

            try
            {
                if (Inserting == true)
                {
                    // The procedure will return the ID of the added row
                    returnvalue = ExecuteIntOutputCommand(Procedure, CommandType.StoredProcedure);
                }
                else
                {
                    ExecuteCommand(Procedure, CommandType.StoredProcedure);
                    returnvalue = 0;
                }
                return returnvalue;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        protected void InitializeDataTable(DataTable dt, int row)
        {
            foreach (DataColumn c in dt.Columns)
            {
                if (c.ColumnName == "idcol")
                {
                    continue;
                }

                if (c.DataType == typeof(System.String))
                {
                    dt.Rows[row][c.ColumnName] = "";
                    continue;
                }
                if (c.DataType == typeof(System.DateTime))
                {
                    dt.Rows[row][c.ColumnName] = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                    continue;
                }
                if (c.DataType == typeof(System.Int16))
                {
                    dt.Rows[row][c.ColumnName] = 0;
                    continue;
                }
                if (c.DataType == typeof(System.Int32))
                {
                    dt.Rows[row][c.ColumnName] = 0;
                    continue;
                }

                if (c.DataType == typeof(System.Decimal))
                {
                    dt.Rows[row][c.ColumnName] = 0;
                    continue;
                }

                if (c.DataType == typeof(System.Boolean))
                {
                    dt.Rows[row][c.ColumnName] = 0;
                    continue;
                }
            } // end for each
        } // InitializeDataTable

        protected void EstablishBlankDataTableRow(DataTable dt)
        {
            dt.Rows.Clear();
            DataRow myRow;
            myRow = dt.NewRow();
            InitializeDataRow(myRow);
            dt.Rows.Add(myRow);
        }

        protected void InitializeDataRow(DataRow row)
        {
            foreach (DataColumn c in row.Table.Columns)
            {
                if (c.ColumnName == "idcol")
                {
                    continue;
                }

                if (c.DataType == typeof(System.String))
                {
                    row[c.ColumnName] = "";
                    continue;
                }
                if (c.DataType == typeof(System.DateTime))
                {
                    row[c.ColumnName] = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                    continue;
                }
                if (c.DataType == typeof(System.Int16))
                {
                    row[c.ColumnName] = 0;
                    continue;
                }
                if (c.DataType == typeof(System.Int32))
                {
                    row[c.ColumnName] = 0;
                    continue;
                }

                if (c.DataType == typeof(System.Decimal))
                {
                    row[c.ColumnName] = 0;
                    continue;
                }

                if (c.DataType == typeof(System.Boolean))
                {
                    row[c.ColumnName] = 0;
                    continue;
                }
            } // end for each
        } // InitializeDatarow

        protected void SetIdcol(DataColumn dc)
        {
            dc.AutoIncrement = true;
            dc.AutoIncrementStep = -1;
            dc.AutoIncrementSeed = -1;
        }

        protected void SaveTable(DataTable Table, string SaveProc)
        {
            DebugWrite($"SaveTable: {Table.TableName}, {SaveProc}");

            // save each row
            foreach (DataRow row in Table.Rows)
            {
                this.SetAllParameters(row);
                this.ExecuteCommand(SaveProc, CommandType.StoredProcedure);
            }
        }

        #endregion Database Methods, using the IDb interface members

        protected void OpenConnection()
        {
            if (this.oConnection.State != ConnectionState.Open)
            {
                this.ErrorMessage = "";
                this.oConnection.Open();
            }
        }

        public void ClearDataTable(DataTable dt)
        {
            dt.Rows.Clear();
        }

        public void UpdateTablestring(DataTable Table, string Column, string Value)
        {
            Table.Rows[0][Column] = Value;
        }

        public string GetTableColumnStringValue(DataTable Table, string Column)
        {
            return Table.Rows[0][Column].ToString();
        }

        public void DeleteTableRow(string tablename, int idcol)
        {
            DebugWrite($"DeleteTableRow: {tablename}");

            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", tablename, "SQL");

            try
            {
                ExecuteCommand("wsgsp_deletetablerow", CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void GenerateAppTableRowSave(DataRow dr)
        {
            int idcolvalue = 0;
            bool Inserting = false;
            string SaveCommand = "";
            string ValueString = "";
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                if (dr.Table.Columns[i].ColumnName == "idcol")
                {
                    if (Convert.ToInt32(dr[i]) <= 0)
                    {
                        Inserting = true;
                    }
                }
            }
            if (Inserting)
            {
                dr["adduser"] = AppUserClass.AppUserId;
                dr["adddate"] = DateTime.Now;
                dr["lckstat"] = "";
                dr["lckuser"] = AppUserClass.AppUserId;
                dr["lckdate"] = DateTime.Now;

                SaveCommand = "Insert INTO " + dr.Table.TableName + "(";
                ValueString = " VALUES (";
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    if (dr.Table.Columns[i].ColumnName != "idcol")
                    {
                        if (!SaveCommand.EndsWith("("))
                        {
                            SaveCommand += ", ";
                        }
                        SaveCommand += "[" + dr.Table.Columns[i].ColumnName + "]";
                        if (!ValueString.EndsWith("("))
                        {
                            ValueString += ", ";
                        }
                        ValueString += "@" + dr.Table.Columns[i].ColumnName;
                    }
                }
                SaveCommand += ")";
                ValueString += ")";
                SaveCommand += ValueString;
            }
            else
            {
                dr["lckstat"] = "";
                dr["lckuser"] = AppUserClass.AppUserId;
                dr["lckdate"] = DateTime.Now;

                SaveCommand = "UPDATE " + dr.Table.TableName + " SET ";
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    if (dr.Table.Columns[i].ColumnName != "idcol")
                    {
                        if (!SaveCommand.TrimEnd().EndsWith("SET"))
                        {
                            SaveCommand += ", ";
                        }
                        SaveCommand += "[" + dr.Table.Columns[i].ColumnName + "]";
                        SaveCommand += " = @" + dr.Table.Columns[i].ColumnName;
                    }
                    else
                    {
                        idcolvalue = Convert.ToInt32(dr[i]);
                    }
                }
                SaveCommand += " WHERE idcol = @idcol";
            }
            //  MessageBox.Show(SaveCommand);
            SetAllParameters(dr);
            if (Inserting == false)
            {
                this.AddParms("@idcol", idcolvalue, "SQL");
            }
            this.ExecuteCommand(SaveCommand, CommandType.Text);
        }

        public string LockTableRow(int idcol, string tablename)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", tablename, "SQL");
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);
            try
            {
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return this.ExecuteStringOutputCommand("wsgsp_locktable", CommandType.StoredProcedure).Trim();
            }
        }

        public void UnlockTableRow(int idcol, string tablename)
        {
            this.ClearParameters();
            this.AddParms("@idcol", idcol, "SQL");
            this.AddParms("@tablename", tablename, "SQL");
            ExecuteCommand("wsgsp_unlocktablerow", CommandType.StoredProcedure);
        }

        protected void SetAllParameters(DataRow Row)
        {
            this.ClearParameters();
            for (int i = 0; i < Row.Table.Columns.Count; i++)
            {
                if (Row.Table.Columns[i].ColumnName != "idcol")
                {
                    this.AddParms(Row.Table.Columns[i].ColumnName, Row[i], "SQL");
                }
            }
        }

        public void CopyDatarow(DataRow sourcerow, DataRow targetrow)
        {
            int columnpointer = 1;

            while (columnpointer <= sourcerow.Table.Columns.Count)
            {
                if (sourcerow.Table.Columns[columnpointer - 1].ColumnName != "idcol")
                {
                    targetrow[columnpointer - 1] = sourcerow[columnpointer - 1];
                }
                columnpointer++;
            }
        }

        public void UnlockInvoicing()
        {
            this.ClearParameters();
            ExecuteCommand("wsgsp_unlockinvoicing", CommandType.StoredProcedure);
        }

        public string LockInvoicing()
        {
            string InvoiceMessage = "";
            this.ClearParameters();
            this.AddParms("@userid", AppUserClass.AppUserId, "SQL");
            this.AddStringOutputParm("@returnmessage", 40);

            try
            {
                InvoiceMessage = this.ExecuteStringOutputCommand("wsgsp_lockinvoicing", CommandType.StoredProcedure).Trim();
                if (InvoiceMessage != "OK")
                {
                    MessageBox.Show(InvoiceMessage);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                InvoiceMessage = "Failure";
            }
            return InvoiceMessage;
        }

        protected void HandleException(Exception e)
        {
            System.Windows.Forms.MessageBox.Show(e.Message);
            WSGUtilitieslib.Telemetry.Telemetry.AddErrorEvent(e.ToString());
        }

        private void DebugWrite(string s)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(s);
#endif
        }
    }

    public class BBRetryException : Exception
    {
        public BBRetryException()
        { }

        public BBRetryException(string error) : base(error)
        {
        }

        public BBRetryException(string error, Exception e) : base(error, e)
        {
        }
    }
} // namespace