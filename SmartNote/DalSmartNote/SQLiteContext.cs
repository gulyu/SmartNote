using Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using SmartNoteService.Entities;
using System;
using System.IO;
using Windows.Storage;

namespace DalSmartNote
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<NoteToNote> NoteToNote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = "smartnote.db";

            try
            {
                databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
            }
            catch (InvalidOperationException)
            { }

            optionsBuilder.UseSqlite($"Data source={databaseFilePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasMany(n => n.Attachments).WithOne("Note").OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Note>().HasMany(n => n.Links).WithOne("OriginalNote").OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<NoteToNote>().HasOne(n => n.ReferenceNote).WithOne("NoteToNoteNavigation").OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<NoteToNote>().HasKey(n => new { n.OriginalNoteId, n.ReferenceNoteId });
        }
    }
}
