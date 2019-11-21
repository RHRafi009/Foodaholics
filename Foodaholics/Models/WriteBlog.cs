using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Models
{
    public class WriteBlog
    {
        public int id { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<string> ParagraphProcess()
        {
            string[] pListArr = Content.Split(new string[] { "*P*" }, StringSplitOptions.None);
            List<string> pList = new List<string>();
            foreach(string s in pListArr)
            {
                pList.Add(s);
            }
            return pList;
        }
    }
}