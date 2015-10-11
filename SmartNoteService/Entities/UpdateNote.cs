using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService.Entities
{
    [DataContract]
    public class UpdateNoteRequest
    {
        [DataMember]
        public Note Note { get; set; }
    }

    public class UpdateNoteResponse
    {
        [DataMember]
        public bool Success { get; set; }
    }
}
