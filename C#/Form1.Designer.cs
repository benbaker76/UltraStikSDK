namespace UltraStikTest
{
    partial class Form1
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
			this.lvwDevices = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.butSetMap = new System.Windows.Forms.Button();
			this.cboMaps = new System.Windows.Forms.ComboBox();
			this.txtMapFile = new System.Windows.Forms.TextBox();
			this.butBrowseMapFile = new System.Windows.Forms.Button();
			this.butSetMapFile = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.chkRestrictor = new System.Windows.Forms.CheckBox();
			this.cboId = new System.Windows.Forms.ComboBox();
			this.buSetId = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.rdoFlash = new System.Windows.Forms.RadioButton();
			this.rdoRAM = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// lvwDevices
			// 
			this.lvwDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
			this.lvwDevices.FullRowSelect = true;
			this.lvwDevices.GridLines = true;
			this.lvwDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwDevices.HideSelection = false;
			this.lvwDevices.Location = new System.Drawing.Point(12, 29);
			this.lvwDevices.MultiSelect = false;
			this.lvwDevices.Name = "lvwDevices";
			this.lvwDevices.Size = new System.Drawing.Size(450, 113);
			this.lvwDevices.TabIndex = 0;
			this.lvwDevices.UseCompatibleStateImageBehavior = false;
			this.lvwDevices.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Vendor Id";
			this.columnHeader1.Width = 80;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Product Id";
			this.columnHeader2.Width = 80;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Version Number";
			this.columnHeader3.Width = 120;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Vendor Name";
			this.columnHeader4.Width = 150;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Product Name";
			this.columnHeader5.Width = 80;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Serial Number";
			this.columnHeader6.Width = 80;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Device Path";
			// 
			// butSetMap
			// 
			this.butSetMap.Location = new System.Drawing.Point(360, 164);
			this.butSetMap.Name = "butSetMap";
			this.butSetMap.Size = new System.Drawing.Size(100, 31);
			this.butSetMap.TabIndex = 1;
			this.butSetMap.Text = "Set Map";
			this.butSetMap.UseVisualStyleBackColor = true;
			this.butSetMap.Click += new System.EventHandler(this.butSetMap_Click);
			// 
			// cboMaps
			// 
			this.cboMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMaps.FormattingEnabled = true;
			this.cboMaps.Location = new System.Drawing.Point(195, 170);
			this.cboMaps.Name = "cboMaps";
			this.cboMaps.Size = new System.Drawing.Size(146, 21);
			this.cboMaps.TabIndex = 2;
			// 
			// txtMapFile
			// 
			this.txtMapFile.Location = new System.Drawing.Point(12, 235);
			this.txtMapFile.Name = "txtMapFile";
			this.txtMapFile.Size = new System.Drawing.Size(289, 20);
			this.txtMapFile.TabIndex = 3;
			// 
			// butBrowseMapFile
			// 
			this.butBrowseMapFile.Location = new System.Drawing.Point(307, 235);
			this.butBrowseMapFile.Name = "butBrowseMapFile";
			this.butBrowseMapFile.Size = new System.Drawing.Size(34, 20);
			this.butBrowseMapFile.TabIndex = 4;
			this.butBrowseMapFile.Text = "...";
			this.butBrowseMapFile.UseVisualStyleBackColor = true;
			this.butBrowseMapFile.Click += new System.EventHandler(this.butBrowseMapFile_Click);
			// 
			// butSetMapFile
			// 
			this.butSetMapFile.Location = new System.Drawing.Point(360, 229);
			this.butSetMapFile.Name = "butSetMapFile";
			this.butSetMapFile.Size = new System.Drawing.Size(100, 31);
			this.butSetMapFile.TabIndex = 5;
			this.butSetMapFile.Text = "Set Map File";
			this.butSetMapFile.UseVisualStyleBackColor = true;
			this.butSetMapFile.Click += new System.EventHandler(this.butSetMapFile_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(192, 154);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(31, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Map:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 219);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Map File:";
			// 
			// chkRestrictor
			// 
			this.chkRestrictor.AutoSize = true;
			this.chkRestrictor.Checked = true;
			this.chkRestrictor.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRestrictor.Location = new System.Drawing.Point(15, 289);
			this.chkRestrictor.Name = "chkRestrictor";
			this.chkRestrictor.Size = new System.Drawing.Size(71, 17);
			this.chkRestrictor.TabIndex = 12;
			this.chkRestrictor.Text = "Restrictor";
			this.chkRestrictor.UseVisualStyleBackColor = true;
			// 
			// cboId
			// 
			this.cboId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboId.FormattingEnabled = true;
			this.cboId.Location = new System.Drawing.Point(15, 170);
			this.cboId.Name = "cboId";
			this.cboId.Size = new System.Drawing.Size(61, 21);
			this.cboId.TabIndex = 13;
			// 
			// buSetId
			// 
			this.buSetId.Location = new System.Drawing.Point(82, 164);
			this.buSetId.Name = "buSetId";
			this.buSetId.Size = new System.Drawing.Size(100, 31);
			this.buSetId.TabIndex = 14;
			this.buSetId.Text = "Set Id";
			this.buSetId.UseVisualStyleBackColor = true;
			this.buSetId.Click += new System.EventHandler(this.buSetId_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 154);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "Id:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Select Joystick:";
			// 
			// rdoFlash
			// 
			this.rdoFlash.AutoSize = true;
			this.rdoFlash.Location = new System.Drawing.Point(307, 288);
			this.rdoFlash.Name = "rdoFlash";
			this.rdoFlash.Size = new System.Drawing.Size(50, 17);
			this.rdoFlash.TabIndex = 17;
			this.rdoFlash.TabStop = true;
			this.rdoFlash.Text = "Flash";
			this.rdoFlash.UseVisualStyleBackColor = true;
			// 
			// rdoRAM
			// 
			this.rdoRAM.AutoSize = true;
			this.rdoRAM.Checked = true;
			this.rdoRAM.Location = new System.Drawing.Point(360, 288);
			this.rdoRAM.Name = "rdoRAM";
			this.rdoRAM.Size = new System.Drawing.Size(49, 17);
			this.rdoRAM.TabIndex = 18;
			this.rdoRAM.TabStop = true;
			this.rdoRAM.Text = "RAM";
			this.rdoRAM.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(472, 326);
			this.Controls.Add(this.rdoRAM);
			this.Controls.Add(this.rdoFlash);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.buSetId);
			this.Controls.Add(this.cboId);
			this.Controls.Add(this.chkRestrictor);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butSetMapFile);
			this.Controls.Add(this.butBrowseMapFile);
			this.Controls.Add(this.txtMapFile);
			this.Controls.Add(this.cboMaps);
			this.Controls.Add(this.butSetMap);
			this.Controls.Add(this.lvwDevices);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "UltraStik Test";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwDevices;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button butSetMap;
        private System.Windows.Forms.ComboBox cboMaps;
        private System.Windows.Forms.TextBox txtMapFile;
        private System.Windows.Forms.Button butBrowseMapFile;
		private System.Windows.Forms.Button butSetMapFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkRestrictor;
        private System.Windows.Forms.ComboBox cboId;
        private System.Windows.Forms.Button buSetId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.RadioButton rdoFlash;
		private System.Windows.Forms.RadioButton rdoRAM;
    }
}

