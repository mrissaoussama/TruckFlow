using DAO.IDAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TruckFlowDomain;
using TruckFlowWebApi.Interface;
using TruckFlowWebApi.Model;
namespace TruckFlowWebApi.Controllers
{[ApiController]
    //Microsoft.AspNetCore.Cors.EnableCors]
    // [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    [EnableCors]
    public class TruckFlowController : Controller
    {
        ICarCheck carCheck;
        IDAOEvent daoevent;
        TruckFlow truckFlow;
        string url;
        bool started=false;

        public TruckFlowController(ICarCheck carCheck, IDAOEvent daoevent)
        {
            this.carCheck = carCheck;
            this.daoevent = daoevent;
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
                    truckFlow.CheckPhoto();
                }).Start();
                started = true;
                return Ok("Started");
            }
            else return Ok("already running");
        }
        [HttpGet]
        [Route("getlastevents")]
        public IEnumerable<Event> GetLastEvents()
        {
            return truckFlow.GetLastEvents();
        }
    }
}
