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
        public DownloadLink()
        {
        }

        public DownloadLink(Quality quality)
        {
            Quality = quality;
        }

        public string AudioLink { get; set; }
        public string VideoLink { get; set; }
        public Quality Quality { get; }
        public DateTime AddedTime { get; set; }
        public Episode Episode { get; set; }
        public int EpisodeNumber { get; set; }
        public int DramaId { get; set; }
    }
}