using Bogus;
using LagaltAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public LagaltContext(DbContextOptions options) : base(options) { }

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

            /* SEEDING PLAN:
            * PROJECTS: 1000
            * USERS: 2000
            * PROFESSIONS: 4
            * ADMINS: 300
            * SKILLS: 100
            * MESSAGES: 1000
            * APPLICATIONS: 1000
            * UPDATES: 1000
            */

            var skillIds = 1;
            var skillFaker = new Faker<Skill>()
                .RuleFor(s => s.Id, f => skillIds++)
                .RuleFor(s => s.Name, f => f.Hacker.IngVerb());
            modelBuilder
                .Entity<Skill>()
                .HasData(skillFaker.Generate(100));

            var userIds = 1;
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => userIds++)
                .RuleFor(u => u.Hidden, f => f.Random.Bool())
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Description, f => f.Name.JobTitle())
                .RuleFor(u => u.Image, f => f.Image.PicsumUrl())
                .RuleFor(u => u.Portfolio, f => f.Internet.Url())
                .RuleFor(u => u.Viewed, f => Enumerable.Range(10, 100).Select(x => f.Random.Int(1, 1000)).ToArray())
                .RuleFor(u => u.Clicked, f => Enumerable.Range(5, 50).Select(x => f.Random.Int(1, 1000)).ToArray())
                .RuleFor(u => u.ContributedTo, f => Enumerable.Range(1, 10).Select(x => f.Random.Int(1, 1000)).ToArray());

            modelBuilder
                .Entity<User>()
                .HasData(userFaker.Generate(2000));

            var rand = new Random();

            var projectIds = 1;
            var projectFaker = new Faker<Project>()
                .RuleFor(p => p.Id, f => projectIds++)
                .RuleFor(p => p.ProfessionId, f => f.Random.Int(1, 4))
                .RuleFor(p => p.AdministratorIds, f => Enumerable.Range(1, 3).Select(x => f.Random.Int(1, 2000)).ToArray())
                .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Progress, f => "founding")
                .RuleFor(p => p.Image, f => f.Image.PicsumUrl());

            modelBuilder
                .Entity<Project>()
                .HasData(projectFaker.Generate(1000));

            var messageIds = 1;
            var messageFaker = new Faker<Message>()
                .RuleFor(m => m.Id, f => messageIds++)
                .RuleFor(m => m.UserId, f => f.Random.Int(1, 2000))
                .RuleFor(m => m.ProjectId, f => f.Random.Int(1, 1000))
                .RuleFor(m => m.Content, f => f.Lorem.Sentence(3, 5))
                .RuleFor(m => m.PostedTime, f => f.Date.Recent());

            modelBuilder
                .Entity<Message>()
                .HasData(messageFaker.Generate(1000));

            var updateIds = 1;
            var updateFaker = new Faker<Update>()
                .RuleFor(m => m.Id, f => updateIds++)
                .RuleFor(m => m.UserId, f => f.Random.Int(1, 2000))
                .RuleFor(m => m.ProjectId, f => f.Random.Int(1, 1000))
                .RuleFor(m => m.Content, f => f.Lorem.Sentence(3, 5))
                .RuleFor(m => m.PostedTime, f => f.Date.Recent());

            modelBuilder
                .Entity<Update>()
                .HasData(updateFaker.Generate(1000));

            var applicationIds = 1;
            var applicationFaker = new Faker<Application>()
                .RuleFor(a => a.Id, f => applicationIds++)
                .RuleFor(a => a.ProjectId, f => f.Random.Int(1, 1000))
                .RuleFor(a => a.UserId, f => f.Random.Int(1, 2000))
                .RuleFor(a => a.Accepted, f => f.Random.Bool())
                .RuleFor(a => a.Motivation, f => f.Lorem.Sentence(3, 5));

            modelBuilder.Entity<Application>().HasData(applicationFaker.Generate(1000));

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
                            GenerateUserSkills()
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
                            GenerateProjectSkills()
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
                           GenerateUserProjects()
                        );
                    }
                );
        }

        private class Up
        {
            public Up(int pId, int uId)
            {
                ProjectId = pId;
                UserId = uId;
            }
            public int ProjectId { get; }
            public int UserId { get; }
        }

        private static List<object> GenerateUserProjects()
        {
            Random rnd = new();
            List<object> output = new();
            for(int i = 0; i < 100; i++)
            {
                int pId = rnd.Next(1, 1000);
                int uId = rnd.Next(1, 2000);
                if (!output.Contains(new { ProjectId = pId, UserId = uId })) {
                    output.Add(new { ProjectId = pId, UserId = uId });
                }
            }
            return output;
        }

        private static List<object> GenerateUserSkills()
        {
            Random rnd = new();
            List<object> output = new();
            for (int i = 1; i <= 100; i++)
            {
                for(int j = 0; j < rnd.Next(3, 6); j++)
                {
                    int sId = rnd.Next(1, 100);
                    if (!output.Contains(new { SkillId = sId, UserId = i }))
                    {
                        output.Add(new { SkillId = sId, UserId = i });
                    }
                }
            }
            return output;
        }

        private static List<object> GenerateProjectSkills()
        {
            Random rnd = new();
            List<object> output = new();
            for (int i = 1; i <= 50; i++)
            {
                for (int j = 0; j < rnd.Next(3, 6); j++)
                {
                    int sId = rnd.Next(1, 100);
                    if (!output.Contains(new { SkillId = sId, ProjectId = i }))
                    {
                        output.Add(new { SkillId = sId, ProjectId = i });
                    }
                }
            }
            return output;
        }
    }
}
