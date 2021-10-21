namespace LagaltAPI.Models.DTOs.Profession
{
    /// <summary> A read-specific DTO of a profession. </summary>
    public class ProfessionReadDTO
    {
        /// <summary> The profession's id. </summary>
        public int Id { get; set; }

        /// <summary> The profession's name. </summary>
        public string Name { get; set; }
    }
}
