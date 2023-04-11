using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShareBites.Models
{
    public partial class RegionMaster
    {
        public RegionMaster()
        {
            Helpers = new HashSet<Helper>();
            Restaurants = new HashSet<Restaurant>();
            Shelters = new HashSet<Shelter>();
            Sponsors = new HashSet<Sponsor>();
        }

        public string RegionId { get; set; } = null!;
        [Required]
        public string RegionName { get; set; } = null!;
        [Required]
        public string Province { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
        [Required]
        public int? IsActive { get; set; }

        public virtual ICollection<Helper> Helpers { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<Shelter> Shelters { get; set; }
        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
