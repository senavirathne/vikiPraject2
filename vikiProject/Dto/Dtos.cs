using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using vikiProject.Models;

namespace vikiProject.Dto

{
    public record JsonDto(JObject JObject);


    // public record UpdateDramaDto(string DramaName,int NoOfEpisodes, string ImageSource);
    // public record AddEpisodeDto(Drama Drama, int EpisodeNumber, string EpisodeSource, string ImageSource);
    
    public record StringDto(string String);

    public record TwoStringDto(string String1, string String2); //todo is it better to use tuple 

    public record SetDramaNameDto(string MainName, string OtherName);


    public record IntegerDto(int Number);

    public record StringIntegerDto(string String, int Number);

    

    public record GetDownloadLinkDto(string String, int Number, Quality Quality);

  
}