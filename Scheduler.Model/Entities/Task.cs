using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Model.EntityModels
{
    public partial class Task :IIdentifable
    {
        public Task()
        {
        }

        public Task(int WorkerId, DateTime StartTime, DateTime StopTime, string TaskName, int Hours, int ProjectId)
        {
            this.id = 0;
            this.WorkerId = WorkerId;
            this.StartTime = StartTime;
            this.StopTime = StopTime;
            this.TaskName = TaskName;
            this.Hours = Hours;
            this.ProjectId = ProjectId;
        }

        public Task(int WorkerId, DateTime StartTime, string TaskName, int Hours, int ProjectId)
        {
            this.id = 0;
            this.WorkerId = WorkerId;
            this.StartTime = StartTime;
            this.StopTime = null;
            this.TaskName = TaskName;
            this.Hours = Hours;
            this.ProjectId = ProjectId;
        }

        public Task(DateTime StartTime, DateTime StopTime, string TaskName, int Hours, int ProjectId)
        {
            this.id = 0;
            this.WorkerId = null;
            this.StartTime = StartTime;
            this.StopTime = StopTime;
            this.TaskName = TaskName;
            this.Hours = Hours;
            this.ProjectId = ProjectId;
        }

        public Task(DateTime StartTime, string TaskName, int Hours, int ProjectId)
        {
            this.id = 0;
            this.WorkerId = null;
            this.StartTime = StartTime;
            this.StopTime = null;
            this.TaskName = TaskName;
            this.Hours = Hours;
            this.ProjectId = ProjectId;
        }
    }
}
