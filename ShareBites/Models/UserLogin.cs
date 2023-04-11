using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareBites.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            Helpers = new HashSet<Helper>();
            MessageFromUsers = new HashSet<Message>();
            MessageToUsers = new HashSet<Message>();
            Restaurants = new HashSet<Restaurant>();
            Shelters = new HashSet<Shelter>();
            Sponsors = new HashSet<Sponsor>();
        }

        public int LoginId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        [Remote("CheckId", "Login", AdditionalFields = "Username")]
        public string Username { get; set; } = null!;

        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; } = null!;
        public string UserTypeId { get; set; } = null!;
        public int? IsVerified { get; set; }
        
        public virtual UserTypeMaster UserType { get; set; } //= null!;
        public virtual ICollection<Helper> Helpers { get; set; }
        public virtual ICollection<Message> MessageFromUsers { get; set; }
        public virtual ICollection<Message> MessageToUsers { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }
        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
