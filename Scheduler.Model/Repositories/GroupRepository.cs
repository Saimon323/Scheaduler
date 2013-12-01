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
    public class GroupRepository : BaseRepository<Scheduler.Model.EntityModels.Group, SchedulerEntities>, IGroupRepository, IDisposable
    {
        #region BaseRepository

        public override void Add(Group group)
        {
            Entities.AddToGroups(group);
        }

        public override IQueryable<Group> Items
        {
            get
            {
                return Entities.Groups;
            }
        }

        #endregion
    }
}
