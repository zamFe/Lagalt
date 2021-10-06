using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a skill</summary>
    public class Skill
    {
        public int Id { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
