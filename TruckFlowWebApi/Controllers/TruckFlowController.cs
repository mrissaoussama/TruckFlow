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
//using Microsoft.AspNet.SignalR;

namespace TruckFlowWebApi.Controllers
{[ApiController]
    //Microsoft.AspNetCore.Cors.EnableCors]
    // [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    [EnableCors]
    public class TruckFlowController : Controller
    {
        
        TruckFlow truckFlow;
        string url;
        bool started=false;
       // private readonly IHubContext<TruckFlowHub> _hubContext;

        public TruckFlowController(ICarCheck carCheck, IDAOEvent daoevent)
        {
          
            this.url = ConfigurationManager.AppSettings["cameraapi"].ToString();
            this.truckFlow = new(carCheck, daoevent, url);
            this.Start();

        }
        [HttpGet]
        [Route("start")]
        public IActionResult Start()
        {if (!started)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    truckFlow.CheckPhotoAsync();
                }).Start();
                started = true;
                return Ok("Started");
            }
            else return Ok("already running");
        }
        [HttpGet]
        [Route("getlasteventsold")]
        public IEnumerable<Event> GetLastEventsAsync()
        {
            return truckFlow.GetLastEvents();
        }
        [HttpGet]
        [Route("getlastevents")]
        public async Task GetLastEvents2()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
        private async Task Echo(HttpContext httpContext, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
    
}
