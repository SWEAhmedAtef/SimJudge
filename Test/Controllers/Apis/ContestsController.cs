
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Test.Dtos;
using Test.Models;

namespace Test.Controllers.Apis
{
	public class ContestsController : ApiController
	{
		private ApplicationDbContext _context;
		public ContestsController()
		{
			_context = new ApplicationDbContext();
		}

		//Get/api/contests
		[AllowAnonymous]
		public IEnumerable<ContestDto> GetContest()
		{
			return _context.Contests.ToList().Select(Mapper.Map<Contest, ContestDto>);
		}

		//Get/api/contests/1
		[AllowAnonymous]
		public IHttpActionResult GetContest(int id)
		{
			var contest = _context.Contests.SingleOrDefault(c => c.Id == id);
			if (contest == null)
				return NotFound();
			return Ok(Mapper.Map<Contest, ContestDto>(contest)); ;
		}

		//Post/api/contests
		[HttpPost]
		public IHttpActionResult PostContest(ContestDto contestDto)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var contest = Mapper.Map<ContestDto, Contest>(contestDto);
			_context.Contests.Add(contest);
			_context.SaveChanges();

			return Created(new Uri(Request.RequestUri + "/" + contest.Id), contestDto);
		}
	}
}
