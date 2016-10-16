using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Collections.ObjectModel;
using Apress.SharePoint.WP7.Personal.UserProfileSvc;
using Microsoft.Silverlight.Samples;
using SilverlightGuidAsmx.Behaviors;


namespace Apress.SharePoint.WP7.Personal.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        //TODO : Add the ColleaguesViewModel Collection


        //TODO : Add the MyProfileViewModel property
        
        
        public MainViewModel()
        {
            //TODO : Create the Colleagues Observable Collection
            //TODO : Create the MyProfileViewModel instance
            
        }


        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            //Add Colleagues
            LoadMyColleagues();
            //LoadMyProfile
            LoadMyProfile();

            this.IsDataLoaded = true;
        }

        public void LoadMyColleagues()
        {
            //TODO Complete the LoadMyColleagues Method
        }

        void svc_GetUserColleaguesCompleted(object sender, GetUserColleaguesCompletedEventArgs e)
        {
            //TODO Complete the svc_GetUserColleaguesCompleted Method
        }

        private void LoadMyProfile()
        {
            //TODO : Load data from the User Profile Service
        }

        void svc_GetUserProfileByNameCompleted(object sender, GetUserProfileByNameCompletedEventArgs e)
        {
            //TODO : Complete the svc_GetUserProfileByNameCompleted event handler

        }

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