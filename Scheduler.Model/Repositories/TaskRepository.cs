using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Model.EntityModels;
using System.Data.Spatial;
using Task = Scheduler.Model.EntityModels.Task;
using System.Data.SqlClient;

namespace Scheduler.Model.Repositories
{
    public class TaskRepository : BaseRepository<Scheduler.Model.EntityModels.Task, SchedulerEntities>, ITaskRepository, IDisposable
    {
        #region BaseRepository

        public override void Add(Scheduler.Model.EntityModels.Task task)
        {
            Entities.AddToTasks(task);
        }

        public override IQueryable<Scheduler.Model.EntityModels.Task> Items
        {
            get
            {
                return Entities.Tasks;
            }
        }

        #endregion

        int autoIncrementId = 0;
        //IUserRepository userRepo = new UserRepository();
        //IProjectRepository projectRepo = new ProjectRepository();
        //IGroupRepository groupRepo = new GroupRepository();

        public Scheduler.Model.EntityModels.Task getTaskById(int id)
        {
            return Entities.Tasks.Where(x => x.id.Equals(id)).FirstOrDefault();
        }

        public Scheduler.Model.EntityModels.Task getTaskByNameAndProjectName(string ProjectName, string TaskName)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            Project projectExist = projectRepo.getProjectByName(ProjectName);
            if (ProjectName == null)
                return null;

            Scheduler.Model.EntityModels.Task task = Entities.Tasks.Where(x => x.ProjectId.Equals(projectExist.id) && x.TaskName.Equals(TaskName)).FirstOrDefault();
            return task;
        }

        public IEnumerable<Scheduler.Model.EntityModels.Task> getAllTasksInProjects(int ProjectId)
        {
            var list = Entities.Tasks.ToList();
            List<Scheduler.Model.EntityModels.Task> tasksList = new List<Scheduler.Model.EntityModels.Task>();
            foreach (var x in list)
            {
                if (x.ProjectId == ProjectId)
                {
                    tasksList.Add(x);
                }
            }
            return tasksList;
        }

        //public void addNewTask(string Login, DateTime StartTime, DateTime StopTime, string TaskName, int Hours, string ProjectName, int GroupId)
        //{
        //    IGroupRepository groupRepo = new GroupRepository();
        //    IProjectRepository projectRepo = new ProjectRepository();
        //    IUserRepository userRepo = new UserRepository();
        //    Project projectExist = projectRepo.getProjectByName(ProjectName);
            
        //    if (projectExist == null)
        //        return;

        //    Scheduler.Model.EntityModels.Task taskExist = getTaskInProjectByTaskName(projectExist.ProjectName, TaskName);
        //    if (taskExist != null)
        //        return;

        //    User userExist = userRepo.getUserByLogin(Login);

        //    if (userExist == null || userExist.GroupId == null)
        //        return;
            
        //    List<Group> GroupInProject = groupRepo.getAllGroupWorkingInProject(projectExist.ProjectName);

        //    foreach (var x in GroupInProject)
        //    {
        //        if (userExist.GroupId == x.id)
        //        {
        //            Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task(userExist.id, StartTime, StopTime, TaskName, Hours, projectExist.id);
        //            Entities.AddToTasks(task);
        //            Entities.SaveChanges();
        //        }
        //    }
            
        //}

        //public void addNewTask(string Login, DateTime StartTime, string TaskName, int Hours, string ProjectName, int GroupId)
        //{
        //    IGroupRepository groupRepo = new GroupRepository();
        //    IProjectRepository projectRepo = new ProjectRepository();
        //    IUserRepository userRepo = new UserRepository();
        //    Project projectExist = projectRepo.getProjectByName(ProjectName);

        //    if (projectExist == null)
        //        return;

        //    Scheduler.Model.EntityModels.Task taskExist = getTaskInProjectByTaskName(projectExist.ProjectName, TaskName);
        //    if (taskExist != null)
        //        return;

        //    User userExist = userRepo.getUserByLogin(Login);

        //    if (userExist == null || userExist.GroupId == null)
        //        return;

        //    List<Group> GroupInProject = groupRepo.getAllGroupWorkingInProject(projectExist.ProjectName);

        //    foreach (var x in GroupInProject)
        //    {
        //        if (userExist.GroupId == x.id)
        //        {
        //            Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task(userExist.id, StartTime, TaskName, Hours, projectExist.id);
        //            Entities.AddToTasks(task);
        //            Entities.SaveChanges();
        //        }
        //    }

        //}

        public void addNewTask(DateTime StartTime, DateTime StopTime, string TaskName, int Hours, string ProjectName, int GroupId)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            Project projectExist = projectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            Scheduler.Model.EntityModels.Task taskExist = getTaskInProjectByTaskName(projectExist.ProjectName, TaskName);
            if (taskExist != null)
                return;

