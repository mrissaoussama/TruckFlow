using DAO.IDAO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TruckFlowDomain;
using TruckFlowWebApi.Interface;
using WebSocketServerProject.MidlleWare;

namespace TruckFlowWebApi.Model
{
    public class TruckFlow
    {
        ICarCheck carCheck;
        IDAOEvent daoevent;
        string url;
        WebSocketServerConnectionManager _socketManager;

        public TruckFlow(ICarCheck carCheck, IDAOEvent daoevent,string url,WebSocketServerConnectionManager socketManager)
        {
            this.url = url;
            this.carCheck = carCheck;
           this.daoevent = daoevent;
            _socketManager = socketManager;
        }
        public IEnumerable<Event> GetLastEvents()
        {
            return daoevent.GetLastEvents();
        }
        public Task CheckPhotoAsync()
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
                            _socketManager.GetSockets();
                        }

                    }
                  Thread.Sleep(8000);
                    }

              }
              );
        }
    }
}
