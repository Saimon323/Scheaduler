using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Model.EntityModels
{
    public partial class User : IIdentifable
    {
        public User()
        {
        }

        public User(string Name, string Surname, string Login, string Password, int RoleId)
        {
            this.id = 0;
            this.Name = Name;
            this.Surname = Surname;
            this.Login = Login;
            this.Password = Password;
            this.RoleId = RoleId;
            this.GroupId = null;
        }

        public User(string Name, string Surname, string Login, string Password, int RoleId, int GroupId)
        {
            this.id = 0;
            this.Name = Name;
            this.Surname = Surname;
            this.Login = Login;
            this.Password = Password;
            this.RoleId = RoleId;
            this.GroupId = GroupId;
        }
    }
}
