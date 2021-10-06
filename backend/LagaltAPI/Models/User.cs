using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a user of Lagalt</summary>
    public class User
    {
        public int Id { get; set; }

        public bool Hidden { get; set; } = true;

        public ICollection<Skill> Skills { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }

        [MaxLength(20), Required]
        public string UserName { get; set; }

        [MaxLength(140)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }

        [MaxLength(100)]
        public string Portfolio { get; set; }
    }
}
