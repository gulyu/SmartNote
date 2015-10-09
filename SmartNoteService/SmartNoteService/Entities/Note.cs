using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService.Entities
{
    [DataContract]
    public class Note
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public User Author { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public DateTime ModoficationDate { get; set; }
        [DataMember]
        public Dictionary<Guid, int> Links { get; set; }
        [DataMember]
        public List<Attachment> Attachments { get; set; }
    }
}
