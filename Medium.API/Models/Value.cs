using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Medium.API.Models
{
    public class Value : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string id { get; set; }
        public string versionId { get; set; }
        public string creatorId { get; set; }
        public User creator { get; set; }
        public Collection homeCollection { get; set; }
        public string homeCollectionId { get; set; }
        public string intendedCollectionId { get; set; }
        public string title { get; set; }
        public string detectedLanguage { get; set; }
        public string latestVersion { get; set; }
        public string latestPublishedVersion { get; set; }
        public bool isPublished { get; set; }
        public bool hasUnpublishedEdits { get; set; }
        public int latestRev { get; set; }
        public object createdAt { get; set; }
        public object updatedAt { get; set; }
        public long acceptedAt { get; set; }
        public object firstPublishedAt { get; set; }
        public object latestPublishedAt { get; set; }
        public bool isRead { get; set; }
        public bool vote { get; set; }
        public string experimentalCss { get; set; }
        public string displayAuthor { get; set; }
        public ValueVirtuals virtuals { get; set; }
        public string type { get; set; }
        public bool _isPopulated { get; set; }

        private bool _is_read;
        public bool is_read
        {
            get
            {
                return _is_read;
            }

            set
            {
                _is_read = value;

                OnPropertyChanged("is_read");

                OnPropertyChanged("title_foreground");
                OnPropertyChanged("snippet_foreground");
                OnPropertyChanged("description_foreground");
            }
        }

        public SolidColorBrush title_foreground
        {
            get
            {
                if (this.is_read == true)
                    return new SolidColorBrush(Color.FromArgb(255, 195, 195, 195));
                else
                    return new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        public SolidColorBrush snippet_foreground
        {
            get
            {
                if (this.is_read == true)
                    return new SolidColorBrush(Color.FromArgb(255, 195, 195, 195));
                else
                    return new SolidColorBrush(Color.FromArgb(255, 130, 130, 130));
            }
        }

        public SolidColorBrush description_foreground
        {
            get
            {
                if (this.is_read == true)
                    return new SolidColorBrush(Color.FromArgb(63, 87, 173, 104));
                else
                    return new SolidColorBrush(Color.FromArgb(127, 87, 173, 104));
            }
        }

        public string FriendlyCollection
        {
            get
            {
                return homeCollection.name;
            }
        }
    
        public string FriendlySnippet
        {
            get
            {
                return virtuals.snippet;
            }
        }

        public Visibility FriendlySnippetVisibility
        {
            get
            {
                if (virtuals.snippet.Length > 0)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public string FriendlyDescription
        {
            get
            {
                return creator.name + " in " + FriendlyCollection + ", " + Math.Round(virtuals.readingTime, 0, MidpointRounding.AwayFromZero) + " minute read";
            }
        }

        public string FriendlyUrl
        {
            get
            {
                return "https://medium.com/" + homeCollection.slug + "/" + id;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
