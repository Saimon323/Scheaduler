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
    }
}
