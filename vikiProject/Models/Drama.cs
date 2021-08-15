using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vikiProject.Models
{
    public class Drama 
    {
        [Key] public Guid Id { get; set; }
        [Required] public string ImageSource { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int NoOfEpisodes { get; set; }


        public int Priority { get; set; }
        public List<Episode> Episodes { get; set; }
    }
}