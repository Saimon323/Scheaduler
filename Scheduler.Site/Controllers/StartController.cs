using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using Scheduler.Model.EntityModels;
using Scheduler.Model.Repositories;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Site.Models;

namespace Scheduler.Site.Controllers
{
    public class StartController : Controller
    {

        private SelectList GetSelectList()
        {
            //var list = new[] 
            //{ 
            //    new Room { RoomId = 1, Building = "FAYARD HALL"},
            //    new Room { RoomId = 2, Building = "WHATEVER HALL"},
            //    new Room { RoomId = 3, Building = "TIME SQUARE"},
            //    new Room { RoomId = 4, Building = "MISSISSIPPI"},
            //    new Room { RoomId = 10, Building = "NEW YORK"}
            //};

            IUserRepository userRepo = new UserRepository();
            IEnumerable<Role> allRoles = userRepo.getAllRole();
            List<RoleList> list = new List<RoleList>();
            foreach (var singleRole in allRoles)
            {
                list.Add(new RoleList(singleRole.id,singleRole.Name));
            }

            return new SelectList(list, "RoleId", "RoleName");
        }

        public ActionResult Index()
        {
           /* HttpCookie log = Request.Cookies.Get("LogOn");
            

            if (log != null)
            {
                return View("Zalogowany");
            }
            return View();*/

            var cookie = Request.Cookies["LogOn"];
            if (cookie != null)
            {
                //return View("Zalogowany");
                return RedirectToAction("HomePage", "Page");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Index(LoginModel data)
        {
            //HttpCookie log = Request.Cookies.Get("LogOn");
            IUserRepository userRepo = new UserRepository();
            bool userExist = userRepo.LogIn(data.Login, data.Password);

            if (ModelState.IsValid && userExist != false)
            {
                /*log = new HttpCookie("LogOn");
               // log.Domain = data.Login.ToString();
                log.Value = DateTime.Now.ToString();
                log.Expires = DateTime.Now.AddMinutes(2d);
                Response.Cookies.Add(log);*/
                string cookieValue = data.Login.ToString();
                var cookie = new HttpCookie("LogOn", cookieValue);
                Response.AppendCookie(cookie);
                return RedirectToAction("HomePage", "Page");
            }
            else return View();
        }

        public ActionResult Register()
        {
            //IUserRepository userRepo = new UserRepository();
            //IEnumerable<Role> allRoles = userRepo.getAllRole();
            //List<SelectListItem> lista = new List<SelectListItem>();
            
            //int i = 0;
            //SelectListItem singlePosition;
            //foreach (var temp in allRoles)
            //{
            //    if (i == 0)
            //    {
            //        singlePosition = new SelectListItem {Selected = true, Text = temp.Name, Value = i.ToString()};
            //    }
            //    else
            //    {
            //        singlePosition = new SelectListItem { Selected = false, Text = temp.Name, Value = i.ToString() };
            //    }
                
            //    lista.Add(singlePosition);
            //    i++;
            //}

            //IEnumerable<SelectListItem> list = lista;
            //ViewBag.MovieType = list;

            //var model = new RegisterModel();

            //return View("Register", model);

            RegisterModel viewModel = new RegisterModel
            {
                RoleSelect = GetSelectList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel data)
        {
            IUserRepository userRepo = new UserRepository();
            bool userExist = userRepo.LogIn(data.Login, data.Password);

            if (userExist == true)
            {
                return View("Index");
            }
            else
            {
                Role role = userRepo.getRoleById(data.RoleId);
                userRepo.addNewUser(data.Name,data.Surname,data.Login,data.Password,role.Name);
                return RedirectToAction("HomePage", "Page");
            }
                
            //ViewBag.messageString = data;
            //return View("Zalogowany");
        }

       
        public ActionResult LogOut( )
        {
           // HttpCookie log = Request.Cookies.Get("LogOn");

          //  log.Expires = DateTime.Now;

           // return View("Index");


          /*  var cookie = Request.Cookies["LogOn"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now;
                Response.Cookies.Add(cookie);
            }
            return View("Index");*/

            if (Request.Cookies["LogOn"] != null)
            {
                HttpCookie myCookie = new HttpCookie("LogOn");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            return RedirectToAction("Index");
        }
        

    }
}
