using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using PuppeteerSharp;
using Constants = vikiProject.Models.Constants;

namespace vikiProject
{
    public class GenerateLinkService
    {
    
        public async Task GetManifest(string url)
        {

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false
            });

            await using (var page = await browser.NewPageAsync())
            {
                await page.SetUserAgentAsync(Constants.UserAgent);
                await page.SetRequestInterceptionAsync(true);

                // disable images to download
                page.Request += async (sender, e) =>
                {
                    if (e.Request.ResourceType == ResourceType.Image || e.Request.ResourceType == ResourceType.Font ||
                        e.Request.ResourceType == ResourceType.StyleSheet ||
                        e.Request.ResourceType == ResourceType.Media)
                    {
                        await e.Request.AbortAsync();
                    }
                    else if (e.Request.ResourceType == ResourceType.Fetch &&
                             e.Request.Url.StartsWith("https://manifest-viki.viki.io/v1/"))
                    {
                       
                        // page.SetRequestInterceptionAsync(false);
                        await DownloadLinkGenerator(e.Request.Url);
                        await browser.CloseAsync(); //todo page close instead
                      
                    }

                    else
                    {
                        await e.Request.ContinueAsync();
                    }
                };
                await page.GoToAsync(url);
            }

            await browser.CloseAsync();
           
        }

        private async Task DownloadLinkGenerator(string link)
        {
           
            var request = (HttpWebRequest) WebRequest.Create(link);
            request.UserAgent = Constants.UserAgent;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse) await request.GetResponseAsync();
            await using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream).ReadToEndAsync();
            XDocument x = XDocument.Parse(await reader);
            
        }
    }
}