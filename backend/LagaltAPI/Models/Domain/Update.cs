using System;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models.Domain
{
    /// <summary> Simple data model representing an update message to a project </summary>
    public class Update
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
        [MaxLength(140), Required]
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
