using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareBites.Models
{
    public partial class Shelter
    {
        public Shelter()
        {
            ExcessFoodOrders = new HashSet<ExcessFoodOrder>();
            SponsoredFoods = new HashSet<SponsoredFood>();
        }

        public int ShelterId { get; set; }
        [Required(ErrorMessage = "Please enter your shelter name")]
        public string? ShelterName { get; set; }
        [Required]
        public string? RegionId { get; set; }
        [Required]
        public bool HasCar { get; set; }
        [Required(ErrorMessage = "Please enter the address")]
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Postal Code")]
        public string? ZipCode { get; set; }
        [Required]
        [RegularExpression(@"^\+?\d{0,2}\-?\d{3}\-?\d{3}\-?\d{4}$", ErrorMessage = "Invalid phone number")]
        public long? PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? EmailId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int? LoginId { get; set; }

        public virtual UserLogin? Login { get; set; }
        public virtual RegionMaster? Region { get; set; }
        public virtual ICollection<ExcessFoodOrder> ExcessFoodOrders { get; set; }
        public virtual ICollection<SponsoredFood> SponsoredFoods { get; set; }
    }
}
