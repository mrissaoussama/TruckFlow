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
    public interface ITruckFlow
    {
  
        public WebSocketServerConnectionManager _socketManager { get; set; }


        public IEnumerable<Event> GetLastEvents();
        public Task CheckPhotoAsync();

    }
}
