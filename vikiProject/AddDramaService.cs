using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using vikiProject.Dto;
using vikiProject.Models;


namespace vikiProject
{
    public class AddDramaService
    {
        private async Task<string> FindDrama(string drama)
        {
            var uri = "https://www.google.com/search?q=site%3Aviki.com%20" + drama.Trim().Replace(" ", "%20");
            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.UserAgent = Constants.UserAgent;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse) await request.GetResponseAsync();
            await using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private async Task<List<string>> GetDrama(string drama)
        {
            var body = await FindDrama(drama);
            var regex = new Regex(@"(?<=https:\/\/www\.viki\.com\/tv\/)\d+c[-|\w]+");
            return regex.Matches(body).Select(m => m.Value).Distinct().ToList();
        }

// user search on search bar todo add drama name to other names
        public async Task<List<(int code, string name)>> GetDramaNameswithCodes(StringDto drama)
        {
            var dramaList = await GetDrama(drama.String);
            var regex = new Regex(@"\d+c-[-|\w]+");
            var list = new List<(int, string)>();
            foreach (var d in dramaList)
            {
                if (regex.Matches(d).Count == 1)
                {
                    var x = d.Split("c-");
                    list.Add((int.Parse(x[0]), x[1].Replace("-", " ")));
                }
            }

            return list; // @todo return erorr
        }

// add details to dataBase
        public async Task<StringDto> GetDramaDetailsAsString(StringDto code)
        {
            var uri =
                $"https://api.viki.io/v4/containers/{code.String}c/episodes.json?token=undefined&direction=asc&blocked=false&only_ids=false&per_page={Constants.PerPage}&app=100000a";
            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.UserAgent = Constants.UserAgent;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse) await request.GetResponseAsync(); // todo use http client
            await using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream).ReadToEndAsync();
            return new StringDto(await reader);
        }

        


        public async Task<JsonDto> GetDramaDetailsAsJObject(IntegerDto code)
        {
            var uri =
                $"https://api.viki.io/v4/containers/{code.Number}c/episodes.json?token=undefined&direction=asc&blocked=false&only_ids=false&per_page={Constants.PerPage}&app=100000a";
            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.UserAgent = Constants.UserAgent;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using var response = (HttpWebResponse) await request.GetResponseAsync(); // todo use http client
            await using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream).ReadToEndAsync();
            var x = await reader;
            return new JsonDto(JsonSerializer.Deserialize<Json>(await reader));


            // return new JsonDto(JObject.Parse(await reader));
        }
    }
}