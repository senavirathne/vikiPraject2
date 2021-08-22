using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vikiProject.Models
{
    public class Drama
    {
        
        public int DramaId { get;  }

         public string ImageSource { get; }

     public string MainName { get;  } // @ todo set name to p.Key

        // public List<string> OtherNames { get; set; }
         public int NoOfEpisodes { get; }


        // public int Priority { get; set; }
        public List<Episode> Episodes { get; set; } = new();
        public List<OtherName> OtherNames { get; set; } = new();

        public Drama(int dramaId)
        {
            DramaId = dramaId;
        }
        public Drama(string imageSource, string mainName, int noOfEpisodes, int dramaId)
        {
            ImageSource = imageSource;
            MainName = mainName;
            NoOfEpisodes = noOfEpisodes;
            DramaId = dramaId;
        }
    }
}