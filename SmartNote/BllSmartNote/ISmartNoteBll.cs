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
        List<Note> GetAllNote(User input, int sortBy);

        bool InsertNote(Note input);

        bool UpdateNote(Note input);

        bool DeleteNote(Note input);

        void InitializeSQLiteDatabase();

        List<Note> GetNotesByParams(User user, string title, DateTime? creatinDate, DateTime? modDate, int? priority,
                                    bool? hasfile, bool? byTitle, bool? byCreationDate, bool? byModifyDate, bool? byPriority);
    }
}
