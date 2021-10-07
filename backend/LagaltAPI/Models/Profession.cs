using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>Simple data model representing a profession</summary>
    public class Profession
    {
        public int Id { get; set; }

        [Required]
        public bool Name { get; set; }
    }
}
