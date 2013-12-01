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
    public class UserRepository : BaseRepository<Scheduler.Model.EntityModels.User, SchedulerEntities>, IUserRepository, IDisposable
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

        int autoIncrementId = 0;

        public User getUserById(int id)
        {
            return Items.Where(x => x.id.Equals(id)).FirstOrDefault();
        }

        public User getUserByLogin(string Login)
        {
            return Items.Where(x => x.Login.Equals(Login)).FirstOrDefault();
        }

        public User getMenagerByLogin(string Login, string RolaName)
        {
            Role existRole = getRoleByName(RolaName);
            if (existRole == null)
                return null;


            User existUser = Items.Where(x => x.Login.Equals(Login) && x.RoleId.Equals(existRole.id)).FirstOrDefault();

            if (existUser == null)
                return null;
            else
                return existUser;

        }

        public void addNewUser(string Name, string Surname, string Login, string Password, string Role)
        {
            User existUser = getUserByLogin(Login);
            if (existUser != null)
                return;

            Role existRole = getRoleByName(Role);
            if (existRole == null)
                return;

            User user = User.CreateUser(autoIncrementId, Name, Surname, Login, Password, existRole.id);
            Entities.AddToUsers(user);
            Entities.SaveChanges();
        }

        public void addNewUser(string Name, string Surname, string Login, string Password, string Role, int GroupId)
        {
            User existUser = getUserByLogin(Login);
            if (existUser != null)
                return;

            Role existRole = getRoleByName(Role);
            if (existRole != null)
                return;

            User user = new User(Name, Surname, Login, Password, existRole.id, GroupId);

            Entities.AddToUsers(user);
            Entities.SaveChanges();
        }

        public Role getRoleById(int id)
        {
            return Entities.Roles.Where(x => x.id.Equals(id)).FirstOrDefault();
        }

        public Role getRoleByName(string Name)
        {
            return Entities.Roles.Where(x => x.Name.Equals(Name)).FirstOrDefault();
        }

        public void addNewRole(string Name)
        {
            Role existRole = getRoleByName(Name);

            if (existRole != null)
                return;

            Role role = Role.CreateRole(autoIncrementId, Name);
            Entities.AddToRoles(role);
            Entities.SaveChanges();


        }
    }
}
