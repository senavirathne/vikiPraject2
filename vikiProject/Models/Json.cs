using System;
using System.Collections.Generic;

namespace vikiProject.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    public class Titles
    {
        public string hi { get; set; }
        public string en { get; set; }
    }


    // public class SubtitleCompletions
    // {
    //     public int ab { get; set; }
    //     public int af { get; set; }
    //     public int am { get; set; }
    //     public int ar { get; set; }
    //     public int bg { get; set; }
    //     public int bn { get; set; }
    //     public int bs { get; set; }
    //     public int ca { get; set; }
    //     public int cs { get; set; }
    //     public int da { get; set; }
    //     public int de { get; set; }
    //     public int el { get; set; }
    //     public int en { get; set; }
    //     public int es { get; set; }
    //     public int et { get; set; }
    //     public int eu { get; set; }
    //     public int fa { get; set; }
    //     public int fi { get; set; }
    //     public int fr { get; set; }
    //     public int ga { get; set; }
    //     public int gl { get; set; }
    //     public int gu { get; set; }
    //     public int he { get; set; }
    //     public int hi { get; set; }
    //     public int hm { get; set; }
    //     public int hr { get; set; }
    //     public int hu { get; set; }
    //     public int id { get; set; }
    //     public int @is { get; set; }
    //     public int it { get; set; }
    //     public int ja { get; set; }
    //     public int jv { get; set; }
    //     public int ka { get; set; }
    //     public int kk { get; set; }
    //     public int km { get; set; }
    //     public int kn { get; set; }
    //     public int ko { get; set; }
    //     public int ku { get; set; }
    //     public int la { get; set; }
    //     public int lol { get; set; }
    //     public int lt { get; set; }
    //     public int lv { get; set; }
    //     public int ml { get; set; }
    //     public int mn { get; set; }
    //     public int mr { get; set; }
    //     public int ms { get; set; }
    //     public int mt { get; set; }
    //     public int mu { get; set; }
    //     public int my { get; set; }
    //     public int ne { get; set; }
    //     public int nl { get; set; }
    //     public int no { get; set; }
    //     public int or { get; set; }
    //     public int pl { get; set; }
    //     public int pt { get; set; }
    //     public int ro { get; set; }
    //     public int ru { get; set; }
    //     public int sk { get; set; }
    //     public int sl { get; set; }
    //     public int sm { get; set; }
    //     public int sq { get; set; }
    //     public int sr { get; set; }
    //     public int su { get; set; }
    //     public int sv { get; set; }
    //     public int ta { get; set; }
    //     public int te { get; set; }
    //     public int th { get; set; }
    //     public int tl { get; set; }
    //     public int tr { get; set; }
    //     public int udm { get; set; }
    //     public int ur { get; set; }
    //     public int uz { get; set; }
    //     public int vi { get; set; }
    //     public int yo { get; set; }
    //     public int zh { get; set; }
    //     public int zt { get; set; }
    //     public int? to { get; set; }
    //     public int? sh { get; set; }
    //     public int? ceb { get; set; }
    //     public int? ky { get; set; }
    //     public int? uk { get; set; }
    //     public int? lo { get; set; }
    //     public int? so { get; set; }
    // }


    public class Images
    {
        public Poster poster { get; set; }
    }

    public class Url
    {
        public string web { get; set; }
    }


    public class Poster
    {
        public string url { get; set; }
    }


    public class Container
    {
        public Titles titles { get; set; }


        public Images images { get; set; }
        public Url url { get; set; }

        public int planned_episodes { get; set; }
    }


    public class Response
    {
        public int number { get; set; }

        public Titles titles { get; set; }
        
        // public SubtitleCompletions subtitle_completions { get; set; }
        public Container container { get; set; }

        public Images images { get; set; }

        public Url url { get; set; }
    }

    public class Json
    {
        public bool more { get; set; }
        public List<Response> response { get; set; }
    }
}