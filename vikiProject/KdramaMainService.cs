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

        public KdramaMainService(AppDbContext dbContext, AddDramaService addDramaService,
            GenerateLinkService generateLinkService)
        {
            _dbContext = dbContext;
            _addDramaService = addDramaService;
            _generateLinkService = generateLinkService;
        }


        public async Task<bool> AddDrama(string code)
        {
            var jObject = (await _addDramaService.GetDramaDetails(code)).JObject; // todo 


            if (jObject != null)
            {
                var dramaName = jObject["response"][0]["container"]["titles"]["hi"].ToString();
                var dramaImageSource = jObject["response"][0]["container"]["images"]["poster"]["url"].ToString();
                var noOfEpisodes =
                    int.Parse(jObject["response"][0]["container"]["planned_episodes"].ToString()); // todo 


                var drama = new Drama(dramaImageSource, dramaName, noOfEpisodes);
                await _dbContext.Dramas.AddAsync(drama);
                
                var episodes = new List<Episode>();
                for (var i = 0; i < noOfEpisodes; i++)
                {
                    var episodeNumber = int.Parse(jObject["response"][i]["number"].ToString());
                    var episodeImageSource = jObject["response"][i]["images"]["poster"]["url"].ToString();
                    var episodeSource = jObject["response"][i]["url"]["web"].ToString();
                    var episode = await AddEpisode(episodeNumber, episodeSource, episodeImageSource, drama);
                    episodes.Add(episode);
                }

                drama.Episodes = episodes;
                _dbContext.Dramas.Update(drama);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        #region MyRegion

        // public async Task<bool> UpdateDrama(UpdateDramaDto dramaDto)
        // {
        //     var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaDto.DramaName);
        //     if (drama != null)
        //     {
        //         drama.ImageSource = dramaDto.ImageSource;
        //         drama.NoOfEpisodes = dramaDto.NoOfEpisodes;
        //
        //         // await _dbContext.Dramas.AddAsync(drama);
        //         await _dbContext.SaveChangesAsync();
        //         return true;
        //     }
        //
        //     return false;
        // }


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

        private async Task<Episode> AddEpisode(int episodeNumber, string episodeSource, string imageSource, Drama drama)
        {
            var episode = new Episode(episodeNumber, imageSource, episodeSource);

            await _dbContext.Episodes.AddAsync(episode);

            return episode;
        }

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

        private async Task<string> GetEpisode(string name, int number)
        {
            var ss = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == name);
            var dd = ss.Episodes.FirstOrDefault(e => e.EpisodeNumber == number)?.EpisodeSource;

            return dd;

            return (await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == name))
                .Episodes.FirstOrDefault(e => e.EpisodeNumber == number)?.EpisodeSource;
        }

        public async Task<bool> AddDownloadLink(StringIntegerDto dramaEpNo)
        {
            ;
            await _generateLinkService.GetManifest(
                new StringDto(await GetEpisode(dramaEpNo.String, dramaEpNo.Number)));
            var xmlAndPrefix = await _generateLinkService.GetMpd2();
            int[] qualities = {240, 360, 480, 720, 1080};
            var list = new List<DownloadLink>();
            foreach (var qValue in qualities)
            {
                var pattern = $"<BaseURL>.+{qValue}p.+" + @"<\/BaseURL>";
                var regex = new Regex(pattern).Matches(xmlAndPrefix.String2);
                if (regex.Count == 2)
                {
                    var vLink = "";
                    var aLink = "";

                    if (regex[0].Value.Contains("video"))
                    {
                        //attach & strip & send to db

                        vLink = xmlAndPrefix.String1 + regex[0].Value[9..^10];
                    }
                    else
                    {
                        aLink = xmlAndPrefix.String1 + regex[1].Value[9..^10];
                    }


                    var link = new DownloadLink(aLink, vLink, (Quality) qValue);

                    list.Add(link);
                }
            }

            await SetEpisodeDownloadlinks(dramaEpNo.String, dramaEpNo.Number, list);


            return false;
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

        private async Task<bool> SetEpisodeDownloadlinks(string name, int number, List<DownloadLink> list)
        {
            var episode =
                (await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == name))
                .Episodes.FirstOrDefault(e => e.EpisodeNumber == number);
            if (episode != null)
            {
                episode.DownloadLinks = list;
                await _dbContext.SaveChangesAsync();
                return true;
            }


            return false; // todo
        }
    }
}