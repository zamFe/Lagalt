using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models.Domain
{
    /// <summary> Simple data model representing an application to a project </summary>
    public class Application
    {
        // Private key.
        public int Id { get; set; }

        // Navigation & foreign key property for relationship with project.
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        // Navigation & foreign key property for relationship with user.
        public int UserId { get; set; }
        public User User { get; set; }

        // Data.
        public bool Accepted { get; set; } = false;

        [MaxLength(140), Required]
        public string Motivation { get; set; }
    }
}
