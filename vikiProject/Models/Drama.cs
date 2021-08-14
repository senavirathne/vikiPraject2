using System;

namespace vikiProject.Models
{
    public class Drama
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NoOfEpisodes { get; set; }
        public string ImageSource { get; set; }
        public int Priority { get; set; }
    }
}