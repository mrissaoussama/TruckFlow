using DAO.IDAO;
using Microsoft.AspNetCore.Mvc;
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
    public class TruckFlowController : Controller
    {
        ICarCheck carCheck;
        IDAOEvent daoevent;
        TruckFlow truckFlow;
        string url;
        public TruckFlowController(ICarCheck carCheck, IDAOEvent daoevent)
        {
            this.carCheck = carCheck;
            this.daoevent = daoevent;
            this.url = ConfigurationManager.AppSettings["cameraapi"].ToString();
            this.truckFlow = new(carCheck, daoevent, url);


        }
        [HttpGet]
        [Route("start")]
        public IActionResult Start()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                truckFlow.CheckPhoto();
            }).Start();
            return Ok("Started");
        }
        [HttpGet]
        [Route("getlastevents")]
        public IEnumerable<Event> GetLastEvents()
        {
            return truckFlow.GetLastEvents();
        }
    }
}
