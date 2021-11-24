using System;
using System.Diagnostics;
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
            { if(chance>50 && chance <=75)

                return new MatriculeFlux(rand.Next(0000, 10000), rand.Next(000, 1000), "in");
                return new MatriculeFlux(rand.Next(0000, 10000), rand.Next(000, 1000), "out");

            }

        }
    }
}
