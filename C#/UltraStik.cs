// ****************************************************************
// UltraStik.cs
// Author: Ben Baker [headsoft.com.au]
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UltraStikTest
{
    public class UltraStik
    {
        public enum UltraStikId : int
        {
            Id1 = 0,
            Id2 = 1,
            Id3 = 2,
            Id4 = 3
        }

		public enum MapName
		{
			vjoy2way,
            joy2way,
            joy4way,
            udbjoy4way,
            djoy4way,
            rdjoy4way,
            joy8way,
            easyjoy8way,
            analog,
            mouse,
		};

		public static string[] MapNameArray =
        {
            "vjoy2way",
            "joy2way",
            "joy4way",
            "udbjoy4way",
            "djoy4way",
            "rdjoy4way",
            "joy8way",
            "easyjoy8way",
            "analog",
            "mouse"
        };

		public static string[] FriendlyMapNameArray =
        {
			"2-Way, Vertical",
			"2-Way, Horizontal",
			"4-Way",
			"4-Way, No Sticky (UD Bias)",
			"4-Way, Diagonals Only",
			"4-Way, Rotated Diagonals",
			"8-Way",
			"8-Way, Easy Diagonals",
			"Analog",
			"Mouse Pointer"
        };

		// ================== 32-bit ====================

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_SetCallbacks", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_SetCallbacks32(USBDEVICE_ATTACHED_CALLBACK usbDeviceAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbDeviceRemovedCallback);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_Initialize", CallingConvention = CallingConvention.StdCall)]
        private static extern int UltraStik_Initialize32();

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_Shutdown", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_Shutdown32();

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_GetVendorId", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_GetVendorId32(int id);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_GetProductId", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_GetProductId32(int id);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_GetVersionNumber", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_GetVersionNumber32(int id);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_GetVendorName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_GetVendorName32(int id, StringBuilder vendorName);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_GetProductName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_GetProductName32(int id, StringBuilder productName);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_GetSerialNumber", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_GetSerialNumber32(int id, StringBuilder serialNumber);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_GetDevicePath", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_GetDevicePath32(int id, StringBuilder devicePath);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_SetRestrictor", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_SetRestrictor32(int id, [MarshalAs(UnmanagedType.Bool)] bool value);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_SetFlash", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_SetFlash32(int id, [MarshalAs(UnmanagedType.Bool)] bool value);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_GetUltraStikId", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_GetUltraStikId32(int id);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_SetUltraStikId", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_SetUltraStikId32(int id, int value);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_LoadMap", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_LoadMap32(int id, StringBuilder map);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_LoadMapFile", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_LoadMapFile32(int id, StringBuilder fileName);

		[DllImport("UltraStik32.dll", EntryPoint = "UltraStik_LoadConfigFile", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_LoadConfigFile32(StringBuilder MapPath, StringBuilder fileName);

		// ================== 64-bit ====================

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_SetCallbacks", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_SetCallbacks64(USBDEVICE_ATTACHED_CALLBACK usbDeviceAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbDeviceRemovedCallback);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_Initialize", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_Initialize64();

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_Shutdown", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_Shutdown64();

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_GetVendorId", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_GetVendorId64(int id);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_GetProductId", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_GetProductId64(int id);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_GetVersionNumber", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_GetVersionNumber64(int id);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_GetVendorName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_GetVendorName64(int id, StringBuilder vendorName);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_GetProductName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_GetProductName64(int id, StringBuilder productName);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_GetSerialNumber", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_GetSerialNumber64(int id, StringBuilder serialNumber);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_GetDevicePath", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_GetDevicePath64(int id, StringBuilder devicePath);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_SetRestrictor", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_SetRestrictor64(int id, [MarshalAs(UnmanagedType.Bool)] bool value);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_SetFlash", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_SetFlash64(int id, [MarshalAs(UnmanagedType.Bool)] bool value);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_GetUltraStikId", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_GetUltraStikId64(int id);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_SetUltraStikId", CallingConvention = CallingConvention.StdCall)]
		private static extern void UltraStik_SetUltraStikId64(int id, int value);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_LoadMap", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_LoadMap64(int id, StringBuilder map);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_LoadMapFile", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_LoadMapFile64(int id, StringBuilder fileName);

		[DllImport("UltraStik64.dll", EntryPoint = "UltraStik_LoadConfigFile", CallingConvention = CallingConvention.StdCall)]
		private static extern int UltraStik_LoadConfigFile64(StringBuilder mapPath, StringBuilder fileName);

		private delegate void USBDEVICE_ATTACHED_CALLBACK(int id);
		private delegate void USBDEVICE_REMOVED_CALLBACK(int id);

		public delegate void UsbDeviceAttachedDelegate(int id);
		public delegate void UsbDeviceRemovedDelegate(int id);

		public event UsbDeviceAttachedDelegate OnUsbDeviceAttached = null;
		public event UsbDeviceRemovedDelegate OnUsbDeviceRemoved = null;

		[MarshalAs(UnmanagedType.FunctionPtr)]
		USBDEVICE_ATTACHED_CALLBACK UsbDeviceAttachedCallbackPtr = null;

		[MarshalAs(UnmanagedType.FunctionPtr)]
		USBDEVICE_REMOVED_CALLBACK UsbDeviceRemovedCallbackPtr = null;

		private Control m_ctrl;

		private bool m_is64Bit = false;

		private int m_deviceCount = 0;

        public UltraStik(Control ctrl)
        {
            m_ctrl = ctrl;
            m_is64Bit = Is64Bit();

			UsbDeviceAttachedCallbackPtr = new USBDEVICE_ATTACHED_CALLBACK(UsbDeviceAttachedCallback);
			UsbDeviceRemovedCallbackPtr = new USBDEVICE_REMOVED_CALLBACK(UsbDeviceRemovedCallback);

            if(m_is64Bit)
				UltraStik_SetCallbacks64(UsbDeviceAttachedCallbackPtr, UsbDeviceRemovedCallbackPtr);
            else
				UltraStik_SetCallbacks32(UsbDeviceAttachedCallbackPtr, UsbDeviceRemovedCallbackPtr);
        }

		private void UsbDeviceAttachedCallback(int id)
        {
            m_deviceCount++;

			if (OnUsbDeviceAttached != null)
				m_ctrl.BeginInvoke(OnUsbDeviceAttached, id);
        }

		private void UsbDeviceRemovedCallback(int id)
        {
            m_deviceCount--;

			if (OnUsbDeviceRemoved != null)
				m_ctrl.BeginInvoke(OnUsbDeviceRemoved, id);
        }

        public int Initialize()
        {
			return m_deviceCount = (m_is64Bit ? UltraStik_Initialize64() : UltraStik_Initialize32());
        }

        public void Shutdown()
        {
			if (m_is64Bit)
				UltraStik_Shutdown64();
			else
				UltraStik_Shutdown32();
        }

		public int GetVendorId(int id)
		{
			return (m_is64Bit ? UltraStik_GetVendorId64(id) : UltraStik_GetVendorId32(id));
		}

		public int GetProductId(int id)
		{
			return (m_is64Bit ? UltraStik_GetProductId64(id) : UltraStik_GetProductId32(id));
		}

		public int GetVersionNumber(int id)
		{
			return (m_is64Bit ? UltraStik_GetVersionNumber64(id) : UltraStik_GetVersionNumber32(id));
		}

		public string GetVendorName(int id)
		{
			StringBuilder sb = new StringBuilder(256);

			if (m_is64Bit)
				UltraStik_GetVendorName64(id, sb);
			else
				UltraStik_GetVendorName32(id, sb);

			return sb.ToString();
		}

		public string GetProductName(int id)
		{
			StringBuilder sb = new StringBuilder(256);

			if (m_is64Bit)
				UltraStik_GetProductName64(id, sb);
			else
				UltraStik_GetProductName32(id, sb);

			return sb.ToString();
		}

		public string GetSerialNumber(int id)
		{
			StringBuilder sb = new StringBuilder(256);

			if (m_is64Bit)
				UltraStik_GetSerialNumber64(id, sb);
			else
				UltraStik_GetSerialNumber32(id, sb);

			return sb.ToString();
		}

		public string GetDevicePath(int id)
		{
			StringBuilder sb = new StringBuilder(256);

			if (m_is64Bit)
				UltraStik_GetDevicePath64(id, sb);
			else
				UltraStik_GetDevicePath32(id, sb);

			return sb.ToString();
		}

        public void SetRestrictor(int id, bool value)
        {
			if (m_is64Bit)
				UltraStik_SetRestrictor64(id, value);
			else
				UltraStik_SetRestrictor32(id, value);
        }

        public void SetFlash(int id, bool value)
        {
			if (m_is64Bit)
				UltraStik_SetFlash64(id, value);
			else
				UltraStik_SetFlash32(id, value);
        }

        public int GetUltraStikId(int id)
        {
			return (m_is64Bit ? UltraStik_GetUltraStikId64(id) : UltraStik_GetUltraStikId32(id));
        }

        public void SetUltraStikId(int id, UltraStikId value)
        {
			if (m_is64Bit)
				UltraStik_SetUltraStikId64(id, (int)value);
			else
				UltraStik_SetUltraStikId32(id, (int)value);
        }

        public int LoadMap(int id, string map)
        {
            StringBuilder sb = new StringBuilder(map);

			return (m_is64Bit ? UltraStik_LoadMap64(id ,sb) : UltraStik_LoadMap32(id, sb));
        }

        public int LoadMapFile(int id, string fileName)
        {
			StringBuilder sb = new StringBuilder(fileName);

			return (m_is64Bit ? UltraStik_LoadMapFile64(id, sb) : UltraStik_LoadMapFile32(id, sb));
        }

		public int LoadConfigFile(string mapPath, string fileName)
        {
            StringBuilder sbPath = new StringBuilder(mapPath);
			StringBuilder sbFileName = new StringBuilder(fileName);

			return (m_is64Bit ? UltraStik_LoadConfigFile64(sbPath, sbFileName) : UltraStik_LoadConfigFile32(sbPath, sbFileName));
        }

        public int NumDevices
        {
			get { return m_deviceCount; }
        }

		private bool Is64Bit()
		{
			return (Marshal.SizeOf(typeof(IntPtr)) == 8);
		}
    }
}
