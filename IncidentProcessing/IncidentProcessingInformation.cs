using CommonAppClasses;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace IncidentProcessing
{
    public class IncidentProcessingInformation : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private string CurrentState = "";
        private WSGUtilities wsgUtilities = new WSGUtilities("Incident Processing");
        private FrmIncident parentform = new FrmIncident();
        public GetSoMethods getSoMethods = new GetSoMethods("SQL", "SQLConnString");
        private string CommandString = "";
        public Form menuForm = new Form();
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private incidentds inds = new incidentds();
        private FrmIncidentSelector selectorForm = new FrmIncidentSelector();
        private bool LoadingForm = true;
        public int SelectedIncidentid = 0;
        private incidentds selectords = new incidentds();
        private incidentds inissueds = new incidentds();
        private incidentds inresolutionds = new incidentds();
        private incidentds inrootcauseds = new incidentds();
        private incidentds infindingdeptds = new incidentds();
        private incidentds incausingdeptds = new incidentds();
        private incidentds inemployeeds = new incidentds();

        public IncidentProcessingInformation(string DataStore, string AppConfigName)
           : base(DataStore, AppConfigName)
        {
            SetIdcol(inds.incident.idcolColumn);
        }

        public void StartApp()
        {
            parentform.MdiParent = menuForm;
            SetEvents();
            CurrentState = "Select";
            LoadReferenceTables();
            SetBindings();
            RefreshControls();
            parentform.Show();
            LoadingForm = false;
        }

        public void SetBindings()
        {
            // Reference comboboxes
            parentform.comboBoxIssue.DataSource = inissueds.view_sysreference;
            parentform.comboBoxIssue.DisplayMember = "refdescrip";
            parentform.comboBoxIssue.ValueMember = "idcol";
            parentform.comboBoxResolution.DataSource = inresolutionds.view_sysreference;
            parentform.comboBoxResolution.DisplayMember = "refdescrip";
            parentform.comboBoxResolution.ValueMember = "idcol";
            parentform.comboBoxRootcause.DataSource = inrootcauseds.view_sysreference;
            parentform.comboBoxRootcause.DisplayMember = "refdescrip";
            parentform.comboBoxRootcause.ValueMember = "idcol";
            parentform.comboBoxFindingdept.DataSource = infindingdeptds.view_sysreference;
            parentform.comboBoxFindingdept.DisplayMember = "refdescrip";
            parentform.comboBoxFindingdept.ValueMember = "idcol";
            parentform.comboBoxCausingdept.DataSource = incausingdeptds.view_sysreference;
            parentform.comboBoxCausingdept.DisplayMember = "refdescrip";
            parentform.comboBoxCausingdept.ValueMember = "idcol";
            parentform.comboBoxEmployee.DataSource = inemployeeds.view_expandedsysreference;
            parentform.comboBoxEmployee.DisplayMember = "refdescrip";
            parentform.comboBoxEmployee.ValueMember = "idcol";
            parentform.dateTimePickerIncidentdate.DataBindings.Add("Value", inds.incident, "incidentdate", true, DataSourceUpdateMode.OnPropertyChanged);
            parentform.textBoxCost.DataBindings.Clear();
            SetTextBoxCurrencyBinding(parentform.textBoxCost, inds, "incident.cost");
            parentform.textBoxNotes.DataBindings.Clear();
            parentform.textBoxNotes.DataBindings.Add("Text", inds.incident, "notes");
        }

        public void SetEvents()
        {
            parentform.buttonReturn.Click += new EventHandler(buttonReturn_Click);
            parentform.buttonSave.Click += new EventHandler(buttonSaveClick);
            parentform.buttonSelectso.Click += new EventHandler(buttonSelectsoClick);
            parentform.buttonCancel.Click += new EventHandler(buttonCancelClick);
            parentform.buttonDelete.Click += new EventHandler(buttonDeleteClick);

            // Activate comboboxes when textboxes are clicked
            parentform.textBoxIssue.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxResolution.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxRootcause.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxFindingdept.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxCausingdept.Click += new System.EventHandler(ShowComboBox);
            parentform.textBoxEmployee.Click += new System.EventHandler(ShowComboBox);
            // Combobox selections
            parentform.comboBoxIssue.SelectedIndexChanged += new System.EventHandler(SetIssue);
            parentform.comboBoxIssue.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxResolution.SelectedIndexChanged += new System.EventHandler(SetResolution);
            parentform.comboBoxResolution.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxRootcause.SelectedIndexChanged += new System.EventHandler(SetRootcause);
            parentform.comboBoxRootcause.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxFindingdept.SelectedIndexChanged += new System.EventHandler(SetFindingdept);
            parentform.comboBoxFindingdept.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxCausingdept.SelectedIndexChanged += new System.EventHandler(SetCausingdept);
            parentform.comboBoxCausingdept.LostFocus += new System.EventHandler(HideComboBox);
            parentform.comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(SetEmployee);
            parentform.comboBoxEmployee.LostFocus += new System.EventHandler(HideComboBox);

            // Selector form events
            selectorForm.dataGridViewIncidentSelector.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(CaptureDoubleClick);
            selectorForm.buttonNewincident.Click += new EventHandler(SetInsert);
            selectorForm.buttonCancel.Click += new EventHandler(SelectorCancel);
        }

        // Events
        private void buttonSaveClick(object sender, EventArgs e)
        {
            GenerateAppTableRowSave(inds.incident[0]);
            inds.incident.Rows.Clear();
            CurrentState = "Select";
            RefreshControls();
        }

        public void SetIssue(object sender, EventArgs e)
        {
            if (!LoadingForm)
            {
                inds.incident[0].issueid = Convert.ToInt32(parentform.comboBoxIssue.SelectedValue);
                RefreshReferenceTextboxes();
            }
        }

        public void SetResolution(object sender, EventArgs e)
        {
            if (!LoadingForm)
            {
                inds.incident[0].resolutionid = Convert.ToInt32(parentform.comboBoxResolution.SelectedValue);
                RefreshReferenceTextboxes();
            }
        }

        public void SetRootcause(object sender, EventArgs e)
        {
            if (!LoadingForm)
            {
                inds.incident[0].rootcauseid = Convert.ToInt32(parentform.comboBoxRootcause.SelectedValue);
                RefreshReferenceTextboxes();
            }
        }

        public void SetFindingdept(object sender, EventArgs e)
        {
            if (!LoadingForm)
            {
                inds.incident[0].findingdeptid = Convert.ToInt32(parentform.comboBoxFindingdept.SelectedValue);
                inds.incident.AcceptChanges();
                FillEmployeeTable(inds.incident[0].findingdeptid);
                RefreshReferenceTextboxes();
            }
        }

        public void SetCausingdept(object sender, EventArgs e)
        {
            if (!LoadingForm)
            {
                inds.incident[0].causingdeptid = Convert.ToInt32(parentform.comboBoxCausingdept.SelectedValue);
                inds.incident.AcceptChanges();
                FillEmployeeTable(inds.incident[0].causingdeptid);
                RefreshReferenceTextboxes();
            }
        }

        public void SetEmployee(object sender, EventArgs e)
        {
            if (!LoadingForm)
            {
                inds.incident[0].employeeid = Convert.ToInt32(parentform.comboBoxEmployee.SelectedValue);
                RefreshReferenceTextboxes();
            }
        }

        private void buttonDeleteClick(object sender, EventArgs e)
        {
            if (wsgUtilities.wsgReply("Delete this incident?"))
            {
                CommandString = "DELETE FROM incident WHERE idcol = @idcol";
                this.ClearParameters();
                this.AddParms("@idcol", inds.incident[0].idcol, "SQL");
                ExecuteCommand(CommandString, CommandType.Text);
                wsgUtilities.wsgNotice("Incident Deleted");
                CurrentState = "Select";
                RefreshControls();
            }
            else
            {
                wsgUtilities.wsgNotice("Deletion cancelled");
            }
        }

        private void buttonCancelClick(object sender, EventArgs e)
        {
            inds.incident.Rows.Clear();
            CurrentState = "Select";
            RefreshControls();
        }

        private void buttonSelectsoClick(object sender, EventArgs e)
        {
            getSoMethods.returnsono = "";
            getSoMethods.GetSono();
            if (!getSoMethods.wascancelled)
            {
                if (getSoMethods.returnsono.TrimEnd() != "")
                {
                    SelectedIncidentid = 0;
                    parentform.labelSono.Text = "SO: " + getSoMethods.returnsono;
                    parentform.labelCustno.Text = "Customer: " + getSoMethods.quoteds.somast[0].custno;
                    parentform.labelSovalue.Text = "SO Value: " + getSoMethods.quoteds.somast[0].ordamt.ToString("C");
                    selectords.view_expandedincident.Rows.Clear();
                    CommandString = "SELECT * FROM view_expandedincident  WHERE sono = @sono ORDER BY incidentdate ";
                    this.ClearParameters();
                    this.AddParms("@sono", getSoMethods.returnsono, "SQL");
                    this.FillData(selectords, "view_expandedincident", CommandString, CommandType.Text);
                    if (selectords.view_expandedincident.Rows.Count < 1)
                    {
                        ProcessInsert();
                    }
                    else
                    {
                        selectorForm.Inserting = false;
                        selectorForm.dataGridViewIncidentSelector.AutoGenerateColumns = false;
                        selectorForm.dataGridViewIncidentSelector.DataSource = selectords.view_expandedincident;
                        selectorForm.dataGridViewIncidentSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
                        selectorForm.dataGridViewIncidentSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
                        parentform.dateTimePickerIncidentdate.Focus();
                        selectorForm.ShowDialog();

                        if (SelectedIncidentid > 0)
                        {
                            inds.incident.Rows.Clear();
                            CommandString = "SELECT * FROM incident WHERE idcol = @idcol";
                            this.ClearParameters();
                            this.AddParms("@idcol", SelectedIncidentid, "SQL");
                            this.FillData(inds, "incident", CommandString, CommandType.Text);
                            CurrentState = "Process";
                        }
                        else
                        {
                            if (selectorForm.Inserting)
                            {
                                ProcessInsert();
                            }
                        }
                    }
                    RefreshControls();
                }
            }
        }

        private void ProcessInsert()
        {
            CurrentState = "Process";
            inds.incident.Rows.Clear();
            EstablishBlankDataTableRow(inds.incident);
            inds.incident[0].sono = getSoMethods.returnsono;
            inds.incident[0].incidentdate = DateTime.Now.Date;
            parentform.labelCustno.Text = "Customer: " + getSoMethods.quoteds.somast[0].custno;
            parentform.labelSovalue.Text = "SO Value: " + getSoMethods.quoteds.somast[0].ordamt.ToString("C");
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            parentform.Close();
        }

        // Selector form events
        public void CaptureIdcol()
        {
            SelectedIncidentid = CaptureIdCol(selectorForm.dataGridViewIncidentSelector);
        }

        private void CaptureDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureIdcol();
            selectorForm.Close();
        }

        private void CaptureClose(object sender, EventArgs e)
        {
            selectorForm.Close();
        }

        public void SetInsert(object sender, EventArgs e)
        {
            selectorForm.Inserting = true;
            selectorForm.Close();
        }

        public void SelectorCancel(object sender, EventArgs e)
        {
            selectorForm.Close();
        }

        public void RefreshControls()
        {
            DisableControls();
            // Hide combo boxes
            parentform.comboBoxIssue.Visible = false;
            parentform.comboBoxResolution.Visible = false;
            parentform.comboBoxRootcause.Visible = false;
            parentform.comboBoxFindingdept.Visible = false;
            parentform.comboBoxCausingdept.Visible = false;
            parentform.comboBoxEmployee.Visible = false;
            RefreshReferenceTextboxes();
            switch (CurrentState)
            {
                case "Select":
                    {
                        parentform.labelSono.Text = "";
                        parentform.labelUsers.Text = "";
                        parentform.labelSovalue.Text = "";
                        parentform.labelCustno.Text = "";
                        parentform.buttonSelectso.Enabled = true;
                        parentform.dateTimePickerIncidentdate.Visible = false;
                        break;
                    }
                case "Process":
                    {
                        parentform.buttonSave.Enabled = true;
                        parentform.labelIncidentdate.Enabled = true;
                        parentform.dateTimePickerIncidentdate.Enabled = true;
                        parentform.dateTimePickerIncidentdate.Visible = true;
                        parentform.buttonCancel.Enabled = true;
                        parentform.textBoxIssue.Enabled = true;
                        parentform.textBoxResolution.Enabled = true;
                        parentform.textBoxRootcause.Enabled = true;
                        parentform.textBoxFindingdept.Enabled = true;
                        parentform.textBoxCausingdept.Enabled = true;
                        parentform.textBoxEmployee.Enabled = true;
                        parentform.textBoxCost.Enabled = true;
                        parentform.textBoxNotes.Enabled = true;
                        parentform.labelIssue.Enabled = true;
                        parentform.labelResolution.Enabled = true;
                        parentform.labelRootcause.Enabled = true;
                        parentform.labelFindingdept.Enabled = true;
                        parentform.labelCausingdept.Enabled = true;
                        parentform.labelEmployee.Enabled = true;
                        parentform.labelNotes.Enabled = true;
                        parentform.labelCustno.Enabled = true;
                        parentform.labelFixCost.Enabled = true;
                        parentform.labelSono.Enabled = true;
                        parentform.labelSovalue.Enabled = true;
                        if (inds.incident[0].idcol > 0)
                        {
                            parentform.labelUsers.Text = "Incident created by " + inds.incident[0].adduser + " ";
                            parentform.labelUsers.Text += inds.incident[0].adddate.ToShortDateString() + ". Last changed by " + inds.incident[0].lckuser + " " + inds.incident[0].lckdate.ToShortDateString() + ".";
                            parentform.buttonDelete.Enabled = true;
                            parentform.labelUsers.Enabled = true;
                        }
                        parentform.dateTimePickerIncidentdate.Focus();
                        parentform.buttonReturn.TabStop = false;
                        break;
                    }
            }
        }

        public void DisableControls()
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in parentform.Controls)
            {
                c.Enabled = false;
                foreach (Control d in c.Controls)
                    if (d is TabPage)
                        foreach (Control ctl in d.Controls)
                        {
                            if (ctl is Label)
                            {
                                ctl.Enabled = true;
                            }
                            else
                            {
                                ctl.Enabled = false;
                            }
                        }
                    else
                    {
                        d.Enabled = false;
                    }
            }
            // Close is always enabled
            parentform.buttonReturn.Enabled = true;
        }

        public void RefreshReferenceTextboxes()
        {
            parentform.textBoxIssue.Text = "";
            parentform.textBoxResolution.Text = "";
            parentform.textBoxRootcause.Text = "";
            parentform.textBoxFindingdept.Text = "";
            parentform.textBoxCausingdept.Text = "";
            parentform.textBoxEmployee.Text = "";
            if (inds.incident.Rows.Count > 0)
            {
                parentform.textBoxIssue.Text = GetRefDescription(inds.incident[0].issueid);
                parentform.textBoxResolution.Text = GetRefDescription(inds.incident[0].resolutionid);
                parentform.textBoxRootcause.Text = GetRefDescription(inds.incident[0].rootcauseid);
                parentform.textBoxFindingdept.Text = GetRefDescription(inds.incident[0].findingdeptid);
                parentform.textBoxCausingdept.Text = GetRefDescription(inds.incident[0].causingdeptid);
                parentform.textBoxEmployee.Text = GetRefDescription(inds.incident[0].employeeid);
            }
        }

        public void LoadReferenceTables()
        {
            FillReferenceTable(inissueds, "Issue");
            FillReferenceTable(inresolutionds, "Resolution");
            FillReferenceTable(inrootcauseds, "Root Cause");
            FillReferenceTable(infindingdeptds, "Department");
            FillReferenceTable(incausingdeptds, "Department");
            // Employee table is loaded when department is selected
        }

        public void FillReferenceTable(incidentds ds, string GroupName)
        {
            ds.view_sysreference.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@groupname", GroupName, "SQL");
            this.FillData(ds, "view_sysreference", "wsgsp_getsysreferencesbyname", CommandType.StoredProcedure);
        }

        public void FillEmployeeTable(int DeptId)
        {
            // Select employees assigned to that dept
            CommandString = " SELECT * FROM  view_expandedsysreference where groupname = 'Employee' ";
            CommandString += " AND idcol IN (SELECT employeeid FROM ";
            CommandString += " departmentemployee WHERE departmentid = @deptid) ORDER BY refdescrip ";
            // Stop the employee selectedindex event from firing
            LoadingForm = true;
            inemployeeds.view_expandedsysreference.Rows.Clear();
            ClearParameters();
            AddParms("@deptid", DeptId, "SQL");
            FillData(inemployeeds, "view_expandedsysreference", CommandString, CommandType.Text);
            LoadingForm = false;
        }

        public string GetRefDescription(int idcol)
        {
            string refdescription = "Unknown";
            CommandString = "SELECT * FROM view_sysreference WHERE idcol = @idcol ";
            inds.view_sysreference.Rows.Clear();
            ClearParameters();
            AddParms("@idcol", idcol, "SQL");
            FillData(inds, "view_sysreference", CommandString, CommandType.Text);
            if (inds.view_sysreference.Rows.Count > 0)
            {
                refdescription = inds.view_sysreference[0].refdescrip;
            }
            return refdescription;
        }

        public void HideComboBox(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.Visible = false;
            parentform.Update();
        }

        public void ShowComboBox(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string SenderName = tb.Name.ToUpper();
            switch (SenderName)
            {
                case "TEXTBOXISSUE":
                    {
                        parentform.comboBoxIssue.Visible = true;
                        parentform.comboBoxIssue.Enabled = true;
                        break;
                    }
                case "TEXTBOXRESOLUTION":
                    {
                        parentform.comboBoxResolution.Visible = true;
                        parentform.comboBoxResolution.Enabled = true;
                        break;
                    }
                case "TEXTBOXROOTCAUSE":
                    {
                        parentform.comboBoxRootcause.Visible = true;
                        parentform.comboBoxRootcause.Enabled = true;

                        break;
                    }
                case "TEXTBOXFINDINGDEPT":
                    {
                        parentform.comboBoxFindingdept.Visible = true;
                        parentform.comboBoxFindingdept.Enabled = true;
                        break;
                    }
                case "TEXTBOXCAUSINGDEPT":
                    {
                        parentform.comboBoxCausingdept.Visible = true;
                        parentform.comboBoxCausingdept.Enabled = true;
                        break;
                    }

                case "TEXTBOXEMPLOYEE":
                    {
                        parentform.comboBoxEmployee.Visible = true;
                        parentform.comboBoxEmployee.Enabled = true;
                        break;
                    }
            }
            parentform.Update();
        }

        private void SetTextBoxCurrencyBinding(TextBox txtbox, incidentds ds, string fieldname)
        {
            Binding b = new Binding("Text", ds, fieldname);
            {
                b.Format += new ConvertEventHandler(DecimalToCurrencyString);
                b.Parse += new ConvertEventHandler(CurrencyStringToDecimal);
            }
            txtbox.DataBindings.Clear();
            txtbox.DataBindings.Add(b);
        }

        private void CurrencyStringToDecimal(object sender, ConvertEventArgs cevent)
        {
            // The method converts back to decimal type only.
            if (cevent.DesiredType != typeof(decimal)) return;

            // Converts the string back to decimal using the static Parse method.
            cevent.Value = Decimal.Parse(cevent.Value.ToString(),
            NumberStyles.Currency, null);
        }

        private void DecimalToCurrencyString(object sender, ConvertEventArgs cevent)
        {
            // The method converts only to string type. Test this using the DesiredType.
            if (cevent.DesiredType != typeof(string)) return;

            // Use the ToString method to format the value as currency ("c").
            cevent.Value = ((decimal)cevent.Value).ToString("N2");
        }
    }
}