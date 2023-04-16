using System;
using System.Collections.Generic;

namespace ShareBites.Models
{
    public partial class ResFoodHandler
    {
        public int FoodId { get; set; }
        public int? ResId { get; set; }
        public DateTime? DateAndTime { get; set; }
        public TimeSpan? WaitingTime { get; set; }
        public string? FoodDesc { get; set; }
        public string? FoodStatus { get; set; }
        public bool IsExcess { get; set; }

        public virtual Restaurant? Res { get; set; }
        public virtual ExcessFoodOrder? ExcessFoodOrder { get; set; }
        public virtual SponsoredFood? SponsoredFood { get; set; }
    }
}
