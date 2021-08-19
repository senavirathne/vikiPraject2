using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace vikiProject.Models
{
    public class EpisodeId
    {
        [Key]
        public int EpisodeNumber { get; set; }
        [ForeignKey("Drama")]
        public int DramaId { get; set; }
    }
}