[Setup]
AppName=RSMPGS2 (SCADA Interface Simulator)
AppVerName=RSMPGS2 (SCADA Interface Simulator) 1.0.3
AppId=RSMPGS2 (SCADA Interface Simulator)
AppPublisher=Acobia AB / TroSoft AB
AppPublisherURL=http://www.automatisera.nu
AppSupportURL=http://www.automatisera.nu
AppUpdatesURL=http://www.automatisera.nu
DefaultDirName={pf}\RSMPGS2
DefaultGroupName=RSMP
OutputBaseFilename=RSMPGS2_1_0_3_Setup
Compression=lzma
DisableDirPage=auto
DisableProgramGroupPage=auto
WizardImageFile=Setup\WizardImage0.bmp
WizardSmallImageFile=Setup\WizardSmallImage0.bmp

[Files]
Source: "RSMPGS2\bin\Release\RSMPGS2.exe"; DestDir: "{app}"; MinVersion: 0.0,6.0; Flags: ignoreversion
Source: "RSMPGS2\Settings\RSMPGS2.INI"; DestDir: "{app}\Settings"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Aggregated_status.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Alarms.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Commands.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Object_types.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Objects.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Status.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Version.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\SXL-example.xlsx"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\SXL-example.yaml"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Documents\RSMP_Template_SignalExchangeList-20120117.xlsx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Documents\Manual_RSMPGS2.pdf"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 

[Dirs]
Name: "{app}"; 
Name: "{app}\LogFiles"; Permissions: users-modify
Name: "{app}\LogFiles\SysLogFiles"; Permissions: users-modify
Name: "{app}\LogFiles\EventFiles"; Permissions: users-modify
Name: "{app}\Settings"; Permissions: users-modify
Name: "{app}\YAML"; Permissions: users-modify
Name: "{app}\Objects"; Permissions: users-modify

[Icons]
Name: "{group}\RSMPGS2 (SCADA Interface Simulator)"; Filename: "{app}\RSMPGS2.exe"; MinVersion: 0.0,6.0; 
Name: "{userdesktop}\RSMPGS2 (SCADA Interface Simulator)"; Filename: "{app}\RSMPGS2.exe"; MinVersion: 0.0,6.0; 
Name: "{group}\RSMPGS2 Manual"; Filename: "{app}\Documents\Manual_RSMPGS2.pdf"; MinVersion: 0.0,6.0; 

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
Name: "eng"; MessagesFile: "Setup\eng.isl"; 
