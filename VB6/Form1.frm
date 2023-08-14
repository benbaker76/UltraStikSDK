VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   1635
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   2985
   LinkTopic       =   "Form1"
   ScaleHeight     =   1635
   ScaleWidth      =   2985
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command2 
      Caption         =   "Stop"
      Height          =   495
      Left            =   240
      TabIndex        =   1
      Top             =   840
      Width           =   2535
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Start"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   240
      Width           =   2535
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Declare Function UltraStik_Initialize Lib "UltraStik32" () As Integer
Private Declare Sub UltraStik_Shutdown Lib "UltraStik32" ()
Private Declare Function UltraStik_GetVendorId Lib "UltraStik32" (ByVal id As Integer) As Integer
Private Declare Function UltraStik_GetProductId Lib "UltraStik32" (ByVal id As Integer) As Integer
Private Declare Sub UltraStik_GetManufacturer Lib "UltraStik32" (ByVal id As Integer, ByVal sManufacturer As String)
Private Declare Sub UltraStik_GetProduct Lib "UltraStik32" (ByVal id As Integer, ByVal sProduct As String)
Private Declare Sub UltraStik_GetSerialNumber Lib "UltraStik32" (ByVal id As Integer, ByVal sSerialNumber As String)
Private Declare Function UltraStik_GetFirmwareVersion Lib "UltraStik32" (ByVal id As Integer) As Integer
Private Declare Sub UltraStik_SetRestrictor Lib "UltraStik32" (ByVal id As Integer, ByVal value As Boolean)
Private Declare Sub UltraStik_SetFlash Lib "UltraStik32" (ByVal id As Integer, ByVal value As Boolean)
Private Declare Function UltraStik_GetUltraStikId Lib "UltraStik32" (ByVal id As Integer) As Integer
Private Declare Sub UltraStik_SetUltraStikId Lib "UltraStik32" (ByVal id As Integer, ByVal value As Integer)
Private Declare Function UltraStik_LoadMap Lib "UltraStik32" (ByVal id As Integer, ByVal map As String) As Integer
Private Declare Function UltraStik_LoadMapFile Lib "UltraStik32" (ByVal id As Integer, ByVal fileName As String) As Integer
Private Declare Function UltraStik_LoadConfigFile Lib "UltraStik32" (ByVal fileName As String) As Integer

Private Sub Command1_Click()
    Dim numDevices As Integer
    
    numDevices = UltraStik_Initialize
    
    For i = 0 To numDevices - 1
        Dim sProduct As String
        sProduct = Space$(256)
        UltraStik_GetProduct i, sProduct
        MsgBox sProduct
    Next i
End Sub

Private Sub Command2_Click()
    UltraStik_Shutdown
End Sub
