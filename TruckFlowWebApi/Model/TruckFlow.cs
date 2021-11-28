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
                    try
                    {
                        response = client.GetAsync(uri).Result;
                    }
                    catch(HttpRequestException ex)
                    {
                        Console.WriteLine("error getting photo: " + ex.Message);
                    }
                    catch (AggregateException ex)
                    {
                        Console.WriteLine("error getting photo: " + ex.Message);
                    }


                    if (response.IsSuccessStatusCode)
                  {
                        mybytearray = response.Content.ReadAsByteArrayAsync().Result;
                        MatriculeFlux m = carCheck.GetVehicleNumberAndFlow(mybytearray);

                        if (m != null)
                      {
                          Event e=new(" تونس " + m.Num+m.Serie,DateTime.Now.Date,DateTime.Now.TimeOfDay,m.Flux,false,mybytearray,false);
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
