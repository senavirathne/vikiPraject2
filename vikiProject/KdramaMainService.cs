using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vikiProject.Dto;
using vikiProject.Models;

namespace vikiProject
{
    public class KdramaMainService
    {
        private readonly AppDbContext _dbContext;
        private readonly AddDramaService _addDramaService;
        private readonly GenerateLinkService _generateLinkService;
        private readonly int[] _qualities = {240, 360, 480, 720, 1080};

        public KdramaMainService(AppDbContext dbContext, AddDramaService addDramaService,
            GenerateLinkService generateLinkService)
        {
            _dbContext = dbContext;
            _addDramaService = addDramaService;
            _generateLinkService = generateLinkService;
        }


        // public async Task<bool> AddDrama(string code) // check drama exits todo
        // {
        // var jObject = (await _addDramaService.GetDramaDetails(code)).JObject;
        // var json = (await _addDramaService.GetDramaDetailsFromString(code)).String;// todo 


        // if (jObject != null)
        // {
        //     var dramaName = jObject["response"][0]["container"]["titles"]["hi"].ToString();
        //     var dramaImageSource = jObject["response"][0]["container"]["images"]["poster"]["url"].ToString();
        //     var noOfEpisodes =
        //         int.Parse(jObject["response"][0]["container"]["planned_episodes"].ToString()); // todo 
        //
        //
        //     var episodes = new List<Episode>();
        //
        //
        //     for (var i = 0; i < noOfEpisodes; i++)
        //     {
        //         var links = new List<DownloadLink>();
        //         foreach (var qValue in _qualities)
        //         {
        //             
        //             var link = new DownloadLink((Quality) qValue);
        //             links.Add(link);
        //         }
        //
        //         var episodeNumber = int.Parse(jObject["response"][i]["number"].ToString());
        //         var episodeImageSource = jObject["response"][i]["images"]["poster"]["url"].ToString();
        //         var episodeSource = jObject["response"][i]["url"]["web"].ToString();
        //          
        //         var episode = new Episode(episodeNumber, episodeImageSource, episodeSource)
        //         {
        //             DownloadLinks = links
        //         };
        //         episodes.Add(episode);
        //     }
        //
        //     var drama = new Drama(dramaImageSource, dramaName, noOfEpisodes) {Episodes = episodes};
        //     await _dbContext.Dramas.AddAsync(drama);
        //
        //     await _dbContext.SaveChangesAsync();
        //     return true;
        // }
        // if (json != null)
        // {
        //     var dramaName = jObject["response"][0]["container"]["titles"]["hi"].ToString();
        //     var dramaImageSource = jObject["response"][0]["container"]["images"]["poster"]["url"].ToString();
        //     var noOfEpisodes =
        //         int.Parse(jObject["response"][0]["container"]["planned_episodes"].ToString()); // todo 
        //
        //
        //     var episodes = new List<Episode>();
        //
        //
        //     for (var i = 0; i < noOfEpisodes; i++)
        //     {
        //         var links = new List<DownloadLink>();
        //         foreach (var qValue in _qualities)
        //         {
        //             
        //             var link = new DownloadLink((Quality) qValue);
        //             links.Add(link);
        //         }
        //
        //         var episodeNumber = int.Parse(jObject["response"][i]["number"].ToString());
        //         var episodeImageSource = jObject["response"][i]["images"]["poster"]["url"].ToString();
        //         var episodeSource = jObject["response"][i]["url"]["web"].ToString();
        //          
        //         var episode = new Episode(episodeNumber, episodeImageSource, episodeSource)
        //         {
        //             DownloadLinks = links
        //         };
        //         episodes.Add(episode);
        //     }
        //
        //     var drama = new Drama(dramaImageSource, dramaName, noOfEpisodes) {Episodes = episodes};
        //     await _dbContext.Dramas.AddAsync(drama);
        //
        //     await _dbContext.SaveChangesAsync();
        //     return true;
        // }
        //
        // return false;
        // }
        private void AddEpisodeToList(List<Episode> episodes, string findEpisodesPattern, string dramaDetails,
            string episodeNumberPattern, string episodePathPattern, string episodeImageSourcePattern)
        {
            var episodeString =
                new Regex(findEpisodesPattern).Match(dramaDetails).Value; // need only to change value todo

            var episodeNumber = int.Parse(new Regex(episodeNumberPattern).Match(episodeString).Value[9..]);

            var episodeSource =
                new Regex(episodePathPattern).Match(episodeString).Value[8..]; // need only to change value todo
            var episodeImageSource =
                new Regex(episodeImageSourcePattern).Match(episodeString)
                    .Value[34..]; // need only to change value todo

            var links = new List<DownloadLink>();
            foreach (var qValue in _qualities)
            {
                var link = new DownloadLink((Quality) qValue);
                links.Add(link);
            }

            var episode = new Episode(episodeNumber, episodeImageSource, episodeSource)
            {
                DownloadLinks = links
            };
            episodes.Add(episode);
        }

