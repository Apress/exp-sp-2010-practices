Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$Path = "C:\Projects\Apress\FooterLinksV2\Upgrade\FooterLinksSchema.wsp"
$SolutionPackage  = "FooterLinksSchema.wsp"
$WebApp = "http://intranet.theopenhighway.net"
$webApp = [Microsoft.SharePoint.Administration.SPWebApplication]::Lookup($WebApp)

# =========================================================================================

Write-Host "=========================================================================="
# ensure solution is deployed
$solution = Get-SPSolution | where-object {$_.Name -eq $SolutionPackage}
if ($solution -ne $null) {
  if($solution.Deployed -eq $true){
    Write-Host "Updating solution package"$SolutionPackage "...."
    Update-SPSolution -Identity $SolutionPackage -LiteralPath $Path -Local -GACDeployment
    Write-Host "Solution has been updated"
  }
  else {
    Write-Host "Cannot find Solution - please verify it is deployed."  
  }
}

# Confirm update solution by displaying current version number of features
$featureId = New-Object System.Guid -ArgumentList "fdcb05db-952a-426a-ba77-ee40d632fa49"
$featureDisplayName = "FooterLinksSchema_FooterLinksSchema"

Write-Host
Write-Host "Feature:"$featureDisplayName

# This is the feature that has been INSTALLED (e.g. 1.1.0.0)
$feature2 = get-spfeature | where {$_.DisplayName -eq $featureDisplayName } 
foreach($ifeature in $feature2){
  Write-Host "The following version is the INSTALLED Version: "$ifeature.Version
}
Write-Host

Write-Host
Write-Host "Update Solution script has completed. Press OK to quit"
Read-Host