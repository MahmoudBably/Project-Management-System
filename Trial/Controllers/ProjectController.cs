using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trial.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using Newtonsoft.Json;

namespace Trial.Controllers
{
    public class ProjectController : Controller
    {
        public ApplicationDbContext context;
        public ProjectController()
        {
            context = new ApplicationDbContext();
        }

        [MyAuthorize(Roles = "Admin")]
        public ActionResult ManageProjects()
        {
            var projects = from p in context.Projects
                           join user in context.Users on p.CustomerId equals user.Id
                           select new ProjectViewModel
                           {
                               Project = p,
                               User = user
                           };


            return View(projects);
        }

        [MyAuthorize]
        public ActionResult UnassignedProjects()
        {
            var projects = from p in context.Projects
                           join user in context.Users on p.CustomerId equals user.Id
                           where p.Assigned == false
                           select new ProjectViewModel
                           {
                               Project = p,
                               User = user
                           };


            return View(projects);
        }

        [MyAuthorize(Roles ="Admin")]
        public ActionResult AllassignedProjects()
        {
            try {
                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("8a831707-be0c-4595-82fa-9884f635e784"))).Select(u => u.Id).ToArray();

                var projects = from p in context.Projects
                               join work in context.Works_on on p.Id equals work.ProjectId
                               join pm in context.Users on work.UserId equals pm.Id
                               where p.Assigned == true && usersinRole.Contains(work.UserId)
                               select new AssignedProjectViewModel
                               {
                                   Project = p,
                                   User = pm,
                                   Works_on = work
                               };

