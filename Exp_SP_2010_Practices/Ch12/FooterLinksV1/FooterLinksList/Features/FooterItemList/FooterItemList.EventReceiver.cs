using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace FooterLinksList.Features.FooterItemList
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("89d43549-8b5b-43a0-b266-b15bb31615ee")]
    public class FooterItemListEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb site = properties.Feature.Parent as SPWeb;

            // Enable Management of Content Types for Footer Links list
            SPList listFooterLinks = site.Lists["Footer Links"];
            listFooterLinks.ContentTypesEnabled = true;
            listFooterLinks.Update();
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        { 
            SPWeb site = properties.Feature.Parent as SPWeb;
            if (site != null)
            {
                SPList list = site.Lists.TryGetList("Footer Links");
                if (list != null)
                {
                    list.Delete();
                }
            }
        }
    }
}
