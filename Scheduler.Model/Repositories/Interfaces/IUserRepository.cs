using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.EntityModels;

namespace Scheduler.Model.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(int idUser);
        IEnumerable<User> GetAllUsers();
        User CreateUser(string name, string surname, string login, string password);
    }
}
