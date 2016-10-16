Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$WebApp = "http://intranet.theopenhighway.net"
$Path = "C:\Projects\Apress\FooterLinksV1\Deployment\FooterLinksList.wsp"
$SolutionPackage  = "FooterLinksList.wsp"

$solution = Get-SPSolution | where-object {$_.Name -eq $SolutionPackage}
if ($Solution -ne $null) {
  if($Solution.Deployed -eq $true) {
    Write-Host "Retracting Solution from the Farm..."
    Uninstall-SPSolution -Identity $SolutionPackage -Local -Confirm:$false -WebApplication $WebApp
    Write-Host "Solution retracted."
    Write-Host
  }
  Write-Host "Deleting Solution from solution store..."
  Remove-SPSolution -Identity $SolutionPackage -Confirm:$false
  Write-Host "Solution deleted."
  Write-Host
}

Write-Host "Adding Solution to solution store..."
Add-SPSolution -LiteralPath $Path
Write-Host "Solution added."
Write-Host
Write-Host "Deploying Solution to the Farm..."
Install-SPSolution -Identity $SolutionPackage -Local -GACDeployment -WebApplication $WebApp
Write-Host "Deployment complete."

Write-Host
Write-Host "Script has completed. Press Enter to continue."
Read-Host