using System;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary> Simple data model representing a project message </summary>
    public class Message
    {
        // Private key.
        public int Id { get; set; }

        // Navigation & foreign key property for relationship with user.
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation & foreign key property for relationship with project.
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        // Data.
        [MaxLength(140), Required]
        public string Content { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
