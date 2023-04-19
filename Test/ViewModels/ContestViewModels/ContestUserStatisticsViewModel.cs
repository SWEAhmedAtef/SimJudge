using System.Collections.Generic;
using Test.Models;

namespace Test.ViewModels
{
	public class ContestUserStatisticsViewModel
	{
		public IEnumerable<ContestUserStatistics> ContestUserStatistics { get; set; }

		public IEnumerable<ContestUser> ContestUsers { get; set; }

		public IEnumerable<ContestProblem> ContestProblems { get; set; }
	}
}