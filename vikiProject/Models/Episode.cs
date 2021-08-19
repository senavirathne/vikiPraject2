using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vikiProject.Models
{
    public class Episode
    {
        [Required] public int EpisodeNumber { get; }

        [Required] public string ImageSource { get; }

        [Required] public string EpisodeSource { get; }

        public List<DownloadLink> DownloadLinks { get; set; }
        
       
        public int DramaId { get; set; }

        public Episode()
        {
        }

        public Episode(int episodeNumber, string imageSource, string episodeSource)
        {
            EpisodeNumber = episodeNumber;

            ImageSource = imageSource;
            EpisodeSource = episodeSource;
        }
    }
}