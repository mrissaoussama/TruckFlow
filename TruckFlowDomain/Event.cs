using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckFlowDomain
{
    public class Event
    {
        public int IDEvent { get; set; }
        public string Mqt { get; set; }
        public DateTime DateEvent { get; set; }
        public Timestamp HeureEvent { get; set; }
        public string flux { get; set; }
        public bool Autorisé { get; set; }
        public byte[] photo { get; set; }
        public bool sync { get; set; }

    }
}
