using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;
using Test.Dtos;
using Test.Models;
namespace Test.Controllers.Apis
{

	public class ProblemsController : ApiController
	{
		private ApplicationDbContext _context;
		public ProblemsController()
		{
			_context = new ApplicationDbContext();
		}


		[AllowAnonymous]
		public IHttpActionResult GetProblem(string query = null)
		{
			var problemsQuery = _context.Problems
				.Include(m => m.SourceWebsite);

			if (!String.IsNullOrWhiteSpace(query))
				problemsQuery = problemsQuery.Where(c => c.Name.Contains(query));

			var problemDtos = problemsQuery
				.ToList()
				.Select(Mapper.Map<Problem, ProblemDto>);

			return Ok(problemDtos);
		}

		[AllowAnonymous]
		public IHttpActionResult GetProblem(int id)
		{
			var problem = _context.Problems.SingleOrDefault(p => p.Id == id);

			if (problem == null)
				return NotFound();

			return Ok(Mapper.Map<Problem, ProblemDto>(problem));
		}

		[HttpPost]
		public IHttpActionResult CreateProblem(ProblemDto problemDtos)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var problem = Mapper.Map<ProblemDto, Problem>(problemDtos);
			_context.Problems.Add(problem);
			_context.SaveChanges();

			return Created(new Uri(Request.RequestUri + "/" + problem.Id), problemDtos);
		}

		public IHttpActionResult UpdateProblem(int id, ProblemDto problemDtos)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var problemInDb = _context.Problems.SingleOrDefault(p => p.Id == id);

			if (problemInDb == null)
				return NotFound();
			Mapper.Map(problemDtos, problemInDb);
			_context.SaveChanges();
			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult DeleteProblem(int id)
		{
			var problemInDb = _context.Problems.SingleOrDefault(p => p.Id == id);

			if (problemInDb == null)
				return NotFound();
			_context.Problems.Remove(problemInDb);
			_context.SaveChanges();
			return Ok();
		}
	}
}
