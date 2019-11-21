using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foodaholics.Repository;
using Foodaholics.Models;

namespace Foodaholics.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ShowBlogRepository sbRepo = new ShowBlogRepository();
        UserRepository uRepo = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HighestWatched(int top)
        {

            List<ShowBlog> sbList = sbRepo.GetPosted();
            sbList = sbList.OrderByDescending(o => o.watch).ToList();
            //int count = sbList.Count;
            if (top >= sbList.Count)
                return View(sbList);
            else
            {
                List<ShowBlog> nList = new List<ShowBlog>();
                for (int i = 0; i < top; i++)
                    nList.Add(sbList[i]);
                return View(nList);
            }


        }

        [HttpPost]
        public ActionResult HighestWriter(int top)
        {
            List<User> uList = uRepo.GetAll();
            List<ShowBlog> bList = new List<ShowBlog>();
            List<Writer> wList = new List<Writer>();
            foreach (User u in uList)
            {
                int temp = 0;
                bList = sbRepo.GetPosted(u.id);
                foreach (ShowBlog sb in bList)
                    temp += sb.watch;
                Writer w = new Writer();
                w.id = u.id;
                w.Name = u.Name;
                w.Watch = temp;
                w.ProfilePicturePath = u.ProfilePicturePath;
                wList.Add(w);
            }
            wList = wList.OrderByDescending(o => o.Watch).ToList();

            if (top >= wList.Count)
                return View(wList);
            else
            {
                List<Writer> nList = new List<Writer>();
                for (int i = 0; i < top; i++)
                    nList.Add(wList[i]);
                return View(nList);
            }
        }
    }
}