using LagaltAPI.Models.DTOs.User;

namespace LagaltAPI.Models.DTOs.Application
{
    public class ApplicationReadDTO
    {
        public int Id { get; set; }
        public UserCompleteReadDTO User { get; set; }
        public bool Accepted { get; set; }
        public bool Seen { get; set; }
        public string Motivation { get; set; }
    }
}
