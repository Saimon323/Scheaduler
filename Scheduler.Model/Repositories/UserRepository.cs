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

        public Message getMessagesById(int id)
        {
            return Entities.Messages.Where(x => x.id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Message> getAllMessageToUser(string Login)
        {
            User userExist = getUserByLogin(Login);
            if (userExist == null)
                return null;

            return Entities.Messages.Where(x => x.ToUserId.Equals(userExist.id));
        }

        public IEnumerable<Message> getAllMessageFromUser(string Login)
        {
            User userExist = getUserByLogin(Login);
            if (userExist == null)
                return null;

            return Entities.Messages.Where(x => x.FromUserId.Equals(userExist.id));
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

        public void addNewUser(string Name, string Surname, string Login, string Password, string Role, string GroupName)
        {
            User existUser = getUserByLogin(Login);
            if (existUser != null)
                return;

            Role existRole = getRoleByName(Role);
            if (existRole == null)
                return;

            IGroupRepository groupRpository = new GroupRepository();
            Group groupExist = groupRpository.getGroupByGroupName(GroupName);
            
            if (groupExist == null)
                return;

            User user = new User(Name, Surname, Login, Password, existRole.id, groupExist.id);

            Entities.AddToUsers(user);
            Entities.SaveChanges();
        }

        public void addUserToGroup(string Login, string GroupName)
        {
            User userexist = getUserByLogin(Login);

            if (userexist == null)
                return;

            Role role = getRoleByName("Worker");
            if (role.id != userexist.RoleId)
                return;

            GroupRepository grouprepo = new GroupRepository();

            Group groupexist = grouprepo.getGroupByGroupName(GroupName);

            if (groupexist == null)
                return;

            if (userexist.id == groupexist.MenagerId)
                return;

            userexist.GroupId = groupexist.id;
            Entities.SaveChanges();
        }

        public void addMassage(string ToUserLogin, string FromUserLogin, string Title, string Text)
        {
            User fromUser = getUserByLogin(FromUserLogin);
            if (fromUser == null)
                return;

            User toUser = getUserByLogin(ToUserLogin);

            if (toUser == null)
                return;

            DateTime today = DateTime.UtcNow;
            Message message = Message.CreateMessage(autoIncrementId, toUser.id, today, Title, Text, fromUser.id);
            Entities.AddToMessages(message);
            Entities.SaveChanges();
        }

        public void deleteUserFromGroup(string Login)
        {
            User userexist = getUserByLogin(Login);

            if (userexist == null)
                return;

            userexist.GroupId = null;
            Entities.SaveChanges();
        }

        public void deleteUser(string Login)
        {
            User userexist = getUserByLogin(Login);

            if (userexist == null)
                return;

            IEnumerable<Message> toUser = getAllMessageToUser(userexist.Login);
            IEnumerable<Message> fromUser = getAllMessageFromUser(userexist.Login);

            foreach (var to in toUser)
            {
                Entities.DeleteObject(to);
            }

            foreach (var from in toUser)
            {
                Entities.DeleteObject(from);
            }

            Entities.DeleteObject(userexist);
            Entities.SaveChanges();

        }

        public void deleteMessages(int id)
        {
            Message messageexist = getMessagesById(id);
            if (messageexist == null)
                return;

            Entities.DeleteObject(messageexist);
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
