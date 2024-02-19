using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using CommonAppClasses;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using WSGUtilitieslib;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Estimating
{
  public partial class FrmCoverSelector : Form
  {

    public System.Windows.Forms.ToolStripMenuItem parenttoolstripmenuitem = null;
    AppUtilities appUtilities = new AppUtilities();
    AppConstants myAppconstants = new AppConstants();
    WSGUtilities wsgUtilities = new WSGUtilities("Cover Selector");
    // Create the customer processing object
    CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");
    // Create the App Information processing object
    AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
    // Create the SO Information processing object
    Soinf soinf = new Soinf("SQL", "SQLConnString");
    BindingSource bindingCovers = new BindingSource();
  
    public FrmCoverSelector()
    {
      InitializeComponent();
      SelectedCover = "";
      dataGridViewCoverSelector.AutoGenerateColumns = false;
      dataGridViewCoverSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
      dataGridViewCoverSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
      bindingCovers.DataSource = soinf.somastds.view_coverdata;
      dataGridViewCoverSelector.DataSource = bindingCovers;

    }
    public string sono;
    public string version;
    public string SelectedCover {get; set;}
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    public void CaptureCover()
    {
      CurrencyManager xCM =
     (CurrencyManager)dataGridViewCoverSelector.BindingContext[dataGridViewCoverSelector.DataSource,
       dataGridViewCoverSelector.DataMember];
      DataRowView xDRV = (DataRowView)xCM.Current;
      DataRow xRow = xDRV.Row;
      // Capture the cover
      SelectedCover = (string)xRow["cover"];
    }


    private void FrmCoverSelector_Load(object sender, EventArgs e)
    {
      int rowcount = 1;
      int rrow = 0;
      soinf.LoadCoverViewData(sono, version);
      while (rowcount <= soinf.somastds.view_coverdata.Rows.Count)
       {
         if ( soinf.somastds.view_coverdata[rrow].covertype != "C ")
           {
             soinf.somastds.view_coverdata[rrow].Delete();  
           }
          rowcount ++;
          rrow++;
     
        }
  
     soinf.somastds.view_coverdata.AcceptChanges();
    }

    private void dataGridViewCoverSelector_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      CaptureCover();
      this.Close();
    }

    private void dataGridViewCoverSelector_KeyDown(object sender, KeyEventArgs e)
    {
     if (e.KeyCode == Keys.Return)
     {
       CaptureCover();
       this.Close();
     }
    }
 }
}  
