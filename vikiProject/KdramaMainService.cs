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

        public KdramaMainService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddDrama(AddDramaDto dramaDto)
        {
            var drama = new Drama()
            {
                Id = Guid.NewGuid(),
                Name = dramaDto.Name,
                // ImageSource = dramaDto.ImageSource,
                // NoOfEpisodes = dramaDto.NoOfEpisodes
            };
            await _dbContext.Dramas.AddAsync(drama);
            await _dbContext.SaveChangesAsync();
            return drama.Id;
        }

        public async Task<bool> UpdateDrama(UpdateDramaDto dramaDto)
        {
            var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaDto.DramaName);
            if (drama != null)
            {
                drama.ImageSource = dramaDto.ImageSource;
                drama.NoOfEpisodes = dramaDto.NoOfEpisodes;

                // await _dbContext.Dramas.AddAsync(drama);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Guid> AddEpisode(AddEpisodeDto episodeDto)
        {
            var episode = new Episode()
            {
                Id = Guid.NewGuid(),
                EpisodeNumber = episodeDto.EpisodeNumber,
                EpisodeSource = episodeDto.EpisodeSource,
                ImageSource = episodeDto.ImageSource,
                // DownloadLink = new DownloadLink(episodeDto.AudioLink, episodeDto.VideoLink)
            };
            await _dbContext.Episodes.AddAsync(episode);
            await _dbContext.SaveChangesAsync();
            return episode.Id;
        }

        public async Task<IEnumerable<StringDto>> SearchDramaName(StringDto searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm.String))
            {
                return (await _dbContext.Dramas.Where(d => d.Name.Contains(searchTerm.String)).ToListAsync())
                    .Select(d => new StringDto(d.Name));
            }

            return null; // @todo
        }

        public async Task<IntegerDto> GetNoOfEpisodes(StringDto dramaName)
        {
            if (!string.IsNullOrEmpty(dramaName.String))
            {
                var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaName.String);
                return new IntegerDto(drama.NoOfEpisodes);
            }

            return null; //return error @todo
        }

        // public async Task<GetEpisodeListDto> GetEpisodes(string name)
        // {
        //     if (!string.IsNullOrEmpty(name))
        //     {
        //         var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == name);
        //         return new GetEpisodeListDto(new List<string>(drama.Episodes.Select(d =>d.EpisodeSource)));
        //     }
        //     return null; //return error @todo
        // }
        public async Task<StringDto> GetDramaImageSource(StringDto dramaName)
        {
            if (!string.IsNullOrEmpty(dramaName.String))
            {
                var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaName.String);
                return new StringDto(drama.ImageSource);
            }

            return null; //return error @todo
        }

        public async Task<StringDto> GetEpisodeImageSource(StringIntegerDto dramaEpNo)
        {
            if (!string.IsNullOrEmpty(dramaEpNo.String))
            {
                // var drama = await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaEpNo.String);

                return new StringDto((await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaEpNo.String))
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

                return new StringDto((await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaEpNo.String))
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
                    (await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == dramaEpNo.String)).Episodes
                    .FirstOrDefault(e => e.EpisodeNumber == dramaEpNo.Number)
                    ?.DownloadLink);
            }

            return null; //return error @todo
        }

        public async Task<bool> AddDownloadLink(AddDownloadLinkDto addDownloadLink)
        {
            if (addDownloadLink != null)
            {
                var episode = (await _dbContext.Dramas.FirstOrDefaultAsync(d => d.Name == addDownloadLink.DramaName))
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