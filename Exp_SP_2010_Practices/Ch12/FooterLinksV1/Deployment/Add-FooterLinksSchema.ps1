Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$Path = "C:\Projects\Apress\Deployment\FooterLinksSchema.wsp"
$SolutionPackage  = "FooterLinksSchema.wsp"

Write-Host "Adding Solution to solution store..."
Add-SPSolution -LiteralPath $Path
Write-Host
Write-Host "Solution added."


Write-Host
Write-Host "Script has completed. Press Enter to continue."
Read-Host