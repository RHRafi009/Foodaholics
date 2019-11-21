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
    public class ProfileController : Controller
    {
        // GET: Profile
        UserRepository uRepo = new UserRepository(); 

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProfileDetails()
        {
            return View(uRepo.GetById((int)Session["UserId"]));
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(new CreateUser());
        }

        [HttpPost]
        public ActionResult SignUp(CreateUser cu)
        {
            if(ModelState.IsValid && cu.Password == cu.ConPassword)
            {
                User u = new User();
                u.Name = cu.Name;
                u.Email = cu.Email;
                u.Password = cu.Password;
                u.RoleId = 1;
                u.ProfilePicturePath = "~/Images/Profile/default.jpg";
                uRepo.Insert(u);
                Session["LoggedIn"] = "true";
                Session["UserId"] = u.id;
                return View("UploadProfilePicture");
            }
            return View("SignUp", cu);
        }

        [HttpGet]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(UpdatePassword up)
        {
            if(ModelState.IsValid)
            {
                User u = uRepo.GetById((int)Session["UserId"]);
                if(u.Password == up.CurrentPassword && up.Password == up.ConPassword)
                {
                    uRepo.PasswordUpdate(u.id, up.Password);
                    return RedirectToAction("ProfileDetails");
                }
            }
            return View("UpdatePassword", up);
        }

        [HttpGet]
        public ActionResult UploadProfilePicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadProfilePicture(HttpPostedFileBase picture)
        {
            //return Content(picture.FileName);

            if (picture != null)
            {
                string filename = Path.GetFileNameWithoutExtension(picture.FileName);
                string extnss = Path.GetExtension(picture.FileName);
                filename = filename + DateTime.Now.ToString("yymmddfff") + extnss;
                uRepo.ProfilePictureUpdate((int)Session["UserId"], "~/Images/Profile/" + filename);
                filename = Path.Combine(Server.MapPath("~/Images/Profile/"),filename);
                picture.SaveAs(filename);
            }
            return RedirectToAction("ProfileDetails");
        }


        [HttpGet]
        public ActionResult SignIn()
        {
            Session["EmailnPass"] = "true";
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInUser su)
        {
            if(ModelState.IsValid)
            {
                User u = uRepo.getByEmailnPass(su.Email, su.Password);
                if(u == null)
                {
                    Session["EmailnPass"] = "false";
                    return View("SignIn", su);
                }
                Session["EmailnPass"] = "true";
                Session["LoggedIn"] = "true";
                Session["UserId"] = u.id;
                Session["RollId"] = u.RoleId;
                return RedirectToAction("Index", "Feed");
            }
            return View("SignIn", su);
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            Session["LoggedIn"] = "false";
            Session["UserId"] = -1;
            return RedirectToAction("Index", "Feed");
        }
    }
}