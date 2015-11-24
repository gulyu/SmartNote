using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNoteService.Entities;

namespace BllSmartNote
{
    public class SmartNoteBll : ISmartNoteBll
    {
        public bool DeleteNote(Note input)
        {
            DalSmartNote.SmartNoteDal dal = new DalSmartNote.SmartNoteDal();
            bool ret = dal.DeleteNote(input);

            return ret;
        }

        public async Task<List<Note>> GetAllNote(User input)
        {
            DalSmartNote.SmartNoteDal dal = new DalSmartNote.SmartNoteDal();
            List<Note> ret = await dal.GetAllNote(input);

            return ret;
        }

        public bool InsertNote(Note input)
        {
            DalSmartNote.SmartNoteDal dal = new DalSmartNote.SmartNoteDal();
            bool ret = dal.InsertNote(input);

            return ret;
        }

        public bool UpdateNote(Note input)
        {
            DalSmartNote.SmartNoteDal dal = new DalSmartNote.SmartNoteDal();
            bool ret = dal.UpdateNote(input);

            return ret;
        }

        public bool DeleteAndInsertAll(List<Note> input, User author)
        {
            DalSmartNote.SmartNoteDal dal = new DalSmartNote.SmartNoteDal();
            bool ret = dal.DeleteAndInsertAll(input, author);

            return ret;
        }
    }
}
