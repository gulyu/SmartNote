using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DalSmartNote;

namespace DalSmartNote.Migrations
{
    [DbContext(typeof(SQLiteContext))]
    partial class SQLiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964");

            modelBuilder.Entity("SmartNoteService.Entities.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content");

                    b.Property<string>("Extension");

                    b.Property<string>("Name");

                    b.Property<int?>("NoteId");

                    b.Property<decimal>("Size");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SmartNoteService.Entities.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuthorId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("ModoficationDate");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SmartNoteService.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("SmartNoteService.Entities.Attachment", b =>
                {
                    b.HasOne("SmartNoteService.Entities.Note")
                        .WithMany()
                        .ForeignKey("NoteId");
                });

            modelBuilder.Entity("SmartNoteService.Entities.Note", b =>
                {
                    b.HasOne("SmartNoteService.Entities.User")
                        .WithMany()
                        .ForeignKey("AuthorId");
                });
        }
    }
}
