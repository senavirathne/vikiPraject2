using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using vikiProject.Models;

namespace vikiProject.Dto

{
    public record JsonDto(JObject JObject);
    

    // public record UpdateDramaDto(string DramaName,int NoOfEpisodes, string ImageSource);
    public record AddEpisodeDto(Drama Drama, int EpisodeNumber, string EpisodeSource, string ImageSource );

    public record StringDto(string String);
    
    public record TupleDto((string prefix,string xml) Tuple);
    public record SetDramaNameDto(string MainName, string OtherName);
    
    
    public record IntegerDto(int Number);
    public record StringIntegerDto(string String,int Number);
    public record GetDownloadLinkDto(DownloadLink DownloadLink);
    public record AddDownloadLinkDto(string AudioLink, string VideoLink,string DramaName,int EpiNumber);
    
    
    public record GetEpisodeListDto(List<string> List);

}