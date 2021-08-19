using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vikiProject.Models
{
    public class Episode
    {
        public int EpisodeNumber { get; }

         public string ImageSource { get; }

       public string EpisodeSource { get; }

        public List<DownloadLink> DownloadLinks { get; set; }
        public Drama Drama { get; set; }
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