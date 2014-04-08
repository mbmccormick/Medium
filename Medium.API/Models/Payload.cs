﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medium.API.Models
{
    public class Payload
    {
        public List<Value> value { get; set; }
        public object nextSlug { get; set; }
        public string prevSlug { get; set; }
        public References references { get; set; }
    }
}
