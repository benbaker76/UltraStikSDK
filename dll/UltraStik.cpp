// ****************************************************************
// UltraStik.cpp
// Author: Ben Baker [headsoft.com.au]
// ****************************************************************
//
// The UltraStik 360 is a full-speed USB 2.0 composite device.  Communication
// to the UltraStik is done through Vendor Request Control Transfers.
//
// Notes:  
// 1. The UltraStik is a full-speed USB 2.0 (not high speed).
// 2. Firmware upgrade 1.7 to 1.9(Beta) or higher changes the device descriptors.  In 
//    Windows this is not recommended and the user should delete or uninstall all instances
//    of the UltraStik for proper operation.
//
// Ulimarc UltraStik VID = 0xD209
// Ulimarc UltraStik PID = 0x0501, 0x0502, 0x0503, 0x0504 (OLD)
// Ulimarc UltraStik PID = 0x0511, 0x0512, 0x0513, 0x0514 (NEW)
//
// Communication
//
// Control Transfer Types (bmRequesType, bRequest, wValue, Data,wLength)
// "Start Message" (0x43,0xE9,0x0001,No Data,0)
// "Data Write" (0x43,0xEB,0x0000,Data,(Size of Data))
// "Data Confirm" (0xC3,0xEA,0x0000,No Data,1) (returns 1 byte)
// "End Message" (0x43,0xE9,0x0000,No Data,0)
// 
// ************ General Transaction Format*************
// All Transactions follow this format.  The only difference
// is the # of "Data" Transfers and Confirms
//
// "Start Message" Transfer
// "Data Write" Transfer, MAX size of 32 Bytes per Transfer
// "Data Confirm" Transfer, follows each "Data Write" Transfer
//  repeat "Data Write" and "Data Confirm" for all Data.
// "End Message" Transfer
//
// ********************Change UltraStik ID Transfer*********************
// 1 "Data Write" Transfer of 32 Bytes
// First Byte determines UltraStik ID
// 0x51 = UltraStik Device #1
// 0x52 = UltraStik Device #2
// 0x53 = UltraStik Device #3
// 0x54 = UltraStik Device #4
//
// *********************Send Map Transfer*******************************
// This is only for Map Size = 9
//
// 3 "Data Write" Transfers of 32 Bytes
// -First Transfer-
// Byte[0] = 0x50 (Header)
// Byte[1] = 0x09 (Map Size?)
// Byte[2] = Restrictor (RestrictorOn = 0x09, RestrictorOff = 0x10)
// Byte[3]-Byte[10] = Map Border Location
// Byte[11]-Byte[31] = Mapping of Block Location. starting from topleft and read left to right.
//
// -Second Transfer-
// 32 Bytes - Continue Mapping of Block Location
//
// -Third Transfer-
// Byte[0]-Byte[27] - Continue Mapping of Block Location
// Byte[28]-Byte[30] - 0x00
// Byte[31] - (Write to Flash 0x00) (Write to RAM 0xFF, supported in Firmware 2.2)
//
//
// ****************************************************
// USB      Mapping
// 0x00 =   Analog (-)
// 0x01 =   Center (C)
// 0x02 =   Up (N)
// 0x03 =   UpRight (NE)
// 0x04 =   Right (E)
// 0x05 =   DownRight (SE)
// 0x06 =   Down (S)
// 0x07 =   DownLeft (SW)
// 0x08 =   Left (W)
// 0x09 =   UpLeft (NW)
// 0x0A =   Sticky (*)

#include "stdafx.h"
#include "UltraStik.h"
extern "C" {
#include "lusb0_usb.h"
#include "Include\hidsdi.h"
#include "Include\hid.h"
}
#include <uuids.h>
#include <tchar.h>
#include <stdio.h>
#include <wtypes.h>

// Header, Map Size, Restrictor
BYTE m_mapHeader[] =			{ MAP_HEADER,MAP_ROW_SIZE,RESTRICTOR_ON };

BYTE m_mapBorderLocations[] =	{ 0x1E,0x3A,0x56,0x72,0x8E,0xAA,0xC6,0xE2 };

// 2-Way, Vertical
BYTE m_vjoy2way[] =	{ 0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02,	// N,N,N,N,N,N,N,N,N
					0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02,	// N,N,N,N,N,N,N,N,N
					0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02,	// N,N,N,N,N,N,N,N,N
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,	// S,S,S,S,S,S,S,S,S
					0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,	// S,S,S,S,S,S,S,S,S
					0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06 };	// S,S,S,S,S,S,S,S,S

// 2-Way, Horizontal
BYTE m_joy2way[] =	{ 0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04 };	// W,W,W,C,C,C,E,E,E

// 4-Way
BYTE m_joy4way[] =	{ 0x0A,0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x0A,	// *,N,N,N,N,N,N,N,*
					0x08,0x0A,0x02,0x02,0x02,0x02,0x02,0x0A,0x04,	// W,*,N,N,N,N,N,*,E
					0x08,0x08,0x0A,0x02,0x02,0x02,0x0A,0x04,0x04,	// W,W,*,N,N,N,*,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x0A,0x06,0x06,0x06,0x0A,0x04,0x04,	// W,W,*,S,S,S,*,E,E
					0x08,0x0A,0x06,0x06,0x06,0x06,0x06,0x0A,0x04,	// W,*,S,S,S,S,S,*,E
					0x0A,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x0A };	// *,S,S,S,S,S,S,S,*

// 4-Way, No Sticky (UD Bias)
BYTE m_udbjoy4way[] =	{ 0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x02,	// N,N,N,N,N,N,N,N,N
						0x08,0x02,0x02,0x02,0x02,0x02,0x02,0x02,0x04,	// W,N,N,N,N,N,N,N,E
						0x08,0x08,0x02,0x02,0x02,0x02,0x02,0x04,0x04,	// W,W,N,N,N,N,N,E,E
						0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
						0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
						0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
						0x08,0x08,0x06,0x06,0x06,0x06,0x06,0x04,0x04,	// W,W,S,S,S,S,S,E,E
						0x08,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x04,	// W,S,S,S,S,S,S,S,E
						0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06 };	// S,S,S,S,S,S,S,S,S

