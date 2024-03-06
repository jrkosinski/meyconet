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
using System.Linq;
using WSGUtilitieslib;

namespace Estimating
{
    public class ScrollingVersionsPanel : ScrollingPanel
    {
        private const int SUB_PANEL_WIDTH = 450;
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

        public void SelectVersionPanel(string version)
        {
            var selected = _versionPanels.FirstOrDefault((i) => i.IsSelected);
            selected.IsSelected = false; 

            var newSelected = _versionPanels.FirstOrDefault((i) => i.VersionCode == version);
            newSelected.IsSelected = true;
            newSelected.Panel.Refresh();
            selected.Panel.Refresh();
        }

        private void OnSelectedVersionChanged(object sender, EventArgs e)
        {
            FrmSOHead parent = (FrmSOHead)this._parentForm;
            if (parent.CurrentVersion != String.Empty)
            {
                foreach(VersionPanel versionPanel in this._versionPanels)
                {
                    versionPanel.IsSelected = (versionPanel.VersionCode == parent.CurrentVersion);
                }
            }
        }

        private class VersionPanel
        {
            public string VersionCode { get { return this.Version.Version; } }
            private VersionDto Version { get; set; }
            public Panel Panel { get; private set; }
            public Label NameLabel { get; private set; }
            public Label DescLabel { get; private set; }
            //public Button SelectButton { get; private set; }
            public Button DeleteButton { get; private set; }
            public Button SaveButton { get; private set; }
            public Button CancelButton { get; private set; }
            public ComboBox ColorDropdown { get; private set; }
            public ComboBox MaterialDropdown { get; private set; }
            public ComboBox SpacingDropdown { get; private set; }
            public ComboBox OverlapDropdown { get; private set; }
            public LinkLabel InternalCommentsButton { get; private set; }
            public LinkLabel CustomerCommentsButton { get; private set; }
            public TextBox InternalCommentsTextbox { get; private set; }
            public TextBox CustomerCommentsTextbox { get; private set; }
            public Label ListPriceLabel { get; private set; }
            public Label NetPriceLabel { get; private set; }
            public Button NewCoverButton { get; private set; }
            public Button NewVersionButton { get; private set; }

