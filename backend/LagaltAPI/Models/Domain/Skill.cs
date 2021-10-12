using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary> Simple data model representing a skill </summary>
    public class Skill
    {
        // Private key.
        public int Id { get; set; }

        // Collection navigation property for relationship with users.
        public ICollection<User> Users { get; set; }

        // Collection navigation property for relationship with projects.
        public ICollection<Project> Projects { get; set; }

        // Data.
        [MaxLength(50), Required]
        public string Name { get; set; }
    }
}
