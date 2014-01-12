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
    public class WorkerController : Controller
    {

        public ActionResult HomePageWorker()
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            User user = userRepo.getUserByLogin(userLogin);
            
            if (user.GroupId == null)
            {
                return RedirectToAction("JoinToGroup", "Worker");
            }

            IGroupRepository groupRepo = new GroupRepository();
            IEnumerable<User> groupMemberList = userRepo.getAllMemberGroup(user.GroupId.GetValueOrDefault());
            Group groupExist = groupRepo.getGroupById(user.GroupId.GetValueOrDefault());
            User menagereExist = userRepo.getUserById(groupExist.MenagerId);
            List<User> memberList = new List<User>();

            foreach (User x in groupMemberList)
            {
                memberList.Add(x);
            }
            GroupDetail groupDetail = new GroupDetail
            {
                Date = groupExist.CreationData,
                GroupName = groupExist.GroupName,
                id = groupExist.id,
                Login = menagereExist.Login,
                MemberGroupList = memberList,
                Name = menagereExist.Name,
                Surname = menagereExist.Surname
            };

            return View(groupDetail);
        }

        public ActionResult JoinToGroup()
        {
            IGroupRepository groupRepo = new GroupRepository();
            IUserRepository userRepo = new UserRepository();
            IEnumerable<Group> groupList = groupRepo.getAllGroup();
            List<GroupList> groupLists = new List<GroupList>();
            User user;
            GroupList singleGroup;

            foreach (var group in groupList)
            {
                user = userRepo.getUserById(group.MenagerId);
                singleGroup = new GroupList
                {
                    id = group.id,
                    Date = group.CreationData,
                    Name = user.Name,
                    GroupName = group.GroupName,
                    Login = user.Login,
                    Surname = user.Surname

                };
                groupLists.Add(singleGroup);
            }

            return View(groupLists);
        }

        public ActionResult GroupDetails(int id, string Name, string Surname, string Login, string GroupName, DateTime Date)
        {
            IGroupRepository groupRepo = new GroupRepository();
            IUserRepository userRepo = new UserRepository();
            IEnumerable<User> groupMemberList = userRepo.getAllMemberGroup(id);
            List<User> memberList = new List<User>();
            foreach (User x in groupMemberList)
            {
                memberList.Add(x);
            }
            GroupDetail groupDetail = new GroupDetail
            {
                Date = Date,
                GroupName = GroupName,
                id = id,
                Login = Login,
                MemberGroupList = memberList,
                Name = Name,
                Surname = Surname
            };

            return View("GroupDetails", groupDetail);
        }

        public ActionResult AddUserToGroup(int idGroup)
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            User userExist = userRepo.getUserByLogin(userLogin);
            Group groupExist = groupRepo.getGroupById(idGroup);
            userRepo.addUserToGroup(userExist.Login, groupExist.GroupName);
            return RedirectToAction("HomePageWorker", "Worker");

        }

        public ActionResult DeleteUserFromGroup(string GroupName)
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            ITaskRepository taskRepo = new TaskRepository();

            User userExist = userRepo.getUserByLogin(userLogin);
            IEnumerable<Scheduler.Model.EntityModels.Task> tasksList = taskRepo.getAllTasksUser(userExist.id);
            userRepo.deleteUserFromGroup(userLogin,GroupName);
            foreach (var x in tasksList)
            {
                x.WorkerId = null;
            }
            taskRepo.setValueNullAllTaskUser(userExist.id);
            
            return RedirectToAction("HomePageWorker", "Worker");
        }

        public ActionResult Projects()
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            IProjectRepository projectRepo = new ProjectRepository();

            User userExist = userRepo.getUserByLogin(userLogin);
            Group groupExist = groupRepo.getGroupById(userExist.GroupId.GetValueOrDefault());
            IEnumerable<Project> projectList = groupRepo.GetAllProjectsRealizationByGroup(groupExist.id);


            return View(projectList);
        }

        public ActionResult TasksList(int ProjectId)
        {

            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;



            ITaskRepository taskRepo = new TaskRepository();
            IUserRepository userRepo = new UserRepository();
            IProjectRepository projectRepo = new ProjectRepository();
            User user = userRepo.getUserByLogin(userLogin);
            User userExist;
            Project projectExist = projectRepo.getProjectById(ProjectId);
            List<TaskInProject> tasksAndDetailsList = new List<TaskInProject>();
            TaskInProject taskInProject = new TaskInProject();
            taskInProject.id = ProjectId;
            taskInProject.GroupId = user.GroupId.GetValueOrDefault();
            taskInProject.ProjectName = projectExist.ProjectName;

            //IEnumerable<Scheduler.Model.EntityModels.Task> tasksInProject = taskRepo.getAllTasksInProjects(ProjectId);
            IEnumerable<Scheduler.Model.EntityModels.Task> tasksInProject = taskRepo.GetAllTasksInProjectByGroupId(ProjectId, user.GroupId.GetValueOrDefault());

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

                }
                else if (x.WorkerId != null)
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

            return View(taskInProject);
        }

        public ActionResult AddUserToTask(string TaskName, string ProjectName)
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IProjectRepository projectRepo = new ProjectRepository();
            ITaskRepository taskRepo = new TaskRepository();
            Project project = projectRepo.getProjectByName(ProjectName);
            taskRepo.addUserToTask(userLogin, TaskName, ProjectName);

            return RedirectToAction("TasksList", "Worker", new {ProjectId = project.id});
        }

        public ActionResult MyTasks(string sortBy = "")
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            ITaskRepository taskRepo = new TaskRepository();
            IProjectRepository projectRepo = new ProjectRepository();
            User user = userRepo.getUserByLogin(userLogin);
            Project projectExist;
            MyTask myTask;
            IEnumerable<Scheduler.Model.EntityModels.Task> tasksList = taskRepo.getAllTasksUser(user.id);
            List<MyTask> myTasksList = new List<MyTask>();


            foreach (var x in tasksList)
            {
                projectExist = projectRepo.getProjectById(x.ProjectId);
                myTask = new MyTask
                {
                    Hours = x.Hours,
                    id = x.id,
                    StartTime = x.StartTime,
                    StopTime = x.StopTime.GetValueOrDefault(),
                    TaskName = x.TaskName,
                    ProjectName = projectExist.ProjectName

                };
                myTasksList.Add(myTask);
            }


            switch (sortBy)
            {
                case "ProjectName":
                    myTasksList = myTasksList.OrderBy(u => u.ProjectName).ToList();
                    break;
                case "TaskName":
                    myTasksList = myTasksList.OrderBy(u => u.TaskName).ToList();
                    break;
                case "StartTime":
                    myTasksList = myTasksList.OrderBy(u => u.StartTime).ToList();
                    break;
                case "StopTime":
                    myTasksList = myTasksList.OrderBy(u => u.StopTime).ToList();
                    break;
                case "Hours":
                    myTasksList = myTasksList.OrderBy(u => u.Hours).ToList();
                    break;
                case "ProjectName_desc":
                    myTasksList = myTasksList.OrderByDescending(u => u.ProjectName).ToList();
                    break;
                case "TaskName_desc":
                    myTasksList = myTasksList.OrderByDescending(u => u.TaskName).ToList();
                    break;
                case "StartTime_desc":
                    myTasksList = myTasksList.OrderByDescending(u => u.StartTime).ToList();
                    break;
                case "StopTimev":
                    myTasksList = myTasksList.OrderByDescending(u => u.StopTime).ToList();
                    break;
                case "Hours_desc":
                    myTasksList = myTasksList.OrderByDescending(u => u.Hours).ToList();
                    break;
                default:
                    myTasksList = myTasksList.OrderBy(u => u.ProjectName).ToList();
                    break;
                
            }

            return View(myTasksList.ToList());
        }

        public ActionResult DeleteRealization(int TaskId)
        {
            ITaskRepository taskRepository = new TaskRepository();
            taskRepository.setValueNullTaskUser(TaskId);

            return RedirectToAction("MyTasks", "Worker");
        }

    }
}
