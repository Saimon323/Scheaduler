using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Model.EntityModels
{
    public partial class Project :IIdentifable
    {
        public Project() { }
        public Project(string ProjectName, float Budget, DateTime StartTime, DateTime StopTime, int OwnerId)
        {
            this.id = 0;
            this.ProjectName = ProjectName;
            this.Budget = Budget;
            this.StartTime = StartTime;
            this.StopTime = StopTime;
            this.OwnerId = OwnerId;
        }

        public Project(string ProjectName, float Budget, DateTime StartTime, int OwnerId)
        {
            this.id = 0;
            this.ProjectName = ProjectName;
            this.Budget = Budget;
            this.StartTime = StartTime;
            this.OwnerId = OwnerId;
        }
    }
}
