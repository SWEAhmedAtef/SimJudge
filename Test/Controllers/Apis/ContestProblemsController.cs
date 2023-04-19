using AutoMapper;
using System.Linq;
using System.Web.Http;
using Test.Dtos;
using Test.Models;
namespace Test.Controllers.Apis
{
	public class ContestProblemsController : ApiController
	{
		private ApplicationDbContext _context;

		public ContestProblemsController()
		{
			_context = new ApplicationDbContext();
		}

		[AllowAnonymous]
		public IHttpActionResult GetContest()
		{
			return Ok(_context.ContestProblems.ToList().Select(Mapper.Map<ContestProblem, ContestProblemDto>));
		}

		[HttpPost]
		[AllowAnonymous]
		public IHttpActionResult AddProblem(ContestProblemDto contestProblem)
		{
			var contest = _context.Contests.Single(
				c => c.Id == contestProblem.ContestId);

			var contestProblems = _context.Problems
				.Where(m => contestProblem.ProblemIds.Contains(m.Id))
				.Distinct()
				.ToList();

			var problems = _context.ContestProblems
				.Where(m => m.ContestId == contestProblem.ContestId)
				.ToList();

			int problemCount = problems.Count();
			char problemIndex = (char)('A' + problemCount);


			foreach (var problem in contestProblems)
			{
				//add problem in contest
				var newproblem = new ContestProblem()
				{
					Contest = contest,
					Problem = problem,
					ProblemIndex = problemIndex++.ToString()
				};
				_context.ContestProblems.Add(newproblem);

				//for each user in contest add record for the problem
				var contestUsers = _context.ContestUsers
					.Where(u => u.ContestId == contest.Id)
					.ToList();
				
				foreach (var user in contestUsers)
				{	
					var stat = new ContestUserStatistics()
					{
						ApplicationUserId = user.ApplicationUserId,
						ContestId = contest.Id,
						ProblemId = problem.Id
					};
					_context.ContestUserStatistics.Add(stat);
				}
			}

			_context.SaveChanges();

			return Ok();
		}

	}
}