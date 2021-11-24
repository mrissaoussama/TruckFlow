using DAO.IDAO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TruckFlowDomain;
using TruckFlowWebApi.Interface;

namespace TruckFlowWebApi.Model
{
    public class TruckFlow
    {
        ICarCheck carCheck;
        IDAOEvent daoevent;
        string url;

        public TruckFlow(ICarCheck carCheck, IDAOEvent daoevent,string url)
        {
            this.url = url;
            this.carCheck = carCheck;
           this.daoevent = daoevent;
        }
        public IEnumerable<Event> GetLastEvents()
        {
            return daoevent.GetLastEvents();
        }
        public Task CheckPhoto()
        {
            return Task.Run(() =>
            {
            

                HttpClient client = new HttpClient();
                  HttpResponseMessage response = new HttpResponseMessage();
                  byte[] mybytearray = null;
                while (true)
                {
                    Uri uri = new(url);
                    response = client.GetAsync(uri).Result;

                    if (response.IsSuccessStatusCode)
                  {
                        mybytearray = response.Content.ReadAsByteArrayAsync().Result;
                        MatriculeFlux m = carCheck.GetVehicleNumberAndFlow(mybytearray);

                        if (m != null)
                      {
                          Event e=new(m.Num+" تونس "+m.Serie,DateTime.Now.Date,DateTime.Now.TimeOfDay,m.Flux,false,mybytearray,false);
                            daoevent.Insert(e);
                        }

                    }
                  Thread.Sleep(8000);
                    }

              }
              );
        }
    }
}
