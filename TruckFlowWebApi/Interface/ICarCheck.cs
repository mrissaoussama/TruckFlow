using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckFlowDomain;

namespace TruckFlowWebApi.Interface
{
   public interface ICarCheck
    {
        public MatriculeFlux GetVehicleNumberAndFlow(byte[] photo);
    }
}
