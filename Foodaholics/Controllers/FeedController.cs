using Foodaholics.Models;
using Foodaholics.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodaholics.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        ShowBlogRepository sbRepo = new ShowBlogRepository();
        BlogContentRepository bcRepo = new BlogContentRepository();
        BlogRepository bRepo = new BlogRepository();
        public ActionResult Index()
        {
            return View(sbRepo.GetPosted());
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            //return Content(search);
            ViewBag.search = search;
            return View("Index", sbRepo.GetPosted(search));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            BlogDetails bd = new BlogDetails();
            Blog b = bRepo.GetById(id);
            bd.Content = bcRepo.GetContent(id);
            bd.id = b.id;
            bd.Title = b.Title;
            bd.Posted = (DateTime) b.Posted;
            bd.WriterName = bRepo.WriterName(id);
            bd.Watch = b.Watch;
            bd.CoverPicturePath = b.CoverPicturePath;
            bd.ContentProcess();
            bRepo.UpdateWatch(id);
            return View(bd);
        }

        [HttpPost]
        public ActionResult Filter()
        {
            //return Content(search);
            if (Request.Form["option"].ToString() == "last7")
            {
                return RedirectToAction("LastSeven");
            }
            else if (Request.Form["option"].ToString() == "lastM")
            {
                return RedirectToAction("LastMonth");
            }
            else if (Request.Form["option"].ToString() == "trend")
            {
                return RedirectToAction("Trending");
            }
            else if (Request.Form["option"].ToString() == "watch")
            {
                return RedirectToAction("HighestWatched");
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult LastSeven()
        {
            List<ShowBlog> sbList = sbRepo.GetPosted();
            List<ShowBlog> rSBList = new List<ShowBlog>();
            DateTime comval = DateTime.Now.AddDays(-7);
            foreach (ShowBlog sb in sbList)
            {
                if (sb.Posted >= comval)
                    rSBList.Add(sb);
            }

            return View("Index", rSBList);
        }

        public ActionResult LastMonth()
        {
            List<ShowBlog> sbList = sbRepo.GetPosted();
            List<ShowBlog> rSBList = new List<ShowBlog>();
            DateTime comval = DateTime.Now.AddDays(-30);
            foreach (ShowBlog sb in sbList)
            {
                if (sb.Posted >= comval)
                    rSBList.Add(sb);
            }

            return View("Index", rSBList);
        }

        public ActionResult Trending()
        {
            List<ShowBlog> sbList = sbRepo.GetPosted();
            List<ShowBlog> rSBList = new List<ShowBlog>();
            DateTime comval = DateTime.Now.AddDays(-7);
            foreach (ShowBlog sb in sbList)
            {
                if (sb.Posted >= comval)
                    rSBList.Add(sb);
            }
            rSBList = rSBList.OrderByDescending(o => o.watch).ToList();
            return View("Index", rSBList);
        }

        public ActionResult HighestWatched()
        {
            List<ShowBlog> sbList = sbRepo.GetPosted();
            sbList = sbList.OrderByDescending(o => o.watch).ToList();
            return View("Index", sbList);
        }
    }
}