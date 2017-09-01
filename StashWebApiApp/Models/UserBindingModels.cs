using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Stash.Services.RequestModels.Users;

namespace StashWebApiApp.Models
{
    public class CreateUserBindingModel
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Phone number")]
        public string phone_number { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string full_name { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string MetaData { get; set; }

    }
}