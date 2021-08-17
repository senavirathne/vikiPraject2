
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Hosting;


namespace vikiProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();

            var url = "https://www.viki.com/videos/67918v-playful-kiss-episode-1";
            var x = new GenerateLinkService();
            await x.GetManifest(url);


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}