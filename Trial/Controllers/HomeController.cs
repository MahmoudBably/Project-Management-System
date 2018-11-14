using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trial.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using Vereyon.Web;

namespace Trial.Controllers
{
    public class HomeController : Controller
    {

        public ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Main()
        {

            return View();
        }

        public ActionResult OurTeam()
        {
            return View();
        }

        [MyAuthorize(Roles = "Admin, Project Manager, Team Leader")]
        public ActionResult Admin_PM_TL()
        {
            // get EF Database 
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            // find the user
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();

            return View(user);
        }

        [MyAuthorize(Roles = "Customer, Junior Developer")]
        public ActionResult Customer_JD()
        {
            // get EF Database
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            // find the user
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();

            return View(user);
        }
        [MyAuthorize]
        public ActionResult ProfilePhoto()
        {

            return PartialView("~/Views/Home/ProfilePhoto.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize]
        public ActionResult ProfilePhoto(HttpPostedFileBase ProfilePhoto)
        {
            // get EF Database
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            // find the user
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();

            // convert image stream to byte array
            try
            {
                byte[] image = new byte[ProfilePhoto.ContentLength];
                ProfilePhoto.InputStream.Read(image, 0, Convert.ToInt32(ProfilePhoto.ContentLength));

                user.ProfilePicture = image;

                // save changes to database
                db.SaveChanges();
                if (User.IsInRole("Admin") | User.IsInRole("Project Manager") | User.IsInRole("Team Leader"))
                {
                    return RedirectToAction("Admin_PM_TL", "Home");
                }
                else
                {
                    return RedirectToAction("Customer_JD", "Home");
                }
            }
            catch (Exception e)
            {
                FlashMessage.Warning("Your error message");
                //TempData["ErrorMessage"] = "This is the message";
                //ModelState.AddModelError("NullPhotoException", "You Didnt Upload A Photo");
                if (User.IsInRole("Admin") | User.IsInRole("Project Manager") | User.IsInRole("Team Leader"))
                {
                    return RedirectToAction("Admin_PM_TL", "Home");
                }
                else
                {
                    return RedirectToAction("Customer_JD", "Home");
                }
            }

        }
        [HttpGet]
        [MyAuthorize]
        public ActionResult ShowProfile()
        {
            // get EF Database
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            // find the user
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            var model = new ApplicationUser();
            model.UserName = user.UserName;
            model.Fname = user.Fname;
            model.Lname = user.Lname;
            model.Job_Description = user.Job_Description;
            model.PhoneNum = user.PhoneNum;
            model.Email = user.Email;



            return View(user);
        }
        [HttpPost]
        [MyAuthorize]
        public ActionResult EditProfile(ApplicationUser newdata)
        {
            if (ModelState.IsValid)
            {
                // get EF Database
                var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

                // find the user
                var userid = User.Identity.GetUserId();
                var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();

                // Update fields
                user.UserName = newdata.UserName;
                user.Fname = newdata.Fname;
                user.Lname = newdata.Lname;
                user.Job_Description = newdata.Job_Description;
                user.PhoneNum = newdata.PhoneNum;
                //user.Email = newdata.Email;
                if (IsValidEmail(newdata.Email))
                {
                    user.Email = newdata.Email;
                }
                else
                {
                    //FlashMessage.Warning("Your error message");

                }

                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();

                if (User.IsInRole("Admin") | User.IsInRole("Project Manager") | User.IsInRole("Team Leader"))
                {
                    return RedirectToAction("ShowProfile", "Home");
                }
                else
                {
                    return RedirectToAction("ShowProfile", "Home");
                }
            }

            return View("ShowProfile",newdata);
        }

        [HttpGet]
        [MyAuthorize(Roles = "Project Manager, Junior Developer, Team Leader")]
        public ActionResult ShowExperience()
        {

            var userid = User.Identity.GetUserId();
            var experience = context.Experience.Where(x => x.UserId == userid).FirstOrDefault();
            var model = new Experience();

            model.ASPNETMVC = experience.ASPNETMVC;
            model.JavaScript = experience.JavaScript;
            model.JQuery = experience.JQuery;
            model.HTML5 = experience.HTML5;
            model.PHP = experience.PHP;
            model.JAVA = experience.JAVA;



            return View(experience);
        }

        [HttpPost]
        [MyAuthorize(Roles = "Project Manager, Junior Developer, Team Leader")]
        public ActionResult EditExperience(Experience newdata)
        {
            if (ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                var experience = context.Experience.Where(x => x.UserId == userid).FirstOrDefault();

                // Update fields
                experience.ASPNETMVC = newdata.ASPNETMVC;
                experience.JavaScript = newdata.JavaScript;
                experience.JQuery = newdata.JQuery;
                experience.HTML5 = newdata.HTML5;
                experience.PHP = newdata.PHP;
                experience.JAVA = newdata.JAVA;


                context.Entry(experience).State = EntityState.Modified;

                context.SaveChanges();

                if (User.IsInRole("Project Manager") | User.IsInRole("Team Leader"))
                {
                    return RedirectToAction("Admin_PM_TL", "Home");
                }
                else
                {
                    return RedirectToAction("Customer_JD", "Home");
                }
            }

            return View(newdata);
        }


        [MyAuthorize(Roles = "Admin")]
        public ActionResult ManageUsers()
        {
            var Db = new ApplicationDbContext();
            var users = Db.Users;
            var model = new List<EditUserViewModel>();
            foreach (var user in users)
            {
                if (user.UserName != "Admin")
                {
                    var u = new EditUserViewModel(user);
                    model.Add(u);
                }

            }
            return View(model);
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }




    }
}