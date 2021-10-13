namespace LagaltAPI.Models.DTOs.Application
{
    public class ApplicationCreateDTO
    {
        // TODO - Figure out where to always set Accepted to false.
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Motivation { get; set; }
    }
}
