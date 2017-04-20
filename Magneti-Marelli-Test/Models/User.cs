﻿using System;
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

        public List<Groups> UserGroups { get; set; }
    }
}