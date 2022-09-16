using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;
namespace middleware.Middlewares
{
    public class HMiddleware
    {
        private readonly RequestDelegate _next;
        public HMiddleware(RequestDelegate next)
        {
           _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Hello World");
            await _next.Invoke(context);
            Console.WriteLine("Bye World");           
        }
    }
     static public class HMiddlewareEntension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HMiddleware>();
        }
    }
}