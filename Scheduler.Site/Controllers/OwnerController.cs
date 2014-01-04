using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Data.Edm.Expressions;
using Scheduler.Model.EntityModels;
using Scheduler.Model.Repositories;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Site.Models;

namespace Scheduler.Site.Controllers
{
    public class OwnerController : Controller
    {
        public ActionResult HomePageOwner()
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            User userExist = userRepo.getUserByLogin(userLogin);
            IProjectRepository projectRepo = new ProjectRepository();
            IEnumerable<Project> projectList = projectRepo.getAllProjectByIdOwner(userExist.id);
            return View("HomePageOwner", projectList);
        }

        public ActionResult ProjectDetail(int ProjectId)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            ITaskRepository taskRepo = new TaskRepository();
            Project projectExist = projectRepo.getProjectById(ProjectId);
            IEnumerable<Scheduler.Model.EntityModels.Task> tasksInProject = taskRepo.getAllTasksInProjects(ProjectId);
            ProjectDetail project = new ProjectDetail
            {
                id = projectExist.id,
                ProjectName = projectExist.ProjectName,
                Budget = projectExist.Budget,
                StartTime = projectExist.StartTime,
                StopTime = projectExist.StopTime,
                tasksInProjectList = tasksInProject
            };
            return View("ProjectDetail", project);
        }

        public ActionResult Reaization(int ProjectId)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            IGroupRepository groupRepo = new GroupRepository();
            IUserRepository userRepo = new UserRepository();

            Project projectExist = projectRepo.getProjectById(ProjectId);
            List<Group> groupList = groupRepo.getAllGroupWorkingInProject(projectExist.ProjectName);
            List<RealizationList> realizationList = new List<RealizationList>();
            RealizationList single;
            User menager;
            IEnumerable<User> memberList;
            foreach (var group in groupList)
            {
                menager = userRepo.getUserById(group.MenagerId);
                memberList = userRepo.getAllMemberGroup(group.id);
                single = new RealizationList
                {
                    GroupName = group.GroupName,
                    Name = menager.Name,
                    Surname = menager.Surname,
                    Login = menager.Login,
                    membersList = memberList
                };
                realizationList.Add(single);
            }

            return View("Reaization",realizationList);
        }

        public ActionResult AddNewProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProject(ProjectModel data)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            IUserRepository userRepo = new UserRepository();

            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            Project projectExist = projectRepo.getProjectByName(data.ProjectName);
            User userExist = userRepo.getUserByLogin(userLogin);
            if (projectExist != null)
                return View("Exist");

            if (data.StopTime == null)
            {
                projectRepo.addNewProject(data.ProjectName, data.Budget, data.StarTime, userExist.Login);
            
            }else if (data.StopTime != null)
            {
                projectRepo.addNewProject(data.ProjectName, data.Budget, data.StarTime, userExist.Login);
            }

            return RedirectToAction("HomePageOwner", "Owner");
        }

    }
}
