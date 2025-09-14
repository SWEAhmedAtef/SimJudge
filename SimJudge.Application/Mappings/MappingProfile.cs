using AutoMapper;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Entities;

namespace SimJudge.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Problem mappings
            CreateMap<Problem, ProblemDto>()
                .ForMember(dest => dest.SourceWebsiteName, opt => opt.MapFrom(src => src.SourceWebsite != null ? src.SourceWebsite.Name : string.Empty));
            CreateMap<ProblemDto, Problem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let database auto-increment
                .ForMember(dest => dest.SourceWebsite, opt => opt.Ignore())
                .ForMember(dest => dest.ProblemDetails, opt => opt.Ignore())
                .ForMember(dest => dest.CodeForcesDetails, opt => opt.Ignore())
                .ForMember(dest => dest.SimJudgeDetails, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // ProblemDetails mappings
            CreateMap<ProblemDetails, ProblemDetailsDto>();
            CreateMap<ProblemDetailsDto, ProblemDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let database auto-increment
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // CodeForcesDetails mappings
            CreateMap<CodeForcesDetails, CodeForcesDetailsDto>();
            CreateMap<CodeForcesDetailsDto, CodeForcesDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let database auto-increment
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // SimJudgeDetails mappings
            CreateMap<SimJudgeDetails, SimJudgeDetailsDto>();
            CreateMap<SimJudgeDetailsDto, SimJudgeDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let database auto-increment
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Contest mappings
            CreateMap<Contest, ContestDto>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.NickName : string.Empty));
            CreateMap<ContestDto, Contest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let database auto-increment
                .ForMember(dest => dest.Creator, opt => opt.Ignore())
                .ForMember(dest => dest.ContestProblems, opt => opt.Ignore())
                .ForMember(dest => dest.ContestUsers, opt => opt.Ignore())
                .ForMember(dest => dest.Submissions, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Submission mappings
            CreateMap<Submission, SubmissionDto>()
                .ForMember(dest => dest.ProblemName, opt => opt.MapFrom(src => src.Problem != null ? src.Problem.Name : string.Empty))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.NickName : string.Empty))
                .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.Language != null ? src.Language.Name : string.Empty))
                .ForMember(dest => dest.ContestName, opt => opt.MapFrom(src => src.Contest != null ? src.Contest.Name : null));
            CreateMap<SubmissionDto, Submission>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let database auto-increment
                .ForMember(dest => dest.Problem, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Language, opt => opt.Ignore())
                .ForMember(dest => dest.Contest, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let database auto-increment
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Create DTOs mappings
            CreateMap<CreateContestDto, Contest>()
                .ForMember(dest => dest.Creator, opt => opt.Ignore())
                .ForMember(dest => dest.ContestProblems, opt => opt.Ignore())
                .ForMember(dest => dest.ContestUsers, opt => opt.Ignore())
                .ForMember(dest => dest.Submissions, opt => opt.Ignore());

            CreateMap<CreateProblemDto, Problem>()
                .ForMember(dest => dest.SourceWebsite, opt => opt.Ignore())
                .ForMember(dest => dest.ProblemDetails, opt => opt.Ignore())
                .ForMember(dest => dest.CodeForcesDetails, opt => opt.Ignore())
                .ForMember(dest => dest.SimJudgeDetails, opt => opt.Ignore());

            CreateMap<CreateSubmissionDto, Submission>()
                .ForMember(dest => dest.Problem, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Language, opt => opt.Ignore())
                .ForMember(dest => dest.Contest, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>();
        }
    }
}
