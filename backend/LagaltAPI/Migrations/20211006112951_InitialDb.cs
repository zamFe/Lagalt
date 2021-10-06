using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LagaltAPI.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Portfolio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessionId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Progress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillUser",
                columns: table => new
                {
                    SkillsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillUser", x => new { x.SkillsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SkillUser_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    PostedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProjects",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    Viewed = table.Column<bool>(type: "bit", nullable: false),
                    Clicked = table.Column<bool>(type: "bit", nullable: false),
                    Applied = table.Column<bool>(type: "bit", nullable: false),
                    Contributed = table.Column<bool>(type: "bit", nullable: false),
                    Administrator = table.Column<bool>(type: "bit", nullable: false),
                    Application = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjects", x => new { x.UserID, x.ProjectID });
                    table.ForeignKey(
                        name: "FK_UserProjects_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjects_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "PostedTime", "ProjectId", "UserId" },
                values: new object[,]
                {
                    { 1, "Anyone else like submarines?", new DateTime(2021, 10, 2, 12, 30, 52, 0, DateTimeKind.Unspecified), null, null },
                    { 2, "Yeah", new DateTime(2021, 10, 2, 12, 40, 33, 0, DateTimeKind.Unspecified), null, null },
                    { 3, "Not sure yet. We will see", new DateTime(2021, 10, 3, 8, 20, 3, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "Professions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Film" },
                    { 3, "Game Development" },
                    { 4, "Web Development" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "Image", "ProfessionId", "Progress", "Source", "Title" },
                values: new object[,]
                {
                    { 4, "It was better before", null, null, "Stalled", "https://github.com/ddevault/TrueCraft", "Minecraft Nostalgia" },
                    { 3, "What could go wrong?", null, null, "Completed", "https://github.com/vocollapse/Blockinger", "Yet Another Tetris Game" },
                    { 5, "I did indeed read it", "https://raw.githubusercontent.com/ddevault/TrueCraft/master/TrueCraft.Client/Content/terrain.png", null, "Stalled", "https://github.com/ddevault/RedditSharp", "Reddit API" },
                    { 1, "I've always wanted to travel by submarine and I've also got to make new songs", "https://upload.wikimedia.org/wikipedia/commons/d/d8/Submarine_Vepr_by_Ilya_Kurganov_crop.jpg", null, "In Progress", null, "Writing an album on a Submarine" },
                    { 2, "Some call them movies and some call them films. But what if both were correct?", null, null, "Founding", null, "The Cinematic Movie Film" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Guitar" },
                    { 2, "Drums" },
                    { 3, "Acting" },
                    { 4, "Unity" },
                    { 5, "Unit testing" },
                    { 6, "TypeScript" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Description", "Hidden", "Image", "Portfolio", "UserName" },
                values: new object[,]
                {
                    { 5, "Game dev, I guess", false, null, null, "Rob" },
                    { 1, "Looking for my friend, Mr. Tambourine", false, "https://upload.wikimedia.org/wikipedia/commons/0/02/Bob_Dylan_-_Azkena_Rock_Festival_2010_2.jpg", "https://en.wikipedia.org/wiki/Bob_Dylan_discography", "Bob" },
                    { 2, "Currently learning to fly", false, null, "https://en.wikipedia.org/wiki/Dave_Grohl#Career", "Grohl" },
                    { 3, null, true, "https://upload.wikimedia.org/wikipedia/commons/6/6b/Sean_Connery_as_James_Bond_in_Goldfinger.jpg", null, "DoubleOh" },
                    { 4, null, false, null, "https://static.wikia.nocookie.net/villains/images/2/21/Mister_Robotnik_the_Doctor.jpg/", "ManOfEgg" },
                    { 6, null, false, "https://avatars.githubusercontent.com/u/1310872", "https://git.sr.ht/~sircmpwn", "Drew" }
                });

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
                name: "IX_SkillUser_UsersId",
                table: "SkillUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_ProjectID",
                table: "UserProjects",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "SkillUser");

            migrationBuilder.DropTable(
                name: "UserProjects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Professions");
        }
    }
}
