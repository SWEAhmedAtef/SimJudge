using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class Language
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Value { get; set; }

		public SourceWebsite SourceWebsite { get; set; }

		public int? SourceWebsiteId { get; set; }
	}
}