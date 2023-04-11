using System;
using System.Collections.Generic;

namespace ShareBites.Models
{
    public partial class ModeOfhelpMaster
    {
        public ModeOfhelpMaster()
        {
            Helpers = new HashSet<Helper>();
            Restaurants = new HashSet<Restaurant>();
            Sponsors = new HashSet<Sponsor>();
        }

        public string HelpId { get; set; } = null!;
        public string ModeOfHelp { get; set; } = null!;
        public string? Description { get; set; }
        public int? CarRequired { get; set; }

        public virtual ICollection<Helper> Helpers { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
