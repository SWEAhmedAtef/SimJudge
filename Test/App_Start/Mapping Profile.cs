using AutoMapper;
using Test.Dtos;
using Test.Models;

namespace Test.App_Start
{
	public class Mapping_Profile : Profile
	{
		public Mapping_Profile()
		{
			Mapper.CreateMap<Problem, ProblemDto>();
			Mapper.CreateMap<ProblemDto, Problem>();

			Mapper.CreateMap<ContestProblem, ContestProblemDto>();
			Mapper.CreateMap<ContestProblemDto, ContestProblem>();

			Mapper.CreateMap<Contest, ContestProblem>();
			Mapper.CreateMap<Contest, Contest>();
		}
	}
}