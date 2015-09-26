using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartNoteService.Entities
{
    [DataContract]
    public class TestMethodRequest
    {
        [DataMember]
        public int Count { get; set; }
    }

    [DataContract]
    public class TestMethodResponse
    {
        [DataMember]
        public List<int> List { get; set; }
    }
}
