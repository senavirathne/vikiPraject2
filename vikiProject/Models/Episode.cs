using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vikiProject.Models
{
    public class Episode
    {
        [Key] public Guid Id { get; set; }
        [Required] public string ImageSource { get; set; }
        [Required] public int EpisodeNumber { get; set; }
        [Required] public string EpisodeSource { get; set; }

        public DownloadLink DownloadLink { get; set; }
        [Required] public Drama Drama { get; set; }
    }
}