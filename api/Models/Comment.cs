
namespace api.Models
{
    public class Comment
    {
        public int Id { get; set;}
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StoclId { get; set; }
        //Navigation
        public Stock? Stock { get; set;}
    }
}