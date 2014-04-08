using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Medium.API.Models;
using Medium.Common;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Medium
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region List Properties

        public static ObservableCollection<Value> Top100Posts { get; set; }

        #endregion

        private bool isTop100Loaded = false;

        ApplicationBarIconButton refresh;

        ApplicationBarMenuItem feedback;
        ApplicationBarMenuItem about;

        public MainPage()
        {
            InitializeComponent();

            App.UnhandledExceptionHandled += new EventHandler<ApplicationUnhandledExceptionEventArgs>(App_UnhandledExceptionHandled);

            Top100Posts = new ObservableCollection<Value>();

            this.BuildApplicationBar();
        }

        private void BuildApplicationBar()
        {
            refresh = new ApplicationBarIconButton();
            refresh.IconUri = new Uri("/Resources/Refresh.png", UriKind.RelativeOrAbsolute);
            refresh.Text = "refresh";
            refresh.Click += Refresh_Click;

            feedback = new ApplicationBarMenuItem();
            feedback.Text = "feedback";
            feedback.Click += Feedback_Click;

            about = new ApplicationBarMenuItem();
            about.Text = "about";
            about.Click += About_Click;

            // build application bar
            ApplicationBar.Buttons.Add(refresh);

            ApplicationBar.MenuItems.Add(feedback);
            ApplicationBar.MenuItems.Add(about);
        }

        private void App_UnhandledExceptionHandled(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                ToggleLoadingText();
                ToggleEmptyText();

                this.prgLoading.Visibility = System.Windows.Visibility.Collapsed;
            });
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.IsNavigationInitiator == false)
            {
                LittleWatson.CheckForPreviousException(true);

                TrialManager.CheckLicense();

                if (isTop100Loaded == false)
                {
                    LoadData(false);
                }

                FeedbackHelper.PromptForRating();
            }
            else
            {
                LoadData(true);
            }
        }

        private void LoadData(bool isNavigationInitiator)
        {
            this.prgLoading.Visibility = System.Windows.Visibility.Visible;

            if (isNavigationInitiator == false)
            {
                App.MediumClient.GetTop100Posts((result) =>
                {
                    SmartDispatcher.BeginInvoke(() =>
                    {
                        Top100Posts.Clear();

                        foreach (Value item in result)
                        {
                            Top100Posts.Add(item);
                        }

                        isTop100Loaded = true;

                        if (isTop100Loaded)
                        {
                            ToggleLoadingText();
                            ToggleEmptyText();

                            this.prgLoading.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    });
                });
            }
        }

        private void ToggleLoadingText()
        {
            this.txtTop100PostsLoading.Visibility = System.Windows.Visibility.Collapsed;

            this.lstTop100Posts.Visibility = System.Windows.Visibility.Visible;
        }

        private void ToggleEmptyText()
        {
            if (Top100Posts.Count == 0)
                this.txtTop100PostsEmpty.Visibility = System.Windows.Visibility.Visible;
            else
                this.txtTop100PostsEmpty.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            if (this.prgLoading.Visibility == System.Windows.Visibility.Visible) return;

            isTop100Loaded = false;

            LoadData(false);
        }

        private void Feedback_Click(object sender, EventArgs e)
        {
            if (this.prgLoading.Visibility == System.Windows.Visibility.Visible) return;

            FeedbackHelper.Default.Feedback();
        }

        private void About_Click(object sender, EventArgs e)
        {
            if (this.prgLoading.Visibility == System.Windows.Visibility.Visible) return;

            NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
        }

        private int _offsetKnob = 5;

        private void LongListSelector_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (this.prgLoading.Visibility == System.Windows.Visibility.Visible) return;

            LongListSelector target = (LongListSelector)sender;

            if (target.ItemsSource != null &&
                target.ItemsSource.Count >= _offsetKnob)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if ((e.Container.Content as Value).Equals(target.ItemsSource[target.ItemsSource.Count - _offsetKnob]))
                    {
                        this.prgLoading.Visibility = System.Windows.Visibility.Visible;

                        if (target == this.lstTop100Posts)
                        {
                            App.MediumClient.GetNextTop100Posts((result) =>
                            {
                                SmartDispatcher.BeginInvoke(() =>
                                {
                                    foreach (Value item in result)
                                    {
                                        Top100Posts.Add(item);
                                    }

                                    this.prgLoading.Visibility = System.Windows.Visibility.Collapsed;
                                });
                            });
                        }
                    }
                }
            }
        }

        private void LongListSelector_ItemUnrealized(object sender, ItemRealizationEventArgs e)
        {
        }
    }
}