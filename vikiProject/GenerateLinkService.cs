using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using PuppeteerSharp;
using vikiProject.Dto;
using vikiProject.Models;
using Constants = vikiProject.Models.Constants;

namespace vikiProject
{
    public class GenerateLinkService
    {
        public string Link { get; set; }

        public async Task GetManifest(StringDto url)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions {Headless = false}); // set headless = true todo add try catch

            var page = await browser.NewPageAsync();

            page.DefaultNavigationTimeout = Constants.DefaultNavigationTimeout;
            await page.SetUserAgentAsync(Constants.UserAgent);
            await page.SetRequestInterceptionAsync(true);

            // disable images to download

            page.Request += async (sender, e) =>
            {
                if (e.Request.ResourceType == ResourceType.Image ||
                    e.Request.ResourceType == ResourceType.Font ||
                    e.Request.ResourceType == ResourceType.StyleSheet ||
                    e.Request.ResourceType == ResourceType.Media)
                {
                    await e.Request.AbortAsync();
                }
                else if (e.Request.ResourceType == ResourceType.Fetch &&
                         e.Request.Url.StartsWith("https://manifest-viki.viki.io/v1/"))
                {
                    // page.SetRequestInterceptionAsync(false);
                    Link = e.Request.Url;
                    // await page.CloseAsync();
                    await browser.CloseAsync();
                    //todo throw new SafelyExitException("safely exited");
                }

                else
                {
                    await e.Request.ContinueAsync();
                }
            };
            
            try
            {
                await page.GoToAsync(url.String);
            }
            catch (NavigationException e)
            {
                Console.WriteLine("NavigationExceptionOccured");
            }
            
        }

        private static async Task<String> GetMpd(string link) // xml file includes another mpd file
        {
            var request = (HttpWebRequest) WebRequest.Create(link);
            request.UserAgent = Constants.UserAgent;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using var response = (HttpWebResponse) await request.GetResponseAsync();
            await using var stream = response.GetResponseStream();
            return await new StreamReader(stream).ReadToEndAsync();
        }

        public async Task<TwoStringDto> GetMpd2()
        {
            var xml1 = await GetMpd(Link);

            var regex = new Regex(@"<BaseURL>.+\.mpd<\/BaseURL>").Matches(xml1);
            

            if (regex.Count == 1)
            {
                var regex2 = new Regex(@"https:\/\/[\w|\.].+\/\d.+v\/dash\/\d").Match(xml1).Value;
               
                var regexValue = regex[0].Value;
                var xml2 = await GetMpd(regexValue[9..^10]);
                
                
                
                return new TwoStringDto(regex2[..^1], xml2);
            }

            
            return new TwoStringDto("", xml1);
        }
    }

// private async Task<string> DownloadLinkGenerator(string link)
// {
//     await GetManifest(link);
//     var xml = await GetXMl();
//     
// }
}