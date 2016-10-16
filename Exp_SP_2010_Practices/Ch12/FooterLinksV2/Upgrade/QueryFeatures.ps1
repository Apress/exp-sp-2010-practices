Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$WebApp = "http://intranet.theopenhighway.net"
$webApp = [Microsoft.SharePoint.Administration.SPWebApplication]::Lookup($WebApp)

# =========================================================================================
$featureId = New-Object System.Guid -ArgumentList "fdcb05db-952a-426a-ba77-ee40d632fa49"
$featureDisplayName = "FooterLinksSchema_FooterLinksSchema"

Write-Host "Feature:"$featureDisplayName
Write-Host "=========================================================================="

# This is the feature that has been INSTALLED (e.g. 1.1.0.0)
$feature2 = get-spfeature | where {$_.DisplayName -eq $featureDisplayName } 
foreach($ifeature in $feature2){
  Write-Host "The following version is the INSTALLED Version: "$ifeature.Version
}

# This is the version that is currently RUNNING in the each site (e.g. 1.0.0.0)
# where the feature is currently activated
$features = $webApp.QueryFeatures($FeatureId)
foreach($feature in $features){
  Write-Host "Version "$feature.Version" is currently RUNNING in site at "$feature.Parent.Url
}
Write-Host

# =========================================================================================
$featureId = New-Object System.Guid -ArgumentList "aaaef021-32c2-4d98-9bf0-178b183eabf3"
$featureDisplayName = "FooterLinksList_FooterItemList"

Write-Host "Feature:"$featureDisplayName
Write-Host "=========================================================================="

# This is the feature that has been INSTALLED (e.g. 1.1.0.0)
$feature2 = get-spfeature | where {$_.DisplayName -eq $featureDisplayName } 
foreach($ifeature in $feature2){
  Write-Host "The following version is the INSTALLED Version: "$ifeature.Version
}

# This is the version that is currently RUNNING in the each site (e.g. 1.0.0.0)
# where the feature is currently activated
$features = $webApp.QueryFeatures($FeatureId)
foreach($feature in $features){
  Write-Host "Version "$feature.Version" is currently RUNNING in site at "$feature.Parent.Url
}
Write-Host

# =========================================================================================
$featureId = New-Object System.Guid -ArgumentList "4e8264e8-3b6d-4e9c-8376-f0f338ad63f5"
$featureDisplayName = "FooterLinksList_FooterWebPart"

Write-Host "Feature:"$featureDisplayName
Write-Host "=========================================================================="

# This is the feature that has been INSTALLED (e.g. 1.1.0.0)
$feature2 = get-spfeature | where {$_.DisplayName -eq $featureDisplayName } 
foreach($ifeature in $feature2){
  Write-Host "The following version is the INSTALLED Version: "$ifeature.Version
}

# This is the version that is currently RUNNING in the each site (e.g. 1.0.0.0)
# where the feature is currently activated
$features = $webApp.QueryFeatures($FeatureId)
foreach($feature in $features){
  Write-Host "Version "$feature.Version" is currently RUNNING in site at "$feature.Parent.Url
}
Write-Host
        
Write-Host
Write-Host "Script has completed. Press Enter to continue."
Read-Host