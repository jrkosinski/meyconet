using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WSGUtilitieslib.Telemetry
{
    //TODO: file write locks
    //TODO: uploading locks 
    public class Session
    {
        public string SessionId { get; private set; }
        internal List<EventEntry> Events { get; private set; }
        public string UserId { get; set; }
        public string FileName
        {
            get { return $"{this.UserId}-{this.SessionId}.json"; }
        }

        public Session()
        {
            this.Events = new List<EventEntry>();
            this.SessionId = CreateRandomSessionId();
        }

        public async Task UploadData()
        {
            string outputFilename = $"{this.SessionId}.zip";
            try
            {
                if (System.IO.File.Exists(outputFilename))
                    System.IO.File.Delete(outputFilename);

                using (ZipArchive zip = ZipFile.Open(outputFilename, ZipArchiveMode.Create))
                {
                    //zip up the archive 
                    try
                    {
                        zip.CreateEntryFromFile(this.FileName, this.FileName);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                    }
                }

                //send the archive 
                await AzureFileStore.WriteFile("meycostore-fileshare", "telemetry", "./", outputFilename);

                //delete the file
                if (System.IO.File.Exists(outputFilename))
                    System.IO.File.Delete(outputFilename);
            }
            catch (Exception e)
            {
                Telemetry.AddErrorEvent(e.ToString());
            }
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append($"\"user\":\"{this.UserId}\", "); //TODO: UserId isn't ever set 
            sb.Append("\"events\": [");
            int index = 0;
            while (true)
            {
                sb.Append(this.Events[index].ToJson());
                index++;
                if (index >= this.Events.Count)
                    break;
                else
                    sb.Append(",");
            }
            sb.Append("]");
            sb.Append("}");

            return sb.ToString();
        }

        internal void AddEntry(EventEntry entry)
        {
            var now = System.DateTime.Now;
            entry.DateTimeString = now.ToString();
            entry.Timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            this.Events.Add(entry);

            if (entry.ControlEventType == ControlEventType.FormEvent && entry.Text.EndsWith(".Closing"))
            {
                this.WriteToFile();
            }
        }

        private string CreateRandomSessionId()
        {
            var rand = new System.Random();
            return "0x" + rand.Next(Int32.MaxValue).ToString("x").PadLeft(8, '0') + rand.Next(Int32.MaxValue).ToString("x").PadLeft(8, '0');
        }

        private void WriteToFile()
        {
            try
            {
                string filename = System.IO.Path.Combine(System.Configuration.ConfigurationManager.AppSettings["TelemetryFolderPath"], this.FileName); 
                System.IO.File.WriteAllText(filename, this.ToJson());
            }
            catch { }
        }
    }

    public enum EntryType
    {
        ControlEvent,
        SqlQuery,
        Error
    }

    public enum ControlEventType
    {
        FormEvent,
        ButtonClick
    }

    internal class EventEntry
    {
        public long Timestamp { get; set; }
        public EntryType Type { get; set; }
        public ControlEventType ControlEventType { get; set; }
        public string DateTimeString { get; set; }
        public string Text { get; set; }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            sb.Append($"\"timestamp\":{this.Timestamp},\"datetime\":\"{this.DateTimeString}\"");

            switch (this.Type)
            {
                case EntryType.ControlEvent:
                    sb.Append($",\"event\":\"{this.ControlEventType.ToString()}\"");
                    break;
                case EntryType.SqlQuery:
                    break;
                case EntryType.Error:
                    break;
            }
            sb.Append($",\"text\":\"{this.Text}\"");
            sb.Append("}");
            return sb.ToString();
        }
    }

    public static class Telemetry
    {
        private static Session session = new Session();

        public static bool TelemetryEnabled
        {
            get
            {
                string value = System.Configuration.ConfigurationManager.AppSettings["TelemetryEnabled"];
                return value != null && value.Trim().ToLower() == "true";
            }
        }

        public static string TelemetryStorageType
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TelemetryStorageType"]; }
        }

        public static void StartSession()
        {
            if (TelemetryEnabled)
                session = new Session();
        }

        public static void SetUserId(string userId)
        {
            if (session != null)
                session.UserId = userId;
        }

        public static void AddControlEvent(string controlName, string eventName, ControlEventType controleEventType)
        {
            Task.Run(() =>
            {
                try
                {
                    if (TelemetryEnabled && session != null)
                    {
                        EventEntry entry = new EventEntry();
                        entry.Type = EntryType.ControlEvent;
                        entry.ControlEventType = controleEventType;
                        entry.Text = $"{controlName}.{eventName}";

                        session.AddEntry(entry);
                    }
                }
                catch { }
            });
        }

        public static void AddErrorEvent(string errorText)
        {
            Task.Run(() =>
            {
                try
                {
                    if (TelemetryEnabled && session != null)
                    {
                        EventEntry entry = new EventEntry();
                        entry.Type = EntryType.Error;
                        entry.Text = errorText;

                        session.AddEntry(entry);
                    }
                }
                catch { }
            });
        }

        public static void AddSqlQueryEvent(string sqlQuery)
        {
            Task.Run(() =>
            {
                try
                {
                    if (TelemetryEnabled && session != null)
                    {
                        EventEntry entry = new EventEntry();
                        entry.Type = EntryType.SqlQuery;
                        entry.Text = sqlQuery;

                        session.AddEntry(entry);
                    }
                }
                catch { }
            });
        }

        public static void AddSqlCommandEvent(System.Data.IDbCommand command)
        {
            Task.Run(() =>
            {
                try
                {
                    if (TelemetryEnabled && session != null)
                    {
                        EventEntry entry = new EventEntry();
                        entry.Type = EntryType.SqlQuery;

                        entry.Text = command.CommandText;
                        foreach (var param in command.Parameters)
                        {
                            entry.Text += " " + param.ToString();
                        }

                        session.AddEntry(entry);
                    }
                }
                catch { }
            });
        }

        public static async Task StoreData()
        {
            if (TelemetryEnabled && session != null)
            {
                if (TelemetryStorageType == "Azure")
                    await session.UploadData();
            }
        }
    }

    public class Button : System.Windows.Forms.Button
    {

        public string TelemetryId
        {
            get
            {
                var parentForm = this.FindForm();
                return parentForm.Name + "." + this.Name;
            }
        }

        public Button() : base()
        {
            this.Click += PrivateClickEventHandler;
        }

        private void PrivateClickEventHandler(object sender, System.EventArgs e)
        {
            Telemetry.AddControlEvent(this.TelemetryId, "Click", ControlEventType.ButtonClick);
        }
    }

    public class Form : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Panel loadingPanel = new Panel();
        private System.Windows.Forms.PictureBox loadingSpinner = new PictureBox();
        private System.Timers.Timer timer = new System.Timers.Timer(); 

        public string TelemetryId
        {
            get { return this.Name; }
        }

        public Form() : base()
        {
            this.Load += this.PrivateLoadEvent;
            this.Shown += this.PrivateShownEvent;
            this.FormClosing += this.PrivateClosingEvent;
            this.timer.Elapsed += this.Timer_Tick;
        }

        private void PrivateLoadEvent(object sender, System.EventArgs e)
        {
            Telemetry.AddControlEvent(this.TelemetryId, "Load", ControlEventType.FormEvent);
        }

        private void PrivateShownEvent(object sender, System.EventArgs e)
        {
            Telemetry.AddControlEvent(this.TelemetryId, "Shown", ControlEventType.FormEvent);
        }

        private void PrivateClosingEvent(object sender, System.EventArgs e)
        {
            Telemetry.AddControlEvent(this.TelemetryId, "Closing", ControlEventType.FormEvent);
        }

        protected void HandleException(Exception e)
        {
            System.Windows.Forms.MessageBox.Show(e.Message);
            Telemetry.AddErrorEvent(e.ToString());
        }

        protected void SetTabOrder(System.Windows.Forms.Control[] controls, int startIndex = 0)
        {
            //this.ClearTabOrder();
            foreach (var c in controls)
            {
                c.TabIndex = startIndex++;
            }
        }

        protected virtual void SetTabOrder() { }

        protected virtual Panel LoadingPanel { get { return this.loadingPanel; } }

        protected virtual void ShowLoadingScreen(bool visible = true)
        {
            if (visible)
            {
                //this.timer.Interval = 100;
                //this.timer.Enabled = true; 
                //this.timer.Start();

                LoadingPanel.Size = this.ClientSize;
                LoadingPanel.Location = new Point(0, 0);
                Bitmap controlImage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                this.DrawToBitmap(controlImage, new Rectangle(Point.Empty, this.ClientSize));
                SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
                Graphics g = Graphics.FromImage(controlImage);
                g.FillRectangle(semiTransBrush, new Rectangle(Point.Empty, controlImage.Size));

                //this.loadingSpinner.Image = Image.FromFile("spinner.gif");
                //this.loadingSpinner.Size = new Size(300, 300);
                //LoadingPanel.Controls.Add(this.loadingSpinner); 

                LoadingPanel.BackgroundImage = controlImage;
                this.Controls.Add(loadingPanel);
                this.Controls.SetChildIndex(loadingPanel, -1);
                LoadingPanel.BringToFront();
            }

            else
            {
                //this.timer.Stop();
                //this.timer.Enabled = false;
                this.Controls.Remove(this.LoadingPanel);
            }
            this.Refresh();
        }

        protected virtual void Timer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            //this.Invoke(new Action( () => this.loadingSpinner.Draw())); 
        }
    }
}
