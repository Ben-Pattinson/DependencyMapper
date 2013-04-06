echo begin
@SET GACUTIL="C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\gacutil.exe"
@SET INSTALLUTIL="C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe"
%GACUTIL% -if GetBuildLocationPSSnapIn.dll
%GACUTIL% -if Mono.Cecil.dll
%INSTALLUTIL% GetBuildLocationPSSnapIn.dll
Pause
