using System;
using System.Collections.Generic;
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
        [Required]
        public DateTime AddedTime { get; set; }

        public List<DownloadLink> DownloadLinks { get; set; }
        [Required] public Drama Drama { get; set; }
    }
}