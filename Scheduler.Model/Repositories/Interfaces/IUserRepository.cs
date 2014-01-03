using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.EntityModels;
using System.Data.Spatial;

namespace Scheduler.Model.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User getUserById(int id);

        User getUserByLogin(string Login);

        User getMenagerByLogin(string Login, string RolaName);

        User getOwnerByLogin(string Login, string RolaName);

        Message getMessagesById(int id);

        IEnumerable<Message> getAllMessageToUser(string Login);

        IEnumerable<Message> getAllMessageFromUser(string Login);

        void addNewUser(string Name, string Surname, string Login, string Password, string Role);

        void addNewUser(string Name, string Surname, string Login, string Password, string Role, string GroupName);

        void addUserToGroup(string Login, string GroupName);

        void addMassage(string ToUserLogin, string FromUserLogin, string Title, string Text);

        void deleteUserFromGroup(string Login);

        void deleteUser(string Login);

        void deleteMessages(int id);

        Role getRoleById(int id);

        Role getRoleByName(string Name);

        void addNewRole(string Name);

        User getUserByGroupId(int idGroup);

        List<User> getUserListByGroupId(int idGroup);

        bool LogIn(string login, string password);

        IEnumerable<Role> getAllRole();

        IEnumerable<User> getAllMemberGroup(int idGroup);
    }
}
