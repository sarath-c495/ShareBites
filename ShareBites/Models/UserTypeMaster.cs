using System;
using System.Collections.Generic;

namespace ShareBites.Models
{
    public partial class UserTypeMaster
    {
        public UserTypeMaster()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public string UserTypeId { get; set; } = null!;
        public string? UserType { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
