using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
	public class CodeForcesDetails
	{
		public int Id { get; set; }

		[Url]
		public string SourceUrl { get; set; }

		public string ContestSource { get; set; }

		[Required]
		public string Examples { get; set; }

		public string Tags { get; set; }

		public string InputFile { get; set; }

		public string OutputFile { get; set; }

		public string Editorial { get; set; }
	}
}