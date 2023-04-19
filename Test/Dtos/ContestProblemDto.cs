using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.Dtos
{
	public class ContestProblemDto
	{
		public int ContestId { get; set; }

		[Required]
		[MaxLength(1)]
		public string ProblemIndex { get; set; }

		public string ProblemNickName { get; set; }

		public List<int> ProblemIds { get; set; }
	}
}