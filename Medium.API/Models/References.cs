using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.API.Models
{
    public class References
    {
        public Dictionary<string, Collection> Collection { get; set; }
        public Dictionary<string, User> User { get; set; }
    }
}
