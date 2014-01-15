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
    public class MessageController : Controller
    {
        //
        // GET: /Message/

        public ActionResult Index(string sortBy = "")
        {
            MessageRepository MessageRepo = new MessageRepository();

            var messages = MessageRepo.GetAllMessages().ToList();


            switch (sortBy)
            {
                case "ToUserId":
                    messages = messages.OrderBy(m => m.ToUserId).ToList();
                    break;
                case "Time":
                    messages = messages.OrderBy(m => m.Time).ToList();
                    break;
                case "Title":
                    messages = messages.OrderBy(m => m.Title).ToList();
                    break;
                case "Text":
                    messages = messages.OrderBy(m => m.Text).ToList();
                    break;
                case "FromUserId":
                    messages = messages.OrderBy(m => m.FromUserId).ToList();
                    break;
                case "ToUserId_desc":
                    messages = messages.OrderByDescending(m => m.ToUserId).ToList();
                    break;
                case "Time_desc":
                    messages = messages.OrderByDescending(m => m.Time).ToList();
                    break;
                case "Title_desc":
                    messages = messages.OrderByDescending(m => m.Title).ToList();
                    break;
                case "Text_desc":
                    messages = messages.OrderByDescending(m => m.Text).ToList();
                    break;
                case "FromUserId_desc":
                    messages = messages.OrderByDescending(m => m.FromUserId).ToList();
                    break;
                case "Status_desc":
                    messages = messages.OrderByDescending(m => m.Status).ToList();
                    break;
                default:
                    messages = messages.OrderBy(m => m.Title).ToList();
                    break;
            }


            return View(messages);
        }

        public ActionResult SearchByToUserId(string toUserId)
        {
            MessageRepository MessageRepo = new MessageRepository();

            var messages = MessageRepo.GetAllMessages();

            int id;
            if(int.TryParse(toUserId, out id))
            {
                messages = messages.Where(m => m.ToUserId == id).ToList();
            }

            return View("Index", messages);
        }

        public ActionResult SearchByTime(string time)
        {
            MessageRepository MessageRepo = new MessageRepository();

            var messages = MessageRepo.GetAllMessages();

            DateTime Time;
            if(DateTime.TryParse(time, out Time))
            {
                messages = messages.Where(m => m.Time == Time).ToList();

            }

            return View("Index", messages);
        }

        public ActionResult SearchByTitle(string title)
        {
            MessageRepository MessageRepo = new MessageRepository();

            var messages = MessageRepo.GetAllMessages().Where(m => m.Title.ToLower().Contains(title.ToLower())).ToList();

            return View("Index", messages);
        }

        public ActionResult SearchByText(string text)
        {
            MessageRepository MessageRepo = new MessageRepository();

            var messages = MessageRepo.GetAllMessages().Where(m => m.Text.ToLower().Contains(text.ToLower())).ToList();

            return View("Index", messages);
        }

        public ActionResult SearchByFromUserId(string fromUserId)
        {
            MessageRepository MessageRepo = new MessageRepository();

            var messages = MessageRepo.GetAllMessages();

            int id;
            if(int.TryParse(fromUserId, out id))
            {
                messages = messages.Where(m => m.FromUserId == id).ToList();
            }

            return View("Index", messages);
        }
        
    }
}
