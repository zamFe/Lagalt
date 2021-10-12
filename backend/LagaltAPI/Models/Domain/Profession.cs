using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models.Domain
{
    /// <summary> Simple data model representing a profession </summary>
    public class Profession
    {
        // Private key.
        public int Id { get; set; }

        [MaxLength(30), Required]
        public string Name { get; set; }

        // Collection navigation property for relationship with projects.
        public ICollection<Project> Projects { get; set; }
    }
}
