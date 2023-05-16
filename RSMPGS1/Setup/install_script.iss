ï»¿;InnoSetupVersion=6.0.0 (Unicode)

[Setup]
AppName=RSMPGS1 (Roadside Simulator)
AppVerName=RSMPGS1 (Roadside Simulator) 1.0.1.7
AppId=RSMPGS1 (Roadside Simulator)
AppPublisher=Acobia AB / TroSoft AB
AppPublisherURL=http://www.automatisera.nu
AppSupportURL=http://www.automatisera.nu
AppUpdatesURL=http://www.automatisera.nu
DefaultDirName={pf}\RSMPGS1
DefaultGroupName=RSMP
OutputBaseFilename=RSMPGS1_1_0_1_7_Setup
Compression=lzma
DisableDirPage=auto
DisableProgramGroupPage=auto
WizardImageFile=embedded\WizardImage0.bmp
WizardSmallImageFile=embedded\WizardSmallImage0.bmp

[Files]
Source: "{app}\RSMPGS1.exe"; DestDir: "{app}"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Settings\RSMPGS1.INI"; DestDir: "{app}\Settings"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Objects\Aggregated status.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Objects\Alarms.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Objects\Commands.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Objects\Object types.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Objects\Objects.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Objects\ProcessImage.dat"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Objects\Status.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Objects\Version.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\YAML\SXL-example.xlsx"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\YAML\SXL-example.yaml"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\YAML\YAML exempel.pdf"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\Handledning RSMP_Tillämpningsrekommendation_signalutbyteslista_3_1_4.docx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\Riktlinje RSMP_Kommunikationsprotokoll_vägsidesutrustning_3_1_4.docx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\rsmp-spec-3.1.5.pdf"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\rsmp-spec-3.1.5_Highlighted changes.pdf"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\RSMP_SignalExchangeList_VH_CrossRoad-20120119.xlsx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\RSMP_Template_SignalExchangeList-20120117.xlsx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\sxl-tlc-1.0.15.pdf"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\Versionshistorik RSMP-Kommunikationsprotokoll vägsidesutrustning.docx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Documents\TrV Manual RSMPGS1.RevE - English - 20201126.docx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Debug.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Debug.designer.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Debug.resx"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Encryption.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Helper.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_JSon.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Main.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Messages.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Objects.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_ProcessImage.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Socket.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Socket_Client.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_Socket_Server.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_SysLog.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPCommon\RSMPGS_YAML.cs"; DestDir: "{app}\Source\RSMPCommon"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\app.config"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1 (Roadside).csproj"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1 (Roadside).csproj.user"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1.cs"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1.exe"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1.sln"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_JSon.cs"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_ProcessImage.cs"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS_Packet.cs"; DestDir: "{app}\Source\RSMPGS1"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Graphics\ListView_Checked.bmp"; DestDir: "{app}\Source\RSMPGS1\Graphics"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Graphics\ListView_Component.bmp"; DestDir: "{app}\Source\RSMPGS1\Graphics"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Graphics\ListView_ComponentGroup.bmp"; DestDir: "{app}\Source\RSMPGS1\Graphics"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Graphics\ListView_Root.bmp"; DestDir: "{app}\Source\RSMPGS1\Graphics"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Graphics\ListView_UnChecked.bmp"; DestDir: "{app}\Source\RSMPGS1\Graphics"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Graphics\RSMPGS1_Debug.ico"; DestDir: "{app}\Source\RSMPGS1\Graphics"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Graphics\RSMPGS1_Main.ico"; DestDir: "{app}\Source\RSMPGS1\Graphics"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Properties\AssemblyInfo.cs"; DestDir: "{app}\Source\RSMPGS1\Properties"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Properties\Resources.Designer.cs"; DestDir: "{app}\Source\RSMPGS1\Properties"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Properties\Resources.resx"; DestDir: "{app}\Source\RSMPGS1\Properties"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Properties\Settings.Designer.cs"; DestDir: "{app}\Source\RSMPGS1\Properties"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\Properties\Settings.settings"; DestDir: "{app}\Source\RSMPGS1\Properties"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Alarms.cs"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Main.cs"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Main.Designer.cs"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Main.ico"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Main.resx"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Main_AggregatedStatus.cs"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Main_Command.cs"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Main_Status.cs"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Main_TestSend.cs"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "{app}\Source\RSMPGS1\RSMPGS1_Main\RSMPGS1_Status.cs"; DestDir: "{app}\Source\RSMPGS1\RSMPGS1_Main"; MinVersion: 0.0,6.0; Flags: ignoreversion 

[Dirs]
Name: "{app}"; 
Name: "{app}\LogFiles"; 
Name: "{app}\Settings"; 

[Icons]
Name: "{group}\RSMPGS1 (Roadside Simulator)"; Filename: "{app}\RSMPGS1.exe"; MinVersion: 0.0,6.0; 
Name: "{userdesktop}\RSMPGS1 (Roadside Simulator)"; Filename: "{app}\RSMPGS1.exe"; MinVersion: 0.0,6.0; 

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; MinVersion: 0.0,6.0; 

[CustomMessages]
eng.NameAndVersion=%1 version %2
eng.AdditionalIcons=Additional shortcuts:
eng.CreateDesktopIcon=Create a &desktop shortcut
eng.CreateQuickLaunchIcon=Create a &Quick Launch shortcut
eng.ProgramOnTheWeb=%1 on the Web
eng.UninstallProgram=Uninstall %1
eng.LaunchProgram=Launch %1
eng.AssocFileExtension=&Associate %1 with the %2 file extension
eng.AssocingFileExtension=Associating %1 with the %2 file extension...
eng.AutoStartProgramGroupDescription=Startup:
eng.AutoStartProgram=Automatically start %1
eng.AddonHostProgramNotFound=%1 could not be located in the folder you selected.%n%nDo you want to continue anyway?

[Languages]
; These files are stubs
; To achieve better results after recompilation, use the real language files
Name: "eng"; MessagesFile: "embedded\eng.isl"; 
