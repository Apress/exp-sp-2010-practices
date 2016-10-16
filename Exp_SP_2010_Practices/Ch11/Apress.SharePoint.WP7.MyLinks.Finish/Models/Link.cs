using System;
using System.ComponentModel;

namespace Apress.SharePoint.WP7.MyLinks
{
    public class Link : INotifyPropertyChanged
    {
        
        //Add code here
        private int id;
        public int Id
        {
            get
            { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string hyperLink;
        public string HyperLink
        {
            get { return hyperLink; }
            set
            {
                hyperLink = value;
                NotifyPropertyChanged("HyperLink");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public Link() { }

        public Link(int id, string SPLinkValue)
        {
            Id = id;
            string[] split = SPLinkValue.Split(',');
            HyperLink = split[0];
            Title = split[1];
        }

        public static string BuildSPLink(string title, string url)
        {
            return string.Format("{0}, {1}", url, title);
        }


        #region INotifyPropertyChanged

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
