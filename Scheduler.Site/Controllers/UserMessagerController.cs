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
    public class UserMessagerController : Controller
    {
        //
        // GET: /UserMessager/

        public ActionResult Index()
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            var userRepo = new UserRepository();
            User user = userRepo.getUserByLogin(userLogin);

            MessageRepository MessageRepo = new MessageRepository();

            ViewBag.NewMessages = MessageRepo.GetAllMessages().Where(m => m.ToUserId == user.id).Count(m => m.Status == true);

            return View();
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            UserRepository UserRepo = new UserRepository();

            ViewBag.Logins = UserRepo.GetAll().Select(u => u.Login).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateNew(MessageModel model)
        {
            if(model != null)
            {
                var cookie = Request.Cookies["LogOn"];
                string userLogin = cookie.Value;
                var userRepo = new UserRepository();
                User user = userRepo.getUserByLogin(userLogin);

                MessageRepository MessRepo = new MessageRepository();
                Message mess = new Message()
                {
                    Status = true,
                    FromUserId = user.id,
                    Text = model.Text,
                    Title = model.Title,
                    Time = DateTime.Now,
                    ToUserId = userRepo.getUserByLogin(model.ToUser).id
                };

                MessRepo.Add(mess);
            }

            return View("Index");
        }

        public ActionResult Received()
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            var userRepo = new UserRepository();
            User user = userRepo.getUserByLogin(userLogin);

            MessageRepository MessageRepo = new MessageRepository();

            var messages = MessageRepo.GetAllMessages().Where(m => m.ToUserId == user.id).ToList();

            return View(messages);
        }
        
        public ActionResult Sent()
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            var userRepo = new UserRepository();
            User user = userRepo.getUserByLogin(userLogin);

            MessageRepository MessageRepo = new MessageRepository();

            var messages = MessageRepo.GetAllMessages().Where(m => m.FromUserId == user.id).ToList();

            return View(messages);
        }

        public ActionResult Show(int id)
        {
            MessageRepository MessRepo = new MessageRepository();

            MessRepo.MarkAsRead(id);

            var mess = MessRepo.getMessageById(id);
            
            return View(mess);
        }

        public ActionResult MarkAsRead(int id)
        {
            MessageRepository repo = new MessageRepository();

            repo.MarkAsRead(id);

            UserRepository userRepo = new UserRepository();
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            var user = userRepo.getUserByLogin(userLogin);

            var messages = repo.GetAllMessages().Where(m => m.ToUserId == user.id).ToList();


            return View("Received", messages);
        }

        public ActionResult MarkAsNotRead(int id)
        {
            MessageRepository repo = new MessageRepository();

            repo.MarkAsNotRead(id);

            UserRepository userRepo = new UserRepository();
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            var user = userRepo.getUserByLogin(userLogin);

            var messages = repo.GetAllMessages().Where(m => m.ToUserId == user.id).ToList();


            return View("Received", messages);
        }
    }
}
