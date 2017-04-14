using Magneti_Marelli_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Magneti_Marelli_Test.Utility
{
    public static class Utility
    {
        public static List<User> users = new List<User>();

        static Utility() {

            User u = new User();
            u.FirstName = "Andrea";
            u.LastName = "Maestroni";
            u.UserId = 1;

            Groups group1 = new Groups() { Id = 1, Name = "Groups1" };
            u.UserGroups.Add(group1);
            users.Add(u);

            User u2 = new User();
            u2.FirstName = "Alice";
            u2.LastName = "Mariani";
            u2.UserId = 2;

            users.Add(u2);
        }
        public static List<User> TestUserFactory() {


            return users;
        }
    }
}