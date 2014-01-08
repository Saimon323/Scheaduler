using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Scheduler.Model.EntityModels;
using Scheduler.Model.Repositories;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Site.Models;


namespace Scheduler.Site.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index(string sortBy = "")
        {
            UserRepository UserRepo = new UserRepository();
            var users = UserRepo.GetAll();

            switch (sortBy)
            {
                case "Name":
                    users = users.OrderBy(u => u.Name).ToList();
                    break;
                case "Surname":
                    users = users.OrderBy(u => u.Surname).ToList();
                    break;
                case "Login":
                    users = users.OrderBy(u => u.Login).ToList();
                    break;
                case "Password":
                    users = users.OrderBy(u => u.Password).ToList();
                    break;
                case "RoleId":
                    users = users.OrderBy(u => u.RoleId).ToList();
                    break;
                case "GroupId":
                    users = users.OrderBy(u => u.GroupId).ToList();
                    break;
                case "Name_desc":
                    users = users.OrderByDescending(u => u.Name).ToList();
                    break;
                case "Surname_desc":
                    users = users.OrderByDescending(u => u.Surname).ToList();
                    break;
                case "Login_desc":
                    users = users.OrderByDescending(u => u.Login).ToList();
                    break;
                case "Password_desc":
                    users = users.OrderByDescending(u => u.Password).ToList();
                    break;
                case "RoleId_desc":
                    users = users.OrderByDescending(u => u.RoleId).ToList();
                    break;
                case "GroupId_desc":
                    users = users.OrderByDescending(u => u.GroupId).ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.Name).ToList();
                    break;
            }


            return View(users.ToList());
        }

        //public ActionResult Sort()
        //{
        //    UserRepository UserRepo = new UserRepository();
        //    var users = UserRepo.GetAll().OrderBy(u => u.Login).ToList();

        //    return View("Index", users);
        //}
    }
}
