using DAO.IDAO;
using DAOExpressSqlEF6.DBContextNS;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckFlowDomain;

namespace DAOExpressSqlEF6.DAONS
{
    public class DAOEvent : IDAOEvent
    {
        DBContext dbContext;
  
        public DAOEvent()
        {
        }
        public List<Event> GetAllEvents()
        {
            this.dbContext = new();

            return dbContext.Events.ToList();
        }
        public List<Event> GetLastEvents()
        {
            this.dbContext = new();

            var x = dbContext.Events.OrderByDescending(p => p.idevent).Take(10).ToList();
            SyncEvents(x);
            return x;
        }
        public void SyncEvents(List<Event> eventlist)
        {
            this.dbContext = new();

            eventlist.ForEach(e => e.autorise = true);
            dbContext.SaveChanges();
                }

        public void Insert(Event e)
        {
            this.dbContext = new();

            dbContext.Events.Add(e);
            dbContext.SaveChanges();
        }
        //not implemented
        public void Update(Event e)
        {
            this.dbContext = new();

            var Event =  dbContext.Events.Find(e);
            if(Event!=null)
            {
                dbContext.SaveChanges();
            }
        }
    }
}
