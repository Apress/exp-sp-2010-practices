using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;
using System.Linq;
using Apress.SharePoint.WP7.MyLinks.ListSvc;
 


namespace Apress.SharePoint.WP7.MyLinks
{
    public class NewLinkViewModel : INotifyPropertyChanged
    {

        #region Properties

        private Link dataModel;
        public Link DataModel
        {
            get
            {
                return dataModel;
            }
            set
            {
                dataModel = value;
                dataModel.PropertyChanged += new PropertyChangedEventHandler(dataModel_PropertyChanged);
            }
        }

        public bool AnyChange { get; set; }

        private Action SaveCompleteAction = null;

        #endregion

        #region Constructors

        public NewLinkViewModel()

        { DataModel = new Link(); }



        #endregion

        #region Event Handlers

        void dataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AnyChange = true;
        }

        #endregion

        #region Internal Methods


        public void Save(Action SaveCompleteAction)
        {
            this.SaveCompleteAction = SaveCompleteAction;
            AddLink(this.dataModel);
            AnyChange = false;
        }

        #endregion

        #region SharePoint Integration

        private void AddLink(Link link)
        {
          
            //TODO: AddLink code here
            XElement updateQuery =
                new XElement("Batch",
                  new XAttribute("OnError", "Continue"),
                  new XAttribute("ListVersion", "1"),

                  new XElement("Method", new XAttribute("ID", "1"),
                    new XAttribute("Cmd", "New"),
                      new XElement("Field", new XAttribute("Name", "ID"), "New"),
                       new XElement("Field", new XAttribute("Name", "URL"), Link.BuildSPLink(link.Title, link.HyperLink))
            ));

            ListsSoapClient svc = new ListsSoapClient();
            svc.CookieContainer = App.Cookies;
            svc.UpdateListItemsCompleted += new EventHandler<UpdateListItemsCompletedEventArgs>(svc_UpdateListItemsCompleted);
            svc.UpdateListItemsAsync(Constants.MYLINKS_LIST_TITLE, updateQuery);

        }

        void svc_UpdateListItemsCompleted(object sender, UpdateListItemsCompletedEventArgs e)
        {
            //TODO: svc_UpdateListItemCompleted code here
            Deployment.Current.Dispatcher.BeginInvoke(SaveCompleteAction);
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