using System.Collections.Generic;
using Test.Models;

namespace Test.ViewModels
{
	public class BlogViewModel
	{
		public Blog Blog { get; set; }

		public IEnumerable<BlogComment> BlogComments { get; set; }

		public BlogComment BlogComment { get; set; }
	}
}