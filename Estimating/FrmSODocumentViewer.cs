using System;
using System.Collections.Generic;
using System.IO;
using CommonAppClasses;
using System.ComponentModel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
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
  public partial class FrmSODocumentViewer : Form
  {
    AppUtilities appUtilities = new AppUtilities();
    AppConstants myAppconstants = new AppConstants();
    WSGUtilities wsgUtilities = new WSGUtilities("Customer Information");
    // Create the customer processing object
    CustomerAccess customerAccess = new CustomerAccess("SQL", "SQLConnString");
    // Create the App Information processing object
    AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
    Soinf soinf= new Soinf("SQL", "SQLConnString");

    public FrmSODocumentViewer()
    {
      InitializeComponent();
    }
    public string CurrentSono{get; set;} 
    public string SODocument{get; set;}
   
    

    private void buttonClose_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }

    private void crystalReportViewerWSG_Load_1(object sender, EventArgs e)
    {
      // Establish somastds dataset, somast and soaddr tables
      soinf.GetSomastBySono(CurrentSono);
      // Establish customer table
      soinf.getSingleCustomerData(soinf.somastds.somast[0].custid);
      // Establish Sales order line view
      soinf.somastds.view_soreportlinedata.Rows.Clear();
      soinf.getallsoreportdata(soinf.somastds.somast[0].sono);
      Estimate estrpt = new Estimate();
      Invoice invrpt = new Invoice();
      WorkOrder worpt = new WorkOrder();
      PackingList plrpt = new PackingList();
      switch (SODocument)
      {
        case "Invoice":
          {
            invrpt.SetDataSource(soinf.quorptds);
            crystalReportViewerWSG.ReportSource = invrpt;
            break;
          }

        case "Work Order":
          {
            worpt.SetDataSource(soinf.quorptds);
            crystalReportViewerWSG.ReportSource = worpt;
            break;
          }
        case "Packing List":
          {
            plrpt.SetDataSource(soinf.quorptds);
            crystalReportViewerWSG.ReportSource = plrpt;
            break;
          }
        case "Estimate":
          {
            estrpt.SetDataSource(soinf.quorptds);
            crystalReportViewerWSG.ReportSource = estrpt;
            break;
          }

        default:
          {
            estrpt.SetDataSource(soinf.quorptds);
            crystalReportViewerWSG.ReportSource = estrpt;
            break;
          }
      }
    
  
    }

   }
}
