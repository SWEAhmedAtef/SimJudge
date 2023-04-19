using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers.Apis
{
	public class ContestStatisticsController : ApiController
	{
		private ApplicationDbContext _context;
		public ContestStatisticsController()
		{
			_context = new ApplicationDbContext();
		}

		[AllowAnonymous]
		public IHttpActionResult GetContestStatistics(int id)
		{
			var contestUserStatistics = _context.ContestUserStatistics
				.Where(c => c.ContestId == id)
				.Include(c => c.ApplicationUser)
				.ToList();


			var users = _context.ContestUsers
				.Where(c => c.ContestId == id)
				.Include(c => c.Contest)
				.ToList();

			var problems = _context.ContestProblems
				.Where(c => c.ContestId == id)
				.ToList();

			var StatisticViewModel = new ContestUserStatisticsViewModel()
			{
				ContestUserStatistics = contestUserStatistics,
				ContestUsers = users,
				ContestProblems = problems
			};
			return Ok(StatisticViewModel); ;
		}
	}
}
