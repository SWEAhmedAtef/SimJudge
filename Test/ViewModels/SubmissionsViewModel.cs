using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.Models;

namespace Test.ViewModels
{
	public class SubmissionsViewModel
	{
		public IEnumerable<Language> Languages { get; set; }

		public string ProblemName { get; set; }

		public string SourceProblemId { get; set; }

		[Required]
		public int ProblemId { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		[Required]
		public int LanguageId { get; set; }

		public DateTime SubmissionTime { get; set; }

		[MaxLength(100)]
		public string Result { get; set; }

		[Required]
		public string Code { get; set; }
	}
}