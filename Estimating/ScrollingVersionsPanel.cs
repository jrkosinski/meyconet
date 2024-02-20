using CommonAppClasses;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using WSGUtilitieslib;

namespace Estimating
{
    public class ScrollingVersionsPanel : ScrollingPanel
    {
        Dictionary<string, int> _versionsLookup = new Dictionary<string, int>();
        List<VersionPanel> _versionPanels = new List<VersionPanel>();

        public ScrollingVersionsPanel(Form parentForm, Panel panel) : base(parentForm, panel)
        { }

        public void AddVersion(string version, List<quoterpt.view_soreportlinedataRow> covers)
        {
            VersionPanel subPanel = new VersionPanel((FrmSOHead)this._parentForm, new VersionDto(version, covers));

            this.AddSubPanel(subPanel.Panel);
            this._versionPanels.Add(subPanel);
            this._versionsLookup.Add(version, this._versionPanels.Count - 1);
        }

        public void UpdateVersionCover(string version, string cover)
        {
            int index = this._versionsLookup.ContainsKey(version) ? this._versionsLookup[version] : -1;
            if (index >= 0)
            {
                this._versionPanels[index].UpdateUI(
                    ((FrmSOHead)this._parentForm).Soinf.clineds.socover[0].idcol,
                    ((FrmSOHead)this._parentForm).Soinf.clineds.socover[0].descrip,
                    ((FrmSOHead)this._parentForm).Soinf.clineds.socover[0].colorid,
                    ((FrmSOHead)this._parentForm).Soinf.clineds.socover[0].materialid
                );
            }
        }

        public override void Clear()
        {
            base.Clear();
            this._versionsLookup.Clear();
            this._versionPanels.Clear();
        }

        public void Enable(bool enabled = true)
        {
            for (int n = 0; n < this._versionPanels.Count; n++)
            {
                this._versionPanels[n].Enable(enabled);
            }
        }

        private class VersionPanel
        {
            public VersionDto Version { get; private set; }
            public Panel Panel { get; private set; }
            public Label NameLabel { get; private set; }
            public Label DescLabel { get; private set; }
            public Button SelectButton { get; private set; }
            public Button DeleteButton { get; private set; }
            public Button SaveButton { get; private set; }
            public Button CancelButton { get; private set; }
            public ComboBox ColorDropdown { get; private set; }
            public ComboBox MaterialDropdown { get; private set; }
            public bool IsDirty { 
                get  {
                    if (this.SelectedColor != null && this.SelectedMaterial != null)
                    {
                        return this.SelectedColor.Display != this.Version.Covers[0].Color.Trim() ||
                            this.SelectedMaterial.Display != this.Version.Covers[0].Material.Trim();
                    }
                    return false;
                }
            }
            protected DropdownItem SelectedColor
            {
                get { 
                    try
                    {
                        return this.ColorDropdown.SelectedItem != null ? ((DropdownItem)this.ColorDropdown.SelectedItem) : null;
                    }
                    catch(NullReferenceException)
                    {
                        return null;
                    }
                }
            }
            protected DropdownItem SelectedMaterial
            {
                get
                {
                    try
                    {
                        return this.MaterialDropdown.SelectedItem != null ? ((DropdownItem)this.MaterialDropdown.SelectedItem) : null;
                    }
                    catch (NullReferenceException)
                    {
                        return null;
                    }
                }
            }

            public VersionPanel(FrmSOHead parentForm, VersionDto version)
            {
                this.Panel = new Panel();
                this.Version = version;

                //label for version name 
                this.NameLabel = new Label();
                string covercount = version.Covers.Count == 1 ? $"1 cover" : $"{version.Covers.Count} covers";
                this.NameLabel.Text = $"Version {version.Version} ({covercount})";
                this.NameLabel.Location = new System.Drawing.Point(10, 10);
                this.NameLabel.Size = new System.Drawing.Size(250, 23);

                //version description label 
                this.DescLabel = new Label();
                this.DescLabel.Text = $"{version.Covers[0].Description.Trim()} {version.Covers[0].Material.Trim()} {version.Covers[0].Color.Trim()}";
                this.DescLabel.Size = new System.Drawing.Size(250, 23);
                this.DescLabel.Location = new System.Drawing.Point(10, 35);

                //select button 
                this.SelectButton = new Button();
                this.SelectButton.Text = "Select";
                this.SelectButton.Location = new System.Drawing.Point(10, 60);
                this.SelectButton.Enabled = true;

                //delete button 
                this.DeleteButton = new Button();
                this.DeleteButton.Text = "Delete";
                this.DeleteButton.Location = new System.Drawing.Point(90, 60);
                this.DeleteButton.Enabled = true;

                //save button 
                this.SaveButton = new Button();
                this.SaveButton.Text = "Save";
                this.SaveButton.Location = new System.Drawing.Point(170, 60);
                this.SaveButton.Enabled = false;

                this.SaveButton.Click += ((object sender, EventArgs e) =>
                {
                    parentForm.ProcessSo(version.Version, "");
                    parentForm.Soinf.clineds.socover[0].colorid = this.SelectedColor.Value;
                    parentForm.Soinf.clineds.socover[0].materialid = this.SelectedMaterial.Value;
                    parentForm.SaveSo(); 
                });

                //cancel button 
                this.CancelButton = new Button();
                this.CancelButton.Text = "Cancel";
                this.CancelButton.Location = new System.Drawing.Point(170, 90);
                this.CancelButton.Enabled = false;

                this.CancelButton.Click += ((object sender, EventArgs e) =>
                {
                    this.SelectColor(version.Covers[0].Color);
                    this.SelectMaterial(version.Covers[0].Material);
                });

                this.ColorDropdown = new ComboBox();
                this.MaterialDropdown = new ComboBox();
                
                //colors listbox 
                this.ColorDropdown.FormattingEnabled = true;
                this.ColorDropdown.Location = new System.Drawing.Point(10, 90);
                this.ColorDropdown.Name = "colorDropdown";
                this.ColorDropdown.Size = new System.Drawing.Size(76, 21);
                this.ColorDropdown.TabIndex = 382;
                this.ColorDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });

