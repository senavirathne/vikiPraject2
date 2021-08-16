using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace vikiProject.Models
{
    public class Drama 
    {
        [Key] public Guid Id { get; set; }
        [Required] public string ImageSource { get; set; }
        [Required] public string MainName { get; set; } // @ todo set name to p.Key
        // public List<string> OtherNames { get; set; }
        [Required] public int NoOfEpisodes { get; set; }


        // public int Priority { get; set; }
        public List<Episode> Episodes { get; set; }
    }
}