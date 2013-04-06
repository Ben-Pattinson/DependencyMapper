[CmdletBinding()]
Param(
  [Parameter(Mandatory=$True,Position=1)]
   [string]$SearchPath
)

(Get-Item -Path "E:\MyCode\GetBinarySourceLocationPSSnapin\bin\Debug\*" -Include "*.dll" | Get-BuildLocation)
