using System.Threading.Tasks;

namespace Test.Models
{
	public class CodeForcesProblemApi
	{
		public static async Task<string> SubmitAsync<T>(T Obj)
		{
			string submitUrl = "http://127.0.0.1:5000/problem/submit";
			var result = await Api.PostAsync(submitUrl, Obj);
			return result;
		}

		public static async Task<string> GetTimeAsync<T>(T Obj)
		{
			string getTimeUrl = "http://127.0.0.1:5000/problem/get_time";
			var result = await Api.PostAsync(getTimeUrl, Obj);
			return result;
		}

		public static async Task<string> GetMemoryAsync<T>(T Obj)
		{
			string getMemoryUrl = "http://127.0.0.1:5000/problem/get_memory";
			var result = await Api.PostAsync(getMemoryUrl, Obj);
			return result;
		}

		public static async Task<string> GetVerdictAsync<T>(T Obj)
		{
			string getVerdictUrl = "http://127.0.0.1:5000/problem/get_verdict";

			var submission_verdict = "";
			while (!ReachedVerdict(submission_verdict))
			{
				submission_verdict = await Api.PostAsync(getVerdictUrl, Obj);
			}
			return submission_verdict;
		}

		private static bool ReachedVerdict(string status)
		{
			return status.Length > 0 && status != "In queue" && status.Substring(0, 7) != "Running";
		}
	}
}