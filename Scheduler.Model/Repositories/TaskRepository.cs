using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Model.EntityModels;
using System.Data.Spatial;

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

        IUserRepository userRepo = new UserRepository();
        IProjectRepository projectRepo = new ProjectRepository();
        IGroupRepository groupRepo = new GroupRepository();

        public Scheduler.Model.EntityModels.Task getTaskById(int id)
        {
            return Entities.Tasks.Where(x => x.id.Equals(id)).FirstOrDefault();
        }

        public Scheduler.Model.EntityModels.Task getTaskByNameAndProjectName(string ProjectName, string TaskName)
        {
            Project projectExist = projectRepo.getProjectByName(ProjectName);
            if (ProjectName == null)
                return null;

            Scheduler.Model.EntityModels.Task task = Entities.Tasks.Where(x => x.ProjectId.Equals(projectExist.id) && x.TaskName.Equals(TaskName)).FirstOrDefault();
            return task;
        }

        public void addNewTask(string Login, DateTime StartTime, DateTime StopTime, string TaskName, int Hours, string ProjectName)
        {

            Project projectExist = projectRepo.getProjectByName(ProjectName);
            
            if (projectExist == null)
                return;

            User userExist = userRepo.getUserByLogin(Login);

            if (userExist == null || userExist.GroupId == null)
                return;
            
            List<Group> GroupInProject = groupRepo.getAllGroupWorkingInProject(projectExist.ProjectName);

            foreach (var x in GroupInProject)
            {
                if (userExist.GroupId == x.id)
                {
                    Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task(userExist.id, StartTime, StopTime, TaskName, Hours, projectExist.id);
                    Entities.AddToTasks(task);
                    Entities.SaveChanges();
                }
            }
            
        }

        public void addNewTask(string Login, DateTime StartTime, string TaskName, int Hours, string ProjectName)
        {

            Project projectExist = projectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            User userExist = userRepo.getUserByLogin(Login);

            if (userExist == null || userExist.GroupId == null)
                return;

            List<Group> GroupInProject = groupRepo.getAllGroupWorkingInProject(projectExist.ProjectName);

            foreach (var x in GroupInProject)
            {
                if (userExist.GroupId == x.id)
                {
                    Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task(userExist.id, StartTime, TaskName, Hours, projectExist.id);
                    Entities.AddToTasks(task);
                    Entities.SaveChanges();
                }
            }

        }

        public void addNewTask(DateTime StartTime, DateTime StopTime, string TaskName, int Hours, string ProjectName)
        {

            Project projectExist = projectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;


            Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task(StartTime, StopTime, TaskName, Hours, projectExist.id);
            Entities.AddToTasks(task);
            Entities.SaveChanges();
             

        }

        public void addNewTask(DateTime StartTime, string TaskName, int Hours, string ProjectName)
        {

            Project projectExist = projectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            Scheduler.Model.EntityModels.Task task = new Scheduler.Model.EntityModels.Task(StartTime, TaskName, Hours, projectExist.id);
            Entities.AddToTasks(task);
            Entities.SaveChanges();


        }

        public void addUserToTask(string Login, string TaskName, string ProjectName)
        {
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

        public bool checkRealization(string ProjectName, string Login)
        {
            Project projectExist = projectRepo.getProjectByName(ProjectName);
            User userExist = userRepo.getUserByLogin(Login);

            if (projectExist != null && userExist != null && userExist.GroupId != null)
                return false;

            List<Group> groupList = groupRepo.getAllGroupWorkingInProject(projectExist.ProjectName);

            foreach (var x in groupList)
            {
                if (userExist.GroupId == x.id)
                    return true;
            }

            return false;
        }
    }
}
