using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareBites.Models
{
    public partial class Helper
    {
        public Helper()
        {
            ExcessFoodOrders = new HashSet<ExcessFoodOrder>();
        }

        public int HelperId { get; set; }

        [Required(ErrorMessage ="Please enter your name")]
        public string? Name { get; set; }
        [Required]
        public string? RegionId { get; set; }
        [Required(ErrorMessage = "Please enter the adrress")]
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        [Required(ErrorMessage = "Please enter the postalcode")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Postal Code")]
        public string? ZipCode { get; set; }
        [Required]
        [RegularExpression(@"^\+?\d{0,2}\-?\d{3}\-?\d{3}\-?\d{4}$", ErrorMessage = "Invalid phone number")]
        //[Phone(ErrorMessage = "Please enter a valid phone number.")]
        public long? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? EmailId { get; set; }
        public bool Notifications { get; set; }
        [Required]
        public string? ModeOfHelp { get; set; }
        public int? LoginId { get; set; }

        public virtual UserLogin? Login { get; set; }
        public virtual ModeOfhelpMaster? ModeOfHelpNavigation { get; set; }
        public virtual RegionMaster? Region { get; set; }
        public virtual ICollection<ExcessFoodOrder> ExcessFoodOrders { get; set; }
    }
}
