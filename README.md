![](/images/ultimarc.png)

# UltraStik 360 SDK

![](/images/UltraStik1.png)

![](/images/UltraStik2.png)

## Description

The Ultimarc UltraStik 360 SDK is a collection of source code examples for
controlling the UltraStik 360 joysticks by Ultimarc.

For more information on these devices please visit:

- [UltraStik 360 Ball Top](https://www.ultimarc.com/arcade-controls/joysticks/ultrastik-360-oval-top-clone)
- [UltraStik 360 Oval Top](https://www.ultimarc.com/arcade-controls/joysticks/ultrastik-360-oval-top-en-2)

It contains source code projects in the following languages:
- C#
- C++
- Delphi
- VB6
- VB.NET

## API

`int UltraStik_Initialize();`

- Initialize the joysticks
- Returns the number of joysticks found
- Joysticks are sorted by ProductId and referenced via an id (0 = Joystick 1, 1 = Joystick 2, 2 = Joystick 3, 3 = Joystick 4)

`void UltraStik_Shutdown();`

- Shutdown joysticks

`int UltraStik_GetVendorId(int id);`

- Return the vendor id for joystick specified by id

`int UltraStik_GetProductId(int id);`

- Return the product id for joystick specified by id

`void UltraStik_GetManufacturer(int id, char *sManufacturer);`

- Get the manufacturer name for joystick specified by id
- The string is written to sManufacturer which must be a pointer to a char buffer of 256 bytes

`void UltraStik_GetProduct(int id, char *sProduct);`

- Get the product name for joystick specified by id
- The string is written to sProduct which must be a pointer to a char buffer of 256 bytes

`void UltraStik_GetSerialNumber(int id, char *sSerialNumber);`

- Get the serial number for joystick specified by id
- The string is written to sSerialNumber which must be a pointer to a char buffer of 256 bytes

`int UltraStik_GetFirmwareVersion(int id);`

- Return the firmware version for joystick specified by id

`void UltraStik_SetRestrictor(int id, bool value);`

- Set the restrictor of the joystick specified by id to on or off

`void UltraStik_SetFlash(int id, bool value);`

- Set the flash of the joystick specified by id
- true = Flash, false = RAM

`int UltraStik_GetUltraStikId(int id);`

- Return the UltraStik id for joystick specified by id

`void UltraStik_SetUltraStikId(int id, int value);`

- Set the UltraStik id for the joystick specified by id
- 0 = Id1, 1 = Id2, 2 = Id3, 3 = Id4

`bool UltraStik_LoadMap(int id, char *map);`

- Load a built in map into the joystick specified by id
- Valid values for map:

| Map | Description |
| --- | --- |
| `vjoy2way` | 2-Way, Vertical |
| `joy2way` | 2-Way, Horizontal |
| `joy4way` | 4-Way |
| `udbjoy4way` | 4-Way, No Sticky (UD Bias) |
| `djoy4way` | 4-Way, Diagonals Only |
| `rdjoy4way` | 4-Way, Rotated Diagonals |
| `joy8way` | 8-Way |
| `easyjoy8way` | 8-Way, Easy Diagonals |
| `analog` | Analog (used for 49-way as well) |
| `mouse` | Mouse Pointer |

- Return true for success and false for fail

`bool UltraStik_LoadMapFile(int id, char *fileName);`

- Load a .um map file into the joystick specified by id
- fileName is a pointer to a full path of the .um file to load
- Return true for success and false for fail

## Release Dates
- 9-9-2015 - 1.1 - Update for new firmware
- 2-6-2008 - 1.0 - First Release

## Contacts
- Andy Warne (Hardware Manufacturer): andy@ultimarc.com
- Ben Baker (PacDrive SDK Developer): headkaze@gmail.com
