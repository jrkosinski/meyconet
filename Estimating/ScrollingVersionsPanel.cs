﻿using CommonAppClasses;
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
        private const int SUB_PANEL_WIDTH = 350;
        private const int SUB_PANEL_HEIGHT = 150;

        Dictionary<string, int> _versionsLookup = new Dictionary<string, int>();
        List<VersionPanel> _versionPanels = new List<VersionPanel>();
        private int _originalPanelWidth = 0;

        public ScrollingVersionsPanel(Form parentForm, Panel panel) : base(parentForm, panel)
        {
            this._originalPanelWidth = panel.Width;

            FrmSOHead parent = (FrmSOHead)parentForm;
            parent.SelectedVersionChanged -= this.OnSelectedVersionChanged;
            parent.SelectedVersionChanged += this.OnSelectedVersionChanged;
        }

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

        public void Hide()
        {
            if (this._panel.Visible)
            {
                this._panel.Size = new Size(0, this._panel.Height);
                this._panel.Visible = false;
                this._parentForm.Size = new Size(this._parentForm.Width - SUB_PANEL_WIDTH, this._parentForm.Height);
            }
        }

        public void Show()
        {
            if (!this._panel.Visible)
            {
                this._panel.Size = new Size(this._originalPanelWidth, this._panel.Height);
                this._panel.Visible = true;
                this._parentForm.Size = new Size(this._parentForm.Width + SUB_PANEL_WIDTH, this._parentForm.Height);
            }
        }

        private void OnSelectedVersionChanged(object sender, EventArgs e)
        {
            FrmSOHead parent = (FrmSOHead)this._parentForm;
            if (parent.CurrentVersion != String.Empty)
            {
                foreach(VersionPanel versionPanel in this._versionPanels)
                {
                    versionPanel.IsSelected = (versionPanel.Version.Version == parent.CurrentVersion);
                }
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
            public Button InternalCommentsButton { get; private set; }
            public Button CustomerCommentsButton { get; private set; }
            public TextBox InternalCommentsTextbox { get; private set; }
            public TextBox CustomerCommentsTextbox { get; private set; }
            public bool IsSelected
            {
                get { return Panel.BackColor == Color.White; }
                set
                {
                    Color backColor = value? Color.White : Color.AliceBlue;
                    if (Panel.BackColor != backColor)
                        Panel.BackColor = backColor;
                }
            }
            public bool IsDirty {
                get {
                    if (this.SelectedColor != null && this.SelectedMaterial != null)
                    {
                        return this.SelectedColor.Display != this.Version.Covers[0].Color.Trim() ||
                            this.SelectedMaterial.Display != this.Version.Covers[0].Material.Trim() || 
                            this.CustomerCommentsTextbox.Text != this.Version.CustomerComments || 
                            this.InternalCommentsTextbox.Text != this.Version.InternalComments;
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
                this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //version description label 
                this.DescLabel = new Label();
                this.DescLabel.Text = $"{version.Covers[0].Description.Trim()} {version.Covers[0].Material.Trim()} {version.Covers[0].Color.Trim()}";
                this.DescLabel.Size = new System.Drawing.Size(250, 23);
                this.DescLabel.Location = new System.Drawing.Point(10, 35);
                this.DescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //select button 
                this.SelectButton = new Button();
                this.SelectButton.Text = "Select";
                this.SelectButton.Location = new System.Drawing.Point(10, 60);
                this.SelectButton.Enabled = true;
                this.SelectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //delete button 
                this.DeleteButton = new Button();
                this.DeleteButton.Text = "Delete";
                this.DeleteButton.Location = new System.Drawing.Point(90, 60);
                this.DeleteButton.Enabled = true;
                this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //save button 
                this.SaveButton = new Button();
                this.SaveButton.Text = "Save";
                this.SaveButton.Location = new System.Drawing.Point(200, 60);
                this.SaveButton.Enabled = false;
                this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.SaveButton.Click += ((object sender, EventArgs e) =>
                {
                    parentForm.ProcessSo(version.Version, "");
                    parentForm.Soinf.clineds.socover[0].colorid = this.SelectedColor.Value;
                    parentForm.Soinf.clineds.socover[0].materialid = this.SelectedMaterial.Value;
                    parentForm.Soinf.somastds.soversion[0].intcomments = this.InternalCommentsTextbox.Text;
                    parentForm.Soinf.somastds.soversion[0].custcomments = this.CustomerCommentsTextbox.Text;
                    parentForm.SaveSo(); 
                });

                //cancel button 
                this.CancelButton = new Button();
                this.CancelButton.Text = "Cancel";
                this.CancelButton.Location = new System.Drawing.Point(200, 90);
                this.CancelButton.Enabled = false;
                this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.CancelButton.Click += ((object sender, EventArgs e) =>
                {
                    this.SelectColor(version.Covers[0].Color);
                    this.SelectMaterial(version.Covers[0].Material);
                    this.InternalCommentsTextbox.Text = version.InternalComments;
                    this.CustomerCommentsTextbox.Text = version.CustomerComments;

                });

                this.ColorDropdown = new ComboBox();
                this.MaterialDropdown = new ComboBox();
                
                //colors listbox 
                this.ColorDropdown.FormattingEnabled = true;
                this.ColorDropdown.Location = new System.Drawing.Point(10, 90);
                this.ColorDropdown.Name = "colorDropdown";
                this.ColorDropdown.Size = new System.Drawing.Size(76, 21);
                this.ColorDropdown.TabIndex = 382;
                this.ColorDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                foreach (var row in parentForm.Soinf.soreferenceds.view_qucolordata)
                {
                    this.ColorDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectColor(version.Covers[0].Color);

                //materials listbox 
                this.MaterialDropdown.FormattingEnabled = true;
                this.MaterialDropdown.Location = new System.Drawing.Point(90, 90);
                this.MaterialDropdown.Name = "materialDropdown";
                this.MaterialDropdown.Size = new System.Drawing.Size(96, 21);
                this.MaterialDropdown.TabIndex = 382;
                this.MaterialDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

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

                //internal comments textbox 
                this.InternalCommentsTextbox = new TextBox();
                this.InternalCommentsTextbox.Visible = false;
                this.InternalCommentsTextbox.Size = new Size(0, 0);
                this.InternalCommentsTextbox.Location = new Point(10, 150);
                this.InternalCommentsTextbox.Multiline = true;
                this.InternalCommentsTextbox.Text = this.Version.InternalComments;
                this.InternalCommentsTextbox.LostFocus += ((object sender, EventArgs e) =>
                {
                    this.ToggleInternalComments(false);
                });

                //customer comments textbox 
                this.CustomerCommentsTextbox = new TextBox();
                this.CustomerCommentsTextbox.Visible = false;
                this.CustomerCommentsTextbox.Size = new Size(0, 0);
                this.CustomerCommentsTextbox.Location = new Point(10, 150);
                this.CustomerCommentsTextbox.Multiline = true;
                this.CustomerCommentsTextbox.Text = this.Version.CustomerComments;
                this.CustomerCommentsTextbox.LostFocus += ((object sender, EventArgs e) =>
                {
                    this.ToggleCustomerComments(false);
                });

                //internal comments button 
                this.InternalCommentsButton = new Button();
                this.InternalCommentsButton.Text = "int comments";
                this.InternalCommentsButton.Size = new Size(100, this.InternalCommentsButton.Height);
                this.InternalCommentsButton.Enabled = true;
                this.InternalCommentsButton.Location = new Point(10, 120);
                this.InternalCommentsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //customer comments button 
                this.CustomerCommentsButton = new Button();
                this.CustomerCommentsButton.Text = "cust comments";
                this.CustomerCommentsButton.Size = new Size(100, this.CustomerCommentsButton.Height);
                this.CustomerCommentsButton.Enabled = true;
                this.CustomerCommentsButton.Location = new Point(110, 120);
                this.CustomerCommentsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.InternalCommentsButton.Click += ((object sender, EventArgs e) =>
                {
                    this.ToggleInternalComments();
                });

                this.CustomerCommentsButton.Click += ((object sender, EventArgs e) =>
                {
                    this.ToggleCustomerComments();
                });


                this.ColorDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });
                this.MaterialDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });
                this.InternalCommentsTextbox.TextChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });
                this.CustomerCommentsTextbox.TextChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });


                this.Panel.Controls.Add(this.NameLabel);
                this.Panel.Controls.Add(this.DescLabel);
                this.Panel.Controls.Add(this.SelectButton);
                this.Panel.Controls.Add(this.DeleteButton);
                this.Panel.Controls.Add(this.ColorDropdown);
                this.Panel.Controls.Add(this.MaterialDropdown);
                this.Panel.Controls.Add(this.SaveButton);
                this.Panel.Controls.Add(this.CancelButton);
                this.Panel.Controls.Add(this.InternalCommentsButton);
                this.Panel.Controls.Add(this.InternalCommentsTextbox);
                this.Panel.Controls.Add(this.CustomerCommentsButton);
                this.Panel.Controls.Add(this.CustomerCommentsTextbox);

                this.IsSelected = this.Version.Version == parentForm.CurrentVersion;

                this.Panel.Enabled = true;
                this.EnableSave(false);
                this.Panel.Size = new Size(SUB_PANEL_WIDTH, SUB_PANEL_HEIGHT);
                this.Panel.BorderStyle = BorderStyle.FixedSingle;

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
                this.MaterialDropdown.Enabled = enabled && this.Version.IsEditable;
                this.ColorDropdown.Enabled = enabled && this.Version.IsEditable;
                this.InternalCommentsButton.Enabled = enabled;
                this.CustomerCommentsButton.Enabled = enabled;
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

            private void ToggleCustomerComments(bool? visible = null)
            {
                if (visible == null)
                    visible = !this.CustomerCommentsTextbox.Visible;

                this.ToggleTextbox(this.CustomerCommentsTextbox, visible.Value);

                //if customer comments are showing, internal comments should be hidden 
                if (visible.Value)
                {
                    this.ToggleInternalComments(false);
                }
            }

            private void ToggleInternalComments(bool? visible = null)
            {
                if (visible == null)
                    visible = !this.InternalCommentsTextbox.Visible;

                this.ToggleTextbox(this.InternalCommentsTextbox, visible.Value);

                //if internal comments are showing, customer comments should be hidden 
                if (visible.Value)
                {
                    this.ToggleCustomerComments(false);
                }
            }

            private void ToggleTextbox(TextBox textBox, bool visible)
            {
                if (visible && !textBox.Visible)
                {
                    textBox.Visible = true;
                    textBox.Size = new Size(290, 80);
                    this.Panel.Size = new Size(this.Panel.Size.Width, SUB_PANEL_HEIGHT + 80);
                    textBox.Focus();
                }
                else if (textBox.Visible)
                {
                    textBox.Visible = false;
                    textBox.Size = new Size(0, 0);
                    this.Panel.Size = new Size(this.Panel.Size.Width, SUB_PANEL_HEIGHT);
                }
            }
        }

        private class CoverDto
        {
            public int IdCol { get; private set; }
            public string Description { get; set; }
            public string Color { get; set; }
            public string Material { get; set; }
            public bool Editable { get; set; }

            public CoverDto(quoterpt.view_soreportlinedataRow row)
            {
                this.Description = row.descrip;
                this.Color = row.color;
                this.IdCol = row.idcol;
                try { this.Material = row.material; } catch (Exception) { }
                this.Editable = row.product.Trim().ToUpper() != "STOCK COVER";
            }
        }

        private class VersionDto
        {
            public string Version { get; set; }
            public List<CoverDto> Covers { get; private set; }
            public string InternalComments { get; private set; }
            public string CustomerComments { get; private set; }
            public bool IsEditable {
                get
                {
                    for(int n=0; n<this.Covers.Count; n++)
                    {
                        if (this.Covers[n].Editable)
                            return true;
                    }
                    return false; 
                } 
            }

            public VersionDto()
            {
                this.Covers = new List<CoverDto>();
            }

            public VersionDto(string version, List<quoterpt.view_soreportlinedataRow> covers) : this()
            {
                this.Version = version;
                this.InternalComments = covers[0].intcomments;
                this.CustomerComments = covers[0].custcomments;

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