using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using Scheduler.Model.EntityModels;

namespace Scheduler.Site.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }


    public class RegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public SelectList RoleSelect { get; set; }

    }

    public class RoleList
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public RoleList(int RoleId, string RoleName)
        {
            this.RoleId = RoleId;
            this.RoleName = RoleName;
        }
    }

    public class GroupList
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string GroupName { get; set; }
        public DateTime Date { get; set; }
    }

    public class GroupDetail : GroupList
    {
        public List<User> MemberGroupList;

        public GroupDetail()
        {
            MemberGroupList = new List<User>();
        }
    }

    public class ProjectDetail : Project
    {
        public IEnumerable<Scheduler.Model.EntityModels.Task> tasksInProjectList;

        public ProjectDetail()
        {
            tasksInProjectList = new List<Scheduler.Model.EntityModels.Task>();
        }
    }

    public class RealizationList
    {
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public IEnumerable<User> membersList;

        public RealizationList()
        {
            membersList = new List<User>();
        }
    }

    public class ProjectModel
    {
        public string ProjectName { get; set; }
        public float Budget { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime? StopTime { get; set; }
    }

    public class TaskInProject
    {
        public int id { get; set; }
        public int GroupId { get; set; }
        public List<TasksInRealization> tasksInRealizationList;
        public List<WaitingTask> tasksWaitingForRealizaionList; 

        public TaskInProject()
        {
            tasksInRealizationList = new List<TasksInRealization>();
            tasksWaitingForRealizaionList = new List<WaitingTask>();
        }
    }

    public class TasksInRealization
    {
        public string TaskName { get; set; }
        public int Hours { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public int id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class WaitingTask
    {
        public string TaskName { get; set; }
        public int Hours { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public int id { get; set; }
    }

    public class NewTask
    {
        public int id { get; set; }
        public int GroupId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public int Hours { get; set; }
    }

    public class ProjectAndGroupRealizationList
    {
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
    }

}