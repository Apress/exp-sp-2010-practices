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

        public override void FeatureUpgrading(SPFeatureReceiverProperties props, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        {
            SPWeb site = props.Feature.Parent as SPWeb;
            if (site != null)
            {
                // determine which custom upgrade action is executing
                switch (upgradeActionName)
                {
                    case "SetIsActiveColumn":
                        // See if the Footer Links List exists 
                        SPList list = site.Lists.TryGetList("Footer Links");
                        if (list != null)
                        {
                            // Set the new IsActive Flag for each item on the current list
                            SPListItemCollection items = list.Items;
                            foreach (SPListItem listItem in items)
                            {
                                listItem["IsActive"] = true;
                                listItem.Update();
                            }
                        }

                        break;
                    default:
                        // exit if unknown feature action
                        break;
                }
            }
        }
    }
}
