namespace dotKnowledge.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; } = "Article Name";
        public string Content { get; set; } = "WIP";
        public int ArticlesCount { get; set; }

    }
}