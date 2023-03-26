// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#region

using FluentValidation.Results;

#endregion

namespace ProductService.Contracts.Responses;

/// <summary>
///     BadRequestResponse
/// </summary>
public sealed class BadRequestResponse
{
    public BadRequestResponse(List<ValidationResponse> errors)
    {
        Errors = errors;
    }
    public BadRequestResponse(IEnumerable<ValidationFailure> validationFailures)
    {
        ParseFailures(validationFailures);
    }
    public BadRequestResponse(ValidationResult validationResult)
    {
        ParseFailures(validationResult.Errors);
    }

    public BadRequestResponse()
    {
        Errors = new List<ValidationResponse>();
    }
    private void ParseFailures(IEnumerable<ValidationFailure> validationFailures)
    {
        Errors = validationFailures.Select(e => new ValidationResponse
        {
            PropertyName = e.PropertyName,
            ErrorMessage = e.ErrorMessage,
            AttemptedValue = e.AttemptedValue,
            FormattedMessagePlaceholderValues = e.FormattedMessagePlaceholderValues
        }).ToList();
    }
    public List<ValidationResponse> Errors { get; set; } = null!;
}
