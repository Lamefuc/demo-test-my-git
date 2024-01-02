namespace Reader.Models
{
    public class ViewCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }
        public string? Thumb { get; set; }
        public bool HomeFlag { get; set; }

        public List<ViewPost> Posts { get; set; }

    }
}
