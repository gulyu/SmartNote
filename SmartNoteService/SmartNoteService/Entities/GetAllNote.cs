using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService.Entities
{
    public class GetAllNoteRequest
    {
        public User Author { get; set; }
    }

    public class GetAllNoteResponse
    {
        public List<Note> Notes { get; set; }
        public bool Success { get; set; }
    }
}
