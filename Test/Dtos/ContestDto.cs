using System.ComponentModel.DataAnnotations;

namespace Test.Dtos
{
	public class ContestDto
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}