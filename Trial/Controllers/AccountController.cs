using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Trial.Models;
using Vereyon.Web;

namespace Trial.Controllers
{
    [MyAuthorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationDbContext context;
        public AccountController()
        {
            context = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

       
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
         
            return PartialView("Login");
        }

        //
        // POST: /Account/Login   
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Main.cshtml");
            }
            
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Main", "Home");
                case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View("~/Views/Home/Main.cshtml");
            }
        }




        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.IsInRole("Admin"))
            {
                return View("AdminRegister");
            }
            else {
                return View("Register");
            }
           
        }

        //
        // POST: /Account/Register   
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {

                if (User.IsInRole("Admin"))
                {
                    return View("AdminRegister", model);
                }
                else
                {
                    //FlashMessage.Warning("Invalid Register");
                    return View("~/Views/Home/MainToRegister.cshtml");
                }
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Fname = model.Fname,
                Lname = model.Lname,
                Job_Description = model.Job_Description,
                PhoneNum = model.PhoneNum
            };
          
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (User.IsInRole("Admin"))
                {
                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    return RedirectToAction("ManageUsers", "Home");
                }
                if (model.UserRoles != "Customer")
                {
                    var experience = new Experience
                    {
                        UserId = user.Id,
                        ASPNETMVC = 0,
                        JavaScript = 0,
                        JQuery = 0,
                        HTML5 = 0,
                        PHP = 0,
                        JAVA = 0
                    };
                    context.Experience.Add(experience);
                    context.SaveChanges();
                }
               
                await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                //TempData["notice"] = "Registered Successfully!";
                FlashMessage.Confirmation("Registered Successfully!");
                return View("~/Views/Home/MainToRegister.cshtml");
            }
            AddErrors(result);
            return View("~/Views/Home/MainToRegister.cshtml");

        }

       
       

        public FileResult Photo()
        {
          
            var userId = User.Identity.GetUserId();
            var user = context.Users.Where(x => x.Id == userId).FirstOrDefault();


            if (user.ProfilePicture != null)
            {
                return new FileContentResult(user.ProfilePicture, "image/jpeg");
            }
            else
            {
                return new FilePathResult("/Content/blankprofile.png", "image/jpeg");
            }
        }



       

        [MyAuthorize(Roles = "Admin")]
        public ActionResult DeleteUser(string id)
        {

            var user = context.Users.First(u => u.Id == id);
            context.Projects.RemoveRange(context.Projects.Where(x => x.CustomerId == id));
            context.ProjectHistory.RemoveRange(context.ProjectHistory.Where(x => x.CustomerId == id));
            context.Works_on.RemoveRange(context.Works_on.Where(x => x.UserId == id));
            context.Worked_on.RemoveRange(context.Worked_on.Where(x => x.UserId == id));
            context.Submits.RemoveRange(context.Submits.Where(x => x.PmId == id));
            context.Requests.RemoveRange(context.Requests.Where(x => x.FromUserId == id));
            context.Requests.RemoveRange(context.Requests.Where(x => x.ToUserId == id));
            context.Requests.RemoveRange(context.Requests.Where(x => x.RegardingUserId == id));
            context.Comments.RemoveRange(context.Comments.Where(x => x.ProjectManagerId == id));
            context.Feedbacks.RemoveRange(context.Feedbacks.Where(x => x.EvaluatedJDId == id));
            context.Feedbacks.RemoveRange(context.Feedbacks.Where(x => x.EvaluatingTLId == id));
            context.Users.Remove(user);
            context.SaveChanges();

            return RedirectToAction("ManageUsers", "Home");

        }


 

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Main", "Home");
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return PartialView("~/Views/Account/ChangePassword.cshtml");
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                if (User.IsInRole("Admin") || User.IsInRole("Project Manager") || User.IsInRole("Team Leader"))
                {
                    return RedirectToAction("Admin_PM_TL", "Home");
                }
                if (User.IsInRole("Customer") || User.IsInRole("Junior Developer"))
                {
                    return RedirectToAction("Customer_JD", "Home");
                }

            }
            AddErrors(result);
            return View("~/Views/Home/Admin_PM_TL.cshtml");
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        
        #endregion
    }

}
