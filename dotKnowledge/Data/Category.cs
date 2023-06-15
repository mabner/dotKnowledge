using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace dotKnowledge.Data
{
	public class Category
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public int ParentId { get; set; }
		public int ArticleCount { get; set; }
	}
}