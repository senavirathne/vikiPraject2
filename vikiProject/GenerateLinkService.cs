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
            string link;
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false
            });

            var page = await browser.NewPageAsync();
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
                        link = e.Request.Url;
                        await page.SetRequestInterceptionAsync(false);
                        await page.CloseAsync();

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

        private async Task<string> GetXMl(string link)
        {
           
            var request = (HttpWebRequest) WebRequest.Create(link);
            request.UserAgent = Constants.UserAgent;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse) await request.GetResponseAsync();
            await using var stream = response.GetResponseStream();
            return await new StreamReader(stream).ReadToEndAsync();

        }

        private async Task<string> DownloadLinkGenerator(string link)
        {
            
        }
    }
}