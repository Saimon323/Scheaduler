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
        ProjectRepository ProjectRepo = new ProjectRepository();

        public User GetUserById(int idUser)
        {
            return Items.Where(u => u.id.Equals(idUser)).FirstOrDefault();
        }

        public User GetUserByLogin(string login)
        {
            return Items.Where(x => x.Login.Equals(login)).FirstOrDefault();
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

        public Group GetGroupById(int idGroup)
        {
            return Entities.Groups.Where(x => x.id.Equals(idGroup)).FirstOrDefault();
        }

        public Group GetGroupByName(string groupName)
        {
            return Entities.Groups.Where(x => x.Name.Equals(groupName)).FirstOrDefault();
        }

        public Group GetGroupAndProject(string groupName, string projectName)
        {
            Project proj = ProjectRepo.GetProjectByName(projectName);
            return Entities.Groups.Where(x => x.Name.Equals(groupName) && x.ProjectId.Equals(proj.id)).FirstOrDefault();
        }

        public void CreateGroup(string groupName, string projectName)
        {
            groupName = groupName.ToLower();
            projectName = projectName.ToLower();
            
            var checkProjectExist = ProjectRepo.GetProjectByName(projectName);

            if(checkProjectExist == null)
                return;

            //var checkGroupExist = Items.Where(t => t.Name.Equals(groupName)).FirstOrDefault();
            Group checkExistRecord = GetGroupAndProject(groupName, projectName);

            if (checkExistRecord != null)
                return;

            Group newGroup = Group.CreateGroup(autoIncrementField, groupName, checkProjectExist.id);
            Entities.AddToGroups(newGroup);
            Entities.SaveChanges();

        }

        public void AddUserToGroup(string login, string groupName)
        {
            User user = GetUserByLogin(login);
            Group group = GetGroupByName(groupName);

            if (user == null || group == null)
                return;

            UsersGroup newUserInGroup = UsersGroup.CreateUsersGroup(autoIncrementField, user.id, group.id);
            Entities.AddToUsersGroups(newUserInGroup);
            Entities.SaveChanges();
        }



    }
}
