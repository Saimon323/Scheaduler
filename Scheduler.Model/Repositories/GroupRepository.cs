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

        UserRepository UserRepo = new UserRepository();
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

        public Group getGroupByGroupName(string GroupName)
        {
            return Items.Where(x => x.GroupName.Equals(GroupName)).FirstOrDefault();
        }

        public void addNewGroups(string Login, string GroupName, DateTime CreationDate)
        {
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
    }
}
