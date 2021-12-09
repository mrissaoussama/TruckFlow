﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace WebSocketServerProject.MidlleWare
{
    public class WebSocketServerConnectionManager
    {
        private ConcurrentDictionary<String, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
        public ConcurrentDictionary<String, WebSocket> GetSockets()
        {
            return _sockets;
        }
        public String AddSocket(WebSocket socket)
        {
            string conId = Guid.NewGuid().ToString();
            _sockets.TryAdd(conId, socket);
            return conId;
        }
    }
}
