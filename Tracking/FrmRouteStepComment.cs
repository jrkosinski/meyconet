using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using WSGBaseClassLibrary;
using WSGUtilitieslib;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tracking
{
  public partial class FrmRouteStepComment : Form
  {
    private BindingSource bindingRouteData = new BindingSource();
    public SqlConnection conn = new SqlConnection();
    public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
    AppUtilities appUtilities = new AppUtilities();
    AppConstants myAppconstants = new AppConstants();
    WSGUtilities wsgUtilities = new WSGUtilities("Step Routing - Comments");
      
    public FrmRouteStepComment()
    {
      InitializeComponent();
      dataGridViewRouteData.BorderStyle = BorderStyle.Fixed3D;
      conn.ConnectionString = myAppconstants.SQLConnectionString;

      // The value for alternating rows overrides the value for all rows. 
      dataGridViewRouteData.RowsDefaultCellStyle.BackColor = Color.LightGray;
      dataGridViewRouteData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
      dataGridViewRouteData.AutoGenerateColumns = false;
      dataGridViewRouteData.RowsDefaultCellStyle.BackColor = Color.LightGray;
      dataGridViewRouteData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        
    }
    private int currentRouteId;
    public int CurrentRouteId
    {

      get
      {
        return currentRouteId;
      }
      set
      {
        currentRouteId = value;
      }
    }
    private DateTime trackDate;
    public DateTime TrackDate
    {

      get
      {
        return trackDate;
      }
      set
      {
        trackDate = value;
      }
    }
        
    private int selectedStepId;
    public int SelectedStepId
    {
      get
      {
        return selectedStepId;
      }
      set
      {
        selectedStepId = value;
      }
    }
    private int routeToStepId;
    public int RouteToStepId
    {
      get
      {
        return routeToStepId;
      }
      set
      {
        routeToStepId = value;
      }
    }
   
    private string currentSono;
    public string CurrentSono
    {
      get
      {
        return currentSono;
      }
      set
      {
        currentSono = value;
      }
    }
    public void CaptureStepKeyData()
    {
      CurrencyManager xCM =
     (CurrencyManager)dataGridViewRouteData.BindingContext[dataGridViewRouteData.DataSource,
     dataGridViewRouteData.DataMember];
      DataRowView xDRV = (DataRowView)xCM.Current;
      DataRow xRow = xDRV.Row;
      // Save the selected step id
      RouteToStepId = (int)xRow["stepid"];
      routeSO();
      this.Close();
    }

    private void filldatagrid()
    {
      DataTable dtRouteData = new DataTable();
      SqlCommand cmd = new SqlCommand("dbo.sp_getroutedata");
      appUtilities.makeSQLCommand(ref cmd, ref conn);
      cmd.Parameters.Add("@route", SqlDbType.Int);
      cmd.Parameters["@route"].Value = CurrentRouteId;

      try
      {
        conn.Open();
        dtRouteData.Load(cmd.ExecuteReader());
        conn.Close();
        if (dtRouteData.Rows.Count > 0)
        {
          bindingRouteData.DataSource = dtRouteData;
          dataGridViewRouteData.DataSource = bindingRouteData;

          // The value for alternating rows overrides the value for all rows. 
          dataGridViewRouteData.Visible = true;
          dataGridViewRouteData.RowsDefaultCellStyle.BackColor = Color.LightGray;
          dataGridViewRouteData.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
          dataGridViewRouteData.Focus();
        }
        else
        {
          wsgUtilities.wsgNotice("There are no next steps for this step.");
          dataGridViewRouteData.Visible = false;
        }
      }
      catch (Exception ex)
      {
        conn.Close();
        MessageBox.Show(ex.Message, "SQL Error");
      }
    } // end filldatagrid

    private void Form2_Shown(object sender, EventArgs e)
    {
      filldatagrid();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void buttonComment_Click(object sender, EventArgs e)
    {

    } // end shown 

    private void routeSO()
    {
      // Route the Sales Order to the designated step - CurrentStepID 
      SqlCommand cmd = new SqlCommand("dbo.sp_inserttrackingevent");
      appUtilities.makeSQLCommand(ref cmd, ref conn);
      cmd.Parameters.Add("@stepid", SqlDbType.Int);
      cmd.Parameters["@stepid"].Value = RouteToStepId;
      cmd.Parameters.Add("@sono", SqlDbType.Char);
      cmd.Parameters["@sono"].Value = CurrentSono;
      cmd.Parameters.Add("@trackdate", SqlDbType.DateTime);
      cmd.Parameters["@trackdate"].Value = TrackDate;
      cmd.Parameters.Add("@comment", SqlDbType.NVarChar);
      cmd.Parameters["@comment"].Value = textBoxComment.Text;
      cmd.Parameters.Add("@userid", SqlDbType.Char);
      cmd.Parameters["@userid"].Value = AppUserClass.AppUserId;
      conn.Open();
      try
      {
        cmd.ExecuteNonQuery();
        conn.Close();
        this.Update();
        wsgUtilities.wsgNotice("Routing complete");
      }
      catch (Exception ex)
      {
        conn.Close();
        MessageBox.Show(ex.Message, "SQL Error");
      } // end catch

    }

    private void dataGridViewRouteData_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
       CaptureStepKeyData();
     
    }

    private void dataGridViewRouteData_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Return)
      {

        CaptureStepKeyData();
        this.Close();
      }
    }

    private void FrmRouteStepComment_Load(object sender, EventArgs e)
    {

    } // route SO

  }
}
