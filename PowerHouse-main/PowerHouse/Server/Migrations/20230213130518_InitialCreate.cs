using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PowerHouse.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConversationUser",
                columns: table => new
                {
                    ConversationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationUser", x => new { x.ConversationsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ConversationUser_Conversation_ConversationsId",
                        column: x => x.ConversationsId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
						principalColumn: "Id",
						onDelete: ReferentialAction.SetNull);
					table.ForeignKey(
                        name: "FK_Messages_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.SetNull);
				});

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
						principalColumn: "Id",
						onDelete: ReferentialAction.SetNull);
					table.ForeignKey(
                        name: "FK_Reports_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
			            principalColumn: "Id", 
                        onDelete: ReferentialAction.SetNull);
		});

            migrationBuilder.InsertData(
                table: "Conversation",
                columns: new[] { "Id", "AuthorId", "IsBlocked", "IsPublic", "Topic" },
                values: new object[,]
                {
                    { new Guid("01f424df-72cd-4dd0-a0a5-929ddbbda931"), new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"), false, true, "Exploring the World of Microorganisms: The Hidden Life in Your Backyard" },
                    { new Guid("7ca6277d-f149-4d92-bfec-aeeb47881689"), new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"), false, true, "Beyond the Horizon: The Fascinating World of Space Exploration" },
                    { new Guid("9990e740-142d-4a11-bb5d-8fb262fdf74a"), new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"), false, true, "The Power of the Mind: Understanding the Science of Memory and Cognition" },
                    { new Guid("a1d66b82-3313-485e-b464-79ad3bd1f84e"), new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"), false, true, "The Science of Sleep: Understanding the Importance of a Good Night's Rest" },
                    { new Guid("a322d519-142c-44ad-adc4-fd0be0b5b752"), new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"), false, true, "Revolutionizing Agriculture: The Future of Sustainable Farming Practices" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Username" },
                values: new object[,]
                {
                    { new Guid("a6bd1e41-b8f0-4434-8387-39329ae2e1a1"), "Bambi.goretti@live.se", "Bambi Goretti" },
                    { new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"), "carlo.goretti@live.se", "Carlo Goretti" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "Action", "ConversationId", "Decision", "IsChecked", "IsClosed", "MessageId", "Reason", "Reported", "ReporterId", "Type" },
                values: new object[,]
                {
                    { new Guid("309a4766-5a12-4211-bb99-9635b7ed1038"), 0, new Guid("a322d519-142c-44ad-adc4-fd0be0b5b752"), null, false, false, null, "Bad text", new DateTime(2023, 2, 13, 13, 5, 18, 443, DateTimeKind.Utc).AddTicks(7616), new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"), 0 },
                    { new Guid("9b7f4c45-940d-4b4f-90ae-9548c2c091b4"), 0, new Guid("a1d66b82-3313-485e-b464-79ad3bd1f84e"), null, false, false, null, "Hurt my feelings", new DateTime(2023, 2, 13, 13, 5, 18, 443, DateTimeKind.Utc).AddTicks(7623), new Guid("b1873f2f-81f0-4683-aa7f-33c3e9845df2"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConversationUser_UsersId",
                table: "ConversationUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ConversationId",
                table: "Reports",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_MessageId",
                table: "Reports",
                column: "MessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversationUser");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