                return PartialView(projects);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }
        }

        [MyAuthorize(Roles = "Admin")]
        public ActionResult AllDeliveredProjects()
        {
            try {
                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("8a831707-be0c-4595-82fa-9884f635e784"))).Select(u => u.Id).ToArray();

                var projects = from p in context.ProjectHistory
                               join work in context.Worked_on on p.Id equals work.ProjectId
                               join pm in context.Users on work.UserId equals pm.Id
                               where usersinRole.Contains(work.UserId)
                               select new DeliveredProjectViewModel
                               {
                                   ProjectHistory = p,
                                   Worked_on = work,
                                   User = pm

                               };


                return PartialView(projects);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }
        }

        [MyAuthorize(Roles = "Admin, Customer")]
        public ActionResult DeleteProject(int id)
        {
            try {
                var project = context.Projects.First(u => u.Id == id);
                var model = new Project(project);
                context.Projects.Remove(project);
                context.SaveChanges();
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("ManageProjects", "Project");
                }
                else
                {
                    return RedirectToAction("CustomerUnassignedProjects", "Project");
                }
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }

        }



        [MyAuthorize(Roles = "Admin, Customer, Project Manager")]
        public ActionResult EditProject(int id)
        {
            try
            {


                var project = context.Projects.First(u => u.Id == id);
                var model = new Project(project);


                model.Name = project.Name;
                //model.CustomerId = project.CustomerId;
                model.Price = project.Price;
                model.Schedule_To = project.Schedule_To;
                model.Shcedule_From = project.Shcedule_From;
                //model.Status = project.Status;
                //model.Assigned = project.Assigned;
                model.Description = project.Description;

                //var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("111deef9-75a4-4a8e-8599-edcfbb0e10a7")));

                //ViewBag.Names = new SelectList(usersinRole, "Id", "UserName");

                if (project == null)
                {
                    return HttpNotFound();
                }
                return PartialView(model);
                //if (User.IsInRole("Project Manager"))
                //{
                //    return PartialView("PMEditProject", model);
                //}
                //else
                //{
                //    return PartialView("EditProject", model);
                //}
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }
        }


        [HttpPost, ActionName("EditProject")]
        [MyAuthorize(Roles = "Admin, Customer, Project Manager")]
        public ActionResult EditProjectConfirm(int id,Project newdata)
        {
            try
            {


                var project = context.Projects.First(u => u.Id == id);
                var projecthistory = context.ProjectHistory.First(u => u.Id == id);

                if (User.IsInRole("Project Manager"))
                {
                    project.Price = newdata.Price;
                    project.Shcedule_From = newdata.Shcedule_From;
                    project.Schedule_To = newdata.Schedule_To;
                    projecthistory.Price = newdata.Price;
                    projecthistory.Schedule_To = newdata.Schedule_To;
                }
                else {
                    project.Name = newdata.Name;
                    project.Description = newdata.Description;
                    projecthistory.Name = newdata.Name;
                    projecthistory.Description = newdata.Description;
                }

                //project.CustomerId = newdata.CustomerId;

                //project.Shcedule_From = newdata.Shcedule_From;
                //project.Status = newdata.Status;
                //project.Assigned = newdata.Assigned;



                //projecthistory.CustomerId = newdata.CustomerId;

                //projecthistory.Shcedule_From = newdata.Shcedule_From;
                //projecthistory.Status = newdata.Status;
                //projecthistory.Assigned = newdata.Assigned;


                //var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("111deef9-75a4-4a8e-8599-edcfbb0e10a7")));

                //ViewBag.Names = new SelectList(usersinRole, "Id", "UserName");

                context.Entry(project).State = EntityState.Modified;
                context.Entry(projecthistory).State = EntityState.Modified;

                context.SaveChanges();

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("ManageProjects", "Project");
                }
                if (User.IsInRole("Project Manager"))
                {
                    return RedirectToAction("Admin_PM_TL", "Home");
                }

                return RedirectToAction("CustomerUnassignedProjects", "Project");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }

        }

        // GET: Project

        [MyAuthorize(Roles ="Customer")]
        public ActionResult CustomerProjectForm()
        {
            try {
                return View("CustomerProjectForm");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }

        }

        [HttpPost]
        [MyAuthorize(Roles = "Customer")]
        public ActionResult CustomerProjectForm(Project project)
        {
          
                var userId = User.Identity.GetUserId();

                Project p = new Project
                {
                    Name = project.Name,
                    CustomerId = userId,
                    Description = project.Description
                };
                ProjectHistory p_history = new ProjectHistory
                {
                    Name = project.Name,
                    CustomerId = userId,
                    Description = project.Description
                };
                context.Projects.Add(p);
                context.ProjectHistory.Add(p_history);
                context.SaveChanges();
                return RedirectToAction("Main", "Home");
           



        }
        [MyAuthorize(Roles = "Admin")]
        public ActionResult AdminProjectForm()
        {
            try {
                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("111deef9-75a4-4a8e-8599-edcfbb0e10a7")));

                ViewBag.Names = new SelectList(usersinRole, "Id", "UserName");
                return View("AdminProjectForm");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }

        }

        [HttpPost]
        [MyAuthorize(Roles = "Admin")]
        public ActionResult AdminProjectForm(Project project)
        {

            try {

                Project p = new Project
                {
                    Name = project.Name,
                    CustomerId = project.CustomerId,
                    Description = project.Description,
                };

                ProjectHistory p_history = new ProjectHistory
                {
                    Name = project.Name,
                    CustomerId = project.CustomerId,
                    Description = project.Description,
                };

                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("111deef9-75a4-4a8e-8599-edcfbb0e10a7")));

                ViewBag.Names = new SelectList(usersinRole, "Id", "UserName");

                context.Projects.Add(p);
                context.ProjectHistory.Add(p_history);
                context.SaveChanges();
                return RedirectToAction("ManageProjects", "Project");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }



        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult SubmitProject(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Submit submit = new Submit
                {
                    PmId = userId,
                    ProjectId = id
                };
                context.Submits.Add(submit);
                context.SaveChanges();
                return RedirectToAction("Main", "Home");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }


        }

        [HttpGet]
        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult Comment(int id )
        {
            try {
                var model = new Comment
                {
                    ProjectId = id
                };

                return PartialView("Comment", model);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }
        }

        [HttpPost]
        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult Comment(int id,Comment Comment)
        {
            try {

                var userId = User.Identity.GetUserId();
                Comment Com = new Comment
                {
                    ProjectManagerId = userId,
                    ProjectId = id,
                    CommentDesc = Comment.CommentDesc
                };
                context.Comments.Add(Com);
                context.SaveChanges();
                return RedirectToAction("Main", "Home");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }


        }


       

        [MyAuthorize(Roles = "Customer")]
        public ActionResult CustomerUnassignedProjects()
        {
            try {
                var userId = User.Identity.GetUserId();
                var projects = context.Projects.Where(u => u.CustomerId == userId && u.Assigned == false);


                return View(projects);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Customer_JD", "Home");
            }
        }


        [HttpGet]
        [MyAuthorize(Roles = "Customer")]
        public ActionResult ShowAssignPM(int id)
        {
            try {
                var submittedPMS = context.Users.Where(u => u.Submits.Any(r => r.ProjectId.Equals(id)));
                ViewBag.Names = new SelectList(submittedPMS, "Id", "UserName");
                return PartialView("ShowAssignPM");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Customer_JD", "Home");
            }

        }

        [HttpPost, ActionName("ShowAssignPM")]
        [MyAuthorize(Roles = "Customer")]
        public ActionResult AssignPM(int id,Works_on New)
        {
            try {
                var project = context.Projects.First(u => u.Id == id);
                project.Assigned = true;
                var projecthistory = context.ProjectHistory.First(u => u.Id == id);
                projecthistory.Assigned = true;
                var user_Id = User.Identity.GetUserId();
                var submittedPMS = context.Users.Where(u => u.Submits.Any(r => r.ProjectId.Equals(id)));

                ViewBag.Names = new SelectList(submittedPMS, "Id", "UserName");

                Works_on newPM = new Works_on
                {
                    ProjectId = id,
                    UserId = New.UserId
                };
                context.Works_on.Add(newPM);
                context.Entry(project).State = EntityState.Modified;
                context.Entry(projecthistory).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("CustomerUnAssignedProjects", "Project");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Customer_JD", "Home");
            }



        }

        [MyAuthorize(Roles ="Team Leader, Project Manager, Junior Developer")]
        public ActionResult UserAssignedProjects()
        {
            try {
                var user_Id = User.Identity.GetUserId();
                var projects = from p in context.Projects
                               join work in context.Works_on on p.Id equals work.ProjectId
                               where work.UserId == user_Id
                               select new ProjectWorksOnViewModel
                               {
                                   Project = p,
                                   Works_on = work

                               };


                return PartialView(projects);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }
        }
        


        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult PMManageProject(int id)
        {
            try {
                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("d8bff653-2a05-4159-948d-6eabd17de838") || r.RoleId.Equals("3b12d84f-0ecc-4e04-8e9c-ff3d441e2ce0"))).Select(u => u.Id).ToList();

                var teamquery = from p in context.Projects
                                join work in context.Works_on on p.Id equals work.ProjectId
                                join member in context.Users on work.UserId equals member.Id
                                where p.Id == id && usersinRole.Contains(work.UserId)
                                select new AssignedProjectViewModel
                                {
                                    Project = p,
                                    Works_on = work,
                                    User = member
                                };

                var team = teamquery.ToList();

                return PartialView("PMManageProject", team);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }

        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult PMManageDelivered(int id)
        {
            
                var roleIds = context.Roles.Where(m => m.Name == "Team Leader").Select(m => m.Id);

                var usersquery = context.Users.First(u =>
                u.Roles.Any(r => roleIds.Contains(r.RoleId)) &&
                u.Worked_on.Any(w => w.ProjectId == id));

                ViewBag.tlid = usersquery.Id;

                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("d8bff653-2a05-4159-948d-6eabd17de838") || r.RoleId.Equals("3b12d84f-0ecc-4e04-8e9c-ff3d441e2ce0"))).Select(u => u.Id).ToList();

                var teamquery = from p in context.ProjectHistory
                                join work in context.Worked_on on p.Id equals work.ProjectId
                                join member in context.Users on work.UserId equals member.Id
                                where p.Id == id && usersinRole.Contains(work.UserId)
                                select new DeliveredProjectViewModel
                                {
                                    ProjectHistory = p,
                                    Worked_on = work,
                                    User = member
                                };

                var team = teamquery.ToList();

                return PartialView("PMManageDelivered", team);
     
        }

        [MyAuthorize(Roles = "Team Leader, Junior Developer")]
        public ActionResult TL_JD_ManageProject(int id)
        {
            try {
                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("8a831707-be0c-4595-82fa-9884f635e784") || r.RoleId.Equals("3b12d84f-0ecc-4e04-8e9c-ff3d441e2ce0"))).Select(u => u.Id).ToList();

                var roleIds = context.Roles.Where(m => m.Name == "Project Manager").Select(m => m.Id);

                var usersquery = context.Users.First(u =>
                u.Roles.Any(r => roleIds.Contains(r.RoleId)) &&
                u.Works_on.Any(w => w.ProjectId == id));

                ViewBag.pmid = usersquery.Id;

                var teamquery = from p in context.Projects
                                join work in context.Works_on on p.Id equals work.ProjectId
                                join member in context.Users on work.UserId equals member.Id
                                where p.Id == id && usersinRole.Contains(work.UserId)
                                select new AssignedProjectViewModel
                                {
                                    Project = p,
                                    Works_on = work,
                                    User = member
                                };

                var team = teamquery.ToList();

                return PartialView("TL_JD_ManageProject", team);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }


        }
        [MyAuthorize(Roles = "Team Leader, Junior Developer")]
        public ActionResult TL_JD_ManageDelivered(int id)
        {
            try {
                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("8a831707-be0c-4595-82fa-9884f635e784") || r.RoleId.Equals("3b12d84f-0ecc-4e04-8e9c-ff3d441e2ce0"))).Select(u => u.Id).ToList();

                var roleIds = context.Roles.Where(m => m.Name == "Project Manager").Select(m => m.Id);

                var usersquery = context.Users.First(u =>
                u.Roles.Any(r => roleIds.Contains(r.RoleId)) &&
                u.Worked_on.Any(w => w.ProjectId == id));

                ViewBag.pmid = usersquery.Id;

                var teamquery = from p in context.ProjectHistory
                                join work in context.Worked_on on p.Id equals work.ProjectId
                                join member in context.Users on work.UserId equals member.Id
                                where p.Id == id && usersinRole.Contains(work.UserId)
                                select new DeliveredProjectViewModel
                                {
                                    ProjectHistory = p,
                                    Worked_on = work,
                                    User = member
                                };

                var team = teamquery.ToList();

                return PartialView("TL_JD_ManageDelivered", team);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }

        }




        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult PMLeaveProject(int id)
        {
            try {
                var user_Id = User.Identity.GetUserId();
                var project = context.Projects.First(u => u.Id == id);
                var projecthistory = context.ProjectHistory.First(u => u.Id == id);

                var submit = context.Submits.First(u => u.ProjectId == id && u.PmId == user_Id);
                project.Assigned = false;
                projecthistory.Assigned = false;
                var works_on = context.Works_on.First(u => u.ProjectId == id && u.UserId == user_Id);
                context.Submits.Remove(submit);
                context.Works_on.Remove(works_on);
                context.Entry(project).State = EntityState.Modified;
                context.Entry(projecthistory).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Admin_PM_TL", "Home");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }

        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult RemoveMember(int id,string memberid)
        {
            try {
                var works_on = context.Works_on.First(u => u.ProjectId == id && u.UserId == memberid);
                context.Works_on.Remove(works_on);
                context.SaveChanges();
                return RedirectToAction("Admin_PM_TL", "Home");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }


        }

        [MyAuthorize]
        public ActionResult ShowComments(int id)
        {
            try {
                var comments = from c in context.Comments
                               join user in context.Users on c.ProjectManagerId equals user.Id
                               where c.ProjectId == id
                               select new CommentViewModel
                               {
                                   Comment = c,
                                   User = user
                               };


                return PartialView("ShowComments", comments);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }
        }

        [MyAuthorize(Roles = "Project Manager, Team Leader, Junior Developer")]
        public ActionResult DeliveredProjects()
        {
            try {
                var user_Id = User.Identity.GetUserId();
                var projects = from p in context.ProjectHistory
                               join work in context.Worked_on on p.Id equals work.ProjectId
                               where work.UserId == user_Id
                               select new ProjectWorkedOnViewModel
                               {
                                   ProjectHistory = p,
                                   Worked_on = work

                               };


                return PartialView(projects);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }
        }

        [MyAuthorize(Roles = "Customer")]
        public ActionResult CustomerAssignedProjects()
        {
            try {
                var user_Id = User.Identity.GetUserId();
                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("8a831707-be0c-4595-82fa-9884f635e784"))).Select(u => u.Id).ToArray();

                var projects = from p in context.Projects
                               join work in context.Works_on on p.Id equals work.ProjectId
                               join pm in context.Users on work.UserId equals pm.Id
                               where p.CustomerId == user_Id && usersinRole.Contains(work.UserId) && p.Assigned == true
                               select new AssignedProjectViewModel
                               {
                                   Project = p,
                                   Works_on = work,
                                   User = pm
                               };


                return View(projects);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Customer_JD", "Home");
            }
        }

        [MyAuthorize(Roles = "Customer")]
        public ActionResult CustomerDeliveredProjects()
        {
            try {
                var user_Id = User.Identity.GetUserId();
                var usersinRole = context.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals("8a831707-be0c-4595-82fa-9884f635e784"))).Select(u => u.Id).ToArray();

                var projects = from p in context.ProjectHistory
                               join work in context.Worked_on on p.Id equals work.ProjectId
                               join pm in context.Users on work.UserId equals pm.Id
                               where p.CustomerId == user_Id && usersinRole.Contains(work.UserId)
                               select new DeliveredProjectViewModel
                               {
                                   ProjectHistory = p,
                                   Worked_on = work,
                                   User = pm

                               };


                return PartialView(projects);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Customer_JD", "Home");
            }
        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult Deliver(int id)
        {
            try {
                var project = context.Projects.First(u => u.Id == id);
                project.Status = true;
                var projecthistory = context.ProjectHistory.First(u => u.Id == id);
                projecthistory.Status = true;
                context.Entry(project).State = EntityState.Modified;
                context.Entry(projecthistory).State = EntityState.Modified;

                var usersinproject_id = context.Users.Where(u => u.Works_on.Any(r => r.ProjectId.Equals(id))).Select(u => u.Id).ToArray();

                foreach (string user in usersinproject_id)
                {
                    Worked_on doneforusers = new Worked_on
                    {
                        ProjectId = id,
                        UserId = user
                    };
                    var users_works_on = context.Works_on.First(u => u.UserId == user & u.ProjectId == id);
                    context.Works_on.Remove(users_works_on);
                    context.Worked_on.Add(doneforusers);

                }

                context.SaveChanges();
                return RedirectToAction("Admin_PM_TL", "Home");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }



        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult SearchTL(int id)
        {
            try {
                ViewBag.projectid = id;
                var roleIds = context.Roles.Where(m => m.Name == "Team Leader").Select(m => m.Id);

                var TlsQuery = context.Users.Any(u =>
                u.Roles.Any(r => roleIds.Contains(r.RoleId)) &&
                u.Works_on.Any(w => w.ProjectId == id));

                ViewBag.TLId = TlsQuery;

                var usersquery = context.Users.Where(u =>
                 u.Roles.Any(r => roleIds.Contains(r.RoleId)) &&
                 !u.Works_on.Any(w => w.ProjectId == id));


                var users = usersquery.ToList();
                return PartialView("SearchTL", users);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }
        }

        [MyAuthorize(Roles = "Project Manager, Team Leader")]
        public ActionResult SearchJD(int id)
        {
            try {
                ViewBag.projectid = id;
                var roleIds = context.Roles.Where(m => m.Name == "Junior developer").Select(m => m.Id);

                var usersquery = context.Users.Where(u =>
                 u.Roles.Any(r => roleIds.Contains(r.RoleId)) &&
                 !u.Works_on.Any(w => w.ProjectId == id));


                var users = usersquery.ToList();
                return PartialView("SearchJD", users);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }
        }


        [MyAuthorize(Roles = "Project Manager, Team Leader")]
        public ActionResult RequestToJoin(int projectid, string userid)
        {
            try {
                var user_Id = User.Identity.GetUserId();
                Request request = new Request
                {
                    ProjectId = projectid,
                    FromUserId = user_Id,
                    ToUserId = userid,
                    Type = "Join"

                };
                context.Requests.Add(request);
                context.SaveChanges();
                return RedirectToAction("Admin_PM_TL", "Home");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }

        }

        [MyAuthorize(Roles = "Team Leader, Junior Developer")]
        public ActionResult SeeProjectRequests()
        {
            try {
                var user_Id = User.Identity.GetUserId();

                var requests = from r in context.Requests
                               join u in context.Users on r.FromUserId equals u.Id
                               join p in context.Projects on r.ProjectId equals p.Id
                               where r.ToUserId == user_Id && r.Type == "Join"
                               select new RequestViewModel
                               {
                                   Request = r,
                                   User = u,
                                   Project = p
                               };

                return View(requests);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Main", "Home");
            }
        }

        [MyAuthorize(Roles = "Team Leader, Junior Developer")]
        public ActionResult AcceptProject(int id, string senderId)
        {
            try {
                var userId = User.Identity.GetUserId();
                Works_on works_on = new Works_on
                {
                    ProjectId = id,
                    UserId = userId
                };
                context.Works_on.Add(works_on);
                var requests = context.Requests.First(u => u.ProjectId == id && u.ToUserId == userId && u.FromUserId == senderId && u.Type == "Join");
                context.Requests.Remove(requests);
                context.SaveChanges();
                return RedirectToAction("SeeProjectRequests", "Project");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("SeeProjectRequests", "Project");
            }

        }

        [MyAuthorize(Roles = "Team Leader, Junior Developer")]
        public ActionResult RejectProject(int id,string senderId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var requests = context.Requests.First(u => u.ProjectId == id && u.ToUserId == userId && u.FromUserId == senderId && u.Type == "Join");
                context.Requests.Remove(requests);
                context.SaveChanges();
                return RedirectToAction("SeeProjectRequests", "Project");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("SeeProjectsRequests", "Project");
            }


        }


        [MyAuthorize(Roles = "Team Leader, Junior Developer")]
        public ActionResult RequestToLeave(int id)
        {
            try {
                var roleIds = context.Roles.Where(m => m.Name == "Project Manager").Select(m => m.Id);

                var usersquery = context.Users.First(u =>
                u.Roles.Any(r => roleIds.Contains(r.RoleId)) &&
                u.Works_on.Any(w => w.ProjectId == id));

                var pmid = usersquery.Id;

                var user_Id = User.Identity.GetUserId();

                Request request = new Request
                {
                    ProjectId = id,
                    FromUserId = user_Id,
                    ToUserId = pmid,
                    Type = "Leave"

                };
                context.Requests.Add(request);
                context.SaveChanges();
                if (User.IsInRole("Team Leader"))
                {
                    return RedirectToAction("Admin_PM_TL", "Home");
                }

                else
                {
                    return RedirectToAction("Customer_JD", "Home");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                if (User.IsInRole("Team Leader"))
                {
                    return RedirectToAction("Admin_PM_TL", "Home");
                }

                else
                {
                    return RedirectToAction("Customer_JD", "Home");
                }
            }



        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult SeeLeaveRequests()
        {
            try
            {
                var user_Id = User.Identity.GetUserId();

                var requests = from r in context.Requests
                               join u in context.Users on r.FromUserId equals u.Id
                               join p in context.Projects on r.ProjectId equals p.Id
                               where r.ToUserId == user_Id && r.Type == "Leave"
                               select new RequestViewModel
                               {
                                   Request = r,
                                   User = u,
                                   Project = p
                               };

                return View(requests);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }
        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult AcceptLeave(int id,string memberId)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var works_on = context.Works_on.First(u => u.ProjectId == id && u.UserId == memberId);
                context.Works_on.Remove(works_on);

                var requests = context.Requests.First(u => u.ProjectId == id && u.ToUserId == userId && u.FromUserId == memberId && u.Type == "Leave");
                context.Requests.Remove(requests);
                context.SaveChanges();
                return RedirectToAction("SeeLeaveRequests", "Project");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }

        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult RejectLeave(int id, string memberId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var requests = context.Requests.First(u => u.ProjectId == id && u.ToUserId == userId && u.FromUserId == memberId && u.Type == "Leave");
                context.Requests.Remove(requests);
                context.SaveChanges();
                return RedirectToAction("SeeLeaveRequests", "Project");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }


        }

        [MyAuthorize(Roles = "Team Leader")]
        public ActionResult RequestRemoveMember(int id,string memberId)
        {
            try
            {
                var roleIds = context.Roles.Where(m => m.Name == "Project Manager").Select(m => m.Id);

                var usersquery = context.Users.First(u =>
                u.Roles.Any(r => roleIds.Contains(r.RoleId)) &&
                u.Works_on.Any(w => w.ProjectId == id));

                var pmid = usersquery.Id;

                var user_Id = User.Identity.GetUserId();

                Request request = new Request
                {
                    ProjectId = id,
                    FromUserId = user_Id,
                    ToUserId = pmid,
                    RegardingUserId = memberId,
                    Type = "Remove"

                };
                context.Requests.Add(request);
                context.SaveChanges();
                return RedirectToAction("Admin_PM_TL", "Home");
            }
            

             catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }

        }


        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult SeeRemoveRequests()
        {

            try {
                var user_Id = User.Identity.GetUserId();

                var requests = from r in context.Requests
                               join u in context.Users on r.FromUserId equals u.Id
                               join reg in context.Users on r.RegardingUserId equals reg.Id
                               join p in context.Projects on r.ProjectId equals p.Id
                               where r.ToUserId == user_Id && r.Type == "Remove"
                               select new RequestRemoveViewModel
                               {
                                   Request = r,
                                   User = u,
                                   RegardingUser = reg,
                                   Project = p
                               };

                return View(requests);
            }
            

             catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }
        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult AcceptRemove(int id, string fromId ,string memberId )
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var works_on = context.Works_on.First(u => u.ProjectId == id && u.UserId == memberId);
                context.Works_on.Remove(works_on);

                var requests = context.Requests.First(u => u.ProjectId == id && u.ToUserId == userId && u.FromUserId == fromId && u.RegardingUserId == memberId && u.Type == "Remove");
                context.Requests.Remove(requests);
                context.SaveChanges();
                return RedirectToAction("SeeRemoveRequests", "Project");
            }
           
              catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }

        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult RejectRemove(int id, string fromId, string memberId)
        {
            try {
                var userId = User.Identity.GetUserId();

                var requests = context.Requests.First(u => u.ProjectId == id && u.ToUserId == userId && u.FromUserId == fromId && u.RegardingUserId == memberId && u.Type == "Remove");
                context.Requests.Remove(requests);
                context.SaveChanges();
                return RedirectToAction("SeeRemoveRequests", "Project");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }


        }

        [HttpGet]
        [MyAuthorize(Roles = "Team Leader")]
        public ActionResult EvaluateUser(int id, string memberid)
        {
            try
            {
                ViewBag.projectid = id;
                ViewBag.userid = memberid;
                return PartialView();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }
        }


        [HttpPost, ActionName("EvaluateUser")]
        [MyAuthorize(Roles = "Team Leader")]
        public ActionResult SendEvaluation(int id,string memberid,Feedback newfeedback)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Feedback Feedback = new Feedback
                {
                    EvaluatingTLId = userId,
                    EvaluatedJDId = memberid,
                    ProjectId = id,
                    FeedbackDesc = newfeedback.FeedbackDesc,
                    Communication = newfeedback.Communication,
                    TeamWork = newfeedback.TeamWork,
                    ProblemSolving =newfeedback.ProblemSolving,
                    WorkKnowledge = newfeedback.WorkKnowledge,
                    IndependantAction = newfeedback.IndependantAction


                };
                context.Feedbacks.Add(Feedback);
                context.SaveChanges();
                return RedirectToAction("Admin_PM_TL", "Home");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }


        }

        [MyAuthorize(Roles = "Project Manager")]
        public ActionResult ViewEvaluation(int id, string memberid)
        {
            try
            {

                ViewBag.id = id;
                ViewBag.memberid = memberid;
                var evaluation = from feedback in context.Feedbacks
                                 join user in context.Users on feedback.EvaluatingTLId equals user.Id
                                 where feedback.EvaluatedJDId == memberid && feedback.ProjectId == id
                                 select new FeedbackViewModel
                                 {
                                     Feedback = feedback,
                                     User = user
                                 };

                var evaluationlist = evaluation.First();

                return PartialView(evaluationlist);
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return RedirectToAction("Admin_PM_TL", "Home");
            }
        }

        [MyAuthorize(Roles = "Junior Developer")]
        public ActionResult EvaluationChart()
        {
            var userId = User.Identity.GetUserId();
            int communication = context.Feedbacks.Where(u => u.EvaluatedJDId == userId).AsEnumerable().Sum(u => u.Communication);
            int TeamWork = context.Feedbacks.Where(u => u.EvaluatedJDId == userId).AsEnumerable().Sum(u => u.TeamWork);
            int Problemolving = context.Feedbacks.Where(u => u.EvaluatedJDId == userId).AsEnumerable().Sum(u => u.ProblemSolving);
            int WorkKnowledge = context.Feedbacks.Where(u => u.EvaluatedJDId == userId).AsEnumerable().Sum(u => u.WorkKnowledge);
            int IndependantAction = context.Feedbacks.Where(u => u.EvaluatedJDId == userId).AsEnumerable().Sum(u => u.IndependantAction);
            ArrayList header = new ArrayList { "Skill", "Score" };
            ArrayList data1 = new ArrayList { "communication", communication };
            ArrayList data2 = new ArrayList { "Team Work", TeamWork };
            ArrayList data3 = new ArrayList { "Problem Solving", Problemolving };
            ArrayList data4 = new ArrayList { "Work Knowledge", WorkKnowledge };
            ArrayList data5 = new ArrayList { "Independant Action", IndependantAction };
            ArrayList data = new ArrayList { header, data1, data2, data3, data4, data5 };
            //convert in json
            string dataStr1 = JsonConvert.SerializeObject(data, Formatting.None);
            //store it in viewdata
            ViewBag.AllData = new HtmlString(dataStr1);
            return View();
        }

        [MyAuthorize(Roles = "Junior Developer, Project Manager, Team Leader")]
        public ActionResult ProjectChart()
        {
            var userId = User.Identity.GetUserId();
            int works_on = context.Works_on.Where(u => u.UserId == userId).Select(u => u.ProjectId).Count();
            int worked_on = context.Worked_on.Where(u => u.UserId == userId).Select(u => u.ProjectId).Count();
            ArrayList header = new ArrayList { "Projects", "Number" };
            ArrayList data1 = new ArrayList { "On Progress", works_on };
            ArrayList data2 = new ArrayList { "Delivered", worked_on };
            ArrayList data = new ArrayList { header, data1, data2 };
            //convert in json
            string dataStr = JsonConvert.SerializeObject(data, Formatting.None);
            //store it in viewdata
            ViewBag.Data = new HtmlString(dataStr);
            return View();
        }

        [MyAuthorize(Roles = "Junior Developer, Project Manager, Team Leader")]
        public ActionResult ExperienceChart()
        {
            var userId = User.Identity.GetUserId();
            var ASPNETMVC = context.Experience.Where(u => u.UserId == userId).Select(u => u.ASPNETMVC).First();
            var JavaScript = context.Experience.Where(u => u.UserId == userId).Select(u => u.JavaScript).First();
            var JQuery = context.Experience.Where(u => u.UserId == userId).Select(u => u.JQuery).First();
            var HTML5 = context.Experience.Where(u => u.UserId == userId).Select(u => u.HTML5).First();
            var PHP = context.Experience.Where(u => u.UserId == userId).Select(u => u.PHP).First();
            var JAVA = context.Experience.Where(u => u.UserId == userId).Select(u => u.JAVA).First();
            ArrayList header = new ArrayList { "Experience", "Years" };
            ArrayList data1 = new ArrayList { "ASP.NET MVC", ASPNETMVC };
            ArrayList data2 = new ArrayList { "JavaScript", JavaScript };
            ArrayList data3 = new ArrayList { "JQuery", JQuery };
            ArrayList data4 = new ArrayList { "HTML5", HTML5 };
            ArrayList data5 = new ArrayList { "PHP", PHP };
            ArrayList data6 = new ArrayList { "JAVA", JAVA };
            ArrayList data = new ArrayList { header, data1, data2, data3, data4, data5, data6};
            //convert in json
            string dataStr3 = JsonConvert.SerializeObject(data, Formatting.None);
            //store it in viewdata
            ViewBag.ExpData = new HtmlString(dataStr3);
            return View();
        }



    }
}