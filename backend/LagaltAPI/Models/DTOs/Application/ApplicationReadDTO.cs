using LagaltAPI.Models.DTOs.User;

namespace LagaltAPI.Models.DTOs.Application
{
    public class ApplicationReadDTO
    {
        public int Id { get; set; }
        public UserCompactReadDTO User { get; set; }
        public bool Accepted { get; set; }
        public string Motivation { get; set; }
    }
}
