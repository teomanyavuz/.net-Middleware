using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using middleware.Middlewares;

namespace middleware
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "middleware", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "middleware v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
 
            

           // app.Run(async context => Console.WriteLine("MiddleWire 1"));
           // app.Run(async context => Console.WriteLine("MiddleWire 2"));

        //    app.Use(async(context,next)=>{
            
        //     Console.WriteLine("md 1 başladı");
        //     await next.Invoke();
        //     Console.WriteLine("md 1 sonlandırıyolıyor");
            
        //    });

        //      app.Use(async(context,next)=>{
            
        //     Console.WriteLine("md 2 başladı");
        //     await next.Invoke();
        //     Console.WriteLine("md 2 sonlandırıyolıyor");
            
        //    });

        //      app.Use(async(context,next)=>{
            
        //     Console.WriteLine("md 3 başladı");
        //     await next.Invoke();
        //     Console.WriteLine("md 3 sonlandırıyolıyor");
            
        //    });
          
                  app.UseHello();
                  app.Use(async(context,next)=>
                  {
                     Console.WriteLine("use md  tetiklendi");
                     await next.Invoke();
                     
                  });

                  //map

                  app.Map("/example", InternalApp=>
                  InternalApp.Run(async context=>
                  {
                      Console.WriteLine("/example mw tetiklendi");
                      await context.Response.WriteAsync("/example mw tetiklendi");
                  }));

                  //Mapwhen

                  app.MapWhen(x=>x.Request.Method=="GET",InternalApp=>
                  {
                    InternalApp.Run(async context => 
                    {                 
                    Console.WriteLine(" mapwhen md tetiklendi");
                    await context.Response.WriteAsync("mapwhen md tetiklendi");
                    });

                  });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
