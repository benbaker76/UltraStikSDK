// ****************************************************************
// UltraStik.h header file.
// Author: Ben Baker [headsoft.com.au]
// ****************************************************************

#ifdef ULTRASTIK_EXPORTS
#define ULTRASTIK_API __declspec(dllexport)
#else
#define ULTRASTIK_API __declspec(dllimport)
#endif

#define VID_ULTIMARC		0xd209	// Ultimarc Vendor Id
#define PID_ULTRASTIK_OLD	0x0500	// Old Firmware: 0x0501, 0x0502, 0x0503, 0x0504
#define PID_ULTRASTIK_NEW	0x0510	// New Firmware: 0x0511, 0x0512, 0x0513, 0x0514

#define RESTRICTOR_ON		0x09
#define RESTRICTOR_OFF		0x10

#define MAP_HEADER			0x50
#define MAP_ROW_SIZE		0x09

#define WRITE_FLASH			0x00
#define WRITE_RAM			0xff

#define TIME_OUT			100

#define MAP_ANALOG			0x00	// -
#define MAP_CENTER			0x01	// C
#define MAP_N				0x02	// N
#define MAP_NE				0x03	// NE
#define MAP_E				0x04	// E
#define MAP_SE				0x05	// SE
#define MAP_S				0x06	// S
#define MAP_SW				0x07	// SW
#define MAP_W				0x08	// W
#define MAP_NW				0x09	// NW
#define MAP_STICKY			0x0A	// *

#define MAP_FILEFORMATVERSION	0
#define MAP_SIZE				1
#define MAP_BORDERLOCATIONS		2
#define MAP_ROW1				3
#define MAP_ROW2				4
#define MAP_ROW3				5
#define MAP_ROW4				6
#define MAP_ROW5				7
#define MAP_ROW6				8
#define MAP_ROW7				9
#define MAP_ROW8				10
#define MAP_ROW9				11

#define CONFIG_FILEFORMATVERSION	0
#define CONFIG_MAP1					1
#define CONFIG_MAP2					2
#define CONFIG_MAP3					3
#define CONFIG_MAP4					4

#define IS_OLDDRIVER(usagePage, usage)		(usagePage == 1 && usage == 4)
#define IS_NEWDRIVER(usagePage, usage)		(usagePage == 1 && usage == 0)

#define WINDOW_NAME		L"UltraStik.dll Event Window"
#define WINDOW_CLASS	L"UltraStik.dll Event Class"

static GUID GUID_DEVINTERFACE_HID =
{ 0x4D1E55B2L, 0xF16F, 0x11CF, { 0x88, 0xCB, 0x00, 0x11, 0x11, 0x00, 0x00, 0x30 } };

typedef struct
{
	UCHAR ReportID;
	UCHAR ReportBuffer[4];
} REPORT_BUF, *PREPORT_BUF;

typedef struct HID_DEVICE_DATA
{
	HANDLE hDevice;
	USHORT VendorID;
	USHORT ProductID;
	USHORT VersionNumber;
	WCHAR VendorName[256];
	WCHAR ProductName[256];
	WCHAR SerialNumber[256];
	WCHAR DevicePath[256];
	USHORT InputReportLen;
	USHORT OutputReportLen;
	USHORT UsagePage;
	USHORT Usage;
	BYTE Restrictor;
	BYTE Write;
	BOOL IsNew;
	HID_DEVICE_DATA *pHidDeviceData;
} *PHID_DEVICE_DATA;

enum MapName
{
	vjoy2way,		// 2-Way, Vertical
	joy2way,		// 2-Way, Horizontal
	joy4way,		// 4-Way
	udbjoy4way,		// 4-Way, No Sticky (UD Bias)
	djoy4way,		// 4-Way, Diagonals Only
	rdjoy4way,		// 4-Way, Rotated Diagonals
	joy8way,		// 8-Way
	easyjoy8way,	// 8-Way, Easy Diagonals
	analog,			// Analog
	mouse			// Mouse Pointer
};

typedef VOID (__stdcall *USBDEVICE_ATTACHED_CALLBACK)(INT id);
typedef VOID (__stdcall *USBDEVICE_REMOVED_CALLBACK)(INT id);

