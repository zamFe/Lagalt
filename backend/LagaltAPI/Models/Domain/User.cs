using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LagaltAPI.Models.Domain
{
    /// <summary> Simple data model representing a user of Lagalt </summary>
    public class User
    {
        // Private key.
        public int Id { get; set; }

        // Collection navigation property for relationship with messages.
        public ICollection<Application> Applications { get; set; }

        // Collection navigation property for relationship with messages.
        public ICollection<Message> Messages { get; set; }

        // Collection navigation property for relationship with projects.
        public ICollection<Project> Projects { get; set; }

        // Collection navigation property for relationship with skills.
        public ICollection<Skill> Skills { get; set; }

        // Data.
        public bool Hidden { get; set; } = true;

        [MaxLength(20), Required]
        public string Username { get; set; }

        [MaxLength(140)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }

        [MaxLength(100)]
        public string Portfolio { get; set; }

        // Account history.
        public int[] Viewed { get; set; }

        public int[] Clicked { get; set; }

        public int[] AppliedTo { get; set; }

        public int[] ContributedTo { get; set; }
    }
}
