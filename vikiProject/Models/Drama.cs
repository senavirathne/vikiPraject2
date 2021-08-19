using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vikiProject.Models
{
    public class Drama
    {
        
        public int DramaId { get; set; }

         public string ImageSource { get; }

     public string MainName { get;  } // @ todo set name to p.Key

        // public List<string> OtherNames { get; set; }
         public int NoOfEpisodes { get; }


        // public int Priority { get; set; }
        public List<Episode> Episodes { get; set; } = new();

        public Drama()
        {
            
        }
        public Drama(string imageSource, string mainName, int noOfEpisodes)
        {
            ImageSource = imageSource;
            MainName = mainName;
            NoOfEpisodes = noOfEpisodes;
           
        }
    }
}