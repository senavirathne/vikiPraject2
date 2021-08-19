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
        public string AudioLink { get; }
        public string VideoLink { get; }
        public Quality Quality { get; }
        public DateTime AddedTime { get; }
        public Episode Episode { get; set; }
        public int EpisodeNumber { get; }
        public int DramaId { get; set; }

        public DownloadLink()
        {
        }

        public DownloadLink(string audioLink, string videoLink, Quality quality)
        {
            AddedTime = DateTime.Now; //todo utc?
            AudioLink = audioLink;
            VideoLink = videoLink;
            Quality = quality;
        }
    }
}