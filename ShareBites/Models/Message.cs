using System;
using System.Collections.Generic;

namespace ShareBites.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
        public string? Message1 { get; set; }
        public int? IsAck { get; set; }

        public virtual UserLogin? FromUser { get; set; }
        public virtual UserLogin? ToUser { get; set; }
    }
}
