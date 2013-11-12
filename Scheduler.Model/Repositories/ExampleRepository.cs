using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Model.EntityModels;


namespace Scheduler.Model.Repositories
{
    public class ExampleRepository : BaseRepository<Scheduler.Model.EntityModels.User, ProjectsEntities>, IExampleRepository, IDisposable
    {
        #region BaseRepository

        public override void Add(User tag)
        {
            
            Entities.AddToUsers(User);
        }

        public override IQueryable<User> Items
        {
            get
            {
                return Entities.Users;
            }
        }

        #endregion
    }
}
