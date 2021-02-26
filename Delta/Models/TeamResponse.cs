using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delta.Models
{
    public class TeamResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PresidentId { get; set; }
        public UserResponse President { get; set; }
        public int? CoachId { get; set; }
        public UserResponse Coach { get; set; }
        /*public ICollection<int> PlayerIds { get; set; }

        public TeamResponse()
        {
            PlayerIds = new List<int>();
        }*/
    }
}