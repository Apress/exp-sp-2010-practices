using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FooterLinksList.Footer
{
    public partial class FooterUserControl : UserControl
    {
        private const string TitleField = "Title";
        private const string UrlField = "URL";

        public string ListName { get; set; }
        public string WebUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web != null)
            {
                try
                {
                    SPWeb web = SPContext.Current.Site.RootWeb;
                    SPList list = web.Lists["Footer Links"];

                    if (list != null)
                    {
                        SPQuery query = new SPQuery();
                        query.Query = @"
<Where>
    <Eq>
        <FieldRef Name=""IsActive"" />
        <Value Type=""Integer"">1</Value>
    </Eq>
</Where>
<OrderBy>
    <FieldRef Name=""SortOrder"" Ascending=""True"" />
</OrderBy>";

                        // Returns all items 
                        SPListItemCollection footerItems = list.GetItems(query);

                        // create data table with results returned
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Title");
                        dt.Columns.Add("Url");

                        foreach (SPListItem item in footerItems)
                        {
                            DataRow dr = dt.NewRow();

                            dr["Title"] = item[TitleField] as string;

                            if (item[UrlField] != null)
                            {
                                SPFieldUrlValue url = new SPFieldUrlValue(item[UrlField].ToString());

                                if (!string.IsNullOrEmpty(url.Url))
                                {
                                    dr["Url"] = url.Url;
                                }
                            }

                            dt.Rows.Add(dr);
                        }

                        FooterList.DataSource = dt.DefaultView;
                        FooterList.DataBind();
                    }
                    else
                    {
                        // clear controls
                        this.Controls.Clear();
                    }
                }

                catch (Exception ex)
                {
                    // clear controls
                    this.Controls.Clear();
                }

            }
        }
    }
}
