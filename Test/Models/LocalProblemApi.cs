using System.Threading.Tasks;

namespace Test.Models
{
	public class LocalProblemApi
	{
		public static async Task<string> SubmitAsync<T>(T Obj)
		{
			string submitUrl = "http://127.0.0.1:5000/combo-box";
			var result = await Api.PostAsync(submitUrl, Obj);
			return result;
		}
	}
}