namespace LagaltAPI.Models
{
    /// <summary>Composite data model for combining users and projects</summary>
    public class UserProject
    {
        // Required to make a composite table
        public int UserID { get; set; }

        public User User { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }

        // Own values
        public bool Viewed { get; set; } = false;

        public bool Clicked { get; set; } = false;

        public bool Applied { get; set; } = false;

        public bool Contributed { get; set; } = false;

        public bool Administrator { get; set; } = false;

        public string Application { get; set; }
    }
}
