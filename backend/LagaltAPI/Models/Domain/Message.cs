using System;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a project message</summary>
    public class Message
    {
        //PK
        public int Id { get; set; }

        //FK User
        public int UserId { get; set; }
        public User User { get; set; }

        //FK Project
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [MaxLength(140), Required]
        public string Content { get; set; }

        public DateTime PostedTime { get; set; }
    }
}
