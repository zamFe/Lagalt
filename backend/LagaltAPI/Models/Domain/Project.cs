using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary> Simple data model representing a project at Lagalt </summary>
    public class Project
    {
        // Private key.
        public int Id { get; set; }

        // Navigation & foreign key property for relationship with profession.
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }

        // Collection navigation property for relationship with messages.
        public ICollection<Message> Messages { get; set; }

        // Collection navigation property for relationship with users.
        public ICollection<UserProject> UserProjects { get; set; }

        // Collection navigation property for relationship with skills.
        public ICollection<Skill> Skills { get; set; }

        // Data.
        [MaxLength(40), Required]
        public string Title { get; set; }

        [MaxLength(300), Required]
        public string Description { get; set; }

        [MaxLength(50), Required]
        public string Progress { get; set; } = "Founding";

        [MaxLength(100)]
        public string Image { get; set; }

        [MaxLength(100)]
        public string Source { get; set; }
    }
}
