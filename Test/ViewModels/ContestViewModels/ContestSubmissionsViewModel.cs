using System.Collections.Generic;
using Test.Models;

namespace Test.ViewModels
{
	public class ContestSubmissionsViewModel
	{
		public IEnumerable<ContestSubmission> ContestSubmissions { get; set; }

		public string ContestName { get; set; }
	}
}