// 4-Way, Diagonals Only
BYTE m_djoy4way[] =	{ 0x09,0x09,0x09,0x09,0x0A,0x03,0x03,0x03,0x03,	// NW,NW,NW,NW,*,NE,NE,NE,NE
					0x09,0x09,0x09,0x09,0x0A,0x03,0x03,0x03,0x03,	// NW,NW,NW,NW,*,NE,NE,NE,NE
					0x09,0x09,0x09,0x09,0x01,0x03,0x03,0x03,0x03,	// NW,NW,NW,NW,C,NE,NE,NE,NE
					0x09,0x09,0x09,0x01,0x01,0x01,0x03,0x03,0x03,	// NW,NW,NW,C,C,C,NE,NE,NE
					0x0A,0x0A,0x01,0x01,0x01,0x01,0x01,0x0A,0x0A,	// *,*,C,C,C,C,C,*,*
					0x07,0x07,0x07,0x01,0x01,0x01,0x05,0x05,0x05,	// SW,SW,SW,C,C,C,SE,SE,SE
					0x07,0x07,0x07,0x07,0x01,0x05,0x05,0x05,0x05,	// SW,SW,SW,SW,C,SE,SE,SE,SE
					0x07,0x07,0x07,0x07,0x0A,0x05,0x05,0x05,0x05,	// SW,SW,SW,SW,*,SE,SE,SE,SE
					0x07,0x07,0x07,0x07,0x0A,0x05,0x05,0x05,0x05 };	// SW,SW,SW,SW,*,SE,SE,SE,SE

// 4-Way, Rotated Diagonals
BYTE m_rdjoy4way[] =	{ 0x08,0x08,0x08,0x08,0x0A,0x02,0x02,0x02,0x02,	// W,W,W,W,*,N,N,N,N
						0x08,0x08,0x08,0x08,0x0A,0x02,0x02,0x02,0x02,	// W,W,W,W,*,N,N,N,N
						0x08,0x08,0x08,0x08,0x01,0x02,0x02,0x02,0x02,	// W,W,W,W,C,N,N,N,N
						0x08,0x08,0x08,0x01,0x01,0x01,0x02,0x02,0x02,	// W,W,W,C,C,C,N,N,N
						0x0A,0x0A,0x01,0x01,0x01,0x01,0x01,0x0A,0x0A,	// *,*,C,C,C,C,C,*,*
						0x06,0x06,0x06,0x01,0x01,0x01,0x04,0x04,0x04,	// S,S,S,C,C,C,E,E,E
						0x06,0x06,0x06,0x06,0x01,0x04,0x04,0x04,0x04,	// S,S,S,S,C,E,E,E,E
						0x06,0x06,0x06,0x06,0x0A,0x04,0x04,0x04,0x04,	// S,S,S,S,*,E,E,E,E
						0x06,0x06,0x06,0x06,0x0A,0x04,0x04,0x04,0x04 };	// S,S,S,S,*,E,E,E,E

// 8-Way
BYTE m_joy8way[] =	{ 0x09,0x09,0x09,0x02,0x02,0x02,0x03,0x03,0x03,	// NW,NW,NW,N,N,N,NE,NE,NE
					0x09,0x09,0x09,0x02,0x02,0x02,0x03,0x03,0x03,	// NW,NW,NW,N,N,N,NE,NE,NE
					0x09,0x09,0x09,0x02,0x02,0x02,0x03,0x03,0x03,	// NW,NW,NW,N,N,N,NE,NE,NE
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
					0x07,0x07,0x07,0x06,0x06,0x06,0x05,0x05,0x05,	// SW,SW,SW,S,S,S,SE,SE,SE
					0x07,0x07,0x07,0x06,0x06,0x06,0x05,0x05,0x05,	// SW,SW,SW,S,S,S,SE,SE,SE
					0x07,0x07,0x07,0x06,0x06,0x06,0x05,0x05,0x05 };	// SW,SW,SW,S,S,S,SE,SE,SE

// 8-Way, Easy Diagonals
BYTE m_easyjoy8way[] =	{ 0x09,0x09,0x0A,0x02,0x02,0x02,0x0A,0x03,0x03,	// NW,NW,*,N,N,N,*,NE,NE
						0x09,0x09,0x09,0x02,0x02,0x02,0x03,0x03,0x03,	// NW,NW,NW,N,N,N,NE,NE,NE
						0x0A,0x09,0x09,0x0A,0x02,0x0A,0x03,0x03,0x0A,	// *,NW,NW,*,N,*,NE,NE,*
						0x08,0x08,0x0A,0x09,0x01,0x03,0x0A,0x04,0x04,	// W,W,*,NW,C,NE,*,E,E
						0x08,0x08,0x08,0x01,0x01,0x01,0x04,0x04,0x04,	// W,W,W,C,C,C,E,E,E
						0x08,0x08,0x0A,0x07,0x01,0x05,0x0A,0x04,0x04,	// W,W,*,SW,C,SE,*,E,E
						0x0A,0x07,0x07,0x0A,0x06,0x0A,0x05,0x05,0x0A,	// *,SW,SW,*,S,*,SE,SE,*
						0x07,0x07,0x07,0x06,0x06,0x06,0x05,0x05,0x05,	// SW,SW,SW,S,S,S,SE,SE,SE
						0x07,0x07,0x0A,0x06,0x06,0x06,0x0A,0x05,0x05 };	// SW,SW,*,S,S,S,*,SE,SE

// Analog
BYTE m_analog[] =	{ 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,	// -,-,-,-,-,-,-,-,-
					0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,	// -,-,-,-,-,-,-,-,-
					0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,	// -,-,-,-,-,-,-,-,-
					0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,	// -,-,-,-,-,-,-,-,-
					0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,	// -,-,-,-,-,-,-,-,-
					0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,	// -,-,-,-,-,-,-,-,-
					0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,	// -,-,-,-,-,-,-,-,-
					0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,	// -,-,-,-,-,-,-,-,-
					0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00 };	// -,-,-,-,-,-,-,-,-

// Mouse Pointer
BYTE m_mouse[] =	{ 0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,	// C,C,C,C,C,C,C,C,C
					0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01 };	// C,C,C,C,C,C,C,C,C

MapInfo m_mapInfo[] =
{
	{ "vjoy2way",		m_vjoy2way },		// 2-Way, Vertical
	{ "joy2way",		m_joy2way },		// 2-Way, Horizontal
	{ "joy4way",		m_joy4way },		// 4-Way
	{ "udbjoy4way",		m_udbjoy4way },		// 4-Way, No Sticky (UD Bias)
	{ "djoy4way",		m_djoy4way },		// 4-Way, Diagonals Only
	{ "rdjoy4way",		m_rdjoy4way },		// 4-Way, Rotated Diagonals
	{ "joy8way",		m_joy8way },		// 8-Way
	{ "easyjoy8way",	m_easyjoy8way },	// 8-Way, Easy Diagonals
	{ "analog",			m_analog },			// Analog
	{ "mouse",			m_mouse }			// Mouse Pointer
};

