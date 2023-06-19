namespace dotKnowledge.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; } = "Category Name";
        public int ArticlesCount { get; set; }

        public ICollection<Article> Articles { get; }

    }
}