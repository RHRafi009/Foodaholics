using Foodaholics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Repository
{
    public class BlogContentRepository : Repository<BlogContent>
    {


        public string GetContent(int blogId)
        {
            string content = "";
            List<BlogContent> cList = GetAll();
            
            foreach (BlogContent bc in cList)
            {
                if (bc.BlogID == blogId)
                    content = bc.Value;
            }

            return content;
        }

        public void RemoveContent(int BlogId)
        {
            List<BlogContent> cList = GetAll();
            foreach (BlogContent bc in cList)
            {
                if (bc.BlogID == BlogId)
                    Delete(bc);
            }
        }

        public void SaveDraft(WriteBlog wb, int BlogId)
        {
            RemoveContent(BlogId);
            BlogContent cb = new BlogContent();
            cb.BlogID = BlogId;
            cb.Value = wb.Content;
            Insert(cb);
        }

        public void SavePost(WriteBlog wb, int BlogId)
        {
            SaveDraft(wb, BlogId);
            BlogRepository bRepo = new BlogRepository();
            Blog b = bRepo.GetById(BlogId);
            bRepo.UpdateStatusId(b, 2);
        }
    }
}