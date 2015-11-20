using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNoteService.Entities;
using DalSmartNote;
using Microsoft.Data.Entity;

namespace BllSmartNote
{
    public class SmartNoteBll : ISmartNoteBll
    {
        private static ISmartNoteDal smartNoteDal;

        public SmartNoteBll()
        {
            smartNoteDal = new SmartNoteDalSQLite();
        }

        public bool DeleteNote(Note input)
        {
            return smartNoteDal.DeleteNote(input);
        }

        public List<Note> GetAllNote(User input)
        {
            return smartNoteDal.GetAllNote(input);
        }

        public void InitializeSQLiteDatabase()
        {
            // SQLite adatbázis inicializáció.
            using(var db = new SQLiteContext())
            {
                db.Database.Migrate();
            }
        }

        public bool InsertNote(Note input)
        {
            return smartNoteDal.InsertNote(input);
        }

        public bool UpdateNote(Note input)
        {
            return smartNoteDal.UpdateNote(input);
        }
    }
}
