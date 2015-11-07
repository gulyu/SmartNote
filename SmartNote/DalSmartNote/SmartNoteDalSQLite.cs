using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNoteService.Entities;

namespace DalSmartNote
{
    public class SmartNoteDalSQLite : ISmartNoteDal
    {
        public bool DeleteNote(Note input)
        {
            using(var db = new SQLiteContext())
            {
                Note note = db.notes.Where(n => n.Id == input.Id).FirstOrDefault();
                if(note != null)
                {
                    db.Remove(input);
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

        public List<Note> GetAllNote(User input)
        {
            using (var db = new SQLiteContext())
            {
                return db.notes.ToList();
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
                Note note = db.notes.Where(n => n.Id == input.Id).FirstOrDefault();
                if(note != null)
                {
                    note.Links = input.Links;
                    note.Text = input.Text;
                    note.Title = input.Title;
                    note.Attachments = input.Attachments;
                    note.Author = input.Author;
                    note.CreationDate = input.CreationDate;
                    note.ModoficationDate = DateTime.Today;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