                foreach (var row in parentForm.Soinf.soreferenceds.view_qucolordata)
                {
                    this.ColorDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectColor(version.Covers[0].Color);

                //materials listbox 
                this.MaterialDropdown.FormattingEnabled = true;
                this.MaterialDropdown.Location = new System.Drawing.Point(90, 90);
                this.MaterialDropdown.Name = "materialDropdown";
                this.MaterialDropdown.Size = new System.Drawing.Size(76, 21);
                this.MaterialDropdown.TabIndex = 382;
                this.MaterialDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });

                foreach (var row in parentForm.Soinf.soreferenceds.view_qumaterialdata)
                {
                    this.MaterialDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectMaterial(version.Covers[0].Material);

                //select action 
                this.SelectButton.Click += ((object sender, EventArgs e) =>
                {
                    parentForm.ProcessSo(version.Version, "");
                });

                //delete action 
                this.DeleteButton.Click += ((object sender, EventArgs e) =>
                {
                    parentForm.DeleteVersion(version.Version);
                });

                this.Panel.Controls.Add(this.NameLabel);
                this.Panel.Controls.Add(this.DescLabel);
                this.Panel.Controls.Add(this.SelectButton);
                this.Panel.Controls.Add(this.DeleteButton);
                this.Panel.Controls.Add(this.ColorDropdown);
                this.Panel.Controls.Add(this.MaterialDropdown);
                this.Panel.Controls.Add(this.SaveButton);
                this.Panel.Controls.Add(this.CancelButton);

                this.Panel.Enabled = true;
                this.EnableSave(false);
                this.Panel.Size = new Size(350, 120);
            }

            public void UpdateUI(int idcol, string descrip, int color, int material)
            {
                if (idcol == this.Version.Covers[0].IdCol)
                    this.DescLabel.Text = descrip;

                this.SelectColor(color);
                this.SelectMaterial(material);
            }

            public void Enable(bool enabled = true)
            {
                this.SelectButton.Enabled = enabled;
                this.DeleteButton.Enabled = enabled;
                this.MaterialDropdown.Enabled = enabled;
                this.ColorDropdown.Enabled = enabled;
                this.EnableSave(this.IsDirty);
            }

            public void EnableSave(bool enabled = true)
            {
                this.SaveButton.Enabled = enabled;
                this.CancelButton.Enabled = enabled;
            }

            public void SelectColor(string color)
            {
                this.SelectDropdownItem(this.ColorDropdown, color);
            }

            public void SelectColor(int color)
            {
                this.SelectDropdownItem(this.ColorDropdown, color);
            }

            public void SelectMaterial(string material)
            {
                this.SelectDropdownItem(this.MaterialDropdown, material);
            }

            public void SelectMaterial(int material)
            {
                this.SelectDropdownItem(this.MaterialDropdown, material);
            }

            private void SelectDropdownItem(ComboBox dropdown, string display)
            {
                display = display.Trim();
                for (int n = 0; n < dropdown.Items.Count; n++)
                {
                    if (((DropdownItem)dropdown.Items[n]).Display == display)
                    {
                        dropdown.SelectedIndex = n;
                        break;
                    }
                }
            }

            private void SelectDropdownItem(ComboBox dropdown, int value)
            {
                for (int n = 0; n < dropdown.Items.Count; n++)
                {
                    if (((DropdownItem)dropdown.Items[n]).Value == value)
                    {
                        dropdown.SelectedIndex = n;
                        break;
                    }
                }
            }
        }

        private class CoverDto
        {
            public int IdCol { get; private set; }
            public string Description { get; set; }
            public string Color { get; set; }
            public string Material { get; set; }

            public CoverDto(quoterpt.view_soreportlinedataRow row)
            {
                this.Description = row.descrip;
                this.Color = row.color;
                this.IdCol = row.idcol;
                this.Material = row.material;
            }
        }

        private class VersionDto
        {
            public string Version { get; set; }
            public List<CoverDto> Covers { get; private set; }

            public VersionDto()
            {
                this.Covers = new List<CoverDto>();
            }

            public VersionDto(string version, List<quoterpt.view_soreportlinedataRow> covers) : this()
            {
                this.Version = version;
                foreach (var row in covers)
                {
                    this.Covers.Add(new CoverDto(row));
                }
            }
        }

        private class DropdownItem
        {
            public int Value { get; private set; }
            public string Display { get; private set; }

            public DropdownItem(int value, string display)
            {
                this.Value = value;
                this.Display = display;
            }

            public override string ToString()
            {
                return this.Display;
            }
        }
    }
}// namespace