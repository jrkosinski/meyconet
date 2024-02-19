﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenDs
{
  public partial class FrmGenerateTypedDataSet : Form
  {
    string connectionString = "Data Source=BILL-PC;Initial Catalog=meycosqldata;Persist Security Info=True;User ID=sa;Password=master";

    // Establish VFP Connection

    OleDbConnection vfpappdataconn = new OleDbConnection("Provider=VFPOLEDB.1;Data Source=d:\\apps\\meyco\\pro60\\data");
    OleDbConnection vfpsysdataconn = new OleDbConnection("Provider=VFPOLEDB.1;Data Source=d:\\apps\\meyco\\pro60");

    // set up the DataSet with the DataSet name
    DataSet ds = new DataSet();


    public FrmGenerateTypedDataSet()
    {
      InitializeComponent();
    }



    private void buttonCreateQuoteDataset_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "quote";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("somast");
      CreateDataTable("soline");
      CreateDataTable("soaddr");
      CreateDataTable("socover");
      CreateDataTable("soversion");
      CreateDataTable("arcust");
      CreateDataTable("icitem");
      CreateDataTable("warranty");
      // Views
      CreateDataTable("view_coverandlines");
      CreateDataTable("view_expandedsoline");
      CreateDataTable("view_coverdata");
      CreateDataTable("view_somastdata");
      CreateDataTable("view_soreportlinedata");
      CreateDataTable("view_versiondata");
      CreateSchema("quote.xsd");

    }


    public void CreateSchema(string filename)
    {
      //Write out the schema
      ds.WriteXmlSchema(filename);
      MessageBox.Show(filename + " successfully created");
    }




    private void CreateVFPDataTable(string sourcetablename, string targettablename, OleDbConnection vfpconn)
    {

      try
      {
        // set up the Oledb Command and DataAdapter with the Select Command
        OleDbCommand oledbc = new OleDbCommand("SELECT top 1 * FROM " + sourcetablename + " ORDER BY 1");
        oledbc.CommandType = CommandType.Text;
        oledbc.Connection = vfpconn;
        OleDbDataAdapter da = new OleDbDataAdapter(oledbc);
        da.TableMappings.Clear();
        da.TableMappings.Add("Table", targettablename);
        // and fill the Datatable
        da.Fill(ds);

      }
      catch (Exception ex)
      {
        string msg = ex.Message;
        if (ex.InnerException != null)
          msg += "\r\n" + ex.InnerException.Message;
        MessageBox.Show("The following error occurred: " + ex.Message);
      }
    }

    private void CreateDataTable(string tablename)
    {

      try
      {
        // set up the Sql Command and DataAdapter with the Stored Proc name
        SqlCommand sc = new SqlCommand("SELECT top 1 * FROM " + tablename, new SqlConnection(connectionString));
        sc.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(sc);

        da.TableMappings.Clear();
        da.TableMappings.Add("Table", tablename);


        // and fill the DataSet
        da.Fill(ds);

      }
      catch (Exception ex)
      {
        string msg = ex.Message;
        if (ex.InnerException != null)
          msg += "\r\n" + ex.InnerException.Message;
        MessageBox.Show("The following error occurred: " + ex.Message);
      }
    }

    private void buttonCreateReferenceDataset_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "reference";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("quoverlap");
      CreateDataTable("quspacing");
      CreateDataTable("qucolor");
      CreateDataTable("qumaterial");
      CreateDataTable("capacitycalendar");
      CreateDataTable("productionunitschedule");

      // Views
      CreateDataTable("view_icitemdata");
      CreateDataTable("view_quoverlapdata");
      CreateDataTable("view_quspacingdata");
      CreateDataTable("view_qucolordata");
      CreateDataTable("view_qumaterialdata");
      CreateDataTable("view_shipdateprodunits");
      CreateSchema("reference.xsd");
    }

    private void buttonCreateCustomer_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "customer";
      ds.Tables.Clear();
      //Tables
      CreateDataTable("arcust");
      CreateDataTable("aracadr");
      CreateDataTable("contact");
      CreateDataTable("custhwpref");
      CreateDataTable("emailaddress");
      CreateDataTable("custalerts");
      CreateDataTable("custroutestep");
      //Views
      CreateDataTable("view_customershiptolist");
      CreateDataTable("view_expandedcusthwpref");
      CreateDataTable("view_expandedcustalerts");
      CreateSchema("customer.xsd");
    }

    private void buttonCreateSystemDataset_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "system";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("appinfo");
      CreateDataTable("appuser");
      CreateDataTable("eventlog");
      CreateDataTable("systemcomments");
      CreateDataTable("sysrefgroup");
      CreateDataTable("sysreference");
      // Views
      CreateDataTable("view_systemcomments");
      CreateDataTable("view_sysreference");
      CreateSchema("system.xsd");
    }

    private void buttonCreateIcitemDataset_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "item";
      ds.Tables.Clear();
      //Tables
      CreateDataTable("icitem");
      CreateDataTable("icloct");
      //Views
      CreateDataTable("view_icitemdata");
      CreateSchema("item.xsd");

    }

    private void buttonCreatePriceDataset_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "price";
      ds.Tables.Clear();
      //Tables
      CreateDataTable("quprsdetail");
      CreateDataTable("quprshead");
      CreateDataTable("quprslocator");
      //Views
      CreateDataTable("view_quprsdetaildata");
      CreateDataTable("view_prslocatordata");
      CreateDataTable("view_quprsheaddata");
      CreateSchema("price.xsd");
    }

    private void buttonCreateQuotereportDataset_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "quoterpt";
      ds.Tables.Clear();
      //Tables
      CreateDataTable("somast");
      CreateDataTable("arcust");
      CreateDataTable("soaddr");
      //Views
      CreateDataTable("view_soreportlinedata");
      CreateDataTable("view_salesanalysislines");
      CreateDataTable("view_customertransactions");
      CreateSchema("quoterpt.xsd");

    }

    private void buttonCreateVFPApplicationDataSet_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "MeycoPro60DataDataSet";
      ds.Tables.Clear();
      //Tables
      CreateVFPDataTable("arcust01", "arcust", vfpappdataconn);
      CreateVFPDataTable("arcadr01", "arcadr", vfpappdataconn);
      CreateVFPDataTable("armast01", "armast", vfpappdataconn);
      CreateVFPDataTable("arymst01", "arymst", vfpappdataconn);
      CreateVFPDataTable("artran01", "artran", vfpappdataconn);
      CreateVFPDataTable("ardist01", "ardist", vfpappdataconn);
      CreateVFPDataTable("arglac01", "arglac", vfpappdataconn);
      CreateVFPDataTable("somast01", "somast", vfpappdataconn);
      CreateVFPDataTable("soaddr01", "soaddr", vfpappdataconn);
      CreateVFPDataTable("soymst01", "soymst", vfpappdataconn);
      CreateVFPDataTable("sotran01", "sotran", vfpappdataconn);
      CreateVFPDataTable("icitem01", "icitem", vfpappdataconn);
      CreateVFPDataTable("icpric01", "icpric", vfpappdataconn);
      CreateVFPDataTable("icdist01", "icpric", vfpappdataconn);
      CreateVFPDataTable("icpgrp01", "icpgrp", vfpappdataconn);
      CreateVFPDataTable("icloct01", "icloct", vfpappdataconn);
      CreateSchema("meycopro60datadataset.xsd");
    }

    private void buttonMeycoSystemDataDataset_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "MeycoSystemDataDataSet";
      ds.Tables.Clear();
      //Tables
      CreateVFPDataTable("sycrlst", "sycrlst", vfpsysdataconn);
      CreateVFPDataTable("sycrule", "sycrule", vfpsysdataconn);
      CreateVFPDataTable("sysdata", "sysdata", vfpsysdataconn);
      CreateVFPDataTable("sycdfis", "sycdfis", vfpsysdataconn);
      CreateVFPDataTable("syusess", "syusess", vfpsysdataconn);
      CreateSchema("meycosystemdatadataset.xsd");
    }

    private void buttonCreateInspectionDataSet_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "inspection";
      ds.Tables.Clear();
      //Tables
      CreateDataTable("inspmast");
      CreateDataTable("inspversion");
      CreateDataTable("inspline");
      CreateDataTable("arcust");
      CreateDataTable("somast");
      CreateDataTable("soaddr");

      //Views
      CreateDataTable("view_insplinedata");
      CreateDataTable("view_inspversiondata");
      CreateDataTable("view_inspversions");
      CreateDataTable("view_inspversiondata");
      CreateDataTable("view_inspreport");
      CreateDataTable("view_inspmastexpanded");
      CreateSchema("inspection.xsd");


    }

    private void button1_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "sysreference";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("quoverlap");
      CreateDataTable("quspacing");
      CreateDataTable("qucolor");
      CreateDataTable("qumaterial");
      // Views
      CreateDataTable("view_icitemdata");
      CreateDataTable("view_quoverlapdata");
      CreateDataTable("view_quspacingdata");
      CreateDataTable("view_qucolordata");
      CreateDataTable("view_qumaterialdata");
      CreateSchema("reference.xsd");

    }

    private void buttonCreateOrderDataSet_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "order";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("somast");
      CreateDataTable("soline");
      CreateDataTable("soaddr");
      // Views
      CreateDataTable("view_somastdata");
      CreateDataTable("view_expandedsoline");
      CreateSchema("order.xsd");

    }

    private void buttonCreateOrderRptDataSet_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "orderrpt";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("arcust");
      CreateDataTable("somast");
      CreateDataTable("soline");
      CreateDataTable("soaddr");
      CreateDataTable("view_expandedsoline");
      // Views
      CreateDataTable("view_salesanalysislines");
      CreateDataTable("view_customertransactions");
      CreateDataTable("view_somastdata");
      CreateSchema("orderrpt.xsd");


    }

    private void buttonCreateTrackingDataSet_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "tracking";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("step");
      CreateDataTable("route");
      CreateDataTable("custroutestep");
      CreateDataTable("workgroup");
      CreateDataTable("workgroupstep");
      CreateDataTable("stepsubscriber");
      CreateDataTable("jtrak");
      CreateDataTable("appuser");
      // Views
      CreateDataTable("view_routedata");
      CreateDataTable("view_expandedroutedata");
      CreateDataTable("view_workgroupstepdata");
      CreateDataTable("view_stepsubscriberdata");
      CreateDataTable("view_solasttrackingdata");
      CreateDataTable("view_customeralertdata");
      CreateDataTable("view_latestsotrackingstepdata");
      CreateDataTable("view_trackingstepdata");
      CreateSchema("tracking.xsd");
    }

    private void buttonCreateWarrantyDataSet_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "warranty";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("warranty");
      CreateSchema("warranty.xsd");

    }

    private void buttonCreateInventoryDataset_Click(object sender, EventArgs e)
    {
      ds.DataSetName = "inventoryds";
      ds.Tables.Clear();
      // Tables
      CreateDataTable("ictran");
      CreateDataTable("icitem");
      // Views
      CreateDataTable("view_itemloctidtdateonhand");
      CreateDataTable("view_itemloctidonhand");
      CreateDataTable("view_openordersitemsonhand");
      CreateDataTable("view_expandedictran");
      CreateDataTable("view_openorderstockitems");
      CreateSchema("inventoryds.xsd");
    }

    private void buttonCreateIncidentDataset_Click(object sender, EventArgs e)
    {

        ds.DataSetName = "incidentds";
        ds.Tables.Clear();
        // Tables
        CreateDataTable("sysrefgroup");
        CreateDataTable("departmentemployee");
        CreateDataTable("somast");
        CreateDataTable("arcust");
        CreateDataTable("incident");
        // Views
        CreateDataTable("view_sysreference");
        CreateDataTable("view_refdescription");
        CreateDataTable("view_expandedsysreference");
        CreateDataTable("view_expandedincident");
      
        CreateSchema("incidentds.xsd");
    }

    private void buttonCreateTicketDataSet_Click(object sender, EventArgs e)
    {
        ds.DataSetName = "ticketds";
        ds.Tables.Clear();
        // Tables
        CreateDataTable("sysrefgroup");
        CreateDataTable("somast");
        CreateDataTable("arcust");
        CreateDataTable("ticket");
        CreateDataTable("contact");
        CreateDataTable("appuser");
        // Views
        CreateDataTable("view_sysreference");
        CreateDataTable("view_refdescription");
        CreateDataTable("view_expandedticket");
        CreateDataTable("view_expandedsysreference");
        CreateSchema("ticket.xsd");
    }
  }
}