HINSTANCE m_hInstance = NULL;
HANDLE m_hThread = NULL;
HWND m_hWnd = NULL;
HANDLE m_hStopEvent = NULL;
CRITICAL_SECTION m_crSection;
HID_DEVICE_DATA m_hidDeviceData[16] = { NULL };
INT m_deviceCount = 0;
USBDEVICE_ATTACHED_CALLBACK m_usbDeviceAttachedCallback = NULL;
USBDEVICE_REMOVED_CALLBACK m_usbDeviceRemovedCallback = NULL;

BOOL APIENTRY DllMain( HANDLE hModule, 
					  DWORD  fdwReason, 
					  LPVOID lpReserved
					  )
{
	DWORD dwThreadId = NULL, dwThrdParam = 1;

	switch (fdwReason)
	{
	case DLL_PROCESS_ATTACH:
		m_hInstance = (HINSTANCE) hModule;
		DisableThreadLibraryCalls(m_hInstance);

		InitializeCriticalSection(&m_crSection);
		m_hStopEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

		m_hThread = CreateThread(NULL, 0, EventWindowThread, &dwThrdParam, 0, &dwThreadId);
		break;
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
		break;
	case DLL_PROCESS_DETACH:
		SendMessage(m_hWnd, WM_CLOSE, 0, 0);
		break;
	}

	return TRUE;
}

VOID RemoveNewDriver()
{
	for(INT i = 0; i < m_deviceCount; i++)
	{
		if(!IS_NEWDRIVER(m_hidDeviceData[i].UsagePage, m_hidDeviceData[i].Usage))
			continue;

		for(INT j = 0; j < m_deviceCount; j++)
		{
			if(m_hidDeviceData[i].ProductID != m_hidDeviceData[j].ProductID)
				continue;

			if(!IS_OLDDRIVER(m_hidDeviceData[j].UsagePage, m_hidDeviceData[j].Usage))
				continue;

			m_hidDeviceData[j].pHidDeviceData = (PHID_DEVICE_DATA)malloc(sizeof(HID_DEVICE_DATA));
			memcpy(m_hidDeviceData[j].pHidDeviceData, &m_hidDeviceData[i], sizeof(HID_DEVICE_DATA));

			for(INT k = i; k < m_deviceCount - 1; k++)
				m_hidDeviceData[k] = m_hidDeviceData[k + 1];
			
			m_deviceCount--;

			break;
		}
	}
}

VOID SortDevices()
{
	for(INT i = 0; i < m_deviceCount; i++)
	{
		for(INT j = 0; j < i; j++)
		{
			if(m_hidDeviceData[i].ProductID < m_hidDeviceData[j].ProductID)
			{
				HID_DEVICE_DATA temp = m_hidDeviceData[i];
				m_hidDeviceData[i] = m_hidDeviceData[j];
				m_hidDeviceData[j] = temp;
			}
		}
	}
}

VOID OutputDevice(INT id, PHID_DEVICE_DATA pHidDeviceData)
{
	DEBUGLOG(L"Id: %d", id);
	DEBUGLOG(L"HID Handle: %08x", pHidDeviceData->hDevice);
	DEBUGLOG(L"VendorID: %04x", pHidDeviceData->VendorID);
	DEBUGLOG(L"ProductID: %04x", pHidDeviceData->ProductID);
	DEBUGLOG(L"VersionNumber: %04x", pHidDeviceData->VersionNumber);
	DEBUGLOG(L"VendorName: %s", pHidDeviceData->VendorName);
	DEBUGLOG(L"ProductName: %s", pHidDeviceData->ProductName);
	DEBUGLOG(L"SerialNumber: %s", pHidDeviceData->SerialNumber);
	DEBUGLOG(L"DevicePath: %s", pHidDeviceData->DevicePath);
	DEBUGLOG(L"InputReportByteLength: %d", pHidDeviceData->InputReportLen);
	DEBUGLOG(L"OutputReportByteLength: %d", pHidDeviceData->OutputReportLen);
	DEBUGLOG(L"UsagePage: %d", pHidDeviceData->UsagePage);
	DEBUGLOG(L"Usage: %d", pHidDeviceData->Usage);
}

VOID OutputDevices()
{
	DEBUGLOG(L"===============================");
	DEBUGLOG(L"DEVICE LIST");

	for(INT i = 0; i < m_deviceCount; i++)
	{
		DEBUGLOG(L"===============================");

		OutputDevice(i, &m_hidDeviceData[i]);
		
		if(m_hidDeviceData[i].pHidDeviceData != NULL)
		{
			DEBUGLOG(L"========= SUB DEVICE ==========");

			OutputDevice(i, m_hidDeviceData[i].pHidDeviceData);
		}
	}

	DEBUGLOG(L"===============================");
}

ULTRASTIK_API VOID __stdcall UltraStik_SetCallbacks(USBDEVICE_ATTACHED_CALLBACK usbDeviceAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbDeviceRemovedCallback)
{
	m_usbDeviceAttachedCallback = usbDeviceAttachedCallback;
	m_usbDeviceRemovedCallback = usbDeviceRemovedCallback;
}

