using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace DalSmartNote.Migrations
{
    public partial class SmartNoteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModoficationDate = table.Column<DateTime>(nullable: false),
                    PlainText = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "NoteToNote",
                columns: table => new
                {
                    OriginalNoteId = table.Column<int>(nullable: false),
                    ReferenceNoteId = table.Column<int>(nullable: false),
                    OriginalNoteId1 = table.Column<int>(nullable: true),
                    ReferenceNoteId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteToNote", x => new { x.OriginalNoteId, x.ReferenceNoteId });
                    table.ForeignKey(
                        name: "FK_NoteToNote_Note_OriginalNoteId1",
                        column: x => x.OriginalNoteId1,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteToNote_Note_ReferenceNoteId1",
                        column: x => x.ReferenceNoteId1,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<byte[]>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NoteId = table.Column<int>(nullable: true),
                    Size = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("NoteToNote");
            migrationBuilder.DropTable("Attachment");
            migrationBuilder.DropTable("Note");
            migrationBuilder.DropTable("User");
        }
    }
}
