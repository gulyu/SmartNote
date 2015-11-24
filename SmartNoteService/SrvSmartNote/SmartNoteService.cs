using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNoteService.Entities;

namespace SrvSmartNote
{
    public class SmartNoteService : ISmartNoteService
    {
        public bool DeleteNote(Note input)
        {
            BllSmartNote.SmartNoteBll bll = new BllSmartNote.SmartNoteBll();
            bool ret = bll.DeleteNote(input);

            return ret;
        }

        public async Task<List<Note>> GetAllNote(User input)
        {
            BllSmartNote.SmartNoteBll bll = new BllSmartNote.SmartNoteBll();
            List<Note> ret = await bll.GetAllNote(input);

            return ret;
        }

        public bool InsertNote(Note input)
        {
            BllSmartNote.SmartNoteBll bll = new BllSmartNote.SmartNoteBll();
            bool ret = bll.InsertNote(input);

            return ret;
        }

        public bool UpdateNote(Note input)
        {
            BllSmartNote.SmartNoteBll bll = new BllSmartNote.SmartNoteBll();
            bool ret = bll.UpdateNote(input);

            return ret;
        }

        public bool DeleteAndInsertAll(List<Note> input, User author)
        {
            BllSmartNote.SmartNoteBll bll = new BllSmartNote.SmartNoteBll();
            bool ret = bll.DeleteAndInsertAll(input, author);

            return ret;
        }
    }
}
