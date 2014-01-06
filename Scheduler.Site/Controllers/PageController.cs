using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Http.Hosting;
using System.Web.Mvc;
using Scheduler.Model.EntityModels;
using Scheduler.Model.Repositories;
using Scheduler.Model.Repositories.Interfaces;
using Scheduler.Site.Models;

namespace Scheduler.Site.Controllers
{
    public class PageController : Controller
    {
        public ActionResult HomePage()
        {
           /* var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            User user = userRepo.getUserByLogin(userLogin);
            Role role = userRepo.getRoleByName("Worker");

            if (user.GroupId == null && user.RoleId == role.id)
            {
                return RedirectToAction("JoinToGroup", "Page");
            }
            
            return View();*/
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            User user = userRepo.getUserByLogin(userLogin);
            Role role = userRepo.getRoleById(user.RoleId);
            if (role.Name == "Owner")
            {
               return RedirectToAction("HomePageOwner", "Owner");
            
            } else if (role.Name == "Menager")
            {
                return RedirectToAction("HomePageMenager", "Menager");
            }

           /* if (user.GroupId == null && user.RoleId == role.id)
            {
                return RedirectToAction("JoinToGroup", "Page");
            }
            */
            return View();
        }

     /*   public ActionResult HomePageOwner()
        {
            var cookie = Request.Cookies["LogOn"];
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            User userExist = userRepo.getUserByLogin(userLogin);
            IProjectRepository projectRepo = new ProjectRepository();
            IEnumerable<Project> projectList = projectRepo.getAllProjectByIdOwner(userExist.id);
            return View("HomePageOwner", projectList);
        }*/

        public ActionResult JoinToGroup()
        {
            IGroupRepository groupRepo = new GroupRepository();
            IUserRepository userRepo = new UserRepository();
            IEnumerable<Group> groupList = groupRepo.getAllGroup();
            List<GroupList> groupLists = new List<GroupList>();
            User user;
            GroupList singleGroup;

            foreach (var group in groupList)
            {
                user = userRepo.getUserById(group.MenagerId);
                singleGroup = new GroupList
                {
                    id = group.id,
                    Date = group.CreationData,
                    Name = user.Name,
                    GroupName = group.GroupName,
                    Login = user.Login,
                    Surname = user.Surname

                };
                groupLists.Add(singleGroup);
            }

            return View(groupLists);
        }

        public ActionResult GroupDetails(int id, string Name, string Surname, string Login, string GroupName, DateTime Date)
        {
            IGroupRepository groupRepo = new GroupRepository();
            IUserRepository userRepo = new UserRepository();
            IEnumerable<User> groupMemberList = userRepo.getAllMemberGroup(id);
            List<User> memberList = new List<User>();
            IEnumerable<User> asd = groupMemberList.Where(x => x.Login.Equals("Asad"));
            foreach (User x in groupMemberList)
            {
                memberList.Add(x);
            }
            GroupDetail groupDetail = new GroupDetail
            {
                Date = Date,
                GroupName = GroupName,
                id = id,
                Login = Login,
                MemberGroupList = memberList,
                Name = Name,
                Surname = Surname
            };

            return View("GroupDetails", groupDetail);
        }

        public ActionResult AddUserToGroup(int idGroup)
        {
            var cookie = Request.Cookies["LogOn"]; 
            string userLogin = cookie.Value;
            IUserRepository userRepo = new UserRepository();
            IGroupRepository groupRepo = new GroupRepository();
            User userExist = userRepo.getUserByLogin(userLogin);
            Group groupExist = groupRepo.getGroupById(idGroup);
            userRepo.addUserToGroup(userExist.Login,groupExist.GroupName);
            return View("HomePage");

        }




    }
}
