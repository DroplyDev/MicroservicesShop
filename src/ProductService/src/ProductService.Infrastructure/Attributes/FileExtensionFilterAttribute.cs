using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Attributes;
public class FileExtensionFilterAttribute : ActionFilterAttribute
{
	private readonly string[] _allowedExtensions;

	public FileExtensionFilterAttribute(params string[] extensions)
	{
		_allowedExtensions = extensions;
	}
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		var file = context.HttpContext.Request.Form.Files.FirstOrDefault();
		if (file == null)
			return;
		var fileExtension = Path.GetExtension(file.FileName).ToLower();
		if (_allowedExtensions.Contains(fileExtension))
			return;

		context.ModelState.AddModelError("File", "Invalid file extension.");
		context.Result = new BadRequestObjectResult(context.ModelState);
	}
}