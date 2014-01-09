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
           /* var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            User userExist = userRepo.getUserByLogin(userLogin);
            IEnumerable<Group> menagerGroupsList = groupRepo.getAllGroupByMenagerId(userExist.id);
            IEnumerable<Project> projectsList = groupRepo.GetAllProjectsRealizationByGroup(userExist.id);

            return View("ProjectsList",projectsList);*/

            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            List<Project> projectsRealization = new List<Project>();
            User userExist = userRepo.getUserByLogin(userLogin);
            IEnumerable<Project> projectsList;
            ProjectAndGroupRealizationList item;
            IEnumerable<Group> menagerGroupsList = groupRepo.getAllGroupByMenagerId(userExist.id);
            //IEnumerable<Project> projectsList = groupRepo.GetAllProjectsRealizationByGroup(userExist.id);

            List<ProjectAndGroupRealizationList> projectAndGroupRealizationLists = new List<ProjectAndGroupRealizationList>();
            foreach (var group in menagerGroupsList)
            {
                projectsList = groupRepo.GetAllProjectsRealizationByGroup(group.id);
                foreach (var project in projectsList)
                {
                    item = new ProjectAndGroupRealizationList
                    {
                        GroupId = group.id,
                        GroupName = group.GroupName,
                        ProjectId = project.id,
                        ProjectName = project.ProjectName
                    };
                    projectAndGroupRealizationLists.Add(item);
                }
            }

            return View("ProjectsList", projectAndGroupRealizationLists);
        }

        public ActionResult TasksInProject(int ProjectId, int GroupId)
        {
            ITaskRepository taskRepo = new TaskRepository();
            IUserRepository userRepo = new UserRepository();
            User userExist;
            List<TaskInProject> tasksAndDetailsList = new List<TaskInProject>();
            TaskInProject taskInProject = new TaskInProject();
            taskInProject.id = ProjectId;
            taskInProject.GroupId = GroupId;

            //IEnumerable<Scheduler.Model.EntityModels.Task> tasksInProject = taskRepo.getAllTasksInProjects(ProjectId);
            IEnumerable<Scheduler.Model.EntityModels.Task> tasksInProject = taskRepo.GetAllTasksInProjectByGroupId(ProjectId,GroupId);

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

        public ActionResult CreatNewTask(int ProjectId, int GroupId)
        {
         /*   NewTask newTask = new NewTask
            {
                id = ProjectId
            };

            return View("CreatNewTask", newTask);*/
            ViewBag.projectId = ProjectId;
            ViewBag.groupId = GroupId;

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

            taskRepo.addNewTask(data.StartTime,data.TaskName,data.Hours,projectExist.ProjectName, data.GroupId);

            return RedirectToAction("TasksInProject", "Menager", new { ProjectId = data.id, GroupId = data.GroupId});
        }

        public ActionResult SelectProject()
        {
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            IProjectRepository projectRepo = new ProjectRepository();
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            User menagerExist = userRepo.getUserByLogin(userLogin);
            IEnumerable<Group> menagerGroups = groupRepo.getAllGroupByMenagerId(menagerExist.id);
            IEnumerable<Project> allProjects = projectRepo.getAllProjects();
            List<Project> projectsToRealizationList = new List<Project>();
            bool add;

            foreach (var project in allProjects)
            {
                foreach (var group in menagerGroups)
                {
                    add = groupRepo.checkRealization(project.ProjectName, group.GroupName);
                    if (add == false)
                    {
                        projectsToRealizationList.Add(project);
                        break;
                    }
                }
            }

            return View("SelectProject", projectsToRealizationList);
        }

        public ActionResult SelectGroup(int ProjectId)
        {
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            IProjectRepository projectRepo = new ProjectRepository();  
 
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            User menagerExist = userRepo.getUserByLogin(userLogin);
            IEnumerable<Group> menagerGroupsList = groupRepo.getGroupByMenagerId(menagerExist.id);
            Project projectExist = projectRepo.getProjectById(ProjectId);

            List<Group> groupsList = new List<Group>();
            bool add;

            foreach (var group in menagerGroupsList)
            {
                add = groupRepo.checkRealization(projectExist.ProjectName, group.GroupName);
                if (add == false)
                {
                    groupsList.Add(group);
                }
            }

            ViewBag.projectId = ProjectId;
            return View("SelectGroup",groupsList);
        }

        public ActionResult AddRealization(int GroupId, int ProjectId)
        {
            IGroupRepository groupRepo = new GroupRepository();
            IProjectRepository projectRepo = new ProjectRepository();

            Group groupExist = groupRepo.getGroupById(GroupId);
            Project projectExist = projectRepo.getProjectById(ProjectId);

            groupRepo.addRealization(projectExist.ProjectName,groupExist.GroupName);

            return RedirectToAction("SelectProject", "Menager");
        }
    }
}
