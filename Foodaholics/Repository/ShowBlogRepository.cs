using Foodaholics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Repository
{
    public class ShowBlogRepository
    {
        BlogRepository bRepo = new BlogRepository();

        private ShowBlog Create(Blog b)
        {
            ShowBlog tempSBlog = new ShowBlog();
            tempSBlog.id = b.id;
            tempSBlog.Title = b.Title;
            tempSBlog.CoverPicturePath = b.CoverPicturePath;
            tempSBlog.like = b.Love;
            tempSBlog.watch = b.Watch;
            tempSBlog.Posted = (DateTime) b.Posted;
            tempSBlog.Created = (DateTime) b.Created;
            tempSBlog.WriterName = bRepo.WriterName(b.id);
            return tempSBlog;
        }


        public List<ShowBlog> GetAll()
        {
            List<ShowBlog> sBlog = new List<ShowBlog>();
            List<Blog> Blog = bRepo.GetAll();
            for(int i=Blog.Count-1; i>=0; i--)
            {
                sBlog.Add(Create(Blog[i]));
            }
            return sBlog;
        }

        public List<ShowBlog> GetPosted()
        {
            List<ShowBlog> sBlog = new List<ShowBlog>();
            List<Blog> Blog = bRepo.GetAll();
            for (int i = Blog.Count - 1; i >= 0; i--)
            {
                if (Blog[i].StatusID == 2)
                    sBlog.Add(Create(Blog[i]));
            }
            return sBlog;
        }

        public List<ShowBlog> GetPosted(int id)
        {
            List<ShowBlog> sBlog = new List<ShowBlog>();
            List<Blog> Blog = bRepo.GetAll();
            for (int i = Blog.Count - 1; i >= 0; i--)
            {
                if (Blog[i].StatusID == 2 && Blog[i].WriterID == id)
                    sBlog.Add(Create(Blog[i]));
            }
            return sBlog;
        }

        public List<ShowBlog> GetPosted(string val)
        {
            List<ShowBlog> sBlog = new List<ShowBlog>();
            List<Blog> Blog = bRepo.GetAll();
            for (int i = Blog.Count - 1; i >= 0; i--)
            {
                if (Blog[i].StatusID == 2 && Blog[i].Title.ToLower().Contains(val.ToLower()))
                    sBlog.Add(Create(Blog[i]));
            }
            return sBlog;
        }

        public List<ShowBlog> GetDrafted(int id)
        {
            List<ShowBlog> sBlog = new List<ShowBlog>();
            List<Blog> Blog = bRepo.GetAll();
            for (int i = Blog.Count - 1; i >= 0; i--)
            {
                if(Blog[i].WriterID == id && Blog[i].StatusID == 1)
                    sBlog.Add(Create(Blog[i]));
            }
            return sBlog;
        }
    }
}