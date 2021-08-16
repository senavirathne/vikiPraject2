using System;
using System.ComponentModel.DataAnnotations;

namespace vikiProject.Models
{
    public class DownloadLink
    {
        public string AudioLink { get; set; }
        public string VideoLink { get; set; }
        public DateTime AddedTime { get; set; }
        [Key]
        public Guid Id { get; set; }

        public DownloadLink(string audioLink, string videoLink)
        {
            
            Id = Guid.NewGuid();
            AudioLink = audioLink;
            VideoLink = videoLink;
            AddedTime = DateTime.UtcNow; //dateTimeUtc @todo
        }
    }
}