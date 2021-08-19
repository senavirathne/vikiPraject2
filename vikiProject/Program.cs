
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Hosting;
using vikiProject.Dto;
using vikiProject.Models;


namespace vikiProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

          //   var url = "https://www.viki.com/videos/67918v-playful-kiss-episode-1";
          //   var x = new GenerateLinkService();
          //   await x.GetManifest(new StringDto(url));
          // var  xx = await x.GetMpd2();
          // Console.WriteLine(xx.Tuple.xml);

          
          

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}