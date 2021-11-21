using System;

namespace TruckFlowDomain
{
    public class MatriculeFlux
    {
        public MatriculeFlux(int num, int serie, string flux)
        {
            Num = num;
            Serie = serie;
            Flux = flux;
        }

        public int Num { get; set; }
        public int Serie { get; set; }
        public string Flux { get; set; }
    }
}
