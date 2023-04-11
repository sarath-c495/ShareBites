using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShareBites.Models
{
    public partial class Sponsor
    {
        public Sponsor()
        {
            SponsoredFoods = new HashSet<SponsoredFood>();
        }

        public int SponsorId { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string? Name { get; set; }
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression(@"^\+?\d{0,2}\-?\d{3}\-?\d{3}\-?\d{4}$", ErrorMessage = "Invalid phone number")]
        public long? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? EmailId { get; set; }
        //[DisplayName("Mode of Help")]
        //public int? ModeOfHelp { get; set; }
        [Required]
        public string? RegionId { get; set; }
        [Required(ErrorMessage = "Please enter the address")]
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        [DisplayName("Mode of Help")]
        [Required]
        public string? ModeOfHelp1 { get; set; }
        public int? LoginId { get; set; }

        public virtual UserLogin? Login { get; set; }
        public virtual ModeOfhelpMaster? ModeOfHelp1Navigation { get; set; }
        public virtual RegionMaster? Region { get; set; }
        public virtual ICollection<SponsoredFood> SponsoredFoods { get; set; }
    }
}
