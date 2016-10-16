using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Apress.SharePoint.WP7.Personal.UserProfileSvc;

namespace Apress.SharePoint.WP7.Personal.ViewModels
{
    public class ColleagueViewModel : INotifyPropertyChanged
    {
        //TODO : Complete the Colleagues ViewNodel class



        #region Property Changed Handler

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
