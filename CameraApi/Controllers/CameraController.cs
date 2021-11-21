using CameraApi.Metier;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CameraApi.Controllers
{
    [ApiController]
    [Route("camera")]
    public class CameraController : Controller
    {
        CameraIP cameraIP=new();
     
        [Route("/getphoto")]
        [HttpGet]
        public ActionResult GetPhoto()
        {
            Console.WriteLine(cameraIP.GetPhoto());
            return File(cameraIP.GetPhoto(), "application/octet-stream");
        }
    }
}