INT UltraStik_Initialize_Old()
{
	//usb_set_debug(255);
	usb_init();
	usb_find_busses();
	usb_find_devices();

	struct usb_bus *bus = NULL;
	struct usb_device *dev = NULL;
	usb_dev_handle *udev = NULL;

	CHAR sManufacturer[256];
	CHAR sProduct[256];
	CHAR sSerialNumber[256];
	INT ret;

	for (bus = usb_busses; bus; bus = bus->next)
	{
		for (dev = bus->devices; dev; dev = dev->next)
		{
			if(dev->descriptor.idVendor != VID_ULTIMARC || (dev->descriptor.idProduct & ~0x7) != PID_ULTRASTIK_OLD)
				continue;

			udev = usb_open(dev);

			if (!udev)
				continue;

			m_hidDeviceData[m_deviceCount].hDevice = udev;
			m_hidDeviceData[m_deviceCount].VendorID = dev->descriptor.idVendor;
			m_hidDeviceData[m_deviceCount].ProductID = dev->descriptor.idProduct;
			m_hidDeviceData[m_deviceCount].VersionNumber = dev->descriptor.bcdDevice;

			if (dev->descriptor.iManufacturer)
				ret = usb_get_string_simple(udev, dev->descriptor.iManufacturer, sManufacturer, sizeof(sManufacturer));

			if (dev->descriptor.iProduct)
				ret = usb_get_string_simple(udev, dev->descriptor.iProduct, sProduct, sizeof(sProduct));

			if (dev->descriptor.iSerialNumber)
				ret = usb_get_string_simple(udev, dev->descriptor.iSerialNumber, sSerialNumber, sizeof(sSerialNumber));

			MultiByteToWideChar(CP_ACP, 0, sManufacturer, -1, (LPWSTR)m_hidDeviceData[m_deviceCount].VendorName, 256);
			MultiByteToWideChar(CP_ACP, 0, sProduct, -1, (LPWSTR)m_hidDeviceData[m_deviceCount].ProductName, 256);
			MultiByteToWideChar(CP_ACP, 0, sSerialNumber, -1, (LPWSTR)m_hidDeviceData[m_deviceCount].SerialNumber, 256);

			m_hidDeviceData[m_deviceCount].Restrictor = RESTRICTOR_ON;
			m_hidDeviceData[m_deviceCount].Write = WRITE_RAM;
			m_hidDeviceData[m_deviceCount].IsNew = FALSE;

			m_deviceCount++;
		}
	}

	SortDevices();

	return m_deviceCount;
}

VOID UltraStik_Shutdown_Old()
{
	for(int i = 0; i < m_deviceCount; i++)
	{
		if(m_hidDeviceData[i].IsNew)
			continue;

		usb_close((usb_dev_handle *)m_hidDeviceData[i].hDevice);
	}
}

ULTRASTIK_API INT __stdcall UltraStik_Initialize()
{
	struct _GUID hidGuid;
	SP_DEVICE_INTERFACE_DATA deviceInterfaceData;
	struct { DWORD cbSize; WCHAR DevicePath[256]; } FunctionClassDeviceData;
	INT success;
	DWORD hidDevice;
	HANDLE pnPHandle;
	ULONG bytesReturned;

	m_deviceCount = 0;

	UltraStik_Initialize_Old();

	HidD_GetHidGuid(&hidGuid);

	pnPHandle = SetupDiGetClassDevs(&hidGuid, 0, 0, 0x12);

	if ((INT) pnPHandle == -1)
		return 0;

	for (hidDevice = 0; hidDevice < 127; hidDevice++)
	{
		deviceInterfaceData.cbSize = sizeof(SP_DEVICE_INTERFACE_DATA);

		success = SetupDiEnumDeviceInterfaces(pnPHandle, 0, &hidGuid, hidDevice, &deviceInterfaceData);

		if (success == 1)
		{
			FunctionClassDeviceData.cbSize = sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA);
			success = SetupDiGetDeviceInterfaceDetail(pnPHandle, &deviceInterfaceData, (PSP_DEVICE_INTERFACE_DETAIL_DATA) &FunctionClassDeviceData, sizeof(FunctionClassDeviceData), &bytesReturned, 0);

			if (success == 0)
				continue;

			if(UsbOpen(FunctionClassDeviceData.DevicePath, &m_hidDeviceData[m_deviceCount]))
				m_deviceCount++;
		}
	}

	SetupDiDestroyDeviceInfoList(pnPHandle);

	RemoveNewDriver();
	SortDevices();
	
#ifdef DEBUG_OUTPUT
	OutputDevices();
#endif

	DEBUGLOG(L"%d UltraStik's Found\n", m_deviceCount);

	return m_deviceCount;
}

ULTRASTIK_API VOID __stdcall UltraStik_Shutdown()
{
	INT i;

	UltraStik_Shutdown_Old();

	for(i = 0; i < m_deviceCount; i++)
	{
		if(!m_hidDeviceData[i].IsNew)
			continue;

		DEBUGLOG(L"Closing HID Handle: %08x\n", m_hidDeviceData[i].hDevice);

		CloseHandle(m_hidDeviceData[i].hDevice);

		if(m_hidDeviceData[i].pHidDeviceData != NULL)
		{
			DEBUGLOG(L"Sub Device...");
			DEBUGLOG(L"Closing HID Handle: %08x\n", m_hidDeviceData[i].pHidDeviceData->hDevice);

			CloseHandle(m_hidDeviceData[i].pHidDeviceData->hDevice);

			free(m_hidDeviceData[i].pHidDeviceData);
		}
	}
}

ULTRASTIK_API INT __stdcall UltraStik_GetVendorId(int id)
{
	return m_hidDeviceData[id].VendorID;
}

ULTRASTIK_API INT __stdcall UltraStik_GetProductId(int id)
{
	return m_hidDeviceData[id].ProductID;
}

ULTRASTIK_API INT __stdcall UltraStik_GetVersionNumber(INT id)
{
	return m_hidDeviceData[id].VersionNumber;
}

ULTRASTIK_API VOID __stdcall UltraStik_GetVendorName(INT id, PWCHAR sVendorName)
{
	wcscpy(sVendorName, m_hidDeviceData[id].VendorName);
}

ULTRASTIK_API VOID __stdcall UltraStik_GetProductName(INT id, PWCHAR sProductName)
{
	wcscpy(sProductName, m_hidDeviceData[id].ProductName);
}

ULTRASTIK_API VOID __stdcall UltraStik_GetSerialNumber(INT id, PWCHAR sSerialNumber)
{
	wcscpy(sSerialNumber, m_hidDeviceData[id].SerialNumber);
}

ULTRASTIK_API VOID __stdcall UltraStik_GetDevicePath(INT id, PWCHAR sDevicePath)
{
	wcscpy(sDevicePath, m_hidDeviceData[id].DevicePath);
}

ULTRASTIK_API VOID __stdcall UltraStik_SetRestrictor(INT id, BOOL value)
{
	(value ? m_hidDeviceData[id].Restrictor = RESTRICTOR_ON : RESTRICTOR_OFF);
}

ULTRASTIK_API VOID __stdcall UltraStik_SetFlash(INT id, BOOL value)
{
	(value ? m_hidDeviceData[id].Write = WRITE_FLASH : WRITE_RAM);
}

ULTRASTIK_API INT __stdcall UltraStik_GetUltraStikId(INT id)
{
	return m_hidDeviceData[id].ProductID;
}

