using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LagaltAPI.Context
{
    /// <summary>
    ///     Simple representation of a database session.
    ///     Data source is picked up from an environment variable.
    /// </summary>
    public class LagaltContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Update> Updates { get; set; }

        // Constructor.
        public LagaltContext(DbContextOptions options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO - Fix seeding.

            var professions = new Profession[]
            {
                new Profession{Id = 1, Name = "Musikk"},
                new Profession{Id = 2, Name = "Film"},
                new Profession{Id = 3, Name = "Spillutvikling"},
                new Profession{Id = 4, Name = "Webutvikling"}
            };
            foreach (Profession p in professions)
                modelBuilder.Entity<Profession>().HasData(p);

            var skills = new Skill[]
            {
                new Skill{Id = 1, Name = "Gitar"},
                new Skill{Id = 2, Name = "Trommer"},
                new Skill{Id = 3, Name = "Skuespill"},
                new Skill{Id = 4, Name = "Unity"},
                new Skill{Id = 5, Name = "Unit testing"},
                new Skill{Id = 6, Name = "TypeScript"}
            };
            foreach (Skill s in skills)
                modelBuilder.Entity<Skill>().HasData(s);

            var users = new User[]
            {
                new User
                {
                    Id = 1,
                    Hidden = false,
                    Username = "Bob",
                    Description = "Looking for my friend, Mr. Tambourine",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/0/02/Bob_Dylan_-_Azkena_Rock_Festival_2010_2.jpg",
                    Portfolio = "https://en.wikipedia.org/wiki/Bob_Dylan_discography",
                    Viewed = new int[] { 1 },
                    Clicked = new int[] { 1 },
                    ContributedTo = new int[] { 1 }
                },
                new User
                {
                    Id = 2,
                    Hidden = false,
                    Username = "Grohl",
                    Description = "Lærer å fly for øyeblikket",
                    Portfolio = "https://en.wikipedia.org/wiki/Dave_Grohl#Career",
                    Viewed = new int[] { 1 },
                    Clicked = new int[] { 1 },
                    AppliedTo = new int[] { 1 },
                    ContributedTo = new int[] { 1 }
                },
                new User
                {
                    Id = 3,
                    Username = "DoubleOh",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/6/6b/Sean_Connery_as_James_Bond_in_Goldfinger.jpg",
                    Viewed = new int[] { 1 },
                    Clicked = new int[] { 1 },
                    AppliedTo = new int[] { 1 }
                },
                new User
                {
                    Id = 4,
                    Hidden = false,
                    Username = "ManOfEgg",
                    Portfolio = "https://static.wikia.nocookie.net/villains/images/2/21/Mister_Robotnik_the_Doctor.jpg/",
                    Viewed = new int[] { 2 },
                    Clicked = new int[] { 2 },
                    ContributedTo = new int[] { 2 }
                },
                new User
                {
                    Id = 5,
                    Hidden = false,
                    Username = "Rob",
                    Description = "Spillutvikler, elns",
                    Viewed = new int[] { 3 },
                    Clicked = new int[] { 3 },
                    ContributedTo = new int[] { 3 }
                },
                new User{
                    Id = 6,
                    Hidden = false,
                    Username = "Drew",
                    Image = "https://avatars.githubusercontent.com/u/1310872",
                    Portfolio = "https://git.sr.ht/~sircmpwn",
                    Viewed = new int[] { 4 },
                    Clicked = new int[] { 4 },
                    ContributedTo = new int[] { 4 }
                }
            };
            foreach (User u in users)
                modelBuilder.Entity<User>().HasData(u);

            var projects = new Project[]
            {
                new Project
                {
                    Id = 1,
                    ProfessionId = professions[0].Id,
                    AdministratorIds = new int[] {1},
                    Title = "Skrive et album på en ubåt",
                    Description = "Jeg har alltid hatt lyst til å reise i en ubåt, og jeg må også skrive låter",
                    Progress = "In Progress",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/d/d8/Submarine_Vepr_by_Ilya_Kurganov_crop.jpg"
                },
                new Project
                {
                    Id = 2,
                    ProfessionId = professions[1].Id,
                    AdministratorIds = new int[] {4},
                    Title = "Den Filmiske Film Filmen",
                    Description = "Some call them movies and some call them films. På norsk gjør vi det litt enklere"
                },
                new Project
                {
                    Id = 3,
                    ProfessionId = professions[2].Id,
                    AdministratorIds = new int[] {5},
                    Title = "Enda et tetris spill",
                    Description = "Hva kan gå galt?",
                    Progress =  "Completed",
                    Source = "https://github.com/vocollapse/Blockinger"
                },
                new Project
                {
                    Id = 4,
                    ProfessionId = professions[2].Id,
                    AdministratorIds = new int[] {6},
                    Title = "Minecraft Nostalgi",
                    Description = "Alt var bedre før",
                    Progress = "Stalled",
                    Source = "https://github.com/ddevault/TrueCraft"
                },
                new Project
                {
                    Id = 5,
                    ProfessionId = professions[3].Id,
                    Title = "Reddit API",
                    Description = "Jeg har definitivt lest det",
                    Progress = "Stalled",
                    Image = "https://raw.githubusercontent.com/ddevault/TrueCraft/master/TrueCraft.Client/Content/terrain.png",
                    Source = "https://github.com/ddevault/RedditSharp"
                },
            };
            foreach (Project p in projects)
                modelBuilder.Entity<Project>().HasData(p);

            var messages = new Message[]
            {
                // Submarine project messsages
                new Message
                {
                    Id = 1,
                    UserId = users[0].Id,
                    ProjectId = projects[0].Id,
                    Content = "Andre enn meg som liker ubåter?",
                    PostedTime = new DateTime(2021, 10, 2, 12, 30, 52)
                },
                new Message
                {
                    Id = 2,
                    UserId = users[1].Id,
                    ProjectId = projects[0].Id,
                    Content = "Jaa",
                    PostedTime = new DateTime(2021, 10, 2, 12, 40, 33)
                },
                new Message
                {
                    Id = 3,
                    UserId = users[2].Id,
                    ProjectId = projects[0].Id,
                    Content = "Usikker, vi må se an",
                    PostedTime = new DateTime(2021, 10, 3, 8, 20, 03)
                },
            };
            foreach (Message m in messages)
                modelBuilder.Entity<Message>().HasData(m);

            var updates = new Update[]
            {
                // Submarine project updates
                new Update
                {
                    Id = 1,
                    UserId = users[0].Id,
                    ProjectId = projects[0].Id,
                    Content = "Nå har vi fått leid en ubåt!",
                    PostedTime = new DateTime(2021, 10, 9, 2, 30, 32)
                },
            };
            foreach (Update u in updates)
                modelBuilder.Entity<Update>().HasData(u);

            var applications = new Application[]
            {
                new Application
                {
                    Id = 1,
                    ProjectId = projects[0].Id,
                    UserId = users[1].Id,
                    Accepted = true,
                    Motivation = "Jeg elsker også ubåter!!!",
                },
                new Application
                {
                    Id = 2,
                    ProjectId = projects[0].Id,
                    UserId = users[2].Id,
                    Accepted = true,
                    Motivation = "Prøver å finne ut om jeg liker ubåter...",
                },
                new Application
                {
                    Id = 3,
                    ProjectId = projects[0].Id,
                    UserId = users[4].Id,
                    Accepted = false,
                    Motivation = "Hva er en ubåt?",
                },
            };
            foreach (Application a in applications)
                modelBuilder.Entity<Application>().HasData(a);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Skills)
                .WithMany(skill => skill.Users)
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
                .HasMany(project => project.Skills)
                .WithMany(skill => skill.Projects)
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

            modelBuilder.Entity<Project>()
                .HasMany(project => project.Users)
                .WithMany(user => user.Projects)
                .UsingEntity<Dictionary<string, object>>
                (
                    "ProjectUsers",
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    l => l.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                    je =>
                    {
                        je.HasKey("UserId", "ProjectId");
                        je.HasData
                        (
                            new { ProjectId = 1, UserId = 1 },
                            new { ProjectId = 1, UserId = 2 },
                            new { ProjectId = 1, UserId = 3 },
                            new { ProjectId = 2, UserId = 4 },
                            new { ProjectId = 3, UserId = 5 },
                            new { ProjectId = 4, UserId = 6 }
                        );
                    }
                );
        }
    }
}
