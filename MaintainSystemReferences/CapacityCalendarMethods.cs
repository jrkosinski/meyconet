using CommonAppClasses;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WSGUtilitieslib;

namespace MaintainSystemReferences
{
    #region Numeric TextBox

    public class NumericTextBox : TextBox
    {
        private bool allowSpace = false;

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
             keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            //    {
            //     // Let the edit control handle control and alt key combinations
            //    }
            else if (this.allowSpace && e.KeyChar == ' ')
            {
            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text);
            }
        }

        public decimal DecimalValue
        {
            get
            {
                return Decimal.Parse(this.Text);
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }
    }

    #endregion Numeric TextBox

    public class CapacityCalendarMethods : WSGDataAccess
    {
        private WSGUtilities wsgUtilities = new WSGUtilities("Capacity Planning");
        public Form menuForm { get; set; }
        public Form parentForm = new Form();
        public Button ButtonSave = new Button();
        public Button ButtonClose = new Button();
        public ComboBox ComboBoxMonth = new ComboBox();
        public ComboBox ComboBoxYear = new ComboBox();
        public string CurrentState { get; set; }
        private reference refds = new reference();
        public int CalendarYear = 0;
        public int CalendarMonth = 0;
        public int CalendarCellWidth = 0;
        public int CalendarCellHeight = 0;
        public int TbCapacityWidth = 0;
        public int TbbufferWidth = 0;
        private string[] DaysOfWeek = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday    ", "Thursday", "    Friday", "Saturday" };
        private int[] MonthProductionCapacity = new int[12] { 20, 20, 20, 20, 20, 35, 35, 45, 85, 65, 60, 45 };
        private int[] MonthProductionExtraStockCapacity = new int[12] { 10, 10, 10, 10, 10, 10, 10, 10, 7, 7, 10, 10 };
        private int[] MonthCustomBuffer = new int[12] { 7, 7, 7, 7, 7, 5, 5, 4, 5, 5, 5, 5 };
        private int[] MonthStockBuffer = new int[12] { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 3, 3 };

        private Label[] DaysOfWeekLb = new Label[7];
        private NumericTextBox[] ArrayCapacityTB = new NumericTextBox[31];
        private NumericTextBox[] ArrayExtraStockCapacityTB = new NumericTextBox[31];
        private NumericTextBox[] ArrayCustomBufferTB = new NumericTextBox[31];
        private NumericTextBox[] ArrayStockBufferTB = new NumericTextBox[31];
        private NumericTextBox[] ArrayBurdenUnitsTB = new NumericTextBox[31];
        private NumericTextBox[] ArrayBurdenCountTB = new NumericTextBox[31];
        private reference[] ArrayRefds = new reference[31];
        private reference[] ArrayBurden = new reference[31];
        private reference[] ArrayBurdenCounts = new reference[31];
        private Label[] ArrayMonthDay = new Label[31];
        public int ButtonHeight = 30;
        public int ButtonWidth = 75;
        public TextBox TBCapacityLegend = new TextBox();
        public TextBox TBExtraStockCapacityLegend = new TextBox();
        public TextBox TBCustomBufferLegend = new TextBox();
        public TextBox TBStockBufferLegend = new TextBox();
        public TextBox TBUsedCapacityLegend = new TextBox();
        public TextBox TBOrderCountLegend = new TextBox();
        public int ButtonTop = 10;
        private MiscellaneousDataMethods miscdatamethods = new MiscellaneousDataMethods("SQL", "SQLConnString");
        public int ButtonLeft = 50;
        public int FirstDayNumber = 0;
        public int CurrentRowTop = 0;
        public int CurrentCellLeft = 0;
        public int MonthDayLabelWidth = 0;
        public DateTime FirstDay { get; set; }

        //= new DateTime(2012, 8, 1);
        public int DaysInMonth = 0;

        public CapacityCalendarMethods(string DataStore, string AppConfigName)
          : base(DataStore, AppConfigName)
        {
            SetIdcol(refds.capacitycalendar.idcolColumn);
            CurrentState = "Select";
            //   RefreshControls();
        }

        public void StartApp()
        {
            FirstDay = DateTime.Now.Date;
            CalendarMonth = DateTime.Now.Month;
            CalendarYear = DateTime.Now.Year;
            SetComboBoxes();
            SetControls();
            SetEvents();
            ShowParent();
        }

        public void SetEvents()
        {
            ButtonClose.Click += new System.EventHandler(CloseparentForm);
            ButtonSave.Click += new System.EventHandler(SaveData);
            ComboBoxMonth.SelectedIndexChanged += new System.EventHandler(SetCalendarMonth);
            ComboBoxYear.SelectedIndexChanged += new System.EventHandler(SetCalendarYear);
        }

        public void SetControls()
        {
            // Remove all controls from the form
            parentForm.Controls.Clear();
            parentForm.Controls.Add(ComboBoxMonth);
            parentForm.Controls.Add(ComboBoxYear);
            FirstDay = new DateTime(CalendarYear, CalendarMonth, 1);
            DaysInMonth = GetDaysInMonth(CalendarYear, CalendarMonth);
            ButtonLeft = 50;
            // Establish Dataset Arrays
            FillDatasets();
            // Establish appearance constants
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            parentForm.Text = "Capacity Schedule " + String.Format("{0:MMMM}", FirstDay) + " " + String.Format("{0:yyyy}", FirstDay);
            parentForm.Height = 975;
            parentForm.Width = 1000;
            MonthDayLabelWidth = 25;

            // Buttons
            SetButton(ButtonSave, "Save", ButtonHeight, ButtonWidth, ButtonTop, ButtonLeft);
            ButtonLeft += ButtonWidth + 5;
            SetButton(ButtonClose, "Close", ButtonHeight, ButtonWidth, ButtonTop, ButtonLeft);
            CurrentRowTop = 40;
            TbCapacityWidth = 150;
            CalendarCellWidth = TbCapacityWidth + 10;
            CalendarCellHeight = 125;
            FirstDayNumber = (int)FirstDay.Date.DayOfWeek;
            SetDayOfWeekLabels();
            SetDayLabels();
            // Set textboxes
            SetCapacityTextBoxes();
            SetExtraStockCapacityTextBoxes();
            SetCustomBufferTextBoxes();
            SetStockBufferTextBoxes();
            SetBurdenTextBoxes();
            SetOrderCountTextBoxes();
            SetLegendTextBoxes();
        }

        // Custom Events
        public void TextboxClick(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int boxnumber = Convert.ToInt32(tb.Name.Substring(tb.Name.Length - 2, 2));
            string Outputmessage = String.Format("{0:MMMM}", FirstDay) + " " +
            (boxnumber + 1).ToString() + ", " + String.Format("{0:yyyy}", FirstDay) + Environment.NewLine;
            Outputmessage += "Capacity: " + ArrayRefds[boxnumber].capacitycalendar[0].capacity.ToString() + Environment.NewLine;
            Outputmessage += "Extra Stock Capacity: " + ArrayRefds[boxnumber].capacitycalendar[0].extrastockcapacity.ToString() + Environment.NewLine;
            Outputmessage += "Custom Buffer: " + ArrayRefds[boxnumber].capacitycalendar[0].custombuffer.ToString() + Environment.NewLine;
            Outputmessage += "Stock Buffer: " + ArrayRefds[boxnumber].capacitycalendar[0].stockbuffer.ToString() + Environment.NewLine;
            Outputmessage += "Used Production Units: " + ArrayBurden[boxnumber].view_shipdateprodunits[0].produnits.ToString() + Environment.NewLine;
            Outputmessage += "Number of orders: " + ArrayBurden[boxnumber].view_shipdateprodunits[0].socount.ToString() + Environment.NewLine;
            wsgUtilities.wsgNotice(Outputmessage);
        }

        public void CloseparentForm(object sender, EventArgs e)
        {
            parentForm.Close();
        }

        public void SetCalendarMonth(object sender, EventArgs e)
        {
            CalendarMonth = ComboBoxMonth.SelectedIndex + 1;
            SetControls();
        }

        public void SetCalendarYear(object sender, EventArgs e)
        {
            CalendarYear = Convert.ToInt32(ComboBoxYear.SelectedItem.ToString());
            SetControls();
        }

        public void SaveData(object sender, EventArgs e)
        {
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                GenerateAppTableRowSave(ArrayRefds[i].capacitycalendar[0]);
            }
        }

        public void SetComboBoxes()
        {
            // ComboBoxes
            ComboBoxYear.Left = ButtonLeft + (ButtonWidth * 2) + 15;
            ComboBoxYear.Top = ButtonTop;
            ComboBoxYear.Width = 50;
            ComboBoxYear.Height = 25;
            ComboBoxYear.Items.Add(FirstDay.AddMonths(-36).Year.ToString());
            ComboBoxYear.Items.Add(FirstDay.AddMonths(-24).Year.ToString());
            ComboBoxYear.Items.Add(FirstDay.AddMonths(-12).Year.ToString());
            ComboBoxYear.Items.Add(FirstDay.Year.ToString());
            ComboBoxYear.Items.Add(FirstDay.AddMonths(12).Year.ToString());
            ComboBoxYear.Items.Add(FirstDay.AddMonths(24).Year.ToString());
            ComboBoxYear.Items.Add(FirstDay.AddMonths(36).Year.ToString());
            ComboBoxYear.SelectedIndex = 3;
            ComboBoxMonth.Left = ComboBoxYear.Left + ComboBoxYear.Width + 5;
            ComboBoxMonth.Top = ComboBoxYear.Top;
            ComboBoxMonth.Width = 75;
            ComboBoxMonth.Items.Add("January");
            ComboBoxMonth.Items.Add("February");
            ComboBoxMonth.Items.Add("March");
            ComboBoxMonth.Items.Add("April");
            ComboBoxMonth.Items.Add("May");
            ComboBoxMonth.Items.Add("June");
            ComboBoxMonth.Items.Add("July");
            ComboBoxMonth.Items.Add("August");
            ComboBoxMonth.Items.Add("September");
            ComboBoxMonth.Items.Add("October");
            ComboBoxMonth.Items.Add("November");
            ComboBoxMonth.Items.Add("December");
            ComboBoxMonth.SelectedItem = String.Format("{0:MMMM}", FirstDay).TrimEnd();
        }

        public void ShowParent()
        {
            parentForm.MdiParent = menuForm;
            parentForm.Show();
        }

        public void SetDayOfWeekLabels()
        {
            CurrentCellLeft = 25;
            for (int i = 0; i <= 6; i++)
            {
                DaysOfWeekLb[i] = new Label();
                DaysOfWeekLb[i].AutoSize = true;
                DaysOfWeekLb[i].Text = DaysOfWeek[i];
                DaysOfWeekLb[i].TextAlign = ContentAlignment.MiddleRight;
                DaysOfWeekLb[i].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                DaysOfWeekLb[i].Left = CurrentCellLeft;
                DaysOfWeekLb[i].Top = CurrentRowTop;
                parentForm.Controls.Add(DaysOfWeekLb[i]);
                CurrentCellLeft += CalendarCellWidth;
            }
            CurrentRowTop += 35;
        }

        public void SetDayLabels()
        {
            CurrentCellLeft = 25 + (FirstDayNumber * CalendarCellWidth);
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                ArrayMonthDay[i] = new Label();
                ArrayMonthDay[i].Text = (i + 1).ToString().TrimStart().TrimEnd();
                ArrayMonthDay[i].Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                ArrayMonthDay[i].Top = CurrentRowTop;
                ArrayMonthDay[i].Left = CurrentCellLeft + (CalendarCellWidth / 3);
                ArrayMonthDay[i].AutoSize = true;
                parentForm.Controls.Add(ArrayMonthDay[i]);
                // Move position to the right, and check for overflow of the week
                CurrentCellLeft += CalendarCellWidth;
                if (CurrentCellLeft > 25 + (6 * CalendarCellWidth))
                {
                    CurrentCellLeft = 25;
                    CurrentRowTop += CalendarCellHeight;
                }
            }
        }

        public void FillDatasets()
        {
            //Order totals
            refds.view_shipdateprodunits.Rows.Clear();
            string commandtext = "SELECT * FROM view_shipdateprodunits WHERE YEAR(ordate) = @year ";
            commandtext += "AND MONTH(ordate) = @month ORDER BY ordate";
            ClearParameters();
            this.AddParms("@year", CalendarYear, "SQL");
            this.AddParms("@month", CalendarMonth, "SQL");
            FillData(refds, "view_shipdateprodunits", commandtext, CommandType.Text);
            // Establish blank units for each day of the month
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                ArrayBurden[i] = new reference();
                EstablishBlankDataTableRow(ArrayBurden[i].view_shipdateprodunits);
            }

            // Populate burden and so count
            if (refds.view_shipdateprodunits.Rows.Count > 0)
            {
                for (int i = 0; i <= refds.view_shipdateprodunits.Rows.Count - 1; i++)
                {
                    ArrayBurden[refds.view_shipdateprodunits[i].ordate.Day - 1].view_shipdateprodunits[0].produnits =
                    refds.view_shipdateprodunits[i].produnits;
                    ArrayBurden[refds.view_shipdateprodunits[i].ordate.Day - 1].view_shipdateprodunits[0].socount =
                    refds.view_shipdateprodunits[i].socount;
                }
            }
            // Capacity Calendar
            refds.capacitycalendar.Rows.Clear();
            commandtext = "SELECT * FROM capacitycalendar WHERE YEAR(proddate) = @year ";
            commandtext += "AND MONTH(proddate) = @month ORDER BY proddate";
            ClearParameters();
            this.AddParms("@year", CalendarYear, "SQL");
            this.AddParms("@month", CalendarMonth, "SQL");
            FillData(refds, "capacitycalendar", commandtext, CommandType.Text);
            if (refds.capacitycalendar.Rows.Count > 0)
            {
                for (int i = 0; i <= DaysInMonth - 1; i++)
                {
                    ArrayRefds[i] = new reference();
                    ArrayRefds[i].capacitycalendar.ImportRow(refds.capacitycalendar.Rows[i]);
                }
            }
            else
            {
                for (int i = 0; i <= DaysInMonth - 1; i++)
                {
                    ArrayRefds[i] = new reference();
                    SetIdcol(ArrayRefds[i].capacitycalendar.idcolColumn);
                    EstablishBlankDataTableRow(ArrayRefds[i].capacitycalendar);
                    ArrayRefds[i].capacitycalendar[0].proddate = FirstDay.AddDays(i);
                    // Skip all Sundays
                    if ((int)FirstDay.Date.AddDays(i).DayOfWeek == 0)
                    {
                        continue;
                    }
                    // Skip all Saturdays
                    if ((int)FirstDay.Date.AddDays(i).DayOfWeek == 6)
                    {
                        continue;
                    }
                    // Skip all Fridays - Jan - May
                    if ((int)FirstDay.Date.AddDays(i).DayOfWeek == 5 && CalendarMonth == 1)
                    {
                        continue;
                    }
                    if ((int)FirstDay.Date.AddDays(i).DayOfWeek == 5 && CalendarMonth == 2)
                    {
                        continue;
                    }
                    if ((int)FirstDay.Date.AddDays(i).DayOfWeek == 5 && CalendarMonth == 3)
                    {
                        continue;
                    }
                    if ((int)FirstDay.Date.AddDays(i).DayOfWeek == 5 && CalendarMonth == 4)
                    {
                        continue;
                    }
                    if ((int)FirstDay.Date.AddDays(i).DayOfWeek == 5 && CalendarMonth == 5)
                    {
                        continue;
                    }
                    ArrayRefds[i].capacitycalendar[0].capacity = MonthProductionCapacity[CalendarMonth - 1];
                    ArrayRefds[i].capacitycalendar[0].extrastockcapacity = MonthProductionExtraStockCapacity[CalendarMonth - 1];
                    ArrayRefds[i].capacitycalendar[0].custombuffer = MonthCustomBuffer[CalendarMonth - 1];
                    ArrayRefds[i].capacitycalendar[0].stockbuffer = MonthStockBuffer[CalendarMonth - 1];
                }
            }
        }

        private void DrawRectangle(int left, int top, int right, int height)
        {
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics = parentForm.CreateGraphics();
            formGraphics.DrawRectangle(myPen, new Rectangle(left, top, right, height));
            myPen.Dispose();
            formGraphics.Dispose();
        }

        public void SetCapacityTextBoxes()
        {
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                string boxnumber = "";
                if (i < 9)
                {
                    boxnumber = "0" + i.ToString();
                }
                else
                {
                    boxnumber = i.ToString();
                }

                ArrayCapacityTB[i] = new NumericTextBox();
                ArrayCapacityTB[i].Name = "TBCapacity" + boxnumber.TrimEnd().TrimStart();
                ArrayCapacityTB[i].Click += new System.EventHandler(TextboxClick);
                SetTextBox(ArrayCapacityTB[i], ArrayMonthDay[i].Left - 40, ArrayMonthDay[i].Top + 25, 25);
                ArrayCapacityTB[i].DataBindings.Add("Text", ArrayRefds[i], "capacitycalendar.capacity");
            }
        }

        public void SetExtraStockCapacityTextBoxes()
        {
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                string boxnumber = "";
                if (i < 9)
                {
                    boxnumber = "0" + i.ToString();
                }
                else
                {
                    boxnumber = i.ToString();
                }

                ArrayExtraStockCapacityTB[i] = new NumericTextBox();
                ArrayExtraStockCapacityTB[i].Name = "TBCapacity" + boxnumber.TrimEnd().TrimStart();
                ArrayExtraStockCapacityTB[i].Click += new System.EventHandler(TextboxClick);
                SetTextBox(ArrayExtraStockCapacityTB[i], ArrayMonthDay[i].Left - 10, ArrayMonthDay[i].Top + 25, 25);
                ArrayExtraStockCapacityTB[i].DataBindings.Add("Text", ArrayRefds[i], "capacitycalendar.extrastockcapacity");
            }
        }

        public void SetCustomBufferTextBoxes()
        {
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                string boxnumber = "";
                if (i < 9)
                {
                    boxnumber = "0" + i.ToString();
                }
                else
                {
                    boxnumber = i.ToString();
                }

                ArrayCustomBufferTB[i] = new NumericTextBox();
                // Establish TB name and click event
                ArrayCustomBufferTB[i].Name = "TBCustomBuffer" + boxnumber.TrimEnd().TrimStart();
                ArrayCustomBufferTB[i].Click += new System.EventHandler(TextboxClick);
                SetTextBox(ArrayCustomBufferTB[i], ArrayMonthDay[i].Left - 40, ArrayMonthDay[i].Top + 50, 25);
                ArrayCustomBufferTB[i].DataBindings.Add("Text", ArrayRefds[i], "capacitycalendar.custombuffer");
            }
        }

        public void SetStockBufferTextBoxes()
        {
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                string boxnumber = "";
                if (i < 9)
                {
                    boxnumber = "0" + i.ToString();
                }
                else
                {
                    boxnumber = i.ToString();
                }

                ArrayStockBufferTB[i] = new NumericTextBox();
                // Establish TB name and click event
                ArrayStockBufferTB[i].Name = "TBStockBuffer" + boxnumber.TrimEnd().TrimStart();
                ArrayStockBufferTB[i].Click += new System.EventHandler(TextboxClick);
                SetTextBox(ArrayStockBufferTB[i], ArrayMonthDay[i].Left - 10, ArrayMonthDay[i].Top + 50, 25);
                ArrayStockBufferTB[i].DataBindings.Add("Text", ArrayRefds[i], "capacitycalendar.stockbuffer");
            }
        }

        public void SetBurdenTextBoxes()
        {
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                string boxnumber = "";
                if (i < 9)
                {
                    boxnumber = "0" + i.ToString();
                }
                else
                {
                    boxnumber = i.ToString();
                }
                ArrayBurdenUnitsTB[i] = new NumericTextBox();
                ArrayBurdenUnitsTB[i].Name = "TBBurden" + boxnumber.TrimEnd().TrimStart();
                ArrayBurdenUnitsTB[i].Click += new System.EventHandler(TextboxClick);
                SetTextBox(ArrayBurdenUnitsTB[i], ArrayMonthDay[i].Left - 40, ArrayMonthDay[i].Top + 75, 25);
                ArrayBurdenUnitsTB[i].DataBindings.Add("Text", ArrayBurden[i], "view_shipdateprodunits.produnits");
            }
        }

        public void SetOrderCountTextBoxes()
        {
            for (int i = 0; i <= DaysInMonth - 1; i++)
            {
                string boxnumber = "";
                if (i < 9)
                {
                    boxnumber = "0" + i.ToString();
                }
                else
                {
                    boxnumber = i.ToString();
                }
                ArrayBurdenCountTB[i] = new NumericTextBox();
                ArrayBurdenCountTB[i].Name = "TBBurdenCount" + boxnumber.TrimEnd().TrimStart();
                ArrayBurdenCountTB[i].Click += new System.EventHandler(TextboxClick);
                SetTextBox(ArrayBurdenCountTB[i], ArrayMonthDay[i].Left - 10, ArrayMonthDay[i].Top + 75, 25);
                ArrayBurdenCountTB[i].DataBindings.Add("Text", ArrayBurden[i], "view_shipdateprodunits.socount");
            }
        }

        public void SetLegendTextBoxes()
        {
            int LegendLeft = ButtonLeft - 100;
            int LegendTop = parentForm.Height - 150;

            Label LabelLegend = new Label();
            LabelLegend.Text = "Legend";
            LabelLegend.AutoSize = true;
            LabelLegend.Top = LegendTop - 20;
            LabelLegend.Left = LegendLeft + 60;
            LabelLegend.Font = new Font(LabelLegend.Font, FontStyle.Bold);
            parentForm.Controls.Add(LabelLegend);

            TBCapacityLegend.Top = LegendTop;
            TBCapacityLegend.Left = LegendLeft;
            TBCapacityLegend.Width = 75;
            TBCapacityLegend.Text = "Capacity";
            parentForm.Controls.Add(TBCapacityLegend);
            TBExtraStockCapacityLegend.Top = LegendTop;
            TBExtraStockCapacityLegend.Left = LegendLeft + 85;
            TBExtraStockCapacityLegend.Width = 125;
            TBExtraStockCapacityLegend.Text = "Extra Stock Capacity";
            parentForm.Controls.Add(TBExtraStockCapacityLegend);

            TBCustomBufferLegend.Top = LegendTop + 30;
            TBCustomBufferLegend.Left = LegendLeft;
            TBCustomBufferLegend.Width = 75;
            TBCustomBufferLegend.Text = "Custom Buffer";
            parentForm.Controls.Add(TBCustomBufferLegend);
            TBStockBufferLegend.Top = LegendTop + 30;
            TBStockBufferLegend.Left = LegendLeft + 85;
            TBStockBufferLegend.Width = 125;
            TBStockBufferLegend.Text = "Stock Buffer";
            parentForm.Controls.Add(TBStockBufferLegend);

            TBUsedCapacityLegend.Top = LegendTop + 60;
            TBUsedCapacityLegend.Left = LegendLeft;
            TBUsedCapacityLegend.Width = 75;
            TBUsedCapacityLegend.Text = "Used Capacity";
            parentForm.Controls.Add(TBUsedCapacityLegend);
            TBOrderCountLegend.Top = LegendTop + 60;
            TBOrderCountLegend.Left = LegendLeft + 85;
            TBOrderCountLegend.Width = 125;
            TBOrderCountLegend.Text = "Order Count";
            parentForm.Controls.Add(TBOrderCountLegend);
        }

        public void SetButton(Button ButtonTarget, string Text, int Height, int Width, int Top, int Left)
        {
            ButtonTarget.Text = Text.TrimEnd();
            ButtonTarget.Height = Height;
            ButtonTarget.Width = Width;
            ButtonTarget.Top = Top;
            ButtonTarget.Left = Left;
            parentForm.Controls.Add(ButtonTarget);
        }

        public void SetTextBox(NumericTextBox TBox, int TLeft, int TTop, int TWidth)
        {
            // Set the position, size and the databindings. Add to the form
            TBox.TextAlign = HorizontalAlignment.Right;
            TBox.ReadOnly = false;
            TBox.Left = TLeft;
            TBox.Text = "0";
            TBox.Top = TTop;
            TBox.Width = TWidth;
            parentForm.Controls.Add(TBox);
        }

        public int GetDaysInMonth(int year, int monthno)
        {
            bool IsLeap = false;
            IsLeap = (((year % 4) == 0) && ((year % 100) != 0) || ((year % 400) == 0));

            int numdays = 0;
            switch (monthno)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    numdays = 31;
                    break;

                case 4:
                case 6:
                case 9:
                case 11:
                    numdays = 30;
                    break;

                case 2:
                    if (IsLeap)
                    {
                        numdays = 29;
                    }
                    else
                    {
                        numdays = 28;
                    }
                    break;
            }
            return numdays;
        }
    }

    public class BurdenInformation
    {
    }
}