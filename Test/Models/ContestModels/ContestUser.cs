namespace Test.Models
{
	public class ContestUser
	{
		public ContestUser()
		{
			SolvedProblems = 0;
			Penalty = 0;
		}

		public int Id { get; set; }

		public Contest Contest { get; set; }
		
		public int ContestId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }
		
		public string ApplicationUserId { get; set; }

		public int Penalty { get; set; }

		public int SolvedProblems { get; set; }
	}
}