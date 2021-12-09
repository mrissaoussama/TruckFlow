using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketServerProject.MidlleWare
{
    public static class WebSocketServerMiddleWareExtention
    {
        public static IApplicationBuilder UseWebSocketMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketServerMiddleWare>();
        }

        public static IServiceCollection AddWebSocketMiddleWare(this IServiceCollection services)
        {
            services.AddSingleton<WebSocketServerConnectionManager>();
            return services;
        }
    }
}
