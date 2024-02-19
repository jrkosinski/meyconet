using CommonAppClasses;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace Ticketing
{
    public class TicketMethods : WSGDataAccess
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Reference Data");
        private customer customerds = new customer();
        private ticketds ticketDs = new ticketds();
        private ticketds ticketnoteds = new ticketds();
        public GetSoMethods getSoMethods = new GetSoMethods("SQL", "SQLConnString");
        public Contacts.ContactMethods contactMethods = new Contacts.ContactMethods("SQL", "SQLConnString");
        private FrmTicketNote frmTicketNote = new FrmTicketNote();
        private FrmTicketSelector frmTicketSelector = new FrmTicketSelector();
        private FrmTicketInformation frmTicketInformation = new FrmTicketInformation();
        private string CurrentSono = "";
        private BindingSource ticketbinding = new BindingSource();
        private string CurrentCustno = "";
        private string CurrentNotes = "";
        public Form menuForm { get; set; }
        private string CommandString = "";
        private UserInformation userInformation = new UserInformation(AppUserClass.AppUserId);
        public reference referenceds { get; set; }

        public TicketMethods(string DataStore, string AppConfigName)
            : base(DataStore, AppConfigName)
        {
            SetIdcol(ticketDs.ticket.idcolColumn);
            SetEvents();
            SetBindings();
            // userInformation.GetUserData(AppUserClass.AppUserId);
            //     userInformation
        }

        public void StartSoTicket(string sono)
        {
            CurrentSono = sono;
            CurrentNotes = "";
            if (sono.TrimEnd() == "")
            {
                getSoMethods.returnsono = "";
                getSoMethods.GetSono();
                if (!getSoMethods.wascancelled)
                {
                    if (getSoMethods.returnsono.TrimEnd() != "")
                    {
                        CurrentSono = getSoMethods.returnsono;
                        // Get sodata
                    }
                }
            }
            if (CurrentSono != "")
            {
                ticketDs.view_expandedticket.Rows.Clear();
                // Get sodata
                this.ClearParameters();
                this.AddParms("@sono", CurrentSono, "SQL");
                CommandString = "SELECT * FROM view_expandedticket WHERE sono = @sono";
                FillData(ticketDs, "view_expandedticket", CommandString, CommandType.Text);
                frmTicketSelector.MdiParent = menuForm;
                frmTicketSelector.labelTicketInformation.Text = "Tickets for SO:" + CurrentSono;
                frmTicketSelector.Show();
                FillTicketSelectorBySono(CurrentSono);
            }
        }

        public void StartCustomerTicket(string custno)
        {
            CurrentSono = "";
            CurrentCustno = custno;
            CurrentNotes = "";
            if (custno.TrimEnd() == "")
            {
                GetCustomerMethods getCust = new GetCustomerMethods("SQL", "SQLConnString");

                if (getCust.SelectedCustid != 0)
                {
                    // Get customer
                    this.ClearParameters();
                    this.AddParms("@idcol", getCust.SelectedCustid, "SQL");
                    CommandString = "SELECT * FROM arcust WHERE idcol = @idcol";
                    FillData(ticketDs, "arcust", CommandString, CommandType.Text);
                    CurrentCustno = ticketDs.arcust[0].custno;
                }
            }

            if (CurrentCustno != "")
            {
                FillTicketSelectorByCustno(CurrentCustno);
                frmTicketSelector.MdiParent = menuForm;
                frmTicketSelector.labelTicketInformation.Text = "Tickets for Custno:" + CurrentCustno;
                frmTicketSelector.Show();
            }
        }

        public void SetEvents()
        {
            // Ticket Information form
            frmTicketInformation.buttonClose.Click += frmTicketInformationbuttonClose_Click;
            frmTicketInformation.buttonContact.Click += frmTicketInformationbuttonContact_Click;
            frmTicketInformation.buttonContactDetails.Click += frmTicketInformationbuttonContactDetails_Click;

            frmTicketInformation.buttonSave.Click += frmTicketInformationbuttonSave_Click;
            frmTicketInformation.buttonAddNote.Click += frmTicketInformationbuttonAddNote_Click;
            frmTicketInformation.buttonEdit.Click += frmTicketInformationbuttonEdit_Click;
            frmTicketInformation.buttonConfirmwithemail.Click += frmTicketbuttonConfirmwithemail_Click;
            frmTicketInformation.buttonConfirmwithtext.Click += frmTicketbuttonConfirmwithtext_Click;
            frmTicketInformation.textBoxDepartment.Enter += textBoxDepartment_Enter;
            frmTicketInformation.comboBoxDepartment.SelectedIndexChanged += comboBoxDepartment_SelectedIndexChanged;
            frmTicketInformation.textBoxCommvia.Enter += textBoxCommvia_Enter;
            frmTicketInformation.comboBoxCommvia.SelectedIndexChanged += comboBoxCommvia_SelectedIndexChanged;

            // Ticket Selector form events
            frmTicketSelector.buttonAddTicket.Click += frmTicketSelectorButtonAddTicket_Click;
            frmTicketSelector.buttonClose.Click += frmTicketSelectorButtonClose_Click;
            frmTicketSelector.dataGridViewTicketSelector.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(datadataGridViewTicketSelector_CellContentDoubleClick);
            frmTicketSelector.dataGridViewTicketSelector.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(dataGridViewTicketSelector_CellFormatting);

            // Ticket Note form events
            frmTicketNote.buttonSave.Click += frmTicketNoteButtonSave_Click;
            frmTicketNote.buttonCancel.Click += frmTicketNoteButtonCancel_Click;
        }

        private void dataGridViewTicketSelector_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex <= ticketDs.view_expandedticket.Rows.Count - 1)
            {
                if (ticketDs.view_expandedticket[e.RowIndex].ticketstatus.TrimEnd() == "Open")
                {
                    frmTicketSelector.dataGridViewTicketSelector.Rows[e.RowIndex].Cells["ColumnTicketstatus"].Style.BackColor = Color.Red;
                    frmTicketSelector.dataGridViewTicketSelector.Rows[e.RowIndex].Cells["ColumnTicketstatus"].Style.SelectionBackColor = Color.DarkRed;
                }
            }
        }

        private void textBoxDepartment_Enter(object sender, EventArgs e)
        {
            frmTicketInformation.comboBoxDepartment.Visible = true;
            frmTicketInformation.comboBoxDepartment.Focus();
        }

        private void textBoxCommvia_Enter(object sender, EventArgs e)
        {
            frmTicketInformation.comboBoxCommvia.Visible = true;
            frmTicketInformation.comboBoxCommvia.Enabled = true;
            frmTicketInformation.comboBoxCommvia.Focus();
        }

        private void comboBoxCommvia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (frmTicketInformation.comboBoxCommvia.Visible == true)
            {
                ticketDs.ticket[0].commvia = frmTicketInformation.comboBoxCommvia.SelectedItem.ToString();
                ticketDs.AcceptChanges();
                frmTicketInformation.comboBoxCommvia.Visible = false;
            }
        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ticketDs.ticket.Rows.Count > 0)
            {
                ticketDs.ticket[0].department = frmTicketInformation.comboBoxDepartment.SelectedItem.ToString();
                ticketDs.AcceptChanges();
                frmTicketInformation.comboBoxDepartment.Visible = false;
            }
        }

        private void frmTicketInformationbuttonEdit_Click(object sender, EventArgs e)
        {
            RefreshfrmTicketInformationControls("Edit");
        }

        private void frmTicketbuttonConfirmwithemail_Click(object sender, EventArgs e)
        {
            SendTicketConfirmation("Email");
        }

        private void frmTicketbuttonConfirmwithtext_Click(object sender, EventArgs e)
        {
            SendTicketConfirmation("Text");
        }

        private void frmTicketInformationbuttonAddNote_Click(object sender, EventArgs e)
        {
            ticketnoteds.ticket.Rows.Clear();
            ticketnoteds.ticket.Rows.Add();
            EstablishBlankDataTableRow(ticketnoteds.ticket);
            frmTicketNote.ShowDialog();
            if (ticketnoteds.ticket[0].ticketnotes.TrimEnd().Length != 0)
            {
                CurrentNotes = ticketnoteds.ticket[0].ticketnotes;
            }
            SaveTicket();
        }

        private void datadataGridViewTicketSelector_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectedTicketId = CaptureIdCol(frmTicketSelector.dataGridViewTicketSelector);
            ProcessTicket(SelectedTicketId);
        }

        private void frmTicketInformationbuttonContactDetails_Click(object sender, EventArgs e)
        {
            if (ticketDs.ticket[0].contactid > 0)
            {
                contactMethods.ProcessContact(ticketDs.ticket[0].contactid);
                frmTicketInformation.LabelContact.Text = "Contact " + contactMethods.GetContactName(ticketDs.ticket[0].contactid).TrimEnd();
            }
            else
            {
                wsgUtilities.wsgNotice("Please select a contact");
            }
        }

        private void frmTicketInformationbuttonContact_Click(object sender, EventArgs e)
        {
            ticketDs.ticket[0].contactid = contactMethods.SelectContact(ticketDs.ticket[0].custno);
            frmTicketInformation.LabelContact.Text = "Contact " + contactMethods.GetContactName(ticketDs.ticket[0].contactid).TrimEnd();
        }

        private void frmTicketNoteButtonSave_Click(object sender, EventArgs e)
        {
            if (ticketDs.ticket[0].ticketnotes != null && ticketDs.ticket[0].ticketnotes.TrimEnd().Length > 0)
            {
                ticketDs.ticket[0].ticketnotes += ticketnoteds.ticket[0].ticketnotes + System.Environment.NewLine + DateTime.Now.ToString() + System.Environment.NewLine;
            }
            else
            {
                ticketDs.ticket[0].ticketnotes = ticketnoteds.ticket[0].ticketnotes + System.Environment.NewLine + DateTime.Now.ToString() + System.Environment.NewLine;
            }
            ticketDs.ticket.AcceptChanges();
            frmTicketNote.Close();
        }

        private void SaveTicket()
        {
            if (OkToSaveTicket())
            {
                if (frmTicketInformation.radioButtonOpen.Checked == true)
                {
                    ticketDs.ticket[0].ticketstatus = "Open";
                }
                if (frmTicketInformation.radioButtonClosed.Checked == true)
                {
                    ticketDs.ticket[0].ticketstatus = "Closed";
                }
                if (frmTicketInformation.radioButtonCancelled.Checked == true)
                {
                    ticketDs.ticket[0].ticketstatus = "Cancelled";
                }
                ticketDs.AcceptChanges();
                GenerateAppTableRowSave(ticketDs.ticket[0]);
                if (ticketDs.ticket[0].sono.TrimEnd() != "")
                {
                    RefreshSoTicket(ticketDs.ticket[0].sono);
                }
                else
                {
                    RefreshCustomerTicket(ticketDs.ticket[0].custno);
                }
                RefreshTicketStatus();
                RefreshfrmTicketInformationControls("View");
                frmTicketInformation.Update();
            }
        }

        public bool OkToSaveTicket()
        {
            bool ticketOK = true;
            if (ticketOK)
            {
                if (ticketDs.ticket[0].department.TrimEnd() == "")
                {
                    wsgUtilities.wsgNotice("Please select a department");
                    ticketOK = false;
                }
            }
            if (ticketOK)
            {
                if (ticketDs.ticket[0].commvia.TrimEnd() == "")
                {
                    wsgUtilities.wsgNotice("Please select a contact method");
                    ticketOK = false;
                }
            }

            return ticketOK;
        }

        public void RefreshTicketStatus()
        {
            switch (ticketDs.ticket[0].ticketstatus.TrimEnd())
            {
                case "Closed":
                    {
                        frmTicketInformation.radioButtonClosed.Checked = true;
                        break;
                    }
                case "Cancelled":
                    {
                        frmTicketInformation.radioButtonCancelled.Checked = true;
                        break;
                    }
                default:
                    {
                        frmTicketInformation.radioButtonOpen.Checked = true;
                        break;
                    }
            }
            frmTicketInformation.Update();
        }

        private void RefreshSoTicket(string sono)
        {
            int ticketid = ticketDs.ticket[0].idcol;
            if (ticketDs.ticket[0].idcol < 0)
            {
                ticketDs.ticket.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@sono", sono, "SQL");
                CommandString = "SELECT TOP 1 * FROM ticket WHERE sono = @sono ORDER BY idcol DESC ";
            }
            else
            {
                ticketDs.ticket.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@idcol", ticketid, "SQL");
                CommandString = "SELECT * FROM ticket WHERE idcol = @idcol ";
            }
            FillData(ticketDs, "ticket", CommandString, CommandType.Text);
            ticketDs.AcceptChanges();
        }

        private void RefreshCustomerTicket(string custno)
        {
            ticketDs.ticket.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@custno", custno, "SQL");
            CommandString = "SELECT TOP 1 * FROM ticket WHERE custno = @custno ORDER BY idcol DESC ";
            FillData(ticketDs, "ticket", CommandString, CommandType.Text);
        }

        private void frmTicketInformationbuttonSave_Click(object sender, EventArgs e)
        {
            SaveTicket();
        }

        private void frmTicketSelectorButtonAddTicket_Click(object sender, EventArgs e)
        {
            ProcessTicket(0);
        }

        private void frmTicketSelectorButtonClose_Click(object sender, EventArgs e)
        {
            frmTicketSelector.Close();
        }

        private void frmTicketNoteButtonCancel_Click(object sender, EventArgs e)
        {
            frmTicketNote.Close();
        }

        private void frmTicketInformationbuttonClose_Click(object sender, EventArgs e)
        {
            if (frmTicketInformation.buttonSave.Enabled == true)
            {
                if (wsgUtilities.wsgReply("Abandon Edit"))
                {
                    frmTicketInformation.Close();
                }
            }
            else
            {
                frmTicketInformation.Close();
            }
        }

        public void SetBindings()
        {
            // Ticket form
            string[] Departments = ConfigurationManager.AppSettings["DEPARTMENTS"].Split(new char[] { ';' });
            frmTicketInformation.comboBoxDepartment.DataSource = Departments;
            frmTicketInformation.textBoxDepartment.DataBindings.Add("text", ticketDs.ticket, "department");

            string[] Commvias = new string[] { "In Person", "Phone", "Fax", "Email", "Text", "Internal" };
            frmTicketInformation.comboBoxCommvia.DataSource = Commvias;
            frmTicketInformation.textBoxCommvia.DataBindings.Add("text", ticketDs.ticket, "commvia");

            frmTicketInformation.textBoxTicketnotes.DataBindings.Add("Text", ticketDs.ticket, "ticketnotes");
            // Ticket Note form binding
            frmTicketNote.textBoxTicketnote.DataBindings.Add("Text", ticketnoteds.ticket, "ticketnotes");

            // Ticket Selector bindings

            frmTicketSelector.dataGridViewTicketSelector.AutoGenerateColumns = false;
            frmTicketSelector.dataGridViewTicketSelector.DataSource = ticketbinding;
            ticketbinding.DataSource = ticketDs.view_expandedticket;
            frmTicketSelector.dataGridViewTicketSelector.RowsDefaultCellStyle.BackColor = Color.LightGray;
            frmTicketSelector.dataGridViewTicketSelector.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
        }

        public void LoadSomast(string sono)
        {
            ticketDs.somast.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            CommandString = "SELECT * FROM somast WHERE sono = @sono";
            FillData(ticketDs, "somast", CommandString, CommandType.Text);
        }

        public void FillTicketSelectorByCustno(string custno)
        {
            ticketDs.view_expandedticket.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@custno", custno, "SQL");
            CommandString = "SELECT ROW_NUMBER() OVER (ORDER BY idcol  ASC) AS sequno,  * FROM ( SELECT dbo.GetContactName(contactid) AS contactname, * from ticket WHERE custno = @custno AND RTRIM(sono) = '') T ORDER BY idcol";
            FillData(ticketDs, "view_expandedticket", CommandString, CommandType.Text);
        }

        public void FillTicketSelectorBySono(string sono)
        {
            ticketDs.view_expandedticket.Rows.Clear();
            this.ClearParameters();
            this.AddParms("@sono", sono, "SQL");
            CommandString = "SELECT ROW_NUMBER() OVER (ORDER BY idcol  ASC) AS sequno,  * FROM ( SELECT dbo.GetContactName(contactid) AS contactname, * from ticket WHERE sono = @sono) T ORDER BY idcol";
            FillData(ticketDs, "view_expandedticket", CommandString, CommandType.Text);
        }

        public void ProcessTicket(int ticketid)
        {
            CurrentNotes = "";
            if (ticketid == 0)
            {
                ticketDs.ticket.Rows.Clear();
                ticketDs.ticket.Rows.Add();
                EstablishBlankDataTableRow(ticketDs.ticket);

                if (CurrentSono != "")
                {
                    LoadSomast(CurrentSono);
                    ticketDs.ticket[0].sono = ticketDs.somast[0].sono;
                    ticketDs.ticket[0].custno = ticketDs.somast[0].custno;
                    ticketDs.ticket[0].ponum = ticketDs.somast[0].ponum;
                }
                else
                {
                    ticketDs.ticket[0].custno = CurrentCustno;
                }
                ticketDs.ticket[0].ticketnotes = "";
                frmTicketInformation.radioButtonOpen.Checked = true;
                RefreshfrmTicketInformationControls("Insert");
            }
            else
            {
                ticketDs.ticket.Rows.Clear();
                this.ClearParameters();
                this.AddParms("@idcol", ticketid, "SQL");
                CommandString = "SELECT * FROM ticket WHERE idcol = @idcol";
                FillData(ticketDs, "ticket", CommandString, CommandType.Text);
                RefreshTicketStatus();
                RefreshfrmTicketInformationControls("View");
            }
            //      frmTicketInformation.MdiParent = menuForm;
            frmTicketInformation.labelCustno.Text = "Customer: " + ticketDs.ticket[0].custno;
            frmTicketInformation.labelSono.Text = "SO: " + ticketDs.ticket[0].sono;
            frmTicketInformation.labelPonum.Text = "PO: " + ticketDs.ticket[0].ponum.TrimEnd();
            frmTicketInformation.LabelContact.Text = "Contact " + contactMethods.GetContactName(ticketDs.ticket[0].contactid).TrimEnd();
            frmTicketInformation.checkBoxConfirmContent.Checked = true;
            frmTicketInformation.ShowDialog();
            if (CurrentSono != "")
            {
                FillTicketSelectorBySono(CurrentSono);
            }
            else
            {
                FillTicketSelectorByCustno(CurrentCustno);
            }
        }

        public void SendTicketConfirmation(string ConfirmationType)
        {
            string NotesToSend = "";
            if (frmTicketInformation.checkBoxConfirmContent.Checked)
            {
                NotesToSend = CurrentNotes;
            }
            else
            {
                NotesToSend = ticketDs.ticket[0].ticketnotes;
            }

            bool mailOK = true;
            string[] CellCarriers = ConfigurationManager.AppSettings["CELLCARRIERS"].Split(new char[] { ';' });
            string textaddress = "";
            string carrier = "";
            EmailMethods emailMethods = new EmailMethods();
            MailMessage TicketMessage = new MailMessage();
            string Subject = "Meyco Conversation Recap";
            string Body = "";
            // Add recipients
            ticketDs.contact.Rows.Clear();
            CommandString = "SELECT * FROM contact WHERE  idcol = @idcol";
            ClearParameters();
            AddParms("@idcol", ticketDs.ticket[0].contactid, "SQL");
            FillData(ticketDs, "contact", CommandString, CommandType.Text);
            if (ticketDs.contact.Rows.Count > 0)
            {
                if (ConfirmationType == "Email")
                {
                    TicketMessage.To.Add(new MailAddress(ticketDs.contact[0].contactemail.TrimEnd()));

                    Body = "Thank you for contacting Meyco pool covers. Below is a recap of the correspondence " + userInformation.username.TrimEnd();
                    Body += " just had with " + ticketDs.contact[0].contactname.TrimEnd() + " via " + ticketDs.ticket[0].commvia.TrimEnd().ToLower();
                    if (ticketDs.ticket[0].sono.TrimEnd() != "")
                    {
                        Body += " regarding the order for SO# " + ticketDs.ticket[0].sono.TrimStart().TrimEnd() + "; PO# " + ticketDs.ticket[0].ponum.TrimStart().TrimEnd();
                    }
                    Body += ". This is for informational purposes only, please review for accuracy: " + NotesToSend.ToUpper();
                }
                else
                {
                    if (ticketDs.ticket[0].sono.TrimEnd() != "")
                    {
                        Body = "SO# " + ticketDs.ticket[0].sono.TrimStart().TrimEnd() + "; PO# " + ticketDs.ticket[0].ponum.TrimStart().TrimEnd() + "  ";
                    }

                    Body += NotesToSend.ToUpper();

                    textaddress = ticketDs.contact[0].contactphone.TrimEnd().Replace("/", "");
                    textaddress = textaddress.Replace("-", "");
                    // Locate the proper email suffix

                    for (int i = 0; i < CellCarriers.Length; i++)
                    {
                        if (CellCarriers[i].IndexOf("@") > 0)
                        {
                            carrier = CellCarriers[i].Substring(0, CellCarriers[i].IndexOf("@"));
                            if (carrier.TrimEnd() == ticketDs.contact[0].contactcarrier.TrimEnd())
                            {
                                string[] suffixes = CellCarriers[i].Split('@');
                                textaddress += "@" + suffixes[1];
                            }
                        }
                    }
                    TicketMessage.To.Add(new MailAddress(textaddress));
                }
            }
            else
            {
                wsgUtilities.wsgNotice("Invalid Contact Information");
                mailOK = false;
            }
            if (mailOK)
            {
                string confirmvia = "";
                if (ConfirmationType == "Email")
                {
                    confirmvia = "Email";
                }
                else
                {
                    confirmvia = "Text";
                }
                emailMethods.SendEmail(TicketMessage, Subject, Body, userInformation.emailaddress.TrimEnd());
                wsgUtilities.wsgNotice("Message Sent");
                ticketDs.ticket[0].ticketnotes += System.Environment.NewLine + "Confirmation Sent Via " + confirmvia + " " + DateTime.Now.ToString() + System.Environment.NewLine;
                ticketDs.AcceptChanges();
                SaveTicket();
                RefreshfrmTicketInformationControls("View");
            }
            else
            {
                wsgUtilities.wsgNotice("Message not Sent. Check errors.");
            }
        }

        public void RefreshfrmTicketInformationControls(string CurrentState)
        {
            DisableControls(frmTicketInformation);
            frmTicketInformation.buttonClose.Enabled = true;
            frmTicketInformation.comboBoxDepartment.Visible = false;
            frmTicketInformation.comboBoxCommvia.Visible = false;

            switch (CurrentState)
            {
                case "Edit":
                    {
                        frmTicketInformation.buttonSave.Enabled = true;
                        frmTicketInformation.buttonContact.Enabled = true;
                        frmTicketInformation.buttonContactDetails.Enabled = true;
                        frmTicketInformation.comboBoxDepartment.Enabled = true;
                        frmTicketInformation.checkBoxConfirmContent.Enabled = true;
                        frmTicketInformation.buttonAddNote.Enabled = true;
                        frmTicketInformation.groupBoxTicketStatus.Enabled = true;
                        frmTicketInformation.radioButtonOpen.Enabled = true;
                        frmTicketInformation.radioButtonClosed.Enabled = true;
                        frmTicketInformation.radioButtonCancelled.Enabled = true;
                        frmTicketInformation.buttonConfirmwithemail.Enabled = true;
                        frmTicketInformation.buttonConfirmwithtext.Enabled = true;
                        frmTicketInformation.textBoxDepartment.Enabled = true;
                        frmTicketInformation.textBoxTicketnotes.Enabled = true;
                        frmTicketInformation.textBoxCommvia.Enabled = true;
                        break;
                    }

                case "Insert":
                    {
                        frmTicketInformation.buttonSave.Enabled = true;
                        frmTicketInformation.buttonContact.Enabled = true;
                        frmTicketInformation.buttonContactDetails.Enabled = true;
                        frmTicketInformation.comboBoxDepartment.Enabled = true;
                        frmTicketInformation.checkBoxConfirmContent.Enabled = true;
                        frmTicketInformation.buttonAddNote.Enabled = true;
                        frmTicketInformation.buttonConfirmwithemail.Enabled = true;
                        frmTicketInformation.groupBoxTicketStatus.Enabled = true;
                        frmTicketInformation.radioButtonOpen.Enabled = true;
                        frmTicketInformation.radioButtonClosed.Enabled = true;
                        frmTicketInformation.radioButtonCancelled.Enabled = true;
                        frmTicketInformation.buttonConfirmwithtext.Enabled = true;
                        frmTicketInformation.textBoxDepartment.Enabled = true;
                        frmTicketInformation.textBoxCommvia.Enabled = true;
                        break;
                    }
                case "View":
                    {
                        frmTicketInformation.buttonEdit.Enabled = true;
                        frmTicketInformation.buttonConfirmwithemail.Enabled = true;
                        frmTicketInformation.buttonConfirmwithtext.Enabled = true;
                        break;
                    }
            }
        }

        public void DisableControls(Form form)
        {
            // Loop thru all the controls on each tab page and disable text boxes and  buttons
            foreach (Control c in form.Controls)
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
        }
    }
}