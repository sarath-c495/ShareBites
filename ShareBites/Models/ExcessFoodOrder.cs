﻿using System;
using System.Collections.Generic;

namespace ShareBites.Models
{
    public partial class ExcessFoodOrder
    {
        public int OrderId { get; set; }
        public int? FoodId { get; set; }
        public int? ShelterId { get; set; }
        public DateTime? OrderDate { get; set; }
        public TimeSpan? OrderPickUp { get; set; }
        public string? ModeOfdelivery { get; set; }
        public int? HelperId { get; set; }

        public virtual ResFoodHandler? Food { get; set; }
        public virtual Helper? Helper { get; set; }
        public virtual Shelter? Shelter { get; set; }
    }
}
