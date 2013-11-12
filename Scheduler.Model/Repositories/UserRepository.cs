using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Model.EntityModels;

namespace Scheduler.Model.Repositories
{
  
    public class UserRepository : BaseRepository<Scheduler.Model.EntityModels.User, ProjectsEntities>, IUserRepository, IDisposable
    {
        #region BaseRepository

        public override void Add(User user)
        {
            Entities.AddToUsers(user);
        }

        public override IQueryable<User> Items
        {
            get
            {
                return Entities.Users;
            }
        }

        #endregion

        public User GetUserById(int idUser)
        {
            return Items.Where(u => u.id.Equals(idUser)).FirstOrDefault();
        }

    }
}
