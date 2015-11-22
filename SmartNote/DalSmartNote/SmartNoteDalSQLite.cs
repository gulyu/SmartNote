using System;
using System.Collections.Generic;
using System.Linq;
using SmartNoteService.Entities;

namespace DalSmartNote
{
    public class SmartNoteDalSQLite : ISmartNoteDal
    {
        private Note getNote(Note input)
        {
            using (var db = new SQLiteContext())
            {
                Note note = db.notes.Where(n => n.Id == input.Id).FirstOrDefault();
                if(note != null)
                {
                    note.Attachments = db.attachments.Where(a => a.Note.Id == note.Id).ToList();
                    return note;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool DeleteNote(Note input)
        {
            using(var db = new SQLiteContext())
            {
                Note note = getNote(input);
                if(note != null)
                {
                    db.notes.Remove(note);
                    db.SaveChanges();
                    // Ha törlés után megtalálom a listában, akkor nem sikerült a törlés.
                    return db.notes.Where(n => n.Id == input.Id).FirstOrDefault() == null ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Note> GetAllNote(User input, int sortBy)
        {
            using (var db = new SQLiteContext())
            {
                List<Note> notes = null;
                switch(sortBy)
                {
                    case 0:
                        notes = db.notes.OrderBy(n => n.Title).ToList();
                        break;
                    case 1:
                        notes = db.notes.OrderByDescending(n => n.Priority).ToList();
                        break;
                    case 2:
                        notes = db.notes.OrderBy(n => n.ModoficationDate).ToList();
                        break;
                }
                foreach (var item in notes)
                {
                    item.Attachments = db.attachments.Where(a => a.Note.Id == item.Id).ToList();
                }
                return notes;
            }
        }

        public bool InsertNote(Note input)
        {
            using (var db = new SQLiteContext())
            {
                db.notes.Add(input);
                db.SaveChanges();

                return db.notes.Where(n => n.Id == input.Id).FirstOrDefault() == null ? false : true;
            }
        }

        public bool UpdateNote(Note input)
        {
            using (var db = new SQLiteContext())
            {
                db.Update(input);
                db.SaveChanges();

                Note note = getNote(input);
                return note.ModoficationDate.Date.Equals(DateTime.Today.Date) ? true : false;
            }
        }
    }
}
