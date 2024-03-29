﻿
using CommonAppClasses;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Estimating
{
    public class ScrollingVersionsPanel : ScrollingPanel
    {
        private const int SUB_PANEL_WIDTH = 450;
        private const int SUB_PANEL_HEIGHT = 170;

        Dictionary<string, int> _versionsLookup = new Dictionary<string, int>();
        List<VersionPanel> _versionPanels = new List<VersionPanel>();
        private int _originalPanelWidth = 0;

        public VersionPanel SelectedVersionPanel
        {
            get {
                return (this._versionPanels.Count > 0) ? this._versionPanels.FirstOrDefault((p) => p.IsSelected) : null;
            }
        }

        public ScrollingVersionsPanel(Form parentForm, Panel panel) : base(parentForm, panel)
        {
            this._originalPanelWidth = panel.Width;

            FrmSOHead parent = (FrmSOHead)parentForm;
            parent.SelectedVersionChanged -= this.OnSelectedVersionChanged;
            parent.SelectedVersionChanged += this.OnSelectedVersionChanged;

            parent.SelectedCoverChanged -= this.OnSelectedCoverChanged;
            parent.SelectedCoverChanged += this.OnSelectedCoverChanged;
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
                var row = ((FrmSOHead)this._parentForm).Soinf.clineds.socover.FirstOrDefault(x => x.cover == cover);

                if (row != null)
                {
                    this._versionPanels[index].UpdateUI(
                        row.idcol,
                        row.product.Trim(),
                        row.descrip.Trim(),
                        row.colorid,
                        row.materialid,
                        row.overlapid,
                        row.spacingid
                    );
                }
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

        public void SoftReload()
        {
            for (int n = 0; n < this._versionPanels.Count; n++)
            {
                if (!this._versionPanels[n].IsSelected)
                {
                    this._versionPanels[n].RestoreState();
                }
            }
        }

        public void SelectVersionPanel(string version)
        {
            var selected = _versionPanels.FirstOrDefault((i) => i.IsSelected);
            if (selected != null)
            {
                selected.IsSelected = false;

                var newSelected = _versionPanels.FirstOrDefault((i) => i.VersionCode == version);
                if (newSelected != null)
                {
                    newSelected.IsSelected = true;
                    newSelected.Panel.Refresh();
                }
                selected.Panel.Refresh();
            }
        }

        private void OnSelectedVersionChanged(object sender, EventArgs e)
        {
            FrmSOHead parent = (FrmSOHead)this._parentForm;
            if (parent.CurrentVersion != String.Empty)
            {
                foreach (VersionPanel versionPanel in this._versionPanels)
                {
                    versionPanel.IsSelected = (versionPanel.VersionCode == parent.CurrentVersion);
                }
            }
        }

        private void OnSelectedCoverChanged(object sender, EventArgs e)
        {
            FrmSOHead parent = (FrmSOHead)this._parentForm;
            if (!String.IsNullOrEmpty(parent.CurrentVersion) && !String.IsNullOrEmpty(parent.CurrentCover))
            {
                VersionPanel currentVersion = _versionPanels.FirstOrDefault((i) => i.VersionCode == parent.CurrentVersion); 
                if (currentVersion != null)
                    currentVersion.SetSelectedCover(parent.CurrentCover);
            }
        }

        public class VersionPanel
        {
            private string _selectedCover = "A";
            private bool _pauseEvents = false;

            public string VersionCode { get { return this.Version.Version; } }
            public CoverDto SelectedCover
            {
                get {
                    var cover = this.GetCover(this._selectedCover);

                    if (cover == null)
                        cover = this.Version.Covers.FirstOrDefault();

                    return cover;
                }
            }
            public string Material
            {
                get
                {
                    CoverDto cover = this.SelectedCover;
                    return cover != null && cover.Material != null ? cover.Material.Trim() : String.Empty;
                }
            }
            public string Color
            {
                get
                {
                    CoverDto cover = this.SelectedCover;
                    return cover != null && cover.Color != null ? cover.Color.Trim() : String.Empty;
                }
            }
            public string Spacing
            {
                get
                {
                    CoverDto cover = this.SelectedCover;
                    return cover != null && cover.Spacing != null ? cover.Spacing.Trim() : String.Empty;
                }
            }
            public string Overlap
            {
                get
                {
                    CoverDto cover = this.SelectedCover;
                    return cover != null && cover.Overlap != null ? cover.Overlap.Trim() : String.Empty;
                }
            }
            public string Description
            {
                get
                {
                    CoverDto cover = this.SelectedCover;
                    return cover != null && cover.Description != null ? cover.Description.Trim() : String.Empty;
                }
            }
            private VersionDto Version { get; set; }
            public Panel Panel { get; private set; }
            public Label NameLabel { get; private set; }
            public Label DescLabel { get; private set; }
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
            public Label ColorTabLabel { get; private set; }
            public Label ColorLabel { get; private set; }
            public Label MaterialLabel { get; private set; }
            public Label OverlapLabel { get; private set; }
            public Label SpacingLabel { get; private set; }
            public ComboBox CoverDropdown { get; private set; }
            public Label CoverLabel { get; private set; }

            public bool IsSelected
            {
                get { return Panel.BackColor == System.Drawing.Color.White; }
                set
                {
                    Color backColor = value ? System.Drawing.Color.White : System.Drawing.Color.LightGray;
                    if (Panel.BackColor != backColor)
                        Panel.BackColor = backColor;

                    this.NewVersionButton.Enabled = value && !this.ParentForm.IsEditing;
                    this.NewCoverButton.Enabled = value && !this.ParentForm.IsEditing;
                }
            }
            public bool IsDirty
            {
                get
                {
                    if (this.SelectedColor != null && this.SelectedMaterial != null)
                    {
                        return (this.SelectedColor != null && this.SelectedColor.Display != this.Color) ||
                            (this.SelectedMaterial != null && this.SelectedMaterial.Display != this.Material) ||
                            (this.SelectedSpacing != null && this.SelectedSpacing.Display != this.Spacing) ||
                            (this.SelectedOverlap != null && this.SelectedOverlap.Display != this.Overlap) ||
                            this.CustomerCommentsTextbox.Text != this.Version.CustomerComments ||
                            this.InternalCommentsTextbox.Text != this.Version.InternalComments;
                    }
                    return false;
                }
            }
            private CoverDto GetCover(string coverCode)
            {
                if (this.Version != null)
                {
                    return this.Version.Covers.FirstOrDefault((c) => c.Cover == coverCode);
                }

                return null;
            }
            private DropdownItem SelectedColor
            {
                get
                {
                    try
                    {
                        return this.ColorDropdown.SelectedItem != null ? ((DropdownItem)this.ColorDropdown.SelectedItem) : null;
                    }
                    catch (NullReferenceException)
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
            private string ProductType
            {
                get
                {
                    CoverDto cover = this.SelectedCover;
                    return cover != null && cover.ProductType != null ? cover.ProductType.Trim() : String.Empty;
                }
            }
            private string NewVersion { get; set; }
            private string DeletingVersion { get; set; }
            private FrmSOHead ParentForm { get; set; }
            private bool IsIntComDirty
            {
                get { return this.InternalCommentsTextbox.Text != this.Version.InternalComments; }
            }
            private bool IsCustComDirty
            {
                get { return this.CustomerCommentsTextbox.Text != this.Version.CustomerComments; }
            }

            public VersionPanel(FrmSOHead parentForm, VersionDto version)
            {
                this.ParentForm = parentForm;

                this.Panel = new Panel();
                this.Version = version;

                //label for version name 
                this.NameLabel = new Label();
                this.NameLabel.Location = new System.Drawing.Point(10, 20);
                this.NameLabel.Size = new System.Drawing.Size(250, 23);
                this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //label for version name 
                this.CoverLabel = new Label();
                this.CoverLabel.Text = "cover";
                this.CoverLabel.AutoSize = true;
                this.CoverLabel.Location = new System.Drawing.Point(324, 24);
                this.CoverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.CoverLabel.Visible = false;

                //dropdown for covers
                this.CoverDropdown = new ComboBox();
                this.CoverDropdown.FormattingEnabled = true;
                this.CoverDropdown.Location = new System.Drawing.Point(375, 20);
                this.CoverDropdown.Size = new System.Drawing.Size(50, 23);
                this.CoverDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.CoverDropdown.Enabled = this.SelectedCover.IsEditable;
                this.CoverDropdown.Visible = false;

                if (version.Covers.Count > 1)
                {
                    this.CoverLabel.Visible = true;
                    this.CoverDropdown.Visible = true;
                    int index = 0;

                    foreach (var row in version.Covers)
                    {
                        this.CoverDropdown.Items.Add(new DropdownItem(index++, row.Cover.Trim()));
                    }
                }


                //version description label 
                this.DescLabel = new Label();
                this.DescLabel.Size = new System.Drawing.Size(250, 23);
                this.DescLabel.Location = new System.Drawing.Point(10, 45);
                this.DescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //delete button 
                this.DeleteButton = new Button();
                this.DeleteButton.Text = "Delete";
                this.DeleteButton.Location = new System.Drawing.Point(10, 70);
                this.DeleteButton.Enabled = true;
                this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //save button 
                this.SaveButton = new Button();
                this.SaveButton.Text = "Save";
                this.SaveButton.Location = new System.Drawing.Point(90, 70);
                this.SaveButton.Enabled = false;
                this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.SaveButton.Click += ((object sender, EventArgs e) =>
                {
                    this.Enable(false);
                    parentForm.ProcessSo(version.Version, this._selectedCover);
                    if (this.SelectedColor != null)
                        parentForm.Soinf.clineds.socover[0].colorid = this.SelectedColor.Value;
                    if (this.SelectedMaterial != null)
                        parentForm.Soinf.clineds.socover[0].materialid = this.SelectedMaterial.Value;
                    if (this.SelectedSpacing != null)
                        parentForm.Soinf.clineds.socover[0].spacingid = this.SelectedSpacing.Value;
                    if (this.SelectedOverlap != null)
                        parentForm.Soinf.clineds.socover[0].overlapid = this.SelectedOverlap.Value;
                    
                    parentForm.Soinf.somastds.soversion[0].intcomments = this.InternalCommentsTextbox.Text;
                    parentForm.Soinf.somastds.soversion[0].custcomments = this.CustomerCommentsTextbox.Text;
                    parentForm.SaveSo(loadVersionsList: false);
                });

                //cancel button 
                this.CancelButton = new Button();
                this.CancelButton.Text = "Cancel";
                this.CancelButton.Location = new System.Drawing.Point(170, 70);
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
                this.NewCoverButton.Location = new System.Drawing.Point(250, 70);
                this.NewCoverButton.Size = new Size(80, this.NewCoverButton.Height);
                this.NewCoverButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.NewCoverButton.Click += ((object sender, EventArgs e) =>
                {
                    if (!this.IsSelected)
                        this.SelectThisVersionPanel();

                    if (!_pauseEvents)
                    {
                        parentForm.EnterEditCoverMode();
                        parentForm.CreateNewCover();
                    }
                });

                //new version button 
                this.NewVersionButton = new Button();
                this.NewVersionButton.Text = "New Version";
                this.NewVersionButton.Location = new System.Drawing.Point(335, 70);
                this.NewVersionButton.Size = new Size(90, this.NewVersionButton.Height);
                this.NewVersionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.NewVersionButton.Click += ((object sender, EventArgs e) =>
                {
                    if (!this.IsSelected)
                        this.SelectThisVersionPanel();

                    if (parentForm.IsEditing)
                        parentForm.CancelEdit();
                    this.NewVersion = parentForm.CreateNewVersion(version.Version);
                    parentForm.EnterEditCoverMode();
                });

                this.ColorDropdown = new ComboBox();
                this.MaterialDropdown = new ComboBox();

                //colors listbox 
                this.ColorDropdown.FormattingEnabled = true;
                this.ColorDropdown.Location = new System.Drawing.Point(110, 110);
                this.ColorDropdown.Name = "colorDropdown";
                this.ColorDropdown.Size = new System.Drawing.Size(76, 21);
                this.ColorDropdown.TabIndex = 382;
                this.ColorDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ColorDropdown.Enabled = this.SelectedCover.IsEditable;

                foreach (var row in parentForm.Soinf.soreferenceds.view_qucolordata)
                {
                    this.ColorDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectColor(version.Covers[0].Color);

                //materials listbox 
                this.MaterialDropdown.FormattingEnabled = true;
                this.MaterialDropdown.Location = new System.Drawing.Point(10, 110);
                this.MaterialDropdown.Name = "materialDropdown";
                this.MaterialDropdown.Size = new System.Drawing.Size(96, 21);
                this.MaterialDropdown.TabIndex = 382;
                this.MaterialDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.MaterialDropdown.Enabled = this.SelectedCover.IsEditable;

                foreach (var row in parentForm.Soinf.soreferenceds.view_qumaterialdata)
                {
                    this.MaterialDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectMaterial(version.Covers[0].Material);

                //spacing listbox
                this.SpacingDropdown = new ComboBox();
                this.SpacingDropdown.Location = new System.Drawing.Point(190, 110);
                this.SpacingDropdown.Name = "spacingDropdown";
                this.SpacingDropdown.Size = new System.Drawing.Size(96, 21);
                this.SpacingDropdown.TabIndex = 382;
                this.SpacingDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.SpacingDropdown.Enabled = this.SelectedCover.IsEditable;

                foreach (var row in parentForm.Soinf.soreferenceds.view_quspacingdata)
                {
                    this.SpacingDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectSpacing(version.Covers[0].Spacing);

                //overlap listbox
                this.OverlapDropdown = new ComboBox();
                this.OverlapDropdown.Location = new System.Drawing.Point(290, 110);
                this.OverlapDropdown.Name = "overlapDropdown";
                this.OverlapDropdown.Size = new System.Drawing.Size(96, 21);
                this.OverlapDropdown.TabIndex = 382;
                this.OverlapDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.OverlapDropdown.Enabled = this.SelectedCover.IsEditable;

                foreach (var row in parentForm.Soinf.soreferenceds.view_quoverlapdata)
                {
                    this.OverlapDropdown.Items.Add(new DropdownItem(row.idcol, row.descrip.Trim()));
                }
                this.SelectOverlap(version.Covers[0].Overlap);

                //color label
                this.ColorLabel = new Label();
                this.ColorLabel.Text = "color";
                this.ColorLabel.AutoSize = true;
                this.ColorLabel.Size = new System.Drawing.Size(10, 45);
                this.ColorLabel.Location = new System.Drawing.Point(110, 95);
                this.ColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //material label
                this.MaterialLabel = new Label();
                this.MaterialLabel.Text = "material";
                this.MaterialLabel.AutoSize = true;
                this.MaterialLabel.Size = new System.Drawing.Size(10, 45);
                this.MaterialLabel.Location = new System.Drawing.Point(10, 95);
                this.MaterialLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //spacing label
                this.SpacingLabel = new Label();
                this.SpacingLabel.Text = "spacing";
                this.SpacingLabel.AutoSize = true;
                this.SpacingLabel.Size = new System.Drawing.Size(10, 45);
                this.SpacingLabel.Location = new System.Drawing.Point(190, 95);
                this.SpacingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //overlap label
                this.OverlapLabel = new Label();
                this.OverlapLabel.Text = "overlap";
                this.OverlapLabel.AutoSize = true;
                this.OverlapLabel.Size = new System.Drawing.Size(10, 45);
                this.OverlapLabel.Location = new System.Drawing.Point(290, 95);
                this.OverlapLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                //delete action 
                this.DeleteButton.Click += ((object sender, EventArgs e) =>
                {
                    this.DeletingVersion = version.Version;
                    parentForm.DeleteVersion(version.Version);
                });

                //internal comments textbox 
                this.InternalCommentsTextbox = new TextBox();
                this.InternalCommentsTextbox.Visible = false;
                this.InternalCommentsTextbox.Size = new Size(0, 0);
                this.InternalCommentsTextbox.Location = new Point(10, 165);
                this.InternalCommentsTextbox.Multiline = true;
                this.InternalCommentsTextbox.Text = this.Version.InternalComments;


                //customer comments textbox 
                this.CustomerCommentsTextbox = new TextBox();
                this.CustomerCommentsTextbox.Visible = false;
                this.CustomerCommentsTextbox.Size = new Size(0, 0);
                this.CustomerCommentsTextbox.Location = new Point(10, 165);
                this.CustomerCommentsTextbox.Multiline = true;
                this.CustomerCommentsTextbox.Text = this.Version.CustomerComments;


                //internal comments button 
                this.InternalCommentsButton = new LinkLabel();
                this.InternalCommentsButton.Text = "int comments";
                this.InternalCommentsButton.Size = new Size(90, this.InternalCommentsButton.Height);
                this.InternalCommentsButton.Enabled = true;
                this.InternalCommentsButton.Location = new Point(10, 140);
                this.InternalCommentsButton.Size = new Size(100, this.InternalCommentsButton.Height);
                this.InternalCommentsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                //customer comments button 
                this.CustomerCommentsButton = new LinkLabel();
                this.CustomerCommentsButton.Text = "cust comments";
                this.CustomerCommentsButton.Size = new Size(90, this.CustomerCommentsButton.Height);
                this.CustomerCommentsButton.Enabled = true;
                this.CustomerCommentsButton.Location = new Point(110, 140);
                this.CustomerCommentsButton.Size = new Size(100, this.CustomerCommentsButton.Height);
                this.CustomerCommentsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //list price label
                this.ListPriceLabel = new Label();
                this.ListPriceLabel.Size = new System.Drawing.Size(250, 23);
                this.ListPriceLabel.AutoSize = true;
                this.ListPriceLabel.Location = new System.Drawing.Point(210, 140);
                this.ListPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //net price label
                this.NetPriceLabel = new Label();
                this.NetPriceLabel.Size = new System.Drawing.Size(250, 23);
                this.NetPriceLabel.AutoSize = true;
                this.NetPriceLabel.Location = new System.Drawing.Point(320, 140);
                this.NetPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.ColorTabLabel = new Label();
                this.ColorTabLabel.Location = new System.Drawing.Point(1, 1);
                this.ColorTabLabel.Size = new System.Drawing.Size(50, 15);
                this.ColorTabLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                if (version.Covers.Count > 0)
                {
                    this.SelectCover(0);
                    this.LoadCover(this.SelectedCover);
                }

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
                this.InternalCommentsTextbox.LostFocus += ((object sender, EventArgs e) =>
                {
                    if (this.IsIntComDirty)
                    {
                        parentForm.Soinf.somastds.soversion[0].intcomments = this.InternalCommentsTextbox.Text;
                        this.Version.InternalComments = this.InternalCommentsTextbox.Text;
                        parentForm.SaveSoVersionComments(this.VersionCode, this.InternalCommentsTextbox.Text, this.CustomerCommentsTextbox.Text);
                    }
                });

                this.CustomerCommentsTextbox.TextChanged += ((object sender, EventArgs e) =>
                {
                    this.EnableSave(this.IsDirty);
                });
                this.CustomerCommentsTextbox.LostFocus += ((object sender, EventArgs e) =>
                {
                    if (this.IsCustComDirty)
                    {
                        parentForm.Soinf.somastds.soversion[0].custcomments = this.CustomerCommentsTextbox.Text;
                        this.Version.CustomerComments = this.CustomerCommentsTextbox.Text;
                        parentForm.SaveSoVersionComments(version.Version, this.InternalCommentsTextbox.Text, this.CustomerCommentsTextbox.Text);
                    }
                });
                this.CoverDropdown.SelectedIndexChanged += ((object sender, EventArgs e) =>
                {
                    if (!_pauseEvents)
                    {
                        string selectedCover = this.CoverDropdown.SelectedItem.ToString();
                        parentForm.ProcessSo(version.Version, selectedCover);

                        //set the correct cover 
                        this.SetSelectedCover(selectedCover);
                    }
                });

                this.Panel.Click += ((object sender, EventArgs e) =>
                {
                    if (!this.IsSelected && !this.ParentForm.IsEditing)
                    {
                        this.SelectThisVersionPanel();
                    }
                });

                this.Panel.Controls.Add(this.NameLabel);
                this.Panel.Controls.Add(this.DescLabel);
                this.Panel.Controls.Add(this.CoverLabel);
                this.Panel.Controls.Add(this.CoverDropdown);

                this.Panel.Controls.Add(this.DeleteButton);
                this.Panel.Controls.Add(this.ColorLabel);
                this.Panel.Controls.Add(this.MaterialLabel);
                this.Panel.Controls.Add(this.OverlapLabel);
                this.Panel.Controls.Add(this.SpacingLabel);
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
                this.Panel.Controls.Add(this.ColorTabLabel);


                this.IsSelected = this.VersionCode == parentForm.CurrentVersion;
                if (this.IsSelected)
                    this.SetSelectedCover(parentForm.CurrentCover);

                this.Panel.Enabled = true;
                this.EnableSave(false);
                this.SetSize(SUB_PANEL_WIDTH, SUB_PANEL_HEIGHT);
                this.Panel.BorderStyle = BorderStyle.FixedSingle;

                foreach (Control control in Panel.Controls)
                {
                    control.MouseClick += ((sender, e) => {
                        if (!this.IsSelected)
                        {
                            if (!String.IsNullOrEmpty(this.NewVersion))
                                this.NewVersion = null;
                            else if (!String.IsNullOrEmpty(this.DeletingVersion))
                                this.DeletingVersion = null;
                            else if (!this.ParentForm.IsEditing)
                                this.SelectThisVersionPanel();
                        }
                    });
                }
            }

            public void RestoreState()
            {
                this.ToggleTextbox(this.InternalCommentsTextbox, false);
                this.ToggleTextbox(this.CustomerCommentsTextbox, false);
                this.CustomerCommentsTextbox.Text = this.Version.CustomerComments;
                this.InternalCommentsTextbox.Text = this.Version.InternalComments;
                this.SelectColor(this.Color);
                this.SelectMaterial(this.Material);
                this.SelectOverlap(this.Overlap);
                this.SelectSpacing(this.Spacing);
            }

            public void UpdateUI(int idcol, string product, string descrip, int color, int material, int overlap, int spacing)
            {
                this.DescLabel.Text = $"{product} {descrip}";
                this.SelectColor(color);
                this.SelectMaterial(material);
                this.SelectOverlap(overlap);
                this.SelectSpacing(spacing);
                this.SelectCover(idcol);
            }

            public void Enable(bool enabled = true)
            {
                this.DeleteButton.Enabled = enabled && !this.ParentForm.IsEditing;
                this.MaterialDropdown.Enabled = enabled && this.SelectedCover.IsEditable && !this.ParentForm.IsEditing;
                this.ColorDropdown.Enabled = enabled && this.SelectedCover.IsEditable && !this.ParentForm.IsEditing;
                this.SpacingDropdown.Enabled = enabled && this.SelectedCover.IsEditable && !this.ParentForm.IsEditing;
                this.OverlapDropdown.Enabled = enabled && this.SelectedCover.IsEditable && !this.ParentForm.IsEditing;
                this.InternalCommentsButton.Enabled = enabled && !this.ParentForm.IsEditing;
                this.CustomerCommentsButton.Enabled = enabled && !this.ParentForm.IsEditing;
                this.CoverDropdown.Enabled = enabled && !this.ParentForm.IsEditing;

                if (!enabled || this.ParentForm.IsEditing)
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

            public void SelectCover(string cover)
            {
                this.SelectDropdownItem(this.CoverDropdown, cover);
            }

            public void SelectCover(int cover)
            {
                this.SelectDropdownItem(this.CoverDropdown, cover);
            }

            public void SetSelectedCover(string coverCode)
            {
                this._pauseEvents = true;
                try
                {
                    if (coverCode != this._selectedCover)
                    {
                        if (this.GetCover(coverCode) != null)
                        {
                            this._selectedCover = coverCode;

                            //find the right cover
                            CoverDto cover = this.SelectedCover;
                            if (cover != null)
                            {
                                this.LoadCover(cover);
                                this.SelectCover(cover.Cover);
                            }
                        }
                    }
                }
                finally
                {
                    this._pauseEvents = false;
                }
            }

            private void SelectThisVersionPanel()
            {
                this.Panel.Enabled = false;
                this.ParentForm.SelectVersionPanel(this.VersionCode, this._selectedCover);
            }

            private void LoadCover(CoverDto cover)
            {
                string covercount = this.Version.Covers.Count == 1 ? $"1 cover" : $"{this.Version.Covers.Count} covers";
                this.NameLabel.Text = $"Version {this.VersionCode} ({covercount})";

                this.DescLabel.Text = GetDescriptionText(this.Version, cover);
                this.SelectColor(cover.Color);
                this.SelectMaterial(cover.Material);
                this.SelectOverlap(cover.Overlap);
                this.SelectSpacing(cover.Spacing);
                this.SelectCover(cover.IdCol);

                this.ColorTabLabel.BackColor = this.Color == "MOCHA" ? ColorTranslator.FromHtml("#C0A392") : System.Drawing.Color.FromName(this.Color);
                this.ListPriceLabel.Text = $"List price: ${this.Version.ListPriceTotal.ToString("#,###.00")}";
                this.NetPriceLabel.Text = $"Net price: ${this.SelectedCover.NetPrice.ToString("#,###.00")}";
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
                    textBox.Size = new Size(400, 90);
                    this.SetSize(this.Panel.Size.Width, SUB_PANEL_HEIGHT + 100);
                    textBox.Focus();
                }
                else if (textBox.Visible)
                {
                    textBox.Visible = false;
                    textBox.Size = new Size(0, 0);
                    this.SetSize(this.Panel.Size.Width, SUB_PANEL_HEIGHT);
                }
            }

            private string GetDescriptionText(VersionDto version, CoverDto cover)
            {
                string output = String.Empty;
                return $"{cover.ProductType} {cover.Description}";
            }

            private string GetDescriptionText(VersionDto version)
            {
                string output = String.Empty;
                if (version != null && version.Covers != null && version.Covers.Count > 0 && version.Covers[0] != null)
                {
                    output = this.GetDescriptionText(version, version.Covers[0]);
                }

                return output;
            }

            private void SetSize(int width, int height)
            {
                this.Panel.Size = new Size(width, height);
                if (height <= SUB_PANEL_HEIGHT)
                {
                    this.InternalCommentsTextbox.Visible = false;
                    this.CustomerCommentsTextbox.Visible = false;
                }
            }
        }

        public class CoverDto
        {
            public int IdCol { get; private set; }
            public string Item { get; set; }
            public string Description { get; set; }
            public string Color { get; set; }
            public string Material { get; set; }
            public string Spacing { get; set; }
            public string Overlap { get; set; }
            public bool IsEditable { get; set; }
            public decimal NetPrice { get; set; }
            public decimal ListPrice { get; set; }
            public string ProductType { get; set; }
            public string Cover { get; set; }

            public CoverDto(quoterpt.view_soreportlinedataRow row)
            {
                try { this.IdCol = row.idcol; } catch (Exception) { }
                try { this.Item = row.item; } catch (Exception) { }
                try { this.Description = row.descrip; } catch (Exception) { }
                try { this.Color = row.color; } catch (Exception) { }
                try { this.Material = row.material; } catch (Exception) { }
                try { this.Overlap = row.overlap; } catch (Exception) { }
                try { this.Spacing = row.spacing; } catch (Exception) { }
                try { this.ListPrice = row.price; } catch (Exception) { }
                try { this.NetPrice = row.ordamt; } catch (Exception) { }
                try { this.ProductType = row.product.Trim(); } catch (Exception) { }
                try { this.Cover = row.cover.Trim(); } catch (Exception) { }

                if (this.Spacing == null) this.Spacing = String.Empty;
                if (this.Overlap == null) this.Overlap = String.Empty;

                this.IsEditable = row.product.Trim().ToUpper() != "STOCK COVER";
            }
        }

        public class VersionDto
        {
            public string Version { get; set; }
            public List<CoverDto> Covers { get; private set; }
            public string InternalComments { get; set; }
            public string CustomerComments { get; set; }
            public decimal ListPriceTotal {
                get {
                    return this.Covers.Select((c) => c.ListPrice).Sum();
                }
            }
            public decimal NetPriceTotal {
                get {
                    return this.Covers.Select((c) => c.NetPrice).Sum();
                }
            }
            public bool IsEditable
            {
                get
                {
                    for (int n = 0; n < this.Covers.Count; n++)
                    {
                        if (this.Covers[n].IsEditable)
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