using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.API.Models
{
    public class Collection
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public List<string> tags { get; set; }
        public string creatorId { get; set; }
        public string responseTimeFuzzy { get; set; }
        public string description { get; set; }
        public string shortDescription { get; set; }
        public Image image { get; set; }
        public Metadata metadata { get; set; }
        public CollectionVirtuals virtuals { get; set; }
        public string type { get; set; }
    }
}
