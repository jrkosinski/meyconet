namespace Inventory
{
  partial class FrmInventoryTransaction
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInventoryTransaction));
            this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
            this.textBoxItem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClear = new WSGUtilitieslib.Telemetry.Button();
            this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
            this.buttonGetItem = new WSGUtilitieslib.Telemetry.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTdate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxQty = new System.Windows.Forms.TextBox();
            this.labelCost = new System.Windows.Forms.Label();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.labelItmdesc = new System.Windows.Forms.Label();
            this.dateTimePickerTdate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(413, 31);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // textBoxItem
            // 
            this.textBoxItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxItem.Location = new System.Drawing.Point(113, 87);
            this.textBoxItem.Name = "textBoxItem";
            this.textBoxItem.Size = new System.Drawing.Size(118, 20);
            this.textBoxItem.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Item";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(202, 31);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(306, 31);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // buttonGetItem
            // 
            this.buttonGetItem.Image = ((System.Drawing.Image)(resources.GetObject("buttonGetItem.Image")));
            this.buttonGetItem.Location = new System.Drawing.Point(238, 84);
            this.buttonGetItem.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetItem.Name = "buttonGetItem";
            this.buttonGetItem.Size = new System.Drawing.Size(24, 23);
            this.buttonGetItem.TabIndex = 200;
            this.buttonGetItem.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 202;
            this.label2.Text = "Location";
            // 
            // labelTdate
            // 
            this.labelTdate.AutoSize = true;
            this.labelTdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTdate.Location = new System.Drawing.Point(55, 158);
            this.labelTdate.Name = "labelTdate";
            this.labelTdate.Size = new System.Drawing.Size(34, 13);
            this.labelTdate.TabIndex = 205;
            this.labelTdate.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 207;
            this.label3.Text = "Quantity";
            // 
            // textBoxQty
            // 
            this.textBoxQty.Location = new System.Drawing.Point(113, 203);
            this.textBoxQty.Name = "textBoxQty";
            this.textBoxQty.Size = new System.Drawing.Size(100, 20);
            this.textBoxQty.TabIndex = 206;
            this.textBoxQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCost.Location = new System.Drawing.Point(30, 244);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(59, 13);
            this.labelCost.TabIndex = 209;
            this.labelCost.Text = "Unit Cost";
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(113, 241);
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(100, 20);
            this.textBoxCost.TabIndex = 208;
            this.textBoxCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Location = new System.Drawing.Point(434, 155);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(194, 98);
            this.textBoxNotes.TabIndex = 210;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNotes.Location = new System.Drawing.Point(378, 159);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(40, 13);
            this.labelNotes.TabIndex = 211;
            this.labelNotes.Text = "Notes";
            // 
            // labelItmdesc
            // 
            this.labelItmdesc.AutoSize = true;
            this.labelItmdesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItmdesc.Location = new System.Drawing.Point(284, 89);
            this.labelItmdesc.Name = "labelItmdesc";
            this.labelItmdesc.Size = new System.Drawing.Size(97, 13);
            this.labelItmdesc.TabIndex = 212;
            this.labelItmdesc.Text = "Item description";
            // 
            // dateTimePickerTdate
            // 
            this.dateTimePickerTdate.Location = new System.Drawing.Point(113, 158);
            this.dateTimePickerTdate.Name = "dateTimePickerTdate";
            this.dateTimePickerTdate.Size = new System.Drawing.Size(154, 20);
            this.dateTimePickerTdate.TabIndex = 214;
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Items.AddRange(new object[] {
            "SC",
            "NY"});
            this.comboBoxLocation.Location = new System.Drawing.Point(113, 121);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(46, 21);
            this.comboBoxLocation.TabIndex = 387;
            // 
            // FrmInventoryTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 310);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.dateTimePickerTdate);
            this.Controls.Add(this.labelItmdesc);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.textBoxCost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxQty);
            this.Controls.Add(this.labelTdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGetItem);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxItem);
            this.Controls.Add(this.buttonClose);
            this.Name = "FrmInventoryTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory Transaction";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.Button buttonClose;
    public System.Windows.Forms.TextBox textBoxItem;
    public System.Windows.Forms.Label label1;
    public System.Windows.Forms.Button buttonClear;
    public System.Windows.Forms.Button buttonSave;
    public System.Windows.Forms.Button buttonGetItem;
    public System.Windows.Forms.Label label2;
    public System.Windows.Forms.Label labelTdate;
    public System.Windows.Forms.Label label3;
    public System.Windows.Forms.TextBox textBoxQty;
    public System.Windows.Forms.TextBox textBoxNotes;
    public System.Windows.Forms.Label labelNotes;
    public System.Windows.Forms.Label labelItmdesc;
    public System.Windows.Forms.TextBox textBoxCost;
    public System.Windows.Forms.DateTimePicker dateTimePickerTdate;
    public System.Windows.Forms.Label labelCost;
    public System.Windows.Forms.ComboBox comboBoxLocation;
  }
}