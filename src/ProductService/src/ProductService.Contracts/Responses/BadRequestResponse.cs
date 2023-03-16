#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using static System.Runtime.InteropServices.JavaScript.JSType;


#endregion

namespace ProductService.Contracts.Responses;

/// <summary>
///     BadRequestResponse
/// </summary>
public sealed class BadRequestResponse
{
	public List<ValidationFailure> Errors { get; set; }

	public BadRequestResponse()
	{
		Errors = new List<ValidationFailure>();
	}
	public BadRequestResponse(List<ValidationFailure> errors)
	{
		Errors = errors;
	}
}