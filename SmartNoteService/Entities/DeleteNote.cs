using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService.Entities
{
    [DataContract]
    public class DeleteNoteRequest
    {
        [DataMember]
        public Note Note { get; set; }
    }

    [DataContract]
    public class DeleteNoteResponse
    {
        [DataMember]
        public bool Success { get; set; }
    }
}
