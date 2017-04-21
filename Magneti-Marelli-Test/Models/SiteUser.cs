using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Magneti_Marelli_Test.Models
{
    public class SiteUser
    {

        public SiteUser() {
            this.Role = PortalRole.Manager;
            this.Groups = new List<string>();
            this.Applications = new List<Application>();
        }
        public string LoginName { get; set; }

        public string DisplayName { get; set; }

        public string Mail { get; set; }

        public PortalRole Role { get; set; }

        public List<string> Groups { get; set; }

        public List<Application> Applications { get; set; }
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