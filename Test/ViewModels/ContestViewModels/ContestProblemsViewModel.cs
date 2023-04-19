using System.Collections.Generic;
using Test.Models;

namespace Test.ViewModels
{
	public class ContestProblemsViewModel
	{
		public ContestProblemsViewModel()
		{
			IsEnrolled = false;
		}

		public List<ContestProblem> ContestProblems { get; set; }

		public Contest Contest { get; set; }

		public bool IsEnrolled { get; set; }

		public int ContestId { get; set; }
	}
}