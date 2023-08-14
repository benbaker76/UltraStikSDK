' ****************************************************************
' UltraStik.vb
' Author: Ben Baker [headsoft.com.au]
' ****************************************************************

Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices

Class UltraStik
	Public Enum UltraStikId As Integer
		Id1 = 0
		Id2 = 1
		Id3 = 2
		Id4 = 3
	End Enum

	Public Enum MapName
		vjoy2way
		joy2way
		joy4way
		udbjoy4way
		djoy4way
		rdjoy4way
		joy8way
		easyjoy8way
		analog
		mouse
	End Enum

	Public Shared MapNameArray As String() = {"vjoy2way", "joy2way", "joy4way", "udbjoy4way", "djoy4way", "rdjoy4way", _
	 "joy8way", "easyjoy8way", "analog", "mouse"}

	Public Shared FriendlyMapNameArray As String() = {"2-Way, Vertical", "2-Way, Horizontal", "4-Way", "4-Way, No Sticky (UD Bias)", "4-Way, Diagonals Only", "4-Way, Rotated Diagonals", _
  "8-Way", "8-Way, Easy Diagonals", "Analog", "Mouse Pointer"}

	' ================== 32-bit ====================

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_SetCallbacks", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_SetCallbacks32(ByVal usbDeviceAttachedCallback As USBDEVICE_ATTACHED_CALLBACK, ByVal usbDeviceRemovedCallback As USBDEVICE_REMOVED_CALLBACK)
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_Initialize", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_Initialize32() As Integer
	End Function

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_Shutdown", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_Shutdown32()
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_GetVendorId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_GetVendorId32(ByVal id As Integer) As Integer
	End Function

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_GetProductId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_GetProductId32(ByVal id As Integer) As Integer
	End Function

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_GetVersionNumber", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_GetVersionNumber32(ByVal id As Integer) As Integer
	End Function

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_GetVendorName", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_GetVendorName32(ByVal id As Integer, ByVal vendorName As StringBuilder)
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_GetProductName", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_GetProductName32(ByVal id As Integer, ByVal productName As StringBuilder)
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_GetSerialNumber", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_GetSerialNumber32(ByVal id As Integer, ByVal serialNumber As StringBuilder)
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_GetDevicePath", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_GetDevicePath32(ByVal id As Integer, ByVal devicePath As StringBuilder)
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_SetRestrictor", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_SetRestrictor32(ByVal id As Integer, <MarshalAs(UnmanagedType.Bool)> ByVal value As Boolean)
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_SetFlash", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_SetFlash32(ByVal id As Integer, <MarshalAs(UnmanagedType.Bool)> ByVal value As Boolean)
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_GetUltraStikId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_GetUltraStikId32(ByVal id As Integer) As Integer
	End Function

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_SetUltraStikId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_SetUltraStikId32(ByVal id As Integer, ByVal value As Integer)
	End Sub

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_LoadMap", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_LoadMap32(ByVal id As Integer, ByVal map As StringBuilder) As Integer
	End Function

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_LoadMapFile", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_LoadMapFile32(ByVal id As Integer, ByVal fileName As StringBuilder) As Integer
	End Function

	<DllImport("UltraStik32.dll", EntryPoint:="UltraStik_LoadConfigFile", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_LoadConfigFile32(ByVal MapPath As StringBuilder, ByVal fileName As StringBuilder) As Integer
	End Function

	' ================== 64-bit ====================

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_SetCallbacks", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_SetCallbacks64(ByVal usbDeviceAttachedCallback As USBDEVICE_ATTACHED_CALLBACK, ByVal usbDeviceRemovedCallback As USBDEVICE_REMOVED_CALLBACK)
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_Initialize", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_Initialize64() As Integer
	End Function

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_Shutdown", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_Shutdown64()
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_GetVendorId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_GetVendorId64(ByVal id As Integer) As Integer
	End Function

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_GetProductId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_GetProductId64(ByVal id As Integer) As Integer
	End Function

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_GetVersionNumber", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_GetVersionNumber64(ByVal id As Integer) As Integer
	End Function

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_GetVendorName", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_GetVendorName64(ByVal id As Integer, ByVal vendorName As StringBuilder)
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_GetProductName", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_GetProductName64(ByVal id As Integer, ByVal productName As StringBuilder)
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_GetSerialNumber", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_GetSerialNumber64(ByVal id As Integer, ByVal serialNumber As StringBuilder)
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_GetDevicePath", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_GetDevicePath64(ByVal id As Integer, ByVal devicePath As StringBuilder)
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_SetRestrictor", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_SetRestrictor64(ByVal id As Integer, <MarshalAs(UnmanagedType.Bool)> ByVal value As Boolean)
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_SetFlash", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_SetFlash64(ByVal id As Integer, <MarshalAs(UnmanagedType.Bool)> ByVal value As Boolean)
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_GetUltraStikId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_GetUltraStikId64(ByVal id As Integer) As Integer
	End Function

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_SetUltraStikId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub UltraStik_SetUltraStikId64(ByVal id As Integer, ByVal value As Integer)
	End Sub

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_LoadMap", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_LoadMap64(ByVal id As Integer, ByVal map As StringBuilder) As Integer
	End Function

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_LoadMapFile", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_LoadMapFile64(ByVal id As Integer, ByVal fileName As StringBuilder) As Integer
	End Function

	<DllImport("UltraStik64.dll", EntryPoint:="UltraStik_LoadConfigFile", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function UltraStik_LoadConfigFile64(ByVal mapPath As StringBuilder, ByVal fileName As StringBuilder) As Integer
	End Function

	Private Delegate Sub USBDEVICE_ATTACHED_CALLBACK(ByVal id As Integer)
	Private Delegate Sub USBDEVICE_REMOVED_CALLBACK(ByVal id As Integer)

	Public Delegate Sub UsbDeviceAttachedDelegate(ByVal id As Integer)
	Public Delegate Sub UsbDeviceRemovedDelegate(ByVal id As Integer)

	Public Event OnUsbDeviceAttached As UsbDeviceAttachedDelegate
	Public Event OnUsbDeviceRemoved As UsbDeviceRemovedDelegate

	<MarshalAs(UnmanagedType.FunctionPtr)> _
	Private UsbDeviceAttachedCallbackPtr As USBDEVICE_ATTACHED_CALLBACK = Nothing

	<MarshalAs(UnmanagedType.FunctionPtr)> _
	Private UsbDeviceRemovedCallbackPtr As USBDEVICE_REMOVED_CALLBACK = Nothing

	Private m_ctrl As Control

	Private m_is64Bit As Boolean = False

	Private m_deviceCount As Integer = 0

	Public Sub New(ByVal ctrl As Control)
		m_ctrl = ctrl
		m_is64Bit = Is64Bit()

		UsbDeviceAttachedCallbackPtr = New USBDEVICE_ATTACHED_CALLBACK(AddressOf UsbDeviceAttachedCallback)
		UsbDeviceRemovedCallbackPtr = New USBDEVICE_REMOVED_CALLBACK(AddressOf UsbDeviceRemovedCallback)

		If m_is64Bit Then
			UltraStik_SetCallbacks64(UsbDeviceAttachedCallbackPtr, UsbDeviceRemovedCallbackPtr)
		Else
			UltraStik_SetCallbacks32(UsbDeviceAttachedCallbackPtr, UsbDeviceRemovedCallbackPtr)
		End If
	End Sub

	Private Sub UsbDeviceAttachedCallback(ByVal id As Integer)
		m_deviceCount += 1

		RaiseEvent OnUsbDeviceAttached(id)
	End Sub

	Private Sub UsbDeviceRemovedCallback(ByVal id As Integer)
		m_deviceCount -= 1

		RaiseEvent OnUsbDeviceRemoved(id)
	End Sub

	Public Function Initialize() As Integer
		Return InlineAssignHelper(m_deviceCount, (If(m_is64Bit, UltraStik_Initialize64(), UltraStik_Initialize32())))
	End Function

	Public Sub Shutdown()
		If m_is64Bit Then
			UltraStik_Shutdown64()
		Else
			UltraStik_Shutdown32()
		End If
	End Sub

	Public Function GetVendorId(ByVal id As Integer) As Integer
		Return (If(m_is64Bit, UltraStik_GetVendorId64(id), UltraStik_GetVendorId32(id)))
	End Function

	Public Function GetProductId(ByVal id As Integer) As Integer
		Return (If(m_is64Bit, UltraStik_GetProductId64(id), UltraStik_GetProductId32(id)))
	End Function

	Public Function GetVersionNumber(ByVal id As Integer) As Integer
		Return (If(m_is64Bit, UltraStik_GetVersionNumber64(id), UltraStik_GetVersionNumber32(id)))
	End Function

	Public Function GetVendorName(ByVal id As Integer) As String
		Dim sb As New StringBuilder(256)

		If m_is64Bit Then
			UltraStik_GetVendorName64(id, sb)
		Else
			UltraStik_GetVendorName32(id, sb)
		End If

		Return sb.ToString()
	End Function

	Public Function GetProductName(ByVal id As Integer) As String
		Dim sb As New StringBuilder(256)

		If m_is64Bit Then
			UltraStik_GetProductName64(id, sb)
		Else
			UltraStik_GetProductName32(id, sb)
		End If

		Return sb.ToString()
	End Function

	Public Function GetSerialNumber(ByVal id As Integer) As String
		Dim sb As New StringBuilder(256)

		If m_is64Bit Then
			UltraStik_GetSerialNumber64(id, sb)
		Else
			UltraStik_GetSerialNumber32(id, sb)
		End If

		Return sb.ToString()
	End Function

	Public Function GetDevicePath(ByVal id As Integer) As String
		Dim sb As New StringBuilder(256)

		If m_is64Bit Then
			UltraStik_GetDevicePath64(id, sb)
		Else
			UltraStik_GetDevicePath32(id, sb)
		End If

		Return sb.ToString()
	End Function

	Public Sub SetRestrictor(ByVal id As Integer, ByVal value As Boolean)
		If m_is64Bit Then
			UltraStik_SetRestrictor64(id, value)
		Else
			UltraStik_SetRestrictor32(id, value)
		End If
	End Sub

	Public Sub SetFlash(ByVal id As Integer, ByVal value As Boolean)
		If m_is64Bit Then
			UltraStik_SetFlash64(id, value)
		Else
			UltraStik_SetFlash32(id, value)
		End If
	End Sub

	Public Function GetUltraStikId(ByVal id As Integer) As Integer
		Return (If(m_is64Bit, UltraStik_GetUltraStikId64(id), UltraStik_GetUltraStikId32(id)))
	End Function

	Public Sub SetUltraStikId(ByVal id As Integer, ByVal value As UltraStikId)
		If m_is64Bit Then
			UltraStik_SetUltraStikId64(id, CInt(value))
		Else
			UltraStik_SetUltraStikId32(id, CInt(value))
		End If
	End Sub

	Public Function LoadMap(ByVal id As Integer, ByVal map As String) As Integer
		Dim sb As New StringBuilder(map)

		Return (If(m_is64Bit, UltraStik_LoadMap64(id, sb), UltraStik_LoadMap32(id, sb)))
	End Function

	Public Function LoadMapFile(ByVal id As Integer, ByVal fileName As String) As Integer
		Dim sb As New StringBuilder(fileName)

		Return (If(m_is64Bit, UltraStik_LoadMapFile64(id, sb), UltraStik_LoadMapFile32(id, sb)))
	End Function

	Public Function LoadConfigFile(ByVal mapPath As String, ByVal fileName As String) As Integer
		Dim sbPath As New StringBuilder(mapPath)
		Dim sbFileName As New StringBuilder(fileName)

		Return (If(m_is64Bit, UltraStik_LoadConfigFile64(sbPath, sbFileName), UltraStik_LoadConfigFile32(sbPath, sbFileName)))
	End Function

	Public ReadOnly Property DeviceCount() As Integer
		Get
			Return m_deviceCount
		End Get
	End Property

	Private Function Is64Bit() As Boolean
		Return (Marshal.SizeOf(GetType(IntPtr)) = 8)
	End Function
	Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
		target = value
		Return value
	End Function
End Class