using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SmartNoteService.Entities
{
    [DataContract]
    public class InsertNoteRequest
    {
        [DataMember]
        public Note Note { get; set; }
    }
    
    public class InsertNoteResponse
    {
        [DataMember]
        public bool Success { get; set; }
    }
}