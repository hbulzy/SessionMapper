; My Programs NSIS Script for install uninstall

;--------------------------------

; The name of the installer
Name "Session Mapper - v1.0 - by: kossboss"

; The file to write
OutFile "sessionmap_1_setup.exe"

; The default installation directory
InstallDir $PROGRAMFILES\SessionMapper\

; The text to prompt the user to enter a directory
; NOTE THE VERSION NUMBER IS IN 2 PLACES RIGHT HERE AND IN THE DESKTOP SHORTCUT PLACE AND UNDELETE
DirText "This will install Session Mapper v1.0 on your computer. Choose a directory"

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

; Set output path to the installation directory.
SetOutPath $INSTDIR

; Put a file there
File SessionMapper.exe
File GMap.NET.WindowsForms.dll
File GMap.NET.Core.dll

CreateDirectory "$SMPROGRAMS\kossboss - Session Mapper"
CreateShortCut "$SMPROGRAMS\kossboss - Session Mapper\SessionMapper.lnk" "$INSTDIR\SessionMapper.exe"
CreateShortCut "$SMPROGRAMS\kossboss - Session Mapper\Uninstall SessionMapper.lnk" "$INSTDIR\Uninstall.exe"
CreateShortCut "$DESKTOP\SessionMapper v1.0.lnk" "$INSTDIR\SessionMapper.exe"


; Tell the compiler to write an uninstaller and to look for a "Uninstall" section 
WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SessionMapper" "DisplayName" "SessionMapper (remove only)"
WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\SessionMapper" "UninstallString" "$INSTDIR\Uninstall.exe"
WriteUninstaller $INSTDIR\Uninstall.exe

SectionEnd ; end the section

; The uninstall section
Section "Uninstall"

;Delete Files 
  RMDir /r "$INSTDIR\*.*"    
 
;Remove the installation directory
  RMDir "$INSTDIR"
 
;Delete Start Menu Shortcuts
  Delete "$DESKTOP\SessionMapper v1.0.lnk"
  RMDir /r "$SMPROGRAMS\kossboss - Session Mapper\*.*"
  RMDir  "$SMPROGRAMS\kossboss - Session Mapper"
 
;Delete Uninstaller And Unistall Registry Entries
  DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\SessionMapper"
  DeleteRegKey HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SessionMapper"  


;Delete "$SMPROGRAMS\kossboss\SessionMapper.lnk" 
;Delete "$SMPROGRAMS\kossboss\Uninstall SessionMapper.lnk" 
;Delete "$DESKTOP\SessionMapper v1.0.lnk"
;RMDir "$SMPROGRAMS\kossboss"
;Delete $INSTDIR\SessionMapper.exe
;Delete $INSTDIR\GMap.NET.WindowsForms.dll
;Delete $INSTDIR\GMap.NET.Core.dll
;Delete $INSTDIR\Uninstall.exe
;RMDir $INSTDIR

SectionEnd


Function .onInstSuccess
  MessageBox MB_OK "You have successfully installed Session Mapper. Use the desktop icon or the start menu to start the program."
FunctionEnd
 
 
Function un.onUninstSuccess
  MessageBox MB_OK "You have successfully uninstalled Session Mapper."
FunctionEnd