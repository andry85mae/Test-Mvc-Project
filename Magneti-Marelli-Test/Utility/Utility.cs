using Magneti_Marelli_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Magneti_Marelli_Test.Utility
{
    public static class Utility
    {
        public static List<User> users = new List<User>();

        static Utility()
        {

            User u = new User();
            u.LoginName = "grupporeti\\maestan1";
            u.FirstName = "Andrea";
            u.LastName = "Maestroni";
            u.ManagerLabel = "Alberto Sangalli";
            u.UserId = 1;
            u.ManagerId = 3;
            u.LastLogon = DateTime.Now;
            u.LastPasswordChange = DateTime.Now;
            u.UserModification = string.Empty;
            u.IsEnable = true;

            Groups group1 = new Groups() { Id = 1, Name = "Groups1" };
            u.UserGroups.Add(group1);
            users.Add(u);

            User u2 = new User();
            u2.LoginName = "grupporeti\\mariani";
            u2.FirstName = "Alice";
            u2.LastName = "Mariani";
            u2.ManagerId = 3;
            u2.ManagerLabel = "Alberto Sangalli";
            u2.UserId = 2;
            u2.LastLogon = DateTime.Now;
            u2.LastPasswordChange = DateTime.Now;
            u2.UserModification = string.Empty;
            u2.IsEnable = false;

            users.Add(u2);

            User u3 = new User();
            u3.LoginName = "grupporeti\\sangalli";
            u3.FirstName = "Alberto";
            u3.LastName = "Sangalli";
            u3.UserId = 3;
            u3.LastLogon = DateTime.Now;
            u3.LastPasswordChange = DateTime.Now;
            u3.UserModification = string.Empty;
            u3.IsEnable = true;
        }

        public static List<User> TestUserFactory()
        {

            User u2 = new User();
            u2.LoginName = "grupporeti\\mariani";
            u2.FirstName = "Alice";
            u2.LastName = "Mariani";
            u2.UserId = 2;

            for (var i = 0; i < 10; i++)
            {

                users.Add(u2);
            }


            return users;
        }


        public static string GetPortalAdministratorGroupName()
        {
            return WebConfigurationManager.AppSettings["PortalAdministratorGroup"];
        }

        public static string GetLocalAdministratorGroupName()
        {
            return WebConfigurationManager.AppSettings["LocalAdministratorGroup"];
        }

        public static List<LookupValue<string, string>> GetLocalAdministratorsGroups()
        {
            CustomConfigSection amministrator = (CustomConfigSection)WebConfigurationManager.GetSection("CustomConfigSection");
            List<LookupValue<string, string>> groups = new List<LookupValue<string, string>>();

            foreach (var l_element in amministrator.ConfigElements.AsEnumerable())
            {
                groups.Add(new LookupValue<string, string>(l_element.Name,l_element.OU));
            }


            return groups;
        }
    }
}