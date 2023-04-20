namespace ProductService.Contracts.Responses;

public class ValidationResponse
{
    /// <summary>
    ///     The name of the property.
    /// </summary>
    public string PropertyName { get; set; } = null!;

    /// <summary>
    ///     The error message
    /// </summary>
    public string ErrorMessage { get; set; } = null!;

    /// <summary>
    ///     The property value that caused the failure.
    /// </summary>
    public object AttemptedValue { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the formatted message placeholder values.
    /// </summary>
    public Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; } = null!;

    /// <summary>
    ///     Creates a textual representation of the failure.
    /// </summary>
    public override string ToString()
    {
        return ErrorMessage;
    }
}
