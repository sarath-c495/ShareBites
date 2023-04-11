using System;
using System.Collections.Generic;

namespace ShareBites.Models
{
    public partial class SponsoredFood
    {
        public int SfId { get; set; }
        public int? FoodId { get; set; }
        public int? SponsorId { get; set; }
        public int? ShelterId { get; set; }

        public virtual ResFoodHandler? Food { get; set; }
        public virtual Shelter? Shelter { get; set; }
        public virtual Sponsor? Sponsor { get; set; }
    }
}
