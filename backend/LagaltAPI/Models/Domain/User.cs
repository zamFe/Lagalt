using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LagaltAPI.Models
{
    /// <summary> Simple data model representing a user of Lagalt </summary>
    public class User
    {
        // Private key.
        public int Id { get; set; }

        // Collection navigation property for relationship with skills.
        public ICollection<Skill> Skills { get; set; }

        // Collection navigation property for relationship with projects.
        public ICollection<UserProject> UserProjects { get; set; }

        // Collection navigation property for relationship with messages.
        public ICollection<Message> Messages { get; set; }

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
    }
}
