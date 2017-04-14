using System;
using System.Collections.Generic;
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


        public int UserId { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        
        public string DisplayName { get; set; }

        public string Description { get; set; }
        
        public string Mail { get; set; }

        public int Phone { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime ExpirationDate { get; set; }

        public User Manager { get; set; }

        public string BL { get; set; }

        public string Site { get; set; }

        public List<Groups> UserGroups { get; set; }
    }
}