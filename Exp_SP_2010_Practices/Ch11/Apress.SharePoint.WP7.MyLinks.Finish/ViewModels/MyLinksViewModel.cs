using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;
using Apress.SharePoint.WP7.MyLinks.ListSvc;


namespace Apress.SharePoint.WP7.MyLinks
{
    public class MyLinksViewModel : INotifyPropertyChanged
    {
        #region Properties

        //TODO MyLinks ObservableCollection here
        private ObservableCollection<Link> myLinks;
        public ObservableCollection<Link> MyLinks
        {
            get { return myLinks; }
            set
            {
                myLinks = value;
                NotifyPropertyChanged("MyLinks");
            }
        }

        
        #endregion

        #region SharePoint Integration

        public void LoadMyLinks()
        {

            //TODO: LoadMyLinks code here
            XElement viewFields = new XElement("ViewFields",
                              new XElement("FieldRef",
                               new XAttribute("Name", "ows_URL")));

            ListsSoapClient svc = new ListsSoapClient();
            svc.CookieContainer = App.Cookies;
            svc.GetListItemsCompleted += new
                          EventHandler<GetListItemsCompletedEventArgs>
            (svc_GetListItemsCompleted);
            svc.GetListItemsAsync("MyLinks", string.Empty, null,
                                  viewFields, null, null, null);

          
        }

        void svc_GetListItemsCompleted(object sender, GetListItemsCompletedEventArgs e)
        {
            
            //TODO svc_GetListItemsCompleted code here
            IEnumerable<XElement> rows = e.Result.Descendants
                                (XName.Get("row", "#RowsetSchema"));
            var myLinks = from element in rows
                          select new Link(
                                        (int)element.Attribute("ows_ID"),
                                        (string)element.Attribute("ows_URL")
                                      );

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (MyLinks == null)
                {
                    MyLinks = new ObservableCollection<Link>();
                }
                MyLinks.Clear();
                myLinks.ToList().ForEach(a => MyLinks.Add(a));
            });


        }

        

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}