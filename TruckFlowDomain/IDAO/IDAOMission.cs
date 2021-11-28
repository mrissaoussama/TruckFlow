using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckFlowDomain;

namespace DAO.IDAO
{
   public interface IDAOMission

    {
        public Mission GetMission(string matricule,DateTime date); 
    }
}
