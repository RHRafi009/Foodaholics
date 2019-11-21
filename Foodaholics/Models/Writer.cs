using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Models
{
    public class Writer
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string ProfilePicturePath { get; set; }
        public int Watch { get; set; }
    }
}