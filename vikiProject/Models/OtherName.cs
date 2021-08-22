namespace vikiProject.Models
{
    public class OtherName
    {
        public OtherName()
        {
            
        }
        public OtherName(int dramaId, string name)
        {
            DramaId = dramaId;
            Name = name;
        }

        public int NameId { get; set; }
        public int DramaId { get; set; }
        public string Name { get; set; }
        public Drama Drama { get; set; }

        
    }
}