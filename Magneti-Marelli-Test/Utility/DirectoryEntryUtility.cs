using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Principal;
using Magneti_Marelli_Test.Models;

namespace Magneti_Marelli_Test.Utility
{
    public static class DirectoryEntryUtility
    {

        public static List<User> GetUserByQuery(HttpContextBase ctx, string qry)
        {
            List<User> users = new List<User>();

            SiteUser currentUser = Utility.GetCurrentUser(ctx);

            foreach (Application app in currentUser.Applications)
            {

                using (DirectoryEntry de = new DirectoryEntry("LDAP://" + app.OU))
                {
                    using (DirectorySearcher userSearcher = new DirectorySearcher(de))
                    {
                        userSearcher.Filter = "(objectClass=user)";
                        userSearcher.PropertiesToLoad.Add("SAMAccountName");
                        userSearcher.PropertiesToLoad.Add("lastLogonTimestamp");
                        userSearcher.PropertiesToLoad.Add("pwdLastSet");
                        userSearcher.PropertiesToLoad.Add("givenName");
                        userSearcher.PropertiesToLoad.Add("sn");
                        userSearcher.PropertiesToLoad.Add("displayName");
                        userSearcher.PropertiesToLoad.Add("description");
                        userSearcher.PropertiesToLoad.Add("description");
                        userSearcher.PropertiesToLoad.Add("mail");
                        userSearcher.PropertiesToLoad.Add("telephoneNumber");
                        userSearcher.PropertiesToLoad.Add("city");
                        userSearcher.PropertiesToLoad.Add("l");
                        userSearcher.PropertiesToLoad.Add("accountExpires");
                        userSearcher.PropertiesToLoad.Add("postBoxOffice");
                        userSearcher.PropertiesToLoad.Add("userAccountControl");

                        foreach (SearchResult user in userSearcher.FindAll())
                        {
                            users.Add(Utility.FromSearchResultToUser(user));

                        }
                    }
                }
            }

            return users;
            
        }

        public static bool isUserInGroup(string group, string userName)
        {
            if (userName == "")
            {
                return false;
            }

            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://DC=DEV,DC=DOM");
                DirectorySearcher mySearcher = new DirectorySearcher(entry);
                mySearcher.Filter = "(&(objectClass=user)(|(cn=" + userName + ")(sAMAccountName=" + userName + ")))";
                SearchResult result = mySearcher.FindOne();

                foreach (string GroupPath in result.Properties["memberOf"])
                {
                    if (GroupPath.Contains(group))
                    {
                        return true;
                    }
                }
            }
            catch (DirectoryServicesCOMException)
            {
            }

            return false;
        }

        public static bool isPortalAdministrator(string username)
        {
            return isUserInGroup(Utility.GetPortalAdministratorGroupName(), username);
        }

        public static SiteUser GetCurrentSiteUser()
        {

            string username = Utility.GetCurrentUserNameWIthoutDomain();
            SiteUser u = new SiteUser();
            using (DirectoryEntry de = new DirectoryEntry("LDAP://DC=DEV,DC=DOM"))
            {
                using (DirectorySearcher adSearch = new DirectorySearcher(de))
                {
                    adSearch.Filter = "(&(objectCategory=user)(objectClass=user)(samaccountName=" + username + "))";

                    adSearch.PropertiesToLoad.Add("memberOf");
                    adSearch.PropertiesToLoad.Add("givenName");
                    adSearch.PropertiesToLoad.Add("SAMAccountName");
                    adSearch.PropertiesToLoad.Add("mail");

                    StringBuilder groupNames = new StringBuilder(); //stuff them in | delimited

                    SearchResult result = adSearch.FindOne();

                    u.DisplayName = (string)result.Properties["givenName"][0];
                    u.LoginName = (string)result.Properties["SAMAccountName"][0];
                    u.Mail = (string)result.Properties["mail"][0];
                    u.Applications = new List<Application>();



                    if (isPortalAdministrator(username) == true)
                    {
                        //if portal admin access all application defined in web.config
                        //and add PortalAdministrator to the groups
                        u.Applications = Utility.GetAllApplicationByConfig();
                        u.Groups.Add(Utility.GetPortalAdministratorGroupName());

                        //check all other groups where the user is present
                        foreach (Application a in u.Applications)
                        {
                            foreach (string g in a.Groups)
                            {
                                if (isUserInGroup(g, username) == true)
                                {
                                    u.Groups.Add(g);
                                }

                            }
                        }
                        u.Role = PortalRole.PortalAdministrators;
                    }
                    else
                    {
                        // if is not portal admin check if is member of groups for local admin
                        foreach (Application a in Utility.GetAllApplicationByConfig())
                        {
                            //check if application is already inserted
                            //if already present don't add
                            if (u.Applications.FirstOrDefault(app => app.Name == a.Name) == null)
                            {
                                foreach (string g in a.Groups)
                                {
                                    if (isUserInGroup(g, username) == true)
                                    {
                                        //if is present add application and set role to LocalAdmin
                                        u.Groups.Add(g);
                                        u.Applications.Add(a);
                                        u.Role = PortalRole.LocalAdmnistrator;
                                    }

                                }
                            }

                        }
                    }

                }
            }


            return u;
        }

        public static List<string> GetUserGroups(string username)
        {
            return new List<string>();
        }
    }
}