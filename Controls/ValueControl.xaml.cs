using System;
using System.Windows;
using System.Windows.Controls;
using Medium.API.Models;
using Microsoft.Phone.Tasks;

namespace Medium
{
    public partial class ValueControl : UserControl
    {
        public ValueControl()
        {
            InitializeComponent();
        }

        private void ValueTitle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Value item = ((FrameworkElement)sender).DataContext as Value;

            WebBrowserTask webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = new Uri(item.FriendlyUrl);

            webBrowserTask.Show();
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            Value item = ((FrameworkElement)sender).DataContext as Value;

            ShareLinkTask shareLinkTask = new ShareLinkTask();

            shareLinkTask.Title = item.title;
            shareLinkTask.LinkUri = new Uri(item.FriendlyUrl);
            shareLinkTask.Message = "Check out this article I found on Medium for Windows Phone!";
            shareLinkTask.Show();
        }
    }
}
