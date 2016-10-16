using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace FooterLinksList.Footer
{
    [ToolboxItemAttribute(false)]
    public class Footer : WebPart
    {
        [WebBrowsable(true),
        WebDisplayName("Website Url"),
        WebDescription("Website url for the footer list."),
        Personalizable(PersonalizationScope.Shared),
        Category("Footer Properties"),
        DefaultValue("")]
        public string WebUrl { get; set; }

        [WebBrowsable(true),
        WebDisplayName("List Name"),
        WebDescription("The name of the footer list."),
        Personalizable(PersonalizationScope.Shared),
        Category("Footer Properties"),
        DefaultValue("")]
        public string ListName { get; set; }

        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/FooterLinksList/Footer/FooterUserControl.ascx";

        protected override void CreateChildControls()
        {
            this.ChromeType = PartChromeType.None;
            FooterUserControl control = (FooterUserControl)Page.LoadControl(_ascxPath);

            // set control properties
            control.WebUrl = WebUrl;
            control.ListName = ListName;

            Controls.Add(control);
        }
    }
}
