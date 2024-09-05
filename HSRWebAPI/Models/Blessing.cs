namespace HSRWebAPI.Models
{
    public class Blessing
    {
        // A blessing contains: ID, Name, Image, Type(Path), Description
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public BlessingPath Path { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }

        public Blessing() { }
    }
}
