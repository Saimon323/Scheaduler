using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.EntityModels;
using System.Data.Spatial;

namespace Scheduler.Model.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Scheduler.Model.EntityModels.Task getTaskById(int id);
        Scheduler.Model.EntityModels.Task getTaskByNameAndProjectName(string ProjectName, string TaskName);
        IEnumerable<Scheduler.Model.EntityModels.Task> getAllTasksInProjects(int ProjectId);
     //   void addNewTask(string Login, DateTime StartTime, DateTime StopTime, string TaskName, int Hours, string ProjectName);
       // void addNewTask(string Login, DateTime StartTime, string TaskName, int Hours, string ProjectName);
        bool addNewTask(DateTime StartTime, DateTime StopTime, string TaskName, int Hours, string ProjectName, int GroupId);
        void addNewTask(DateTime StartTime, string TaskName, int Hours, string ProjectName, int GroupId);
        void addUserToTask(string Login, string TaskName, string ProjectName);
        void addTaskStopTime(string ProjectName, string TaskName, DateTime StopTime);
        void deleteTask(string ProjectName, string TaskName);
        void addNewComment(string ProjectName, string TaskName, string Text, DateTime Time, string Login);
        bool checkRealization(string ProjectName, string Login);
        Scheduler.Model.EntityModels.Task getTaskInProjectByTaskName(string ProjectName, string TaskName);
        IEnumerable<Scheduler.Model.EntityModels.Task> GetAllTasksInProjectByGroupId(int ProjectId, int GroupId);
        IEnumerable<Scheduler.Model.EntityModels.Task> getAllTasksUser(int id);
        void setValueNullAllTaskUser(int id);
        void setValueNullTaskUser(int TaskId);
    }
}
