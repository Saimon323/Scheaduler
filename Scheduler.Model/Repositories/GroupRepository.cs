using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
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

        //IUserRepository UserRepo = new UserRepository();
        //IProjectRepository ProjectRepo = new ProjectRepository();
        string RoleName = "Menager";
        int autoIncrementId = 0;

        public Group getGroupById(int id)
        {
            return Items.Where(x => x.id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Group> getGroupByMenagerId(int MenagerId)
        {
            return Items.Where(x => x.MenagerId.Equals(MenagerId));
        }

        public List<Group> getAllGroupWorkingInProject(string ProjectName)
        {
            IProjectRepository ProjectRepo = new ProjectRepository();

            Project projectExist = ProjectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return null;

            IEnumerable<ProjectsToGroupsRealization> realizationsList = Entities.ProjectsToGroupsRealizations.Where(x => x.ProjectId.Equals(projectExist.id));
            
            if(realizationsList == null)
                return null;
            
            List<Group> groupList = new List<Group>();

            foreach(var real in realizationsList)
            {
                Group group = getGroupById(real.GroupId);
                groupList.Add(group);
            }

            return groupList;
        }

        public Group getGroupByGroupName(string GroupName)
        {
            return Items.Where(x => x.GroupName.Equals(GroupName)).FirstOrDefault();
        }

        public void addNewGroups(string Login, string GroupName, DateTime CreationDate)
        {
            IUserRepository UserRepo = new UserRepository();
            Group existGroup = getGroupByGroupName(GroupName);
            if (existGroup != null)
                return;

            User user = UserRepo.getMenagerByLogin(Login, RoleName);
            if (user == null)
                return;

            Group group = Group.CreateGroup(autoIncrementId, user.id, GroupName, CreationDate);
            Entities.AddToGroups(group);
            Entities.SaveChanges();
        }

        public void deleteGroup(string GroupName)
        {
            Group groupExist = getGroupByGroupName(GroupName);
            if (groupExist == null)
                return;

            Entities.DeleteObject(groupExist);
            Entities.SaveChanges();
        }

        public void addRealization(string ProjectName, string GroupName)
        {
            IProjectRepository ProjectRepo = new ProjectRepository();

            Project projectExist = ProjectRepo.getProjectByName(ProjectName);
            
            if (projectExist == null)
                return;

            Group groupExist = getGroupByGroupName(GroupName);

            if (groupExist == null)
                return;

            ProjectsToGroupsRealization realization = Entities.ProjectsToGroupsRealizations.Where(x => x.GroupId.Equals(groupExist.id) && x.ProjectId.Equals(projectExist.id)).FirstOrDefault();
            if (realization != null)
                return;

            realization = ProjectsToGroupsRealization.CreateProjectsToGroupsRealization(projectExist.id, groupExist.id, autoIncrementId);
            Entities.AddToProjectsToGroupsRealizations(realization);
            Entities.SaveChanges();
        }

        public void deleteRealization(string ProjectName, string GroupName)
        {
            IProjectRepository ProjectRepo = new ProjectRepository();

            Project projectExist = ProjectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return;

            Group groupExist = getGroupByGroupName(GroupName);

            if (groupExist == null)
                return;

            ProjectsToGroupsRealization realization = Entities.ProjectsToGroupsRealizations.Where(x => x.GroupId.Equals(groupExist.id) && x.ProjectId.Equals(projectExist.id)).FirstOrDefault();
            if (realization == null)
                return;

            Entities.DeleteObject(realization);
            Entities.SaveChanges();
        }

        public bool checkRealization(string ProjectName, string GroupName)
        {
            IProjectRepository ProjectRepo = new ProjectRepository();

            Project projectExist = ProjectRepo.getProjectByName(ProjectName);

            if (projectExist == null)
                return false;

            Group groupExist = getGroupByGroupName(GroupName);

            if (groupExist == null)
                return false;

            ProjectsToGroupsRealization realization = Entities.ProjectsToGroupsRealizations.Where(x => x.GroupId.Equals(groupExist.id) && x.ProjectId.Equals(projectExist.id)).FirstOrDefault();
            if (realization == null)
                return false;
            else
                return true;


        }

        public List<User> getUserListInGroup(string GroupName)
        {
            IUserRepository UserRepo = new UserRepository();
            Group groupExist = getGroupByGroupName(GroupName);

            if (groupExist == null)
                return null;

            List<User> userList = UserRepo.getUserListByGroupId(groupExist.id);

            if (userList == null)
                return null;
            else
                return userList;
        }

        public User getMenagerById(int MenagerId)
        {
            IUserRepository UserRepo = new UserRepository();
            User menager = UserRepo.getUserById(MenagerId);
            return menager;
        }

        public IEnumerable<Group> getAllGroup()
        {
            var allGroups = Items.ToList();
            IEnumerable<Group> groups = allGroups;

            return groups;
        } 
    }
}