            //Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task(StartTime, StopTime, TaskName, Hours, projectExist.id);
            Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task
            {
                StartTime = StartTime,
                StopTime = StopTime,
                TaskName = TaskName,
                Hours = Hours,
                ProjectId = projectExist.id,
                GroupId = GroupId
            };
            Entities.AddToTasks(task);
            Entities.SaveChanges();
            /*try
            {
                Entities.SaveChanges();
            }

            catch (SqlException ex)
            {
                Entities.Detach(task);
                int index = ex.InnerException.Message.IndexOf('\r');
                //return ex.InnerException.Message.Substring(0, index);
            }
            catch (Exception ex)
            {
                Entities.Detach(task);
                int index = ex.InnerException.Message.IndexOf('\r');
              //  return ex.InnerException.Message.Substring(0, index);
            }*/

        }

        public void addNewTask(DateTime StartTime, string TaskName, int Hours, string ProjectName, int GroupId)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            Project projectExist = projectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            Scheduler.Model.EntityModels.Task taskExist = getTaskInProjectByTaskName(projectExist.ProjectName, TaskName);
            if (taskExist != null)
                return;

            //Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task(StartTime, TaskName, Hours, projectExist.id);
            Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task
            {
                StartTime = StartTime,
                TaskName = TaskName,
                Hours = Hours,
                ProjectId = projectExist.id,
                GroupId = GroupId
            };
            Entities.AddToTasks(task);
            Entities.SaveChanges();


        }

        public void addUserToTask(string Login, string TaskName, string ProjectName)
        {
            IUserRepository userRepo = new UserRepository();

            Scheduler.Model.EntityModels.Task taskExist = getTaskByNameAndProjectName(ProjectName, TaskName);

            User userExist = userRepo.getUserByLogin(Login);

            if((taskExist == null && userExist == null) || userExist.GroupId == null)
                return ;

            bool realize = checkRealization(ProjectName, Login);

            if (realize == false)
                return;

            taskExist.WorkerId = userExist.id;
            Entities.SaveChanges();

        }

        public void addTaskStopTime(string ProjectName, string TaskName, DateTime StopTime)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            Project projectExist = projectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            Scheduler.Model.EntityModels.Task taskExist = getTaskByNameAndProjectName(projectExist.ProjectName, TaskName);

            if (taskExist == null)
                return;

            taskExist.StopTime = StopTime;
            Entities.SaveChanges();
        }

        public void deleteTask(string ProjectName, string TaskName)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            Project projectExist = projectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            Scheduler.Model.EntityModels.Task taskExist = getTaskByNameAndProjectName(projectExist.ProjectName, TaskName);

            if (taskExist == null)
                return;

            Entities.DeleteObject(taskExist);
            Entities.SaveChanges();
        }

        public void addNewComment(string ProjectName, string TaskName, string Text, DateTime Time, string Login)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            IUserRepository userRepo = new UserRepository();
            Project projectExist = projectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            Scheduler.Model.EntityModels.Task taskExist = getTaskByNameAndProjectName(projectExist.ProjectName, TaskName);

            if (taskExist == null)
                return;

            User userExist = userRepo.getUserByLogin(Login);

            if (userExist == null)
                return;

            Comment comment = Comment.CreateComment(autoIncrementId, taskExist.id, Text, Time, userExist.id);
            Entities.AddToComments(comment);
            Entities.SaveChanges();
        }

        public bool checkRealization(string ProjectName, string Login)
        {
            IGroupRepository groupRepo = new GroupRepository();
            IProjectRepository projectRepo = new ProjectRepository();
            IUserRepository userRepo = new UserRepository();

            Project projectExist = projectRepo.getProjectByName(ProjectName);

            User userExist = userRepo.getUserByLogin(Login);

            if (projectExist == null || userExist == null || userExist.GroupId == null)
                return false;

            List<Group> groupList = groupRepo.getAllGroupWorkingInProject(projectExist.ProjectName);

            foreach (var x in groupList)
            {
                if (userExist.GroupId == x.id)
                    return true;
            }

            return false;
        }

        public Scheduler.Model.EntityModels.Task getTaskInProjectByTaskName(string ProjectName, string TaskName)
        {
            IProjectRepository projectRepo = new ProjectRepository();
            Project projectExist = projectRepo.getProjectByName(ProjectName);
            return Items.Where(x => x.ProjectId.Equals(projectExist.id) && x.TaskName.Equals(TaskName)).FirstOrDefault();
        }

        public IEnumerable<Scheduler.Model.EntityModels.Task> GetAllTasksInProjectByGroupId(int ProjectId, int GroupId)
        {
            return Items.Where(x => x.ProjectId.Equals(ProjectId) && x.GroupId.Equals(GroupId));
        }

        public IEnumerable<Scheduler.Model.EntityModels.Task> getAllTasksUser(int id)
        {
            List<Scheduler.Model.EntityModels.Task> tasks = Items.ToList();
            List<Scheduler.Model.EntityModels.Task> resultList = new List<Task>();
            foreach (var x in tasks)
            {
                if (x.WorkerId == id)
                {
                    resultList.Add(x);
                }
            }

            return resultList;
        }

        public void setValueNullAllTaskUser(int id)
        {
            IEnumerable<Scheduler.Model.EntityModels.Task> tasksList = getAllTasksUser(id);
            foreach (var x in tasksList)
            {
                x.WorkerId = null;
            }

            Entities.SaveChanges();
        }

        public void setValueNullTaskUser(int TaskId)
        {
            Scheduler.Model.EntityModels.Task task = getTaskById(TaskId);
            task.WorkerId = null;
            Entities.SaveChanges();
        }
    }
}
