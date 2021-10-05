using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a project at Lagalt</summary>
    public class Project
    {
        public int Id { get; set; }

        public Profession Profession { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }

        public string Source { get; set; }

        public string Progress { get; set; }
    }
}
