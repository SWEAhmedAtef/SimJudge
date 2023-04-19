using System.ComponentModel.DataAnnotations;
namespace Test.Models
{
	public class ValidDate : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var contest = (Contest)validationContext.ObjectInstance;
			if (contest.StartDate < contest.CreatedAt)
				return new ValidationResult("Please enter valid date");

			return (contest.EndDate > contest.StartDate) 
				? ValidationResult.Success 
				: new ValidationResult("End Date should be greater than Start Date");
		}
	}
}