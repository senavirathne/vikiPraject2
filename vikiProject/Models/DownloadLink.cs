using System;
using System.ComponentModel.DataAnnotations;
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
        [Required] public string AudioLink { get; set; }
        [Required] public string VideoLink { get; set; }
        [Required] public Quality Quality { get; set; }

        [Key] public Guid Id { get; set; }

        public DownloadLink(string audioLink, string videoLink, Quality quality)
        {
            Id = Guid.NewGuid();
            AudioLink = audioLink;
            VideoLink = videoLink;
            Quality = quality;
        }
    }
}