ULTRASTIK_API VOID __stdcall UltraStik_SetUltraStikId(INT id, INT value)
{
	BYTE buffer[32];

	memset(buffer, 0, sizeof(buffer));

	buffer[0] = MAP_HEADER + (value + 1); // Id

	if(m_hidDeviceData[id].IsNew)
	{
		SendData(id, buffer, 4);
	}
	else
	{
		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0x43, 0xE9, 0x0001, 0, NULL, 0, TIME_OUT);				//Start Message Transfer

		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0x43, 0xEB, 0x0000, 0, (PCHAR) buffer, 32, TIME_OUT);	//Data Write Transfer
		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0xC3, 0xEA, 0x0000, 0, NULL, 1, TIME_OUT);				//Data Confirm Transfer

		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0x43, 0xE9, 0x0000, 0, NULL, 0, TIME_OUT);				//End Message Transfer
	}

error:

	return;
}

ULTRASTIK_API INT __stdcall UltraStik_LoadMap(INT id, PCHAR map)
{
	INT retVal = 0;
	BYTE buffer[96];

	memset(buffer, 0, 96);

	memcpy(&buffer[0], m_mapHeader, sizeof(m_mapHeader));
	memcpy(&buffer[sizeof(m_mapHeader)], m_mapBorderLocations, sizeof(m_mapBorderLocations));
	INT mapOffset = (sizeof(m_mapHeader) + sizeof(m_mapBorderLocations));
	INT mapSize = (MAP_ROW_SIZE * MAP_ROW_SIZE);

	for(int i = 0; i < sizeof(m_mapInfo) / sizeof(m_mapInfo[0]); i++)
	{
		if(strcmp(map, m_mapInfo[i].Name) == 0)
			memcpy(&buffer[mapOffset], m_mapInfo[i].Data, mapSize);
	}

	retVal = SendMap(id, buffer);

error:

	return retVal;
}

/* ULTRASTIK_API INT __stdcall UltraStik_LoadMap(INT id, MapName map)
{
	return UltraStik_LoadMap(id, m_mapInfo[map].Name);
} */

ULTRASTIK_API INT __stdcall UltraStik_LoadMapFile(INT id, PCHAR fileName)
{
	INT retVal = 0;
	BYTE buffer[96];

	FILE *pFile = fopen(fileName, "r");

	if (pFile == NULL)
		goto error;

	memset(buffer, 0, sizeof(buffer));

	memcpy(&buffer[0], m_mapHeader, sizeof(m_mapHeader));
	memcpy(&buffer[sizeof(m_mapHeader)], m_mapBorderLocations, sizeof(m_mapBorderLocations));
	INT mapOffset = sizeof(m_mapHeader) + sizeof(m_mapBorderLocations);
	INT mapRowSize = MAP_ROW_SIZE;

	CHAR line[256];

	while (fgets(line, sizeof line, pFile) != NULL)
	{
		char *newLine = strchr(line, '\n');

		if (newLine != NULL)
			*newLine = '\0';

		INT map = -1;
		PCHAR pch = strtok(line, "=");

		while(pch != NULL)
		{
			if(map == -1)
			{
				if(strcmp(pch, "MapFileFormatVersion") == 0)
					map = MAP_FILEFORMATVERSION;
				else if(strcmp(pch, "MapSize") == 0)
					map = MAP_SIZE;
				else if(strcmp(pch, "MapBorderLocations") == 0)
					map = MAP_BORDERLOCATIONS;
				else if(strcmp(pch, "MapRow1") == 0)
					map = MAP_ROW1;
				else if(strcmp(pch, "MapRow2") == 0)
					map = MAP_ROW2;
				else if(strcmp(pch, "MapRow3") == 0)
					map = MAP_ROW3;
				else if(strcmp(pch, "MapRow4") == 0)
					map = MAP_ROW4;
				else if(strcmp(pch, "MapRow5") == 0)
					map = MAP_ROW5;
				else if(strcmp(pch, "MapRow6") == 0)
					map = MAP_ROW6;
				else if(strcmp(pch, "MapRow7") == 0)
					map = MAP_ROW7;
				else if(strcmp(pch, "MapRow8") == 0)
					map = MAP_ROW8;
				else if(strcmp(pch, "MapRow9") == 0)
					map = MAP_ROW9;
			}
			else
			{
				switch(map)
				{
				case MAP_FILEFORMATVERSION:
					if(strcmp(pch, "1.0") != 0)
						goto error;
					break;
				case MAP_SIZE:
					if(strcmp(pch, "9") != 0)
						goto error;
					break;
				case MAP_BORDERLOCATIONS:
					GetBorderValues(pch, &buffer[sizeof(m_mapHeader)]);
					break;
				case MAP_ROW1:
					{
						GetRow(pch, &buffer[mapOffset]);
						break;
					}
				case MAP_ROW2:
					{
						GetRow(pch, &buffer[mapOffset + mapRowSize]);
						break;
					}
				case MAP_ROW3:
					{
						GetRow(pch, &buffer[mapOffset + (mapRowSize * 2)]);
						break;
					}
				case MAP_ROW4:
					{
						GetRow(pch, &buffer[mapOffset + (mapRowSize * 3)]);
						break;
					}
				case MAP_ROW5:
					{
						GetRow(pch, &buffer[mapOffset + (mapRowSize * 4)]);
						break;
					}
				case MAP_ROW6:
					{
						GetRow(pch, &buffer[mapOffset + (mapRowSize * 5)]);
						break;
					}
				case MAP_ROW7:
					{
						GetRow(pch, &buffer[mapOffset + (mapRowSize * 6)]);
						break;
					}
				case MAP_ROW8:
					{
						GetRow(pch, &buffer[mapOffset + (mapRowSize * 7)]);
						break;
					}
				case MAP_ROW9:
					{
						GetRow(pch, &buffer[mapOffset + (mapRowSize * 8)]);
						break;
					}
				}
			}

			pch = strtok(NULL, "=");
		}
	}

	retVal = SendMap(id, buffer);

error:

	fclose (pFile);

	return retVal;
}

