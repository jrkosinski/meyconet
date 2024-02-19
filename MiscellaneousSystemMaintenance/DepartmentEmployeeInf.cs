using CommonAppClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MiscellaneousSystemMaintenance
{
    public class DepartmentEmployeeInf : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private FrmDepartmentEmployee parentform = new FrmDepartmentEmployee();
        public Form menuForm = new Form();
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private int DeptId = 0;
        private incidentds inds = new incidentds();
        private incidentds unassignedds = new incidentds();
        private incidentds assignedds = new incidentds();

        public DepartmentEmployeeInf(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            SetIdcol(inds.departmentemployee.idcolColumn);
        }

        public void StartApp()
        {
            EstablishControls();
            SetEvents();
            parentform.MdiParent = menuForm;
            parentform.Show();
        }

        public void SetEvents()
        {
            parentform.listBoxUnassigned.DoubleClick += new EventHandler(listBoxUnassignedAlerts_DoubleClick);
            parentform.listBoxAssigned.DoubleClick += new EventHandler(listBoxAssignedAlerts_DoubleClick);
            parentform.buttonReturn.Click += new EventHandler(buttonReturn_Click);
            parentform.listBoxDepartment.DoubleClick += new EventHandler(listBoxDepartment_DoubleClick);
            parentform.buttonSave.Click += new EventHandler(buttonSaveClick);
        }

        // Events
        private void listBoxDepartment_DoubleClick(object sender, EventArgs e)
        {
            DeptId = inds.view_expandedsysreference[parentform.listBoxDepartment.SelectedIndex].idcol;
            LoadAssignments();
        }

        private void buttonSaveClick(object sender, EventArgs e)
        {
            SaveAssignments();
            DeptId = 0;
            assignedds.view_expandedsysreference.Rows.Clear();
            unassignedds.view_expandedsysreference.Rows.Clear();
            unassignedds.AcceptChanges();
            assignedds.AcceptChanges();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            parentform.Close();
        }

        private void listBoxUnassignedAlerts_DoubleClick(object sender, EventArgs e)
        {
            if (unassignedds.view_expandedsysreference.Rows.Count > 0)
            {
                assignedds.view_expandedsysreference.ImportRow(unassignedds.view_expandedsysreference.Rows[parentform.listBoxUnassigned.SelectedIndex]);
                unassignedds.view_expandedsysreference.Rows[parentform.listBoxUnassigned.SelectedIndex].Delete();
                assignedds.view_expandedsysreference.AcceptChanges();
                unassignedds.view_expandedsysreference.AcceptChanges();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no assigned alerts");
            }
        }

        private void listBoxAssignedAlerts_DoubleClick(object sender, EventArgs e)
        {
            if (assignedds.view_expandedsysreference.Rows.Count > 0)
            {
                unassignedds.view_expandedsysreference.ImportRow(assignedds.view_expandedsysreference.Rows[parentform.listBoxAssigned.SelectedIndex]);
                assignedds.view_expandedsysreference.Rows[parentform.listBoxAssigned.SelectedIndex].Delete();
                assignedds.view_expandedsysreference.AcceptChanges();
                unassignedds.view_expandedsysreference.AcceptChanges();
            }
            else
            {
                wsgUtilities.wsgNotice("There are no assigned alerts");
            }
        }

        public void SaveAssignments()
        {
            string commandstring = "DELETE FROM departmentemployee WHERE departmentid = @deptid";
            ClearParameters();
            AddParms("@deptid", DeptId, "SQL");
            try
            {
                ExecuteCommand(commandstring, CommandType.Text);
            }
            catch (SqlException ex)
            {
                HandleException(ex);
            }

            for (int i = 0; i <= assignedds.view_expandedsysreference.Rows.Count - 1; i++)
            {
                inds.departmentemployee.Rows.Clear();
                EstablishBlankDataTableRow(inds.departmentemployee);
                inds.departmentemployee[0].departmentid = DeptId;
                inds.departmentemployee[0].employeeid = assignedds.view_expandedsysreference[i].idcol;
                GenerateAppTableRowSave(inds.departmentemployee[0]);
            }
        }

        public void EstablishControls()
        {
            // Load Departments
            string commandstring = "SELECT * FROM view_expandedsysreference WHERE groupname = 'Department' ORDER BY refdescrip";

            inds.view_expandedsysreference.Rows.Clear();
            this.ClearParameters();
            this.FillData(inds, "view_expandedsysreference", commandstring, CommandType.Text);
            parentform.listBoxDepartment.DataSource = inds.view_expandedsysreference;
            parentform.listBoxDepartment.DisplayMember = "refdescrip";
            parentform.listBoxDepartment.ValueMember = "idcol";
        }

        public void LoadAssignments()
        {
            // Load assigned and unassigned employees

            // Unasssigned
            string commandstring = " SELECT * FROM  view_expandedsysreference where groupname = 'Employee' ";
            commandstring += " AND idcol NOT IN (SELECT employeeid FROM ";
            commandstring += " departmentemployee WHERE departmentid = @deptid) ORDER BY refdescrip ";
            unassignedds.view_expandedsysreference.Rows.Clear();
            ClearParameters();
            AddParms("@deptid", DeptId, "SQL");
            FillData(unassignedds, "view_expandedsysreference", commandstring, CommandType.Text);
            parentform.listBoxUnassigned.DataSource = unassignedds.view_expandedsysreference;
            parentform.listBoxUnassigned.DisplayMember = "refdescrip";
            parentform.listBoxUnassigned.ValueMember = "idcol";

            // Aasssigned
            commandstring = " SELECT * FROM  view_expandedsysreference where groupname = 'Employee' ";
            commandstring += " AND idcol IN (SELECT employeeid FROM ";
            commandstring += " departmentemployee WHERE departmentid = @deptid) ORDER BY refdescrip ";
            assignedds.view_expandedsysreference.Rows.Clear();
            ClearParameters();
            AddParms("@deptid", DeptId, "SQL");
            FillData(assignedds, "view_expandedsysreference", commandstring, CommandType.Text);
            parentform.listBoxAssigned.DataSource = assignedds.view_expandedsysreference;
            parentform.listBoxAssigned.DisplayMember = "refdescrip";
            parentform.listBoxAssigned.ValueMember = "idcol";
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
    }
}