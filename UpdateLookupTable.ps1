[CmdletBinding()]
Param(
  [Parameter(Mandatory=$True,Position=1)]
   [string]$SearchPath, 
   [Parameter(Mandatory=$True,Position=2)]
   [string]$OutputPath
)

add-PSSnapin BenPattinson.GetBuildLocation.PSSnapIn
$Items = (Get-ChildItem -Path "$SearchPath\*" -Include "*.dll" -Recurse | Get-BuildLocation)

$buildLocationDictionary =  New-Object 'System.Collections.Generic.SortedDictionary[string,object]'

#TODO load in the existing dictionary

foreach($Item in $Items)
{
	if(($Item.BuildLocation -ne "") -and ($Item.BuildLocation -ne $null))
	{
		if(!$buildLocationDictionary.ContainsKey($Item.BuildLocation+"_"+$Item.NameSpace))
		{
			$buildLocationDictionary.Add($Item.BuildLocation+"_"+$Item.NameSpace,$Item);
		}
	}
}
$OutputFile = $OutputPath+"\Mapping.csv"

#Then write out the updated one

"Build Location, File Name, Namespace, Alias" | Out-File $OutputFile

foreach($Item in $buildLocationDictionary.Values)
{
	($Item.BuildLocation + " , " + $Item.Name + " , " + $Item.Namespace +",") | Out-File $OutputFile -Append
}

