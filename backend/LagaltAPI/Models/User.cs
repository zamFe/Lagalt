using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a user of Lagalt</summary>
    public class User
    {
        public int Id { get; set; }

        public bool Hidden { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Portfolio { get; set; }
    }
}
