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
        
        #endregion

        #region SharePoint Integration

        public void LoadMyLinks()
        {

            //TODO: LoadMyLinks code here
          
        }

        void svc_GetListItemsCompleted(object sender, GetListItemsCompletedEventArgs e)
        {
            
            //TODO svc_GetListItemsCompleted code here

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