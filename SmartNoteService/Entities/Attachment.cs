using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService.Entities
{
    [DataContract]
    public class Attachment
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public byte[] Content { get; set; }
        [DataMember]
        public decimal Size { get; set; }
    }
}
