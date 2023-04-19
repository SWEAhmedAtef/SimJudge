using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.Models;

namespace Test.ViewModels
{
	public class ContestSubmissionViewModel
	{
		public IEnumerable<Language> Languages { get; set; }

		public int? SubmissionId { get; set; }

		public string ProblemName { get; set; }

		public string SourceProblemId { get; set; }

		[Required]
		public int ProblemId { get; set; }

		[Required]
		public int ProblemDetailsId { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		public int ContestId { get; set; }

		[Required]
		public int LanguageId { get; set; }

		public DateTime SubmissionTime { get; set; }

		[MaxLength(100)]
		public string Result { get; set; }

		public string Time { get; set; }

		public string Memory { get; set; }

		[Required]
		public string Code { get; set; }
	}
}