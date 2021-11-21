using DAO.IDAO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TruckFlowWebApi.Interface;
using TruckFlowWebApi.Model;

namespace TruckFlowWebApi.Controllers
{[ApiController]
    public class TruckFlowController : Controller
    {
        ICarCheck carCheck;
        //IDAOEvent daoevent;
        string url= "http://localhost:61893/getphoto";
        public TruckFlowController(ICarCheck carCheck)
        {
           // this.url = url;
            this.carCheck = carCheck;
        //    this.daoevent = daoevent;
            
        }
        [HttpGet]
        [Route("start")]
        public void Start()
        {
            TruckFlow truckFlow = new(carCheck, url);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                truckFlow.CheckPhoto();
            }).Start();
        }
    }
}
