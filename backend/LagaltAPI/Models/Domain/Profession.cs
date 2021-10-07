using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a profession</summary>
    public class Profession
    {
        //PK
        public int Id { get; set; }

        [MaxLength(30), Required]
        public string Name { get; set; }

        //Many-to-Many Relation
        public ICollection<Project> Projects { get; set; }
    }
}
