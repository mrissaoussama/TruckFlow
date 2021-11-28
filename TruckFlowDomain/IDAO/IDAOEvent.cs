using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckFlowDomain;

namespace DAO.IDAO
{
   public interface IDAOEvent
    {
        public List<Event> GetLastEvents();
        public void Update(Event e);
        public void Insert(Event e);
    }
}
