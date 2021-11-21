using DAO.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //IDAOEvent daoevent;
        string url;

        public TruckFlow(ICarCheck carCheck, string url)
        {
            this.url = url;
            this.carCheck = carCheck;
          //  this.daoevent = daoevent;
        }
        public Task CheckPhoto()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("start");

                HttpClient client = new HttpClient();
                  HttpResponseMessage response = new HttpResponseMessage();
                  byte[] mybytearray = null;
                while(true)
                {
                    Console.WriteLine("get photo");
                    Uri uri = new(url);
                    response = client.GetAsync(uri).Result;
                    Console.WriteLine(response.IsSuccessStatusCode.ToString());

                    if (response.IsSuccessStatusCode)
                  {
                        string result = null;
                        mybytearray = response.Content.ReadAsByteArrayAsync().Result;
                        Console.WriteLine(mybytearray.Length);
                      if (mybytearray != null || mybytearray.Length != 0)
                      {
                          MatriculeFlux m = carCheck.GetVehicleNumberAndFlow(mybytearray);
                          Console.WriteLine("added"+mybytearray.Length.ToString());
                      }

                  }
                  Thread.Sleep(5000);
                    }

              }
              );
        }
    }
}
