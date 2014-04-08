using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.API.Models
{
    public class ValueVirtuals
    {
        public string currentCollectionId { get; set; }
        public string statusForCollection { get; set; }
        public string createdAtRelative { get; set; }
        public string updatedAtRelative { get; set; }
        public string acceptedAtRelative { get; set; }
        public string createdAtEnglish { get; set; }
        public string updatedAtEnglish { get; set; }
        public string acceptedAtEnglish { get; set; }
        public string firstPublishedAtEnglish { get; set; }
        public string latestPublishedAtEnglish { get; set; }
        public bool allowNotes { get; set; }
        public int languageTier { get; set; }
        public string snippet { get; set; }
        public Image previewImage { get; set; }
        public int wordCount { get; set; }
        public int imageCount { get; set; }
        public double readingTime { get; set; }
        public string draftSnippet { get; set; }
        public string subtitle { get; set; }
        public bool isOnReadingList { get; set; }
        public List<Mapping> postedIn { get; set; }
        public int publishedInCount { get; set; }
        public List<object> usersBySocialRecommends { get; set; }
        public List<object> notesBySocialRecommends { get; set; }
    }
}
