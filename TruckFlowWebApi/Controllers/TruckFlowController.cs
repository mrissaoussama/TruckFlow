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
        
        TruckFlow truckFlow;
        bool started=false;
        // private readonly IHubContext<TruckFlowHub> _hubContext;
        public TruckFlowController(ICarCheck carCheck, IDAOEvent daoevent, TruckFlow truckFlow)
        {

            this.truckFlow = truckFlow;
            this.StartAsync();

        }
        [HttpGet]
        [Route("start")]
        public async Task StartAsync()
        {if (!started)
            {
                 truckFlow.CheckPhotoAsync().Start();

                started = true;
              //  return Ok("Started");
            }
           // else return Ok("already running");
        }
        [HttpGet]
        [Route("getlasteventsold")]
        public IEnumerable<Event> GetLastEventsAsync()
        {
            return truckFlow.GetLastEvents();
        }
        private async Task RecieveMessageAsync(WebSocket ws, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);
                handleMessage(result, buffer);
            }

        }

        [HttpGet]
        [Route("getlastevents")]
        public async Task GetLastEvents2()
        {
           
        }
   
    }
    
}
