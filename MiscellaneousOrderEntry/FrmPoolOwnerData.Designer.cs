namespace MiscellaneousOrderEntry
{
  partial class FrmPoolOwnerData
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPoolOwnerData));
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.textBoxLname = new System.Windows.Forms.TextBox();
        this.buttonClose = new WSGUtilitieslib.Telemetry.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.textBoxFname = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.textBoxCity = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.textBoxZip = new System.Windows.Forms.TextBox();
        this.textBoxState = new System.Windows.Forms.TextBox();
        this.buttonSave = new WSGUtilitieslib.Telemetry.Button();
        this.label5 = new System.Windows.Forms.Label();
        this.textBoxAddress = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridView1
        // 
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Location = new System.Drawing.Point(11, 120);
        this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.Size = new System.Drawing.Size(412, 181);
        this.dataGridView1.TabIndex = 0;
        // 
        // textBoxLname
        // 
        this.textBoxLname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxLname.Location = new System.Drawing.Point(78, 36);
        this.textBoxLname.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxLname.MaxLength = 20;
        this.textBoxLname.Name = "textBoxLname";
        this.textBoxLname.Size = new System.Drawing.Size(166, 20);
        this.textBoxLname.TabIndex = 2;
        // 
        // buttonClose
        // 
        this.buttonClose.Location = new System.Drawing.Point(343, 10);
        this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(56, 28);
        this.buttonClose.TabIndex = 2;
        this.buttonClose.Text = "Close";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(9, 40);
        this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(67, 13);
        this.label1.TabIndex = 3;
        this.label1.Text = "Last Name";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(9, 14);
        this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(67, 13);
        this.label2.TabIndex = 5;
        this.label2.Text = "First Name";
        // 
        // textBoxFname
        // 
        this.textBoxFname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxFname.Location = new System.Drawing.Point(78, 10);
        this.textBoxFname.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxFname.MaxLength = 20;
        this.textBoxFname.Name = "textBoxFname";
        this.textBoxFname.Size = new System.Drawing.Size(75, 20);
        this.textBoxFname.TabIndex = 1;
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(9, 90);
        this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(66, 13);
        this.label3.TabIndex = 7;
        this.label3.Text = "City, State";
        // 
        // textBoxCity
        // 
        this.textBoxCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxCity.Location = new System.Drawing.Point(78, 83);
        this.textBoxCity.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxCity.MaxLength = 20;
        this.textBoxCity.Name = "textBoxCity";
        this.textBoxCity.Size = new System.Drawing.Size(166, 20);
        this.textBoxCity.TabIndex = 4;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(296, 90);
        this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(25, 13);
        this.label4.TabIndex = 9;
        this.label4.Text = "Zip";
        // 
        // textBoxZip
        // 
        this.textBoxZip.Location = new System.Drawing.Point(321, 83);
        this.textBoxZip.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxZip.Name = "textBoxZip";
        this.textBoxZip.Size = new System.Drawing.Size(56, 20);
        this.textBoxZip.TabIndex = 6;
        // 
        // textBoxState
        // 
        this.textBoxState.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxState.Location = new System.Drawing.Point(247, 83);
        this.textBoxState.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxState.MaxLength = 2;
        this.textBoxState.Name = "textBoxState";
        this.textBoxState.Size = new System.Drawing.Size(32, 20);
        this.textBoxState.TabIndex = 5;
        // 
        // buttonSave
        // 
        this.buttonSave.Location = new System.Drawing.Point(283, 10);
        this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Size = new System.Drawing.Size(56, 28);
        this.buttonSave.TabIndex = 7;
        this.buttonSave.Text = "Save";
        this.buttonSave.UseVisualStyleBackColor = true;
        this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(9, 64);
        this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(52, 13);
        this.label5.TabIndex = 13;
        this.label5.Text = "Address";
        // 
        // textBoxAddress
        // 
        this.textBoxAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.textBoxAddress.Location = new System.Drawing.Point(78, 60);
        this.textBoxAddress.Margin = new System.Windows.Forms.Padding(2);
        this.textBoxAddress.MaxLength = 30;
        this.textBoxAddress.Name = "textBoxAddress";
        this.textBoxAddress.Size = new System.Drawing.Size(166, 20);
        this.textBoxAddress.TabIndex = 3;
        // 
        // FrmPoolOwnerData
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.LightSteelBlue;
        this.ClientSize = new System.Drawing.Size(436, 313);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.buttonSave);
        this.Controls.Add(this.textBoxState);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.textBoxZip);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.textBoxCity);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.textBoxFname);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.textBoxLname);
        this.Controls.Add(this.dataGridView1);
        this.Controls.Add(this.textBoxAddress);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Margin = new System.Windows.Forms.Padding(2);
        this.Name = "FrmPoolOwnerData";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Pool Owner Data";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.TextBox textBoxLname;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxFname;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBoxCity;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBoxZip;
    private System.Windows.Forms.TextBox textBoxState;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox textBoxAddress;
  }
}