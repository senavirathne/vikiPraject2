using System;
using System.Collections.Generic;
using vikiProject.Models;

namespace vikiProject.Dto

{
    public record AddDramaDto(Guid Id, string Name);

    public record UpdateDramaDto(string DramaName,int NoOfEpisodes, string ImageSource);
    public record AddEpisodeDto(Guid Id,int EpisodeNumber, string EpisodeSource, string ImageSource,string AudioLink
        ,string VideoLink );

    public record StringDto(string String);
    public record IntegerDto(int Number);
    public record StringIntegerDto(string String,int Number);
    public record GetDownloadLinkDto(DownloadLink DownloadLink);
    public record AddDownloadLinkDto(string AudioLink, string VideoLink,string DramaName,int EpiNumber);
    
    
    public record GetEpisodeListDto(List<string> List);

}