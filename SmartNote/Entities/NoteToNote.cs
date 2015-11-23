using SmartNoteService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [DataContract]
    public class NoteToNote
    {
        [DataMember]
        [Key]
        public int OriginalNoteId { get; set; }
        [DataMember]
        [Key]
        public int ReferenceNoteId { get; set; }

        [DataMember]
        public Note OriginalNote { get; set; }
        [DataMember]
        public Note ReferenceNote { get; set; }
    }
}
