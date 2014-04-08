using System.Collections.Generic;

namespace Medium.API.Models
{
    public class Top100Response
    {
        public bool success { get; set; }
        public Payload payload { get; set; }
        public int v { get; set; }
        public string b { get; set; }
    }
}