extern BYTE m_mapHeader[];
extern BYTE m_mapBorderLocations[];

extern BYTE m_vjoy2way[];		// 2-Way, Vertical
extern BYTE m_joy2way[];		// 2-Way, Horizontal
extern BYTE m_joy4way[];		// 4-Way
extern BYTE m_udbjoy4way[];		// 4-Way, No Sticky (UD Bias)
extern BYTE m_djoy4way[];		// 4-Way, Diagonals Only
extern BYTE m_rdjoy4way[];		// 4-Way, Rotated Diagonals
extern BYTE m_joy8way[];		// 8-Way
extern BYTE m_easyjoy8way[];	// 8-Way, Easy Diagonals
extern BYTE m_analog[];			// Analog
extern BYTE m_mouse[];			// Mouse Pointer

struct MapInfo
{
	PCHAR Name;
	PBYTE Data;
};

extern MapInfo m_mapInfo[];

ULTRASTIK_API VOID __stdcall UltraStik_SetCallbacks(USBDEVICE_ATTACHED_CALLBACK usbDeviceAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbDeviceRemovedCallback);

ULTRASTIK_API INT __stdcall UltraStik_Initialize();
ULTRASTIK_API VOID __stdcall UltraStik_Shutdown();
ULTRASTIK_API INT __stdcall UltraStik_GetVendorId(INT id);
ULTRASTIK_API INT __stdcall UltraStik_GetProductId(INT id);
ULTRASTIK_API INT __stdcall UltraStik_GetVersionNumber(INT id);
ULTRASTIK_API VOID __stdcall UltraStik_GetVendorName(INT id, PWCHAR sVendorName);
ULTRASTIK_API VOID __stdcall UltraStik_GetProductName(INT id, PWCHAR sProductName);
ULTRASTIK_API VOID __stdcall UltraStik_GetSerialNumber(INT id, PWCHAR sSerialNumber);
ULTRASTIK_API VOID __stdcall UltraStik_GetDevicePath(INT id, PWCHAR sDevicePath);
ULTRASTIK_API VOID __stdcall UltraStik_SetRestrictor(INT id, BOOL value);
ULTRASTIK_API VOID __stdcall UltraStik_SetFlash(INT id, BOOL value);
ULTRASTIK_API INT __stdcall UltraStik_GetUltraStikId(INT id);
ULTRASTIK_API VOID __stdcall UltraStik_SetUltraStikId(INT id, INT value);
ULTRASTIK_API INT __stdcall UltraStik_LoadMap(INT id, PCHAR map);
//ULTRASTIK_API INT __stdcall UltraStik_LoadMap(INT id, MapName map);
ULTRASTIK_API INT __stdcall UltraStik_LoadMapFile(INT id, PCHAR fileName);
ULTRASTIK_API INT __stdcall UltraStik_LoadConfigFile(PCHAR mapPath, PCHAR fileName);

VOID OutputDevice(PHID_DEVICE_DATA pHidDeviceData);
VOID OutputDevices();
VOID SortDevices();
VOID GetBorderValues(PCHAR line, PBYTE out);
VOID GetRow(PCHAR line, PBYTE out);
BYTE Str2Byte(PCHAR map);
INT SendMap(INT id, PBYTE pBuffer);
BOOL SendData(INT id, PBYTE pBuffer, INT bufferSize);
BOOL UsbOpen(LPCWSTR devicePath, HID_DEVICE_DATA *pDeviceData);
BOOL UsbRead(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport);
BOOL UsbRead(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport, DWORD timeOut);
BOOL UsbWrite(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pOutputReport);
BOOL UsbWrite(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport, DWORD timeOut);

DWORD WINAPI EventWindowThread(LPVOID lpParam);
BOOL RegisterDeviceInterface(HWND hWnd);
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);

BOOL GetDeviceInfo(HANDLE hDevice, USHORT& vendorID, USHORT& productID, USHORT& versionNumber, USHORT& usage, USHORT& usagePage, USHORT& inputReportLen, USHORT& outputReportLen);

VOID DebugOutput(LPCTSTR lpszFormat, ...);

VOID WriteBufferToDisk(PCHAR fileName, PCHAR buffer);

VOID strlow(WCHAR *src);
VOID strlow(CHAR *s);
