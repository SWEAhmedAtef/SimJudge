using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Test.ViewModels
{
	public class CompilerViewModel
	{
		[MaxLength(5000)]
		[AllowHtml]
		public string Code { get; set; }
       
        public string Result { get; set; }
	}
}