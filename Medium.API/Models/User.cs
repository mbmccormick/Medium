using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.API.Models
{
    public class User
    {
        public string userId { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public object createdAt { get; set; }
        public object lastPostCreatedAt { get; set; }
        public string imageId { get; set; }
        public string backgroundImageId { get; set; }
        public string bio { get; set; }
        public UserVirtuals virtuals { get; set; }
        public string type { get; set; }
    }
}
