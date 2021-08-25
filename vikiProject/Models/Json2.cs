// using System;
// using System.Collections.Generic;
//
// namespace vikiProject.Models
// {
//     // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
//   
//    
//     public class Titles
//     {
//         public string pt { get; set; }
//         public string sr { get; set; }
//         public string zh { get; set; }
//         public string zt { get; set; }
//         public string da { get; set; }
//         public string vi { get; set; }
//         public string nl { get; set; }
//         public string et { get; set; }
//         public string id { get; set; }
//         public string hu { get; set; }
//         public string sq { get; set; }
//         public string it { get; set; }
//         public string de { get; set; }
//         public string pl { get; set; }
//         public string lt { get; set; }
//         public string tr { get; set; }
//         public string el { get; set; }
//         public string fr { get; set; }
//         public string es { get; set; }
//         public string ko { get; set; }
//         public string en { get; set; }
//         public string ar { get; set; }
//         public string sk { get; set; }
//         public string th { get; set; }
//         public string hi { get; set; }
//         public string tl { get; set; }
//     }
//
//   
//    
//
//     public class SubtitleCompletions
//     {
//         public int ar { get; set; }
//         public int bg { get; set; }
//         public int ca { get; set; }
//         public int cs { get; set; }
//         public int da { get; set; }
//         public int de { get; set; }
//         public int el { get; set; }
//         public int en { get; set; }
//         public int es { get; set; }
//         public int et { get; set; }
//         public int fa { get; set; }
//         public int fi { get; set; }
//         public int fr { get; set; }
//         public int he { get; set; }
//         public int hi { get; set; }
//         public int hm { get; set; }
//         public int hr { get; set; }
//         public int hu { get; set; }
//         public int id { get; set; }
//         public int it { get; set; }
//         public int ja { get; set; }
//         public int ka { get; set; }
//         public int km { get; set; }
//         public int ko { get; set; }
//         public int ku { get; set; }
//         public int ky { get; set; }
//         public int la { get; set; }
//         public int lt { get; set; }
//         public int lv { get; set; }
//         public int mh { get; set; }
//         public int mk { get; set; }
//         public int mo { get; set; }
//         public int ms { get; set; }
//         public int nl { get; set; }
//         public int no { get; set; }
//         public int pl { get; set; }
//         public int pt { get; set; }
//         public int ro { get; set; }
//         public int ru { get; set; }
//         public int sk { get; set; }
//         public int sl { get; set; }
//         public int so { get; set; }
//         public int sq { get; set; }
//         public int sr { get; set; }
//         public int sv { get; set; }
//         public int ta { get; set; }
//         public int te { get; set; }
//         public int th { get; set; }
//         public int tl { get; set; }
//         public int tr { get; set; }
//         public int uk { get; set; }
//         public int ur { get; set; }
//         public int uz { get; set; }
//         public int vi { get; set; }
//         public int zh { get; set; }
//         public int zt { get; set; }
//         public int? bn { get; set; }
//         public int? cr { get; set; }
//         public int? na { get; set; }
//         public int? sh { get; set; }
//         public int? jv { get; set; }
//         public int? sw { get; set; }
//         public int? ab { get; set; }
//         public int? sb { get; set; }
//         public int? aa { get; set; }
//         public int? cy { get; set; }
//         public int? lo { get; set; }
//         public int? eo { get; set; }
//         public int? mu { get; set; }
//         public int? my { get; set; }
//         public int? fy { get; set; }
//         public int? ay { get; set; }
//         public int? bs { get; set; }
//         public int? nh { get; set; }
//         public int? or { get; set; }
//         public int? sn { get; set; }
//         public int? tw { get; set; }
//         public int? tq { get; set; }
//     }
//
//    
//     public class Images
//     {
//      
//         public Poster poster { get; set; }
//     }
//
//     public class Url
//     {
//         public string web { get; set; }
//         public string api { get; set; }
//         public string fb { get; set; }
//     }
//
//
//     public class Poster
//     {
//         public string url { get; set; }
//         public string source { get; set; }
//     }
//
//    
//     public class Container
//     {
//         public string id { get; set; }
//         public string type { get; set; }
//         public string subtype { get; set; }
//         public Titles titles { get; set; }
//         public string team_name { get; set; }
//         public List<string> genres { get; set; }
//       
//         public Images images { get; set; }
//         public System.Security.Policy.Url url { get; set; }
//     
//         public int planned_episodes { get; set; }
//     }
//
//  
//
//     
//
//     
//
//     public class Response
//     {
//         public string id { get; set; }
//     
//         public DateTime created_at { get; set; }
//         public DateTime updated_at { get; set; }
//         public string type { get; set; }
//         public int duration { get; set; }
//         public int number { get; set; }
//         public string root_id { get; set; }
//      
//         public Titles titles { get; set; }
//        
//         public string kaltura_id { get; set; }
//        
//         public SubtitleCompletions subtitle_completions { get; set; }
//         public System.ComponentModel.Container container { get; set; }
//         public List<object> hardsubs { get; set; }
//         public List<object> hardsub_languages { get; set; }
//         public string source { get; set; }
//         public Images images { get; set; }
//         
//         public System.Security.Policy.Url url { get; set; }
//         
//         public int credits_marker { get; set; }
//         public int part_index { get; set; }
//         public string author { get; set; }
//         public string author_url { get; set; }
//         public bool blocked { get; set; }
//         
//         public string rating { get; set; }
//     }
//
//     public class Root
//     {
//         public bool more { get; set; }
//         public List<Response> response { get; set; }
//     }
//
//
// }