using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;

namespace Apress.SharePoint.WP7.MyLinks
{
    public partial class IEView : PhoneApplicationPage
    {
        string navigateTo;

        #region Constructors

        public IEView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(IEView_Loaded);
        }

        #endregion

        #region Event Handlers

        void IEView_Loaded(object sender, RoutedEventArgs e)
        {
            webBrowser.Navigate(new Uri(navigateTo));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            IDictionary<string, string> queryString = this.NavigationContext.QueryString;
            if (queryString.ContainsKey("navTo"))
            {
                navigateTo = queryString["navTo"];
            }
            else
            {
                MessageBox.Show("No url to navigate to.");
            }

            base.OnNavigatedTo(e);
        }

        #endregion

    }
}