ULTRASTIK_API INT __stdcall UltraStik_LoadConfigFile(PCHAR mapPath, PCHAR fileName)
{
	INT retVal = 0;

	FILE *pFile = fopen(fileName, "r");

	if (pFile == NULL)
		goto error;

	CHAR line[256];
	CHAR buf[512];

	while (fgets(line, sizeof line, pFile) != NULL)
	{
		PCHAR newLine = strchr(line, '\n');

		if (newLine != NULL)
			*newLine = '\0';

		INT config = -1;
		PCHAR pch = strtok(line, "=");

		while(pch != NULL)
		{
			if(config == -1)
			{
				if(strcmp(pch, "UltraStikGameConfigurationFileFormatVersion") == 0)
					config = CONFIG_FILEFORMATVERSION;
				else if(strcmp(pch, "UltraStik1MapFile") == 0)
					config = CONFIG_MAP1;
				else if(strcmp(pch, "UltraStik2MapFile") == 0)
					config = CONFIG_MAP2;
				else if(strcmp(pch, "UltraStik3MapFile") == 0)
					config = CONFIG_MAP3;
				else if(strcmp(pch, "UltraStik4MapFile") == 0)
					config = CONFIG_MAP4;
			}
			else
			{
				switch(config)
				{
				case CONFIG_FILEFORMATVERSION:
					if(strcmp(pch, "1.0") != 0)
						goto error;
					break;
				case CONFIG_MAP1:
					if(m_deviceCount > 0)
					{
						retVal = UltraStik_LoadMap(0, pch);

						if(retVal == 0)
						{
							sprintf(buf, "%s\\%s.um", mapPath, pch);

							retVal = UltraStik_LoadMapFile(0, buf);
						}
					}
					break;
				case CONFIG_MAP2:
					if(m_deviceCount > 1)
					{
						retVal = UltraStik_LoadMap(1, pch);

						if(retVal == 0)
						{
							sprintf(buf, "%s\\%s.um", mapPath, pch);

							retVal = UltraStik_LoadMapFile(1, buf);
						}
					}
					break;
				case CONFIG_MAP3:
					if(m_deviceCount > 2)
					{
						retVal = UltraStik_LoadMap(2, pch);

						if(retVal == 0)
						{
							sprintf(buf, "%s\\%s.um", mapPath, pch);

							retVal = UltraStik_LoadMapFile(2, buf);
						}
					}
					break;
				case CONFIG_MAP4:
					if(m_deviceCount > 3)
					{
						retVal = UltraStik_LoadMap(3, pch);

						if(retVal == 0)
						{
							sprintf(buf, "%s\\%s.um", mapPath, pch);

							retVal = UltraStik_LoadMapFile(3, buf);
						}
					}
					break;
				}
			}

			pch = strtok(NULL, "=");
		}
	}

error:

	fclose (pFile);

	return retVal;
}

void GetBorderValues(PCHAR line, PBYTE out)
{
	PCHAR pch = strtok(line, ",");

	while(pch != NULL)
	{
		*out++ = atoi(pch);

		pch = strtok(NULL, ",");
	}
}

void GetRow(PCHAR line, PBYTE out)
{
	PCHAR pch = strtok(line, ",");

	while(pch != NULL)
	{
		*out++ = Str2Byte(pch);

		pch = strtok(NULL, ",");
	}
}

BYTE Str2Byte(PCHAR str)
{
	if(strcmp(str, "-") == 0)
		return MAP_ANALOG;
	else if(strcmp(str, "C") == 0)
		return MAP_CENTER;
	else if(strcmp(str, "N") == 0)
		return MAP_N;
	else if(strcmp(str, "NE") == 0)
		return MAP_NE;
	else if(strcmp(str, "E") == 0)
		return MAP_E;
	else if(strcmp(str, "SE") == 0)
		return MAP_SE;
	else if(strcmp(str, "S") == 0)
		return MAP_S;
	else if(strcmp(str, "SW") == 0)
		return MAP_SW;
	else if(strcmp(str, "W") == 0)
		return MAP_W;
	else if(strcmp(str, "NW") == 0)
		return MAP_NW;
	else if(strcmp(str, "*") == 0)
		return MAP_STICKY;

	return NULL;
}

INT SendMap(INT id, PBYTE pBuffer)
{
	pBuffer[0] = MAP_HEADER;		// Header
	pBuffer[1] = MAP_ROW_SIZE;		// Map Size
	pBuffer[2] = m_hidDeviceData[id].Restrictor;	// Restrictor ON or OFF
	pBuffer[95] = m_hidDeviceData[id].Write;		// RAM or Flash

	if(m_hidDeviceData[id].IsNew)
	{
		SendData(id, &pBuffer[0], 96);
	}
	else
	{
		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0x43, 0xE9, 0x0001, 0, NULL, 0, TIME_OUT);					//Start Message Transfer

		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0x43, 0xEB, 0x0000, 0, (PCHAR) &pBuffer[0], 32, TIME_OUT);	//Data Write Transfer
		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0xC3, 0xEA, 0x0000, 0, NULL, 1, TIME_OUT);					//Data Confirm Transfer

		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0x43, 0xEB, 0x0000, 0, (PCHAR) &pBuffer[32], 32, TIME_OUT);	//Data Write Transfer
		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0xC3, 0xEA, 0x0000, 0, NULL, 1, TIME_OUT);					//Data Confirm Transfer

		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0x43, 0xEB, 0x0000, 0, (PCHAR) &pBuffer[64], 32, TIME_OUT);	//Data Write Transfer
		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0xC3, 0xEA, 0x0000, 0, NULL, 1, TIME_OUT);					//Data Confirm Transfer

		usb_control_msg((usb_dev_handle *)m_hidDeviceData[id].hDevice, 0x43, 0xE9, 0x0000, 0, NULL, 0, TIME_OUT);					//End Message Transfer
	}

	return 1;
}

BOOL SendData(INT id, PBYTE pBuffer, INT bufferSize)
{
	REPORT_BUF outputReport;
	BOOL retVal = FALSE;

	if(m_hidDeviceData[id].pHidDeviceData == NULL)
		return FALSE;

	outputReport.ReportID = 0;

	for (int i = 0; i < bufferSize; i += 4)
	{
		outputReport.ReportBuffer[0] = pBuffer[i];
		outputReport.ReportBuffer[1] = pBuffer[i + 1];
		outputReport.ReportBuffer[2] = pBuffer[i + 2];
		outputReport.ReportBuffer[3] = pBuffer[i + 3];

		retVal = UsbWrite(m_hidDeviceData[id].pHidDeviceData, &outputReport);
	}

	return retVal;
}

