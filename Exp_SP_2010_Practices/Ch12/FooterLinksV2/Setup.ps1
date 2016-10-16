Write-Host 

$SiteTitle = "SharePoint Deployment Best Practices - V2 Site"
$SiteUrl = "http://intranet.theopenhighway.net/sites/apress2"
$SiteTemplate = "STS#0"

# check to ensure Microsoft.SharePoint.PowerShell is loaded
Write-Host "Loading SharePoint Powershell Snapin" -ForegroundColor Yellow
Add-PSSnapin "Microsoft.SharePoint.Powershell" -ErrorAction SilentlyContinue

# delete any existing site found at target URL
$targetUrl = Get-SPSite -Limit All  | Where-Object {$_.Url -eq $SiteUrl}
if ($targetUrl -ne $null) {
  Write-Host "Deleting existing site at" $SiteUrl
  Remove-SPSite -Identity $SiteUrl -Confirm:$false
}

# create new site at target URL
Write-Host "Creating new site at" $SiteUrl 
$NewSite = New-SPSite -URL $SiteUrl -OwnerAlias Administrator -Template $SiteTemplate -Name $SiteTitle
$RootWeb = $NewSite.RootWeb
# display site info to user
Write-Host "-------------------------------------"
Write-Host "Site created successfully"
Write-Host "Title:" $RootWeb.Title -ForegroundColor Green
Write-Host "URL:" $RootWeb.Url -ForegroundColor Red
Write-Host "ID:" $RootWeb.Id.ToString() -ForegroundColor Blue
Write-Host "-------------------------------------"

