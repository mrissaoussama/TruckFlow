using DAO.IDAO;
using DAOExpressSqlEF6.DAONS;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TruckFlowDomain;
using TruckFlowWebApi.Interface;
using TruckFlowWebApi.Model;
using WebSocketServerProject.Models.Domaine;

namespace WebSocketServerProject.MidlleWare
{
    public class WebSocketServerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketServerConnectionManager _wsServerConnManager;
       
        TruckFlow truckFlow;

        public WebSocketServerMiddleWare(

            RequestDelegate next, WebSocketServerConnectionManager webSocketServerConnectionManager)

        {

            _next = next;
            _wsServerConnManager = webSocketServerConnectionManager;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            this.truckFlow = new(new CarCheck(), new DAOEvent(), ConfigurationManager.AppSettings["cameraapi"].ToString());

            WriteRequestParam(context);

            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();
                string connID = _wsServerConnManager.AddSocket(websocket);
                await SendMessageAsync(websocket, connID);
                Console.WriteLine("Socket connected...");
                await RecieveMessageAsync(websocket, async (result, message) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine("Message recieved...");
                        string messageRecu= Encoding.UTF8.GetString(message, 0, result.Count);
                        Console.WriteLine($"Message {messageRecu}");
                        await RouteJSONMessagesAsync(messageRecu);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Recieve Close...");
                        return;
                    }
                });
            }
            else
                await _next(context);
        }
        public void WriteRequestParam(HttpContext context)
        {

            Console.WriteLine("Request Method: " + context.Request.Method);
            Console.WriteLine("Request Protocol: " + context.Request.Protocol);

            if (context.Request.Headers != null)
            {
                Console.WriteLine("Request Headers: ");
                foreach (var h in context.Request.Headers)
                {
                    Console.WriteLine("--> " + h.Key + ": " + h.Value);
                }
            }

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
       
        private async Task SendMessageAsync(WebSocket ws, string msg)
        { 
            var json = JsonConvert.SerializeObject(truckFlow.GetLastEvents());
           // ArraySegment<Byte> arr = new(truckFlow.GetLastEvents().ToArray()); 
         var  buffer = Encoding.UTF8.GetBytes(json.ToCharArray());
            await ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);


        }

        public async Task RouteJSONMessagesAsync(string message)
        {
        //    var data = JsonConvert.DeserializeObject<MessageTransmis>(message);
            if (true)
            {
                WebSocket ws = _wsServerConnManager.GetSockets().First().Value;
                await SendMessageAsync(ws, message);
            }
            else
            {
                Console.WriteLine("BroadCast...");
                foreach (var ws in _wsServerConnManager.GetSockets())
                {
                    if (ws.Value.State==WebSocketState.Open)
                    {
                        await SendMessageAsync(ws.Value, message);
                    }
                    else
                    {
                        //remove from dict
                    }
                }
            }
        }
    }
}
