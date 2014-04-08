using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.API.Models
{
    public class Mapping
    {
        public string collectionId { get; set; }
        public Collection collection { get; set; }
        public string status { get; set; }
        public string posterId { get; set; }
    }
}
