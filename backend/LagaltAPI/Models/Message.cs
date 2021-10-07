using System;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a project message</summary>
    public class Message
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Project Project { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PostedTime { get; set; }
    }
}
