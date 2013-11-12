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

        public int autoIncrementField = 0;

        public User GetUserById(int idUser)
        {
            return Items.Where(u => u.id.Equals(idUser)).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Items.Select(x => x);
        }

        public User CreateUser(string name, string surname, string login, string password)
        {
            name = name.ToLower();
            surname = surname.ToLower();
            var checkProjectExist = Items.Where(t => t.Name.Equals(name) && t.Surname.Equals(surname) && t.Login.Equals(login)).FirstOrDefault();

            if (checkProjectExist == null)
            {
                User newUser = User.CreateUser(autoIncrementField, name, surname, login, password);
                Entities.AddToUsers(newUser);
                Entities.SaveChanges();
                return newUser;
            }
            else
                return checkProjectExist;
        }



    }
}
