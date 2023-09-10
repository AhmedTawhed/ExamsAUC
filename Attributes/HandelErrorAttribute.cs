using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.FileProviders.Physical;

namespace MVCExamProject.Attributes
{
	public class HandelErrorAttribute : Attribute, IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			ViewResult viewResult = new ViewResult();
			viewResult.ViewName = "Error";
            context.Result = viewResult;
		}
	}
}
