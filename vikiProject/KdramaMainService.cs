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


        public async Task<StringIntegerDto> AddDramaFromJObject(StringIntegerDto codeAndName) // check drama exits todo
        {
            var jObject = (await _addDramaService.GetDramaDetailsAsJObject(new IntegerDto(codeAndName.Number))).JObject;

            if (jObject.response.Count > 5)
            {
                var dramaId = codeAndName.Number;
                var dramaName = jObject.response[0].container.titles.en ?? jObject.response[0].container.titles.hi;
                var dramaImageSource = jObject.response[0].container.images.poster.url;
                var noOfEpisodes = jObject.response[0].container.planned_episodes;

                var episodes = new List<Episode>();

                for (var i = 0; i < noOfEpisodes; i++)
                {
                    var downloadLinks = _qualities.Select(qValue => new DownloadLink((Quality) qValue)).ToList();
                    var episodeNumber = jObject.response[i].number;
                    var episodeImageSource = jObject.response[i].images.poster.url;
                    var episodeSource = jObject.response[i].url.web;

                    var episode = new Episode(episodeNumber, episodeImageSource, episodeSource)
                    {
                        DownloadLinks = downloadLinks
                    };
                    episodes.Add(episode);
                }


                var otherNames = new List<OtherName>();
                otherNames.Add(new OtherName(dramaId, dramaName));
                otherNames.Add(new OtherName(dramaId, codeAndName.String));


                var drama = new Drama(dramaImageSource, dramaName, noOfEpisodes, dramaId)
                {
                    Episodes = episodes,
                    OtherNames = otherNames
                };

                await _dbContext.Dramas.AddAsync(drama); // check drma exits

                await _dbContext.SaveChangesAsync();
                return (new StringIntegerDto(dramaName, noOfEpisodes));
            }

            return null;
        }

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

        public async Task<bool> AddDramaFromstring(StringDto code)
        {
            var dramaDetails = (await _addDramaService.GetDramaDetailsAsString(code)).String;

            if (dramaDetails != null)
            {
                var dramaId = int.Parse(code.String);
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
                AddEpisodeToList(episodes, findEpisode1Pattern, dramaDetails, episodeNumberPattern, episodePathPattern,
                    episodeImageSourcePattern);

                for (int i = 2; i < noOfEpisodes; i++)
                {
                    var findEpisodesPattern = $",\"number\":{i},\".+,\"number\":{i + 1},\""; // todo epi No 0
                    AddEpisodeToList(episodes, findEpisodesPattern, dramaDetails, episodeNumberPattern,
                        episodePathPattern, episodeImageSourcePattern);
                }

                AddEpisodeToList(episodes, ",\"number\":16,\".+", dramaDetails, episodeNumberPattern,
                    episodePathPattern, episodeImageSourcePattern);

                var otherNames = new List<OtherName>();
                otherNames.Add(new OtherName(dramaId, dramaName));
                var drama = new Drama(dramaImageSource, dramaName, noOfEpisodes, dramaId)
                {
                    Episodes = episodes,
                    OtherNames = otherNames
                };

                await _dbContext.Dramas.AddAsync(drama);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<ListDto> SearchDramaName(StringDto searchTerm)
        {
            var list = await _dbContext.OtherNames.Where(d => d.Name.Contains(searchTerm.String))
                .Select(s =>new Tuple<int, string>(s.DramaId,s.Drama.MainName)).ToListAsync();
            return list.Count > 0 ? new ListDto(list) : null;
        }

        public async Task<IntegerDto> GetNoOfEpisodes(IntegerDto dramaId)
        {
            if (dramaId != null)
            {
                var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.DramaId == dramaId.Number);
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

                return new StringDto(
                    (await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaEpNo.String))
                    .Episodes.FirstOrDefault(e => e.EpisodeNumber == dramaEpNo.Number)
                    ?.ImageSource);
            }

            return null; //return error @todo
        }

        private Episode GetEpisode(int id, int number)
        {
            var episode = _dbContext.Episodes.Where(e => e.Drama.DramaId == id)
                .FirstOrDefault(e => e.EpisodeNumber == number);

            return episode;
        }

        public async Task<int> AddDownloadLink(TwointDto dramaEpNo)
        {
            var episode = GetEpisode(dramaEpNo.interger1, dramaEpNo.interger2);
            try
            {
                await _generateLinkService.GetManifest(new StringDto(episode.EpisodeSource));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw; // todo
            }

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


                    await SetEpisodeDownloadlink((Quality) qValue, episode, aLink, vLink); // todo set event
                }
            }


            await _dbContext.SaveChangesAsync();
            return episode.EpisodeNumber;
        }

        public async Task<TwoStringDto> GetEpisodeDownloadlinks(GetDownloadLinkDto getLink)
        {
            if (getLink != null)
            {
                var dLink = await _dbContext.DownloadLinks.Where(e =>
                        e.DramaId == getLink.Did && e.EpisodeNumber == getLink.Eid && e.Quality == getLink.Quality)
                    .FirstOrDefaultAsync();


                if (dLink != null && DateTime.Now.Second - dLink.AddedTime.Second < Constants.LinkExpiryTime)
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