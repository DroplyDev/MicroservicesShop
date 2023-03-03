using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductService.Presentation;
public class KebabCaseControllerModelConvention : IControllerModelConvention
{
	public void Apply(ControllerModel controller)
	{
		// Convert the controller name to kebab-case
		string kebabCaseControllerName = ToKebabCase(controller.ControllerName);

		// Apply the kebab-case name to the controller's route template
		foreach (var attributeRouteModel in controller.Selectors.Select(s => s.AttributeRouteModel))
			attributeRouteModel!.Template = attributeRouteModel.Template!.Replace("[controller]", kebabCaseControllerName);
	}

	private static string ToKebabCase(string input)
	{
		// Replace upper case letters with hyphen + lower case letters
		return Regex.Replace(input, @"(\p{Lu})", "-$1").TrimStart('-').ToLowerInvariant();
	}
}
