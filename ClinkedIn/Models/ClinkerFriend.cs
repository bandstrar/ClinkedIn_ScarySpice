using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class ClinkerFriend
    {
        public int Id { get; set; }
        public int ClinkerId { get; set; }
        public int FriendId { get; set; }
    }
}
