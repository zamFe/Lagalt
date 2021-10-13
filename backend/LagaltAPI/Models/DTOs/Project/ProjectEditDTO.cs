﻿namespace LagaltAPI.Models.DTOs.Project
{
    public class ProjectEditDTO
    {
        public int Id { get; set; }
        public int Profession { get; set; }
        public int[] Messages { get; set; }
        public int[] Users { get; set; }
        public int[] Administrators { get; set; }
        public int[] Skills { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Progress { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
        
    }
}