BOOL UsbOpen(LPCWSTR devicePath, PHID_DEVICE_DATA pDeviceData)
{
	HANDLE hidHandle;
	USHORT vendorID, productID, versionNumber;
	USHORT inputReportLen, outputReportLen;
	USHORT usagePage, usage;
	WCHAR tempPath[256];
	INT success;

	wcscpy_s(tempPath, devicePath);

	strlow(tempPath);

	hidHandle = CreateFile(tempPath, GENERIC_WRITE | GENERIC_READ, FILE_SHARE_WRITE | FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL);

	// Open device as non-overlapped so we can get data
	//hidHandle = CreateFile(tempPath, GENERIC_WRITE | GENERIC_READ, FILE_SHARE_WRITE | FILE_SHARE_READ, NULL, OPEN_EXISTING, 0, NULL);

	if (hidHandle == INVALID_HANDLE_VALUE)
		return FALSE;

	if(GetDeviceInfo(hidHandle, vendorID, productID, versionNumber, usage, usagePage, inputReportLen, outputReportLen))
	{
		if(vendorID == VID_ULTIMARC && (productID & ~0x7) == PID_ULTRASTIK_NEW)
		{
			wcscpy_s(pDeviceData->DevicePath, tempPath);

			pDeviceData->hDevice = hidHandle;
			pDeviceData->VendorID = vendorID;
			pDeviceData->ProductID = productID;
			pDeviceData->VersionNumber = versionNumber;
			pDeviceData->InputReportLen = inputReportLen;
			pDeviceData->OutputReportLen = outputReportLen;
			pDeviceData->UsagePage = usagePage;
			pDeviceData->Usage = usage;
			pDeviceData->pHidDeviceData = NULL;
			pDeviceData->Restrictor = RESTRICTOR_ON;
			pDeviceData->Write = WRITE_RAM;
			pDeviceData->IsNew = TRUE;

			HidD_GetManufacturerString(hidHandle, pDeviceData->VendorName, sizeof(pDeviceData->VendorName));
			HidD_GetProductString(hidHandle, pDeviceData->ProductName, sizeof(pDeviceData->ProductName));
			HidD_GetSerialNumberString(hidHandle, pDeviceData->SerialNumber, sizeof(pDeviceData->SerialNumber));

			//DEBUGLOG(L"Error: %x\n", GetLastError());

			//OutputDevice(pDeviceData);

			return TRUE;
		}
	}

	CloseHandle(hidHandle);

	return FALSE;
}

BOOL UsbRead(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport)
{
	return UsbRead(pHidDeviceData, pInputReport, INFINITE);
}

BOOL UsbRead(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport, DWORD timeOut)
{
	OVERLAPPED ol;
	DWORD cbRet = 0;
	BOOL bRet = FALSE;

	memset(&ol, 0, sizeof(ol));
	ol.hEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

	EnterCriticalSection(&m_crSection);
	ResetEvent(ol.hEvent);

	bRet = ReadFile(pHidDeviceData->hDevice, pInputReport, pHidDeviceData->InputReportLen, &cbRet, &ol);
	LeaveCriticalSection(&m_crSection);

	if (!bRet)
	{
		if (GetLastError() == ERROR_IO_PENDING)
		{
			HANDLE handles[2] = { ol.hEvent, m_hStopEvent };
			DWORD waitRet = WaitForMultipleObjects(2, handles, FALSE, timeOut);

			if (waitRet == WAIT_OBJECT_0)
			{
				// Data came in
				bRet = GetOverlappedResult(pHidDeviceData->hDevice, &ol, &cbRet, TRUE);
			}
			else if (waitRet == (WAIT_OBJECT_0 + 1))
			{
				// Stop event was set
				ResetEvent(m_hStopEvent);

				bRet = FALSE;
			}
			else if (waitRet == WAIT_TIMEOUT)
			{
				bRet = FALSE;
			}
		}
	}

	CloseHandle(ol.hEvent);

	return bRet;
}

BOOL UsbWrite(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pOutputReport)
{
	return UsbWrite(pHidDeviceData, pOutputReport, INFINITE);
}

BOOL UsbWrite(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pOutputReport, DWORD timeOut)
{
	OVERLAPPED ol;
	DWORD cbRet = 0;
	BOOL bRet = FALSE;

	/* for(int i=0; i<size; i++)
	{
		DEBUGLOG(L"%02x", pOutputReport->ReportBuffer[i]);
	} */

	memset(&ol, 0, sizeof(ol));
	ol.hEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

	EnterCriticalSection(&m_crSection);
	ResetEvent(ol.hEvent);

	bRet = WriteFile(pHidDeviceData->hDevice, pOutputReport, pHidDeviceData->OutputReportLen, &cbRet, &ol);
	LeaveCriticalSection(&m_crSection);

	if (!bRet)
	{
		if (GetLastError() == ERROR_IO_PENDING)
		{
			HANDLE handles[2] = { ol.hEvent, m_hStopEvent };
			DWORD waitRet = WaitForMultipleObjects(2, handles, FALSE, timeOut);

			if (waitRet == WAIT_OBJECT_0)
			{
				// Data came in
				bRet = GetOverlappedResult(pHidDeviceData->hDevice, &ol, &cbRet, TRUE);
			}
			else if (waitRet == (WAIT_OBJECT_0 + 1))
			{
				// Stop event was set
				ResetEvent(m_hStopEvent);

				bRet = FALSE;
			}
			else if (waitRet == WAIT_TIMEOUT)
			{
				bRet = FALSE;
			}
		}
	}

	CloseHandle(ol.hEvent);

	return bRet;
}

