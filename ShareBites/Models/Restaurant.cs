using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareBites.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            ResFoodHandlers = new HashSet<ResFoodHandler>();
        }

        public int ResId { get; set; }
        [Required(ErrorMessage = "Please enter your Restaurants name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? RegionId { get; set; }
        [Required(ErrorMessage = "Please enter the address")]
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public TimeSpan? WaitTime { get; set; }
        [Required]
        [RegularExpression(@"^\+?\d{0,2}\-?\d{3}\-?\d{3}\-?\d{4}$", ErrorMessage = "Invalid phone number")]
        public long? PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? EmailId { get; set; }
        [Required]
        public string? ModeOfHelp { get; set; }
        public int? LoginId { get; set; }

        public virtual UserLogin? Login { get; set; }
        public virtual ModeOfhelpMaster? ModeOfHelpNavigation { get; set; }
        public virtual RegionMaster? Region { get; set; }
        public virtual ICollection<ResFoodHandler> ResFoodHandlers { get; set; }
    }
}
