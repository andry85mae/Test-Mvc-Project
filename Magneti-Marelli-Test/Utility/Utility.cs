using Magneti_Marelli_Test.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Principal;
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
            u.ManagerId = 3;
            u.LastLogon = DateTime.Now;
            u.LastPasswordChange = DateTime.Now;
            u.UserModification = string.Empty;
            u.IsEnable = true;

            users.Add(u);

            User u2 = new User();
            u2.LoginName = "grupporeti\\mariani";
            u2.FirstName = "Alice";
            u2.LastName = "Mariani";
            u2.ManagerId = 3;
            u2.ManagerLabel = "Alberto Sangalli";
            u2.LastLogon = DateTime.Now;
            u2.LastPasswordChange = DateTime.Now;
            u2.UserModification = string.Empty;
            u2.IsEnable = false;

            users.Add(u2);

            User u3 = new User();
            u3.LoginName = "grupporeti\\sangalli";
            u3.FirstName = "Alberto";
            u3.LastName = "Sangalli";
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

        public static Group GetPortalAdministratorGroup() {

            return DirectoryEntryUtility.GetGroupByName(WebConfigurationManager.AppSettings["PortalAdministratorGroup"]);
        }

        public static List<Application> GetAllApplicationByConfig()
        {
            CustomConfigSection amministrator = (CustomConfigSection)WebConfigurationManager.GetSection("CustomConfigSection");
            List<Application> app = new List<Application>();

            //Add local admin groups
            foreach (var l_element in amministrator.ConfigElements.AsEnumerable())
            {
                Application a = new Application();

                a.Groups.AddRange(l_element.Groups.Split(';'));
                a.Name = l_element.Name;
                a.OU = l_element.OU;
                app.Add(a);
            }


            return app;
        }

        public static HttpCookie CreateCurrentUserCookie()
        {

            SiteUser user = DirectoryEntryUtility.GetCurrentSiteUser();
            //create cookie with current user serialized add set expiration to 2h
            string serialized_user = JsonConvert.SerializeObject(user);
            var cookie = new HttpCookie("currentUser", serialized_user)
            {
                Expires = DateTime.Now.AddHours(2)
            };

            return cookie;
        }

        public static string GetCurrentUserNameWithoutDomain()
        {

            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            return wi.Name.Split('\\').Last();
        }

        public static string GetUserWithoutDomain(string userWithDomani)
        {
            return userWithDomani.Split('\\').Last();
        }

        public static SiteUser GetCurrentUser(HttpContextBase ctx)
        {

            HttpCookie ck = ctx.Request.Cookies["currentUser"];
            SiteUser user = null;
            if (ck != null && ck.Expires > DateTime.Now)
            {
                user = JsonConvert.DeserializeObject<SiteUser>(ck.Value);
            }
            else
            {

                var cookie = CreateCurrentUserCookie();
                user = JsonConvert.DeserializeObject<SiteUser>(cookie.Value);
                ctx.Response.Cookies.Add(cookie);
            }

            return user;
        }

        public static bool UserIsActiveByActiveDirectoryValue(int flags)
        {
            return !Convert.ToBoolean(flags & 0x0002);
        }

        public static bool CanPasswordExpired(int flags)
        {
            bool m_Check = true;

            int ADS_UF_DONT_EXPIRE_PASSWD = (int)0x00010000;


            if (Convert.ToBoolean(flags & ADS_UF_DONT_EXPIRE_PASSWD))
            {
                m_Check = false;
            } //end

            return m_Check;
        }

        public static bool CanUserExpired(long DateInTinks)
        {
            //A value of 0 or 0x7FFFFFFFFFFFFFFF (9223372036854775807) indicates that the account never expires.
            if (DateInTinks != 0 && DateInTinks != 9223372036854775807)
                return true;

            return false;
        }

        public static User FromSearchResultToUser(SearchResult user)
        {

            User u = new User();
            u.LastLogon = user.Properties["lastLogonTimestamp"].Count != 0 ? new Nullable<DateTime>(new DateTime((Int64)user.Properties["lastLogonTimestamp"][0])) : null;
            u.LastPasswordChange = user.Properties["pwdLastSet"].Count != 0 ? new Nullable<DateTime>(new DateTime((Int64)user.Properties["pwdLastSet"][0])) : null;
            u.UserModification = string.Empty;

            u.LoginName = user.Properties["SAMAccountName"].Count != 0 ? (string)user.Properties["SAMAccountName"][0] : null;
            u.FirstName = user.Properties["givenName"].Count != 0 ? (string)user.Properties["givenName"][0] : null;
            u.LastName = user.Properties["sn"].Count != 0 ? (string)user.Properties["sn"][0] : null;
            u.DisplayName = user.Properties["displayName"].Count != 0 ? (string)user.Properties["displayName"][0] : null;
            u.Description = user.Properties["description"].Count != 0 ? (string)user.Properties["description"][0] : null;
            u.Mail = user.Properties["mail"].Count != 0 ? (string)user.Properties["mail"][0] : null;

            u.Phone = user.Properties["telephoneNumber"].Count != 0 ? (string)user.Properties["telephoneNumber"][0] : null;

            u.City = user.Properties["city"].Count != 0 ? (string)user.Properties["telephoneNumber"][0] : null;

            u.Country = user.Properties["l"].Count != 0 ? (string)user.Properties["telephoneNumber"][0] : null;

            u.CanPasswordExpire = Utility.CanPasswordExpired((int)user.Properties["userAccountControl"][0]);

            u.CanUserExpire = Utility.CanUserExpired((long)user.Properties["accountExpires"][0]);

            if (u.CanUserExpire == true)
            {

                u.ExpirationDate = user.Properties["accountExpires"].Count != 0 ? new Nullable<DateTime>(new DateTime((long)user.Properties["accountExpires"][0])) : null;

            }
            else
            {
                u.ExpirationDate = DateTime.MaxValue;
            }
            u.ManagerId = 3;
            u.ManagerLabel = "Sangalli Alberto";

            u.BL = user.Properties["postBoxOffice"].Count != 0 ? (string)user.Properties["telephoneNumber"][0] : null;

            u.Site = string.Empty;

            u.IsEnable = Utility.UserIsActiveByActiveDirectoryValue((int)user.Properties["userAccountControl"][0]);

            u.Groups = new List<Group>();
            if (user.Properties["memberOf"].Count > 0)
            {

                
                foreach (string g in user.Properties["memberOf"])
                {

                    u.Groups.Add(DirectoryEntryUtility.GetGroupByDistinguishedName(g));
                }

            }
            

            return u;
        }

        public static Group FromSearchResultToGroup(SearchResult group)
        {
            Group g = new Group();

            g.Name = group.Properties["SAMAccountName"].Count != 0 ? (string)group.Properties["SAMAccountName"][0] : null;
            g.DistinguishedName= group.Properties["distinguishedName"].Count != 0 ? (string)group.Properties["distinguishedName"][0] : null;

            return g;
        }
    }
}