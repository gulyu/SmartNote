using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService.Entities
{
    [DataContract]
    public class NoteToNote
    {
        [DataMember]
        public int OriginalNoteId { get; set; }
        [DataMember]
        public int ReferenceNoteId { get; set; }

        [DataMember]
        public Note OriginalNote { get; set; }
        [DataMember]
        public Note ReferenceNote { get; set; }
    }
}
