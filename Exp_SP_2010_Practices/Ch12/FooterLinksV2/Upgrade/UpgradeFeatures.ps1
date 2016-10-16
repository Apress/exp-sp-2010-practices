Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$WebApp = "http://intranet.theopenhighway.net"
$webApp = [Microsoft.SharePoint.Administration.SPWebApplication]::Lookup($WebApp)

# =========================================================================================
$featureId = New-Object System.Guid -ArgumentList "fdcb05db-952a-426a-ba77-ee40d632fa49"
$featureDisplayName = "FooterLinksSchema_FooterLinksSchema"

$features = $webApp.QueryFeatures($FeatureId, $true)

foreach($feature in $features){
  Write-Host "Updating feature"$featureDisplayName "at"$feature.Parent.Url
  $feature.Upgrade($true)
}

# =========================================================================================
$featureId = New-Object System.Guid -ArgumentList "aaaef021-32c2-4d98-9bf0-178b183eabf3"
$featureDisplayName = "FooterLinksList_FooterItemList"

$features = $webApp.QueryFeatures($FeatureId, $true)

foreach($feature in $features){
  Write-Host "Updating feature"$featureDisplayName "at"$feature.Parent.Url
 # $feature.Upgrade($true)
}

# =========================================================================================
$featureId = New-Object System.Guid -ArgumentList "4e8264e8-3b6d-4e9c-8376-f0f338ad63f5"
$featureDisplayName = "FooterLinksList_FooterWebPart"

$features = $webApp.QueryFeatures($FeatureId, $true)

foreach($feature in $features){
  Write-Host "Updating feature"$featureDisplayName "at"$feature.Parent.Url
  $feature.Upgrade($true)
}
        
        
Write-Host
Write-Host "Update Solution script has completed. Press OK to quit"
Read-Host