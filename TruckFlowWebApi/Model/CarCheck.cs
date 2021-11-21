using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckFlowDomain;
using TruckFlowWebApi.Interface;

namespace TruckFlowWebApi.Model
{
    public class CarCheck : ICarCheck
    {
        public CarCheck() { }
        public MatriculeFlux GetVehicleNumberAndFlow(byte[] photo)
        {
            Random rand = new Random();
            int chance = rand.Next(1, 101);
            if (chance <= 50)//50% chance
            {
                return null;
            }
            else
            {
                return new MatriculeFlux(545, 5465, "in");
            }

        }
    }
}
