using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Apress.SharePoint.WP7.Personal.ViewModels
{
    public class MyProfileViewModel : INotifyPropertyChanged
    {
        //public MyProfileViewModel(){}

        private string _fullName;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
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

        private string _title;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
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

        private string _pictureUrl;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string PictureUrl
        {
            get
            {
                return _pictureUrl;
            }
            set
            {
                if (value != _pictureUrl)
                {
                    _pictureUrl = value;
                    NotifyPropertyChanged("PictureUrl");
                }
            }
        }

        private string _aboutMe;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string AboutMe
        {
            get
            {
                return _aboutMe;
            }
            set
            {
                if (value != _aboutMe)
                {
                    _aboutMe = value;
                    NotifyPropertyChanged("AboutMe");
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
