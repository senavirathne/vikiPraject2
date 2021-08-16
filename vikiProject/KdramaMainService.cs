using System;
using System.Collections.Generic;
using System.Linq;
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

        public KdramaMainService(AppDbContext dbContext, AddDramaService addDramaService)
        {
            _dbContext = dbContext;
            _addDramaService = addDramaService;
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
                
                var drama = new Drama()
                {
                    Id = Guid.NewGuid(),
                    MainName = dramaName,
                    ImageSource = dramaImageSource,
                    NoOfEpisodes = noOfEpisodes
                };
                await _dbContext.Dramas.AddAsync(drama);
                for (var i = 0; i < noOfEpisodes; i++)
                {
                    var episodeNumber = int.Parse(jObject["response"][i]["number"].ToString());
                    var episodeImageSource = jObject["response"][i]["images"]["poster"]["url"].ToString();
                    var episodeSource = jObject["response"][i]["url"]["web"].ToString();
                    await AddEpisode(episodeNumber, episodeSource, episodeImageSource, drama); // check if true todo 
                }

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

        #endregion

        public async Task<bool> SetOtherNames(SetDramaNameDto dramaName)
        {
            // var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaName.MainName);
            // if (drama != null)
            // {
            //     drama.OtherNames.Add(dramaName.OtherName);
            //
            //     await _dbContext.SaveChangesAsync();
            //     return true;
            // }
            //
            return false;
        }

        private async Task<Guid> AddEpisode(int episodeNumber, string episodeSource, string imageSource, Drama drama)
        {
            var episode = new Episode()
            {
                Id = Guid.NewGuid(),
                EpisodeNumber = episodeNumber,
                EpisodeSource = episodeSource,
                ImageSource = imageSource,
                Drama = drama
            };
            await _dbContext.Episodes.AddAsync(episode);

            return episode.Id;
        }

        public async Task<IEnumerable<StringDto>> SearchDramaName(StringDto searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm.String))
            {
                return (await _dbContext.Dramas.Where(d => d.MainName.Contains(searchTerm.String)).ToListAsync())
                    .Select(d => new StringDto(d.MainName));
            }

            return null; // @todo
        }

        public async Task<IntegerDto> GetNoOfEpisodes(StringDto dramaName)
        {
            if (!string.IsNullOrEmpty(dramaName.String))
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
            if (!string.IsNullOrEmpty(dramaName.String))
            {
                var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaName.String);
                return new StringDto(drama.ImageSource);
            }

            return null; //return error @todo
        }

        public async Task<StringDto> GetEpisodeImageSource(StringIntegerDto dramaEpNo)
        {
            if (!string.IsNullOrEmpty(dramaEpNo.String))
            {
                // var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaEpNo.String);

                return new StringDto((await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaEpNo.String))
                    .Episodes.FirstOrDefault(e => e.EpisodeNumber == dramaEpNo.Number)
                    ?.ImageSource);
            }

            return null; //return error @todo
        }

        public async Task<StringDto> GetEpisodeSource(StringIntegerDto dramaEpNo)
        {
            if (!string.IsNullOrEmpty(dramaEpNo.String))
            {
                // var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaEpNo.String); //join linq @todo

                return new StringDto((await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaEpNo.String))
                    .Episodes.FirstOrDefault(e => e.EpisodeNumber == dramaEpNo.Number)
                    ?.EpisodeSource);
            }

            return null; //return error @todo
        }

        public async Task<GetDownloadLinkDto> GetDownloadLink(StringIntegerDto dramaEpNo)
        {
            if (!string.IsNullOrEmpty(dramaEpNo.String))
            {
                // var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaEpNo.String);

                return new GetDownloadLinkDto(
                    (await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == dramaEpNo.String)).Episodes
                    .FirstOrDefault(e => e.EpisodeNumber == dramaEpNo.Number)
                    ?.DownloadLink);
            }

            return null; //return error @todo
        }

        public async Task<bool> AddDownloadLink(AddDownloadLinkDto addDownloadLink)
        {
            if (addDownloadLink != null)
            {
                var episode =
                    (await _dbContext.Dramas.FirstOrDefaultAsync(d => d.MainName == addDownloadLink.DramaName))
                    .Episodes.FirstOrDefault(e => e.EpisodeNumber == addDownloadLink.EpiNumber);
                if (episode != null)
                {
                    episode.DownloadLink = new DownloadLink(addDownloadLink.AudioLink, addDownloadLink.VideoLink);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }

            return false; //return error @todo
        }
    }
}