DWORD WINAPI EventWindowThread(LPVOID lpParam)
{
	INT exitcode = 1;
	WNDCLASS wc = { 0 };

	wc.lpszClassName 	= WINDOW_CLASS;
	wc.hInstance 		= m_hInstance;
	wc.lpfnWndProc		= WndProc;

	if (!RegisterClass(&wc))
		return 0;

	m_hWnd = CreateWindowEx(0, WINDOW_CLASS, WINDOW_NAME, WS_OVERLAPPEDWINDOW, 0, 0, 1, 1, NULL, NULL, m_hInstance, NULL);

	if (m_hWnd == NULL)
	{
		UnregisterClass(WINDOW_CLASS, m_hInstance);

		return 0;
	}

	RegisterDeviceInterface(m_hWnd);

	MSG msg;

	while(GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	UnregisterClass(WINDOW_CLASS, m_hInstance);

	return msg.wParam;
}

BOOL RegisterDeviceInterface(HWND hWnd)
{
	DEV_BROADCAST_DEVICEINTERFACE NotificationFilter;

	ZeroMemory(&NotificationFilter, sizeof(NotificationFilter));
	NotificationFilter.dbcc_size = sizeof(DEV_BROADCAST_DEVICEINTERFACE);
	NotificationFilter.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;
	NotificationFilter.dbcc_classguid = GUID_DEVINTERFACE_HID;

	HDEVNOTIFY hDevNotify = RegisterDeviceNotification(m_hWnd, &NotificationFilter, DEVICE_NOTIFY_WINDOW_HANDLE);

	if(!hDevNotify)
		return FALSE;

	return TRUE;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PDEV_BROADCAST_HDR pHdr;
	PDEV_BROADCAST_HANDLE pHandle;
	PDEV_BROADCAST_DEVICEINTERFACE pInterface;

	switch(message)
	{
	case WM_CLOSE:
		DestroyWindow(hWnd);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	case WM_DEVICECHANGE:
		switch(wParam)
		{
			case DBT_DEVICEARRIVAL:
				pHdr = (PDEV_BROADCAST_HDR) lParam;

				switch (pHdr->dbch_devicetype)
				{
					case DBT_DEVTYP_DEVICEINTERFACE:
						pInterface = (PDEV_BROADCAST_DEVICEINTERFACE) lParam;
						INT id = 0;

						if(UsbOpen(pInterface->dbcc_name, &m_hidDeviceData[m_deviceCount]))
						{
							m_deviceCount++;

							RemoveNewDriver();
							SortDevices();

#ifdef DEBUG_OUTPUT
							DEBUGLOG(L"DBT_DEVICEARRIVAL");
							OutputDevices();
#endif

							for(INT id = 0; id < m_deviceCount; id++)
							{
								if(_wcsicmp(m_hidDeviceData[id].DevicePath, pInterface->dbcc_name) != 0)
								{
									if(m_usbDeviceAttachedCallback != NULL)
										m_usbDeviceAttachedCallback(id);

									break;
								}
							}
						}
						break;
				}
				break;
			case DBT_DEVICEREMOVECOMPLETE:
				pHdr = (PDEV_BROADCAST_HDR) lParam;

				switch (pHdr->dbch_devicetype)
				{
					case DBT_DEVTYP_HANDLE:
						pHandle = (PDEV_BROADCAST_HANDLE) pHdr;

						UnregisterDeviceNotification(pHandle->dbch_hdevnotify);
						break;
					case DBT_DEVTYP_DEVICEINTERFACE:
						pInterface = (PDEV_BROADCAST_DEVICEINTERFACE) lParam;

						for(INT id = 0; id < m_deviceCount; id++)
						{
							if(_wcsicmp(m_hidDeviceData[id].DevicePath, pInterface->dbcc_name) != 0)
								continue;

							if(m_hidDeviceData[id].pHidDeviceData != NULL)
								free(m_hidDeviceData[id].pHidDeviceData);

							for(INT i = id; i < m_deviceCount - 1; i++)
								m_hidDeviceData[i] = m_hidDeviceData[i + 1];

							m_deviceCount--;

#ifdef DEBUG_OUTPUT
							DEBUGLOG(L"DBT_DEVICEREMOVECOMPLETE");
							OutputDevices();
#endif

							if(m_usbDeviceRemovedCallback != NULL)
								m_usbDeviceRemovedCallback(id);

							break;
						}
						break;
				}
				break;
		}
		break;
	default:
		break;
	}

	return DefWindowProc(hWnd, message, wParam, lParam);
}

BOOL GetDeviceInfo(HANDLE hidHandle, USHORT& vendorID, USHORT& productID, USHORT& versionNumber, USHORT& usage, USHORT& usagePage, USHORT& inputReportLen, USHORT& outputReportLen)
{
	HIDD_ATTRIBUTES hidAttributes;

	if (!HidD_GetAttributes(hidHandle, &hidAttributes))
		return FALSE;

	vendorID = hidAttributes.VendorID;
	productID = hidAttributes.ProductID;
	versionNumber = hidAttributes.VersionNumber;

	//DEBUGLOG(L"VendorID: %04x\n", hidAttributes.VendorID);
	//DEBUGLOG(L"ProductID: %04x\n", hidAttributes.ProductID);
	//DEBUGLOG(L"VersionNumber: %04x\n", hidAttributes.VersionNumber);

	PHIDP_PREPARSED_DATA hidPreparsedData;

	if (!HidD_GetPreparsedData(hidHandle, &hidPreparsedData))
		return FALSE;

	HIDP_CAPS hidCaps;

	if(HidP_GetCaps(hidPreparsedData, &hidCaps) != HIDP_STATUS_SUCCESS)
		return FALSE;

	usage = hidCaps.Usage;
	usagePage = hidCaps.UsagePage;
	inputReportLen = hidCaps.InputReportByteLength;
	outputReportLen = hidCaps.OutputReportByteLength;

	/* DEBUGLOG(L"UsagePage: %d\n", hidCaps.UsagePage);
	DEBUGLOG(L"Usage: %d\n", hidCaps.Usage);
	DEBUGLOG(L"InputReportByteLength: %d\n", hidCaps.InputReportByteLength);
	DEBUGLOG(L"OutputReportByteLength: %d\n", hidCaps.OutputReportByteLength);
	DEBUGLOG(L"FeatureReportByteLength: %d\n", hidCaps.FeatureReportByteLength);
	DEBUGLOG(L"NumberLinkCollectionNodes: %d\n", hidCaps.NumberLinkCollectionNodes);
	DEBUGLOG(L"NumberInputButtonCaps: %d\n", hidCaps.NumberInputButtonCaps);
	DEBUGLOG(L"NumberInputValueCaps: %d\n", hidCaps.NumberInputValueCaps);
	DEBUGLOG(L"NumberOutputButtonCaps: %d\n", hidCaps.NumberOutputButtonCaps);
	DEBUGLOG(L"NumberOutputValueCaps: %d\n", hidCaps.NumberOutputValueCaps);
	DEBUGLOG(L"NumberFeatureButtonCaps: %d\n", hidCaps.NumberFeatureButtonCaps);
	DEBUGLOG(L"NumberFeatureValueCaps: %d\n", hidCaps.NumberFeatureValueCaps); */

	// For more info use FillDeviceInfo
	// https://xp-dev.com/sc/36636/44/%2Ftrunk%2FProjects%2Fusb-device-hid-transfer-project-at91sam7x-ek%2Fusb-device-hid-transfer-project%2FHIDTest%2Fpnp.c

	return TRUE;
}

VOID WriteBufferToDisk(PCHAR fileName, PCHAR buffer)
{
	FILE *file = fopen(fileName, "wb");

	fwrite(buffer, 1, 32, file);

	fclose(file);
}

VOID strlow(WCHAR *src)
{
	for (UINT i = 0; i < wcslen(src); i++)
		src[i] = towlower(src[i]);
}

VOID strlow(CHAR *s)
{
	for (;*s != '\0';s++)
		*s = tolower(*s);
}
