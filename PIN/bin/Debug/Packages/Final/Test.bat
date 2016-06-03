@echo off
echo Finalização ASL
echo Desabilitando windows update
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update" /v AUOptions /t REG_DWORD /d 1 /f  
echo Desabilitando centro de ações
::dActionCenter.reg
echo Habilitando plano de alta perfomance
powercfg -s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c

echo Set oWMP = CreateObject("WMPlayer.OCX.7")  >> %temp%\temp.vbs
echo Set colCDROMs = oWMP.cdromCollection       >> %temp%\temp.vbs
echo For i = 0 to colCDROMs.Count-1             >> %temp%\temp.vbs
echo colCDROMs.Item(i).Eject                    >> %temp%\temp.vbs
echo next                                       >> %temp%\temp.vbs
echo oWMP.close                                 >> %temp%\temp.vbs
%temp%\temp.vbs
timeout /t 1
del %temp%\temp.vbs


cls


COLOR 04

echo Lembre-se de checar : 
echo.

COLOR 07

echo Windows update;
echo Plano de energia;
echo Centro de acoes;
echo Wallpaper;
echo CD;
echo.
pause
