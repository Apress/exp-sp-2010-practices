﻿#pragma checksum "C:\Projects\Apress.SharePoint.WP7.Personal\Views\LogIn.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B76E7AB83F1556A02EFBF97DB93359FD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Apress.SharePoint.WP7.Personal.Views {
    
    
    public partial class LogIn : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox UserNameValue;
        
        internal System.Windows.Controls.PasswordBox PasswordValue;
        
        internal System.Windows.Controls.Button Login;
        
        internal System.Windows.Controls.TextBlock userNameLabel;
        
        internal System.Windows.Controls.TextBlock passwordLabel;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Apress.SharePoint.WP7.Personal;component/Views/LogIn.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.UserNameValue = ((System.Windows.Controls.TextBox)(this.FindName("UserNameValue")));
            this.PasswordValue = ((System.Windows.Controls.PasswordBox)(this.FindName("PasswordValue")));
            this.Login = ((System.Windows.Controls.Button)(this.FindName("Login")));
            this.userNameLabel = ((System.Windows.Controls.TextBlock)(this.FindName("userNameLabel")));
            this.passwordLabel = ((System.Windows.Controls.TextBlock)(this.FindName("passwordLabel")));
        }
    }
}

