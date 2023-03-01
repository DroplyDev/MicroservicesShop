using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Attributes;
public sealed class ImageExtensionFilterAttribute : FileExtensionFilterAttribute
{
	public ImageExtensionFilterAttribute() : base(".jpg", ".jpeg", ".png")
	{

	}

}
