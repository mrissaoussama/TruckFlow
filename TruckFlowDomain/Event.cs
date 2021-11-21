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
        public Event(string mat, DateTime dateevent, DateTime heureevent, string flux, bool autorise, byte[] photo, bool sync)
        {
            this.mat = mat;
            this.dateevent = dateevent;
            this.heureevent = heureevent;
            this.flux = flux;
            this.autorise = autorise;
            this.photo = photo;
            this.sync = sync;
        }
        public Event()
        {
            
        }
        public Event(int idevent, string mat, DateTime dateevent, DateTime heureevent, string flux, bool autorise, byte[] photo, bool sync)
        {
            this.idevent = idevent;
            this.mat = mat;
            this.dateevent = dateevent;
            this.heureevent = heureevent;
            this.flux = flux;
            this.autorise = autorise;
            this.photo = photo;
            this.sync = sync;
        }

        public int idevent { get; set; }
        public string mat { get; set; }
        public DateTime dateevent { get; set; }
        public DateTime heureevent { get; set; }
        public string flux { get; set; }
        public bool autorise { get; set; }
        public byte[] photo { get; set; }
        public bool sync { get; set; }

    }
}
