using System;

namespace vikiProject.Models
{
    public class DownloadLink
    {
        public string AudioLink { get; set; }
        public string VideoLink { get; set; }
        public DateTime AddedTime { get; set; }

        public DownloadLink(string audioLink, string videoLink)
        {
            AudioLink = audioLink;
            VideoLink = videoLink;
            AddedTime = DateTime.UtcNow; //dateTimeUtc @todo
        }
    }
}