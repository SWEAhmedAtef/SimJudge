﻿using System.Web.Mvc;

namespace Test
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new RequireHttpsAttribute());
			filters.Add(new AuthorizeAttribute());
		}
	}
}
