using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllSmartNote
{
    public interface ISmartNoteBll
    {
        List<Note> GetAllNote(User input);

        bool InsertNote(Note input);

        bool UpdateNote(Note input);

        bool DeleteNote(Note input);
    }
}
