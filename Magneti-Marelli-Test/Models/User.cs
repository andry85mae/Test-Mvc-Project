using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magneti_Marelli_Test.Models
{
    public class User
    {

        public User()
        {
            this.UserGroups = new List<Groups>();

        }

        [Display(Name = "Last LogOn")]
        public DateTime LastLogon { get; set; }

        [Display(Name = "Last Password Change")]
        public DateTime LastPasswordChange { get; set; }

        public string UserModification { get; set; }

        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        public string LoginName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Mail { get; set; }

        public int Phone { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Manager")]
        public int ManagerId { get; set; }

        public string ManagerLabel { get; set; }

        public string BL { get; set; }

        public string Site { get; set; }

        public bool IsEnable { get; set; }

        public PortalRole Role
        {
            get
            {
                string portalAdministratorGroup = Utility.Utility.GetPortalAdministratorGroupName();
                string localAdministratorGroups = Utility.Utility.GetLocalAdministratorGroupName();

                if (this.UserGroups.FirstOrDefault(x => x.Name == portalAdministratorGroup) != null)
                    return PortalRole.PortalAdministrators;

                if (OUList.Count > 0)
                    return PortalRole.LocalAdmnistrator;

                return PortalRole.Manager;
            }
        }

        public List<string> OUList
        {
            get
            {

                List<string> OU = new List<string>();
                List<LookupValue<string, string>> localGroups = Utility.Utility.GetLocalAdministratorsGroups();

                string portalAdministratorGroup = Utility.Utility.GetPortalAdministratorGroupName();
                if ((this.UserGroups.FirstOrDefault(x => x.Name == portalAdministratorGroup) != null))
                {
                    foreach (LookupValue<string, string> lg in localGroups)
                    {
                        OU.Add(lg.Value);

                    }
                    
                    return OU;
                }

                foreach (LookupValue<string, string> lg in localGroups)
                {
                    if (this.UserGroups.FirstOrDefault(ug => ug.Name == lg.Key) != null)
                        OU.Add(lg.Value);

                }

                return OU;
            }
        }
        public List<Groups> UserGroups { get; set; }
    }


    public enum PortalRole
    {
        [Display(Name = "Portal Adminsitrator")]
        PortalAdministrators = 1,
        [Display(Name = "Local Adminsitrator")]
        LocalAdmnistrator = 2,
        [Display(Name = "Manager")]
        Manager = 3

    }

}