        public async Task<bool> AddDrama(StringDto code)
        {
            var dramaDetails = (await _addDramaService.GetDramaDetailsAsString(code)).String;

            if (dramaDetails != null)
            {
                const string findEpisode1Pattern = ",\"number\":1,\".+,\"number\":2,\"";
                var episode1 = new Regex(findEpisode1Pattern).Match(dramaDetails);


                const string dramaNamePattern = "hi\":\"[\\w|\\s]+";
                const string dramaImageSourcePattern =
                    "json\"}}],\"images\":{\"poster\":{\"url\":\"https:\\/\\/[\\/|\\.|\\w|\\d\\&\\?\\=]+";
                const string noOfEpisodesPattern = "\"planned_episodes\":\\d+";

                const string episodeImageSourcePattern =
                    "\"viki\",\"images\":{\"poster\":{\"url\":\"https:\\/\\/[\\/|\\.|\\w|\\d\\&\\?\\=]+";
                const string episodePathPattern = ",\"web\":\"https:\\/\\/www\\.viki\\.com\\/videos\\/[\\d|\\w-]+";
                const string episodeNumberPattern = "\"number\":\\d+";

                var dramaName = new Regex(dramaNamePattern).Match(episode1.Value).Value[5..];
                var dramaImageSource = new Regex(dramaImageSourcePattern).Match(episode1.Value).Value[36..];
                var noOfEpisodes = int.Parse(new Regex(noOfEpisodesPattern).Match(episode1.Value).Value[19..]);

                var episodes = new List<Episode>();
                AddEpisodeToList(episodes,findEpisode1Pattern,dramaDetails,episodeNumberPattern,episodePathPattern,episodeImageSourcePattern);

                for (int i = 2; i < noOfEpisodes; i++)
                {
                    var findEpisodesPattern = $",\"number\":{i},\".+,\"number\":{i + 1},\""; // todo epi No 0
                    AddEpisodeToList(episodes,findEpisodesPattern,dramaDetails,episodeNumberPattern,episodePathPattern,episodeImageSourcePattern);
                }
                AddEpisodeToList(episodes,",\"number\":16,\".+",dramaDetails,episodeNumberPattern,episodePathPattern,episodeImageSourcePattern);

                var drama = new Drama(dramaImageSource, dramaName, noOfEpisodes) {Episodes = episodes};
                await _dbContext.Dramas.AddAsync(drama); //todo check drama exits

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        #region MyRegion

        // public async Task<bool> SetOtherNames(SetDramaNameDto dramaName)
        // {
        //     // var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaName.MainName);
        //     // if (drama != null)
        //     // {
        //     //     drama.OtherNames.Add(dramaName.OtherName);
        //     //
        //     //     await _dbContext.SaveChangesAsync();
        //     //     return true;
        //     // }
        //     //
        //     return false;
        // } //todo

        #endregion

        // private async Task<Episode> AddEpisode(int episodeNumber, string episodeSource, string imageSource, Drama drama)
        // {
        //     var episode = new Episode(episodeNumber, imageSource, episodeSource);
        //
        //     await _dbContext.Episodes.AddAsync(episode);
        //
        //     return episode;
        // }

        public async Task<IEnumerable<StringDto>> SearchDramaName(StringDto searchTerm)
        {
            if (searchTerm != null)
            {
                return (await _dbContext.Dramas.Where(d => d.MainName.Contains(searchTerm.String)).ToListAsync())
                    .Select(d => new StringDto(d.MainName));
            }

            return null; // @todo
        }

        public async Task<IntegerDto> GetNoOfEpisodes(StringDto dramaName)
        {
            if (dramaName != null)
            {
                var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaName.String);
                return new IntegerDto(drama.NoOfEpisodes);
            }

            return null; //return error @todo
        }

        #region MyRegion

        // public async Task<GetEpisodeListDto> GetEpisodes(string name)
        // {
        //     if (!string.IsNullOrEmpty(name))
        //     {
        //         var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == name);
        //         return new GetEpisodeListDto(new List<string>(drama.Episodes.Select(d =>d.EpisodeSource)));
        //     }
        //     return null; //return error @todo
        // }

        #endregion

        public async Task<StringDto> GetDramaImageSource(StringDto dramaName)
        {
            if (dramaName != null)
            {
                var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaName.String);
                return new StringDto(drama.ImageSource);
            }

            return null; //return error @todo
        }

