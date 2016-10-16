using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Apress.SharePoint.WP7.MyLinks
{
    public partial class MainPage : PhoneApplicationPage
    {
        MyLinksViewModel myLinksviewModel;

        #region Constructors

        public MainPage()
        {
            InitializeComponent();
            myLinksviewModel = new MyLinksViewModel();
            this.DataContext = myLinksviewModel;
            myLinksviewModel.LoadMyLinks();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

        }

        #endregion

        #region Event Handlers

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("RefreshNeeded")
                && (PhoneApplicationService.Current.State["RefreshNeeded"] is bool)
                && (bool)PhoneApplicationService.Current.State["RefreshNeeded"])
            {
                myLinksviewModel.LoadMyLinks();
                PhoneApplicationService.Current.State.Remove("RefreshNeeded");
            }
        
        }
          
        private void MyLinksRefresh_Click(object sender, EventArgs e)
        {
            myLinksviewModel.LoadMyLinks();
        }

        private void MyLinksAdd_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewLinkView.xaml", UriKind.Relative));
        }

        private void MyLinksListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (MyLinksListBox.SelectedIndex < 0)
            {
                return;
            }

            var selectedLink = MyLinksListBox.SelectedItem as Link;
            string navigateTo = selectedLink.HyperLink;
            NavigationService.Navigate(new Uri(string.Format("/Views/IEView.xaml?navTo={0}", navigateTo), UriKind.Relative));

        }

        #endregion
    }
}