using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.Models;

namespace Test.ViewModels
{
	public class TutorialViewModel
	{
		public Tutorial Tutorial { get; set; }

		public IEnumerable<TutorialComment> TutorialComments { get; set; }

		public TutorialComment TutorialComment { get; set; }
	}
}