using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TeamExercise.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // foreign key
        [Required]
        [ForeignKey("President")]
        public int PresidentId { get; set; }

        // navigation property
        public virtual President President { get; set; }

        // foreign key
        [ForeignKey("Coach")]
        public int CoachId { get; set; }

        // navigation property
        public virtual Coach Coach { get; set; }

        public int PlayerId1 { get; set; }

        public int PlayerId2 { get; set; }

        public int PlayerId3 { get; set; }

        public int PlayerId4 { get; set; }

        public int PlayerId5 { get; set; }

        public int PlayerId6 { get; set; }

        public int PlayerId7 { get; set; }

        public int PlayerId8 { get; set; }

        public int PlayerId9 { get; set; }

        public int PlayerId10 { get; set; }

        public int PlayerId11 { get; set; }

        public int PlayerId12 { get; set; }
    }
}