        public async Task<StringDto> GetEpisodeImageSource(StringIntegerDto dramaEpNo)
        {
            if (dramaEpNo != null)
            {
                // var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaEpNo.String);

                return new StringDto((await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaEpNo.String))
                    .Episodes.FirstOrDefault(e => e.EpisodeNumber == dramaEpNo.Number)
                    ?.ImageSource);
            }

            return null; //return error @todo
        }

        private Episode GetEpisode(string name, int number)
        {
            var episode = _dbContext.Episodes.Where(e => e.Drama.MainName == name)
                .FirstOrDefault(e => e.EpisodeNumber == number);

            return episode;
        }

        public async Task<bool> AddDownloadLink(StringIntegerDto dramaEpNo)
        {
            var episode = GetEpisode(dramaEpNo.String, dramaEpNo.Number);
            await _generateLinkService.GetManifest(
                new StringDto(episode.EpisodeSource));
            var xmlAndPrefixOfLinks = await _generateLinkService.GetMpd2();

            foreach (var qValue in _qualities)
            {
                var pattern = $"<BaseURL>.+{qValue}p.+" + @"<\/BaseURL>";
                var regex = new Regex(pattern).Matches(xmlAndPrefixOfLinks.String2);
                if (regex.Count == 2)
                {
                    var vLink = "";
                    var aLink = "";

                    if (regex[0].Value.Contains("video"))
                    {
                        //attach & strip & send to db

                        vLink = xmlAndPrefixOfLinks.String1 + regex[0].Value[9..^10];
                        aLink = xmlAndPrefixOfLinks.String1 + regex[1].Value[9..^10];
                    }
                    else
                    {
                        vLink = xmlAndPrefixOfLinks.String1 + regex[1].Value[9..^10];
                        aLink = xmlAndPrefixOfLinks.String1 + regex[0].Value[9..^10];
                    }


                    await SetEpisodeDownloadlink((Quality) qValue, episode, aLink, vLink);
                }
            }


            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TwoStringDto> GetEpisodeDownloadlinks(GetDownloadLinkDto getLink)
        {
            if (getLink != null)
            {
                var dLink = (await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == getLink.String))
                    .Episodes.FirstOrDefault(e => e.EpisodeNumber == getLink.Number)?.DownloadLinks
                    .FirstOrDefault(l => l.Quality == getLink.Quality);
                if (dLink != null && DateTime.Now.Second - dLink.AddedTime.Second > Constants.LinkExpiryTime)
                {
                    return new TwoStringDto(dLink.VideoLink, dLink.AudioLink);
                }
            }

            return null; // todo
        }

        private async Task<bool> SetEpisodeDownloadlink(Quality quality, Episode episode, string audioLink,
            string videoLink)
        {
            var link = await _dbContext.DownloadLinks.Where(l => l.Episode == episode)
                .FirstOrDefaultAsync(l => l.Quality == quality);
            if (link != null)
            {
                link.AddedTime = DateTime.Now; // utcs?? todo
                link.AudioLink = audioLink;
                link.VideoLink = videoLink;
                _dbContext.DownloadLinks.Update(link);

                return true;
            }


            return false; // todo
        }
    }
}