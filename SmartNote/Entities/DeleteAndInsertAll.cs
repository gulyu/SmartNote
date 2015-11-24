using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService.Entities
{
    [DataContract]
    public class DeleteAndInsertAllRequest
    {
        [DataMember]
        public List<Note> Notes { get; set; }

        [DataMember]
        public User Author { get; set; }
    }

    [DataContract]
    public class DeleteAndInsertAllResponse
    {
        [DataMember]
        public bool Success { get; set; }
    }
}
