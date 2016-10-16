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
        public MainViewModel()
        {
            this.Colleagues = new ObservableCollection<ColleagueViewModel>();
            this.MyProfile = new MyProfileViewModel();
        }

        /// <summary>
        /// A collection for ColleagueViewModel objects.
        /// </summary>
        public ObservableCollection<ColleagueViewModel> Colleagues { get; private set; }


        private MyProfileViewModel _myProfile;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public MyProfileViewModel MyProfile
        {
            get
            {
                return _myProfile;
            }
            set
            {
                if (value != _myProfile)
                {
                    _myProfile = value;
                    NotifyPropertyChanged("MyProfile");
                }
            }
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

        private void LoadMyProfile()
        {
            //Create the Message Inspector
            SPAsmxMessageInspector messageInspector = new SPAsmxMessageInspector();
            //Apply the Message Inspector to the Binding
            BasicHttpMessageInspectorBinding binding = new BasicHttpMessageInspectorBinding(messageInspector);

            EndpointAddress endpoint = new EndpointAddress(Constants.USERPROFILE_SERVICE_URL);

            UserProfileSvc.UserProfileServiceSoapClient svc = new UserProfileServiceSoapClient(binding, endpoint);
            svc.CookieContainer = App.Cookies;
            svc.GetUserProfileByNameCompleted += new EventHandler<GetUserProfileByNameCompletedEventArgs>(svc_GetUserProfileByNameCompleted);
            svc.GetUserProfileByNameAsync(App.UserName);

        }

        void svc_GetUserProfileByNameCompleted(object sender, GetUserProfileByNameCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //For each user
                foreach (UserProfileSvc.PropertyData propertyData in e.Result)
                {

                    switch (propertyData.Name)
                    {
                        case "PreferredName":
                            MyProfile.FullName = propertyData.Values.Count > 0
                                                                           ? (propertyData.Values[0].Value as string)
                                                                           : String.Empty;
                            break;
                        case "Title":
                            MyProfile.Title = propertyData.Values.Count > 0
                                                                           ? (propertyData.Values[0].Value as string)
                                                                           : String.Empty;
                            break;

                        case "AboutMe":
                            MyProfile.AboutMe = propertyData.Values.Count > 0
                                                                           ? (propertyData.Values[0].Value as string)
                                                                           : String.Empty;
                            
                            break;

                        case "PictureURL":
                            MyProfile.PictureUrl = propertyData.Values.Count > 0
                                                                           ? (propertyData.Values[0].Value as string)
                                                                           : String.Empty;
                            break;

                    }
                }

            }
            else
            {
                Debug.WriteLine("Error: {0}", e.Error.Message);
            }
        }

        public void LoadMyColleagues()
        {
            UserProfileSvc.UserProfileServiceSoapClient svc = new UserProfileServiceSoapClient();
            svc.CookieContainer = App.Cookies;
            svc.GetUserColleaguesCompleted +=
                new EventHandler<GetUserColleaguesCompletedEventArgs>(svc_GetUserColleaguesCompleted);
            svc.GetUserColleaguesAsync(App.UserName);
        }

        void svc_GetUserColleaguesCompleted(object sender, GetUserColleaguesCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {

                    Colleagues.Clear();
                    
                    foreach (ContactData contactData in e.Result.ToList())
                    {
                        Colleagues.Add(new ColleagueViewModel()
                        {
                            AccountName = contactData.AccountName,
                            FullName = contactData.Name,
                            Title = contactData.Title,
                            EMail = contactData.Email,
                            PersonalUrl = contactData.Url
                        });
                    }


                });

                Debug.WriteLine("Colleagues: {0}", Colleagues.Count);
            }
            else
            {
                Debug.WriteLine("Error: {0}", e.Error.Message);
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