unit SDIMAIN;

interface

uses Windows, Classes, Graphics, Forms, Controls, Menus,
  Dialogs, StdCtrls, Buttons, ExtCtrls, ComCtrls, ImgList, StdActns,
  ActnList, ToolWin, SysUtils;

const
  ULTRASTIK_MAX_DEVICES : Integer = 4;

type
  TSDIAppForm = class(TForm)
    procedure FormShow(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

Function UltraStik_Initialize() : Integer; stdcall; external 'UltraStik.dll';
Procedure UltraStik_Shutdown() stdcall; external 'UltraStik.dll';
Function UltraStik_GetVendorId(id: Integer) : Integer; stdcall; external 'UltraStik.dll';
Function UltraStik_GetProductId(id: Integer) : Integer; stdcall; external 'UltraStik.dll';
Procedure UltraStik_GetManufacturer(id: Integer; sManufacturer: PChar) stdcall; external 'UltraStik.dll';
Procedure UltraStik_GetProduct(id: Integer; sProduct: PChar) stdcall; external 'UltraStik.dll';
Procedure UltraStik_GetSerialNumber(id: Integer; sSerialNumber: PChar) stdcall; external 'UltraStik.dll';
Function UltraStik_GetFirmwareVersion(id: Integer) : Integer; stdcall; external 'UltraStik.dll';
Procedure UltraStik_SetRestrictor(id: Integer; value: Boolean) stdcall; external 'UltraStik.dll';
Procedure UltraStik_SetFlash(id: Integer; value: Boolean) stdcall; external 'UltraStik.dll';
Function UltraStik_GetUltraStikId(id: Integer) : Integer; stdcall; external 'UltraStik.dll';
Procedure UltraStik_SetUltraStikId(id: Integer; value: Integer) stdcall; external 'UltraStik.dll';
Function UltraStik_LoadMap(id: Integer; map: PChar) : Integer; stdcall; external 'UltraStik.dll';
Function UltraStik_LoadMapFile(id: Integer; fileName: PChar) : Integer; stdcall; external 'UltraStik.dll';
Function UltraStik_LoadConfigFile(fileName: PChar) : Integer; stdcall; external 'UltraStik.dll';

var
  SDIAppForm: TSDIAppForm;

implementation

{$R *.dfm}

procedure TSDIAppForm.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  UltraStik_Shutdown();
end;

procedure TSDIAppForm.FormShow(Sender: TObject);
var
  numDevices: Integer;
begin
  numDevices := UltraStik_Initialize();

  if (numDevices > 0) then
  begin
    UltraStik_LoadMap(0, PChar('mouse'));
  end;
end;

end.

