using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a profession</summary>
    public class Profession
    {
        public int Id { get; set; }

        [MaxLength(30), Required]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
