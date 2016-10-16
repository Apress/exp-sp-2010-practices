using System;
using System.ComponentModel;

namespace Apress.SharePoint.WP7.MyLinks
{
    public class Link : INotifyPropertyChanged
    {
        
        //Add code here

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
