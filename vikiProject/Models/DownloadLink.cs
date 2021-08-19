using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.Net.Http.Headers;

namespace vikiProject.Models
{
    public enum Quality

    {
        P240 = 240,
        P360 = 360,
        P480 = 480,
        P720 = 720,
        P1080 = 1080
    }

    public class DownloadLink
    {
        [Required] public string AudioLink { get;  }

        [Required] public string VideoLink { get;  }
        [Required] public Quality Quality { get;  }
        [Required] public DateTime AddedTime { get; }
        public int EpisodeNumber { get; }
        public int DramaId { get; set; }
        
        [ForeignKey("EpisodeNumber","DramaId")]
        public virtual Episode Episode { get; set; }
        

        [Key] public Guid Id { get; set; }

        public DownloadLink()
        {
            
        }
        public DownloadLink(string audioLink, string videoLink, Quality quality)
        {
            Id = Guid.NewGuid();
            AddedTime = DateTime.Now; //todo utc?
            AudioLink = audioLink;
            VideoLink = videoLink;
            Quality = quality;
            
        }
    }
}