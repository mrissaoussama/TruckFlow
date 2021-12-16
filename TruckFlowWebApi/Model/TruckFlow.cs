using DAO.IDAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
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
        public WebSocketServerConnectionManager _socketManager { get; set; }

        public TruckFlow(ICarCheck carCheck, IDAOEvent daoevent)
        {
            this.url = ConfigurationManager.AppSettings["cameraapi"].ToString();
            this.carCheck = carCheck;
           this.daoevent = daoevent;
        }
        public IEnumerable<Event> GetLastEvents()
        {
            return daoevent.GetLastEvents();
        }
        public  Task CheckPhotoAsync()
        {
          //   return new Task(() =>
             {

                // Thread.CurrentThread.IsBackground = true;
                return Task.Run(async () =>
                 {
                     Console.WriteLine("from truck" + _socketManager.GetSockets().Count);

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
                         catch (HttpRequestException ex)
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
                                 Event e = new(" تونس " + m.Num + m.Serie, DateTime.Now.Date, DateTime.Now.TimeOfDay, m.Flux, false, mybytearray, false);
                                 daoevent.Insert(e);
                                 List<Event> list = new();
                                 list.Add(e);
                                 Console.WriteLine("inserted"+ _socketManager.GetSockets().IsEmpty);
                                 if (_socketManager.GetSockets().IsEmpty != true)
                                 {
                                     foreach (var socket in _socketManager.GetSockets())
                                     {
                                         var json = JsonConvert.SerializeObject(list);
                                        // ArraySegment<Byte> arr = new(truckFlow.GetLastEvents().ToArray()); 
                                        var buffer = Encoding.UTF8.GetBytes(json.ToCharArray());
                                         await socket.Value.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                                         Console.WriteLine("sent");
                                     }
                                 }
                             }
                         }
                         await Task.Delay(5000);
                    }
                 });

            }
            //);
        }
    }
}
