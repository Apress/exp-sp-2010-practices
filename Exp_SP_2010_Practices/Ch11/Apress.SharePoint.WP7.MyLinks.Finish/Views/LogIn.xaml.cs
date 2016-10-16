using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace Apress.SharePoint.WP7.MyLinks
{
    public partial class LogIn : PhoneApplicationPage
    {

        #region Constructors

        public LogIn()
        {
           InitializeComponent();
        }

        #endregion

        #region Event Handlers

       //TODO: Add Event Handler code here
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(UserNameValue.Text)) &&
                (!String.IsNullOrEmpty(PasswordValue.Password)))
            {
                FBA fba = new FBA();
                fba.OnAuthenticated += new
                         EventHandler<FBAAuthenticatedEventArgs>(fba_OnAuthenticated);
                fba.OnFailedAuthentication += new
                         EventHandler(fba_OnFailedAuthentication);
                fba.Authenticate(UserNameValue.Text,
                                 PasswordValue.Password,
                                 Constants.AUTHENTICATION_SERVICE_URL);
            }
            else
            {
                MessageBox.Show("Please enter user name and password.");
            }
        }

        void fba_OnFailedAuthentication(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("Failed Login");
            });
        }

        void fba_OnAuthenticated(object sender, FBAAuthenticatedEventArgs e)
        {

            App.Cookies = e.CookieJar;
            this.Dispatcher.BeginInvoke(() =>
            {
                NavigationService.Navigate(new Uri("/Views/MainPage.xaml",
                                                   UriKind.Relative));
            });
        }


        #endregion
   
    }
} 