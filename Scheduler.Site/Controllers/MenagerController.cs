using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Scheduler.Model.EntityModels;
using Scheduler.Model.Repositories;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Site.Models;

namespace Scheduler.Site.Controllers
{
    public class MenagerController : Controller
    {
        public ActionResult HomePageMenager()
        {
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            User userExist = userRepo.getUserByLogin(userLogin);
            IEnumerable<Group> menagerGroupsList = groupRepo.getAllGroupByMenagerId(userExist.id);

            return View("HomePageMenager", menagerGroupsList);
        }

        public ActionResult GroupDetails(int GroupId)
        {
            IGroupRepository groupRepo = new GroupRepository();
            IUserRepository userRepo = new UserRepository();
            Group groupExist = groupRepo.getGroupById(GroupId);
            IEnumerable<User> groupMembersList = userRepo.getAllMemberGroup(groupExist.id);
            
            return View("GroupDetails",groupMembersList);
        }

        public ActionResult CreateNewGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewGroup(Group data)
        {
            IGroupRepository groupRepo = new GroupRepository();
            IUserRepository userRepo = new UserRepository();

            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            Group groupExist = groupRepo.getGroupByGroupName(data.GroupName);
            User userExist = userRepo.getUserByLogin(userLogin);
            if(groupExist != null)
                return View("Exist");

            groupRepo.addNewGroups(userExist.Login, data.GroupName, data.CreationData);

            return RedirectToAction("HomePageMenager", "Menager");
        }

        public ActionResult ProjectsList()
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            User userExist = userRepo.getUserByLogin(userLogin);
            IEnumerable<Group> menagerGroupsList = groupRepo.getAllGroupByMenagerId(userExist.id);
            IEnumerable<Project> projectsList = groupRepo.GetAllProjectsRealizationByGroup(userExist.id);

            return View("ProjectsList",projectsList);
        }

        public ActionResult TasksInProject(int ProjectId)
        {
            ITaskRepository taskRepo = new TaskRepository();
            IUserRepository userRepo = new UserRepository();
            User userExist;
            List<TaskInProject> tasksAndDetailsList = new List<TaskInProject>();
            TaskInProject taskInProject = new TaskInProject();
            taskInProject.id = ProjectId;

            IEnumerable<Scheduler.Model.EntityModels.Task> tasksInProject = taskRepo.getAllTasksInProjects(ProjectId);

            foreach (var x in tasksInProject)
            {
                if (x.WorkerId == null)
                {
                    taskInProject.tasksWaitingForRealizaionList.Add(new WaitingTask
                    {
                        Hours = x.Hours,
                        id = x.id,
                        StartTime = x.StartTime,
                        StopTime = x.StopTime,
                        TaskName = x.TaskName
                    });
                
                }else if (x.WorkerId != null)
                {
                    userExist = userRepo.getUserById(x.WorkerId.GetValueOrDefault());
                    taskInProject.tasksInRealizationList.Add(new TasksInRealization
                    {
                        TaskName = x.TaskName,
                        Hours = x.Hours,
                        StartTime = x.StartTime,
                        StopTime = x.StopTime,
                        id = x.id,
                        Login = userExist.Login,
                        Name = userExist.Name,
                        Surname = userExist.Surname
                    });
                   
                }
            }



            return View("TasksInProject", taskInProject);
        }

        public ActionResult CreatNewTask(int ProjectId)
        {
         /*   NewTask newTask = new NewTask
            {
                id = ProjectId
            };

            return View("CreatNewTask", newTask);*/
            ViewBag.projectId = ProjectId;
            return View();
        }

        [HttpPost]
        public ActionResult CreatNewTask(NewTask data)
        {
            ITaskRepository taskRepo = new TaskRepository();
            IProjectRepository projectRepo = new ProjectRepository();
            Project projectExist = projectRepo.getProjectById(data.id);
            Scheduler.Model.EntityModels.Task taskExist = taskRepo.getTaskByNameAndProjectName(projectExist.ProjectName, data.TaskName);
            if (taskExist != null)
                return View("TaskExist");

            taskRepo.addNewTask(data.StartTime,data.TaskName,data.Hours,projectExist.ProjectName);

            return RedirectToAction("TasksInProject", "Menager", new { ProjectId = data.id});
        }
    }
}
