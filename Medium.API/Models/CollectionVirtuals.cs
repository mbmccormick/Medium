using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.API.Models
{
    public class CollectionVirtuals
    {
        public bool canPost { get; set; }
        public bool canAdminister { get; set; }
        public bool isSubscribed { get; set; }
    }
}
