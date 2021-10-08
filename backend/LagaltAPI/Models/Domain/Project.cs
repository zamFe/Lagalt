using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a project at Lagalt</summary>
    public class Project
    {
        //PK
        public int Id { get; set; }

        //FK Profession
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }

        //Many-to-One Relations
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
        public ICollection<Skill> Skills { get; set; }

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
