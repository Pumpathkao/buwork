using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Models
{
    public class X
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class inspeclist
    {
        public string type { get; set; }
        public List<X> x { get; set; }
    }
}
