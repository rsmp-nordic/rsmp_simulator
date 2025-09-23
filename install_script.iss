#ifdef RSMPGS1
#define Name "RSMPGS1"
#define Description "Roadside Simulator"
#else
#define Name "RSMPGS2"
#define Description "SCADA Interface simulator"
#endif
#define Version "1.0.13"
#define Version_ "1_0_13"

[Setup]
AppName={#Name} ({#Description})
AppVerName={#Name} ({#Description}) {#Version}
AppId={#Name} ({#Description})
AppPublisher=Acobia AB / TroSoft AB
AppPublisherURL=http://www.automatisera.nu
AppSupportURL=http://www.automatisera.nu
AppUpdatesURL=http://www.automatisera.nu
DefaultDirName={pf}\{#Name}
DefaultGroupName=RSMP
OutputBaseFilename={#Name}_{#Version_}_Setup
Compression=lzma
DisableDirPage=auto
DisableProgramGroupPage=auto
WizardImageFile=Setup\WizardImage0.bmp
WizardSmallImageFile=Setup\WizardSmallImage0.bmp

[Files]
Source: "{#Name}.exe"; DestDir: "{app}"; MinVersion: 0.0,6.0; Flags: ignoreversion
Source: "Settings\{#Name}.INI"; DestDir: "{app}\Settings"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Aggregated_status.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Alarms.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Commands.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Object_types.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Objects.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Status.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Version.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\SXL-example.xlsx"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\TLC_SXL_1_2.yaml"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\TLC_SXL_1_1.yaml"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\TLC_SXL_1_0_15.yaml"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\TLC_SXL_1_0_7.yaml"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Documents\RSMP_Template_SignalExchangeList-20120117.xlsx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Documents\Manual_{#Name}.pdf"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Tools\Certificates\RSMP testcert Password.txt"; DestDir: "{app}\Tools\Certificates"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Tools\Certificates\RSMP testcert.cer"; DestDir: "{app}\Tools\Certificates"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Tools\Certificates\RSMP testcert.pfx"; DestDir: "{app}\Tools\Certificates"; MinVersion: 0.0,6.0; Flags: ignoreversion 

[Dirs]
Name: "{app}"; 
Name: "{app}\LogFiles"; Permissions: users-modify
Name: "{app}\LogFiles\SysLogFiles"; Permissions: users-modify
Name: "{app}\LogFiles\EventFiles"; Permissions: users-modify
Name: "{app}\Settings"; Permissions: users-modify
Name: "{app}\YAML"; Permissions: users-modify
Name: "{app}\Objects"; Permissions: users-modify
Name: "{app}\Tools"; Permissions: users-modify
Name: "{app}\Tools\Certificates"; Permissions: users-modify

[Icons]
Name: "{group}\{#Name} ({#Description})"; Filename: "{app}\{#Name}.exe"; MinVersion: 0.0,6.0; 
Name: "{userdesktop}\{#Name} ({#Description})"; Filename: "{app}\{#Name}.exe"; MinVersion: 0.0,6.0; 
Name: "{group}\{#Name} Manual"; Filename: "{app}\Documents\Manual_{#Name}.pdf"; MinVersion: 0.0,6.0; 

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
