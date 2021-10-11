using LagaltAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace LagaltAPI.Context
{
    /// <summary>
    /// Simple representation of a database session.
    /// Data source is picked up from a ".env" file.
    /// </summary>
    public class LagaltContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Profession> Professions { get; set; }
        
        public LagaltContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var professions = new Profession[]
            {
                new Profession{Id = 1, Name = "Music"},
                new Profession{Id = 2, Name = "Film"},
                new Profession{Id = 3, Name = "Game Development"},
                new Profession{Id = 4, Name = "Web Development"}
            };

            var skills = new Skill[]
            {
                new Skill{Id = 1, Name = "Guitar"},
                new Skill{Id = 2, Name = "Drums"},
                new Skill{Id = 3, Name = "Acting"},
                new Skill{Id = 4, Name = "Unity"},
                new Skill{Id = 5, Name = "Unit testing"},
                new Skill{Id = 6, Name = "TypeScript"}
            };

            var users = new User[]
            {
                new User
                {
                    Id = 1,
                    Hidden = false,
                    UserName = "Bob",
                    Description = "Looking for my friend, Mr. Tambourine",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/0/02/Bob_Dylan_-_Azkena_Rock_Festival_2010_2.jpg",
                    Portfolio = "https://en.wikipedia.org/wiki/Bob_Dylan_discography"
                },
                new User
                {
                    Id = 2,
                    Hidden = false,
                    UserName = "Grohl",
                    Description = "Currently learning to fly",
                    Portfolio = "https://en.wikipedia.org/wiki/Dave_Grohl#Career"
                },
                new User
                {
                    Id = 3,
                    UserName = "DoubleOh",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/6/6b/Sean_Connery_as_James_Bond_in_Goldfinger.jpg"
                },
                new User
                {
                    Id = 4,
                    Hidden = false,
                    UserName = "ManOfEgg",
                    Portfolio = "https://static.wikia.nocookie.net/villains/images/2/21/Mister_Robotnik_the_Doctor.jpg/"
                },
                new User
                {
                    Id = 5,
                    Hidden = false,
                    UserName = "Rob",
                    Description = "Game dev, I guess"
                },
                new User{
                    Id = 6,
                    Hidden = false,
                    UserName = "Drew",
                    Image = "https://avatars.githubusercontent.com/u/1310872",
                    Portfolio = "https://git.sr.ht/~sircmpwn"
                }
            };

            var projects = new Project[]
            {
                new Project
                {
                    Id = 1,
                    ProfessionId = professions[0].Id,
                    Title = "Writing an album on a Submarine",
                    Description = "I've always wanted to travel by submarine and I've also got to make new songs",
                    Progress = "In Progress",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/d/d8/Submarine_Vepr_by_Ilya_Kurganov_crop.jpg"
                },
                new Project
                {
                    Id = 2,
                    ProfessionId = professions[1].Id,
                    Title = "The Cinematic Movie Film",
                    Description = "Some call them movies and some call them films. But what if both were correct?"
                },
                new Project
                {
                    Id = 3,
                    ProfessionId = professions[2].Id,
                    Title = "Yet Another Tetris Game",
                    Description = "What could go wrong?",
                    Progress =  "Completed",
                    Source = "https://github.com/vocollapse/Blockinger"
                },
                new Project
                {
                    Id = 4,
                    ProfessionId = professions[2].Id,
                    Title = "Minecraft Nostalgia",
                    Description = "It was better before",
                    Progress = "Stalled",
                    Source = "https://github.com/ddevault/TrueCraft"
                },
                new Project
                {
                    Id = 5,
                    ProfessionId = professions[3].Id,
                    Title = "Reddit API",
                    Description = "I did indeed read it",
                    Progress = "Stalled",
                    Image = "https://raw.githubusercontent.com/ddevault/TrueCraft/master/TrueCraft.Client/Content/terrain.png",
                    Source = "https://github.com/ddevault/RedditSharp"
                },
            };

            // TODO - Add more messages
            var messages = new Message[]
            {
                // Submarine project messsages
                new Message
                {
                    Id = 1,
                    UserId = users[0].Id,
                    ProjectId = projects[0].Id,
                    Content = "Anyone else like submarines?",
                    PostedTime = new DateTime(2021, 10, 2, 12, 30, 52)
                },
                new Message
                {
                    Id = 2,
                    UserId = users[1].Id,
                    ProjectId = projects[0].Id,
                    Content = "Yeah",
                    PostedTime = new DateTime(2021, 10, 2, 12, 40, 33)
                },
                new Message
                {
                    Id = 3,
                    UserId = users[2].Id,
                    ProjectId = projects[0].Id,
                    Content = "Not sure yet. We will see",
                    PostedTime = new DateTime(2021, 10, 3, 8, 20, 03)
                },
            };

            // TODO - add UserProject entries
            var userProjects = new UserProject[]
            {
                new UserProject
                {
                    UserID = 1,
                    ProjectID = 1,
                    Viewed = true,
                    Clicked = true,
                    Applied = true,
                    Administrator = true
                },
                new UserProject
                {
                    UserID = 2,
                    ProjectID = 1,
                    Viewed = true,
                    Clicked = true,
                    Applied = true,
                    Application = "Plz i luv submarinezz!!!1!!1!!"
                },
                new UserProject
                {
                    UserID = 3,
                    ProjectID = 1,
                    Viewed = true,
                    Clicked = true,
                    Applied = true,
                    Application = "Request Access"
                },
                new UserProject
                {
                    UserID = 4,
                    ProjectID = 1,
                    Viewed = true,
                },
                new UserProject
                {
                    UserID = 5,
                    ProjectID = 1,
                    Viewed = true,
                    Clicked = true
                }
            };

            foreach (Profession p in professions)
                modelBuilder.Entity<Profession>().HasData(p);
            foreach (Skill s in skills)
                modelBuilder.Entity<Skill>().HasData(s);
            foreach (User u in users)
                modelBuilder.Entity<User>().HasData(u);
            foreach (Project p in projects)
                modelBuilder.Entity<Project>().HasData(p);
            foreach (Message m in messages)
                modelBuilder.Entity<Message>().HasData(m);
            foreach (UserProject up in userProjects)
                modelBuilder.Entity<UserProject>().HasData(up);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Skills)
                .WithMany(s => s.Users)
                .UsingEntity<Dictionary<string, object>>
                (
                    "UserSkills",
                    r => r.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    je =>
                    {
                        je.HasKey("SkillId", "UserId");
                        je.HasData
                        (
                            new { UserId = 1, SkillId = 1 },
                            new { UserId = 2, SkillId = 1 },
                            new { UserId = 2, SkillId = 2 },
                            new { UserId = 3, SkillId = 3 },
                            new { UserId = 4, SkillId = 3 },
                            new { UserId = 4, SkillId = 4 },
                            new { UserId = 4, SkillId = 5 },
                            new { UserId = 5, SkillId = 1 },
                            new { UserId = 5, SkillId = 2 },
                            new { UserId = 5, SkillId = 3 },
                            new { UserId = 5, SkillId = 4 },
                            new { UserId = 5, SkillId = 5 },
                            new { UserId = 5, SkillId = 6 }
                        );
                    }
                );

            modelBuilder.Entity<Project>()
                .HasMany(u => u.Skills)
                .WithMany(s => s.Projects)
                .UsingEntity<Dictionary<string, object>>
                (
                    "ProjectSkills",
                    r => r.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                    l => l.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                    je =>
                    {
                        je.HasKey("SkillId", "ProjectId");
                        je.HasData
                        (
                            new { ProjectId = 1, SkillId = 1 },
                            new { ProjectId = 1, SkillId = 2 },
                            new { ProjectId = 2, SkillId = 3 },
                            new { ProjectId = 3, SkillId = 4 },
                            new { ProjectId = 4, SkillId = 5 },
                            new { ProjectId = 5, SkillId = 6 }
                        );
                    }
                );

            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserID, up.ProjectID });
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserID);
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectID);
        }
    }
}
