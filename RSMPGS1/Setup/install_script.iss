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
Source: "RSMPGS1.exe"; DestDir: "{app}"
Source: "Settings\RSMPGS1.INI"; DestDir: "{app}\Settings"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Aggregated status.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Alarms.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Commands.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Object types.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Objects.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\ProcessImage.dat"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Status.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Objects\Version.csv"; DestDir: "{app}\Objects"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\SXL-example.xlsx"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\SXL-example.yaml"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "YAML\YAML exempel.pdf"; DestDir: "{app}\YAML"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Documents\rsmp-spec-3.1.5.pdf"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Documents\RSMP_Template_SignalExchangeList-20120117.xlsx"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 
Source: "Documents\sxl-tlc-1.0.15.pdf"; DestDir: "{app}\Documents"; MinVersion: 0.0,6.0; Flags: ignoreversion 

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