            public bool IsSelected
            {
                get { return Panel.BackColor == Color.White; }
                set
                {
                    Color backColor = value? Color.White : Color.LightGray;
                    if (Panel.BackColor != backColor)
                        Panel.BackColor = backColor;

                    this.NewVersionButton.Enabled = value;
                    this.NewCoverButton.Enabled = value;
                }
            }
            public bool IsDirty {
                get {
                    if (this.SelectedColor != null && this.SelectedMaterial != null)
                    {
                        return (this.SelectedColor != null && this.SelectedColor.Display != this.Version.Covers[0].Color.Trim()) ||
                            (this.SelectedMaterial != null && this.SelectedMaterial.Display != this.Version.Covers[0].Material.Trim()) ||
                            (this.SelectedSpacing != null && this.SelectedSpacing.Display != this.Version.Covers[0].Spacing.Trim()) ||
                            (this.SelectedOverlap != null && this.SelectedOverlap.Display != this.Version.Covers[0].Overlap.Trim()) ||
                            this.CustomerCommentsTextbox.Text != this.Version.CustomerComments || 
                            this.InternalCommentsTextbox.Text != this.Version.InternalComments;
                    }
                    return false;
                }
            }
            private DropdownItem SelectedColor
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
            private DropdownItem SelectedMaterial
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
            private DropdownItem SelectedSpacing
            {
                get
                {
                    try
                    {
                        return this.SpacingDropdown.SelectedItem != null ? ((DropdownItem)this.SpacingDropdown.SelectedItem) : null;
                    }
                    catch (NullReferenceException)
                    {
                        return null;
                    }
                }
            }
            private DropdownItem SelectedOverlap
            {
                get
                {
                    try
                    {
                        return this.OverlapDropdown.SelectedItem != null ? ((DropdownItem)this.OverlapDropdown.SelectedItem) : null;
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
                //TODO: check for nulls 
                this.DescLabel.Text = $"{version.Covers[0].Description.Trim()} {version.Covers[0].Material.Trim()} {version.Covers[0].Color.Trim()}";
                this.DescLabel.Size = new System.Drawing.Size(250, 23);
                this.DescLabel.Location = new System.Drawing.Point(10, 35);
                this.DescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //delete button 
                this.DeleteButton = new Button();
                this.DeleteButton.Text = "Delete";
                this.DeleteButton.Location = new System.Drawing.Point(10, 60);
                this.DeleteButton.Enabled = true;
                this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //save button 
                this.SaveButton = new Button();
                this.SaveButton.Text = "Save";
                this.SaveButton.Location = new System.Drawing.Point(90, 60);
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
                this.CancelButton.Location = new System.Drawing.Point(170, 60);
                this.CancelButton.Enabled = false;
                this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.CancelButton.Click += ((object sender, EventArgs e) =>
                {
                    this.SelectColor(version.Covers[0].Color);
                    this.SelectMaterial(version.Covers[0].Material);
                    this.InternalCommentsTextbox.Text = version.InternalComments;
                    this.CustomerCommentsTextbox.Text = version.CustomerComments;
                });

                //new cover button 
                this.NewCoverButton = new Button();
                this.NewCoverButton.Text = "New Cover";
                this.NewCoverButton.Location = new System.Drawing.Point(250, 60);
                this.NewCoverButton.Size = new Size(80, this.NewCoverButton.Height);
                this.NewCoverButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.NewCoverButton.Click += ((object sender, EventArgs e) =>
                {
                    parentForm.EnterEditCoverMode();
                    parentForm.CreateNewCover();
                });

                //new version button 
                this.NewVersionButton = new Button();
                this.NewVersionButton.Text = "New Version";
                this.NewVersionButton.Location = new System.Drawing.Point(335, 60);
                this.NewVersionButton.Size = new Size(90, this.NewVersionButton.Height);
                this.NewVersionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.NewVersionButton.Click += ((object sender, EventArgs e) =>
                {
                    parentForm.EnterEditCoverMode();
                    parentForm.CreateNewVersion();
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
                this.ColorDropdown.Enabled = this.Version.IsEditable;

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
                this.MaterialDropdown.Enabled = this.Version.IsEditable;

                foreach (var row in parentForm.Soinf.soreferenceds.view_qumaterialdata)
                {
                    this.MaterialDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectMaterial(version.Covers[0].Material);

                //spacing listbox
                this.SpacingDropdown = new ComboBox();
                this.SpacingDropdown.Location = new System.Drawing.Point(190, 90);
                this.SpacingDropdown.Name = "spacingDropdown";
                this.SpacingDropdown.Size = new System.Drawing.Size(96, 21);
                this.SpacingDropdown.TabIndex = 382;
                this.SpacingDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.SpacingDropdown.Enabled = this.Version.IsEditable;

                foreach (var row in parentForm.Soinf.soreferenceds.view_quspacingdata)
                {
                    this.SpacingDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectSpacing(version.Covers[0].Spacing);

                //overlap listbox
                this.OverlapDropdown = new ComboBox();
                this.OverlapDropdown.Location = new System.Drawing.Point(290, 90);
                this.OverlapDropdown.Name = "overlapDropdown";
                this.OverlapDropdown.Size = new System.Drawing.Size(96, 21);
                this.OverlapDropdown.TabIndex = 382;
                this.OverlapDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.OverlapDropdown.Enabled = this.Version.IsEditable;

                foreach (var row in parentForm.Soinf.soreferenceds.view_quoverlapdata)
                {
                    this.OverlapDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectOverlap(version.Covers[0].Overlap);

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
                

                //customer comments textbox 
                this.CustomerCommentsTextbox = new TextBox();
                this.CustomerCommentsTextbox.Visible = false;
                this.CustomerCommentsTextbox.Size = new Size(0, 0);
                this.CustomerCommentsTextbox.Location = new Point(10, 150);
                this.CustomerCommentsTextbox.Multiline = true;
                this.CustomerCommentsTextbox.Text = this.Version.CustomerComments;
                

                //internal comments button 
                this.InternalCommentsButton = new LinkLabel();
                this.InternalCommentsButton.Text = "int comments";
                this.InternalCommentsButton.Size = new Size(90, this.InternalCommentsButton.Height);
                this.InternalCommentsButton.Enabled = true;
                this.InternalCommentsButton.Location = new Point(10, 120);
                this.InternalCommentsButton.Size = new Size(100, this.InternalCommentsButton.Height);
                this.InternalCommentsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                //customer comments button 
                this.CustomerCommentsButton = new LinkLabel();
                this.CustomerCommentsButton.Text = "cust comments";
                this.CustomerCommentsButton.Size = new Size(90, this.CustomerCommentsButton.Height);
                this.CustomerCommentsButton.Enabled = true;
                this.CustomerCommentsButton.Location = new Point(110, 120);
                this.CustomerCommentsButton.Size = new Size(100, this.CustomerCommentsButton.Height);
                this.CustomerCommentsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //list price label
                this.ListPriceLabel = new Label();
                this.ListPriceLabel.Text = $"List price: $0.00";
                this.ListPriceLabel.Size = new System.Drawing.Size(250, 23);
                this.ListPriceLabel.Location = new System.Drawing.Point(320, 120);
                this.ListPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //net price label
                this.NetPriceLabel = new Label();
                this.NetPriceLabel.Text = $"Net price: ${version.Covers[0].Price.ToString("#.00")}";
                this.NetPriceLabel.Size = new System.Drawing.Size(250, 23);
                this.NetPriceLabel.Location = new System.Drawing.Point(210, 120);
                this.NetPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.InternalCommentsButton.Click += ((object sender, EventArgs e) =>
                {
                    this.ToggleInternalComments();

                    //if internal comments are showing, customer comments should be hidden 
                    this.ToggleCustomerComments(false);
                });

                this.CustomerCommentsButton.Click += ((object sender, EventArgs e) =>
                {
                    this.ToggleCustomerComments();

                    //if customer comments are showing, internal comments should be hidden 
                    this.ToggleInternalComments(false);
                });


                this.ColorDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });
                this.MaterialDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });
                this.SpacingDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });
                this.OverlapDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
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

                this.Panel.Click += ((object sender, EventArgs e) =>
                {
                    if (!this.IsSelected)
                    {
                        parentForm.SelectVersionPanel(this.Version.Version);
                    }
                });

                this.Panel.Controls.Add(this.NameLabel);
                this.Panel.Controls.Add(this.DescLabel);
                
                this.Panel.Controls.Add(this.DeleteButton);
                this.Panel.Controls.Add(this.ColorDropdown);
                this.Panel.Controls.Add(this.MaterialDropdown);
                this.Panel.Controls.Add(this.SpacingDropdown);
                this.Panel.Controls.Add(this.OverlapDropdown);
                this.Panel.Controls.Add(this.SaveButton);
                this.Panel.Controls.Add(this.CancelButton);
                this.Panel.Controls.Add(this.InternalCommentsButton);
                this.Panel.Controls.Add(this.InternalCommentsTextbox);
                this.Panel.Controls.Add(this.CustomerCommentsButton);
                this.Panel.Controls.Add(this.CustomerCommentsTextbox);
                this.Panel.Controls.Add(this.ListPriceLabel);
                this.Panel.Controls.Add(this.NetPriceLabel);
                this.Panel.Controls.Add(this.NewCoverButton);
                this.Panel.Controls.Add(this.NewVersionButton);

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
                this.DeleteButton.Enabled = enabled;
                this.MaterialDropdown.Enabled = enabled && this.Version.IsEditable;
                this.ColorDropdown.Enabled = enabled && this.Version.IsEditable;
                this.SpacingDropdown.Enabled = enabled && this.Version.IsEditable;
                this.OverlapDropdown.Enabled = enabled && this.Version.IsEditable;
                this.InternalCommentsButton.Enabled = enabled;
                this.CustomerCommentsButton.Enabled = enabled;

                if (!enabled)
                {
                    this.NewCoverButton.Enabled = false;
                    this.NewVersionButton.Enabled = false;
                }
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

            public void SelectSpacing(string spacing)
            {
                this.SelectDropdownItem(this.SpacingDropdown, spacing);
            }

            public void SelectSpacing(int spacing)
            {
                this.SelectDropdownItem(this.SpacingDropdown, spacing);
            }

            public void SelectOverlap(string overlap)
            {
                this.SelectDropdownItem(this.OverlapDropdown, overlap); ;
            }

            public void SelectOverlap(int overlap)
            {
                this.SelectDropdownItem(this.OverlapDropdown, overlap); ;
            }

            private void SelectDropdownItem(ComboBox dropdown, string display)
            {
                if (display != null)
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
                else
                    visible = false;

                this.ToggleTextbox(this.CustomerCommentsTextbox, visible.Value);
            }

            private void ToggleInternalComments(bool? visible = null)
            {
                if (visible == null)
                    visible = !this.InternalCommentsTextbox.Visible;
                else
                    visible = false;

                this.ToggleTextbox(this.InternalCommentsTextbox, visible.Value);
            }

            private void ToggleTextbox(TextBox textBox, bool visible)
            {
                if (visible && !textBox.Visible)
                {
                    textBox.Visible = true;
                    textBox.Size = new Size(400, 80);
                    this.Panel.Size = new Size(this.Panel.Size.Width, SUB_PANEL_HEIGHT + 100);
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
            public string Spacing { get; set; }
            public string Overlap { get; set; }
            public bool Editable { get; set; }
            public decimal Price { get; set; }
            public string Product { get; set; }

            public CoverDto(quoterpt.view_soreportlinedataRow row)
            {
                try { this.IdCol = row.idcol; } catch (Exception) { } 
                try { this.Description = row.descrip; } catch (Exception) { }
                try { this.Color = row.color; } catch (Exception) { }
                try { this.Material = row.material; } catch (Exception) { }
                try { this.Overlap = row.overlap; } catch (Exception) { }
                try { this.Spacing = row.spacing; } catch (Exception) { }
                try { this.Price = row.price; } catch (Exception) { }
                try { this.Product = row.product.Trim(); } catch (Exception) { }

                if (this.Spacing == null) this.Spacing = String.Empty;
                if (this.Overlap == null) this.Overlap = String.Empty;

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