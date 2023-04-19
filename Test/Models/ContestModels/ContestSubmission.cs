using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Test.Models
{
	public class ContestSubmission
	{
		public ContestSubmission()
		{
			Time = "0 ms";
			Memory = "0 KB";
		}


		[Key]
		public int Id { get; set; }

		public Contest Contest { get; set; }

		public int ContestId { get; set; }

		public Problem Problem { set; get; }

		[Required]
		public int ProblemId { get; set; }

		public string SubmissionId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		public Language Language { get; set; }

		public int LanguageId { get; set; }

		public DateTime SubmissionTime { get; set; }

		[MaxLength(100)]
		public string Result { get; set; }

		public string Time { get; set; }

		public string Memory { get; set; }

		[Required]
		[AllowHtml]
		public string Code { get; set; }

		public static async Task<ContestSubmission> ProcessSubmissionAsync(ContestSubmission contestSubmission, Problem problem, Language language)
		{
			contestSubmission = (problem.SourceWebsite.Name == Problem.LocalProblem)
				? await SubmitLocalProblem(contestSubmission, problem, language)
				: await SubmitCodeForcesProblem(contestSubmission, problem, language);

			contestSubmission.SubmissionTime = DateTime.Now;
			return contestSubmission;
		}

		private static async Task<ContestSubmission> SubmitLocalProblem(ContestSubmission contestSubmission, Problem problem, Language language)
		{
			contestSubmission.Result = await LocalProblemApi.SubmitAsync(new
			{
				code = contestSubmission.Code,
				input = problem.SimJudgeDetails.TestCaseInput,
				output = problem.SimJudgeDetails.TestCaseOutput,
				language = language.Value
			});
			return contestSubmission;
		}

		private static async Task<ContestSubmission> SubmitCodeForcesProblem(ContestSubmission contestSubmission, Problem problem, Language language)
		{
			string contest_id = Problem.GetContestIdFromProblemId(problem.SourceProblemId);

			string submission_id = await CodeForcesProblemApi.SubmitAsync(new
			{
				problem_id = problem.SourceProblemId,
				code = contestSubmission.Code,
				language_id = language.Value
			});

			if (submission_id == "Duplicate")
			{
				contestSubmission.Result = "Duplicate Code on Source";
			}
			else
			{
				contestSubmission.SubmissionId = submission_id;

				var dataObject = new
				{
					submission_id,
					contest_id
				};
				contestSubmission.Result = await CodeForcesProblemApi.GetVerdictAsync(dataObject);
				contestSubmission.Time = await CodeForcesProblemApi.GetTimeAsync(dataObject);
				contestSubmission.Memory = await CodeForcesProblemApi.GetMemoryAsync(dataObject);
			}
			return contestSubmission;
		}
	}
}