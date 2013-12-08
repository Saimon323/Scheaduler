using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Model.EntityModels;
using System.Data.Spatial;

namespace Scheduler.Model.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Group getGroupById(int id);
        IEnumerable<Group> getGroupByMenagerId(int MenagerId);
        Group getGroupByGroupName(string GroupName);
        void addNewGroups(string Login, string GroupName, DateTime CreationDate);
        void deleteGroup(string GroupName);
        void addRealization(string ProjectName, string GroupName);
        void deleteRealization(string ProjectName, string GroupName);
        bool checkRealization(string ProjectName, string GroupName);
        List<Group> getAllGroupWorkingInProject(string ProjectName);
        List<User> getUserListInGroup(string GroupName);
        User getMenagerById(int MenagerId);
    }
}
