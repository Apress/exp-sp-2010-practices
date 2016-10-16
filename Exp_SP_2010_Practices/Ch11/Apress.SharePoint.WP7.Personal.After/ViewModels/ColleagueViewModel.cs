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
         private string _fullName;
        /// <summary>
        /// Colleague ViewModel FullName property; this property is the Full Name of the colleague.
        /// </summary>
        /// <returns></returns>
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                if (value != _fullName)
                {
                    _fullName = value;
                    NotifyPropertyChanged("FullName");
                }
            }
        }

        private string _accountName;
        /// <summary>
        /// Colleague ViewModel AccountName property; this property is the Colleagues Account Name.
        /// </summary>
        /// <returns></returns>
        public string AccountName
        {
            get
            {
                return _accountName;
            }
            set
            {
                if (value != _accountName)
                {
                    _accountName = value;
                    NotifyPropertyChanged("AccountName");
                }
            }
        }

        private string _personalUrl;
        /// <summary>
        /// Colleague ViewModel PersonalUrl property; this property is the URL to the Colleagues Personal Site.
        /// </summary>
        /// <returns></returns>
        public string PersonalUrl
        {
            get
            {
                return _personalUrl;
            }
            set
            {
                if (value != _personalUrl)
                {
                    _personalUrl = value;
                    NotifyPropertyChanged("PersonalUrl");
                }
            }
        }

        private string _title;
        /// <summary>
        /// Colleague ViewModel PersonalUrl property; 
        /// this property is the Title from the User Profile.
        /// </summary>
        /// <returns></returns>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _email;
        /// <summary>
        /// Colleague ViewModel PersonalUrl property; 
        /// this property is the E-mail from the User Profile.
        /// </summary>
        /// <returns></returns>
        public string EMail
        {
            get
            {
                return _email;
            }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    NotifyPropertyChanged("EMail");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
       
    }
    
}
