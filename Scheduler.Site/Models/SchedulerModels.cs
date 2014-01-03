﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using Scheduler.Model.EntityModels;

namespace Scheduler.Site.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }


    public class RegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public SelectList RoleSelect { get; set; }

    }

    public class RoleList
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public RoleList(int RoleId, string RoleName)
        {
            this.RoleId = RoleId;
            this.RoleName = RoleName;
        }
    }

    public class GroupList
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string GroupName { get; set; }
        public DateTime Date { get; set; }
    }

    public class GroupDetail : GroupList
    {
        public List<User> MemberGroupList;

        public GroupDetail()
        {
            MemberGroupList = new List<User>();
        }
    }
}