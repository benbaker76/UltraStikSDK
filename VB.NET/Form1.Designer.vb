<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.label4 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.buSetId = New System.Windows.Forms.Button
		Me.cboId = New System.Windows.Forms.ComboBox
		Me.chkRestrictor = New System.Windows.Forms.CheckBox
		Me.label2 = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.butSetMapFile = New System.Windows.Forms.Button
		Me.butBrowseMapFile = New System.Windows.Forms.Button
		Me.txtMapFile = New System.Windows.Forms.TextBox
		Me.cboMaps = New System.Windows.Forms.ComboBox
		Me.butSetMap = New System.Windows.Forms.Button
		Me.lvwDevices = New System.Windows.Forms.ListView
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader3 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader4 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader5 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader6 = New System.Windows.Forms.ColumnHeader
		Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
		Me.rdoRAM = New System.Windows.Forms.RadioButton
		Me.rdoFlash = New System.Windows.Forms.RadioButton
		Me.SuspendLayout()
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(15, 11)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(81, 13)
		Me.label4.TabIndex = 31
		Me.label4.Text = "Select Joystick:"
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(12, 155)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(19, 13)
		Me.label3.TabIndex = 30
		Me.label3.Text = "Id:"
		'
		'buSetId
		'
		Me.buSetId.Location = New System.Drawing.Point(82, 165)
		Me.buSetId.Name = "buSetId"
		Me.buSetId.Size = New System.Drawing.Size(100, 31)
		Me.buSetId.TabIndex = 29
		Me.buSetId.Text = "Set Id"
		Me.buSetId.UseVisualStyleBackColor = True
		'
		'cboId
		'
		Me.cboId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboId.FormattingEnabled = True
		Me.cboId.Location = New System.Drawing.Point(15, 171)
		Me.cboId.Name = "cboId"
		Me.cboId.Size = New System.Drawing.Size(61, 21)
		Me.cboId.TabIndex = 28
		'
		'chkRestrictor
		'
		Me.chkRestrictor.AutoSize = True
		Me.chkRestrictor.Checked = True
		Me.chkRestrictor.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkRestrictor.Location = New System.Drawing.Point(15, 290)
		Me.chkRestrictor.Name = "chkRestrictor"
		Me.chkRestrictor.Size = New System.Drawing.Size(71, 17)
		Me.chkRestrictor.TabIndex = 27
		Me.chkRestrictor.Text = "Restrictor"
		Me.chkRestrictor.UseVisualStyleBackColor = True
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(9, 220)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(50, 13)
		Me.label2.TabIndex = 26
		Me.label2.Text = "Map File:"
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(192, 155)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(31, 13)
		Me.label1.TabIndex = 25
		Me.label1.Text = "Map:"
		'
		'butSetMapFile
		'
		Me.butSetMapFile.Location = New System.Drawing.Point(360, 230)
		Me.butSetMapFile.Name = "butSetMapFile"
		Me.butSetMapFile.Size = New System.Drawing.Size(100, 31)
		Me.butSetMapFile.TabIndex = 22
		Me.butSetMapFile.Text = "Set Map File"
		Me.butSetMapFile.UseVisualStyleBackColor = True
		'
		'butBrowseMapFile
		'
		Me.butBrowseMapFile.Location = New System.Drawing.Point(307, 236)
		Me.butBrowseMapFile.Name = "butBrowseMapFile"
		Me.butBrowseMapFile.Size = New System.Drawing.Size(34, 20)
		Me.butBrowseMapFile.TabIndex = 21
		Me.butBrowseMapFile.Text = "..."
		Me.butBrowseMapFile.UseVisualStyleBackColor = True
		'
		'txtMapFile
		'
		Me.txtMapFile.Location = New System.Drawing.Point(12, 236)
		Me.txtMapFile.Name = "txtMapFile"
		Me.txtMapFile.Size = New System.Drawing.Size(289, 20)
		Me.txtMapFile.TabIndex = 20
		'
		'cboMaps
		'
		Me.cboMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboMaps.FormattingEnabled = True
		Me.cboMaps.Location = New System.Drawing.Point(195, 171)
		Me.cboMaps.Name = "cboMaps"
		Me.cboMaps.Size = New System.Drawing.Size(146, 21)
		Me.cboMaps.TabIndex = 19
		'
		'butSetMap
		'
		Me.butSetMap.Location = New System.Drawing.Point(360, 165)
		Me.butSetMap.Name = "butSetMap"
		Me.butSetMap.Size = New System.Drawing.Size(100, 31)
		Me.butSetMap.TabIndex = 18
		Me.butSetMap.Text = "Set Map"
		Me.butSetMap.UseVisualStyleBackColor = True
		'
		'lvwDevices
		'
		Me.lvwDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2, Me.columnHeader3, Me.columnHeader4, Me.columnHeader5, Me.columnHeader6, Me.ColumnHeader7})
		Me.lvwDevices.FullRowSelect = True
		Me.lvwDevices.GridLines = True
		Me.lvwDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
		Me.lvwDevices.HideSelection = False
		Me.lvwDevices.Location = New System.Drawing.Point(12, 30)
		Me.lvwDevices.Name = "lvwDevices"
		Me.lvwDevices.Size = New System.Drawing.Size(450, 113)
		Me.lvwDevices.TabIndex = 17
		Me.lvwDevices.UseCompatibleStateImageBehavior = False
		Me.lvwDevices.View = System.Windows.Forms.View.Details
		'
		'columnHeader1
		'
		Me.columnHeader1.Text = "Vendor Id"
		Me.columnHeader1.Width = 80
		'
		'columnHeader2
		'
		Me.columnHeader2.Text = "Product Id"
		Me.columnHeader2.Width = 80
		'
		'columnHeader3
		'
		Me.columnHeader3.Text = "Manufacturer"
		Me.columnHeader3.Width = 120
		'
		'columnHeader4
		'
		Me.columnHeader4.Text = "Product"
		Me.columnHeader4.Width = 150
		'
		'columnHeader5
		'
		Me.columnHeader5.Text = "Serial Number"
		Me.columnHeader5.Width = 80
		'
		'columnHeader6
		'
		Me.columnHeader6.Text = "Firmware Version"
		Me.columnHeader6.Width = 80
		'
		'ColumnHeader7
		'
		Me.ColumnHeader7.Text = "Device Path"
		'
		'rdoRAM
		'
		Me.rdoRAM.AutoSize = True
		Me.rdoRAM.Checked = True
		Me.rdoRAM.Location = New System.Drawing.Point(360, 289)
		Me.rdoRAM.Name = "rdoRAM"
		Me.rdoRAM.Size = New System.Drawing.Size(49, 17)
		Me.rdoRAM.TabIndex = 33
		Me.rdoRAM.TabStop = True
		Me.rdoRAM.Text = "RAM"
		Me.rdoRAM.UseVisualStyleBackColor = True
		'
		'rdoFlash
		'
		Me.rdoFlash.AutoSize = True
		Me.rdoFlash.Location = New System.Drawing.Point(307, 289)
		Me.rdoFlash.Name = "rdoFlash"
		Me.rdoFlash.Size = New System.Drawing.Size(50, 17)
		Me.rdoFlash.TabIndex = 32
		Me.rdoFlash.TabStop = True
		Me.rdoFlash.Text = "Flash"
		Me.rdoFlash.UseVisualStyleBackColor = True
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(478, 330)
		Me.Controls.Add(Me.rdoRAM)
		Me.Controls.Add(Me.rdoFlash)
		Me.Controls.Add(Me.label4)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.buSetId)
		Me.Controls.Add(Me.cboId)
		Me.Controls.Add(Me.chkRestrictor)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.butSetMapFile)
		Me.Controls.Add(Me.butBrowseMapFile)
		Me.Controls.Add(Me.txtMapFile)
		Me.Controls.Add(Me.cboMaps)
		Me.Controls.Add(Me.butSetMap)
		Me.Controls.Add(Me.lvwDevices)
		Me.Name = "Form1"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "UltraStik Test"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Private WithEvents label4 As System.Windows.Forms.Label
	Private WithEvents label3 As System.Windows.Forms.Label
	Private WithEvents buSetId As System.Windows.Forms.Button
	Private WithEvents cboId As System.Windows.Forms.ComboBox
	Private WithEvents chkRestrictor As System.Windows.Forms.CheckBox
	Private WithEvents label2 As System.Windows.Forms.Label
	Private WithEvents label1 As System.Windows.Forms.Label
	Private WithEvents butSetMapFile As System.Windows.Forms.Button
	Private WithEvents butBrowseMapFile As System.Windows.Forms.Button
	Private WithEvents txtMapFile As System.Windows.Forms.TextBox
	Private WithEvents cboMaps As System.Windows.Forms.ComboBox
	Private WithEvents butSetMap As System.Windows.Forms.Button
	Private WithEvents lvwDevices As System.Windows.Forms.ListView
	Private WithEvents columnHeader1 As System.Windows.Forms.ColumnHeader
	Private WithEvents columnHeader2 As System.Windows.Forms.ColumnHeader
	Private WithEvents columnHeader3 As System.Windows.Forms.ColumnHeader
	Private WithEvents columnHeader4 As System.Windows.Forms.ColumnHeader
	Private WithEvents columnHeader5 As System.Windows.Forms.ColumnHeader
	Private WithEvents columnHeader6 As System.Windows.Forms.ColumnHeader
	Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
	Private WithEvents rdoRAM As System.Windows.Forms.RadioButton
	Private WithEvents rdoFlash As System.Windows.Forms.RadioButton

End Class
