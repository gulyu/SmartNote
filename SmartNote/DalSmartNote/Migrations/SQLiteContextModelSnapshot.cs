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
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Entities.NoteToNote", b =>
                {
                    b.Property<int>("OriginalNoteId");

                    b.Property<int>("ReferenceNoteId");

                    b.Property<int?>("OriginalNoteId1");

                    b.Property<int?>("ReferenceNoteId1");

                    b.HasKey("OriginalNoteId", "ReferenceNoteId");
                });

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

                    b.Property<string>("PlainText");

                    b.Property<int>("Priority");

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

            modelBuilder.Entity("Entities.NoteToNote", b =>
                {
                    b.HasOne("SmartNoteService.Entities.Note")
                        .WithMany()
                        .HasForeignKey("OriginalNoteId1");

                    b.HasOne("SmartNoteService.Entities.Note")
                        .WithOne()
                        .HasForeignKey("Entities.NoteToNote", "ReferenceNoteId1");
                });

            modelBuilder.Entity("SmartNoteService.Entities.Attachment", b =>
                {
                    b.HasOne("SmartNoteService.Entities.Note")
                        .WithMany()
                        .HasForeignKey("NoteId");
                });

            modelBuilder.Entity("SmartNoteService.Entities.Note", b =>
                {
                    b.HasOne("SmartNoteService.Entities.User")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });
        }
    }
}
