Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$WebApp = "http://intranet.theopenhighway.net"
$Path = "C:\Projects\Apress\FooterLinksV1\Deployment\FooterLinksList.wsp"
$SolutionPackage  = "FooterLinksList.wsp"

Write-Host "Deploying Solution to the Farm..."
Install-SPSolution -Identity $SolutionPackage -Local -GACDeployment -WebApplication $WebApp -Force
Write-Host "Deployment complete."

Write-Host
Write-Host "Script has completed. Press Enter to continue."
Read-Host