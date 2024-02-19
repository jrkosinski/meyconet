using System;
using System.Configuration;
using System.Data;
using WSGUtilitieslib;

namespace CommonAppClasses
{
    public class InvoicingMethods : WSGDataAccess
    {
        private AppUtilities appUtilities = new AppUtilities();
        private AppConstants myAppconstants = new AppConstants();
        private WSGUtilities wsgUtilities = new WSGUtilities("SO Information");
        private AppInformation appInformation = new AppInformation("SQL", "SQLConnString");
        private MiscellaneousDataMethods miscDataMethods = new MiscellaneousDataMethods("SQL", "SQLConnString");
        private quote sods = new quote();
        private customer ards = new customer();
        private alereds AlereDs = new alereds();
        private AlereDataMethods alereDataMethods = new AlereDataMethods();

        public InvoicingMethods()
            : base("SQL", "SQLConnString")
        {
        }

        public string CreateInvoice(string sono, DateTime invdate)
        {
            /*ALERE tables

             coactlog
             copref
             glheader
             glitem
             glpost
             gltotals
             imactlog
             slcust
             slheader
             sllines
             cofilter
             glchart
             coinfo
             */
            string yearprd = "";
            invdate = invdate.Date;
            string CommandString = "";
            string invno = "";
            bool processinvoice = true;
            sods.somast.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            CommandString = "SELECT * FROM  somast WHERE sono = @sono";
            this.FillData(sods, "somast", CommandString, CommandType.Text);
            if (sods.somast.Rows.Count < 1)
            {
                wsgUtilities.wsgNotice("This SO cannot be found. Get help");
                processinvoice = false;
            }

            if (processinvoice == true)
            {
                AlereDs.glperiod.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@invdate", invdate, "SQL");
                CommandString = "SELECT * FROM glperiod WHERE  @invdate >= strtdate AND  @invdate <= closdate";
                this.FillData(AlereDs, "glperiod", CommandString, CommandType.Text);
                if (AlereDs.glperiod.Rows.Count > 0)
                {
                    yearprd = AlereDs.glperiod[0].fiscalyr + AlereDs.glperiod[0].periodid;
                }

                if (yearprd.TrimEnd() == "")
                {
                    wsgUtilities.wsgNotice("That date cannot be found in the GL.Invoice Creation Cancelled");
                    processinvoice = false;
                }
            }
            if (processinvoice == true)
            {
                invno = "";
                AlereDs.coinfo.Rows.Clear();
                this.ClearParameters();
                CommandString = "SELECT * FROM coinfo";
                this.FillData(AlereDs, "coinfo", CommandString, CommandType.Text);
                if (AlereDs.coinfo.Rows.Count > 0)
                {
                    invno = AlereDs.coinfo[0].arnumber.ToString().PadLeft(10);
                    CommandString = "UPDATE coinfo SET arnumber = arnumber +1";
                    ExecuteCommand(CommandString, CommandType.Text);
                }

                if (invno.TrimEnd() != "")
                {
                    sods.somast[0].invno = invno;
                    sods.somast[0].invdte = invdate;
                    sods.somast[0].sostat = "C";
                    //   SaveSomastData();

                    //Position billing company address table
                    ards.arcust.Rows.Clear();
                    this.AddParms("@custno", sods.somast[0].custno, "SQL");
                    CommandString = "SELECT * FROM arcust WHERE custno = @custno";
                    this.FillData(ards, "arcust", CommandString, CommandType.Text);

                    // Create ALERE AR Table Rows
                    //slheader
                    AlereDs.slheader.Rows.Clear();
                    AlereDs.slheader.Rows.Add();
                    EstablishBlankDataTableRow(AlereDs.slheader);

                    AlereDs.slheader[0].docid = invno;
                    AlereDs.slheader[0].doctype = "AR";
                    AlereDs.slheader[0].ordrstat = "A";
                    AlereDs.slheader[0].billto = sods.somast[0].custno;
                    AlereDs.slheader[0].shipto = sods.somast[0].custno;
                    AlereDs.slheader[0].billlocn = sods.somast[0].custno; ;
                    AlereDs.slheader[0].shipfrom = sods.somast[0].defloc;
                    AlereDs.slheader[0].shipstax = sods.somast[0].taxrate;
                    AlereDs.slheader[0].taxdist = sods.somast[0].taxdist;
                    AlereDs.slheader[0].coname = ards.arcust[0].company;
                    AlereDs.slheader[0].street1 = ards.arcust[0].address1;
                    AlereDs.slheader[0].street2 = ards.arcust[0].address2;
                    AlereDs.slheader[0].city = ards.arcust[0].city;
                    AlereDs.slheader[0].state = ards.arcust[0].state;
                    AlereDs.slheader[0].country = ards.arcust[0].country;
                    AlereDs.slheader[0].created = DateTime.Now.Date;
                    AlereDs.slheader[0].ordrdate = invdate;
                    AlereDs.slheader[0].arstat = "N";
                    AlereDs.slheader[0].shipstat = "N";
                    AlereDs.slheader[0].terms = sods.somast[0].termid;
                    AlereDs.slheader[0].salesman = sods.somast[0].salesmn;
                    AlereDs.slheader[0].shiplocn = sods.somast[0].defloc;
                    AlereDs.slheader[0].shipvia = sods.somast[0].shipvia.Substring(0, 6);
                    AlereDs.slheader[0].fob = sods.somast[0].fob.Substring(0, 6);
                    AlereDs.slheader[0].duedate = invdate.AddDays((Int32)sods.somast[0].pdays);
                    AlereDs.slheader[0].taxamt = sods.somast[0].tax;
                    AlereDs.slheader[0].totalamt = sods.somast[0].ordamt;
                    // WLF 09/17/17 Put PO Number in custpono. Put SO number in misc01
                    AlereDs.slheader[0].custpono = sods.somast[0].ponum.PadRight(20).Substring(0, 20);
                    AlereDs.slheader[0].misc01 = sods.somast[0].sono;
                    AlereDs.slheader[0].acptdate = invdate;
                    AlereDs.slheader[0].docid = invno;
                    AlereDs.slheader[0].acptdate = invdate;
                    AlereDs.slheader[0].currency = "";
                    AlereDs.slheader[0].idlotsn = "N";
                    AlereDs.slheader[0].exchange = 1;
                    alereDataMethods.GenerateAlereTableRowSave(AlereDs.slheader[0], true, "");

                    //sllines
                    AlereDs.sllines.Rows.Clear();
                    AlereDs.sllines.Rows.Add();
                    EstablishBlankDataTableRow(AlereDs.sllines);
                    AlereDs.sllines[0].docid = invno;
                    AlereDs.sllines[0].doctype = "AR";
                    AlereDs.sllines[0].linenum = "   1";
                    AlereDs.sllines[0].glacct = "11000-000";
                    AlereDs.sllines[0].lineseq = "   1";
                    AlereDs.sllines[0].item = "MANUALINVOICE";
                    AlereDs.sllines[0].descrip = "Manual Invoice";
                    AlereDs.sllines[0].linetype = "S";
                    AlereDs.sllines[0].needby = invdate;
                    AlereDs.sllines[0].orderqty = 1;
                    AlereDs.sllines[0].initqty = 1;
                    AlereDs.sllines[0].shipqty = 1;
                    AlereDs.sllines[0].arqty = 1;
                    AlereDs.sllines[0].shipfrom = sods.somast[0].defloc;
                    AlereDs.sllines[0].linestat = "A";
                    AlereDs.sllines[0].unitcost = 0;
                    AlereDs.sllines[0].regprice = sods.somast[0].ordamt - sods.somast[0].tax;
                    AlereDs.sllines[0].ourprice = sods.somast[0].ordamt - sods.somast[0].tax;
                    if (sods.somast[0].tax != 0)
                    {
                        AlereDs.sllines[0].taxable = "T";
                    }
                    else
                    {
                        AlereDs.sllines[0].taxable = "F";
                    }
                    alereDataMethods.GenerateAlereTableRowSave(AlereDs.sllines[0], true, "");

                    // Accounting
                    // Journal Header
                    string journlid = AlereDs.coinfo[0].jenumber.ToString().PadLeft(10);
                    CommandString = "UPDATE coinfo SET  jenumber = jenumber +1";
                    ExecuteCommand(CommandString, CommandType.Text);
                    // Journal Entry Header
                    AlereDs.glheader.Rows.Clear();
                    AlereDs.glheader.Rows.Add();
                    EstablishBlankDataTableRow(AlereDs.glheader);
                    AlereDs.glheader[0].journlid = journlid;
                    AlereDs.glheader[0].postdate = invdate;
                    AlereDs.glheader[0].futpost = false;
                    AlereDs.glheader[0].moduleid = "SL";
                    AlereDs.glheader[0].doctype = "AR";
                    AlereDs.glheader[0].docid = invno;
                    AlereDs.glheader[0].descrip = "AR-" + invno + " - Invoicing, " + sods.somast[0].custno;
                    alereDataMethods.GenerateAlereTableRowSave(AlereDs.glheader[0], true, "");

                    string glacct = "";
                    string posttype = "";
                    decimal glamt = 0;

                    // Posting details
                    for (int x = 1; x <= 4; x++)
                    {
                        switch (x)
                        {
                            case 1:
                                {
                                    // Debit A/R
                                    glacct = ConfigurationManager.AppSettings["ARAccount"];
                                    posttype = "D";
                                    glamt = sods.somast[0].ordamt;
                                    break;
                                }
                            case 2:
                                {
                                    // Credit Sales
                                    glacct = ConfigurationManager.AppSettings["REVAccount"];
                                    posttype = "C";
                                    glamt = ((sods.somast[0].ordamt - sods.somast[0].tax) - sods.somast[0].shpamt);
                                    break;
                                }

                            case 3:
                                {
                                    // Credit Sales Tax
                                    glacct = ConfigurationManager.AppSettings["SLSTaxAccount"];
                                    posttype = "C";
                                    glamt = sods.somast[0].tax;
                                    break;
                                }
                            case 4:
                                {
                                    // Credit Shipping
                                    glacct = ConfigurationManager.AppSettings["SHIPAccount"]; ;
                                    posttype = "C";
                                    glamt = sods.somast[0].shpamt;
                                    break;
                                }
                        }

                        if (glamt == 0)
                        {
                            continue;
                        }

                        // Posting Detail

                        AlereDs.glpost.Rows.Clear();
                        AlereDs.glpost.Rows.Add();
                        EstablishBlankDataTableRow(AlereDs.glpost);
                        AlereDs.glpost[0].journlid = journlid;
                        AlereDs.glpost[0].postdate = invdate;
                        AlereDs.glpost[0].seqno = x.ToString().PadLeft(4);
                        AlereDs.glpost[0].posttype = posttype;
                        AlereDs.glpost[0].account = glacct;
                        AlereDs.glpost[0].amount = glamt;
                        alereDataMethods.InsertAlereGLPost(AlereDs.glpost[0]);

                        // Update gltotals
                        bool inserting = false;
                        AlereDs.gltotals.Rows.Clear();
                        this.ClearParameters();
                        this.AddParms("@account", glacct, "SQL");
                        this.AddParms("@fiscalyr", yearprd.Substring(0, 4), "SQL");
                        this.AddParms("@periodid", yearprd.Substring(4, 2), "SQL");
                        CommandString = "SELECT * FROM gltotals WHERE account = @account AND fiscalyr = @fiscalyr AND periodid = @periodid";
                        this.FillData(AlereDs, "gltotals", CommandString, CommandType.Text);
                        if (AlereDs.gltotals.Rows.Count < 1)
                        {
                            AlereDs.gltotals.Rows.Add();
                            EstablishBlankDataTableRow(AlereDs.gltotals);
                            AlereDs.gltotals[0].account = glacct;
                            AlereDs.gltotals[0].fiscalyr = yearprd.Substring(0, 4);
                            AlereDs.gltotals[0].periodid = yearprd.Substring(4, 2);

                            inserting = true;
                        }

                        if (posttype == "D")
                        {
                            AlereDs.gltotals[0].debit += glamt;
                        }
                        else
                        {
                            AlereDs.gltotals[0].credit += glamt;
                        }
                        if (inserting)
                        {
                            alereDataMethods.GenerateAlereTableRowSave(AlereDs.gltotals[0], true, "");
                        }
                        else
                        {
                            alereDataMethods.GenerateAlereTableRowSave(AlereDs.gltotals[0], false, "account = '" + glacct + "' AND fiscalyr = '" + yearprd.Substring(0, 4) + "' AND periodid = '" + yearprd.Substring(4, 2) + "'");
                        }

                        // Update glchart balance
                        AlereDs.glchart.Rows.Clear();
                        this.ClearParameters();
                        this.AddParms("@account", glacct, "SQL");
                        CommandString = "SELECT * FROM glchart WHERE acctno = @account";
                        this.FillData(AlereDs, "glchart", CommandString, CommandType.Text);
                        if (AlereDs.glchart.Rows.Count > 0)
                        {
                            AlereDs.glchart[0].acctbal += glamt;
                            alereDataMethods.GenerateAlereTableRowSave(AlereDs.glchart[0], false, "acctno = '" + glacct + "'");
                        }
                    } // end for loop
                    // Update slcust balances
                    AlereDs.slcust.Rows.Clear();
                    this.ClearParameters();
                    this.AddParms("@coid", sods.somast[0].custno, "SQL");
                    CommandString = "SELECT * FROM slcust WHERE coid = @coid";
                    this.FillData(AlereDs, "slcust", CommandString, CommandType.Text);
                    if (AlereDs.slcust.Rows.Count > 0)
                    {
                        AlereDs.slcust[0].balance += sods.somast[0].ordamt;
                        AlereDs.slcust[0].ytdsales += ((sods.somast[0].ordamt - sods.somast[0].tax) - sods.somast[0].shpamt);
                        AlereDs.slcust[0].lastsale = invdate;
                        alereDataMethods.GenerateAlereTableRowSave(AlereDs.slcust[0], false, "coid = '" + sods.somast[0].custno + "'");
                    }
                }
            }
            return invno;
        }
    }
}