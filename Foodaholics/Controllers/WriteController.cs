using Foodaholics.Models;
using Foodaholics.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodaholics.Controllers
{
    public class WriteController : Controller
    {
        // GET: Write
        ShowBlogRepository sbRepo = new ShowBlogRepository();
        BlogRepository bRepo = new BlogRepository();
        BlogContentRepository bCRepo = new BlogContentRepository();
        public ActionResult Index()
        {
            return View(sbRepo.GetDrafted((int)Session["UserId"]));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Blog b = bRepo.GetById(id);
            //return Content(b.id.ToString());
            bCRepo.RemoveContent(id);
            bRepo.Delete(b);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MyBlogs()
        {
            return View(sbRepo.GetPosted((int)Session["UserId"]));
        }

        [HttpGet]
        public ActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBlog(string Title, HttpPostedFileBase CoverPicture)
        {
            Blog b = new Blog();
            b.Title = Title;

            string CoverPicturePath = Path.GetFileNameWithoutExtension(CoverPicture.FileName);
            string extnsn = Path.GetExtension(CoverPicture.FileName);
            CoverPicturePath = CoverPicturePath + DateTime.Now.ToString("yymmddfff") + extnsn;
            b.CoverPicturePath = "/Images/Blog/" + CoverPicturePath;
            CoverPicturePath = Path.Combine(Server.MapPath("~/Images/Blog/"), CoverPicturePath);
            CoverPicture.SaveAs(CoverPicturePath);

            b.WriterID = (int)Session["UserId"];
            b.StatusID = 1;
            b.Posted = DateTime.Now;
            b.Created = DateTime.Now;
            b.LastEdited = DateTime.Now;
            bRepo.Insert(b);
            return RedirectToAction("WriteBlog", new { id=b.id});
        }

        [HttpGet]
        public ActionResult WriteBlog(int id)
        {

            WriteBlog wb = new WriteBlog();
            wb.BlogId = id;
            Blog b = bRepo.GetById(id);
            wb.Title = b.Title;
            wb.Content = bCRepo.GetContent(id);
            Session["CurrentBlogId"] = id;
            return View(wb);
        }

        [NonAction]
        private WriteBlog CreateWriteBlog()
        {
            WriteBlog wb = new WriteBlog();
            Blog b = bRepo.GetById((int)Session["CurrentBlogId"]);
            wb.BlogId = (int)Session["CurrentBlogId"];
            wb.Title = b.Title;
            return wb;
        }

        [HttpPost]
        public ActionResult WriteAction(string content)
        {
            if (Request.Form["Bold"] != null)
            {
                return RedirectToAction("Bold", new { content = content });
            }
            else if (Request.Form["Link"] != null)
            {
                return RedirectToAction("Link", new { content = content });
            }
            else if (Request.Form["Italic"] != null)
            {
                return RedirectToAction("Italic", new { content = content });
            }
            else if (Request.Form["AddPicture"] != null)
            {
                return RedirectToAction("AddPicture", new { content = content });
            }
            else if (Request.Form["AddPara"] != null)
            {
                return RedirectToAction("AddPara", new { content = content });
            }
            else if (Request.Form["Clear"] != null)
            {
                return RedirectToAction("Clear");
            }
            else if (Request.Form["Draft"] != null)
            {
                return RedirectToAction("Draft", new { content = content });
            }
            else if (Request.Form["Post"] != null)
            {
                return RedirectToAction("Post", new { content = content });
            }
            else if (Request.Form["Preview"] != null)
            {
                return RedirectToAction("Preview", new { content = content });
            }

            return RedirectToAction("Index");
            
        }

        

        public ActionResult Bold(string content)
        {
            //return Content("Inside bold");
            WriteBlog wb = CreateWriteBlog();
            wb.Content += content + " *BOLD* Write here *BOLD* ";
            return View("WriteBlog", wb);
        }

        public ActionResult Link(string content)
        {
            WriteBlog wb = CreateWriteBlog();
            wb.Content += content + " *L* write here *A* write address here *A**L*";
            return View("WriteBlog", wb);
        }

        public ActionResult Italic(string content)
        {
            WriteBlog wb = CreateWriteBlog();
            wb.Content += content + " *I* write here *I*";
            return View("WriteBlog", wb);
        }
        
        public ActionResult AddPicture(string content)
        {
            Session["BlogContent"] = content;
            if (Session["BlogContent"] == null)
                Session["BlogContent"] = "";
            return View();
        }

        public ActionResult AddPara(string content)
        {
            WriteBlog wb = CreateWriteBlog();
            wb.Content += content + " *P* new para start here ";
            return View("WriteBlog", wb);
        }

        public ActionResult Clear()
        {
            bCRepo.RemoveContent((int)Session["CurrentBlogId"]);
            WriteBlog wb = CreateWriteBlog();
            wb.Content = "";
            return View("WriteBlog", wb);
        }

        public ActionResult Preview(string content)
        {
            WriteBlog wb = CreateWriteBlog();
            if (content == null || content == "")
                wb.Content = "  ";
            else
                wb.Content = content;
            
            bCRepo.SaveDraft(wb, (int)Session["CurrentBlogId"]);

            BlogDetails bD = new BlogDetails();
            Blog b = bRepo.GetById((int)Session["CurrentBlogId"]);
            bD.id = b.id;
            bD.Title = b.Title;
            bD.CoverPicturePath = b.CoverPicturePath;
            bD.Content = content;
            bD.ContentProcess();

            return View(bD);
        }

        public ActionResult Draft(string content)
        {
            WriteBlog wb = CreateWriteBlog();
            wb.Content = content;
            bCRepo.SaveDraft(wb, (int)Session["CurrentBlogId"]);
            return RedirectToAction("Index");
        }

        public ActionResult Post(string content)
        {
            WriteBlog wb = CreateWriteBlog();
            wb.Content = content;
            bCRepo.SavePost(wb, (int)Session["CurrentBlogId"]);
            return RedirectToAction("Index");
        }

        public ActionResult UploadPicture(HttpPostedFileBase Picture)
        {

            if (Picture != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(Picture.FileName);
                string extnsn = Path.GetExtension(Picture.FileName);
                fileName = fileName + Session["CurrentBlogId"].ToString() + DateTime.Now.ToString("yymmddfff") + extnsn;
                WriteBlog wb = CreateWriteBlog();
                wb.Content = Session["BlogContent"].ToString() + "*image*[Do not edit]" + fileName+ "*image*";
                fileName = Path.Combine(Server.MapPath("~/Images/Blog/"), fileName);
                Picture.SaveAs(fileName);

                return View("WriteBlog", wb);
            }

            return RedirectToAction("CancelUploadPicture");
        }

        public ActionResult CancelUploadPicture()
        {
            WriteBlog wb = CreateWriteBlog();
            wb.Content = Session["BlogContent"].ToString();
            return View("WriteBlog", wb);
        }
    }
}