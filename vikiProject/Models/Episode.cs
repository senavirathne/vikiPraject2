using System;

namespace vikiProject.Models
{
    public class Episode
    {
        public Guid Id { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeSource { get; set; }
        public string ImageSource { get; set; }
        public string AudioLink { get; set; }
        public string VideoLink { get; set; }
        public int DramaId { get; set; } //<==
        
    }
} 