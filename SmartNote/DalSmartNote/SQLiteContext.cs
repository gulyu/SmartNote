using Microsoft.Data.Entity;
using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DalSmartNote
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Note> notes { get; set; }
        public DbSet<User> users { get; set; }

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
            // Itt lehet megadni a kötelező attribútumokat...
            //modelBuilder.Entity<Note>().Property(b => b.Id).IsRequired();
        }
    }
}
