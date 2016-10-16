using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Apress.SharePoint.WP7.MyLinks
{
    public partial class NewLinkView : PhoneApplicationPage
    {
        NewLinkViewModel viewModel = new NewLinkViewModel();

        #region Constructors

        public NewLinkView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        #endregion

        #region Events

        private void save_Click(object sender, RoutedEventArgs e)
        {
            bool canSave = true;

            if (String.IsNullOrEmpty(TitleText.Text))
            {
                MessageBox.Show("You must enter a title.");
                canSave = false;
            }

            if (canSave & String.IsNullOrEmpty(LinkText.Text))
            {
                MessageBox.Show("You must enter a Url.");
                canSave = false;
            }

            if(canSave & (!LinkText.Text.ToUpper().StartsWith("HTTP://") && !LinkText.Text.ToUpper().StartsWith("HTTPS://")))
            {
                MessageBox.Show("Url must begin with http:// or https://.");
                canSave = false;
            }

            if (canSave)
            {
                TitleText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                LinkText.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                viewModel.Save(() =>
                {   
                    NavigationService.GoBack();
                    PhoneApplicationService.Current.State["RefreshNeeded"] = true;
                });
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }


        #endregion

    }
}