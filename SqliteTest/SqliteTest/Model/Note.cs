using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteTest.Model
{
    class Note
    {
        public int id { get; set; }
        public String text { get; set; }
        public DateTime created { get; set; }
    }
}
