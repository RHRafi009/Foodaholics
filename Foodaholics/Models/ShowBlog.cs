using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Models
{
    public class ShowBlog
    {
        public int id { get; set; }
        public string Title { get; set; }

        public string CoverPicturePath { get; set; }

        public int like { get; set; }

        public int watch { get; set; }

        public string WriterName { get; set; }

        public DateTime Posted { get; set; }

        public DateTime Created { get; set; }

        public ShowBlog()
        {

        }
    }
}