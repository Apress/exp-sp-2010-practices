using System;
using System.ServiceModel;
using System.Windows;
using Microsoft.Phone.Controls;



namespace Apress.SharePoint.WP7.Personal.Views
{
    public partial class LogIn : PhoneApplicationPage
    {
        LogInViewModel viewModel;

        public LogIn()
        {
            viewModel = new LogInViewModel();
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(UserNameValue.Text)) && (!String.IsNullOrEmpty(PasswordValue.Password)))
            {
                FBA fba = new FBA();
                fba.OnAuthenticated += new EventHandler<FBAAuthenticatedEventArgs>(fba_OnAuthenticated);
                fba.OnFailedAuthentication += new EventHandler(fba_OnFailedAuthentication);
                fba.Authenticate(UserNameValue.Text, PasswordValue.Password, Constants.AUTHENTICATION_SERVICE_URL);
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
            App.IsAuthenticated = true;
            App.Cookies = e.CookieJar;
            
            this.Dispatcher.BeginInvoke(() =>
                {
                    //Get the full UserName and then redirect the user
                    GetUserName(this.UserNameValue.Text);
                }
            );
        }

        private void GetUserName(string userName)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(Constants.PEOPLE_SERVICE_URL);

            PeopleSvc.PeopleSoapClient people = new PeopleSvc.PeopleSoapClient();

            people.CookieContainer = App.Cookies;
            people.ResolvePrincipalsCompleted += new EventHandler<PeopleSvc.ResolvePrincipalsCompletedEventArgs>(p_ResolvePrincipalsCompleted);

            PeopleSvc.ArrayOfString users = new PeopleSvc.ArrayOfString();
            users.Add(userName);
            people.ResolvePrincipalsAsync(users, PeopleSvc.SPPrincipalType.User, true);

        }

        void p_ResolvePrincipalsCompleted(object sender, PeopleSvc.ResolvePrincipalsCompletedEventArgs e)
        {
            if ((e.Result != null) && (e.Result.Count == 1))
            {
                App.UserName = e.Result[0].AccountName;
                NavigationService.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Failed to get the USer Account");
            }
        }
    }
}