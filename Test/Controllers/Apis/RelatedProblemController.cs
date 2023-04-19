using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Test.Dtos;
using Test.Models;

namespace Test.Controllers.Apis
{
	public class RelatedProblemController : ApiController
	{
		private ApplicationDbContext _context;

		public RelatedProblemController()
		{
			_context = new ApplicationDbContext();
		}

		[Authorize(Roles = "IsAdmin")]
		[HttpPost]
		public IHttpActionResult AddRelatedProblem(TutorialRelatedProblemDto tutorialRelatedProblemDto)
		{
			var tutorial = _context.Tutorials
				.Include(t => t.TutorialDetails)
				.SingleOrDefault(t => t.Id == tutorialRelatedProblemDto.TutorialId);

			tutorial.TutorialDetails.RelatedProblemId = tutorialRelatedProblemDto.ProblemId;

			_context.SaveChanges();
			return Ok();
		}
	}
}