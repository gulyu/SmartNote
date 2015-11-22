using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

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
        [NotMapped]
        public Dictionary<Guid, int> Links { get; set; }
        [DataMember]
        public ICollection<Attachment> Attachments { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public string PlainText { get; set; }
    }
}
