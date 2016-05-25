@echo off
echo Finalização ASL
echo Desabilitando windows update
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update" /v AUOptions /t REG_DWORD /d 1 /f  
echo Desabilitando centro de ações
::dActionCenter.reg
echo Habilitando plano de alta perfomance
powercfg -s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c
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
