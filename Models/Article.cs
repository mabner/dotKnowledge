using Microsoft.EntityFrameworkCore;

namespace dotKnowledge.Models
{
	public class Article
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public int AuthorId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
		public string? Title { get; set; }
		public string? Content { get; set; }
		public int Views { get; set; }
		public bool IsActive { get; set; } = true;
	}
}