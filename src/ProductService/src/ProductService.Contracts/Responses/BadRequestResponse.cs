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
public sealed record BadRequestResponse
{
    public BadRequestResponse(List<ValidationResponse> errors)
    {
        Errors = errors;
    }

    public BadRequestResponse(ValidationResult validationResult)
    {
        Errors = validationResult.Errors.Select(e => new ValidationResponse
        {
            PropertyName = e.PropertyName,
            ErrorMessage = e.ErrorMessage,
            AttemptedValue = e.AttemptedValue,
            FormattedMessagePlaceholderValues = e.FormattedMessagePlaceholderValues
        }).ToList();
    }

    public List<ValidationResponse> Errors { get; set; }

    public static BadRequestResponse With(ValidationResult validationResult)
    {
        return new BadRequestResponse(validationResult);
    }
}
