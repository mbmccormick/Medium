using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.API.Models
{
    public class Image
    {
        public string imageId { get; set; }
        public string filter { get; set; }
        public string backgroundSize { get; set; }
        public int originalWidth { get; set; }
        public int originalHeight { get; set; }
        public string strategy { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }
}
