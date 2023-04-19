using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Test.Models
{
	public class Submission
	{
		public Submission()
		{
			Time = "0 ms";
			Memory = "0 KB";
		}


		[Key]
		public int Id { get; set; }

		public string SubmissionId { get; set; }

		public Problem Problem { set; get; }

		[Required]
		public int ProblemId { get; set; }

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

		public static async Task<Submission> ProcessSubmissionAsync(Submission submission, Problem problem, Language language)
		{
			submission = (problem.SourceWebsite.Name == Problem.LocalProblem)
				? await SubmitLocalProblem(submission, problem, language)
				: await SubmitCodeForcesProblem(submission, problem, language);

			submission.SubmissionTime = DateTime.Now;
			return submission;
		}

		private static async Task<Submission> SubmitCodeForcesProblem(Submission submission, Problem problem, Language language)
		{
			string contest_id = Problem.GetContestIdFromProblemId(problem.SourceProblemId);

			string submission_id = await CodeForcesProblemApi.SubmitAsync(new
			{
				problem_id = problem.SourceProblemId,
				code = submission.Code,
				language_id = language.Value
			});

			if (submission_id == "Duplicate")
			{
				submission.Result = "Duplicate Code on Source";
			}
			else
			{
				submission.SubmissionId = submission_id;

				var dataObject = new
				{
					submission_id,
					contest_id
				};
				submission.Result = await CodeForcesProblemApi.GetVerdictAsync(dataObject);
				submission.Time = await CodeForcesProblemApi.GetTimeAsync(dataObject);
				submission.Memory = await CodeForcesProblemApi.GetMemoryAsync(dataObject);
			}

			return submission;
		}

		private static async Task<Submission> SubmitLocalProblem(Submission submission, Problem problem, Language language)
		{
			submission.Result = await LocalProblemApi.SubmitAsync(new
			{
				code = submission.Code,
				input = problem.SimJudgeDetails.TestCaseInput,
				output = problem.SimJudgeDetails.TestCaseOutput,
				language = language.Value
			});

			return submission;
		}
	}
}