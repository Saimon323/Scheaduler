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
            var users = UserRepo.GetAll().ToList();

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
                case "GroupName":
                    var usersWithGroups = users.Where(u => u.Group != null);
                    users = users.Where(u => u.Group == null).ToList();// najpierw wrzuca na liste tych którzy nie mają grupy
                    users.AddRange(usersWithGroups.OrderBy(u => u.Group.GroupName));//potem tych co mają grupa i postortowanych
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
                case "GroupName_desc":
                    var usersWithoutGroups = users.Where(u => u.Group == null);
                    users = users.Where(u => u.Group != null).OrderByDescending(u => u.Group.GroupName).ToList();//najpierw wrzuca tych co mają grupe (malejąco)
                    users.AddRange(usersWithoutGroups);// na koniec tych co nie mają grupy
                    break;
                default:
                    users = users.OrderBy(u => u.Name).ToList();
                    break;
            }


            return View(users);
        }

        public ActionResult SearchByName(string param)
        {
            UserRepository UserRepo = new UserRepository();
            var users = (!String.IsNullOrWhiteSpace(param)) ?
                        UserRepo.GetAll().Where(u => u.Name.ToLower().Contains(param.ToLower())).ToList() 
                        : UserRepo.GetAll().ToList();

            return View("Index", users);
        }

        public ActionResult SearchBySurname(string param)
        {
            UserRepository UserRepo = new UserRepository();
            var users = (!String.IsNullOrWhiteSpace(param)) ?
                        UserRepo.GetAll().Where(u => u.Surname.ToLower().Contains(param.ToLower())).ToList()
                        : UserRepo.GetAll().ToList();

            return View("Index", users);
        }

        public ActionResult SearchByLogin(string param)
        {
            UserRepository UserRepo = new UserRepository();
            var users = (!String.IsNullOrWhiteSpace(param)) ?
                        UserRepo.GetAll().Where(u => u.Login.ToLower().Contains(param.ToLower())).ToList()
                        : UserRepo.GetAll().ToList();

            return View("Index", users);
        }

        public ActionResult SearchByPassword(string param)
        {
            UserRepository UserRepo = new UserRepository();
            var users = (!String.IsNullOrWhiteSpace(param)) ?
                        UserRepo.GetAll().Where(u => u.Password.ToLower().Contains(param.ToLower())).ToList()
                        : UserRepo.GetAll().ToList();

            return View("Index", users);
        }

        public ActionResult SearchByRoleId(string param)
        {
            UserRepository UserRepo = new UserRepository();
            var users = (!String.IsNullOrWhiteSpace(param)) ?
                        UserRepo.GetAll().Where(u => u.RoleId.ToString().Contains(param.ToLower())).ToList()
                        : UserRepo.GetAll().ToList();

            return View("Index", users);
        }

        public ActionResult SearchByGroupId(string param)
        {
            UserRepository UserRepo = new UserRepository();
            var users = (!String.IsNullOrWhiteSpace(param)) ?
                        UserRepo.GetAll().Where(u => u.GroupId.ToString().Contains(param.ToLower())).ToList()
                        : UserRepo.GetAll().ToList();

            return View("Index", users);
        }

        public ActionResult SearchByGroupName(string groupName)
        {
            UserRepository UserRepo = new UserRepository();
            var users = (!String.IsNullOrWhiteSpace(groupName)) ?
                        UserRepo.GetAll().Where(u => ((u.Group != null) && (u.Group.GroupName.Contains(groupName.ToLower())))).ToList()
                        : UserRepo.GetAll().ToList();

            return View("Index", users);
        }
    }
}
