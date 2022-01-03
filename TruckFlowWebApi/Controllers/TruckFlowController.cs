using DAO.IDAO;
using TruckFlowDomain;
using TruckFlowWebApi.Interface;
using TruckFlowWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using System.Configuration;
using WebSocketServerProject.MidlleWare;
using Newtonsoft.Json;
using System.Text;
//using Microsoft.AspNet.SignalR;

namespace TruckFlowWebApi.Controllers
{[ApiController]
    //Microsoft.AspNetCore.Cors.EnableCors]
    // [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    [EnableCors]
    public class TruckFlowController : Controller
    {
        
        ITruckFlow truckFlow;
        bool started=false;
        public TruckFlowController(ITruckFlow truckFlow)
        {

            this.truckFlow = truckFlow;
            Start();

        }
        [HttpGet]
        [Route("start")]
        public void Start()
        {
            Console.WriteLine("started");
            if (started==false)
            {
                started = true;
            }
        }
        [HttpGet]
        [Route("getlasteventsold")]
        public IEnumerable<Event> GetLastEventsAsync()
        {
            return truckFlow.GetLastEvents();
        }
  

    }
    
}
