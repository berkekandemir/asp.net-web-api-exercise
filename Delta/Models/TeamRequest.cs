using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Delta.Models
{
    public class TeamRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // foreign key
        [Required]
        public int PresidentId { get; set; }
        // foreign key
        public int? CoachId { get; set; }
        /*public ICollection<int> PlayerIds { get; set; }

        public TeamRequest()
        {
            PlayerIds = new List<int>();
        }*/
    }
}