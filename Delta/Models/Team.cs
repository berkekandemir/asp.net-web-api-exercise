using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Delta.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // foreign key
        [ForeignKey("President")]
        public int PresidentId { get; set; }
        // navigation property
        public virtual User President { get; set; }
        // foreign key
        [ForeignKey("Coach")]
        public int? CoachId { get; set; }
        // navigation property
        public virtual User Coach { get; set; }
        //public ICollection<int> PlayerIds { get; set; }
        /*public int[] PlayerIds { get; set; }

        public Team()
        {
            //PlayerIds = new List<int>();
            PlayerIds = new int[12];
        }*/
    }
}