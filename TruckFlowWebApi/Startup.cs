
using DAO.IDAO;
using DAOExpressSqlEF6.DAONS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TruckFlowWebApi.Controllers;
using TruckFlowWebApi.Interface;
using TruckFlowWebApi.Model;
using WebSocketServerProject.MidlleWare;

namespace TruckFlowWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
       
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebSocketMiddleWare();
            services.AddSingleton<WebSocketServerConnectionManager>();
            services.AddSingleton<ICarCheck, CarCheck>();
            services.AddSingleton<IDAOEvent, DAOEvent>();
            services.AddSingleton<ITruckFlow, TruckFlow>();

            //services.AddTransient<TruckFlow>();
            // services.AddSingleton<TruckFlow>();

            services.AddCors(o => o.AddDefaultPolicy( builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyHeader()
                                                  .AllowAnyMethod(); 

            }));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TruckFlowWebApi", Version = "v1" });
            });
           // services.AddSignalR();
     
                


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TruckFlowWebApi v1"));
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseWebSockets();
            app.UseWebSocketMiddleWare();

            // app.UseSignalR();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
