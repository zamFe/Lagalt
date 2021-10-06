using System.ComponentModel.DataAnnotations;

namespace LagaltAPI.Models
{
    /// <summary>
    /// Data model for linking users and projects plus additional data
    /// </summary>
    public class UserProject
    {
        // Required to make a linking table
        public int UserID { get; set; }

        public User User { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }

        // Additional values beyond mandatory ones
        public bool Viewed { get; set; } = false;

        public bool Clicked { get; set; } = false;

        public bool Applied { get; set; } = false;

        public bool Contributed { get; set; } = false;

        public bool Administrator { get; set; } = false;

        [MaxLength(140)]
        public string Application { get; set; }
    }
}
