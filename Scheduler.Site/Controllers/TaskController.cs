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
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index(string sortBy = "")
        {
            TaskRepository TaskRepo = new TaskRepository();

            var tasks = TaskRepo.GetAll().ToList();

            switch (sortBy)
            {
                case "WorkerId":
                    tasks = tasks.OrderBy(t => t.WorkerId).ToList();
                    break;
                case "WorkerId_desc":
                    tasks = tasks.OrderByDescending(t => t.WorkerId).ToList();
                    break;
                case "StartTime":
                    tasks = tasks.OrderBy(t => t.StartTime).ToList();
                    break;
                case "StartTime_desc":
                    tasks = tasks.OrderByDescending(t => t.StartTime).ToList();
                    break;
                case "StopTime":
                    tasks = tasks.OrderBy(t => t.StopTime).ToList();
                    break;
                case "StopTime_desc":
                    tasks = tasks.OrderByDescending(t => t.StopTime).ToList();
                    break;
                case "TaskName":
                    tasks = tasks.OrderBy(t => t.TaskName).ToList();
                    break;
                case "TaskName_desc":
                    tasks = tasks.OrderByDescending(t => t.TaskName).ToList();
                    break;
                case "Hours":
                    tasks = tasks.OrderBy(t => t.Hours).ToList();
                    break;
                case "Hours_desc":
                    tasks = tasks.OrderByDescending(t => t.Hours).ToList();
                    break;
                case "ProjectId":
                    tasks = tasks.OrderBy(t => t.ProjectId).ToList();
                    break;
                case "ProjectId_desc":
                    tasks = tasks.OrderByDescending(t => t.ProjectId).ToList();
                    break;
                case "ProjectName":
                    var tasksWithProject = tasks.Where(t => t.Project != null);
                    tasks = tasks.Where(t => t.Project == null).ToList();
                    tasks.AddRange(tasksWithProject.OrderBy(t => t.Project.ProjectName).ToList());
                    break;
                case "ProjectName_desc":
                    var tasksWithoutProject = tasks.Where(t => t.Project == null);
                    tasks = tasks.Where(t => t.Project != null).OrderByDescending(t => t.Project.ProjectName).ToList();
                    tasks.AddRange(tasksWithoutProject);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.TaskName).ToList();
                    break;
            }
            
            return View(tasks.ToList());
        }

        public ActionResult SearchByWorkerId(string workerId)
        {
            TaskRepository TaskRepo = new TaskRepository();

            int id;

            if(int.TryParse(workerId, out id))
            {
                var tasks = TaskRepo.GetAll().Where(t => t.WorkerId == id).ToList();
                return View("Index", tasks);
            }

            return View("Index", TaskRepo.GetAll().ToList());
            
        }

        public ActionResult SearchByName(string taskName)
        {
            TaskRepository TaskRepo = new TaskRepository();

            var tasks = TaskRepo.GetAll().Where(t => t.TaskName.ToLower().Contains(taskName.ToLower())).ToList();

            return View("Index", tasks);
        }

        public ActionResult SearchByTime(string time)
        {
            TaskRepository TaskRepo = new TaskRepository();

            int hours;

            if(int.TryParse(time, out hours))
            {
                var tasks = TaskRepo.GetAll().Where(t => t.Hours == hours).ToList();
                return View("Index", tasks);
            }

            return View("Index", TaskRepo.GetAll().ToList());
        }

        public ActionResult SearchByProjectId(string projectId)
        {
           TaskRepository TaskRepo = new TaskRepository();

            int id;

            if(int.TryParse(projectId, out id))
            {
                var tasks = TaskRepo.GetAll().Where(t => t.ProjectId == id).ToList();
                return View("Index", tasks);
            }

            return View("Index", TaskRepo.GetAll().ToList());
        }

        public ActionResult SearchByProjectName(string projectName)
        {
            TaskRepository TaskRepo = new TaskRepository();

            var tasks = TaskRepo.GetAll().Where(t => (t.Project != null) && (t.Project.ProjectName.ToLower().Contains(projectName.ToLower()))).ToList();

            return View("Index", tasks);
        }
    }
}
