using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LagaltAPI.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    Username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: true),
                    Image = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Portfolio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Viewed = table.Column<int[]>(type: "integer[]", nullable: true),
                    Clicked = table.Column<int[]>(type: "integer[]", nullable: true),
                    AppliedTo = table.Column<int[]>(type: "integer[]", nullable: true),
                    ContributedTo = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfessionId = table.Column<int>(type: "integer", nullable: false),
                    AdministratorIds = table.Column<int[]>(type: "integer[]", nullable: true),
                    Title = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Progress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Source = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => new { x.SkillId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Accepted = table.Column<bool>(type: "boolean", nullable: false),
                    Seen = table.Column<bool>(type: "boolean", nullable: false),
                    Motivation = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false),
                    PostedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSkills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkills", x => new { x.SkillId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Updates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false),
                    PostedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Updates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Updates_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Updates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Professions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Musikk" },
                    { 2, "Film" },
                    { 3, "Spillutvikling" },
                    { 4, "Webutvikling" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Gitar" },
                    { 2, "Trommer" },
                    { 3, "Skuespill" },
                    { 4, "Unity" },
                    { 5, "Unit testing" },
                    { 6, "TypeScript" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AppliedTo", "Clicked", "ContributedTo", "Description", "Hidden", "Image", "Portfolio", "Username", "Viewed" },
                values: new object[,]
                {
                    { 1, null, new[] { 1 }, new[] { 1 }, "Looking for my friend, Mr. Tambourine", false, "https://upload.wikimedia.org/wikipedia/commons/0/02/Bob_Dylan_-_Azkena_Rock_Festival_2010_2.jpg", "https://en.wikipedia.org/wiki/Bob_Dylan_discography", "Bob", new[] { 1 } },
                    { 2, new[] { 1 }, new[] { 1 }, new[] { 1 }, "Lærer å fly for øyeblikket", false, null, "https://en.wikipedia.org/wiki/Dave_Grohl#Career", "Grohl", new[] { 1 } },
                    { 3, new[] { 1 }, new[] { 1 }, null, null, true, "https://upload.wikimedia.org/wikipedia/commons/6/6b/Sean_Connery_as_James_Bond_in_Goldfinger.jpg", null, "DoubleOh", new[] { 1 } },
                    { 4, null, new[] { 2 }, new[] { 2 }, null, false, null, "https://static.wikia.nocookie.net/villains/images/2/21/Mister_Robotnik_the_Doctor.jpg/", "ManOfEgg", new[] { 2 } },
                    { 5, null, new[] { 3 }, new[] { 3 }, "Spillutvikler, elns", false, null, null, "Rob", new[] { 3 } },
                    { 6, null, new[] { 4 }, new[] { 4 }, null, false, "https://avatars.githubusercontent.com/u/1310872", "https://git.sr.ht/~sircmpwn", "Drew", new[] { 4 } }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "AdministratorIds", "Description", "Image", "ProfessionId", "Progress", "Source", "Title" },
                values: new object[,]
                {
                    { 1, new[] { 1 }, "Jeg har alltid hatt lyst til å reise i en ubåt, og jeg må også skrive låter", "https://upload.wikimedia.org/wikipedia/commons/d/d8/Submarine_Vepr_by_Ilya_Kurganov_crop.jpg", 1, "In Progress", null, "Skrive et album på en ubåt" },
                    { 2, new[] { 4 }, "Some call them movies and some call them films. På norsk gjør vi det litt enklere", null, 2, "Founding", null, "Den Filmiske Film Filmen" },
                    { 3, new[] { 5 }, "Hva kan gå galt?", null, 3, "Completed", "https://github.com/vocollapse/Blockinger", "Enda et tetris spill" },
                    { 4, new[] { 6 }, "Alt var bedre før", null, 3, "Stalled", "https://github.com/ddevault/TrueCraft", "Minecraft Nostalgi" },
                    { 5, null, "Jeg har definitivt lest det", "https://raw.githubusercontent.com/ddevault/TrueCraft/master/TrueCraft.Client/Content/terrain.png", 4, "Stalled", "https://github.com/ddevault/RedditSharp", "Reddit API" }
                });

            migrationBuilder.InsertData(
                table: "UserSkills",
                columns: new[] { "SkillId", "UserId" },
                values: new object[,]
                {
                    { 4, 5 },
                    { 3, 5 },
                    { 2, 5 },
                    { 1, 5 },
                    { 5, 4 },
                    { 3, 3 },
                    { 3, 4 },
                    { 5, 5 },
                    { 2, 2 },
                    { 1, 2 },
                    { 1, 1 },
                    { 4, 4 },
                    { 6, 5 }
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "Accepted", "Motivation", "ProjectId", "Seen", "UserId" },
                values: new object[,]
                {
                    { 1, true, "Jeg elsker også ubåter!!!", 1, false, 2 },
                    { 2, true, "Prøver å finne ut om jeg liker ubåter...", 1, false, 3 },
                    { 3, false, "Hva er en ubåt?", 1, false, 5 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "PostedTime", "ProjectId", "UserId" },
                values: new object[,]
                {
                    { 1, "Andre enn meg som liker ubåter?", new DateTime(2021, 10, 2, 12, 30, 52, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, "Jaa", new DateTime(2021, 10, 2, 12, 40, 33, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 3, "Usikker, vi må se an", new DateTime(2021, 10, 3, 8, 20, 3, 0, DateTimeKind.Unspecified), 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProjectSkills",
                columns: new[] { "ProjectId", "SkillId" },
                values: new object[,]
                {
                    { 4, 5 },
                    { 3, 4 },
                    { 2, 3 },
                    { 5, 6 },
                    { 1, 2 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProjectUsers",
                columns: new[] { "ProjectId", "UserId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 3, 5 },
                    { 4, 6 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Updates",
                columns: new[] { "Id", "Content", "PostedTime", "ProjectId", "UserId" },
                values: new object[] { 1, "Nå har vi fått leid en ubåt!", new DateTime(2021, 10, 9, 2, 30, 32, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ProjectId",
                table: "Applications",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ProjectId",
                table: "Messages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProfessionId",
                table: "Projects",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Updates_ProjectId",
                table: "Updates",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Updates_UserId",
                table: "Updates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ProjectSkills");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "Updates");

            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Professions");
        }
    }
}
