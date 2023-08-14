Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO

Public Class Form1
	Private m_ultraStik As UltraStik = Nothing

	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		m_ultraStik = New UltraStik(Me)
		AddHandler m_ultraStik.OnUsbDeviceAttached, AddressOf OnUsbDeviceAttached
		AddHandler m_ultraStik.OnUsbDeviceRemoved, AddressOf OnUsbDeviceRemoved
		m_ultraStik.Initialize()

		UpdateDeviceList()

		cboMaps.Items.AddRange(UltraStik.FriendlyMapNameArray)
		cboMaps.SelectedIndex = 0

		cboId.Items.AddRange(New String() {"1", "2", "3", "4", "5"})
		cboId.SelectedIndex = 0
	End Sub

	Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
		m_ultraStik.Shutdown()
	End Sub

	Private Sub butSetMap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles butSetMap.Click
		If lvwDevices.SelectedIndices.Count = 0 Then
			Return
		End If

		Dim selectedJoy As Integer = lvwDevices.SelectedIndices(0)

		m_ultraStik.SetRestrictor(selectedJoy, chkRestrictor.Checked)
		m_ultraStik.SetFlash(selectedJoy, rdoFlash.Checked)

		m_ultraStik.LoadMap(selectedJoy, UltraStik.MapNameArray(cboMaps.SelectedIndex))
	End Sub

	Private Sub butBrowseMapFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles butBrowseMapFile.Click
		Dim fd As New OpenFileDialog()

		fd.Filter = "UltraStik Map (*.um,*.ugc)|*.um;*.ugc|All files (*.*)|*.*"
		fd.InitialDirectory = Path.Combine(Application.StartupPath, "UltraStikMaps")

		If fd.ShowDialog() = DialogResult.OK Then
			txtMapFile.Text = fd.FileName
		End If
	End Sub

	Private Sub butSetMapFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles butSetMapFile.Click
		If lvwDevices.SelectedIndices.Count = 0 Then
			Return
		End If

		Dim selectedJoy As Integer = lvwDevices.SelectedIndices(0)

		m_ultraStik.SetRestrictor(selectedJoy, chkRestrictor.Checked)
		m_ultraStik.SetFlash(selectedJoy, rdoFlash.Checked)

		If Path.GetExtension(txtMapFile.Text) = ".ugc" Then
			m_ultraStik.LoadConfigFile(Path.Combine(Application.StartupPath, "UltraStikMaps"), txtMapFile.Text)
		Else
			m_ultraStik.LoadMapFile(selectedJoy, txtMapFile.Text)
		End If
	End Sub

	Private Sub buSetId_Click(ByVal sender As Object, ByVal e As EventArgs) Handles buSetId.Click
		If lvwDevices.SelectedIndices.Count = 0 Then
			Return
		End If

		Dim selectedJoy As Integer = lvwDevices.SelectedIndices(0)

		Select Case cboId.Text
			Case "1"
				m_ultraStik.SetUltraStikId(selectedJoy, UltraStik.UltraStikId.Id1)
				Exit Select
			Case "2"
				m_ultraStik.SetUltraStikId(selectedJoy, UltraStik.UltraStikId.Id2)
				Exit Select
			Case "3"
				m_ultraStik.SetUltraStikId(selectedJoy, UltraStik.UltraStikId.Id3)
				Exit Select
			Case "4"
				m_ultraStik.SetUltraStikId(selectedJoy, UltraStik.UltraStikId.Id4)
				Exit Select
		End Select
	End Sub

	Private Sub OnUsbDeviceAttached(ByVal id As Integer)
		UpdateDeviceList()
	End Sub

	Private Sub OnUsbDeviceRemoved(ByVal id As Integer)
		UpdateDeviceList()
	End Sub

	Private Sub UpdateDeviceList()
		lvwDevices.Items.Clear()

		For i As Integer = 0 To m_ultraStik.DeviceCount - 1
			lvwDevices.Items.Add(New ListViewItem(New String() {m_ultraStik.GetVendorId(i).ToString("X"), m_ultraStik.GetProductId(i).ToString("X"), m_ultraStik.GetVersionNumber(i).ToString("X"), m_ultraStik.GetVendorName(i), m_ultraStik.GetProductName(i), m_ultraStik.GetSerialNumber(i), _
		  m_ultraStik.GetDevicePath(i)}))
		Next

		For i As Integer = 0 To lvwDevices.Columns.Count - 1
			lvwDevices.Columns(i).Width = -2
		Next

		If lvwDevices.Items.Count > 0 Then
			lvwDevices.SelectedIndices.Clear()
			lvwDevices.SelectedIndices.Add(0)
		End If
	End Sub
End Class
