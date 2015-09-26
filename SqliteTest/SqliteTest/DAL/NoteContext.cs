using Microsoft.Data.Entity;
using SqliteTest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SqliteTest.DAL
{
    class NoteContext : DbContext
    {
        public DbSet<Note> notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = "notes.sqlite";
            try
            {
                databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
            }
            catch (InvalidOperationException)
            {}

            optionsBuilder.UseSqlite($"Data source={databaseFilePath}");
        }
    }
}
