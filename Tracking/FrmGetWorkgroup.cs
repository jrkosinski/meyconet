using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using WSGUtilitieslib;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLTracking
{
   public partial class FrmGetWorkgroup : Form
   {
      private BindingSource bindingWorkgroupData = new BindingSource();
      public SqlConnection conn = new SqlConnection();
      public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
      wsgclasses.AppUtilities appUtilities = new wsgclasses.AppUtilities();
      wsgclasses.AppConstants myAppconstants = new wsgclasses.AppConstants();
      WSGUtilities wsgUtilities = new WSGUtilities("Work Group Selector");
      
      public FrmGetWorkgroup()
      {
         InitializeComponent();
         // Set the DataGridView control's border.
         dataGridViewWorkgroups.BorderStyle = BorderStyle.Fixed3D;

         conn.ConnectionString = myAppconstants.SQLConnectionString;
         // Fill the grid with data
         filldatagrid();

         // The value for alternating rows overrides the value for all rows. 
         dataGridViewWorkgroups.RowsDefaultCellStyle.BackColor = Color.LightGray;
         dataGridViewWorkgroups.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
         dataGridViewWorkgroups.Focus();

      
      }
      private int selectedWorkGroupId;
      public int SelectedWorkgroupId
      {

         get
         {
            return selectedWorkGroupId;
         }
         set
         {
            selectedWorkGroupId = value;
         }

      }

      private string selectedWorkGroupName;
      public string SelectedWorkgroupName
      {

         get
         {
            return selectedWorkGroupName;
         }
         set
         {
            selectedWorkGroupName = value;
         }

      }

      private void filldatagrid()
      {
         DataTable dtWorkgroupData = new DataTable();
         SqlCommand cmd = new SqlCommand("dbo.sp_getworkgroupdata");
         appUtilities.makeSQLCommand(ref cmd, ref conn);

         try
         {
            conn.Open();
            dtWorkgroupData.Load(cmd.ExecuteReader());
            conn.Close();
            bindingWorkgroupData.DataSource = dtWorkgroupData;
            dataGridViewWorkgroups.DataSource = bindingWorkgroupData;
        
         }
         catch (Exception ex)
         {
            conn.Close();
            MessageBox.Show(ex.Message, "SQL Error");
         }

      }

      public void CaptureWorkgroupKeyData()
      {

         CurrencyManager xCM =
   (CurrencyManager)dataGridViewWorkgroups.BindingContext[dataGridViewWorkgroups.DataSource,
        dataGridViewWorkgroups.DataMember];
         DataRowView xDRV = (DataRowView)xCM.Current;
         DataRow xRow = xDRV.Row;
         // Save the select SO number

         SelectedWorkgroupName = xRow["groupname"].ToString();
         SelectedWorkgroupId = (int)xRow["idcol"];
         this.Close();

      }

      private void buttonButtonCancel_Click(object sender, EventArgs e)
      {
         SelectedWorkgroupId = 0;
         this.Close();
      }

      private void dataGridViewWorkgroups_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Return)
         {
            CaptureWorkgroupKeyData();
           
         }

      }

      private void dataGridViewWorkgroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
      {
         CaptureWorkgroupKeyData();

      }

   }
}
